// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.PenManager
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Visuals
{
    internal class PenManager : IPenManager, IDisposable
    {
        private readonly IRenderContext2D _renderContext;
        private readonly bool _antiAliasing;
        private readonly float _strokeThickness;
        private readonly double _opacity;
        private readonly Dictionary<Color, IPen2D> _renderPens;
        private readonly Dictionary<Color, IBrush2D> _renderBrushes;
        private readonly Dictionary<Brush, IBrush2D> _textureBrushes;
        private readonly double[] _strokeDashArray;

        public PenManager( IRenderContext2D renderContext, bool antiAliasing, float strokeThickness, double opacity, double[ ] strokeDashArray = null )
        {
            _renderContext = renderContext;
            _antiAliasing = antiAliasing;
            _strokeThickness = strokeThickness;
            _opacity = opacity;
            _renderPens = new Dictionary<Color, IPen2D>();
            _renderBrushes = new Dictionary<Color, IBrush2D>();
            _textureBrushes = new Dictionary<Brush, IBrush2D>();
            _strokeDashArray = strokeDashArray;
        }

        public IPen2D GetPen( Color color )
        {
            IPen2D pen2D;
            if ( _renderPens.TryGetValue( color, out pen2D ) )
                return pen2D;
            IPen2D pen = _renderContext.CreatePen(color, _antiAliasing, _strokeThickness, _opacity, _strokeDashArray, PenLineCap.Round);
            _renderPens.Add( color, pen );
            return pen;
        }

        public IBrush2D GetBrush( Color color )
        {
            IBrush2D brush2D;
            if ( _renderBrushes.TryGetValue( color, out brush2D ) )
                return brush2D;
            IBrush2D brush = _renderContext.CreateBrush(color, _opacity, new bool?());
            _renderBrushes.Add( color, brush );
            return brush;
        }

        public IBrush2D GetBrush( Brush fromBrush )
        {
            IBrush2D brush2D;
            if ( _textureBrushes.TryGetValue( fromBrush, out brush2D ) )
                return brush2D;
            IBrush2D brush = _renderContext.CreateBrush(fromBrush, 1.0, TextureMappingMode.PerPrimitive);
            _textureBrushes.Add( fromBrush, brush );
            return brush;
        }

        public void Dispose()
        {
            foreach ( IDisposable disposable in _renderPens.Values )
                disposable.Dispose();
            foreach ( IDisposable disposable in _renderBrushes.Values )
                disposable.Dispose();
            foreach ( IDisposable disposable in _textureBrushes.Values )
                disposable.Dispose();
            _renderPens.Clear();
            _renderBrushes.Clear();
        }
    }
}
