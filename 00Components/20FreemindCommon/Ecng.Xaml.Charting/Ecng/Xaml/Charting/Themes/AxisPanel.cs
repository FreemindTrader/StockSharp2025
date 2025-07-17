// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Themes.AxisPanel
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Numerics.TickCoordinateProviders;
using StockSharp.Xaml.Charting.Rendering;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.Themes
{
    public class AxisPanel : Panel, IAxisPanel, INotifyPropertyChanged
    {
        public static readonly DependencyProperty DrawLabelsProperty = DependencyProperty.Register(nameof (DrawLabels), typeof (bool), typeof (AxisPanel), new PropertyMetadata((object) true));
        public static readonly DependencyProperty DrawMinorTicksProperty = DependencyProperty.Register(nameof (DrawMinorTicks), typeof (bool), typeof (AxisPanel), new PropertyMetadata((object) true));
        public static readonly DependencyProperty DrawMajorTicksProperty = DependencyProperty.Register(nameof (DrawMajorTicks), typeof (bool), typeof (AxisPanel), new PropertyMetadata((object) true));
        public static readonly DependencyProperty MajorTickLineStyleProperty = DependencyProperty.Register(nameof (MajorTickLineStyle), typeof (Style), typeof (AxisPanel), new PropertyMetadata((object) null, new PropertyChangedCallback(AxisPanel.OnMajorTickLineStyleDependencyPropertyChanged)));
        public static readonly DependencyProperty MinorTickLineStyleProperty = DependencyProperty.Register(nameof (MinorTickLineStyle), typeof (Style), typeof (AxisPanel), new PropertyMetadata((object) null, new PropertyChangedCallback(AxisPanel.OnMinorTickLineStyleDependencyPropertyChanged)));
        public static readonly DependencyProperty AxisAlignmentProperty = DependencyProperty.Register(nameof (AxisAlignment), typeof (AxisAlignment), typeof (AxisPanel), new PropertyMetadata((object) AxisAlignment.Default, new PropertyChangedCallback(AxisPanel.OnAxisAlignmentChanged)));
        public static readonly DependencyProperty AxisLabelToTickIndentProperty = DependencyProperty.Register(nameof (AxisLabelToTickIndent), typeof (double), typeof (AxisPanel), new PropertyMetadata((object) 2.0, new PropertyChangedCallback(AxisPanel.OnAxisLabelToTickIndentChanged)));
        public static readonly DependencyProperty IsLabelCullingEnabledProperty = DependencyProperty.Register(nameof (IsLabelCullingEnabled), typeof (bool), typeof (AxisPanel), new PropertyMetadata((object) true));
        protected Line LineToStyle = new Line();
        protected Grid _labelsContainer;
        protected Image _axisImage;
        protected AxisTitle _axisTitle;
        private WriteableBitmap _renderWriteableBitmap;
        private double _minorTickSize;
        private double _majorTickSize;
        private bool _isInitialized;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLabelCullingEnabled
        {
            get
            {
                return ( bool ) this.GetValue( AxisPanel.IsLabelCullingEnabledProperty );
            }
            set
            {
                this.SetValue( AxisPanel.IsLabelCullingEnabledProperty, ( object ) value );
            }
        }

        public AxisAlignment AxisAlignment
        {
            get
            {
                return ( AxisAlignment ) this.GetValue( AxisPanel.AxisAlignmentProperty );
            }
            set
            {
                this.SetValue( AxisPanel.AxisAlignmentProperty, ( object ) value );
            }
        }

        public Style MajorTickLineStyle
        {
            get
            {
                return ( Style ) this.GetValue( AxisPanel.MajorTickLineStyleProperty );
            }
            set
            {
                this.SetValue( AxisPanel.MajorTickLineStyleProperty, ( object ) value );
            }
        }

        public Style MinorTickLineStyle
        {
            get
            {
                return ( Style ) this.GetValue( AxisPanel.MinorTickLineStyleProperty );
            }
            set
            {
                this.SetValue( AxisPanel.MinorTickLineStyleProperty, ( object ) value );
            }
        }

        public bool DrawMajorTicks
        {
            get
            {
                return ( bool ) this.GetValue( AxisPanel.DrawMajorTicksProperty );
            }
            set
            {
                this.SetValue( AxisPanel.DrawMajorTicksProperty, ( object ) value );
            }
        }

        public bool DrawMinorTicks
        {
            get
            {
                return ( bool ) this.GetValue( AxisPanel.DrawMinorTicksProperty );
            }
            set
            {
                this.SetValue( AxisPanel.DrawMinorTicksProperty, ( object ) value );
            }
        }

        public bool DrawLabels
        {
            get
            {
                return ( bool ) this.GetValue( AxisPanel.DrawLabelsProperty );
            }
            set
            {
                this.SetValue( AxisPanel.DrawLabelsProperty, ( object ) value );
            }
        }

        public double AxisLabelToTickIndent
        {
            get
            {
                return ( double ) this.GetValue( AxisPanel.AxisLabelToTickIndentProperty );
            }
            set
            {
                this.SetValue( AxisPanel.AxisLabelToTickIndentProperty, ( object ) value );
            }
        }

        public bool IsHorizontalAxis
        {
            get
            {
                AxisAlignment axisAlignment = this.AxisAlignment;
                if ( axisAlignment != AxisAlignment.Bottom )
                    return axisAlignment == AxisAlignment.Top;
                return true;
            }
        }

        public Thickness LabelToTickIndent
        {
            get
            {
                return new Thickness( this.AxisAlignment == AxisAlignment.Right ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == AxisAlignment.Bottom ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == AxisAlignment.Left ? this.AxisLabelToTickIndent : 0.0, this.AxisAlignment == AxisAlignment.Top ? this.AxisLabelToTickIndent : 0.0 );
            }
        }

        public double MajorTickSize
        {
            get
            {
                if ( !this.DrawMajorTicks )
                    return 0.0;
                return this._majorTickSize;
            }
            private set
            {
                if ( value.Equals( this._majorTickSize ) )
                    return;
                this._majorTickSize = value;
                this.OnPropertyChanged( nameof( MajorTickSize ) );
            }
        }

        public double MinorTickSize
        {
            get
            {
                if ( !this.DrawMinorTicks )
                    return 0.0;
                return this._minorTickSize;
            }
            private set
            {
                if ( value.Equals( this._minorTickSize ) )
                    return;
                this._minorTickSize = value;
                this.OnPropertyChanged( nameof( MinorTickSize ) );
            }
        }

        public Action<AxisCanvas> AddLabels
        {
            get; set;
        }

        protected override Size MeasureOverride( Size availableSize )
        {
            if ( !this._isInitialized )
            {
                foreach ( UIElement child in this.Children )
                    this.Initialize( child );
            }
            this.AddTickLabels( this.AddLabels );
            this._labelsContainer.Measure( availableSize );
            double num1 = Math.Max(this.MinorTickSize, this.MajorTickSize);
            Size size;
            switch ( this.AxisAlignment )
            {
                case AxisAlignment.Right:
                case AxisAlignment.Left:
                    this._axisImage.Measure( new Size( num1, availableSize.Height ) );
                    double num2 = num1 + this._labelsContainer.DesiredSize.Width;
                    this._axisTitle.Measure( new Size( availableSize.Width - num2, availableSize.Height ) );
                    size = new Size( num2 + this._axisTitle.DesiredSize.Width, this._labelsContainer.DesiredSize.Height );
                    break;
                case AxisAlignment.Top:
                case AxisAlignment.Bottom:
                    this._axisImage.Measure( new Size( availableSize.Width, num1 ) );
                    double num3 = num1 + this._labelsContainer.DesiredSize.Height;
                    this._axisTitle.Measure( new Size( availableSize.Width, availableSize.Height - num3 ) );
                    size = new Size( this._labelsContainer.DesiredSize.Width, num3 + this._axisTitle.DesiredSize.Height );
                    break;
                default:
                    size = new Size( this._labelsContainer.DesiredSize.Width, this._labelsContainer.DesiredSize.Height );
                    break;
            }
            return size;
        }

        private void Initialize( UIElement child )
        {
            if ( child is Image )
                this._axisImage = ( Image ) child;
            if ( child is AxisTitle )
                this._axisTitle = ( AxisTitle ) child;
            if ( child is Grid )
                this._labelsContainer = ( Grid ) child;
            this._isInitialized = this._axisImage != null && this._axisTitle != null && this._labelsContainer != null;
        }

        protected override Size ArrangeOverride( Size finalSize )
        {
            double num = Math.Max(this.MinorTickSize, this.MajorTickSize);
            switch ( this.AxisAlignment )
            {
                case AxisAlignment.Right:
                    this._axisImage.Arrange( new Rect( 0.0, 0.0, num, finalSize.Height ) );
                    this._labelsContainer.Arrange( new Rect( num, 0.0, this._labelsContainer.DesiredSize.Width, finalSize.Height ) );
                    this._axisTitle.Arrange( new Rect( num + this._labelsContainer.DesiredSize.Width, 0.0, this._axisTitle.DesiredSize.Width, finalSize.Height ) );
                    break;
                case AxisAlignment.Left:
                    double x1 = finalSize.Width - num;
                    this._axisImage.Arrange( new Rect( x1, 0.0, num, finalSize.Height ) );
                    double x2 = x1 - this._labelsContainer.DesiredSize.Width;
                    this._labelsContainer.Arrange( new Rect( x2, 0.0, this._labelsContainer.DesiredSize.Width, finalSize.Height ) );
                    this._axisTitle.Arrange( new Rect( x2 - this._axisTitle.DesiredSize.Width, 0.0, this._axisTitle.DesiredSize.Width, finalSize.Height ) );
                    break;
                case AxisAlignment.Top:
                    double y1 = finalSize.Height - num;
                    this._axisImage.Arrange( new Rect( 0.0, y1, finalSize.Width, num ) );
                    double y2 = y1 - this._labelsContainer.DesiredSize.Height;
                    this._labelsContainer.Arrange( new Rect( 0.0, y2, finalSize.Width, this._labelsContainer.DesiredSize.Height ) );
                    this._axisTitle.Arrange( new Rect( 0.0, y2 - this._axisTitle.DesiredSize.Height, finalSize.Width, this._axisTitle.DesiredSize.Height ) );
                    break;
                case AxisAlignment.Bottom:
                    this._axisImage.Arrange( new Rect( 0.0, 0.0, finalSize.Width, num ) );
                    this._labelsContainer.Arrange( new Rect( 0.0, num, finalSize.Width, this._labelsContainer.DesiredSize.Height ) );
                    this._axisTitle.Arrange( new Rect( 0.0, num + this._labelsContainer.DesiredSize.Height, finalSize.Width, this._axisTitle.DesiredSize.Height ) );
                    break;
            }
            return finalSize;
        }

        public void AddTickLabels( Action<AxisCanvas> addOnCanvas )
        {
            if ( this._labelsContainer == null || !this.DrawLabels )
                return;
            AxisCanvas axisCanvas1 = this._labelsContainer.Children[0].IsVisible() ? (AxisCanvas) this._labelsContainer.Children[1] : (AxisCanvas) this._labelsContainer.Children[0];
            AxisCanvas axisCanvas2 = this._labelsContainer.Children[0].IsVisible() ? (AxisCanvas) this._labelsContainer.Children[0] : (AxisCanvas) this._labelsContainer.Children[1];
            axisCanvas1.Visibility = Visibility.Collapsed;
            axisCanvas1.SizeHeightToContent = this.IsHorizontalAxis;
            axisCanvas1.SizeWidthToContent = !this.IsHorizontalAxis;
            addOnCanvas( axisCanvas1 );
            axisCanvas1.Visibility = Visibility.Visible;
            axisCanvas2.Visibility = Visibility.Collapsed;
        }

        public void Invalidate()
        {
            this.InvalidateMeasure();
            this.InvalidateArrange();
        }

        public virtual void DrawTicks( TickCoordinates tickCoords, float offset )
        {
            Size renderContextSize = this.GetRenderContextSize();
            int width = (int) renderContextSize.Width;
            int height = (int) renderContextSize.Height;
            if ( this._renderWriteableBitmap == null || this._renderWriteableBitmap.PixelWidth != width || this._renderWriteableBitmap.PixelHeight != height )
                this._renderWriteableBitmap = BitmapFactory.New( width, height );
            if ( this._renderWriteableBitmap == null || this._axisImage == null )
                return;
            using ( HsRenderContext renderContext = this._renderWriteableBitmap.GetRenderContext( this._axisImage, ( TextureCacheBase ) null ) )
            {
                renderContext.Clear();
                if ( this.DrawMinorTicks && tickCoords.MinorTickCoordinates != null )
                    this.DrawTicks( ( IRenderContext2D ) renderContext, this.MinorTickLineStyle, this.MinorTickSize, tickCoords.MinorTickCoordinates, offset );
                if ( !this.DrawMajorTicks || tickCoords.MajorTickCoordinates == null )
                    return;
                this.DrawTicks( ( IRenderContext2D ) renderContext, this.MajorTickLineStyle, this.MajorTickSize, tickCoords.MajorTickCoordinates, offset );
            }
        }

        protected virtual Size GetRenderContextSize()
        {
            return new Size( this.IsHorizontalAxis ? ( double ) ( int ) this._labelsContainer.ActualWidth : ( double ) ( int ) this.MajorTickSize, this.IsHorizontalAxis ? ( double ) ( int ) this.MajorTickSize : ( double ) ( int ) this._labelsContainer.ActualHeight );
        }

        protected virtual void DrawTicks( IRenderContext2D renderContext, Style tickStyle, double tickSize, float[ ] tickCoords, float offset )
        {
            this.LineToStyle.Style = tickStyle;
            ThemeManager.SetTheme( ( DependencyObject ) this.LineToStyle, ThemeManager.GetTheme( ( DependencyObject ) this ) );
            using ( IPen2D styledPen = renderContext.GetStyledPen( this.LineToStyle, false ) )
            {
                foreach ( float tickCoord in tickCoords )
                    this.DrawTick( renderContext, styledPen, tickCoord, offset, tickSize );
            }
        }

        private void DrawTick( IRenderContext2D renderContext, IPen2D tickPen, float coord, float offset, double tickSize )
        {
            Size viewportSize = renderContext.ViewportSize;
            float num1 = this.IsHorizontalAxis ? (float) viewportSize.Height : (float) viewportSize.Width;
            float num2 = coord - offset;
            float num3 = num2;
            float num4 = num2;
            float num5 = 0.0f;
            float num6 = (float) tickSize;
            if ( this.AxisAlignment == AxisAlignment.Top || this.AxisAlignment == AxisAlignment.Left )
            {
                num6 = num1 - num6;
                num5 = num1;
            }
            Point pt1;
            Point pt2;
            if ( this.IsHorizontalAxis )
            {
                pt1 = new Point( ( double ) num3, ( double ) num5 );
                pt2 = new Point( ( double ) num4, ( double ) num6 );
            }
            else
            {
                pt1 = new Point( ( double ) num5, ( double ) num3 );
                pt2 = new Point( ( double ) num6, ( double ) num4 );
            }
            renderContext.DrawLine( tickPen, pt1, pt2 );
        }

        public void ClearLabels()
        {
            this._labelsContainer.Children.OfType<TickLabelAxisCanvas>().ForEachDo<TickLabelAxisCanvas>( ( Action<TickLabelAxisCanvas> ) ( panel => panel.Children.Clear() ) );
        }

        protected virtual void OnPropertyChanged( string propertyName )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged( ( object ) this, new PropertyChangedEventArgs( propertyName ) );
        }

        private static void OnAxisAlignmentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AxisPanel axisPanel = d as AxisPanel;
            if ( axisPanel == null )
                return;
            axisPanel.OnPropertyChanged( "LabelToTickIndent" );
            axisPanel.OnPropertyChanged( "IsHorizontalAxis" );
            axisPanel.MajorTickSize = axisPanel.MeasureTickSize( axisPanel.MajorTickLineStyle );
            axisPanel.MinorTickSize = axisPanel.MeasureTickSize( axisPanel.MinorTickLineStyle );
            axisPanel.Invalidate();
        }

        private static void OnMajorTickLineStyleDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AxisPanel axisPanel = d as AxisPanel;
            if ( axisPanel == null )
                return;
            Style newValue = (Style) e.NewValue;
            axisPanel.MajorTickSize = axisPanel.MeasureTickSize( newValue );
        }

        private static void OnMinorTickLineStyleDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AxisPanel axisPanel = d as AxisPanel;
            if ( axisPanel == null )
                return;
            Style newValue = (Style) e.NewValue;
            axisPanel.MinorTickSize = axisPanel.MeasureTickSize( newValue );
        }

        private double MeasureTickSize( Style lineStyle )
        {
            Line line1 = new Line();
            line1.Style = lineStyle;
            Line line2 = line1;
            if ( !this.IsHorizontalAxis )
                return line2.X2 + 1.0;
            return line2.Y2 + 1.0;
        }

        private static void OnAxisLabelToTickIndentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as AxisPanel )?.OnPropertyChanged( "LabelToTickIndent" );
        }

        internal Image AxisImage
        {
            get
            {
                return this._axisImage;
            }
        }

        internal Grid LabelContainer
        {
            get
            {
                return this._labelsContainer;
            }
        }
    }
}
