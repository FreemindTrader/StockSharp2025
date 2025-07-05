// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartHelper
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Logging;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Statistics;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.IndicatorPainters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

#nullable disable
namespace StockSharp.Xaml.Charting;

public static class ChartHelper
{

    private static readonly Lazy<IndicatorColorProvider> _indicatorColorProviders = new Lazy<IndicatorColorProvider>();

    public static Stream SaveToImage( this IChart chart )
    {
        if ( chart == null )
            throw new ArgumentNullException( nameof( chart ) );

        if ( chart is Chart elem )
        {
            MemoryStream file = new MemoryStream();
            elem.GetImage().SaveImage( ( Stream ) file );
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

    public static void FillDefaultValues( this OptimizerChart3D chart )
    {
        ChartHelper.SealClass034 skXz9pb7XdulaJda = new ChartHelper.SealClass034();
        skXz9pb7XdulaJda._optimizerChart3d = chart;
        if ( skXz9pb7XdulaJda._optimizerChart3d == null )
            throw new ArgumentNullException( nameof( chart ) );
        skXz9pb7XdulaJda._optimizerChart3d.X = skXz9pb7XdulaJda._optimizerChart3d.XValues.FirstOrDefault<IChart3DParameter>();
        skXz9pb7XdulaJda._optimizerChart3d.Y = skXz9pb7XdulaJda._optimizerChart3d.YValues.Where<IChart3DParameter>( new Func<IChart3DParameter, bool>( skXz9pb7XdulaJda.\u0023\u003DzDD4NhX\u0024EQrjP\u0024EvGXA\u003D\u003D) ).FirstOrDefault<IChart3DParameter>() ?? skXz9pb7XdulaJda._optimizerChart3d.YValues.FirstOrDefault<IChart3DParameter>();
        skXz9pb7XdulaJda._optimizerChart3d.Z = skXz9pb7XdulaJda._optimizerChart3d.ZValues.FirstOrDefault<IStatisticParameter>();
    }

    internal static Chart GetDrawingChart( this IChartElement _param0 )
    {
        return ( Chart ) _param0.TryGetChart();
    }

    public static IIndicator TryGetIndicator( this IChartIndicatorElement element )
    {
        return element.GetDrawingChart()?.GetIndicatorElement( element );
    }

    public static Subscription TryGetSubscription( this IChartElement element )
    {
        return element.GetDrawingChart()?.TryGetSubscription( element );
    }

    public static IIndicatorColorProvider TryColorProvider
    {
        get => ConfigManager.TryGetService<IIndicatorColorProvider>();
    }

    public static IIndicatorColorProvider EnsureColorProvider()
    {
        return ChartHelper.TryColorProvider ?? ( IIndicatorColorProvider ) ChartHelper._indicatorColorProviders.Value;
    }

    public static void DrawPnL(
      this EquityCurveChart chart,
      Strategy strategy,
      IChartBandElement pnl,
      IChartBandElement unrealized,
      IChartBandElement commission )
    {
        if ( chart == null )
            throw new ArgumentNullException( nameof( chart ) );
        if ( strategy == null )
            throw new ArgumentNullException( nameof( strategy ) );
        if ( pnl == null )
            throw new ArgumentNullException( nameof( pnl ) );
        if ( unrealized == null )
            throw new ArgumentNullException( nameof( unrealized ) );
        if ( commission == null )
            throw new ArgumentNullException( nameof( commission ) );
        IChartDrawData data = chart.CreateData();
        data.Group( ( ( BaseLogSource ) strategy ).CurrentTime ).Add( pnl, strategy.PnL ).Add( unrealized, strategy.PnLManager.UnrealizedPnL ).Add( commission, strategy.Commission.GetValueOrDefault() );
        chart.Draw( data );
    }

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

    private sealed class SealClass034
    {
        public OptimizerChart3D _optimizerChart3d;

        internal bool \u0023\u003DzDD4NhX\u0024EQrjP\u0024EvGXA\u003D\u003D(IChart3DParameter _param1)
    {
      return _param1 != this._optimizerChart3d.X;
    }
}
}
