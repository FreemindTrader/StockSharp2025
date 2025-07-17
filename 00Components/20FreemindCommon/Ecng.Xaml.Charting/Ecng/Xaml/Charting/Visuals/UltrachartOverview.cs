// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.UltrachartOverview
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using StockSharp.Xaml.Charting.ChartModifiers;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Utility.Mouse;
using StockSharp.Xaml.Charting.Visuals.Axes;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Visuals
{
    [TemplatePart( Name = "PART_Container", Type = typeof( Panel ) )]
    [TemplatePart( Name = "PART_BackgroundSurface", Type = typeof( ISciChartSurface ) )]
    [TemplatePart( Name = "PART_Scrollbar", Type = typeof( UltrachartScrollbar ) )]
    public class UltrachartOverview : Control, IInvalidatableElement
    {
        public static readonly DependencyProperty SeriesColorProperty = DependencyProperty.Register(nameof (SeriesColor), typeof (Color), typeof (UltrachartOverview), new PropertyMetadata((object) new Color()));
        public static readonly DependencyProperty AreaBrushProperty = DependencyProperty.Register(nameof (AreaBrush), typeof (Brush), typeof (UltrachartOverview), new PropertyMetadata((object) null));
        public static readonly DependencyProperty DataSeriesProperty = DependencyProperty.Register(nameof (DataSeries), typeof (IDataSeries), typeof (UltrachartOverview), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty ParentSurfaceProperty = DependencyProperty.Register(nameof (ParentSurface), typeof (ISciChartSurface), typeof (UltrachartOverview), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartOverview.OnParentSurfaceDependencyPropertyChanged)));
        public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(nameof (XAxisId), typeof (string), typeof (UltrachartOverview), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(UltrachartOverview.OnAxisIdDependencyPropertyChanged)));
        public static readonly DependencyProperty SelectedRangeProperty = DependencyProperty.Register(nameof (SelectedRange), typeof (IRange), typeof (UltrachartOverview), new PropertyMetadata((object) null));
        public static readonly DependencyProperty AxisProperty = DependencyProperty.Register(nameof (Axis), typeof (IAxis), typeof (UltrachartOverview), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartOverview.OnAxisDependencyPropertyChanged)));
        public static readonly DependencyProperty ScrollbarStyleProperty = DependencyProperty.Register(nameof (ScrollbarStyle), typeof (Style), typeof (UltrachartOverview), new PropertyMetadata((object) null));
        public static readonly DependencyProperty RenderableSeriesStyleProperty = DependencyProperty.Register(nameof (RenderableSeriesStyle), typeof (Style), typeof (UltrachartOverview), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartOverview.OnRenderableSeriesStylePropertyChanged)));
        public static readonly DependencyProperty RenderableSeriesTypeProperty = DependencyProperty.Register(nameof (RenderableSeriesType), typeof (Type), typeof (UltrachartOverview), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartOverview.OnRenderableSeriesTypePropertyChanged)));
        private ISciChartSurface _backgroundChartSurface;
        private Ecng.Xaml.PropertyChangeNotifier _renderableSeriesPropertyNotifier;
        private Ecng.Xaml.PropertyChangeNotifier _renderSeriesDataSeriesPropertyNotifier;
        private ObservableCollection<IRenderableSeries> _renderableSeries;
        private Ecng.Xaml.PropertyChangeNotifier _xAxesPropertyNotifier;
        private AxisCollection _axisCollection;
        private readonly RenderTimerHelper _renderTimerHelper;

        public UltrachartOverview()
        {
            this.DefaultStyleKey = ( object ) typeof( UltrachartOverview );
            this._renderTimerHelper = new RenderTimerHelper( new Action( this.InvalidateElement ), ( IDispatcherFacade ) new DispatcherUtil( this.Dispatcher ) );
            this.Loaded += ( RoutedEventHandler ) ( ( sender, args ) => this._renderTimerHelper.OnLoaded() );
            this.Unloaded += ( RoutedEventHandler ) ( ( sender, args ) => this._renderTimerHelper.OnUnlodaed() );
        }

        public ISciChartSurface BackgroundChartSurface
        {
            get
            {
                return this._backgroundChartSurface;
            }
        }

        public Style RenderableSeriesStyle
        {
            get
            {
                return ( Style ) this.GetValue( UltrachartOverview.RenderableSeriesStyleProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.RenderableSeriesStyleProperty, ( object ) value );
            }
        }

        public Type RenderableSeriesType
        {
            get
            {
                return ( Type ) this.GetValue( UltrachartOverview.RenderableSeriesTypeProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.RenderableSeriesTypeProperty, ( object ) value );
            }
        }

        public Color SeriesColor
        {
            get
            {
                return ( Color ) this.GetValue( UltrachartOverview.SeriesColorProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.SeriesColorProperty, ( object ) value );
            }
        }

        public Brush AreaBrush
        {
            get
            {
                return ( Brush ) this.GetValue( UltrachartOverview.AreaBrushProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.AreaBrushProperty, ( object ) value );
            }
        }

        public IDataSeries DataSeries
        {
            get
            {
                return ( IDataSeries ) this.GetValue( UltrachartOverview.DataSeriesProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.DataSeriesProperty, ( object ) value );
            }
        }

        public string XAxisId
        {
            get
            {
                return ( string ) this.GetValue( UltrachartOverview.XAxisIdProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.XAxisIdProperty, ( object ) value );
            }
        }

        public ISciChartSurface ParentSurface
        {
            get
            {
                return ( ISciChartSurface ) this.GetValue( UltrachartOverview.ParentSurfaceProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.ParentSurfaceProperty, ( object ) value );
            }
        }

        public IRange SelectedRange
        {
            get
            {
                return ( IRange ) this.GetValue( UltrachartOverview.SelectedRangeProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.SelectedRangeProperty, ( object ) value );
            }
        }

        public IAxis Axis
        {
            get
            {
                return ( IAxis ) this.GetValue( UltrachartOverview.AxisProperty );
            }
            private set
            {
                this.SetValue( UltrachartOverview.AxisProperty, ( object ) value );
            }
        }

        public Style ScrollbarStyle
        {
            get
            {
                return ( Style ) this.GetValue( UltrachartOverview.ScrollbarStyleProperty );
            }
            set
            {
                this.SetValue( UltrachartOverview.ScrollbarStyleProperty, ( object ) value );
            }
        }

        public void InvalidateElement()
        {
            if ( this.BackgroundChartSurface == null || this.Axis == null )
            {
                return;
            }

            IRange range = this.Axis.DataRange;
            if ( range == null )
            {
                return;
            }

            if ( this.Axis.GrowBy != null )
            {
                range = range.GrowBy( this.Axis.GrowBy.Min, this.Axis.GrowBy.Max );
            }

            this.BackgroundChartSurface.XAxis.VisibleRange = range;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._backgroundChartSurface = this.GetTemplateChild( "PART_BackgroundSurface" ) as ISciChartSurface;
            this.SynchronizeXAxisType( this.Axis );
            this.SynchronizeRenderableSeriesType( this.RenderableSeriesStyle, this.RenderableSeriesType );
            UltrachartScrollbar templateChild;
            if ( ( templateChild = this.GetTemplateChild( "PART_Scrollbar" ) as UltrachartScrollbar ) == null )
            {
                return;
            }

            templateChild.MouseWheel += ( MouseWheelEventHandler ) ( ( sender, args ) => this.ParentSurface?.ChartModifier?.OnModifierMouseWheel( new ModifierMouseArgs( args.GetPosition( ( IInputElement ) this.ParentSurface.RootGrid ), MouseButtons.None, MouseExtensions.GetCurrentModifier(), args.Delta, true, ( IReceiveMouseEvents ) null ) ) );
        }

        private void SynchronizeXAxisType( IAxis xAxis )
        {
            if ( this.BackgroundChartSurface == null || xAxis == null )
            {
                return;
            }

            IAxis axis = xAxis.Clone();
            axis.Visibility = Visibility.Collapsed;
            axis.ParentSurface = this.BackgroundChartSurface;
            axis.DrawMinorGridLines = false;
            axis.DrawMajorGridLines = false;
            Binding binding = new Binding("FlipCoordinates") { Source = (object) xAxis, Mode = BindingMode.OneWay };
            ( ( FrameworkElement ) axis ).SetBinding( AxisBase.FlipCoordinatesProperty, ( BindingBase ) binding );
            this.BackgroundChartSurface.XAxis = axis;
        }

        private bool DoesSurfaceHaveThisDataSeries( ISciChartSurface scs, IDataSeries dataSeries )
        {
            if ( scs == null || scs.RenderableSeries.IsNullOrEmpty<IRenderableSeries>() )
            {
                return false;
            }

            return scs.RenderableSeries.Any<IRenderableSeries>( ( Func<IRenderableSeries, bool> ) ( x => x.DataSeries == dataSeries ) );
        }

        private static void OnParentSurfaceDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as UltrachartOverview )?.OnParentSurfaceChanged( e );
        }

        private void OnParentSurfaceChanged( DependencyPropertyChangedEventArgs e )
        {
            this.DisposeNotifiers();
            UltrachartSurface newValue = e.NewValue as UltrachartSurface;
            if ( newValue != null )
            {
                this._renderableSeriesPropertyNotifier = new Ecng.Xaml.PropertyChangeNotifier( ( DependencyObject ) newValue, UltrachartSurface.RenderableSeriesProperty );
                this._renderableSeriesPropertyNotifier.ValueChanged += ( Action ) ( () => this.OnRenderableSeriesChanged( ( ObservableCollection<IRenderableSeries> ) this._renderableSeriesPropertyNotifier.Value ) );
                this._xAxesPropertyNotifier = new Ecng.Xaml.PropertyChangeNotifier( ( DependencyObject ) newValue, UltrachartSurface.XAxesProperty );
                this._xAxesPropertyNotifier.ValueChanged += ( Action ) ( () => this.OnXAxesPropertyChanged( ( AxisCollection ) this._xAxesPropertyNotifier.Value ) );
                if ( !this.DoesSurfaceHaveThisDataSeries( ( ISciChartSurface ) newValue, this.DataSeries ) )
                {
                    this.SetCurrentValue( UltrachartOverview.DataSeriesProperty, ( object ) null );
                }

                this.OnRenderableSeriesChanged( newValue.RenderableSeries );
                this.OnXAxesPropertyChanged( newValue.XAxes );
            }
            else
            {
                this.DisposeNotifiers();
                this.SetCurrentValue( UltrachartOverview.DataSeriesProperty, ( object ) null );
            }
        }

        private void DisposeNotifiers()
        {
            if ( this._xAxesPropertyNotifier != null )
            {
                this._xAxesPropertyNotifier.Dispose();
                this._xAxesPropertyNotifier = ( Ecng.Xaml.PropertyChangeNotifier ) null;
            }
            if ( this._renderableSeriesPropertyNotifier == null )
            {
                return;
            }

            this._renderableSeriesPropertyNotifier.Dispose();
            this._renderSeriesDataSeriesPropertyNotifier = ( Ecng.Xaml.PropertyChangeNotifier ) null;
        }

        private void OnXAxesPropertyChanged( AxisCollection xAxes )
        {
            if ( this._axisCollection != null )
            {
                this._axisCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.OnXAxesCollectionChanged );
            }

            this._axisCollection = xAxes;
            if ( this._axisCollection != null )
            {
                this._axisCollection.CollectionChanged += new NotifyCollectionChangedEventHandler( this.OnXAxesCollectionChanged );
            }

            this.OnXAxesCollectionChanged( ( object ) this, ( NotifyCollectionChangedEventArgs ) null );
        }

        private void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs )
        {
            this.Axis = !this._axisCollection.IsNullOrEmpty<IAxis>() ? this._axisCollection.GetAxisById( this.XAxisId, false ) : ( IAxis ) null;
        }

        private void OnRenderableSeriesChanged( ObservableCollection<IRenderableSeries> renderableSeries )
        {
            if ( this._renderableSeries != null )
            {
                this._renderableSeries.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.RenderableSeriesOnCollectionChanged );
                this._renderableSeries = ( ObservableCollection<IRenderableSeries> ) null;
            }
            this._renderableSeries = renderableSeries;
            if ( this._renderableSeries == null )
            {
                return;
            }

            this._renderableSeries.CollectionChanged += new NotifyCollectionChangedEventHandler( this.RenderableSeriesOnCollectionChanged );
            this.RenderableSeriesOnCollectionChanged( ( object ) this, ( NotifyCollectionChangedEventArgs ) null );
        }

        private void RenderableSeriesOnCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( this.DataSeries != null || this.ParentSurface == null )
            {
                return;
            }

            IRenderableSeries renderableSeries = this.ParentSurface.RenderableSeries.FirstOrDefault<IRenderableSeries>();
            if ( this._renderSeriesDataSeriesPropertyNotifier != null )
            {
                this._renderSeriesDataSeriesPropertyNotifier.Dispose();
                this._renderSeriesDataSeriesPropertyNotifier = ( Ecng.Xaml.PropertyChangeNotifier ) null;
            }
            if ( renderableSeries == null )
            {
                return;
            }

            if ( renderableSeries is BaseRenderableSeries )
            {
                this._renderSeriesDataSeriesPropertyNotifier = new Ecng.Xaml.PropertyChangeNotifier( ( DependencyObject ) ( renderableSeries as BaseRenderableSeries ), BaseRenderableSeries.DataSeriesProperty );
                this._renderSeriesDataSeriesPropertyNotifier.ValueChanged += ( Action ) ( () => this.OnDataSeriesDependencyPropertyChanged( renderableSeries.DataSeries ) );
            }
            this.OnDataSeriesDependencyPropertyChanged( renderableSeries.DataSeries );
        }

        private void OnDataSeriesDependencyPropertyChanged( IDataSeries dataSeries )
        {
            this.SetCurrentValue( UltrachartOverview.DataSeriesProperty, ( object ) dataSeries );
        }

        private static void OnAxisIdDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartOverview ultrachartOverview = d as UltrachartOverview;
            ultrachartOverview?.OnXAxesCollectionChanged( ( object ) ultrachartOverview, ( NotifyCollectionChangedEventArgs ) null );
        }

        private static void OnAxisDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartOverview ultrachartOverview = d as UltrachartOverview;
            if ( ultrachartOverview == null )
            {
                return;
            }

            IAxis oldValue = e.OldValue as IAxis;
            if ( oldValue != null )
            {
                oldValue.DataRangeChanged -= new EventHandler<EventArgs>( ultrachartOverview.OnDataRangeChanged );
            }

            IAxis newValue = e.NewValue as IAxis;
            if ( newValue == null )
            {
                return;
            }

            newValue.DataRangeChanged += new EventHandler<EventArgs>( ultrachartOverview.OnDataRangeChanged );
            ultrachartOverview.SynchronizeXAxisType( newValue );
        }

        private void OnDataRangeChanged( object sender, EventArgs e )
        {
            this._renderTimerHelper.Invalidate();
        }

        private static void OnRenderableSeriesTypePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Type newValue = (Type) e.NewValue;
            if ( !newValue.IsAssignableFrom( typeof( BaseRenderableSeries ) ) )
            {
                return;
            } ( ( UltrachartOverview ) d ).SynchronizeRenderableSeriesType( ( ( UltrachartOverview ) d ).RenderableSeriesStyle, newValue );
        }

        private static void OnRenderableSeriesStylePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( UltrachartOverview ) d ).SynchronizeRenderableSeriesType( e.NewValue as Style, ( ( UltrachartOverview ) d ).RenderableSeriesType );
        }

        private void SynchronizeRenderableSeriesType( Style renderableSeriesStyle, Type renderableSeriesType )
        {
            if ( this._backgroundChartSurface == null )
            {
                return;
            }

            if ( this._backgroundChartSurface.RenderableSeries.IsEmpty<IRenderableSeries>() || this._backgroundChartSurface.RenderableSeries.First<IRenderableSeries>().GetType() != renderableSeriesType )
            {
                BaseRenderableSeries instance = (BaseRenderableSeries) Activator.CreateInstance(renderableSeriesType);
                instance.SetCurrentValue( FrameworkElement.DataContextProperty, ( object ) this );
                if ( renderableSeriesStyle == null || renderableSeriesStyle.TargetType.IsAssignableFrom( renderableSeriesType ) )
                {
                    instance.Style = renderableSeriesStyle;
                }

                using ( this._backgroundChartSurface.SuspendUpdates() )
                {
                    this._backgroundChartSurface.RenderableSeries.Clear();
                    this._backgroundChartSurface.RenderableSeries.Add( ( IRenderableSeries ) instance );
                }
            }
            else
            {
                this._backgroundChartSurface.RenderableSeries[ 0 ].Style = renderableSeriesStyle;
            }
        }
    }
}
