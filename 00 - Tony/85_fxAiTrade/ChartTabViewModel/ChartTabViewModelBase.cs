using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using fx.Algorithm;
using fx.Bars;
using fx.Charting;
using fx.Definitions;
using fx.Indicators;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class ChartTabViewModelBase : ViewModelBase, IMVVMDockingProperties, ISupportParameter, ILogSource, IPersistable, ILogReceiver, IChartTabViewModel, ISupportParentViewModel
    {
        protected int _waveScenarioNumber = 0;


        public int WaveScenarioNumber
        {
            get
            {
                return _waveScenarioNumber;
            }

            set
            {
                _waveScenarioNumber = value;
            }
        }

        public void IncreaseWaveScenario() { _waveScenarioNumber++; }

        protected Stopwatch _stopWatch = new Stopwatch();

        protected FreemindIndicator _freemindIndicator;
        public FreemindIndicator FreemindIndicator
        {
            get
            {
                return _freemindIndicator;
            }
        }

        protected ChartExViewModel _chartVM;
        public ChartExViewModel ChartVM
        {
            get
            {
                return _chartVM;
            }

            set
            {
                _chartVM = value;
                InitializeChart();
            }
        }

        protected virtual void InitializeChart()
        {
        }


        protected override void OnParameterChanged( object Parameter )
        {
            if ( Parameter is ChartExViewModel )
            {
                ChartVM = ( ChartExViewModel )Parameter;
            }
        }

        TradeStationViewModel _parentVM = null;

        protected TradeStationViewModel ParentViewModel
        {
            get
            {
                return _parentVM;
            }
            set
            {
                _parentVM = value;
            }
        }

        protected override void OnParentViewModelChanged( object parentViewModel )
        {
            _parentVM = ( TradeStationViewModel )parentViewModel;

            base.OnParentViewModelChanged( parentViewModel );
        }

        public long SelectedCandleBarTime
        {
            get
            {
                return _chartVM.SelectedCandleBarTime;
            }
            set
            {
                _chartVM.SelectedCandleBarTime = value;
            }
        }

        public string TargetName
        {
            get
            {
                return "DocumentsGroup";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected bool _needToCenterOnBar;
        public bool NeedToCenterOnBar
        {
            get { return _needToCenterOnBar; }
            set { SetValue( ref _needToCenterOnBar, value ); }
        }

        #region LogSource
        [Display(
            ResourceType = typeof( LocalizedStrings ),
            Name = LocalizedStrings.IdKey,
            Description = LocalizedStrings.IdKey,
            GroupName = LocalizedStrings.LoggingKey,
            Order = 1000 )]
        [ReadOnly( true )]
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        private string _LogSourceName = "ChartTabViewModel";

        /// <inheritdoc />
        [ReadOnly( true )]
        [Display(
            ResourceType = typeof( LocalizedStrings ),
            Name = LocalizedStrings.NameKey,
            Description = LocalizedStrings.Str7Key,
            GroupName = LocalizedStrings.LoggingKey,
            Order = 1001 )]
        string ILogSource.Name
        {
            get => _LogSourceName;
            set
            {
                if ( value.IsEmpty() )
                    throw new ArgumentNullException( nameof( value ) );

                _LogSourceName = value;
            }
        }

        private ILogSource _parent;

        /// <inheritdoc />
        [Browsable( false )]
        public ILogSource Parent
        {
            get => _parent;
            set
            {
                if ( value == _parent )
                    return;

                if ( value != null && _parent != null )
                    throw new ArgumentException( LocalizedStrings.Str8Params.Put( this, _parent ), nameof( value ) );

                if ( value == this )
                    throw new ArgumentException( LocalizedStrings.CyclicDependency.Put( this ), nameof( value ) );

                _parent = value;

                if ( _parent == null )
                    ParentRemoved?.Invoke( this );
            }
        }

        /// <inheritdoc />
        public event Action<ILogSource> ParentRemoved;
        public event Action<IChartTabViewModel> TabActivated;
        public event Action<IChartTabViewModel> CandlesLoadedEvent;
        public event Action<IChartTabViewModel> DoneDownloadBarsEvent;

        /// <inheritdoc />
        [Display(
            ResourceType = typeof( LocalizedStrings ),
            Name = LocalizedStrings.Str9Key,
            Description = LocalizedStrings.Str9Key + LocalizedStrings.Dot,
            GroupName = LocalizedStrings.LoggingKey,
            Order = 1001 )]
        public virtual LogLevels LogLevel { get; set; } = LogLevels.Inherit;

        /// <inheritdoc />
        [Browsable( false )]
        public virtual DateTimeOffset CurrentTime => TimeHelper.NowWithOffset;

        /// <inheritdoc />
        [Browsable( false )]
        public bool IsRoot { get; set; }

        private Action<LogMessage> _log;

        /// <inheritdoc />
        public event Action<LogMessage> Log
        {
            add => _log += value;
            remove => _log -= value;
        }


        /// <summary>
        /// To call the event <see cref="ILogSource.Log"/>.
        /// </summary>
        /// <param name="message">A debug message.</param>
        protected virtual void RaiseLog( LogMessage message )
        {
            if ( message == null )
                throw new ArgumentNullException( nameof( message ) );

            if ( message.Level < message.Source.LogLevel )
                return;

            _log?.Invoke( message );

            var parent = Parent as ILogReceiver;

            parent?.AddLog( message );
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _LogSourceName;
        }

        void ILogReceiver.AddLog( LogMessage message )
        {
            RaiseLog( message );
        }



        private readonly object _lock = new object();


        public bool IsDisposed { get; private set; }

        public virtual void Dispose()
        {
            lock ( _lock )
            {
                if ( IsDisposed )
                    return;
                IsDisposed = true;
                GC.SuppressFinalize( this );
            }
        }
        #endregion

        /// <summary>
        /// Load settings.
        /// </summary>
        /// <param name="storage">Settings storage.</param>
        public virtual void Load( SettingsStorage storage )
        {
            LogLevel = storage.GetValue( nameof( LogLevel ), LogLevels.Inherit );
        }

        /// <summary>
        /// Save settings.
        /// </summary>
        /// <param name="storage">Settings storage.</param>
        public virtual void Save( SettingsStorage storage )
        {
            storage.SetValue( nameof( LogLevel ), LogLevel.To<string>() );
        }

        public virtual void UnRegisterCommandsAndEvents()
        {

        }

        public virtual void AddWaveOneToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {

        }

        public virtual void AddWaveTwoToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {

        }

        public virtual void AddWaveToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {
        }

        public virtual void AddWaveFourToChart( int waveScenarioNo, ElliottWaveCycle waveCycle )
        {

        }

        public virtual void AddWaveAToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {

        }

        public virtual void AddWaveBToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {

        }

        public virtual void AddWaveCToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {

        }

        public virtual void CycleUpSelectedBar( int waveScenarioNo )
        {

        }

        public virtual void CycleDownSelectedBar( int waveScenarioNo )
        {

        }

        public virtual void RemoveWavesFromManagerAndBar( int waveScenarioNo, TimeSpan period )
        {

        }

        public virtual void AnalyzeWaveTarget()
        {

        }


        public virtual void ClassicWave3Target()
        {

        }

        public virtual void ExtendedWave3Target()
        {

        }

        public virtual void SuperExtendedWave3Target()
        {

        }

        public virtual void WaveCTarget()
        {

        }
        public virtual void Wave4Target()
        {

        }


        public virtual void LockWavesInDB()
        {

        }

        public virtual void AnalysisWave()
        {

        }


        public virtual void MarkPrimary()
        {

        }

        public virtual void CopyBarTimeToClipboard()
        {

        }

        public virtual void FindAndLoadDatabarsLowerTimeFrame( int waveScenarioNo )
        {

        }

        public virtual void FindAndLoadDatabarsHigherTimeFrame( int waveScenarioNo )
        {

        }


        public virtual void StartSimulation()
        {

        }

        public virtual void BranchWaves()
        {

        }

        bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                SetValue( ref _isActive, value );

                if ( _chartVM != null )
                {
                    _chartVM.IsActive = value;
                }
            }
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Since the UI thread will be different from the Disruptor Thread, changing to use Step By Step variable needs to be volatile.
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        volatile bool _stepByStep;
        public bool StepByStep
        {
            get => _stepByStep;
            set
            {
                if ( _stepByStep == value )
                    return;
                _stepByStep = value;
            }
        }


        protected HewManager _hews = null;

        long _centerViewOnThisBarTime = -1;
        public long CenterViewOnThisBarTime
        {
            get { return _centerViewOnThisBarTime; }
            set
            {
                _centerViewOnThisBarTime = value;
            }
        }

        protected fxHistoricBarsRepo _bars;

        public fxHistoricBarsRepo DatabarRepo
        {
            get
            {
                return _bars;
            }
        }

        protected PivotPointsCustom _pivotPointIndicator;
        public PivotPointsCustom PivotPointIndicator
        {
            get
            {
                return _pivotPointIndicator;
            }
        }

        protected ResettableTimer _drawTimer;
        protected string _name = string.Empty;

        protected void FillIndicators()
        {
            var allIndicators = ChartHelper.GetIndicatorTypes();

            ChartVM.IndicatorTypes.Clear();

            CollectionHelper.AddRange( ChartVM.IndicatorTypes, allIndicators );
        }

        public virtual void ReloadCandles()
        {

        }
    }
}
