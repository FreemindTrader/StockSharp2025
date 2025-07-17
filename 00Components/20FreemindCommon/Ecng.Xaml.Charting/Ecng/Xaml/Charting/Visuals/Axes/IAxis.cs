// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.IAxis
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace fx.Xaml.Charting
{
    public interface IAxis : IAxisParams, IHitTestable, ISuspendable, IInvalidatableElement, IDrawable
    {
        event EventHandler<VisibleRangeChangedEventArgs> VisibleRangeChanged;

        event EventHandler<EventArgs> DataRangeChanged;

        string Id
        {
            get; set;
        }

        bool AutoTicks
        {
            get; set;
        }

        ITickProvider TickProvider
        {
            get; set;
        }

        [TypeConverter( typeof( StringToDoubleRangeTypeConverter ) )]
        IRange AnimatedVisibleRange
        {
            get; set;
        }

        IRange DataRange
        {
            get;
        }

        IServiceContainer Services
        {
            get; set;
        }

        ISciChartSurface ParentSurface
        {
            get; set;
        }

        Orientation Orientation
        {
            get; set;
        }

        [Obsolete( "MajorLineStroke is obsolete, please use MajorTickLineStyle instead", true )]
        Brush MajorLineStroke
        {
            get; set;
        }

        [Obsolete( "MinorLineStroke is obsolete, please use MajorTickLineStyle instead", true )]
        Brush MinorLineStroke
        {
            get; set;
        }

        Style MajorTickLineStyle
        {
            get; set;
        }

        Style MinorTickLineStyle
        {
            get; set;
        }

        Style MajorGridLineStyle
        {
            get; set;
        }

        Style MinorGridLineStyle
        {
            get; set;
        }

        AutoRange AutoRange
        {
            get; set;
        }

        string TextFormatting
        {
            get; set;
        }

        string CursorTextFormatting
        {
            get; set;
        }

        ILabelProvider LabelProvider
        {
            get; set;
        }

        bool IsXAxis
        {
            get; set;
        }

        bool IsHorizontalAxis
        {
            get;
        }

        bool IsStaticAxis
        {
            get; set;
        }

        bool FlipCoordinates
        {
            get; set;
        }

        bool HasValidVisibleRange
        {
            get;
        }

        bool HasDefaultVisibleRange
        {
            get;
        }

        string AxisTitle
        {
            get; set;
        }

        Brush TickTextBrush
        {
            get; set;
        }

        bool AutoAlignVisibleRange
        {
            get; set;
        }

        bool DrawMinorTicks
        {
            get; set;
        }

        bool DrawMajorTicks
        {
            get; set;
        }

        bool DrawMajorGridLines
        {
            get; set;
        }

        bool DrawMinorGridLines
        {
            get; set;
        }

        HorizontalAlignment HorizontalAlignment
        {
            get; set;
        }

        VerticalAlignment VerticalAlignment
        {
            get; set;
        }

        [Obsolete( "IAxis.AxisMode is obsolete, please use NumericAxis or LogarithmicNumericAxis instead" )]
        AxisMode AxisMode
        {
            get; set;
        }

        AxisAlignment AxisAlignment
        {
            get; set;
        }

        bool IsCategoryAxis
        {
            get;
        }

        bool IsLogarithmicAxis
        {
            get;
        }

        bool IsPolarAxis
        {
            get;
        }

        bool IsCenterAxis
        {
            get; set;
        }

        bool IsPrimaryAxis
        {
            get; set;
        }

        IAnnotationCanvas ModifierAxisCanvas
        {
            get;
        }

        Visibility Visibility
        {
            get; set;
        }

        bool IsAxisFlipped
        {
            get;
        }

        [TypeConverter( typeof( StringToDoubleRangeTypeConverter ) )]
        IRange VisibleRangeLimit
        {
            get; set;
        }

        RangeClipMode VisibleRangeLimitMode
        {
            get; set;
        }

        IComparable MinimalZoomConstrain
        {
            get; set;
        }

        bool IsLabelCullingEnabled
        {
            get; set;
        }

        ICoordinateCalculator<double> GetCurrentCoordinateCalculator();

        IAxisInteractivityHelper GetCurrentInteractivityHelper();

        bool CaptureMouse();

        void ReleaseMouseCapture();

        void SetMouseCursor( Cursor cursor );

        AxisInfo HitTest( Point atPoint );

        [Obsolete( "IAxis.GetPointRange is obsolete, please call IDataSeries.GetIndicesRange(VisibleRange) instead", true )]
        IntegerRange GetPointRange();

        IRange CalculateYRange( RenderPassInfo renderPassInfo );

        IRange GetWindowedYRange( IDictionary<string, IRange> xRanges );

        double GetCoordinate( IComparable value );

        IComparable GetDataValue( double pixelCoordinate );

        double GetAxisOffset();

        void OnBeginRenderPass( RenderPassInfo renderPassInfo = default( RenderPassInfo ), IPointSeries firstPointSeries = null );

        void Scroll( double pixelsToScroll, ClipMode clipMode );

        void Scroll( double pixelsToScroll, ClipMode clipMode, TimeSpan duration );

        void ScrollByDataPoints( int pointAmount );

        void ScrollByDataPoints( int pointAmount, TimeSpan duration );

        void Zoom( double fromCoord, double toCoord );

        void Zoom( double fromCoord, double toCoord, TimeSpan duration );

        void ZoomBy( double minFraction, double maxFraction );

        void ZoomBy( double minFraction, double maxFraction, TimeSpan duration );

        [Obsolete( "IAxis.ScrollTo is obsolete, please call IAxis.Scroll(pixelsToScroll) instead" )]
        void ScrollTo( IRange startVisibleRange, double pixelsToScroll );

        void ScrollToWithLimit( IRange startVisibleRange, double pixelsToScroll, IRange rangeLimit );

        void AssertDataType( Type dataType );

        [Obsolete( "The FormatText method which takes a format string is obsolete. Please use the method overload with one argument instead.", true )]
        string FormatText( IComparable value, string format );

        string FormatText( IComparable value );

        string FormatCursorText( IComparable value );

        bool IsValidRange( IRange range );

        IAxis Clone();

        void AnimateVisibleRangeTo( IRange range, TimeSpan duration );

        void ValidateAxis();

        void Clear();

        IRange GetUndefinedRange();

        IRange GetDefaultNonZeroRange();

        double CurrentDatapointPixelSize
        {
            get;
        }
    }
}
