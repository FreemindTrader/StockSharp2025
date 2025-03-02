using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.Xpf.Layout.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Studio.Controls
{
    public sealed class LayoutManager : BaseLogReceiver
    {
        private readonly Dictionary<string, LayoutPanel> _panels = new Dictionary<string, LayoutPanel>();
        private readonly HashSet<IStudioControl> _controls = new HashSet<IStudioControl>();
        private readonly SynchronizedDictionary<IStudioControl, SettingsStorage> _dockingControlSettings = new SynchronizedDictionary<IStudioControl, SettingsStorage>();
        private readonly SynchronizedSet<IStudioControl> _changedControls = new CachedSynchronizedSet<IStudioControl>();
        private readonly DocumentGroup _documentGroup;
        private bool _isLayoutChanged;
        private string _layout;

        public DockLayoutManager DockingManager { get; }

        public IEnumerable<IStudioControl> DockingControls
        {
            get
            {
                return _controls.ToArray();
            }
        }

        public IDictionary<Type, IStudioControl> PredefinedControls { get; }

        public event Action Changed;

        public event Action LayoutChanged;

        public event Action<IStudioControl> ControlChanged;

        public event Action<IStudioControl> ControlRemoved;

        public LayoutManager( DockLayoutManager dockingManager, DocumentGroup documentGroup = null )
        {
            _documentGroup = documentGroup;
            DockLayoutManager dockLayoutManager = dockingManager;
            if ( dockLayoutManager == null )
                throw new ArgumentNullException( nameof( dockingManager ) );
            DockingManager = dockLayoutManager;
            DXSerializer.AddAllowPropertyHandler( DockingManager, ( s, e ) =>
                {
                    if ( e.DependencyProperty != BaseLayoutItem.CaptionProperty )
                        return;
                    e.Allow = false;
                } );
            DockingManager.DockItemClosing += new DockItemCancelEventHandler( OnDockingManagerDockItemClosing );
            DockingManager.DockItemClosed += new DockItemClosedEventHandler( OnDockingManagerDockItemClosed );
            DockingManager.DockItemHidden += new DockItemEventHandler( DockingManager_DockItemHidden );
            DockingManager.DockItemRestored += new DockItemEventHandler( DockingManager_DockItemRestored );
            DockingManager.DockItemExpanded += new DockItemExpandedEventHandler( DockingManager_DockItemExpanded );
            DockingManager.DockItemActivated += new DockItemActivatedEventHandler( DockingManager_DockItemActivated );
            DockingManager.DockItemEndDocking += new DockItemDockingEventHandler( DockingManager_DockItemEndDocking );
            DockingManager.LayoutItemSizeChanged += new LayoutItemSizeChangedEventHandler( DockingManager_LayoutItemSizeChanged );
            PredefinedControls = new Dictionary<Type, IStudioControl>();
        }

        public ContentItem ActivePanel
        {
            get
            {
                return ( ContentItem )DockingManager.DockController.ActiveItem;
            }
        }

        public IStudioControl ActiveControl
        {
            get
            {
                return ( IStudioControl )ActivePanel?.Content;
            }
        }

        public void Activate( IStudioControl control )
        {
            if ( control == null )
                throw new ArgumentNullException( nameof( control ) );
            LayoutPanel layoutPanel = _panels.TryGetValue( control.Key );
            if ( layoutPanel == null )
                return;
            DockingManager.DockController.Activate( layoutPanel );
        }

        public void Activate( Func<IStudioControl, bool> where )
        {
            if ( where == null )
                throw new ArgumentNullException( nameof( where ) );
            IStudioControl control = _controls.FirstOrDefault( where );
            if ( control == null )
                return;
            Activate( control );
        }

        private IStudioControl GetOrCreate( Type controlType )
        {
            if ( controlType == null )
                throw new ArgumentNullException( nameof( controlType ) );
            if ( !typeof( IStudioControl ).IsAssignableFrom( controlType ) )
                throw new ArgumentException( LocalizedStrings.TypeNotImplemented.Put( controlType.Name, "IStudioControl" ), nameof( controlType ) );
            IStudioControl studioControl;
            if ( !PredefinedControls.TryGetValue( controlType, out studioControl ) )
                return controlType.CreateInstance<IStudioControl>();
            return studioControl;
        }

        public IStudioControl OpenToolWithKey( Type controlType )
        {
            return OpenToolWindow( controlType, controlType.CreateKey(), true );
        }

        public IStudioControl OpenToolWindow( Type controlType, string key = null, bool canClose = true )
        {
            if ( key != null )
                return OpenToolWindow( key, controlType, () => GetOrCreate( controlType ), canClose );
            return OpenToolWindow( GetOrCreate( controlType ), canClose );
        }

        public IStudioControl OpenToolWindow( IStudioControl content, bool canClose = true )
        {
            if ( content == null )
                throw new ArgumentNullException( nameof( content ) );
            return OpenToolWindow( content.Key, content.GetType(), () => content, canClose );
        }

        public IStudioControl OpenToolWindow<T>(
          string key,
          Func<T> getControl,
          bool canClose = true )
          where T : IStudioControl
        {
            return OpenWindow( true, key, typeof( T ), () => getControl(), canClose );
        }

        private IStudioControl OpenToolWindow(
          string key,
          Type ctrlType,
          Func<IStudioControl> getControl,
          bool canClose = true )
        {
            return OpenWindow( true, key, ctrlType, getControl, canClose );
        }

        public IStudioControl OpenDocumentWithKey( Type controlType )
        {
            return OpenDocumentWindow( controlType, controlType.CreateKey(), true );
        }

        public IStudioControl OpenDocumentWindow(
          Type controlType,
          string key = null,
          bool canClose = true )
        {
            if ( key != null )
                return OpenDocumentWindow( key, controlType, () => GetOrCreate( controlType ), canClose );
            return OpenDocumentWindow( GetOrCreate( controlType ), canClose );
        }

        public IStudioControl OpenDocumentWindow( IStudioControl content, bool canClose = true )
        {
            if ( content == null )
                throw new ArgumentNullException( nameof( content ) );
            return OpenDocumentWindow( content.Key, content.GetType(), () => content, canClose );
        }

        public IStudioControl OpenDocumentWindow<T>(
          string key,
          Func<T> getControl,
          bool canClose = true )
          where T : IStudioControl
        {
            return OpenWindow( false, key, typeof( T ), () => getControl(), canClose );
        }

        private IStudioControl OpenDocumentWindow(
          string key,
          Type ctrlType,
          Func<IStudioControl> getControl,
          bool canClose = true )
        {
            return OpenWindow( false, key, ctrlType, getControl, canClose );
        }

        private IStudioControl OpenWindow<T>(
          bool isTool,
          string key,
          Func<T> getControl,
          bool canClose )
          where T : IStudioControl
        {
            return OpenWindow( isTool, key, typeof( T ), () => getControl(), canClose );
        }

        private IStudioControl OpenWindow(
          bool isTool,
          string key,
          Type ctrlType,
          Func<IStudioControl> getControl,
          bool canClose )
        {
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            if ( getControl == null )
                throw new ArgumentNullException( nameof( getControl ) );
            IDockController dockController = DockingManager.DockController;
            ctrlType.CreateKey();
            LayoutPanel layoutPanel = _panels.TryGetValue( key );
            if ( layoutPanel == null )
            {
                IStudioControl control = getControl();
                layoutPanel = isTool ? dockController.AddPanel( DockType.Right ) : dockController.AddDocumentPanel( _documentGroup );
                layoutPanel.Name = key;
                layoutPanel.Content = control;
                layoutPanel.ShowCloseButton = canClose;
                layoutPanel.Visibility = Visibility.Visible;
                SetHelpSourceUrl( layoutPanel, control );
                BindProperties( layoutPanel, control );
                _panels[key] = layoutPanel;
                _controls.Add( control );
                MarkControlChanged( control );
            }
            else
            {
                dockController.Restore( layoutPanel );
                layoutPanel.Visibility = Visibility.Visible;
            }
            dockController.Activate( layoutPanel );
            return ( IStudioControl )_panels[key].Content;
        }

        private LayoutPanel TryGetPanel( IStudioControl content )
        {
            return _panels.TryGetValue( content.Key );
        }

        public bool Remove( IStudioControl content )
        {
            if ( content == null )
                throw new ArgumentNullException( nameof( content ) );
            LayoutPanel panel = TryGetPanel( content );
            if ( panel == null )
                return false;
            panel.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
            if ( DockingManager.ClosedPanels.Contains( panel ) )
                DockingManager.DockController.Restore( panel );
            return DockingManager.DockController.Close( panel );
        }

        public void Remove( Func<IStudioControl, bool> where )
        {
            if ( where == null )
                throw new ArgumentNullException( nameof( where ) );
            IStudioControl content = _controls.FirstOrDefault( where );
            if ( content == null )
                return;
            Remove( content );
        }

        public void Clear()
        {
            foreach ( LayoutPanel layoutPanel in _panels.Values.Distinct().ToArray() )
            {
                layoutPanel.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                DockingManager.DockController.Close( layoutPanel );
            }
            _panels.Clear();
            _changedControls.Clear();
            _dockingControlSettings.Clear();
        }

        public override void Load( SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            Clear();
            foreach ( SettingsStorage storage1 in storage.GetValue<SettingsStorage[ ]>( "Controls", null ) )
                LoadControl( storage1 );
            _layout = storage.GetValue<string>( "Layout", null );
            if ( _layout.IsEmpty() )
                return;
            LoadLayout( _layout );
        }

        public void Save( SettingsStorage storage, bool force )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            IStudioControl[ ] studioControlArray = force ? _dockingControlSettings.SyncGet( d => d.Keys.ToArray() ) : _changedControls.SyncGet( c => c.CopyAndClear() );
            int num = _isLayoutChanged ? 1 : 0;
            _isLayoutChanged = false;
            if ( studioControlArray.Length != 0 )
                Save( studioControlArray );
            if ( num != 0 )
                _layout = SaveLayout();
            storage.SetValue( "Controls", _dockingControlSettings.SyncGet( c => c.Select( p => p.Value ).ToArray() ) );
            storage.SetValue( "Layout", _layout );
        }

        public override void Save( SettingsStorage storage )
        {
            Save( storage, false );
        }

        public void LoadLayout( string layout )
        {
            if ( layout == null )
                throw new ArgumentNullException( nameof( layout ) );
            try
            {
                Dictionary<string, string> dictionary = DockingManager.GetItems().OfType<LayoutPanel>().Where( c => !c.Name.IsEmpty() ).ToDictionary( c => c.Name, c => ( string )c.Caption );
                DockingManager.LoadLayout( layout );
                foreach ( LayoutPanel panel in DockingManager.GetItems().OfType<LayoutPanel>().ToArray() )
                {
                    _panels[panel.Name] = panel;
                    IStudioControl content = panel.Content as IStudioControl;
                    if ( content != null )
                    {
                        BindProperties( panel, content );
                    }
                    else
                    {
                        string str = dictionary.TryGetValue( panel.Name );
                        if ( !str.IsEmpty() )
                            panel.Caption = str;
                    }
                }
            }
            catch ( Exception ex )
            {
                this.AddErrorLog( ex, LocalizedStrings.Str3649 );
            }
        }

        public string SaveLayout()
        {
            try
            {
                return DockingManager.SaveLayout();
            }
            catch ( Exception ex )
            {
                this.AddErrorLog( ex, LocalizedStrings.Str3649 );
            }
            return string.Empty;
        }

        public void LoadControl( SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            try
            {
                IStudioControl studioControl = LoadBaseStudioControl( storage );
                int num = storage.GetValue( "IsToolWindow", true ) ? 1 : 0;
                _dockingControlSettings.Add( studioControl, storage );
                if ( num != 0 )
                    OpenToolWindow( studioControl, true );
                else
                    OpenDocumentWindow( studioControl, true );
            }
            catch ( Exception ex )
            {
                this.AddErrorLog( ex );
            }
        }

        public SettingsStorage SaveControl( IStudioControl control )
        {
            if ( control == null )
                throw new ArgumentNullException( nameof( control ) );
            return Save( control );
        }

        public void MarkControlChanged( IStudioControl control )
        {
            if ( !control.SaveWithLayout || !_controls.Contains( control ) )
                return;
            _changedControls.Add( control );
            Action changed = Changed;
            if ( changed != null )
                changed();
            Action<IStudioControl> controlChanged = ControlChanged;
            if ( controlChanged == null )
                return;
            controlChanged( control );
        }

        private void OnDockingManagerDockItemClosing( object sender, ItemCancelEventArgs e )
        {
            e.Cancel = OnDockingManagerItemClosing( e.Item );
        }

        private bool OnDockingManagerItemClosing( BaseLayoutItem item )
        {
            LayoutPanel layoutPanel = item as LayoutPanel;
            if ( layoutPanel != null )
            {
                BaseStudioControl content = layoutPanel.Content as BaseStudioControl;
                if ( content == null )
                {
                    item.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                    return false;
                }
                CloseAction closeAction;
                switch ( item.ClosingBehavior )
                {
                    case ClosingBehavior.Default:
                        closeAction = content.CanClose( CloseReason.CloseWindow );
                        break;
                    case ClosingBehavior.HideToClosedPanelsCollection:
                        closeAction = CloseAction.Hide;
                        break;
                    default:
                        closeAction = CloseAction.Close;
                        break;
                }
                switch ( closeAction )
                {
                    case CloseAction.StayOpen:
                        return true;
                    case CloseAction.Close:
                        item.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                        return false;
                    case CloseAction.Hide:
                        item.ClosingBehavior = ClosingBehavior.HideToClosedPanelsCollection;
                        item.Visibility = Visibility.Collapsed;
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                LayoutGroup layoutGroup = item as LayoutGroup;
                if ( layoutGroup != null )
                    return layoutGroup.Items.Any( new Func<BaseLayoutItem, bool>( OnDockingManagerItemClosing ) );
                return false;
            }
        }

        private void OnDockingManagerDockItemClosed( object sender, DockItemClosedEventArgs e )
        {
            foreach ( BaseLayoutItem affectedItem in e.AffectedItems )
            {
                if ( affectedItem.ClosingBehavior == ClosingBehavior.HideToClosedPanelsCollection )
                    LogHiddenPanel( affectedItem );
                else if ( affectedItem.ClosingBehavior == ClosingBehavior.ImmediatelyRemove )
                    OnDockingManagerItemClosed( affectedItem );
            }
            OnDockingChanged();
        }

        private void LogHiddenPanel( BaseLayoutItem item )
        {
            string str = item.Name;
            LayoutPanel layoutPanel = item as LayoutPanel;
            if ( layoutPanel != null )
            {
                BaseStudioControl content = layoutPanel.Content as BaseStudioControl;
                if ( content != null )
                    str = content.Title + " (" + item.Name + ", " + content.GetType().Name + ")";
            }
            this.AddWarningLog( "Panel " + str + " was hidden to closed panels" );
        }

        private void OnDockingManagerItemClosed( BaseLayoutItem item )
        {
            LayoutPanel panel = item as LayoutPanel;
            if ( panel != null )
            {
                IStudioControl content = panel.Content as IStudioControl;
                if ( content == null )
                    return;
                DisposeControl( content, CloseReason.CloseWindow );
                _panels.RemoveWhere( p => Equals( p.Value, panel ) );
                _controls.Remove( content );
                _changedControls.Remove( content );
                _dockingControlSettings.Remove( content );
                Action<IStudioControl> controlRemoved = ControlRemoved;
                if ( controlRemoved != null )
                    controlRemoved( content );
            }
            LayoutGroup layoutGroup = item as LayoutGroup;
            if ( layoutGroup == null )
                return;
            foreach ( BaseLayoutItem baseLayoutItem in ( Collection<BaseLayoutItem> )layoutGroup.Items )
                OnDockingManagerItemClosed( baseLayoutItem );
        }

        private void DockingManager_DockItemEndDocking( object sender, DockItemDockingEventArgs e )
        {
            OnDockingChanged();
        }

        private void DockingManager_DockItemActivated( object sender, DockItemActivatedEventArgs ea )
        {
            OnDockingChanged();
        }

        private void DockingManager_DockItemExpanded( object sender, DockItemExpandedEventArgs e )
        {
            OnDockingChanged();
        }

        private void DockingManager_DockItemRestored( object sender, ItemEventArgs e )
        {
            OnDockingChanged();
        }

        private void DockingManager_DockItemHidden( object sender, ItemEventArgs e )
        {
            OnDockingChanged();
        }

        private void DockingManager_LayoutItemSizeChanged(
          object sender,
          LayoutItemSizeChangedEventArgs e )
        {
            OnDockingChanged();
        }

        private void OnDockingChanged()
        {
            _isLayoutChanged = true;
            Action changed = Changed;
            if ( changed != null )
                changed();
            Action layoutChanged = LayoutChanged;
            if ( layoutChanged == null )
                return;
            layoutChanged();
        }

        private void Save( IEnumerable<IStudioControl> items )
        {
            foreach ( IStudioControl control in items )
            {
                if ( control.SaveWithLayout )
                    _dockingControlSettings[control] = Save( control );
            }
        }

        private SettingsStorage Save( IStudioControl control )
        {
            bool flag = TryGetPanel( control ) is DocumentPanel;
            SettingsStorage storage = new SettingsStorage();
            storage.SetValue( "ControlType", control.GetType().GetTypeName( false ) );
            storage.SetValue( "IsToolWindow", !flag );
            control.Save( storage );
            return storage;
        }

        private IStudioControl LoadBaseStudioControl( SettingsStorage settings )
        {
            string str = settings.GetValue<string>( "ControlType", null );
            if ( str.StartsWith( "StockSharp.Designer" ) && !str.Contains( "Panels" ) )
                str = str.Replace( "StockSharp.Designer", "StockSharp.Designer.Panels" );
            Type type = str.Replace( "StockSharp.Hydra.Server.UsersPane,", "StockSharp.Hydra.Panes.UsersPane," ).Replace( "StockSharp.Hydra.Server.SubscriptionsPane,", "StockSharp.Hydra.Panes.SubscriptionsPane," ).Replace( "StockSharp.Hydra.Server.SecurityMappingPane,", "StockSharp.Hydra.Panes.SecurityMappingPane," ).To<Type>();
            IStudioControl studioControl = PredefinedControls.TryGetValue( type ) ?? ( IStudioControl )Activator.CreateInstance( type );
            studioControl.Load( settings );
            return studioControl;
        }

        protected override void DisposeManaged()
        {
            foreach ( IStudioControl control in _controls )
                DisposeControl( control, CloseReason.Shutdown );
            base.DisposeManaged();
        }

        private void BindProperties( LayoutPanel panel, IStudioControl control )
        {
            panel.SetBindings( BaseLayoutItem.CaptionProperty, control, new PropertyPath( "(0)", new object[1]
            {
         typeof (IStudioControl).GetProperty("Title")
            } ), BindingMode.TwoWay, null, null );
            panel.SetBindings( BaseLayoutItem.AllowRenameProperty, control, new PropertyPath( "(0)", new object[1]
            {
         typeof (IStudioControl).GetProperty("IsTitleEditable")
            } ), BindingMode.OneWay, null, null );
        }

        private static void SetHelpSourceUrl( LayoutPanel document, IStudioControl control )
        {
            DocAttribute attribute = control.GetType().GetAttribute<DocAttribute>( true );
            if ( attribute == null )
            {
                string url = Doc.GetUrl( ( UIElement )control );
                if ( url.IsEmpty() )
                    return;
                Doc.SetUrl( document, url );
            }
            else
                Doc.SetUrl( document, attribute.DocUrl );
        }

        private static void DisposeControl( IStudioControl control, CloseReason reason )
        {
            BaseStudioControl baseStudioControl = control as BaseStudioControl;
            if ( baseStudioControl != null )
                baseStudioControl.Dispose( reason );
            else
                control.Dispose();
        }
    }
}
