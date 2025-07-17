// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.UltrachartSurface
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using StockSharp.Xaml.Charting.ChartModifiers;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers;
using StockSharp.Xaml.Charting.Common.Messaging;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.Numerics.PointResamplers;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer;
using StockSharp.Xaml.Charting.Services;
using StockSharp.Xaml.Charting.StrategyManager;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Utility.Mouse;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using StockSharp.Xaml.Charting.Visuals.Axes;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Visuals
{
    [TemplatePart( Name = "PART_GridLinesArea", Type = typeof( StockSharp.Xaml.Charting.Visuals.Axes.GridLinesPanel ) )]
    [TemplatePart( Name = "PART_LeftAxisArea", Type = typeof( AxisArea ) )]
    [TemplatePart( Name = "PART_TopAxisArea", Type = typeof( AxisArea ) )]
    [TemplatePart( Name = "PART_RightAxisArea", Type = typeof( AxisArea ) )]
    [TemplatePart( Name = "PART_BottomAxisArea", Type = typeof( AxisArea ) )]
    [TemplatePart( Name = "PART_AnnotationsOverlaySurface", Type = typeof( AnnotationSurface ) )]
    [TemplatePart( Name = "PART_AnnotationsUnderlaySurface", Type = typeof( AnnotationSurface ) )]
    [TemplatePart( Name = "PART_ChartAdornerLayer", Type = typeof( Canvas ) )]
    public class UltrachartSurface : UltrachartSurfaceBase, ISciChartSurface, IUltrachartSurfaceBase, ISuspendable, IInvalidatableElement, IUltrachartController, IDisposable, IXmlSerializable
    {
        public static readonly DependencyProperty ClipUnderlayAnnotationsProperty   = DependencyProperty.Register(nameof (ClipUnderlayAnnotations), typeof (bool), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty ClipOverlayAnnotationsProperty    = DependencyProperty.Register(nameof (ClipOverlayAnnotations), typeof (bool), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty ZoomExtentsCommandProperty        = DependencyProperty.Register(nameof (ZoomExtentsCommand), typeof (ICommand), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty AnimateZoomExtentsCommandProperty = DependencyProperty.Register(nameof (AnimateZoomExtentsCommand), typeof (ICommand), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty XAxisProperty                     = DependencyProperty.Register(nameof (XAxis), typeof (IAxis), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnXAxisChanged)));
        public static readonly DependencyProperty YAxisProperty                     = DependencyProperty.Register(nameof (YAxis), typeof (IAxis), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnYAxisChanged)));
        public static readonly DependencyProperty YAxesProperty                     = DependencyProperty.Register(nameof (YAxes), typeof (AxisCollection), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnYAxesDependencyPropertyChanged)));
        public static readonly DependencyProperty XAxesProperty                     = DependencyProperty.Register(nameof (XAxes), typeof (AxisCollection), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnXAxesDependencyPropertyChanged)));
        public static readonly DependencyProperty AnnotationsProperty               = DependencyProperty.Register(nameof (Annotations), typeof (AnnotationCollection), typeof (UltrachartSurface), new PropertyMetadata(new PropertyChangedCallback(UltrachartSurface.OnAnnotationsDependencyPropertyChanged)));
        [Obsolete("We're Sorry! The AutoRangeOnStartup property has been deprecated in Ultrachart", true)]
        public static readonly DependencyProperty AutoRangeOnStartupProperty        = DependencyProperty.Register(nameof (AutoRangeOnStartup), typeof (bool), typeof (UltrachartSurface), new PropertyMetadata((object) true));
        public static readonly DependencyProperty ChartModifierProperty             = DependencyProperty.Register(nameof (ChartModifier), typeof (IChartModifier), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnChartModifierChanged)));
        public static readonly DependencyProperty LeftAxesPanelTemplateProperty     = DependencyProperty.Register(nameof (LeftAxesPanelTemplate), typeof (ItemsPanelTemplate), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty RightAxesPanelTemplateProperty    = DependencyProperty.Register(nameof (RightAxesPanelTemplate), typeof (ItemsPanelTemplate), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty TopAxesPanelTemplateProperty      = DependencyProperty.Register(nameof (TopAxesPanelTemplate), typeof (ItemsPanelTemplate), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty BottomAxesPanelTemplateProperty   = DependencyProperty.Register(nameof (BottomAxesPanelTemplate), typeof (ItemsPanelTemplate), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty CenterXAxesPanelTemplateProperty  = DependencyProperty.Register(nameof (CenterXAxesPanelTemplate), typeof (ItemsPanelTemplate), typeof (UltrachartSurface), new PropertyMetadata((object) null));
        public static readonly DependencyProperty CenterYAxesPanelTemplateProperty  = DependencyProperty.Register(nameof (CenterYAxesPanelTemplate), typeof (ItemsPanelTemplate), typeof (UltrachartSurface), new PropertyMetadata((object) null));
        public static readonly DependencyProperty GridLinesPanelStyleProperty       = DependencyProperty.Register(nameof (GridLinesPanelStyle), typeof (Style), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnChildStyleChanged)));
        public static readonly DependencyProperty RenderSurfaceStyleProperty        = DependencyProperty.Register(nameof (RenderSurfaceStyle), typeof (Style), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnChildStyleChanged)));
        public static readonly DependencyProperty RenderableSeriesProperty          = DependencyProperty.Register(nameof (RenderableSeries), typeof (ObservableCollection<IRenderableSeries>), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnRenderableSeriesDependencyPropertyChanged)));
        public static readonly DependencyProperty SelectedRenderableSeriesProperty  = DependencyProperty.Register(nameof (SelectedRenderableSeries), typeof (ObservableCollection<IRenderableSeries>), typeof (UltrachartSurface), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty ViewportManagerProperty           = DependencyProperty.Register(nameof (ViewportManager), typeof (IViewportManager), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnViewportManagerChanged)));
        public static readonly DependencyProperty SeriesSourceProperty              = DependencyProperty.Register(nameof (SeriesSource), typeof (ObservableCollection<IChartSeriesViewModel>), typeof (UltrachartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartSurface.OnSeriesSourceDependencyPropertyChanged)));
        public static readonly DependencyProperty IsPolarChartProperty              = DependencyProperty.Register(nameof (IsPolarChart), typeof (bool), typeof (UltrachartSurface), new PropertyMetadata((object) false, new PropertyChangedCallback(UltrachartSurface.OnIsPolarChartDependencyPropertyChanged)));
        private static char globalId = 'a';
        private readonly object _syncRoot = new object();
        private readonly HashSet<IDataSeries> _dsToNotify = new HashSet<IDataSeries>();
        private string id = UltrachartSurface.globalId++.ToString();
        private StockSharp.Xaml.Charting.Visuals.Axes.GridLinesPanel _gridLinesPanel;
        private AxisArea _bottomAxisArea;
        private AxisArea _topAxisArea;
        private AxisArea _rightAxisArea;
        private AxisArea _leftAxisArea;
        private AxisArea _centerXAxisArea;
        private AxisArea _centerYAxisArea;
        private IEventAggregator _eventAggregator;
        private IUltrachartRenderer _ultraChartRenderer;
        private bool _isRendering;
        private AnnotationSurface _overlayAnnotationCanvas;
        private AnnotationSurface _underlayAnnotationCanvas;
        private Canvas _adornerLayerCanvas;
        private IRenderSurface2D _renderSurface;
        private UltrachartSurface.RenderableSeriesCollection _renderableSeries;
        private static int loadedCount;

        public event EventHandler<AxisAlignmentChangedEventArgs> AxisAlignmentChanged;

        public event EventHandler XAxesCollectionNewCollectionAssigned;

        public event EventHandler YAxesCollectionNewCollectionAssigned;

        public event EventHandler AnnotationsCollectionNewCollectionAssigned;

        public UltrachartSurface()
        {
            DefaultStyleKey = ( object ) typeof( UltrachartSurface );
            _renderableSeries = new UltrachartSurface.RenderableSeriesCollection( this );
            SelectedRenderableSeries = new ObservableCollection<IRenderableSeries>();
            SetCurrentValue( UltrachartSurface.RenderableSeriesProperty, ( object ) new ObservableCollection<IRenderableSeries>() );
            SetCurrentValue( UltrachartSurface.YAxesProperty, ( object ) new AxisCollection() );
            SetCurrentValue( UltrachartSurface.XAxesProperty, ( object ) new AxisCollection() );
            SetCurrentValue( UltrachartSurface.AnnotationsProperty, ( object ) new AnnotationCollection() );
            SetCurrentValue( UltrachartSurface.ViewportManagerProperty, ( object ) new DefaultViewportManager() );
            SetCurrentValue( UltrachartSurfaceBase.RenderSurfaceProperty, ( object ) new HighSpeedRenderSurface() );
            ZoomExtentsCommand = ( ICommand ) new ActionCommand( new Action( ZoomExtents ) );
            AnimateZoomExtentsCommand = ( ICommand ) new ActionCommand( ( Action ) ( () => AnimateZoomExtents( TimeSpan.FromMilliseconds( 500.0 ) ) ) );
        }

        internal IDispatcherFacade DispatcherFacade
        {
            get
            {
                return Services.GetService<IDispatcherFacade>();
            }
        }

        public int LicenseDaysRemaining
        {
            get; internal set;
        }

        public ItemsPanelTemplate LeftAxesPanelTemplate
        {
            get
            {
                return ( ItemsPanelTemplate ) GetValue( UltrachartSurface.LeftAxesPanelTemplateProperty );
            }
            set
            {
                SetValue( UltrachartSurface.LeftAxesPanelTemplateProperty, ( object ) value );
            }
        }

        public ItemsPanelTemplate RightAxesPanelTemplate
        {
            get
            {
                return ( ItemsPanelTemplate ) GetValue( UltrachartSurface.RightAxesPanelTemplateProperty );
            }
            set
            {
                SetValue( UltrachartSurface.RightAxesPanelTemplateProperty, ( object ) value );
            }
        }

        public ItemsPanelTemplate BottomAxesPanelTemplate
        {
            get
            {
                return ( ItemsPanelTemplate ) GetValue( UltrachartSurface.BottomAxesPanelTemplateProperty );
            }
            set
            {
                SetValue( UltrachartSurface.BottomAxesPanelTemplateProperty, ( object ) value );
            }
        }

        public ItemsPanelTemplate TopAxesPanelTemplate
        {
            get
            {
                return ( ItemsPanelTemplate ) GetValue( UltrachartSurface.TopAxesPanelTemplateProperty );
            }
            set
            {
                SetValue( UltrachartSurface.TopAxesPanelTemplateProperty, ( object ) value );
            }
        }

        public ItemsPanelTemplate CenterXAxesPanelTemplate
        {
            get
            {
                return ( ItemsPanelTemplate ) GetValue( UltrachartSurface.CenterXAxesPanelTemplateProperty );
            }
            set
            {
                SetValue( UltrachartSurface.CenterXAxesPanelTemplateProperty, ( object ) value );
            }
        }

        public ItemsPanelTemplate CenterYAxesPanelTemplate
        {
            get
            {
                return ( ItemsPanelTemplate ) GetValue( UltrachartSurface.CenterYAxesPanelTemplateProperty );
            }
            set
            {
                SetValue( UltrachartSurface.CenterYAxesPanelTemplateProperty, ( object ) value );
            }
        }

        public bool ClipOverlayAnnotations
        {
            get
            {
                return ( bool ) GetValue( UltrachartSurface.ClipOverlayAnnotationsProperty );
            }
            set
            {
                SetValue( UltrachartSurface.ClipOverlayAnnotationsProperty, ( object ) value );
            }
        }

        public bool ClipUnderlayAnnotations
        {
            get
            {
                return ( bool ) GetValue( UltrachartSurface.ClipUnderlayAnnotationsProperty );
            }
            set
            {
                SetValue( UltrachartSurface.ClipUnderlayAnnotationsProperty, ( object ) value );
            }
        }

        public ObservableCollection<IRenderableSeries> RenderableSeries
        {
            get
            {
                return ( ObservableCollection<IRenderableSeries> ) GetValue( UltrachartSurface.RenderableSeriesProperty );
            }
            set
            {
                SetValue( UltrachartSurface.RenderableSeriesProperty, ( object ) value );
            }
        }

        public ObservableCollection<IRenderableSeries> SelectedRenderableSeries
        {
            get
            {
                return ( ObservableCollection<IRenderableSeries> ) GetValue( UltrachartSurface.SelectedRenderableSeriesProperty );
            }
            private set
            {
                SetValue( UltrachartSurface.SelectedRenderableSeriesProperty, ( object ) value );
            }
        }

        [Obsolete( "We're Sorry! The AutoRangeOnStartup property has been deprecated in Ultrachart", true )]
        public bool AutoRangeOnStartup
        {
            get
            {
                return ( bool ) GetValue( UltrachartSurface.AutoRangeOnStartupProperty );
            }
            set
            {
                SetValue( UltrachartSurface.AutoRangeOnStartupProperty, ( object ) value );
            }
        }

        public ICommand ZoomExtentsCommand
        {
            get
            {
                return ( ICommand ) GetValue( UltrachartSurface.ZoomExtentsCommandProperty );
            }
            set
            {
                SetValue( UltrachartSurface.ZoomExtentsCommandProperty, ( object ) value );
            }
        }

        public ICommand AnimateZoomExtentsCommand
        {
            get
            {
                return ( ICommand ) GetValue( UltrachartSurface.AnimateZoomExtentsCommandProperty );
            }
            set
            {
                SetValue( UltrachartSurface.AnimateZoomExtentsCommandProperty, ( object ) value );
            }
        }

        public IAxis XAxis
        {
            get
            {
                return ( IAxis ) GetValue( UltrachartSurface.XAxisProperty );
            }
            set
            {
                SetValue( UltrachartSurface.XAxisProperty, ( object ) value );
            }
        }

        public IAxis YAxis
        {
            get
            {
                return ( IAxis ) GetValue( UltrachartSurface.YAxisProperty );
            }
            set
            {
                SetValue( UltrachartSurface.YAxisProperty, ( object ) value );
            }
        }

        public AxisCollection YAxes
        {
            get
            {
                return ( AxisCollection ) GetValue( UltrachartSurface.YAxesProperty );
            }
            set
            {
                SetValue( UltrachartSurface.YAxesProperty, ( object ) value );
            }
        }

        public AxisCollection XAxes
        {
            get
            {
                return ( AxisCollection ) GetValue( UltrachartSurface.XAxesProperty );
            }
            set
            {
                SetValue( UltrachartSurface.XAxesProperty, ( object ) value );
            }
        }

        public AnnotationCollection Annotations
        {
            get
            {
                return ( AnnotationCollection ) GetValue( UltrachartSurface.AnnotationsProperty );
            }
            set
            {
                SetValue( UltrachartSurface.AnnotationsProperty, ( object ) value );
            }
        }

        public IViewportManager ViewportManager
        {
            get
            {
                return ( IViewportManager ) GetValue( UltrachartSurface.ViewportManagerProperty );
            }
            set
            {
                SetValue( UltrachartSurface.ViewportManagerProperty, ( object ) value );
            }
        }

        public IAnnotationCanvas AnnotationOverlaySurface
        {
            get
            {
                return ( IAnnotationCanvas ) _overlayAnnotationCanvas;
            }
        }

        public IAnnotationCanvas AnnotationUnderlaySurface
        {
            get
            {
                return ( IAnnotationCanvas ) _underlayAnnotationCanvas;
            }
        }

        public Canvas AdornerLayerCanvas
        {
            get
            {
                return _adornerLayerCanvas;
            }
        }

        public IChartModifier ChartModifier
        {
            get
            {
                return ( IChartModifier ) GetValue( UltrachartSurface.ChartModifierProperty );
            }
            set
            {
                SetValue( UltrachartSurface.ChartModifierProperty, ( object ) value );
            }
        }

        public IGridLinesPanel GridLinesPanel
        {
            get
            {
                return ( IGridLinesPanel ) _gridLinesPanel;
            }
        }

        public Style GridLinesPanelStyle
        {
            get
            {
                return ( Style ) GetValue( UltrachartSurface.GridLinesPanelStyleProperty );
            }
            set
            {
                SetValue( UltrachartSurface.GridLinesPanelStyleProperty, ( object ) value );
            }
        }

        public Style RenderSurfaceStyle
        {
            get
            {
                return ( Style ) GetValue( UltrachartSurface.RenderSurfaceStyleProperty );
            }
            set
            {
                SetValue( UltrachartSurface.RenderSurfaceStyleProperty, ( object ) value );
            }
        }

        public ObservableCollection<IChartSeriesViewModel> SeriesSource
        {
            get
            {
                return ( ObservableCollection<IChartSeriesViewModel> ) GetValue( UltrachartSurface.SeriesSourceProperty );
            }
            set
            {
                SetValue( UltrachartSurface.SeriesSourceProperty, ( object ) value );
            }
        }

        public bool IsPolarChart
        {
            get
            {
                return ( bool ) GetValue( UltrachartSurface.IsPolarChartProperty );
            }
            private set
            {
                SetValue( UltrachartSurface.IsPolarChartProperty, ( object ) value );
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DetachChildren();
            if ( _topAxisArea != null )
            {
                _topAxisArea.Items.Clear();
            }

            if ( _bottomAxisArea != null )
            {
                _bottomAxisArea.Items.Clear();
            }

            if ( _rightAxisArea != null )
            {
                _rightAxisArea.Items.Clear();
            }

            if ( _leftAxisArea != null )
            {
                _leftAxisArea.Items.Clear();
            }

            if ( _centerXAxisArea != null )
            {
                _centerXAxisArea.Items.Clear();
            }

            if ( _centerYAxisArea != null )
            {
                _centerYAxisArea.Items.Clear();
            }

            _gridLinesPanel = GetAndAssertTemplateChild<StockSharp.Xaml.Charting.Visuals.Axes.GridLinesPanel>( "PART_GridLinesArea" );
            _bottomAxisArea = GetAndAssertTemplateChild<AxisArea>( "PART_BottomAxisArea" );
            _topAxisArea = GetAndAssertTemplateChild<AxisArea>( "PART_TopAxisArea" );
            _rightAxisArea = GetAndAssertTemplateChild<AxisArea>( "PART_RightAxisArea" );
            _leftAxisArea = GetAndAssertTemplateChild<AxisArea>( "PART_LeftAxisArea" );
            _centerXAxisArea = GetAndAssertTemplateChild<AxisArea>( "PART_CenterXAxisArea" );
            _centerYAxisArea = GetAndAssertTemplateChild<AxisArea>( "PART_CenterYAxisArea" );
            _overlayAnnotationCanvas = GetAndAssertTemplateChild<AnnotationSurface>( "PART_AnnotationsOverlaySurface" );
            _underlayAnnotationCanvas = GetAndAssertTemplateChild<AnnotationSurface>( "PART_AnnotationsUnderlaySurface" );
            _adornerLayerCanvas = GetAndAssertTemplateChild<Canvas>( "PART_ChartAdornerLayer" );
            ( ( ServiceContainer ) Services ).RegisterService<IChartModifierSurface>( ModifierSurface );

            if ( GridLinesPanelStyle != null )
            {
                _gridLinesPanel.Style = GridLinesPanelStyle;
            }

            _gridLinesPanel.EventAggregator = _eventAggregator;
            YAxes.ForEachDo<IAxis>( new Action<IAxis>( PlaceAxis ) );
            XAxes.ForEachDo<IAxis>( new Action<IAxis>( PlaceAxis ) );
            AttachChildren();
            new StockSharp.Xaml.Licensing.Core.LicenseManager().Validate<UltrachartSurface>( this, ( IProviderFactory ) new UltrachartLicenseProviderFactory() );
            InvalidateElement();
        }

        public override void OnUltrachartRendered()
        {
            if ( ViewportManager != null )
            {
                ViewportManager.OnParentSurfaceRendered( ( ISciChartSurface ) this );
            }

            base.OnUltrachartRendered();
            NotifyAxes();
        }

        private void NotifyAxes()
        {
            lock ( _dsToNotify )
            {
                HashSet<IAxis> enumerable = new HashSet<IAxis>();
                foreach ( IRenderableSeries renderableSeries in ( Collection<IRenderableSeries> ) RenderableSeries )
                {
                    if ( _dsToNotify.Contains( renderableSeries.DataSeries ) )
                    {
                        enumerable.Add( renderableSeries.YAxis );
                        enumerable.Add( renderableSeries.XAxis );
                    }
                }
                enumerable.ForEachDo<IAxis>( new Action<IAxis>( AxisBase.NotifyDataRangeChanged ) );
                _dsToNotify.Clear();
            }
        }

        protected override void Dispose( bool disposing )
        {
            lock ( _dsToNotify )
            {
                _dsToNotify.Clear();
            }

            base.Dispose( disposing );
        }

        protected override Size MeasureOverride( Size constraint )
        {
            YAxes.ForEachDo<IAxis>( new Action<IAxis>( PlaceAxis ) );
            XAxes.ForEachDo<IAxis>( new Action<IAxis>( PlaceAxis ) );
            return base.MeasureOverride( constraint );
        }

        private void PlaceAxis( IAxis axis )
        {
            Tuple<AxisAlignment, bool> actualPlacementOf = GetActualPlacementOf(axis);
            AxisAlignment oldAlignment = actualPlacementOf.Item1;
            bool oldIsCenterAxis = actualPlacementOf.Item2;
            if ( oldAlignment == axis.AxisAlignment && oldIsCenterAxis == axis.IsCenterAxis )
            {
                return;
            }

            ChangeAxisContainer( axis, oldAlignment, oldIsCenterAxis );
        }

        private Tuple<AxisAlignment, bool> GetActualPlacementOf( IAxis axis )
        {
            AxisAlignment axisAlignment = AxisAlignment.Default;
            bool flag = false;
            if ( _leftAxisArea.Items.Contains( ( object ) axis ) )
            {
                axisAlignment = AxisAlignment.Left;
            }
            else if ( _rightAxisArea.Items.Contains( ( object ) axis ) )
            {
                axisAlignment = AxisAlignment.Right;
            }
            else if ( _topAxisArea.Items.Contains( ( object ) axis ) )
            {
                axisAlignment = AxisAlignment.Top;
            }
            else if ( _bottomAxisArea.Items.Contains( ( object ) axis ) )
            {
                axisAlignment = AxisAlignment.Bottom;
            }
            else if ( _centerXAxisArea.Items.Contains( ( object ) axis ) || _centerYAxisArea.Items.Contains( ( object ) axis ) )
            {
                axisAlignment = axis.AxisAlignment;
                flag = true;
            }
            return new Tuple<AxisAlignment, bool>( axisAlignment, flag );
        }

        private void DetachChildren()
        {
            UnsubscribeFromMouseEvents();
            if ( ChartModifier != null )
            {
                DetachModifier( ChartModifier );
            }

            if ( Annotations == null )
            {
                return;
            }

            Annotations.UnsubscribeSurfaceEvents( ( ISciChartSurface ) this );
        }

        private void AttachChildren()
        {
            if ( ChartModifier != null )
            {
                AttachModifier( ChartModifier );
            }

            if ( Annotations == null )
            {
                return;
            }

            Annotations.SubscribeSurfaceEvents( ( ISciChartSurface ) this );
        }

        public virtual void ZoomExtents()
        {
            ZoomExtents( TimeSpan.Zero );
        }

        public void AnimateZoomExtents( TimeSpan duration )
        {
            ZoomExtents( duration );
        }

        private void ZoomExtents( TimeSpan duration )
        {
            if ( XAxes.IsNullOrEmpty<IAxis>() || YAxes.IsNullOrEmpty<IAxis>() )
            {
                if ( !DebugWhyDoesntUltrachartRender )
                {
                    return;
                }

                string formatString = " UltrachartSurface didn't render, " + (object) RendererErrorCodes.BecauseXAxesOrYAxesIsNull;
                Console.WriteLine( formatString );
                UltrachartDebugLogger.Instance.WriteLine( formatString );
            }
            else
            {
                UltrachartDebugLogger.Instance.WriteLine( "ZoomExtents() called" );
                using ( SuspendUpdates() )
                {
                    ZoomExtentsY( ZoomExtentsXInternal( duration ), duration );
                }
            }
        }

        private IDictionary<string, IRange> ZoomExtentsXInternal( TimeSpan duration )
        {
            Dictionary<string, IRange> dictionary = new Dictionary<string, IRange>();
            foreach ( IAxis xax in ( Collection<IAxis> ) XAxes )
            {
                IRange maximumRange = xax.GetMaximumRange();
                xax.TrySetOrAnimateVisibleRange( maximumRange, duration );
                dictionary.Add( xax.Id, maximumRange );
            }
            return ( IDictionary<string, IRange> ) dictionary;
        }

        private void ZoomExtentsY( IDictionary<string, IRange> xRanges, TimeSpan duration )
        {
            foreach ( IAxis yax in ( Collection<IAxis> ) YAxes )
            {
                ZoomExtentsOnYAxis( yax, xRanges, duration );
            }
        }

        private void ZoomExtentsOnYAxis( IAxis axis, IDictionary<string, IRange> xRanges, TimeSpan duration )
        {
            IRange windowedYrange = axis.GetWindowedYRange(xRanges);
            axis.TrySetOrAnimateVisibleRange( windowedYrange, duration );
        }

        public void ZoomExtentsY()
        {
            UltrachartDebugLogger.Instance.WriteLine( "ZoomExtentsY() called" );
            using ( SuspendUpdates() )
            {
                ZoomExtentsY( ( IDictionary<string, IRange> ) null, TimeSpan.Zero );
            }
        }

        public void AnimateZoomExtentsY( TimeSpan duration )
        {
            using ( SuspendUpdates() )
            {
                ZoomExtentsY( ( IDictionary<string, IRange> ) null, duration );
            }
        }

        public void ZoomExtentsX()
        {
            ZoomExtentsXInternal( TimeSpan.Zero );
        }

        public void AnimateZoomExtentsX( TimeSpan duration )
        {
            ZoomExtentsXInternal( duration );
        }

        public new IUpdateSuspender SuspendUpdates()
        {
            return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this );
        }

        public new void ResumeUpdates( IUpdateSuspender updateSuspender )
        {
            if ( !updateSuspender.ResumeTargetOnDispose )
            {
                return;
            }

            InvalidateElement();
        }

        public new void DecrementSuspend()
        {
        }

        [Obsolete( "We're Sorry but ISciChartSurface.Clear() has been deprecated. Please call ISciChartSurface.RenderableSeries.Clear() to clear the chart", true )]
        public void ClearSeries()
        {
        }

        public static string VersionAndLicenseInfo
        {
            get
            {
                return string.Format( "Ultrachart v{0}", ( object ) new AssemblyName( typeof( UltrachartSurface ).Assembly.FullName ).Version );
            }
        }

        internal static string LicenseKey
        {
            get; private set;
        }

        public static void SetLicenseKey( string key )
        {
            UltrachartSurface.LicenseKey = key;
        }

        private void OnRenderSurfaceDraw( object sender, DrawEventArgs e )
        {
            if ( !IsLoaded || RenderPriority == RenderPriority.Immediate || _isRendering )
            {
                return;
            }

            _isRendering = true;
            Action render = (Action) (() =>
            {
                object syncRoot = SyncRoot;
                Monitor.Enter(syncRoot);
                DoDrawingLoop();
                Monitor.Exit(syncRoot);
                _isRendering = false;
            });
            if ( RenderPriority == RenderPriority.Normal )
            {
                render();
            }
            else
            {
                if ( RenderPriority != RenderPriority.Low )
                {
                    return;
                }

                CompositionSyncedDelegate compositionSyncedDelegate;
                Services.GetService<IDispatcherFacade>().BeginInvoke( ( Action ) ( () => compositionSyncedDelegate = new CompositionSyncedDelegate( render ) ), DispatcherPriority.Input );
            }
        }

        protected override void DoDrawingLoop()
        {
            if ( IsDisposed || _renderSurface == null )
            {
                return;
            }

            if ( Visibility != Visibility.Visible )
            {
                return;
            }

            try
            {
                using ( IRenderContext2D renderContext = _renderSurface.GetRenderContext() )
                {
                    try
                    {
                        RendererErrorCode rendererErrorCode = _ultraChartRenderer.RenderLoop(renderContext);
                        if ( RendererErrorCodes.Success.Equals( ( object ) rendererErrorCode ) || !DebugWhyDoesntUltrachartRender )
                        {
                            return;
                        }

                        string formatString = " UltrachartSurface didn't render, " + (object) rendererErrorCode;
                        Console.WriteLine( formatString );
                        UltrachartDebugLogger.Instance.WriteLine( formatString );
                    }
                    catch ( Exception ex )
                    {
                        OnRenderFault( ex );
                    }
                }
            }
            catch ( Exception ex )
            {
                OnRenderFault( ex );
            }
        }

        [Obsolete( "Obsolete. Please use UltrachartSurface.RootGrid.TranslatePoint instead", true )]
        public Point TranslatePoint( Point point, IHitTestable relativeTo )
        {
            throw new NotImplementedException( "Obsolete. Please use UltrachartSurface.RootGrid.TranslatePoint instead" );
        }

        [Obsolete( "Obsolete. Please use UltrachartSurface.RootGrid.IsPointWithinBounds instead", true )]
        public bool IsPointWithinBounds( Point point )
        {
            throw new NotImplementedException( "Obsolete. Please use UltrachartSurface.RootGrid.TranslatePoint instead" );
        }

        [Obsolete( "Obsolete. Please use UltrachartSurface.RootGrid.GetBoundsRelativeTo instead", true )]
        public Rect GetBoundsRelativeTo( IHitTestable relativeTo )
        {
            throw new NotImplementedException( "Obsolete. Please use UltrachartSurface.RootGrid.GetBoundsRelativeTo instead" );
        }

        public void AttachDataSeries( IDataSeries dataSeries )
        {
            if ( dataSeries == null )
            {
                return;
            }

            dataSeries.ParentSurface = dataSeries.ParentSurface ?? ( ISciChartSurface ) this;
            if ( IsUltrachartSurfaceLoaded )
            {
                dataSeries.DataSeriesChanged -= new EventHandler<DataSeriesChangedEventArgs>( OnDataSeriesChanged );
                dataSeries.DataSeriesChanged += new EventHandler<DataSeriesChangedEventArgs>( OnDataSeriesChanged );
            }
            TryAddDataSeriesForNotification( dataSeries );
        }

        public void DetachDataSeries( IDataSeries dataSeries )
        {
            if ( dataSeries == null )
            {
                return;
            }

            if ( dataSeries.ParentSurface == this )
            {
                dataSeries.ParentSurface = ( ISciChartSurface ) null;
            }

            dataSeries.DataSeriesChanged -= new EventHandler<DataSeriesChangedEventArgs>( OnDataSeriesChanged );
            TryAddDataSeriesForNotification( dataSeries );
        }

        private void OnDataSeriesChanged( object sender, DataSeriesChangedEventArgs e )
        {
            TryAddDataSeriesForNotification( sender as IDataSeries );
            OnDataSeriesUpdated( sender, ( EventArgs ) e );
        }

        private void TryAddDataSeriesForNotification( IDataSeries dataSeries )
        {
            if ( dataSeries == null )
            {
                return;
            }

            lock ( _dsToNotify )
            {
                _dsToNotify.Add( dataSeries );
            }
        }

        private void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            if ( args.OldItems != null )
            {
                foreach ( IAxis oldItem in ( IEnumerable ) args.OldItems )
                {
                    DetachAxis( oldItem );
                }
            }
            if ( args.NewItems != null )
            {
                foreach ( IAxis newItem in ( IEnumerable ) args.NewItems )
                {
                    AttachAxis( newItem, false );
                }
            }
            if ( args.Action == NotifyCollectionChangedAction.Reset )
            {
                ClearAxisAreasFrom( false );
            }

            if ( Annotations != null )
            {
                Annotations.OnYAxesCollectionChanged( sender, args );
            }

            if ( ChartModifier != null )
            {
                ChartModifier.OnYAxesCollectionChanged( sender, args );
            }

            InvalidateElement();
        }

        private void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            if ( args.OldItems != null )
            {
                foreach ( IAxis oldItem in ( IEnumerable ) args.OldItems )
                {
                    DetachAxis( oldItem );
                }
            }
            if ( args.NewItems != null )
            {
                foreach ( IAxis newItem in ( IEnumerable ) args.NewItems )
                {
                    AttachAxis( newItem, true );
                }
            }
            if ( args.Action == NotifyCollectionChangedAction.Reset )
            {
                ClearAxisAreasFrom( true );
            }

            if ( Annotations != null )
            {
                Annotations.OnXAxesCollectionChanged( sender, args );
            }

            if ( ChartModifier != null )
            {
                ChartModifier.OnXAxesCollectionChanged( sender, args );
            }

            InvalidateElement();
        }

        private void ClearAxisAreasFrom( bool isXAxes )
        {
            ClearAxisAreaFrom( _rightAxisArea, isXAxes );
            ClearAxisAreaFrom( _leftAxisArea, isXAxes );
            ClearAxisAreaFrom( _topAxisArea, isXAxes );
            ClearAxisAreaFrom( _bottomAxisArea, isXAxes );
            ClearAxisAreaFrom( _centerXAxisArea, isXAxes );
            ClearAxisAreaFrom( _centerYAxisArea, isXAxes );
        }

        private void ClearAxisAreaFrom( AxisArea area, bool isXAxes )
        {
            if ( area == null )
            {
                return;
            }

            foreach ( IAxis axis in area.Items.Cast<IAxis>().Where<IAxis>( ( Func<IAxis, bool> ) ( axis => axis.IsXAxis == isXAxes ) ).ToList<IAxis>() )
            {
                DetachAxis( axis );
            }
        }

        private void DetachAxis( IAxis axis )
        {
            RemoveFromAxisContainer( axis, axis.AxisAlignment, axis.IsCenterAxis );
            axis.ParentSurface = ( ISciChartSurface ) null;
            axis.Services = ( IServiceContainer ) null;
        }

        private void AttachAxis( IAxis axis, bool isXAxis )
        {
            axis.ParentSurface = ( ISciChartSurface ) this;
            axis.IsXAxis = isXAxis;
            if ( axis.AxisAlignment == AxisAlignment.Default )
            {
                AxisAlignment axisAlignment = axis.IsXAxis ? AxisAlignment.Bottom : AxisAlignment.Right;
                ( ( DependencyObject ) axis ).SetCurrentValue( AxisBase.AxisAlignmentProperty, ( object ) axisAlignment );
            }
            else
            {
                AddToAxisContainer( axis, axis.AxisAlignment, axis.IsCenterAxis );
            }
        }

        private void OnRenderableSeriesCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == NotifyCollectionChangedAction.Reset )
            {
                _renderableSeries.Clear();
            }

            if ( e.OldItems != null )
            {
                e.OldItems.Cast<IRenderableSeries>().ForEachDo<IRenderableSeries>( ( Action<IRenderableSeries> ) ( series => _renderableSeries.Remove( series ) ) );
            }

            if ( e.NewItems == null )
            {
                return;
            }

            e.NewItems.Cast<IRenderableSeries>().ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( ( ( Collection<IRenderableSeries> ) _renderableSeries ).Add ) );
        }

        private void DetachRenderableSeries( IRenderableSeries rSeries )
        {
            if ( _renderSurface != null )
            {
                rSeries.SelectionChanged -= new EventHandler( OnSeriesSelectionChanged );
                _renderSurface.RemoveSeries( rSeries );
            }
            if ( rSeries.DataSeries != null )
            {
                DetachDataSeries( rSeries.DataSeries );
            }

            if ( rSeries.IsSelected )
            {
                ( ( DependencyObject ) rSeries ).SetCurrentValue( BaseRenderableSeries.IsSelectedProperty, ( object ) false );
            }

            if ( SelectedRenderableSeries.Contains( rSeries ) )
            {
                SelectedRenderableSeries.Remove( rSeries );
            }

            if ( rSeries is IStackedColumnRenderableSeries )
            {
                StackedColumnsWrapper.RemoveSeries( ( IStackedColumnRenderableSeries ) rSeries );
                if ( StackedColumnsWrapper.GetStackedSeriesCount() == 0 )
                {
                    StackedColumnsWrapper = ( IStackedColumnsWrapper ) null;
                }
            }
            if ( rSeries is IStackedMountainRenderableSeries )
            {
                StackedMountainsWrapper.RemoveSeries( ( IStackedMountainRenderableSeries ) rSeries );
                if ( StackedMountainsWrapper.GetStackedSeriesCount() == 0 )
                {
                    StackedMountainsWrapper = ( IStackedMountainsWrapper ) null;
                }
            }
            InvalidateElement();
        }

        private void AttachRenderableSeries( IRenderableSeries rSeries )
        {
            if ( _renderSurface != null )
            {
                _renderSurface.AddSeries( rSeries );
                rSeries.SelectionChanged -= new EventHandler( OnSeriesSelectionChanged );
                rSeries.SelectionChanged += new EventHandler( OnSeriesSelectionChanged );
                if ( rSeries.IsSelected && !SelectedRenderableSeries.Contains( rSeries ) )
                {
                    SelectedRenderableSeries.Add( rSeries );
                }
            }
            if ( rSeries.DataSeries != null )
            {
                AttachDataSeries( rSeries.DataSeries );
            }

            if ( rSeries is IStackedColumnRenderableSeries )
            {
                if ( StackedColumnsWrapper == null )
                {
                    StackedColumnsWrapper = ( IStackedColumnsWrapper ) new StockSharp.Xaml.Charting.Visuals.RenderableSeries.StackedColumnsWrapper();
                }

                StackedColumnsWrapper.AddSeries( ( IStackedColumnRenderableSeries ) rSeries );
            }
            if ( rSeries is IStackedMountainRenderableSeries )
            {
                if ( StackedMountainsWrapper == null )
                {
                    StackedMountainsWrapper = ( IStackedMountainsWrapper ) new StockSharp.Xaml.Charting.Visuals.RenderableSeries.StackedMountainsWrapper();
                }

                StackedMountainsWrapper.AddSeries( ( IStackedMountainRenderableSeries ) rSeries );
            }
            InvalidateElement();
        }

        private void DetachChartSeries( IChartSeriesViewModel oldItem )
        {
            oldItem.PropertyChanged -= new PropertyChangedEventHandler( ChartSeriesViewModelPropertyListener );
            if ( oldItem.RenderSeries == null || !RenderableSeries.Contains( oldItem.RenderSeries ) )
            {
                return;
            }

            RenderableSeries.Remove( oldItem.RenderSeries );
        }

        private void AttachChartSeries( IChartSeriesViewModel newItem )
        {
            Guard.NotNull( ( object ) newItem, nameof( newItem ) );
            newItem.PropertyChanged -= new PropertyChangedEventHandler( ChartSeriesViewModelPropertyListener );
            newItem.PropertyChanged += new PropertyChangedEventHandler( ChartSeriesViewModelPropertyListener );
            int index = SeriesSource.IndexOf(newItem);
            if ( newItem.RenderSeries == null )
            {
                return;
            }

            if ( !RenderableSeries.Contains( newItem.RenderSeries ) )
            {
                RenderableSeries.Insert( index, newItem.RenderSeries );
            }

            newItem.RenderSeries.DataSeries = newItem.DataSeries;
        }

        private void OnSeriesSelectionChanged( object sender, EventArgs e )
        {
            IRenderableSeries renderableSeries = sender as IRenderableSeries;
            if ( renderableSeries == null )
            {
                return;
            }

            if ( renderableSeries.IsSelected )
            {
                SelectedRenderableSeries.Add( renderableSeries );
            }
            else
            {
                SelectedRenderableSeries.Remove( renderableSeries );
            }
        }

        protected override void OnUltrachartSurfaceLoaded()
        {
            if ( IsUltrachartSurfaceLoaded || IsDisposed )
            {
                return;
            }

            base.OnUltrachartSurfaceLoaded();
            Interlocked.Increment( ref UltrachartSurface.loadedCount );
            if ( SeriesSource != null )
            {
                SeriesSource.CollectionChanged -= new NotifyCollectionChangedEventHandler( OnChartSeriesCollectionChanged );
                SeriesSource.CollectionChanged += new NotifyCollectionChangedEventHandler( OnChartSeriesCollectionChanged );
                foreach ( IChartSeriesViewModel newItem in ( Collection<IChartSeriesViewModel> ) SeriesSource )
                {
                    AttachChartSeries( newItem );
                }
            }
            RenderableSeries.ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( AttachRenderableSeries ) );
            XAxes.ForEachDo<IAxis>( ( Action<IAxis> ) ( axis => AttachAxis( axis, true ) ) );
            YAxes.ForEachDo<IAxis>( ( Action<IAxis> ) ( axis => AttachAxis( axis, false ) ) );
            AttachChildren();
        }

        protected override void OnUltrachartSurfaceUnloaded()
        {
            if ( !IsUltrachartSurfaceLoaded )
            {
                return;
            }

            base.OnUltrachartSurfaceUnloaded();
            Interlocked.Decrement( ref UltrachartSurface.loadedCount );
            if ( SeriesSource != null )
            {
                foreach ( IChartSeriesViewModel oldItem in ( Collection<IChartSeriesViewModel> ) SeriesSource )
                {
                    DetachChartSeries( oldItem );
                }

                SeriesSource.CollectionChanged -= new NotifyCollectionChangedEventHandler( OnChartSeriesCollectionChanged );
            }
            RenderableSeries.ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( DetachRenderableSeries ) );
            XAxes.ForEachDo<IAxis>( new Action<IAxis>( DetachAxis ) );
            YAxes.ForEachDo<IAxis>( new Action<IAxis>( DetachAxis ) );
            DetachChildren();
        }

        public Size OnArrangeUltrachart()
        {
            int num = RenderSurface.NeedsResizing ? 1 : 0;
            return new Size( RenderSurface.ActualWidth, RenderSurface.ActualHeight );
        }

        [Obsolete( "ISciChartSurface.GetWindowedYRange is obsolete. Use IAxis.GetWindowedYRange instead", true )]
        public IRange GetWindowedYRange( IAxis yAxis, IRange xRange )
        {
            IEnumerable<IDataSeries> dataSeriesFor = GetDataSeriesFor(yAxis.Id);
            IRange maximumRange = yAxis.GetMaximumRange();
            if ( !dataSeriesFor.Any<IDataSeries>() )
            {
                if ( yAxis.VisibleRange != null && !yAxis.VisibleRange.IsDefined )
                {
                    return maximumRange;
                }

                return yAxis.VisibleRange;
            }
            IRange[] array = dataSeriesFor.Select<IDataSeries, IRange>((Func<IDataSeries, IRange>) (ds => ds.GetWindowedYRange(xRange, yAxis.IsLogarithmicAxis))).Where<IRange>((Func<IRange, bool>) (range => range.IsDefined)).ToArray<IRange>();
            IRange range1 = ((IEnumerable<IRange>) array).FirstOrDefault<IRange>();
            if ( range1 != null )
            {
                for ( int index = 1 ; index < array.Length ; ++index )
                {
                    range1 = range1.Union( array[ index ] );
                }

                if ( yAxis.GrowBy != null && yAxis.GrowBy.IsDefined )
                {
                    range1.GrowBy( yAxis.GrowBy.Min, yAxis.GrowBy.Max );
                }
            }
            return range1 ?? maximumRange;
        }

        public void OnIsCenterAxisChanged( IAxis axis )
        {
            ChangeAxisContainer( axis, axis.AxisAlignment, !axis.IsCenterAxis );
        }

        public void OnAxisAlignmentChanged( IAxis axis, AxisAlignment oldValue )
        {
            AxisAlignment axisAlignment = axis.AxisAlignment;
            ChangeAxisContainer( axis, oldValue, axis.IsCenterAxis );
            OnAxisAlignmentChanged( new AxisAlignmentChangedEventArgs( axis.Id, oldValue, axisAlignment ) );
        }

        private void ChangeAxisContainer( IAxis axis, AxisAlignment oldAlignment, bool oldIsCenterAxis )
        {
            AxisAlignment axisAlignment = axis.AxisAlignment;
            bool isCenterAxis = axis.IsCenterAxis;
            using ( axis.SuspendUpdates() )
            {
                RemoveFromAxisContainer( axis, oldAlignment, oldIsCenterAxis );
                AddToAxisContainer( axis, axisAlignment, isCenterAxis );
            }
        }

        private void RemoveFromAxisContainer( IAxis axis, AxisAlignment alignment, bool isCenterAxis )
        {
            AxisArea containerFor = GetContainerFor(axis, alignment, isCenterAxis);
            if ( containerFor != null && axis.ParentSurface != null )
            {
                containerFor.SafeRemoveItem( ( object ) axis );
            }

            InvalidateIsPolarChartProperty();
        }

        private void InvalidateIsPolarChartProperty()
        {
            if ( _centerXAxisArea == null || _centerYAxisArea == null )
            {
                return;
            }

            IsPolarChart = _centerXAxisArea.Items.OfType<IAxis>().Any<IAxis>( ( Func<IAxis, bool> ) ( x => x.IsPolarAxis ) ) || _centerYAxisArea.Items.OfType<IAxis>().Any<IAxis>( ( Func<IAxis, bool> ) ( x => x.IsPolarAxis ) );
        }

        private AxisArea GetContainerFor( IAxis axis, AxisAlignment axisAlignment, bool isCenterAxis )
        {
            if ( isCenterAxis )
            {
                if ( !axis.IsXAxis )
                {
                    return _centerYAxisArea;
                }

                return _centerXAxisArea;
            }
            AxisArea axisArea = (AxisArea) null;
            switch ( axisAlignment )
            {
                case AxisAlignment.Right:
                    axisArea = _rightAxisArea;
                    break;
                case AxisAlignment.Left:
                    axisArea = _leftAxisArea;
                    break;
                case AxisAlignment.Top:
                    axisArea = _topAxisArea;
                    break;
                case AxisAlignment.Bottom:
                    axisArea = _bottomAxisArea;
                    break;
            }
            return axisArea;
        }

        private void AddToAxisContainer( IAxis axis, AxisAlignment alignment, bool isCenterAxis )
        {
            AxisArea containerFor = GetContainerFor(axis, alignment, isCenterAxis);
            if ( containerFor != null && !containerFor.Items.Contains( ( object ) axis ) )
            {
                if ( containerFor != _centerXAxisArea && containerFor != _centerYAxisArea && ( alignment == AxisAlignment.Left || alignment == AxisAlignment.Top ) )
                {
                    containerFor.Items.Insert( 0, ( object ) axis );
                }
                else
                {
                    containerFor.Items.Add( ( object ) axis );
                }
            }
            InvalidateIsPolarChartProperty();
        }

        private void OnAxisAlignmentChanged( AxisAlignmentChangedEventArgs args )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<AxisAlignmentChangedEventArgs> alignmentChanged = AxisAlignmentChanged;
            if ( alignmentChanged == null )
            {
                return;
            }

            alignmentChanged( ( object ) this, args );
        }

        public XmlSchema GetSchema()
        {
            return ( XmlSchema ) null;
        }

        public virtual void ReadXml( XmlReader reader )
        {
            if ( reader.MoveToContent() != XmlNodeType.Element || !( reader.LocalName == GetType().Name ) )
            {
                return;
            }

            UltrachartSurfaceSerializationHelper.Instance.DeserializeProperties( this, reader );
        }

        public virtual void WriteXml( XmlWriter writer )
        {
            UltrachartSurfaceSerializationHelper.Instance.SerializeProperties( this, writer );
        }

        public BitmapSource ExportToBitmapSource()
        {
            RenderPriority renderPriority = RenderPriority;
            try
            {
                PrepareSurface( Width, Height );
                return BitmapPrintingHelper.ExportToBitmapSource( this );
            }
            finally
            {
                RenderPriority = renderPriority;
            }
        }

        private void PrepareSurface( double width, double height )
        {
            if ( IsLoaded )
            {
                return;
            }

            Size size = new Size(width, height);
            RenderPriority = RenderPriority.Immediate;
            ApplyTemplate();
            OnLoad();
            if ( Annotations != null )
            {
                Annotations.OfType<FrameworkElement>().ForEachDo<FrameworkElement>( ( Action<FrameworkElement> ) ( annotation => annotation.RaiseEvent( new RoutedEventArgs( FrameworkElement.LoadedEvent ) ) ) );
            }

            Measure( size );
            Arrange( new Rect( new Point( 0.0, 0.0 ), size ) );
            UpdateLayout();
            InvalidateElement();
            UpdateLayout();
            InvalidateElement();
            UpdateLayout();
        }

        public void ExportToFile( string fileName, ExportType exportType )
        {
            BitmapPrintingHelper.SaveToFile( ExportToBitmapSource(), fileName, exportType );
        }

        public void Print( string description = null )
        {
            RenderPriority renderPriority = RenderPriority;
            try
            {
                double width   = Width;
                double height  = Height;
                var dialog     = new PrintDialog();
                bool? nullable = dialog.ShowDialog();
                bool flag      = true;

                if ( ( nullable.GetValueOrDefault() == flag ? ( nullable.HasValue ? 1 : 0 ) : 0 ) == 0 )
                {
                    return;
                }

                PrepareSurface( dialog.PrintableAreaWidth, dialog.PrintableAreaHeight );

                Action toBeDone = delegate()
                {
                    dialog.PrintVisual( ( Visual ) this, description );
                };

                Dispatcher.BeginInvoke( toBeDone );
            }
            finally
            {
                RenderPriority = renderPriority;
            }
        }

        protected override void OnUltrachartSurfaceSizeChanged()
        {
            UltrachartDebugLogger.Instance.WriteLine( "UltrachartSurface Resized: x={0}\ty={1}", ( object ) ActualWidth, ( object ) ActualHeight );
            _eventAggregator.Publish<UltrachartResizedMessage>( new UltrachartResizedMessage( ( object ) this ) );
            base.OnUltrachartSurfaceSizeChanged();
        }

        protected override void OnDataContextChanged( DependencyPropertyChangedEventArgs e )
        {
            if ( ChartModifier == null )
            {
                return;
            }

            ChartModifier.DataContext = e.NewValue;
        }

        private static void OnViewportManagerChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ISciChartSurface scs = (ISciChartSurface) d;
            IViewportManager newValue = e.NewValue as IViewportManager;
            ( e.OldValue as IViewportManager )?.DetachUltrachartSurface();
            newValue?.AttachUltrachartSurface( scs );
            scs.InvalidateElement();
        }

        private static void OnAnnotationsDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            AnnotationCollection newValue = e.NewValue as AnnotationCollection;
            AnnotationCollection oldValue = e.OldValue as AnnotationCollection;
            if ( oldValue != null )
            {
                oldValue.ParentSurface = ( ISciChartSurface ) null;
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( UltrachartSurface.AnnotationCollectionChanged );
            }
            if ( newValue != null )
            {
                newValue.ParentSurface = ( ISciChartSurface ) ultrachartSurface;
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( UltrachartSurface.AnnotationCollectionChanged );
                ultrachartSurface.InvalidateElement();
            }
            if ( ultrachartSurface.ChartModifier == null )
            {
                return;
            }

            UltrachartSurface.AnnotationCollectionChanged( ( object ) ultrachartSurface, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
        }

        private static void AnnotationCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            AnnotationCollection annotationCollection = sender as AnnotationCollection;
            ISciChartSurface ultrachartSurface = annotationCollection != null ? annotationCollection.ParentSurface : sender as ISciChartSurface;
            if ( ultrachartSurface == null || ultrachartSurface.ChartModifier == null )
            {
                return;
            }

            ultrachartSurface.ChartModifier.OnAnnotationCollectionChanged( sender, args );
        }

        private static void OnRenderableSeriesDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            using ( ultrachartSurface.SuspendUpdates() )
            {
                ultrachartSurface._renderableSeries.Clear();
                ObservableCollection<IRenderableSeries> newValue = e.NewValue as ObservableCollection<IRenderableSeries>;
                ObservableCollection<IRenderableSeries> oldValue = e.OldValue as ObservableCollection<IRenderableSeries>;
                if ( oldValue != null )
                {
                    oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( ultrachartSurface.OnRenderableSeriesCollectionChanged );
                }

                if ( newValue == null )
                {
                    return;
                }

                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( ultrachartSurface.OnRenderableSeriesCollectionChanged );
                newValue.ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( ( ( Collection<IRenderableSeries> ) ultrachartSurface._renderableSeries ).Add ) );
            }
        }

        private static void OnYAxesDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            AxisCollection newValue = e.NewValue as AxisCollection;
            AxisCollection oldValue = e.OldValue as AxisCollection;
            if ( oldValue != null )
            {
                foreach ( IAxis axis in ( Collection<IAxis> ) oldValue )
                {
                    ultrachartSurface.DetachAxis( axis );
                }

                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( ultrachartSurface.OnYAxesCollectionChanged );
            }
            if ( newValue != null )
            {
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( ultrachartSurface.OnYAxesCollectionChanged );
                foreach ( IAxis axis in ( Collection<IAxis> ) newValue )
                {
                    ultrachartSurface.AttachAxis( axis, false );
                }
            }
            if ( ultrachartSurface.Annotations != null )
            {
                ultrachartSurface.Annotations.OnYAxesCollectionChanged( ( object ) ultrachartSurface, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
            }

            if ( ultrachartSurface.ChartModifier == null )
            {
                return;
            }

            ultrachartSurface.ChartModifier.OnYAxesCollectionChanged( ( object ) ultrachartSurface, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
        }

        private static void OnXAxesDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            AxisCollection newValue = e.NewValue as AxisCollection;
            AxisCollection oldValue = e.OldValue as AxisCollection;
            if ( oldValue != null )
            {
                foreach ( IAxis axis in ( Collection<IAxis> ) oldValue )
                {
                    ultrachartSurface.DetachAxis( axis );
                }

                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( ultrachartSurface.OnXAxesCollectionChanged );
            }
            if ( newValue != null )
            {
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( ultrachartSurface.OnXAxesCollectionChanged );
                foreach ( IAxis axis in ( Collection<IAxis> ) newValue )
                {
                    ultrachartSurface.AttachAxis( axis, true );
                }
            }
            if ( ultrachartSurface.Annotations != null )
            {
                ultrachartSurface.Annotations.OnXAxesCollectionChanged( ( object ) ultrachartSurface, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
            }

            if ( ultrachartSurface.ChartModifier == null )
            {
                return;
            }

            ultrachartSurface.ChartModifier.OnXAxesCollectionChanged( ( object ) ultrachartSurface, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
        }

        private static void OnYAxisChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            IAxis oldValue = e.OldValue as IAxis;
            IAxis newValue = e.NewValue as IAxis;
            if ( oldValue != null )
            {
                ultrachartSurface.YAxes.Remove( oldValue );
            }

            if ( newValue == null )
            {
                return;
            }

            UltrachartDebugLogger.Instance.WriteLine( "Inserting Primary Y-Axis" );
            ultrachartSurface.YAxes.Insert( 0, newValue );
        }

        private static void OnXAxisChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            IAxis oldValue = e.OldValue as IAxis;
            if ( oldValue != null )
            {
                ultrachartSurface.XAxes.Remove( oldValue );
            }

            IAxis newValue = e.NewValue as IAxis;
            if ( newValue == null )
            {
                return;
            }

            UltrachartDebugLogger.Instance.WriteLine( "Inserting Primary X-Axis" );
            ultrachartSurface.XAxes.Insert( 0, newValue );
        }

        private static void OnIsPolarChartDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = d as UltrachartSurface;
            if ( ultrachartSurface == null )
            {
                return;
            }

            CoordinateSystem coordinateSystem = (bool) e.NewValue ? CoordinateSystem.Polar : CoordinateSystem.Cartesian;
            ultrachartSurface.Services.GetService<IEventAggregator>().Publish<CoordinateSystemMessage>( new CoordinateSystemMessage( ( object ) ultrachartSurface, coordinateSystem ) );
        }

        private static void OnChildStyleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            StockSharp.Xaml.Charting.Visuals.Axes.GridLinesPanel gridLinesPanel = ultrachartSurface.GridLinesPanel as StockSharp.Xaml.Charting.Visuals.Axes.GridLinesPanel;
            if ( gridLinesPanel != null )
            {
                gridLinesPanel.Style = ultrachartSurface.GridLinesPanelStyle;
            }

            IRenderSurface2D renderSurface = ultrachartSurface.RenderSurface as IRenderSurface2D;
            if ( renderSurface != null )
            {
                renderSurface.Style = ultrachartSurface.RenderSurfaceStyle;
            }

            UltrachartDebugLogger.Instance.WriteLine( nameof( OnChildStyleChanged ) );
        }

        protected override void OnRenderSurfaceDependencyPropertyChanged( DependencyPropertyChangedEventArgs e )
        {
            base.OnRenderSurfaceDependencyPropertyChanged( e );
            IRenderSurface2D oldValue = e.OldValue as IRenderSurface2D;
            if ( oldValue != null )
            {
                oldValue.Draw -= new EventHandler<DrawEventArgs>( OnRenderSurfaceDraw );
                oldValue.Services = ( IServiceContainer ) null;
                oldValue.ClearSeries();
                oldValue.Dispose();
            }
            IRenderSurface2D newValue = e.NewValue as IRenderSurface2D;
            if ( newValue != null )
            {
                newValue.Draw += new EventHandler<DrawEventArgs>( OnRenderSurfaceDraw );
                newValue.Services = Services;
                if ( RenderSurfaceStyle != null )
                {
                    newValue.Style = RenderSurfaceStyle;
                }

                newValue.AddSeries( ( IEnumerable<IRenderableSeries> ) RenderableSeries );
            }
            _renderSurface = newValue;
        }

        private static void OnSeriesSourceDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            ObservableCollection<IChartSeriesViewModel> newValue = e.NewValue as ObservableCollection<IChartSeriesViewModel>;
            ObservableCollection<IChartSeriesViewModel> oldValue = e.OldValue as ObservableCollection<IChartSeriesViewModel>;
            if ( oldValue != null )
            {
                foreach ( IChartSeriesViewModel oldItem in ( Collection<IChartSeriesViewModel> ) oldValue )
                {
                    ultrachartSurface.DetachChartSeries( oldItem );
                }

                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( ultrachartSurface.OnChartSeriesCollectionChanged );
            }
            if ( newValue == null )
            {
                return;
            }

            newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( ultrachartSurface.OnChartSeriesCollectionChanged );
            foreach ( IChartSeriesViewModel newItem in ( Collection<IChartSeriesViewModel> ) newValue )
            {
                ultrachartSurface.AttachChartSeries( newItem );
            }
        }

        private static void OnChartModifierChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = (UltrachartSurface) d;
            IChartModifier oldValue = e.OldValue as IChartModifier;
            if ( oldValue != null )
            {
                ultrachartSurface.DetachModifier( oldValue );
            }

            IChartModifier newValue = e.NewValue as IChartModifier;
            if ( newValue != null )
            {
                ultrachartSurface.AttachModifier( newValue );
            }

            UltrachartDebugLogger.Instance.WriteLine( nameof( OnChartModifierChanged ) );
            ultrachartSurface.InvalidateElement();
        }

        [Obsolete( "GetDataSeriesFor is obsolete. Please call RenderableSeries.DataSeries to get the DataSeries", true )]
        internal IDataSeries GetDataSeriesFor( IRenderableSeries renderableSeries )
        {
            throw new NotImplementedException( "GetDataSeriesFor is obsolete. Please call RenderableSeries.DataSeries to get the DataSeries" );
        }

        internal IEnumerable<IDataSeries> GetDataSeriesFor( string axisName )
        {
            if ( RenderableSeries != null )
            {
                foreach ( IRenderableSeries renderableSeries in ( Collection<IRenderableSeries> ) RenderableSeries )
                {
                    if ( !( renderableSeries.YAxisId != axisName ) && renderableSeries.IsVisible )
                    {
                        IDataSeries dataSeries = renderableSeries.DataSeries;
                        if ( dataSeries != null )
                        {
                            yield return dataSeries;
                        }
                    }
                }
            }
        }

        protected override void RegisterServices( IServiceContainer serviceContainer )
        {
            base.RegisterServices( serviceContainer );
            serviceContainer.RegisterService<IUltrachartRenderer>( ( IUltrachartRenderer ) new UltrachartRenderer( this ) );
            serviceContainer.RegisterService<IMouseManager>( ( IMouseManager ) new MouseManager() );
            serviceContainer.RegisterService<ICoordinateCalculatorFactory>( ( ICoordinateCalculatorFactory ) new CoordinateCalculatorFactory() );
            serviceContainer.RegisterService<IPointResamplerFactory>( ( IPointResamplerFactory ) new PointResamplerFactory() );
            serviceContainer.RegisterService<ISciChartSurface>( ( ISciChartSurface ) this );
            serviceContainer.RegisterService<IStrategyManager>( ( IStrategyManager ) new DefaultStrategyManager( this ) );
            _eventAggregator = serviceContainer.GetService<IEventAggregator>();
            _eventAggregator.Subscribe<ZoomExtentsMessage>( ( Action<ZoomExtentsMessage> ) ( m =>
            {
                if ( m.ZoomYOnly )
                {
                    ZoomExtentsY();
                }
                else
                {
                    ZoomExtents();
                }
            } ), true );
            _eventAggregator.Subscribe<InvalidateUltrachartMessage>( ( Action<InvalidateUltrachartMessage> ) ( m => InvalidateElement() ), true );
            _ultraChartRenderer = serviceContainer.GetService<IUltrachartRenderer>();
        }

        private void AttachModifier( IChartModifier chartModifier )
        {
            if ( chartModifier.IsAttached )
            {
                DetachModifier( chartModifier );
            }

            UltrachartDebugLogger.Instance.WriteLine( "Attaching ChartModifier {0}", ( object ) chartModifier.GetType() );
            AttachAsVisualChild( chartModifier );
            chartModifier.ParentSurface = ( ISciChartSurface ) this;
            chartModifier.Services = Services;
            if ( RootGrid != null )
            {
                Services.GetService<IMouseManager>().Subscribe( ( IPublishMouseEvents ) RootGrid, ( IReceiveMouseEvents ) chartModifier );
            }

            chartModifier.DataContext = DataContext;
            chartModifier.IsAttached = true;
            chartModifier.OnAttached();
        }

        private void AttachAsVisualChild( IChartModifier chartModifier )
        {
            FrameworkElement frameworkElement = chartModifier as FrameworkElement;
            if ( frameworkElement == null || frameworkElement.Parent != null )
            {
                return;
            }

            frameworkElement.Visibility = Visibility.Collapsed;
            RootGrid.SafeAddChild( ( object ) frameworkElement, -1 );
        }

        private void DetachAsVisualChild( IChartModifier chartModifier )
        {
            FrameworkElement frameworkElement = chartModifier as FrameworkElement;
            if ( frameworkElement == null || frameworkElement.Parent != RootGrid )
            {
                return;
            }

            RootGrid.SafeRemoveChild( ( object ) frameworkElement );
        }

        private void DetachModifier( IChartModifier chartModifier )
        {
            if ( !chartModifier.IsAttached )
            {
                return;
            }

            UltrachartDebugLogger.Instance.WriteLine( "Dettaching ChartModifier {0}", ( object ) chartModifier.GetType() );
            UnsubscribeFromMouseEvents();
            chartModifier.OnDetached();
            chartModifier.ParentSurface = ( ISciChartSurface ) null;
            chartModifier.Services = ( IServiceContainer ) null;
            chartModifier.IsAttached = false;
            DetachAsVisualChild( chartModifier );
        }

        private void UnsubscribeFromMouseEvents()
        {
            if ( RootGrid == null || Services == null )
            {
                return;
            }

            Services.GetService<IMouseManager>().Unsubscribe( ( IPublishMouseEvents ) RootGrid );
        }

        private void ChartSeriesViewModelPropertyListener( object sender, PropertyChangedEventArgs e )
        {
            IChartSeriesViewModel chartSeriesViewModel = (IChartSeriesViewModel) sender;
            int index = SeriesSource.IndexOf(chartSeriesViewModel);
            if ( index == -1 )
            {
                chartSeriesViewModel.PropertyChanged -= new PropertyChangedEventHandler( ChartSeriesViewModelPropertyListener );
            }
            else
            {
                if ( e.PropertyName == "DataSeries" )
                {
                    Guard.NotNull( ( object ) chartSeriesViewModel.DataSeries, "IChartSeriesViewModel.DataSeries" );
                    if ( chartSeriesViewModel.RenderSeries != null )
                    {
                        chartSeriesViewModel.RenderSeries.DataSeries = chartSeriesViewModel.DataSeries;
                    }
                }
                if ( !( e.PropertyName == "RenderSeries" ) )
                {
                    return;
                }

                if ( chartSeriesViewModel.RenderSeries == null )
                {
                    RenderableSeries.RemoveAt( index );
                }
                else
                {
                    RenderableSeries[ index ] = chartSeriesViewModel.RenderSeries;
                    chartSeriesViewModel.RenderSeries.DataSeries = chartSeriesViewModel.DataSeries;
                }
            }
        }

        private void OnChartSeriesCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            using ( SuspendUpdates() )
            {
                if ( e.Action == NotifyCollectionChangedAction.Reset )
                {
                    RenderableSeries.Clear();
                }

                if ( e.OldItems != null )
                {
                    e.OldItems.Cast<IChartSeriesViewModel>().ForEachDo<IChartSeriesViewModel>( new Action<IChartSeriesViewModel>( DetachChartSeries ) );
                }

                if ( e.NewItems == null )
                {
                    return;
                }

                e.NewItems.Cast<IChartSeriesViewModel>().ForEachDo<IChartSeriesViewModel>( new Action<IChartSeriesViewModel>( AttachChartSeries ) );
            }
        }

        public AxisArea AxisAreaBottom
        {
            get
            {
                return _bottomAxisArea;
            }
        }

        public AxisArea AxisAreaRight
        {
            get
            {
                return _rightAxisArea;
            }
        }

        public AxisArea AxisAreaTop
        {
            get
            {
                return _topAxisArea;
            }
        }

        public AxisArea AxisAreaLeft
        {
            get
            {
                return _leftAxisArea;
            }
        }

        public AxisArea CenterXAxisArea
        {
            get
            {
                return _centerXAxisArea;
            }
        }

        public AxisArea CenterYAxisArea
        {
            get
            {
                return _centerYAxisArea;
            }
        }

        internal IStackedColumnsWrapper StackedColumnsWrapper
        {
            get; set;
        }

        internal IStackedMountainsWrapper StackedMountainsWrapper
        {
            get; set;
        }

        internal UltrachartSurface.RenderableSeriesCollection RenderableSeriesInternal
        {
            get
            {
                return _renderableSeries;
            }
        }

        internal UltrachartSurface( IServiceContainer mockServices )
          : this()
        {
            Services = mockServices;
            _ultraChartRenderer = mockServices.GetService<IUltrachartRenderer>();
            _eventAggregator = mockServices.GetService<IEventAggregator>();
        }

        internal HashSet<IDataSeries> DataSeriesToNotify
        {
            get
            {
                return _dsToNotify;
            }
        }

        [SpecialName]
        bool IUltrachartSurfaceBase.IsVisible
        {
            get
            {
                return IsVisible;
            }
        }

        internal class VersionFinder : Credentials
        {
        }

        internal class RenderableSeriesCollection : ObservableCollection<IRenderableSeries>
        {
            private readonly UltrachartSurface _parentSurface;

            public RenderableSeriesCollection( UltrachartSurface parentSurface )
            {
                _parentSurface = parentSurface;
            }

            protected override void OnCollectionChanged( NotifyCollectionChangedEventArgs e )
            {
                using ( _parentSurface.SuspendUpdates() )
                {
                    if ( e.OldItems != null )
                    {
                        e.OldItems.Cast<IRenderableSeries>().ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( _parentSurface.DetachRenderableSeries ) );
                    }

                    if ( e.NewItems != null )
                    {
                        e.NewItems.Cast<IRenderableSeries>().ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( _parentSurface.AttachRenderableSeries ) );
                    }
                }
                base.OnCollectionChanged( e );
            }

            protected override void ClearItems()
            {
                using ( _parentSurface.SuspendUpdates() )
                {
                    this.ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( _parentSurface.DetachRenderableSeries ) );
                }

                base.ClearItems();
            }
        }
    }
}
