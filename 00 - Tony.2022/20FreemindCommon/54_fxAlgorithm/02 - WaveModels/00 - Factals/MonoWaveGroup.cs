using fx.Bars;
using fx.Collections;
using fx.Definitions;
using fx.Definitions.Collections;
using StockSharp.Algo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public class MonoWaveGroup : INeelyWave
    {
        PooledList< KeyValuePair< long, WavePointImportance > >      _inBtwWaves = null;

        private MonoWaveManager _parent;
        RangeEx< double >       _monoWaveRange = null;
        int                     _beginBarIndex;
        int                     _endBarIndex;
        int                     _redDotCount = 0;
        int                     _allWaveImptCount = 0;
        int                     _monowaveIndex = -1;
        int                     _complexityLevel;
        int                     _waveImportanceNumber;

        private Vector          _beginPtVector;
        private Vector          _endPtVector;
        private Vector          _beginEndIndex;

        public MonoWaveGroup( MonoWaveManager parent, KeyValuePair<long, WavePointImportance> beginPt, ref SBar beginBar, KeyValuePair<long, WavePointImportance> endPt, ref SBar endBar, TrendDirection direction, MonoWaveNumber waveNumber, PooledList<KeyValuePair<long, WavePointImportance>> intBtw, int waveImportanceNumber )
        {
            BeginPt               = beginPt;
            EndPt                 = endPt;
            _beginBarIndex        = beginBar.Index;
            _endBarIndex          = endBar.Index;
            Direction             = direction;
            WaveNumber            = waveNumber;

            _monoWaveRange        = ( direction == TrendDirection.Uptrend ) ? new RangeEx<double>( beginBar.Low < endBar.High ? beginBar.Low : endBar.High, beginBar.Low < endBar.High ? endBar.High : beginBar.Low )
                                                                            : new RangeEx<double>( beginBar.High < endBar.Low ? beginBar.High : endBar.Low, beginBar.High < endBar.Low ? endBar.Low : beginBar.High );

            _beginPtVector        = ( direction == TrendDirection.Uptrend ) ? new Vector( beginBar.LinuxTime, beginBar.Low ) : new Vector( beginBar.LinuxTime, beginBar.High );

            _endPtVector          = ( direction == TrendDirection.Uptrend ) ? new Vector( endBar.LinuxTime, endBar.High ) : new Vector( endBar.LinuxTime, endBar.Low );

            _beginEndIndex        = new Vector( beginBar.Index, endBar.Index );

            _inBtwWaves           = intBtw;

            _redDotCount          = _inBtwWaves.Where( x => x.Value.WaveImportance == GlobalConstants.DAILYIMPT ).Count( ) - 1;

            _allWaveImptCount     = _inBtwWaves.Count( ) - 2;

            _waveImportanceNumber = waveImportanceNumber;

            _parent               = parent;
        }

        //public MonoWavesGroup( ElliottWaveEnum beginWaveName, DateTime beginTime, ElliottWaveEnum endWaveName, DateTime endTime, ElliottWaveCycle waveCycle )
        //{
        //    BeginWaveName = beginWaveName;
        //    BeginTime = beginTime;
        //    EndWaveName = endWaveName;
        //    EndTime = endTime;
        //    WaveCycle = waveCycle;
        //}

        public Vector BeginVector
        {
            get
            {
                return _beginPtVector;
            }
        }


        public Vector BeginEndBarIndex
        {
            get
            {
                return _beginEndIndex;
            }

        }

        public int WaveImportanceNumber
        {
            get { return _waveImportanceNumber; }
            set
            {
                _waveImportanceNumber = value;
            }
        }


        public Vector EndVector
        {
            get
            {
                return _endPtVector;
            }
        }

        
        public int ComplexityLevel
        {
            get { return _complexityLevel; }
            set
            {
                _complexityLevel = value;
            }
        }
        

        //public static BTreeDictionary<MonoWaveKey, MonoWaveGroup> AllWavesImpt
        //{
        //    get
        //    {
        //        return _allMonoWaves;
        //    }

        //    set
        //    {
        //        _allMonoWaves = value;
        //    }
        //}

        //public static BTreeDictionary<long, WavePointImportance> WaveImportance { get; set; }


        //public static IStructureLabel StructureLabelManager { get; set; }


        public int MonowaveIndex
        {
            get { return _monowaveIndex; }
            set
            {
                _monowaveIndex = value;
            }
        }


        public MonoWaveKey Key
        {
            get
            {
                return new MonoWaveKey( BeginPt.Key, EndPt.Key );
            }
        }

        public long BeginTime
        {
            get
            {
                return BeginPt.Key;
            }
        }


        public override string ToString( )
        {
            string output = "";

            if ( Direction == TrendDirection.Uptrend )
            {
                output = "[" + GlobalConstants.UpTrend + "] " + _beginBarIndex + GlobalConstants.UpTrendArrow + _endBarIndex;
            }
            else
            {
                output = "[" + GlobalConstants.DownTrend + "] " + _beginBarIndex + GlobalConstants.DownTrendArrow + _endBarIndex;
            }

            if ( _structureLabel != StructureLabelEnum.NONE )
            {
                output += GetStructureLabelString( );
            }

            return output;
        }

        public string GetStructureLabelString( )
        {
            return GetStructureLabelString( _structureLabel );
        }

        public static string GetStructureLabelString( StructureLabelEnum label )
        {
            string output = "";

            if ( label.HasFlag( StructureLabelEnum.F3 ) )
            {               
                output += ":F3";
            }

            if ( label.HasFlag( StructureLabelEnum.c3 ) )
            {
                if( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":c3";
            }

            if ( label.HasFlag( StructureLabelEnum.x_c3 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "x:c3";
            }

            if ( label.HasFlag( StructureLabelEnum.sL3 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":sL3";
            }

            if ( label.HasFlag( StructureLabelEnum.L3 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":L3";
            }

            if ( label.HasFlag( StructureLabelEnum._5 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":5";
            }

            if ( label.HasFlag( StructureLabelEnum.s5 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":s5";
            }

            if ( label.HasFlag( StructureLabelEnum.L5 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":L5";
            }

            if ( label.HasFlag( StructureLabelEnum.F3_LESSLIKELY ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "(:F3)";
            }

            if ( label.HasFlag( StructureLabelEnum.c3_LESSLIKELY ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "(:c3)";
            }

            if ( label.HasFlag( StructureLabelEnum.x_c3_LESSLIKELY ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "(x:c3)";
            }

            if ( label.HasFlag( StructureLabelEnum.sL3_LESSLIKELY ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "(:sL3)";
            }

            if ( label.HasFlag( StructureLabelEnum.L3_LESSLIKELY ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "(:L3)";
            }

            if ( label.HasFlag( StructureLabelEnum._5_LESSLIKELY ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "(:5)";
            }

            if ( label.HasFlag( StructureLabelEnum.s5_LESSLIKELY ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "(:s5)";
            }

            if ( label.HasFlag( StructureLabelEnum.L5_LESSLIKELY ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "(:L5)";
            }



            if ( label.HasFlag( StructureLabelEnum.F3_RARE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "[:F3]";
            }

            if ( label.HasFlag( StructureLabelEnum.c3_RARE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "[:c3]";
            }

            if ( label.HasFlag( StructureLabelEnum.x_c3_RARE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "[x:c3]";
            }

            if ( label.HasFlag( StructureLabelEnum.sL3_RARE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "[:sL3]";
            }

            if ( label.HasFlag( StructureLabelEnum.L3_RARE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "[:L3]";
            }

            if ( label.HasFlag( StructureLabelEnum._5_RARE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "[:5]";
            }

            if ( label.HasFlag( StructureLabelEnum.s5_RARE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "[:s5]";
            }

            if ( label.HasFlag( StructureLabelEnum.L5_RARE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "[:L5]";
            }





            if ( label.HasFlag( StructureLabelEnum.F3_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":F3?";
            }

            if ( label.HasFlag( StructureLabelEnum.c3_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":c3?";
            }

            if ( label.HasFlag( StructureLabelEnum.x_c3_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "x:c3?";
            }

            if ( label.HasFlag( StructureLabelEnum.sL3_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":sL3?";
            }

            if ( label.HasFlag( StructureLabelEnum.L3_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":L3?";
            }

            if ( label.HasFlag( StructureLabelEnum._5_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":5?";
            }

            if ( label.HasFlag( StructureLabelEnum.s5_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":s5?";
            }

            if ( label.HasFlag( StructureLabelEnum.L5_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += ":L5?";
            }


            if ( label.HasFlag( StructureLabelEnum.b_F3_MAYBE ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "b:F3";
            }

            if ( label.HasFlag( StructureLabelEnum.b_c3 ) )
            {
                if ( !string.IsNullOrEmpty( output ) )
                    output += " ";

                output += "b:c3";
            }

            output += "}";
            output = "{" + output;


            return output;
        }

        public bool Is3Wave( )
        {
            var subMonoWaves = GetSubWaves( );

            if ( MonoWavesCount > 0 )
            {
                
            }

            return false;
        }

        public PooledList<MonoWaveGroup> GetSubWaves()
        {
            PooledList< MonoWaveGroup > output = new PooledList< MonoWaveGroup >( );



            return output;
        }

        public PooledList< MonoWaveGroup > SubDivide( PooledList<KeyValuePair<long, WavePointImportance>> waveList )
        {
            PooledList< MonoWaveGroup > output = new PooledList< MonoWaveGroup >( );

            var beginPt = waveList[ 0 ];
            var lastPt = waveList.Last( );
            KeyValuePair<long, WavePointImportance> endPt = waveList[ 0 ];

            int highestWaveImpt = -1;

            for ( int i = 1; i < waveList.Count - 1; i++ )
            {
                if ( waveList[ i ].Value.WaveImportance > highestWaveImpt )
                {
                    highestWaveImpt = waveList[ i ].Value.WaveImportance;
                }
            }


            ref SBar beginBar = ref _parent.DatabarsRepo.GetBarByTime(  waveList[ 0 ].Key );

            var beginBarTime = beginBar.LinuxTime;


            if ( beginBar == SBar.EmptySBar ) return null;

                
            
            for ( int i = 0; i < waveList.Count; i++ )
            {
                if ( waveList[ i ].Value.WaveImportance == highestWaveImpt )
                {
                    endPt     = waveList[ i ];

                    ref SBar endBar = ref _parent.DatabarsRepo.GetBarByTime(  waveList[ i ].Key );

                    if ( endBar != SBar.EmptySBar )
                    {
                        var endBarTime = endBar.LinuxTime;

                        var inBtw = waveList.Where( x => x.Key >= beginBarTime && x.Key <= endBarTime ).ToPooledList( );
                        var dir   = endBar.IsWavePeak( ) ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                        MonoWaveGroup waveGroup = new MonoWaveGroup( _parent, beginPt, ref beginBar, endPt, ref endBar, dir, MonoWaveNumber.NA, inBtw, highestWaveImpt );

                        output.Add( waveGroup );

                        beginBar = endBar;
                        beginPt = endPt;
                    }                    
                }
            }

            if ( lastPt.Value.WaveImportance != highestWaveImpt )
            {
                ref SBar endBar = ref _parent.DatabarsRepo.GetBarByTime(  lastPt.Key );

                if ( endBar != SBar.EmptySBar )
                {
                    var endBarTime = endBar.LinuxTime;

                    var inBtw = waveList.Where( x => x.Key >= beginBarTime && x.Key <= endBarTime ).ToPooledList( );
                    var dir   = endBar.IsWavePeak( ) ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    MonoWaveGroup waveGroup = new MonoWaveGroup(_parent, beginPt, ref beginBar, lastPt, ref endBar, dir, MonoWaveNumber.NA, inBtw, highestWaveImpt );

                    output.Add( waveGroup );
                }
                
            }

            return output;
        }

        public PooledList<((SBar, WavePointImportance), (SBar, WavePointImportance))> FindHighestWaveImportancePairs( PooledList<KeyValuePair<long, WavePointImportance>> wavePt )
        {            
            PooledList< ((SBar, WavePointImportance), (SBar, WavePointImportance)) > output = new PooledList<((SBar, WavePointImportance), (SBar, WavePointImportance))>();

            int highestWaveImpt = -1;

            for ( int i = 1; i < wavePt.Count - 1; i++ )
            {
                if ( wavePt[ i ].Value.WaveImportance > highestWaveImpt )
                {
                    highestWaveImpt = wavePt[ i ].Value.WaveImportance;
                }
            }

            SBar beginBar = default;
            WavePointImportance beginWaveImpt = null;
            
            SBar endBar = default;
            WavePointImportance endWaveImpt = null;
            

            for ( int i = 0; i < wavePt.Count; i++ )
            {
                if ( wavePt[ i ].Value.WaveImportance == highestWaveImpt )
                {
                    ref SBar currentBar = ref _parent.DatabarsRepo.GetBarByTime( wavePt[ i ].Key );

                    if ( currentBar != SBar.EmptySBar )                        
                    {
                        if ( beginBar == default )
                        {
                            beginBar = currentBar;
                            beginWaveImpt = wavePt[ i ].Value;

                            continue;
                        }

                        if ( beginBar != default && endBar == default )
                        {
                            endBar = currentBar;
                            endWaveImpt = wavePt[ i ].Value;
                        }

                        if ( beginBar != default && endBar != default )
                        {
                            output.Add( ((beginBar, beginWaveImpt), (endBar, endWaveImpt)) );

                            beginBar = default;
                            endBar = default;
                        }
                    }
                }
            }

            return output;
        }

        public bool IsImpulsiveWave( )
        {
            return false;
        }

        private bool HasNonOverlappedWave()
        {
            return false;
        }

        public bool BreakTrendLineBtwXYInLETimeToZ( INeelyWave m2_, INeelyWave m0, INeelyWave m1 )
        {
            var m2_m0TrendLine = new TrendLine( m2_, m0 );
            var m2TrendLine = TrendLine;

            if ( m2_m0TrendLine.Intersect( m2TrendLine, out Vector intersection ) )
            {
                var brokenTime = TimeTakenToBreak( intersection.Y );

                if ( brokenTime != -1 )
                {
                    if ( m1.TimeUnit >= brokenTime )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool BreakTrendLineBtwXY( INeelyWave trendLineBegin, INeelyWave trendLineEnd )
        {
            var m2_m0TrendLine = new TrendLine( trendLineBegin, trendLineEnd );
            var m2TrendLine = TrendLine;

            if ( m2_m0TrendLine.Intersect( m2TrendLine, out Vector intersection ) )
            {
                return true;
            }

            return false;
        }

        public DateTime BeginDateTime
        {
            get
            {
                return BeginPt.Key.FromLinuxTime( );
            }
        }

        public bool Overlap( IWave other )
        {
            if ( Range.Overlaps( other.Range ) )
            {
                return true;
            }

            return false;
        }

        public bool EndNotExceededForXTime( int fourtime_m3_Tom1 )
        {
            var m1BrokenLevel    = End;            
            int timeSpent        = 0;
            bool didBreak        = false;

            var testingMonowave  = GetNext();

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  and the end of m1 is not exceeded for a period which is four times as long as that consumed by m(-3) through m1 and 
             *  
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            do
            {
                timeSpent += testingMonowave.TimeUnit;

                if ( timeSpent > fourtime_m3_Tom1 )
                {
                    break;
                }

                if ( testingMonowave.Break( m1BrokenLevel ) )
                {
                    didBreak = true;
                    break;
                }

                testingMonowave = testingMonowave.GetNext( );

            } while ( testingMonowave != null );

            return !didBreak;
        }

        public bool DontOverlap( IWave other )
        {
            return !Overlap( other );
        }

        public DateTime EndTime { get; set; }

        //public bool IsLessThanXBy( MonoWaveGroup other, double percentage )
        //{
        //    if ( other.DividedBy( this ) < percentage )
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        

        public bool IsRetraced_LtX_byM( double percent, IWave other )
        {
            var retracePercentage = other.Over( this );

            if ( retracePercentage < percent )
            {
                return true;
            }

            return false;
        }

        public bool IsRetraced_NoMoreThanX_byM( double percent, IWave other )
        {
            var retracePercentage = other.Over( this );

            if ( retracePercentage <= percent )
            {
                return true;
            }

            return false;
        }

        public bool IsRetraced_AtLeastX_byM( double percent, IWave other )
        {
            var retracePercentage = other.Over( this );

            if ( retracePercentage > percent )
            {
                return true;
            }

            return false;
        }

        public bool IsRetraced_AtLeastX_PriceTime_byM( double percent, IWave other )
        {
            var retracePercentage = other.Over( this );

            double timeRetrace = other.TimeUnit / ( double ) TimeUnit;

            if ( retracePercentage > percent && timeRetrace > percent )
            {
                return true;
            }

            return false;
        }

        public bool IsRetraced_LtX_EqZtime_byM( double percent, int timeUnit, IWave other )
        {
            var newIndex = other.BeginBarIndex+ timeUnit;
            double otherRange = -1;            

            ref var newBar = ref _parent.DatabarsRepo.GetBarByIndex( newIndex );
            ref var othBar = ref _parent.DatabarsRepo.GetBarByIndex( other.BeginBarIndex );

            if ( Direction == TrendDirection.Uptrend )
            {
                otherRange = newBar.High - othBar.Low;
            }
            else
            {
                otherRange = othBar.High - newBar.Low;
            }

            var myRange = Range.UpperBound - Range.LowerBound;
            var retracePercentage = otherRange / myRange * 100;                       

            if ( retracePercentage < percent )
            {
                return true;
            }

            return false;
        }

        public bool IsPeakTroughForAtLeast_XTime( int timeUnit )
        {
            ref var extremeBar = ref _parent.DatabarsRepo.GetBarByIndex( EndBarIndex ); 
            var newIndex   = extremeBar.BarIndex + timeUnit;

            int brokenIndex = -1;

            if ( extremeBar.IsPeak() )
            {
                brokenIndex = _parent.DatabarsRepo.GetIndexBreakUp( extremeBar.BarIndex + 1 , newIndex, extremeBar.PeakTroughValue );
            }
            else if ( extremeBar.IsTrough() )
            {
                brokenIndex = _parent.DatabarsRepo.GetIndexBreakDown( extremeBar.BarIndex + 1, newIndex, extremeBar.PeakTroughValue );
            }

            return ( brokenIndex == -1 );
        }

        public bool IsRetraced_AtLeastX_NoMoreThanY_byM( double lowerBound, double upperBound, IWave other )
        {
            var retracePercentage = other.Over( this );

            if ( retracePercentage >= lowerBound && retracePercentage < upperBound )
            {
                return true;
            }

            return false;
        }

        public bool IsRetracedLtX_OrGtY_inLETime_byM( double lowerBound, double upperBound, IWave other )
        {
            var retracePercentage = other.Over( this );

            if ( retracePercentage < lowerBound && retracePercentage > upperBound )
            {
                if ( TimeUnit <= other.TimeUnit)
                {
                    return true;
                }                
            }

            return false;
        }

        public bool TimeToEqualX_LessThan( IWave other )
        {
            var requirement = other.Pips;

            double brokenLevel = -1;

            ref var beginBar = ref _parent.DatabarsRepo.GetBarByIndex( BeginBarIndex );

            if ( Direction == TrendDirection.Uptrend )
            {
                brokenLevel = beginBar.Low + requirement;
            }
            else if ( Direction == TrendDirection.DownTrend )
            {
                brokenLevel = beginBar.High - requirement;
            }

            var timeRequired = TimeTakenToBreak( brokenLevel );

            if ( timeRequired < other.TimeUnit )
            {
                return true;
            }

            return false;
        }

        public bool IsRetracedLtX_OrGtY_inLE_ZTime_byM( double lowerBound, double upperBound, int timeUnit, IWave other )
        {
            var retracePercentage = other.Over( this );

            if ( retracePercentage < lowerBound && retracePercentage > upperBound )
            {
                if ( TimeUnit <= timeUnit )
                {
                    return true;
                }
            }

            return false;
        }


        public bool IsCompletelyRetraced_LT_Ytime_byM( int timeUnit, IWave other  )
        {
            if ( other.Range.Contains( Range ) )
            {
                if ( other.TimeUnit <= timeUnit )
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCompletelyRetraced_GT_Ytime_byM(  int timeUnit, IWave other )
        {
            if ( other.Range.Contains( Range ) )
            {
                if ( other.TimeUnit > timeUnit )
                {
                    return true;
                }
            }

            return false;
        }

        public bool CloseOrExceedX_TimeLTEy( double price, double timeTaken )
        {
            var totalTravelled = Math.Abs( Begin - price );

            var close = totalTravelled * 0.1;

            double closePrice = 0;

            if ( Begin > price )
            {
                closePrice = price + close;
            }
            else
            {
                closePrice = price - close;
            }


            var sameDistTime = TimeTakenToBreak( price );

            if ( sameDistTime > -1 )
            {
                if ( sameDistTime < timeTaken )
                {
                    return true;
                }
            }

            var closeDistTime = TimeTakenToBreak( closePrice );

            if ( closeDistTime > -1 )
            {
                if ( closeDistTime < timeTaken )
                {
                    return true;
                }
            }

            return false;            
        }

        public bool ReturnToX_TimeLTEy( double price, double timeTaken )
        {
            var totalTravelled = Math.Abs( Begin - price );            

            var sameDistTime = TimeTakenToBreak( price );

            if ( sameDistTime > -1 )
            {
                if ( sameDistTime < timeTaken )
                {
                    return true;
                }
            }            

            return false;
        }


        public bool IsCompletelyRetraced_byM( IWave other )
        {
            if ( other.Range.Contains( Range ) )
            {
                return true;
            }

            return false;
        }

        public bool IsCompletelyRetraced_Faster_byM( IWave other )
        {
            return ( IsCompletelyRetraced_LT_Ytime_byM( TimeUnit, other ) );            
        }

        public bool IsCompletelyRetraced_Slower_byM( IWave other )
        {
            return ( IsCompletelyRetraced_GT_Ytime_byM( TimeUnit, other ) );
        }

        public bool Plus1TU_IsCompletelyRetraced_Faster_byM( IWave other )
        {
            if ( other.Range.Contains( Range ) )
            {
                if ( ( TimeUnit + 1  ) > other.TimeUnit )
                {
                    return true;
                }
            }

            return false;
        }

        public bool Plus1TU_IsCompletelyRetraced_Slower_byM( IWave other )
        {
            if ( other.Range.Contains( Range ) )
            {
                if ( TimeUnit <= other.TimeUnit || ( TimeUnit + 1 ) < other.TimeUnit )
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCompletelyRetraced_byM_Minus1TU( IWave other )
        {
            if ( other.Range.Contains( Range ) )
            {
                if ( TimeUnit <= other.TimeUnit || TimeUnit <= other.TimeUnit - 1 )
                {
                    return true;
                }
            }

            return false;
        }

        


        public bool Minus1TU_IsCompletelyRetraced_byM( IWave other )
        {
            if ( other.Range.Contains( Range ) )
            {
                if ( TimeUnit <= other.TimeUnit || ( TimeUnit - 1 ) < other.TimeUnit )
                {
                    return true;
                }
            }

            return false;
        }

        public bool PreferableStructureIs( StructureLabelEnum prefered )
        {
            if ( Is3Wave() )
            {

            }
            else if ( IsImpulsiveWave() )
            {

            }

            return false;
        }

        public MonoWaveNumber WaveNumber { get; set; }

        public KeyValuePair<long, WavePointImportance> BeginPt { get; set; }

        public KeyValuePair<long, WavePointImportance> EndPt { get; set; }

        public TrendDirection Direction { get; set; }

        public PriceActionThruTime PriceAction { get; set; }        

        public TimeSpan Duration { get; set; }

        public RangeEx<double> Range
        {
            get { return _monoWaveRange; }            
        }

        public bool PriceTimeAlternation( INeelyWave m0 )
        {
            bool timeAlternate = false;
            bool priceAlternate = false;

            if ( ( m0.TimeUnit > TimeUnit * 2 ) || TimeUnit > m0.TimeUnit * 2 )
            {
                timeAlternate = true;
            }

            if ( ( AllWaveImptCount > m0.AllWaveImptCount * 2 ) || ( m0.AllWaveImptCount > AllWaveImptCount * 2 ) )
            {
                priceAlternate = true;
            }

            return( timeAlternate || priceAlternate );
        }

        public MonoWaveGroup Combine( INeelyWave m2, INeelyWave m3 )
        {
            ref var beginBar = ref _parent.DatabarsRepo.GetBarByIndex( BeginBarIndex );
            var beginLinux = beginBar.LinuxTime;

            ref var m3EndBar = ref _parent.DatabarsRepo.GetBarByIndex( m3.EndBarIndex );
            var m3endLinux = m3EndBar.LinuxTime;

            if ( Direction == TrendDirection.Uptrend )
            {
                if ( m2.Direction == TrendDirection.DownTrend && m3.Direction == TrendDirection.Uptrend )
                {
                    var inBtw = _parent.WaveImportance.Where( x => x.Key >= beginLinux && x.Key <= m3endLinux ).ToPooledList( );

                    var newMono = new MonoWaveGroup( _parent, BeginPt, ref beginBar, m3.EndPt, ref m3EndBar, TrendDirection.Uptrend, MonoWaveNumber.NA, inBtw, _waveImportanceNumber );

                    return newMono;
                }
            }
            else if ( Direction == TrendDirection.DownTrend )
            {
                if ( m2.Direction == TrendDirection.Uptrend && m3.Direction == TrendDirection.DownTrend )
                {
                    var inBtw = _parent.WaveImportance.Where( x => x.Key >= beginLinux && x.Key <= m3endLinux ).ToPooledList( );

                    var newMono = new MonoWaveGroup( _parent, BeginPt, ref beginBar, m3.EndPt, ref m3EndBar, TrendDirection.DownTrend, MonoWaveNumber.NA, inBtw, _waveImportanceNumber );

                    return newMono;
                }
            }

            return null;
        }

        public MonoWaveGroup Combine( INeelyWave m1, INeelyWave m2, INeelyWave m3, INeelyWave m4  )
        {
            var m0_m2 = Combine( m1, m2 );
            var m0_m4 = m0_m2.Combine( m3, m4 );

            return m0_m4;
        }


        public bool EndExceededBy( IWave m2 )
        {
            double brokenLevel = 0;
            TrendDirection breakUpOrDown = TrendDirection.NoTrend;

            ref var endBar = ref _parent.DatabarsRepo.GetBarByIndex( EndBarIndex );

            if ( m2.Direction == TrendDirection.Uptrend )
            {
                brokenLevel = endBar.Low;
                breakUpOrDown = TrendDirection.DownTrend;                
            }
            else if ( m2.Direction == TrendDirection.DownTrend )
            {
                brokenLevel = endBar.High;
                breakUpOrDown = TrendDirection.Uptrend;                
            }

            int brokenIndex = -1;

            if ( breakUpOrDown != TrendDirection.NoTrend && brokenLevel != 0 )
            {
                if ( breakUpOrDown == TrendDirection.Uptrend )
                {
                    brokenIndex = _parent.DatabarsRepo.GetIndexBreakUp( m2.BeginPt.Key, m2.EndPt.Key, brokenLevel );
                }
                else if ( breakUpOrDown == TrendDirection.DownTrend )
                {
                    brokenIndex = _parent.DatabarsRepo.GetIndexBreakUp( m2.BeginPt.Key, m2.EndPt.Key, brokenLevel );
                }

                if ( brokenIndex > -1 )
                {
                    return true;
                }
            }

            return false;
        }

        public bool CompleteWithoutExceeding( IWave m2, WavePosition pos )
        {
            double brokenLevel = 0;
            TrendDirection breakUpOrDown = TrendDirection.NoTrend;

            if ( m2.Direction == TrendDirection.Uptrend )
            {
                if ( pos == WavePosition.Begin )
                {
                    ref var bar = ref _parent.DatabarsRepo.GetBarByIndex( m2.BeginBarIndex );

                    brokenLevel = bar.Low;
                    breakUpOrDown = TrendDirection.DownTrend;
                }
                else if ( pos == WavePosition.End )
                {
                    ref var bar = ref _parent.DatabarsRepo.GetBarByIndex( m2.EndBarIndex );
                    brokenLevel = bar.High;
                    breakUpOrDown = TrendDirection.Uptrend;
                }
            }
            else if ( m2.Direction == TrendDirection.DownTrend )
            {
                if ( pos == WavePosition.Begin )
                {
                    ref var bar = ref _parent.DatabarsRepo.GetBarByIndex( m2.BeginBarIndex );
                    brokenLevel = bar.High;
                    breakUpOrDown = TrendDirection.Uptrend;
                }
                else if ( pos == WavePosition.End )
                {
                    ref var bar = ref _parent.DatabarsRepo.GetBarByIndex( m2.EndBarIndex );
                    brokenLevel = bar.Low;
                    breakUpOrDown = TrendDirection.DownTrend;
                }
            }

            int brokenIndex = -1;

            if ( breakUpOrDown != TrendDirection.NoTrend && brokenLevel != 0 )
            {
                if ( breakUpOrDown == TrendDirection.Uptrend )
                {
                    brokenIndex = _parent.DatabarsRepo.GetIndexBreakUp( m2.BeginPt.Key, m2.EndPt.Key, brokenLevel );
                }
                else if ( breakUpOrDown == TrendDirection.DownTrend )
                {
                    brokenIndex = _parent.DatabarsRepo.GetIndexBreakUp( m2.BeginPt.Key, m2.EndPt.Key, brokenLevel );
                }

                if ( brokenIndex > -1 )
                {
                    return false;
                }
            }

            return true;
        }

        public int BeginBarIndex
        {
            get { return _beginBarIndex; }            
        }
        public int EndBarIndex
        {
            get { return _endBarIndex; }            
        }

        public BrokenLevel BrokenBy( double high, double low )
        {
            var withinUpper = _monoWaveRange.Contains( high );
            var withinLower = _monoWaveRange.Contains( low );

            if ( withinUpper && withinLower )
            {
                return BrokenLevel.NONE;
            }

            if ( high > _monoWaveRange.UpperBound )
            {
                return BrokenLevel.BrokenUp;
            }

            if ( low < _monoWaveRange.LowerBound )
            {
                return BrokenLevel.BrokenDown;
            }

            return BrokenLevel.NONE;
        }

        public double Highest
        {
            get
            {
                ref var beginbar = ref _parent.DatabarsRepo.GetBarByIndex( BeginBarIndex );
                ref var endbar = ref _parent.DatabarsRepo.GetBarByIndex( EndBarIndex );
                
                if ( Direction == TrendDirection.Uptrend )
                {
                    return ( beginbar.Low < endbar.High ? endbar.High : beginbar.Low );
                }
                else if ( Direction == TrendDirection.DownTrend )
                {
                    return ( beginbar.High < endbar.Low ? endbar.Low : beginbar.High );
                }

                return -1;
            }
        }

        public double Lowest
        {
            get
            {
                ref var beginbar = ref _parent.DatabarsRepo.GetBarByIndex( BeginBarIndex );
                ref var endbar = ref _parent.DatabarsRepo.GetBarByIndex( EndBarIndex );

                if ( Direction == TrendDirection.Uptrend )
                {
                    return ( beginbar.Low < endbar.High ? beginbar.Low : endbar.High );
                }
                else if ( Direction == TrendDirection.DownTrend )
                {
                    return ( beginbar.High < endbar.Low ? beginbar.High : endbar.Low );
                }

                return -1;
            }
        }

        



        public BrokenLevel BrokenBy( ref SBar testBar )
        {
            return BrokenBy( testBar.High, testBar.Low );            
        }

        

        public double Retracement( double otherRange )
        {            
            var myRange    = Range.UpperBound  - Range.LowerBound;

            if ( myRange > 0 )
            {
                return ( otherRange / myRange * 100 );
            }

            return 0;
        }

        public int TimeUnit
        {
            get
            {
                return Math.Abs( BeginBarIndex - EndBarIndex );
            }
        }

        public bool BeginBrokenBy( IWave m4 )
        {
            ref var beginbar = ref _parent.DatabarsRepo.GetBarByIndex( BeginBarIndex );
            ref var endbar = ref _parent.DatabarsRepo.GetBarByIndex( m4.EndBarIndex );

            if ( Direction == TrendDirection.Uptrend )
            {
                var testLevel = beginbar.Low;

                if ( m4.Direction == TrendDirection.DownTrend )
                {
                    if ( endbar.Low < testLevel )
                    {
                        return true;
                    }
                }

            }
            else if ( Direction == TrendDirection.DownTrend )
            {
                var testLevel = beginbar.High;

                if ( m4.Direction == TrendDirection.Uptrend )
                {
                    if ( endbar.High > testLevel )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool BeginBrokenBy( double brokenLevel )
        {
            ref var beginbar = ref _parent.DatabarsRepo.GetBarByIndex( BeginBarIndex );
            
            if ( Direction == TrendDirection.Uptrend )
            {
                if ( brokenLevel < beginbar.Low )
                {
                    return true;
                }                
            }
            else if ( Direction == TrendDirection.DownTrend )
            {                
                if ( brokenLevel > beginbar.High )
                {
                    return true;
                }
            }

            return false;
        }

        public int TimeTakenToBreak( double testLevel )
        {
            //ref SBar beginBar = ref _parent.DatabarsRepo.GetBarByTime( BeginPt.Key );
            //if( beginBar == SBar.EmptySBar )
            //    return -1;

            //ref SBar endBar = ref _parent.DatabarsRepo.GetBarByTime( EndPt.Key );
            //if ( endBar == SBar.EmptySBar )
            //    return -1;
            
            if ( Direction == TrendDirection.Uptrend )
            {
                var index = _parent.DatabarsRepo.GetIndexBreakUp( BeginBarIndex, EndBarIndex, testLevel );

                if ( index > -1 )
                {
                    return ( index - BeginBarIndex );
                }

            }
            else if ( Direction == TrendDirection.DownTrend )
            {
                var index = _parent.DatabarsRepo.GetIndexBreakDown( BeginBarIndex, EndBarIndex, testLevel );

                if ( index > -1 )
                {
                    return ( index - BeginBarIndex );
                }
            }

            return -1;
        }

        public int TimeTakenbyMToBreakBegin( IWave m4 )
        {
            ref SBar beginBar = ref _parent.DatabarsRepo.GetBarByTime( BeginPt.Key );
            if( beginBar == SBar.EmptySBar )
                return -1;

            if ( Direction == TrendDirection.Uptrend )
            {
                var testLevel = beginBar.Low;

                if ( m4.Direction == TrendDirection.DownTrend )
                {
                    var index = _parent.DatabarsRepo.GetIndexBreakDown( m4.BeginPt.Key, m4.EndPt.Key, testLevel );

                    if ( index > -1 )
                    {
                        return(  index - m4.BeginBarIndex );
                    }
                }

            }
            else if ( Direction == TrendDirection.DownTrend )
            {
                var testLevel = beginBar.High;

                if ( m4.Direction == TrendDirection.Uptrend )
                {
                    var index = _parent.DatabarsRepo.GetIndexBreakUp( m4.BeginPt.Key, m4.EndPt.Key, testLevel );

                    if ( index > -1 )
                    {
                        return ( index - m4.BeginBarIndex );
                    }
                }
            }

            return -1;
        }

        public int MonoWavesCount
        {
            get
            {
                return _redDotCount;
            }
        }

        public int AllWaveImptCount
        {
            get
            {
                return _allWaveImptCount;
            }
        }

        public double End 
        {
            get
            {
                ref SBar endBar = ref _parent.DatabarsRepo.GetBarByIndex( _endBarIndex );

                if ( Direction == TrendDirection.Uptrend )
                {
                    return endBar.High;
                }
                else if ( Direction == TrendDirection.DownTrend )
                {
                    return endBar.Low;
                }

                return -1;
            }            
        }

        public double Begin
        {
            get
            {
                ref SBar bar = ref _parent.DatabarsRepo.GetBarByIndex( _beginBarIndex );

                if ( Direction == TrendDirection.Uptrend )
                {
                    return bar.Low;
                }
                else if ( Direction == TrendDirection.DownTrend )
                {
                    return bar.High;
                }

                return -1;
            }
        }

        public WavePosition MissingXWavePos { get; set; }

        public StructureLabelEnum MissingXWaves { get; set; }

        public WavePosition WavePosition { get; set; }


        

        public bool IsCloserToX_ThanY_ofZ( double lowerBound, double higherBound, IWave other )
        {
            double ratio = Over( other );

            var higherDiff = Math.Abs( higherBound - ratio );
            var lowerDiff = Math.Abs( lowerBound - ratio );

            if (lowerDiff < higherDiff )
            {
                return true;
            }


            return false;
        }

        public WavePattern WavePattern { get; set; }

        public WaveType MainWaveType { get; set; }

        public ElliottWaveEnum PotentialWave { get; set; }

        private StructureLabelEnum _structureLabel;


        
        public StructureLabelEnum StructureLabel 
        {
            get
            {
                return _structureLabel;
            } 

            set
            {
                _structureLabel = value;

                ref SBar endBar = ref _parent.DatabarsRepo.GetBarByIndex( _endBarIndex );

                //endBar.AddSignal( TASignal.HAS_STRUCT_LABEL );

                _parent.StructuralMgr.SetStructureLabel(ref endBar, _structureLabel );
            }
        
        }

        public void AddStructureLabel( StructureLabelEnum input )
        {
            StructureLabel |= input;
        }

        public void DropStructureLabel( StructureLabelEnum input )
        {
            StructureLabel &= ~input;
        }


        public TrendLine TrendLine
        {
            get
            {
                return new TrendLine( _beginPtVector, _endPtVector );
            }
        }

        public bool IsLongerThan( IWave other )
        {
            if ( Pips > other.Pips )
            {
                return true;
            }

            return false;
        }

        public bool IsLongerThan( IWave first, IWave second )
        {
            if ( Pips > first.Pips && Pips > second.Pips )
            {
                return true;
            }

            return false;
        }

        public bool IsNotTheShortest( IWave first, IWave second )
        {
            if ( Pips > first.Pips || Pips > second.Pips )
            {
                return true;
            }

            return false;
        }

        public bool IsTheShortest( IWave first, IWave second )
        {
            if ( Pips < first.Pips && Pips < second.Pips )
            {
                return true;
            }

            return false;
        }

        public bool IsTheLongest( IWave first, IWave second )
        {
            if ( Pips > first.Pips && Pips > second.Pips )
            {
                return true;
            }

            return false;
        }


        public bool IsNotTheLongest( IWave first, IWave second )
        {
            if ( Pips < first.Pips || Pips < second.Pips )
            {
                return true;
            }

            return false;
        }



        public bool IsShorterThan( IWave other )
        {
            if ( Pips < other.Pips )
            {
                return true;
            }

            return false;
        }

        public bool IsMoreThanX_ofM( double percentage, IWave other )
        {
            if ( Over( other ) > percentage )
            {
                return true;
            }

            return false;
        }

        public bool IsNotMuchMoreThanX_ofM( double percentage, IWave other )
        {
            var pct = Over( other );

            var upperBound = pct * ( 1 + Allowance / 100 );

            if ( pct >= percentage && pct <= upperBound )
            {
                return true;
            }            

            return false;
        }

        public bool IsMoreThanX_LessEqTime_ofM( double percentage, IWave other )
        {
            if ( Over( other ) > percentage )
            {
                if ( TimeUnit <= other.TimeUnit )
                {
                    return true;
                }                
            }

            return false;
        }

        public bool AchieveSameDistanceInLessTime( IWave other )
        {
            var toBeTraveledDist = other.Pips;
            var toBeTraveledTime = other.TimeUnit;

            double sameDistLevel = 0;

            ref SBar bar = ref _parent.DatabarsRepo.GetBarByIndex( _beginBarIndex );

            if ( Direction == TrendDirection.Uptrend )
            {
                sameDistLevel = bar.Low + toBeTraveledDist;
            }
            else
            {
                sameDistLevel = bar.High - toBeTraveledDist;
            }
            
            if ( sameDistLevel > 0 )
            {
                var sameDistTime = TimeTakenToBreak( sameDistLevel );

                if ( sameDistTime > -1 )
                {
                    if ( sameDistTime < toBeTraveledTime )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool AchieveSameDistanceInEqualOrLessTime( IWave other )
        {
            var toBeTraveledDist = other.Pips;
            var toBeTraveledTime = other.TimeUnit;

            double sameDistLevel = 0;

            ref SBar bar = ref _parent.DatabarsRepo.GetBarByIndex( _beginBarIndex );

            if ( Direction == TrendDirection.Uptrend )
            {
                sameDistLevel = bar.Low + toBeTraveledDist;
            }
            else
            {
                sameDistLevel = bar.High - toBeTraveledDist;
            }

            if ( sameDistLevel > 0 )
            {
                var sameDistTime = TimeTakenToBreak( sameDistLevel );

                if ( sameDistTime > -1 )
                {
                    if ( sameDistTime <= toBeTraveledTime )
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool IsAtLeastX_OfM( double percentage, IWave other )
        {
            if ( Over( other ) >= percentage )
            {
                return true;
            }

            return false;
        }

        public bool IsAtLeastX_PriceTime_OfM( double percentage, IWave other )
        {
            bool priceCondition = false;
            bool timeCondition = false;
            
            if ( Over( other ) >= percentage )
            {
                priceCondition = true;
            }

            if ( TimePercenatage( other ) >= percentage )
            {
                timeCondition = true;
            }

            return ( priceCondition && timeCondition );
        }

        public bool IsAtMostX_OfM( double percentage, IWave other )
        {
            if ( Over( other ) <= percentage )
            {
                return true;
            }

            return false;
        }

        public bool IsWithinX_Time_OfM( double percentage, IWave other )
        {
            if ( TimePercenatage( other ) < percentage )
            {
                return true;
            }

            return false;
        }

        public bool IsAtLeastX_Time_OfX( double percentage, IWave other )
        {
            var diff = other.TimeUnit * percentage / 100;
            
            if ( TimeUnit >= diff )
            {
                return true;
            }

            return false;
        }

        public bool IsLessThanX_OfM( double percentage, IWave other )
        {
            if ( Over( other ) < percentage )
            {
                return true;
            }

            return false;
        }

        public bool IsNoMoreThanX_OfM( double percentage, IWave other )
        {
            if ( Over( other ) <= percentage )
            {
                return true;
            }

            return false;
        }

        public bool Break( double m3BrokenLevel )
        {
            if (  _monoWaveRange.Contains( m3BrokenLevel ) )
            {
                if ( ( Math.Abs( _monoWaveRange.UpperBound - m3BrokenLevel ) > 0.00000000001 ) && 
                     ( Math.Abs( _monoWaveRange.LowerBound - m3BrokenLevel ) > 0.00000000001 ) )
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsAtLeastX_LessThanY_ofM( double lowerbound, double upperBound, IWave other )
        {
            var pct = Over( other );

            if ( pct >= lowerbound && pct < upperBound )
            {
                return true;
            }

            return false;
        }

        public bool IsBtw_XYInclusive_ofM( double lowerbound, double upperBound, IWave other )
        {
            var pct = Over( other );

            if ( pct >= lowerbound && pct <= upperBound )
            {
                return true;
            }

            return false;
        }

        public bool IsBtw_AtLeastX_LessThanY_ofM( double lowerbound, double upperBound, IWave other )
        {
            var pct = Over( other );

            if ( pct >= lowerbound && pct < upperBound )
            {
                return true;
            }

            return false;
        }

        public bool HasAnyVariationOf3( )
        {
            if ( StructureLabel.HasFlag( StructureLabelEnum.F3 ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.c3 ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.x_c3 ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.sL3 ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.L3 ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.F3_MAYBE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.c3_MAYBE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.x_c3_MAYBE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.sL3_MAYBE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.L3_MAYBE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.F3_LESSLIKELY ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.c3_LESSLIKELY ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.x_c3_LESSLIKELY ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.sL3_LESSLIKELY ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.L3_LESSLIKELY ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.F3_RARE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.c3_RARE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.x_c3_RARE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.sL3_RARE ) ||
                 StructureLabel.HasFlag( StructureLabelEnum.L3_RARE ) ||
                StructureLabel.HasFlag( StructureLabelEnum.b_F3_MAYBE ) ||
                StructureLabel.HasFlag( StructureLabelEnum.b_c3 ) )
            {
                return true;
            }

            return false;
        }

        public bool IsBtw_XYExclusive_ofM( double lowerbound, double upperBound, IWave other )
        {
            var pct = Over( other );

            if ( pct > lowerbound && pct < upperBound )
            {
                return true;
            }

            return false;
        }

        public bool IsMoreVerticalThan( IWave other )
        {
            if ( TimeUnit < other.TimeUnit )
            {
                return true;
            }

            return false;
        }

        public bool IsLessVerticalThan( IWave other )
        {
            if ( TimeUnit > other.TimeUnit )
            {
                return true;
            }

            return false;
        }

        public bool IsLongerAndMoreVerticalThan( IWave other )
        {
            return ( IsLongerThan( other ) && IsMoreVerticalThan( other ) );
        }

        public ( long start, long end ) First3Monowaves()
        {
            (long start, long end) output = default;

            if ( MonoWavesCount > 3 )
            {
                var first3 = _inBtwWaves.Where( x => x.Value.WaveImportance == GlobalConstants.DAILYIMPT ).OrderBy( x => x.Key ).Take( 4 ).ToList();

                output.start = first3[ 0 ].Key;
                output.end   = first3[ 3 ].Key;
            }

            return output;
        }

        public (long start, long end) FirstNthMonowaves( int monoWaveCount )
        {
            (long start, long end) output = default;

            if ( MonoWavesCount > monoWaveCount )
            {
                var firstNth = _inBtwWaves.Where( x => x.Value.WaveImportance == GlobalConstants.DAILYIMPT ).OrderBy( x => x.Key ).Take( monoWaveCount + 1 ).ToList();

                output.start = firstNth[ 0 ].Key;
                output.end   = firstNth[ monoWaveCount + 1 ].Key;
            }

            return default;
        }

        public double First3MonowavesPips()
        {
            (long start, long end) threeMonos = First3Monowaves( );

            ref SBar barBegin = ref _parent.DatabarsRepo.GetBarByTime( threeMonos.start );
            if ( barBegin == SBar.EmptySBar )
                return -1;

            ref SBar barEnd = ref _parent.DatabarsRepo.GetBarByTime( threeMonos.end );
            if ( barEnd == SBar.EmptySBar )
                return -1;            

            return Math.Abs( barBegin.PeakTroughValue - barEnd.PeakTroughValue );
        }

        public double FirstNthMonowavesPips( int monoWaveCount )
        {
            (long start, long end) nthMonos = FirstNthMonowaves( monoWaveCount );

            ref SBar barBegin = ref _parent.DatabarsRepo.GetBarByTime( nthMonos.start );
            if ( barBegin == SBar.EmptySBar )
                return -1;

            ref SBar barEnd = ref _parent.DatabarsRepo.GetBarByTime( nthMonos.end );
            if ( barEnd == SBar.EmptySBar )
                return -1;

            return Math.Abs( barBegin.PeakTroughValue - barEnd.PeakTroughValue );
        }

        public INeelyWave GetNext( )
        {            
            if ( _parent.MonoWaves.Count > _monowaveIndex + 1 )
            {
                return _parent.MonoWaves.At( _monowaveIndex + 1 ).Value;
            }

            return null;
        }

        public INeelyWave GetPrevious( )
        {
            if ( _parent.MonoWaves.Count > _monowaveIndex && ( _monowaveIndex -1 ) >= 0 )
            {
                return _parent.MonoWaves.At( _monowaveIndex - 1 ).Value;
            }

            return null;
        }

        public bool Exceed( double otherEndValue )
        {
            ref SBar endBar = ref _parent.DatabarsRepo.GetBarByIndex( _endBarIndex );

            if ( Direction == TrendDirection.Uptrend )
            {
                if ( otherEndValue < endBar.High )
                {
                    return true;
                }
            }
            else if ( Direction == TrendDirection.DownTrend )
            {
                if ( otherEndValue > endBar.Low )
                {
                    return true;
                }
            }

            return false;
        }

        

        public double Allowance
        {
            get;
            set;
        } = 5.0;


        public static double DiffPercentage( double one, double two )
        {
            var diff = Math.Abs( one - two );
            var max = Math.Max( one, two );

            return ( diff / max * 100 );
        }

        public double PricePercenatage( IWave m2 )
        {
            return DiffPercentage( Pips, m2.Pips);            
        }

        public double TimePercenatage( IWave m2 )
        {
            return DiffPercentage( TimeUnit, m2.TimeUnit );            
        }

        public bool PriceApproxEqual( IWave m2 )
        {
            var priceDiff   = PricePercenatage( m2 );            

            if ( priceDiff < Allowance )
            {
                return true;
            }

            return false;
        }

        

        public bool TimeApproxEqual( IWave m2 )
        {
            var timeDiffPer = TimePercenatage( m2 );

            if ( timeDiffPer < Allowance )
            {
                return true;
            }

            return false;
        }

        public bool TimeApproxEqual( IWave m2, double pctAllowance )
        {
            var timeDiffPer = TimePercenatage( m2 );

            if ( timeDiffPer < pctAllowance )
            {
                return true;
            }

            return false;
        }

        public bool PriceTimeApproxEqual( IWave m2 )
        {
            var priceDiff   = PricePercenatage( m2 );
            var timeDiffPer = TimePercenatage( m2 );

            if ( priceDiff < Allowance && timeDiffPer < Allowance )
            {
                return true;
            }

            return false;
        }

        public double DividedBy( IWave other )
        {
            var otherRange = other.Range.UpperBound - other.Range.LowerBound;
            var myRange    = Range.UpperBound  - Range.LowerBound;

            if ( myRange > 0 )
            {
                return ( otherRange / myRange * 100 );
            }

            return 0;
        }

        public double Over( IWave other )
        {
            var otherRange = other.Range.UpperBound - other.Range.LowerBound;
            var myRange    = Range.UpperBound  - Range.LowerBound;

            if ( otherRange > 0 )
            {
                return ( myRange / otherRange * 100 );
            }

            return 0;
        }

        public bool IsApproxX_OfM( double percentage, IWave other  )
        {
            var pct = Over( other );

            var pctDiff = DiffPercentage( pct, percentage );

            if ( pctDiff < Allowance )
            {
                return true;
            }

            return false;
        }

        public bool IsApproxEqual_X_OfM( double pctAllowance, IWave m2  )
        {
            var priceDiff   = PricePercenatage( m2 );

            if ( priceDiff < pctAllowance )
            {
                return true;
            }

            return false;
        }


        public bool IsApproxX_PriceTime_OfM( double percentage, IWave m2  )
        {
            var priceDiffPct   = PricePercenatage( m2 );
            var timeDiffPct    = TimePercenatage( m2 );

            var priceDiff      = DiffPercentage( priceDiffPct, percentage );
            var timeDiff       = DiffPercentage( timeDiffPct, percentage );

            if ( priceDiffPct < Allowance && timeDiff < Allowance )
            {
                return true;
            }

            if ( priceDiff < Allowance && timeDiffPct < Allowance )
            {
                return true;
            }

            if ( priceDiff < Allowance && timeDiff < Allowance )
            {
                return true;
            }

            return false;
        }

        
        //public double PriceLengthTo( IWave end )
        //{
        //    double beginValue = -1;
        //    double endValue   = -1;

        //    if ( Direction == TrendDirection.Uptrend )
        //    {
        //        beginValue = BeginBar.Low;

        //        if ( end.Direction == TrendDirection.Uptrend )
        //        {
        //            endValue = end.EndBar.High;
        //        }
        //        else if ( end.Direction == TrendDirection.DownTrend )
        //        {
        //            endValue = end.EndBar.Low;
        //        }
        //    }
        //    else if ( Direction == TrendDirection.DownTrend )
        //    {
        //        beginValue = BeginBar.High;

        //        if ( end.Direction == TrendDirection.Uptrend )
        //        {
        //            endValue = end.EndBar.High;
        //        }
        //        else if ( end.Direction == TrendDirection.DownTrend )
        //        {
        //            endValue = end.EndBar.Low;
        //        }
        //    }

        //    if ( beginValue != -1 && endValue != -1 )
        //    {
        //        return Math.Abs( beginValue - endValue );
        //    }

        //    return -1;
        //}

        //public int TimesTakenTo( IWave end )
        //{
        //    long beginValue = BeginBar.BarIndex;
        //    long endValue = end.EndBar.BarIndex;

        //    

        //    return (int) ( endValue - beginValue );
        //}

        public double Pips
        {
            get
            {
                ref SBar bar = ref _parent.DatabarsRepo.GetBarByIndex( _beginBarIndex );

                var ps = SymbolHelper.GetInstrumentPointSize( bar.SymbolEx.OfferId );

                return ( Math.Round( ( _monoWaveRange.UpperBound - _monoWaveRange.LowerBound ) / ps, 1 ) );
            }

        }

        
    }

    public class MonoWaveKey : IComparable, IComparable<MonoWaveKey>, IEquatable<MonoWaveKey>
    {
        long            _beginBarLinuxTime;
        long            _endBarLinuxTime;
        public MonoWaveKey( long beginBarTime, long endBarTime )
        {
            BeginBarLinuxTime = beginBarTime;
            EndBarLinuxTime = endBarTime;
        }

        public long BeginBarLinuxTime
        {
            get { return _beginBarLinuxTime; }
            set { _beginBarLinuxTime = value; }
        }
        public long EndBarLinuxTime
        {
            get { return _endBarLinuxTime; }
            set { _endBarLinuxTime = value; }
        }

        public bool ContainTime( long barTime )
        {
            if ( BeginBarLinuxTime == barTime || EndBarLinuxTime == barTime )
            {
                return true;
            }

            return false;
        }


        public int CompareTo( object obj )
        {
            if ( obj == null )
                return 1;

            MonoWaveKey other = ( MonoWaveKey ) obj ;

            if ( other == null )
                throw new ArgumentException( "obj is not a MonoWaveKey" );

            return CompareTo( other );
        }

        public int CompareTo( MonoWaveKey other )
        {
            if ( other == null )
                return 1;

            int result = 0;

            result = BeginBarLinuxTime.CompareTo( other.BeginBarLinuxTime );

            if ( result != 0 )
                return result;

            return EndBarLinuxTime.CompareTo( other.EndBarLinuxTime );
        }

        public override bool Equals( object obj )
        {
            if ( obj is MonoWaveKey )
            {
                return Equals( ( MonoWaveKey ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( MonoWaveKey first, MonoWaveKey second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( MonoWaveKey first, MonoWaveKey second )
        {
            return !( first == second );
        }

        public bool Equals( MonoWaveKey other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _beginBarLinuxTime.Equals( other._beginBarLinuxTime ) && _endBarLinuxTime.Equals( other._endBarLinuxTime );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _beginBarLinuxTime.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _endBarLinuxTime.GetHashCode( );
                return hashCode;
            }
        }
    }

    

}
