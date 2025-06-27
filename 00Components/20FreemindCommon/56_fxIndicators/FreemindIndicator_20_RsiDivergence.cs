using fx.Common;
using fx.Bars;

using DevExpress.Mvvm;

using fx.Definitions;
using fx.Algorithm;
using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using fx.TALib;
using fx.Definitions.Collections;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        protected Task AdvancedRsiDivergenceTasks(bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList, int barsCountBeforeCalculation )
        {
            if (_noCalculationAllowed && (updateType != DataBarUpdateType.Reloaded))
            {
                return null;
            }

            Task first = new Task(() => DetectRsiDivergenceFromZigZag(fullRecalculation, updateType, barsCountBeforeCalculation ), IndicatorExitToken);

            tasksList.Add(first);

            return first;
        }

        protected void DetectRsiDivergenceFromZigZag(bool fullRecalculation, DataBarUpdateType? updateType, int barB4Calculation )
        {
            if (_noCalculationAllowed && (updateType != DataBarUpdateType.Reloaded))
            {
                return;
            }

            
            //var lastSignal = TASignal.NONE;
            //var foundDivergence = false;

            var symbol = Bars.Security.Code;
            var period = Bars.Period.Value;

            if (_hews != null)
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );
                if( aa == null )
                    return;

                var taManager = ( PeriodXTaManager )aa.GetPeriodXTa( period );
                var waveImportanceCopy = _hews.GetAscendingWaveImportanceClone(period);

                if (waveImportanceCopy.Count > 0)
                {
                    RsiDetectLongTermR2RDivergence(symbol, period, taManager, waveImportanceCopy);
                    RsiDetectShortTermLocalDivergence(symbol, period, taManager, waveImportanceCopy);
                }

            }
        }
        void RsiDetectLongTermR2RDivergence(string symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var waves = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.HRS08IMPT).ToArray();

            var peakTroughCount = waves.Count();

            var barB4Calculation = _barCountBeforeCalculation;
            //var lastSignal = TADivergence.NoDivergence;
            //var ithBar = -1;
            var foundDivergence = false;

            // Find major turning points divergence
            for (int i = peakTroughCount - 1; i >= 0; i--)
            {
                if (i >= _lastMacdCrossIndex) continue;

                if ((i - 2) >= 0)
                {
                    var first = waves[i];
                    var second = waves[i - 1];
                    var third = waves[i - 2];

                    ref SBar bar1st = ref Bars.GetBarByTime(  first.Key );
                    ref SBar bar2nd = ref Bars.GetBarByTime(  second.Key );
                    ref SBar bar3rd = ref Bars.GetBarByTime(  third.Key );


                    if ( bar1st == SBar.EmptySBar ||
                         bar2nd == SBar.EmptySBar ||
                         bar3rd == SBar.EmptySBar )
                    {
                        continue;
                    }

                    if (period == TimeSpan.FromMinutes(5) && bar1st.BarIndex >= 9270)
                    {

                    }

                    if (bar1st.BarIndex >= _lastMacdCrossIndex) continue;


                    if (first.Value.Signal == TASignal.WAVE_PEAK)
                    {
                        // Here we have a down trend
                        var higherHigh = HigherOrCloselyEqual(period, bar1st.High, bar3rd.High);

                        if ((higherHigh != DivergenceBoolean.False) /*&& ( bar2nd.Low > bar4th.Low )*/ )
                        {
                            //if (MacdLowerOrCloselyEqual(period, bar1st, bar3rd) != DivergenceBoolean.False)
                            //{
                            //    macdDivergence[(int)bar1st.BarIndex] = TASignal.HAS_DIVERGENCE;

                            //    foundDivergence = true;
                            //    var divergence = (higherHigh == DivergenceBoolean.CloselyEqual) ? TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_DOUBLE_TOP : TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH;

                            //    if ((int)bar1st.BarIndex > ithBar)
                            //    {
                            //        ithBar = (int)bar1st.BarIndex;

                            //        lastSignal = divergence;
                            //    }

                            //    taManager.AddDivergenceInfo(bar1st, new DivergenceInfo(period, divergence, bar1st.BarIndex, bar1st.High, bar3rd.BarIndex, bar3rd.High));
                            //}
                        }
                    }
                    else if (first.Value.Signal == TASignal.WAVE_TROUGH)
                    {
                        var lowerLow = LowerOrCloselyEqual(period, bar1st.Low, bar3rd.Low);

                        if ((lowerLow != DivergenceBoolean.False) /*&& ( bar2nd.High < bar4th.High ) */ )
                        {
                            //if ( bar1stMacd > bar3rdMacd )
                            //if (MacdHigherOrCloselyEqual(period, bar1st, bar3rd) != DivergenceBoolean.False)
                            //{
                            //    macdDivergence[(int)bar1st.BarIndex] = TASignal.HAS_DIVERGENCE;

                            //    foundDivergence = true;

                            //    var divergence = (lowerLow == DivergenceBoolean.CloselyEqual) ? TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM : TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW;

                            //    if ((int)bar1st.BarIndex > ithBar)
                            //    {
                            //        ithBar = (int)bar1st.BarIndex;

                            //        lastSignal = divergence;
                            //    }

                            //    taManager.AddDivergenceInfo(bar1st, new DivergenceInfo(period, divergence, bar1st.BarIndex, bar1st.Low, bar3rd.BarIndex, bar3rd.Low));
                            //}
                        }
                    }

                }
            }

            if (foundDivergence)
            {
                //DatabarsRepo.AddSignalsToDataBar(macdDivergence);
                //_fxTradingEventsBindingList = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security ).TradingEventsBindingList(symbol);
                //_fxTradingEventsBindingList.AddDevergence(period, ithBar, lastSignal, barB4Calculation);
            }

        }
        void RsiDetectShortTermLocalDivergence(string symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportanceCopy)
        {
            //throw new NotImplementedException();
        }

    }
}
