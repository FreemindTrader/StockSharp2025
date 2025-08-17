using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Linq;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Collections;
using fx.Collections;
using fx.Bars;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        private bool? isZigZagUp( fxHistoricBarsRepo bars, TimeSpan period, long corrBegin, long corrEnd )
        {
            var waveImportance  = GetWaveImportanceDictionary( period );
            var innerPts        = waveImportance.Where( x => x.Key >= corrBegin && x.Key <= corrEnd ).OrderBy( x => x.Key ).ToList();
            var myImportance    = waveImportance[ corrBegin ].WaveImportance;

            int currentWaveImportance = myImportance;

            KeyValuePair< long, WavePointImportance > [] result = null;

            do
            {
                currentWaveImportance = FinancialHelper.GetWaveImportanceDegreeLower( currentWaveImportance );

                result = waveImportance.Where(
                                                x =>
                                                {
                                                    if ( x.Key > corrBegin && x.Key < corrEnd )
                                                    {
                                                        if ( x.Value.WaveImportance == currentWaveImportance )
                                                        {
                                                            return true;
                                                        }
                                                    }

                                                    return false;
                                                }

                                            ).OrderBy( x => x.Key ).ToArray( );


            } while ( result.Length == 0 && currentWaveImportance > -1 );

            if ( result.Length == 0 )
            {
                return null;
            }

            var segmentCount = result.Length + 1;

            ref SBar beginBar = ref bars.GetBarByTime( corrBegin );
            if ( beginBar == SBar.EmptySBar )
                return false;

            ref SBar endBar = ref bars.GetBarByTime( corrEnd );
            if ( endBar == SBar.EmptySBar )
                return false;            

            var peaks    = result.Where( x => x.Value.Signal == TASignal.WAVE_PEAK ).ToPooledList();
            var troughs  = result.Where( x => x.Value.Signal == TASignal.WAVE_TROUGH ).ToPooledList();

            var checkedBar = beginBar;
            var higherHigh = checkedBar.High;

            foreach ( var peak in peaks )
            {
                ref SBar peakBar = ref bars.GetBarByTime( peak.Key );
                
                if ( peakBar != SBar.EmptySBar )                                    
                {
                    if ( peakBar.High > higherHigh )
                    {
                        higherHigh = peakBar.High;
                    }
                    else
                    {
                        return false;
                    }
                }                
            }

            var higherLow = checkedBar.Low;

            foreach ( var trough in troughs )
            {
                ref SBar troughBar = ref bars.GetBarByTime( trough.Key );

                if ( troughBar != SBar.EmptySBar )
                {
                    if ( troughBar.Low > higherLow )
                    {
                        higherLow = troughBar.Low;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if ( endBar.High > higherHigh ) 
            {
                return true;
            }

            return false;
        }
    

        public bool? isZigZagDown( fxHistoricBarsRepo bars, TimeSpan period, long corrBegin, long corrEnd )
        {
            var waveImportance  = GetWaveImportanceDictionary( period );
            var myImportance    = waveImportance[ corrBegin ].WaveImportance;

            int currentWaveImportance = myImportance;

            KeyValuePair< long, WavePointImportance > [] result = null;

            do
            {
                currentWaveImportance = FinancialHelper.GetWaveImportanceDegreeLower( currentWaveImportance );

                result = waveImportance.Where(
                                                x =>
                                                {
                                                    if ( x.Key > corrBegin && x.Key < corrEnd )
                                                    {
                                                        if ( x.Value.WaveImportance == currentWaveImportance )
                                                        {
                                                            return true;
                                                        }
                                                    }

                                                    return false;
                                                }

                                            ).OrderBy( x => x.Key ).ToArray( );


            } while ( result.Length == 0 && currentWaveImportance > -1 );

            if ( result.Length == 0 )
            {
                return null;
            }

            var segmentCount = result.Length + 1;

            // ![](1181B14802AC4A6E9181CF68596F61C5.png;;;0.04358,0.02694)                    


            ref SBar beginBar = ref bars.GetBarByTime( corrBegin );
            if ( beginBar == SBar.EmptySBar )
                return false;

            ref SBar endBar = ref bars.GetBarByTime( corrEnd );
            if ( endBar == SBar.EmptySBar )
                return false;
            

            var peaks    = result.Where( x => x.Value.Signal == TASignal.WAVE_PEAK ).ToPooledList();
            var troughs  = result.Where( x => x.Value.Signal == TASignal.WAVE_TROUGH ).ToPooledList();

            var checkedBar = beginBar;
            var lowerHigh = checkedBar.High;

            foreach ( var peak in peaks )
            {
                ref SBar peakBar = ref bars.GetBarByTime( peak.Key );

                if ( peakBar != SBar.EmptySBar )
                {                    
                    if ( peakBar.High < lowerHigh )
                    {
                        lowerHigh = peakBar.High;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            var lowerLow = checkedBar.Low;

            foreach ( var trough in troughs )
            {
                ref SBar troughBar = ref bars.GetBarByTime( trough.Key );

                if ( troughBar != SBar.EmptySBar )
                {
                    if ( troughBar.Low < lowerLow )
                    {
                        lowerLow = troughBar.Low;
                    }
                    else
                    {
                        return false;
                    }
                }                
            }

            if ( endBar.Low < lowerLow )
            {
                return true;
            }

            return false;
        }

        private WavePattern GetZigZagTypeDown( fxHistoricBarsRepo bars, TimeSpan period, long corrBegin, long corrEnd )
        {
            WavePattern ouput = WavePattern.UNKNOWN;
            var waveImportance  = GetWaveImportanceDictionary( period );
            var innerPts        = waveImportance.Where( x => x.Key >= corrBegin && x.Key <= corrEnd ).OrderBy( x => x.Key ).ToList();


            return ouput;
        }

        private WavePattern GetZigZagTypeUp( fxHistoricBarsRepo bars, TimeSpan period, long corrBegin, long corrEnd )
        {
            WavePattern ouput = WavePattern.UNKNOWN;

            return ouput;
        }

        public bool? isExpandedFlatDown( fxHistoricBarsRepo bars, TimeSpan period, long corrBegin, long corrEnd )
        {
            //throw new NotImplementedException( );

            return null;
        }

        public bool? isExpandedUpDown( fxHistoricBarsRepo bars, TimeSpan period, long corrBegin, long corrEnd )
        {
            //throw new NotImplementedException( );

            return null;
        }
    }
}