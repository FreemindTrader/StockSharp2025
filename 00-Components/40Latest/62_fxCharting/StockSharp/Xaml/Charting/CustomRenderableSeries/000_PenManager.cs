using SciChart.Drawing.Common;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;

public interface IPenManager : IDisposable
{
    IPen2D GetPen(Color _param1, float? _param2 = null);
}


/// <summary>
/// Manages pens and brushes for rendering in a 2D context.
/// </summary>
internal sealed class PenManager : IPenManager, IDisposable
{
    private readonly IRenderContext2D _renderContext;

    private readonly bool _antiAliasing;

    private readonly float _strokeThickness;

    private readonly double _opacity;

    private readonly Dictionary<(Color, float), IPen2D> _2DColorPen;

    private readonly Dictionary<Color, IBrush2D> _2DColorBrush;

    private readonly Dictionary<Brush, IBrush2D> _2DTextureBrush;

    private readonly double[] _strokeDashArray;

    public PenManager(IRenderContext2D renderContext, bool antiAliasing, float strokeThickness, double opacity, double[] strokeDashArray = null)
    {
        _renderContext   = renderContext;
        _antiAliasing    = antiAliasing;
        _strokeThickness = strokeThickness;
        _opacity         = opacity;
        _2DColorPen      = new Dictionary<(Color, float), IPen2D>();
        _2DColorBrush    = new Dictionary<Color, IBrush2D>();
        _2DTextureBrush  = new Dictionary<Brush, IBrush2D>();
        _strokeDashArray = strokeDashArray;
    }

    public IPen2D GetPen(Color color, float? stroke = null)
    {
        stroke.GetValueOrDefault();
        if(!stroke.HasValue)
            stroke = new float?(_strokeThickness);
        (Color, float) key = (color, stroke.Value);
        IPen2D myPen;
        if(_2DColorPen.TryGetValue(key, out myPen))
            return myPen;
        myPen = _renderContext.CreatePen(color, _antiAliasing, (float)( stroke ?? _strokeThickness ), _opacity, _strokeDashArray, PenLineCap.Round);
        _2DColorPen.Add(key, myPen);
        return myPen;
    }

    public IBrush2D GetBrush(Color desiredColor)
    {
        IBrush2D myBrush;

        if(_2DColorBrush.TryGetValue(desiredColor, out myBrush))
            return myBrush;

        IBrush2D drawingBrush = _renderContext.CreateBrush(desiredColor, _opacity, null);
        _2DColorBrush.Add(desiredColor, drawingBrush);

        return drawingBrush;
    }

    public IBrush2D GetBrush(Brush desiredBrush)
    {
        IBrush2D myBrush;

        if(_2DTextureBrush.TryGetValue(desiredBrush, out myBrush))
            return myBrush;

        IBrush2D drawingBrush = _renderContext.CreateBrush(
            desiredBrush,
            1.0,
            SciChart.Drawing.Common.TextureMappingMode.PerPrimitive);

        _2DTextureBrush.Add(desiredBrush, drawingBrush);
        return drawingBrush;
    }

    public void Dispose()
    {
        foreach(IDisposable disposable in _2DColorPen.Values)
            disposable.Dispose();
        foreach(IDisposable disposable in _2DColorBrush.Values)
            disposable.Dispose();
        foreach(IDisposable disposable in _2DTextureBrush.Values)
            disposable.Dispose();
        _2DColorPen.Clear();
        _2DColorBrush.Clear();
    }
}
