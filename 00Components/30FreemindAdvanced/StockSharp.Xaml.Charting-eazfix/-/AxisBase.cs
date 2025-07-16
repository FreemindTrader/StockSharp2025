using StockSharp.Xaml.Charting.Common.Databinding;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.Numerics.CoordinateProviders;
using StockSharp.Xaml.Charting.Numerics.TickCoordinateProviders;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer;
using StockSharp.Xaml.Charting.Themes;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;
using StockSharp.Xaml.Licensing.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace StockSharp.Xaml.Charting.Visuals.Axes;

[TemplatePart( Name = "PART_AxisCanvas", Type = typeof( IAxisPanel ) )]
[TemplatePart( Name = "PART_ModifierAxisCanvas", Type = typeof( AxisCanvas ) )]
[TemplatePart( Name = "PART_AxisContainer", Type = typeof( StackPanel ) )]
[TemplatePart( Name = "PART_AxisRenderSurface", Type = typeof( HighSpeedRenderSurface ) )]
public abstract class AxisBase : ContentControl, IAxis, IAxisParams, IHitTestable, ISuspendable, IInvalidatableElement, IDrawable, INotifyPropertyChanged, IXmlSerializable
{
    public static readonly DependencyProperty TickCoordinatesProviderProperty = DependencyProperty.Register(
        nameof(TickCoordinatesProvider),
        typeof(ITickCoordinatesProvider),
        typeof(AxisBase),
        new PropertyMetadata(new PropertyChangedCallback(AxisBase.OnTickCoordinatesProviderChanged)));
    public static readonly DependencyProperty IsStaticAxisProperty = DependencyProperty.Register(
        nameof(IsStaticAxis),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) false, new PropertyChangedCallback(AxisBase.OnIsStaticAxisChanged)));
    public static readonly DependencyProperty IsPrimaryAxisProperty = DependencyProperty.Register(
        nameof(IsPrimaryAxis),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) false, new PropertyChangedCallback(AxisBase.OnIsPrimaryAxisChanged)));
    public static readonly DependencyProperty IsCenterAxisProperty = DependencyProperty.Register(
        nameof(IsCenterAxis),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata(
            (object) false,
            new PropertyChangedCallback(AxisBase.OnIsCenterAxisDependencyPropertyChanged)));
    [Obsolete(
        "We're sorry! AxisBase.AxisMode is obsolete. To create a chart with Logarithmic axis, please the LogarithmicNumericAxis type instead")]
    public static readonly DependencyProperty AxisModeProperty = DependencyProperty.Register(
        nameof(AxisMode),
        typeof(AxisMode),
        typeof(AxisBase),
        new PropertyMetadata((object) AxisMode.Linear));
    public static readonly DependencyProperty AutoRangeProperty = DependencyProperty.Register(
        nameof(AutoRange),
        typeof(AutoRange),
        typeof(AxisBase),
        new PropertyMetadata((object) AutoRange.Once, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty MajorDeltaProperty = DependencyProperty.Register(
        nameof(MajorDelta),
        typeof(IComparable),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty MinorDeltaProperty = DependencyProperty.Register(
        nameof(MinorDelta),
        typeof(IComparable),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty MinorsPerMajorProperty = DependencyProperty.Register(
        nameof(MinorsPerMajor),
        typeof(int),
        typeof(AxisBase),
        new PropertyMetadata((object) 5, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty GrowByProperty = DependencyProperty.Register(
        nameof(GrowBy),
        typeof(IRange<double>),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty VisibleRangeProperty = DependencyProperty.Register(
        nameof(VisibleRange),
        typeof(IRange),
        typeof(AxisBase),
        new PropertyMetadata(
            (object) null,
            new PropertyChangedCallback(AxisBase.OnVisibleRangeDependencyPropertyChanged)));
    public static readonly DependencyProperty VisibleRangeLimitProperty = DependencyProperty.Register(
        nameof(VisibleRangeLimit),
        typeof(IRange),
        typeof(AxisBase),
        new PropertyMetadata(
            (object) null,
            new PropertyChangedCallback(AxisBase.OnVisibleRangeLimitDependencyPropertyChanged)));
    public static readonly DependencyProperty VisibleRangeLimitModeProperty = DependencyProperty.Register(
        nameof(VisibleRangeLimitMode),
        typeof(RangeClipMode),
        typeof(AxisBase),
        new PropertyMetadata((object) RangeClipMode.MinMax));
    public static readonly DependencyProperty AnimatedVisibleRangeProperty = DependencyProperty.Register(
        nameof(AnimatedVisibleRange),
        typeof(IRange),
        typeof(AxisBase),
        new PropertyMetadata(
            (object) null,
            new PropertyChangedCallback(AxisBase.OnAnimatedVisibleRangeDependencyPropertyChanged)));
    public static readonly DependencyProperty VisibleRangePointProperty = DependencyProperty.Register(
        "VisibleRangePoint",
        typeof(Point),
        typeof(AxisBase),
        new PropertyMetadata(
            (object) new Point(),
            new PropertyChangedCallback(AxisBase.OnVisibleRangePointDependencyPropertyChanged)));
    public static readonly DependencyProperty AutoAlignVisibleRangeProperty = DependencyProperty.Register(
        nameof(AutoAlignVisibleRange),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) false, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty MaxAutoTicksProperty = DependencyProperty.Register(
        nameof(MaxAutoTicks),
        typeof(int),
        typeof(AxisBase),
        new PropertyMetadata((object) 10, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty AutoTicksProperty = DependencyProperty.Register(
        nameof(AutoTicks),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) true, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty TickProviderProperty = DependencyProperty.Register(
        nameof(TickProvider),
        typeof(ITickProvider),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.OnTickProviderChanged)));
    public static readonly DependencyProperty MinimalZoomConstrainProperty = DependencyProperty.Register(
        nameof(MinimalZoomConstrain),
        typeof(IComparable),
        typeof(AxisBase),
        new PropertyMetadata((object) null));
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation),
        typeof(Orientation),
        typeof(AxisBase),
        new PropertyMetadata(
            (object) Orientation.Horizontal,
            new PropertyChangedCallback(AxisBase.OnOrientationChanged)));
    public static readonly DependencyProperty AxisAlignmentProperty = DependencyProperty.Register(
        nameof(AxisAlignment),
        typeof(AxisAlignment),
        typeof(AxisBase),
        new PropertyMetadata((object) AxisAlignment.Default, new PropertyChangedCallback(AxisBase.OnAlignmentChanged)));
    public static readonly DependencyProperty IdProperty = DependencyProperty.Register(
        nameof(Id),
        typeof(string),
        typeof(AxisBase),
        new PropertyMetadata((object) nameof(DefaultAxisId), new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty FlipCoordinatesProperty = DependencyProperty.Register(
        nameof(FlipCoordinates),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) false, new PropertyChangedCallback(AxisBase.OnFlipCoordinatesChanged)));
    public static readonly DependencyProperty LabelProviderProperty = DependencyProperty.Register(
        nameof(LabelProvider),
        typeof(ILabelProvider),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.OnLabelProviderChanged)));
    public static readonly DependencyProperty DefaultLabelProviderProperty = DependencyProperty.Register(
        nameof(DefaultLabelProvider),
        typeof(ILabelProvider),
        typeof(AxisBase),
        new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty TextFormattingProperty = DependencyProperty.Register(
        nameof(TextFormatting),
        typeof(string),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty CursorTextFormattingProperty = DependencyProperty.Register(
        nameof(CursorTextFormatting),
        typeof(string),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty AxisTitleProperty = DependencyProperty.Register(
        nameof(AxisTitle),
        typeof(string),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty TitleStyleProperty = DependencyProperty.Register(
        nameof(TitleStyle),
        typeof(Style),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty TitleFontWeightProperty = DependencyProperty.Register(
        nameof(TitleFontWeight),
        typeof(FontWeight),
        typeof(AxisBase),
        new PropertyMetadata((object) FontWeights.Normal, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
        nameof(TitleFontSize),
        typeof(double),
        typeof(AxisBase),
        new PropertyMetadata((object) 12.0, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty TickTextBrushProperty = DependencyProperty.Register(
        nameof(TickTextBrush),
        typeof(Brush),
        typeof(AxisBase),
        new PropertyMetadata(new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
        nameof(StrokeThickness),
        typeof(double),
        typeof(AxisBase),
        new PropertyMetadata((object) 1.0, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty MajorTickLineStyleProperty = DependencyProperty.Register(
        nameof(MajorTickLineStyle),
        typeof(Style),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty MinorTickLineStyleProperty = DependencyProperty.Register(
        nameof(MinorTickLineStyle),
        typeof(Style),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty DrawMajorTicksProperty = DependencyProperty.Register(
        nameof(DrawMajorTicks),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) true, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty DrawMinorTicksProperty = DependencyProperty.Register(
        nameof(DrawMinorTicks),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) true, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty DrawLabelsProperty = DependencyProperty.Register(
        nameof(DrawLabels),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) true, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty MajorGridLineStyleProperty = DependencyProperty.Register(
        nameof(MajorGridLineStyle),
        typeof(Style),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty MinorGridLineStyleProperty = DependencyProperty.Register(
        nameof(MinorGridLineStyle),
        typeof(Style),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty DrawMajorGridLinesProperty = DependencyProperty.Register(
        nameof(DrawMajorGridLines),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) true, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty DrawMinorGridLinesProperty = DependencyProperty.Register(
        nameof(DrawMinorGridLines),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) true, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty DrawMajorBandsProperty = DependencyProperty.Register(
        nameof(DrawMajorBands),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) false, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty AxisBandsFillProperty = DependencyProperty.Register(
        nameof(AxisBandsFill),
        typeof(Color),
        typeof(AxisBase),
        new PropertyMetadata((object) new Color(), new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty TickLabelStyleProperty = DependencyProperty.Register(
        nameof(TickLabelStyle),
        typeof(Style),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    public static readonly DependencyProperty ScrollbarProperty = DependencyProperty.Register(
        nameof(Scrollbar),
        typeof(UltrachartScrollbar),
        typeof(AxisBase),
        new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.OnScrollBarChanged)));
    public static readonly DependencyProperty IsLabelCullingEnabledProperty = DependencyProperty.Register(
        nameof(IsLabelCullingEnabled),
        typeof(bool),
        typeof(AxisBase),
        new PropertyMetadata((object) true, new PropertyChangedCallback(AxisBase.InvalidateParent)));
    private static readonly int[] LabelCullingDistances = new int[5] { 2, 4, 8, 16, 32 };
    private bool _isXAxis = true;
    private AxisAlignmentToVeticalAnchorPointConverter _axisAlignmentToVerticalAnchorPointConverter = new AxisAlignmentToVeticalAnchorPointConverter(
        );
    private AxisAlignmentToHorizontalAnchorPointConverter _axisAlignmentToHorizontalAnchorPointConverter = new AxisAlignmentToHorizontalAnchorPointConverter(
        );
    protected System.Windows.Shapes.Line LineToStyle = new System.Windows.Shapes.Line();
    private IServiceContainer _serviceContainer;
    protected ICoordinateCalculator<double> _currentCoordinateCalculator;
    protected IAxisInteractivityHelper _currentInteractivityHelper;
    private ILabelProvider _defaultLabelProvider;
    private ISciChartSurface _parentSurface;
    private IAxisPanel _axisPanel;
    private StockSharp.Xaml.Charting.Themes.ModifierAxisCanvas _modifierAxisCanvas;
    private ITickLabelsPool _labelsPool;
    private TickCoordinates _tickCoords;
    private float _offset;
    private bool _isAnimationChange;
    private IRange _lastValidRange;
    private IRange _secondLastValidRange;
    private Point _fromPoint;
    public const string DefaultAxisId = "DefaultAxisId";
    protected const int MinDistanceToBounds = 1;
    protected const double ZeroRangeGrowBy = 0.01;
    private StackPanel _axisContainer;

    public event PropertyChangedEventHandler PropertyChanged;

    public event EventHandler<EventArgs> Arranged;

    public event EventHandler<VisibleRangeChangedEventArgs> VisibleRangeChanged;

    public event EventHandler<EventArgs> DataRangeChanged;

    protected AxisBase()
    {
        DefaultStyleKey = typeof( AxisBase );
        _secondLastValidRange = _lastValidRange = GetDefaultNonZeroRange();
        SetCurrentValue( AxisBase.TickCoordinatesProviderProperty, new DefaultTickCoordinatesProvider() );
        InitializeLabelsPool();
        SizeChanged += ( SizeChangedEventHandler ) ( ( s, e ) => InvalidateElement() );
    }

    internal AxisBase( IAxisPanel axisPanel ) : this()
    {
        _axisPanel = axisPanel;
    }

    private void InitializeLabelsPool()
    {
        _labelsPool = _labelsPool ??
            ( this is NumericAxis
                ? ( ITickLabelsPool ) new StockSharp.Xaml.Charting.Visuals.Axes.TickLabelsPool<NumericTickLabel>(
                    MaxAutoTicks,
                    new Func<DefaultTickLabel, DefaultTickLabel>( ApplyStyle ) )
                : ( ITickLabelsPool ) new StockSharp.Xaml.Charting.Visuals.Axes.TickLabelsPool<DefaultTickLabel>(
                    MaxAutoTicks,
                    new Func<DefaultTickLabel, DefaultTickLabel>( ApplyStyle ) ) );
    }

    private DefaultTickLabel ApplyStyle( DefaultTickLabel defaultTickLabel )
    {
        defaultTickLabel.DataContext = null;
        defaultTickLabel.SetBinding(
            DefaultTickLabel.DefaultForegroundProperty,
            ( BindingBase ) new Binding( "TickTextBrush" ) { Source = this } );
        defaultTickLabel.SetBinding(
            DefaultTickLabel.DefaultHorizontalAnchorPointProperty,
            ( BindingBase ) new Binding( "AxisAlignment" )
            {
                Source = this,
                Converter = ( IValueConverter ) _axisAlignmentToHorizontalAnchorPointConverter
            } );
        defaultTickLabel.SetBinding(
            DefaultTickLabel.DefaultVerticalAnchorPointProperty,
            ( BindingBase ) new Binding( "AxisAlignment" )
            {
                Source = this,
                Converter = ( IValueConverter ) _axisAlignmentToVerticalAnchorPointConverter
            } );
        defaultTickLabel.SetBinding(
            FrameworkElement.StyleProperty,
            ( BindingBase ) new Binding( "TickLabelStyle" ) { Source = this } );
        return defaultTickLabel;
    }

    internal bool IsLicenseValid
    {
        get;
        set;
    }

    bool IAxis.IsXAxis
    {
        get
        {
            return IsXAxis;
        }
        set
        {
            IsXAxis = value;
        }
    }

    public bool IsXAxis
    {
        get
        {
            return _isXAxis;
        }
        private set
        {
            _isXAxis = value;
            OnPropertyChanged( nameof( IsXAxis ) );
        }
    }

    public virtual bool IsHorizontalAxis
    {
        get
        {
            return Orientation == Orientation.Horizontal;
        }
    }

    public bool IsAxisFlipped
    {
        get
        {
            return IsHorizontalAxis != IsXAxis;
        }
    }

    public bool IsLabelCullingEnabled
    {
        get
        {
            return ( bool ) GetValue( AxisBase.IsLabelCullingEnabledProperty );
        }
        set
        {
            SetValue( AxisBase.IsLabelCullingEnabledProperty, value );
        }
    }

    public bool IsCenterAxis
    {
        get
        {
            return ( bool ) GetValue( AxisBase.IsCenterAxisProperty );
        }
        set
        {
            SetValue( AxisBase.IsCenterAxisProperty, value );
        }
    }

    public bool IsPrimaryAxis
    {
        get
        {
            return ( bool ) GetValue( AxisBase.IsPrimaryAxisProperty );
        }
        set
        {
            SetValue( AxisBase.IsPrimaryAxisProperty, value );
        }
    }

    public bool IsStaticAxis
    {
        get
        {
            return ( bool ) GetValue( AxisBase.IsStaticAxisProperty );
        }
        set
        {
            SetValue( AxisBase.IsStaticAxisProperty, value );
        }
    }

    public bool AutoAlignVisibleRange
    {
        get
        {
            return ( bool ) GetValue( AxisBase.AutoAlignVisibleRangeProperty );
        }
        set
        {
            SetValue( AxisBase.AutoAlignVisibleRangeProperty, value );
        }
    }

    public bool HasValidVisibleRange
    {
        get
        {
            return IsVisibleRangeValid();
        }
    }

    public bool HasDefaultVisibleRange
    {
        get
        {
            IRange defaultNonZeroRange = GetDefaultNonZeroRange();
            if ( VisibleRange.Equals( defaultNonZeroRange ) )
                return _secondLastValidRange.Equals( defaultNonZeroRange );
            return false;
        }
    }

    double IDrawable.Width
    {
        get
        {
            return ActualWidth;
        }
        set
        {
        }
    }

    double IDrawable.Height
    {
        get
        {
            return ActualHeight;
        }
        set
        {
        }
    }

    public ISciChartSurface ParentSurface
    {
        get
        {
            return _parentSurface;
        }
        set
        {
            _parentSurface = value;
            if ( _parentSurface != null && _parentSurface.Services != null )
                Services = _parentSurface.Services;
            OnPropertyChanged( nameof( ParentSurface ) );
        }
    }

    public IServiceContainer Services
    {
        get
        {
            return _serviceContainer;
        }
        set
        {
            _serviceContainer = value;
        }
    }

    public string AxisTitle
    {
        get
        {
            return ( string ) GetValue( AxisBase.AxisTitleProperty );
        }
        set
        {
            SetValue( AxisBase.AxisTitleProperty, value );
        }
    }

    public Style TitleStyle
    {
        get
        {
            return ( Style ) GetValue( AxisBase.TitleStyleProperty );
        }
        set
        {
            SetValue( AxisBase.TitleStyleProperty, value );
        }
    }

    public FontWeight TitleFontWeight
    {
        get
        {
            return ( FontWeight ) GetValue( AxisBase.TitleFontWeightProperty );
        }
        set
        {
            SetValue( AxisBase.TitleFontWeightProperty, value );
        }
    }

    public double TitleFontSize
    {
        get
        {
            return ( double ) GetValue( AxisBase.TitleFontSizeProperty );
        }
        set
        {
            SetValue( AxisBase.TitleFontSizeProperty, value );
        }
    }

    public string TextFormatting
    {
        get
        {
            return ( string ) GetValue( AxisBase.TextFormattingProperty );
        }
        set
        {
            SetValue( AxisBase.TextFormattingProperty, value );
        }
    }

    public string CursorTextFormatting
    {
        get
        {
            return ( string ) GetValue( AxisBase.CursorTextFormattingProperty );
        }
        set
        {
            SetValue( AxisBase.CursorTextFormattingProperty, value );
        }
    }

    public ILabelProvider LabelProvider
    {
        get
        {
            return ( ILabelProvider ) GetValue( AxisBase.LabelProviderProperty );
        }
        set
        {
            SetValue( AxisBase.LabelProviderProperty, value );
        }
    }

    public ILabelProvider DefaultLabelProvider
    {
        get
        {
            return ( ILabelProvider ) GetValue( AxisBase.DefaultLabelProviderProperty );
        }
        protected set
        {
            SetValue( AxisBase.DefaultLabelProviderProperty, value );
        }
    }

    [Obsolete(
        "We're sorry! AxisBase.AxisMode is obsolete. To create a chart with Logarithmic axis, please the LogarithmicNumericAxis type instead" )]
    public AxisMode AxisMode
    {
        get
        {
            throw new Exception(
                "We're sorry! AxisBase.AxisMode is obsolete. To create a chart with Logarithmic axis, please the LogarithmicNumericAxis type instead" );
        }
        set
        {
            throw new Exception(
                "We're sorry! AxisBase.AxisMode is obsolete. To create a chart with Logarithmic axis, please the LogarithmicNumericAxis type instead" );
        }
    }

    public AutoRange AutoRange
    {
        get
        {
            return ( AutoRange ) GetValue( AxisBase.AutoRangeProperty );
        }
        set
        {
            SetValue( AxisBase.AutoRangeProperty, value );
        }
    }

    [TypeConverter( typeof( StringToDoubleRangeTypeConverter ) )]
    public IRange<double> GrowBy
    {
        get
        {
            return ( IRange<double> ) GetValue( AxisBase.GrowByProperty );
        }
        set
        {
            SetValue( AxisBase.GrowByProperty, value );
        }
    }

    public bool FlipCoordinates
    {
        get
        {
            return ( bool ) GetValue( AxisBase.FlipCoordinatesProperty );
        }
        set
        {
            SetValue( AxisBase.FlipCoordinatesProperty, value );
        }
    }

    public IComparable MajorDelta
    {
        get
        {
            return ( IComparable ) GetValue( AxisBase.MajorDeltaProperty );
        }
        set
        {
            SetValue( AxisBase.MajorDeltaProperty, value );
        }
    }

    public int MinorsPerMajor
    {
        get
        {
            return ( int ) GetValue( AxisBase.MinorsPerMajorProperty );
        }
        set
        {
            SetValue( AxisBase.MinorsPerMajorProperty, value );
        }
    }

    public int MaxAutoTicks
    {
        get
        {
            return ( int ) GetValue( AxisBase.MaxAutoTicksProperty );
        }
        set
        {
            SetValue( AxisBase.MaxAutoTicksProperty, value );
        }
    }

    public bool AutoTicks
    {
        get
        {
            return ( bool ) GetValue( AxisBase.AutoTicksProperty );
        }
        set
        {
            SetValue( AxisBase.AutoTicksProperty, value );
        }
    }

    public ITickProvider TickProvider
    {
        get
        {
            return ( ITickProvider ) GetValue( AxisBase.TickProviderProperty );
        }
        set
        {
            SetValue( AxisBase.TickProviderProperty, value );
        }
    }

    public ITickCoordinatesProvider TickCoordinatesProvider
    {
        get
        {
            return ( ITickCoordinatesProvider ) GetValue( AxisBase.TickCoordinatesProviderProperty );
        }
        set
        {
            SetValue( AxisBase.TickCoordinatesProviderProperty, value );
        }
    }

    public IComparable MinorDelta
    {
        get
        {
            return ( IComparable ) GetValue( AxisBase.MinorDeltaProperty );
        }
        set
        {
            SetValue( AxisBase.MinorDeltaProperty, value );
        }
    }

    public Brush TickTextBrush
    {
        get
        {
            return ( Brush ) GetValue( AxisBase.TickTextBrushProperty );
        }
        set
        {
            SetValue( AxisBase.TickTextBrushProperty, value );
        }
    }

    [Obsolete( "MajorLineStroke is obsolete, please use MajorTickLineStyle instead", true )]
    public Brush MajorLineStroke
    {
        get
        {
            return ( Brush ) null;
        }
        set
        {
            throw new Exception( "MajorLineStroke is obsolete, please use MajorTickLineStyle instead" );
        }
    }

    [Obsolete( "MinorLineStroke is obsolete, please use MajorTickLineStyle instead", true )]
    public Brush MinorLineStroke
    {
        get
        {
            return ( Brush ) null;
        }
        set
        {
            throw new Exception( "MinorLineStroke is obsolete, please use MajorTickLineStyle instead" );
        }
    }

    public Style MajorTickLineStyle
    {
        get
        {
            return ( Style ) GetValue( AxisBase.MajorTickLineStyleProperty );
        }
        set
        {
            SetValue( AxisBase.MajorTickLineStyleProperty, value );
        }
    }

    public Style MinorTickLineStyle
    {
        get
        {
            return ( Style ) GetValue( AxisBase.MinorTickLineStyleProperty );
        }
        set
        {
            SetValue( AxisBase.MinorTickLineStyleProperty, value );
        }
    }

    public Style MajorGridLineStyle
    {
        get
        {
            return ( Style ) GetValue( AxisBase.MajorGridLineStyleProperty );
        }
        set
        {
            SetValue( AxisBase.MajorGridLineStyleProperty, value );
        }
    }

    public Style MinorGridLineStyle
    {
        get
        {
            return ( Style ) GetValue( AxisBase.MinorGridLineStyleProperty );
        }
        set
        {
            SetValue( AxisBase.MinorGridLineStyleProperty, value );
        }
    }

    public bool DrawMinorTicks
    {
        get
        {
            return ( bool ) GetValue( AxisBase.DrawMinorTicksProperty );
        }
        set
        {
            SetValue( AxisBase.DrawMinorTicksProperty, value );
        }
    }

    public bool DrawLabels
    {
        get
        {
            return ( bool ) GetValue( AxisBase.DrawLabelsProperty );
        }
        set
        {
            SetValue( AxisBase.DrawLabelsProperty, value );
        }
    }

    public bool DrawMajorTicks
    {
        get
        {
            return ( bool ) GetValue( AxisBase.DrawMajorTicksProperty );
        }
        set
        {
            SetValue( AxisBase.DrawMajorTicksProperty, value );
        }
    }

    public bool DrawMajorGridLines
    {
        get
        {
            return ( bool ) GetValue( AxisBase.DrawMajorGridLinesProperty );
        }
        set
        {
            SetValue( AxisBase.DrawMajorGridLinesProperty, value );
        }
    }

    public bool DrawMinorGridLines
    {
        get
        {
            return ( bool ) GetValue( AxisBase.DrawMinorGridLinesProperty );
        }
        set
        {
            SetValue( AxisBase.DrawMinorGridLinesProperty, value );
        }
    }

    public bool DrawMajorBands
    {
        get
        {
            return ( bool ) GetValue( AxisBase.DrawMajorBandsProperty );
        }
        set
        {
            SetValue( AxisBase.DrawMajorBandsProperty, value );
        }
    }

    public Color AxisBandsFill
    {
        get
        {
            return ( Color ) GetValue( AxisBase.AxisBandsFillProperty );
        }
        set
        {
            SetValue( AxisBase.AxisBandsFillProperty, value );
        }
    }

    public Orientation Orientation
    {
        get
        {
            return ( Orientation ) GetValue( AxisBase.OrientationProperty );
        }
        set
        {
            SetValue( AxisBase.OrientationProperty, value );
        }
    }

    public AxisAlignment AxisAlignment
    {
        get
        {
            return ( AxisAlignment ) GetValue( AxisBase.AxisAlignmentProperty );
        }
        set
        {
            SetValue( AxisBase.AxisAlignmentProperty, value );
        }
    }

    public string Id
    {
        get
        {
            return ( string ) GetValue( AxisBase.IdProperty );
        }
        set
        {
            SetValue( AxisBase.IdProperty, value );
        }
    }

    public double StrokeThickness
    {
        get
        {
            return ( double ) GetValue( AxisBase.StrokeThicknessProperty );
        }
        set
        {
            SetValue( AxisBase.StrokeThicknessProperty, value );
        }
    }

    public Style TickLabelStyle
    {
        get
        {
            return ( Style ) GetValue( AxisBase.TickLabelStyleProperty );
        }
        set
        {
            SetValue( AxisBase.TickLabelStyleProperty, value );
        }
    }

    public UltrachartScrollbar Scrollbar
    {
        get
        {
            return ( UltrachartScrollbar ) GetValue( AxisBase.ScrollbarProperty );
        }
        set
        {
            SetValue( AxisBase.ScrollbarProperty, value );
        }
    }

    public bool IsSuspended
    {
        get
        {
            return UpdateSuspender.GetIsSuspended( ( ISuspendable ) this );
        }
    }

    public IAnnotationCanvas ModifierAxisCanvas
    {
        get
        {
            return ( IAnnotationCanvas ) _modifierAxisCanvas;
        }
    }

    protected IRenderSurface RenderSurface
    {
        get
        {
            if ( ParentSurface == null )
                return ( IRenderSurface ) null;
            return ParentSurface.RenderSurface;
        }
    }

    public virtual bool IsCategoryAxis
    {
        get
        {
            return false;
        }
    }

    public virtual bool IsLogarithmicAxis
    {
        get
        {
            return false;
        }
    }

    public virtual bool IsPolarAxis
    {
        get
        {
            return false;
        }
    }

    [TypeConverter( typeof( StringToDoubleRangeTypeConverter ) )]
    public IRange AnimatedVisibleRange
    {
        get
        {
            return ( IRange ) GetValue( AxisBase.AnimatedVisibleRangeProperty );
        }
        set
        {
            SetValue( AxisBase.AnimatedVisibleRangeProperty, value );
        }
    }

    [TypeConverter( typeof( StringToDoubleRangeTypeConverter ) )]
    public IRange VisibleRange
    {
        get
        {
            return ( IRange ) GetValue( AxisBase.VisibleRangeProperty );
        }
        set
        {
            SetValue( AxisBase.VisibleRangeProperty, value );
        }
    }

    [TypeConverter( typeof( StringToDoubleRangeTypeConverter ) )]
    public IRange VisibleRangeLimit
    {
        get
        {
            return ( IRange ) GetValue( AxisBase.VisibleRangeLimitProperty );
        }
        set
        {
            SetValue( AxisBase.VisibleRangeLimitProperty, value );
        }
    }

    public RangeClipMode VisibleRangeLimitMode
    {
        get
        {
            return ( RangeClipMode ) GetValue( AxisBase.VisibleRangeLimitModeProperty );
        }
        set
        {
            SetValue( AxisBase.VisibleRangeLimitModeProperty, value );
        }
    }

    public IComparable MinimalZoomConstrain
    {
        get
        {
            return ( IComparable ) GetValue( AxisBase.MinimalZoomConstrainProperty );
        }
        set
        {
            SetValue( AxisBase.MinimalZoomConstrainProperty, value );
        }
    }

    public IRange DataRange
    {
        get
        {
            return CalculateDataRange();
        }
    }

    internal IAxisPanel AxisPanel
    {
        get
        {
            return _axisPanel;
        }
    }

    internal StackPanel AxisContainer
    {
        get
        {
            return _axisContainer;
        }
    }

    internal ITickLabelsPool TickLabelsPool
    {
        get
        {
            return _labelsPool;
        }
    }

    public virtual double CurrentDatapointPixelSize
    {
        get
        {
            return double.NaN;
        }
    }

    [Obsolete( "AxisBase.GetPointRange is obsolete, please call DataSeries.GetIndicesRange(VisibleRange) instead", true )]
    public IntegerRange GetPointRange()
    {
        throw new NotSupportedException(
            "AxisBase.GetPointRange is obsolete, please call DataSeries.GetIndicesRange(VisibleRange) instead" );
    }

    public abstract IRange GetUndefinedRange();

    public abstract IRange GetDefaultNonZeroRange();

    public abstract IRange CalculateYRange( RenderPassInfo renderPassInfo );

    protected virtual IRange CalculateDataRange()
    {
        if ( ParentSurface == null || ParentSurface.RenderableSeries.IsNullOrEmpty<IRenderableSeries>() )
            return ( IRange ) null;
        if ( !IsXAxis )
            return GetYDataRange();
        return GetXDataRange();
    }

    private IRange GetXDataRange()
    {
        IRange range = (IRange) null;
        foreach ( IRenderableSeries renderableSeries in ParentSurface.RenderableSeries
            .Where<IRenderableSeries>(
                ( Func<IRenderableSeries, bool> ) ( x =>
                {
                    if ( !( x.XAxisId == Id ) || !x.IsVisible )
                        return false;
                    IDataSeries dataSeries = x.DataSeries;
                    if ( dataSeries == null )
                        return false;
                    return dataSeries.HasValues;
                } ) ) )
        {
            IRange xrange = renderableSeries.GetXRange();
            if ( xrange != null && xrange.IsDefined )
            {
                DoubleRange doubleRange = xrange.AsDoubleRange();
                range = range == null ? ( IRange ) doubleRange : doubleRange.Union( range );
            }
        }
        return range;
    }

    private IRange GetYDataRange()
    {
        IRange range = (IRange) null;
        foreach ( IRenderableSeries renderableSeries in ParentSurface.RenderableSeries
            .Where<IRenderableSeries>(
                ( Func<IRenderableSeries, bool> ) ( x =>
                {
                    if ( x.YAxisId == Id && x.IsVisible )
                        return x.DataSeries != null;
                    return false;
                } ) ) )
        {
            IRange yrange = renderableSeries.DataSeries.YRange;
            if ( yrange != null && yrange.IsDefined )
            {
                DoubleRange doubleRange = yrange.AsDoubleRange();
                range = range == null ? ( IRange ) doubleRange : doubleRange.Union( range );
            }
        }
        return range;
    }

    public virtual IRange GetMaximumRange()
    {
        IRange range1 = (IRange) new DoubleRange(double.NaN, double.NaN);
        if ( ParentSurface != null && !ParentSurface.RenderableSeries.IsNullOrEmpty<IRenderableSeries>() )
        {
            if ( IsXAxis )
            {
                range1 = GetXDataRange() ?? range1;
                if ( range1.IsZero )
                    range1 = CoerceZeroRange( range1 );
                if ( GrowBy != null )
                {
                    double logBase = IsLogarithmicAxis ? ((ILogarithmicAxis) this).LogarithmicBase : 0.0;
                    range1 = range1.GrowBy( GrowBy.Min, GrowBy.Max, IsLogarithmicAxis, logBase );
                }
                if ( VisibleRangeLimit != null )
                    range1.ClipTo( ( IRange ) VisibleRangeLimit.AsDoubleRange(), VisibleRangeLimitMode );
            }
            else
                range1 = GetWindowedYRange( ( IDictionary<string, IRange> ) null );
        }
        IRange range2 = VisibleRange == null || !VisibleRange.IsDefined ? GetDefaultNonZeroRange() : VisibleRange;
        if ( range1 == null || !range1.IsDefined )
            return ( IRange ) range2.AsDoubleRange();
        return range1;
    }

    protected virtual IRange CoerceZeroRange( IRange maximumRange )
    {
        return maximumRange.GrowBy( 0.01, 0.01 );
    }

    public IRange GetWindowedYRange( IDictionary<string, IRange> xRanges )
    {
        IRange range = (IRange) new DoubleRange(double.NaN, double.NaN);
        if ( ParentSurface != null && !ParentSurface.RenderableSeries.IsNullOrEmpty<IRenderableSeries>() )
        {
            foreach ( IRenderableSeries renderableSeries in ParentSurface.RenderableSeries
                .Where<IRenderableSeries>(
                    ( Func<IRenderableSeries, bool> ) ( x =>
                    {
                        if ( x.YAxisId == Id && x.DataSeries != null && x.IsVisible )
                            return x.DataSeries.HasValues;
                        return false;
                    } ) ) )
            {
                IRange xRange = xRanges == null || !xRanges.ContainsKey(renderableSeries.XAxisId)
                    ? (renderableSeries.XAxis ?? ParentSurface.XAxes.GetAxisById(renderableSeries.XAxisId, false))?.VisibleRange
                    : xRanges[renderableSeries.XAxisId];
                if ( xRange != null && xRange.IsDefined )
                {
                    DoubleRange doubleRange = renderableSeries.GetYRange(xRange, IsLogarithmicAxis).AsDoubleRange();
                    if ( doubleRange.IsDefined )
                        range = doubleRange.Union( range );
                }
            }
            if ( range.IsZero )
                range = CoerceZeroRange( range );
            if ( GrowBy != null )
            {
                double logBase = IsLogarithmicAxis ? ((ILogarithmicAxis) this).LogarithmicBase : 0.0;
                range = range != null ? range.GrowBy( GrowBy.Min, GrowBy.Max, IsLogarithmicAxis, logBase ) : ( IRange ) null;
            }
            if ( VisibleRangeLimit != null )
                range.ClipTo( ( IRange ) VisibleRangeLimit.AsDoubleRange(), VisibleRangeLimitMode );
        }
        if ( range == null || !range.IsDefined )
            return VisibleRange != null ? ( IRange ) VisibleRange.AsDoubleRange() : ( IRange ) null;
        return range;
    }

    public void Scroll( double pixelsToScroll, ClipMode clipMode )
    {
        Scroll( pixelsToScroll, clipMode, TimeSpan.Zero );
    }

    public void Scroll( double pixelsToScroll, ClipMode clipMode, TimeSpan duration )
    {
        IAxisInteractivityHelper interactivityHelper = GetCurrentInteractivityHelper();
        if ( interactivityHelper == null )
            return;
        IRange rangeToClip = interactivityHelper.Scroll(VisibleRange, pixelsToScroll);
        IRange range = rangeToClip;
        if ( clipMode != ClipMode.None )
        {
            IRange maximumRange = GetMaximumRange();
            range = interactivityHelper.ClipRange( rangeToClip, maximumRange, clipMode );
        }
        IRange newRange = RangeFactory.NewWithMinMax(VisibleRange, range.Min, range.Max);
        TryApplyVisibleRangeLimit( newRange );

        this.TrySetOrAnimateVisibleRange( newRange, duration );
    }

    protected void TryApplyVisibleRangeLimit( IRange newRange )
    {
        if ( VisibleRangeLimit == null )
            return;
        newRange.ClipTo( VisibleRangeLimit, VisibleRangeLimitMode );
    }

    public void ScrollByDataPoints( int pointAmount )
    {
        ScrollByDataPoints( pointAmount, TimeSpan.Zero );
    }

    public virtual void ScrollByDataPoints( int pointAmount, TimeSpan duration )
    {
        throw new InvalidOperationException( "ScrollByDataPoints is only valid CategoryDateTimeAxis" );
    }

    public void Zoom( double fromCoord, double toCoord )
    {
        Zoom( fromCoord, toCoord, TimeSpan.Zero );
    }

    public void Zoom( double fromCoord, double toCoord, TimeSpan duration )
    {
        IRange newRange = GetCurrentInteractivityHelper().Zoom(VisibleRange, fromCoord, toCoord);
        TryApplyVisibleRangeLimit( newRange );

        this.TrySetOrAnimateVisibleRange( newRange, duration );
    }

    public void ZoomBy( double minFraction, double maxFraction )
    {
        ZoomBy( minFraction, maxFraction, TimeSpan.Zero );
    }

    public void ZoomBy( double minFraction, double maxFraction, TimeSpan duration )
    {
        IAxisInteractivityHelper interactivityHelper = GetCurrentInteractivityHelper();
        if ( interactivityHelper == null )
            return;
        IRange newRange = interactivityHelper.ZoomBy(VisibleRange, minFraction, maxFraction);
        TryApplyVisibleRangeLimit( newRange );
        this.TrySetOrAnimateVisibleRange( newRange, duration );
    }

    [Obsolete( "AxisBase.ScrollTo is obsolete, please call AxisBase.Scroll(pixelsToScroll) instead" )]
    public virtual void ScrollTo( IRange startVisibleRange, double pixelsToScroll )
    {
        ScrollToWithLimit( startVisibleRange, pixelsToScroll, ( IRange ) null );
    }

    public virtual void ScrollToWithLimit( IRange startVisibleRange, double pixelsToScroll, IRange rangeLimit )
    {
        IRange range1 = GetCurrentInteractivityHelper().Scroll(VisibleRange, pixelsToScroll);
        IRange range2;
        if ( rangeLimit == null )
        {
            range2 = range1;
        }
        else
        {
            DoubleRange doubleRange = range1.AsDoubleRange();
            range2 = RangeFactory.NewWithMinMax( VisibleRange, doubleRange.Min, doubleRange.Max, rangeLimit );
        }
        if ( !IsValidRange( range2 ) )
            return;
        VisibleRange = range2;
    }

    public virtual bool IsValidRange( IRange range )
    {
        if ( IsOfValidType( range ) && range.IsDefined )
            return range.Min.CompareTo( range.Max ) <= 0;
        return false;
    }

    public abstract bool IsOfValidType( IRange range );

    public XmlSchema GetSchema()
    {
        return ( XmlSchema ) null;
    }

    public virtual void ReadXml( XmlReader reader )
    {
        if ( reader.MoveToContent() != XmlNodeType.Element || !( reader.LocalName == GetType().Name ) )
            return;
        AxisSerializationHelper.Instance.DeserializeProperties( this, reader );
    }

    public virtual void WriteXml( XmlWriter writer )
    {
        AxisSerializationHelper.Instance.SerializeProperties( this, writer );
    }

    public abstract IAxis Clone();

    public virtual void AssertDataType( Type dataType )
    {
        List<Type> supportedTypes = GetSupportedTypes();
        if ( !supportedTypes.Contains( dataType ) )
            throw new InvalidOperationException(
                string.Format(
                    "{0} does not support the type {1}. Supported types include {2}",
                    GetType().Name,
                    dataType,
                    string.Join(
                        ", ",
                        supportedTypes.Select<Type, string>( ( Func<Type, string> ) ( x => x.Name ) ).ToArray<string>() ) ) );
    }

    protected abstract List<Type> GetSupportedTypes();

    public void ValidateAxis()
    {
        if ( !AutoTicks &&
            ( MajorDelta == null ||
                MajorDelta.Equals( AxisBase.MajorDeltaProperty.GetMetadata( typeof( AxisBase ) ).DefaultValue ) ||
                ( MinorDelta == null ||
                    MinorDelta.Equals( AxisBase.MinorDeltaProperty.GetMetadata( typeof( AxisBase ) ).DefaultValue ) ) ) )
            throw new InvalidOperationException(
                "The MinDelta, MaxDelta properties have to be set if AutoTicks == False." );
    }

    public void SetMouseCursor( Cursor cursor )
    {
        SetCurrentValue( FrameworkElement.CursorProperty, cursor );
    }

    public void Clear()
    {
        if ( AxisPanel == null )
            return;
        AxisPanel.ClearLabels();
    }

    public IAxisInteractivityHelper GetCurrentInteractivityHelper()
    {
        return _currentInteractivityHelper;
    }

    public virtual ICoordinateCalculator<double> GetCurrentCoordinateCalculator()
    {
        _currentCoordinateCalculator = ( Services == null
            ? ( ICoordinateCalculatorFactory ) new CoordinateCalculatorFactory()
            : Services.GetService<ICoordinateCalculatorFactory>() ).New( GetAxisParams() );
        _currentInteractivityHelper = ( IAxisInteractivityHelper ) new AxisInteractivityHelper(
            _currentCoordinateCalculator );
        return _currentCoordinateCalculator;
    }

    public virtual void OnBeginRenderPass(
        RenderPassInfo renderPassInfo = default( RenderPassInfo ),
        IPointSeries firstPointSeries = null )
    {
        GetCurrentCoordinateCalculator();
    }

    public virtual AxisParams GetAxisParams()
    {
        IRenderSurface renderSurface = RenderSurface;
        DoubleRange doubleRange = (VisibleRange ?? (IRange) new DoubleRange(double.NaN, double.NaN)).AsDoubleRange();
        int num = IsHorizontalAxis ? (int) ActualWidth : (int) ActualHeight;
        if ( ( double ) Math.Abs( num ) < double.Epsilon && renderSurface != null )
            num = IsHorizontalAxis ? ( int ) renderSurface.ActualWidth : ( int ) renderSurface.ActualHeight;
        AxisParams axisParams = new AxisParams()
        {
            FlipCoordinates = FlipCoordinates,
            IsXAxis = IsXAxis,
            IsHorizontal = IsHorizontalAxis,
            VisibleMax = doubleRange.Max.ToDouble(),
            VisibleMin = doubleRange.Min.ToDouble(),
            Offset = GetAxisOffset(),
            Size = (double) num,
            DataPointPixelSize = double.NaN
        };
        IDataSeries baseDataSeries = GetBaseDataSeries();
        if ( baseDataSeries != null )
        {
            axisParams.BaseXValues = baseDataSeries.XValues;
            axisParams.IsBaseXValuesSorted = baseDataSeries.IsSorted;
        }
        return axisParams;
    }

    public virtual double GetAxisOffset()
    {
        double num = 0.0;
        if ( RenderSurface != null )
        {
            Rect boundsRelativeTo = ElementExtensions.GetBoundsRelativeTo(this, (IHitTestable) RenderSurface);
            num = boundsRelativeTo == Rect.Empty ? 0.0 : ( IsHorizontalAxis ? boundsRelativeTo.X : boundsRelativeTo.Y );
        }
        return num;
    }

    private IDataSeries GetBaseDataSeries()
    {
        IDataSeries dataSeries1 = (IDataSeries) null;
        if ( ParentSurface != null && ParentSurface.RenderableSeries != null )
        {
            IRenderableSeries renderableSeries = IsXAxis
                ? ParentSurface.RenderableSeries
                    .FirstOrDefault<IRenderableSeries>(
                        (Func<IRenderableSeries, bool>) (x =>
                        {
                            if(!(x.XAxisId == Id))
                                return false;
                            IDataSeries dataSeries2 = x.DataSeries;
                            if(dataSeries2 == null)
                                return false;
                            return dataSeries2.HasValues;
                        }))
                : ParentSurface.RenderableSeries
                    .FirstOrDefault<IRenderableSeries>(
                        (Func<IRenderableSeries, bool>) (x =>
                        {
                            if(!(x.YAxisId == Id))
                                return false;
                            IDataSeries dataSeries2 = x.DataSeries;
                            if(dataSeries2 == null)
                                return false;
                            return dataSeries2.HasValues;
                        }));
            if ( renderableSeries != null && renderableSeries.DataSeries != null )
                dataSeries1 = renderableSeries.DataSeries;
        }
        return dataSeries1;
    }

    protected virtual void OnPropertyChanged( string propertyName )
    {
        // ISSUE: reference to a compiler-generated field
        PropertyChangedEventHandler propertyChanged = PropertyChanged;
        if ( propertyChanged == null )
            return;
        propertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
    }

    public void DecrementSuspend()
    {
    }

    public IUpdateSuspender SuspendUpdates()
    {
        return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this );
    }

    public void ResumeUpdates( IUpdateSuspender updateSuspender )
    {
        if ( !updateSuspender.ResumeTargetOnDispose )
            return;
        InvalidateElement();
    }

    public void InvalidateElement()
    {
        InvalidateMeasure();
        InvalidateArrange();
        if ( ParentSurface == null )
            return;
        ParentSurface.InvalidateElement();
    }

    public virtual AxisInfo HitTest( Point atPoint )
    {
        if ( GetCurrentCoordinateCalculator() == null )
            return ( AxisInfo ) null;
        return HitTest( GetDataValue( IsHorizontalAxis ? atPoint.X : atPoint.Y ) );
    }

    public virtual AxisInfo HitTest( IComparable dataValue )
    {
        string str1 = FormatText(dataValue);
        string str2 = FormatCursorText(dataValue);
        return new AxisInfo()
        {
            AxisId = Id,
            DataValue = dataValue,
            AxisAlignment = AxisAlignment,
            AxisFormattedDataValue = str1,
            CursorFormattedDataValue = str2,
            AxisTitle = AxisTitle,
            IsHorizontal = IsHorizontalAxis,
            IsXAxis = IsXAxis
        };
    }

    [Obsolete(
        "The FormatText method which takes a format string is obsolete. Please use the method overload with one argument instead.",
        true )]
    public virtual string FormatText( IComparable value, string format )
    {
        throw new NotSupportedException(
            "The FormatText method which takes a format string is obsolete. Please use the method overload with one argument instead." );
    }

    public virtual string FormatText( IComparable value )
    {
        if ( LabelProvider == null )
            return value.ToString();
        return LabelProvider.FormatLabel( value );
    }

    public virtual string FormatCursorText( IComparable value )
    {
        if ( LabelProvider == null )
            return value.ToString();
        return LabelProvider.FormatCursorLabel( value );
    }

    public virtual IComparable GetDataValue( double pixelCoordinate )
    {
        if ( _currentCoordinateCalculator == null )
            return ( IComparable ) double.NaN;
        return ( IComparable ) _currentCoordinateCalculator.GetDataValue( pixelCoordinate );
    }

    public virtual double GetCoordinate( IComparable value )
    {
        if ( _currentCoordinateCalculator == null )
            return double.NaN;
        return _currentCoordinateCalculator.GetCoordinate( value.ToDouble() );
    }

    public bool IsPointWithinBounds( Point point )
    {
        return ElementExtensions.IsPointWithinBounds(
            this,
            ParentSurface.RootGrid.TranslatePoint( point, ( IHitTestable ) this ) );
    }

    public Point TranslatePoint( Point point, IHitTestable relativeTo )
    {
        return ElementExtensions.TranslatePoint( this, point, relativeTo );
    }

    public Rect GetBoundsRelativeTo( IHitTestable relativeTo )
    {
        return ElementExtensions.GetBoundsRelativeTo( this, relativeTo );
    }

    public void OnDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
    {
        if ( !IsValidForDrawing() )
            return;
        using ( IUpdateSuspender updateSuspender = SuspendUpdates() )
        {
            updateSuspender.ResumeTargetOnDispose = false;
            Stopwatch stopwatch = Stopwatch.StartNew();
            if ( LabelProvider != null )
                LabelProvider.OnBeginAxisDraw();
            _tickCoords = CalculateTicks();
            DrawGridLines( renderContext, _tickCoords );
            if ( IsShown() )
                OnDrawAxis( _tickCoords );
            stopwatch.Stop();
            UltrachartDebugLogger.Instance
                .WriteLine(
                    "Drawn {0}: Width={1}, Height={2} in {3}ms",
                    GetType().Name,
                    ActualWidth,
                    ActualHeight,
                    stopwatch.ElapsedMilliseconds );
        }
    }

    private bool IsValidForDrawing()
    {
        if ( !IsSuspended && IsVisibleRangeValid() )
            return IsLicenseValid;
        return false;
    }

    private bool IsShown()
    {
        UltrachartSurface parentSurface = ParentSurface as UltrachartSurface;
        if ( parentSurface != null && parentSurface.IsVisible() && this.IsVisible() )
            return HasAxisPanel();
        return false;
    }

    private bool IsVisibleRangeValid()
    {
        bool flag = IsValidRange(VisibleRange) && !VisibleRange.IsZero;
        if ( !flag )
            UltrachartDebugLogger.Instance.WriteLine( "{0} is not a valid VisibleRange for {1}", VisibleRange, GetType() );
        return flag;
    }

    protected virtual TickCoordinates CalculateTicks()
    {
        CalculateDelta();
        Guard.NotNull( TickProvider, "TickProvider" );
        IAxisParams axis = (IAxisParams) this;
        double[] majorTicks = TickProvider.GetMajorTicks(axis);
        return TickCoordinatesProvider.GetTickCoordinates( TickProvider.GetMinorTicks( axis ), majorTicks );
    }

    protected abstract void CalculateDelta();

    protected abstract IDeltaCalculator GetDeltaCalculator();

    protected virtual uint GetMaxAutoTicks()
    {
        return ( uint ) Math.Max( 1, MaxAutoTicks );
    }

    protected virtual void DrawGridLines( IRenderContext2D renderContext, TickCoordinates tickCoords )
    {
        if ( renderContext == null )
            return;
        if ( DrawMinorGridLines && tickCoords.MinorTickCoordinates.Length != 0 )
            renderContext.Layers[ RenderLayer.AxisMinorGridlines ].Enqueue(
                ( Action ) ( () => DrawGridLine(
                    renderContext,
                    MinorGridLineStyle,
                    ( IEnumerable<float> ) tickCoords.MinorTickCoordinates ) ) );
        if ( tickCoords.MajorTickCoordinates.Length == 0 )
            return;
        if ( DrawMajorBands )
            renderContext.Layers[ RenderLayer.AxisBands ].Enqueue(
                ( Action ) ( () => DrawBand( renderContext, tickCoords.MajorTicks, tickCoords.MajorTickCoordinates ) ) );
        if ( !DrawMajorGridLines )
            return;
        renderContext.Layers[ RenderLayer.AxisMajorGridlines ].Enqueue(
            ( Action ) ( () => DrawGridLine(
                renderContext,
                MajorGridLineStyle,
                ( IEnumerable<float> ) tickCoords.MajorTickCoordinates ) ) );
    }

    private void DrawBand( IRenderContext2D renderContext, double[ ] ticks, float[ ] ticksCoords )
    {
        IRenderSurface renderSurface = RenderSurface;
        if ( renderSurface == null )
            return;
        XyDirection direction = IsHorizontalAxis ? XyDirection.XDirection : XyDirection.YDirection;
        using ( IBrush2D brush = renderContext.CreateBrush( AxisBandsFill, 1.0, new bool?() ) )
        {
            float axisOffset = (float) GetAxisOffset();
            float num = IsHorizontalAxis ? 0.0f : axisOffset + (float) ActualHeight;
            float coord0_1 = IsHorizontalAxis ? (float) renderSurface.ActualWidth : axisOffset;
            if ( FlipCoordinates ^ IsAxisFlipped )
                NumberUtil.Swap( ref num, ref coord0_1 );
            bool flag = GetMajorTickIndex(ticks[0]) % new Decimal(2) == Decimal.Zero;
            for ( int index = 0 ; index < ticksCoords.Length ; ++index )
            {
                if ( flag )
                {
                    float coord0_2 = index == 0 ? num : ticksCoords[index - 1];
                    float ticksCoord = ticksCoords[index];
                    DrawBand( renderContext, brush, direction, coord0_2, ticksCoord );
                }
                flag = !flag;
            }
            if ( !flag )
                return;
            DrawBand( renderContext, brush, direction, coord0_1, ( ( IEnumerable<float> ) ticksCoords ).Last<float>() );
        }
    }

    private void DrawBand(
        IRenderContext2D renderContext,
        IBrush2D bandBrush,
        XyDirection direction,
        float coord0,
        float coord1 )
    {
        IRenderSurface renderSurface = RenderSurface;
        if ( renderSurface == null )
            return;
        Point pt1 = direction == XyDirection.YDirection
            ? new Point(0.0, (double) coord0)
            : new Point((double) coord0, 0.0);
        Point pt2 = direction == XyDirection.YDirection
            ? new Point(renderSurface.ActualWidth, (double) coord1)
            : new Point((double) coord1, renderSurface.ActualHeight);
        renderContext.FillRectangle( bandBrush, pt1, pt2, 0.0 );
    }

    protected virtual void DrawGridLine(
        IRenderContext2D renderContext,
        Style gridLineStyle,
        IEnumerable<float> coordsToDraw )
    {
        XyDirection direction = IsHorizontalAxis ? XyDirection.XDirection : XyDirection.YDirection;
        LineToStyle.Style = gridLineStyle;
        ThemeManager.SetTheme( ( DependencyObject ) LineToStyle, ThemeManager.GetTheme( ( DependencyObject ) this ) );
        using ( IPen2D styledPen = renderContext.GetStyledPen( LineToStyle, false ) )
        {
            if ( styledPen == null || styledPen.Color.A == ( byte ) 0 )
                return;
            foreach ( float atPoint in coordsToDraw )
                DrawGridLine( renderContext, styledPen, direction, atPoint );
        }
    }

    protected void DrawGridLine( IRenderContext2D renderContext, IPen2D linePen, XyDirection direction, float atPoint )
    {
        IRenderSurface renderSurface = RenderSurface;
        if ( renderSurface == null )
            return;
        float strokeThickness = linePen.StrokeThickness;
        Point pt1 = direction == XyDirection.YDirection
            ? new Point(-(double) strokeThickness, (double) atPoint)
            : new Point((double) atPoint, -(double) strokeThickness);
        Point pt2 = direction == XyDirection.YDirection
            ? new Point(renderSurface.ActualWidth + (double) strokeThickness, (double) atPoint)
            : new Point((double) atPoint, renderSurface.ActualHeight + (double) strokeThickness);
        renderContext.DrawLine( linePen, pt1, pt2 );
    }

    protected virtual void OnDrawAxis( TickCoordinates tickCoords )
    {
        _offset = ( float ) GetOffsetForLabels();
        AxisPanel.DrawTicks( tickCoords, _offset );
        if ( !DrawLabels )
            Clear();
        AxisPanel.Invalidate();
    }

    protected virtual double GetOffsetForLabels()
    {
        return ( IsHorizontalAxis ? BorderThickness.Left : BorderThickness.Top ) + GetAxisOffset();
    }

    protected virtual void DrawTickLabels( AxisCanvas canvas, TickCoordinates tickCoords, float offset )
    {
        double[] majorTicks = tickCoords.MajorTicks;
        float[] majorTickCoordinates = tickCoords.MajorTickCoordinates;
        if ( majorTicks == null || majorTickCoordinates == null )
            return;
        int num = canvas.Children.Count - majorTickCoordinates.Length;
        if ( num > 0 )
        {
            using ( canvas.SuspendUpdates() )
            {
                for ( int index1 = num - 1 ; index1 >= 0 ; --index1 )
                {
                    int index2 = majorTickCoordinates.Length + index1;
                    RemoveTickLabel( canvas, index2 );
                }
            }
        }
        for ( int index = 0 ; index < majorTickCoordinates.Length ; ++index )
        {
            double tick = majorTicks[index];
            DefaultTickLabel label = index < canvas.Children.Count
                ? (DefaultTickLabel) canvas.Children[index]
                : _labelsPool.Get(new Func<DefaultTickLabel, DefaultTickLabel>(ApplyStyle));
            IComparable dataValue = ConvertTickToDataValue((IComparable) tick);
            UpdateAxisLabel( label, dataValue );
            label.CullingPriority = CalculateLabelCullingPriority( tick );
            Point labelPosition = GetLabelPosition(offset, majorTickCoordinates[index]);
            label.Position = labelPosition;
            canvas.SafeAddChild( label, -1 );
        }
    }

    protected virtual Point GetLabelPosition( float offset, float coords )
    {
        float num = coords - offset;
        double x = IsHorizontalAxis ? (double) num : 0.0;
        return new Point( x, ( double ) num - x );
    }

    protected void RemoveTickLabel( AxisCanvas canvas, int index )
    {
        DefaultTickLabel child = (DefaultTickLabel) canvas.Children[index];
        canvas.Children.Remove( ( UIElement ) child );
        _labelsPool.Put( child );
    }

    protected virtual IComparable ConvertTickToDataValue( IComparable value )
    {
        return value;
    }

    private void UpdateAxisLabel( DefaultTickLabel label, IComparable value )
    {
        ILabelProvider labelProvider = LabelProvider;
        if ( labelProvider == null )
            return;
        label.DataContext = label.DataContext == null
            ? labelProvider.CreateDataContext( value )
            : labelProvider.UpdateDataContext( ( ITickLabelViewModel ) label.DataContext, value );
    }

    private int CalculateLabelCullingPriority( double tick )
    {
        Decimal tickNum = GetMajorTickIndex(tick);
        return ( ( IEnumerable<int> ) AxisBase.LabelCullingDistances ).Count<int>(
            ( Func<int, bool> ) ( i => tickNum % ( Decimal ) i == Decimal.Zero ) );
    }

    private Decimal GetMajorTickIndex( double tick )
    {
        ICategoryCoordinateCalculator coordinateCalculator = _currentCoordinateCalculator as ICategoryCoordinateCalculator;
        if ( coordinateCalculator != null )
            tick = ( double ) coordinateCalculator.TransformDataToIndex( ( IComparable ) tick );
        double num1 = MajorDelta.ToDouble();
        if ( IsLogarithmicAxis )
        {
            ILogarithmicAxis logarithmicAxis = this as ILogarithmicAxis;
            tick = Math.Log( tick, logarithmicAxis.LogarithmicBase );
        }
        double d = tick / num1;
        if ( d >= ( double ) int.MaxValue )
        {
            double num2 = d / (double) int.MaxValue;
            d = ( num2 - ( double ) ( int ) num2 ) * ( double ) int.MaxValue;
        }
        return ( Decimal ) ( ( int ) d.RoundOff() );
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _axisContainer = GetAndAssertTemplateChild<StackPanel>( "PART_AxisContainer" );
        _axisPanel = GetAndAssertTemplateChild<IAxisPanel>( "PART_AxisCanvas" );
        ( ( StockSharp.Xaml.Charting.Themes.AxisPanel ) _axisPanel ).AddLabels = ( Action<AxisCanvas> ) ( canvas =>
        {
            if ( !IsValidForDrawing() || !IsShown() )
                return;
            DrawTickLabels( canvas, _tickCoords, _offset );
        } );
        _modifierAxisCanvas = GetAndAssertTemplateChild<StockSharp.Xaml.Charting.Themes.ModifierAxisCanvas>(
            "PART_ModifierAxisCanvas" );
        _modifierAxisCanvas.ParentAxis = this;
        if ( VisibleRange != null )
            return;
        SetCurrentValue( AxisBase.VisibleRangeProperty, GetUndefinedRange() );
    }

    protected T GetAndAssertTemplateChild<T>( string childName ) where T : class
    {
        T templateChild = GetTemplateChild(childName) as T;
        if ( templateChild != null )
            return templateChild;
        throw new InvalidOperationException(
            string.Format( "Unable to Apply the Control Template. {0} is missing or of the wrong type", childName ) );
    }

    private static void OnVisibleRangeDependencyPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = (AxisBase) d;
        if ( !axisBase.Dispatcher.CheckAccess() )
        {
            Action action = (Action) (() => AxisBase.OnVisibleRangeDependencyPropertyChanged(d, e));
            axisBase.Dispatcher.BeginInvoke( ( Delegate ) action );
        }
        else
        {
            IRange oldValue = e.OldValue as IRange;
            IRange newValue = e.NewValue as IRange;
            if ( oldValue != null )
                oldValue.PropertyChanged -= new PropertyChangedEventHandler(
                    axisBase.OnMaxMinVisibleRangePropertiesChanged );
            if ( newValue == null )
                return;
            if ( !axisBase.HasValidVisibleRange || !axisBase.IsVisibleRangeMinimalConstrainValid() )
                axisBase.CoerceVisibleRange();
            if ( !axisBase.HasValidVisibleRange || !axisBase.IsVisibleRangeMinimalConstrainValid() )
            {
                axisBase.SetCurrentValue( AxisBase.VisibleRangeProperty, axisBase._lastValidRange );
                axisBase.AssertRangeType( newValue );
            }
            else
            {
                newValue.PropertyChanged += new PropertyChangedEventHandler(
                    axisBase.OnMaxMinVisibleRangePropertiesChanged );
                if ( !axisBase.TryApplyVisibleRange( newValue, oldValue ) )
                    return;
                axisBase._secondLastValidRange = axisBase._lastValidRange;
                axisBase._lastValidRange = newValue;
            }
        }
    }

    private void OnMaxMinVisibleRangePropertiesChanged( object sender, PropertyChangedEventArgs e )
    {
        IComparable min = VisibleRange.Min;
        IComparable max = VisibleRange.Max;
        string propertyName = e.PropertyName;
        if ( !( propertyName == "Min" ) )
        {
            if ( propertyName == "Max" )
                max = ( IComparable ) ( ( PropertyChangedEventArgsWithValues ) e ).OldValue;
        }
        else
            min = ( IComparable ) ( ( PropertyChangedEventArgsWithValues ) e ).OldValue;
        if ( !TryApplyVisibleRange( VisibleRange, RangeFactory.NewWithMinMax( VisibleRange, min, max ) ) )
            return;
        BindingExpression bindingExpression = GetBindingExpression(AxisBase.VisibleRangeProperty);
        if ( bindingExpression == null ||
            bindingExpression.ParentBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit )
            return;
        bindingExpression.UpdateSource();
        bindingExpression.UpdateTarget();
    }

    private bool TryApplyVisibleRange( IRange newRange, IRange oldRange )
    {
        bool flag = false;
        ValidateVisibleRange( newRange );
        if ( !newRange.Equals( oldRange ) )
        {
            OnVisibleRangeChanged( new VisibleRangeChangedEventArgs( oldRange, newRange, _isAnimationChange ) );
            InvalidateParentSurface();
            flag = true;
        }
        return flag;
    }

    private void ValidateVisibleRange( IRange range )
    {
        AssertRangeType( range );
        if ( range.Min.CompareTo( range.Max ) > 0 )
            throw new ArgumentException(
                string.Format(
                    "VisibleRange.Min (value={0}) must be less than VisibleRange.Max (value={1})",
                    range.Min,
                    range.Max ) );
    }

    protected virtual void AssertRangeType( IRange range )
    {
        if ( !IsOfValidType( range ) )
            throw new InvalidOperationException(
                string.Format(
                    "Axis type {0} requires that VisibleRange is of type {1}",
                    GetType().Name,
                    VisibleRange.GetType().FullName ) );
    }

    protected virtual void OnVisibleRangeChanged( VisibleRangeChangedEventArgs args )
    {
        if ( ParentSurface != null )
            ParentSurface.ViewportManager?.OnVisibleRangeChanged( ( IAxis ) this );
        // ISSUE: reference to a compiler-generated field
        EventHandler<VisibleRangeChangedEventArgs> visibleRangeChanged = VisibleRangeChanged;
        if ( visibleRangeChanged == null )
            return;
        visibleRangeChanged( this, args );
    }

    internal static void NotifyDataRangeChanged( IAxis target )
    {
        AxisBase axisBase = target as AxisBase;
        if ( axisBase == null )
            return;
        axisBase.OnDataRangeChanged();
        axisBase.OnPropertyChanged( "DataRange" );
    }

    private void OnDataRangeChanged()
    {
        // ISSUE: reference to a compiler-generated field
        EventHandler<EventArgs> dataRangeChanged = DataRangeChanged;
        if ( dataRangeChanged == null )
            return;
        dataRangeChanged( this, EventArgs.Empty );
    }

    private bool IsVisibleRangeMinimalConstrainValid()
    {
        if ( MinimalZoomConstrain != null )
            return VisibleRange.Diff.ToDouble() >= MinimalZoomConstrain.ToDouble();
        return true;
    }

    protected virtual void CoerceVisibleRange()
    {
    }

    private static void OnVisibleRangeLimitDependencyPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = (AxisBase) d;
        IRange oldValue = e.OldValue as IRange;
        IRange newValue = e.NewValue as IRange;
        if ( oldValue != null )
            oldValue.PropertyChanged -= new PropertyChangedEventHandler(
                axisBase.OnMaxMinVisibleRangeLimitPropertiesChanged );
        if ( newValue == null || axisBase.VisibleRange == null )
            return;
        newValue.PropertyChanged += new PropertyChangedEventHandler( axisBase.OnMaxMinVisibleRangeLimitPropertiesChanged );
        IRange range = ((IRange) axisBase.VisibleRange.Clone()).ClipTo(newValue, axisBase.VisibleRangeLimitMode);
        axisBase.SetCurrentValue( AxisBase.VisibleRangeProperty, range );
    }

    private void OnMaxMinVisibleRangeLimitPropertiesChanged( object sender, PropertyChangedEventArgs e )
    {
        TryApplyVisibleRangeLimit( VisibleRange );
        BindingExpression bindingExpression = GetBindingExpression(AxisBase.VisibleRangeLimitProperty);
        if ( bindingExpression == null ||
            bindingExpression.ParentBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit )
            return;
        bindingExpression.UpdateSource();
        bindingExpression.UpdateTarget();
    }

    private static void OnAnimatedVisibleRangeDependencyPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = (AxisBase) d;
        if ( e.NewValue == null )
            return;
        axisBase.AnimateVisibleRangeTo( ( IRange ) e.NewValue, TimeSpan.FromMilliseconds( 500.0 ) );
    }

    public void AnimateVisibleRangeTo( IRange to, TimeSpan duration )
    {
        Guard.NotNull( to, nameof( to ) );
        if ( !HasValidVisibleRange )
        {
            VisibleRange = to;
        }
        else
        {
            _fromPoint = TransformRangeToPoint( VisibleRange );
            Point point = TransformRangeToPoint(to);
            PointAnimation pointAnimation1 = new PointAnimation();
            pointAnimation1.From = new Point?( new Point( 0.0, 0.0 ) );
            pointAnimation1.To = new Point?( new Point( point.X - _fromPoint.X, point.Y - _fromPoint.Y ) );
            pointAnimation1.Duration = ( Duration ) duration;
            ExponentialEase exponentialEase = new ExponentialEase();
            exponentialEase.EasingMode = EasingMode.EaseOut;
            exponentialEase.Exponent = 7.0;
            pointAnimation1.EasingFunction = ( IEasingFunction ) exponentialEase;
            PointAnimation pointAnimation = pointAnimation1;
            IRange prevRange = (IRange) VisibleRange.Clone();
            pointAnimation.Completed += ( EventHandler ) ( ( s, e ) =>
            {
                VisibleRange = to;
                _isAnimationChange = false;
                pointAnimation.FillBehavior = FillBehavior.Stop;
                OnVisibleRangeChanged( new VisibleRangeChangedEventArgs( prevRange, to, _isAnimationChange ) );
            } );
            Storyboard.SetTarget( ( DependencyObject ) pointAnimation, ( DependencyObject ) this );
            Storyboard.SetTargetProperty(
                ( DependencyObject ) pointAnimation,
                new PropertyPath( "VisibleRangePoint", new object[ 0 ] ) );
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = ( Duration ) duration;
            storyboard.Children.Add( ( Timeline ) pointAnimation );
            _isAnimationChange = true;
            storyboard.Begin();
        }
    }

    private Point TransformRangeToPoint( IRange range )
    {
        DoubleRange doubleRange = range.AsDoubleRange();
        double num1 = doubleRange.Min;
        double num2 = doubleRange.Max;
        if ( IsLogarithmicAxis )
        {
            double logarithmicBase = ((ILogarithmicAxis) this).LogarithmicBase;
            num1 = Math.Log( num1, logarithmicBase );
            num2 = Math.Log( num2, logarithmicBase );
        }
        return new Point( num1, num2 );
    }

    private static void OnVisibleRangePointDependencyPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e )
    {
        Point p = (Point) e.NewValue;
        AxisBase axis = (AxisBase) d;
        axis.Dispatcher
            .BeginInvokeAlways(
                ( Action ) ( () =>
                {
                    if ( axis.VisibleRange == null )
                        return;
                    double y1 = axis._fromPoint.X + p.X;
                    double y2 = axis._fromPoint.Y + p.Y;
                    if ( axis.IsLogarithmicAxis )
                    {
                        double logarithmicBase = ((ILogarithmicAxis) axis).LogarithmicBase;
                        y1 = Math.Pow( logarithmicBase, y1 );
                        y2 = Math.Pow( logarithmicBase, y2 );
                    }
                    axis._isAnimationChange = true;
                    axis.VisibleRange = RangeFactory.NewRange(
                        axis.VisibleRange.GetType(),
                        ( IComparable ) y1,
                        ( IComparable ) y2 );
                } ) );
    }

    private static void OnOrientationChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( d as AxisBase )?.OnPropertyChanged( "IsHorizontalAxis" );
    }

    private static void OnAlignmentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = (AxisBase) d;
        ISciChartSurface parentSurface = axisBase.ParentSurface;
        if ( parentSurface != null && !axisBase.IsSuspended )
            parentSurface.OnAxisAlignmentChanged( ( IAxis ) axisBase, ( AxisAlignment ) e.OldValue );
        AxisBase.InvalidateParent( d, e );
    }

    private static void OnIsCenterAxisDependencyPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = (AxisBase) d;
        ISciChartSurface parentSurface = axisBase.ParentSurface;
        if ( parentSurface != null && !axisBase.IsSuspended )
            parentSurface.OnIsCenterAxisChanged( ( IAxis ) axisBase );
        AxisBase.InvalidateParent( d, e );
    }

    protected static void InvalidateParent( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = (AxisBase) d;
        if ( !axisBase.HasAxisPanel() )
            return;
        axisBase.InvalidateParentSurface();
    }

    private static void OnScrollBarChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = d as AxisBase;
        UltrachartScrollbar newValue = e.NewValue as UltrachartScrollbar;
        if ( newValue == null )
            return;
        newValue.Axis = ( IAxis ) axisBase;
    }

    private bool HasAxisPanel()
    {
        return AxisPanel != null;
    }

    private void InvalidateParentSurface()
    {
        if ( Services == null || ParentSurface == null || ParentSurface.IsSuspended )
            return;
        Services.GetService<IEventAggregator>()
            .Publish<InvalidateUltrachartMessage>( new InvalidateUltrachartMessage( this ) );
    }

    private static void OnLabelProviderChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = d as AxisBase;
        ( e.NewValue as ILabelProvider )?.Init( ( IAxis ) axisBase );
        AxisBase.InvalidateParent( d, e );
    }

    private static void OnTickProviderChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = d as AxisBase;
        ( e.NewValue as ITickProvider )?.Init( ( IAxis ) axisBase );
        ( e.OldValue as ITickProvider )?.Init( ( IAxis ) null );
        AxisBase.InvalidateParent( d, e );
    }

    private static void OnIsPrimaryAxisChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = d as AxisBase;
        if ( axisBase == null || !axisBase.IsPrimaryAxis )
            return;
        axisBase.InvalidateElement();
    }

    private static void OnFlipCoordinatesChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = d as AxisBase;
        if ( axisBase == null )
            return;
        if ( axisBase.FlipCoordinates && axisBase.IsCategoryAxis )
            throw new InvalidOperationException(
                "The CategoryDateTimeAxis type does not support coordinate reversal (FlipCoordinates)." );
        AxisBase.InvalidateParent( d, e );
    }

    private static void OnIsStaticAxisChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = d as AxisBase;
        if ( axisBase == null )
            return;
        if ( axisBase.IsStaticAxis && axisBase.IsCategoryAxis )
            throw new InvalidOperationException(
                "The CategoryDateTimeAxis type does not support the Static mode (IsStatic)." );
        axisBase.TickCoordinatesProvider = axisBase.IsStaticAxis
            ? ( ITickCoordinatesProvider ) new StaticTickCoordinatesProvider()
            : ( ITickCoordinatesProvider ) new DefaultTickCoordinatesProvider();
    }

    private static void OnTickCoordinatesProviderChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        AxisBase axisBase = d as AxisBase;
        axisBase?.TickCoordinatesProvider.Init( ( IAxis ) axisBase );
    }

    [SpecialName]
    HorizontalAlignment IAxis.HorizontalAlignment
    {
        get
        {
            return HorizontalAlignment;
        }

        set
        {
            HorizontalAlignment = value;
        }
    }


    [SpecialName]
    VerticalAlignment IAxis.VerticalAlignment
    {
        get
        {
            return VerticalAlignment;
        }

        set
        {
            VerticalAlignment = value;
        }
    }


    [SpecialName]
    Visibility IAxis.Visibility
    {
        get
        {
            return Visibility;
        }

        set
        {
            Visibility = value;
        }
    }


    [SpecialName]
    double IHitTestable.ActualWidth
    {
        get
        {
            return ActualWidth;
        }
    }

    [SpecialName]
    double IHitTestable.ActualHeight
    {
        get
        {
            return ActualHeight;
        }
    }
}
