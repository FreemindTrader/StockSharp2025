// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.UltraStockChart
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using StockSharp.Xaml.Charting.ChartModifiers;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Utility.Mouse;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting
{

    public class UltraStockChart : UltrachartSurface
    {
        public static readonly DependencyProperty XAxisStyleProperty = DependencyProperty.Register(nameof (XAxisStyle), typeof (Style), typeof (UltraStockChart), new PropertyMetadata((object) null));
        public static readonly DependencyProperty YAxisStyleProperty = DependencyProperty.Register(nameof (YAxisStyle), typeof (Style), typeof (UltraStockChart), new PropertyMetadata((object) null));
        public static readonly DependencyProperty IsCursorEnabledProperty = DependencyProperty.Register(nameof (IsCursorEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) false, new PropertyChangedCallback(UltraStockChart.OnDataProviderChanged)));
        public static readonly DependencyProperty IsRolloverEnabledProperty = DependencyProperty.Register(nameof (IsRolloverEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) false, new PropertyChangedCallback(UltraStockChart.OnDataProviderChanged)));
        public static readonly DependencyProperty IsPanEnabledProperty = DependencyProperty.Register(nameof (IsPanEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) false));
        public static readonly DependencyProperty IsRubberBandZoomEnabledProperty = DependencyProperty.Register(nameof (IsRubberBandZoomEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) false));
        public static readonly DependencyProperty BarTimeFrameProperty = DependencyProperty.Register(nameof (BarTimeFrame), typeof (double), typeof (UltraStockChart), new PropertyMetadata((object) -1.0));
        public static readonly DependencyProperty IsXAxisVisibleProperty = DependencyProperty.Register(nameof (IsXAxisVisible), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) true, new PropertyChangedCallback(UltraStockChart.OnIsXAxisVisibleDependencyPropertyChanged)));
        public static readonly DependencyProperty VerticalChartGroupIdProperty = DependencyProperty.Register(nameof (VerticalChartGroupId), typeof (string), typeof (UltraStockChart), new PropertyMetadata((object) null, new PropertyChangedCallback(UltraStockChart.OnVerticalChartGroupIdChanged)));
        public static readonly DependencyProperty IsAxisMarkersEnabledProperty = DependencyProperty.Register(nameof (IsAxisMarkersEnabled), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) true, new PropertyChangedCallback(UltrachartSurfaceBase.OnInvalidateUltrachartSurface)));
        public static readonly DependencyProperty LegendSourceProperty = DependencyProperty.Register(nameof (LegendSource), typeof (ChartDataObject), typeof (UltraStockChart), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty DefaultDataProviderProperty = DependencyProperty.Register(nameof (DefaultDataProvider), typeof (InspectSeriesModifierBase), typeof (UltraStockChart), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty ShowLegendProperty = DependencyProperty.Register(nameof (ShowLegend), typeof (bool), typeof (UltraStockChart), new PropertyMetadata((object) true));
        public static readonly DependencyProperty LegendStyleProperty = DependencyProperty.Register(nameof (LegendStyle), typeof (Style), typeof (UltraStockChart), new PropertyMetadata((object) null));

        public UltraStockChart()
        {
            this.DefaultStyleKey = ( object ) typeof( UltraStockChart );
        }

        public InspectSeriesModifierBase DefaultDataProvider
        {
            get
            {
                return ( InspectSeriesModifierBase ) this.GetValue( UltraStockChart.DefaultDataProviderProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.DefaultDataProviderProperty, ( object ) value );
            }
        }

        public ChartDataObject LegendSource
        {
            get
            {
                return ( ChartDataObject ) this.GetValue( UltraStockChart.LegendSourceProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.LegendSourceProperty, ( object ) value );
            }
        }

        public bool ShowLegend
        {
            get
            {
                return ( bool ) this.GetValue( UltraStockChart.ShowLegendProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.ShowLegendProperty, ( object ) value );
            }
        }

        public Style LegendStyle
        {
            get
            {
                return ( Style ) this.GetValue( UltraStockChart.LegendStyleProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.LegendStyleProperty, ( object ) value );
            }
        }

        public bool IsAxisMarkersEnabled
        {
            get
            {
                return ( bool ) this.GetValue( UltraStockChart.IsAxisMarkersEnabledProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.IsAxisMarkersEnabledProperty, ( object ) value );
            }
        }

        public string VerticalChartGroupId
        {
            get
            {
                return ( string ) this.GetValue( UltraStockChart.VerticalChartGroupIdProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.VerticalChartGroupIdProperty, ( object ) value );
            }
        }

        public Style XAxisStyle
        {
            get
            {
                return ( Style ) this.GetValue( UltraStockChart.XAxisStyleProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.XAxisStyleProperty, ( object ) value );
            }
        }

        public Style YAxisStyle
        {
            get
            {
                return ( Style ) this.GetValue( UltraStockChart.YAxisStyleProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.YAxisStyleProperty, ( object ) value );
            }
        }

        public bool IsXAxisVisible
        {
            get
            {
                return ( bool ) this.GetValue( UltraStockChart.IsXAxisVisibleProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.IsXAxisVisibleProperty, ( object ) value );
            }
        }

        public bool IsCursorEnabled
        {
            get
            {
                return ( bool ) this.GetValue( UltraStockChart.IsCursorEnabledProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.IsCursorEnabledProperty, ( object ) value );
            }
        }

        public bool IsRolloverEnabled
        {
            get
            {
                return ( bool ) this.GetValue( UltraStockChart.IsRolloverEnabledProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.IsRolloverEnabledProperty, ( object ) value );
            }
        }

        public bool IsPanEnabled
        {
            get
            {
                return ( bool ) this.GetValue( UltraStockChart.IsPanEnabledProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.IsPanEnabledProperty, ( object ) value );
            }
        }

        public bool IsRubberBandZoomEnabled
        {
            get
            {
                return ( bool ) this.GetValue( UltraStockChart.IsRubberBandZoomEnabledProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.IsRubberBandZoomEnabledProperty, ( object ) value );
            }
        }

        public double BarTimeFrame
        {
            get
            {
                return ( double ) this.GetValue( UltraStockChart.BarTimeFrameProperty );
            }
            set
            {
                this.SetValue( UltraStockChart.BarTimeFrameProperty, ( object ) value );
            }
        }

        public override void ZoomExtents()
        {
            if ( this.YAxes.IsNullOrEmpty<IAxis>() )
            {
                return;
            }

            using ( this.SuspendUpdates() )
            {
                this.YAxis.GrowBy = ( IRange<double> ) new DoubleRange( 0.1, 0.1 );
                base.ZoomExtents();
            }
        }

        private static void OnIsXAxisVisibleDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltraStockChart ultraStockChart = (UltraStockChart) d;
            bool newValue = (bool) e.NewValue;
            if ( ultraStockChart.XAxis == null )
            {
                return;
            }

            ultraStockChart.XAxis.Visibility = newValue ? Visibility.Visible : Visibility.Collapsed;
        }

        private static void OnVerticalChartGroupIdChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltraStockChart ultraStockChart = (UltraStockChart) d;
            if ( ultraStockChart == null )
            {
                return;
            }

            ModifierGroup chartModifier = ultraStockChart.ChartModifier as ModifierGroup;
            if ( chartModifier == null )
            {
                return;
            }

            MouseManager.SetMouseEventGroup( ( DependencyObject ) chartModifier, e.NewValue as string );
        }

        private static void OnDataProviderChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltraStockChart ultraStockChart = d as UltraStockChart;
            if ( ultraStockChart == null )
            {
                return;
            }

            ModifierGroup chartModifier = (ModifierGroup) ultraStockChart.ChartModifier;
            if ( chartModifier == null )
            {
                return;
            }

            InspectSeriesModifierBase seriesModifierBase = (InspectSeriesModifierBase) chartModifier["LegendModifier"];
            if ( ultraStockChart.IsRolloverEnabled )
            {
                seriesModifierBase = ( InspectSeriesModifierBase ) chartModifier[ "RolloverModifier" ];
            }
            else if ( ultraStockChart.IsCursorEnabled )
            {
                seriesModifierBase = ( InspectSeriesModifierBase ) chartModifier[ "CursorModifier" ];
            }

            ultraStockChart.DefaultDataProvider = seriesModifierBase;
        }
    }
}
