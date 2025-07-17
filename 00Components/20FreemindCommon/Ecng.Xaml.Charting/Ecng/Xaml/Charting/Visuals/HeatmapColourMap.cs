// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.HeatmapColourMap
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
namespace fx.Xaml.Charting
{
    public class HeatmapColourMap : Control, INotifyPropertyChanged
    {
        public static readonly DependencyProperty FastHeatMapRenderableSeriesProperty = DependencyProperty.Register(nameof (FastHeatMapRenderableSeries), typeof (FastHeatMapRenderableSeries), typeof (HeatmapColourMap), new PropertyMetadata((object) null, new PropertyChangedCallback(HeatmapColourMap.OnMappingSettingsChanged)));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (HeatmapColourMap), new PropertyMetadata((object) Orientation.Horizontal, new PropertyChangedCallback(HeatmapColourMap.OnOrientationChanged)));

        public FastHeatMapRenderableSeries FastHeatMapRenderableSeries
        {
            get
            {
                return ( FastHeatMapRenderableSeries ) this.GetValue( HeatmapColourMap.FastHeatMapRenderableSeriesProperty );
            }
            set
            {
                this.SetValue( HeatmapColourMap.FastHeatMapRenderableSeriesProperty, ( object ) value );
            }
        }

        public Orientation Orientation
        {
            get
            {
                return ( Orientation ) this.GetValue( HeatmapColourMap.OrientationProperty );
            }
            set
            {
                this.SetValue( HeatmapColourMap.OrientationProperty, ( object ) value );
            }
        }

        public HeatmapColourMap()
        {
            this.DefaultStyleKey = ( object ) typeof( HeatmapColourMap );
        }

        private static void OnMappingSettingsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
        }

        private static void OnOrientationChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
