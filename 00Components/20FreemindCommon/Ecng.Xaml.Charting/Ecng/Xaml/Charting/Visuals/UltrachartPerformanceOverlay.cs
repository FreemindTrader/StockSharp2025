// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.UltrachartPerformanceOverlay
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Visuals
{
    public class UltrachartPerformanceOverlay : ContentControl
    {
        public static readonly DependencyProperty TargetSurfaceProperty = DependencyProperty.Register(nameof (TargetSurface), typeof (ISciChartSurface), typeof (UltrachartPerformanceOverlay), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartPerformanceOverlay.OnTargetSurfaceDependencyPropertyChanged)));
        public static readonly DependencyProperty WpfFpsProperty = DependencyProperty.Register(nameof (WpfFps), typeof (double), typeof (UltrachartPerformanceOverlay), new PropertyMetadata((object) 0.0));
        public static readonly DependencyProperty UltrachartFpsProperty = DependencyProperty.Register(nameof (UltrachartFps), typeof (double), typeof (UltrachartPerformanceOverlay), new PropertyMetadata((object) 0.0));
        public static readonly DependencyProperty WpfFpsSeriesProperty = DependencyProperty.Register(nameof (WpfFpsSeries), typeof (XyDataSeries<double, double>), typeof (UltrachartPerformanceOverlay), new PropertyMetadata((object) null));
        public static readonly DependencyProperty UltrachartFpsSeriesProperty = DependencyProperty.Register(nameof (UltrachartFpsSeries), typeof (XyDataSeries<double, double>), typeof (UltrachartPerformanceOverlay), new PropertyMetadata((object) null));
        public static readonly DependencyProperty SmoothingWindowSizeProperty = DependencyProperty.Register(nameof (SmoothingWindowSize), typeof (int), typeof (UltrachartPerformanceOverlay), new PropertyMetadata((object) 20));
        public static readonly DependencyProperty TotalPointCountProperty = DependencyProperty.Register(nameof (TotalPointCount), typeof (int), typeof (UltrachartPerformanceOverlay), new PropertyMetadata((object) 0));
        public static readonly DependencyProperty ChartsVisibilityProperty = DependencyProperty.Register(nameof (ChartsVisibility), typeof (Visibility), typeof (UltrachartPerformanceOverlay), new PropertyMetadata((object) Visibility.Visible));
        private Stopwatch _stopWatch;
        private double _lastUltrachartRenderTime;
        private double _lastWpfRenderTime;

        public UltrachartPerformanceOverlay()
        {
            DefaultStyleKey = ( object ) typeof( UltrachartPerformanceOverlay );
        }

        public Visibility ChartsVisibility
        {
            get
            {
                return ( Visibility ) GetValue( UltrachartPerformanceOverlay.ChartsVisibilityProperty );
            }
            set
            {
                SetValue( UltrachartPerformanceOverlay.ChartsVisibilityProperty, ( object ) value );
            }
        }

        public int TotalPointCount
        {
            get
            {
                return ( int ) GetValue( UltrachartPerformanceOverlay.TotalPointCountProperty );
            }
            set
            {
                SetValue( UltrachartPerformanceOverlay.TotalPointCountProperty, ( object ) value );
            }
        }

        public int SmoothingWindowSize
        {
            get
            {
                return ( int ) GetValue( UltrachartPerformanceOverlay.SmoothingWindowSizeProperty );
            }
            set
            {
                SetValue( UltrachartPerformanceOverlay.SmoothingWindowSizeProperty, ( object ) value );
            }
        }

        public XyDataSeries<double, double> UltrachartFpsSeries
        {
            get
            {
                return ( XyDataSeries<double, double> ) GetValue( UltrachartPerformanceOverlay.UltrachartFpsSeriesProperty );
            }
            set
            {
                SetValue( UltrachartPerformanceOverlay.UltrachartFpsSeriesProperty, ( object ) value );
            }
        }

        public XyDataSeries<double, double> WpfFpsSeries
        {
            get
            {
                return ( XyDataSeries<double, double> ) GetValue( UltrachartPerformanceOverlay.WpfFpsSeriesProperty );
            }
            set
            {
                SetValue( UltrachartPerformanceOverlay.WpfFpsSeriesProperty, ( object ) value );
            }
        }

        public double WpfFps
        {
            get
            {
                return ( double ) GetValue( UltrachartPerformanceOverlay.WpfFpsProperty );
            }
            set
            {
                SetValue( UltrachartPerformanceOverlay.WpfFpsProperty, ( object ) value );
            }
        }

        public ISciChartSurface TargetSurface
        {
            get
            {
                return ( ISciChartSurface ) GetValue( UltrachartPerformanceOverlay.TargetSurfaceProperty );
            }
            set
            {
                SetValue( UltrachartPerformanceOverlay.TargetSurfaceProperty, ( object ) value );
            }
        }

        public double UltrachartFps
        {
            get
            {
                return ( double ) GetValue( UltrachartPerformanceOverlay.UltrachartFpsProperty );
            }
            set
            {
                SetValue( UltrachartPerformanceOverlay.UltrachartFpsProperty, ( object ) value );
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        private static void OnTargetSurfaceDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartPerformanceOverlay performanceOverlay = (UltrachartPerformanceOverlay) d;
            UltrachartSurface newValue = e.NewValue as UltrachartSurface;
            if ( newValue == null )
                return;
            newValue.Loaded -= new RoutedEventHandler( performanceOverlay.OnUltrachartSurfaceLoaded );
            newValue.Loaded += new RoutedEventHandler( performanceOverlay.OnUltrachartSurfaceLoaded );
            newValue.Unloaded -= new RoutedEventHandler( performanceOverlay.OnUltrachartSurfaceUnloaded );
            newValue.Unloaded += new RoutedEventHandler( performanceOverlay.OnUltrachartSurfaceUnloaded );
            if ( !newValue.IsLoaded )
                return;
            performanceOverlay.OnUltrachartSurfaceLoaded( ( object ) newValue, ( RoutedEventArgs ) null );
        }

        private void OnUltrachartSurfaceUnloaded( object sender, RoutedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = sender as UltrachartSurface;
            CompositionTarget.Rendering -= new EventHandler( OnCompositionTargetRendering );
            EventHandler<EventArgs> eventHandler = new EventHandler<EventArgs>(OnUltrachartSurfaceRendered);
            ultrachartSurface.Rendered -= eventHandler;
            _stopWatch.Stop();
        }

        private void OnUltrachartSurfaceLoaded( object sender, RoutedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = sender as UltrachartSurface;
            _stopWatch = Stopwatch.StartNew();
            _lastUltrachartRenderTime = 0.0;
            _lastWpfRenderTime = 0.0;
            XyDataSeries<double, double> xyDataSeries1 = new XyDataSeries<double, double>();
            xyDataSeries1.FifoCapacity = new int?( SmoothingWindowSize );
            UltrachartFpsSeries = xyDataSeries1;
            XyDataSeries<double, double> xyDataSeries2 = new XyDataSeries<double, double>();
            xyDataSeries2.FifoCapacity = new int?( SmoothingWindowSize );
            WpfFpsSeries = xyDataSeries2;
            TotalPointCount = 0;
            CompositionTarget.Rendering -= new EventHandler( OnCompositionTargetRendering );
            CompositionTarget.Rendering += new EventHandler( OnCompositionTargetRendering );
            EventHandler<EventArgs> eventHandler = new EventHandler<EventArgs>(OnUltrachartSurfaceRendered);
            ultrachartSurface.Rendered += eventHandler;
        }

        private void OnUltrachartSurfaceRendered( object sender, EventArgs e )
        {
            double y = 1000.0 / ((double) _stopWatch.ElapsedMilliseconds - _lastUltrachartRenderTime);
            _lastUltrachartRenderTime = ( double ) _stopWatch.ElapsedMilliseconds;
            UltrachartFpsSeries.Append( UltrachartFpsSeries.Count == 0 ? 0.0 : ( double ) ( int ) ( UltrachartFpsSeries.XValues.Last<double>() + 1.0 ), y );
            UltrachartFps = UltrachartFpsSeries.YValues.Sum() / ( double ) UltrachartFpsSeries.Count;
            TotalPointCount = TargetSurface.RenderableSeries.Sum<IRenderableSeries>( ( Func<IRenderableSeries, int> ) ( r =>
            {
                if ( r.DataSeries == null )
                    return 0;
                return r.DataSeries.Count;
            } ) );
        }

        private void OnCompositionTargetRendering( object sender, EventArgs e )
        {
            double num = 1000.0 / ((double) _stopWatch.ElapsedMilliseconds - _lastWpfRenderTime);
            if ( double.IsInfinity( num ) )
                return;
            _lastWpfRenderTime = ( double ) _stopWatch.ElapsedMilliseconds;
            WpfFpsSeries.Append( WpfFpsSeries.Count == 0 ? 0.0 : ( double ) ( int ) ( WpfFpsSeries.XValues.Last<double>() + 1.0 ), num );
            WpfFps = WpfFpsSeries.YValues.Sum() / ( double ) WpfFpsSeries.Count;
        }
    }
}
