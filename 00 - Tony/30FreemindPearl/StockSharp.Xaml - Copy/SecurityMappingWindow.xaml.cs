using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Wintellect.PowerCollections;

namespace StockSharp.Xaml
{
    public partial class SecurityMappingWindow : DXWindow, IPersistable
    {
        public static readonly RoutedCommand RemoveCommand = new RoutedCommand();
        public static readonly RoutedCommand AddCommand = new RoutedCommand();
        private readonly ObservableCollection<SecurityIdMappingItem> _itemSource = new ObservableCollection<SecurityIdMappingItem>();
        private readonly IList<ConnectorInfo> _conectorInfo;
        private ISecurityMappingStorage _storage;
        

        public SecurityMappingWindow( )
        {
            _conectorInfo = ( IList<ConnectorInfo> ) new ThreadSafeObservableCollection<ConnectorInfo>( ( IListEx<ConnectorInfo> ) new ObservableCollectionEx<ConnectorInfo>() );
            InitializeComponent();

            SecuritiesGrid.ItemsSource = _itemSource;
        }

        public IList<ConnectorInfo> ConnectorsInfo
        {
            get
            {
                return _conectorInfo;
            }
        }

        public ISecurityMappingStorage Storage
        {
            get
            {
                return _storage;
            }
            set
            {
                ISecurityMappingStorage isecurityMappingStorage = value;
                if ( isecurityMappingStorage == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _storage = isecurityMappingStorage;
            }
        }

        private void OnSecurityMappingWindowLoaded( object sender, RoutedEventArgs e )
        {
            DataContext = this;
            _itemSource.Clear();

            foreach ( string storageName in Storage.GetStorageNames() )
            {
                var storageNames = Storage.Get( storageName );

                foreach ( var item in storageNames )
                {
                    _itemSource.Add( new SecurityIdMappingItem( storageName, item ) );
                }
            }
        }

        private IEnumerable<SecurityIdMappingItem> SelectedGridItems( )
        {
            var grid = SecuritiesGrid;
            IEnumerable<SecurityIdMappingItem> output;

            if ( grid == null )
            {
                output = null;
            }
            else
            {
                output = grid.SelectedItems.Cast<SecurityIdMappingItem>();

                if ( output != null )
                {
                    return output;
                }
            }
            return Enumerable.Empty<SecurityIdMappingItem>();
        }

        private void CanExecuteRemoveCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedGridItems().Any<SecurityIdMappingItem>();
        }

        private void ExecuteRemoveCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _itemSource.RemoveRange<SecurityIdMappingItem>( SelectedGridItems().ToArray() );
        }

        private void CanExecuteAddCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void ExecuteAddCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _itemSource.Add( new SecurityIdMappingItem() );
        }

        private void OnOkButtonClicked( object sender, RoutedEventArgs e )
        {
            Dictionary<string, PairSet<SecurityId, SecurityId>> nameToSecurityIdDict = new Dictionary<string, PairSet<SecurityId, SecurityId>>();
            foreach ( SecurityIdMappingItem item in _itemSource )
            {
                if ( !item.StorageName.IsEmpty() )
                {
                    if ( !item.SecurityCode.IsEmpty() && !item.BoardCode.IsEmpty() && ( !item.AdapterBoard.IsEmpty() && !item.AdapterBoard.IsEmpty() ) )
                    {
                        PairSet<SecurityId, SecurityId> storageNameDict = (PairSet<SecurityId, SecurityId>) nameToSecurityIdDict.SafeAdd( item.StorageName );
                        SecurityId securityIdKey = NewSecurityId( item.SecurityCode, item.BoardCode );
                        SecurityId securityIdValue = NewSecurityId( item.AdapterCode, item.AdapterBoard );

                        if ( ! storageNameDict.ContainsKey( securityIdKey ) && !storageNameDict.ContainsValue( securityIdValue ) )
                        {
                            storageNameDict.Add( securityIdKey, securityIdValue );
                        }
                        else
                        {
                            ShowMessage( LocalizedStrings.SecurityOrBoardCodeDuplicatedParams.Put( item ) );
                            return;
                        }
                    }
                    else
                    {
                        ShowMessage( LocalizedStrings.SecurityOrBoardCodeNotSpecifiedParams.Put( item ) );
                        return;
                    }
                }
                else
                {
                    ShowMessage( LocalizedStrings.ConnectionNotSpecifiedParams.Put( item ) );
                    return;
                }
            }

            foreach ( KeyValuePair<string, PairSet<SecurityId, SecurityId>> item in nameToSecurityIdDict )
            {
                string StorageName = item.Key;               


                var idMappings = Storage.Get( StorageName );

                foreach ( var idMapping in idMappings )
                {
                    SecurityId toBeRemoved = item.Value.TryGetAndRemove( idMapping.StockSharpId );

                    if ( toBeRemoved.IsDefault() )
                    {
                        Storage.Remove( StorageName, idMapping.StockSharpId );
                    }
                    else if ( !toBeRemoved.Equals( idMapping.AdapterId ) )
                    {
                        Storage.Add( StorageName, idMapping );
                    }
                }

                foreach ( KeyValuePair<SecurityId, SecurityId> valueItem in item.Value )
                {
                    Storage.Add( StorageName, ( SecurityIdMapping )( valueItem ) );
                }
            }

            DialogResult = true;
        }

        private static SecurityId NewSecurityId( string securityCode, string boardCode )
        {
            SecurityId securityId = new SecurityId();
            securityId.SecurityCode = ( securityCode );
            securityId.BoardCode = ( boardCode );

            return securityId;
        }

        private static void ShowMessage( string msg )
        {
            int num = (int) new MessageBoxBuilder().Caption(LocalizedStrings.SecuritiesAndConnections ).Warning().Text(msg).Show();
        }

        public void Load( SettingsStorage storage )
        {
            ( ( Window ) this ).LoadWindowSettings( storage );
            SettingsStorage storage1 = (SettingsStorage) storage.GetValue<SettingsStorage>("SecuritiesGrid",  null);
            if ( storage1 != null )
            {
                SecuritiesGrid.Load( storage1 );
            } ( ( DependencyObject ) BarManager ).LoadDevExpressControl( ( string ) storage.GetValue<string>( "BarManager", null ) );
        }

        public void Save( SettingsStorage storage )
        {
            ( ( Window ) this ).SaveWindowSettings( storage );
            storage.SetValue<SettingsStorage>( "SecuritiesGrid", PersistableHelper.Save( ( IPersistable ) SecuritiesGrid ) );
            storage.SetValue<string>( "BarManager", ( ( DependencyObject ) BarManager ).SaveDevExpressControl() );
        }



        private sealed class SecurityIdMappingItem
        {
            private string _storageName;
            private string _securityCode;
            private string _boardCode;
            private string _adapterCode;
            private string _adapterBoard;

            public SecurityIdMappingItem( )
            {
            }

            public SecurityIdMappingItem( string storageName, SecurityIdMapping mapping )
            {
                StorageName = storageName;
                SecurityCode = mapping.StockSharpId.SecurityCode;
                BoardCode = mapping.StockSharpId.BoardCode;
                AdapterCode = mapping.AdapterId.SecurityCode;
                AdapterBoard = mapping.AdapterId.BoardCode;
            }

            public string StorageName
            {
                get
                {
                    return _storageName;
                }
                set
                {
                    _storageName = value;
                }
            }

            public string SecurityCode
            {
                get
                {
                    return _securityCode;
                }
                set
                {
                    _securityCode = value;
                }
            }

            public string BoardCode
            {
                get
                {
                    return _boardCode;
                }
                set
                {
                    _boardCode = value;
                }
            }

            public string AdapterCode
            {
                get
                {
                    return _adapterCode;
                }
                set
                {
                    _adapterCode = value;
                }
            }

            public string AdapterBoard
            {
                get
                {
                    return _adapterBoard;
                }
                set
                {
                    _adapterBoard = value;
                }
            }

            public override string ToString( )
            {
                return string.Format( "{0}: {1}@{2} <-> {3}@{4}", ( object ) StorageName, ( object ) SecurityCode, ( object ) BoardCode, ( object ) AdapterCode, ( object ) AdapterBoard );
            }
        }
    }
}