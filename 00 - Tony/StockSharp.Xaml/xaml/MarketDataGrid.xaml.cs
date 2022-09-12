using DevExpress.Xpf.Grid;
using Ecng.Common;
using Ecng.Collections;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using StockSharp.Logging;
using Ecng.ComponentModel;
using DevExpress.Data;
using DevExpress.XtraGrid;

namespace StockSharp.Xaml
{
    public partial class MarketDataGrid : BaseGridControl, IComponentConnector
    {
        private readonly Dictionary<string, GridColumn> _candleColumns = new Dictionary<string, GridColumn>( );
        private readonly SyncObject _lock = new SyncObject( );
        private readonly ObservableCollection<MarketDataGrid.MarketDataEntry> _itemSource;
        private Tuple<IStorageRegistry, Security, StorageFormats, IMarketDataDrive> tuple_0;
        private bool bool_2;
        private bool bool_3;
        
        public MarketDataGrid( )
        {
            InitializeComponent( );
            ItemsSource = ( object )( _itemSource = new ObservableCollection<MarketDataGrid.MarketDataEntry>( ) );
            CustomColumnSort += new CustomColumnSortEventHandler( MarketDataGrid_CustomColumnSort );
        }

        public event Action DataLoading;

        public event Action DataLoaded;

        public void BeginMakeEntries(
          IStorageRegistry storageRegistry,
          Security security,
          StorageFormats format,
          IMarketDataDrive drive )
        {
            if ( storageRegistry == null )
            {
                throw new ArgumentNullException( nameof( storageRegistry ) );
            }

            lock ( _lock )
            {
                tuple_0 = new Tuple<IStorageRegistry, Security, StorageFormats, IMarketDataDrive>( storageRegistry, security, format, drive );
                bool_3 = true;
                if ( bool_2 )
                {
                    return;
                }

                bool_2 = true;
                new Action( method_34 ).Thread( ).Launch( );
            }
        }

        public void CancelMakeEntires( )
        {
            lock ( _lock )
            {
                if ( !bool_2 )
                {
                    return;
                }

                tuple_0 = ( Tuple<IStorageRegistry, Security, StorageFormats, IMarketDataDrive> )null;
                bool_3 = true;
            }
        }

        private void method_30( )
        {
            try
            {
                this.GuiSync( new Action( method_35 ) );
                while ( true )
                {
                    Tuple<IStorageRegistry, Security, StorageFormats, IMarketDataDrive> tuple0;
                    lock ( _lock )
                    {
                        bool_3 = false;
                        if ( tuple_0 == null )
                        {
                            this.GuiAsync( new Action( method_36 ) );
                            bool_2 = false;
                            break;
                        }
                        tuple0 = tuple_0;
                        tuple_0 = ( Tuple<IStorageRegistry, Security, StorageFormats, IMarketDataDrive> )null;
                    }
                    try
                    {
                        method_31( tuple0.Item1, tuple0.Item2, tuple0.Item3, tuple0.Item4 );
                    }
                    catch ( EndpointNotFoundException ex )
                    {
                        ex.LogError( ( string )null );
                        GuiDispatcher.GlobalDispatcher.AddAction( new Action( method_37 ) );
                    }
                    catch ( Exception ex )
                    {
                        ex.LogError( ( string )null );
                        GuiDispatcher.GlobalDispatcher.AddAction( new Action( method_38 ) );
                    }
                }
            }
            catch ( Exception ex )
            {
                ex.LogError( ( string )null );
            }
        }

        private void method_31(
          IStorageRegistry istorageRegistry_0,
          Security security_0,
          StorageFormats storageFormats_0,
          IMarketDataDrive imarketDataDrive_0 )
        {
            MarketDataGrid.Class550 class550 = new MarketDataGrid.Class550( );
            class550.marketDataGrid_0 = this;
            this.GuiSync( new Action( class550.method_0 ) );
            if ( security_0 == null )
            {
                return;
            }

            class550.dictionary_0 = new Dictionary<DateTime, MarketDataGrid.MarketDataEntry>( );
            imarketDataDrive_0 = imarketDataDrive_0 ?? istorageRegistry_0.DefaultDrive;
            Dictionary<string, DataType> dictionary = new Dictionary<string, DataType>( );
            lock ( _lock )
            {
                if ( bool_3 )
                {
                    return;
                }
            }
            foreach ( DataType availableDataType in imarketDataDrive_0.GetAvailableDataTypes( security_0.ToSecurityId( ( SecurityIdGenerator )null ), storageFormats_0 ) )
            {
                if ( availableDataType.IsCandles( ) )
                {
                    dictionary.Add( availableDataType.ToString( ), availableDataType.Clone( ) );
                }
            }
            class550.string_0 = dictionary.Keys.ToArray<string>( );
            if ( class550.string_0.Length != 0 )
            {
                this.GuiSync( new Action( class550.method_1 ) );
            }

            method_32( ( IDictionary<DateTime, MarketDataGrid.MarketDataEntry> )class550.dictionary_0, istorageRegistry_0.GetTickMessageStorage( security_0, imarketDataDrive_0, storageFormats_0 ).Dates, class550.string_0, MarketDataGrid.Class554.action_0 ?? ( MarketDataGrid.Class554.action_0 = new Action<MarketDataGrid.MarketDataEntry>( MarketDataGrid.Class554.class554_0.method_0 ) ) );
            method_32( ( IDictionary<DateTime, MarketDataGrid.MarketDataEntry> )class550.dictionary_0, istorageRegistry_0.GetQuoteMessageStorage( security_0, imarketDataDrive_0, storageFormats_0 ).Dates, class550.string_0, MarketDataGrid.Class554.action_1 ?? ( MarketDataGrid.Class554.action_1 = new Action<MarketDataGrid.MarketDataEntry>( MarketDataGrid.Class554.class554_0.method_1 ) ) );
            method_32( ( IDictionary<DateTime, MarketDataGrid.MarketDataEntry> )class550.dictionary_0, istorageRegistry_0.GetLevel1MessageStorage( security_0, imarketDataDrive_0, storageFormats_0 ).Dates, class550.string_0, MarketDataGrid.Class554.action_2 ?? ( MarketDataGrid.Class554.action_2 = new Action<MarketDataGrid.MarketDataEntry>( MarketDataGrid.Class554.class554_0.method_2 ) ) );
            method_32( ( IDictionary<DateTime, MarketDataGrid.MarketDataEntry> )class550.dictionary_0, istorageRegistry_0.GetOrderLogMessageStorage( security_0, imarketDataDrive_0, storageFormats_0 ).Dates, class550.string_0, MarketDataGrid.Class554.DataLoading ?? ( MarketDataGrid.Class554.DataLoading = new Action<MarketDataGrid.MarketDataEntry>( MarketDataGrid.Class554.class554_0.method_3 ) ) );
            method_32( ( IDictionary<DateTime, MarketDataGrid.MarketDataEntry> )class550.dictionary_0, istorageRegistry_0.GetTransactionStorage( security_0, imarketDataDrive_0, storageFormats_0 ).Dates, class550.string_0, MarketDataGrid.Class554.DataLoaded ?? ( MarketDataGrid.Class554.DataLoaded = new Action<MarketDataGrid.MarketDataEntry>( MarketDataGrid.Class554.class554_0.method_4 ) ) );
            foreach ( string str in class550.string_0 )
            {
                MarketDataGrid.Class553 class553 = new MarketDataGrid.Class553( );
                class553.string_0 = str;
                lock ( _lock )
                {
                    if ( bool_3 )
                    {
                        return;
                    }
                }
                DataType dataType = dictionary[ class553.string_0 ];
                IEnumerable<DateTime> dates = istorageRegistry_0.GetCandleMessageStorage( dataType.MessageType, security_0, dataType.Arg, imarketDataDrive_0, storageFormats_0 ).Dates;
                method_32( ( IDictionary<DateTime, MarketDataGrid.MarketDataEntry> )class550.dictionary_0, dates, class550.string_0, new Action<MarketDataGrid.MarketDataEntry>( class553.method_0 ) );
            }
            if ( class550.dictionary_0.Count > 0 )
            {
                List<DateTime> list = class550.dictionary_0.Keys.Min<DateTime>( ).Range( class550.dictionary_0.Keys.Max<DateTime>( ), TimeSpan.FromDays( 1.0 ) ).Where<DateTime>( new Func<DateTime, bool>( class550.method_2 ) ).OrderBy<DateTime>( ).ToList<DateTime>( );
                foreach ( IGrouping<DateTime, DateTime> source in ( ( IEnumerable<DateTime> )list.ToArray( ) ).GroupBy<DateTime, DateTime>( MarketDataGrid.Class554.func_0 ?? ( MarketDataGrid.Class554.func_0 = new Func<DateTime, DateTime>( MarketDataGrid.Class554.class554_0.method_5 ) ) ) )
                {
                    DateTime dateTime1 = source.First<DateTime>( );
                    DateTime dateTime2 = source.Last<DateTime>( );
                    if ( dateTime1.Day == 1 && dateTime2.Day == source.Key.DaysInMonth( ) )
                    {
                        list.RemoveRange<DateTime>( ( IEnumerable<DateTime> )source );
                    }
                }
                method_32( ( IDictionary<DateTime, MarketDataGrid.MarketDataEntry> )class550.dictionary_0, ( IEnumerable<DateTime> )list, class550.string_0, MarketDataGrid.Class554.action_5 ?? ( MarketDataGrid.Class554.action_5 = new Action<MarketDataGrid.MarketDataEntry>( MarketDataGrid.Class554.class554_0.method_6 ) ) );
            }
            lock ( _lock )
            {
                if ( bool_3 )
                {
                    return;
                }
            }
            this.GuiSync( new Action( class550.method_3 ) );
        }

        private void method_32(
          IDictionary<DateTime, MarketDataGrid.MarketDataEntry> idictionary_0,
          IEnumerable<DateTime> ienumerable_0,
          string[ ] string_0,
          Action<MarketDataGrid.MarketDataEntry> action_5 )
        {
            lock ( _lock )
            {
                if ( bool_3 )
                {
                    return;
                }
            }
            foreach ( IEnumerable<DateTime> ienumerable_0_1 in ienumerable_0.Batch<DateTime>( 10 ) )
            {
                lock ( _lock )
                {
                    if ( bool_3 )
                    {
                        break;
                    }
                }
                MarketDataGrid.smethod_1( idictionary_0, ienumerable_0_1, ( IEnumerable<string> )string_0, action_5 );
            }
        }

        private static void smethod_1(
          IDictionary<DateTime, MarketDataGrid.MarketDataEntry> idictionary_0,
          IEnumerable<DateTime> ienumerable_0,
          IEnumerable<string> ienumerable_1,
          Action<MarketDataGrid.MarketDataEntry> action_5 )
        {
            ienumerable_0.Select<DateTime, MarketDataGrid.MarketDataEntry>( new Func<DateTime, MarketDataGrid.MarketDataEntry>( new MarketDataGrid.Class552( )
            {
                idictionary_0 = idictionary_0,
                ienumerable_0 = ienumerable_1
            }.method_0 ) ).ForEach<MarketDataGrid.MarketDataEntry>( action_5 );
        }

        private void MarketDataGrid_CustomColumnSort( object sender, CustomColumnSortEventArgs e )
        {
            MarketDataGrid.MarketDataEntry row1 = ( MarketDataGrid.MarketDataEntry )GetRow( e.ListSourceRowIndex1 );
            MarketDataGrid.MarketDataEntry row2 = ( MarketDataGrid.MarketDataEntry )GetRow( e.ListSourceRowIndex2 );
            if ( row1 == null || row2 == null )
            {
                return;
            }

            if ( !object.Equals( ( object )e.Column, ( object )MonthColumn ) )
            {
                string index = e.Column.Header.To<string>( );
                bool candle1 = row1.Candles[ index ];
                bool candle2 = row2.Candles[ index ];
                e.Result = Comparer<bool>.Default.Compare( candle1, candle2 );
            }
            else
            {
                e.Result = Comparer<int>.Default.Compare( row1.Month, row2.Month );
            }

            e.Handled = true;
        }       

        private void method_34( )
        {
            CultureInfo.InvariantCulture.DoInCulture( new Action( method_30 ) );
        }

        private void method_35( )
        {
            Action action3 = DataLoading;
            if ( action3 == null )
            {
                return;
            }

            action3( );
        }

        private void method_36( )
        {
            Action action4 = DataLoaded;
            if ( action4 == null )
            {
                return;
            }

            action4( );
        }

        private void method_37( )
        {
            int num = ( int )new MessageBoxBuilder( ).Owner( ( DependencyObject )this ).Error( ).Text( LocalizedStrings.ServerUnavailable ).Show( );
        }

        private void method_38( )
        {
            int num = ( int )new MessageBoxBuilder( ).Owner( ( DependencyObject )this ).Error( ).Text( LocalizedStrings.Str1538 ).Show( );
        }

        sealed class BoolToCheckMarkConverter : IValueConverter
        {
            object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
            {
                return ( bool )value ? "\u2713" : string.Empty;
            }

            object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
            {
                throw new NotSupportedException( );
            }
        }

        private sealed class Class550
        {
            public MarketDataGrid marketDataGrid_0;
            public string[ ] string_0;
            public Dictionary<DateTime, MarketDataGrid.MarketDataEntry> dictionary_0;

            internal void method_0( )
            {
                marketDataGrid_0._itemSource.Clear( );
                marketDataGrid_0.Columns.RemoveRange<GridColumn>( ( IEnumerable<GridColumn> )marketDataGrid_0._candleColumns.Values );
                marketDataGrid_0._candleColumns.Clear( );
            }

            internal void method_1( )
            {
                foreach ( string key in string_0 )
                {
                    GridColumn gridColumn1 = new GridColumn( );
                    gridColumn1.Header = ( object )key;
                    gridColumn1.Binding = ( BindingBase )new Binding( )
                    {
                        Path = new PropertyPath( "Candles[" + key + "]", new object[ 0 ] ),
                        Converter = ( IValueConverter )new BoolToCheckMarkConverter( )
                    };
                    gridColumn1.SortMode = ColumnSortMode.Custom;
                    GridColumn gridColumn2 = gridColumn1;
                    marketDataGrid_0.Columns.Add( gridColumn2 );
                    marketDataGrid_0._candleColumns.Add( key, gridColumn2 );
                }
            }

            internal bool method_2( DateTime dateTime_0 )
            {
                return !dictionary_0.ContainsKey( dateTime_0 );
            }

            internal void method_3( )
            {
                marketDataGrid_0._itemSource.AddRange<MarketDataGrid.MarketDataEntry>( ( IEnumerable<MarketDataGrid.MarketDataEntry> )dictionary_0.Values );
                ( marketDataGrid_0.View as TableView )?.BestFitColumns( );
                marketDataGrid_0.SortBy( marketDataGrid_0.DayColumn, ColumnSortOrder.Ascending );
            }
        }

        private sealed class MarketDataEntry : NotifiableObject
        {
            private readonly DateTime _date;
            private bool _isDepth;
            private bool _isTick;
            private bool _isOrderLog;
            private bool _isLevel1;
            private bool _isTransaction;
            private readonly Dictionary<string, bool> _candles;

            public MarketDataEntry( DateTime date, IEnumerable<string> candleKeys )
            {
                if ( candleKeys == null )
                {
                    throw new ArgumentNullException( "candleKeys" );
                }

                _date = date;
                _candles = new Dictionary<string, bool>( );

                candleKeys.ForEach( c => Candles[ c ] = false );
            }

            public DateTime Date
            {
                get
                {
                    return _date;
                }
            }

            public int Year
            {
                get
                {
                    return Date.Year;
                }
            }

            public int Month
            {
                get
                {
                    return Date.Month;
                }
            }

            public int Day
            {
                get
                {
                    return Date.Day;
                }
            }

            public bool IsDepth
            {
                get
                {
                    return _isDepth;
                }
                set
                {
                    _isDepth = value;
                    NotifyChanged( nameof( IsDepth ) );
                }
            }

            public bool IsTick
            {
                get
                {
                    return _isTick;
                }
                set
                {
                    _isTick = value;
                    NotifyChanged( nameof( IsTick ) );
                }
            }

            public bool IsOrderLog
            {
                get
                {
                    return _isOrderLog;
                }
                set
                {
                    _isOrderLog = value;
                    NotifyChanged( nameof( IsOrderLog ) );
                }
            }

            public bool IsLevel1
            {
                get
                {
                    return _isLevel1;
                }
                set
                {
                    _isLevel1 = value;
                    NotifyChanged( nameof( IsLevel1 ) );
                }
            }

            public bool IsTransaction
            {
                get
                {
                    return _isTransaction;
                }
                set
                {
                    _isTransaction = value;
                    NotifyChanged( nameof( IsTransaction ) );
                }
            }

            public Dictionary<string, bool> Candles
            {
                get
                {
                    return _candles;
                }
            }

            private void method_0( string string_0 )
            {
                Candles[ string_0 ] = false;
            }
        }

        private sealed class Class552
        {
            public IDictionary<DateTime, MarketDataGrid.MarketDataEntry> idictionary_0;
            public IEnumerable<string> ienumerable_0;
            public Func<DateTime, MarketDataGrid.MarketDataEntry> func_0;

            internal MarketDataGrid.MarketDataEntry method_0( DateTime dateTime_0 )
            {
                return idictionary_0.SafeAdd<DateTime, MarketDataGrid.MarketDataEntry>( dateTime_0, func_0 ?? ( func_0 = new Func<DateTime, MarketDataGrid.MarketDataEntry>( method_1 ) ) );
            }

            internal MarketDataGrid.MarketDataEntry method_1( DateTime dateTime_0 )
            {
                return new MarketDataGrid.MarketDataEntry( dateTime_0, ienumerable_0 );
            }
        }

        private sealed class Class553
        {
            public string string_0;

            internal void method_0( MarketDataGrid.MarketDataEntry class551_0 )
            {
                class551_0.Candles[ string_0 ] = true;
            }
        }

        [Serializable]
        private sealed class Class554
        {
            public static readonly MarketDataGrid.Class554 class554_0 = new MarketDataGrid.Class554( );
            public static Action<MarketDataGrid.MarketDataEntry> action_0;
            public static Action<MarketDataGrid.MarketDataEntry> action_1;
            public static Action<MarketDataGrid.MarketDataEntry> action_2;
            public static Action<MarketDataGrid.MarketDataEntry> DataLoading;
            public static Action<MarketDataGrid.MarketDataEntry> DataLoaded;
            public static Func<DateTime, DateTime> func_0;
            public static Action<MarketDataGrid.MarketDataEntry> action_5;

            internal void method_0( MarketDataGrid.MarketDataEntry class551_0 )
            {
                class551_0.IsTick = true;
            }

            internal void method_1( MarketDataGrid.MarketDataEntry class551_0 )
            {
                class551_0.IsDepth = true;
            }

            internal void method_2( MarketDataGrid.MarketDataEntry class551_0 )
            {
                class551_0.IsLevel1 = true;
            }

            internal void method_3( MarketDataGrid.MarketDataEntry class551_0 )
            {
                class551_0.IsOrderLog = true;
            }

            internal void method_4( MarketDataGrid.MarketDataEntry class551_0 )
            {
                class551_0.IsTransaction = true;
            }

            internal DateTime method_5( DateTime dateTime_0 )
            {
                return new DateTime( dateTime_0.Year, dateTime_0.Month, 1 );
            }

            internal void method_6( MarketDataGrid.MarketDataEntry class551_0 )
            {
            }
        }
    }
}
