using DevExpress.Xpf.Charts.Native;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals.RenderableSeries.DrawingProviders;
using SciChart.Charting.Visuals.RenderableSeries.HitTesters;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Media;


namespace StockSharp.Xaml.Charting;

public class MyFastLineRenderableSeries : FastLineRenderableSeries
{
    
    public static readonly DependencyProperty OhlcDrawModeProperty = DependencyProperty.Register( nameof(OhlcDrawMode), typeof(OhlcLineDrawMode), typeof(MyFastLineRenderableSeries), new PropertyMetadata( (object) OhlcLineDrawMode.Close, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

    public MyFastLineRenderableSeries()
    {
        HitTestProvider = new MyFastLineHitTestProvider(this);
        

        DefaultStyleKey = (object) typeof(MyFastLineRenderableSeries);
    }
    
    public OhlcLineDrawMode OhlcDrawMode
    {
        get
        {
            return (OhlcLineDrawMode) GetValue(MyFastLineRenderableSeries.OhlcDrawModeProperty);
        }
        set
        {
            SetValue(MyFastLineRenderableSeries.OhlcDrawModeProperty, (object) value);
        }
    }

    protected override void InternalDraw(IRenderContext2D renderContext, IRenderPassData renderPassData)
    {
        base.InternalDraw(renderContext, renderPassData);

        var pointseries = renderPassData.PointSeries;

        IPointSeries pointSeries = CurrentRenderPassData.PointSeries;
                
        IPathContextFactory linesPathFactory = SeriesDrawingHelpersFactory.GetLinesPathFactory(renderContext, CurrentRenderPassData);

        var lineColor = Stroke;

        if ( PaletteProvider != null )
        {
            PenManager penManager = new PenManager(renderContext, AntiAliasing, (float)StrokeThickness, Opacity, StrokeDashArray);
            try
            {                
                Func<double, double, IPen2D> func = ( (x, y) => penManager.GetPen(lineColor) );
                FastLinesHelper.DrawLines(linesPathFactory, (Func<double, double, IPathColor>)func, pointSeries, CurrentRenderPassData.XCoordinateCalculator, CurrentRenderPassData.YCoordinateCalculator, IsDigitalLine, DrawNaNAs == LineDrawMode.ClosedLines);
            }
            finally
            {
                if ( penManager != null )
                    penManager.Dispose();
            }
        }
        else
        {
            using ( IPen2D pen = renderContext.CreatePen(lineColor, AntiAliasing, (float)StrokeThickness, Opacity, StrokeDashArray, PenLineCap.Round) )
                FastLinesHelper.DrawLines(linesPathFactory, pen, pointSeries, CurrentRenderPassData.XCoordinateCalculator, CurrentRenderPassData.YCoordinateCalculator, IsDigitalLine, DrawNaNAs == LineDrawMode.ClosedLines);
        }
        var pointMarker = GetPointMarker();
        if ( pointMarker == null )
            return;
        FastPointsHelper.DrawPoints(SeriesDrawingHelpersFactory.GetPointMarkerPathFactory(renderContext, CurrentRenderPassData, pointMarker), pointSeries, CurrentRenderPassData.XCoordinateCalculator, CurrentRenderPassData.YCoordinateCalculator);
    }

    

    public class MyFastLineHitTestProvider : DefaultHitTestProvider<MyFastLineRenderableSeries>
    {
        public MyFastLineHitTestProvider(MyFastLineRenderableSeries renderSeries) : base(renderSeries)
        {
        }

        public override HitTestInfo HitTest(System.Windows.Point rawPoint, double hitTestRadius, bool interpolate = false)
        {
            var hitTestInfo = base.HitTest(rawPoint, hitTestRadius, interpolate);

            if ( hitTestInfo.DataSeriesType != DataSeriesType.Ohlc && hitTestInfo.DataSeriesType != DataSeriesType.Hlc )
                return hitTestInfo;

            hitTestInfo.DataSeriesType = DataSeriesType.Xy;

            switch ( RenderableSeries.OhlcDrawMode )
            {
                case OhlcLineDrawMode.Open:
                    hitTestInfo.YValue = hitTestInfo.OpenValue;
                    break;
                case OhlcLineDrawMode.High:
                    hitTestInfo.YValue = hitTestInfo.HighValue;
                    break;
                case OhlcLineDrawMode.Low:
                    hitTestInfo.YValue = hitTestInfo.LowValue;
                    break;
                case OhlcLineDrawMode.Close:
                    hitTestInfo.YValue = hitTestInfo.CloseValue;
                    break;
            }
            return hitTestInfo;
        }
    }
}





    //public override object PointSeriesArg
    //{
    //    get
    //    {
    //        return (object) OhlcDrawMode;
    //    }
    //}

    //protected override void OnSeriesColorChanged()
    //{
    //}

    //protected override bool GetIsValidForDrawing()
    //{
    //    if(!base.GetIsValidForDrawing())
    //        return false;
    //    if(SeriesColor.A == (byte) 0 || StrokeThickness <= 0)
    //        return PointMarker != null;
    //    return true;
    //}

    //protected override HitTestInfo ToHitTestInfoImpl(int nearestDataPointIndex)
    //{
    //    HitTestInfo hitTestInfoImpl = base.ToHitTestInfoImpl(nearestDataPointIndex);
    //    if(hitTestInfoImpl.DataSeriesType != DataSeriesType.Ohlc && hitTestInfoImpl.DataSeriesType != DataSeriesType.Hlc)
    //        return hitTestInfoImpl;
    //    hitTestInfoImpl.DataSeriesType = DataSeriesType.Xy;
    //    switch(OhlcDrawMode)
    //    {
    //        case OhlcLineDrawMode.Open:
    //            hitTestInfoImpl.YValue = hitTestInfoImpl.OpenValue;
    //            break;
    //        case OhlcLineDrawMode.High:
    //            hitTestInfoImpl.YValue = hitTestInfoImpl.HighValue;
    //            break;
    //        case OhlcLineDrawMode.Low:
    //            hitTestInfoImpl.YValue = hitTestInfoImpl.LowValue;
    //            break;
    //        case OhlcLineDrawMode.Close:
    //            hitTestInfoImpl.YValue = hitTestInfoImpl.CloseValue;
    //            break;
    //    }
    //    return hitTestInfoImpl;
    //}

    //protected override void InternalDraw(IRenderContext2D renderContext, IRenderPassData renderPassData)
    //{
    //    IPointSeries pointSeries = CurrentRenderPassData.PointSeries;
    //    int num = PaletteProvider != null ? 1 : 0;
    //    Color lineColor = SeriesColor;
    //    IPathContextFactory linesPathFactory = SeriesDrawingHelpersFactory.GetLinesPathFactory(
    //        renderContext,
    //        CurrentRenderPassData);
    //    if(num != 0)
    //    {
    //        PenManager penManager = new PenManager(
    //            renderContext,
    //            AntiAliasing,
    //            (float) StrokeThickness,
    //            Opacity,
    //            StrokeDashArray);
    //        try
    //        {
    //            Func<double, double, IPen2D> func = (Func<double, double, IPen2D>) ((x, y) => penManager.GetPen(
    //                PaletteProvider.GetColor((IRenderableSeries) this, x, y) ?? lineColor));
    //            FastLinesHelper.DrawLines(
    //                linesPathFactory,
    //                (Func<double, double, IPathColor>) func,
    //                pointSeries,
    //                CurrentRenderPassData.XCoordinateCalculator,
    //                CurrentRenderPassData.YCoordinateCalculator,
    //                IsDigitalLine,
    //                DrawNaNAs == LineDrawMode.ClosedLines);
    //        } finally
    //        {
    //            if(penManager != null)
    //                penManager.Dispose();
    //        }
    //    } else
    //    {
    //        using(IPen2D pen = renderContext.CreatePen(
    //            SeriesColor,
    //            AntiAliasing,
    //            (float) StrokeThickness,
    //            Opacity,
    //            StrokeDashArray,
    //            PenLineCap.Round))
    //            FastLinesHelper.DrawLines(
    //                linesPathFactory,
    //                pen,
    //                pointSeries,
    //                CurrentRenderPassData.XCoordinateCalculator,
    //                CurrentRenderPassData.YCoordinateCalculator,
    //                IsDigitalLine,
    //                DrawNaNAs == LineDrawMode.ClosedLines);
    //    }
    //    IPointMarker pointMarker = GetPointMarker();
    //    if(pointMarker == null)
    //        return;
    //    FastPointsHelper.DrawPoints(
    //        SeriesDrawingHelpersFactory.GetPointMarkerPathFactory(
    //            renderContext,
    //            CurrentRenderPassData,
    //            pointMarker),
    //        pointSeries,
    //        CurrentRenderPassData.XCoordinateCalculator,
    //        CurrentRenderPassData.YCoordinateCalculator);
    //}
//}
