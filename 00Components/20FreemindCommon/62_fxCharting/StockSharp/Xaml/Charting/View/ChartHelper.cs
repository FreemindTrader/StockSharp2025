using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting.Themes;
using SciChart.Charting;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.IO;
using System.Drawing;
using System.Windows.Media;
using StockSharp.Configuration;

using StockSharp.Algo.Indicators;
using System.Linq;
using StockSharp.Xaml.Charting.IndicatorPainters;
using System.ComponentModel;
using Ecng.Collections;
using StockSharp.Xaml;
using Ecng.Configuration;
using Ecng.Logging;
using StockSharp.Charting;
using StockSharp.BusinessEntities;

namespace StockSharp.Xaml.Charting;

public static class ChartHelper
{
    public static int GetFifoCapcity(TimeSpan period)
    {
        if(period == TimeSpan.FromMinutes(1))
        {
            return 10000;
        } else if(period == TimeSpan.FromMinutes(5))
        {
            return 5000;
        } else if(period == TimeSpan.FromMinutes(15))
        {
            return 3000;
        } else if(period > TimeSpan.FromDays(1))
        {
            return 1000;
        }

        return 2000;
    }

    static ChartHelper()
    {
        // Tony: Can't find RootSection in new version

        //StockSharpSection rootSection = StockSharp.Configuration.Extensions.RootSection;
        //_customIndicators = ( rootSection != null ? rootSection.CustomIndicators.SafeAdd( new Func<IndicatorElement, IndicatorType>( elem => new IndicatorType( elem.Type.To<Type>( ), elem.Painter.To<Type>( ) ) ) )  : null ) ?? Array.Empty<IndicatorType>( );
    }

    private static readonly IndicatorType[ ] _customIndicators = Array.Empty<IndicatorType>();
    private static IndicatorType[ ] _indicatorTypes;

    public static void Draw(this IChart chart, CandlestickUI element, Candle candle)
    {
        if(element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }
        if(candle == null)
        {
            throw new ArgumentNullException(nameof(candle));
        }
        ChartDrawData data = new ChartDrawData();
        data.Group(candle.OpenTime).Add(element, candle);
        chart.Draw(data);
    }

    //[Obsolete( "Use the Draw method instead." )]
    //public static void Draw( this IChart chart,
    //                         DateTimeOffset time,
    //                         IChartElement element,
    //                         object value )
    //{
    //    if( chart == null )
    //    {
    //        throw new ArgumentNullException( nameof( chart ) );
    //    }
    //    chart.Draw( time,
    //                  new PooledDictionary<IChartElement, object>( )
    //    {
    //        {
    //            element,
    //            value
    //        }
    //    } );
    //}

    //[Obsolete( "Use the Draw method instead." )]
    //public static void Draw( this IChart chart,
    //                         DateTimeOffset time,
    //                         IDictionary< IChartElement, object > values )
    //{
    //    if( chart == null )
    //    {
    //        throw new ArgumentNullException( nameof( chart ) );
    //    }
    //    chart.Draw( new RefPair<DateTimeOffset, IDictionary<IChartElement, object>>[ 1 ]
    //    {
    //        RefTuple.Create( time, values )
    //    } );
    //}

    //[Obsolete( "Use the Draw method instead." )]
    //public static void Draw( this IChart chart, IEnumerable< RefPair< DateTimeOffset, IDictionary< IChartElement, object > > > values )
    //{
    //    chart.Draw( new ChartDrawData( values ) );
    //}

    

    public static ChartArea GetArea(this Chart chart, int index)
    {
        if(chart == null)
        {
            throw new ArgumentNullException(nameof(chart));
        }
        if(index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        while(chart.ChartAreas.Count < index + 1)
        {
            ChartArea chartArea = new ChartArea()
            {
                Title = LocalizedStrings.Panel + " " + index,
                XAxisType = chart.XAxisType
            };

            var viewModel = new ScichartSurfaceMVVM(chartArea);
            chartArea.ChartSurfaceViewModel = viewModel;

            chart.ChartAreas.Add(chartArea);
        }
        return chart.ChartAreas[index];
    }

    public static IChart GetChart(this Strategy strategy)
    {
        if(strategy == null)
        {
            throw new ArgumentNullException(nameof(strategy));
        }
        return strategy.Environment.GetValue("Chart", (IChart)null);
    }

    public static void SetChart(this Strategy strategy, IChart chart)
    {
        if(strategy == null)
        {
            throw new ArgumentNullException(nameof(strategy));
        }
        strategy.Environment.SetValue("Chart", chart);
    }

    internal static string Title(this CandleSeries candleSeries_0)
    {
        if(candleSeries_0 != null)
        {
            return string.Format("{0} ({1})", candleSeries_0.Security.Id, candleSeries_0.Arg);
        }
        return string.Empty;
    }

    
    

    public static bool IsVolumeProfileChart(this ChartCandleDrawStyles style)
    {
        if(style != ChartCandleDrawStyles.BoxVolume)
        {
            return style == ChartCandleDrawStyles.ClusterProfile;
        }
        return true;
    }

    //public static IEnumerable<IndicatorType> GetIndicatorTypes()
    //{
    //    if(_indicatorTypes == null)
    //    {
    //        var ns = typeof(IIndicator).Namespace;

    //        var rendererTypes = typeof(Chart).Assembly
    //            .GetTypes()
    //            .Where(
    //                t => !t.IsAbstract &&
    //                    typeof(BaseChartIndicatorPainter).IsAssignableFrom(t) &&
    //                    t.GetAttribute<IndicatorAttribute>() != null)
    //            .ToDictionary(t => t.GetAttribute<IndicatorAttribute>().Type);

    //        _indicatorTypes = typeof(IIndicator).Assembly
    //            .GetTypes()
    //            .Where(
    //                t => t.Namespace == ns &&
    //                    !t.IsAbstract &&
    //                    typeof(IIndicator).IsAssignableFrom(t) &&
    //                    t.GetConstructor(Type.EmptyTypes) != null &&
    //                    t.GetAttribute<BrowsableAttribute>()?.Browsable != false)
    //            .Select(t => new IndicatorType(t/*, rendererTypes.TryGetValue( t )*/))
    //            .Concat(_customIndicators)
    //            .OrderBy(t => t.Name)
    //            .ToArray();
    //    }

    //    return _indicatorTypes;
    //}

    /// <summary>
    /// Fill <see cref="IChart.IndicatorTypes"/> using <see cref="GetIndicatorTypes"/>.
    /// </summary>
    /// <param name="chart">Chart.</param>
    public static void FillIndicators(this IChart chart)
    {
        if(chart == null)
            throw new ArgumentNullException(nameof(chart));

        chart.IndicatorTypes.Clear();
        chart.IndicatorTypes.AddRange(GetIndicatorTypes());
    }

    private static readonly Lazy<IndicatorColorProvider> _indicatorColorProviders = new Lazy<IndicatorColorProvider>();

    public static Stream SaveToImage( this IChart chart )
    {
        if ( chart == null )
            throw new ArgumentNullException( nameof( chart ) );

        if ( chart is Chart elem )
        {
            MemoryStream file = new MemoryStream();
            var image = Ecng.Xaml.XamlHelper.GetImage(elem);
            Ecng.Xaml.XamlHelper.SaveImage( image, ( Stream ) file );
            file.Position = 0L;
            return ( Stream ) file;
        }

        throw new ArgumentException( StringHelper.Put( LocalizedStrings.UnknownType, new object[ 1 ]
        {
      (object) chart.GetType()
        } ), nameof( chart ) );
    }

    internal static string ChartSeriesTitle( this Subscription sub )
    {
        if ( sub == null )
            return string.Empty;

        return StringHelper.Put( LocalizedStrings.ChartSeriesTitleParams, new object[ 2 ]
        {
            sub.SecurityId,
            sub.DataType.Arg
        } );
    }

    public static string CurrChartTheme()
    {
        return ApplicationThemeHelper.ApplicationThemeName.ToChartTheme();
    }

    public static string ToChartTheme( this string appTheme )
    {
        return !appTheme.IsDark() ? "Chrome" : "ExpressionDark";
    }

    public static void UpdateTheme( this IThemeableChart chart )
    {
        if ( chart == null )
            throw new ArgumentNullException( nameof( chart ) );
        chart.ChartTheme = ChartHelper.CurrChartTheme();
    }

    public static IEnumerable<IndicatorType> GetIndicatorTypes()
    {
        return IChartExtensions.IndicatorProvider.All;
    }

    //public static void FillDefaultValues( this OptimizerChart3D chart )
    //{
    //    // TONYFIXME 01

    //    throw new NotImplementedException();

    //    //ChartHelper.SealClass034 skXz9pb7XdulaJda = new ChartHelper.SealClass034();
    //    //skXz9pb7XdulaJda._optimizerChart3d = chart;
    //    //if ( skXz9pb7XdulaJda._optimizerChart3d == null )
    //    //    throw new ArgumentNullException( nameof( chart ) );
    //    //skXz9pb7XdulaJda._optimizerChart3d.X = skXz9pb7XdulaJda._optimizerChart3d.XValues.FirstOrDefault<IChart3DParameter>();
    //    //skXz9pb7XdulaJda._optimizerChart3d.Y = skXz9pb7XdulaJda._optimizerChart3d.YValues.Where<IChart3DParameter>( new Func<IChart3DParameter, bool>( skXz9pb7XdulaJda.\u0023\u003DzDD4NhX\u0024EQrjP\u0024EvGXA\u003D\u003D) ).FirstOrDefault<IChart3DParameter>() ?? skXz9pb7XdulaJda._optimizerChart3d.YValues.FirstOrDefault<IChart3DParameter>();
    //    //skXz9pb7XdulaJda._optimizerChart3d.Z = skXz9pb7XdulaJda._optimizerChart3d.ZValues.FirstOrDefault<IStatisticParameter>();
    //}

    internal static Chart GetDrawingChart( this IChartElement _param0 )
    {
        //StockSharp.Charting.IChartExtensions.TryGetChart(  _param0 );
        // TONYFIXME 03
        throw new NotImplementedException();
        // return ( Chart ) _param0.TryGetChart();
    }

    public static IIndicator TryGetIndicator( this IChartIndicatorElement element )
    {
        // TONYFIXME 01
        throw new NotImplementedException();
        //return element.GetDrawingChart()?.GetIndicatorElement( element );
    }

    public static Subscription TryGetSubscription( this IChartElement element )
    {
        // TONYFIXME 02
        throw new NotImplementedException();
        //return element.GetDrawingChart()?.TryGetSubscription( element );
    }

    public static IIndicatorColorProvider TryColorProvider
    {
        get => ConfigManager.TryGetService<IIndicatorColorProvider>();
    }

    public static IIndicatorColorProvider EnsureColorProvider()
    {
        return ChartHelper.TryColorProvider ?? ( IIndicatorColorProvider ) ChartHelper._indicatorColorProviders.Value;
    }

    //public static void DrawPnL(
    //  this EquityCurveChart chart,
    //  Strategy strategy,
    //  IChartBandElement pnl,
    //  IChartBandElement unrealized,
    //  IChartBandElement commission )
    //{
    //    if ( chart == null )
    //        throw new ArgumentNullException( nameof( chart ) );
    //    if ( strategy == null )
    //        throw new ArgumentNullException( nameof( strategy ) );
    //    if ( pnl == null )
    //        throw new ArgumentNullException( nameof( pnl ) );
    //    if ( unrealized == null )
    //        throw new ArgumentNullException( nameof( unrealized ) );
    //    if ( commission == null )
    //        throw new ArgumentNullException( nameof( commission ) );
    //    IChartDrawData data = chart.CreateData();
    //    data.Group( ( ( BaseLogSource ) strategy ).CurrentTime ).Add( pnl, strategy.PnL ).Add( unrealized, strategy.PnLManager.UnrealizedPnL ).Add( commission, strategy.Commission.GetValueOrDefault() );
    //    chart.Draw( data );
    //}

    [Obsolete( "Use Subscription overload." )]
    public static void AddElement(
      this IWpfChart chart,
      IChartArea area,
      IChartCandleElement element,
      CandleSeries series )
    {
        if ( chart == null )
            throw new ArgumentNullException( nameof( chart ) );
        chart.AddElement( area, element, new Subscription( series ) );
    }

    [Obsolete( "Use Subscription overload." )]
    public static void AddElement(
      this IWpfChart chart,
      IChartArea area,
      IChartIndicatorElement element,
      CandleSeries series,
      IIndicator indicator )
    {
        if ( chart == null )
            throw new ArgumentNullException( nameof( chart ) );
        chart.AddElement( area, element, new Subscription( series ), indicator );
    }

    //private sealed class SealClass034
    //{
    //    public OptimizerChart3D _optimizerChart3d;

    //    internal bool \u0023\u003DzDD4NhX\u0024EQrjP\u0024EvGXA\u003D\u003D(IChart3DParameter _param1)
    //{
    //  return _param1 != this._optimizerChart3d.X;
    //}
}
