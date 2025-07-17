// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.AxisProxy
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Schema;
namespace Ecng.Xaml.Charting
{
    internal class AxisProxy : IAxis, IAxisParams, IHitTestable, ISuspendable, IInvalidatableElement, IDrawable
    {
        private IAxis _axis;

        public AxisProxy( IAxis axis )
        {
            this.VisibleRange = axis.VisibleRange;
            this.IsCategoryAxis = axis.IsCategoryAxis;
            this.Id = axis.Id;
            this._axis = axis;
        }

        public double ActualWidth
        {
            get; private set;
        }

        public double ActualHeight
        {
            get; private set;
        }

        public Point TranslatePoint( Point point, IHitTestable relativeTo )
        {
            throw new NotImplementedException();
        }

        public bool IsPointWithinBounds( Point point )
        {
            throw new NotImplementedException();
        }

        public Rect GetBoundsRelativeTo( IHitTestable relativeTo )
        {
            throw new NotImplementedException();
        }

        public bool IsSuspended
        {
            get; private set;
        }

        public IUpdateSuspender SuspendUpdates()
        {
            throw new NotImplementedException();
        }

        public void ResumeUpdates( IUpdateSuspender suspender )
        {
            throw new NotImplementedException();
        }

        public void DecrementSuspend()
        {
            throw new NotImplementedException();
        }

        public void InvalidateElement()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<VisibleRangeChangedEventArgs> VisibleRangeChanged;

        public event EventHandler<EventArgs> DataRangeChanged;

        public string Id
        {
            get; set;
        }

        public bool AutoTicks
        {
            get; set;
        }

        public ITickProvider TickProvider
        {
            get; set;
        }

        public IRange AnimatedVisibleRange
        {
            get; set;
        }

        public IRange VisibleRange
        {
            get; set;
        }

        public IRange DataRange
        {
            get; private set;
        }

        public double Width
        {
            get; private set;
        }

        double IDrawable.Height
        {
            get
            {
                return this.Height;
            }
            set
            {
                this.Height = value;
            }
        }

        public void OnDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            throw new NotImplementedException();
        }

        double IDrawable.Width
        {
            get
            {
                return this.Width;
            }
            set
            {
                this.Width = value;
            }
        }

        public double Height
        {
            get; private set;
        }

        public IRange<double> GrowBy
        {
            get; set;
        }

        public IComparable MinorDelta
        {
            get; set;
        }

        public IComparable MajorDelta
        {
            get; set;
        }

        public IServiceContainer Services
        {
            get; set;
        }

        public ISciChartSurface ParentSurface
        {
            get; set;
        }

        public Orientation Orientation
        {
            get; set;
        }

        public Brush MajorLineStroke
        {
            get; set;
        }

        public Brush MinorLineStroke
        {
            get; set;
        }

        public Style MajorTickLineStyle
        {
            get; set;
        }

        public Style MinorTickLineStyle
        {
            get; set;
        }

        public Style MajorGridLineStyle
        {
            get; set;
        }

        public Style MinorGridLineStyle
        {
            get; set;
        }

        public AutoRange AutoRange
        {
            get; set;
        }

        public bool AutoRangeOnce
        {
            get; set;
        }

        public string TextFormatting
        {
            get; set;
        }

        public string CursorTextFormatting
        {
            get; set;
        }

        public ILabelProvider LabelProvider
        {
            get; set;
        }

        public bool IsXAxis
        {
            get; set;
        }

        public bool IsHorizontalAxis
        {
            get; private set;
        }

        public bool IsStaticAxis
        {
            get; set;
        }

        public bool FlipCoordinates
        {
            get; set;
        }

        public bool HasValidVisibleRange
        {
            get; private set;
        }

        public bool HasDefaultVisibleRange
        {
            get; private set;
        }

        public string AxisTitle
        {
            get; set;
        }

        public Brush TickTextBrush
        {
            get; set;
        }

        public bool AutoAlignVisibleRange
        {
            get; set;
        }

        public bool DrawMinorTicks
        {
            get; set;
        }

        public bool DrawMajorTicks
        {
            get; set;
        }

        public bool DrawMajorGridLines
        {
            get; set;
        }

        public bool DrawMinorGridLines
        {
            get; set;
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get; set;
        }

        public VerticalAlignment VerticalAlignment
        {
            get; set;
        }

        public AxisMode AxisMode
        {
            get; set;
        }

        public AxisAlignment AxisAlignment
        {
            get; set;
        }

        public bool IsCategoryAxis
        {
            get; private set;
        }

        public bool IsLogarithmicAxis
        {
            get; private set;
        }

        public bool IsPolarAxis
        {
            get; private set;
        }

        public bool IsCenterAxis
        {
            get; set;
        }

        public bool IsPrimaryAxis
        {
            get; set;
        }

        public IAnnotationCanvas ModifierAxisCanvas
        {
            get; private set;
        }

        public Visibility Visibility
        {
            get; set;
        }

        public bool IsAxisFlipped
        {
            get; private set;
        }

        public IRange VisibleRangeLimit
        {
            get; set;
        }

        public RangeClipMode VisibleRangeLimitMode
        {
            get; set;
        }

        public IComparable MinimalZoomConstrain
        {
            get; set;
        }

        public bool IsLabelCullingEnabled
        {
            get; set;
        }

        public ICoordinateCalculator<double> GetCurrentCoordinateCalculator()
        {
            throw new NotImplementedException();
        }

        public IAxisInteractivityHelper GetCurrentInteractivityHelper()
        {
            throw new NotImplementedException();
        }

        public bool CaptureMouse()
        {
            throw new NotImplementedException();
        }

        public void ReleaseMouseCapture()
        {
            throw new NotImplementedException();
        }

        public void SetMouseCursor( Cursor cursor )
        {
            throw new NotImplementedException();
        }

        public AxisInfo HitTest( Point atPoint )
        {
            throw new NotImplementedException();
        }

        public IntegerRange GetPointRange()
        {
            throw new NotImplementedException();
        }

        public IRange CalculateYRange( RenderPassInfo renderPassInfo )
        {
            throw new NotImplementedException();
        }

        public IRange GetMaximumRange()
        {
            throw new NotImplementedException();
        }

        public IRange GetWindowedYRange( IDictionary<string, IRange> xRanges )
        {
            throw new NotImplementedException();
        }

        public void ScrollXRange( int deltaX )
        {
            throw new NotImplementedException();
        }

        public double GetCoordinate( IComparable value )
        {
            throw new NotImplementedException();
        }

        public double GetAxisOffset()
        {
            throw new NotImplementedException();
        }

        public IComparable GetDataValue( double pixelCoordinate )
        {
            throw new NotImplementedException();
        }

        public Size OnArrangeAxis()
        {
            throw new NotImplementedException();
        }

        public void OnBeginRenderPass( RenderPassInfo renderPassInfo = default( RenderPassInfo ), IPointSeries firstPointSeries = null )
        {
            throw new NotImplementedException();
        }

        public void Scroll( double pixelsToScroll, ClipMode clipMode )
        {
            throw new NotImplementedException();
        }

        public void Scroll( double pixelsToScroll, ClipMode clipMode, TimeSpan duration )
        {
            throw new NotImplementedException();
        }

        public void ScrollByDataPoints( int pointAmount )
        {
            throw new NotImplementedException();
        }

        public void ScrollByDataPoints( int pointAmount, TimeSpan duration )
        {
            throw new NotImplementedException();
        }

        public void Zoom( double fromCoord, double toCoord )
        {
            throw new NotImplementedException();
        }

        public void Zoom( double fromCoord, double toCoord, TimeSpan duration )
        {
            throw new NotImplementedException();
        }

        public void ZoomBy( double minFraction, double maxFraction )
        {
            throw new NotImplementedException();
        }

        public void ZoomBy( double minFraction, double maxFraction, TimeSpan duration )
        {
            throw new NotImplementedException();
        }

        public void ScrollTo( IRange startVisibleRange, double pixelsToScroll )
        {
            throw new NotImplementedException();
        }

        public void ScrollToWithLimit( IRange startVisibleRange, double pixelsToScroll, IRange rangeLimit )
        {
            throw new NotImplementedException();
        }

        public void AssertDataType( Type dataType )
        {
            this._axis.AssertDataType( dataType );
        }

        public string FormatText( IComparable value, string format )
        {
            throw new NotImplementedException();
        }

        public string FormatText( IComparable value )
        {
            throw new NotImplementedException();
        }

        public string FormatCursorText( IComparable value )
        {
            throw new NotImplementedException();
        }

        public bool IsValidRange( IRange range )
        {
            throw new NotImplementedException();
        }

        public IAxis Clone()
        {
            throw new NotImplementedException();
        }

        public void AnimateVisibleRangeTo( IRange range, TimeSpan duration )
        {
            throw new NotImplementedException();
        }

        public void ValidateAxis()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IRange GetUndefinedRange()
        {
            throw new NotImplementedException();
        }

        public IRange GetDefaultNonZeroRange()
        {
            throw new NotImplementedException();
        }

        public double CurrentDatapointPixelSize
        {
            get
            {
                return double.NaN;
            }
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public double ZoomScale
        {
            get; set;
        }

        public double ZoomScaleLog
        {
            get; set;
        }
    }
}
