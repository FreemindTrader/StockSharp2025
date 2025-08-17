using fx.Collections;

using fx.Definitions;
using fx.Definitions.Collections;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Bars;

namespace fx.Algorithm
{
    public partial class MonoWaveManager
    {
        public MonoWaveManager( )
        {            
        }

        private BTreeDictionary< MonoWaveKey, MonoWaveGroup >           _ascendingMonowaves = new BTreeDictionary<MonoWaveKey, MonoWaveGroup>( );
        private BTreeDictionary< long, WavePointImportance >            _completeWaveImportanceCopy = null;
        private PooledList< KeyValuePair< long, WavePointImportance > > _monoWaveImportance = null;

        private ThreadSafeDictionary< long, MonoWaveGroup >             _barTimeToWave = new ThreadSafeDictionary< long, MonoWaveGroup >( );

        private fxHistoricBarsRepo _historicBarsRepo = null;
        public fxHistoricBarsRepo DatabarsRepo
        {
            get
            {
                return _historicBarsRepo;
            }
        }

        public BTreeDictionary<MonoWaveKey, MonoWaveGroup> MonoWaves
        {
            get
            {
                return _ascendingMonowaves;
            }
        }

        public BTreeDictionary<long, WavePointImportance> WaveImportance
        {
            get
            {
                return _completeWaveImportanceCopy;
            }
        }

        PeriodXTaManager _taManager;

        public PeriodXTaManager StructuralMgr
        {
            get
            {
                return _taManager;
            }
        }

        public void Initialize( fxHistoricBarsRepo historicBarsRepo, BTreeDictionary<long, WavePointImportance> completeWaveImportanceCopy )
        {
            _historicBarsRepo = historicBarsRepo;
            _completeWaveImportanceCopy = completeWaveImportanceCopy;
        }

        //public void BuildMonoWaves( PeriodXTaManager taManager )
        //{
        //    _taManager = taManager;
        //    _monoWaveImportance = _completeWaveImportanceCopy.Where( x => x.Value.WaveImportance == GlobalConstants.DAILYIMPT ).ToPooledList( );

        //    var count           = _monoWaveImportance.Count;

        //    if ( count < 2 )
        //        return;

        //    var m1BeginPt         = _monoWaveImportance[ count - 2 ];
        //    var m1EndPt           = _monoWaveImportance[ count - 1 ];

        //    ref SBar m1BeginBar = ref DatabarsRepo.GetBarByTime( m1BeginPt.Key );
        //    if ( m1BeginBar == SBar.EmptySBar )
        //        return;

        //    ref SBar m1EndBar = ref DatabarsRepo.GetBarByTime( m1EndPt.Key );
        //    if ( m1EndBar == SBar.EmptySBar )
        //        return;

        //    var beginBarTime      = m1BeginBar.LinuxTime;
        //    var endBarTime        = m1EndBar.LinuxTime;

        //    var trend             = m1BeginPt.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.DownTrend : TrendDirection.Uptrend;

        //    var inBtw             = _completeWaveImportanceCopy.Where( x => x.Key >= beginBarTime && x.Key <= endBarTime ).ToPooledList( );

        //    int monowaveIndex     = 0;

        //    var m1                = new MonoWaveGroup( this, m1BeginPt, ref m1BeginBar, m1EndPt, ref m1EndBar, trend, MonoWaveNumber.m1, inBtw, GlobalConstants.DAILYIMPT );

        //    m1.MonowaveIndex = monowaveIndex;

        //    if ( m1 != null )
        //    {
        //        _ascendingMonowaves.Add( m1.Key, m1 );
        //        _barTimeToWave.Add( m1.BeginTime, m1 );

        //        MonoWaveGroup mono = m1;

        //        do
        //        {
        //            var monowave = GetPreviousMonoWave( mono, _monoWaveImportance );

        //            if ( monowave != null )
        //            {
        //                monowaveIndex++;
        //                monowave.MonowaveIndex = monowaveIndex;

        //                _ascendingMonowaves.Add( monowave.Key, monowave );
        //                _barTimeToWave.Add( monowave.BeginTime, monowave );

        //                mono = monowave;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //        while ( true );
        //    }

        //    //MonoWaveGroup.AllWavesImpt          = _ascendingMonowaves;
        //    //MonoWaveGroup.StructureLabelManager = taManager;
        //    //MonoWaveGroup.WaveImportance        = _completeWaveImportanceCopy;
        //}

        public void BuildMonoWaves( PeriodXTaManager taManager )
        {
            _taManager = taManager;
            _monoWaveImportance = _completeWaveImportanceCopy.Where( x => x.Value.WaveImportance >= GlobalConstants.HRS08IMPT ).ToPooledList( );

            var count = _monoWaveImportance.Count;

            if ( count < 2 )
                return;

            var m1BeginPt         = _monoWaveImportance[ 0 ];
            var m1EndPt           = _monoWaveImportance[ 1 ];

            ref SBar m1BeginBar = ref DatabarsRepo.GetBarByTime( m1BeginPt.Key );
            if ( m1BeginBar == SBar.EmptySBar )
                return;

            ref SBar m1EndBar = ref DatabarsRepo.GetBarByTime( m1EndPt.Key );
            if ( m1EndBar == SBar.EmptySBar )
                return;

            var beginBarTime = m1BeginBar.LinuxTime;
            var endBarTime = m1EndBar.LinuxTime;
            

            var trend             = m1BeginPt.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.DownTrend : TrendDirection.Uptrend;

            var inBtw             = _completeWaveImportanceCopy.Where( x => x.Key >= beginBarTime && x.Key <= endBarTime ).ToPooledList( );

            int monowaveIndex     = 0;

            var m1                = new MonoWaveGroup( this, m1BeginPt, ref m1BeginBar, m1EndPt, ref m1EndBar, trend, MonoWaveNumber.m1, inBtw, GlobalConstants.DAILYIMPT );

            m1.MonowaveIndex = monowaveIndex;

            if ( m1 != null )
            {
                _ascendingMonowaves.Add( m1.Key, m1 );
                _barTimeToWave.Add( m1.BeginTime, m1 );

                MonoWaveGroup mono = m1;

                do
                {
                    var monowave = GetNextMonoWave( mono, _monoWaveImportance );

                    if ( monowave != null )
                    {
                        monowaveIndex++;
                        monowave.MonowaveIndex = monowaveIndex;

                        _ascendingMonowaves.Add( monowave.Key, monowave );
                        _barTimeToWave.Add( monowave.BeginTime, monowave );

                        mono = monowave;
                    }
                    else
                    {
                        break;
                    }
                }
                while ( true );
            }

            //MonoWaveGroup.AllWavesImpt          = _ascendingMonowaves;
            //MonoWaveGroup.StructureLabelManager = taManager;
            //MonoWaveGroup.WaveImportance        = _completeWaveImportanceCopy;
        }

        public MonoWaveGroup GetNextMonoWave( MonoWaveGroup currentMonoWave, PooledList<KeyValuePair<long, WavePointImportance>> allWaves )
        {
            MonoWaveGroup nextWave = null;

            if ( currentMonoWave == null )
                return nextWave;

            int currentMonoWaveEndIndex = allWaves.IndexOf( currentMonoWave.EndPt );

            if ( currentMonoWaveEndIndex == -1 )
                return nextWave;

            var currentMonoWaveEndTime = allWaves[ currentMonoWaveEndIndex ].Key;

            ref SBar currentMonoWaveBar = ref DatabarsRepo.GetBarByTime( currentMonoWaveEndTime );
            if ( currentMonoWaveBar == SBar.EmptySBar )
                return nextWave;            

            int nextMonoWaveIndex      = currentMonoWaveEndIndex + 1;

            if ( nextMonoWaveIndex >= allWaves.Count )
            {
                return nextWave;
            }

            var nextPt     = allWaves[ nextMonoWaveIndex ];

            if ( currentMonoWave.Direction == TrendDirection.Uptrend )
            {
                var lowestBar  = currentMonoWaveBar;
                KeyValuePair< long, WavePointImportance > lowestPt = nextPt;

                do
                {
                    nextPt = allWaves[ nextMonoWaveIndex ];

                    ref SBar nextBar = ref DatabarsRepo.GetBarByTime( nextPt.Key );
                    if ( nextBar == SBar.EmptySBar )
                        return nextWave;
                                    

                    if ( nextBar.Low < lowestBar.Low )
                    {
                        lowestBar = nextBar;
                        lowestPt = nextPt;
                    }

                    var broken = currentMonoWave.BrokenBy( ref nextBar );

                    if ( broken == BrokenLevel.BrokenDown )
                    {
                        //![](2CB8B296D0B590579985E52DF5BCF767.png;;;0.02239,0.01814)
                        // If we are broken Downward, the next immediate point is Next Monowave.

                        if ( lowestBar != currentMonoWaveBar )
                        {
                            var currentTime = currentMonoWaveBar.LinuxTime;
                            var nextBarTime = nextBar.LinuxTime;
                            var inBtw      = _completeWaveImportanceCopy.Where( x => x.Key >= currentTime && x.Key <= nextBarTime ).ToPooledList( );

                            MonoWaveNumber nextWaveNo = MonoWaveNumber.NA;

                            if ( currentMonoWave.WaveNumber != MonoWaveNumber.m8_ && currentMonoWave.WaveNumber != MonoWaveNumber.NA )
                            {
                                nextWaveNo = currentMonoWave.WaveNumber.Next( );
                            }

                            nextWave = new MonoWaveGroup( this, currentMonoWave.EndPt, ref currentMonoWaveBar, nextPt, ref nextBar, TrendDirection.DownTrend, nextWaveNo, inBtw, currentMonoWave.WaveImportanceNumber );

                            return nextWave;
                        }

                    }
                    else if ( broken == BrokenLevel.BrokenUp )
                    {

                        //![](749931275DC3166CCCE5A6096645B2D9.png;;;0.03107,0.02689)
                        //

                        if ( lowestBar != currentMonoWaveBar )
                        {
                            var currentTime = currentMonoWaveBar.LinuxTime;
                            var lowestBarTime = lowestBar.LinuxTime;

                            var inBtw      = _completeWaveImportanceCopy.Where( x => x.Key >= currentTime && x.Key <= lowestBarTime ).ToPooledList( );

                            MonoWaveNumber nextWaveNo = MonoWaveNumber.NA;

                            if ( currentMonoWave.WaveNumber != MonoWaveNumber.m8_ && currentMonoWave.WaveNumber != MonoWaveNumber.NA )
                            {
                                nextWaveNo = currentMonoWave.WaveNumber.Next( );
                            }

                            nextWave = new MonoWaveGroup( this, currentMonoWave.EndPt, ref currentMonoWaveBar, lowestPt, ref lowestBar, TrendDirection.DownTrend, nextWaveNo, inBtw, currentMonoWave.WaveImportanceNumber );

                            return nextWave;
                        }
                    }

                    nextMonoWaveIndex++;

                } while ( nextMonoWaveIndex < allWaves.Count );
            }
            else if ( currentMonoWave.Direction == TrendDirection.DownTrend )
            {
                var highestBar = currentMonoWaveBar;
                KeyValuePair< long, WavePointImportance > highestPt = nextPt;

                do
                {
                    nextPt = allWaves[ nextMonoWaveIndex ];

                    ref SBar nextBar = ref DatabarsRepo.GetBarByTime( nextPt.Key );
                    if ( nextBar == SBar.EmptySBar )
                        return nextWave;
                    
                    if ( nextBar.High > highestBar.High )
                    {
                        highestBar = nextBar;
                        highestPt = nextPt;
                    }

                    var broken = currentMonoWave.BrokenBy( ref nextBar );

                    if ( broken == BrokenLevel.BrokenDown )
                    {
                        //![](FD384EC9D05C7FA132C8C58C516B6734.png;;;0.02448,0.02509)
                        if ( highestBar != currentMonoWaveBar )
                        {
                            var currentTime = currentMonoWaveBar.LinuxTime;
                            var highBarTime = highestBar.LinuxTime;

                            var inBtw      = _completeWaveImportanceCopy.Where( x => x.Key >= currentTime && x.Key <= highBarTime ).ToPooledList( );

                            MonoWaveNumber nextWaveNo = MonoWaveNumber.NA;

                            if ( currentMonoWave.WaveNumber != MonoWaveNumber.m8_ && currentMonoWave.WaveNumber != MonoWaveNumber.NA )
                            {
                                nextWaveNo = currentMonoWave.WaveNumber.Next( );
                            }

                            nextWave = new MonoWaveGroup( this, currentMonoWave.EndPt, ref currentMonoWaveBar, highestPt, ref highestBar, TrendDirection.Uptrend, nextWaveNo, inBtw, currentMonoWave.WaveImportanceNumber );

                            return nextWave;
                        }

                    }
                    else if ( broken == BrokenLevel.BrokenUp )
                    {
                        // If we are broken upward, the next immediate point is Next Monowave.
                        //![](C144FD46C4847B4873121B5AD572E090.png;;;0.02805,0.02827)
                        //

                        if ( highestBar != currentMonoWaveBar )
                        {
                            var currentTime = currentMonoWaveBar.LinuxTime;
                            var nextBarTime = nextBar.LinuxTime;

                            var inBtw      = _completeWaveImportanceCopy.Where( x => x.Key >= currentTime && x.Key <= nextBarTime ).ToPooledList( );

                            MonoWaveNumber nextWaveNo = MonoWaveNumber.NA;

                            if ( currentMonoWave.WaveNumber != MonoWaveNumber.m8_ && currentMonoWave.WaveNumber != MonoWaveNumber.NA )
                            {
                                nextWaveNo = currentMonoWave.WaveNumber.Next( );
                            }

                            nextWave = new MonoWaveGroup( this, currentMonoWave.EndPt, ref currentMonoWaveBar, nextPt, ref nextBar, TrendDirection.Uptrend, nextWaveNo, inBtw, currentMonoWave.WaveImportanceNumber );

                            return nextWave;
                        }
                    }

                    nextMonoWaveIndex++;

                } while ( nextMonoWaveIndex < allWaves.Count );

                //If we are instead broken downward, m2 is the beginIndex to the highest point.
            }


            return nextWave;
        }

        

        public MonoWaveGroup GetPreviousMonoWave( MonoWaveGroup currentMonoWave, PooledList<KeyValuePair<long, WavePointImportance>> allWaves )
        {
            if ( currentMonoWave == null )
                return null;

            MonoWaveGroup previousWave = null;

            int currentMonoWaveBeginIndex = allWaves.IndexOf( currentMonoWave.BeginPt );

            if ( currentMonoWaveBeginIndex == -1 )
                return previousWave;

            var currentMonoWaveBeginTime = allWaves[ currentMonoWaveBeginIndex ].Key;

            ref SBar currentMonoWaveBeginBar = ref DatabarsRepo.GetBarByTime( currentMonoWaveBeginTime );
            if ( currentMonoWaveBeginBar == SBar.EmptySBar )
                return null;            

            int previousMonoWaveIndex  = currentMonoWaveBeginIndex - 1;

            if ( previousMonoWaveIndex < 0 )
            {
                return previousWave;
            }

            var previousPt     = allWaves[ previousMonoWaveIndex ];

            if ( currentMonoWave.Direction == TrendDirection.Uptrend )
            {
                var highestBar  = currentMonoWaveBeginBar;
                KeyValuePair< long, WavePointImportance > highestPt = previousPt;

                do
                {
                    previousPt = allWaves[ previousMonoWaveIndex ];

                    ref SBar previousBar = ref DatabarsRepo.GetBarByTime( previousPt.Key);
                    if ( previousBar == SBar.EmptySBar )
                        return null;                    

                    if ( previousBar.High > highestBar.High )
                    {
                        highestBar = previousBar;
                        highestPt = previousPt;
                    }

                    var broken = currentMonoWave.BrokenBy( ref previousBar );

                    if ( broken == BrokenLevel.BrokenDown )
                    {
                        //![](1C55B8EE2A3A11B8E9303D30A1AFC0D6.png;;;0.02629,0.02402)
                        // If we are broken Downward, the highest point to begin of m1 is the previous MonoWaveGroup

                        if ( highestBar != currentMonoWaveBeginBar )
                        {
                            var currentTime = currentMonoWaveBeginBar.LinuxTime;
                            var highBarTime = highestBar.LinuxTime;

                            var inBtw      = _completeWaveImportanceCopy.Where( x => x.Key >= highBarTime && x.Key <= currentTime ).ToPooledList( );

                            MonoWaveNumber preWave = MonoWaveNumber.NA;

                            if ( currentMonoWave.WaveNumber != MonoWaveNumber.m8_ && currentMonoWave.WaveNumber != MonoWaveNumber.NA )
                            {
                                preWave = currentMonoWave.WaveNumber.Previous( );
                            }

                            previousWave = new MonoWaveGroup( this, highestPt, ref highestBar, currentMonoWave.BeginPt, ref  currentMonoWaveBeginBar, TrendDirection.DownTrend, preWave, inBtw, currentMonoWave.WaveImportanceNumber );

                            return previousWave;
                        }

                    }
                    else if ( broken == BrokenLevel.BrokenUp )
                    {

                        //![](91FD7D2EF202AC69023285FEF97F8CFD.png;;;0.02676,0.02383)
                        //

                        if ( highestBar != currentMonoWaveBeginBar )
                        {
                            var currentTime = currentMonoWaveBeginBar.LinuxTime;
                            var prevBarTime = previousBar.LinuxTime;


                            var inBtw      = _completeWaveImportanceCopy.Where( x => x.Key >= prevBarTime && x.Key <= currentTime ).ToPooledList( );

                            MonoWaveNumber preWave = MonoWaveNumber.NA;

                            if ( currentMonoWave.WaveNumber != MonoWaveNumber.m8_ && currentMonoWave.WaveNumber != MonoWaveNumber.NA )
                            {
                                preWave = currentMonoWave.WaveNumber.Previous( );
                            }

                            previousWave = new MonoWaveGroup( this, previousPt, ref previousBar, currentMonoWave.BeginPt, ref currentMonoWaveBeginBar, TrendDirection.DownTrend, preWave, inBtw, currentMonoWave.WaveImportanceNumber );

                            return previousWave;
                        }
                    }

                    previousMonoWaveIndex--;

                } while ( previousMonoWaveIndex >= 0 );


            }
            else if ( currentMonoWave.Direction == TrendDirection.DownTrend )
            {
                var lowestBar  = currentMonoWaveBeginBar;
                KeyValuePair< long, WavePointImportance > lowestPt = previousPt;

                do
                {
                    previousPt = allWaves[ previousMonoWaveIndex ];

                    ref SBar previousBar = ref DatabarsRepo.GetBarByTime( previousPt.Key);
                    if ( previousBar == SBar.EmptySBar )
                        return null;

                    if ( previousBar.Low < lowestBar.Low )
                    {
                        lowestBar = previousBar;
                        lowestPt = previousPt;
                    }

                    var broken = currentMonoWave.BrokenBy( ref previousBar );

                    if ( broken == BrokenLevel.BrokenDown )
                    {
                        //![](605ED71B461B2838978D09FCE98AE2C8.png;;;0.02703,0.02483)
                        // If we are broken Downward, the next immediate point is the MonoWaveGroup

                        if ( lowestBar != currentMonoWaveBeginBar )
                        {
                            var currentTime = currentMonoWaveBeginBar.LinuxTime;
                            var prevBarTime = previousBar.LinuxTime;

                            var inBtw      = _completeWaveImportanceCopy.Where( x => x.Key >= prevBarTime && x.Key <= currentTime ).ToPooledList( );

                            MonoWaveNumber preWave = MonoWaveNumber.NA;

                            if ( currentMonoWave.WaveNumber != MonoWaveNumber.m8_ && currentMonoWave.WaveNumber != MonoWaveNumber.NA )
                            {
                                preWave = currentMonoWave.WaveNumber.Previous( );
                            }

                            previousWave = new MonoWaveGroup( this, previousPt, ref previousBar, currentMonoWave.BeginPt, ref currentMonoWaveBeginBar, TrendDirection.Uptrend, preWave, inBtw, currentMonoWave.WaveImportanceNumber );

                            return previousWave;
                        }

                    }
                    else if ( broken == BrokenLevel.BrokenUp )
                    {

                        //![](BA2B65696A7445309686D38BE349A3AD.png;;;0.02700,0.01925)
                        //

                        if ( lowestBar != currentMonoWaveBeginBar )
                        {
                            var currentTime = currentMonoWaveBeginBar.LinuxTime;
                            var lowestBarTime = lowestBar.LinuxTime;

                            var inBtw      = _completeWaveImportanceCopy.Where( x => x.Key >= lowestBarTime && x.Key <= currentTime ).ToPooledList( );
                            MonoWaveNumber preWave = MonoWaveNumber.NA;

                            if ( currentMonoWave.WaveNumber != MonoWaveNumber.m8_ && currentMonoWave.WaveNumber != MonoWaveNumber.NA )
                            {
                                preWave = currentMonoWave.WaveNumber.Previous( );
                            }

                            previousWave = new MonoWaveGroup( this, lowestPt, ref lowestBar, currentMonoWave.BeginPt, ref currentMonoWaveBeginBar, TrendDirection.Uptrend, preWave, inBtw, currentMonoWave.WaveImportanceNumber );

                            return previousWave;
                        }
                    }

                    previousMonoWaveIndex--;

                } while ( previousMonoWaveIndex > 0 );

                //If we are instead broken downward, m2 is the beginIndex to the highest point.
            }


            return previousWave;
        }

        public PooledList<MonoLine> GetMonoLines( long barTime )
        {
            PooledList< MonoLine > output = new PooledList< MonoLine >( );
            if ( _barTimeToWave.ContainsKey( barTime ) )
            {
                var m0 = _barTimeToWave[ barTime ];

                if ( m0 != null )
                {
                    output.Add( new MonoLine(  m0.BeginVector, m0.EndVector, m0.BeginEndBarIndex, MonoWaveNumber.m0 ));
                    
                    var m1 = m0.GetNext( );                    

                    if ( m1 != null )
                    {
                        output.Add( new MonoLine( m1.BeginVector, m1.EndVector, m1.BeginEndBarIndex, MonoWaveNumber.m1 ) );

                        var m2 = m1.GetNext( );

                        if ( m2 != null )
                        {
                            output.Add( new MonoLine( m2.BeginVector, m2.EndVector, m2.BeginEndBarIndex, MonoWaveNumber.m2 ) );

                            var m3 = m2.GetNext( );

                            if ( m3 != null )
                            {
                                output.Add( new MonoLine( m3.BeginVector, m3.EndVector, m3.BeginEndBarIndex, MonoWaveNumber.m3 ) );

                                var m4 = m3.GetNext( );

                                if ( m4 != null )
                                {
                                    output.Add( new MonoLine( m4.BeginVector, m4.EndVector, m4.BeginEndBarIndex, MonoWaveNumber.m4 ) );
                                }
                            }
                        }
                    }

                    var m1_ = m0.GetPrevious( );

                    if ( m1_ != null )
                    {
                        output.Add( new MonoLine( m1_.BeginVector, m1_.EndVector, m1_.BeginEndBarIndex, MonoWaveNumber.m1_ ) );

                        var m2_ = m1_.GetPrevious( );

                        if ( m2_ != null )
                        {
                            output.Add( new MonoLine( m2_.BeginVector, m2_.EndVector, m2_.BeginEndBarIndex, MonoWaveNumber.m2_ ) );

                            var m3_ = m2_.GetPrevious( );

                            if ( m3_ != null )
                            {
                                output.Add( new MonoLine( m3_.BeginVector, m3_.EndVector, m3_.BeginEndBarIndex, MonoWaveNumber.m3_ ) );

                                var m4_ = m3_.GetPrevious( );

                                if ( m4_ != null )
                                {
                                    output.Add( new MonoLine( m4_.BeginVector, m4_.EndVector, m4_.BeginEndBarIndex, MonoWaveNumber.m4_ ) );                                    
                                }
                            }
                        }
                    }
                }


            }

            return output;
        }

        public void NewAnalyzeMonoWaves( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var peakTroughCounts = _ascendingMonowaves.Count();

            if ( peakTroughCounts >= 7 )
            {
                var m1Begin = 0;
                do
                {
                    MonoWaveGroup m3_  = _ascendingMonowaves.At( m1Begin + 0 ).Value;
                    MonoWaveGroup m2_  = _ascendingMonowaves.At( m1Begin + 1 ).Value;
                    MonoWaveGroup m1_  = _ascendingMonowaves.At( m1Begin + 2 ).Value;
                    MonoWaveGroup m0   = _ascendingMonowaves.At( m1Begin + 3 ).Value;
                    MonoWaveGroup m1   = _ascendingMonowaves.At( m1Begin + 4 ).Value;
                    MonoWaveGroup m2   = _ascendingMonowaves.At( m1Begin + 5 ).Value;
                    MonoWaveGroup m3   = _ascendingMonowaves.At( m1Begin + 6 ).Value;

                    double m2Retrace = m2.Over( m1 );

                    if( m2Retrace != 0 )
                    {
                        Process7Rules( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3, m2Retrace );
                    }

                    m1Begin++;
                }
                while ( m1Begin < peakTroughCounts - 7 );
            }

            
        }
    }
}
