using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Controls;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Hydra.Panes
{
    [Guid( "669CF6FF-D61C-4FAF-B0C9-D9CF026678FA" )]
    [DisplayNameLoc( "Str2835" )]
    [VectorIcon( "Certificate" )]
    public partial class AllSecuritiesPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {
        private readonly SecurityTypes[ ] _defaultSecurityTypes = new SecurityTypes[5] { SecurityTypes.Currency, SecurityTypes.Index, SecurityTypes.Stock, SecurityTypes.Future, SecurityTypes.CryptoCurrency };
        private IExtendedInfoStorageItem _extendedStorage;
        
        public AllSecuritiesPane()
        {
            InitializeComponent();
            Progress.Init( ExportBtn, MainGrid );
            SecurityPicker.SecurityProvider = SecurityProvider;
            SecurityPicker.ExcludeAllSecurity();
            ExportBtn.SetTypeEnabled( ExportTypes.StockSharp, false );
            MarketData.DataLoading += () => BusyIndicator.IsSplashScreenShown = new bool?( true );
            MarketData.DataLoaded += () => BusyIndicator.IsSplashScreenShown = new bool?( false );
            ExtendedInfoStorage.Deleted += new Action<IExtendedInfoStorageItem>( OnExtendedInfoStorageDeleted );
        }

        private static IExtendedInfoStorage ExtendedInfoStorage
        {
            get
            {
                return ServicesRegistry.ExtendedInfoStorage;
            }
        }

        private void DriveCtrl_OnSelectedDriveChanged()
        {
            UpdateMarketDataGrid();
        }

        private void SecurityPicker_OnSecuritySelected( Security security )
        {
            UpdateMarketDataGrid();
            DeleteSecurity.IsEnabled = SecurityPicker.SelectedSecurities.Any();
            if ( DeleteSecurity.IsEnabled )
                EditSecurities.IsEnabled = SecurityPicker.SelectedSecurities.GroupBy( s => s.GetType() ).Count() == 1;
            else
                EditSecurities.IsEnabled = false;
        }

        private void DrivePanel_OnStorageFormatChanged()
        {
            UpdateMarketDataGrid();
        }

        private void UpdateMarketDataGrid()
        {
            MarketDataGrid marketData = MarketData;
            IStorageRegistry storageRegistry = ServicesRegistry.StorageRegistry;
            Security selectedSecurity = SecurityPicker.SelectedSecurity;
            SecurityId? securityId = selectedSecurity != null ? new SecurityId?( selectedSecurity.ToSecurityId( null, true, false ) ) : new SecurityId?();
            int storageFormat = ( int )DrivePanel.StorageFormat;
            IMarketDataDrive selectedDrive = DrivePanel.SelectedDrive;
            marketData.BeginMakeEntries( storageRegistry, securityId, ( StorageFormats )storageFormat, selectedDrive );
        }

        private static ISecurityStorage SecurityStorage
        {
            get
            {
                return ServicesRegistry.SecurityStorage;
            }
        }

        private void CreateRegular( object sender, RoutedEventArgs e )
        {
            new SecurityCreateWindow()
            {
                SecurityStorage = SecurityStorage,
                Security = new Security()
            }.ShowModal( this );
        }

        private void CreateIndex( object sender, RoutedEventArgs e )
        {
            new IndexSecurityWindow()
            {
                SecurityStorage = SecurityStorage
            }.ShowModal( this );
        }

        private void CreateContinuous( object sender, RoutedEventArgs e )
        {
            new ContinuousSecurityWindow()
            {
                SecurityStorage = SecurityStorage,
                ExchangeInfoProvider = ExchangeInfoProvider
            }.ShowModal( this );
        }

        private void EditSingleSecurity( Security security )
        {
            if ( !security.IsBasket() )
                new SecurityCreateWindow()
                {
                    SecurityStorage = SecurityStorage,
                    Security = security
                }.ShowModal( this );
            else if ( security.IsIndex() )
            {
                new IndexSecurityWindow()
                {
                    SecurityStorage = SecurityStorage,
                    Security = security
                }.ShowModal( this );
            }
            else
            {
                if ( !security.IsContinuous() )
                    throw new ArgumentOutOfRangeException( nameof( security ), security.GetType(), LocalizedStrings.Str2117 );
                new ContinuousSecurityWindow()
                {
                    SecurityStorage = SecurityStorage,
                    ExchangeInfoProvider = ExchangeInfoProvider,
                    Security = security
                }.ShowModal( this );
            }
        }

        private void SecurityPicker_OnSecurityDoubleClick( Security security )
        {
            EditSingleSecurity( security );
        }

        private void EditSecurities_OnClick( object sender, RoutedEventArgs e )
        {
            Security[ ] array = SecurityPicker.SelectedSecurities.ToArray();
            if ( array.IsEmpty() )
                return;
            Security security = array[0];
            if ( array.Length == 1 )
                EditSingleSecurity( security );
            else if ( security.GetType() == typeof( Security ) )
                new SecurityCreateWindow()
                {
                    SecurityStorage = SecurityStorage,
                    Securities = array
                }.ShowModal( this );
            else
                EditSingleSecurity( security );
        }

        private void DeleteSecurity_OnClick( object sender, RoutedEventArgs e )
        {
            Security[ ] array = SecurityPicker.SelectedSecurities.ToArray();
            if ( new MessageBoxBuilder().Owner( this ).Warning().Text( LocalizedStrings.DeleteNSecurities.Put( array.Length ) ).OkCancel().Show() != MessageBoxResult.OK )
                return;
            HydraTaskManager.Instance.Delete( array );
        }

        bool IPane.IsValid
        {
            get
            {
                return true;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            DrivePanel.Load( storage.GetValue<SettingsStorage>( "DrivePanel", null ) );
            MarketData.Load( storage.GetValue<SettingsStorage>( "MarketData", null ) );
            SecurityPicker.Load( storage.GetValue<SettingsStorage>( "SecurityPicker", null ) );
            string str = storage.GetValue<string>( "ExtendedInfo", null );
            if ( !str.IsEmpty() )
            {
                _extendedStorage = ExtendedInfoStorage.Get( str );
                ApplyExtendedStorage();
            }
            base.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            storage.SetValue( "DrivePanel", DrivePanel.Save() );
            storage.SetValue( "MarketData", MarketData.Save() );
            storage.SetValue( "SecurityPicker", SecurityPicker.Save() );
            storage.SetValue( "SecurityPicker", SecurityPicker.Save() );
            storage.SetValue( "ExtendedInfo", _extendedStorage?.StorageName );
            base.Save( storage );
        }

        public override void Dispose()
        {
            Progress.Stop();
            MarketData.CancelMakeEntires();
            base.Dispose();
        }

        private void ExportBtn_OnExportStarted()
        {
            IListEx<Security> securities = SecurityPicker.FilteredSecurities;
            if ( securities.Count == 0 )
            {
                Progress.DoesntExist();
            }
            else
            {
                StorageFormats format = StorageFormats.Csv;
                object path = ExportBtn.GetPath( null, null, DataType.Securities, new DateTime?(), new DateTime?(), null, ref format );
                if ( path == null )
                    return;
                Progress.Start( null, DataType.Securities, ( s1, f, t ) => securities.Select( s => s.ToMessage( new SecurityId?(), 0L, false ) ), securities.Count, path, new DateTime?(), new DateTime?(), false, null, format );
            }
        }

        private void ExtendedInfo_OnClick( object sender, RoutedEventArgs e )
        {
            if ( SecurityPicker.ExtendedInfoStorage != null )
            {
                SecurityPicker.ExtendedInfoStorage = null;
                ExtendedInfo.Content = LocalizedStrings.ExtendedInfo;
                _extendedStorage = null;
            }
            else
            {
                ExtendedInfoStorageWindow wnd = new ExtendedInfoStorageWindow();
                if ( !wnd.ShowModal( this ) )
                    return;
                _extendedStorage = wnd.SelectedStorage;
                ApplyExtendedStorage();
            }
        }

        private void OnExtendedInfoStorageDeleted( IExtendedInfoStorageItem storage )
        {
            this.GuiAsync( () =>
         {
             if ( SecurityPicker.ExtendedInfoStorage != storage )
                 return;
             SecurityPicker.ExtendedInfoStorage = null;
         } );
        }

        private void ApplyExtendedStorage()
        {
            SecurityPicker.ExtendedInfoStorage = _extendedStorage;
            ExtendedInfo.Content = LocalizedStrings.ExtendedInfo + ": " + _extendedStorage?.StorageName;
        }

        private void SecurityMappings_OnClick( object sender, RoutedEventArgs e )
        {
            SecurityMappingWindow wnd = new SecurityMappingWindow();
            foreach ( IMessageAdapter possibleAdapter in ServicesRegistry.AdapterProvider.PossibleAdapters )
                wnd.ConnectorsInfo.Add( new ConnectorInfo( possibleAdapter ) );
            wnd.Storage = ServicesRegistry.MappingStorage;
            wnd.ShowModal( this );
        }

        private void DriveCtrl_OnChanged( IMarketDataDrive drive, bool isNew )
        {
            if ( new MessageBoxBuilder().Text( LocalizedStrings.ImportSecurities ).Question().YesNo().Owner( this ).Show() != MessageBoxResult.Yes )
                return;
            SecurityLookupWindow wnd = new SecurityLookupWindow() { CriteriaMessage = new SecurityLookupMessage() { SecurityTypes = _defaultSecurityTypes } };
            if ( !wnd.ShowModal( this ) )
                return;
            OnStorageLookup( wnd.CriteriaMessage );
        }

        private void OnStorageLookup( SecurityLookupMessage filter )
        {
            IMarketDataDrive drive = DrivePanel.SelectedDrive;
            Task.Run( () =>
            {
                try
                {
                    int count = 0;
                    ISecurityStorage securityStorage = SecurityStorage;
                    IExchangeInfoProvider exchangeInfoProvider = ExchangeInfoProvider;
                    drive.LookupSecurities( filter, securityStorage, msg =>
              {
                  securityStorage.Save( msg.ToSecurity( exchangeInfoProvider ), false );
                  ++count;
              }, () => false, ( i, c ) => { } );
                    return count;
                }
                catch ( SystemException ex )
                {
                    ex.LogError( null );
                    throw new InvalidOperationException( LocalizedStrings.ServerUnavailable );
                }
            } ).ContinueWith( t =>
            {
                Exception innerException = t.Exception?.InnerException;
                if ( innerException == null )
                    return;
                innerException.LogError( null );
            } );
        }

        
    }
}
