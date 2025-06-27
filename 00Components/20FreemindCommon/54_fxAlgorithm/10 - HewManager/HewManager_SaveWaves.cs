using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using System.Linq;
using fx.Definitions;
using fx.Definitions.UndoRedo;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Ecng.Collections;


using fx.Collections;

namespace fx.Algorithm
{
    public partial class HewManager
    {
      public void SaveElliottWaveToDatabase( TimeSpan timeframe )
        {
            bool didUpdate = false;

            var periodString = FinancialHelper.GetPeriodId( timeframe );

            using( var context = new ForexDatabars( ) )
            {
                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  We will have to deleted all the waves to be removed first and then we will add new saved waves
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                var waveList = GetSelectedRemovedWavesList( timeframe );

                if ( waveList.Count > 0 )
                {
                    didUpdate = true;

                    foreach ( var rawBarTime in waveList )
                    {
                        try
                        {
                            var itemToRemove = from x in context.ELLIOTTWAVE where x.StartDate == rawBarTime && x.Period == periodString && x.IsLocked != true select x; //returns a single item.
                            //var itemToRemove = context.ELLIOTTWAVE.SingleOrDefault( x => x.StartDate == rawBarTime && x.Period == periodString && x.IsLocked != true  ); //returns a single item.

                            if ( itemToRemove != null )
                            {
                                foreach ( var tobeRemove in itemToRemove )
                                {
                                    context.ELLIOTTWAVE.Remove( tobeRemove );
                                }

                            }
                        }
                        catch ( DbException ex )
                        {
                            //this.LogError( ex );
                        }
                    }
                }

                var hews = GetElliottWavesDictionary( timeframe );

                if( hews.Count > 0 )
                {
                    didUpdate = true;

                    foreach( var wavePair in hews )
                    {
                        var timeOfHigherTF = wavePair.Key;
                        var waveInfo       = wavePair.Value;
                        long id            = waveInfo.Id;

                        if ( waveInfo.HarmonicElliottWaveBit      == 0 &&
                             waveInfo.HarmonicElliottWaveExtraBit == 0 && 
                             waveInfo.AlternativeHewBit           == 0 && 
                             waveInfo.AlternativeHewExtraBit      == 0 )
                        {
                            var itemToRemove = from x in context.ELLIOTTWAVE where x.StartDate == waveInfo.StartDate && x.Period == periodString && x.IsLocked != true select x; //returns a single item.
                            
                            if ( itemToRemove != null )
                            {
                                foreach ( var tobeRemove in itemToRemove )
                                {
                                    context.ELLIOTTWAVE.Remove( tobeRemove );
                                }
                            }

                            continue;
                        }

                        if( id > 0 )
                        {
                            var found = from b in context.ELLIOTTWAVE where b.Id == id select b;

                            if( found.Any( ) )
                            {
                                var first                         = found.FirstOrDefault( );
                                first.StartDate                   = waveInfo.StartDate;
                                first.HarmonicElliottWaveBit      = waveInfo.HarmonicElliottWaveBit;
                                first.HarmonicElliottWaveExtraBit = waveInfo.HarmonicElliottWaveExtraBit;
                                first.AlternativeHewBit           = waveInfo.AlternativeHewBit;
                                first.AlternativeHewExtraBit      = waveInfo.AlternativeHewExtraBit;
                                first.WaveLabelPosition           = waveInfo.WaveLabelPosition;
                                first.OfferId                     = waveInfo.OfferId;
                                first.Period                      = waveInfo.Period;
                                first.HighestWaveCycle            = waveInfo.HighestWaveCycle;
                                first.WaveDate                    = waveInfo.StartDate.FromLinuxTime( );
                            }
                            else
                            {
                                var first                         = context.ELLIOTTWAVE.Create( );
                                first.StartDate                   = waveInfo.StartDate;
                                first.HarmonicElliottWaveBit      = waveInfo.HarmonicElliottWaveBit;
                                first.HarmonicElliottWaveExtraBit = waveInfo.HarmonicElliottWaveExtraBit;
                                first.AlternativeHewBit           = waveInfo.AlternativeHewBit;
                                first.AlternativeHewExtraBit      = waveInfo.AlternativeHewExtraBit;
                                first.WaveLabelPosition           = waveInfo.WaveLabelPosition;
                                first.OfferId                     = waveInfo.OfferId;
                                first.Period                      = waveInfo.Period;
                                first.HighestWaveCycle            = waveInfo.HighestWaveCycle;
                                first.WaveDate                    = waveInfo.StartDate.FromLinuxTime( );

                                context.ELLIOTTWAVE.Add( first );
                            }
                        }
                        else
                        {
                            var found = from b in context.ELLIOTTWAVE where b.StartDate == timeOfHigherTF && b.Period == waveInfo.Period select b;
                            if( found.Any( ) )
                            {
                                var first                         = found.FirstOrDefault( );
                                first.HarmonicElliottWaveBit      = waveInfo.HarmonicElliottWaveBit;
                                first.HarmonicElliottWaveExtraBit = waveInfo.HarmonicElliottWaveExtraBit;
                                first.WaveLabelPosition           = waveInfo.WaveLabelPosition;
                                first.AlternativeHewBit           = waveInfo.AlternativeHewBit;
                                first.AlternativeHewExtraBit      = waveInfo.AlternativeHewExtraBit;
                                first.OfferId                     = waveInfo.OfferId;
                                first.Period                      = waveInfo.Period;
                                first.HighestWaveCycle            = waveInfo.HighestWaveCycle;
                                first.WaveDate                    = waveInfo.StartDate.FromLinuxTime( );
                            }
                            else
                            {
                                var first                         = context.ELLIOTTWAVE.Create( );
                                first.StartDate                   = waveInfo.StartDate;
                                first.HarmonicElliottWaveBit      = waveInfo.HarmonicElliottWaveBit;
                                first.HarmonicElliottWaveExtraBit = waveInfo.HarmonicElliottWaveExtraBit;
                                first.AlternativeHewBit           = waveInfo.AlternativeHewBit;
                                first.AlternativeHewExtraBit      = waveInfo.AlternativeHewExtraBit;
                                first.WaveLabelPosition           = waveInfo.WaveLabelPosition;
                                first.OfferId                     = waveInfo.OfferId;
                                first.Period                      = waveInfo.Period;
                                first.HighestWaveCycle            = waveInfo.HighestWaveCycle;
                                first.WaveDate                    = waveInfo.StartDate.FromLinuxTime( );

                                context.ELLIOTTWAVE.Add( first );
                            }
                        }
                    }
                }

                

                if( didUpdate )
                {
                    try
                    {
                        context.SaveChanges( );
                    }
                    catch( DbException ex )
                    {
                        //this.LogError( ex );
                    }

                    Messenger.Default.Send( new WorkDoneMessage( "Save Waves to Database" ) );
                }
            }
        }

        public void SaveElliottWaveToDatabase( )
        {
            if( _hews.Count > 0 )
            {
                //if ( !_dbElliottWaveRepo.Any( ) ) // The table is empty
                //{
                //    try
                //    {
                //        //_dbElliottWaveRepo.AddRange( _timeToDbElliott_selectedWaveImportanceDict );
                //        _unitOfWork.SaveChanges( );
                //    }
                //    catch ( DbException ex )
                //    {
                //        this.LogError( ex.ErrorMessage );
                //    }
                //}
                //else
                {
                    using( var context = new ForexDatabars( ) )
                    {
                        foreach( var wavePair in _hews )
                        {
                            var timeOfHigherTF = wavePair.Key;
                            var waveInfo = wavePair.Value;
                            long id = waveInfo.Id;

                            if( id > 0 )
                            {
                                var found = from b in context.ELLIOTTWAVE where b.Id == id select b;

                                if( found.Any( ) )
                                {
                                    var first                         = found.FirstOrDefault( );
                                    first.StartDate                   = waveInfo.StartDate;
                                    first.HarmonicElliottWaveBit      = waveInfo.HarmonicElliottWaveBit;
                                    first.HarmonicElliottWaveExtraBit = waveInfo.HarmonicElliottWaveExtraBit;
                                    first.WaveLabelPosition           = waveInfo.WaveLabelPosition;
                                    first.OfferId                     = waveInfo.OfferId;
                                    first.Period                      = waveInfo.Period;
                                    first.HighestWaveCycle            = waveInfo.HighestWaveCycle;
                                }
                                else
                                {
                                    throw new ArgumentException( );
                                }
                            }
                            else
                            {
                                var found                             = from b in context.ELLIOTTWAVE where b.StartDate == timeOfHigherTF && b.Period == waveInfo.Period select b;
                                if( found.Any( ) )
                                {
                                    var first                         = found.FirstOrDefault( );
                                    first.HarmonicElliottWaveBit      = waveInfo.HarmonicElliottWaveBit;
                                    first.HarmonicElliottWaveExtraBit = waveInfo.HarmonicElliottWaveExtraBit;
                                    first.WaveLabelPosition           = waveInfo.WaveLabelPosition;
                                    first.OfferId                     = waveInfo.OfferId;
                                    first.Period                      = waveInfo.Period;
                                    first.HighestWaveCycle            = waveInfo.HighestWaveCycle;
                                }
                                else
                                {
                                    var first                         = context.ELLIOTTWAVE.Create( );
                                    first.StartDate                   = waveInfo.StartDate;
                                    first.HarmonicElliottWaveBit      = waveInfo.HarmonicElliottWaveBit;
                                    first.HarmonicElliottWaveExtraBit = waveInfo.HarmonicElliottWaveExtraBit;
                                    first.WaveLabelPosition           = waveInfo.WaveLabelPosition;
                                    first.OfferId                     = waveInfo.OfferId;
                                    first.Period                      = waveInfo.Period;
                                    first.HighestWaveCycle            = waveInfo.HighestWaveCycle;

                                    context.ELLIOTTWAVE.Add( first );
                                }
                            }
                        }

                        try
                        {
                            context.SaveChanges( );
                        }
                        catch( DbException ex )
                        {
                            //this.LogError( ex );
                        }

                        Messenger.Default.Send( new WorkDoneMessage( "Save Waves to Database" ) );
                    }
                }
            }
        }

        
        public void LockAndSaveWavesInDB( TimeSpan responsibleForWhatTimeFrame, PooledList<long> lockedTime )
        {
            throw new NotImplementedException( );
        }

        public void LockAndSaveWaveInDB( TimeSpan responsibleForWhatTimeFrame, long linuxTime )
        {
            throw new NotImplementedException( );
        }
    }
}
