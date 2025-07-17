// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.AxisDragCursor
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class AxisDragCursor : Control
    {
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(nameof (Angle), typeof (double), typeof (AxisDragCursor), new PropertyMetadata((object) 0.0));

        public AxisDragCursor()
        {
            this.DefaultStyleKey = ( object ) typeof( AxisDragCursor );
        }

        public double Angle
        {
            get
            {
                return ( double ) this.GetValue( AxisDragCursor.AngleProperty );
            }
            set
            {
                this.SetValue( AxisDragCursor.AngleProperty, ( object ) value );
            }
        }
    }
}
