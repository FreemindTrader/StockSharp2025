using System;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Collections;
using fx.Definitions.Collections;
using StockSharp.BusinessEntities;
using Ecng.Common;
using fx.TimePeriod;
using fx.Collections;
using fx.Bars;
using System.Linq;
using fx.Definitions.UndoRedo;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        private IFactal _rootWave;

        public void BuildElliottWaves( TimeSpan period )
        {
            var waveCycle = ElliottWaveCycle.GrandSupercycle;
            var hews      = GetElliottWavesDictionary( period );
            var dbHews    = new List< DbElliottWave >( );

            if ( hews.Count > 0 )
            {
                do
                {
                    foreach ( var eWave in hews )
                    {
                        ref var hew  = ref eWave.Value.GetWaveFromScenario( 1 );
                        var lastWave = hew.GetWavesAtCycle( waveCycle );

                        if ( lastWave.Count > 0 )
                        {
                            dbHews.Add( eWave.Value );
                        }
                    }

                    if ( dbHews.Count > 0 )
                    {
                        BuildElliottWavesFromCycles( period, 1, waveCycle, hews, dbHews );
                    }

                    waveCycle = waveCycle - GlobalConstants.OneWaveCycle;
                    dbHews.Clear( );
                }
                while ( waveCycle > ElliottWaveCycle.Miniscule );
            }
            
        }

        private void BuildElliottWavesFromCycles( TimeSpan period, int waveScenarioNo, ElliottWaveCycle waveCycle, UndoRedoBTreeDictionary<long, DbElliottWave> hews, List<DbElliottWave> dbHews )
        {
            var security  = _bars.Security;

            var dbHewsAsc = dbHews.OrderBy( x => x.BeginTimeUTC ).ToList();

            var aa        = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _bars.Security );

            if ( aa == null )
            {
                return;
            }

            if ( period == TimeSpan.FromDays( 1 ) )
            {

            }


            DbElliottWave currHew = null;
            DbElliottWave nextHew = null;

            for ( int i = 0; i < dbHewsAsc.Count; i++ )
            {
                currHew = dbHewsAsc[ i ];
                
                if ( i + 1 < dbHewsAsc.Count )
                {
                    nextHew = dbHewsAsc[ i + 1 ];

                    ProcessFinishedWaves( period, waveScenarioNo, waveCycle, hews, security, dbHewsAsc, aa, currHew, nextHew );
                }
                else
                {
                    ProcessOngoingWaves( period, waveScenarioNo, waveCycle, hews, security, dbHewsAsc, aa, currHew );
                }
            }
        }

        void ProcessOngoingWaves( TimeSpan period, int waveScenarioNo, ElliottWaveCycle waveCycle, UndoRedoBTreeDictionary<long, DbElliottWave> hews, Security security, List<DbElliottWave> dbHewsAsc, AdvancedAnalysisManager aa, DbElliottWave begin )
        {
            ref var beginHew = ref begin.GetWaveFromScenario( waveScenarioNo );
            
            var beginWave    = beginHew.GetFirstHighestWaveInfo( );
            
            bool is5Waves = false;

            if ( beginWave.HasValue )
            {
                var beginWaveName   = beginWave.Value.WaveName;
                var beginWaveDegree = beginWave.Value.WaveCycle;

                switch ( beginWaveName )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave2:
                    {
                        is5Waves = true;
                    }
                    break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                    {

                        is5Waves = false;
                    }
                    break;

                    case ElliottWaveEnum.Wave4:
                    {
                        //if ( beginWaveName.IsNextWave( curreWaveName ) && ( beginWaveDegree == curreWaveDegree ) )
                        //{
                        //    var waveKey     = new WaveModelKey( security, period, waveScenarioNo, begin.StartDate, current.StartDate, beginWaveDegree );
                        //    is5Waves = true;
                        //    C5Waves c5waves = null;
                        //    var bars        = GetDatabarsRepository( period );

                        //    if ( aa.GetOrCreate5Waves( waveKey, bars, this, out c5waves ) )
                        //    {
                        //        c5waves.ProcessdbHews( waveScenarioNo, begin.StartDate, current.StartDate, waveCycle, hews, dbHewsAsc );
                        //    }
                        //}
                    }
                    break;

                    case ElliottWaveEnum.Wave5:
                    case ElliottWaveEnum.Wave5C:
                    {
                        //AnalyseWave1OrWaveATarget( waveScenarioNo, period, bars, selectedBarTime, waveDegree, ref eWave );
                    }
                    break;

                    case ElliottWaveEnum.WaveTA:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveTB:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveTC:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveTD:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveTE:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveEFA:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveEFB:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveEFC:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveA:
                    {
                        //if ( beginWaveName.IsNextWave( curreWaveName ) && ( beginWaveDegree == curreWaveDegree ) )
                        //{
                        //    var waveKey     = new WaveModelKey( security, period, waveScenarioNo, begin.StartDate, current.StartDate, beginWaveDegree );
                        //    is5Waves = false;
                        //    C3Waves c3waves = null;
                        //    var bars        = GetDatabarsRepository( period );

                        //    if ( aa.GetOrCreate3Waves( waveKey, bars, this, out c3waves ) )
                        //    {
                        //        c3waves.ProcessdbHews( waveScenarioNo, begin.StartDate, current.StartDate, waveCycle, hews, dbHewsAsc );
                        //    }
                        //}
                    }
                    break;

                    case ElliottWaveEnum.WaveB:
                    {
                        var waveKey     = new WaveModelKey( security, period, waveScenarioNo, begin.StartDate, -1, beginWaveDegree );
                        is5Waves = true;
                        C5Waves c5waves = null;
                        var bars        = GetDatabarsRepository( period );

                        if ( aa.GetOrCreate5Waves( waveKey, bars, this, out c5waves ) )
                        {
                            if ( _rootWave == null )
                            {
                                _rootWave = c5waves;
                            }

                            c5waves.ProcessOngoingHews( waveScenarioNo, begin.StartDate, -1, waveCycle, hews, dbHewsAsc );
                        }
                    }
                    break;

                    case ElliottWaveEnum.WaveC:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveX:
                    {
                        var waveKey     = new WaveModelKey( security, period, waveScenarioNo, begin.StartDate, -1, beginWaveDegree );
                        is5Waves        = true;
                        C5Waves c5waves = null;
                        var bars        = GetDatabarsRepository( period );

                        if ( aa.GetOrCreate5Waves( waveKey, bars, this, out c5waves ) )
                        {
                            if ( _rootWave == null )
                            {
                                _rootWave = c5waves;
                            }

                            c5waves.ProcessOngoingHews( waveScenarioNo, begin.StartDate, -1, waveCycle, hews, dbHewsAsc );
                        }

                    }
                    break;

                    case ElliottWaveEnum.WaveY:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveZ:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveW:
                    {


                    }
                    break;

                }
            }

            if ( is5Waves )
            {

            }
        }


        void ProcessFinishedWaves( TimeSpan period, int waveScenarioNo, ElliottWaveCycle waveCycle, UndoRedoBTreeDictionary<long, DbElliottWave> hews, Security security, List<DbElliottWave> dbHewsAsc, AdvancedAnalysisManager aa, DbElliottWave begin, DbElliottWave current )
        {
            ref var beginHew = ref begin.GetWaveFromScenario( waveScenarioNo );
            ref var currxHew = ref current.GetWaveFromScenario( waveScenarioNo );

            var beginWave    = beginHew.GetFirstHighestWaveInfo( );
            var curreWave    = currxHew.GetFirstHighestWaveInfo( );

            bool is5Waves = false;

            if ( beginWave.HasValue )
            {
                var beginWaveName   = beginWave.Value.WaveName;
                var beginWaveDegree = beginWave.Value.WaveCycle;

                var curreWaveName   = curreWave.Value.WaveName;
                var curreWaveDegree = curreWave.Value.WaveCycle;

                switch ( beginWaveName )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave2:
                    {
                        is5Waves = true;
                    }
                    break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                    {

                        is5Waves = false;
                    }
                    break;

                    case ElliottWaveEnum.Wave4:
                    {
                        if ( beginWaveName.IsNextWave( curreWaveName ) && ( beginWaveDegree == curreWaveDegree ) )
                        {
                            var waveKey     = new WaveModelKey( security, period, waveScenarioNo, begin.StartDate, current.StartDate, beginWaveDegree );
                            is5Waves = true;
                            C5Waves c5waves = null;
                            var bars        = GetDatabarsRepository( period );

                            if ( aa.GetOrCreate5Waves( waveKey, bars, this, out c5waves ) )
                            {
                                c5waves.ProcessFinishedWaves( waveScenarioNo, begin.StartDate, current.StartDate, waveCycle, hews, dbHewsAsc );
                            }
                        }
                    }
                    break;

                    case ElliottWaveEnum.Wave5:
                    case ElliottWaveEnum.Wave5C:
                    {
                        //AnalyseWave1OrWaveATarget( waveScenarioNo, period, bars, selectedBarTime, waveDegree, ref eWave );
                    }
                    break;

                    case ElliottWaveEnum.WaveTA:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveTB:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveTC:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveTD:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveTE:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveEFA:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveEFB:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveEFC:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveA:
                    {
                        if ( beginWaveName.IsNextWave( curreWaveName ) && ( beginWaveDegree == curreWaveDegree ) )
                        {
                            var waveKey     = new WaveModelKey( security, period, waveScenarioNo, begin.StartDate, current.StartDate, beginWaveDegree );
                            is5Waves = false;
                            C3Waves c3waves = null;
                            var bars        = GetDatabarsRepository( period );

                            if ( aa.GetOrCreate3Waves( waveKey, bars, this, out c3waves ) )
                            {
                                c3waves.ProcessdbHews( waveScenarioNo, begin.StartDate, current.StartDate, waveCycle, hews, dbHewsAsc );
                            }
                        }
                    }
                    break;

                    case ElliottWaveEnum.WaveB:
                    {
                        foreach ( var ewave in dbHewsAsc )
                        {
                            var info = ewave.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo( );

                            if ( info.HasValue )
                            {
                                var smName  = info.Value.WaveName;
                                var smCycle = info.Value.WaveCycle;

                                if ( smName == ElliottWaveEnum.Wave1 ||
                                     smName == ElliottWaveEnum.Wave1C ||
                                     smName == ElliottWaveEnum.Wave2 ||
                                     smName == ElliottWaveEnum.Wave3 ||
                                     smName == ElliottWaveEnum.Wave3C ||
                                     smName == ElliottWaveEnum.Wave4 ||
                                     smName == ElliottWaveEnum.Wave5 ||
                                     smName == ElliottWaveEnum.Wave5C
                                   )
                                {
                                    var waveKey     = new WaveModelKey( security, period, waveScenarioNo, begin.StartDate , current.StartDate, smCycle );
                                    is5Waves = true;
                                    C5Waves c5waves = null;
                                    var bars        = GetDatabarsRepository( period );

                                    if ( aa.GetOrCreate5Waves( waveKey, bars, this, out c5waves ) )
                                    {
                                        c5waves.ProcessFinishedWaves( waveScenarioNo, begin.StartDate, current.StartDate, smCycle, hews, dbHewsAsc );
                                    }
                                    break;
                                }
                            }
                        }

                    }
                    break;

                    case ElliottWaveEnum.WaveC:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveX:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveY:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveZ:
                    {


                    }
                    break;

                    case ElliottWaveEnum.WaveW:
                    {


                    }
                    break;

                }
            }

            if ( is5Waves )
            {

            }
        }

    }
}
