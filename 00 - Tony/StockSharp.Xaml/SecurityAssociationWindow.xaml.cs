using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using Wintellect.PowerCollections;

namespace StockSharp.Xaml
{
    public partial class SecurityAssociationWindow : DXWindow, IComponentConnector
    {
        public static readonly DependencyProperty ConnectorsInfoProperty = DependencyProperty.Register( nameof( ConnectorsInfo ), typeof( IList<ConnectorInfo> ), typeof( SecurityAssociationWindow ), new PropertyMetadata( ( PropertyChangedCallback )null ) );
        public static readonly DependencyProperty SecurityProviderProperty = DependencyProperty.Register( nameof( SecurityProvider ), typeof( ISecurityProvider ), typeof( SecurityAssociationWindow ), new PropertyMetadata( ( PropertyChangedCallback )null ) );

        private readonly Dictionary<SecurityId, PairSet<string, SecurityId>> _secIdDictionary = new Dictionary<SecurityId, PairSet<string, SecurityId>>( );
        private readonly ObservableCollection<SecurityId> _masterGridItemSource = new ObservableCollection<SecurityId>( );
        private readonly ObservableCollection<SecurityAssociationWindow.SecurityAssociationInfo> _tradableGridItemSource = new ObservableCollection<SecurityAssociationWindow.SecurityAssociationInfo>( );
        private ISecurityAssociationStorage _associationStorage;
        
        public SecurityAssociationWindow( )
        {
            InitializeComponent( );
            MasterGrid.ItemsSource = ( object )_masterGridItemSource;
            TradableGrid.ItemsSource = ( object )_tradableGridItemSource;
            ConnectorsInfo = ( IList<ConnectorInfo> )new ThreadSafeObservableCollection<ConnectorInfo>( ( IListEx<ConnectorInfo> )new ObservableCollectionEx<ConnectorInfo>( ) );
        }

        public IList<ConnectorInfo> ConnectorsInfo
        {
            get
            {
                return ( IList<ConnectorInfo> )GetValue( SecurityAssociationWindow.ConnectorsInfoProperty );
            }
            private set
            {
                SetValue( SecurityAssociationWindow.ConnectorsInfoProperty, ( object )value );
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return ( ISecurityProvider )GetValue( SecurityAssociationWindow.SecurityProviderProperty );
            }
            set
            {
                SetValue( SecurityAssociationWindow.SecurityProviderProperty, ( object )value );
            }
        }

        public ISecurityAssociationStorage AssociationStorage
        {
            get
            {
                return _associationStorage;
            }
            set
            {
                _associationStorage = value;
                _secIdDictionary.Clear( );
                _masterGridItemSource.Clear( );
                if ( value == null )
                    return;
                foreach ( KeyValuePair<SecurityId, PairSet<string, SecurityId>> keyValuePair in ( IEnumerable<KeyValuePair<SecurityId, PairSet<string, SecurityId>>> )value.Load( ) )
                    _secIdDictionary.Add( keyValuePair.Key, keyValuePair.Value );
                _masterGridItemSource.AddRange<SecurityId>( ( IEnumerable<SecurityId> )_secIdDictionary.Keys );
                if ( _masterGridItemSource.Count <= 0 )
                    return;
                MasterGrid.SelectedItem = ( object )_masterGridItemSource[ 0 ];
            }
        }

        private void method_0( string string_0 )
        {
            int num = ( int )new MessageBoxBuilder( ).Text( string_0 ).Owner( ( Window )this ).Warning( ).Show( );
        }

        private void simpleButton_0_Click( object sender, RoutedEventArgs e )
        {
            SecurityId securityId = MasterId.Text.ToSecurityId( ( SecurityIdGenerator )null );
            if ( _secIdDictionary.ContainsKey( securityId ) )
            {
                method_0( LocalizedStrings.Str2927Params.Put( ( object )securityId ) );
            }
            else
            {
                MasterId.Text = ( string )null;
                _secIdDictionary.Add( securityId, new PairSet<string, SecurityId>( ( IEqualityComparer<string> )StringComparer.InvariantCultureIgnoreCase ) );
                _masterGridItemSource.Add( securityId );
                MasterGrid.SelectedItem = ( object )securityId;
            }
        }

        private void simpleButton_1_Click( object sender, RoutedEventArgs e )
        {
            foreach ( SecurityId key in MasterGrid.SelectedItems.Cast<SecurityId>( ).ToArray<SecurityId>( ) )
            {
                _secIdDictionary.Remove( key );
                _masterGridItemSource.Remove( key );
            }
        }

        private void method_1( object sender, RoutedEventArgs e )
        {
            KeyValuePair<SecurityId, PairSet<string, SecurityId>> keyValuePair1 = _secIdDictionary.FirstOrDefault<KeyValuePair<SecurityId, PairSet<string, SecurityId>>>( SecurityAssociationWindow.Class620.func_0 ?? ( SecurityAssociationWindow.Class620.func_0 = new Func<KeyValuePair<SecurityId, PairSet<string, SecurityId>>, bool>( SecurityAssociationWindow.Class620.class620_0.method_0 ) ) );
            if ( keyValuePair1.Value != null )
            {
                method_0( LocalizedStrings.Str682Params.Put( ( object )keyValuePair1.Key, null ) );
            }
            else
            {
                AssociationStorage.DeleteAll( );
                foreach ( KeyValuePair<SecurityId, PairSet<string, SecurityId>> keyValuePair2 in _secIdDictionary )
                    AssociationStorage.Save( keyValuePair2.Key, keyValuePair2.Value );
                DialogResult = new bool?( true );
            }
        }

        private void simpleButton_2_Click( object sender, RoutedEventArgs e )
        {
            _tradableGridItemSource.Add( new SecurityAssociationWindow.SecurityAssociationInfo( method_3( ) ) );
        }

        private void simpleButton_3_Click( object sender, RoutedEventArgs e )
        {
            SecurityAssociationWindow.SecurityAssociationInfo[ ] array = TradableGrid.SelectedItems.Cast<SecurityAssociationWindow.SecurityAssociationInfo>( ).ToArray<SecurityAssociationWindow.SecurityAssociationInfo>( );
            PairSet<string, SecurityId> pairSet = method_3( );
            foreach ( SecurityAssociationWindow.SecurityAssociationInfo class619 in array )
            {
                _tradableGridItemSource.Remove( class619 );
                if ( !class619.StorageName.IsEmpty( ) )
                    pairSet.Remove( class619.StorageName );
            }
        }

        private SecurityId? method_2( )
        {
            return ( SecurityId? )MasterGrid.SelectedItem;
        }

        private PairSet<string, SecurityId> method_3( )
        {
            return _secIdDictionary[ method_2( ).Value ];
        }

        private void OnTradableSelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            DeleteTradableBtn.IsEnabled = TradableGrid.SelectedItem != null;
        }

        private void OnSelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            SimpleButton simpleButton1 = DeleteMasterBtn;
            SimpleButton simpleButton2 = AddTradableBtn;
            SecurityId? nullable = method_2( );
            int num1;
            bool flag = ( num1 = nullable.HasValue ? 1 : 0 ) != 0;
            simpleButton2.IsEnabled = num1 != 0;
            int num2 = flag ? 1 : 0;
            simpleButton1.IsEnabled = num2 != 0;
            _tradableGridItemSource.Clear( );
            if ( !DeleteMasterBtn.IsEnabled )
                return;
            PairSet<string, SecurityId> pairSet_1 = method_3( );
            foreach ( KeyValuePair<string, SecurityId> keyValuePair in ( CollectionBase<KeyValuePair<string, SecurityId>> )pairSet_1 )
            {
                SecurityAssociationWindow.SecurityAssociationInfo class619 = new SecurityAssociationWindow.SecurityAssociationInfo( pairSet_1 ) { Init = true, StorageName = keyValuePair.Key, Security = SecurityProvider.LookupById( keyValuePair.Value ) };
                _tradableGridItemSource.Add( class619 );
                class619.Init = false;
            }
        }

        private void OnEditValueChangedEvent( object sender, EditValueChangedEventArgs e )
        {
            AddMasterBtn.IsEnabled = !MasterId.Text.IsEmpty( );
        }

        

        private sealed class SecurityAssociationInfo : NotifiableObject
        {
            private readonly PairSet<string, SecurityId> pairSet_0;
            private string _storageName;
            private Security _security;
            private bool _init;

            public SecurityAssociationInfo( PairSet<string, SecurityId> pairSet_1 )
            {
                PairSet<string, SecurityId> pairSet = pairSet_1;
                if ( pairSet == null )
                    throw new ArgumentNullException( "set" );
                pairSet_0 = pairSet;
            }

            public string StorageName
            {
                get
                {
                    return _storageName;
                }
                set
                {
                    if ( Init )
                    {
                        _storageName = value;
                    }
                    else
                    {
                        if ( _storageName.CompareIgnoreCase( value ) )
                            return;
                        if ( !value.IsEmpty( ) && pairSet_0.ContainsKey( value ) )
                            throw new ArgumentException( nameof( value ) );
                        if ( !_storageName.IsEmpty( ) )
                            pairSet_0.Remove( _storageName );
                        if ( !value.IsEmpty( ) && Security != null )
                            pairSet_0.Add( value, Security.ToSecurityId( ( SecurityIdGenerator )null ) );
                        _storageName = value;
                        NotifyChanged( nameof( StorageName ) );
                    }
                }
            }

            public Security Security
            {
                get
                {
                    return _security;
                }
                set
                {
                    if ( Init )
                    {
                        _security = value;
                    }
                    else
                    {
                        if ( _security == value )
                            return;
                        if ( value != null && pairSet_0.ContainsValue( value.ToSecurityId( ( SecurityIdGenerator )null ) ) )
                            throw new ArgumentException( nameof( value ) );
                        if ( _security != null && !StorageName.IsEmpty( ) )
                            pairSet_0.Remove( StorageName );
                        if ( value != null && !StorageName.IsEmpty( ) )
                            pairSet_0.Add( StorageName, value.ToSecurityId( ( SecurityIdGenerator )null ) );
                        _security = value;
                        NotifyChanged( nameof( Security ) );
                    }
                }
            }

            public bool Init
            {
                get
                {
                    return _init;
                }
                set
                {
                    _init = value;
                }
            }
        }

        [Serializable]
        private sealed class Class620
        {
            public static readonly SecurityAssociationWindow.Class620 class620_0 = new SecurityAssociationWindow.Class620( );
            public static Func<KeyValuePair<SecurityId, PairSet<string, SecurityId>>, bool> func_0;

            internal bool method_0(
              KeyValuePair<SecurityId, PairSet<string, SecurityId>> keyValuePair_0 )
            {
                return keyValuePair_0.Value.Count == 0;
            }
        }
    }
}
