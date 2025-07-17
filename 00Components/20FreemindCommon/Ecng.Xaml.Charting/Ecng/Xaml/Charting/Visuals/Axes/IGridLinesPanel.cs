// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.IGridLinesPanel
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
using System.Windows.Shapes;

namespace fx.Xaml.Charting
{
    public interface IGridLinesPanel
    {
        void Clear( XyDirection xyDirection );

        void AddLine( XyDirection xyDirection, Line line );

        int Width
        {
            get;
        }

        int Height
        {
            get;
        }

        Thickness BorderThickness
        {
            get;
        }

        Line GenerateElement( int lineId, XyDirection xyDirection, Style lineStyle );

        void RemoveElementsAfter( XyDirection xyDirection, int index );
    }
}
