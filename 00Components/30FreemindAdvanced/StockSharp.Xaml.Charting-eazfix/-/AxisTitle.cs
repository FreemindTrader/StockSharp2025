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
