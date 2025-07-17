// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ChartModifiers.TooltipModifierBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Common.Helpers;
using Ecng.Xaml.Charting.StrategyManager;
using Ecng.Xaml.Charting.Themes;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Annotations;
using Ecng.Xaml.Charting.Visuals.Axes;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting.ChartModifiers
{
    public abstract class TooltipModifierBase : InspectSeriesModifierBase
    {
        public static readonly DependencyProperty ShowTooltipOnProperty = DependencyProperty.Register(nameof (ShowTooltipOn), typeof (ShowTooltipOptions), typeof (TooltipModifierBase), new PropertyMetadata((object) ShowTooltipOptions.MouseOver));
        public static readonly DependencyProperty LineOverlayStyleProperty = DependencyProperty.Register(nameof (LineOverlayStyle), typeof (Style), typeof (InspectSeriesModifierBase), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty AxisLabelTemplateProperty = DependencyProperty.Register(nameof (AxisLabelTemplate), typeof (ControlTemplate), typeof (TooltipModifierBase), new PropertyMetadata(new PropertyChangedCallback(TooltipModifierBase.OnAxisLabelTemplatePropertyChanged)));
        public static readonly DependencyProperty ShowAxisLabelsProperty = DependencyProperty.Register(nameof (ShowAxisLabels), typeof (bool), typeof (TooltipModifierBase), new PropertyMetadata((object) true, (PropertyChangedCallback) null));
        public static readonly DependencyProperty TooltipLabelTemplateProperty = DependencyProperty.Register(nameof (TooltipLabelTemplate), typeof (ControlTemplate), typeof (TooltipModifierBase), new PropertyMetadata(new PropertyChangedCallback(TooltipModifierBase.OnTooltipLabelTemplatePropertyChanged)));
        public static readonly DependencyProperty AxisLabelTemplateSelectorProperty = DependencyProperty.Register(nameof (AxisLabelTemplateSelector), typeof (IDataTemplateSelector), typeof (TooltipModifierBase), new PropertyMetadata(new PropertyChangedCallback(TooltipModifierBase.OnAxisLabelTemplatePropertyChanged)));
        public static readonly DependencyProperty TooltipLabelTemplateSelectorProperty = DependencyProperty.Register(nameof (TooltipLabelTemplateSelector), typeof (IDataTemplateSelector), typeof (TooltipModifierBase), new PropertyMetadata(new PropertyChangedCallback(TooltipModifierBase.OnTooltipLabelTemplatePropertyChanged)));
        public static readonly DependencyProperty DefaultAxisLabelTemplateSelectorProperty = DependencyProperty.Register(nameof (DefaultAxisLabelTemplateSelector), typeof (IDataTemplateSelector), typeof (TooltipModifierBase), new PropertyMetadata((object) null));
        public static readonly DependencyProperty DefaultAxisLabelTemplateSelectorStyleProperty = DependencyProperty.Register(nameof (DefaultAxisLabelTemplateSelectorStyle), typeof (Style), typeof (TooltipModifierBase), new PropertyMetadata((object) null, new PropertyChangedCallback(TooltipModifierBase.OnDefaultAxisLabelTemplateSelectorStyleChanged)));
        public static readonly DependencyProperty DefaultTooltipLabelTemplateSelectorProperty = DependencyProperty.Register(nameof (DefaultTooltipLabelTemplateSelector), typeof (IDataTemplateSelector), typeof (TooltipModifierBase), new PropertyMetadata((object) null));
        public static readonly DependencyProperty DefaultTooltipLabelTemplateSelectorStyleProperty = DependencyProperty.Register(nameof (DefaultTooltipLabelTemplateSelectorStyle), typeof (Style), typeof (TooltipModifierBase), new PropertyMetadata((object) null, new PropertyChangedCallback(TooltipModifierBase.OnDefaultLabelTooltipTemplateSelectorStyleChanged)));
        public static readonly DependencyProperty HoverDelayProperty = DependencyProperty.Register("HoverDelay", typeof (double), typeof (TooltipModifierBase), new PropertyMetadata((object) 500.0, new PropertyChangedCallback(TooltipModifierBase.OnHoverDelayDependencyPropertyChanged)));
        private IEnumerable<Tuple<IAxis, FrameworkElement>> _xAxisLabelsCache;
        private IEnumerable<Tuple<IAxis, FrameworkElement>> _yAxisLabelsCache;
        internal DelayActionHelper _delayActionHelper;

        public ShowTooltipOptions ShowTooltipOn
        {
            get
            {
                return ( ShowTooltipOptions ) GetValue( TooltipModifierBase.ShowTooltipOnProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.ShowTooltipOnProperty, ( object ) value );
            }
        }

        public Style LineOverlayStyle
        {
            get
            {
                return ( Style ) GetValue( TooltipModifierBase.LineOverlayStyleProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.LineOverlayStyleProperty, ( object ) value );
            }
        }

        public ControlTemplate AxisLabelTemplate
        {
            get
            {
                return ( ControlTemplate ) GetValue( TooltipModifierBase.AxisLabelTemplateProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.AxisLabelTemplateProperty, ( object ) value );
            }
        }

        public ControlTemplate TooltipLabelTemplate
        {
            get
            {
                return ( ControlTemplate ) GetValue( TooltipModifierBase.TooltipLabelTemplateProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.TooltipLabelTemplateProperty, ( object ) value );
            }
        }

        public bool ShowAxisLabels
        {
            get
            {
                return ( bool ) GetValue( TooltipModifierBase.ShowAxisLabelsProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.ShowAxisLabelsProperty, ( object ) value );
            }
        }

        public IDataTemplateSelector AxisLabelTemplateSelector
        {
            get
            {
                return ( IDataTemplateSelector ) GetValue( TooltipModifierBase.AxisLabelTemplateSelectorProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.AxisLabelTemplateSelectorProperty, ( object ) value );
            }
        }

        public IDataTemplateSelector TooltipLabelTemplateSelector
        {
            get
            {
                return ( IDataTemplateSelector ) GetValue( TooltipModifierBase.TooltipLabelTemplateSelectorProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.TooltipLabelTemplateSelectorProperty, ( object ) value );
            }
        }

        public IDataTemplateSelector DefaultAxisLabelTemplateSelector
        {
            get
            {
                return ( IDataTemplateSelector ) GetValue( TooltipModifierBase.DefaultAxisLabelTemplateSelectorProperty );
            }
            protected set
            {
                SetValue( TooltipModifierBase.DefaultAxisLabelTemplateSelectorProperty, ( object ) value );
            }
        }

        public Style DefaultAxisLabelTemplateSelectorStyle
        {
            get
            {
                return ( Style ) GetValue( TooltipModifierBase.DefaultAxisLabelTemplateSelectorStyleProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.DefaultAxisLabelTemplateSelectorStyleProperty, ( object ) value );
            }
        }

        public IDataTemplateSelector DefaultTooltipLabelTemplateSelector
        {
            get
            {
                return ( IDataTemplateSelector ) GetValue( TooltipModifierBase.DefaultTooltipLabelTemplateSelectorProperty );
            }
            protected set
            {
                SetValue( TooltipModifierBase.DefaultTooltipLabelTemplateSelectorProperty, ( object ) value );
            }
        }

        public Style DefaultTooltipLabelTemplateSelectorStyle
        {
            get
            {
                return ( Style ) GetValue( TooltipModifierBase.DefaultTooltipLabelTemplateSelectorStyleProperty );
            }
            set
            {
                SetValue( TooltipModifierBase.DefaultTooltipLabelTemplateSelectorStyleProperty, ( object ) value );
            }
        }

        protected TooltipModifierBase()
        {
            AxisInfoTemplateSelector templateSelector1 = new AxisInfoTemplateSelector();
            templateSelector1.Style = DefaultAxisLabelTemplateSelectorStyle;
            DefaultAxisLabelTemplateSelector = ( IDataTemplateSelector ) templateSelector1;
            SeriesInfoTemplateSelector templateSelector2 = new SeriesInfoTemplateSelector();
            templateSelector2.Style = DefaultTooltipLabelTemplateSelectorStyle;
            DefaultTooltipLabelTemplateSelector = ( IDataTemplateSelector ) templateSelector2;
        }

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            base.OnModifierMouseDown( e );
            HandleMouseButtonEvent( e );
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            base.OnModifierMouseUp( e );
            HandleMouseButtonEvent( e );
        }

        protected override void OnIsEnabledChanged()
        {
            ClearAll();
        }

        private void HandleMouseButtonEvent( ModifierMouseArgs e )
        {
            if ( ShowTooltipOn != ShowTooltipOptions.MouseLeftButtonDown && ShowTooltipOn != ShowTooltipOptions.MouseMiddleButtonDown && ShowTooltipOn != ShowTooltipOptions.MouseRightButtonDown )
            {
                return;
            }

            MouseButtons mouseButtons = e.MouseButtons;
            e.MouseButtons = MouseButtons.None;
            HandleMouseEvent( e );
            e.MouseButtons = mouseButtons;
            e.Handled = false;
        }

        protected bool HasToShowTooltip()
        {
            if ( ShowTooltipOn == ShowTooltipOptions.Always || ShowTooltipOn == ShowTooltipOptions.MouseHover || ShowTooltipOn == ShowTooltipOptions.MouseLeftButtonDown && IsMouseLeftButtonDown || ShowTooltipOn == ShowTooltipOptions.MouseMiddleButtonDown && IsMouseMiddleButtonDown )
            {
                return true;
            }

            if ( ShowTooltipOn == ShowTooltipOptions.MouseRightButtonDown )
            {
                return IsMouseRightButtonDown;
            }

            return false;
        }

        protected override bool IsHitPointValid( HitTestInfo hitTestInfo )
        {
            if ( hitTestInfo.IsEmpty() )
            {
                return false;
            }

            if ( !hitTestInfo.IsHit )
            {
                return hitTestInfo.IsWithinDataBounds;
            }

            return true;
        }

        protected override void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            RecreateLabelsOnXAxes();
        }

        protected override void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            RecreateLabelsOnYAxes();
        }

        protected bool IsLabelsCacheActual()
        {
            if ( _yAxisLabelsCache != null && _yAxisLabelsCache.Count<Tuple<IAxis, FrameworkElement>>() == YAxes.Count<IAxis>() && _xAxisLabelsCache != null )
            {
                return _xAxisLabelsCache.Count<Tuple<IAxis, FrameworkElement>>() == XAxes.Count<IAxis>();
            }

            return false;
        }

        protected void UpdateAxesOverlay( Point mousePoint )
        {
            foreach ( IAxis xax in XAxes )
            {
                IAxis xAxis = xax;
                FrameworkElement axisLabel = _xAxisLabelsCache.Where<Tuple<IAxis, FrameworkElement>>((Func<Tuple<IAxis, FrameworkElement>, bool>) (x => x.Item1 == xAxis)).Select<Tuple<IAxis, FrameworkElement>, FrameworkElement>((Func<Tuple<IAxis, FrameworkElement>, FrameworkElement>) (pair => pair.Item2)).FirstOrDefault<FrameworkElement>();
                UpdateAxisOverlay( mousePoint, xAxis, axisLabel );
            }
            foreach ( IAxis yax in YAxes )
            {
                IAxis yAxis = yax;
                FrameworkElement axisLabel = _yAxisLabelsCache.Where<Tuple<IAxis, FrameworkElement>>((Func<Tuple<IAxis, FrameworkElement>, bool>) (x => x.Item1 == yAxis)).Select<Tuple<IAxis, FrameworkElement>, FrameworkElement>((Func<Tuple<IAxis, FrameworkElement>, FrameworkElement>) (pair => pair.Item2)).FirstOrDefault<FrameworkElement>();
                UpdateAxisOverlay( mousePoint, yAxis, axisLabel );
            }
        }

        private void UpdateAxisOverlay( Point mousePoint, IAxis axis, FrameworkElement axisLabel )
        {
            if ( axisLabel == null || axis == null )
            {
                return;
            }

            IAnnotationCanvas modifierAxisCanvas = axis.ModifierAxisCanvas;
            bool isPolarAxis = axis.IsPolarAxis;
            ITransformationStrategy transformationStrategy = Services.GetService<IStrategyManager>().GetTransformationStrategy();
            Point point = isPolarAxis ? transformationStrategy.Transform(mousePoint) : ParentSurface.ModifierSurface.TranslatePoint(mousePoint, (IHitTestable) axis);
            if ( TooltipModifierBase.IsInBounds( point, axis ) )
            {
                axisLabel.DataContext = ( object ) HitTestAxis( axis, mousePoint );
                if ( isPolarAxis )
                {
                    SetPolarOffset( axisLabel, point );
                }
                else
                {
                    axis.SetHorizontalOffset( axisLabel, point );
                    axis.SetVerticalOffset( axisLabel, point );
                }
                modifierAxisCanvas.SafeAddChild( ( object ) axisLabel, -1 );
            }
            else
            {
                modifierAxisCanvas.SafeRemoveChild( ( object ) axisLabel );
            }
        }

        private static bool IsInBounds( Point point, IAxis axis )
        {
            double num1;
            double num2;
            if ( axis.IsHorizontalAxis )
            {
                num1 = point.X;
                num2 = axis.ActualWidth;
            }
            else
            {
                num1 = point.Y;
                num2 = axis.ActualHeight;
            }
            if ( num1 <= num2 )
            {
                return num1 >= 0.0;
            }

            return false;
        }

        private void SetPolarOffset( FrameworkElement axisLabel, Point mousePoint )
        {
            Point point = mousePoint;
            axisLabel.SetValue( AxisCanvas.BottomProperty, ( object ) 0.0 );
            axisLabel.SetValue( AxisCanvas.CenterLeftProperty, ( object ) point.X );
        }

        protected void ClearAxesOverlay()
        {
            ClearAxesOverlays( _xAxisLabelsCache );
            ClearAxesOverlays( _yAxisLabelsCache );
        }

        private void ClearAxesOverlays( IEnumerable<Tuple<IAxis, FrameworkElement>> labelsCache )
        {
            if ( labelsCache == null || ParentSurface == null )
            {
                return;
            }

            foreach ( Tuple<IAxis, FrameworkElement> tuple in labelsCache )
            {
                tuple.Item1.ModifierAxisCanvas.SafeRemoveChild( ( object ) tuple.Item2 );
            }
        }

        protected void RecreateLabels()
        {
            RecreateLabelsOnXAxes();
            RecreateLabelsOnYAxes();
        }

        private void RecreateLabelsOnXAxes()
        {
            ClearAxesOverlays( _xAxisLabelsCache );
            _xAxisLabelsCache = CreateLabelsFor( XAxes, AxisLabelTemplate );
        }

        private void RecreateLabelsOnYAxes()
        {
            ClearAxesOverlays( _yAxisLabelsCache );
            _yAxisLabelsCache = CreateLabelsFor( YAxes, AxisLabelTemplate );
        }

        protected IEnumerable<Tuple<IAxis, FrameworkElement>> CreateLabelsFor( IEnumerable<IAxis> axes, ControlTemplate labelTemplate )
        {
            IEnumerable<Tuple<IAxis, FrameworkElement>> tuples = (IEnumerable<Tuple<IAxis, FrameworkElement>>) null;
            if ( axes != null )
            {
                tuples = ( IEnumerable<Tuple<IAxis, FrameworkElement>> ) axes.Select<IAxis, Tuple<IAxis, FrameworkElement>>( ( Func<IAxis, Tuple<IAxis, FrameworkElement>> ) ( axis => new Tuple<IAxis, FrameworkElement>( axis, ( FrameworkElement ) CreateFromTemplate( labelTemplate, AxisLabelTemplateSelector, ( object ) null ) ) ) ).ToArray<Tuple<IAxis, FrameworkElement>>();
            }

            return tuples;
        }

        protected TemplatableControl CreateFromTemplate( ControlTemplate template, IDataTemplateSelector dataTemplateSelector, object dataContext )
        {
            TemplatableControl templatableControl = (TemplatableControl) null;
            if ( template != null )
            {
                TooltipControl tooltipControl = new TooltipControl();
                tooltipControl.Template = template;
                tooltipControl.DataContext = dataContext;
                tooltipControl.Selector = dataTemplateSelector;
                templatableControl = ( TemplatableControl ) tooltipControl;
            }
            return templatableControl;
        }

        protected abstract void OnTooltipLabelTemplateChanged();

        protected abstract void OnAxisLabelTemplateChanged();

        private static void OnAxisLabelTemplatePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( TooltipModifierBase ) d )?.OnAxisLabelTemplateChanged();
        }

        private static void OnTooltipLabelTemplatePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( TooltipModifierBase ) d )?.OnTooltipLabelTemplateChanged();
        }

        private static void OnDefaultAxisLabelTemplateSelectorStyleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            TooltipModifierBase tooltipModifierBase = d as TooltipModifierBase;
            if ( tooltipModifierBase == null )
            {
                return;
            } ( ( FrameworkElement ) tooltipModifierBase.DefaultAxisLabelTemplateSelector ).Style = ( Style ) e.NewValue;
        }

        private static void OnDefaultLabelTooltipTemplateSelectorStyleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            TooltipModifierBase tooltipModifierBase = d as TooltipModifierBase;
            if ( tooltipModifierBase == null )
            {
                return;
            } ( ( FrameworkElement ) tooltipModifierBase.DefaultTooltipLabelTemplateSelector ).Style = ( Style ) e.NewValue;
        }

        private static void OnHoverDelayDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            TooltipModifierBase tooltipModifierBase = d as TooltipModifierBase;
            if ( tooltipModifierBase == null )
            {
                return;
            }

            DelayActionHelper delayActionHelper = tooltipModifierBase._delayActionHelper;
            if ( delayActionHelper == null )
            {
                return;
            }

            delayActionHelper.Interval = ( double ) e.NewValue;
        }
    }
}
