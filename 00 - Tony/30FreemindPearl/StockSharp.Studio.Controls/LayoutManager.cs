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
        private readonly SynchronizedSet<IStudioControl> _changedControls = ( SynchronizedSet<IStudioControl> )new CachedSynchronizedSet<IStudioControl>();
        private readonly DocumentGroup _documentGroup;
        private bool _isLayoutChanged;
        private string _layout;

        public DockLayoutManager DockingManager { get; }

        public IEnumerable<IStudioControl> DockingControls
        {
            get
            {
                return ( IEnumerable<IStudioControl> )this._controls.ToArray<IStudioControl>();
            }
        }

        public IDictionary<Type, IStudioControl> PredefinedControls { get; }

        public event Action Changed;

        public event Action LayoutChanged;

        public event Action<IStudioControl> ControlChanged;

        public event Action<IStudioControl> ControlRemoved;

        public LayoutManager( DockLayoutManager dockingManager, DocumentGroup documentGroup = null )
        {
            this._documentGroup = documentGroup;
            DockLayoutManager dockLayoutManager = dockingManager;
            if ( dockLayoutManager == null )
                throw new ArgumentNullException( nameof( dockingManager ) );
            this.DockingManager = dockLayoutManager;
            DXSerializer.AddAllowPropertyHandler( ( DependencyObject )this.DockingManager, ( AllowPropertyEventHandler )( ( s, e ) =>
                {
                    if ( e.DependencyProperty != BaseLayoutItem.CaptionProperty )
                        return;
                    e.Allow = false;
                } ) );
            this.DockingManager.DockItemClosing += new DockItemCancelEventHandler( this.OnDockingManagerDockItemClosing );
            this.DockingManager.DockItemClosed += new DockItemClosedEventHandler( this.OnDockingManagerDockItemClosed );
            this.DockingManager.DockItemHidden += new DockItemEventHandler( this.DockingManager_DockItemHidden );
            this.DockingManager.DockItemRestored += new DockItemEventHandler( this.DockingManager_DockItemRestored );
            this.DockingManager.DockItemExpanded += new DockItemExpandedEventHandler( this.DockingManager_DockItemExpanded );
            this.DockingManager.DockItemActivated += new DockItemActivatedEventHandler( this.DockingManager_DockItemActivated );
            this.DockingManager.DockItemEndDocking += new DockItemDockingEventHandler( this.DockingManager_DockItemEndDocking );
            this.DockingManager.LayoutItemSizeChanged += new LayoutItemSizeChangedEventHandler( this.DockingManager_LayoutItemSizeChanged );
            this.PredefinedControls = ( IDictionary<Type, IStudioControl> )new Dictionary<Type, IStudioControl>();
        }

        public ContentItem ActivePanel
        {
            get
            {
                return ( ContentItem )this.DockingManager.DockController.ActiveItem;
            }
        }

        public IStudioControl ActiveControl
        {
            get
            {
                return ( IStudioControl )this.ActivePanel?.Content;
            }
        }

        public void Activate( IStudioControl control )
        {
            if ( control == null )
                throw new ArgumentNullException( nameof( control ) );
            LayoutPanel layoutPanel = this._panels.TryGetValue<string, LayoutPanel>( control.Key );
            if ( layoutPanel == null )
                return;
            this.DockingManager.DockController.Activate( ( BaseLayoutItem )layoutPanel );
        }

        public void Activate( Func<IStudioControl, bool> where )
        {
            if ( where == null )
                throw new ArgumentNullException( nameof( where ) );
            IStudioControl control = this._controls.FirstOrDefault<IStudioControl>( where );
            if ( control == null )
                return;
            this.Activate( control );
        }

        private IStudioControl GetOrCreate( Type controlType )
        {
            if ( controlType == ( Type )null )
                throw new ArgumentNullException( nameof( controlType ) );
            if ( !typeof( IStudioControl ).IsAssignableFrom( controlType ) )
                throw new ArgumentException( LocalizedStrings.TypeNotImplemented.Put( ( object )controlType.Name, ( object )"IStudioControl" ), nameof( controlType ) );
            IStudioControl studioControl;
            if ( !this.PredefinedControls.TryGetValue( controlType, out studioControl ) )
                return controlType.CreateInstance<IStudioControl>();
            return studioControl;
        }

        public IStudioControl OpenToolWithKey( Type controlType )
        {
            return this.OpenToolWindow( controlType, controlType.CreateKey(), true );
        }

        public IStudioControl OpenToolWindow( Type controlType, string key = null, bool canClose = true )
        {
            if ( key != null )
                return this.OpenToolWindow( key, controlType, ( Func<IStudioControl> )( () => this.GetOrCreate( controlType ) ), canClose );
            return this.OpenToolWindow( this.GetOrCreate( controlType ), canClose );
        }

        public IStudioControl OpenToolWindow( IStudioControl content, bool canClose = true )
        {
            if ( content == null )
                throw new ArgumentNullException( nameof( content ) );
            return this.OpenToolWindow( content.Key, content.GetType(), ( Func<IStudioControl> )( () => content ), canClose );
        }

        public IStudioControl OpenToolWindow<T>(
          string key,
          Func<T> getControl,
          bool canClose = true )
          where T : IStudioControl
        {
            return this.OpenWindow( true, key, typeof( T ), ( Func<IStudioControl> )( () => ( IStudioControl )getControl() ), canClose );
        }

        private IStudioControl OpenToolWindow(
          string key,
          Type ctrlType,
          Func<IStudioControl> getControl,
          bool canClose = true )
        {
            return this.OpenWindow( true, key, ctrlType, getControl, canClose );
        }

        public IStudioControl OpenDocumentWithKey( Type controlType )
        {
            return this.OpenDocumentWindow( controlType, controlType.CreateKey(), true );
        }

        public IStudioControl OpenDocumentWindow(
          Type controlType,
          string key = null,
          bool canClose = true )
        {
            if ( key != null )
                return this.OpenDocumentWindow( key, controlType, ( Func<IStudioControl> )( () => this.GetOrCreate( controlType ) ), canClose );
            return this.OpenDocumentWindow( this.GetOrCreate( controlType ), canClose );
        }

        public IStudioControl OpenDocumentWindow( IStudioControl content, bool canClose = true )
        {
            if ( content == null )
                throw new ArgumentNullException( nameof( content ) );
            return this.OpenDocumentWindow( content.Key, content.GetType(), ( Func<IStudioControl> )( () => content ), canClose );
        }

        public IStudioControl OpenDocumentWindow<T>(
          string key,
          Func<T> getControl,
          bool canClose = true )
          where T : IStudioControl
        {
            return this.OpenWindow( false, key, typeof( T ), ( Func<IStudioControl> )( () => ( IStudioControl )getControl() ), canClose );
        }

        private IStudioControl OpenDocumentWindow(
          string key,
          Type ctrlType,
          Func<IStudioControl> getControl,
          bool canClose = true )
        {
            return this.OpenWindow( false, key, ctrlType, getControl, canClose );
        }

        private IStudioControl OpenWindow<T>(
          bool isTool,
          string key,
          Func<T> getControl,
          bool canClose )
          where T : IStudioControl
        {
            return this.OpenWindow( isTool, key, typeof( T ), ( Func<IStudioControl> )( () => ( IStudioControl )getControl() ), canClose );
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
            IDockController dockController = this.DockingManager.DockController;
            ctrlType.CreateKey();
            LayoutPanel layoutPanel = this._panels.TryGetValue<string, LayoutPanel>( key );
            if ( layoutPanel == null )
            {
                IStudioControl control = getControl();
                layoutPanel = isTool ? dockController.AddPanel( DockType.Right ) : ( LayoutPanel )dockController.AddDocumentPanel( this._documentGroup );
                layoutPanel.Name = key;
                layoutPanel.Content = ( object )control;
                layoutPanel.ShowCloseButton = canClose;
                layoutPanel.Visibility = Visibility.Visible;
                LayoutManager.SetHelpSourceUrl( layoutPanel, control );
                this.BindProperties( layoutPanel, control );
                this._panels[key] = layoutPanel;
                this._controls.Add( control );
                this.MarkControlChanged( control );
            }
            else
            {
                dockController.Restore( ( BaseLayoutItem )layoutPanel );
                layoutPanel.Visibility = Visibility.Visible;
            }
            dockController.Activate( ( BaseLayoutItem )layoutPanel );
            return ( IStudioControl )this._panels[key].Content;
        }

        private LayoutPanel TryGetPanel( IStudioControl content )
        {
            return this._panels.TryGetValue<string, LayoutPanel>( content.Key );
        }

        public bool Remove( IStudioControl content )
        {
            if ( content == null )
                throw new ArgumentNullException( nameof( content ) );
            LayoutPanel panel = this.TryGetPanel( content );
            if ( panel == null )
                return false;
            panel.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
            if ( this.DockingManager.ClosedPanels.Contains( panel ) )
                this.DockingManager.DockController.Restore( ( BaseLayoutItem )panel );
            return this.DockingManager.DockController.Close( ( BaseLayoutItem )panel );
        }

        public void Remove( Func<IStudioControl, bool> where )
        {
            if ( where == null )
                throw new ArgumentNullException( nameof( where ) );
            IStudioControl content = this._controls.FirstOrDefault<IStudioControl>( where );
            if ( content == null )
                return;
            this.Remove( content );
        }

        public void Clear()
        {
            foreach ( LayoutPanel layoutPanel in this._panels.Values.Distinct<LayoutPanel>().ToArray<LayoutPanel>() )
            {
                layoutPanel.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                this.DockingManager.DockController.Close( ( BaseLayoutItem )layoutPanel );
            }
            this._panels.Clear();
            this._changedControls.Clear();
            this._dockingControlSettings.Clear();
        }

        public override void Load( SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            this.Clear();
            foreach ( SettingsStorage storage1 in storage.GetValue<SettingsStorage[ ]>( "Controls", ( SettingsStorage[ ] )null ) )
                this.LoadControl( storage1 );
            this._layout = storage.GetValue<string>( "Layout", ( string )null );
            if ( this._layout.IsEmpty() )
                return;
            this.LoadLayout( this._layout );
        }

        public void Save( SettingsStorage storage, bool force )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            IStudioControl[ ] studioControlArray = force ? this._dockingControlSettings.SyncGet<SynchronizedDictionary<IStudioControl, SettingsStorage>, IStudioControl[ ]>( ( Func<SynchronizedDictionary<IStudioControl, SettingsStorage>, IStudioControl[ ]> )( d => d.Keys.ToArray<IStudioControl>() ) ) : this._changedControls.SyncGet<SynchronizedSet<IStudioControl>, IStudioControl[ ]>( ( Func<SynchronizedSet<IStudioControl>, IStudioControl[ ]> )( c => c.CopyAndClear<IStudioControl>() ) );
            int num = this._isLayoutChanged ? 1 : 0;
            this._isLayoutChanged = false;
            if ( studioControlArray.Length != 0 )
                this.Save( ( IEnumerable<IStudioControl> )studioControlArray );
            if ( num != 0 )
                this._layout = this.SaveLayout();
            storage.SetValue<SettingsStorage[ ]>( "Controls", this._dockingControlSettings.SyncGet<SynchronizedDictionary<IStudioControl, SettingsStorage>, SettingsStorage[ ]>( ( Func<SynchronizedDictionary<IStudioControl, SettingsStorage>, SettingsStorage[ ]> )( c => c.Select<KeyValuePair<IStudioControl, SettingsStorage>, SettingsStorage>( ( Func<KeyValuePair<IStudioControl, SettingsStorage>, SettingsStorage> )( p => p.Value ) ).ToArray<SettingsStorage>() ) ) );
            storage.SetValue<string>( "Layout", this._layout );
        }

        public override void Save( SettingsStorage storage )
        {
            this.Save( storage, false );
        }

        public void LoadLayout( string layout )
        {
            if ( layout == null )
                throw new ArgumentNullException( nameof( layout ) );
            try
            {
                Dictionary<string, string> dictionary = this.DockingManager.GetItems().OfType<LayoutPanel>().Where<LayoutPanel>( ( Func<LayoutPanel, bool> )( c => !c.Name.IsEmpty() ) ).ToDictionary<LayoutPanel, string, string>( ( Func<LayoutPanel, string> )( c => c.Name ), ( Func<LayoutPanel, string> )( c => ( string )c.Caption ) );
                this.DockingManager.LoadLayout( layout );
                foreach ( LayoutPanel panel in this.DockingManager.GetItems().OfType<LayoutPanel>().ToArray<LayoutPanel>() )
                {
                    this._panels[panel.Name] = panel;
                    IStudioControl content = panel.Content as IStudioControl;
                    if ( content != null )
                    {
                        this.BindProperties( panel, content );
                    }
                    else
                    {
                        string str = dictionary.TryGetValue<string, string>( panel.Name );
                        if ( !str.IsEmpty() )
                            panel.Caption = ( object )str;
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
                return this.DockingManager.SaveLayout();
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
                IStudioControl studioControl = this.LoadBaseStudioControl( storage );
                int num = storage.GetValue<bool>( "IsToolWindow", true ) ? 1 : 0;
                this._dockingControlSettings.Add( studioControl, storage );
                if ( num != 0 )
                    this.OpenToolWindow( studioControl, true );
                else
                    this.OpenDocumentWindow( studioControl, true );
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
            return this.Save( control );
        }

        public void MarkControlChanged( IStudioControl control )
        {
            if ( !control.SaveWithLayout || !this._controls.Contains( control ) )
                return;
            this._changedControls.Add( control );
            Action changed = this.Changed;
            if ( changed != null )
                changed();
            Action<IStudioControl> controlChanged = this.ControlChanged;
            if ( controlChanged == null )
                return;
            controlChanged( control );
        }

        private void OnDockingManagerDockItemClosing( object sender, ItemCancelEventArgs e )
        {
            e.Cancel = this.OnDockingManagerItemClosing( e.Item );
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
                    return layoutGroup.Items.Any<BaseLayoutItem>( new Func<BaseLayoutItem, bool>( this.OnDockingManagerItemClosing ) );
                return false;
            }
        }

        private void OnDockingManagerDockItemClosed( object sender, DockItemClosedEventArgs e )
        {
            foreach ( BaseLayoutItem affectedItem in e.AffectedItems )
            {
                if ( affectedItem.ClosingBehavior == ClosingBehavior.HideToClosedPanelsCollection )
                    this.LogHiddenPanel( affectedItem );
                else if ( affectedItem.ClosingBehavior == ClosingBehavior.ImmediatelyRemove )
                    this.OnDockingManagerItemClosed( affectedItem );
            }
            this.OnDockingChanged();
        }

        private void LogHiddenPanel( BaseLayoutItem item )
        {
            string str = item.Name;
            LayoutPanel layoutPanel = item as LayoutPanel;
            if ( layoutPanel != null )
            {
                BaseStudioControl content = layoutPanel.Content as BaseStudioControl;
                if ( content != null )
                    str = content.Title + " (" + item.Name + ", " + ( ( object )content ).GetType().Name + ")";
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
                LayoutManager.DisposeControl( content, CloseReason.CloseWindow );
                this._panels.RemoveWhere<KeyValuePair<string, LayoutPanel>>( ( Func<KeyValuePair<string, LayoutPanel>, bool> )( p => object.Equals( ( object )p.Value, ( object )panel ) ) );
                this._controls.Remove( content );
                this._changedControls.Remove( content );
                this._dockingControlSettings.Remove( content );
                Action<IStudioControl> controlRemoved = this.ControlRemoved;
                if ( controlRemoved != null )
                    controlRemoved( content );
            }
            LayoutGroup layoutGroup = item as LayoutGroup;
            if ( layoutGroup == null )
                return;
            foreach ( BaseLayoutItem baseLayoutItem in ( Collection<BaseLayoutItem> )layoutGroup.Items )
                this.OnDockingManagerItemClosed( baseLayoutItem );
        }

        private void DockingManager_DockItemEndDocking( object sender, DockItemDockingEventArgs e )
        {
            this.OnDockingChanged();
        }

        private void DockingManager_DockItemActivated( object sender, DockItemActivatedEventArgs ea )
        {
            this.OnDockingChanged();
        }

        private void DockingManager_DockItemExpanded( object sender, DockItemExpandedEventArgs e )
        {
            this.OnDockingChanged();
        }

        private void DockingManager_DockItemRestored( object sender, ItemEventArgs e )
        {
            this.OnDockingChanged();
        }

        private void DockingManager_DockItemHidden( object sender, ItemEventArgs e )
        {
            this.OnDockingChanged();
        }

        private void DockingManager_LayoutItemSizeChanged(
          object sender,
          LayoutItemSizeChangedEventArgs e )
        {
            this.OnDockingChanged();
        }

        private void OnDockingChanged()
        {
            this._isLayoutChanged = true;
            Action changed = this.Changed;
            if ( changed != null )
                changed();
            Action layoutChanged = this.LayoutChanged;
            if ( layoutChanged == null )
                return;
            layoutChanged();
        }

        private void Save( IEnumerable<IStudioControl> items )
        {
            foreach ( IStudioControl control in items )
            {
                if ( control.SaveWithLayout )
                    this._dockingControlSettings[control] = this.Save( control );
            }
        }

        private SettingsStorage Save( IStudioControl control )
        {
            bool flag = this.TryGetPanel( control ) is DocumentPanel;
            SettingsStorage storage = new SettingsStorage();
            storage.SetValue<string>( "ControlType", control.GetType().GetTypeName( false ) );
            storage.SetValue<bool>( "IsToolWindow", !flag );
            control.Save( storage );
            return storage;
        }

        private IStudioControl LoadBaseStudioControl( SettingsStorage settings )
        {
            string str = settings.GetValue<string>( "ControlType", ( string )null );
            if ( str.StartsWith( "StockSharp.Designer" ) && !str.Contains( "Panels" ) )
                str = str.Replace( "StockSharp.Designer", "StockSharp.Designer.Panels" );
            Type type = str.Replace( "StockSharp.Hydra.Server.UsersPane,", "StockSharp.Hydra.Panes.UsersPane," ).Replace( "StockSharp.Hydra.Server.SubscriptionsPane,", "StockSharp.Hydra.Panes.SubscriptionsPane," ).Replace( "StockSharp.Hydra.Server.SecurityMappingPane,", "StockSharp.Hydra.Panes.SecurityMappingPane," ).To<Type>();
            IStudioControl studioControl = this.PredefinedControls.TryGetValue<Type, IStudioControl>( type ) ?? ( IStudioControl )Activator.CreateInstance( type );
            studioControl.Load( settings );
            return studioControl;
        }

        protected override void DisposeManaged()
        {
            foreach ( IStudioControl control in this._controls )
                LayoutManager.DisposeControl( control, CloseReason.Shutdown );
            base.DisposeManaged();
        }

        private void BindProperties( LayoutPanel panel, IStudioControl control )
        {
            panel.SetBindings( BaseLayoutItem.CaptionProperty, ( object )control, new PropertyPath( "(0)", new object[1]
            {
        (object) typeof (IStudioControl).GetProperty("Title")
            } ), BindingMode.TwoWay, ( IValueConverter )null, ( object )null );
            panel.SetBindings( BaseLayoutItem.AllowRenameProperty, ( object )control, new PropertyPath( "(0)", new object[1]
            {
        (object) typeof (IStudioControl).GetProperty("IsTitleEditable")
            } ), BindingMode.OneWay, ( IValueConverter )null, ( object )null );
        }

        private static void SetHelpSourceUrl( LayoutPanel document, IStudioControl control )
        {
            DocAttribute attribute = control.GetType().GetAttribute<DocAttribute>( true );
            if ( attribute == null )
            {
                string url = Doc.GetUrl( ( UIElement )control );
                if ( url.IsEmpty() )
                    return;
                Doc.SetUrl( ( UIElement )document, url );
            }
            else
                Doc.SetUrl( ( UIElement )document, attribute.DocUrl );
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
