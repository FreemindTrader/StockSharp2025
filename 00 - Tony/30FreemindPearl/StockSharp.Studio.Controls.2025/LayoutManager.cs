// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LayoutManager
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.Xpf.Docking.VisualElements;
using DevExpress.Xpf.Layout.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    public sealed class LayoutManager : BaseLogReceiver
    {
        private readonly DocumentGroup _documentGroup;
        private readonly Dictionary<string, LayoutPanel> _panels;
        private readonly HashSet<IStudioControl> _controls;
        private readonly SynchronizedDictionary<IStudioControl, SettingsStorage> _dockingControlSettings;
        private readonly CachedSynchronizedSet<IStudioControl> _changedControls;
        private bool _supressChanges;
        private bool _isLayoutChanged;
        private string _layout;
        private IStudioControl _loadingCtrl;

        public DockLayoutManager DockingManager { get; }

        public IEnumerable<IStudioControl> DockingControls
        {
            get
            {
                var controls = _controls;
                int index = 0;
                var items = new IStudioControl[controls.Count];

                foreach ( IStudioControl studioControl in controls )
                {
                    items [index] = studioControl;
                    ++index;
                }
                return ( IEnumerable<IStudioControl> ) new ReadOnlyCollection<IStudioControl>( items );
            }
        }

        public event Action Changed;

        public event Action LayoutChanged;

        public event Action<IStudioControl> ControlAdded;

        public event Action<IStudioControl> ControlChanged;

        public event Action<IStudioControl> ControlRemoved;

        public LayoutManager( DockLayoutManager dockingManager, DocumentGroup documentGroup = null )
        {            
            _documentGroup = documentGroup;
            DockLayoutManager dockLayoutManager = dockingManager;
            if ( dockLayoutManager == null )
                throw new ArgumentNullException( nameof( dockingManager ) );
            DockingManager = dockLayoutManager;
            DXSerializer.AddAllowPropertyHandler( DockingManager, ( AllowPropertyEventHandler ) ( ( s, e ) =>
            {
                if ( e.DependencyProperty != BaseLayoutItem.CaptionProperty )
                    return;
                e.Allow = false;
            } ) );
            DockingManager.DockItemClosing += OnDockingManagerDockItemClosing;
            DockingManager.DockItemClosed += OnDockingManagerDockItemClosed;
            DockingManager.DockItemHidden += DockingManager_DockItemHidden;
            DockingManager.DockItemRestored += DockingManager_DockItemRestored;
            DockingManager.DockItemExpanded += DockingManager_DockItemExpanded;
            DockingManager.DockItemActivated += DockingManager_DockItemActivated;
            DockingManager.DockItemEndDocking += DockingManager_DockItemEndDocking;
            DockingManager.LayoutItemSizeChanged += DockingManager_LayoutItemSizeChanged;
        }

        public ContentItem ActivePanel
        {
            get
            {
                return ( ContentItem ) DockingManager.DockController.ActiveItem;
            }
        }

        public IStudioControl ActiveControl
        {
            get
            {
                return ( IStudioControl ) ActivePanel?.Content;
            }
        }

        public void Activate( IStudioControl control )
        {
            if ( control == null )
                throw new ArgumentNullException( nameof( control ) );
            LayoutPanel layoutPanel;
            if ( !_panels.TryGetValue( control.Key, out layoutPanel ) )
                return;
            DockingManager.DockController.Activate( ( BaseLayoutItem ) layoutPanel );
        }

        public void Activate( Func<IStudioControl, bool> where )
        {
            if ( where == null )
                throw new ArgumentNullException( nameof( where ) );
            IStudioControl control = _controls.FirstOrDefault<IStudioControl>(where);
            if ( control == null )
                return;
            Activate( control );
        }
        
        private (IStudioControl ctrl, bool isNew) OpenWindow( bool isTool, Type controlType )
        {
            if (  controlType == null )
                throw new ArgumentNullException( nameof( controlType ) );
            if ( !TypeHelper.Is<IStudioControl>( controlType, true ) )
                throw new ArgumentException( StringHelper.Put( LocalizedStrings.TypeNotImplemented, new object [2]
                {
          (object) controlType.Name,
          (object) "IStudioControl"
                } ), nameof( controlType ) );
            string key = controlType.CreateKey();
            return OpenWindow( isTool, key, ( Func<IStudioControl> ) ( () =>
            {
                var instance = TypeHelper.CreateInstance<IStudioControl>(controlType, Array.Empty<object>());
                ( ( IStudioControl ) instance ).Key = key;
                return ( IStudioControl ) instance;
            } ) );
        }
        
        public (IStudioControl ctrl, bool isNew) OpenToolWindow( Type controlType )
        {
            return OpenWindow( true, controlType );
        }

        
        public (IStudioControl ctrl, bool isNew) OpenDocumentWindow( Type controlType )
        {
            return OpenWindow( false, controlType );
        }

        
        public (IStudioControl ctrl, bool isNew) OpenDocumentWindow( IStudioControl content )
        {
            if ( content == null )
                throw new ArgumentNullException( nameof( content ) );
            return OpenWindow( false, content.Key, ( Func<IStudioControl> ) ( () => content ) );
        }

        
        public (IStudioControl ctrl, bool isNew) OpenDocumentWindow<T>( string key, Func<T> getControl )
          where T : IStudioControl
        {
            return OpenWindow( false, key, ( Func<IStudioControl> ) ( () => ( IStudioControl ) getControl() ) );
        }

        
        private (IStudioControl ctrl, bool isNew) OpenWindow( bool isTool, string key, Func<IStudioControl> getControl )
        {
            if ( StringHelper.IsEmpty( key ) )
                throw new ArgumentNullException( nameof( key ) );
            if ( getControl == null )
                throw new ArgumentNullException( nameof( getControl ) );
            IDockController dockController = DockingManager.DockController;
            bool flag = false;
            LayoutPanel layoutPanel;
            if ( !_panels.TryGetValue( key, out layoutPanel ) )
            {
                flag = true;
                IStudioControl control = getControl();
                layoutPanel = isTool ? dockController.AddPanel( DockType.Right ) : ( LayoutPanel ) dockController.AddDocumentPanel( _documentGroup );
                layoutPanel.Name = key;
                layoutPanel.Content =  control;
                layoutPanel.ShowCloseButton = layoutPanel.AllowClose = true;
                layoutPanel.Visibility = Visibility.Visible;
                LayoutManager.SetHelpSourceUrl( layoutPanel, control );
                LayoutManager.BindProperties( layoutPanel, control );
                _panels [key] = layoutPanel;
                _controls.Add( control );
                Action<IStudioControl> controlAdded = ControlAdded;
                if ( controlAdded != null )
                    controlAdded( control );
                if ( _loadingCtrl != control )
                    MarkControlChanged( control );
            }
            else
            {
                dockController.Restore( ( BaseLayoutItem ) layoutPanel );
                layoutPanel.Visibility = Visibility.Visible;
            }
            dockController.Activate( ( BaseLayoutItem ) layoutPanel );
            return new ValueTuple<IStudioControl, bool>( ( IStudioControl ) layoutPanel.Content, flag );
        }

        public bool Remove( IStudioControl content )
        {
            if ( content == null )
                throw new ArgumentNullException( nameof( content ) );
            LayoutPanel layoutPanel;
            if ( !_panels.TryGetValue( content.Key, out layoutPanel ) )
                return false;
            layoutPanel.AllowClose = true;
            layoutPanel.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
            if ( DockingManager.ClosedPanels.Contains( layoutPanel ) )
                DockingManager.DockController.Restore( ( BaseLayoutItem ) layoutPanel );
            return DockingManager.DockController.Close( ( BaseLayoutItem ) layoutPanel );
        }

        public void Remove( Func<IStudioControl, bool> where )
        {
            if ( where == null )
                throw new ArgumentNullException( nameof( where ) );
            IStudioControl content = _controls.FirstOrDefault<IStudioControl>(where);
            if ( content == null )
                return;
            Remove( content );
        }

        public void Clear()
        {
            foreach ( LayoutPanel layoutPanel in _panels.Values.Distinct<LayoutPanel>().ToArray<LayoutPanel>() )
            {
                layoutPanel.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                DockingManager.DockController.Close( ( BaseLayoutItem ) layoutPanel );
            }
            _panels.Clear();
            ( ( BaseCollection<IStudioControl, ISet<IStudioControl>> ) _changedControls ).Clear();
            _dockingControlSettings.Clear();
        }

        public override void Load( SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            Clear();
            foreach ( SettingsStorage storage1 in ( SettingsStorage [ ] ) storage.GetValue<SettingsStorage [ ]>( "Controls",  null ) )
                LoadControl( storage1 );
            _layout = ( string ) storage.GetValue<string>( "Layout",  null );
            if ( StringHelper.IsEmpty( _layout ) )
                return;
            LoadLayout( _layout );
        }

        public void Save( SettingsStorage storage, bool force )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            IStudioControl[] studioControlArray = force ? (IStudioControl[]) Ecng.Collections.CollectionHelper.SyncGet<SynchronizedDictionary<IStudioControl, SettingsStorage>, IStudioControl[]>( _dockingControlSettings,  (d => d.Keys.ToArray<IStudioControl>())) : (IStudioControl[]) Ecng.Collections.CollectionHelper.SyncGet<CachedSynchronizedSet<IStudioControl>, IStudioControl[]>( _changedControls,  (c => (IStudioControl[]) Ecng.Collections.CollectionHelper.CopyAndClear<IStudioControl>( c)));
            int num = _isLayoutChanged ? 1 : 0;
            _isLayoutChanged = false;
            if ( studioControlArray.Length != 0 )
                Save( ( IEnumerable<IStudioControl> ) studioControlArray );
            if ( num != 0 && !DockingManager.IsDisposing )
                _layout = SaveLayout();
            storage.SetValue<SettingsStorage [ ]>( "Controls",  Ecng.Collections.CollectionHelper.SyncGet<SynchronizedDictionary<IStudioControl, SettingsStorage>, SettingsStorage [ ]>(  _dockingControlSettings,  ( c => ( ( IEnumerable<KeyValuePair<IStudioControl, SettingsStorage>> ) c ).Select<KeyValuePair<IStudioControl, SettingsStorage>, SettingsStorage>( ( Func<KeyValuePair<IStudioControl, SettingsStorage>, SettingsStorage> ) ( p => p.Value ) ).ToArray<SettingsStorage>() ) ) );
            storage.SetValue<string>( "Layout",  _layout );
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
                LayoutPanel[] array = DockingManager.GetItems().OfType<LayoutPanel>().ToArray<LayoutPanel>();
                Dictionary<string, string> dictionary = ((IEnumerable<LayoutPanel>) array).Where<LayoutPanel>((Func<LayoutPanel, bool>) (c => !StringHelper.IsEmpty(c.Name))).ToDictionary<LayoutPanel, string, string>((Func<LayoutPanel, string>) (c => c.Name), (Func<LayoutPanel, string>) (c => (string) c.Caption));
                _supressChanges = true;
                try
                {
                    DockingManager.LoadLayout( layout );
                }
                finally
                {
                    _supressChanges = false;
                }
                foreach ( LayoutPanel panel in array )
                {
                    _panels [panel.Name] = panel;
                    IStudioControl content = panel.Content as IStudioControl;
                    if ( content != null )
                    {
                        LayoutManager.BindProperties( panel, content );
                    }
                    else
                    {
                        string str = (string) Ecng.Collections.CollectionHelper.TryGetValue<string, string>( dictionary,  panel.Name);
                        if ( !StringHelper.IsEmpty( str ) )
                            panel.Caption =  str;
                    }
                }
            }
            catch ( Exception ex )
            {
                LoggingHelper.AddErrorLog( ( ILogReceiver ) this, ex );
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
                LoggingHelper.AddErrorLog( ( ILogReceiver ) this, ex, "error saving dockmanager layout: {0}" );
                return _layout;
            }
        }

        public void LoadControl( SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            try
            {
                string str = LayoutManager.Migrate((string) storage.GetValue<string>("ControlType",  null));
                bool flag = (bool) storage.GetValue<bool>("IsToolWindow",  true);
                IStudioControl control = (IStudioControl) TypeHelper.CreateInstance<IStudioControl>((Type) Converter.To<Type>((object) str), Array.Empty<object>());
                ( ( IPersistable ) control ).Load( storage );
                _dockingControlSettings.Add( control, storage );
                _loadingCtrl = control;
                try
                {
                    if ( flag )
                        OpenWindow( true, control.Key, ( Func<IStudioControl> ) ( () => control ) );
                    else
                        OpenDocumentWindow( control );
                }
                finally
                {
                    _loadingCtrl = ( IStudioControl ) null;
                }
            }
            catch ( Exception ex )
            {
                LoggingHelper.AddErrorLog( ( ILogReceiver ) this, ex );
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
            ( ( BaseCollection<IStudioControl, ISet<IStudioControl>> ) _changedControls ).Add( control );
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

        public event Action<LayoutPanel> Hidden;

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
                CloseAction closeAction1;
                switch ( item.ClosingBehavior )
                {
                    case ClosingBehavior.Default:
                    closeAction1 = content.CanClose( CloseReason.CloseWindow );
                    break;
                    case ClosingBehavior.HideToClosedPanelsCollection:
                    closeAction1 = CloseAction.Hide;
                    break;
                    default:
                    closeAction1 = CloseAction.Close;
                    break;
                }
                CloseAction closeAction2 = closeAction1;
                switch ( closeAction2 )
                {
                    case CloseAction.StayOpen:
                    return true;
                    case CloseAction.Close:
                    item.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                    return false;
                    case CloseAction.Hide:
                    item.ClosingBehavior = ClosingBehavior.HideToClosedPanelsCollection;
                    item.Visibility = Visibility.Collapsed;
                    Action<LayoutPanel> hidden = Hidden;
                    if ( hidden != null )
                        hidden( layoutPanel );
                    return true;
                    default:
                    throw new InvalidOperationException( closeAction2.ToString() );
                }
            }
            else
            {
                LayoutGroup layoutGroup = item as LayoutGroup;
                if ( layoutGroup != null )
                    return layoutGroup.Items.Any<BaseLayoutItem>( new Func<BaseLayoutItem, bool>( OnDockingManagerItemClosing ) );
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
                {
                    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
                    interpolatedStringHandler.AppendFormatted( content.Title );
                    interpolatedStringHandler.AppendLiteral( " (" );
                    interpolatedStringHandler.AppendFormatted( item.Name );
                    interpolatedStringHandler.AppendLiteral( ", " );
                    interpolatedStringHandler.AppendFormatted( (  content ).GetType().Name );
                    interpolatedStringHandler.AppendLiteral( ")" );
                    str = interpolatedStringHandler.ToStringAndClear();
                }
            }
            LoggingHelper.AddWarningLog( ( ILogReceiver ) this, "Panel " + str + " was hidden to closed panels", Array.Empty<object>() );
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
                Ecng.Collections.CollectionHelper.RemoveWhere<KeyValuePair<string, LayoutPanel>>(  _panels,  ( p => object.Equals(  p.Value,  panel ) ) );
                _controls.Remove( content );
                ( ( BaseCollection<IStudioControl, ISet<IStudioControl>> ) _changedControls ).Remove( content );
                _dockingControlSettings.Remove( content );
                Action<IStudioControl> controlRemoved = ControlRemoved;
                if ( controlRemoved != null )
                    controlRemoved( content );
            }
            LayoutGroup layoutGroup = item as LayoutGroup;
            if ( layoutGroup == null )
                return;
            foreach ( BaseLayoutItem baseLayoutItem in ( Collection<BaseLayoutItem> ) layoutGroup.Items )
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
            if ( _supressChanges )
                return;
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
                    _dockingControlSettings[control]= Save( control );
            }
        }

        private SettingsStorage Save( IStudioControl control )
        {
            bool flag = Ecng.Collections.CollectionHelper.TryGetValue<string, LayoutPanel>( _panels,  control.Key) is DocumentPanel;
            SettingsStorage settingsStorage = new SettingsStorage().Set<string>("ControlType",  TypeHelper.GetTypeName(control.GetType(), false)).Set<bool>("IsToolWindow",  !flag);
            ( ( IPersistable ) control ).Save( settingsStorage );
            return settingsStorage;
        }

        private static string Migrate( string typeName )
        {
            if ( typeName.StartsWith( "StockSharp.Designer" ) && !typeName.Contains( "Panels" ) )
                typeName = typeName.Replace( "StockSharp.Designer", "StockSharp.Designer.Panels" );
            typeName = typeName.Replace( "StockSharp.Hydra.Server.UsersPane,", "StockSharp.Hydra.Panes.UsersPane," );
            typeName = typeName.Replace( "StockSharp.Hydra.Server.SubscriptionsPane,", "StockSharp.Hydra.Panes.SubscriptionsPane," );
            typeName = typeName.Replace( "StockSharp.Hydra.Server.SecurityMappingPane,", "StockSharp.Hydra.Panes.SecurityMappingPane," );
            typeName = typeName.Replace( "StockSharp.Studio.Controls.MyTradesTable", "StockSharp.Studio.Controls.MyTradesPanel" );
            return typeName;
        }

        protected override void DisposeManaged()
        {
            foreach ( IStudioControl control in _controls )
                LayoutManager.DisposeControl( control, CloseReason.Shutdown );
            
            this.DisposeManaged();
        }

        private static void BindProperties( LayoutPanel panel, IStudioControl control )
        {
            panel.SetBindings( BaseLayoutItem.CaptionProperty,  control, new PropertyPath( "(0)", new object [1]
            {
        (object) typeof (IStudioControl).GetProperty("Title")
            } ), BindingMode.TwoWay, ( IValueConverter ) null,  null );
            panel.SetBindings( BaseLayoutItem.AllowRenameProperty,  control, new PropertyPath( "(0)", new object [1]
            {
        (object) typeof (IStudioControl).GetProperty("IsTitleEditable")
            } ), BindingMode.OneWay, ( IValueConverter ) null,  null );
            panel.SetBindings( BaseLayoutItem.CaptionImageProperty,  control, new PropertyPath( "(0)", new object [1]
            {
        (object) typeof (IStudioControl).GetProperty("Icon")
            } ), BindingMode.OneWay, ( IValueConverter ) null,  null );
            LayoutPanel layoutPanel = panel;
            Style style = new Style(typeof (CaptionImage));
            style.Setters.Add( ( SetterBase ) new Setter( FrameworkElement.WidthProperty,  16.0 ) );
            style.Setters.Add( ( SetterBase ) new Setter( FrameworkElement.HeightProperty,  16.0 ) );
            style.Setters.Add( ( SetterBase ) new Setter( Image.StretchProperty,  Stretch.Fill ) );
            layoutPanel.CaptionImageStyle = style;
        }

        private static void SetHelpSourceUrl( LayoutPanel document, IStudioControl control )
        {
            if ( document == null )
                throw new ArgumentNullException( nameof( document ) );
            if ( control == null )
                throw new ArgumentNullException( nameof( control ) );
            string str = StringHelper.IsEmpty(control.DocUrl, ((UIElement) control).GetUrl());
            if ( StringHelper.IsEmpty( str ) )
                return;
            document.SetUrl( str );
        }

        private static void DisposeControl( IStudioControl control, CloseReason reason )
        {
            BaseStudioControl baseStudioControl = control as BaseStudioControl;
            if ( baseStudioControl != null )
                baseStudioControl.Dispose( reason );
            else
                control.Dispose();
        }

        private static class Keys
        {
            public const string ControlType = "ControlType";
            public const string IsToolWindow = "IsToolWindow";
        }
    }
}
