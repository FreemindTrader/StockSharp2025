using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using fx.Definitions;
using StockSharp.Xaml.Charting.ATony;
using StockSharp.Xaml.Charting.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using fx.Algorithm;
using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Core.Extensions;
using System.Windows.Controls;
using StockSharp.BusinessEntities;
using StockSharp.Xaml.Charting.HewFibonacci;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Numerics.CoordinateCalculators;
using fx.Indicators;
using fx.Common;
using StockSharp.Xaml.Charting.CustomAnnotations;
using fx.Bars;

namespace StockSharp.Xaml.Charting
{
    public partial class FreemindCandlestickRenderableSeries : FastCandlestickRenderableSeries
    {
        public static readonly DependencyProperty ShowCandlePatternProperty   = DependencyProperty.Register( nameof( ShowCandlePattern ),   typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnShowCandlePatternProperty ) ) );
        public static readonly DependencyProperty ShowIndicatorResultProperty = DependencyProperty.Register( nameof( ShowIndicatorResult ), typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback(OnShowIndicatorResult) ) );
        public static readonly DependencyProperty ShowExtremesProperty        = DependencyProperty.Register( nameof( ShowExtremes ),        typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnInvalidateParentSurface ) ) );
        public static readonly DependencyProperty ShowMacdExtremesProperty    = DependencyProperty.Register( nameof( ShowMacdExtremes ),    typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnInvalidateParentSurface ) ) );
        public static readonly DependencyProperty ShowTradingTimeProperty     = DependencyProperty.Register( nameof( ShowTradingTime ),     typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnInvalidateParentSurface ) ) );
        public static readonly DependencyProperty ShowElliottWaveProperty     = DependencyProperty.Register( nameof( ShowElliottWave ),     typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnShowElliottWaveProperty ) ) );
        public static readonly DependencyProperty IsSimulationProperty        = DependencyProperty.Register( nameof( IsSimulation ),        typeof(bool),   typeof(FreemindCandlestickRenderableSeries),   new PropertyMetadata(false, new PropertyChangedCallback( OnIsSimulation )));
        public static readonly DependencyProperty ShowMonoWaveProperty        = DependencyProperty.Register( nameof( ShowMonoWave ),        typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnShowMonoWavePropertyChanged ) ) );
        public static readonly DependencyProperty ShowHewDetectionProperty    = DependencyProperty.Register( nameof( ShowHewDetection ),    typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnInvalidateParentSurface ) ) );

        public static readonly DependencyProperty ShowDivergenceProperty      = DependencyProperty.Register( nameof( ShowDivergence ),      typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnInvalidateParentSurface ) ) );
        public static readonly DependencyProperty ShowPriceTimeSignalProperty = DependencyProperty.Register( nameof( ShowPriceTimeSignal ), typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( false, new PropertyChangedCallback( OnInvalidateParentSurface ) ) );
        public static readonly DependencyProperty HighQualityWaveTextProperty = DependencyProperty.Register( nameof( HighQualityWaveText ), typeof( bool ), typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( true,  new PropertyChangedCallback( OnInvalidateParentSurface ) ) );
        public static readonly DependencyProperty SignalMarginProperty        = DependencyProperty.Register( nameof( SignalMargin ),        typeof( int ) , typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( 2,     new PropertyChangedCallback( OnSignalMarginChanged ) ) );
        public static readonly DependencyProperty FifoCapacityProperty        = DependencyProperty.Register( nameof( FifoCapacity ),        typeof( int ) , typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( -1,    new PropertyChangedCallback( OnFifoCapcityChanged ) ) );
        public static readonly DependencyProperty WaveScenarioNoProperty      = DependencyProperty.Register( nameof( WaveScenarioNo ),      typeof( int ),  typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( 0,     new PropertyChangedCallback( OnWaveScenarioNoChanged ) ) );
        public static readonly DependencyProperty WaveImportanceProperty      = DependencyProperty.Register( nameof( WaveImportance ),      typeof( int ),  typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( GlobalConstants.DAILYIMPT,     new PropertyChangedCallback( OnWaveImportanceChanged ) ) );
        public static readonly DependencyProperty WaveCycleProperty           = DependencyProperty.Register( nameof( WaveCycle ),           typeof( ElliottWaveCycle ),  typeof( FreemindCandlestickRenderableSeries ), new PropertyMetadata( ElliottWaveCycle.Miniscule,     new PropertyChangedCallback( OnWaveCycleChanged ) ) );


        private DictionarySlim< ( TASignalSymbol, int ), FxSpritePointMarker>  _pointMarkerCache                  = new DictionarySlim<(TASignalSymbol, int), FxSpritePointMarker>( );
        private DictionarySlim< ( TASignalSymbol, int ), FxSpritePointMarker>  _drawPointMarkers                  = new DictionarySlim<(TASignalSymbol, int), FxSpritePointMarker>( );
        private DictionarySlim< WaveAnnotationKey, PooledList<TextAnnotation>> _waveAnnotationAdded               = new DictionarySlim<WaveAnnotationKey, PooledList<TextAnnotation>>( );
        private DictionarySlim< WaveAnnotationKey, PooledList<TextAnnotation>> _monoWaveAnnotationAdded           = new DictionarySlim<WaveAnnotationKey, PooledList<TextAnnotation>>( );
        private PooledList<(SRlevel, IfxImportantLevel)>                       _confluencelevels                  = new PooledList<(SRlevel, IfxImportantLevel)>( );
        private PooledList<IfxImportantLevel>                                  _selectedAnnotation                = new PooledList<IfxImportantLevel>( );
        private PooledList<IfxImportantLevel>                                  _lockedFibonacciAnnotations        = new PooledList<IfxImportantLevel>( );
        private PooledList<IfxImportantLevel>                                  _lockedPPandFibObjects             = new PooledList<IfxImportantLevel>( );
        private PooledList<(SRlevel, IfxImportantLevel)>                       _lockedAllIfxImportantLines        = new PooledList<(SRlevel, IfxImportantLevel)>( );
        private PooledList<(SRlevel, IfxImportantLevel)>                       _lockedPPLines                     = new PooledList<(SRlevel, IfxImportantLevel)>( );
        private TrendDirection                                                 _lockedImptLinesDirection          = TrendDirection.NoTrend;

        private fxList<Tuple<double, SessionEnum>>                             _sessionTime                       = new fxList<Tuple<double, SessionEnum>>( );
        private fxList<Tuple<int, DateTime>>                                   _tradingSessionEnd                 = new fxList<Tuple<int, DateTime>>( );

        private int              _lastBarIndex                                                                    = -1;
        private int              _lastSelectedBarIndex        = -1;
        private int              _highestSelectedBarIndex     = -1;
        private int              _lastReducedSelectedBarIndex = -1;

        private HewManager       _hews                     = null;
        private PeriodXTaManager _taManager;
        private FontFamily       _fontFamily              = new FontFamily( "Arial" );
        private FontStyle        _fontStyle               = FontStyles.Normal;
        private FontWeight       _fontWeight              = FontWeights.Normal;
        private double           _lastBottomPosition      = 0;
        private double           _lastTopPosition         = 0;
        private int              _lastDrawWaveIndex       = 0;
        //private ElliottWaveCycle _lastWaveCycle           = ElliottWaveCycle.UNKNOWN;
        private int              _elliottWaveFibCount     = 0;
        private long             _lastSelectedBarTime     = -1;
        private SymbolEx         _symbol;
        private TimeSpan         _period;

        private ElliottWaveCycle _minimumToShow           = ElliottWaveCycle.UNKNOWN;
        private ElliottWaveCycle _lowestWaveDeg           = ElliottWaveCycle.MAX;
        private ElliottWaveCycle _higestWaveDeg           = ElliottWaveCycle.UNKNOWN;


        private bool             _needToRedrawElliottWave = false;

        private SBarList         _barList                    = null;

        private bool             _drawFibonacci           = false;
        private bool             _drawMonoWave            = false;
        private bool             _canUpdate               = true;

        private IPen2D           _upWickPen;
        private IPen2D           _downWickPen;
        private IBrush2D         _upBodyBrush;
        private IBrush2D         _downBodyBrush;
        protected int            _candleWidth;

        private bool             _hasMonolines            = false;


        
        private DateTime                            _earliestLockedLevel               = DateTime.MaxValue;
        private SBar                                _extremumBarIndex                  = default;
        private double                              _extremumValue                     = 0;

        //private SellMarkerAnnotation                _tradingEventSmallView             = null;

        private Action< IRenderContext2D, IRenderPassData, IStrokePaletteProvider, IPenManager > _reducedViewFunction = null;
        private Action< IRenderContext2D, IRenderPassData, IStrokePaletteProvider, IFillPaletteProvider, IPenManager > _vanillaViewFunction = null;

        #region Dependency Property


        private static void OnShowIndicatorResult(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fm = d as FreemindCandlestickRenderableSeries;

            fm.SetDrawingFucntion();
        }


        private static void OnIsSimulation(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fm = d as FreemindCandlestickRenderableSeries;            

            fm.SetDrawingFucntion();
        }
        

        private static void OnShowElliottWaveProperty( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var fm = d as FreemindCandlestickRenderableSeries;

            if ( fm.ShowElliottWave == false )
            {
                fm.RemoveAllEWaves();
            }
            

            fm.SetDrawingFucntion();
        }

        private static void OnShowMonoWavePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var fm = d as FreemindCandlestickRenderableSeries;

            if ( fm.ShowMonoWave == false    )
            {
                fm.RemoveAllEWaves();
                fm.RemoveAllMonowavesLines( );
                fm.RemoveAllStructuralLabels();
            }
            

            fm.SetDrawingFucntion();
        }

        private bool _needToDeleteWaves = false;

        public void RemoveAllEWaves()
        {
            var surface = GetParentSurface( );

            if ( surface == null )
            {
                _needToDeleteWaves = true;
                return;
            }

            var annotations = surface.Annotations;

            if ( annotations == null ) return;

            foreach ( var iBarEx in _waveAnnotationAdded )
            {
                var textAnnotations = iBarEx.Value;

                foreach ( var text in textAnnotations )
                {
                    annotations.Remove( text );
                }
            }
            if ( _elliottWaveFibCount > 0 )
            {
                RemoveAllFibonacciTargets();
            }

            _waveAnnotationAdded.Clear();

            _needToDeleteWaves = false;
        }

        private void SetDrawingFucntion()
        {
            if ( ShowMonoWave )
            {
                _reducedViewFunction = DrawReduced;
                _vanillaViewFunction = DrawVanilla;
            }
            else if ( ShowElliottWave )
            {
                _reducedViewFunction = DrawReducedEWave;
                _vanillaViewFunction = DrawVanillaEWave;
            }
            else if ( IsSimulation || ShowIndicatorResult )
            {
                _reducedViewFunction = DrawReduced;
                _vanillaViewFunction = DrawVanilla;
            }
            else
            {
                _reducedViewFunction = DrawReducedBareBone;
                _vanillaViewFunction = DrawVanillaBareBone;
            }
        }

        private static void OnShowCandlePatternProperty( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {

        }

        private static void OnSignalMarginChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {

        }

        private static void OnFifoCapcityChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {

        }

        private static void OnWaveScenarioNoChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {

        }

        private static void OnWaveImportanceChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {

        }

        private static void OnWaveCycleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var fm = d as FreemindCandlestickRenderableSeries;

            fm.RemoveAllEWaves();

            fm.SetMininumWaveCycle( );
        }


        private void SetMininumWaveCycle()
        {
            _minimumToShow = WaveCycle;
        }
        public int WaveScenarioNo
        {
            get
            {
                return ( int ) GetValue( WaveScenarioNoProperty );
            }
            set
            {
                SetValue( WaveScenarioNoProperty, value );
            }
        }

        public int WaveImportance
        {
            get
            {
                return ( int ) GetValue( WaveImportanceProperty );
            }
            set
            {
                SetValue( WaveImportanceProperty, value );
            }
        }

        public ElliottWaveCycle WaveCycle
        {
            get
            {
                return ( ElliottWaveCycle ) GetValue( WaveCycleProperty );
            }
            set
            {
                SetValue( WaveCycleProperty, value );
            }
        }

        public int FifoCapacity
        {
            get
            {
                return ( int ) GetValue( FifoCapacityProperty );
            }
            set
            {
                SetValue( FifoCapacityProperty, value );
            }
        }


        public bool ShowCandlePattern
        {
            get
            {
                return ( bool ) GetValue( ShowCandlePatternProperty );
            }
            set
            {
                SetValue( ShowCandlePatternProperty, value );
            }
        }

        public bool ShowIndicatorResult
        {
            get
            {
                return ( bool ) GetValue( ShowIndicatorResultProperty );
            }
            set
            {
                SetValue( ShowIndicatorResultProperty, value );
            }
        }

        public bool ShowExtremes
        {
            get
            {
                return ( bool ) GetValue( ShowExtremesProperty );
            }
            set
            {
                SetValue( ShowExtremesProperty, value );
            }
        }

        public bool ShowMacdExtremes
        {
            get
            {
                return ( bool ) GetValue( ShowExtremesProperty );
            }
            set
            {
                SetValue( ShowExtremesProperty, value );
            }
        }

        public bool ShowTradingTime
        {
            get
            {
                return ( bool ) GetValue( ShowTradingTimeProperty );
            }
            set
            {
                SetValue( ShowTradingTimeProperty, value );
            }
        }

        public bool ShowElliottWave
        {
            get
            {
                return ( bool ) GetValue( ShowElliottWaveProperty );
            }
            set
            {
                SetValue( ShowElliottWaveProperty, value );
            }
        }

        public bool IsSimulation
        {
            get
            {
                return (bool)GetValue(IsSimulationProperty);
            }
            set
            {
                SetValue(IsSimulationProperty, value);
            }
        }

        public bool ShowMonoWave
        {
            get
            {
                return ( bool ) GetValue( ShowMonoWaveProperty );
            }
            set
            {
                SetValue( ShowMonoWaveProperty, value );
            }
        }

        public bool ShowHewDetection
        {
            get
            {
                return ( bool ) GetValue( ShowHewDetectionProperty );
            }
            set
            {
                SetValue( ShowHewDetectionProperty, value );
            }
        }

        public bool ShowDivergence
        {
            get
            {
                return ( bool ) GetValue( ShowDivergenceProperty );
            }
            set
            {
                SetValue( ShowDivergenceProperty, value );
            }
        }

        public bool ShowPriceTimeSignal
        {
            get
            {
                return ( bool ) GetValue( ShowPriceTimeSignalProperty );
            }
            set
            {
                SetValue( ShowPriceTimeSignalProperty, value );
            }
        }

        

        public bool HighQualityWaveText
        {
            get
            {
                return ( bool ) GetValue( HighQualityWaveTextProperty );
            }
            set
            {
                SetValue( HighQualityWaveTextProperty, value );
            }
        }


        public int SignalMargin
        {
            get
            {
                return ( int ) GetValue( SignalMarginProperty );
            }
            set
            {
                SetValue( SignalMarginProperty, value );
            }
        }

        #endregion

    }
}
