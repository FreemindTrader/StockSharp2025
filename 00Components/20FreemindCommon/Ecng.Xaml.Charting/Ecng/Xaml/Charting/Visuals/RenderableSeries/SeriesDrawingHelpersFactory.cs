// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.SeriesDrawingHelpersFactory
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.StrategyManager;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals.PointMarkers;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    internal static class SeriesDrawingHelpersFactory
    {
        public static IPathContextFactory GetLinesPathFactory( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            if ( renderPassData.XCoordinateCalculator.IsPolarAxisCalculator )
                return ( IPathContextFactory ) new PolarLinesPathContextFactory( renderContext, renderPassData.TransformationStrategy );
            return ( IPathContextFactory ) new LinesClippingDecoratorFactory( ( IPathContextFactory ) new LinesPathContextFactory( renderContext ), renderContext.ViewportSize );
        }

        public static IPathContextFactory GetMountainAreaPathFactory( IRenderContext2D renderContext, IRenderPassData renderPassData, float zeroCoord, double gradientRotationAngle )
        {
            if ( renderPassData.XCoordinateCalculator.IsPolarAxisCalculator )
                return ( IPathContextFactory ) new PolarMountainAreaPathContextFactory( renderContext, renderPassData.TransformationStrategy, ( double ) zeroCoord );
            return ( IPathContextFactory ) new MountainAreaClippingDecoratorFactory( new MountainAreaPathContextFactory( renderContext, renderPassData.IsVerticalChart, ( double ) zeroCoord, gradientRotationAngle ), renderContext.ViewportSize );
        }

        public static IPathContextFactory GetStackedMountainAreaPathFactory( IRenderContext2D renderContext, IRenderPassData renderPassData, double gradientRotationAngle )
        {
            if ( renderPassData.XCoordinateCalculator.IsPolarAxisCalculator )
                return ( IPathContextFactory ) new PolarStackedMountainAreaPathContextFactory( renderContext, renderPassData.TransformationStrategy );
            return ( IPathContextFactory ) new MountainAreaClippingDecoratorFactory( new MountainAreaPathContextFactory( renderContext, renderPassData.IsVerticalChart, gradientRotationAngle ), renderContext.ViewportSize );
        }

        public static IPathContextFactory GetPointMarkerPathFactory( IRenderContext2D renderContext, IRenderPassData renderPassData, IPointMarker pointMarker )
        {
            if ( renderPassData.XCoordinateCalculator.IsPolarAxisCalculator )
                return ( IPathContextFactory ) new PolarPointMarkerPathContextFactory( renderContext, renderPassData.TransformationStrategy, pointMarker );
            return ( IPathContextFactory ) new PointMarkerPathContextFactory( renderContext, pointMarker );
        }

        public static ISeriesDrawingHelper GetSeriesDrawingHelper( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            if ( renderPassData.XCoordinateCalculator.IsPolarAxisCalculator )
                return ( ISeriesDrawingHelper ) new PolarSeriesDrawingHelper( renderContext, renderPassData.TransformationStrategy );
            return ( ISeriesDrawingHelper ) new CartesianSeriesDrawingHelper( renderContext );
        }

        public static IPathContextFactory NewPolarPolygonsFactory( IRenderContext2D renderContext, ITransformationStrategy transformationStrategy )
        {
            Size viewportSize = new Size(360.0, PolarUtil.CalculateViewportRadius(renderContext.ViewportSize));
            return ( IPathContextFactory ) new PolygonClippingDecoratorFactory( ( IPathContextFactory ) new PolarPathDrawingDecoratorFactory( ( IPathContextFactory ) new PolygonPathContextFactory( renderContext ), transformationStrategy ), viewportSize );
        }

        public static IPathContextFactory NewPolarLinesFactory( IRenderContext2D renderContext, ITransformationStrategy transformationStrategy )
        {
            Size viewportSize = new Size(360.0, PolarUtil.CalculateViewportRadius(renderContext.ViewportSize));
            return ( IPathContextFactory ) new LinesClippingDecoratorFactory( ( IPathContextFactory ) new PolarPathDrawingDecoratorFactory( ( IPathContextFactory ) new LinesPathContextFactory( renderContext ), transformationStrategy ), viewportSize );
        }
    }
}
