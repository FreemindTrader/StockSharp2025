// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.PointMarkers.IPointMarker
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Visuals.PointMarkers
{
    public interface IPointMarker
    {
        void Draw( IRenderContext2D context, IEnumerable<Point> centers );

        void Draw( IRenderContext2D context, double x, double y, IPen2D defaultPen, IBrush2D defaultBrush );

        Color Stroke
        {
            get; set;
        }

        Color Fill
        {
            get; set;
        }

        double Width
        {
            get; set;
        }

        double Height
        {
            get; set;
        }

        double StrokeThickness
        {
            get; set;
        }

        void Begin( IRenderContext2D context, IPen2D defaultPen, IBrush2D defaultBrush );

        void End( IRenderContext2D context );
    }
}
