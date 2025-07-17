// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.UltrachartLegend
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;

namespace fx.Xaml.Charting
{
    public sealed class UltrachartLegend : ItemsControl
    {
        public static readonly DependencyProperty LegendDataProperty = DependencyProperty.Register(nameof (LegendData), typeof (ChartDataObject), typeof (UltrachartLegend), new PropertyMetadata((object) null, new PropertyChangedCallback(OnLegendDataChanged)));
        public static readonly DependencyProperty ShowVisibilityCheckboxesProperty = DependencyProperty.Register(nameof (ShowVisibilityCheckboxes), typeof (bool), typeof (UltrachartLegend), new PropertyMetadata((object) false, new PropertyChangedCallback(OnShowVisibilityCheckboxesChanged)));
        public static readonly DependencyProperty ShowSeriesMarkersProperty = DependencyProperty.Register(nameof (ShowSeriesMarkers), typeof (bool), typeof (UltrachartLegend), new PropertyMetadata((object) true));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (UltrachartLegend), new PropertyMetadata((object) Orientation.Vertical));

        public UltrachartLegend()
        {
            DefaultStyleKey = ( object ) typeof( UltrachartLegend );
        }

        public ChartDataObject LegendData
        {
            get
            {
                return ( ChartDataObject ) GetValue( LegendDataProperty );
            }
            set
            {
                SetValue( LegendDataProperty, ( object ) value );
            }
        }

        public bool ShowVisibilityCheckboxes
        {
            get
            {
                return ( bool ) GetValue( ShowVisibilityCheckboxesProperty );
            }
            set
            {
                SetValue( ShowVisibilityCheckboxesProperty, ( object ) value );
            }
        }

        public bool ShowSeriesMarkers
        {
            get
            {
                return ( bool ) GetValue( ShowSeriesMarkersProperty );
            }
            set
            {
                SetValue( ShowSeriesMarkersProperty, ( object ) value );
            }
        }

        public Orientation Orientation
        {
            get
            {
                return ( Orientation ) GetValue( OrientationProperty );
            }
            set
            {
                SetValue( OrientationProperty, ( object ) value );
            }
        }

        private static void OnShowVisibilityCheckboxesChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartLegend ultrachartLegend = (UltrachartLegend) d;
            if ( ultrachartLegend.LegendData == null )
            {
                return;
            }

            ultrachartLegend.LegendData.ShowVisibilityCheckboxes = ( bool ) e.NewValue;
        }

        private static void OnLegendDataChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartLegend ultrachartLegend = (UltrachartLegend) d;
            ChartDataObject newValue = e.NewValue as ChartDataObject;
            if ( newValue == null )
            {
                return;
            }

            newValue.ShowVisibilityCheckboxes = ultrachartLegend.ShowVisibilityCheckboxes;
        }
    }
}
