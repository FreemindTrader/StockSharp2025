using DevExpress.Utils;
using DevExpress.Xpf.Grid;
using DevExpress.XtraGrid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class SecurityGrid : BaseGridControl, IComponentConnector
    {
        private readonly SynchronizedDictionary<SecurityItem, RefTriple<Dictionary<Level1Fields, object>, DateTimeOffset, DateTimeOffset>> _securityItemTriDictionary  = new SynchronizedDictionary<SecurityItem, RefTriple<Dictionary<Level1Fields, object>, DateTimeOffset, DateTimeOffset>>( );
        private readonly List<GridColumn> _listGridColumns  = new List<GridColumn>( );
        private readonly Dictionary<string, IDictionary<string, object>> _extendedInfoDictionary  = new Dictionary<string, IDictionary<string, object>>( StringComparer.InvariantCultureIgnoreCase );
        private readonly SecuritiesCollection _securitiesCollection;
        private readonly ConvertibleObservableCollection<Security, SecurityItem> _securities;
        private readonly ObservableCollectionEx<SecurityItem> _securityItemCollection;
        private object _periodicObject;
        private IExtendedInfoStorageItem _extendedInfoStorage;
        private IMarketDataProvider _marketDataProvider;
        private IPriceChartDataProvider _ipriceChartDataProvider;
        ///private bool bool_2;

        public SecurityGrid( )
        {
            InitializeComponent( );

            _securityItemCollection = new ObservableCollectionEx<SecurityItem>( );
            ItemsSource             = _securityItemCollection;
            _securities             = new ConvertibleObservableCollection<Security, SecurityItem>( new ThreadSafeObservableCollection<SecurityItem>( _securityItemCollection ), new Func<Security, SecurityItem>( Converter ) );
            _securitiesCollection   = new SecuritiesCollection( this );
            CustomColumnSort       += new CustomColumnSortEventHandler( SecurityGrid_CustomColumnSort );
        }

        public Security SelectedSecurity
        {
            get
            {
                return ( ( SecurityItem )SelectedItem )?.Security;
            }
            set
            {
                if ( SelectionMode != MultiSelectMode.Row )
                    SelectedItems.Clear( );
                SelectedItem = value == null ? null : TryGetSecurity( value );
            }
        }

        public IListEx<Security> Securities
        {
            get
            {
                return _securities;
            }
        }

        public IList<Security> SelectedSecurities
        {
            get
            {
                return _securitiesCollection;
            }
        }

        public IExtendedInfoStorageItem ExtendedInfoStorage
        {
            get
            {
                return _extendedInfoStorage;
            }
            set
            {
                if ( _extendedInfoStorage == value )
                {
                    return;
                }
                    
                if ( _extendedInfoStorage != null )
                {
                    CollectionHelper.RemoveRange( Columns, _listGridColumns  );
                    _listGridColumns .Clear( );
                    _extendedInfoDictionary .Clear( );
                }
                _extendedInfoStorage = value;

                if ( _extendedInfoStorage == value )
                {
                    return;
                }

                var loaded = _extendedInfoStorage.Load( );

                _extendedInfoDictionary.AddRange( loaded.ToDictionary( x => x.Item1.ToStringId( null ), y => y.Item2, StringComparer.InvariantCultureIgnoreCase ) );


                foreach ( Tuple<string, Type> field in _extendedInfoStorage.Fields )
                {
                    string str = field.Item1;
                    var col = new GridColumn( );
                    col.Header = str;
                    col.Width = 100.0;
                    col.FieldName = str;
                    col.Binding = new Binding( )
                    {
                        Path = new PropertyPath( "RowData.Row.ExtendedInfo", new object[ 0 ] ),
                        Converter = new DictionaryConverter( ),
                        ConverterParameter = str
                    };
                    col.SortMode = ColumnSortMode.Custom;
                    col.AllowSorting = DefaultBoolean.True;
                    
                    Columns.Add( col );
                    _listGridColumns .Add( col );
                }
            }
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return _marketDataProvider;
            }
            set
            {
                if ( value == _marketDataProvider )
                    return;

                if ( _marketDataProvider != null )
                {
                    _marketDataProvider.ValuesChanged -= MarketDataProviderOnValuesChanged;
                }
                    
                _marketDataProvider = value;

                if ( _marketDataProvider == null )
                {
                    return;
                }
                    
                _marketDataProvider.ValuesChanged += MarketDataProviderOnValuesChanged;
            }
        }

        public IPriceChartDataProvider PriceChartDataProvider
        {
            get
            {
                return _ipriceChartDataProvider;
            }
            set
            {
                _ipriceChartDataProvider = value;
            }
        }

        private void MarketDataProviderOnValuesChanged(
                                                          Security sec,
                                                          IEnumerable<KeyValuePair<Level1Fields, object>> changes,
                                                          DateTimeOffset serverTime,
                                                          DateTimeOffset localTime 
                                                     )
        {
            SecurityItem data = TryGetSecurity( sec );

            if ( data == null )
            {
                return;
            }
                
            lock ( _securityItemTriDictionary.SyncRoot )
            {
                var refTriple =   _securityItemTriDictionary.SafeAdd( data, i => RefTuple.Create( new Dictionary<Level1Fields, object>( ), new DateTimeOffset( ), new DateTimeOffset( ) ) );

                foreach ( var change in changes )
                {
                    refTriple.First[ change.Key ] = change.Value;
                }
                
                refTriple.Second = ( serverTime );
                refTriple.Third = ( localTime );
            }
        }

        private SecurityItem TryGetSecurity( Security sec )
        {
            return _securities.TryGet( sec );
        }

        private SecurityItem Converter( Security sec )
        {
            SecurityItem item = new SecurityItem( sec, this );
            IMarketDataProvider marketDataProvider = MarketDataProvider;
            IDictionary<Level1Fields, object> dictionary = marketDataProvider != null ? TraderHelper.GetSecurityValues( marketDataProvider, sec ) : null;

            if ( dictionary != null )
            {
                TraderHelper.ApplyChanges( item, dictionary, TimeHelper.NowWithOffset, TimeHelper.NowWithOffset );
                item.RefreshLastTradeDirection( );
            }
            return item;
        }

        internal Security CurrentSecurity
        {
            get
            {
                return ( ( SecurityItem )CurrentItem )?.Security;
            }
        }

        private void BaseGridControl_Loaded( object sender, RoutedEventArgs e )
        {
            _periodicObject = GuiDispatcher.GlobalDispatcher.AddPeriodicalAction( new Action( OnRefreshTradeDirection ) );
        }

        private void BaseGridControl_Unloaded( object sender, RoutedEventArgs e )
        {
            if ( _periodicObject == null )
                return;
            GuiDispatcher.GlobalDispatcher.RemovePeriodicalAction( _periodicObject );
        }

        private void SecurityGrid_CustomColumnSort( object sender, CustomColumnSortEventArgs e )
        {
            SecurityItem row1 = ( SecurityItem )GetRow( e.ListSourceRowIndex1 );
            SecurityItem row2 = ( SecurityItem )GetRow( e.ListSourceRowIndex2 );
            if ( row1?.ExtendedInfo == null || row2?.ExtendedInfo == null )
                return;
            string index =   e.Column.Header .To<string>( );
            object obj1 = row1.ExtendedInfo[ index ];
            object obj2 = row2.ExtendedInfo[ index ];
            e.Result = CompareHelper.Compare( obj1, obj2 );
            e.Handled = true;
        }

        private void BaseGridControl_CustomUnboundColumnData( object sender, GridColumnDataEventArgs e )
        {
            if ( e.Column.FieldName != "ClosePrices" )
                return;            

            if ( PriceChartDataProvider == null )
            {
                return;
            }
                
            var securityItem = _securityItemCollection[ e.ListSourceRowIndex ];
            var lastPrice  = PriceChartDataProvider.Get( securityItem.Security, Level1Fields.LastTradePrice );
            int pointCount = PointCountDepProp.GetPointCount( e.Column );
            e.Value =    lastPrice.AveragePriceByCount( pointCount );
            
        }

        private void OnRefreshTradeDirection( )
        {
            var secItemCopy = _securityItemTriDictionary.SyncGet( d => d.CopyAndClear( ) );

            if ( secItemCopy.Length == 0 )
                return;

            BeginDataUpdate( );
            try
            {
                foreach ( var secInfo in secItemCopy )
                {                    
                    TraderHelper.ApplyChanges( secInfo.Key, secInfo.Value.First, secInfo.Value.Second, secInfo.Value.Third );
                    secInfo.Key.RefreshLastTradeDirection( );
                }
            }
            finally
            {
                EndDataUpdate( );
            }
        }

        

        private sealed class SecurityItem  : Security
        {
            private Decimal? _lastTradePrice;
            private Decimal? _lastBestBidPrice;
            private Decimal? _lastBestAskPrice;
            private Decimal? _lastClosePrice;
            private readonly Security _securityItem;
            private int _bestBidPriceDirection;
            private int _bestAskPriceDirection;
            private int _lastTradePriceDirection;
            private int _lastClosePriceDirection;
            private readonly SecurityGrid _securityGrid;

            public SecurityItem ( Security security_1, SecurityGrid securityGrid_1 ) : base()
            {                
                Security security = security_1;
                if ( security == null )
                    throw new ArgumentNullException( "security" );
                _securityItem = security;
                SecurityGrid securityGrid = securityGrid_1;
                if ( securityGrid == null )
                    throw new ArgumentNullException( "grid" );
                _securityGrid = securityGrid;
            }

            public Security Security
            {
                get
                {
                    return _securityItem;
                }
            }

            public int BestBidPriceDirection
            {
                get
                {
                    return _bestBidPriceDirection;
                }
                set
                {
                    if ( _bestBidPriceDirection == value )
                        return;
                    _bestBidPriceDirection = value;
                    Notify( nameof( BestBidPriceDirection ) );
                }
            }

            public int BestAskPriceDirection
            {
                get
                {
                    return _bestAskPriceDirection;
                }
                set
                {
                    if ( _bestAskPriceDirection == value )
                        return;
                    _bestAskPriceDirection = value;
                    Notify( nameof( BestAskPriceDirection ) );
                }
            }

            public int LastTradePriceDirection
            {
                get
                {
                    return _lastTradePriceDirection;
                }
                set
                {
                    if ( _lastTradePriceDirection == value )
                        return;
                    _lastTradePriceDirection = value;
                    Notify( nameof( LastTradePriceDirection ) );
                }
            }

            public int ClosePriceDirection
            {
                get
                {
                    return _lastClosePriceDirection;
                }
                set
                {
                    if ( _lastClosePriceDirection == value )
                        return;
                    _lastClosePriceDirection = value;
                    Notify( nameof( ClosePriceDirection ) );
                }
            }

            public Decimal? LastTradeVolume
            {
                get
                {
                    Decimal? volume = LastTrade?.Volume;

                    if ( volume.HasValue )
                    {
                        return volume.Value;
                    }

                    return null;
                }
            }

            public void RefreshLastTradeDirection( )
            {
                Decimal? price = BestBid?.Price;
                Decimal? nullable1;
                if ( price.HasValue )
                {
                    if ( _lastBestBidPrice.HasValue )
                    {
                        nullable1 = _lastBestBidPrice;
                        Decimal num1 = price.Value;
                        if ( !( nullable1.GetValueOrDefault( ) == num1 & nullable1.HasValue ) )
                            BestBidPriceDirection = 0;
                        nullable1 = _lastBestBidPrice;
                        Decimal num2 = price.Value;
                        if ( nullable1.GetValueOrDefault( ) < num2 & nullable1.HasValue )
                        {
                            BestBidPriceDirection = 1;
                        }
                        else
                        {
                            nullable1 = _lastBestBidPrice;
                            Decimal num3 = price.Value;
                            if ( nullable1.GetValueOrDefault( ) > num3 & nullable1.HasValue )
                                BestBidPriceDirection = -1;
                        }
                    }
                    _lastBestBidPrice = new Decimal?( price.Value );
                }
                Quote bestAsk = BestAsk;
                Decimal? nullable2;
                if ( bestAsk == null )
                {
                    nullable1 = new Decimal?( );
                    nullable2 = nullable1;
                }
                else
                    nullable2 = new Decimal?( bestAsk.Price );
                Decimal? nullable3 = nullable2;
                if ( nullable3.HasValue )
                {
                    if ( _lastBestAskPrice.HasValue )
                    {
                        nullable1 = _lastBestAskPrice;;
                        Decimal num1 = nullable3.Value;
                        if ( !( nullable1.GetValueOrDefault( ) == num1 & nullable1.HasValue ) )
                            BestAskPriceDirection = 0;
                        nullable1 = _lastBestAskPrice;;
                        Decimal num2 = nullable3.Value;
                        if ( nullable1.GetValueOrDefault( ) < num2 & nullable1.HasValue )
                        {
                            BestAskPriceDirection = 1;
                        }
                        else
                        {
                            nullable1 = _lastBestAskPrice;;
                            Decimal num3 = nullable3.Value;
                            if ( nullable1.GetValueOrDefault( ) > num3 & nullable1.HasValue )
                                BestAskPriceDirection = -1;
                        }
                    }
                    _lastBestAskPrice = new Decimal?( nullable3.Value );
                }
                Trade lastTrade = LastTrade;
                Decimal? nullable4;
                if ( lastTrade == null )
                {
                    nullable1 = new Decimal?( );
                    nullable4 = nullable1;
                }
                else
                    nullable4 = new Decimal?( lastTrade.Price );
                Decimal? nullable5 = nullable4;
                if ( nullable5.HasValue )
                {
                    if ( _lastTradePrice.HasValue )
                    {
                        nullable1 = _lastTradePrice;
                        Decimal num1 = nullable5.Value;
                        if ( !( nullable1.GetValueOrDefault( ) == num1 & nullable1.HasValue ) )
                            LastTradePriceDirection = 0;
                        nullable1 = _lastTradePrice;
                        Decimal num2 = nullable5.Value;
                        if ( nullable1.GetValueOrDefault( ) < num2 & nullable1.HasValue )
                        {
                            LastTradePriceDirection = 1;
                        }
                        else
                        {
                            nullable1 = _lastTradePrice;
                            Decimal num3 = nullable5.Value;
                            if ( nullable1.GetValueOrDefault( ) > num3 & nullable1.HasValue )
                                LastTradePriceDirection = -1;
                        }
                    }
                    _lastTradePrice = new Decimal?( nullable5.Value );
                }
                Decimal? closePrice = ClosePrice;
                if ( closePrice.HasValue )
                {
                    if ( _lastClosePrice.HasValue )
                    {
                        nullable1 = _lastClosePrice;;
                        Decimal? nullable6 = closePrice;
                        if ( !( nullable1.GetValueOrDefault( ) == nullable6.GetValueOrDefault( ) & nullable1.HasValue == nullable6.HasValue ) )
                            ClosePriceDirection = 0;
                        Decimal? nullable3_1 = _lastClosePrice;;
                        nullable1 = closePrice;
                        if ( nullable3_1.GetValueOrDefault( ) < nullable1.GetValueOrDefault( ) & ( nullable3_1.HasValue & nullable1.HasValue ) )
                        {
                            ClosePriceDirection = 1;
                        }
                        else
                        {
                            nullable1 = _lastClosePrice;;
                            Decimal? nullable7 = closePrice;
                            if ( nullable1.GetValueOrDefault( ) > nullable7.GetValueOrDefault( ) & ( nullable1.HasValue & nullable7.HasValue ) )
                                ClosePriceDirection = -1;
                        }
                    }
                    else
                        _lastClosePrice = closePrice;
                }
                Notify( "LastTradeVolume" );
            }

            public IDictionary<string, object> ExtendedInfo
            {
                get
                {
                    return   _securityGrid._extendedInfoDictionary .TryGetValue( Security.Id );
                }
            }

            public int? DaysToExpire
            {
                get
                {
                    DateTimeOffset? expiryDate = Security.ExpiryDate;
                    if ( !expiryDate.HasValue )
                        return new int?( );
                    int num = ( int )( expiryDate.Value - DateTimeOffset.Now ).TotalDays;
                    if ( num < 0 )
                        num = 0;
                    return new int?( num );
                }
            }
        }

        private sealed class SecuritiesCollection : IList<Security>, ICollection<Security>, IEnumerable<Security>, IEnumerable
        {
            private readonly SecurityGrid _ownerGrid;

            public SecuritiesCollection( SecurityGrid grid )
            {                
                if ( grid == null )
                {
                    throw new ArgumentNullException( "parent" );
                }
                    
                _ownerGrid = grid;
            }

            private SecurityItem GetSecurityItem( Security sec )
            {
                if ( sec == null )
                {
                    throw new ArgumentNullException( "security" );
                }
                    
                SecurityItem data = _ownerGrid.TryGetSecurity( sec );

                if ( data == null )
                {
                    throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.Str1548Params, new object[ 1 ] { sec.Id } ) );
                }
                    
                return data;
            }

            private IList<object> SelectedItems( )
            {
                return ( IList<object> )_ownerGrid.SelectedItems;
            }

            void ICollection<Security>.Add( Security item )
            {
                SelectedItems( ).Add( GetSecurityItem( item ) );
            }

            void ICollection<Security>.Clear( )
            {
                SelectedItems( ).Clear( );
            }

            bool ICollection<Security>.Contains( Security item )
            {
                return SelectedItems( ).Contains( GetSecurityItem( item ) );
            }

            void ICollection<Security>.CopyTo( Security[ ] array, int arrayIndex )
            {
                object[ ] objArray = new object[ SelectedItems( ).Count ];
                SelectedItems( ).CopyTo( objArray, arrayIndex );
                foreach ( SecurityItem data in objArray.Cast<SecurityItem>( ) )
                {
                    array[ arrayIndex ] = data.Security;
                    ++arrayIndex;
                }
            }

            bool ICollection<Security>.Remove( Security item )
            {
                return SelectedItems( ).Remove( GetSecurityItem( item ) );
            }

            int ICollection<Security>.Count
            {
                get
                {
                    return SelectedItems( ).Count;
                }
            }

            

            bool ICollection<Security>.IsReadOnly
            {
                get
                {
                    return false;
                }
            }

           

            public IEnumerator<Security> GetEnumerator( )
            {
                return SelectedItems( ).Cast<SecurityItem>( ).Select( i => i.Security ).GetEnumerator( );
            }

            int IList<Security>.IndexOf( Security item )
            {
                return SelectedItems( ).IndexOf( GetSecurityItem( item ) );
            }

            void IList<Security>.Insert( int index, Security item )
            {
                SelectedItems( ).Insert( index, GetSecurityItem( item ) );
            }

            void IList<Security>.RemoveAt( int index )
            {
                SelectedItems( ).RemoveAt( index );
            }

            

            Security IList<Security>.this[ int index ]
            {
                get
                {
                    return ( ( SecurityItem )SelectedItems( )[ index ] ).Security;
                }
                set
                {
                    SelectedItems( )[ index ] = GetSecurityItem( value );
                }
            }

            IEnumerator IEnumerable.GetEnumerator( )
            {
                return GetEnumerator( );
            }            
        }
    }
}
