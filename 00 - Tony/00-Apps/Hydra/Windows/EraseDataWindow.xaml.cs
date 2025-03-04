using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Controls;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Hydra.Windows
{
    public partial class EraseDataWindow : ThemedWindow, IComponentConnector
    {
        private static readonly DataType _candles = DataType.Create( typeof( TimeFrameCandleMessage ), null ).SetName( LocalizedStrings.Candles );
        private CancellationTokenSource _token;

        private IEnumerable<Security> SelectedSecurities
        {
            get
            {
                return SelectSecurityBtn.SelectedSecurities;
            }
        }

        public IStorageRegistry StorageRegistry { get; set; }

        public IEntityRegistry EntityRegistry { get; set; }

        public EraseDataWindow()
        {
            InitializeComponent();
            DataTypeComboBox.SetItemsSource( new DataType[8]
            {
        DataType.Ticks,
        DataType.MarketDepth,
        _candles,
        DataType.OrderLog,
        DataType.Level1,
        DataType.Transactions,
        DataType.PositionChanges,
        DataType.News
            }, null, null );
            From.DateTime = DateTime.Today - TimeSpan.FromDays( 7.0 );
            To.DateTime = DateTime.Today + TimeSpan.FromDays( 1.0 );
            Drive.SelectedDrive = ServicesRegistry.DriveCache.Drives.FirstOrDefault();
            CandleSettings.Settings = new CandleSeries()
            {
                CandleType = typeof( TimeFrameCandle ),
                Arg = TimeSpan.FromMinutes( 1.0 )
            };
        }

        protected override void OnClosing( CancelEventArgs e )
        {
            if ( _token != null )
            {
                if ( Erase.IsEnabled && new MessageBoxBuilder().Text( LocalizedStrings.Str2928 ).Warning().YesNo().Owner( this ).Show() == MessageBoxResult.Yes )
                    StopSync();
                e.Cancel = true;
            }
            base.OnClosing( e );
        }

        private void StopSync()
        {
            Erase.IsEnabled = false;
            _token.Cancel();
        }

        private void Erase_Click( object sender, RoutedEventArgs e )
        {
            if ( _token != null )
            {
                StopSync();
            }
            else
            {
                bool? isChecked = AllSecurities.IsChecked;
                bool flag1 = true;
                Security[ ] securities = isChecked.GetValueOrDefault() == flag1 & isChecked.HasValue
                    ? null
                    : SelectedSecurities.ToArray();
                isChecked = AllDates.IsChecked;
                bool flag2 = true;
                DateTimeOffset? nullable1;
                if ( !( isChecked.GetValueOrDefault() == flag2 & isChecked.HasValue ) )
                {
                    DateTime? editValue = ( DateTime? )From.EditValue;
                    nullable1 = editValue.HasValue
                        ? new DateTimeOffset?( editValue.GetValueOrDefault() )
                        : new DateTimeOffset?();
                }
                else
                {
                    nullable1 = new DateTimeOffset?();
                }

                DateTimeOffset? from = nullable1;
                isChecked = AllDates.IsChecked;
                bool flag3 = true;
                DateTimeOffset? nullable2;
                if ( !( isChecked.GetValueOrDefault() == flag3 & isChecked.HasValue ) )
                {
                    DateTime? editValue = ( DateTime? )To.EditValue;
                    nullable2 = editValue.HasValue
                        ? new DateTimeOffset?( editValue.GetValueOrDefault() )
                        : new DateTimeOffset?();
                }
                else
                {
                    nullable2 = new DateTimeOffset?();
                }

                DateTimeOffset? to = nullable2;
                DateTimeOffset? nullable3 = from;
                DateTimeOffset? nullable4 = to;
                if ( ( nullable3.HasValue & nullable4.HasValue
                        ? ( nullable3.GetValueOrDefault() > nullable4.GetValueOrDefault() ? 1 : 0 )
                        : 0 ) !=
                    0 )
                {
                    int num1 = ( int )new MessageBoxBuilder().Caption( LocalizedStrings.Str2879 )
                        .Text( LocalizedStrings.Str1119Params.Put( from, to ) )
                        .Warning()
                        .Owner( this )
                        .Show();
                }
                else
                {
                    string str = string.Empty;
                    DateTimeOffset dateTimeOffset;
                    if ( from.HasValue || to.HasValue )
                    {
                        str += LocalizedStrings.Str3846;
                        if ( from.HasValue )
                        {
                            string[ ] strArray = new string[5]
                            {
                                str,
                                " ",
                                LocalizedStrings.XamlStr624.ToLowerInvariant( ),
                                " ",
                                null
                            };
                            dateTimeOffset = from.Value;
                            strArray[4] = dateTimeOffset.ToString( "d" );
                            str = string.Concat( strArray );
                        }
                        if ( to.HasValue )
                        {
                            string[ ] strArray = new string[5] { str, " ", LocalizedStrings.till, " ", null };
                            dateTimeOffset = to.Value;
                            strArray[4] = dateTimeOffset.ToString( "d" );
                            str = string.Concat( strArray );
                        }
                    }
                    if ( new MessageBoxBuilder().Caption( LocalizedStrings.Str2879 )
                            .Text( LocalizedStrings.Str2884Params.Put( str ) )
                            .Warning()
                            .YesNo()
                            .Owner( this )
                            .Show() !=
                        MessageBoxResult.Yes )
                    {
                        return;
                    }

                    Erase.Content = LocalizedStrings.Str2890;
                    _token = new CancellationTokenSource();
                    isChecked = AllDrives.IsChecked;
                    bool flag4 = true;
                    IEnumerable<IMarketDataDrive> marketDataDrives;
                    if ( !( isChecked.GetValueOrDefault() == flag4 & isChecked.HasValue ) )
                    {
                        marketDataDrives = ( new IMarketDataDrive[1]
                        {
                            Drive.SelectedDrive
                        } );
                    }
                    else
                    {
                        marketDataDrives = ServicesRegistry.DriveCache.Drives;
                    }

                    IEnumerable<IMarketDataDrive> drives = marketDataDrives;
                    if ( to.HasValue )
                    {
                        dateTimeOffset = to.Value;
                        if ( dateTimeOffset.TimeOfDay == TimeSpan.Zero )
                        {
                            to = new DateTimeOffset?( to.Value.EndOfDay() );
                        }
                    }
                    DataType selectedDataType = DataTypeComboBox.GetSelected<DataType>();
                    TimeSpan time = TimeSpan.Zero;
                    Task.Factory
                        .StartNew(
                            ( Action )( () => time =
                                Watch.Do(
                                    () =>
                                    {
                                        StorageFormats[ ] array = Enumerator.GetValues<StorageFormats>().ToArray();
                                        Security[ ] securityArray = securities;
                                        int iterCount = ( securityArray != null
                                                            ? securityArray.Length
                                                            : ( ( ICollection<Security> )EntityRegistry.Securities ).Count ) *
                                            ( selectedDataType == null ? 5 : 1 ) *
                                            array.Length;
                                        this.GuiSync( () => Progress.Maximum = iterCount );
                                        if ( _token.IsCancellationRequested )
                                        {
                                            return;
                                        }

                                        foreach ( IMarketDataDrive drive in drives )
                                        {
                                            if ( _token.IsCancellationRequested )
                                            {
                                                break;
                                            }

                                            foreach ( SecurityId securityId in drive.AvailableSecurities.ToArray() )
                                            {
                                                if ( !_token.IsCancellationRequested )
                                                {
                                                    string id = securityId.ToStringId( null );
                                                    Security security = securities == null
                                                        ? EntityRegistry.Securities.ReadById( id )
                                                        : securities.FirstOrDefault(
                                                            s => s.Id.EqualsIgnoreCase( id ) );
                                                    if ( security != null )
                                                    {
                                                        foreach ( StorageFormats format in array )
                                                        {
                                                            if ( !_token.IsCancellationRequested )
                                                            {
                                                                foreach ( DataType dataType in drive.GetAvailableDataTypes(
                                                                    securityId,
                                                                    format )
                                                                    .ToArray() )
                                                                {
                                                                    if ( !_token.IsCancellationRequested )
                                                                    {
                                                                        if ( !( selectedDataType != null ) ||
                                                                            !( selectedDataType != dataType ) )
                                                                        {
                                                                            StorageRegistry.GetStorage(
                                                                                security,
                                                                                dataType.MessageType,
                                                                                dataType.Arg,
                                                                                drive,
                                                                                format )
                                                                                .Delete( from, to );
                                                                            this.GuiSync(
                                                                                ( Action )( () => ++Progress.Value ) );
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                    } ) ),
                            _token.Token )
                        .ContinueWithExceptionHandling(
                            this,
                            res =>
                            {
                                if ( res )
                                {
                                    int num = ( int )new MessageBoxBuilder().Caption( LocalizedStrings.Str2879 )
                                        .Text( LocalizedStrings.Str3024.Put( time ) )
                                        .Info()
                                        .Owner( this )
                                        .Show();
                                }
                                Erase.Content = LocalizedStrings.Str2060;
                                Erase.IsEnabled = true;
                                Progress.Value = 0.0;
                                _token = null;
                            } );
                }
            }
        }


        private void SelectSecurityBtn_SecuritySelected()
        {
            TryEnableErase();
        }

        private void DataTypeComboBox_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( CandleSettings == null )
                return;
            CandleSettings.Visibility = DataTypeComboBox.GetSelected<DataType>() == _candles ? Visibility.Visible : Visibility.Collapsed;
        }

        private void AllSecurities_Click( object sender, RoutedEventArgs e )
        {
            TryEnableErase();
        }

        private void Drive_OnSelectionChanged( object sender, EditValueChangedEventArgs e )
        {
            TryEnableErase();
        }

        private void AllDrives_Click( object sender, RoutedEventArgs e )
        {
            TryEnableErase();
        }

        private void AllDates_Click( object sender, RoutedEventArgs e )
        {
            TryEnableErase();
        }

        private void TryEnableErase()
        {
            SimpleButton erase = Erase;
            bool? isChecked1 = AllDrives.IsChecked;
            bool flag1 = true;
            int num;
            if ( isChecked1.GetValueOrDefault() == flag1 & isChecked1.HasValue || Drive.SelectedDrive != null )
            {
                bool? isChecked2 = AllSecurities.IsChecked;
                bool flag2 = true;
                num = isChecked2.GetValueOrDefault() == flag2 & isChecked2.HasValue ? 1 : ( !SelectedSecurities.IsEmpty() ? 1 : 0 );
            }
            else
                num = 0;
            erase.IsEnabled = num != 0;
        }


    }
}
