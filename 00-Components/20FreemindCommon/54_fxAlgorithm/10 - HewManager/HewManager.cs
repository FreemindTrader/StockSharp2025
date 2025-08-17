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


using fx.Definitions.Collections;
using StockSharp.BusinessEntities;
using fx.Collections;
using fx.Bars;
using Ecng.Logging;

namespace fx.Algorithm
{
    public partial class HewManager : BaseLogReceiver, IHewManager
    {
        public bool DoneLoading { get; set; }
        private IForexDatabarsUnitOfWork                                       _unitOfWork;
        private TimeSpan                                                       _currentActiveTimeFrame;
        private fxHistoricBarsRepo                                              _bars;

        private IUnitOfWorkFactory< IForexDatabarsUnitOfWork >                 _unitOfWorkFactory            = null;
        private UndoRedoBTreeDictionary< long, DbElliottWave >                 _hews            = null;
        private BTreeDictionary< long, WavePointImportance >                   _selectedWaveImportanceDict   = null;
        private UndoRedoList< long >                                           _selectedRemovedWavesList     = null;

        private readonly UndoRedoArea                                          _monthlyUndoArea              = new UndoRedoArea( "Monthly" );
        private readonly UndoRedoArea                                          _weeklyUndoArea               = new UndoRedoArea( "Weekly" );
        private readonly UndoRedoArea                                          _dailyUndoArea                = new UndoRedoArea( "Daily" );
        private readonly UndoRedoArea                                          _08HourUndoArea               = new UndoRedoArea( "08Hour" );
        private readonly UndoRedoArea                                          _06HourUndoArea               = new UndoRedoArea( "06Hour" );
        private readonly UndoRedoArea                                          _04HourUndoArea               = new UndoRedoArea( "04Hour" );
        private readonly UndoRedoArea                                          _03HourUndoArea               = new UndoRedoArea( "03Hour" );
        private readonly UndoRedoArea                                          _02HourUndoArea               = new UndoRedoArea( "02Hour" );
        private readonly UndoRedoArea                                          _01HourUndoArea               = new UndoRedoArea( "01Hour" );
        private readonly UndoRedoArea                                          _30minsUndoArea               = new UndoRedoArea( "30Mins" );
        private readonly UndoRedoArea                                          _15minsUndoArea               = new UndoRedoArea( "15Mins" );
        private readonly UndoRedoArea                                          _05minsUndoArea               = new UndoRedoArea( "05Mins" );
        private readonly UndoRedoArea                                          _04minsUndoArea               = new UndoRedoArea( "04Mins" );
        private readonly UndoRedoArea                                          _01minsUndoArea               = new UndoRedoArea( "01Mins" );
        private readonly UndoRedoArea                                          _01SecondUndoArea             = new UndoRedoArea( "ticks" );

        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _monthlyEWaveDescDictionary   = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _weeklyEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _dailyEWaveDescDictionary     = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _01HourEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _02HourEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _03HourEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _04HourEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _06HourEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _08HourEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _30minsEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _15minsEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _05minsEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _04minsEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _01minsEWaveDescDictionary    = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.
        private readonly UndoRedoBTreeDictionary< long, DbElliottWave >        _01SecondEWaveDescDictionary  = new UndoRedoBTreeDictionary< long, DbElliottWave >( new ReverseComparer( ) ); // Here I want to keep track of the wave count in a reverse order.

        private readonly UndoRedoList< long >                                  _monthlyRemovedWaves          = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _weeklyRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _dailyRemovedWaves            = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _08HourRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _06HourRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _04HourRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _03HourRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _02HourRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _01HourRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _30minsRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _15minsRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _05minsRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _04minsRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _01minsRemovedWaves           = new UndoRedoList< long >( );
        private readonly UndoRedoList< long >                                  _01SecondRemovedWaves         = new UndoRedoList< long >( );

        private BTreeDictionary< long, WavePointImportance >                   _01SecElliottWave             = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _01minElliottWave             = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _04minElliottWave             = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _05minElliottWave             = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _15minElliottWave             = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _30minElliottWave             = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _01HourElliottWave            = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _02HourElliottWave            = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _03HourElliottWave            = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _04HourElliottWave            = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _06HourElliottWave            = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _08HourElliottWave            = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _dailyElliottWave             = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _weeklyElliottWave            = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _monthlyElliottWave           = new BTreeDictionary< long, WavePointImportance >( );

        private BTreeDictionary< long, WavePointImportance >                   _01SecGannSwing               = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _01MinGannSwing               = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _04MinGannSwing               = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _05MinGannSwing               = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _15MinGannSwing               = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _30MinGannSwing               = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _01HourMinGannSwing           = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _02HourMinGannSwing           = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _03HourMinGannSwing           = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _04HourMinGannSwing           = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _06HourMinGannSwing           = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _08HourMinGannSwing           = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _dailyGannSwing               = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _weeklyGannSwing              = new BTreeDictionary< long, WavePointImportance >( );
        private BTreeDictionary< long, WavePointImportance >                   _monthlyGannSwing             = new BTreeDictionary< long, WavePointImportance >( );

        //private BTreeDictionary< long, HewFibTargets >                         _waveFibTargets               = new BTreeDictionary< long, HewFibTargets >( );

        private bool                                                           _smartWaveLabeling            = true;
        private object                                                         _objLock                      = new object( );
        private object                                                         _gannLock                     = new object( );
        private object                                                         _zigZagLock                   = new object( );
        private bool                                                           _doneLoadingDatabase          = false;

        private IRepository< DbElliottWave, long >                             _dbElliottWaveRepo;
        private IRepository< DbSmartWaveCycles, long >                         _dbSmartWaveCycles;
        private int                                                            _offerId;

        private Security _security;



        public HewManager()
        {
            _unitOfWorkFactory = UnitOfWorkSource.GetUnitOfWorkFactory();

            _unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            _dbElliottWaveRepo = _unitOfWork.ELLIOTTWAVES;

            _dbSmartWaveCycles = _unitOfWork.SMARTWAVECYCLES;
        }


        public List<long> GetWaveDatesList( TimeSpan period )
        {
            var hews = GetElliottWavesDictionary( period );

            return hews.Keys.ToList();
        }

        public int GetWaveCount( TimeSpan period )
        {
            var hews = GetElliottWavesDictionary( period );

            return hews.Count;
        }

        private class ReverseComparer : IComparer<long>
        {
            public int Compare( long x, long y )
            {
                return y.CompareTo( x );
            }
        }

        #region Properties

        public BTreeDictionary<long, WavePointImportance> Submicro01MinWaves
        {
            get
            {
                return _01MinGannSwing;
            }
            set
            {
                _01MinGannSwing = value;
            }
        }

        public BTreeDictionary<long, WavePointImportance> Micro04MinWaves
        {
            get
            {
                return _04MinGannSwing;
            }
            set
            {
                _04MinGannSwing = value;
            }
        }

        public BTreeDictionary<long, WavePointImportance> Micro05MinWaves
        {
            get
            {
                return _05MinGannSwing;
            }
            set
            {
                _05MinGannSwing = value;
            }
        }

        public BTreeDictionary<long, WavePointImportance> Subminuette15MinWaves
        {
            get
            {
                return _15MinGannSwing;
            }
            set
            {
                _15MinGannSwing = value;
            }
        }

        public BTreeDictionary<long, WavePointImportance> Minuette60MinWaves
        {
            get
            {
                return _01HourMinGannSwing;
            }
            set
            {
                _01HourMinGannSwing = value;
            }
        }

        public BTreeDictionary<long, WavePointImportance> MinuteDailyWaves
        {
            get
            {
                return _dailyGannSwing;
            }
            set
            {
                _dailyGannSwing = value;
            }
        }

        public BTreeDictionary<long, WavePointImportance> MinorWeeklyWaves
        {
            get
            {
                return _weeklyGannSwing;
            }
            set
            {
                _weeklyGannSwing = value;
            }
        }

        public BTreeDictionary<long, WavePointImportance> IntermediateMonthlyWaves
        {
            get
            {
                return _monthlyGannSwing;
            }
            set
            {
                _monthlyGannSwing = value;
            }
        }

        #endregion

        public void SwitchTimeFrame( IMutltiTimeFrameSessionDataRepo repo, TimeSpan responsibleTimeFrame )
        {
            _offerId = SymbolsMgr.Instance.GetOfferId( repo.Security );

            _security = repo.Security;

            _currentActiveTimeFrame = responsibleTimeFrame;

            _hews = GetElliottWavesDictionary( _currentActiveTimeFrame );

            _selectedRemovedWavesList = GetSelectedRemovedWavesList( _currentActiveTimeFrame );

            _bars = GetDatabarsRepository( _currentActiveTimeFrame );

            _selectedWaveImportanceDict = GetWaveImportanceDictionary( _currentActiveTimeFrame );
        }

        #region Wave Related Functions

        public void ReplaceWaveInManagerAndBar( int waveScenarioNo,
                                                TimeSpan responsibleForWhatTimeFrame,
                                                long selectedBarTime,
                                                ElliottWaveCycle cycle,
                                                ElliottWaveEnum oldElliottWave,
                                                ElliottWaveEnum newElliottWave,
                                                ref SBar bar )
        {
            if ( _hews.ContainsKey( selectedBarTime ) )
            {
                var dbHew = _hews[ selectedBarTime ];

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                hew.ReplaceWave( cycle, oldElliottWave, newElliottWave );

                bar.ReplaceWave( waveScenarioNo, ref hew );

                //_hews[ selectedBarTime ] = dbHew;

                //ReplaceWaveDownSmallerTimeFrame( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime.FromLinuxTime(), cycle, oldElliottWave, newElliottWave );

                //ReplaceWaveUpHigherTimeFrame( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime.FromLinuxTime(), cycle, oldElliottWave, newElliottWave );
            }
        }

        public bool ReplaceWaveUpHigherTimeFrame( int waveScenarioNo,
                                                    TimeSpan currentTimeFrame,
                                                    DateTime barTime,
                                                    ElliottWaveCycle cycle,
                                                    ElliottWaveEnum oldElliottWave,
                                                    ElliottWaveEnum newElliottWave )
        {
            TimeSpan higherTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                higherTimeSpan = aa.GetOneTimeSpanHigher( currentTimeFrame );
            }
            else
            {
                return false;
            }

            if ( higherTimeSpan != TimeSpan.Zero )
            {
                var bars = GetDatabarsRepository( higherTimeSpan );

                ref SBar bar = ref bars.GetBarContainingTime( barTime, higherTimeSpan );

                if ( bar != SBar.EmptySBar )
                {
                    var hews = GetElliottWavesDictionary( higherTimeSpan );

                    if ( hews.ContainsKey( bar.LinuxTime ) )
                    {
                        ref var hew = ref hews[ bar.LinuxTime ].GetWaveFromScenario( waveScenarioNo );

                        if ( hew.ReplaceWave( cycle, oldElliottWave, newElliottWave ) )
                        {
                            bar.ReplaceWave( waveScenarioNo, ref hew );

                            ReplaceWaveUpHigherTimeFrame( waveScenarioNo, higherTimeSpan, bar.BarTime, cycle, oldElliottWave, newElliottWave );

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private fxHistoricBarsRepo GetDatabarsRepository( TimeSpan period )
        {
            return SymbolsMgr.Instance.GetDatabarRepo( _security, period );
        }

        public bool ReplaceWaveDownSmallerTimeFrame( int waveScenarioNo, TimeSpan currentTimeFrame,
                                                        DateTime barTime,
                                                        ElliottWaveCycle cycle,
                                                        ElliottWaveEnum oldElliottWave,
                                                        ElliottWaveEnum newElliottWave )
        {
            TimeSpan lowerTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                lowerTimeSpan = aa.GetOneTimeSpanLower( currentTimeFrame );
            }
            else
            {
                return false;
            }

            if ( lowerTimeSpan != TimeSpan.Zero )
            {
                var hews = GetElliottWavesDictionary( lowerTimeSpan );
                var bars = GetDatabarsRepository( lowerTimeSpan );

                long begin = barTime.ToLinuxTime( );

                long end = ( barTime + currentTimeFrame ).ToLinuxTime( );

                if ( hews.Count > 0 )
                {
                    var previousWaves = hews.GetElementsRightOf( end );

                    foreach ( var wave in previousWaves )
                    {
                        if ( wave.Key >= begin )
                        {
                            ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                            if ( hew.ReplaceWave( cycle, oldElliottWave, newElliottWave ) )
                            {
                                ref SBar bar = ref bars.GetBarByTime( wave.Key );

                                if ( bar != SBar.EmptySBar )
                                {
                                    bar.ReplaceWave( waveScenarioNo, ref hew );
                                }

                                ReplaceWaveDownSmallerTimeFrame( waveScenarioNo, lowerTimeSpan, wave.Key.FromLinuxTime(), cycle, oldElliottWave, newElliottWave );

                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool RestoreWaveToManager( string symbol )
        {
            if ( !_doneLoadingDatabase )
            {
                int  offerId = SymbolsMgr.Instance.GetOfferId( symbol );
                bool lockTaken = false;

                try
                {
                    Monitor.TryEnter( _objLock, 5, ref lockTaken );

                    if ( lockTaken )
                    {
                        var resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.Period == "m1" ).OrderByDescending( x => x.StartDate );

                        if ( resultSet != null )
                        {
                            foreach ( DbElliottWave dbElliottWave in resultSet )
                            {
                                if ( !_01minsEWaveDescDictionary.ContainsKey( dbElliottWave.StartDate ) )
                                {
                                    _01minsEWaveDescDictionary.Add( dbElliottWave.StartDate, dbElliottWave );
                                }
                            }
                        }

                        resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.Period == "m5" ).OrderByDescending( x => x.StartDate );

                        if ( resultSet != null )
                        {
                            foreach ( DbElliottWave dbElliottWave in resultSet )
                            {
                                if ( !_05minsEWaveDescDictionary.ContainsKey( dbElliottWave.StartDate ) )
                                {
                                    _05minsEWaveDescDictionary.Add( dbElliottWave.StartDate, dbElliottWave );
                                }
                            }
                        }

                        resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.Period == "m4" ).OrderByDescending( x => x.StartDate );

                        if ( resultSet != null )
                        {
                            foreach ( DbElliottWave dbElliottWave in resultSet )
                            {
                                if ( !_04minsEWaveDescDictionary.ContainsKey( dbElliottWave.StartDate ) )
                                {
                                    _04minsEWaveDescDictionary.Add( dbElliottWave.StartDate, dbElliottWave );
                                }
                            }
                        }

                        resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.Period == "m15" ).OrderByDescending( x => x.StartDate );

                        if ( resultSet != null )
                        {
                            foreach ( DbElliottWave dbElliottWave in resultSet )
                            {
                                if ( !_15minsEWaveDescDictionary.ContainsKey( dbElliottWave.StartDate ) )
                                {
                                    _15minsEWaveDescDictionary.Add( dbElliottWave.StartDate, dbElliottWave );
                                }
                            }
                        }

                        resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.Period == "H1" ).OrderByDescending( x => x.StartDate );

                        if ( resultSet != null )
                        {
                            foreach ( DbElliottWave dbElliottWave in resultSet )
                            {
                                if ( !_01HourEWaveDescDictionary.ContainsKey( dbElliottWave.StartDate ) )
                                {
                                    _01HourEWaveDescDictionary.Add( dbElliottWave.StartDate, dbElliottWave );
                                }
                            }
                        }

                        resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.Period == "D1" ).OrderByDescending( x => x.StartDate );

                        if ( resultSet != null )
                        {
                            foreach ( DbElliottWave dbElliottWave in resultSet )
                            {
                                if ( !_dailyEWaveDescDictionary.ContainsKey( dbElliottWave.StartDate ) )
                                {
                                    _dailyEWaveDescDictionary.Add( dbElliottWave.StartDate, dbElliottWave );
                                }
                            }
                        }

                        resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.Period == "W1" ).OrderByDescending( x => x.StartDate );

                        if ( resultSet != null )
                        {
                            foreach ( DbElliottWave dbElliottWave in resultSet )
                            {
                                if ( !_weeklyEWaveDescDictionary.ContainsKey( dbElliottWave.StartDate ) )
                                {
                                    _weeklyEWaveDescDictionary.Add( dbElliottWave.StartDate, dbElliottWave );
                                }
                            }
                        }

                        resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.Period == "M1" ).OrderByDescending( x => x.StartDate );

                        if ( resultSet != null )
                        {
                            foreach ( DbElliottWave dbElliottWave in resultSet )
                            {
                                if ( !_monthlyEWaveDescDictionary.ContainsKey( dbElliottWave.StartDate ) )
                                {
                                    _monthlyEWaveDescDictionary.Add( dbElliottWave.StartDate, dbElliottWave );
                                }
                            }
                        }

                        _doneLoadingDatabase = true;

                        return true;
                    }
                }
                finally
                {
                    if ( lockTaken )
                    {
                        Monitor.Exit( _objLock );
                    }
                }
            }

            return false;
        }

        public UndoRedoBTreeDictionary<long, DbElliottWave> GetElliottWavesDictionary( TimeSpan responsibleForWhatTimeFrame )
        {
            if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 30 ) )
            {
                return _monthlyEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 7 ) )
            {
                return _weeklyEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 1 ) )
            {
                return _dailyEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 8 ) )
            {
                return _08HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 6 ) )
            {
                return _06HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 4 ) )
            {
                return _04HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 3 ) )
            {
                return _03HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 2 ) )
            {
                return _02HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 1 ) )
            {
                return _01HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 30 ) )
            {
                return _30minsEWaveDescDictionary;
            }

            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 15 ) )
            {
                return _15minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 5 ) )
            {
                return _05minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 4 ) )
            {
                return _04minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 1 ) )
            {
                return _01minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromSeconds( 1 ) )
            {
                return _01SecondEWaveDescDictionary;
            }

            throw new ArgumentException();
        }

        public void ClearWave( TimeSpan responsibleForWhatTimeFrame )
        {
            if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 30 ) )
            {
                _monthlyEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 7 ) )
            {
                _weeklyEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 1 ) )
            {
                _dailyEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 8 ) )
            {
                _08HourEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 6 ) )
            {
                _06HourEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 4 ) )
            {
                _04HourEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 3 ) )
            {
                _03HourEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 2 ) )
            {
                _02HourEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 1 ) )
            {
                _01HourEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 30 ) )
            {
                _30minsEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 15 ) )
            {
                _15minsEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 5 ) )
            {
                _05minsEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 4 ) )
            {
                _04minsEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 1 ) )
            {
                _01minsEWaveDescDictionary.Clear();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromSeconds( 1 ) )
            {
                _01SecondEWaveDescDictionary.Clear();
            }
        }

        public UndoRedoList<long> GetSelectedRemovedWavesList( TimeSpan responsibleForWhatTimeFrame )
        {
            if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 30 ) )
            {
                return _monthlyRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 7 ) )
            {
                return _weeklyRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 1 ) )
            {
                return _dailyRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 8 ) )
            {
                return _08HourRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 6 ) )
            {
                return _06HourRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 4 ) )
            {
                return _04HourRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 3 ) )
            {
                return _03HourRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 2 ) )
            {
                return _02HourRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 1 ) )
            {
                return _01HourRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 30 ) )
            {
                return _30minsRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 15 ) )
            {
                return _15minsRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 5 ) )
            {
                return _05minsRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 4 ) )
            {
                return _04minsRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 1 ) )
            {
                return _01minsRemovedWaves;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromSeconds( 1 ) )
            {
                return _01SecondRemovedWaves;
            }

            throw new ArgumentException();
        }

        public UndoRedoBTreeDictionary<long, DbElliottWave> GetOneTimeSpanLowerDictionary( TimeSpan responsibleForWhatTimeFrame )
        {
            if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 30 ) )
            {
                return _weeklyEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 7 ) )
            {
                return _dailyEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 1 ) )
            {
                return _08HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 8 ) )
            {
                return _06HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 6 ) )
            {
                return _04HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 4 ) )
            {
                return _03HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 3 ) )
            {
                return _02HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 2 ) )
            {
                return _01HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 1 ) )
            {
                return _30minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 30 ) )
            {
                return _15minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 15 ) )
            {
                return _05minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 5 ) )
            {
                return _04minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 4 ) )
            {
                return _01minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 1 ) )
            {
                return _01SecondEWaveDescDictionary;
            }

            throw new ArgumentException();
        }

        public int GetOneWaveImportanceLower( int waveImportance )
        {
            if ( waveImportance == 89 )
            {
                return 55;
            }
            else if ( waveImportance == 55 )
            {
                return 34;
            }
            else if ( waveImportance == 34 )
            {
                return 21;
            }
            else if ( waveImportance == 21 )
            {
                return 13;
            }
            else if ( waveImportance == 13 )
            {
                return 8;
            }
            else if ( waveImportance == 8 )
            {
                return 5;
            }
            else if ( waveImportance == 5 )
            {
                return -1;
            }

            return -1;
        }

        public int GetOneWaveImportanceHigher( int waveImportance )
        {
            if ( waveImportance == 89 )
            {
                return -1;
            }
            else if ( waveImportance == 55 )
            {
                return 89;
            }
            else if ( waveImportance == 34 )
            {
                return 55;
            }
            else if ( waveImportance == 21 )
            {
                return 34;
            }
            else if ( waveImportance == 13 )
            {
                return 21;
            }
            else if ( waveImportance == 8 )
            {
                return 13;
            }
            else if ( waveImportance == 5 )
            {
                return 8;
            }

            return -1;
        }

        public UndoRedoBTreeDictionary<long, DbElliottWave> GetOneTimeSpanHigherDictionary( TimeSpan responsibleForWhatTimeFrame )
        {
            if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 30 ) )
            {
                throw new ArgumentException();
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 7 ) )
            {
                return _monthlyEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 1 ) )
            {
                return _weeklyEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 8 ) )
            {
                return _dailyEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 6 ) )
            {
                return _08HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 4 ) )
            {
                return _06HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 3 ) )
            {
                return _04HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 2 ) )
            {
                return _03HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 1 ) )
            {
                return _02HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 30 ) )
            {
                return _01HourEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 15 ) )
            {
                return _30minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 5 ) )
            {
                return _15minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 4 ) )
            {
                return _05minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 1 ) )
            {
                return _04minsEWaveDescDictionary;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromSeconds( 1 ) )
            {
                return _01minsEWaveDescDictionary;
            }

            throw new ArgumentException();
        }

        public WaveLabelPosition GetWaveLabelPositionByTASignal( ref SBar bar )
        {
            WaveLabelPosition output = WaveLabelPosition.UNKNOWN;

            if ( bar.IsWavePeak() )
            {
                output = WaveLabelPosition.TOP;
            }
            else if ( bar.IsWaveTrough() )
            {
                output = WaveLabelPosition.BOTTOM;
            }

            return output;
        }

        protected WaveLabelPosition InternalGetCurrentWaveLabelPosition( TimeSpan responsibleForWhatTimeFrame,
                                                                         long rawBarTime,
                                                                         ElliottWaveCycle cycle,
                                                                         ElliottWaveEnum currentWaveName,
                                                                         ref SBar bar )
        {
            WaveLabelPosition output = WaveLabelPosition.UNKNOWN;

            return output;
        }

        public PooledList<WaveInfo> GetAllWavesDescending( int waveScenarioNo, long rawBarTime )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = _hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                if ( hew.HasElliottWave )
                {
                    var resultingWaves = hew.GetAllWaves( ).OrderByDescending( l => l.WaveCycle ).ThenBy( l => l.WaveName ).ToPooledList();

                    return resultingWaves;
                }
            }

            return null;
        }

        public PooledList<WaveInfo> GetAllWavesDescending( int waveScenarioNo, TimeSpan period, long rawBarTime )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                if ( hew.HasElliottWave )
                {
                    var resultingWaves = hew.GetAllWaves( ).OrderByDescending( l => l.WaveCycle ).ThenBy( l => l.WaveName ).ToPooledList();

                    return resultingWaves;
                }
            }

            return null;
        }

        public int WaveCount( int waveScenarioNo, long rawBarTime )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = _hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                if ( hew.HasElliottWave )
                {
                    return hew.Count;
                }
            }

            return 0;
        }

        public int WaveCount( int waveScenarioNo, TimeSpan period, long rawBarTime )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                if ( hew.HasElliottWave )
                {
                    return hew.Count;
                }
            }

            return 0;
        }

        public ElliottWaveEnum GetWaveOfDegree( int waveScenarioNo, TimeSpan period, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                PooledList< ElliottWaveEnum > lastWave = hew.GetWavesAtCycle( currentWaveDegree );

                if ( lastWave.Count > 0 )
                {
                    return lastWave[ 0 ];
                }
            }

            return ElliottWaveEnum.NONE;
        }

        public ElliottWaveEnum GetWaveOfDegree( int waveScenarioNo, long rawBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = _hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                PooledList< ElliottWaveEnum > lastWave = hew.GetWavesAtCycle( currentWaveDegree );

                if ( lastWave.Count > 0 )
                {
                    return lastWave[ 0 ];
                }
            }

            return ElliottWaveEnum.NONE;
        }

        public WaveInfo? GetWaveOfHigestDegree( int waveScenarioNo, long rawBarTime )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = _hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                var lastWave = hew.GetLastHighestWaveDegree( );

                if ( lastWave.HasValue )
                {
                    return lastWave;
                }
            }

            return null;
        }

        public WaveInfo? GetWaveOfHigestDegree( int waveScenarioNo, TimeSpan period, long rawBarTime )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var outputWave = hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                var lastWave = hew.GetLastHighestWaveDegree( );

                if ( lastWave.HasValue )
                {
                    return lastWave;
                }
            }

            return null;
        }

        public WaveInfo? GetWaveOfLowestDegree( int waveScenarioNo, TimeSpan period, long rawBarTime )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                var lastWave = hew.GetLowestDegreeWaveInfo( );

                if ( lastWave.HasValue )
                {
                    return lastWave;
                }
            }

            return null;
        }

        public WaveInfo? GetWaveOfLowestDegree( int waveScenarioNo, long rawBarTime )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var outputWave = _hews[ rawBarTime ];

                ref var hew = ref outputWave.GetWaveFromScenario( waveScenarioNo );

                var lastWave = hew.GetLowestDegreeWaveInfo( );

                if ( lastWave.HasValue )
                {
                    return lastWave;
                }
            }

            return null;
        }

        public bool IsValidLabeling( long rawBarTime, ElliottWaveCycle currentWaveDegree, ElliottWaveEnum currentWaveName )
        {
            return false;
        }

        // Done: instead of Remove the timeStamp from the database directly, we shall wait till the enduser press the save Button. So we can do undo
        public DbElliottWave DeleteWavesFromManagerAndBar( int waveScenarioNo, long rawBarTime, ref SBar bar, TimeSpan period )
        {
            DbElliottWave dbHew = null;

            if ( _hews.ContainsKey( rawBarTime ) )
            {
                dbHew = _hews[ rawBarTime ];

                bar.RemoveWavesFromDatabar( waveScenarioNo );
                dbHew.SyncWithBar( ref bar );

                if ( _selectedRemovedWavesList != null && ! dbHew.HasWaves() )
                {
                    _hews.Remove( rawBarTime );

                    var index = _selectedRemovedWavesList.FindIndex( x => x.Equals( rawBarTime ) );

                    if ( index == -1 )
                    {
                        _selectedRemovedWavesList.Add( rawBarTime );
                    }

                    Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, false ) );
                }
            }
            else
            {
                if ( bar.HasElliottWave )
                {
                    bar.RemoveWavesFromDatabar( waveScenarioNo );
                }
            }

            return dbHew;
        }

        //public DbElliottWave MarkWaveAsAlternativeWave( long rawBarTime,
        //                                                     ref SBar bar,
        //                                                     TimeSpan period )
        //{
        //    DbElliottWave output = null;

        //    if ( _hews.ContainsKey( rawBarTime ) )
        //    {
        //        output = _hews[ rawBarTime ];

        //        MarkWaveAsAlternativeWaveDownSmallerTimeFrame( period, output );

        //        // MarkWaveAsAlternativeWaveUpLargerTimeFrame( period, output );

        //        output.SwapMainWavesWithAlternative();
        //    }

        //    return output;
        //}

        //public bool MarkWaveAsAlternativeWaveDownSmallerTimeFrame( TimeSpan currentTimeFrame, DbElliottWave wavesToBeChanged )
        //{
        //    TimeSpan lowerTimeSpan = TimeSpan.MinValue;

        //    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //    if ( aa != null )
        //    {
        //        lowerTimeSpan = aa.GetOneTimeSpanLower( currentTimeFrame );
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if ( lowerTimeSpan != TimeSpan.Zero )
        //    {
        //        var hews = GetElliottWavesDictionary( lowerTimeSpan );

        //        var bars = GetDatabarsRepository( lowerTimeSpan );

        //        var listofWaveTime = bars.FindOwningBars( wavesToBeChanged );

        //        foreach ( var waveTime in listofWaveTime )
        //        {
        //            if ( hews.ContainsKey( waveTime ) )
        //            {
        //                var output = hews[ waveTime ];

        //                MarkWaveAsAlternativeWaveDownSmallerTimeFrame( lowerTimeSpan, output );

        //                output.SwapMainWavesWithAlternative();
        //            }
        //        }
        //    }

        //    return false;
        //}

        //public bool MarkWaveAsAlternativeWaveUpLargerTimeFrame( int waveScenarioNo, TimeSpan currentTimeFrame, DbElliottWave wavesToBeChanged )
        //{
        //    TimeSpan htf = TimeSpan.MinValue;

        //    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //    if ( aa != null )
        //    {
        //        htf = aa.GetOneTimeSpanHigher( currentTimeFrame );
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if ( htf != TimeSpan.Zero )
        //    {
        //        var hews = GetElliottWavesDictionary( htf );

        //        var bars = GetDatabarsRepository( htf );

        //        long ltf = wavesToBeChanged.StartDate;

        //        var lowerTimeFrameDT = ltf.FromLinuxTime( );

        //        ref SBar bar = ref bars.GetBarContainingTime( lowerTimeFrameDT, htf);

        //        if ( bar != SBar.EmptySBar )
        //        {
        //            if ( hews.ContainsKey( bar.LinuxTime ) )
        //            {
        //                var output = hews[ bar.LinuxTime ];

        //                MarkWaveAsAlternativeWaveUpLargerTimeFrame( waveScenarioNo, htf, output );

        //                output.SwapMainWavesWithAlternative( wavesToBeChanged.GetWaveFromScenario( waveScenarioNo ) );
        //            }
        //        }
        //    }

        //    return false;
        //}

        public void MarkWaveAsPrimary( int waveScenarioNo, TimeSpan period )
        {
            var hews = GetElliottWavesDictionary( period );
            var bars = GetDatabarsRepository( period );

            foreach ( var eWave in hews )
            {
                var dbHew = eWave.Value;
                dbHew.SwapWaves( bars, 1, waveScenarioNo );

            }
        }

        //public bool DeleteWavesDownSmallerTimeFrame( int waveScenarioNo, TimeSpan currentTimeFrame, DbElliottWave wavesToBeDeleted )
        //{
        //    TimeSpan lowerTimeSpan = TimeSpan.MinValue;

        //    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //    if ( aa != null )
        //    {
        //        lowerTimeSpan = aa.GetOneTimeSpanLower( currentTimeFrame );
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if ( lowerTimeSpan != TimeSpan.Zero )
        //    {
        //        var hews = GetElliottWavesDictionary( lowerTimeSpan );

        //        var removedWaveList = GetSelectedRemovedWavesList( lowerTimeSpan );

        //        var bars = GetDatabarsRepository( lowerTimeSpan );

        //        var listofWaveTime = bars.RemoveWavesFromRange( waveScenarioNo, wavesToBeDeleted );

        //        foreach ( var waveTime in listofWaveTime )
        //        {
        //            hews.Remove( waveTime );

        //            if ( removedWaveList != null )
        //            {
        //                var index = removedWaveList.FindIndex( x => x.Equals( waveTime ) );

        //                if ( index == -1 )
        //                {
        //                    removedWaveList.Add( waveTime );

        //                    // Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, false ) );
        //                }
        //            }
        //        }
        //    }

        //    if ( lowerTimeSpan != TimeSpan.Zero )
        //    {
        //        DeleteWavesDownSmallerTimeFrame( waveScenarioNo, lowerTimeSpan, wavesToBeDeleted );
        //    }

        //    return false;
        //}

        //public bool DeleteWavesDownSmallerTimeFrame( int waveScenarioNo, ref WaveInfo wavesToBeDeleted, DateTime beginTimeUTC, TimeSpan currentTimeFrame )
        //{
        //    TimeSpan lowerTimeSpan = TimeSpan.MinValue;

        //    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //    if ( aa != null )
        //    {
        //        lowerTimeSpan = aa.GetOneTimeSpanLower( currentTimeFrame );
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if ( lowerTimeSpan != TimeSpan.Zero )
        //    {
        //        var hews = GetElliottWavesDictionary( lowerTimeSpan );
        //        var removedWaveList = GetSelectedRemovedWavesList( lowerTimeSpan );
        //        var bars = GetDatabarsRepository( lowerTimeSpan );
        //        var listofWaveTime = bars.RemoveWavesFromRange( waveScenarioNo, ref wavesToBeDeleted, beginTimeUTC, currentTimeFrame );

        //        foreach ( var waveTime in listofWaveTime )
        //        {
        //            var lowerWave = hews[ waveTime ];

        //            ref var newWave = ref lowerWave.GetWaveFromScenario( waveScenarioNo );

        //            newWave.RemoveMatchedWaves( wavesToBeDeleted.ElliottWave );

        //            if ( newWave.Count == 0 )
        //            {
        //                if ( removedWaveList != null )
        //                {
        //                    var index = removedWaveList.FindIndex( x => x.Equals( waveTime ) );

        //                    if ( index == -1 )
        //                    {
        //                        removedWaveList.Add( waveTime );

        //                        // Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, false ) );
        //                    }
        //                }
        //            }

        //            DeleteWavesDownSmallerTimeFrame( waveScenarioNo, ref wavesToBeDeleted, waveTime.FromLinuxTime(), lowerTimeSpan );
        //        }
        //    }

        //    return false;
        //}

        //public bool AddWavesDownSmallerTimeFrame( int waveScenarioNo, int offerId, ref WaveInfo waveToBeAdded, DateTime barTime, TimeSpan currentTimeFrame )
        //{
        //    TimeSpan lowerTimeSpan = TimeSpan.MinValue;

        //    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //    if ( aa != null )
        //    {
        //        lowerTimeSpan = aa.GetOneTimeSpanLower( currentTimeFrame );
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if ( lowerTimeSpan != TimeSpan.Zero )
        //    {
        //        var hews = GetElliottWavesDictionary( lowerTimeSpan );

        //        var bars = GetDatabarsRepository( lowerTimeSpan );

        //        var position = waveToBeAdded.LabelPosition;

        //        if ( position == WaveLabelPosition.TOP ) // we need to find the highest bar for the day
        //        {
        //            ref SBar smallerTfHighestBar = ref bars.GetHighestBarOfTheRange( barTime, currentTimeFrame );

        //            if ( smallerTfHighestBar != SBar.EmptySBar )
        //            {
        //                smallerTfHighestBar.MergeHigherTimeFrameWaves( waveScenarioNo, ref waveToBeAdded );

        //                if ( !hews.ContainsKey( smallerTfHighestBar.LinuxTime ) )
        //                {
        //                    var wave = new DbElliottWave( offerId, bars, ref smallerTfHighestBar );

        //                    hews.Add( smallerTfHighestBar.LinuxTime, wave );
        //                }
        //                else
        //                {
        //                    ref var hew = ref hews[ smallerTfHighestBar.LinuxTime ].GetWaveFromScenario( waveScenarioNo );

        //                    hew.MergeWave( waveToBeAdded );
        //                }

        //                if ( lowerTimeSpan != TimeSpan.Zero )
        //                {
        //                    AddWavesDownSmallerTimeFrame( waveScenarioNo, offerId, ref waveToBeAdded, smallerTfHighestBar.BarTime, lowerTimeSpan );
        //                }
        //            }
        //        }
        //        else if ( position == WaveLabelPosition.BOTTOM ) // we need to find the lowest bar for the day
        //        {
        //            ref SBar smallTfLowestBar = ref bars.GetLowestBarOfTheRange( barTime, currentTimeFrame );

        //            if ( smallTfLowestBar != SBar.EmptySBar )
        //            {
        //                smallTfLowestBar.MergeHigherTimeFrameWaves( waveScenarioNo, ref waveToBeAdded );

        //                if ( !hews.ContainsKey( smallTfLowestBar.LinuxTime ) )
        //                {
        //                    var wave = new DbElliottWave( offerId, bars, ref smallTfLowestBar );

        //                    hews.Add( smallTfLowestBar.LinuxTime, wave );
        //                }
        //                else
        //                {
        //                    ref var hew = ref hews[ smallTfLowestBar.LinuxTime ].GetWaveFromScenario( waveScenarioNo );

        //                    hew.MergeWave( waveToBeAdded );
        //                }

        //                if ( lowerTimeSpan != TimeSpan.Zero )
        //                {
        //                    AddWavesDownSmallerTimeFrame( waveScenarioNo, offerId, ref waveToBeAdded, smallTfLowestBar.BarTime, lowerTimeSpan );
        //                }

        //            }
        //        }
        //    }



        //    return false;
        //}

        //public bool AddWaveUpHigherTimeFrame( int waveScenarioNo, int offerId, ref WaveInfo waveToBeAdded, DateTime barTime, TimeSpan currentTimeFrame )
        //{
        //    TimeSpan htf = TimeSpan.MinValue;

        //    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //    if ( aa != null )
        //    {
        //        htf = aa.GetOneTimeSpanHigher( currentTimeFrame );
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if ( htf != TimeSpan.Zero )
        //    {
        //        var minimum = FinancialHelper.GetMinimumWavesToMerge( htf );

        //        if ( waveToBeAdded.WaveCycle >= minimum )
        //        {
        //            var hews = GetElliottWavesDictionary( htf );

        //            var bars = GetDatabarsRepository( htf );

        //            ref SBar bar = ref bars.GetBarContainingTime( barTime, htf );

        //            if ( bar != SBar.EmptySBar )
        //            {
        //                bar.MergeWave( waveScenarioNo, ref waveToBeAdded );

        //                if ( hews.ContainsKey( bar.LinuxTime ) )
        //                {
        //                    var dbHew = hews[ bar.LinuxTime ];

        //                    ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

        //                    ref var higherHew = ref bar.GetWaveFromScenario( waveScenarioNo );

        //                    hew.CopyFrom( ref higherHew );
        //                }
        //                else
        //                {
        //                    if ( bar.HasElliottWave )
        //                    {
        //                        var higherTFNewWave = new DbElliottWave( offerId, bars, ref bar );

        //                        hews.Add( bar.LinuxTime, higherTFNewWave );
        //                    }
        //                }
        //            }

        //            AddWaveUpHigherTimeFrame( waveScenarioNo, offerId, ref waveToBeAdded, barTime, htf );
        //        }
        //    }

        //    return false;
        //}

        //public bool DeleteWavesUpHigherTimeFrame( int waveScenarioNo, ref WaveInfo wavesToBeDeleted, DateTime beginTimeUTC, TimeSpan currentTimeFrame )
        //{
        //    TimeSpan htf = TimeSpan.MinValue;

        //    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //    if ( aa != null )
        //    {
        //        htf = aa.GetOneTimeSpanHigher( currentTimeFrame );
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if ( htf != TimeSpan.Zero )
        //    {
        //        var hews          = GetElliottWavesDictionary( htf );

        //        var removedWavesList     = GetSelectedRemovedWavesList( htf );

        //        var bars         = GetDatabarsRepository( htf );

        //        ref SBar bar = ref bars.GetBarContainingTime( beginTimeUTC, htf );

        //        if ( bar != SBar.EmptySBar )
        //        {
        //            bar.RemoveMatchedWavesFromDatabar( waveScenarioNo, wavesToBeDeleted.ElliottWave );

        //            ref var hew = ref bar.GetWaveFromScenario( waveScenarioNo );

        //            if ( hew.Count == 0 )
        //            {
        //                var waveTime = bar.BarTime.ToLinuxTime( );
        //                hews.Remove( waveTime );

        //                if ( removedWavesList != null )
        //                {
        //                    var index = removedWavesList.FindIndex( x => x.Equals( waveTime ) );

        //                    if ( index == -1 )
        //                    {
        //                        removedWavesList.Add( waveTime );

        //                        // Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, false ) );
        //                    }
        //                }
        //            }
        //        }


        //        if ( htf != TimeSpan.Zero )
        //        {
        //            DeleteWavesUpHigherTimeFrame( waveScenarioNo, ref wavesToBeDeleted, beginTimeUTC, htf );
        //        }
        //    }

        //    return false;
        //}

        //public bool DeleteWavesUpHigherTimeFrame( int waveScenarioNo, TimeSpan currentTimeFrame, DbElliottWave wavesToBeDeleted )
        //{
        //    TimeSpan htf = TimeSpan.MinValue;

        //    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

        //    if ( aa != null )
        //    {
        //        htf = aa.GetOneTimeSpanHigher( currentTimeFrame );
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if ( htf != TimeSpan.Zero )
        //    {
        //        var hews = GetElliottWavesDictionary( htf );

        //        var removedWavesList = GetSelectedRemovedWavesList( htf );

        //        var bars = GetDatabarsRepository( htf );

        //        ref SBar bar = ref bars.GetBarContainingTime( wavesToBeDeleted.BeginTimeUTC, htf );

        //        if ( bar != SBar.EmptySBar )
        //        {
        //            ref var hew = ref wavesToBeDeleted.GetWaveFromScenario( waveScenarioNo );

        //            bar.RemoveMatchedWavesFromDatabar( waveScenarioNo, hew );

        //            ref var hewHTF = ref bar.GetWaveFromScenario( waveScenarioNo );

        //            if ( hewHTF.Count == 0 )
        //            {
        //                var waveTime = bar.BarTime.ToLinuxTime( );
        //                hews.Remove( waveTime );

        //                if ( removedWavesList != null )
        //                {
        //                    var index = removedWavesList.FindIndex( x => x.Equals( waveTime ) );

        //                    if ( index == -1 )
        //                    {
        //                        removedWavesList.Add( waveTime );

        //                        // Messenger.Default.Send( new ToggleCommandMessage( CommandMessages.HasHewFunctions, false ) );
        //                    }
        //                }
        //            }
        //        }


        //        DeleteWavesUpHigherTimeFrame( waveScenarioNo, htf, wavesToBeDeleted );
        //    }

        //    return false;
        //}

        public void SetWaveLabelPosition( long rawBarTime, WaveLabelPosition waveLabelPositioin )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var db = _hews[ rawBarTime ];

                db.WaveLabelPosition = waveLabelPositioin;

                _hews[ rawBarTime ] = db;
            }
        }

        #endregion

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave024X_WaveA( int waveScenarioNo, long waveBtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveAtime = FindPreviousWaveA( waveScenarioNo, waveBtime, currentWaveDegree );

            if ( -1 == waveAtime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, waveAtime, waveBtime, currentWaveDegree ) )
            {
                var wave024Xtime = FindPreviousWave024X( waveScenarioNo,waveAtime, currentWaveDegree );

                if ( -1 == wave024Xtime )
                {
                    return default;
                }

                if ( NoHigherWaveBetween( waveScenarioNo, wave024Xtime, waveAtime, currentWaveDegree ) )
                {
                    if ( wave024Xtime == waveAtime )
                    {
                        return ( GetSpecialWavePoints( waveScenarioNo, _bars, _currentActiveTimeFrame, wave024Xtime, waveBtime, currentWaveDegree ) );
                    }

                    return ( GetWavePoints( waveScenarioNo, wave024Xtime, waveAtime, currentWaveDegree ) );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_WaveA_WaveB( int waveScenarioNo, long waveBtime, ElliottWaveCycle currentWaveDegree )
        {
            return GetPoints_WaveA_WaveB( waveScenarioNo, _currentActiveTimeFrame, waveBtime, currentWaveDegree );
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_WaveA_WaveB( int waveScenarioNo, TimeSpan period, long waveBtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveAtime = FindPreviousWaveA( waveScenarioNo, period, waveBtime, currentWaveDegree );

            if ( -1 == waveAtime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, waveAtime, waveBtime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );

                if ( waveBtime == waveAtime )
                {
                    return ( GetSpecialWavePoints( waveScenarioNo, bars, period, waveAtime, waveBtime, currentWaveDegree ) );
                }

                return ( GetWavePoints( waveScenarioNo, bars, period, waveAtime, waveBtime, currentWaveDegree ) );
            }

            return default;
        }

        public PooledList<SBar> GetImportanceWavePointsForWaveC( int waveScenarioNo, long waveBTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave0, (DateTime, float PeakTrough ) waveA ) pts = GetPoints_Wave024X_WaveA( waveScenarioNo, waveBTime, currentWaveDegree );

            bool uptrend = pts.waveA.PeakTrough > pts.wave0.PeakTrough ? false : true;

            var waveImportance = GetWaveImportanceDictionary( _currentActiveTimeFrame );

            var nextWaveImportance = waveImportance.GetElementsRightOf( waveBTime );


            ref SBar barB = ref _bars.GetBarByTime(  waveBTime );

            if ( uptrend )
            {
                double lowest = pts.waveA.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < lowest )
                            {
                                lowest = bar.Low;

                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > barB.High )
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double highest = pts.waveA.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > highest )
                            {
                                highest = bar.High;

                                output.Add( bar );

                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < barB.Low )
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }


        public PooledList<SBar> GetImportanceWavePointsForWaveC( int waveScenarioNo, TimeSpan period, long waveBTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave0, (DateTime, float PeakTrough ) waveA ) pts = GetPoints_Wave024X_WaveA( waveScenarioNo, period, waveBTime, currentWaveDegree );

            bool uptrend = pts.waveA.PeakTrough > pts.wave0.PeakTrough ? false : true;

            var waveImportance = GetWaveImportanceDictionary( period );

            var nextWaveImportance = waveImportance.GetElementsRightOf( waveBTime );

            var bars = GetDatabarsRepository( period );

            ref SBar barB = ref bars.GetBarByTime(  waveBTime );

            if ( uptrend )
            {
                double lowest = pts.waveA.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < lowest )
                            {
                                lowest = bar.Low;

                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > barB.High )
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double highest = pts.waveA.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > highest )
                            {
                                highest = bar.High;

                                output.Add( bar );

                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < barB.Low )
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave024X_WaveA( int waveScenarioNo, TimeSpan period, long waveBtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveAtime = FindPreviousWaveA( waveScenarioNo,period, waveBtime, currentWaveDegree );

            if ( -1 == waveAtime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, waveAtime, waveBtime, currentWaveDegree ) )
            {
                var wave024Xtime = FindPreviousWave024X( waveScenarioNo,period, waveAtime, currentWaveDegree );

                if ( -1 == wave024Xtime )
                {
                    return default;
                }

                if ( NoHigherWaveBetween( waveScenarioNo, period, wave024Xtime, waveAtime, currentWaveDegree ) )
                {
                    var bars = GetDatabarsRepository( period );

                    if ( wave024Xtime == waveAtime )
                    {
                        return ( GetSpecialWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveBtime, currentWaveDegree ) );
                    }

                    return ( GetWavePoints( waveScenarioNo,  bars, period, wave024Xtime, waveAtime, currentWaveDegree ) );
                }
            }

            return default;
        }

        public (DateTime, float) GetCurrentPoint( int waveScenarioNo, long rawBarTime )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var currentWave = _hews[ rawBarTime ];

                ref SBar startBar = ref _bars.GetBarByTime( rawBarTime );

                if ( startBar != SBar.EmptySBar )
                {
                    if ( currentWave.WaveLabelPosition == WaveLabelPosition.TOP )
                    {
                        return (rawBarTime.FromLinuxTime(), ( float ) startBar.High);
                    }
                    else if ( currentWave.WaveLabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        return (rawBarTime.FromLinuxTime(), ( float ) startBar.Low);
                    }
                    else if ( currentWave.WaveLabelPosition == WaveLabelPosition.BOTH )
                    {
                        ref var hew = ref currentWave.GetWaveFromScenario( waveScenarioNo );

                        var highestWave = hew.GetFirstWave( );

                        if ( highestWave.HasValue )
                        {
                            if ( highestWave.Value.LabelPosition == WaveLabelPosition.TOP )
                            {
                                return (rawBarTime.FromLinuxTime(), ( float ) startBar.High);
                            }
                            else if ( highestWave.Value.LabelPosition == WaveLabelPosition.BOTTOM )
                            {
                                return (rawBarTime.FromLinuxTime(), ( float ) startBar.Low);
                            }
                        }
                    }
                }
            }

            return ( default );
        }

        public (DateTime, float) GetCurrentPoint( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long rawBarTime )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( hews.ContainsKey( rawBarTime ) )
            {
                var currentWave = hews[ rawBarTime ];

                ref SBar startBar = ref bars.GetBarByTime( rawBarTime );

                if ( startBar != SBar.EmptySBar )
                {
                    if ( currentWave.WaveLabelPosition == WaveLabelPosition.TOP )
                    {
                        return (rawBarTime.FromLinuxTime(), ( float ) startBar.High);
                    }
                    else if ( currentWave.WaveLabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        return (rawBarTime.FromLinuxTime(), ( float ) startBar.Low);
                    }
                    else if ( currentWave.WaveLabelPosition == WaveLabelPosition.BOTH )
                    {
                        ref var hew = ref currentWave.GetWaveFromScenario( waveScenarioNo );

                        var highestWave = hew.GetFirstWave( );

                        if ( highestWave.HasValue )
                        {
                            if ( highestWave.Value.LabelPosition == WaveLabelPosition.TOP )
                            {
                                return (rawBarTime.FromLinuxTime(), ( float ) startBar.High);
                            }
                            else if ( highestWave.Value.LabelPosition == WaveLabelPosition.BOTTOM )
                            {
                                return (rawBarTime.FromLinuxTime(), ( float ) startBar.Low);
                            }
                        }
                    }
                }
            }

            return ( default );
        }

        public (DateTime, float) GetCurrentPoint( fxHistoricBarsRepo bars, TimeSpan period, long rawBarTime, WaveLabelPosition labelPos )
        {
            ref SBar startBar = ref bars.GetBarByTime( rawBarTime );

            if ( startBar != SBar.EmptySBar )
            {
                if ( startBar != SBar.EmptySBar )
                {
                    if ( labelPos == WaveLabelPosition.TOP )
                    {
                        return (rawBarTime.FromLinuxTime(), ( float ) startBar.High);
                    }
                    else if ( labelPos == WaveLabelPosition.BOTTOM )
                    {
                        return (rawBarTime.FromLinuxTime(), ( float ) startBar.Low);
                    }
                }
            }

            return ( default );
        }

        #region Find Wave of ABC

        public ((DateTime, float), (DateTime, float)) GetWaveABeginEndPoints( int waveScenarioNo, long waveAtime, ElliottWaveCycle currentWaveDegree )
        {
            var wave024Xtime = FindPreviousWave024X( waveScenarioNo,waveAtime, currentWaveDegree );

            if ( -1 == wave024Xtime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, wave024Xtime, waveAtime, currentWaveDegree ) )
            {
                var wave024X = _hews[ wave024Xtime ];

                ref var hew = ref wave024X.GetWaveFromScenario( waveScenarioNo );

                var beginningWave = hew.GetFirstHighestWaveInfo( );

                if ( beginningWave.HasValue )
                {
                    return ( GetWavePoints( waveScenarioNo, wave024Xtime, waveAtime, currentWaveDegree ) );
                }
            }

            return ( default );
        }

        public ((DateTime, float), (DateTime, float)) GetWaveABeginEndPoints( int waveScenarioNo, TimeSpan period, long waveAtime, ElliottWaveCycle currentWaveDegree )
        {
            var wave024Xtime = FindPreviousWave024X( waveScenarioNo, period, waveAtime, currentWaveDegree );

            if ( -1 == wave024Xtime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave024Xtime, waveAtime, currentWaveDegree ) )
            {
                var hews = GetElliottWavesDictionary( period );

                var wave024X = hews[ wave024Xtime ];

                ref var hew = ref wave024X.GetWaveFromScenario( waveScenarioNo );

                var beginningWave = hew.GetFirstHighestWaveInfo( );

                if ( beginningWave.HasValue )
                {
                    var bars = GetDatabarsRepository( period );

                    return ( GetWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveAtime, currentWaveDegree ) );
                }
            }

            return ( default );
        }

        public PooledList<SBar> GetImportanceWavePointsForWaveB( int waveScenarioNo, long wave1CTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave0, (DateTime, float PeakTrough ) waveA ) pts = GetPoints_Wave0_Wave1C(waveScenarioNo, wave1CTime, currentWaveDegree );

            bool uptrend = pts.waveA.PeakTrough > pts.wave0.PeakTrough ? true : false;

            var waveImportance = GetWaveImportanceDictionary( _currentActiveTimeFrame );

            var nextWaveImportance = waveImportance.GetElementsRightOf( wave1CTime );

            if ( uptrend )
            {
                double lowest = pts.waveA.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < lowest )
                            {
                                lowest = bar.Low;

                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > pts.waveA.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double highest = pts.waveA.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > highest )
                            {
                                highest = bar.High;
                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < pts.waveA.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }

        public PooledList<SBar> GetImportanceWavePointsForWaveB( int waveScenarioNo, TimeSpan period, long wave1CTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave0, (DateTime, float PeakTrough ) waveA ) pts = GetPoints_Wave0_Wave1C(waveScenarioNo, period, wave1CTime, currentWaveDegree );

            bool uptrend = pts.waveA.PeakTrough > pts.wave0.PeakTrough ? true : false;

            var waveImportance = GetWaveImportanceDictionary( period );

            var bars = GetDatabarsRepository( period );

            var nextWaveImportance = waveImportance.GetElementsRightOf( wave1CTime );

            if ( uptrend )
            {
                double lowest = pts.waveA.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < lowest )
                            {
                                lowest = bar.Low;

                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > pts.waveA.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double highest = pts.waveA.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > highest )
                            {
                                highest = bar.High;
                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < pts.waveA.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }



        public CorrectiveWavesInfo GetWaveFormation( long wave2B4XTime, ElliottWaveCycle currentWaveDegree )
        {
            CorrectiveWavesInfo output = null;

            return output;
        }

        public ElliottWaveEnum WaveBOfWhat( int waveScenarioNo, long waveBtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveAtime = FindPreviousWaveA( waveScenarioNo,waveBtime, currentWaveDegree );

            if ( -1 == waveAtime )
            {
                return ElliottWaveEnum.NONE;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, waveAtime, waveBtime, currentWaveDegree ) )
            {
                var wave024Xtime = FindPreviousWave024X( waveScenarioNo,waveAtime, currentWaveDegree );

                if ( -1 == wave024Xtime )
                {
                    return ElliottWaveEnum.NONE;
                }

                if ( NoHigherWaveBetween( waveScenarioNo, wave024Xtime, waveAtime, currentWaveDegree ) )
                {
                    var wave024X = _hews[ wave024Xtime ];

                    ref var hew = ref wave024X.GetWaveFromScenario( waveScenarioNo );

                    var beginningWave = hew.GetFirstHighestWaveInfo( );

                    if ( beginningWave.Value.WaveCycle == currentWaveDegree )
                    {
                        switch ( beginningWave.Value.WaveName )
                        {
                            case ElliottWaveEnum.Wave2:
                            {
                                return ElliottWaveEnum.Wave3;
                            }

                            case ElliottWaveEnum.Wave4:
                            {
                                return ElliottWaveEnum.Wave5;
                            }

                            case ElliottWaveEnum.WaveX:
                            {
                                var prevWYTime = FindPreviousWaveWY(waveScenarioNo, waveAtime, currentWaveDegree );

                                if ( -1 != prevWYTime )
                                {
                                    var waveDbWY = _hews[ prevWYTime ];
                                    ref var hew2 = ref waveDbWY.GetWaveFromScenario( waveScenarioNo );
                                    var waveWY = hew2.GetFirstHighestWaveInfo( );

                                    switch ( waveWY.Value.WaveName )
                                    {
                                        case ElliottWaveEnum.WaveW:
                                            return ElliottWaveEnum.WaveY;

                                        case ElliottWaveEnum.WaveY:
                                            return ElliottWaveEnum.WaveZ;
                                    }
                                }
                            }
                            break;

                            case ElliottWaveEnum.WaveW:
                            case ElliottWaveEnum.WaveY:
                            {
                                return ElliottWaveEnum.WaveX;
                            }


                            default:
                                break;
                        }
                    }
                    else if ( beginningWave.Value.WaveCycle == currentWaveDegree + GlobalConstants.OneWaveCycle )
                    {
                        switch ( beginningWave.Value.WaveName )
                        {
                            case ElliottWaveEnum.Wave1:
                            case ElliottWaveEnum.Wave1C:
                                return ElliottWaveEnum.Wave2;

                            case ElliottWaveEnum.Wave3:
                            case ElliottWaveEnum.Wave3C:
                                return ElliottWaveEnum.Wave4;

                            // Since previous wave of One Higher degress is Wave X, that means we are in Wave A, the first wave B will be Wave 1 of Wave A
                            case ElliottWaveEnum.WaveW:
                            case ElliottWaveEnum.WaveX:
                            case ElliottWaveEnum.WaveY:
                            case ElliottWaveEnum.WaveZ:
                                return ElliottWaveEnum.Wave1;

                            // Since previous wave of One Higher degress is Wave B, that means we are in Wave C, the first wave B will be Wave 1 of Wave C
                            case ElliottWaveEnum.WaveB:
                                return ElliottWaveEnum.Wave1;

                            case ElliottWaveEnum.WaveA:
                                return ElliottWaveEnum.WaveB;

                            case ElliottWaveEnum.WaveEFA:
                                return ElliottWaveEnum.WaveEFB;

                            case ElliottWaveEnum.Wave5:
                            case ElliottWaveEnum.Wave5C:
                                return ElliottWaveEnum.Wave1;

                            case ElliottWaveEnum.Wave4:
                                return ElliottWaveEnum.Wave1;

                            case ElliottWaveEnum.Wave2:
                                return ElliottWaveEnum.Wave1;
                        }
                    }
                }
            }

            return ElliottWaveEnum.WaveC;
        }

        public ElliottWaveEnum WaveBOfWhat( int waveScenarioNo, TimeSpan period, long waveBtime, ElliottWaveCycle currentWaveDegree )
        {
            var hews = GetElliottWavesDictionary( period );

            var waveAtime = FindPreviousWaveA( waveScenarioNo,period, waveBtime, currentWaveDegree );

            if ( -1 == waveAtime )
            {
                return ElliottWaveEnum.NONE;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, waveAtime, waveBtime, currentWaveDegree ) )
            {
                var wave024Xtime = FindPreviousWave024X( waveScenarioNo, period, waveAtime, currentWaveDegree );

                if ( -1 == wave024Xtime )
                {
                    return ElliottWaveEnum.NONE;
                }

                if ( NoHigherWaveBetween( waveScenarioNo, period, wave024Xtime, waveAtime, currentWaveDegree ) )
                {
                    var wave024X = hews[ wave024Xtime ];

                    ref var hew = ref wave024X.GetWaveFromScenario( waveScenarioNo );

                    var beginningWave = hew.GetFirstHighestWaveInfo( );

                    if ( beginningWave.Value.WaveCycle == currentWaveDegree )
                    {
                        switch ( beginningWave.Value.WaveName )
                        {
                            case ElliottWaveEnum.Wave2:
                                return ElliottWaveEnum.Wave3;

                            case ElliottWaveEnum.Wave4:
                                return ElliottWaveEnum.Wave5;

                            case ElliottWaveEnum.WaveX:
                            {
                                var prevWYTime = FindPreviousWaveWY( waveScenarioNo, waveAtime, currentWaveDegree );

                                if ( -1 != prevWYTime )
                                {
                                    var waveDbWY = _hews[ prevWYTime ];
                                    ref var hew2 = ref waveDbWY.GetWaveFromScenario( waveScenarioNo );
                                    var waveWY = hew2.GetFirstHighestWaveInfo( );

                                    switch ( waveWY.Value.WaveName )
                                    {
                                        case ElliottWaveEnum.WaveW:
                                            return ElliottWaveEnum.WaveY;

                                        case ElliottWaveEnum.WaveY:
                                            return ElliottWaveEnum.WaveZ;
                                    }
                                }
                            }
                            break;

                            default:
                                break;
                        }
                    }
                    else if ( beginningWave.Value.WaveCycle == currentWaveDegree + GlobalConstants.OneWaveCycle )
                    {
                        switch ( beginningWave.Value.WaveName )
                        {
                            case ElliottWaveEnum.Wave1:
                            case ElliottWaveEnum.Wave1C:
                                return ElliottWaveEnum.Wave2;

                            case ElliottWaveEnum.Wave3:
                            case ElliottWaveEnum.Wave3C:
                                return ElliottWaveEnum.Wave4;

                            // Since previous wave of One Higher degress is Wave X, that means we are in Wave A, the first wave B will be Wave 1 of Wave A
                            case ElliottWaveEnum.WaveW:
                            case ElliottWaveEnum.WaveX:
                            case ElliottWaveEnum.WaveY:
                            case ElliottWaveEnum.WaveZ:
                                return ElliottWaveEnum.Wave1;

                            // Since previous wave of One Higher degress is Wave B, that means we are in Wave C, the first wave B will be Wave 1 of Wave C
                            case ElliottWaveEnum.WaveB:
                                return ElliottWaveEnum.Wave1;

                            case ElliottWaveEnum.WaveA:
                                return ElliottWaveEnum.WaveB;

                            case ElliottWaveEnum.WaveEFA:
                                return ElliottWaveEnum.WaveEFB;

                            case ElliottWaveEnum.Wave5:
                            case ElliottWaveEnum.Wave5C:
                                return ElliottWaveEnum.Wave1;

                            case ElliottWaveEnum.Wave4:
                                return ElliottWaveEnum.Wave1;

                            case ElliottWaveEnum.Wave2:
                                return ElliottWaveEnum.Wave1;
                        }
                    }
                }
            }

            return ElliottWaveEnum.WaveC;
        }

        #endregion



        public bool GetElliottWave( int offerId,
                                    long lowerBound,
                                    long upperBound,
                                    TimeSpan period )
        {
            string periodString = FinancialHelper.GetPeriodId( period );

            var hews = GetElliottWavesDictionary( period );

            if ( period == TimeSpan.FromDays( 1 ) )
            {
            }

            lock ( _objLock )
            {
                var resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.StartDate >= lowerBound && c.StartDate <= upperBound && c.Period == periodString ).OrderBy( x => x.StartDate );

                if ( resultSet != null )
                {
                    foreach ( DbElliottWave dbElliottWave in resultSet )
                    {
                        if ( !hews.ContainsKey( dbElliottWave.StartDate ) )
                        {
                            hews.Add( dbElliottWave.StartDate, dbElliottWave );
                        }
                        else
                        {
                            //throw new NotImplementedException( );
                        }
                    }
                }
            }

            return true;
        }

        public void DoDatabaseConversion()
        {
            lock ( _objLock )
            {
                var currentTime = DateTime.UtcNow;

                long upperBound = currentTime.ToFileTimeUtc( );

                bool didUpdate = false;

                using ( var context = new ForexDatabars() )
                {
                    var found = from b in context.ELLIOTTWAVE where b.StartDate <= upperBound select b;

                    if ( found.Any() )
                    {
                        didUpdate = true;

                        foreach ( var first in found )
                        {
                            var originalDate = first.StartDate;
                            var newDate = DateTime.FromFileTimeUtc( originalDate );

                            first.StartDate = newDate.ToLinuxTime();
                        }
                    }
                    else
                    {
                    }

                    try
                    {
                        if ( didUpdate )
                        {
                            context.SaveChanges();
                        }
                    }
                    catch ( DbException ex )
                    {
                        this.LogError( ex );
                    }

                    Messenger.Default.Send( new WorkDoneMessage( "Save Waves to Database" ) );
                }
            }
        }

        #region Finding Waves functions

        public ((DateTime, float), (DateTime, float)) GetImpulsiveWaveRange( int waveScenarioNo, long endBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var dbHewPrev = GetPreviousWaveStructure( waveScenarioNo, endBarTime );

            if ( dbHewPrev == null )
            {
                return default;
            }

            ref var hew = ref dbHewPrev.GetWaveFromScenario( waveScenarioNo );
            var startWaveName = hew.GetWavesAtCycle( currentWaveDegree );

            long startBarTime = dbHewPrev.StartDate;

            if ( startWaveName.Count == 0 ) // This means we are missing some waves at the current levels. So we will go up one more degree to see 
            {
                long previousWaveStructTime = dbHewPrev.StartDate;

                ElliottWaveCycle nextHigherWaveDegree = currentWaveDegree + 5;

                ref var hew2 = ref dbHewPrev.GetWaveFromScenario( waveScenarioNo );

                var nextHigherWaveName = hew2.GetWavesAtCycle( nextHigherWaveDegree );

                if ( nextHigherWaveName.Count > 0 )
                {
                    return ( GetWavePoints( waveScenarioNo, startBarTime, endBarTime, currentWaveDegree ) );
                }
            }

            return ( GetWavePoints( waveScenarioNo, startBarTime, endBarTime, currentWaveDegree ) );
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_Wave1C( int waveScenarioNo, long wave1CTime, ElliottWaveCycle currentWaveDegree )
        {
            var startBarTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, wave1CTime, currentWaveDegree );

            if ( -1 == startBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, startBarTime, wave1CTime, currentWaveDegree ) )
            {
                return ( GetWavePoints( waveScenarioNo, startBarTime, wave1CTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_Wave1C( int waveScenarioNo, TimeSpan period, long wave1CTime, ElliottWaveCycle currentWaveDegree )
        {
            var startBarTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, period, wave1CTime, currentWaveDegree );

            if ( -1 == startBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, startBarTime, wave1CTime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );
                return ( GetWavePoints( waveScenarioNo, bars, period, startBarTime, wave1CTime, currentWaveDegree ) );
            }

            return default;
        }

        public PooledList<SBar> GetImportanceWavePointsForWave2( int waveScenarioNo, long wave1CTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave0, (DateTime, float PeakTrough ) wave1C ) pts = GetPoints_Wave0_Wave1C( waveScenarioNo, wave1CTime, currentWaveDegree );

            bool uptrend = pts.wave1C.PeakTrough > pts.wave0.PeakTrough ? true : false;

            var waveImportance = GetWaveImportanceDictionary( _currentActiveTimeFrame );

            var nextWaveImportance = waveImportance.GetElementsRightOf( wave1CTime );

            if ( uptrend )
            {
                double lowest = pts.wave1C.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < lowest )
                            {
                                lowest = bar.Low;

                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > pts.wave1C.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double highest = pts.wave1C.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > highest )
                            {
                                highest = bar.High;
                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < pts.wave1C.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }

        public PooledList<SBar> GetImportanceWavePointsForWave2( int waveScenarioNo, TimeSpan period, long wave1CTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave0, (DateTime, float PeakTrough ) wave1C ) pts = GetPoints_Wave0_Wave1C( waveScenarioNo, period, wave1CTime, currentWaveDegree );

            bool uptrend = pts.wave1C.PeakTrough > pts.wave0.PeakTrough ? true : false;

            var waveImportance = GetWaveImportanceDictionary( period );
            var bars = GetDatabarsRepository( period );

            var nextWaveImportance = waveImportance.GetElementsRightOf( wave1CTime );

            if ( uptrend )
            {
                double lowest = pts.wave1C.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < lowest )
                            {
                                lowest = bar.Low;

                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > pts.wave1C.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double highest = pts.wave1C.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > highest )
                            {
                                highest = bar.High;
                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < pts.wave1C.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }

        public bool GetNextRightDot( long barTime, ref SBar bar )
        {
            var waveImportance = GetWaveImportanceDictionary( _currentActiveTimeFrame );

            var nextWaveImportance = waveImportance.GetElementsRightOf( barTime );

            foreach ( var wav in nextWaveImportance )
            {
                if ( wav.Value.WaveImportance == GlobalConstants.DAILYIMPT )
                {
                    bar = ref _bars.GetBarByTime( wav.Key );

                    if ( bar != SBar.EmptySBar )
                    {
                        return true;
                    }
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave2_Wave3C( int waveScenarioNo, long waveThreeBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave2BarTime = FindPreviousWave2( waveScenarioNo,waveThreeBarTime, currentWaveDegree );

            if ( -1 == wave2BarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, wave2BarTime, waveThreeBarTime, currentWaveDegree ) )
            {
                return ( GetWavePoints( waveScenarioNo, wave2BarTime, waveThreeBarTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave2_Wave3C( int waveScenarioNo, TimeSpan period, long waveThreeBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave2BarTime = FindPreviousWave2( waveScenarioNo, period, waveThreeBarTime, currentWaveDegree );

            if ( -1 == wave2BarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave2BarTime, waveThreeBarTime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );
                return ( GetWavePoints( waveScenarioNo, bars, period, wave2BarTime, waveThreeBarTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave3B_Wave3C( int waveScenarioNo, long waveThreeBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave3BarTime = FindPreviousWaveB( waveScenarioNo,waveThreeBarTime, currentWaveDegree );

            if ( -1 == wave3BarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, wave3BarTime, waveThreeBarTime, currentWaveDegree ) )
            {
                return ( GetWavePoints( waveScenarioNo, wave3BarTime, waveThreeBarTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave3B_Wave3C( int waveScenarioNo, TimeSpan period, long waveThreeBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave3BarTime = FindPreviousWaveB( waveScenarioNo, period, waveThreeBarTime, currentWaveDegree );

            if ( -1 == wave3BarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave3BarTime, waveThreeBarTime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );
                return ( GetWavePoints( waveScenarioNo, bars, period, wave3BarTime, waveThreeBarTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave4B_Wave5( int waveScenarioNo, long wave4Time, ElliottWaveCycle wave4Degree, long wave5Time )
        {
            var time4B = FindPreviousWaveB( waveScenarioNo,wave5Time, wave4Degree );

            if ( -1 == time4B )
            {
                return default;
            }

            if ( time4B > wave4Time )
            {
                if ( NoHigherWaveBetween( waveScenarioNo, wave4Time, time4B, wave4Degree ) && NoHigherWaveBetween( waveScenarioNo, time4B, wave5Time, wave4Degree ) )
                {
                    return ( GetWavePoints( waveScenarioNo, time4B, wave5Time, wave4Degree ) );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_LowerDegreeWave4_Wave5( int waveScenarioNo, long time4B, ElliottWaveCycle wave4Degree, long wave5Time )
        {
            var timeWave4 = FindPreviousWave4( waveScenarioNo,wave5Time, wave4Degree );

            if ( -1 == timeWave4 )
            {
                return default;
            }

            if ( timeWave4 > time4B )
            {
                if ( NoHigherWaveBetween( waveScenarioNo, time4B, timeWave4, wave4Degree ) && NoHigherWaveBetween( waveScenarioNo, timeWave4, wave5Time, wave4Degree ) )
                {
                    return ( GetWavePoints( waveScenarioNo, timeWave4, wave5Time, wave4Degree ) );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_smallWave4B_Wave3( int waveScenarioNo, long wave3time, ElliottWaveCycle currentWaveDegree )
        {
            ((DateTime, float) smallWave4, (DateTime, float) wave3 ) small4_3 = GetPoints_smallWave4_Wave3( waveScenarioNo, wave3time, currentWaveDegree );

            if ( default != small4_3 )
            {
                var previousWaves = _hews.GetElementsRightOf( wave3time );
                var wave4time     = small4_3.smallWave4.Item1.ToLinuxTime( );
                var oneDegLower   = currentWaveDegree - GlobalConstants.OneWaveCycle;

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > wave4time )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWaveBOfDegree( oneDegLower ) )
                        {
                            return GetWavePoints( waveScenarioNo, wave.Key, wave3time, currentWaveDegree );
                        }
                    }
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_smallWave4_Wave3( int waveScenarioNo, long wave3time, ElliottWaveCycle currentWaveDegree )
        {
            var wave2time = FindPreviousWave2( waveScenarioNo, wave3time, currentWaveDegree );

            if ( -1 == wave2time )
            {
                return default;
            }

            var oneDegLower = currentWaveDegree - GlobalConstants.OneWaveCycle;

            if ( NoHigherWaveBetween( waveScenarioNo, wave2time, wave3time, currentWaveDegree ) )
            {
                var previousWaves = _hews.GetElementsRightOf( wave3time );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > wave2time )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWave4OfDegree( oneDegLower ) )
                        {
                            return GetWavePoints( waveScenarioNo, wave.Key, wave3time, currentWaveDegree );
                        }
                    }
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_smallWave4B_Wave3( int waveScenarioNo, TimeSpan period, long wave3time, ElliottWaveCycle currentWaveDegree )
        {
            ((DateTime, float) smallWave4, (DateTime, float) wave3 ) small4_3 = GetPoints_smallWave4_Wave3( waveScenarioNo, period, wave3time, currentWaveDegree );

            if ( default != small4_3 )
            {
                var hews = GetElliottWavesDictionary( period );

                var previousWaves = hews.GetElementsRightOf( wave3time );
                var wave4time     = small4_3.smallWave4.Item1.ToLinuxTime( );
                var oneDegLower   = currentWaveDegree - GlobalConstants.OneWaveCycle;

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > wave4time )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWaveBOfDegree( oneDegLower ) )
                        {
                            var bars = GetDatabarsRepository( period );
                            return GetWavePoints( waveScenarioNo, bars, period, wave.Key, wave3time, currentWaveDegree );
                        }
                    }
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_smallWave4_Wave3( int waveScenarioNo, TimeSpan period,  long wave3time, ElliottWaveCycle currentWaveDegree )
        {
            var wave2time = FindPreviousWave2( waveScenarioNo, period, wave3time, currentWaveDegree );

            if ( -1 == wave2time )
            {
                return default;
            }

            var oneDegLower = currentWaveDegree - GlobalConstants.OneWaveCycle;

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave2time, wave3time, currentWaveDegree ) )
            {
                var hews = GetElliottWavesDictionary( period );

                var previousWaves = hews.GetElementsRightOf( wave3time );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > wave2time )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWave4OfDegree( oneDegLower ) )
                        {
                            var bars = GetDatabarsRepository( period );
                            return GetWavePoints( waveScenarioNo, bars, period, wave.Key, wave3time, currentWaveDegree );
                        }
                    }
                }
            }

            return default;
        }

        public PooledList<SBar> GetImportanceWavePointsForWave4( int waveScenarioNo, long wave1CTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave2, (DateTime, float PeakTrough ) wave3C ) pts = GetPoints_Wave2_Wave3C( waveScenarioNo,wave1CTime, currentWaveDegree );

            bool uptrend = pts.wave3C.PeakTrough > pts.wave2.PeakTrough ? true : false;

            var waveImportance = GetWaveImportanceDictionary( _currentActiveTimeFrame );

            var nextWaveImportance = waveImportance.GetElementsRightOf( wave1CTime );

            if ( uptrend )
            {
                double lowest = pts.wave3C.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < lowest )
                            {
                                lowest = bar.Low;

                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > pts.wave3C.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double highest = pts.wave3C.PeakTrough;
                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > highest )
                            {
                                highest = bar.High;
                                output.Add( bar );

                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref _bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < pts.wave3C.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }

        public PooledList<SBar> GetImportanceWavePointsForWave4( int waveScenarioNo, TimeSpan period, long wave1CTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave2, (DateTime, float PeakTrough ) wave3C ) pts = GetPoints_Wave2_Wave3C( waveScenarioNo, period, wave1CTime, currentWaveDegree );

            bool uptrend = pts.wave3C.PeakTrough > pts.wave2.PeakTrough ? true : false;

            var waveImportance = GetWaveImportanceDictionary( period );
            var bars = GetDatabarsRepository( period );

            var nextWaveImportance = waveImportance.GetElementsRightOf( wave1CTime );

            if ( uptrend )
            {
                double lowest = pts.wave3C.PeakTrough;

                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < lowest )
                            {
                                lowest = bar.Low;

                                output.Add( bar );
                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > pts.wave3C.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double highest = pts.wave3C.PeakTrough;
                foreach ( var wav in nextWaveImportance )
                {
                    if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.High > highest )
                            {
                                highest = bar.High;
                                output.Add( bar );

                            }
                        }
                    }
                    else
                    {
                        ref SBar bar = ref bars.GetBarByTime( wav.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( bar.Low < pts.wave3C.PeakTrough )
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }


        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_Wave5C( int waveScenarioNo, long wave5CTime, ElliottWaveCycle currentWaveDegree )
        {
            var startBarTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo,wave5CTime, currentWaveDegree );

            if ( -1 == startBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, startBarTime, wave5CTime, currentWaveDegree ) )
            {
                return ( GetWavePoints( waveScenarioNo, startBarTime, wave5CTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_Wave5C( int waveScenarioNo, TimeSpan period, long wave5CTime, ElliottWaveCycle currentWaveDegree )
        {
            var startBarTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, period, wave5CTime, currentWaveDegree );

            if ( -1 == startBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, startBarTime, wave5CTime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );
      
                return ( GetWavePoints( waveScenarioNo, bars, period, startBarTime, wave5CTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0X_WaveC( int waveScenarioNo, long waveCtime, ElliottWaveCycle currentWaveDegree )
        {
            var wave0X = FindPreviousWave0X( waveScenarioNo,waveCtime, currentWaveDegree );

            if ( -1 == wave0X )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, wave0X, waveCtime, currentWaveDegree ) )
            {
                return ( GetWavePoints( waveScenarioNo, wave0X, waveCtime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0X_WaveC( int waveScenarioNo, TimeSpan period, long waveCtime, ElliottWaveCycle currentWaveDegree )
        {
            var wave0X = FindPreviousWave0X( waveScenarioNo, period, waveCtime, currentWaveDegree );

            if ( -1 == wave0X )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave0X, waveCtime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );

                return ( GetWavePoints( waveScenarioNo, bars, period, wave0X, waveCtime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_WaveEFA( int waveScenarioNo, long waveEFBtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveEFAtime = FindPreviousWaveEFA( waveScenarioNo, waveEFBtime, currentWaveDegree );

            if ( -1 == waveEFAtime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, waveEFAtime, waveEFBtime, currentWaveDegree ) )
            {
                return ( GetWavePoints( waveScenarioNo, waveEFAtime, waveEFBtime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_WaveEFA( int waveScenarioNo, TimeSpan period, long waveEFBtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveEFAtime = FindPreviousWaveEFA( waveScenarioNo, period, waveEFBtime, currentWaveDegree );

            if ( -1 == waveEFAtime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, waveEFAtime, waveEFBtime, currentWaveDegree ) )
            {
                return ( GetWavePoints( waveScenarioNo, waveEFAtime, waveEFBtime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_BeginningOfExpandedFlat( int waveScenarioNo, long waveEFXtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveEFBeginTime = FindBeginningWaveOfCurrentExpandedFlat( waveScenarioNo,waveEFXtime, currentWaveDegree );

            if ( -1 == waveEFBeginTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, waveEFBeginTime, waveEFXtime, currentWaveDegree ) )
            {
                return ( GetWavePoints( waveScenarioNo, waveEFBeginTime, waveEFXtime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_BeginningOfExpandedFlat( int waveScenarioNo, TimeSpan period, long waveEFXtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveEFBeginTime = FindBeginningWaveOfCurrentExpandedFlat( waveScenarioNo,period, waveEFXtime, currentWaveDegree );

            if ( -1 == waveEFBeginTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, waveEFBeginTime, waveEFXtime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );

                return ( GetWavePoints( waveScenarioNo, bars, period, waveEFBeginTime, waveEFXtime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetSpecialWavePoints( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long specialBarTime, long referenceBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( bars != null )
            {
                var hews = GetElliottWavesDictionary( period );

                if ( specialBarTime < referenceBarTime )
                {
                    if ( IsSpecialBar( waveScenarioNo, period, specialBarTime ) )
                    {
                        ref SBar beginBar = ref bars.GetBarByTime( specialBarTime );
                        ref SBar endBar   = ref bars.GetBarByTime( referenceBarTime );

                        if ( beginBar != SBar.EmptySBar && endBar != SBar.EmptySBar )
                        {
                            var referenceBarHew = hews[ referenceBarTime ];

                            ref var hew = ref referenceBarHew.GetWaveFromScenario( waveScenarioNo );

                            var firstWave = hew.GetFirstHighestWaveInfo( );
                            var firstWavePos = firstWave.Value.LabelPosition;

                            var startBarPoint = ( specialBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) beginBar.High : ( float ) beginBar.Low );

                            var endBarPoint = ( referenceBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) endBar.Low : ( float ) endBar.High );

                            var output = ( startBarPoint, endBarPoint );

                            return ( output );
                        }
                    }
                    else if ( IsSpecialBar( waveScenarioNo, period, referenceBarTime ) )
                    {
                        ref SBar beginBar = ref bars.GetBarByTime( specialBarTime );
                        ref SBar endBar   = ref bars.GetBarByTime( referenceBarTime );

                        if ( beginBar != SBar.EmptySBar && endBar != SBar.EmptySBar )
                        {
                            var referenceBarHew = hews[ specialBarTime ];
                            ref var hew = ref referenceBarHew.GetWaveFromScenario( waveScenarioNo );

                            var firstWave = hew.GetFirstHighestWaveInfo( );
                            
                            var firstWavePos = firstWave.Value.LabelPosition;


                            var startBarPoint = ( specialBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) beginBar.High : ( float ) beginBar.Low );

                            var endBarPoint = ( referenceBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) endBar.Low : ( float ) endBar.High );

                            var output = ( startBarPoint, endBarPoint );

                            return ( output );

                        }
                    }
                }
                else
                {
                    ref SBar specialBar = ref bars.GetBarByTime( specialBarTime );

                    if ( specialBar != SBar.EmptySBar )
                    {
                        var speicalBarHew = hews[ specialBarTime ];

                        ref var hew = ref speicalBarHew.GetWaveFromScenario( waveScenarioNo );

                        var firstWave = hew.GetFirstWave( );
                        
                        var firstWavePos = firstWave.Value.LabelPosition;

                        var startBarPoint = ( specialBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) specialBar.High : ( float ) specialBar.Low );

                        var endBarPoint = ( specialBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) specialBar.Low : ( float ) specialBar.High );

                        var output = ( startBarPoint, endBarPoint );

                        return ( output );
                    }
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) Get2SpecialWavePoints( fxHistoricBarsRepo bars, TimeSpan period, long beginBarTime, long endBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( bars != null )
            {
                var hews = GetElliottWavesDictionary( period );

                ref SBar beginBar = ref bars.GetBarByTime( beginBarTime );
                if ( beginBar == SBar.EmptySBar )
                    return default;

                ref SBar endBar = ref bars.GetBarByTime( endBarTime );
                if ( endBar == SBar.EmptySBar )
                    return default;

                double length1 = Math.Abs( beginBar.High - endBar.Low );
                double length2 = Math.Abs( beginBar.Low - endBar.High );

                if ( length1 > length2 )
                {
                    var startBarPoint = ( beginBarTime.FromLinuxTime( ), ( float ) beginBar.High );

                    var endBarPoint = ( endBarTime.FromLinuxTime( ), ( float ) endBar.Low );

                    var output = ( startBarPoint, endBarPoint );

                    return ( output );
                }
                else
                {
                    var startBarPoint = ( beginBarTime.FromLinuxTime( ), ( float ) beginBar.Low );

                    var endBarPoint = ( endBar.BarTime, ( float ) endBar.High );

                    var output = ( startBarPoint, endBarPoint );

                    return ( output );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetSpecialWavePoints( int waveScenarioNo, long specialBarTime, ElliottWaveCycle currentWaveDegree )
        {
            ref SBar specialBar = ref _bars.GetBarByTime( specialBarTime );

            if ( specialBar != SBar.EmptySBar )
            {
                var speicalBarHew = _hews[ specialBarTime ];

                ref var hew = ref speicalBarHew.GetWaveFromScenario( waveScenarioNo );
                var firstWave = hew.GetFirstWave( );
                var firstWavePos = firstWave.Value.LabelPosition;

                var startBarPoint = ( specialBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) specialBar.High : ( float ) specialBar.Low );

                var endBarPoint = ( specialBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) specialBar.Low : ( float ) specialBar.High );

                var output = ( startBarPoint, endBarPoint );

                return ( output );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetSpecialWavePoints( int waveScenarioNo, fxHistoricBarsRepo selectedDatarepo, TimeSpan period, long specialBarTime, ElliottWaveCycle currentWaveDegree )
        {
            if ( selectedDatarepo != null )
            {
                var hews = GetElliottWavesDictionary( period );

                ref SBar specialBar = ref selectedDatarepo.GetBarByTime( specialBarTime );

                if ( specialBar != SBar.EmptySBar )
                {
                    var speicalBarHew = hews[ specialBarTime ];
                    ref var hew = ref speicalBarHew.GetWaveFromScenario( waveScenarioNo );
                    var firstWave = hew.GetFirstWave( );
                    
                    var firstWavePos = firstWave.Value.LabelPosition;

                    var startBarPoint = ( specialBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) specialBar.High : ( float ) specialBar.Low );

                    var endBarPoint = ( specialBarTime.FromLinuxTime( ), firstWavePos == WaveLabelPosition.TOP ? ( float ) specialBar.Low : ( float ) specialBar.High );

                    var output = ( startBarPoint, endBarPoint );

                    return ( output );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetWavePoints( int waveScenarioNo, long startBarTime, long endBarTime, ElliottWaveCycle currentWaveDegree )
        {
            return GetWavePoints( waveScenarioNo, _bars, _currentActiveTimeFrame, startBarTime, endBarTime, currentWaveDegree );
        }

        public ((DateTime, float), (DateTime, float)) GetWavePoints( int waveScenarioNo, fxHistoricBarsRepo bars,
                                                                            TimeSpan period,
                                                                            long startBarTime,
                                                                            long endBarTime,
                                                                            ElliottWaveCycle currentWaveDegree )
        {
            var hews = GetElliottWavesDictionary( period );

            if ( !hews.ContainsKey( startBarTime ) )
            {
                return default;
            }

            if ( !hews.ContainsKey( endBarTime ) )
            {
                return default;
            }

            (( DateTime, float ), ( DateTime, float )) output = default;

            if ( IsSpecialBar( waveScenarioNo,  period, startBarTime ) || IsSpecialBar( waveScenarioNo, period, endBarTime ) )
            {
                if ( startBarTime != endBarTime )
                {
                    return ( Get2SpecialWavePoints( bars, period, startBarTime, endBarTime, currentWaveDegree ) );
                }
                else
                {
                    return ( GetSpecialWavePoints( waveScenarioNo, bars, period, startBarTime, currentWaveDegree ) );
                }
            }
            else
            {
                ref SBar startBar = ref bars.GetBarByTime( startBarTime );
                ref SBar endBar   = ref bars.GetBarByTime( endBarTime );


                if ( startBar != SBar.EmptySBar && endBar != SBar.EmptySBar )
                {
                    var startBarHew   = hews[ startBarTime ];
                    var endWaveHew    = hews[ endBarTime ];

                    var startBarPoint = ( startBarTime.FromLinuxTime( ), startBarHew.WaveLabelPosition == WaveLabelPosition.TOP ? ( float ) startBar.High : ( float ) startBar.Low );

                    var endBarPoint   = ( endBarTime.FromLinuxTime( ), endWaveHew.WaveLabelPosition == WaveLabelPosition.TOP ? ( float ) endBar.High : ( float ) endBar.Low );

                    output = (startBarPoint, endBarPoint);
                }
            }

            return ( output );
        }


        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_Wave3C_ForWave4( int waveScenarioNo, long wave4BarTime, ElliottWaveCycle currentWaveDegree )
        {
            return GetPoints_Wave0_Wave3C_ForWave4( waveScenarioNo, _currentActiveTimeFrame, wave4BarTime, currentWaveDegree );
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_Wave3C_ForWave4( int waveScenarioNo, TimeSpan period, long wave4BarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave3BarTime = FindPreviousWave3( waveScenarioNo, period, wave4BarTime, currentWaveDegree );

            if ( -1 == wave3BarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave3BarTime, wave4BarTime, currentWaveDegree ) )
            {
                var startBarTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, period, wave3BarTime, currentWaveDegree );

                if ( -1 == startBarTime )
                {
                    return default;
                }

                if ( NoHigherWaveBetween( waveScenarioNo, period, startBarTime, wave3BarTime, currentWaveDegree ) )
                {
                    var bars = GetDatabarsRepository( period );

                    if ( startBarTime == wave3BarTime )
                    {
                        return ( GetSpecialWavePoints( waveScenarioNo, bars, period, startBarTime, wave4BarTime, currentWaveDegree ) );
                    }

                    return ( GetWavePoints( waveScenarioNo, bars, period, startBarTime, wave3BarTime, currentWaveDegree ) );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave3C_Wave4( int waveScenarioNo, long wave4BarTime, ElliottWaveCycle currentWaveDegree )
        {
            return GetPoints_Wave3C_Wave4( waveScenarioNo, _currentActiveTimeFrame, wave4BarTime, currentWaveDegree );
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave3C_Wave4( int waveScenarioNo, TimeSpan period, long wave4BarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave3BarTime = FindPreviousWave3( waveScenarioNo, period, wave4BarTime, currentWaveDegree );

            if ( -1 == wave3BarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave3BarTime, wave4BarTime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );

                if ( wave4BarTime == wave3BarTime )
                {
                    return ( GetSpecialWavePoints( waveScenarioNo, bars, period, wave3BarTime, wave4BarTime, currentWaveDegree ) );
                }

                return ( GetWavePoints( waveScenarioNo, bars, period, wave3BarTime, wave4BarTime, currentWaveDegree ) );
            }

            return default;
        }



        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_Wave1C_ForWave2( int waveScenarioNo, long wave2BarTime, ElliottWaveCycle currentWaveDegree )
        {
            return ( GetPoints_Wave0_Wave1C_ForWave2( waveScenarioNo, _currentActiveTimeFrame, wave2BarTime, currentWaveDegree ) );
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_Wave1C_ForWave2( int waveScenarioNo, TimeSpan period, long wave2BarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave1BarTime = FindPreviousWave1( waveScenarioNo, period, wave2BarTime, currentWaveDegree );

            if ( -1 == wave2BarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave1BarTime, wave2BarTime, currentWaveDegree ) )
            {
                var startBarTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, period, wave1BarTime, currentWaveDegree );

                if ( -1 == startBarTime )
                {
                    return default;
                }

                if ( NoHigherWaveBetween( waveScenarioNo, period, startBarTime, wave1BarTime, currentWaveDegree ) )
                {
                    var bars = GetDatabarsRepository( period );

                    if ( startBarTime == wave1BarTime )
                    {
                        return ( GetSpecialWavePoints( waveScenarioNo, bars, period, startBarTime, wave2BarTime, currentWaveDegree ) );
                    }

                    return ( GetWavePoints( waveScenarioNo, bars, period, startBarTime, wave1BarTime, currentWaveDegree ) );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave1C_Wave12( int waveScenarioNo, long wave2BarTime, ElliottWaveCycle currentWaveDegree )
        {
            return GetPoints_Wave1C_Wave12( waveScenarioNo, _currentActiveTimeFrame, wave2BarTime, currentWaveDegree );
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave1C_Wave12( int waveScenarioNo, TimeSpan period, long wave2BarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave1BarTime = FindPreviousWave1( waveScenarioNo, period, wave2BarTime, currentWaveDegree );

            if ( -1 == wave2BarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave1BarTime, wave2BarTime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );

                if ( wave2BarTime == wave1BarTime )
                {
                    return ( GetSpecialWavePoints( waveScenarioNo, bars, period, wave1BarTime, wave2BarTime, currentWaveDegree ) );
                }

                return ( GetWavePoints( waveScenarioNo, bars, period, wave1BarTime, wave2BarTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_smallWave4B_WaveA( int waveScenarioNo, long waveAtime, ElliottWaveCycle currentWaveDegree )
        {
            ((DateTime, float) wave4, (DateTime, float) waveA ) small4_A = GetPoints_smallWave4_WaveA( waveScenarioNo, waveAtime, currentWaveDegree );

            if ( default != small4_A )
            {
                var previousWaves = _hews.GetElementsRightOf( waveAtime );
                var wave4time     = small4_A.wave4.Item1.ToLinuxTime( );
                var oneDegLower   = currentWaveDegree - GlobalConstants.OneWaveCycle;

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > wave4time )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );
                        
                        if ( hew.HasWaveBOfDegree( oneDegLower ) )
                        {
                            return GetWavePoints( waveScenarioNo, wave.Key, waveAtime, currentWaveDegree );
                        }
                    }
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_smallWave4_WaveA( int waveScenarioNo, long waveAtime, ElliottWaveCycle currentWaveDegree )
        {
            var wave024Xtime = FindPreviousWave024X(waveScenarioNo, waveAtime, currentWaveDegree );

            if ( -1 == wave024Xtime )
            {
                return default;
            }

            var oneDegLower = currentWaveDegree - GlobalConstants.OneWaveCycle;

            if ( NoHigherWaveBetween( waveScenarioNo, wave024Xtime, waveAtime, currentWaveDegree ) )
            {
                var previousWaves = _hews.GetElementsRightOf( waveAtime );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > wave024Xtime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWave4OfDegree( oneDegLower ) )
                        {
                            return GetWavePoints( waveScenarioNo, wave.Key, waveAtime, currentWaveDegree );
                        }
                    }
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_smallWave4_WaveA( int waveScenarioNo, TimeSpan period, long waveAtime, ElliottWaveCycle currentWaveDegree )
        {
            var wave024Xtime = FindPreviousWave024X( waveScenarioNo, period, waveAtime, currentWaveDegree );

            if ( -1 == wave024Xtime )
            {
                return default;
            }

            var oneDegLower = currentWaveDegree - GlobalConstants.OneWaveCycle;

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave024Xtime, waveAtime, currentWaveDegree ) )
            {
                var hews = GetElliottWavesDictionary( period );

                var previousWaves = hews.GetElementsRightOf( waveAtime );

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > wave024Xtime )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWave4OfDegree( oneDegLower ) )
                        {
                            var bars = GetDatabarsRepository( period );

                            return GetWavePoints( waveScenarioNo, bars, period, wave.Key, waveAtime, currentWaveDegree );
                        }
                    }
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_smallWave4B_WaveA( int waveScenarioNo, TimeSpan period, long waveAtime, ElliottWaveCycle currentWaveDegree )
        {
            ((DateTime, float) wave4, (DateTime, float) waveA ) small4_A = GetPoints_smallWave4_WaveA( waveScenarioNo, period, waveAtime, currentWaveDegree );

            if ( default != small4_A )
            {
                var hews = GetElliottWavesDictionary( period );

                var previousWaves = hews.GetElementsRightOf( waveAtime );
                var wave4time     = small4_A.wave4.Item1.ToLinuxTime( );
                var oneDegLower   = currentWaveDegree - GlobalConstants.OneWaveCycle;

                foreach ( var wave in previousWaves )
                {
                    if ( wave.Key > wave4time )
                    {
                        ref var hew = ref wave.Value.GetWaveFromScenario( waveScenarioNo );

                        if ( hew.HasWaveBOfDegree( oneDegLower ) )
                        {
                            var bars = GetDatabarsRepository( period );

                            return GetWavePoints( waveScenarioNo, bars, period, wave.Key, waveAtime, currentWaveDegree );
                        }
                    }
                }
            }

            return default;
        }



        public PooledList<SBar> GetImportanceWavePointsForWave3( int waveScenarioNo, long wave2CTime, ElliottWaveCycle currentWaveDegree )
        {
            return GetImportanceWavePointsForWave3( waveScenarioNo, _currentActiveTimeFrame, wave2CTime, currentWaveDegree );
        }

        public PooledList<SBar> GetImportanceWavePointsForWave3( int waveScenarioNo, TimeSpan period, long wave2CTime, ElliottWaveCycle currentWaveDegree )
        {
            PooledList< SBar > output = new PooledList< SBar >( );

            ( (DateTime, float PeakTrough ) wave0, (DateTime, float PeakTrough ) wave1C ) pts = GetPoints_Wave0_Wave1C_ForWave2( waveScenarioNo, period, wave2CTime, currentWaveDegree );

            bool uptrend = pts.wave1C.PeakTrough > pts.wave0.PeakTrough ? false : true;

            var waveImportance = GetWaveImportanceDictionary( period );
            var bars = GetDatabarsRepository( period );

            var nextWaveImportance = waveImportance.GetElementsRightOf( wave2CTime );

            ref SBar bar2 = ref bars.GetBarByTime( wave2CTime );

            if ( bar2 != SBar.EmptySBar )
            {
                if ( uptrend )
                {
                    double lowest = bar2.High;

                    foreach ( var wav in nextWaveImportance )
                    {
                        if ( wav.Value.Signal == TASignal.WAVE_TROUGH )
                        {
                            ref SBar bar = ref bars.GetBarByTime( wav.Key );

                            if ( bar != SBar.EmptySBar )
                            {
                                if ( bar.Low < lowest )
                                {
                                    lowest = bar.Low;

                                    output.Add( bar );
                                }
                            }
                        }
                        else
                        {
                            ref SBar bar = ref bars.GetBarByTime( wav.Key );

                            if ( bar != SBar.EmptySBar )
                            {
                                if ( bar.High > bar2.High )
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    double highest = bar2.Low;

                    foreach ( var wav in nextWaveImportance )
                    {
                        if ( wav.Value.Signal == TASignal.WAVE_PEAK )
                        {
                            ref SBar bar = ref bars.GetBarByTime( wav.Key );

                            if ( bar != SBar.EmptySBar )
                            {

                                if ( bar.High > highest )
                                {
                                    highest = bar.High;
                                    output.Add( bar );
                                }
                            }
                        }
                        else
                        {
                            ref SBar bar = ref bars.GetBarByTime( wav.Key );

                            if ( bar != SBar.EmptySBar )
                            {

                                if ( bar.Low < bar2.Low )
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return output;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_WaveA_WaveC_ForWaveX( int waveScenarioNo, long waveXBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave0XBarTime = FindPreviousWave0X( waveScenarioNo,waveXBarTime, currentWaveDegree );

            if ( -1 == waveXBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, wave0XBarTime, waveXBarTime, currentWaveDegree ) )
            {
                var waveCtime = FindPreviousWaveC( waveScenarioNo, waveXBarTime, currentWaveDegree );

                if ( -1 == waveCtime )
                {
                    return default;
                }

                if ( NoHigherWaveBetween( waveScenarioNo, waveCtime, wave0XBarTime, currentWaveDegree ) )
                {
                    if ( wave0XBarTime == waveCtime )
                    {
                        return ( GetSpecialWavePoints( waveScenarioNo, _bars, _currentActiveTimeFrame, wave0XBarTime, waveCtime, currentWaveDegree ) );
                    }

                    return ( GetWavePoints( waveScenarioNo, wave0XBarTime, waveCtime, currentWaveDegree ) );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_WaveA_WaveC_ForWaveX( int waveScenarioNo, TimeSpan period, long waveXBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var wave0XBarTime = FindPreviousWave0X( waveScenarioNo, period, waveXBarTime, currentWaveDegree );

            if ( -1 == waveXBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave0XBarTime, waveXBarTime, currentWaveDegree ) )
            {
                var waveCtime = FindPreviousWaveC( waveScenarioNo, period, waveXBarTime, currentWaveDegree );

                if ( -1 == waveCtime )
                {
                    return default;
                }

                if ( NoHigherWaveBetween( waveScenarioNo, period, waveCtime, wave0XBarTime, currentWaveDegree ) )
                {
                    var bars = GetDatabarsRepository( period );

                    if ( wave0XBarTime == waveCtime )
                    {
                        return ( GetSpecialWavePoints( waveScenarioNo, bars, period, wave0XBarTime, waveCtime, currentWaveDegree ) );
                    }

                    return ( GetWavePoints( waveScenarioNo, bars, period, wave0XBarTime, waveCtime, currentWaveDegree ) );
                }
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_WaveX_WaveY_ForWaveX( int waveScenarioNo, long waveYtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveXBarTime = FindBeginningWaveOfWaveY( waveScenarioNo, waveYtime, currentWaveDegree );

            if ( -1 == waveXBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, waveXBarTime, waveYtime, currentWaveDegree ) )
            {
                if ( waveXBarTime == waveYtime )
                {
                    return ( GetSpecialWavePoints( waveScenarioNo, _bars, _currentActiveTimeFrame, waveXBarTime, waveYtime, currentWaveDegree ) );
                }

                return ( GetWavePoints( waveScenarioNo, waveXBarTime, waveYtime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_WaveX_WaveY_ForWaveX( int waveScenarioNo, TimeSpan period, long waveYtime, ElliottWaveCycle currentWaveDegree )
        {
            var waveXBarTime = FindBeginningWaveOfWaveY( waveScenarioNo, period, waveYtime, currentWaveDegree );

            if ( -1 == waveXBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, waveXBarTime, waveYtime, currentWaveDegree ) )
            {
                if ( waveXBarTime == waveYtime )
                {
                    return ( GetSpecialWavePoints( waveScenarioNo, _bars, _currentActiveTimeFrame, waveXBarTime, waveYtime, currentWaveDegree ) );
                }

                return ( GetWavePoints( waveScenarioNo, waveXBarTime, waveYtime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_WaveW_ForWaveX( int waveScenarioNo, long waveWBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var waveWBeginningBarTime = FindBeginningWaveOfWaveW( waveScenarioNo, waveWBarTime, currentWaveDegree );

            if ( -1 == waveWBeginningBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, waveWBeginningBarTime, waveWBarTime, currentWaveDegree ) )
            {
                if ( waveWBeginningBarTime == waveWBarTime )
                {
                    return ( GetSpecialWavePoints( waveScenarioNo, _bars, _currentActiveTimeFrame, waveWBeginningBarTime, waveWBarTime, currentWaveDegree ) );
                }

                return ( GetWavePoints( waveScenarioNo, waveWBeginningBarTime, waveWBarTime, currentWaveDegree ) );
            }

            return default;
        }

        public ((DateTime, float), (DateTime, float)) GetPoints_Wave0_WaveW_ForWaveX( int waveScenarioNo, TimeSpan period, long waveWBarTime, ElliottWaveCycle currentWaveDegree )
        {
            var waveWBeginningBarTime = FindBeginningWaveOfWaveW( waveScenarioNo, period, waveWBarTime, currentWaveDegree );

            if ( -1 == waveWBeginningBarTime )
            {
                return default;
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, waveWBeginningBarTime, waveWBarTime, currentWaveDegree ) )
            {
                var bars = GetDatabarsRepository( period );

                if ( waveWBeginningBarTime == waveWBarTime )
                {
                    return ( GetSpecialWavePoints( waveScenarioNo, bars, period, waveWBeginningBarTime, waveWBarTime, currentWaveDegree ) );
                }

                return ( GetWavePoints( waveScenarioNo, bars, period, waveWBeginningBarTime, waveWBarTime, currentWaveDegree ) );
            }

            return default;
        }

        public (WaveInfo, WaveInfo) GetSpecialbarTopAndBottomHighestWaves( int waveScenarioNo, long specailBarTime )
        {
            ( WaveInfo, WaveInfo ) output = default;

            if ( !_hews.ContainsKey( specailBarTime ) )
            {
                return default;
            }

            var specialBarHew = _hews[ specailBarTime ];

            ref var hew = ref specialBarHew.GetWaveFromScenario( waveScenarioNo );

            var firstWave = hew.GetFirstWave( );

            var secondaryWave = hew.GetSecondaryWave( );

            if ( firstWave.HasValue && secondaryWave.HasValue )
            {
                output = (firstWave.Value, secondaryWave.Value);
            }

            return output;
        }

        public bool SyncWavesDownSmallerTimeFrame( int waveScenarioNo, TimeSpan currentTimeFrame, bool recursive )
        {
            var periodString = FinancialHelper.GetPeriodId( currentTimeFrame );

            var hews = GetElliottWavesDictionary( currentTimeFrame );

            var output = new PooledList< DbElliottWave >( );

            foreach ( var eWave in hews )
            {
                output.Add( eWave.Value );
            }

            TimeSpan lowerTimeSpan = TimeSpan.MinValue;

            var aa =  ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                lowerTimeSpan = aa.GetOneWaveTimeSpanLower( currentTimeFrame );
            }
            else
            {
                return false;
            }

            if ( output.Count > 0 )
            {
                if ( lowerTimeSpan != TimeSpan.Zero )
                {
                    var bars = GetDatabarsRepository( lowerTimeSpan );

                    foreach ( DbElliottWave dbHewHTF in output )
                    {
                        long timeOfHigherTF = dbHewHTF.StartDate;

                        var position = dbHewHTF.WaveLabelPosition;

                        if ( position == WaveLabelPosition.TOP ) // we need to find the highest bar for the day
                        {
                            ref SBar bar = ref bars.GetHighestBarOfTheRange( dbHewHTF.BeginTimeUTC, currentTimeFrame);

                            if ( bar != SBar.EmptySBar )
                            {
                                bar.MergeHigherTimeFrameWaves( waveScenarioNo, dbHewHTF.GetBitsFromScenario( waveScenarioNo ).Value );

                                UpdateWaveInManager( currentTimeFrame, timeOfHigherTF, lowerTimeSpan, bar.LinuxTime, dbHewHTF, ref bar );
                            }
                        }
                        else if ( position == WaveLabelPosition.BOTTOM ) // we need to find the lowest bar for the day
                        {
                            ref SBar bar = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, currentTimeFrame);

                            if ( bar != SBar.EmptySBar )
                            {
                                bar.MergeHigherTimeFrameWaves( waveScenarioNo, dbHewHTF.GetBitsFromScenario( waveScenarioNo ).Value );

                                UpdateWaveInManager( currentTimeFrame, timeOfHigherTF, lowerTimeSpan, bar.LinuxTime, dbHewHTF, ref bar );
                            }
                        }
                        else if ( position == WaveLabelPosition.BOTH ) // we need to find the lowest bar for the day
                        {
                            SyncSpecialWaveDownSmallerTimeFrame( waveScenarioNo, dbHewHTF, bars, currentTimeFrame );
                        }
                    }
                }
            }

            if ( lowerTimeSpan != TimeSpan.Zero && recursive )
            {
                SyncWavesDownSmallerTimeFrame( waveScenarioNo, lowerTimeSpan, true );
            }

            return false;
        }

        private void SyncSpecialWaveDownSmallerTimeFrame( int waveScenarioNo,
                                                           DbElliottWave dbHewHTF,
                                                           fxHistoricBarsRepo bars,
                                                           TimeSpan currentTimeFrame
                                                        )
        {
            TimeSpan lowerTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                lowerTimeSpan = aa.GetOneWaveTimeSpanLower( currentTimeFrame );
            }
            else
            {
                return;
            }

            if ( lowerTimeSpan == TimeSpan.Zero )
            {
                throw new ArgumentOutOfRangeException();
            }

            var hews = GetElliottWavesDictionary( lowerTimeSpan );
            ref var hew = ref dbHewHTF.GetWaveFromScenario( waveScenarioNo );            

            var waveByDegree = hew.GetWavesByDegreeDesc( );

            // If the special bar has more than 2 waves, it is very complicated to sync down the lower time frame

            if ( waveByDegree.Count > 2 )
            {
                ComplicatelySyncSpecialWaveDownSmallerTimeFrame( dbHewHTF, bars, currentTimeFrame );
            }
            else if ( waveByDegree.Count == 2 )
            {
                /* -----------------------------------------------------------------------------------------------------------------------
                 * 
                 * Since the special bar only have 2 waves, it will be either a top label and a bottom label
                 * 
                 * -----------------------------------------------------------------------------------------------------------------------
                 */

                var firstWave                       = hew.GetFirstWave( );

                ElliottWaveCycle firstWaveCycle     = ElliottWaveCycle.UNKNOWN;
                ElliottWaveEnum firstWaveName       = ElliottWaveEnum.NONE;
                WaveLabelPosition firstWavePos      = WaveLabelPosition.UNKNOWN;

                ElliottWaveCycle secondaryWaveCycle = ElliottWaveCycle.UNKNOWN;
                ElliottWaveEnum secondaryWaveName   = ElliottWaveEnum.NONE;
                WaveLabelPosition secondaryWavePos  = WaveLabelPosition.UNKNOWN;

                if ( firstWave != null )
                {
                    firstWaveCycle = firstWave.Value.WaveCycle;
                    firstWaveName = firstWave.Value.WaveName;
                    firstWavePos = firstWave.Value.LabelPosition;
                }

                var secondaryWave = hew.GetSecondaryWave( );

                if ( secondaryWave != null )
                {
                    secondaryWaveCycle = secondaryWave.Value.WaveCycle;
                    secondaryWaveName = secondaryWave.Value.WaveName;
                    secondaryWavePos = secondaryWave.Value.LabelPosition;
                }

                if ( firstWaveCycle > secondaryWaveCycle )
                {
                    SyncFirstThenSecond( waveScenarioNo, dbHewHTF, bars, currentTimeFrame, hews, hew, firstWavePos );
                }
                else if ( firstWaveCycle < secondaryWaveCycle )                    // uptrend when the bottom is Higher Wave Degree
                {
                    SyncSecondThenFirst( waveScenarioNo, dbHewHTF, bars, currentTimeFrame, hews, hew, secondaryWavePos );
                }
                else if ( firstWaveCycle == secondaryWaveCycle )
                {
                    if ( firstWaveName.IsNextWave( secondaryWaveName ) )
                    {
                        SyncFirstThenSecond( waveScenarioNo, dbHewHTF, bars, currentTimeFrame, hews, hew, firstWavePos );
                    }
                    else if ( secondaryWaveName.IsNextWave( firstWaveName ) )                  // uptrend when the bottom is wave A, bottom is wave C.
                    {
                        SyncSecondThenFirst( waveScenarioNo, dbHewHTF, bars, currentTimeFrame, hews, hew, secondaryWavePos );
                    }
                    else
                    {
                        throw new InvalidProgramException();
                    }
                }
            }
            else
            {
                hew = ref dbHewHTF.GetWaveFromScenario( waveScenarioNo );

                var position = hew.GetLabelPositionFromHew( );

                var offerId = dbHewHTF.OfferId;

                dbHewHTF.WaveLabelPosition = position;

                if ( position == WaveLabelPosition.TOP ) // we need to find the highest bar for the day
                {
                    ref SBar bar = ref bars.GetHighestBarOfTheRange( dbHewHTF.BeginTimeUTC, currentTimeFrame);

                    if ( bar != SBar.EmptySBar )
                    {
                        bar.MergeWaves( waveScenarioNo, ref hew );

                        if ( !hews.ContainsKey( bar.LinuxTime ) )
                        {
                            var dbHewNew = new DbElliottWave( offerId, bars, ref bar );

                            hews.Add( bar.LinuxTime, dbHewNew );
                        }
                    }
                }
                else if ( position == WaveLabelPosition.BOTTOM ) // we need to find the lowest bar for the day
                {
                    ref SBar bar = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, currentTimeFrame);

                    if ( bar != SBar.EmptySBar )
                    {
                        bar.MergeWaves( waveScenarioNo, ref hew );

                        if ( !hews.ContainsKey( bar.LinuxTime ) )
                        {
                            var dbHewNew = new DbElliottWave( offerId, bars, ref bar );

                            hews.Add( bar.LinuxTime, dbHewNew );
                        }
                    }
                }
            }
        }

        private void SyncSecondThenFirst( int waveScenarioNo, DbElliottWave dbHewHTF, fxHistoricBarsRepo bars, TimeSpan currentTimeFrame, UndoRedoBTreeDictionary<long, DbElliottWave> hews, HewLong elliottWave, WaveLabelPosition secondaryWavePos )
        {
            /* ----------------------------------------------------------------------------------------------------------------------
            * 
            * Since the second Cycle is larger, that mean the second cycle will be the extremum of the chart, the first extremum will
            * be found from the BEGINNING OF THE RANGE to this Extremum
            * 
            * ----------------------------------------------------------------------------------------------------------------------
            */

            if ( secondaryWavePos == WaveLabelPosition.TOP )
            {
                var endBarTime = dbHewHTF.BeginTimeUTC + currentTimeFrame;

                ref SBar bar = ref bars.GetHighestBarOfTheRange( dbHewHTF.BeginTimeUTC, endBarTime);

                if ( bar != SBar.EmptySBar )
                {
                    var topWaves = elliottWave.GetTopWaves( );

                    bar.MergeHigherTimeFrameWaves( waveScenarioNo, topWaves );

                    if ( !hews.ContainsKey( bar.LinuxTime ) )
                    {
                        var dbHewNew = new DbElliottWave( _offerId, bars, ref bar );

                        hews.Add( bar.LinuxTime, dbHewNew );
                    }

                    ref SBar previousExtremumLoweTF = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, bar.BarTime );
                    if ( previousExtremumLoweTF != SBar.EmptySBar )
                    {
                        var bottomWaves = elliottWave.GetBottomWaves( );

                        previousExtremumLoweTF.MergeHigherTimeFrameWaves( waveScenarioNo, bottomWaves );

                        if ( !hews.ContainsKey( previousExtremumLoweTF.LinuxTime ) )
                        {
                            var dbHewNew = new DbElliottWave(  _offerId, bars, ref previousExtremumLoweTF );

                            hews.Add( previousExtremumLoweTF.LinuxTime, dbHewNew );
                        }
                    }
                }
            }
            else if ( secondaryWavePos == WaveLabelPosition.BOTTOM )
            {
                var endBarTime = dbHewHTF.BeginTimeUTC + currentTimeFrame;

                ref SBar bar = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, endBarTime);

                if ( bar != SBar.EmptySBar )
                {
                    var bottomWaves = elliottWave.GetBottomWaves( );

                    bar.MergeHigherTimeFrameWaves( waveScenarioNo, bottomWaves );

                    if ( !hews.ContainsKey( bar.LinuxTime ) )
                    {
                        var dbHewNew = new DbElliottWave( _offerId, bars, ref bar );

                        hews.Add( bar.LinuxTime, dbHewNew );
                    }


                    /* ----------------------------------------------------------------------------------------------------------------------
                     * 
                     * Now that we locate the extremum of this range, the second label has to start from this extremum to the 
                     * end of the range
                     * 
                     * ----------------------------------------------------------------------------------------------------------------------
                     */

                    ref SBar previousExtremumLoweTF = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, bar.BarTime );
                    if ( previousExtremumLoweTF != SBar.EmptySBar )
                    {
                        var topWaves = elliottWave.GetTopWaves( );

                        previousExtremumLoweTF.MergeHigherTimeFrameWaves( waveScenarioNo, topWaves );

                        if ( !hews.ContainsKey( previousExtremumLoweTF.LinuxTime ) )
                        {
                            var dbHewNew = new DbElliottWave( _offerId, bars, ref previousExtremumLoweTF );

                            hews.Add( previousExtremumLoweTF.LinuxTime, dbHewNew );
                        }
                    }
                }
            }
        }

        private void SyncFirstThenSecond( int waveScenarioNo, DbElliottWave dbHewHTF, fxHistoricBarsRepo bars, TimeSpan currentTimeFrame, UndoRedoBTreeDictionary<long, DbElliottWave> hews, HewLong elliottWave, WaveLabelPosition firstWavePos )
        {
            /* ----------------------------------------------------------------------------------------------------------------------
            * 
            * Since the first Cycle is larger, that mean the first cycle will be the extremum of the chart, the second extremum will
            * be found from the first extremum to end of the range
            * 
            * ----------------------------------------------------------------------------------------------------------------------
            */
            var endBarTime = dbHewHTF.BeginTimeUTC + currentTimeFrame;

            if ( firstWavePos == WaveLabelPosition.TOP )
            {
                ref SBar bar = ref bars.GetHighestBarOfTheRange( dbHewHTF.BeginTimeUTC, endBarTime );

                if ( bar != SBar.EmptySBar )
                {
                    var topWaves = elliottWave.GetTopWaves( );

                    if ( !bars.FindWavesInTheRange( topWaves, dbHewHTF.BeginTimeUTC, dbHewHTF.Period ) )
                    {
                        bar.MergeHigherTimeFrameWaves( waveScenarioNo, topWaves );

                        if ( !hews.ContainsKey( bar.LinuxTime ) )
                        {
                            var dbHewNew = new DbElliottWave( _offerId, bars, ref bar );

                            hews.Add( bar.LinuxTime, dbHewNew );
                        }
                    }

                    var bottomWaves = elliottWave.GetBottomWaves( );

                    if ( !bars.FindWavesInTheRange( bottomWaves, dbHewHTF.BeginTimeUTC, dbHewHTF.Period ) )
                    {
                        ref SBar nextExtremumLoweTF = ref bars.GetHighestBarOfTheRange( bar.BarTime, endBarTime );

                        if ( nextExtremumLoweTF != SBar.EmptySBar )
                        {
                            nextExtremumLoweTF.MergeHigherTimeFrameWaves( waveScenarioNo, bottomWaves );

                            if ( !hews.ContainsKey( nextExtremumLoweTF.LinuxTime ) )
                            {
                                var dbHewNew = new DbElliottWave( _offerId, bars, ref nextExtremumLoweTF );

                                hews.Add( nextExtremumLoweTF.LinuxTime, dbHewNew );
                            }
                        }
                    }
                }
            }
            else if ( firstWavePos == WaveLabelPosition.BOTTOM )
            {
                ref SBar bar = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, endBarTime );

                if ( bar != SBar.EmptySBar )
                {
                    var bottomWaves = elliottWave.GetBottomWaves( );

                    if ( !bars.FindWavesInTheRange( bottomWaves, dbHewHTF.BeginTimeUTC, dbHewHTF.Period ) )
                    {
                        bar.MergeHigherTimeFrameWaves( waveScenarioNo, bottomWaves );

                        if ( !hews.ContainsKey( bar.LinuxTime ) )
                        {
                            var dbHewNew = new DbElliottWave( _offerId, bars, ref bar );

                            hews.Add( bar.LinuxTime, dbHewNew );
                        }
                    }

                    /* ----------------------------------------------------------------------------------------------------------------------
                    * 
                    * Now that we locate the extremum of this range, the second label has to start from this extremum to the 
                    * end of the range
                    * 
                    * ----------------------------------------------------------------------------------------------------------------------
                    */
                    var topWaves = elliottWave.GetTopWaves( );

                    if ( !bars.FindWavesInTheRange( topWaves, dbHewHTF.BeginTimeUTC, dbHewHTF.Period ) )
                    {
                        ref SBar nextExtremumLoweTF = ref bars.GetHighestBarOfTheRange( bar.BarTime, endBarTime );

                        if ( nextExtremumLoweTF != SBar.EmptySBar )
                        {
                            nextExtremumLoweTF.MergeHigherTimeFrameWaves( waveScenarioNo, topWaves );

                            if ( !hews.ContainsKey( nextExtremumLoweTF.LinuxTime ) )
                            {
                                var dbHewNew = new DbElliottWave( _offerId, bars, ref nextExtremumLoweTF );

                                hews.Add( nextExtremumLoweTF.LinuxTime, dbHewNew );
                            }
                        }

                    }
                }
            }
        }

        public bool SyncWavesUpHigherTimeFrame( int waveScenarioNo, TimeSpan currentTimeFrame )
        {
            var periodString = FinancialHelper.GetPeriodId( currentTimeFrame );

            var currhews = GetElliottWavesDictionary( currentTimeFrame );

            var output = new PooledList< DbElliottWave >( );

            foreach ( var currentWave in currhews )
            {
                output.Add( currentWave.Value );
            }

            output.Reverse();

            TimeSpan htf = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                htf = aa.GetOneWaveTimeSpanHigher( currentTimeFrame );
            }
            else
            {
                return false;
            }

            if ( htf != TimeSpan.Zero )
            {
                var hews = GetElliottWavesDictionary( htf );

                if ( output.Count > 0 )
                {
                    var bars = GetDatabarsRepository( htf );

                    foreach ( DbElliottWave dbHewLTF in output )
                    {
                        long ltf = dbHewLTF.StartDate;

                        var lowerTimeFrameDT = ltf.FromLinuxTime( );

                        ref SBar bar = ref bars.GetBarContainingTime( lowerTimeFrameDT, htf );

                        if ( bar != SBar.EmptySBar )
                        {
                            ref var hew = ref dbHewLTF.GetWaveFromScenario( waveScenarioNo );

                            bar.MergeWaves( waveScenarioNo, ref hew );

                            if ( hews.ContainsKey( bar.LinuxTime ) )
                            {
                                var dbHew = hews[ bar.LinuxTime ];

                                ref var hew2 = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                                ref var hewHTF = ref bar.GetWaveFromScenario( waveScenarioNo );

                                hew2.CopyFrom( ref hewHTF );
                                                               
                            }
                            else
                            {
                                if ( bar.HasElliottWave )
                                {
                                    var dbHewNew = new DbElliottWave( dbHewLTF.OfferId, bars, ref bar );

                                    hews.Add( bar.LinuxTime, dbHewNew );
                                }
                            }
                        }
                    }
                }

                SyncWavesUpHigherTimeFrame( waveScenarioNo, htf );
            }

            return false;
        }

        public void SyncWavesDownSmallerTimeFrame( int waveScenarioNo, string symbol, TimeSpan higerTimeFramePeriod, TimeSpan smallerTimerFramePeriod )
        {
            var bars = GetDatabarsRepository( smallerTimerFramePeriod );

            if ( bars != null && bars.TotalBarCount > 0 )
            {
                DateTime earliestBarTime = bars.FirstBarTime.Value;
                DateTime latestBarTime = bars.LastBarTime.Value;

                int offerId = SymbolsMgr.Instance.GetOfferId( symbol );

                long lowerBound = earliestBarTime.ToLinuxTime( );
                long upperBound = latestBarTime.ToLinuxTime( );

                string periodId = FinancialHelper.GetPeriodId( higerTimeFramePeriod );

                PooledList< DbElliottWave > output = new PooledList< DbElliottWave >( );

                lock ( _objLock )
                {
                    var resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == offerId && c.StartDate >= lowerBound && c.StartDate <= upperBound && c.Period == periodId ).OrderBy( x => x.StartDate );

                    if ( resultSet != null )
                    {
                        foreach ( DbElliottWave dbElliottWave in resultSet )
                        {
                            output.Add( dbElliottWave );
                        }
                    }
                }

                // This means the higher time frame has 
                if ( output.Count > 0 )
                {
                    foreach ( DbElliottWave dbHewHTF in output )
                    {
                        long timeOfHigherTF = dbHewHTF.StartDate;

                        var position = dbHewHTF.WaveLabelPosition;

                        if ( position == WaveLabelPosition.TOP ) // we need to find the highest bar for the day
                        {
                            ref SBar bar = ref bars.GetHighestBarOfTheRange( dbHewHTF.BeginTimeUTC, higerTimeFramePeriod );
                            if ( bar != SBar.EmptySBar )
                            {
                                long timeOfLowerTF = bar.LinuxTime;

                                bar.MergeHigherTimeFrameWaves( waveScenarioNo, dbHewHTF.GetBitsFromScenario( waveScenarioNo ).Value );

                                UpdateWaveInManager( higerTimeFramePeriod, timeOfHigherTF, smallerTimerFramePeriod, timeOfLowerTF, dbHewHTF, ref bar );
                            }
                        }
                        else if ( position == WaveLabelPosition.BOTTOM ) // we need to find the lowest bar for the day
                        {
                            ref SBar bar = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, higerTimeFramePeriod );

                            if ( bar != SBar.EmptySBar )
                            {
                                long timeOfLowerTF = bar.LinuxTime;
                                bar.MergeHigherTimeFrameWaves( waveScenarioNo, dbHewHTF.GetBitsFromScenario( waveScenarioNo ).Value );

                                UpdateWaveInManager( higerTimeFramePeriod, timeOfHigherTF, smallerTimerFramePeriod, timeOfLowerTF, dbHewHTF, ref bar );
                            }
                        }
                        else if ( position == WaveLabelPosition.BOTH ) // we need to find the lowest bar for the day
                        {
                            var hew = dbHewHTF.GetWaveFromScenario( waveScenarioNo );
                            var firstWave = hew.GetFirstWave( );

                            if ( firstWave.HasValue )
                            {
                                if ( firstWave.Value.LabelPosition == WaveLabelPosition.TOP )
                                {
                                    ref SBar highestBarOfLowerTF = ref bars.GetHighestBarOfTheRange( dbHewHTF.BeginTimeUTC, higerTimeFramePeriod );

                                    if ( highestBarOfLowerTF != SBar.EmptySBar )
                                    {
                                        var topWaves = hew.GetTopWaves( );

                                        highestBarOfLowerTF.MergeHigherTimeFrameWaves( waveScenarioNo, topWaves );

                                        UpdateWaveInManager( higerTimeFramePeriod, timeOfHigherTF, smallerTimerFramePeriod, highestBarOfLowerTF.LinuxTime, dbHewHTF, ref highestBarOfLowerTF );
                                    }

                                    ref SBar lowestBarOfLowerTF = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, higerTimeFramePeriod);

                                    if ( lowestBarOfLowerTF != SBar.EmptySBar )
                                    {
                                        var bottomWave = hew.GetFirstOppositeWave( );

                                        if ( bottomWave.HasValue )
                                        {
                                            var bot = bottomWave.Value;
                                            lowestBarOfLowerTF.MergeHigherTimeFrameWaves( waveScenarioNo, ref bot );

                                            UpdateWaveInManager( higerTimeFramePeriod, timeOfHigherTF, smallerTimerFramePeriod, lowestBarOfLowerTF.LinuxTime, dbHewHTF, ref lowestBarOfLowerTF );
                                        }
                                    }
                                }
                                else if ( firstWave.Value.LabelPosition == WaveLabelPosition.BOTTOM )
                                {
                                    ref SBar lowestBarOfLowerTF = ref bars.GetLowestBarOfTheRange( dbHewHTF.BeginTimeUTC, higerTimeFramePeriod );

                                    if ( lowestBarOfLowerTF != SBar.EmptySBar )
                                    {
                                        var bottomWaves = hew.GetBottomWaves( );

                                        lowestBarOfLowerTF.MergeHigherTimeFrameWaves( waveScenarioNo, bottomWaves );

                                        UpdateWaveInManager( higerTimeFramePeriod, timeOfHigherTF, smallerTimerFramePeriod, lowestBarOfLowerTF.LinuxTime, dbHewHTF, ref lowestBarOfLowerTF );
                                    }

                                    ref SBar highestBarOfLowerTF = ref bars.GetHighestBarOfTheRange( dbHewHTF.BeginTimeUTC, higerTimeFramePeriod);

                                    if ( highestBarOfLowerTF != SBar.EmptySBar )
                                    {
                                        var topwave = hew.GetFirstOppositeWave( );

                                        if ( topwave.HasValue )
                                        {
                                            var top = topwave.Value;
                                            highestBarOfLowerTF.MergeHigherTimeFrameWaves( waveScenarioNo, ref top );

                                            UpdateWaveInManager( higerTimeFramePeriod, timeOfHigherTF, smallerTimerFramePeriod, highestBarOfLowerTF.LinuxTime, dbHewHTF, ref highestBarOfLowerTF );
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ComplicatelySyncSpecialWaveDownSmallerTimeFrame( DbElliottWave dbHewHTF,
                                                                      fxHistoricBarsRepo bars,
                                                                      TimeSpan currentTimeFrame )
        {
            //throw new NotImplementedException( );
        }

        private void UpdateWaveInManager(
                                         TimeSpan higerTimeFramePeriod,
                                         long timeOfHigherTF,
                                         TimeSpan smallerTimerFramePeriod,
                                         long timeOfLowerTF,
                                         DbElliottWave dbHewHTF,
                                         ref SBar bar )
        {
            var hewsHTF = GetElliottWavesDictionary( higerTimeFramePeriod );

            var hews = GetElliottWavesDictionary( smallerTimerFramePeriod );

            if ( !hewsHTF.ContainsKey( timeOfHigherTF ) )
            {
                throw new ArgumentException();
            }

            var bars = GetDatabarsRepository( smallerTimerFramePeriod );

            var offerId = dbHewHTF.OfferId;

            if ( hews.ContainsKey( timeOfLowerTF ) )
            {
                var id = hews[ timeOfLowerTF ].Id;

                var dbHewNew = new DbElliottWave( id, offerId, bars, ref bar );

                hews[ timeOfLowerTF ] = dbHewNew;
            }
            else
            {
                var dbHewNew = new DbElliottWave( 0, offerId, bars, ref bar );

                hews.Add( timeOfLowerTF, dbHewNew );
            }
        }

        public WaveLabelPosition ProcessComplexBar( int waveScenarioNo, TimeSpan responsibleForWhatTimeFrame,
                                                    long rawBarTime,
                                                    ElliottWaveCycle cycle,
                                                    ElliottWaveEnum currentWaveName,
                                                    ref SBar bar )
        {
            TimeSpan searchingTF = TimeSpan.FromMinutes( 5 );

            if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 1 ) )
            {
                DateTime startDateDT = rawBarTime.FromLinuxTime( );
                DateTime upperBoundDT = startDateDT.AddDays( 1 );
                long upperBound = upperBoundDT.ToLinuxTime( );

                var resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == _offerId && c.StartDate >= rawBarTime && c.StartDate <= upperBound && c.Period != "H1" ).OrderBy( x => x.StartDate );

                if ( resultSet == null )
                {
                    resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == _offerId && c.StartDate >= rawBarTime && c.StartDate <= upperBound && c.Period != "m5" ).OrderBy( x => x.StartDate );
                }

                if ( resultSet != null )
                {
                }

                //    searchingTF     = TimeSpan.FromHours( 1 );
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 60 ) )
            {
                DateTime startDateDT = rawBarTime.FromLinuxTime( );
                DateTime upperBoundDT = startDateDT.AddHours( 1 );
                long upperBound = upperBoundDT.ToLinuxTime( );

                var resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == _offerId && c.StartDate >= rawBarTime && c.StartDate <= upperBound && c.Period != "m5" ).OrderBy( x => x.StartDate );

                if ( resultSet == null )
                {
                    resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == _offerId && c.StartDate >= rawBarTime && c.StartDate <= upperBound && c.Period != "m1" ).OrderBy( x => x.StartDate );
                }

                if ( resultSet != null )
                {
                }

                //searchingTF         = TimeSpan.FromMinutes( 5 );
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 15 ) )
            {
                DateTime startDateDT = rawBarTime.FromLinuxTime( );
                DateTime upperBoundDT = startDateDT.AddMinutes( 15 );
                long upperBound = upperBoundDT.ToLinuxTime( );

                var resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == _offerId && c.StartDate >= rawBarTime && c.StartDate <= upperBound && c.Period != "m1" ).OrderBy( x => x.StartDate );

                if ( resultSet != null )
                {
                }
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 5 ) )
            {
                DateTime startDateDT = rawBarTime.FromLinuxTime( );
                DateTime upperBoundDT = startDateDT.AddMinutes( 5 );
                long upperBound = upperBoundDT.ToLinuxTime( );

                var resultSet = _dbElliottWaveRepo.Where( c => c.OfferId == _offerId && c.StartDate >= rawBarTime && c.StartDate <= upperBound && c.Period != "m1" ).OrderBy( x => x.StartDate );

                if ( resultSet != null )
                {
                }

                searchingTF = TimeSpan.FromMinutes( 1 );
            }

            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var dbHew = _hews[ rawBarTime ];

                var hew = new HewLong( dbHew.HarmonicElliottWaveBit );

                // Since this is a complex bar, it should normally be the end of a wave and the beginning of a series of wave counts.
                // Finding the next wave will allow us to see what sequence of wave is missing.

                var dbHewNext = GetNextWaveStructure( waveScenarioNo, rawBarTime );

                var missingWaves = GetMissingWavesBetween( waveScenarioNo, rawBarTime, dbHewNext.StartDate, cycle, ref bar );
            }

            return WaveLabelPosition.UNKNOWN;
        }

        public PooledList<ElliottWaveEnum> GetMissingWavesBetween( int waveScenarioNo, long beginTime,
                                                                    long endTime,
                                                                    ElliottWaveCycle cycle,
                                                                    ref SBar bar )
        {
            PooledList< ElliottWaveEnum > output = new PooledList< ElliottWaveEnum >( );

            if ( _hews.ContainsKey( beginTime ) && _hews.ContainsKey( endTime ) )
            {
                var beginWave = _hews[ beginTime ];

                var endWave = _hews[ endTime ];

                var hew = beginWave.GetWaveFromScenario( waveScenarioNo );

                var beginWaveName = hew.GetWavesAtCycle( cycle );

                var hew2 = endWave.GetWaveFromScenario( waveScenarioNo );
                var endWaveName = hew2.GetWavesAtCycle( cycle );

                var beginWaveHigherName = hew.GetWavesAtCycle( cycle + GlobalConstants.OneWaveCycle );

                if ( beginWaveName.Count > 0 && beginWaveHigherName.Count > 0 && endWaveName.Count > 0 )
                {
                    if ( beginWaveName[ 0 ] == ElliottWaveEnum.WaveC || beginWaveName[ 0 ] == ElliottWaveEnum.Wave1C || beginWaveName[ 0 ] == ElliottWaveEnum.Wave3C || beginWaveName[ 0 ] == ElliottWaveEnum.Wave5C )
                    {
                    }
                }
            }

            return output;
        }

        public void ToggleSmartWaveLabeling( bool enableOrDisable )
        {
            _smartWaveLabeling = enableOrDisable;
        }

        public BTreeDictionary<long, WavePointImportance> GetWaveImportanceDictionary( TimeSpan period )
        {
            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                return _01SecElliottWave;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return _01minElliottWave;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return _04minElliottWave;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return _05minElliottWave;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return _15minElliottWave;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return _30minElliottWave;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return _01HourElliottWave;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                return _02HourElliottWave;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                return _03HourElliottWave;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                return _04HourElliottWave;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                return _06HourElliottWave;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                return _08HourElliottWave;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                return _dailyElliottWave;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                return _weeklyElliottWave;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                return _monthlyElliottWave;
            }

            return null;
        }

        public BTreeDictionary<long, WavePointImportance> GetAscendingWaveImportanceClone( TimeSpan period )
        {
            lock ( _zigZagLock )
            {
                if ( period == TimeSpan.FromSeconds( 1 ) )
                {
                    return ( _01SecElliottWave );
                }
                else if ( period == TimeSpan.FromMinutes( 1 ) )
                {
                    return ( _01minElliottWave );
                }
                else if ( period == TimeSpan.FromMinutes( 4 ) )
                {
                    return ( _04minElliottWave );
                }
                else if ( period == TimeSpan.FromMinutes( 5 ) )
                {
                    return ( _05minElliottWave );
                }
                else if ( period == TimeSpan.FromMinutes( 15 ) )
                {
                    return ( _15minElliottWave );
                }
                else if ( period == TimeSpan.FromMinutes( 30 ) )
                {
                    return ( _30minElliottWave );
                }
                else if ( period == TimeSpan.FromHours( 1 ) )
                {
                    return ( _01HourElliottWave );
                }
                else if ( period == TimeSpan.FromHours( 2 ) )
                {
                    return ( _02HourElliottWave );
                }
                else if ( period == TimeSpan.FromHours( 3 ) )
                {
                    return ( _03HourElliottWave );
                }
                else if ( period == TimeSpan.FromHours( 4 ) )
                {
                    return ( _04HourElliottWave );
                }
                else if ( period == TimeSpan.FromHours( 6 ) )
                {
                    return ( _06HourElliottWave );
                }
                else if ( period == TimeSpan.FromHours( 8 ) )
                {
                    return ( _08HourElliottWave );
                }
                else if ( period == TimeSpan.FromDays( 1 ) )
                {
                    return ( _dailyElliottWave );
                }
                else if ( period == TimeSpan.FromDays( 7 ) )
                {
                    return ( _weeklyElliottWave );
                }
                else if ( period == TimeSpan.FromDays( 30 ) )
                {
                    return ( _monthlyElliottWave );
                }
            }

            return null;
        }

        public BTreeDictionary<long, WavePointImportance> GetGannSwingDictionaryClone( TimeSpan period )
        {
            lock ( _zigZagLock )
            {
                if ( period == TimeSpan.FromSeconds( 1 ) )
                {
                    return ( _01SecGannSwing );
                }
                else if ( period == TimeSpan.FromMinutes( 1 ) )
                {
                    return ( _01MinGannSwing );
                }
                else if ( period == TimeSpan.FromMinutes( 4 ) )
                {
                    return ( _04MinGannSwing );
                }
                else if ( period == TimeSpan.FromMinutes( 5 ) )
                {
                    return ( _05MinGannSwing );
                }
                else if ( period == TimeSpan.FromMinutes( 15 ) )
                {
                    return ( _15MinGannSwing );
                }
                else if ( period == TimeSpan.FromMinutes( 30 ) )
                {
                    return ( _30MinGannSwing );
                }
                else if ( period == TimeSpan.FromHours( 1 ) )
                {
                    return ( _01HourMinGannSwing );
                }
                else if ( period == TimeSpan.FromHours( 2 ) )
                {
                    return ( _02HourMinGannSwing );
                }
                else if ( period == TimeSpan.FromHours( 3 ) )
                {
                    return ( _03HourMinGannSwing );
                }
                else if ( period == TimeSpan.FromHours( 4 ) )
                {
                    return ( _04HourMinGannSwing );
                }
                else if ( period == TimeSpan.FromHours( 6 ) )
                {
                    return ( _06HourMinGannSwing );
                }
                else if ( period == TimeSpan.FromHours( 8 ) )
                {
                    return ( _08HourMinGannSwing );
                }
                else if ( period == TimeSpan.FromDays( 1 ) )
                {
                    return ( _dailyGannSwing );
                }
                else if ( period == TimeSpan.FromDays( 7 ) )
                {
                    return ( _weeklyGannSwing );
                }
                else if ( period == TimeSpan.FromDays( 30 ) )
                {
                    return ( _monthlyGannSwing );
                }
            }

            return null;
        }

        public BTreeDictionary<long, WavePointImportance> GetGannSwingDictionary( TimeSpan period )
        {
            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                return _01SecGannSwing;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return _01MinGannSwing;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return _04MinGannSwing;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return _05MinGannSwing;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return _15MinGannSwing;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return _30MinGannSwing;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return _01HourMinGannSwing;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                return _02HourMinGannSwing;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                return _03HourMinGannSwing;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                return _04HourMinGannSwing;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                return _06HourMinGannSwing;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                return _08HourMinGannSwing;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                return _dailyGannSwing;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                return _weeklyGannSwing;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                return _monthlyGannSwing;
            }

            return null;
        }

        #endregion

        #region 00 Label Positions

        public void InternalAddWaveC( int waveScenarioNo,
                                      TimeSpan responsibleForWhatTimeFrame,
                                      long selectedBarTime,
                                      ElliottWaveCycle waveCycle,
                                      ref SBar bar )
        {
            var beginWaveDB = FindBeginningDbElliottWaveOfThisWaveC(waveScenarioNo, selectedBarTime, waveCycle );

            if ( beginWaveDB == null )
            {
                return;
            }

            var hew = beginWaveDB.GetWaveFromScenario( waveScenarioNo );

            var beginningWaveName = hew.GetFirstHighestWaveInfo().Value.WaveName;
            var beginningWaveTime = beginWaveDB.StartDate;


            ElliottWaveEnum nextWave = ElliottWaveEnum.NONE;

            if ( beginningWaveName != ElliottWaveEnum.NONE )
            {
                switch ( beginningWaveName )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                    {
                        nextWave = ElliottWaveEnum.WaveC;

                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.Wave2:
                    {
                        nextWave = ElliottWaveEnum.Wave3C;

                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                    {
                        nextWave = ElliottWaveEnum.WaveC;

                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.Wave4:
                    {
                        nextWave = ElliottWaveEnum.Wave5C;

                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.Wave5:
                    case ElliottWaveEnum.Wave5C:
                    {
                        nextWave = ElliottWaveEnum.WaveC;

                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.WaveC:
                    {
                        nextWave = ElliottWaveEnum.WaveC;

                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.WaveX:
                    {
                        if ( beginWaveDB.HighestWaveCycle == waveCycle + GlobalConstants.OneWaveCycle )
                        {
                            var prevWYTime = FindPreviousWaveWY(waveScenarioNo, beginningWaveTime, beginWaveDB.HighestWaveCycle );

                            if ( -1 != prevWYTime )
                            {
                                var waveDbWY = _hews[ prevWYTime ];
                                var hew2     = waveDbWY.GetWaveFromScenario( waveScenarioNo );
                                var waveWY   = hew2.GetFirstHighestWaveInfo( );

                                switch ( waveWY.Value.WaveName )
                                {
                                    case ElliottWaveEnum.WaveW:
                                    {
                                        nextWave = ElliottWaveEnum.WaveC;

                                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );

                                        var higherDgWaveName = ElliottWaveEnum.WaveY;

                                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, beginWaveDB.HighestWaveCycle, higherDgWaveName, ref bar );
                                    }
                                    break;

                                    case ElliottWaveEnum.WaveY:
                                    {
                                        nextWave = ElliottWaveEnum.WaveC;

                                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );

                                        var higherDgWaveName = ElliottWaveEnum.WaveZ;

                                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, beginWaveDB.HighestWaveCycle, higherDgWaveName, ref bar );
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            nextWave = ElliottWaveEnum.WaveC;

                            SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                        }

                    }
                    break;


                    default:
                    {
                        nextWave = ElliottWaveEnum.WaveC;

                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, responsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;
                }
            }
        }

        public void ProcessAddingWaveC(
                                        int waveScenarioNo,
                                        long selectedBarTime,
                                        ElliottWaveCycle waveCycle,
                                        TimeSpan ResponsibleForWhatTimeFrame,
                                        ref SBar bar )
        {
            var beginWaveHewInfo = FindBeginningWaveOfThisWaveC( waveScenarioNo, selectedBarTime, waveCycle );

            if ( !beginWaveHewInfo.HasValue )
            {
                return;
            }

            var beginningWave = beginWaveHewInfo.Value.WaveName;

            var nextWave = ElliottWaveEnum.NONE;

            if ( beginningWave != ElliottWaveEnum.NONE )
            {
                switch ( beginningWave )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                    {
                        nextWave = ElliottWaveEnum.WaveC;
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.Wave2:
                    {
                        nextWave = ElliottWaveEnum.Wave3C;
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                    {
                        nextWave = ElliottWaveEnum.WaveC;
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.Wave4:
                    {
                        nextWave = ElliottWaveEnum.Wave5C;
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.Wave5:
                    case ElliottWaveEnum.Wave5C:
                    {
                        nextWave = ElliottWaveEnum.WaveC;
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    case ElliottWaveEnum.WaveX:
                    case ElliottWaveEnum.WaveC:
                    {
                        nextWave = ElliottWaveEnum.WaveC;
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;

                    default:
                    {
                        nextWave = ElliottWaveEnum.WaveC;
                        SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleForWhatTimeFrame, selectedBarTime, waveCycle, nextWave, ref bar );
                    }
                    break;
                }

                return;
            }
        }

        public long GetLastConfirmedRedDotTime( TimeSpan period )
        {
            var waveImportance = GetAscendingWaveImportanceClone( period );

            var waves = waveImportance.Where( x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT );

            if ( waves != null )
            {
                var wavList = waves.ToPooledList();

                if ( wavList.Count >= 3 )
                {
                    var secondLast = wavList[ wavList.Count - 3 ];

                    return secondLast.Key;
                }
            }

            return 0;
        }

        public long GetLinuxTimeOfLastRedDot( TimeSpan period )
        {
            var waveImportance = GetAscendingWaveImportanceClone( period );

            var waves = waveImportance.Where( x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT );

            if ( waves != null )
            {
                var wavList = waves.ToPooledList();

                if ( wavList.Count >= 1 )
                {
                    var secondLast = wavList[ wavList.Count - 1 ];

                    return secondLast.Key;
                }
            }

            return 0;
        }

        public void BuildWavesImportance( fxHistoricBarsRepo bars, TimeSpan period, int extDepth, ThreadSafeDictionary<long, TASignal> extDepthZigZag )
        {
            lock ( _zigZagLock )
            {
                var all = GetWaveImportanceDictionary( period );

                if ( all != null )
                {
                    foreach ( var wavePair in extDepthZigZag )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wavePair.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( all.ContainsKey( wavePair.Key ) )
                            {
                                int currentHighestWaveImportance = all[ wavePair.Key ].WaveImportance;

                                if ( extDepth > currentHighestWaveImportance )
                                {
                                    if ( bar.BarIndex > 873 )
                                    {

                                    }

                                    all[ wavePair.Key ] = new WavePointImportance( period, wavePair.Key, bar.BarIndex, extDepth, WavePointImportanceType.nenZigZag, wavePair.Value );
                                }
                            }
                            else
                            {
                                var waveImportance = new WavePointImportance( period, wavePair.Key, bar.BarIndex, extDepth, WavePointImportanceType.nenZigZag, wavePair.Value );
                                all.Add( wavePair.Key, waveImportance );
                            }
                        }
                    }
                }
            }
        }

        public void BuildWavesImportance( fxHistoricBarsRepo bars, TimeSpan period, int extDepth, IEnumerable<KeyValuePair<long, TASignal>> extDepthZigZag )
        {
            lock ( _zigZagLock )
            {
                var all = GetWaveImportanceDictionary( period );

                if ( all != null )
                {
                    foreach ( var wavePair in extDepthZigZag )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wavePair.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( all.ContainsKey( wavePair.Key ) )
                            {
                                int currentHighestWaveImportance = all[ wavePair.Key ].WaveImportance;

                                if ( extDepth > currentHighestWaveImportance )
                                {
                                    if ( bar.BarIndex > 873 )
                                    {

                                    }

                                    all[ wavePair.Key ] = new WavePointImportance( period, wavePair.Key, bar.BarIndex, extDepth, WavePointImportanceType.nenZigZag, wavePair.Value );
                                }
                            }
                            else
                            {
                                var waveImportance = new WavePointImportance( period, wavePair.Key, bar.BarIndex, extDepth, WavePointImportanceType.nenZigZag, wavePair.Value );
                                all.Add( wavePair.Key, waveImportance );
                            }
                        }
                    }
                }
            }
        }

        public void ResetWavesImportance( fxHistoricBarsRepo bars, TimeSpan period, long last2ndRedTime )
        {
            lock ( _zigZagLock )
            {
                var WaveImportanceDict = GetWaveImportanceDictionary( period );

                var tobechanged = WaveImportanceDict.Where( x => x.Key >= last2ndRedTime );

                foreach ( var changed in tobechanged )
                {
                    changed.Value.WaveImportance = 5;
                }
            }
        }

        //public void ClearWaveImportance( TimeSpan period )
        //{
        //    lock( _zigZagLock )
        //    {
        //        if( period == TimeSpan.FromTicks( 1 ) )
        //        {
        //            _01SecElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromMinutes( 1 ) )
        //        {
        //            _01minElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromMinutes( 4 ) )
        //        {
        //            _04minElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromMinutes( 5 ) )
        //        {
        //            _05minElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromMinutes( 15 ) )
        //        {
        //            _15minElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromMinutes( 30 ) )
        //        {
        //            _30minElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromHours( 1 ) )
        //        {
        //            _01HourElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromHours( 2 ) )
        //        {
        //            _02HourElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromHours( 4 ) )
        //        {
        //            _04HourElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromDays( 1 ) )
        //        {
        //            _dailyElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromDays( 7 ) )
        //        {
        //            _weeklyElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //        else if( period == TimeSpan.FromDays( 30 ) )
        //        {
        //            _monthlyElliottWave = new BTreeDictionary< long, WavePointImportance >( );
        //        }
        //    }
        //}

        public void BuildGannSwingImportance( fxHistoricBarsRepo bars, TimeSpan period, int nthHigherHighOrLowerLowForTrendChange, ThreadSafeDictionary<long, TASignal> elliottWaveZigZag )
        {
            var WaveImportanceDict = GetGannSwingDictionary( period );

            if ( WaveImportanceDict != null )
            {
                lock ( _gannLock )
                {
                    foreach ( var wavePair in elliottWaveZigZag )
                    {
                        ref SBar bar = ref bars.GetBarByTime( wavePair.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( WaveImportanceDict.ContainsKey( wavePair.Key ) )
                            {
                                int currentHighestWaveImportance = WaveImportanceDict[ wavePair.Key ].WaveImportance;

                                if ( nthHigherHighOrLowerLowForTrendChange > currentHighestWaveImportance )
                                {
                                    WaveImportanceDict[ wavePair.Key ] = new WavePointImportance( period, wavePair.Key, bar.BarIndex, nthHigherHighOrLowerLowForTrendChange, WavePointImportanceType.GannSwing, wavePair.Value );
                                }
                            }
                            else
                            {
                                var waveImportance = new WavePointImportance( period, wavePair.Key, bar.BarIndex, nthHigherHighOrLowerLowForTrendChange, WavePointImportanceType.GannSwing, wavePair.Value );
                                WaveImportanceDict.Add( wavePair.Key, waveImportance );
                            }
                        }
                    }
                }
            }

            //if ( zigZagDictionary != null && bars != null )
            //{
            //    lock ( zigZagDictionary )
            //    {
            //        foreach ( var wave in zigZagDictionary )
            //        {
            //            int barIndex = wave.Key;

            //            SBar bar = bars.GetBarByIndex( barIndex );

            //            long barTime = bar.EpochTime;

            //            if ( zigZagDictionary.ContainsKey( barTime ) )
            //            {
            //                int currentHighestWaveDepth = zigZagDictionary [ wavePair.Key ];

            //                if ( extDepth > currentHighestWaveDepth )
            //                {
            //                    zigZagDictionary [ wavePair.Key ] = extDepth;
            //                }
            //            }
            //            else
            //            {
            //                zigZagDictionary.Add( wavePair.Key, extDepth );
            //            }

            //            
            //        }

            //        
            //    }
            //}
        }

        public long GetLowestZigZagBetween( TimeSpan responsibleForWhatTimeFrame,
                                            long startDate,
                                            long endDate )
        {
            var zigZagCount   = new PooledList< long >( );

            ref SBar startBar = ref _bars.GetBarByTime( startDate );
            ref SBar endBar   = ref _bars.GetBarByTime( endDate );

            if ( startBar == SBar.EmptySBar || endBar == SBar.EmptySBar )
                return -1;

            long lowestIndex    = -1;
            double lowestValue = double.MaxValue;

            if ( _bars.IsDownTrend( startDate, endDate ) )
            {
                lowestIndex = endBar.BarIndex;

                lowestValue = endBar.Low;
            }
            else if ( _bars.IsUpTrend( startDate, endDate ) )
            {
                lowestIndex = startBar.BarIndex;

                lowestValue = startBar.Low;
            }

            if ( _selectedWaveImportanceDict.Count > 0 )
            {
                foreach ( var wave in _selectedWaveImportanceDict )
                {
                    long iteratingBarTime = wave.Key;

                    if ( ( startDate < iteratingBarTime ) && ( iteratingBarTime < endDate ) )
                    {
                        ref SBar iteratingBar = ref _bars.GetBarByTime( iteratingBarTime );

                        if ( iteratingBar != SBar.EmptySBar )
                        {

                            if ( iteratingBar.Low < lowestValue )
                            {
                                lowestValue = iteratingBar.Low;
                                lowestIndex = iteratingBar.BarIndex;
                            }
                        }
                    }
                }
            }

            return ( lowestIndex );
        }

        public long GetHighestZigZagBetween( TimeSpan responsibleForWhatTimeFrame,
                                             long startDate,
                                             long endDate )
        {
            var zigZagCount = new PooledList< long >( );

            ref SBar startBar = ref _bars.GetBarByTime( startDate );
            ref SBar endBar = ref _bars.GetBarByTime( endDate );

            if ( startBar == SBar.EmptySBar || endBar == SBar.EmptySBar )
            {
                return -1;
            }

            long highestIndex = -1;
            double highestValue = double.MinValue;

            if ( _bars.IsDownTrend( startDate, endDate ) )
            {
                highestIndex = startBar.BarIndex;
                highestValue = startBar.High;
            }
            else if ( _bars.IsUpTrend( startDate, endDate ) )
            {
                highestIndex = endBar.BarIndex;
                highestValue = endBar.High;
            }

            //SBar iteratingBar = default;

            if ( _selectedWaveImportanceDict.Count > 0 )
            {
                foreach ( var wave in _selectedWaveImportanceDict )
                {
                    long iteratingBarTime = wave.Key;

                    if ( ( startDate < iteratingBarTime ) && ( iteratingBarTime < endDate ) )
                    {
                        ref SBar iteratingBar = ref _bars.GetBarByTime( iteratingBarTime );

                        if ( iteratingBar != SBar.EmptySBar )
                        {
                            if ( iteratingBar.High > highestValue )
                            {
                                highestValue = iteratingBar.High;
                                highestIndex = iteratingBar.BarIndex;
                            }
                        }
                    }
                }
            }

            return ( highestIndex );
        }

        #endregion

        #region General Functions

        public void CycleUpSelectedBar(
                                        int waveScenarioNo,
                                        TimeSpan responsibleForWhatTimeFrame,
                                        long rawBarTime,
                                        ref SBar bar )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var dbHew = _hews[ rawBarTime ];

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                if ( hew.HasElliottWave )
                {
                    var allWaves = hew.GetAllWaves( );

                    var ascendingOrder = allWaves.OrderBy( i => i ).ToPooledList( );
                    var descendingOrder = allWaves.OrderByDescending( i => i ).ToPooledList( );

                    hew.CycleUpAllWaves();

                    bar.UpdateWave( waveScenarioNo, ref hew );

                    dbHew.HighestWaveCycle = hew.GetLastHighestWaveDegree().Value.WaveCycle;

                    Messenger.Default.Send( new HewMessage( dbHew.HighestWaveCycle ) );
                }     
            }
           
        }

        public void CycleDownSelectedBar( int waveScenarioNo,
                                            TimeSpan responsibleForWhatTimeFrame,
                                            long rawBarTime,
                                            ref SBar bar )
        {
            if ( _hews.ContainsKey( rawBarTime ) )
            {
                var dbHew = _hews[ rawBarTime ];

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );
                
                if ( hew.HasElliottWave )
                {

                    var allWaves = hew.GetAllWaves( );

                    var ascendingOrder = allWaves.OrderBy( i => i ).ToPooledList( );
                    var descendingOrder = allWaves.OrderByDescending( i => i ).ToPooledList( );

                    hew.CycleDownAllWaves();

                    bar.UpdateWave( waveScenarioNo, ref hew );

                    dbHew.HighestWaveCycle = hew.GetLastHighestWaveDegree().Value.WaveCycle;

                    
                    Messenger.Default.Send( new HewMessage( dbHew.HighestWaveCycle ) );
                }
            }
        }

        private void CycleDownWavesDownSmallerTimeFrame( int waveScenarioNo,
                                                            TimeSpan currentTimeFrame,
                                                            DateTime barTime,
                                                            PooledList<WaveInfo> allWavesInfo )
        {
            TimeSpan lowerTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                lowerTimeSpan = aa.GetOneWaveTimeSpanLower( currentTimeFrame );
            }
            else
            {
                return;
            }

            if ( lowerTimeSpan != TimeSpan.Zero )
            {
                var hews = GetElliottWavesDictionary( lowerTimeSpan );
                var bars = GetDatabarsRepository( lowerTimeSpan );

                foreach ( WaveInfo waveInfo in allWavesInfo )
                {
                    if ( waveInfo.LabelPosition == WaveLabelPosition.TOP ) // we need to find the highest bar for the day
                    {
                        ref SBar bar = ref bars.GetHighestBarOfTheRange( barTime, currentTimeFrame );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( hews.ContainsKey( bar.LinuxTime ) )
                            {
                                var dbHew = hews[ bar.LinuxTime ];

                                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                                hew.CycleDownWave( waveInfo );
                                bar.UpdateWave( waveScenarioNo, ref hew );
                                dbHew.HighestWaveCycle = hew.GetLastHighestWaveDegree().Value.WaveCycle;
                                hews[ bar.LinuxTime ] = dbHew;
                            }

                            CycleDownWavesDownSmallerTimeFrame( waveScenarioNo, lowerTimeSpan, bar.BarTime, allWavesInfo );
                        }
                    }
                    else if ( waveInfo.LabelPosition == WaveLabelPosition.BOTTOM ) // we need to find the lowest bar for the day
                    {
                        ref SBar bar = ref bars.GetLowestBarOfTheRange( barTime, currentTimeFrame );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( hews.ContainsKey( bar.LinuxTime ) )
                            {
                                var dbHew = hews[ bar.LinuxTime ];

                                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                                hew.CycleDownWave( waveInfo );
                                bar.UpdateWave( waveScenarioNo, ref hew );
                                dbHew.HighestWaveCycle = hew.GetLastHighestWaveDegree().Value.WaveCycle;
                                hews[ bar.LinuxTime ] = dbHew;

                            }

                            CycleDownWavesDownSmallerTimeFrame( waveScenarioNo, lowerTimeSpan, bar.BarTime, allWavesInfo );
                        }
                    }
                }
            }
        }

        private void CycleDownWavesUpLargerTimeFrame( int waveScenarioNo,
                                                        TimeSpan currentTimeFrame,
                                                      DateTime barTime,
                                                      PooledList<WaveInfo> allWavesInfo )
        {
            TimeSpan higherTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                higherTimeSpan = aa.GetOneWaveTimeSpanHigher( currentTimeFrame );
            }
            else
            {
                return;
            }

            if ( higherTimeSpan != TimeSpan.Zero )
            {
                var hewsHTF = GetElliottWavesDictionary( higherTimeSpan );
                var bars = GetDatabarsRepository( higherTimeSpan );

                ref SBar barHigherTF = ref bars.GetBarContainingTime( barTime, higherTimeSpan );

                if ( barHigherTF != SBar.EmptySBar )
                {
                    var minimumToMerge = FinancialHelper.GetMinimumWavesToMerge( higherTimeSpan );

                    foreach ( WaveInfo waveInfo in allWavesInfo )
                    {
                        if ( hewsHTF.ContainsKey( barHigherTF.LinuxTime ) )
                        {
                            var dbHew = hewsHTF[ barHigherTF.LinuxTime ];

                            ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                            if ( hew.HasElliottWave )
                            {
                                hew.CycleDownWave( waveInfo, minimumToMerge );

                                if ( hew.Count > 0 )
                                {
                                    barHigherTF.UpdateWave( waveScenarioNo, ref hew );
                                    dbHew.HighestWaveCycle = hew.GetLastHighestWaveDegree().Value.WaveCycle;

                                    hewsHTF[ barHigherTF.LinuxTime ] = dbHew;
                                }
                                else
                                {
                                    hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                                    if ( hew.Count == 0 )
                                    {
                                        var higherTFRemoveList = GetSelectedRemovedWavesList( higherTimeSpan );

                                        hewsHTF.Remove( barHigherTF.LinuxTime );

                                        if ( higherTFRemoveList != null )
                                        {
                                            var hfTime = barHigherTF.LinuxTime;
                                            var index = higherTFRemoveList.FindIndex( x => x.Equals( hfTime ) );

                                            if ( index == -1 )
                                            {
                                                higherTFRemoveList.Add( barHigherTF.LinuxTime );
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

                if ( barHigherTF != SBar.EmptySBar )
                {
                    CycleDownWavesUpLargerTimeFrame( waveScenarioNo, higherTimeSpan, barHigherTF.BarTime, allWavesInfo );
                }

            }
        }

        private void CycleUpWavesDownSmallerTimeFrame(
                                                        int waveScenarioNo,
                                                        TimeSpan currentTimeFrame,
                                                        DateTime barTime,
                                                        PooledList<WaveInfo> allWavesInfo )
        {
            TimeSpan lowerTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                lowerTimeSpan = aa.GetOneWaveTimeSpanLower( currentTimeFrame );
            }
            else
            {
                return;
            }

            if ( lowerTimeSpan != TimeSpan.Zero )
            {
                var hewLTF = GetElliottWavesDictionary( lowerTimeSpan );
                var bars = GetDatabarsRepository( lowerTimeSpan );

                foreach ( var waveInfo in allWavesInfo )
                {
                    var position = waveInfo.LabelPosition;

                    if ( position == WaveLabelPosition.TOP ) // we need to find the highest bar for the day
                    {
                        ref SBar bar = ref bars.GetHighestBarOfTheRange( barTime, currentTimeFrame );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( hewLTF.ContainsKey( bar.LinuxTime ) )
                            {
                                var dbHew = hewLTF[ bar.LinuxTime ];

                                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                                hew.CycleUpWave( waveInfo );

                                bar.UpdateWave( waveScenarioNo, ref hew );

                                dbHew.HighestWaveCycle = hew.GetLastHighestWaveDegree().Value.WaveCycle;

                                hewLTF[ bar.LinuxTime ] = dbHew;

                            }

                            CycleUpWavesDownSmallerTimeFrame( waveScenarioNo, lowerTimeSpan, bar.BarTime, allWavesInfo );
                        }
                    }
                    else if ( position == WaveLabelPosition.BOTTOM ) // we need to find the lowest bar for the day
                    {
                        ref SBar bar = ref bars.GetLowestBarOfTheRange( barTime, currentTimeFrame );

                        if ( bar != SBar.EmptySBar )
                        {
                            if ( hewLTF.ContainsKey( bar.LinuxTime ) )
                            {
                                var dbHew = hewLTF[ bar.LinuxTime ];

                                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                                hew.CycleUpWave( waveInfo );

                                bar.UpdateWave( waveScenarioNo, ref hew );

                                dbHew.HighestWaveCycle = hew.GetLastHighestWaveDegree().Value.WaveCycle;

                                hewLTF[ bar.LinuxTime ] = dbHew;
                            }

                            CycleUpWavesDownSmallerTimeFrame( waveScenarioNo, lowerTimeSpan, bar.BarTime, allWavesInfo );
                        }
                    }
                }
            }
        }

        private void CycleUpWavesUpLargerTimeFrame(
                                                    int waveScenarioNo,
                                                    TimeSpan currentTimeFrame,
                                                    DateTime barTime,
                                                    PooledList<WaveInfo> allWavesInfo )
        {
            TimeSpan higherTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                higherTimeSpan = aa.GetOneWaveTimeSpanHigher( currentTimeFrame );
            }
            else
            {
                return;
            }

            if ( higherTimeSpan != TimeSpan.Zero )
            {
                var hewsHTF = GetElliottWavesDictionary( higherTimeSpan );
                var bars = GetDatabarsRepository( higherTimeSpan );

                ref SBar bar = ref bars.GetBarContainingTime( barTime, higherTimeSpan );
                if ( bar != SBar.EmptySBar )
                {

                    foreach ( WaveInfo waveInfo in allWavesInfo )
                    {
                        if ( hewsHTF.ContainsKey( bar.LinuxTime ) )
                        {
                            var dbHew = hewsHTF[ bar.LinuxTime ];
                            ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );
                            hew.CycleUpWave( waveInfo );

                            bar.UpdateWave( waveScenarioNo, ref hew );

                            dbHew.HighestWaveCycle = hew.GetLastHighestWaveDegree().Value.WaveCycle;

                            hewsHTF[ bar.LinuxTime ] = dbHew;
                        }
                    }
                }

                if ( bar != SBar.EmptySBar )
                {
                    CycleUpWavesUpLargerTimeFrame( waveScenarioNo, higherTimeSpan, bar.BarTime, allWavesInfo );
                }
            }
        }

        public bool IsSpecialBar( int waveScenarioNo, long selectedBarTime )
        {
            if ( _hews.ContainsKey( selectedBarTime ) )
            {
                var dbHew = _hews[ selectedBarTime ];

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                var waves = hew.GetHighestDegreeWaves( );

                return ( waves.Count > 1 );
            }

            return false;
        }

        public bool IsSpecialBar( int waveScenarioNo, TimeSpan period, long selectedBarTime )
        {
            bool hasTopLabel = false;
            bool hasBotLabel = false;
            var hews = GetElliottWavesDictionary( period );

            if ( hews.ContainsKey( selectedBarTime ) )
            {
                var dbHew = hews[ selectedBarTime ];

                ref var hew = ref dbHew.GetWaveFromScenario( waveScenarioNo );

                var waves = hew.GetHighestDegreeWaves( );

                foreach ( var wave in waves )
                {
                    if ( wave.LabelPosition == WaveLabelPosition.TOP )
                    {
                        hasTopLabel = true;
                    }
                    else if ( wave.LabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        hasBotLabel = true;
                    }
                }

                return ( hasTopLabel && hasBotLabel );
            }

            return false;
        }

        public float GetFibLevelImportant( FibonacciType fibonacciType, float fibonacciLevel )
        {
            switch ( fibonacciType )
            {
                case FibonacciType.Wave2Retracement:
                    break;

                case FibonacciType.Wave4Retracement:
                    break;

                case FibonacciType.WaveCProjection:
                    break;

                case FibonacciType.Wave3Projection:
                {
                    var output = GetWave3ProjectionOccurence( fibonacciLevel );
                }

                break;

                case FibonacciType.Wave3CProjection:
                    break;

                case FibonacciType.Wave5Projection:
                    break;

                case FibonacciType.Wave5CProjection:
                    break;

                case FibonacciType.ABCWaveCProjection:
                    break;

                case FibonacciType.ABCWaveBRetracement:
                    break;

                case FibonacciType.WaveEFBRetracement:
                    break;

                case FibonacciType.WaveTriBRetracement:
                    break;

                case FibonacciType.WaveTriCProjection:
                    break;

                case FibonacciType.WaveTriDProjection:
                    break;

                case FibonacciType.WaveTriEProjection:
                    break;
            }

            return 0;
        }

        public FibLevelOccurance GetWave3ProjectionOccurence( float fibonacciLevel )
        {
            return FibLevelOccurance.Unknown;
        }

        public void GetFirstXProjection( out FibLevelsInfo output )
        {
            output = new FibLevelsInfo( FibonacciType.FirstXProjection, GlobalConstants.FirstXProjectionLevels, GlobalConstants.FirstXProjectionStrength );
        }

        public void GetSecondXProjection( out FibLevelsInfo output )
        {
            output = new FibLevelsInfo( FibonacciType.SecondXProjection, GlobalConstants.SecondXProjectionLevels, GlobalConstants.SecondXProjectionStrength );
        }

        public void GetWaveCProjection( ElliottWaveEnum wave, out FibLevelsInfo output )
        {
            switch ( wave )
            {
                case ElliottWaveEnum.Wave1:                 // We are calculating the Wave C of Wave 1
                case ElliottWaveEnum.Wave1C:                // We are calculating the Wave C of Wave 2
                {
                    output = ( new FibLevelsInfo( FibonacciType.ABCWaveCProjection, GlobalConstants.ABCWaveCProjectionLevels, GlobalConstants.ABCWaveCProjectionStrength ) );
                }
                break;


                case ElliottWaveEnum.Wave3:                 // We are calculating the Wave C of Wave 3                    
                case ElliottWaveEnum.Wave3C:                // We are calculating the Wave C of Wave 2
                {
                    output = ( new FibLevelsInfo( FibonacciType.Wave3CProjection, GlobalConstants.Wave3CProjectionLevels, GlobalConstants.Wave3CProjectionStrength ) );
                }
                break;


                case ElliottWaveEnum.Wave5:                 // We are calculating the Wave C of Wave 5                    
                case ElliottWaveEnum.Wave5C:                // We are calculating the Wave C of Wave 5
                {
                    output = ( new FibLevelsInfo( FibonacciType.Wave5CProjection, GlobalConstants.Wave5CProjectionLevels, GlobalConstants.Wave5CProjectionStrength ) );
                }
                break;


                case ElliottWaveEnum.Wave2:                 // We are calculating the Wave C of Wave 2                
                case ElliottWaveEnum.Wave4:                 // We are calculating the Wave C of Wave 4                                
                case ElliottWaveEnum.WaveC:
                case ElliottWaveEnum.WaveEFC:
                {
                    output = ( new FibLevelsInfo( FibonacciType.WaveCProjection, GlobalConstants.ABCWaveCProjectionLevels, GlobalConstants.ABCWaveCProjectionStrength ) );
                }
                break;

                default:
                {
                    output = ( new FibLevelsInfo( FibonacciType.WaveCProjection, GlobalConstants.ABCWaveCProjectionLevels, GlobalConstants.ABCWaveCProjectionStrength ) );
                }
                break;
            }
        }

        public bool GetFibonacciRetracment( ElliottWaveEnum wave, out FibLevelsInfo output )
        {
            switch ( wave )
            {
                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                case ElliottWaveEnum.Wave2:
                    output = ( new FibLevelsInfo( FibonacciType.Wave2Retracement, GlobalConstants.Wave2RetracementLevels, GlobalConstants.Wave2RetracementStrength ) );
                    break;

                case ElliottWaveEnum.Wave4:
                    output = ( new FibLevelsInfo( FibonacciType.Wave4Retracement, GlobalConstants.Wave4RetracementLevels, GlobalConstants.Wave4RetracementStrength ) );
                    break;

                case ElliottWaveEnum.WaveB:
                    output = ( new FibLevelsInfo( FibonacciType.ABCWaveBRetracement, GlobalConstants.ABCWaveBRetracementLevels, GlobalConstants.ABCWaveBRetracementStrength ) );
                    break;

                case ElliottWaveEnum.WaveTB:
                    output = ( new FibLevelsInfo( FibonacciType.WaveTriBRetracement, GlobalConstants.WaveTriBRetracementLevels, GlobalConstants.WaveTriBRetracementStrength ) );
                    break;

                case ElliottWaveEnum.WaveTC:
                    output = ( new FibLevelsInfo( FibonacciType.WaveTriCProjection, GlobalConstants.WaveTriCRetracementLevels, GlobalConstants.WaveTriCRetracementStrength ) );
                    break;

                case ElliottWaveEnum.WaveTD:
                    output = ( new FibLevelsInfo( FibonacciType.WaveTriDProjection, GlobalConstants.WaveTriDRetracementLevels, GlobalConstants.WaveTriDRetracementStrength ) );
                    break;

                case ElliottWaveEnum.WaveTE:
                    output = ( new FibLevelsInfo( FibonacciType.WaveTriEProjection, GlobalConstants.WaveTriERetracementLevels, GlobalConstants.WaveTriERetracementStrength ) );
                    break;

                case ElliottWaveEnum.WaveEFB:
                    output = ( new FibLevelsInfo( FibonacciType.WaveEFBRetracement, GlobalConstants.WaveEFBRetracementLevels, GlobalConstants.WaveEFBRetracementStrength ) );
                    break;

                default:
                    output = default;
                    return false;
            }

            return true;
        }

        public bool GetFibonacciProjection( ElliottWaveEnum wave, out FibLevelsInfo output )
        {
            switch ( wave )
            {
                case ElliottWaveEnum.Wave3: // We are calculating the Wave C of Wave 3                
                    output = ( new FibLevelsInfo( FibonacciType.Wave3Projection, GlobalConstants.Wave3ProjectionLevels, GlobalConstants.Wave3ProjectionStrength ) );
                    break;

                case ElliottWaveEnum.Wave5: // We are calculating the Wave C of Wave 5                
                    output = ( new FibLevelsInfo( FibonacciType.Wave5Projection, GlobalConstants.Wave5ProjectionLevels, GlobalConstants.Wave5ProjectionStrength ) );
                    break;

                default:
                    output = default;
                    return false;
            }

            return true;
        }

        public WaveVector GetContainingWaveOfThis( int waveScenarioNo, long selectedBarTime, TimeSpan period )
        {
            if ( _hews.ContainsKey( selectedBarTime ) )
            {
                ref var hew = ref _hews[ selectedBarTime ].GetWaveFromScenario( waveScenarioNo );

                var currentWaveHighest = hew.GetLastHighestWaveDegree( );

                var containingDegree = currentWaveHighest.Value.WaveCycle + GlobalConstants.OneWaveCycle;

                var previousWaves = GetPreviousWavePointInfoOfDegreeOrHigher( waveScenarioNo,selectedBarTime, containingDegree );
                var nextWaves = GetNextWavePointInfoOfDegreeOrHigher( waveScenarioNo, selectedBarTime, containingDegree );

                if ( previousWaves.HasValue && nextWaves.HasValue )
                {
                    return new WaveVector( period, previousWaves.Value, nextWaves.Value );
                }
            }

            return null;
        }

        public PooledList<WavePointImportance> FindImportantWavePointsBetween( TimeSpan responsibleForWhatTimeFrame,
                                                                           long startDate,
                                                                           long endDate )
        {
            var zigZagCount = new PooledList< WavePointImportance >( );

            var selectedWaveImportanceDict = GetWaveImportanceDictionary( responsibleForWhatTimeFrame );

            if ( selectedWaveImportanceDict.Count > 0 )
            {
                foreach ( var wave in selectedWaveImportanceDict )
                {
                    long iteratingBarTime = wave.Key;

                    if ( ( startDate < iteratingBarTime ) && ( iteratingBarTime <= endDate ) )
                    {
                        zigZagCount.Add( wave.Value );
                    }
                }
            }

            return ( zigZagCount );
        }

        public PooledList<WavePointImportance> FindImportantWavePointsInclusively( TimeSpan responsibleForWhatTimeFrame,
                                                                           long startDate,
                                                                           long endDate )
        {
            var zigZagCount = new PooledList< WavePointImportance >( );

            var selectedWaveImportanceDict = GetWaveImportanceDictionary( responsibleForWhatTimeFrame );

            if ( selectedWaveImportanceDict.Count > 0 )
            {
                foreach ( var wave in selectedWaveImportanceDict )
                {
                    long iteratingBarTime = wave.Key;

                    if ( ( startDate <= iteratingBarTime ) && ( iteratingBarTime <= endDate ) )
                    {
                        zigZagCount.Add( wave.Value );
                    }
                }
            }

            return ( zigZagCount );
        }

        public PooledList<WavePointImportance> FindGannSwingBetween( TimeSpan responsibleForWhatTimeFrame,
                                                                  long startDate,
                                                                  long endDate )
        {
            var zigZagCount = new PooledList< WavePointImportance >( );

            var zigZagDictionary = GetGannSwingDictionary( responsibleForWhatTimeFrame );

            var bars = GetDatabarsRepository( responsibleForWhatTimeFrame );

            if ( zigZagDictionary.Count > 0 )
            {
                foreach ( var wave in zigZagDictionary )
                {
                    long iteratingBarTime = wave.Key;

                    if ( ( startDate < iteratingBarTime ) && ( iteratingBarTime <= endDate ) )
                    {
                        zigZagCount.Add( wave.Value );
                    }
                }
            }

            return ( zigZagCount );
        }

        public bool BranchWaves( int waveScenarioNo, long rawBarTime, TimeSpan period )
        {
            var bars = GetDatabarsRepository( period );
            var hews = GetElliottWavesDictionary( period );

            if ( hews.Count > 0 )
            {
                var prevWaves = hews.GetElementsRightOf( rawBarTime );

                foreach ( KeyValuePair<long, DbElliottWave> wave in prevWaves )
                {
                    ref SBar prevBar = ref bars.GetBarByTime( wave.Key );

                    if ( prevBar != SBar.EmptySBar )
                    {
                        prevBar.BranchWaves( waveScenarioNo );

                        wave.Value.SyncWithBar( ref prevBar );
                    }
                }

                ref SBar bar = ref bars.GetBarByTime( rawBarTime );

                if ( bar != SBar.EmptySBar )
                {
                    bar.BranchWaves( waveScenarioNo );

                    hews[ rawBarTime ].SyncWithBar( ref bar );
                }

                return true;
            }

            return false;
        }

        public bool ConfirmWavesDownSmallerTimeFrame( int waveScenarioNo, long rawBarTime, TimeSpan currentTimeFrame )
        {
            TimeSpan lowerTF = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                lowerTF = aa.GetOneWaveTimeSpanLower( currentTimeFrame );
            }
            else
            {
                return false;
            }

            if ( lowerTF != TimeSpan.Zero )
            {
                var hews = GetElliottWavesDictionary( lowerTF );
                var bars = GetDatabarsRepository( lowerTF );

                if ( hews.Count > 0 )
                {
                    var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                    foreach ( var previousWave in previousWaves )
                    {
                        ref SBar bar = ref bars.GetBarByTime( previousWave.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            bar.ConfirmWaves( waveScenarioNo );
                        }
                    }

                    return ConfirmWavesDownSmallerTimeFrame( waveScenarioNo, rawBarTime, lowerTF );
                }
            }

            return false;
        }

        public bool ConfirmWavesUpHigherTimeFrame( int waveScenarioNo, long rawBarTime, TimeSpan currentTimeFrame )
        {
            TimeSpan higherTF = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                higherTF = aa.GetOneWaveTimeSpanHigher( currentTimeFrame );
            }
            else
            {
                return false;
            }

            if ( higherTF != TimeSpan.Zero )
            {
                var hews = GetElliottWavesDictionary( higherTF );
                var bars = GetDatabarsRepository( higherTF );

                if ( hews.Count > 0 )
                {
                    var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                    foreach ( var previousWave in previousWaves )
                    {
                        ref SBar bar = ref bars.GetBarByTime( previousWave.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            bar.ConfirmWaves( waveScenarioNo );
                        }
                    }

                    return ConfirmWavesUpHigherTimeFrame( waveScenarioNo, rawBarTime, higherTF );
                }
            }

            return false;
        }

        public bool BranchWavesDownSmallerTimeFrame( int waveScenarioNo, long rawBarTime, TimeSpan currentTimeFrame )
        {
            TimeSpan lowerTF = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                lowerTF = aa.GetOneWaveTimeSpanLower( currentTimeFrame );
            }
            else
            {
                return false;
            }

            if ( lowerTF != TimeSpan.Zero )
            {
                var hews = GetElliottWavesDictionary( lowerTF );
                var bars = GetDatabarsRepository( lowerTF );

                if ( hews.Count > 0 )
                {
                    foreach ( var eWave in hews )
                    {
                        ref SBar bar = ref bars.GetBarByTime( eWave.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            bar.BranchWaves( waveScenarioNo );
                        }
                    }

                    var nextWaves = hews.GetElementsLeftOf( rawBarTime );

                    foreach ( var nextWave in nextWaves )
                    {
                        ref SBar bar = ref bars.GetBarByTime( nextWave.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            bar.SwapWavesAndZeroOut();
                        }
                    }

                    return BranchWavesDownSmallerTimeFrame( waveScenarioNo, rawBarTime, lowerTF );
                }
            }

            return false;
        }

        public bool BranchWavesUpHigherTimeFrame( int waveScenarioNo, long rawBarTime, TimeSpan currentTimeFrame )
        {
            TimeSpan higherTF = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                higherTF = aa.GetOneWaveTimeSpanHigher( currentTimeFrame );
            }
            else
            {
                return false;
            }

            if ( higherTF != TimeSpan.Zero )
            {
                var hews = GetElliottWavesDictionary( higherTF );
                var bars = GetDatabarsRepository( higherTF );

                if ( hews.Count > 0 )
                {
                    foreach ( var eWave in hews )
                    {
                        ref SBar bar = ref bars.GetBarByTime( eWave.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            bar.BranchWaves( waveScenarioNo );
                        }
                    }

                    var nextWaves = hews.GetElementsLeftOf( rawBarTime );

                    foreach ( var nextWave in nextWaves )
                    {
                        ref SBar bar = ref bars.GetBarByTime( nextWave.Key );

                        if ( bar != SBar.EmptySBar )
                        {
                            bar.SwapWavesAndZeroOut();
                        }
                    }

                    return BranchWavesUpHigherTimeFrame( waveScenarioNo, rawBarTime, higherTF );
                }
            }

            return false;
        }

        public PooledList<int> GetTimeLevelsBarIndices( TimeSpecialNumbersType fibLucasGannTimeType, long selectedBarTime, ref int selectedBarIndex )
        {
            var output = new PooledList< int >( );

            int count = -1;

            selectedBarIndex = _bars.GetIndexByTime( selectedBarTime );

            if ( selectedBarIndex > -1 )
            {
                switch ( fibLucasGannTimeType )
                {
                    case TimeSpecialNumbersType.FibSeq:
                    {
                        count = WaveRotation.FibsSeq.Count();

                        for ( int i = 0; i < count; i++ )
                        {
                            output.Add( selectedBarIndex + WaveRotation.FibsSeq[ i ] );
                        }
                    }
                    break;

                    case TimeSpecialNumbersType.LucasSeq:
                    {
                        count = WaveRotation.LucasSeq.Count();

                        for ( int i = 0; i < count; i++ )
                        {
                            output.Add( selectedBarIndex + WaveRotation.LucasSeq[ i ] );
                        }
                    }
                    break;

                    case TimeSpecialNumbersType.GannNumbers:
                    {
                        count = WaveRotation.GannNumbers.Count();

                        for ( int i = 0; i < count; i++ )
                        {
                            output.Add( selectedBarIndex + WaveRotation.GannNumbers[ i ] );
                        }
                    }
                    break;

                    case TimeSpecialNumbersType.FibRatio:
                    {
                        count = WaveRotation.FibRatio.Count();

                        for ( int i = 0; i < count; i++ )
                        {
                            output.Add( selectedBarIndex + WaveRotation.FibRatio[ i ] );
                        }
                    }
                    break;

                    case TimeSpecialNumbersType.SqRootNumbers:
                    {
                    }
                    break;

                    //case SpecialNumbersType.SacredGeo:
                    //{

                    //}
                    //break;
                }
            }

            return output;
        }

        public bool ConfirmWaves( int waveScenarioNo, long rawBarTime, TimeSpan period )
        {
            var bars = GetDatabarsRepository( period );

            if ( _hews.Count > 0 )
            {
                var previousWaves = _hews.GetElementsRightOf( rawBarTime );

                foreach ( var previousWave in previousWaves )
                {
                    ref SBar bar = ref bars.GetBarByTime( previousWave.Key );

                    if ( bar != SBar.EmptySBar )
                    {
                        bar.ConfirmWaves( waveScenarioNo );
                    }
                }

                ConfirmWavesDownSmallerTimeFrame( waveScenarioNo, rawBarTime, period );
                ConfirmWavesUpHigherTimeFrame( waveScenarioNo, rawBarTime, period );

                return true;
            }

            return false;
        }
        #endregion
    }
}
