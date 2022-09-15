using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml.GridControl;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class ConnectorWindow : DXWindow, IPersistable
    {
        public static readonly RoutedCommand RemoveCommand = new RoutedCommand( );
        public static readonly RoutedCommand EnableCommand = new RoutedCommand( );

        private readonly ObservableCollection<ConnectorInfo>           _connectorInfoCollection = new ObservableCollection<ConnectorInfo>( );
        private readonly ObservableCollection<ConnectorWindow.GridRow> _connectorRowsCollection = new ObservableCollection<ConnectorWindow.GridRow>( );
        private BasketMessageAdapter                                   _basketAdapter;


        public ConnectorWindow( )
        {
            InitializeComponent( );
            _connectorInfoCollection.CollectionChanged += new NotifyCollectionChangedEventHandler( observableCollection_0_CollectionChanged );
            ConnectorTypesGrid.ItemsSource              = _connectorInfoCollection;
            ConnectorTypesGrid.GroupSummary.Clear( );
            ConnectorTypesGrid.ItemDoubleClick         += OnConnectorTypesGridDoubleClicked;
            ConnectorsGrid.ItemsSource                  = _connectorRowsCollection;
            ConnectorsGrid.SelectionChanged            += new GridSelectionChangedEventHandler( OnConnectsGridSelectionChanged );
            OpenAccountLink.EditValue                   = "https://stocksharp." + LocalizedStrings.Domain + "/broker/openaccount/";
        }

        public bool AutoConnect
        {
            get
            {
                bool? isChecked = AutoConnectCtrl.IsChecked;
                return isChecked.GetValueOrDefault( ) & isChecked.HasValue;
            }
            set
            {
                AutoConnectCtrl.IsChecked = new bool?( value );
            }
        }

        public BasketMessageAdapter Adapter
        {
            get
            {
                return _basketAdapter;
            }
            set
            {
                if ( _basketAdapter == value )
                {
                    return;
                }

                _basketAdapter = value;
                _connectorRowsCollection.Clear( );
                if ( _basketAdapter == null )
                {
                    return;
                }

                _connectorRowsCollection.AddRange<ConnectorWindow.GridRow>( _basketAdapter.InnerAdapters.Select( new Func<IMessageAdapter, ConnectorWindow.GridRow>( CreateRow ) ) );
            }
        }

        private ConnectorWindow.GridRow CreateRow( IMessageAdapter adapter )
        {            
            if ( adapter == null )
            {
                throw new ArgumentNullException( "adapter" );
            }

            IMessageChannel messageChannel = adapter.Clone( );
            var myType = messageChannel.GetType( );

            ConnectorInfo connectorInfo = _connectorInfoCollection.FirstOrDefault( i => i.AdapterType == myType );

            if ( connectorInfo == null )
            {
                throw new ArgumentException( LocalizedStrings.Str1553Params.Put( myType ), "adapter" );
            }

            return new ConnectorWindow.GridRow( this, connectorInfo, adapter )
            {
                IsEnabled = Adapter.InnerAdapters[ adapter ] != -1
            };
        }

        public IList<ConnectorInfo> ConnectorsInfo
        {
            get
            {
                return _connectorInfoCollection;
            }
        }

        private ConnectorWindow.GridRow GetSelectedRow( )
        {
            return ( ConnectorWindow.GridRow )ConnectorsGrid?.SelectedItem;
        }

        private void method_2( ConnectorWindow.GridRow row )
        {
            ConnectorsGrid.SelectedItem = row;
        }

        private IEnumerable<ConnectorWindow.GridRow> GetSelectedRows( )
        {
            
            IEnumerable<ConnectorWindow.GridRow> rows;
            if ( ConnectorsGrid == null )
            {
                rows = null;
            }
            else
            {
                rows = ConnectorsGrid.SelectedItems.Cast<ConnectorWindow.GridRow>( );

                if ( rows != null )
                {
                    return rows;
                }
            }
            return Enumerable.Empty<ConnectorWindow.GridRow>( );
        }

        private void CommandBinding_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            foreach ( ConnectorWindow.GridRow row in GetSelectedRows( ).ToArray<ConnectorWindow.GridRow>( ) )
            {
                Adapter.InnerAdapters.Remove( row.Adapter );
                _connectorRowsCollection.Remove( row );
            }
        }

        private void CommandBinding_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = GetSelectedRow( ) != null;
        }

        private void OnConnectsGridSelectionChanged( object sender, GridSelectionChangedEventArgs e )
        {
            ConnectorWindow.GridRow selectedRow = GetSelectedRow( );

            method_7( GetSelectedRows( ).Count<ConnectorWindow.GridRow>( ) == 1 ? selectedRow : null );
        }

        private void OnConnectorTypesGridDoubleClicked( object sender, ItemDoubleClickEventArgs e )
        {
            ConnectorInfo row = ( ConnectorInfo )e.Row;
            IMessageAdapter instanceArgs = row.AdapterType.CreateInstanceArgs<IMessageAdapter>( new object[ ] { Adapter.TransactionIdGenerator } );

            ConnectorWindow.GridRow gridRow = new ConnectorWindow.GridRow( this, row, instanceArgs );
            Adapter.InnerAdapters[ instanceArgs ] = 0;
            _connectorRowsCollection.Add( gridRow );

            ConnectorsGrid.SelectedItem = gridRow;
            
            ConnectorTypesButton.IsPopupOpen = false;
        }

        private void method_7( ConnectorWindow.GridRow class77_0 )
        {
            if ( class77_0 == null )
            {
                SupportedMessages.Adapter   = null;
                PropertyGrid.SelectedObject = null;
                HelpButton.DocUrl           = null;
                AdapterButtons.IsEnabled    = false;
            }
            else
            {
                SupportedMessages.Adapter   = class77_0.Adapter;
                PropertyGrid.SelectedObject = class77_0.Adapter;
                HelpButton.DocUrl           = class77_0.Adapter.GetType( ).GetDocUrl( );
                AdapterButtons.IsEnabled    = true;
            }
        }

        private bool method_8( IEnumerable<IMessageAdapter> adapters )
        {
            foreach ( IMessageAdapter messageAdapter in adapters )
            {
                ;
            }

            return true;
        }

        private void Test_Click( object sender, RoutedEventArgs e )
        {
            ConnectorWindow.Class76 class76 = new ConnectorWindow.Class76( );
            class76.connectorWindow_0 = this;

            IMessageAdapter[ ] adapters = GetSelectedRows( ).Select( r => {
                                                                            r.Adapter.Parent = null;
                                                                            return r.Adapter;
                                                                        } ).ToArray<IMessageAdapter>( );
            if ( !method_8( adapters ) )
            {
                return;
            }

            BusyIndicator.IsBusy = true;
            Test.IsEnabled       = false;

            class76.LogManager = ServicesRegistry.LogManager;
            class76.NewConnector = new Connector( )
            {
                LookupMessagesOnConnect = false,
                AutoPortfoliosSubscribe = false
            };
            class76.NewConnector.Adapter.InnerAdapters.AddRange<IMessageAdapter>( adapters );
            class76.NewConnector.Connected       += new Action( class76.method_0 );
            class76.NewConnector.Disconnected    += new Action( class76.method_2 );
            class76.NewConnector.ConnectionError += new Action<Exception>( class76.method_3 );
            class76.LogManager?.Sources.Add( class76.NewConnector );
            class76.NewConnector.SendInMessage( new ResetMessage( ) );
            class76.NewConnector.Connect( );
        }

        private void OnEditNetworkSetting( object sender, RoutedEventArgs e )
        {
            BaseApplication.EditProxySettings( this );
        }

        private void OnOkayButtonClicked( object sender, RoutedEventArgs e )
        {
            var enabledAdapters = _connectorRowsCollection.Where( r => r.IsEnabled ).Select( r => r.Adapter ).ToArray( );

            foreach ( IMessageAdapter adapter in enabledAdapters )
            {
                
            }            

            DialogResult = new bool?( true );
        }

        private void SupportedMessages_OnSelectedChanged( )
        {
            GetSelectedRow( )?.Refresh( );
        }

        public void Load( SettingsStorage storage )
        {
            this.LoadWindowSettings( storage );
            ConnectorsGrid.Load( storage.GetValue<SettingsStorage>( "ConnectorsGrid", null ) );
        }

        public void Save( SettingsStorage storage )
        {
            this.SaveWindowSettings( storage );
            storage.SetValue<SettingsStorage>( "ConnectorsGrid", ConnectorsGrid.Save( ) );
        }

        

        private void observableCollection_0_CollectionChanged(
          object sender,
          NotifyCollectionChangedEventArgs e )
        {
            ConnectorTypesGrid.ExpandAllGroups( );
        }

        private sealed class Class76
        {
            public Connector NewConnector;
            public ConnectorWindow connectorWindow_0;
            public LogManager LogManager;
            public Action action_0;

            internal void method_0( )
            {
                GuiDispatcher.GlobalDispatcher.AddAction( action_0 ?? ( action_0 = new Action( method_1 ) ) );
            }

            internal void method_1( )
            {
                NewConnector.Disconnect( );
                int num = ( int )new MessageBoxBuilder( ).Text( LocalizedStrings.Str1560 ).Owner( connectorWindow_0 ).Show( );
                connectorWindow_0.BusyIndicator.IsBusy = false;
                connectorWindow_0.Test.IsEnabled = true;
            }

            internal void method_2( )
            {
                LogManager?.Sources.Remove( NewConnector );
                Task.Factory.StartNew( new Action( NewConnector.Dispose ) );
            }

            internal void method_3( Exception exception_0 )
            {
                ConnectorWindow.Class78 class78 = new ConnectorWindow.Class78( );
                class78.class76_0 = this;
                class78.exception_0 = exception_0;
                LogManager?.Sources.Remove( NewConnector );
                Task.Factory.StartNew( new Action( NewConnector.Dispose ) );
                GuiDispatcher.GlobalDispatcher.AddAction( new Action( class78.method_0 ) );
            }
        }

        private sealed class GridRow : NotifiableObject
        {
            private bool bool_0 = true;
            private readonly ConnectorWindow _connectorWindow;
            private readonly ConnectorInfo _connectorInfo;
            private readonly IMessageAdapter _imessageAdapter;

            public GridRow(
              ConnectorWindow connectorWindow_1,
              ConnectorInfo connectorInfo_1,
              IMessageAdapter imessageAdapter_1 )
            {
                ConnectorWindow connectorWindow = connectorWindow_1;
                if ( connectorWindow == null )
                {
                    throw new ArgumentNullException( "parent" );
                }

                _connectorWindow = connectorWindow;
                ConnectorInfo connectorInfo = connectorInfo_1;
                if ( connectorInfo == null )
                {
                    throw new ArgumentNullException( "info" );
                }

                _connectorInfo = connectorInfo;
                IMessageAdapter messageAdapter = imessageAdapter_1;
                if ( messageAdapter == null )
                {
                    throw new ArgumentNullException( "adapter" );
                }

                _imessageAdapter = messageAdapter;
            }

            public ConnectorInfo Info
            {
                get
                {
                    return _connectorInfo;
                }
            }

            public IMessageAdapter Adapter
            {
                get
                {
                    return _imessageAdapter;
                }
            }

            public bool IsTransactionEnabled
            {
                get
                {
                    return Adapter.IsMessageSupported( MessageTypes.OrderRegister );
                }
            }

            public bool IsMarketDataEnabled
            {
                get
                {
                    if ( !Adapter.IsMessageSupported( MessageTypes.MarketData ) )
                    {
                        return Adapter.IsMessageSupported( MessageTypes.SecurityLookup );
                    }

                    return true;
                }
            }

            public bool IsEnabled
            {
                get
                {
                    return bool_0;
                }
                set
                {
                    bool_0 = value;
                    _connectorWindow.Adapter.InnerAdapters[ Adapter ] = value ? 0 : -1;
                    this.NotifyChanged( nameof( IsEnabled ) );
                }
            }

            public Uri Icon
            {
                get
                {
                    return Adapter.GetType( ).GetIconUrl( );
                }
            }

            public void Refresh( )
            {
                this.NotifyChanged( "IsTransactionEnabled" );
                this.NotifyChanged( "IsMarketDataEnabled" );
            }
        }

        private sealed class Class78
        {
            public Exception exception_0;
            public ConnectorWindow.Class76 class76_0;

            internal void method_0( )
            {
                int num = ( int )new MessageBoxBuilder( ).Text( exception_0.Message ).Caption( LocalizedStrings.Str1561 ).Error( ).Owner( class76_0.connectorWindow_0 ).Show( );
                class76_0.connectorWindow_0.BusyIndicator.IsBusy = false;
                class76_0.connectorWindow_0.Test.IsEnabled = true;
            }
        }
           
    }
}
