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
using fx.Bars;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        private ElliottWaveCycle GetWaveCycleFromWaveImportance(
                                                                   long previousWaveTime,
                                                                   long nextWaveTime,
                                                                   TimeSpan responsibleForWhatTimeFrame,
                                                                   long selectedBarTime,
                                                                   ElliottWaveCycle previousHighestWaveCycle,
                                                                   ElliottWaveCycle cycle,
                                                                   ElliottWaveCycle nextHighestWaveCycle )
        {
            var estPreviousWaveImportance = -1;
            var estNextWaveImportance     = -1;
            var estWaveImportance         = -1;
            var output                    = cycle;

            var waveDictionary = GetWaveImportanceDictionary( responsibleForWhatTimeFrame );

            if( waveDictionary != null )
            {
                if( waveDictionary.ContainsKey( selectedBarTime ) )
                {
                    estWaveImportance = waveDictionary[ selectedBarTime ].WaveImportance;
                }

                if( waveDictionary.ContainsKey( previousWaveTime ) )
                {
                    estPreviousWaveImportance = waveDictionary[ previousWaveTime ].WaveImportance;
                }

                if( waveDictionary.ContainsKey( nextWaveTime ) )
                {
                    estNextWaveImportance = waveDictionary[ nextWaveTime ].WaveImportance;
                }
            }

            if( previousHighestWaveCycle == nextHighestWaveCycle )
            {
                /*  
                    *  -----------------------------------------------------------------------------------------------------------------------------
                    *      Since they are neither of the above, we want to check from the wave Estimation.
                    *      1) Since they are both of the same wave Degress, the WaveImportance should be either the same or off by one level
                    *      2) If the current Wave's WaveImportance is of the same as those previous and Next wave, they should be of the same
                    *         degree.
                    *  -----------------------------------------------------------------------------------------------------------------------------
                   */
                if( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                {
                    var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                    var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                    if( estWaveImportance > maxImportance )
                    {
                        output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
                    }
                    else if( estWaveImportance < minImportance )
                    {
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                    }
                    else if( estWaveImportance == minImportance )
                    {
                        output = previousHighestWaveCycle;
                    }
                }
            }
            else if( previousHighestWaveCycle > nextHighestWaveCycle )
            {
                /*  
                             *  -----------------------------------------------------------------------------------------------------------------------------
                             *      Since they are neither of the above, we want to check from the wave Estimation.
                             *      1) Since the previous wave is one degree higher than the next wave 
                             *      2) So we can compare out estWaveImportance with previous one and the next one and see which one it is closed too.
                             *  -----------------------------------------------------------------------------------------------------------------------------
                            */
                if( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                {
                    var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                    var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                    if( estWaveImportance > maxImportance )
                    {
                        output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
                    }
                    else if( estWaveImportance < minImportance )
                    {
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                    }
                    else if( estWaveImportance == estPreviousWaveImportance )
                    {
                        output = previousHighestWaveCycle;
                    }
                    else if( estWaveImportance == estNextWaveImportance )
                    {
                        output = nextHighestWaveCycle;
                    }
                }
            }
            else if( previousHighestWaveCycle < nextHighestWaveCycle )
            {
                /*  
                            *  -----------------------------------------------------------------------------------------------------------------------------
                            *      Since they are neither of the above, we want to check from the wave Estimation.
                            *      1) Since the previous wave is one degree higher than the next wave 
                            *      2) So we can compare out estWaveImportance with previous one and the next one and see which one it is closed too.
                            *  -----------------------------------------------------------------------------------------------------------------------------
                           */
                if( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                {
                    var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                    var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                    if( estWaveImportance > maxImportance )
                    {
                        output = nextHighestWaveCycle + GlobalConstants.OneWaveCycle;
                    }
                    else if( estWaveImportance < minImportance )
                    {
                        output = previousHighestWaveCycle - GlobalConstants.OneWaveCycle;
                    }
                    else if( estWaveImportance == estPreviousWaveImportance )
                    {
                        output = previousHighestWaveCycle;
                    }
                    else if( estWaveImportance == estNextWaveImportance )
                    {
                        output = nextHighestWaveCycle;
                    }
                }
            }

            /*  
                *  -----------------------------------------------------------------------------------------------------------------------------
                *      Since they are neither of the above, we want to check from the wave Estimation.
                *      1) Since the previous wave is one degree higher than the next wave 
                *      2) So we can compare out estWaveImportance with previous one and the next one and see which one it is closed too.
                *  -----------------------------------------------------------------------------------------------------------------------------
            */            if( estPreviousWaveImportance > -1 && estNextWaveImportance > -1 && estWaveImportance > -1 )
                          {
                              var minImportance = Math.Min( estPreviousWaveImportance, estNextWaveImportance );
                              var maxImportance = Math.Max( estPreviousWaveImportance, estNextWaveImportance );

                              if( estWaveImportance > maxImportance )
                              {
                                  output = previousHighestWaveCycle + GlobalConstants.OneWaveCycle;
                              }
                              else if( estWaveImportance < minImportance )
                              {
                                  output = nextHighestWaveCycle - GlobalConstants.OneWaveCycle;
                              }
                              else if( estWaveImportance == estPreviousWaveImportance )
                              {
                                  output = previousHighestWaveCycle;
                              }
                              else if( estWaveImportance == estNextWaveImportance )
                              {
                                  output = nextHighestWaveCycle;
                              }
                          }

            return output;
        }

        private WaveInfo GetCycleAndLabelPositionFromPreviousWave( DbElliottWave previousWave,
                                                                       TimeSpan responsibleForWhatTimeFrame,
                                                                       long selectedBarTime,
                                                                       ElliottWaveCycle cycle,
                                                                       ElliottWaveEnum currentWaveName,
                                                                       ref SBar bar,
                                                                       bool isOutsideBar )
        {
            var output = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            return output;
        }

        private WaveInfo GetCycleAndLabelPositionFromNextWave( DbElliottWave nextWave,
                                                                   TimeSpan responsibleForWhatTimeFrame,
                                                                   long selectedBarTime,
                                                                   ElliottWaveCycle cycle,
                                                                   ElliottWaveEnum currentWaveName,
                                                                   ref SBar bar )
        {
            var output = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            return output;
        }
    }
}
