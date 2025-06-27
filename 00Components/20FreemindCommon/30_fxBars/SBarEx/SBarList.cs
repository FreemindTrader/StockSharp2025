using fx.Bars;
using fx.Collections;
using fx.Definitions;
using fx.TimePeriod;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace fx.Bars
{
    

    public partial class SBarList : IEnumerable<SBar>, IBarList, IDisposable
    {
        public static long      LondonDstBegin;
        public static long      LondonDstEnd;

        public static long      NewYorkDstBegin;
        public static long      NewYorkDstEnd;


        private SBar[ ]            _barArray;
        private AdvBarInfo[]       _waveArray;

        private sBarNode           _firstNode;
        private sBarNode           _actualNode;

        private waveNode           _firstWaveNode;
        private waveNode           _actualWaveNode;

        private readonly Sequence  _barCount = new Sequence( 0 );
        private readonly Sequence  _wavCount = new Sequence( 0 );

        private readonly Func<int> _getMaximumBlockSize;
        private readonly Sequence  _barCountInActualArray = new Sequence( 0 );
        private readonly Sequence  _waveCountInActualArray = new Sequence( 0 );

        private Security           _security;
        private SymbolEx           _symbol;
        private TimeSpan           _period;

        private int                _currentYear = 0;
        
        private ThreadSafeDictionary< long, uint >       _timeIndexCache  = new ThreadSafeDictionary<long, uint>();
        private DictionarySlim< uint, AdvBarInfo > _extraInfoCache        = new DictionarySlim<uint, AdvBarInfo>();
        private DictionarySlim< long, int >        _cachedSessionBegin    = new DictionarySlim<long, int>();
        private DictionarySlim< long, int >        _weeklySessionBegin    = new DictionarySlim<long, int>();
        private TimePeriodCollectionEx             _cacheTimePeriodToIndex   = null;

        private Func< DateTime, SessionEnum > _barSessionFunc = null;

        private bool _isNotDailyBar = true;

        private int _lastSessionIndex = 0;

        private static int DefaultWaveCount = 100;

        

        public SymbolEx SymbolEx
        {
            get
            {
                return _symbol;
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public bool HasExtraInfo( uint index, out AdvBarInfo advBarInfo )
        {
            if ( _extraInfoCache.TryGetValue( index, out advBarInfo ) )
            {
                return true;
            }

            return false;
        }

        

        public SBarList( SymbolEx symbol, int capacity, Func<int> getMaximumBlockSize = null )
        {
            Debug.Assert( capacity > 0 );

            _symbol = symbol;

            CreateStorage( capacity );

            if ( getMaximumBlockSize == null )
            {
                // we actually run the getDefaultMaximumBlock size instead of
                // simply storing it so, if it changes, we will use the changed
                // version.
                getMaximumBlockSize = () => _getDefaultMaximumBlockSize();
            }

            _getMaximumBlockSize = getMaximumBlockSize;

            CreateWaveStorage(DefaultWaveCount);

            SetBarSessionFunction(symbol, symbol.Period);
        }

        public SBarList(SecurityId secId, TimeSpan period, int capacity)
        {
            var secProvider = ServicesRegistry.TrySecurityProvider;

            if (secProvider is not null)
            {
                _security = secProvider.LookupById(secId);
                _period = period;

                CreateStorage(capacity);

                _symbol = new SymbolEx(secId, _security.Type.Value, period);

                // Here I am going to pre-allocate the SBarEx
                _firstNode.PreAllocate(this, _symbol, 0);

                CreateWaveStorage(DefaultWaveCount);

                SetBarSessionFunction(_security, period);
            }
        }


        /// <summary>
        /// The following constructor will create a SBarList and instantiate all the SBarEx and partly the Elliott Wave info. The restore to databars will be done later
        /// </summary>
        /// <param name="security"></param>
        /// <param name="period"></param>
        /// <param name="capacity"></param>        
        public SBarList(SecurityId secId, TimeSpan period, IList<TimeFrameCandleMessage> candles, List<long> wavesTimeList )
        {
            var secProvider = ServicesRegistry.TrySecurityProvider;

            if (secProvider is not null)
            {
                _security = secProvider.LookupById(secId);

                _period = period;
                _symbol = new SymbolEx(secId, _security.Type.Value, period);

                _cacheTimePeriodToIndex = new TimePeriodCollectionEx(candles.Count);

                if (_period < TimeSpan.FromDays(1))
                {
                    _isNotDailyBar = true;
                }

                var capacity = candles.Count;

                if (capacity < 0)
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);

                CreateStorage(capacity);



                _symbol = new SymbolEx(secId, _security.Type.Value, period);

                // Here I am going to pre-allocate the SBarEx
                _firstNode.PreAllocate(this, _symbol, 0);

                if (wavesTimeList == null || wavesTimeList.Count == 0)
                {
                    CreateWaveStorage(DefaultWaveCount);
                }
                else
                {
                    CreateWaveStorage(wavesTimeList.Count);
                }

                SetBarSessionFunction(_security, period);
            }
        }

        public void SetBarSessionFunction(SymbolEx security, TimeSpan period )
        {
            int offerId = security.OfferId;

            string symbolName = SymbolHelper.GetSymbolFromOfferId(offerId);

            if ( string.IsNullOrEmpty( symbolName) )
            {
                throw new NotImplementedException();
            }

            switch ( security.SecurityType )
            {
                case SmallSecurityTypes.Currency:
                {
                    _barSessionFunc = SBarStaticFunctions.CurrencyFunc;                    
                }
                break;

                case SmallSecurityTypes.Cfd:
                {
                    switch ( symbolName )
                    {
                        case string us when us.Contains( ".US" ):                        
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;

                        case string hk when hk.Contains( ".HK" ):
                        {
                            _barSessionFunc = SBarStaticFunctions.HKStockFunc;
                        }
                        break;

                        case string de when de.Contains( ".DE" ):
                        {
                            _barSessionFunc = SBarStaticFunctions.GermanyStockFunc;
                        }
                        break;

                        case string uk when uk.Contains( ".UK" ):
                        {
                            _barSessionFunc = SBarStaticFunctions.UKStockFunc;
                        }
                        break;

                        case string fr when fr.Contains( ".FR" ):
                        {
                            _barSessionFunc = SBarStaticFunctions.FranceStockFunc;
                        }
                        break;

                        default:
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;
                    }
                }
                break;

                case SmallSecurityTypes.Index:
                {
                    switch ( symbolName )
                    {
                        case "AUS200":
                        {
                            _barSessionFunc = SBarStaticFunctions.AustraliaStockFunc;
                        }
                        break;

                        case "ESP35":
                        {
                            _barSessionFunc = SBarStaticFunctions.SpainStockFunc;
                        }
                        break;

                        case "FRA40":
                        {
                            _barSessionFunc = SBarStaticFunctions.FranceStockFunc;
                        }
                        break;

                        case "GER30":
                        {
                            _barSessionFunc = SBarStaticFunctions.GermanyStockFunc;
                        }
                        break;

                        case "HKG33":
                        {
                            _barSessionFunc = SBarStaticFunctions.HKStockFunc;
                        }
                        break;

                        case "JNP225":
                        {
                            _barSessionFunc = SBarStaticFunctions.JapanStockFunc;
                        }
                        break;

                        case "NAS100":
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;

                        case "US2000":
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;

                        case "SPX500":
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;

                        case "UK100":
                        {
                            _barSessionFunc = SBarStaticFunctions.UKStockFunc;
                        }
                        break;

                        case "US30":
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;
                    }
                }
                break;

                case SmallSecurityTypes.Commodity:
                {
                    switch ( symbolName )
                    {
                        case "USOIL":
                        case "USOILSPOT":
                        case "UKOIL":
                        case "UKOILSPOT":
                        {

                        }
                        break;

                        case "SOYF":
                        {

                        }
                        break;

                        case "WHEATF":
                        {

                        }
                        break;

                        case "CORNF":
                        {

                        }
                        break;                        
                    }
                }
                break;
                
            }

            if ( period < TimeSpan.FromDays( 1 ) )
            {
                //_sessionCheckFunc
            }
        }

        public void SetBarSessionFunction(Security security, TimeSpan period)
        {
            switch (security.Type)
            {
                case StockSharp.Messages.SecurityTypes.Currency:
                {
                    _barSessionFunc = SBarStaticFunctions.CurrencyFunc;
                }
                break;

                case StockSharp.Messages.SecurityTypes.Cfd:
                {
                    switch (security.Code)
                    {
                        case string us when us.Contains(".US"):
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;

                        case string hk when hk.Contains(".HK"):
                        {
                            _barSessionFunc = SBarStaticFunctions.HKStockFunc;
                        }
                        break;

                        case string de when de.Contains(".DE"):
                        {
                            _barSessionFunc = SBarStaticFunctions.GermanyStockFunc;
                        }
                        break;

                        case string uk when uk.Contains(".UK"):
                        {
                            _barSessionFunc = SBarStaticFunctions.UKStockFunc;
                        }
                        break;

                        case string fr when fr.Contains(".FR"):
                        {
                            _barSessionFunc = SBarStaticFunctions.FranceStockFunc;
                        }
                        break;

                        default:
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;
                    }
                }
                break;

                case StockSharp.Messages.SecurityTypes.Index:
                {
                    switch (security.Code)
                    {
                        case "AUS200":
                        {
                            _barSessionFunc = SBarStaticFunctions.AustraliaStockFunc;
                        }
                        break;

                        case "ESP35":
                        {
                            _barSessionFunc = SBarStaticFunctions.SpainStockFunc;
                        }
                        break;

                        case "FRA40":
                        {
                            _barSessionFunc = SBarStaticFunctions.FranceStockFunc;
                        }
                        break;

                        case "GER30":
                        {
                            _barSessionFunc = SBarStaticFunctions.GermanyStockFunc;
                        }
                        break;

                        case "HKG33":
                        {
                            _barSessionFunc = SBarStaticFunctions.HKStockFunc;
                        }
                        break;

                        case "JNP225":
                        {
                            _barSessionFunc = SBarStaticFunctions.JapanStockFunc;
                        }
                        break;

                        case "NAS100":
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;

                        case "US2000":
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;

                        case "SPX500":
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;

                        case "UK100":
                        {
                            _barSessionFunc = SBarStaticFunctions.UKStockFunc;
                        }
                        break;

                        case "US30":
                        {
                            _barSessionFunc = SBarStaticFunctions.UsaStockFunc;
                        }
                        break;
                    }
                }
                break;

                case StockSharp.Messages.SecurityTypes.Commodity:
                {
                    switch (security.Code)
                    {
                        case "USOIL":
                        case "USOILSPOT":
                        case "UKOIL":
                        case "UKOILSPOT":
                        {

                        }
                        break;

                        case "SOYF":
                        {

                        }
                        break;

                        case "WHEATF":
                        {

                        }
                        break;

                        case "CORNF":
                        {

                        }
                        break;
                    }
                }
                break;
                
            }

            if (period < TimeSpan.FromDays(1))
            {
                //_sessionCheckFunc
            }
        }

        public uint LastBarIndex => ( uint ) _barCount.Value - 1;

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref SBar UpdateLastCandle(ICandleMessage candle )
        {
            int size = _barCount.Value;

            if ( size - 1 < 0 )
            {
                ThrowHelper.ThrowArgumentOutOfRange_IndexException();
            }

            ref var bar = ref this[ size - 1 ];
            bar.Update( candle );

            return ref this[ size - 1 ];
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void CopyCandlesSetSession( IList<TimeFrameCandleMessage> candles )
        {
            DateTime lastBarTime = DateTime.MinValue;

            for ( int i = 0; i < candles.Count; i++ )
            {
                var barTime = candles[ i ].OpenTime.UtcDateTime;

                if ( i > 0 )
                {
                    _cacheTimePeriodToIndex.Add(  lastBarTime, barTime );
                }  

                AddSingleCandleNoBoundCheck( candles[ i ] );

                lastBarTime = barTime;
            }            
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public (uint, uint) AddReplaceCandles( List<TimeFrameCandleMessage> candles )
        {
            var candleCount = candles.Count;

            var expanded = ExpandStorage( candleCount );            

            if ( expanded )
            {
                return AddCandlesWithExpansion( candles );
            }            
            else
            {
                return AddCandlesReturnRange( candles );
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public (uint, uint) AddCandlesWithExpansion( List<TimeFrameCandleMessage> candles )
        {
            DateTime lastBarTime = LastBarTime;

            uint begin = 0, end = 0, lastBarIndex = 0, barIndex = 0;

            for ( int i = 0; i < candles.Count; i++ )
            {
                var barTime = candles[ i ].OpenTime.UtcDateTime;

                if ( barTime >= lastBarTime + _period )
                {
                    _cacheTimePeriodToIndex.Add( lastBarTime, barTime );

                    ref SBar added = ref AddCandleToExpanded( candles[ i ] );

                    barIndex = added.BarIndex;

                    lastBarTime = barTime;                    
                }
                else
                {
                    // Update of existing bars.
                    var barLinuxTime = barTime.ToLinuxTime();

                    if ( _timeIndexCache.TryGetValue( barLinuxTime, out uint index ) )
                    {
                        ref SBar existingBar =ref this[ index ];

                        existingBar.CopyFrom( candles[ i ] );

                        barIndex = existingBar.BarIndex;

                        lastBarIndex = index;
                    }
                    else
                    {
                        // Here is where the insertion of new bars into BarList

                        lastBarIndex++;

                        ref SBar existingBar =ref this[ lastBarIndex ];

                        if ( existingBar.isValidBar )
                        {
                            _timeIndexCache.Remove( existingBar.LinuxTime );

                            existingBar.CopyFrom( candles[ i ] );

                            _timeIndexCache.TryAdd( existingBar.LinuxTime, lastBarIndex );
                        }
                        else
                        {
                            ref SBar added = ref AddCandle( candles[ i ] );

                            barIndex = added.BarIndex;

                            lastBarTime = barTime;
                        }
                    }
                }

                if ( begin == 0 ) begin = barIndex;

                if ( barIndex > end ) end = barIndex;
            }

            return (begin, end);
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public (uint, uint) AddCandlesReturnRange( List<TimeFrameCandleMessage> candles )
        {
            DateTime lastBarTime = LastBarTime;

            uint begin = 0, end = 0, barIndex = 0;

            uint lastBarIndex = 0;            

            for ( int i = 0; i < candles.Count; i++ )
            {
                var barTime = candles[ i ].OpenTime.UtcDateTime;

                // adding of new bars to BarList
                if ( barTime >= lastBarTime + _period )
                {
                    _cacheTimePeriodToIndex.Add( lastBarTime, barTime );

                    ref SBar added = ref AddSingleCandleNoBoundCheck( candles[ i ] );

                    barIndex = added.BarIndex;

                    lastBarTime = barTime;
                }                
                else
                {
                    // Update of existing bars.
                    var barLinuxTime = barTime.ToLinuxTime();
                    
                    if ( _timeIndexCache.TryGetValue( barLinuxTime, out uint index ) )
                    {
                        ref SBar existingBar =ref this[ index ];

                        if ( existingBar.LinuxTime != barLinuxTime )
                        {

                        }


                        existingBar.CopyFrom( candles[ i ] );

                        

                        barIndex = existingBar.BarIndex;

                        lastBarIndex = index;
                    }  
                    else
                    {
                        // Here is where the insertion of new bars into BarList

                        lastBarIndex++;

                        ref SBar existingBar =ref this[ lastBarIndex ];

                        if ( existingBar.isValidBar )
                        {
                            _timeIndexCache.Remove( existingBar.LinuxTime );

                            existingBar.CopyFrom( candles[ i ] );

                            _timeIndexCache.TryAdd( existingBar.LinuxTime, lastBarIndex );
                        }
                        else
                        {
                            ref SBar added = ref AddCandle( candles[ i ] );

                            barIndex = added.BarIndex;

                            lastBarTime = barTime;
                        }
                    }
                }

                if ( begin == 0 ) begin = barIndex;

                if ( barIndex > end ) end = barIndex;                
            }

            return (begin, end);
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void AddCandlesNoBoundCheck( List<ICandleMessage> candles )
        {
            DateTime lastBarTime = LastBarTime;            

            for ( int i = 0; i < candles.Count; i++ )
            {
                var barTime = candles[ i ].OpenTime.UtcDateTime;

                if ( barTime >= lastBarTime + _period )
                {
                    _cacheTimePeriodToIndex.Add( lastBarTime, barTime );

                    AddSingleCandleNoBoundCheck( candles[ i ] );                    

                    lastBarTime = barTime;
                }
            }            
        }


        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public bool ExpandStorage( int capacity )
        {
            if ( _barCountInActualArray.Value + capacity >= _barArray.Length  )
            {
                var lastIndex = _barArray[ _barArray.Length - 1 ].BarIndex + 1;

                var newNode           = new sBarNode( capacity );                
                newNode.PreAllocate( this, _symbol, lastIndex );

                _actualNode._nextNode = newNode;

                return true;
            }

            return false;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void AllocateWaves( List<long> wavesTimeList )
        {
            for ( int i = 0; i < wavesTimeList.Count; i++ )
            {
                if ( _timeIndexCache.TryGetValue( wavesTimeList[ i ], out uint barIndex ) )
                {
                    int waveIndex = _wavCount.Value;
                    _wavCount.IncrementAndGet();
                    _waveCountInActualArray.IncrementAndGet();

                    _extraInfoCache.GetOrAddValueRef( barIndex ) = _waveArray[ waveIndex ];
                }
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref SBar GetBarByTime( DateTime barTime )
        {            
            if ( _timeIndexCache.TryGetValue( barTime.ToLinuxTime(), out uint barIndex ) )
            {
                return ref this[ barIndex ];
            }

            return ref SBar.EmptySBar;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref SBar GetBarByTime( long rawBarTime )
        {            
            if ( _timeIndexCache.TryGetValue( rawBarTime, out uint barIndex ) )
            {
                return ref this[ barIndex ];
            }

            return ref SBar.EmptySBar;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public int GetIndexByTime( long rawBarTime )
        {
            if ( _barCount.Value == 0  )
            {
                return -1;
            }

            if ( _timeIndexCache.TryGetValue( rawBarTime, out uint barIndex ) )
            {
                return (int) barIndex;
            }

            return -1;
        }

        public AdvBarInfo GetOrAllocate( uint index )
        {
            if ( _extraInfoCache.TryGetValue( index, out AdvBarInfo info ) )
            {
                return info;
            }

            var newInfo = AllocateWaveInfo();

            _extraInfoCache.GetOrAddValueRef( index ) = newInfo;

            return newInfo;
        }

        public AdvBarInfo AllocateWaveInfo()
        {
            if ( _waveCountInActualArray.Value == _waveArray.Length )
            {
                var newNode               = new waveNode( _wavCount.Value, this );
                
                _actualWaveNode._nextNode = newNode;
                _actualWaveNode           = newNode;
                _waveArray                = newNode._waveArray;
                
                _waveCountInActualArray.SetValueVolatile( 0 );
            }

            int size = _waveCountInActualArray.Value;
            _wavCount.IncrementAndGet();
            _waveCountInActualArray.IncrementAndGet();

            return _waveArray[ size ];
        }

        public ref SBar RestoreWave( long barLinuxTime, ref HewLong mainWave, ref HewLong alterWave01, ref HewLong alterWave02, ref HewLong alterWave03 )
        {
            if ( _timeIndexCache.TryGetValue( barLinuxTime, out uint barIndex ) )
            {
                ref SBar tmpBar = ref this[ barIndex ];  
                
                tmpBar.RestoreElliottWave( ref mainWave, ref alterWave01, ref alterWave02, ref alterWave03 );
                tmpBar.Features.HasElliottWave = true;

                return ref tmpBar;
            }

            return ref SBar.EmptySBar;
        }


        


        // OK. We may not want to put tests in our delegates for specific types, yet
        // we may want to configure specific types with different block sizes.
        // So, we can do it by using the delegates in here. These affect only
        // the actual SBarEx, not the others.
        private static Func<int> _getDefaultFirstBlockSize = ( ) => SBarStaticFunctions.GetDefaultFirstBlockSize( typeof( SBar ) );
        public static Func<int> GetDefaultFirstBlockSize
        {
            get
            {
                return _getDefaultFirstBlockSize;
            }
            set
            {
                if ( value == null )
                    _getDefaultFirstBlockSize = () => SBarStaticFunctions.GetDefaultFirstBlockSize( typeof( SBar ) );
                else
                    _getDefaultFirstBlockSize = value;
            }
        }        

        private static Func<int> _getDefaultMaximumBlockSize = ( ) => SBarStaticFunctions.GetDefaultMaximumBlockSize( typeof( SBar ) );
        public static Func<int> GetDefaultMaximumBlockSize
        {
            get
            {
                return _getDefaultMaximumBlockSize;
            }
            set
            {
                if ( value == null )
                    _getDefaultMaximumBlockSize = () => SBarStaticFunctions.GetDefaultMaximumBlockSize( typeof( SBar ) );
                else
                    _getDefaultMaximumBlockSize = value;
            }
        }

        internal sealed class sBarNode
        {
            internal sBarNode( int size )
            {
                _sBarArray      = ArrayPool<SBar>.Shared.Rent( size );
                _timeBlockArray = ArrayPool<TimeBlock>.Shared.Rent( size );
            }

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal void PreAllocate( SBarList parent, SymbolEx symbol, uint startingIndex )
            {
                for ( uint i = 0; i < _sBarArray.Length; i++ )
                {
                    _sBarArray[ i ].Parent = parent;
                    _sBarArray[ i ]._barIndex = startingIndex + i;                    
                }
            }

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal void PreAllocate( SBarList parent, SymbolEx symbol, int startingIndex )
            {
                for ( int i = 0; i < _sBarArray.Length; i++ )
                {
                    _sBarArray[ i ].Parent    = parent;
                    _sBarArray[ i ]._barIndex = (uint)( startingIndex + i );                    
                }
            }

            internal readonly SBar[ ] _sBarArray;
            internal readonly TimeBlock[] _timeBlockArray;
            internal sBarNode _nextNode;
        }

        internal sealed class waveNode
        {
            internal waveNode( int size, SBarList collection )
            {
                _waveArray = ArrayPool<AdvBarInfo>.Shared.Rent( size );
                Array.Clear( _waveArray, 0, _waveArray.Length );

                for ( int i = 0; i < _waveArray.Length; i++ )
                {
                    _waveArray[ i ] = new AdvBarInfo( collection );
                }
            }            

            internal readonly AdvBarInfo[ ] _waveArray;
            internal waveNode _nextNode;
        }


        public void Dispose()
        {
            ReturnArray();
            _barCount.SetValue( 0 );
        }

        private void ReturnArray()
        {
            var node = _firstNode;

            while ( node != null )
            {
                var array    = node._sBarArray;
                var nextNode = node._nextNode;

                try
                {
                    // Clear the elements so that the gc can reclaim the references.
                    ArrayPool<SBar>.Shared.Return( array );
                }
                catch ( ArgumentException )
                {
                    // oh well, the array pool didn't like our array
                }
                                
                node = nextNode;
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void CreateStorage( int capacity )
        {
            _firstNode   = new sBarNode( capacity );
            _actualNode  = _firstNode;
            _barArray    = _firstNode._sBarArray;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void CreateWaveStorage( int capacity )
        {
            _firstWaveNode  = new waveNode( capacity, this );
            _actualWaveNode = _firstWaveNode;
            _waveArray      = _firstWaveNode._waveArray;
        }

        
        public long LongCount
        {
            get
            {
                return _barCount.Value;
            }
        }

        public int Count
        {
            get
            {
                return _barCount.Value;
            }
        }

        public void Clear()
        {
            var size = _firstNode._sBarArray.Length;

            ReturnArray();
            
            _firstNode          = new sBarNode( size );
            _barArray        = _firstNode._sBarArray;
            _actualNode         = _firstNode;

            _barCount.SetValueVolatile( 0 );
            _barCountInActualArray.SetValueVolatile( 0 );
        }
        
        //public void Add( ref SBar item )
        //{
        //    if ( _barCountInActualArray.Value == _barArray.Length )
        //    {
        //        int addedSize = GetBlockSizeFromPeriod( item.BarTime, item.BarPeriod );
        //        int maxSize = _getDefaultMaximumBlockSize();

        //        if ( addedSize > maxSize )
        //        {
        //            addedSize = maxSize;
        //        }

        //        if ( addedSize < 1 )
        //            throw new InvalidOperationException( "The GetMaximumBlockSize delegate returned an invalid value." );                

        //        var newNode           = new sBarNode( addedSize );

        //        newNode.PreAllocate( this, item.SymbolEx, _barCount.Value );
        //        _actualNode._nextNode = newNode;
        //        _actualNode           = newNode;
        //        _barArray             = newNode._sBarArray;
        //        _barCountInActualArray.SetValueVolatile( 0 );
        //    }

        //    _barArray[ _barCountInActualArray.Value ] = item;

        //    _timeIndexCache.GetOrAddValueRef( item.LinuxTime ) = (uint) _barCount.Value ;

        //    _barCount.IncrementAndGet();
        //    _barCountInActualArray.IncrementAndGet();

        //    
        //}

        public int GetBlockSizeFromPeriod( DateTime lastBarTime, TimeSpan period )
        {           
            if ( period < TimeSpan.FromDays( 1 ) )
            {
                var diff = DateTimeHelper.NextFirday() - lastBarTime;
                int bars = ( int )( diff.Ticks / period.Ticks ) + 1;

                return bars;
            }
            else
            {
                var diff = DateTimeHelper.LastDateOfMonth() - lastBarTime;

                int bars = ( int )( diff.Ticks / period.Ticks ) + 1;

                return bars;
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref SBar AddSingleCandleNoBoundCheck(ICandleMessage candle )
        {
            var lastBarTime = LastBarTime;
            var barTime     = candle.OpenTime.UtcDateTime;
            var candleYear  = barTime.Year;

            if ( candleYear > _currentYear )
            {
                _currentYear = candleYear;

                var london      = TimeZoneInfo.FindSystemTimeZoneById( "GMT Standard Time" );
                var adjust      = london.GetAdjustmentRuleForYear( _currentYear );
                LondonDstBegin  = adjust.GetDaylightTransitionStartForYear( _currentYear ).ToLinuxTime();
                LondonDstEnd    = adjust.GetDaylightTransitionEndForYear( _currentYear ).ToLinuxTime();

                var newYork     = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
                var nyrule      = newYork.GetAdjustmentRuleForYear( _currentYear );
                NewYorkDstBegin = nyrule.GetDaylightTransitionStartForYear( _currentYear ).ToLinuxTime();
                NewYorkDstEnd   = nyrule.GetDaylightTransitionEndForYear( _currentYear ).ToLinuxTime();
            }

            var session     = _barSessionFunc.Invoke( barTime );                        
            ref SBar bar    = ref _barArray[ _barCountInActualArray.Value ];
            bar.CopyFrom( candle );
            bar.BarSession  = session;            

            if ( _isNotDailyBar )
            {
                if( ( barTime - lastBarTime ).TotalMinutes > 720 )
                {
                    _cachedSessionBegin.GetOrAddValueRef( bar.LinuxTime ) = bar.Index;
                    _lastSessionIndex = bar.Index;
                }
            }

            if ( session == SessionEnum.DailySessionEnd )
            {
                _cachedSessionBegin.GetOrAddValueRef( bar.LinuxTime ) = bar.Index;
                _lastSessionIndex = bar.Index;
            }

            if ( session == SessionEnum.WeeklySessionBegin )
            {
                _weeklySessionBegin.GetOrAddValueRef( bar.LinuxTime ) = bar.Index;
            }

            _timeIndexCache.TryAdd( bar.LinuxTime, ( uint ) _barCount.Value );

            _barCount.IncrementAndGet();
            _barCountInActualArray.IncrementAndGet();

            

            return ref bar;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref SBar AddCandleToExpanded(ICandleMessage candle )
        {
            var lastBarTime = LastBarTime;
            var barTime     = candle.OpenTime.UtcDateTime;
            var candleYear  = barTime.Year;

            if ( candleYear > _currentYear )
            {
                _currentYear = candleYear;

                var london      = TimeZoneInfo.FindSystemTimeZoneById( "GMT Standard Time" );
                var adjust      = london.GetAdjustmentRuleForYear( _currentYear );
                LondonDstBegin = adjust.GetDaylightTransitionStartForYear( _currentYear ).ToLinuxTime();
                LondonDstEnd = adjust.GetDaylightTransitionEndForYear( _currentYear ).ToLinuxTime();

                var newYork     = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
                var nyrule      = newYork.GetAdjustmentRuleForYear( _currentYear );
                NewYorkDstBegin = nyrule.GetDaylightTransitionStartForYear( _currentYear ).ToLinuxTime();
                NewYorkDstEnd = nyrule.GetDaylightTransitionEndForYear( _currentYear ).ToLinuxTime();
            }

            var session     = _barSessionFunc.Invoke( barTime );

            if ( _barCountInActualArray.Value == _barArray.Length )
            {                
                var nextNode = _actualNode._nextNode;
                
                _actualNode  = nextNode;
                _barArray    = nextNode._sBarArray;
                _barCountInActualArray.SetValueVolatile( 0 );                
            }

            ref SBar bar    = ref _barArray[ _barCountInActualArray.Value ];
            bar.CopyFrom( candle );
            bar.BarSession = session;

            if ( _isNotDailyBar )
            {
                if ( ( barTime - lastBarTime ).TotalMinutes > 720 )
                {
                    _cachedSessionBegin.GetOrAddValueRef( bar.LinuxTime ) = bar.Index;
                    _lastSessionIndex = bar.Index;
                }
            }

            if ( session == SessionEnum.DailySessionEnd )
            {
                _cachedSessionBegin.GetOrAddValueRef( bar.LinuxTime ) = bar.Index;
                _lastSessionIndex = bar.Index;
            }

            if ( session == SessionEnum.WeeklySessionBegin )
            {
                _weeklySessionBegin.GetOrAddValueRef( bar.LinuxTime ) = bar.Index;
            }

            _timeIndexCache.TryAdd( bar.LinuxTime, ( uint ) _barCount.Value );
            

            _barCount.IncrementAndGet();
            _barCountInActualArray.IncrementAndGet();

            return ref bar;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref SBar AddCandle( TimeFrameCandleMessage candle )
        {
            if ( _barCountInActualArray.Value == _barArray.Length )
            {
                int addedSize = GetBlockSizeFromPeriod( candle.OpenTime.UtcDateTime, (TimeSpan) candle.Arg );
                int maxSize = _getDefaultMaximumBlockSize();

                if ( addedSize < 1 )
                    throw new InvalidOperationException( "The GetMaximumBlockSize delegate returned an invalid value." );

                if ( addedSize > maxSize )
                {
                    addedSize = maxSize;
                }

                var secProvider = ServicesRegistry.TrySecurityProvider;

                if (secProvider is not null)
                {
                    var security = secProvider.LookupById(candle.SecurityId);

                    var newNode = new sBarNode(addedSize);
                    _symbol = new SymbolEx(candle.SecurityId, security.Type.Value, (TimeSpan)candle.Arg);
                    newNode.PreAllocate(this, _symbol, _barCount.Value);

                    _actualNode._nextNode = newNode;
                    _actualNode = newNode;
                    _barArray = newNode._sBarArray;
                    _barCountInActualArray.SetValueVolatile(0);
                }
            }

            return ref AddSingleCandleNoBoundCheck( candle );            
        }

        public ref SBar this[ long index ]
        {
            get
            {
                if ( index < 0 && index > _barCount.Value )
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }

                var node = _firstNode;

                while ( node != null )
                {
                    var array = node._sBarArray;
                    if ( index < array.Length )
                        return ref array[ index ];

                    index -= array.Length;
                    node = node._nextNode;
                }


                return ref SBar.EmptySBar;
            }

        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public float High( int index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].High;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public float Low( int index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].Low;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public float Open( int index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].Open;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public float Close( int index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].Close;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public uint Volume( int index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].Volume;
        }

        

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public float High( uint index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].High;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public float Low( uint index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].Low;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public float Open( uint index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].Open;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public float Close( uint index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].Close;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public uint Volume( uint index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].Volume;
        }


        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public DateTime BarTime( int index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].BarTime;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public DateTime BarTime( uint index )
        {
            Debug.Assert( index < _barCount.Value );

            return this[ index ].BarTime;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public TimeSpan Period()
        {
            if ( _barCount.Value > 0 )
            {
                return this[ 0 ].BarPeriod;
            }

            return TimeSpan.Zero;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public double PriceStep()
        {
            if ( _barCount.Value > 0 )
            {
                return this[ 0 ].SymbolEx.PriceStep;
            }

            return 0;
        }

        
        public ref SBar First
        {
            get
            {
                Debug.Assert( _barCount.Value > 0 );

                return ref _firstNode._sBarArray[ 0 ];
            }
        }

        
        public ref SBar Last
        {
            get
            {
                var count = _barCount.Value;
                if ( count > 0 )
                {
                    return ref this[ count - 1 ];
                }

                return ref SBar.EmptySBar;
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref SBar GetNextRefSlot()
        {
            if ( _barCountInActualArray.Value == _barArray.Length )
            {
                int addedSize = GetBlockSizeFromPeriod( LastBarTime, _symbol.Period );
                int maxSize = _getDefaultMaximumBlockSize();

                if ( addedSize < 1 )
                    throw new InvalidOperationException( "The GetMaximumBlockSize delegate returned an invalid value." );

                if ( addedSize > maxSize )
                {
                    addedSize = maxSize;
                }

                var newNode           = new sBarNode( addedSize );                
                newNode.PreAllocate( this, _symbol, _barCount.Value );

                _actualNode._nextNode = newNode;
                _actualNode           = newNode;
                _barArray             = newNode._sBarArray;

                _barCountInActualArray.SetValueVolatile( 0 );
            }            

            
            _barCount.IncrementAndGet();
            _barCountInActualArray.IncrementAndGet();

            return ref _barArray[ _barCountInActualArray.Value ];
        }


        

        public ref SBar LastStableBar
        {
            get
            {
                var count = _barCount.Value;
                if ( count > 1 )
                {
                    return ref this[ count - 2 ];
                }

                return ref SBar.EmptySBar;
            }
        }        

        public DateTime LastBarTime
        {
            get
            {
                if ( _barCount.Value == 0 ) return DateTime.MinValue;

                return this[ _barCount.Value - 1 ].BarTime;
            }
        }

        IEnumerator<SBar> IEnumerable<SBar>.GetEnumerator()
        {
            return new SBarForEach.Enumerator( _firstNode, _barArray, _barCountInActualArray.Value );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SBarForEach.Enumerator( _firstNode, _barArray, _barCountInActualArray.Value );
        }

        public SBarForEach.Enumerator GetEnumerator()
        {
            return new SBarForEach.Enumerator( _firstNode, _barArray, _barCountInActualArray.Value );
        }
        public SBarForEach AsImmutable()
        {
            return new SBarForEach( _firstNode, _barArray, _barCountInActualArray.Value, _barCount.Value );
        }
        public SBar[ ] ToArray()
        {
            return AsImmutable().ToArray();
        }
        public void CopyTo( SBar[ ] array, long arrayIndex )
        {
            AsImmutable().CopyTo( array, arrayIndex );
        }
        
       


        

        public bool IsTodaySession( int barIndex )
        {
            if ( _cachedSessionBegin.Count > 0 )
            {
                if ( barIndex >= _lastSessionIndex )
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public int GetTimeBlockIndex( DateTime barTime )
        {
            return( _cacheTimePeriodToIndex.IntersectionPeriods( barTime ) );            
        }
    }


    // We use delegates to get the default values, and they are really "global".
    // That is, instead of being per generic type, they can be configured for
    // all the data-types at once.
    public static class SBarStaticFunctions
    {
        public delegate int GetBlockSizeDelegate( Type itemType );

        private static GetBlockSizeDelegate _getDefaultFirstBlockSize = ( itemType ) => 32;

        public static GetBlockSizeDelegate GetDefaultFirstBlockSize
        {
            get
            {
                return _getDefaultFirstBlockSize;
            }
            set
            {
                if ( value == null )
                    _getDefaultFirstBlockSize = ( itemType ) => 32;
                else
                    _getDefaultFirstBlockSize = value;
            }
        }

        private static GetBlockSizeDelegate _getDefaultMaximumBlockSize = ( itemType ) => 1000000;
        public static GetBlockSizeDelegate GetDefaultMaximumBlockSize
        {
            get
            {
                return _getDefaultMaximumBlockSize;
            }
            set
            {
                if ( value == null )
                    _getDefaultMaximumBlockSize = ( itemType ) => 1000000;
                else
                    _getDefaultMaximumBlockSize = value;
            }
        }

        public static SessionEnum CurrencyFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            long linuxTime = barTime.ToLinuxTime();
            bool londonDST = false;
            bool newYorkDST = false;

            if ( linuxTime > SBarList.LondonDstBegin && linuxTime < SBarList.LondonDstEnd )
            {
                londonDST = true;
            }

            if ( linuxTime > SBarList.NewYorkDstBegin && linuxTime < SBarList.NewYorkDstEnd )
            {
                newYorkDST = true;
            }

            double barMinutes = barTime.TimeOfDay.TotalMinutes;


            // ![](53C5DB3B445B42D79F302F6B0899D81E.png;;;0.02839,0.02821)

            // ![](6656668B7ACBB29CE88B1D36DE3CF0B1.png;;;0.02022,0.02929)
            switch ( barMinutes )
            {
                case 0:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;

                case ( 7 * 60 ) when londonDST == true:
                {
                    output = SessionEnum.EuropeanSessionStart;
                }
                break;

                case ( 8 * 60 ) when londonDST == false:
                {
                    output = SessionEnum.EuropeanSessionStart;
                }
                break;

                case ( 13 * 60 + 30 ) when newYorkDST == true:
                {
                    output = SessionEnum.UsaSessionStart;
                }
                break;

                case ( 14 * 60 + 30 ) when newYorkDST == false:
                {
                    output = SessionEnum.UsaSessionStart;
                }
                break;

                case ( 22 * 60 + 30 ) when newYorkDST == true:
                {
                    output = SessionEnum.DailySessionEnd;
                }
                break;

                case ( 21 * 60 ) when newYorkDST == false:
                {
                    if ( barTime.DayOfWeek == DayOfWeek.Sunday )
                    {
                        output = SessionEnum.WeeklySessionBegin;
                    }                    
                }
                break;

                case ( 22 * 60 ) when newYorkDST == true:
                {
                    if ( barTime.DayOfWeek == DayOfWeek.Sunday )
                    {
                        output = SessionEnum.WeeklySessionBegin;
                    }
                }
                break;

                case ( 23 * 60 + 30 ) when newYorkDST == false:
                {
                    output = SessionEnum.DailySessionEnd;
                }
                break;
            }

            return output;
        }

        

        

        public static SessionEnum UsaStockFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            long linuxTime = barTime.ToLinuxTime();
            bool londonDST = false;
            bool newYorkDST = false;

            if ( linuxTime > SBarList.LondonDstBegin && linuxTime < SBarList.LondonDstEnd )
            {
                londonDST = true;
            }

            if ( linuxTime > SBarList.NewYorkDstBegin && linuxTime < SBarList.NewYorkDstEnd )
            {
                newYorkDST = true;
            }            

            double barMinutes = barTime.TimeOfDay.TotalMinutes;


            // ![](33EBD752ACED762389C247E0F8700ED8.png;;;0.03156,0.03945)

            // 
            switch ( barMinutes )
            {
                case 0:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;

                case ( 7 * 60 ) when londonDST == true:
                {
                    output = SessionEnum.EuropeanSessionStart;
                }
                break;

                case ( 8 * 60 ) when londonDST == false:
                {
                    output = SessionEnum.EuropeanSessionStart;
                }
                break;

                

                case ( 13 * 60 + 30 ) when newYorkDST == true:
                {
                    output = SessionEnum.UsaSessionStart;
                }
                break;

                case ( 14 * 60 + 30 ) when newYorkDST == false:
                {
                    output = SessionEnum.UsaSessionStart;
                }
                break;

                case ( 22 * 60 + 30 ) when newYorkDST == true:
                {
                    output = SessionEnum.DailySessionEnd;
                }
                break;

                case ( 23 * 60 + 30 ) when newYorkDST == false:
                {
                    output = SessionEnum.DailySessionEnd;
                }
                break;
            }

            return output;
        }

        public static SessionEnum HKStockFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            double barHours = barTime.TimeOfDay.TotalHours;


            switch ( barHours )
            {
                case 7:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;
            }

            return output;
        }

        public static SessionEnum JapanStockFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            double barHours = barTime.TimeOfDay.TotalHours;


            switch ( barHours )
            {
                case 7:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;
            }

            return output;
        }

        public static SessionEnum GermanyStockFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            double barHours = barTime.TimeOfDay.TotalHours;


            switch ( barHours )
            {
                case 7:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;
            }

            return output;
        }

        public static SessionEnum UKStockFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            double barHours = barTime.TimeOfDay.TotalHours;


            switch ( barHours )
            {
                case 7:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;
            }

            return output;
        }

        public static SessionEnum FranceStockFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            double barHours = barTime.TimeOfDay.TotalHours;


            switch ( barHours )
            {
                case 7:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;
            }

            return output;
        }

        public static SessionEnum AustraliaStockFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            double barHours = barTime.TimeOfDay.TotalHours;


            switch ( barHours )
            {
                case 7:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;
            }

            return output;
        }

        public static SessionEnum SpainStockFunc( DateTime barTime )
        {
            SessionEnum output = SessionEnum.NONE;

            double barHours = barTime.TimeOfDay.TotalHours;


            switch ( barHours )
            {
                case 7:
                {
                    output = SessionEnum.AsiaSessionStart;
                }
                break;
            }

            return output;
        }
    }

    public sealed class SBarForEach
    {
        private readonly SBarList.sBarNode _firstNode;
        private readonly SBar[ ] _lastArray;
        private readonly int _countInLastArray;
        private readonly long _barCount;

        internal SBarForEach( SBarList.sBarNode firstNode, SBar[ ] lastArray, int countInLastArray, long count )
        {
            _firstNode        = firstNode;
            _lastArray        = lastArray;
            _countInLastArray = countInLastArray;
            _barCount         = count;
        }

        public long Count
        {
            get
            {
                return _barCount;
            }
        }

        // This method is here for completeness, but it is slower for the
        // latest items as many blocks may need to be navigated.
        // Yet this ElementAt is much faster than the LINQ one, as here
        // we only need to navigate blocks, not element by element.
        public SBar ElementAt( long index )
        {
            Debug.Assert( index >= 0 && index < _barCount );

            var node = _firstNode;
            while ( true )
            {
                var array = node._sBarArray;
                if ( index < array.Length )
                    return array[ index ];

                index -= array.Length;
                node = node._nextNode;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator( _firstNode, _lastArray, _countInLastArray );
        }

        public struct Enumerator : IEnumerator<SBar>
        {
            private SBar[ ] _array;
            private SBarList.sBarNode _node;
            private long _positionInArray;
            private long _countInArray;
            private SBar[ ] _lastArray;
            private int _countInLastArray;


            internal Enumerator( SBarList.sBarNode firstNode, SBar[ ] lastArray, int countInLastArray )
            {
                _node            = firstNode;
                _array           = _node._sBarArray;
                _positionInArray = -1;

                _lastArray       = lastArray;

                if ( _array == lastArray )
                    _countInArray = countInLastArray;
                else
                    _countInArray = _array.Length;

                _countInLastArray = countInLastArray;
            }

            public SBar Current
            {
                get
                {
                    return _array[ _positionInArray ];
                }
            }

            public void Dispose()
            {
                _array     = null;
                _node      = null;
                _lastArray = null;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public bool MoveNext()
            {
                if ( _array == null )
                    return false;

                _positionInArray++;
                if ( _positionInArray >= _countInArray )
                {
                    _node = _node._nextNode;

                    if ( _node == null )
                    {
                        _array = null;
                        return false;
                    }

                    _array = _node._sBarArray;
                    _positionInArray = 0;

                    if ( _array == _lastArray )
                        _countInArray = _countInLastArray;
                    else
                        _countInArray = _array.Length;
                }

                return true;
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
        }

        public SBar[ ] ToArray()
        {
            var result = new SBar[ _barCount ];

            CopyTo( result, 0 );

            return result;
        }
        public void CopyTo( SBar[ ] array, long arrayIndex )
        {
            Debug.Assert( array != null );

            Debug.Assert( arrayIndex >= 0 && arrayIndex <= ( array.Length - _barCount ) );

            var node = _firstNode;
            while ( true )
            {
                var nodeArray = node._sBarArray;

                if ( nodeArray == _lastArray )
                {
                    Array.Copy( nodeArray, 0, array, arrayIndex, _countInLastArray );
                    return;
                }

                nodeArray.CopyTo( array, arrayIndex );
                arrayIndex += nodeArray.Length;

                node = node._nextNode;
            }
        }
    }
}



