using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum TrendType
    {
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\1BarTrend.jpg" />
        /// From a low price each time the market makes a higher high than the previous bar, a minor trend line moves up from the recent low to the new high
        MinorTrend = 0,
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\2BarTrend.jpg" />
        /// From a low price each time the market makes a higher-high than the previous bar for two consecutive time perids, an intermediate trend line
        /// moves up for the low two bars back to the new high.
        IntermediateTrend = 1,
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\3BarTrend.jpg" />
        /// The main swing chart follows the three-bar movements of the market from a low price each time the market makes a higher high than the previous
        /// bar for three consecutive time periods, a main trend line moves up from the low three bars back to the new high.
        MainTrend = 2
    }



    public class GannSwingVariables
    {
        public double            _lastHigh                            = 0.0;
        public double            _lastLow                             = 0.0;

        public double            _lastHighBackup                      = 0.0;
        public double            _lastLowBackup                       = 0.0;
        public TrendDirection    _trendDirectionBackup                = TrendDirection.NoTrend;

        public int               _lastHigherHighIndex                 = 0 ;



        public int               _lastLowerLowIndex                   = 0;


        public long              _timeOfLastCheckedBar                = long.MinValue;
        public int               _lastHigherHighIndexBackup           = 0;
        public int               _lastLowerLowIndexBackup             = 0;


        public TrendDirection    _trendDirection;
        public TrendType         _trendType                           = TrendType.IntermediateTrend;
        public int               _lastGannSwingExtremumIndex          = 0;

        public int               _newLastGannSwingExtremumIndex       = 0;
        public int               _newLastHigherHighIndex              = 0 ;
        public int               _newLastLowerLowIndex                = 0;
        public TrendDirection    _newTrendDirection;





        public void RestoreVariablesSnapshot( )
        {
            _lastHigherHighIndex           = _lastHigherHighIndexBackup;
            _lastLowerLowIndex             = _lastLowerLowIndexBackup;
            _trendDirection                = _trendDirectionBackup;
            _lastLow                       = _lastLowBackup;
            _lastHigh                      = _lastHighBackup;
        }

        public void ResetVariablesSnapshot( )
        {
            _lastHigherHighIndex           = 0;
            _lastLowerLowIndex             = 0;
            _trendDirection                = TrendDirection.NoTrend;
            _lastLow                       = 0;
            _lastHigh                      = 0;
        }

        public void StoreVariablesSnapshot( double lastLow, double lastHigh )
        {
            _lastHigherHighIndexBackup     = _lastHigherHighIndex;
            _lastLowerLowIndexBackup       = _lastLowerLowIndex;
            _trendDirectionBackup          = _trendDirection;
            _lastLowBackup                 = lastLow;
            _lastHighBackup                = lastHigh;
        }

        public void ResetVariables( )
        {
            _lastHigh                      = 0.0;
            _lastLow                       = 0.0;

            _lastHighBackup                = 0.0;
            _lastLowBackup                 = 0.0;
            _trendDirectionBackup          = TrendDirection.NoTrend;

            _lastHigherHighIndex           = 0;
            _lastLowerLowIndex             = 0;
            _timeOfLastCheckedBar          = long.MinValue;
            _lastHigherHighIndexBackup     = 0;
            _lastLowerLowIndexBackup       = 0;


            _trendDirection                = TrendDirection.NoTrend;
            _trendType                     = TrendType.IntermediateTrend;
            _lastGannSwingExtremumIndex    = 0;
        }

        public void NewAndSwapVariables( )
        {
            _newLastHigherHighIndex        = 0;
            _newLastLowerLowIndex          = 0;

            _newLastGannSwingExtremumIndex = 0;

            _newTrendDirection             = TrendDirection.NoTrend;
        }

        public void SwapVariablesAfterDone( )
        {
            _lastHigherHighIndex           = _newLastHigherHighIndex;
            _lastLowerLowIndex             = _newLastLowerLowIndex;

            _lastGannSwingExtremumIndex    = _newLastGannSwingExtremumIndex;

            _trendDirection                = _newTrendDirection;
        }
    }

    

    public class NeoSwingVariables
    {

        double _zigZag4;
        double _zigZag3;
        double _zigZag2;
        long _time4;
        long _time3;
        long _time2;
        public int                _extDepth                         = 21;
        public int                _extBackstep                      = 34;
        public bool               _noBackstep                       = false;
        public bool               _recoverFilter                    = true;

        public int                _lastnenZigZagExtremumIndex       = -1;

        public int                _extLabel                         = 0;


        public int               _zigzagLabel                      = 0; // = 0-before the first fracture ZZ. = 1-looking tags highs. = 2-looking for tags lows.        



        public long Time2
        {
            get => _time2;
            set => _time2 = value;
        }


        public long Time3
        {
            get => _time3;
            set => _time3 = value;
        }


        public long Time4
        {
            get => _time4;
            set => _time4 = value;
        }


        public double ZigZag2
        {
            get => _zigZag2;
            set => _zigZag2 = value;
        }


        public double ZigZag3
        {
            get => _zigZag3;
            set => _zigZag3 = value;
        }

        
        public double ZigZag4
        {
            get => _zigZag4;
            set => _zigZag4 = value;
        }
        

        public void RestoreVariablesSnapshot( )
        {
            
        }

        public void ResetVariablesSnapshot( )
        {
            
        }

        public void StoreVariablesSnapshot( double lastLow, double lastHigh )
        {
            
        }

        public void ResetVariables( )
        {
            
        }

        public void NewAndSwapVariables( )
        {
            
        }

        public void SwapVariablesAfterDone( )
        {
            
        }
    }
}
