using Ecng.Collections;
using Ecng.Serialization;
using SciChart.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.Algo.Derivatives;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;


namespace StockSharp.Xaml.Charting
{
    public partial class OptionPositionChartEx : UserControl, IComponentConnector, IPersistable, IThemeableChart
    {
        private readonly PooledDictionary<Security, OptionNowExpLines> _securityOptionLines = new PooledDictionary<Security, OptionNowExpLines>( );
        private Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> _chartParamFunc = _premiumFunc;
        private readonly ChartAnnotation _chartAnnotation = new ChartAnnotation( ) { Type = ChartAnnotationTypes.VerticalLineAnnotation };
        private readonly CachedSynchronizedSet<Security> _options = new CachedSynchronizedSet<Security>( );
        private bool _showExpiration = true;
        private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> _premiumFunc = ( b, d, t ) => b.Premium( t, null, d );
        private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> _deltaFunc = ( b, d, t ) => b.Delta( t, null, d );
        private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> _gammaFunc = ( b, d, t ) => b.Gamma( t, null, d );
        private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> _thetaFunc = ( b, d, t ) => b.Theta( t, null, d );
        private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> _vegaFunc = ( b, d, t ) => b.Vega( t, null, d );
        private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> _rhoFunc = ( b, d, t ) => b.Rho( t, null, d );
        private readonly ScichartSurfaceMVVM _chartPaneViewModel;
        private BasketBlackScholes _basketBlackScholes;
        private OptionNowExpLines _optionNowExpLines;
        private ISecurityProvider _securityProvider;
        private IMarketDataProvider _marketDataProvider;
        private IPositionProvider _positionProvider;
        private Security _security;
        private OptionPositionChartParams _optionPositionChartParams;
        private bool _showSeparated;
        private bool _useBlackModel;


        static OptionPositionChartEx( )
        {
            LicenseManager.CreateInstance( );
        }



        public OptionPositionChartEx( )
        {
            InitializeComponent( );
            _chartPaneViewModel = ( ScichartSurfaceMVVM )Chart.DataContext;
            _chartPaneViewModel.ShowLegend = true;

            var xAxis = _chartPaneViewModel.Area.XAxises.First( );
            var yAxis = _chartPaneViewModel.Area.YAxises.First( );
            
            yAxis.AutoRange = true;
            yAxis.TextFormatting = "0.##";

            var childModifiers = _chartPaneViewModel.ChartModifier.ChildModifiers;
            var modifiersArray = new ChartModifierBase[ 5 ];

            var zoomPanModifier = new ZoomPanModifier( );
            zoomPanModifier.XyDirection = XyDirection.XDirection;
            zoomPanModifier.ClipModeX = ClipMode.None;


            var wheelZoomModifier = new MouseWheelZoomModifier( );
            wheelZoomModifier.XyDirection = XyDirection.XDirection;

            var zoomExtentsModifier = new ZoomExtentsModifier( );
            zoomExtentsModifier.ExecuteOn = ExecuteOn.MouseDoubleClick;

            var yaxisDragModifier = new YAxisDragModifier( );
            yaxisDragModifier.AxisId = "Y";

            var rolloverModifier = new RolloverModifier( );
            rolloverModifier.ShowAxisLabels = false;
            rolloverModifier.UseInterpolation = false;


            modifiersArray[ 0 ] = zoomPanModifier;
            modifiersArray[ 1 ] = wheelZoomModifier;
            modifiersArray[ 2 ] = zoomExtentsModifier;
            modifiersArray[ 3 ] = yaxisDragModifier;
            modifiersArray[ 4 ] = rolloverModifier;

            childModifiers.AddRange( modifiersArray );
            GetChartAreaElments( ).Add( _chartAnnotation );
            bool init = false;

            Loaded += ( s, e ) =>
            {
                if ( init )
                {
                    return;
                }

                init = true;
                var annoData = new ChartDrawData.sAnnotation( ) { IsVisible = new bool?( true ), IsEditable = new bool?( false ), ShowLabel = new bool?( true ), LabelPlacement = new LabelPlacement?( ( LabelPlacement )8 ), Stroke =   Brushes.Orange, Thickness = new Thickness?( new Thickness( 2.0 ) ), VerticalAlignment = new VerticalAlignment?( VerticalAlignment.Stretch ) };
                var drawData = new ChartDrawData( );
                drawData.Add( _chartAnnotation, annoData );
                Chart.Draw( drawData );
            };
        }

        private INotifyList<IChartElement> GetChartAreaElments( )
        {
            return Chart.Area.Elements;
        }

        public string ChartTheme
        {
            get
            {
                return _chartPaneViewModel.SelectedTheme;
            }
            set
            {
                _chartPaneViewModel.SelectedTheme = value;
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return _securityProvider;
            }
            set
            {
                _securityProvider = value;
            }
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return _marketDataProvider;
            }
            set
            {
                _marketDataProvider = value;
            }
        }

        public IPositionProvider PositionProvider
        {
            get
            {
                return _positionProvider;
            }
            set
            {
                _positionProvider = value;
            }
        }

        private IExchangeInfoProvider _exchangeInfoProvider;
        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return _exchangeInfoProvider;
            }
            set
            {
                _exchangeInfoProvider = value;
            }
        }

        public Security UnderlyingAsset
        {
            get
            {
                return _security;
            }
            set
            {
                if ( _security == value )
                {
                    return;
                }

                _security = value;
                Options.Clear( );
                Clear( );
                _basketBlackScholes = null;
            }
        }

        public CachedSynchronizedSet<Security> Options
        {
            get
            {
                return _options;
            }
        }

        public OptionPositionChartParams ChartParam
        {
            get
            {
                return _optionPositionChartParams;
            }
            set
            {
                if ( _optionPositionChartParams == value )
                {
                    return;
                }

                switch ( value )
                {
                    case OptionPositionChartParams.Premium:
                        _chartParamFunc = _premiumFunc;
                        break;
                    case OptionPositionChartParams.Delta:
                        _chartParamFunc = _deltaFunc;
                        break;
                    case OptionPositionChartParams.Gamma:
                        _chartParamFunc = _gammaFunc;
                        break;
                    case OptionPositionChartParams.Theta:
                        _chartParamFunc = _thetaFunc;
                        break;
                    case OptionPositionChartParams.Vega:
                        _chartParamFunc = _vegaFunc;
                        break;
                    case OptionPositionChartParams.Rho:
                        _chartParamFunc = _rhoFunc;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                }
                _optionPositionChartParams = value;
                _basketBlackScholes = null;
            }
        }

        public bool ShowSeparated
        {
            get
            {
                return _showSeparated;
            }
            set
            {
                if ( _showSeparated == value )
                {
                    return;
                }

                _showSeparated = value;
                _basketBlackScholes = null;
            }
        }

        public bool ShowExpiration
        {
            get
            {
                return _showExpiration;
            }
            set
            {
                if ( _showExpiration == value )
                {
                    return;
                }

                _showExpiration = value;
                _basketBlackScholes = null;
            }
        }

        public bool UseBlackModel
        {
            get
            {
                return _useBlackModel;
            }
            set
            {
                if ( _useBlackModel == value )
                {
                    return;
                }

                _useBlackModel = value;
                _basketBlackScholes = null;
            }
        }

        public void Refresh( Decimal? assetPrice = null, DateTimeOffset? currentTime = null, DateTimeOffset? expiryDate = null )
        {
            if ( SecurityProvider == null || MarketDataProvider == null || ( UnderlyingAsset == null || PositionProvider == null ) )
            {
                return;
            }

            if ( _basketBlackScholes == null )
            {
                _basketBlackScholes = new BasketBlackScholes( UnderlyingAsset, MarketDataProvider, ExchangeInfoProvider, PositionProvider );
                Clear( );

                foreach ( Security key in Options.Cache )
                {
                    _basketBlackScholes.InnerModels.Add( UseBlackModel ? new Black( key, SecurityProvider, MarketDataProvider, ExchangeInfoProvider ) : new BlackScholes( key, SecurityProvider, MarketDataProvider, ExchangeInfoProvider ) );

                    if ( ShowSeparated )
                    {
                        string id = key.Id;
                        var optionLines = new OptionNowExpLines( );
                        optionLines.Now( CreateChartLineElement( id + " (NOW)", Colors.Black ) );
                        optionLines.Expiration( ShowExpiration ? CreateChartLineElement( id + " (EXP)", Colors.SandyBrown ) : null );

                        _securityOptionLines.Add( key, optionLines );

                        GetChartAreaElments( ).Add( optionLines.GetNowChartLine( ) );

                        if ( ShowExpiration )
                        {
                            GetChartAreaElments( ).Add( optionLines.GetExpirationChartLine( ) );
                        }
                    }
                }
                if ( !ShowSeparated )
                {
                    var optionLines = new OptionNowExpLines( );
                    optionLines.Now( CreateChartLineElement( "NOW", Colors.Black ) );
                    optionLines.Expiration( ShowExpiration ? CreateChartLineElement( "EXP", Colors.SandyBrown ) : null );
                    _optionNowExpLines = optionLines;

                    GetChartAreaElments( ).Add( _optionNowExpLines.GetNowChartLine( ) );

                    if ( ShowExpiration )
                    {
                        GetChartAreaElments( ).Add( _optionNowExpLines.GetExpirationChartLine( ) );
                    }
                }
            }
            if ( !assetPrice.HasValue )
            {
                assetPrice = ( Decimal? )MarketDataProvider.GetSecurityValue( UnderlyingAsset, ( Level1Fields )28 );
            }

            Unit unitPrice = assetPrice.Value;

            Decimal twentyMinus = !assetPrice.HasValue ? Decimal.Zero : ( decimal )( unitPrice - UnitHelper.Percents( 20 ) );
            Decimal twentyPlus = !assetPrice.HasValue ? Decimal.Zero : ( decimal )( unitPrice + UnitHelper.Percents( 20 ) );

            if ( !currentTime.HasValue )
            {
                currentTime = new DateTimeOffset?( DateTimeOffset.Now );
            }

            if ( !expiryDate.HasValue )
            {
                expiryDate = new DateTimeOffset?( UnderlyingAsset.ExpiryDate ?? DateTimeOffset.Now );
            }

            Decimal? priceStep = UnderlyingAsset.PriceStep;
            Decimal num3 = priceStep ?? new Decimal( 1, 0, 0, false,   2 );

            if ( num3 < Decimal.Zero )
            {
                throw new InvalidOperationException( );
            }

            Chart.Reset( GetChartAreaElments( ) );
            ChartDrawData drawData = new ChartDrawData( );

            if ( ShowSeparated )
            {
                foreach ( Security index in Options.Cache )
                {
                    OptionNowExpLines optionLines = _securityOptionLines[ index ];
                    BlackScholes blackScholes = _basketBlackScholes.InnerModels[ index ];
                    Decimal d = twentyMinus;

                    while ( d < twentyPlus )
                    {
                        var drawDataItems = drawData.Group( Decimal.ToDouble( d ) );
                        Decimal? optionFuncValue = _chartParamFunc( blackScholes, d, currentTime.Value );

                        drawDataItems.Add( optionLines.GetNowChartLine( ), optionFuncValue.HasValue ? Ecng.Common.Converter.To<double>( optionFuncValue.GetValueOrDefault( ) ) : double.NaN, double.NaN );

                        if ( optionLines.GetExpirationChartLine( ) != null )
                        {
                            var element = optionLines.GetExpirationChartLine( );
                            priceStep = _chartParamFunc( blackScholes, d, expiryDate.Value );
                            var priceStepD = priceStep.HasValue ? Ecng.Common.Converter.To<double>( priceStep.GetValueOrDefault( ) ) : double.NaN;

                            drawDataItems.Add( element, priceStepD, double.NaN );
                        }
                        d += num3;
                    }
                }
            }
            else
            {
                Decimal d = twentyMinus;

                while ( d < twentyPlus )
                {
                    var drawDataItems = drawData.Group( Decimal.ToDouble( d ) );
                    var optionFuncValue = _chartParamFunc(   _basketBlackScholes, d, currentTime.Value );

                    drawDataItems.Add( _optionNowExpLines.GetNowChartLine( ), optionFuncValue.HasValue ? Ecng.Common.Converter.To<double>( optionFuncValue.GetValueOrDefault( ) ) : double.NaN, double.NaN );

                    if ( _optionNowExpLines.GetExpirationChartLine( ) != null )
                    {
                        var expLine = _optionNowExpLines.GetExpirationChartLine( );
                        priceStep = _chartParamFunc( _basketBlackScholes, d, expiryDate.Value );
                        var priceStepD = priceStep.HasValue ? Ecng.Common.Converter.To<double>( priceStep.GetValueOrDefault( ) ) : double.NaN;

                        drawDataItems.Add( expLine, priceStepD, double.NaN );
                    }
                    d += num3;
                }
            }

            drawData.Add( _chartAnnotation, new ChartDrawData.sAnnotation( ) { X1 = assetPrice, Y1 = 0.0 } );
            Chart.Draw( drawData );
        }

        private void Clear( )
        {
            GetChartAreaElments( ).RemoveRange( GetChartAreaElments( ).Where( e => ( ( ChartAnnotation )e ) != _chartAnnotation ).ToArray( ) );
            _securityOptionLines.Clear( );
            _optionNowExpLines = null;
        }

        public void Load( SettingsStorage storage )
        {
            ChartTheme = storage.GetValue( "ChartTheme", ChartTheme );
            UseBlackModel = storage.GetValue( "UseBlackModel", ( UseBlackModel ) );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "ChartTheme", ChartTheme );
            storage.SetValue( "UseBlackModel", ( UseBlackModel ) );
        }



        internal static ChartLineElement CreateChartLineElement( string title, Color color )
        {
            var lineElment = new ChartLineElement( );
            lineElment.FullTitle = title;
            lineElment.Color = color;

            return lineElment;
        }



    }
}
