using System;
using fx.Algorithm;
using System.Linq;
using fx.Common;
using fx.TALib;
using fx.Bars;
using fx.Definitions;

namespace fx.Indicators
{
    /// <summary>
    /// This indicator is not tradeable since it changes its symbolPositionSummary and requres future looking.
    /// Indicator written manually no external lib used.
    /// </summary>
    [Serializable]
    [UserFriendlyName( "JMacd" )]
    public class FreemindJurikMacd : CustomPlatformIndicator
    {
        protected double [,] _macdBackup;
        protected double [,] _macdSigBackup;

        protected double [,] _macdBuffer;
        protected double [,] _macdSigBuffer;

        protected int        _macdBackupIndex = -1;
        protected int        _macdSignalBackupIndex = -1;

        private TASignal     _lastCrossingDirection = TASignal.NONE;

        int                  _lookback = -1;
        private int          _fastPeriod = 20;
        private int          _slowPeriod = 40;
        private int          _custom_signal = 10;

        private fxList<double> _unfilteredMacd = new fxList<double>( );
        private fxList<double> _unfilteredMacdSig = new fxList<double>( );

        protected int bsmax                = 5;
        protected int bsmin                = 6;
        protected int volty                = 7;
        protected int vsum                 = 8;
        protected int avolty               = 9;
        protected int myPrice              = 10;

        protected int _backupRows = 15;

        protected int _jmaLength   = 26;
        protected double _jmaPhase    = 0;
        protected int _lastJmaIndex = 0;

        public int FastPeriod
        {
            get { return _fastPeriod; }
            set { _fastPeriod = value; }
        }

        public int SlowPeriod
        {
            get { return _slowPeriod; }
            set { _slowPeriod = value; }
        }

        public int Signal
        {
            get { return _custom_signal; }
            set { _custom_signal = value; }
        }

        public TASignal LastCrossingDirection
        {
            get { return _lastCrossingDirection; }
        }

        public FreemindJurikMacd() : base( typeof( FreemindJurikMacd ).Name, true, true, false, new string[ ] { "MACD", "MACDSignal", "MACDHistory" } )
        {
            //this.ChartSeries.OutputResultSetsPens[ "MACD" ]        = Pens.Red;
            //this.ChartSeries.OutputResultSetsPens[ "MACDSignal" ]  = Pens.Green;
            //this.ChartSeries.OutputResultSetsPens[ "MACDHistory" ] = Pens.Black;

            //this.ChartSeries.Visible                               = true;
            //this.ChartSeries.InvolvedInDisplayAreaCalculation      = true;

            _lookback = Core.MacdExtLookback();
        }

        protected override void OnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
        {
            var barB4Calculation  = Bars.TotalBarCount;
            int resultSetLength   = IndicatorResult["MACD"].Count;

            if ( resultSetLength == 0 )
            {
                ProcessNewIndicatorBuffer( barB4Calculation );
            }
            else
            {
                ProcessExistingBuffer( e.UpdateType, barB4Calculation, resultSetLength );
            }
        }

        private void ProcessNewIndicatorBuffer( int barB4Calculation )
        {
            var startIndex  = 0;
            var endIndex    = barB4Calculation - 1;
            var indexCount  = endIndex - startIndex + 1;

            var close       = Bars.GetDataBarSubset( DataBarProperty.Close, 0, indexCount ).ToArray( );
            var macd        = new double [ indexCount ];
            var macdSignal  = new double [ indexCount ];
            var macdHistory = new double [ indexCount ];

            long unstablePeriod = 120;

            if ( _lookback > barB4Calculation )
            {
                unstablePeriod = Core.GetUnstablePeriod( Core.FuncUnstId.Ema );

                var retCode = Core.SetUnstablePeriod( Core.FuncUnstId.Ema, 30 );

                if ( retCode != Core.RetCode.Success )
                {
                    return;
                }
            }

            int outBeginIdx   = 0;
            int outNBElement = 0;

            Core.MacdExt( close, startIndex, endIndex, macd, macdSignal, macdHistory, out outBeginIdx, out outNBElement, Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 12, 26, 9 );

            AddUnfilteredValue( _unfilteredMacd, outBeginIdx, outNBElement, true, macd );
            AddUnfilteredValue( _unfilteredMacdSig, outBeginIdx, outNBElement, true, macdSignal );

            var filterMacd       = new double[ outNBElement ];
            var filterMacdSig    = new double[ outNBElement ];
            _macdBuffer = new double[ outNBElement, 11 ];
            _macdSigBuffer = new double[ outNBElement, 11 ];

            for ( int i = 0; i < outNBElement; i++ )
            {
                filterMacd[ i ] = iSmooth( ref _macdBuffer, 0, macd[ i ], _jmaLength, _jmaPhase, i, 0 );
            }

            BackupLastXRows( _backupRows, outNBElement, _macdBuffer, out _macdBackup );
            _macdBackupIndex = ( int ) _unfilteredMacd.Count - _backupRows;

            for ( int i = 0; i < outNBElement; i++ )
            {
                filterMacdSig[ i ] = iSmooth( ref _macdSigBuffer, 0, macdSignal[ i ], _jmaLength, _jmaPhase, i, 0 );
            }

            BackupLastXRows( _backupRows, outNBElement, _macdSigBuffer, out _macdSigBackup );
            _macdSignalBackupIndex = ( int ) _unfilteredMacdSig.Count - _backupRows;

            lock ( this )
            {
                IndicatorResult.AddSetValues( "MACD", outBeginIdx, outNBElement, true, filterMacd );
                IndicatorResult.AddSetValues( "MACDSignal", outBeginIdx, outNBElement, true, filterMacdSig );
            }
        }


        public bool AddUnfilteredValue( fxList<double> unfiltered, int startingIndex, int count, bool overrideExistingValues, double[ ] inputResultPiece )
        {
            var indicatorfxList = unfiltered;

            for ( long i = indicatorfxList.Count; i < startingIndex; i++ )
            {
                // Only if there are some empty spaces before the start, fill them with no value.
                indicatorfxList.Add( 0 );
            }

            var endIndex = startingIndex + count;

            // Get the dataSource from the result it is provided to us.
            for ( int i = startingIndex; i < endIndex; i++ )
            {
                if ( indicatorfxList.Count <= i )
                {
                    indicatorfxList.Add( inputResultPiece[ i - startingIndex ] );
                }
                else
                {
                    if ( overrideExistingValues )
                    {
                        indicatorfxList.Update( i, inputResultPiece[ i - startingIndex ] );
                    }
                }
            }

            return true;
        }

        protected int BackupLastXRows( int rows, int endIndex, double[ , ] originalBuffer, out double[ , ] backup )
        {
            backup = new double[ rows, 11 ];

            int startIndex = -1;

            if ( originalBuffer.GetLength( 0 ) >= rows )
            {
                if ( endIndex < rows )
                {
                    endIndex += rows;
                }

                startIndex = endIndex - rows;

                int rowCount = 0;

                if ( startIndex >= 0 )
                {
                    for ( int i = startIndex; i < endIndex; i++ )
                    {
                        for ( int j = 0; j < 11; ++j )
                        {
                            backup[ rowCount, j ] = originalBuffer[ i, j ];
                        }

                        rowCount++;
                    }
                }
            }

            return startIndex;
        }




        private void ProcessExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
        {
            var repoStartingIndex = Math.Max( 0, resultSetLength - _lookback - 2 );

            var startIndex        = 0;
            var endIndex          = barB4Calculation - repoStartingIndex - 1;
            var indexCount        = endIndex - startIndex + 1;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            int outBeginIdx       = 0;
            int outNBElement      = 0;

            var close             = Bars.GetDataBarSubset( DataBarProperty.Close, repoStartingIndex, indexCount ).ToArray( );
            var macd              = new double [ indexCount ];
            var macdSignal        = new double [ indexCount ];
            var macdHistory       = new double [ indexCount ];

            Core.MacdExt( close, startIndex, endIndex, macd, macdSignal, macdHistory, out outBeginIdx, out outNBElement, Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 12, 26, 9 );

            AddUnfilteredValue( _unfilteredMacd, repoStartingIndex + outBeginIdx, outNBElement, true, macd );
            AddUnfilteredValue( _unfilteredMacdSig, repoStartingIndex + outBeginIdx, outNBElement, true, macdSignal );

            //var newCount          = (int) _unfilteredMacd.Count - _lookback;

            //var filterMacd        = new double[ newCount ];
            //var filterMacdSig     = new double[ newCount ];
            //_macdBuffer           = new double[ newCount, 11 ];
            //_macdSigBuffer        = new double[ newCount, 11 ];

            //for ( int i = 0; i < newCount; i++ )
            //{
            //    if (i == 2334 )
            //    {

            //    }

            //    double macdValue = _unfilteredMacd.ElementAt( i + _lookback );

            //    filterMacd[ i ] = iSmooth( ref _macdBuffer, 0, macdValue, _jmaLength, _jmaPhase, i, 0 );
            //}


            //for ( int i = 0; i < newCount; i++ )
            //{
            //    double macdSigValue = _unfilteredMacdSig.ElementAt( i + _lookback );

            //    filterMacdSig[ i ] = iSmooth( ref _macdSigBuffer, 0, macdSigValue, _jmaLength, _jmaPhase, i, 0 );
            //}

            //_macdSignalBackupIndex = outBeginIdx + BackupLastXRows( _backupRows, outNBElement, macdSignalBuffer, out _macdSignalBackup );

            //lock ( this )
            //{
            //    IndicatorResult.AddSetValues( "MACD"      , _lookback, newCount, true, filterMacd );
            //    IndicatorResult.AddSetValues( "MACDSignal", _lookback, newCount, true, filterMacdSig );
            //}

            //var diff1 = CompareBuffers( _macdBuffer, macdBuffer );
            //var diff2 = CompareBuffers( _macdSigBuffer, macdSignalBuffer );

            ProcessPartialBuffer( updateType, barB4Calculation, repoStartingIndex, outBeginIdx, outNBElement );
        }

        private void ProcessPartialBuffer( DataBarUpdateType? updateType, int barB4Calculation, int repoStartingIndex, int outBeginIdx, int outNBElement )
        {
            var hasMacdCount            = ( int ) _unfilteredMacd.Count - _lookback;
            var changdStartIndex        = ( int ) _unfilteredMacd.Count - outNBElement;

            var realMacdBeginIndex      = repoStartingIndex + outBeginIdx;
            var realMacdEndIndex        = repoStartingIndex + outBeginIdx + outNBElement;

            double[,] macdBuffer        = null;
            double[,] macdSigBuffer     = null;

            double [] filterMacd        = new double [ outNBElement ];
            double [] filterMacdSig     = new double [ outNBElement ];

            int allocatedRows           = 0;

            if ( realMacdEndIndex > _macdBackupIndex + _backupRows )
            {
                allocatedRows = outNBElement + _backupRows;
                macdBuffer = new double[ allocatedRows, 11 ];
                macdSigBuffer = new double[ allocatedRows, 11 ];
            }
            else
            {
                allocatedRows = _backupRows;
                macdBuffer = new double[ allocatedRows, 11 ];
                macdSigBuffer = new double[ allocatedRows, 11 ];
            }

            var partialBufferStartIndex = changdStartIndex - _macdBackupIndex;

            if ( partialBufferStartIndex < 0 )
            {
                var newbarCount  = Bars.TotalBarCount;
                ProcessNewIndicatorBuffer( newbarCount );
                return;
            }

            RestoreLastXRows( _backupRows, _macdBackup, ref macdBuffer );

            for ( int i = 0; i < outNBElement; i++ )
            {
                double macdValue = _unfilteredMacd.ElementAt( changdStartIndex + i );

                filterMacd[ i ] = iSmooth( ref macdBuffer, partialBufferStartIndex, macdValue, _jmaLength, _jmaPhase, i, 0 );
            }

            BackupLastXRows( _backupRows, allocatedRows, macdBuffer, out _macdBackup );
            _macdBackupIndex = ( int ) _unfilteredMacd.Count - _backupRows;

            //if( CompareBuffers( _macdBuffer, macdBuffer, _macdBackupIndex ) )
            //{

            //}

            RestoreLastXRows( _backupRows, _macdSigBackup, ref macdSigBuffer );

            for ( int i = 0; i < outNBElement; i++ )
            {
                double macdSig = _unfilteredMacdSig.ElementAt( changdStartIndex + i );

                filterMacdSig[ i ] = iSmooth( ref macdSigBuffer, partialBufferStartIndex, macdSig, _jmaLength, _jmaPhase, i, 0 );
            }

            BackupLastXRows( _backupRows, allocatedRows, macdSigBuffer, out _macdSigBackup );
            _macdSignalBackupIndex = ( int ) _unfilteredMacdSig.Count - _backupRows;

            //if ( CompareBuffers( _macdSigBuffer, macdSigBuffer, _macdBackupIndex ) )
            //{

            //}

            lock ( this )
            {
                IndicatorResult.AddSetValues( "MACD", repoStartingIndex + outBeginIdx, outNBElement, true, filterMacd );
                IndicatorResult.AddSetValues( "MACDSignal", repoStartingIndex + outBeginIdx, outNBElement, true, filterMacdSig );
            }

        }


        private void ProcessExistingIndicatorBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
        {
            var repoStartingIndex = Math.Max( 0, resultSetLength - _lookback - 2 );

            var startIndex        = 0;
            var endIndex          = barB4Calculation - repoStartingIndex - 1;
            var indexCount        = endIndex - startIndex + 1;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            int outBeginIdx       = 0;
            int outNBElement     = 0;

            var close                  = Bars.GetDataBarSubset( DataBarProperty.Close, repoStartingIndex, indexCount ).ToArray( );
            var macd                   = new double [ indexCount ];
            var macdSignal             = new double [ indexCount ];
            var macdHistory            = new double [ indexCount ];

            Core.MacdExt( close, startIndex, endIndex, macd, macdSignal, macdHistory, out outBeginIdx, out outNBElement, Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 12, 26, 9 );

            AddUnfilteredValue( _unfilteredMacd, outBeginIdx, outNBElement, true, macd );
            AddUnfilteredValue( _unfilteredMacdSig, outBeginIdx, outNBElement, true, macdSignal );

            var realMacdBeginIndex     = repoStartingIndex + outBeginIdx;
            var realMacdEndIndex       = repoStartingIndex + outBeginIdx + outNBElement;
            double [] filterMacd       = new double [ outNBElement ];
            double [] filterMacdSig    = new double [ outNBElement ];

            int beginIndex             = 0;

            double[,] macdBuffer       = null;
            double[,] macdSigBuffer    = null;

            int allocatedRows = 0;

            if ( realMacdEndIndex > _macdBackupIndex + _backupRows )
            {
                allocatedRows = outNBElement + _backupRows;
                macdBuffer = new double[ allocatedRows, 11 ];
            }
            else
            {
                allocatedRows = _backupRows;
                macdBuffer = new double[ allocatedRows, 11 ];
            }

            beginIndex = realMacdBeginIndex - _macdBackupIndex;

            RestoreLastXRows( _backupRows, _macdBuffer, ref macdBuffer );

            for ( int i = 0; i < outNBElement; i++ )
            {
                filterMacd[ i ] = iSmooth( ref macdBuffer, beginIndex, macd[ i ], _jmaLength, _jmaPhase, i, 0 );
            }

            BackupLastXRows( _backupRows, allocatedRows, macdBuffer, out _macdBuffer );
            _macdBackupIndex = repoStartingIndex + outBeginIdx + outNBElement - _backupRows;




            if ( realMacdEndIndex >= _macdSignalBackupIndex + _backupRows )
            {
                allocatedRows = outNBElement + _backupRows;
                macdSigBuffer = new double[ allocatedRows, 11 ];
            }
            else
            {
                allocatedRows = _backupRows;
                macdSigBuffer = new double[ _backupRows, 11 ];
            }

            beginIndex = realMacdBeginIndex - _macdBackupIndex;

            RestoreLastXRows( _backupRows, _macdSigBuffer, ref macdSigBuffer );

            for ( int i = 0; i < outNBElement; i++ )
            {
                filterMacdSig[ i ] = iSmooth( ref macdSigBuffer, beginIndex, macdSignal[ i ], _jmaLength, _jmaPhase, i, 0 );
            }

            BackupLastXRows( _backupRows, allocatedRows, macdSigBuffer, out _macdSigBuffer );
            _macdSignalBackupIndex = repoStartingIndex + outBeginIdx + outNBElement - _backupRows;

            lock ( this )
            {
                IndicatorResult.AddSetValues( "MACD", repoStartingIndex + outBeginIdx, outNBElement, true, filterMacd );
                IndicatorResult.AddSetValues( "MACDSignal", repoStartingIndex + outBeginIdx, outNBElement, true, filterMacdSig );
            }
        }

        public int CompareBuffers( double[ , ] oldBuffer, double[ , ] newBuffer )
        {
            var minRows = Math.Min( oldBuffer.GetLength( 0 ), newBuffer.GetLength( 0 ) );

            for ( int i = 0; i < minRows; i++ )
            {
                for ( int j = 0; j < 11; ++j )
                {
                    if ( oldBuffer[ i, j ] != newBuffer[ i, j ] )
                    {
                        return i;
                    }
                }
            }

            return minRows;
        }

        public bool CompareBuffers( double[ , ] largeBuffer, double[ , ] small, int startIndex )
        {
            var minRows = Math.Max( largeBuffer.GetLength( 0 ), small.GetLength( 0 ) );

            for ( int i = startIndex; i < minRows; i++ )
            {
                for ( int j = 0; j < 11; ++j )
                {
                    if ( largeBuffer[ i, j ] != small[ i - startIndex, j ] )
                    {
                        return true;
                    }
                }
            }

            return false;
        }



        //protected void Calculate___FilteredMacd( bool fullRecalculation, DataBarUpdateType? updateType, double [ ] macd, int outBeginIdx, int outNBElement, out double [ ] filterMacd )
        //{
        //    filterMacd = new double [ outNBElement ];

        //    _multiWrk = new double [ outNBElement + 10, 11 ];

        //    int beginIndex = 0;

        //    if ( IndicatorResult.SetLength > 10 )
        //    {
        //        RestoreLastXRows( _backupRows, _macdBackup );
        //        beginIndex = updateType == DataBarUpdateType.CurrentBarUpdate ? 9 : 10;
        //    }
        //    else
        //    {
        //        beginIndex = 0;
        //    }

        //    for ( int i = 0; i < outNBElement; i++ )
        //    {
        //        filterMacd [ i ] = iSmooth( beginIndex, macd [ i ], _jmaLength, _jmaPhase, i, 0 );                
        //    }

        //    _lastJmaIndex = outBeginIdx + outNBElement;

        //    BackupLastXRows( _backupRows, _lastJmaIndex, out _macdBackup );
        //}

        //protected void Calculate_FilteredSignal( bool fullRecalculation, DataBarUpdateType? updateType, double [ ] macdSignal, int outBeginIdx, int outNBElement, out double [ ] filterMacdSignal )
        //{
        //    filterMacdSignal = new double [ outNBElement ];

        //    _multiWrk = new double [ outNBElement + 10, 11 ];

        //    int beginIndex = 0;

        //    if ( IndicatorResult.SetLength > 10 )
        //    {
        //        RestoreLastXRows( _backupRows, _macdSignalBackup );
        //        beginIndex = updateType == DataBarUpdateType.CurrentBarUpdate ? 9 : 10;
        //    }
        //    else
        //    {
        //        beginIndex = 0;
        //    }

        //    for ( int i = 0; i < outNBElement; i++ )
        //    {
        //        filterMacdSignal [ i ] = iSmooth( beginIndex, macdSignal [ i ], _jmaLength, _jmaPhase, i, 0 );                
        //    }

        //    _lastJmaIndex = outBeginIdx + outNBElement;

        //    BackupLastXRows( _backupRows, _lastJmaIndex, out _macdSignalBackup );            
        //}

        public override void OnInitialDataBarUpdatePostCalculation( bool fullRecalculation )
        {
            Signals.PerformCrossingResultAnalysis( ProvideSignalAnalysisLines() );
        }

        public override void OnNewBarArrivesPostCalculation( bool fullRecalculation )
        {
            Signals.StartingIndex = Math.Max( Signals.LastSignalIndex - 5, 0 );
            Signals.PerformCrossingResultAnalysis( ProvideSignalAnalysisLines() );
        }

        public override void OnHistoryBarUpdatePostCalculation( bool fullRecalculation )
        {
            Signals.StartingIndex = Math.Max( Signals.LastSignalIndex - 5, 0 );

            Signals.PerformCrossingResultAnalysis( ProvideSignalAnalysisLines() );
        }

        public override float OnResultAnalysisCrossingFound( int line1index, double line1value, int line2index, double line2value, bool direction, double currentSignalPositionValue )
        {
            if ( line1index == 0 &&
                 line2index == 1 )
            {
                if ( direction )
                {
                    _lastCrossingDirection = TASignal.HAS_BOTTOMING_SIGNAL;
                    return ( int ) TASignal.HAS_BOTTOMING_SIGNAL;
                }
                else
                {
                    _lastCrossingDirection = TASignal.HAS_TOPPING_SIGNAL;
                    return ( int ) TASignal.HAS_TOPPING_SIGNAL;
                }
            }

            return ( int ) TASignal.NONE;
        }

        public override PlatformIndicator OnSimpleClone()
        {
            FreemindJurikMacd result = new FreemindJurikMacd( );

            result._description = _description;
            result._name = _name;
            result._fastPeriod = _fastPeriod;
            result._slowPeriod = _slowPeriod;
            result._custom_signal = _custom_signal;

            return result;
        }

        protected override double[ ][ ] ProvideSignalAnalysisLines()
        {
            double[ ] macdDoubles;
            double[ ] macdSignal;

            lock ( this )
            {
                macdDoubles = GeneralHelper.EnumerableToArray( IndicatorResult[ "MACD" ] );
                macdSignal = GeneralHelper.EnumerableToArray( IndicatorResult[ "MACDSignal" ] );
            }

            return new double[ ][ ] { macdDoubles, macdSignal };

            //return null;
        }

        protected override void OnFinalCalculate( bool fullRecalculation, DataBarUpdateType? updateType )
        {

        }

        public double iSmooth( ref double[ , ] workingBuffer, int beginIndex, double price, double length, double phase, int i, int s = 0 )
        {
            if ( length <= 1 )
            {
                return ( price );
            }
            //if ( ArrayRange( wrk, 0 ) != Bars ) ArrayResize( wrk, Bars );

            int r = beginIndex + i;

            if ( r < 0 )
            {
                throw new NotImplementedException();
            }

            if ( r == 0 )       // For the first bar, we need to initialize the arrary
            {
                int k;

                //! assuming the close is 1.1673
                //!     1st     2nd     3rd     4th     5th     6th     7th     8th     9th     10th
                //! 1.1673  1.1673  1.1673  1.1673  1.1673  1.1673  1.1673      0       0       0

                for ( k = 0; k < 7; k++ )
                {
                    workingBuffer[ r, k + s ] = price;
                }

                for ( ; k < 10; k++ )
                {
                    workingBuffer[ r, k + s ] = 0;
                }

                return ( price );
            }

            //!where len1 - additional periodic factor:
            //!len1 = Log( SquareRoot( len ) ) / Log( 2.0 ) + 2(if len1 < 0 then len1 = 0).
            double len1   = Math.Max( Math.Log( Math.Sqrt( 0.5*( length-1 ))) / Math.Log(2.0)+2.0,0);

            //! pow1 - power of relative volatility with following formula:
            //! pow1 = len1 - 2(if pow1 < 0.5 then pow1 = 0.5),
            double pow1   = Math.Max( len1-2.0, 0.5 );

            //! del1 - distance between price and upper band del1 = Price - UpperBand
            double del1   = price - workingBuffer[ r-1,bsmax+s ];

            //! del2 - distance between price and lower band del2 = Price - LowerBand
            double del2   = price - workingBuffer[ r-1,bsmin+s ];

            double div    = 1.0/( 10.0 + 10.0*( Math.Min( Math.Max( length-10,0 ),100))/100);
            int    forBar = Math.Min(r,10);

            //! The formula for price volatility is
            //! Volty = max between Abs( del1) and Abs( del2), if Abs( del1 ) = Abs( del2 ) then Volty = 0,

            //! if Abs( del1 ) = Abs( del2 ) then Volty = 0
            workingBuffer[ r, volty + s ] = 0;

            if ( Math.Abs( del1 ) > Math.Abs( del2 ) )
            {
                //! Volty = max between Abs( del1) and Abs( del2),
                workingBuffer[ r, volty + s ] = Math.Abs( del1 );
            }

            if ( Math.Abs( del1 ) < Math.Abs( del2 ) )
            {
                //! Volty = max between Abs( del1) and Abs( del2),
                workingBuffer[ r, volty + s ] = Math.Abs( del2 );
            }

            //! vSum - incremental sum of (Volty - Volty[10])/10;
            workingBuffer[ r, vsum + s ] = workingBuffer[ r - 1, vsum + s ] + ( workingBuffer[ r, volty + s ] - workingBuffer[ r - forBar, volty + s ] ) * div;


            //! AvgVolty - average volatility for which Jurik use difficult enough algorithm of calculation:
            //! AvgVolty = Average( vSum, AvgLen )
            //! AvgLen - period of average (Jurik use 65)
            workingBuffer[ r, avolty + s ] = workingBuffer[ r - 1, avolty + s ] + ( 2.0 / ( Math.Max( 4.0 * length, 30 ) + 1.0 ) ) * ( workingBuffer[ r, vsum + s ] - workingBuffer[ r - 1, avolty + s ] );

            double dVolty = 0;

            //! The formula for relative price volatility is
            //! rVolty = Volty / AvgVolty
            //! (if rVolty > len1 ^ ( 1 / pow1 ) then rVolty = len1^(1/pow1), if rVolty < 1 then rVolty = 1), where:
            if ( workingBuffer[ r, avolty + s ] > 0 )
            {
                dVolty = workingBuffer[ r, volty + s ] / workingBuffer[ r, avolty + s ];
            }

            if ( dVolty > Math.Pow( len1, 1.0 / pow1 ) )
            {
                dVolty = Math.Pow( len1, 1.0 / pow1 );
            }

            if ( dVolty < 1 )
            {
                dVolty = 1.0;
            }


            double pow2 = Math.Pow(dVolty, pow1);
            double len2 = Math.Sqrt(0.5*(length-1))*len1;

            //! Kv - volatility's factor Kv = bet ^ SquareRoot(pow2).
            double Kv   = Math.Pow(len2/(len2+1), Math.Sqrt(pow2));

            //! if del1 > 0 then UpperBand = Price else UpperBand = Price - Kv*del1
            if ( del1 > 0 )
            {
                workingBuffer[ r, bsmax + s ] = price;
            }
            else
            {
                workingBuffer[ r, bsmax + s ] = price - Kv * del1;
            }

            //! if del2 < 0 then LowerBand = Price else LowerBand = Price - Kv*del2
            if ( del2 < 0 )
            {
                workingBuffer[ r, bsmin + s ] = price;
            }
            else
            {
                workingBuffer[ r, bsmin + s ] = price - Kv * del2;
            }

            //! PR - Phase Ratio: PR = Phase/100 + 1.5 (if Phase < -100 then PR=0.5, if Phase > 100 then PR=2.5).
            double R     = Math.Max(Math.Min(phase,100),-100)/100.0 + 1.5;

            //! beta - periodic ratio = 0.45*(Length-1)/(0.45*(Length-1)+2)
            double beta  = 0.45*(length-1)/(0.45*(length-1)+2);

            //! The Dynamic Factor is periodic factor (beta) raised to a power (pow):
            //! alpha = beta ^ Pow,            
            double alpha = Math.Pow(beta,pow2);

            //! 1st stage - preliminary smoothing by adaptive EMA:
            //! MA1 = (1-alpha) X Price + alpha X MA1[1]; 
            //! MA1 = price - alpha X price + alpha X MA[1] = price + alpha x ( MA[1] - price )
            workingBuffer[ r, 0 + s ] = price + alpha * ( workingBuffer[ r - 1, 0 + s ] - price );

            //! 2nd stage - one more preliminary smoothing by Kalman filter:
            //! Det0 = ( Price - MA1 ) * ( 1 - beta ) + beta * Det0 [ 1 ];            
            workingBuffer[ r, 1 + s ] = ( price - workingBuffer[ r, 0 + s ] ) * ( 1 - beta ) + beta * workingBuffer[ r - 1, 1 + s ];

            //! MA2 = MA1 + PR * Det0;
            workingBuffer[ r, 2 + s ] = ( workingBuffer[ r, 0 + s ] + R * workingBuffer[ r, 1 + s ] );


            //! 3rd stage - final smoothing by unique Jurik adaptive filter:
            //! Det1 = ( MA2 - JMA [ 1 ] ) * ( 1 - alpha ) ^ 2 + alpha ^ 2 * Det1 [ 1 ];            
            workingBuffer[ r, 3 + s ] = ( workingBuffer[ r, 2 + s ] - workingBuffer[ r - 1, 4 + s ] ) * Math.Pow( ( 1 - alpha ), 2 ) + Math.Pow( alpha, 2 ) * workingBuffer[ r - 1, 3 + s ];

            //! JMA = JMA [ 1 ] + Det1;
            workingBuffer[ r, 4 + s ] = ( workingBuffer[ r - 1, 4 + s ] + workingBuffer[ r, 3 + s ] );

            workingBuffer[ r, myPrice + s ] = price;


            return ( workingBuffer[ r, 4 + s ] );
        }

        protected void RestoreLastXRows( int rows, double[ , ] backup, ref double[ , ] originalBuffer )
        {
            if ( originalBuffer.GetLength( 0 ) >= rows )
            {
                for ( int i = 0; i < rows; i++ )
                {
                    for ( int j = 0; j < 11; ++j )
                    {
                        originalBuffer[ i, j ] = backup[ i, j ];
                    }
                }
            }
        }

    }
}

