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
