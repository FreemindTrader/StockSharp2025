// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.AxisTitle
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public class AxisTitle : ContentControl
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (AxisTitle), new PropertyMetadata((object) Orientation.Horizontal));

        public AxisTitle()
        {
            this.DefaultStyleKey = ( object ) typeof( AxisTitle );
        }

        public Orientation Orientation
        {
            get
            {
                return ( Orientation ) this.GetValue( AxisTitle.OrientationProperty );
            }
            set
            {
                this.SetValue( AxisTitle.OrientationProperty, ( object ) value );
            }
        }
    }
}
