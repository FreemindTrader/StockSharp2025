//// Decompiled with JetBrains decompiler
//// Type: fx.Xaml.Charting.BaseRenderableSeries
//// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using StockSharp.Xaml.Licensing.Core;
//using System;
//using System.Diagnostics;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Markup;
//using System.Windows.Media;
//using System.Xml;
//using System.Xml.Schema;
//using System.Xml.Serialization;

//namespace fx.Xaml.Charting//{
//    [ContentProperty( "PointMarker" )]
//    public abstract class BaseRenderableSeries : ContentControl, IRenderableSeries, IRenderableSeriesBase, IDrawable, IXmlSerializable
//    {
//        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(nameof (StrokeThickness), typeof (int), typeof (BaseRenderableSeries), new PropertyMetadata((object) 1, new PropertyChangedCallback(BaseRenderableSeries.OnStrokeThicknessChanged)));
//        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof (IsSelected), typeof (bool), typeof (BaseRenderableSeries), new PropertyMetadata(new PropertyChangedCallback(BaseRenderableSeries.OnIsSelectedChanged)));
//        [Obsolete("We're sorry! The DataSeriesIndex property has been made obsolete. You can now bind RenderableSeries directly to DataSeries via the BaseRenderableSeries.DataSeries DependencyProperty", true)]
//        public static readonly DependencyProperty DataSeriesIndexProperty = DependencyProperty.Register("DataSeriesIndex", typeof (int), typeof (BaseRenderableSeries), new PropertyMetadata((object) -1, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
//        public static readonly DependencyProperty DataSeriesProperty = DependencyProperty.Register(nameof (DataSeries), typeof (IDataSeries), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, (PropertyChangedCallback) ((s, e) => ((BaseRenderableSeries) s).OnDataSeriesDependencyPropertyChanged((IDataSeries) e.OldValue, (IDataSeries) e.NewValue))));
//        public new static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(nameof (IsVisible), typeof (bool), typeof (BaseRenderableSeries), new PropertyMetadata((object) true, new PropertyChangedCallback(BaseRenderableSeries.OnIsVisibleChanged)));
//        public static readonly DependencyProperty SeriesColorProperty = DependencyProperty.Register(nameof (SeriesColor), typeof (Color), typeof (BaseRenderableSeries), new PropertyMetadata((object) Colors.White, new PropertyChangedCallback(BaseRenderableSeries.OnSeriesColorPropertyChanged)));
//        public static readonly DependencyProperty SelectedSeriesStyleProperty = DependencyProperty.Register(nameof (SelectedSeriesStyle), typeof (Style), typeof (BaseRenderableSeries), new PropertyMetadata(new PropertyChangedCallback(BaseRenderableSeries.OnSelectedSeriesStyleChanged)));
//        public static readonly DependencyProperty ResamplingModeProperty = DependencyProperty.Register(nameof (ResamplingMode), typeof (ResamplingMode), typeof (BaseRenderableSeries), new PropertyMetadata((object) ResamplingMode.Auto, new PropertyChangedCallback(BaseRenderableSeries.OnResamplingPropertyChanged)));
//        public static readonly DependencyProperty AntiAliasingProperty = DependencyProperty.Register(nameof (AntiAliasing), typeof (bool), typeof (BaseRenderableSeries), new PropertyMetadata((object) true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
//        public static readonly DependencyProperty PointMarkerTemplateProperty = DependencyProperty.Register(nameof (PointMarkerTemplate), typeof (ControlTemplate), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnPointMarkerTemplatePropertyChanged)));
//        public static readonly DependencyProperty PointMarkerProperty = DependencyProperty.Register(nameof (PointMarker), typeof (IPointMarker), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnPointMarkerPropertyChanged)));
//        public static readonly DependencyProperty RolloverMarkerTemplateProperty = DependencyProperty.Register(nameof (RolloverMarkerTemplate), typeof (ControlTemplate), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnRolloverMarkerPropertyChanged)));
//        public static readonly DependencyProperty LegendMarkerTemplateProperty = DependencyProperty.Register(nameof (LegendMarkerTemplate), typeof (DataTemplate), typeof (BaseRenderableSeries), new PropertyMetadata((PropertyChangedCallback) null));
//        public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(nameof (YAxisId), typeof (string), typeof (BaseRenderableSeries), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
//        public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(nameof (XAxisId), typeof (string), typeof (BaseRenderableSeries), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
//        public static readonly DependencyProperty PaletteProviderProperty = DependencyProperty.Register(nameof (PaletteProvider), typeof (IPaletteProvider), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
//        public static readonly DependencyProperty ZeroLineYProperty = DependencyProperty.Register(nameof (ZeroLineY), typeof (double), typeof (BaseRenderableSeries), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
//        public static readonly DependencyProperty DrawNaNAsProperty = DependencyProperty.Register(nameof (DrawNaNAs), typeof (LineDrawMode), typeof (BaseRenderableSeries), new PropertyMetadata((object) LineDrawMode.Gaps, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
//        private Size _lastViewportSize = Size.Empty;
//        private FrameworkElement _rolloverMarkerCache;
//        private IPointMarker _pointMarkerFromTemplate;
//        private IDataSeries _dataSeries;
//        private bool _useXCoordOnlyForHitTest;
//        private IAxis _xAxis;
//        private IAxis _yAxis;
//        public const double DefaultHitTestRadius = 7.07;
//        private const int DefaultDatapointWidth = 70;

//        public event EventHandler SelectionChanged;

//        public event EventHandler IsVisibleChanged;

//        protected BaseRenderableSeries( )
//        {
//            this.DefaultStyleKey = ( object ) typeof( BaseRenderableSeries );
//            this.Initialize();
//        }

//        internal bool IsLicenseValid { get; set; }

//        protected internal virtual bool IsPartOfExtendedFeatures
//        {
//            get
//            {
//                return false;
//            }
//        }

//        public IServiceContainer Services { get; set; }

//        public double ZeroLineY
//        {
//            get
//            {
//                return ( double ) this.GetValue( BaseRenderableSeries.ZeroLineYProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.ZeroLineYProperty, ( object ) value );
//            }
//        }

//        public new bool IsVisible
//        {
//            get
//            {
//                return ( bool ) this.GetValue( BaseRenderableSeries.IsVisibleProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.IsVisibleProperty, ( object ) value );
//            }
//        }

//        public int StrokeThickness
//        {
//            get
//            {
//                return ( int ) this.GetValue( BaseRenderableSeries.StrokeThicknessProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.StrokeThicknessProperty, ( object ) value );
//            }
//        }

//        [Obsolete( "The ResamplingResolution property is no longer used. Please remove from your code", true )]
//        public int ResamplingResolution { get; set; }

//        public IPaletteProvider PaletteProvider
//        {
//            get
//            {
//                return ( IPaletteProvider ) this.GetValue( BaseRenderableSeries.PaletteProviderProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.PaletteProviderProperty, ( object ) value );
//            }
//        }

//        public FrameworkElement RolloverMarker
//        {
//            get
//            {
//                return this._rolloverMarkerCache;
//            }
//        }

//        public ControlTemplate PointMarkerTemplate
//        {
//            get
//            {
//                return ( ControlTemplate ) this.GetValue( BaseRenderableSeries.PointMarkerTemplateProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.PointMarkerTemplateProperty, ( object ) value );
//            }
//        }

//        public IPointMarker PointMarker
//        {
//            get
//            {
//                return ( IPointMarker ) this.GetValue( BaseRenderableSeries.PointMarkerProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.PointMarkerProperty, ( object ) value );
//            }
//        }

//        public ControlTemplate RolloverMarkerTemplate
//        {
//            get
//            {
//                return ( ControlTemplate ) this.GetValue( BaseRenderableSeries.RolloverMarkerTemplateProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.RolloverMarkerTemplateProperty, ( object ) value );
//            }
//        }

//        public DataTemplate LegendMarkerTemplate
//        {
//            get
//            {
//                return ( DataTemplate ) this.GetValue( BaseRenderableSeries.LegendMarkerTemplateProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.LegendMarkerTemplateProperty, ( object ) value );
//            }
//        }

//        public string YAxisId
//        {
//            get
//            {
//                return ( string ) this.GetValue( BaseRenderableSeries.YAxisIdProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.YAxisIdProperty, ( object ) value );
//            }
//        }

//        public string XAxisId
//        {
//            get
//            {
//                return ( string ) this.GetValue( BaseRenderableSeries.XAxisIdProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.XAxisIdProperty, ( object ) value );
//            }
//        }

//        public bool AntiAliasing
//        {
//            get
//            {
//                return ( bool ) this.GetValue( BaseRenderableSeries.AntiAliasingProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.AntiAliasingProperty, ( object ) value );
//            }
//        }

//        public ResamplingMode ResamplingMode
//        {
//            get
//            {
//                return ( ResamplingMode ) this.GetValue( BaseRenderableSeries.ResamplingModeProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.ResamplingModeProperty, ( object ) value );
//            }
//        }

//        public virtual object PointSeriesArg
//        {
//            get
//            {
//                return ( object ) null;
//            }
//        }

//        public Color SeriesColor
//        {
//            get
//            {
//                return ( Color ) this.GetValue( BaseRenderableSeries.SeriesColorProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.SeriesColorProperty, ( object ) value );
//            }
//        }

//        public Style SelectedSeriesStyle
//        {
//            get
//            {
//                return ( Style ) this.GetValue( BaseRenderableSeries.SelectedSeriesStyleProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.SelectedSeriesStyleProperty, ( object ) value );
//            }
//        }

//        public bool IsSelected
//        {
//            get
//            {
//                return ( bool ) this.GetValue( BaseRenderableSeries.IsSelectedProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.IsSelectedProperty, ( object ) value );
//            }
//        }

//        public IDataSeries DataSeries
//        {
//            get
//            {
//                return ( IDataSeries ) this.GetValue( BaseRenderableSeries.DataSeriesProperty );
//            }
//            set
//            {
//                this.ThreadSafeSetValue( BaseRenderableSeries.DataSeriesProperty, ( object ) value );
//            }
//        }

//        public LineDrawMode DrawNaNAs
//        {
//            get
//            {
//                return ( LineDrawMode ) this.GetValue( BaseRenderableSeries.DrawNaNAsProperty );
//            }
//            set
//            {
//                this.SetValue( BaseRenderableSeries.DrawNaNAsProperty, ( object ) value );
//            }
//        }

//        public IRenderPassData CurrentRenderPassData { get; set; }

//        public IAxis XAxis
//        {
//            get
//            {
//                return this._xAxis;
//            }
//            set
//            {
//                this._xAxis = BaseRenderableSeries.SetAndNotifyAxes( this._xAxis, value );
//            }
//        }

//        public IAxis YAxis
//        {
//            get
//            {
//                return this._yAxis;
//            }
//            set
//            {
//                this._yAxis = BaseRenderableSeries.SetAndNotifyAxes( this._yAxis, value );
//            }
//        }

//        private static IAxis SetAndNotifyAxes( IAxis currentAxis, IAxis newAxis )
//        {
//            if ( currentAxis != newAxis )
//            {
//                IAxis target = currentAxis;
//                currentAxis = newAxis;
//                AxisBase.NotifyDataRangeChanged( target );
//                AxisBase.NotifyDataRangeChanged( currentAxis );
//            }
//            return currentAxis;
//        }

//        public virtual bool DisplaysDataAsXy { get; private set; }

//        internal virtual bool IsValidForDrawing
//        {
//            get
//            {
//                if ( this.GetIsValidForDrawing() )
//                {
//                    return this.IsLicenseValid;
//                }

//                return false;
//            }
//        }

//        protected virtual bool GetIsValidForDrawing( )
//        {
//            return this.DataSeries != null && this.DataSeries.HasValues && ( this.IsVisible && this.CurrentRenderPassData != null ) && this.CurrentRenderPassData.PointSeries != null;
//        }

//        protected IPointMarker GetPointMarker( )
//        {
//            return this.PointMarker ?? this._pointMarkerFromTemplate;
//        }

//        public virtual void OnInvalidateParentSurface( )
//        {
//            if ( this.Services == null )
//            {
//                return;
//            }

//            this.Services.GetService<ISciChartSurface>().InvalidateElement();
//        }

//        protected virtual void OnSeriesColorChanged( )
//        {
//        }

//        protected virtual void OnDataSeriesDependencyPropertyChanged( )
//        {
//        }

//        protected void AssertDataPointType<TSeriesPoint>( string dataSeriesType ) where TSeriesPoint : ISeriesPoint<double>
//        {
//            if ( this.CurrentRenderPassData.PointSeries != null && this.CurrentRenderPassData.PointSeries.Count != 0 && !( this.CurrentRenderPassData.PointSeries[ 0 ] is GenericPoint2D<TSeriesPoint> ) )
//            {
//                throw new InvalidOperationException( string.Format( "{0} is expecting data passed as {1}. Please use dataseries type {2}", ( object ) this.GetType(), ( object ) typeof( TSeriesPoint ), ( object ) dataSeriesType ) );
//            }
//        }

//        void IDrawable.OnDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
//        {
//            this.CurrentRenderPassData = renderPassData;
//            if ( !this.IsValidForDrawing )
//            {
//                return;
//            }

//            if ( this._lastViewportSize != renderContext.ViewportSize )
//            {
//                this.OnParentSurfaceViewportSizeChanged();
//                this._lastViewportSize = renderContext.ViewportSize;
//            }
//            if ( renderContext.ViewportSize.IsEmpty || renderContext.ViewportSize == new Size( 1.0, 1.0 ) )
//            {
//                UltrachartDebugLogger.Instance.WriteLine( "Aborting {0}.Draw() as ViewportSize is (1,1)", ( object ) this.GetType().Name );
//            }
//            else
//            {
//                Stopwatch stopwatch = new Stopwatch();
//                stopwatch.Start();
//                this.InternalDraw( renderContext, renderPassData );
//                stopwatch.Stop();
//                UltrachartDebugLogger.Instance.WriteLine( "{0} DrawTime: {1}ms", ( object ) this.GetType().Name, ( object ) stopwatch.ElapsedMilliseconds );
//            }
//        }

//        protected abstract void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData );

//        public int GetDatapointWidth( ICoordinateCalculator<double> xCoordinateCalculator, IPointSeries pointSeries, double widthFraction )
//        {
//            return this.GetDatapointWidth( xCoordinateCalculator, pointSeries, ( double ) pointSeries.Count, widthFraction );
//        }

//        public int GetDatapointWidth( ICoordinateCalculator<double> xCoordinateCalculator, IPointSeries pointSeries, double barsAmount, double widthFraction )
//        {
//            if ( widthFraction < 0.0 || widthFraction > 1.0 )
//            {
//                throw new ArgumentException( "WidthFraction should be between 0.0 and 1.0 inclusive", nameof( widthFraction ) );
//            }

//            double num = xCoordinateCalculator.IsHorizontalAxisCalculator ? this._lastViewportSize.Width : this._lastViewportSize.Height;
//            if ( barsAmount > 1.0 )
//            {
//                num = Math.Abs( xCoordinateCalculator.GetCoordinate( pointSeries[ pointSeries.Count - 1 ].X ) - xCoordinateCalculator.GetCoordinate( pointSeries[ 0 ].X ) ) / ( barsAmount - 1.0 );
//            }

//            return ( int ) ( num * widthFraction );
//        }

//        protected virtual double GetYZeroCoord( )
//        {
//            double dataValue = (double) this.GetValue(BaseRenderableSeries.ZeroLineYProperty);
//            return this.CurrentRenderPassData.IsVerticalChart ? Math.Min( this._lastViewportSize.Width + 1.0, this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate( dataValue ) ) : Math.Min( this._lastViewportSize.Height + 1.0, this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate( dataValue ) );
//        }

//        public HitTestInfo HitTest( Point rawPoint, bool interpolate = false )
//        {
//            return this.HitTest( rawPoint, 7.07, interpolate );
//        }

//        public HitTestInfo VerticalSliceHitTest( Point rawPoint, bool interpolate = false )
//        {
//            this._useXCoordOnlyForHitTest = true;
//            HitTestInfo hitTestInfo = this.HitTest(rawPoint, 0.0, interpolate);
//            this._useXCoordOnlyForHitTest = false;
//            return hitTestInfo;
//        }

//        public virtual HitTestInfo HitTest( Point rawPoint, double hitTestRadius, bool interpolate = false )
//        {
//            HitTestInfo hitTestInfo = HitTestInfo.Empty;
//            if ( this.CurrentRenderPassData != null && this.DataSeries != null && this.DataSeries.HasValues )
//            {
//                double hitTestRadius1 = hitTestRadius + (double) this.StrokeThickness / 2.0;
//                ITransformationStrategy transformationStrategy = this.CurrentRenderPassData.TransformationStrategy;
//                rawPoint = transformationStrategy.Transform( rawPoint );
//                hitTestInfo = this.HitTestInternal( rawPoint, hitTestRadius1, interpolate );
//                hitTestInfo.HitTestPoint = transformationStrategy.ReverseTransform( hitTestInfo.HitTestPoint );
//                hitTestInfo.Y1HitTestPoint = transformationStrategy.ReverseTransform( hitTestInfo.Y1HitTestPoint );
//            }
//            return hitTestInfo;
//        }

//        protected virtual HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
//        {
//            SearchMode searchMode = interpolate ? SearchMode.RoundDown : SearchMode.Nearest;
//            HitTestInfo nearestHitResult = this.NearestHitResult(rawPoint, hitTestRadius, searchMode, !this._useXCoordOnlyForHitTest);
//            if ( interpolate )
//            {
//                nearestHitResult = this.InterpolatePoint( rawPoint, nearestHitResult, hitTestRadius );
//            }

//            return nearestHitResult;
//        }

//        protected double GetHitTestRadiusConsideringPointMarkerSize( double hitTestRadius )
//        {
//            IPointMarker pointMarker = this.GetPointMarker();
//            return pointMarker != null && !pointMarker.Height.IsNaN() && !pointMarker.Width.IsNaN() ? Math.Max( pointMarker.Width, pointMarker.Height ) / 2.0 + hitTestRadius : hitTestRadius;
//        }

//        protected virtual HitTestInfo NearestHitResult( Point mouseRawPoint, double hitTestRadiusInPixels, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation )
//        {
//            if ( double.IsNaN( hitTestRadiusInPixels ) )
//            {
//                throw new ArgumentException( "hitTestRadiusInPixels is NAN" );
//            }

//            Tuple<IComparable, IComparable> hitDataValue = this.GetHitDataValue(mouseRawPoint);
//            Point point = this.TransformPoint(mouseRawPoint, this.CurrentRenderPassData.IsVerticalChart);
//            double num1 = Math.Abs(this.CurrentRenderPassData.XCoordinateCalculator.GetDataValue(point.X + 1.0) - this.CurrentRenderPassData.XCoordinateCalculator.GetDataValue(point.X));
//            double num2 = Math.Abs(this.CurrentRenderPassData.YCoordinateCalculator.GetDataValue(point.Y + 1.0) - this.CurrentRenderPassData.YCoordinateCalculator.GetDataValue(point.Y));
//            double xyScaleRatio = considerYCoordinateForDistanceCalculation ? num1 / num2 : 0.0;
//            IDataSeries dataSeries = this.DataSeries;
//            if ( dataSeries.Count < 2 )
//            {
//                searchMode = SearchMode.Nearest;
//            }

//            int nearestDataPointIndex;
//            if ( searchMode != SearchMode.Nearest )
//            {
//                if ( searchMode != SearchMode.RoundDown )
//                {
//                    throw new NotImplementedException();
//                }

//                nearestDataPointIndex = dataSeries.FindClosestLine( hitDataValue.Item1, hitDataValue.Item2, xyScaleRatio, num1 * hitTestRadiusInPixels, this.DrawNaNAs );
//            }
//            else
//            {
//                if ( hitTestRadiusInPixels.CompareTo( 0.0 ) == 0 && !this.DataSeries.IsSorted )
//                {
//                    hitTestRadiusInPixels = 7.07;
//                }

//                nearestDataPointIndex = dataSeries.FindClosestPoint( hitDataValue.Item1, hitDataValue.Item2, xyScaleRatio, num1 * hitTestRadiusInPixels );
//            }
//            return nearestDataPointIndex == -1 || !( ( IComparable ) dataSeries.YValues[ nearestDataPointIndex ] ).IsDefined() ? HitTestInfo.Empty : this.GetHitTestInfo( nearestDataPointIndex, mouseRawPoint, hitTestRadiusInPixels, hitDataValue.Item1 );
//        }

//        protected Tuple<IComparable, IComparable> GetHitDataValue( Point rawPoint )
//        {
//            rawPoint = this.TransformPoint( rawPoint, this.CurrentRenderPassData.IsVerticalChart );
//            ICoordinateCalculator<double> xcoordinateCalculator = this.CurrentRenderPassData.XCoordinateCalculator;
//            ICoordinateCalculator<double> ycoordinateCalculator = this.CurrentRenderPassData.YCoordinateCalculator;
//            double dataValue = xcoordinateCalculator.GetDataValue(rawPoint.X);
//            IComparable c = ComparableUtil.FromDouble(dataValue, this.DataSeries.XValues[0].GetType());
//            ICategoryCoordinateCalculator coordinateCalculator = xcoordinateCalculator as ICategoryCoordinateCalculator;
//            if ( coordinateCalculator != null )
//            {
//                int index1 = (int) dataValue;
//                c = ( IComparable ) coordinateCalculator.TransformIndexToData( index1 );
//                double coordinate1 = coordinateCalculator.GetCoordinate((double) index1);
//                int index2 = coordinate1 <= rawPoint.X ? Math.Min(index1 + 1, this.DataSeries.XValues.Count - 1) : Math.Max(index1 - 1, 0);
//                if ( index1 != index2 )
//                {
//                    double val1 = coordinateCalculator.TransformIndexToData(index2).ToDouble();
//                    double coordinate2 = coordinateCalculator.GetCoordinate((double) index2);
//                    double num = Math.Abs((rawPoint.X - Math.Min(coordinate1, coordinate2)) / (coordinate2 - coordinate1));
//                    c = ( IComparable ) new DateTime( ( long ) ( Math.Min( val1, c.ToDouble() ) + Math.Abs( val1 - c.ToDouble() ) * num ) );
//                }
//            }
//            IComparable comparable = ComparableUtil.FromDouble(ycoordinateCalculator.GetDataValue(rawPoint.Y), this.DataSeries.YValues[0].GetType());
//            return new Tuple<IComparable, IComparable>( c, comparable );
//        }

//        protected virtual HitTestInfo ToHitTestInfoImpl( int nearestDataPointIndex )
//        {
//            return this.DataSeries.ToHitTestInfo( nearestDataPointIndex );
//        }

//        protected HitTestInfo GetHitTestInfo( int nearestDataPointIndex, Point rawPoint, double hitTestRadius, IComparable hitXValue )
//        {
//            HitTestInfo hitTestInfoImpl = this.ToHitTestInfoImpl(nearestDataPointIndex);
//            lock ( this.DataSeries.SyncRoot )
//            {
//                hitTestInfoImpl.IsWithinDataBounds = !this.DataSeries.IsSorted || this.DataSeries.HasValues && hitXValue.CompareTo( this.DataSeries.XValues[ 0 ] ) >= 0 && hitXValue.CompareTo( this.DataSeries.XValues[ this.DataSeries.XValues.Count - 1 ] ) <= 0;
//            }

//            ICategoryCoordinateCalculator xcoordinateCalculator = this.CurrentRenderPassData.XCoordinateCalculator as ICategoryCoordinateCalculator;
//            double x = xcoordinateCalculator != null ? xcoordinateCalculator.GetCoordinate((double) nearestDataPointIndex) : this.CurrentRenderPassData.XCoordinateCalculator.GetCoordinate(hitTestInfoImpl.XValue.ToDouble());
//            double coordinate = this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate(hitTestInfoImpl.YValue.ToDouble());
//            Point point = new Point(x, coordinate);
//            rawPoint = this.TransformPoint( rawPoint, this.CurrentRenderPassData.IsVerticalChart );
//            hitTestInfoImpl.HitTestPoint = hitTestInfoImpl.Y1HitTestPoint = this.TransformPoint( point, this.CurrentRenderPassData.IsVerticalChart );
//            hitTestInfoImpl.IsVerticalHit = ( hitTestInfoImpl.IsWithinDataBounds |= Math.Abs( x - rawPoint.X ) < hitTestRadius );
//            double num = this.XAxis == null || !this.XAxis.IsPolarAxis ? PointUtil.Distance(point, rawPoint) : PointUtil.PolarDistance(point, rawPoint);
//            hitTestInfoImpl.IsHit = num < hitTestRadius;
//            return hitTestInfoImpl;
//        }

//        protected virtual HitTestInfo InterpolatePoint( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius )
//        {
//            if ( !nearestHitResult.IsEmpty() )
//            {
//                int dataSeriesIndex = nearestHitResult.DataSeriesIndex;
//                int num = nearestHitResult.DataSeriesIndex + 1;
//                if ( num >= 0 && num < this.DataSeries.Count )
//                {
//                    Tuple<double, double> prevAndNextYvalues = this.GetPrevAndNextYValues(dataSeriesIndex, (Func<int, double>) (i => ((IComparable) this.DataSeries.YValues[i]).ToDouble()));
//                    nearestHitResult = this.InterpolatePoint( rawPoint, nearestHitResult, hitTestRadius, prevAndNextYvalues, ( Tuple<double, double> ) null );
//                }
//            }
//            return nearestHitResult;
//        }

//        protected HitTestInfo InterpolatePoint( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, Tuple<double, double> yValues, Tuple<double, double> y1Values = null )
//        {
//            Tuple<IComparable, IComparable> hitDataValue = this.GetHitDataValue(rawPoint);
//            Point rawPoint1 = new Point(hitDataValue.Item1.ToDouble(), hitDataValue.Item2.ToDouble());
//            int index = nearestHitResult.DataSeriesIndex + 1;
//            Point pt1_1 = new Point(nearestHitResult.XValue.ToDouble(), yValues.Item1);
//            Point previousDataPoint = pt1_1;
//            Point point = new Point(((IComparable) this.DataSeries.XValues[index]).ToDouble(), yValues.Item2);
//            Point pt1_2 = new Point();
//            Point pt2 = new Point();
//            if ( y1Values != null )
//            {
//                pt1_2 = new Point( pt1_1.X, y1Values.Item1 );
//                pt2 = new Point( point.X, y1Values.Item2 );
//            }
//            if ( ( this.CurrentRenderPassData.IsVerticalChart ? Math.Abs( rawPoint.Y - nearestHitResult.HitTestPoint.Y ) : Math.Abs( rawPoint.X - nearestHitResult.HitTestPoint.X ) ) >= 2.0 )
//            {
//                pt1_1 = this.InterpolateAtPoint( rawPoint1, pt1_1, point );
//                if ( y1Values != null )
//                {
//                    pt1_2 = this.InterpolateAtPoint( rawPoint1, pt1_2, pt2 );
//                }

//                nearestHitResult.HitTestPoint = this.GetCoordinateForDataPoint( pt1_1.X, pt1_1.Y );
//            }
//            if ( !nearestHitResult.IsHit )
//            {
//                nearestHitResult.IsHit = this.IsHitTest( rawPoint, nearestHitResult, hitTestRadius, previousDataPoint, point );
//            }

//            nearestHitResult.XValue = ComparableUtil.FromDouble( pt1_1.X, this.DataSeries.XValues[ 0 ].GetType() );
//            nearestHitResult.YValue = ComparableUtil.FromDouble( pt1_1.Y, this.DataSeries.YValues[ 0 ].GetType() );
//            if ( y1Values != null )
//            {
//                nearestHitResult.Y1Value = ComparableUtil.FromDouble( pt1_2.Y, this.DataSeries.YValues[ 0 ].GetType() );
//            }

//            return nearestHitResult;
//        }

//        protected Tuple<double, double> GetPrevAndNextYValues( int dataPointIndex, Func<int, double> getYValue )
//        {
//            double num = getYValue(dataPointIndex);
//            ++dataPointIndex;
//            double d = getYValue(dataPointIndex);
//            if ( this.DrawNaNAs == LineDrawMode.ClosedLines )
//            {
//                for ( ; double.IsNaN( d ) && dataPointIndex < this.DataSeries.Count - 1; d = getYValue( dataPointIndex ) )
//                {
//                    ++dataPointIndex;
//                }
//            }
//            return new Tuple<double, double>( num, d );
//        }

//        private Point InterpolateAtPoint( Point rawPoint, Point pt1, Point pt2 )
//        {
//            double x1 = pt1.X;
//            double y1 = pt1.Y;
//            double x2 = pt2.X;
//            double y2 = pt2.Y;
//            NumberUtil.SortedSwap( ref x1, ref x2, ref y1, ref y2 );
//            double num = (rawPoint.X - x1) / (x2 - x1);
//            if ( num > 1.0 )
//            {
//                num = 1.0;
//            }
//            else if ( num < 0.0 )
//            {
//                num = 0.0;
//            }

//            double x3 = x1 + (x2 - x1) * num;
//            if ( !this.HasDigitalLine() )
//            {
//                y1 += ( y2 - y1 ) * num;
//            }

//            return new Point( x3, y1 );
//        }

//        protected Point GetCoordinateForDataPoint( double xDataValue, double yDataValue )
//        {
//            ICoordinateCalculator<double> xcoordinateCalculator = this.CurrentRenderPassData.XCoordinateCalculator;
//            ICoordinateCalculator<double> ycoordinateCalculator = this.CurrentRenderPassData.YCoordinateCalculator;
//            ICategoryCoordinateCalculator coordinateCalculator = xcoordinateCalculator as ICategoryCoordinateCalculator;
//            if ( coordinateCalculator != null )
//            {
//                xDataValue = ( double ) coordinateCalculator.TransformDataToIndex( ( IComparable ) xDataValue );
//            }

//            return this.TransformPoint( new Point( xcoordinateCalculator.GetCoordinate( xDataValue ), ycoordinateCalculator.GetCoordinate( yDataValue ) ), this.CurrentRenderPassData.IsVerticalChart );
//        }

//        protected virtual bool IsHitTest( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, Point previousDataPoint, Point nextDataPoint )
//        {
//            Point coordinateForDataPoint1 = this.GetCoordinateForDataPoint(previousDataPoint.X, previousDataPoint.Y);
//            Point coordinateForDataPoint2 = this.GetCoordinateForDataPoint(nextDataPoint.X, nextDataPoint.Y);
//            bool flag1 = false;
//            bool flag2;
//            if ( this.HasDigitalLine() )
//            {
//                int index = nearestHitResult.DataSeriesIndex - 1;
//                if ( index >= 0 && index < this.DataSeries.Count )
//                {
//                    Point point = new Point(((IComparable) this.DataSeries.XValues[index]).ToDouble(), previousDataPoint.Y);
//                    Point coordinateForDataPoint3 = this.GetCoordinateForDataPoint(point.X, point.Y);
//                    Point start = this.CurrentRenderPassData.IsVerticalChart ? new Point(coordinateForDataPoint3.X, coordinateForDataPoint1.Y) : new Point(coordinateForDataPoint1.X, coordinateForDataPoint3.Y);
//                    flag1 = PointUtil.DistanceFromLine( rawPoint, start, coordinateForDataPoint1, true ) < hitTestRadius;
//                }
//                Point point1 = this.CurrentRenderPassData.IsVerticalChart ? new Point(coordinateForDataPoint1.X, coordinateForDataPoint2.Y) : new Point(coordinateForDataPoint2.X, coordinateForDataPoint1.Y);
//                flag2 = ( ( flag1 ? 1 : 0 ) | ( PointUtil.DistanceFromLine( rawPoint, coordinateForDataPoint1, point1, true ) < hitTestRadius ? 1 : ( PointUtil.DistanceFromLine( rawPoint, point1, coordinateForDataPoint2, true ) < hitTestRadius ? 1 : 0 ) ) ) != 0;
//            }
//            else
//            {
//                flag2 = PointUtil.DistanceFromLine( rawPoint, coordinateForDataPoint1, coordinateForDataPoint2, true ) < hitTestRadius;
//            }

//            return flag2;
//        }

//        protected virtual HitTestInfo HitTestSeriesWithBody( Point rawPoint, HitTestInfo nearestHitPoint, double hitTestRadius )
//        {
//            if ( this.DataSeries != null && this.CurrentRenderPassData != null )
//            {
//                bool isVerticalChart = this.CurrentRenderPassData.IsVerticalChart;
//                double coordinate1 = this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate(this.GetSeriesBodyLowerDataBound(nearestHitPoint));
//                double coordinate2 = this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate(this.GetSeriesBodyUpperDataBound(nearestHitPoint));
//                if ( coordinate2 < coordinate1 )
//                {
//                    NumberUtil.Swap( ref coordinate2, ref coordinate1 );
//                }

//                double num1 = (double) this.StrokeThickness * 0.5 + hitTestRadius;
//                double y1 = coordinate1 - num1;
//                double y2 = coordinate2 + num1;
//                double num2 = this.GetSeriesBodyWidth(nearestHitPoint) * 0.5 + num1;
//                double num3 = isVerticalChart ? nearestHitPoint.HitTestPoint.Y : nearestHitPoint.HitTestPoint.X;
//                Point point1 = new Point(num3 - num2, y2);
//                Point point2 = new Point(num3 + num2, y1);
//                Rect boundaries = new Rect(this.TransformPoint(point1, isVerticalChart), this.TransformPoint(point2, isVerticalChart));
//                nearestHitPoint.IsHit = this.IsBodyHit( rawPoint, boundaries, nearestHitPoint );
//            }
//            return nearestHitPoint;
//        }

//        protected virtual double GetSeriesBodyWidth( HitTestInfo nearestHitPoint )
//        {
//            return 0.0;
//        }

//        protected virtual double GetSeriesBodyLowerDataBound( HitTestInfo nearestHitPoint )
//        {
//            return 0.0;
//        }

//        protected virtual double GetSeriesBodyUpperDataBound( HitTestInfo nearestHitPoint )
//        {
//            return 0.0;
//        }

//        protected virtual bool IsBodyHit( Point pt, Rect boundaries, HitTestInfo nearestHitPoint )
//        {
//            return boundaries.Contains( pt );
//        }

//        protected static bool CheckIsInBounds( double coord, double lowerBound, double upperBound )
//        {
//            if ( lowerBound > upperBound )
//            {
//                NumberUtil.Swap( ref lowerBound, ref upperBound );
//            }

//            if ( coord >= lowerBound )
//            {
//                return coord <= upperBound;
//            }

//            return false;
//        }

//        internal SeriesInfo GetSeriesInfo( Point point )
//        {
//            return this.GetSeriesInfo( this.HitTest( point, false ) );
//        }

//        public virtual SeriesInfo GetSeriesInfo( HitTestInfo hitTestInfo )
//        {
//            return RenderableSeriesExtension.GetSeriesInfo( this, hitTestInfo );
//        }

//        public virtual IRange GetXRange( )
//        {
//            return this.DataSeries.XRange;
//        }

//        public IRange GetYRange( IRange xRange )
//        {
//            return this.GetYRange( xRange, false );
//        }

//        public virtual IRange GetYRange( IRange xRange, bool getPositiveRange )
//        {
//            return this.DataSeries.GetWindowedYRange( xRange, getPositiveRange );
//        }

//        public virtual IndexRange GetExtendedXRange( IndexRange range )
//        {
//            return range;
//        }

//        public bool GetIncludeSeries( Modifier modifier )
//        {
//            bool flag = true;
//            switch ( modifier )
//            {
//                case Modifier.Rollover:
//                    flag = RolloverModifier.GetIncludeSeries( ( DependencyObject ) this );
//                    break;
//                case Modifier.Cursor:
//                    flag = CursorModifier.GetIncludeSeries( ( DependencyObject ) this );
//                    break;
//                case Modifier.Tooltip:
//                    flag = TooltipModifier.GetIncludeSeries( ( DependencyObject ) this );
//                    break;
//                case Modifier.VerticalSlice:
//                    flag = VerticalSliceModifier.GetIncludeSeries( ( DependencyObject ) this );
//                    break;
//            }
//            return flag;
//        }

//        protected virtual void OnResamplingModeChanged( )
//        {
//            this.OnInvalidateParentSurface();
//        }

//        protected virtual void OnParentSurfaceViewportSizeChanged( )
//        {
//        }

//        private void OnSelectionChanged( EventArgs args )
//        {
//            // ISSUE: reference to a compiler-generated field
//            EventHandler selectionChanged = this.SelectionChanged;
//            if ( selectionChanged == null )
//            {
//                return;
//            }

//            selectionChanged( ( object ) this, args );
//        }

//        private void OnVisibilityChanged( EventArgs args )
//        {
//            // ISSUE: reference to a compiler-generated field
//            EventHandler isVisibleChanged = this.IsVisibleChanged;
//            if ( isVisibleChanged == null )
//            {
//                return;
//            }

//            isVisibleChanged( ( object ) this, args );
//        }

//        protected double GetChartRotationAngle( IRenderPassData renderPassData )
//        {
//            return ( renderPassData.IsVerticalChart ? Math.PI / 2.0 : 0.0 ) + ( renderPassData.YCoordinateCalculator.HasFlippedCoordinates ? Math.PI : 0.0 );
//        }

//        protected Point TransformPoint( float x, float y, bool isVerticalChart )
//        {
//            return DrawingHelper.TransformPoint( x, y, isVerticalChart );
//        }

//        protected Point TransformPoint( Point point, bool isVerticalChart )
//        {
//            return DrawingHelper.TransformPoint( point, isVerticalChart );
//        }

//        protected static void OnInvalidateParentSurface( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            ( d as BaseRenderableSeries )?.OnInvalidateParentSurface();
//        }

//        protected virtual void OnDataSeriesDependencyPropertyChanged( IDataSeries oldDataSeries, IDataSeries newDataSeries )
//        {
//            ISciChartSurface parentSurface = this.GetParentSurface();
//            if ( parentSurface != null )
//            {
//                parentSurface.DetachDataSeries( oldDataSeries );
//                parentSurface.AttachDataSeries( newDataSeries );
//            }
//            this._dataSeries = newDataSeries;
//            if ( this.IsVisible )
//            {
//                this.OnDataSeriesDependencyPropertyChanged();
//            }

//            this.OnInvalidateParentSurface();
//        }

//        protected internal ISciChartSurface GetParentSurface( )
//        {
//            if ( this.Services == null )
//            {
//                return ( ISciChartSurface ) null;
//            }

//            return this.Services.GetService<ISciChartSurface>();
//        }

//        protected virtual void CreateRolloverMarker( )
//        {
//            this._rolloverMarkerCache = ( FrameworkElement ) fx.Xaml.Charting.PointMarker.CreateFromTemplate( this.RolloverMarkerTemplate, ( object ) this );
//        }

//        public XmlSchema GetSchema( )
//        {
//            return ( XmlSchema ) null;
//        }

//        public virtual void ReadXml( XmlReader reader )
//        {
//            if ( reader.MoveToContent() != XmlNodeType.Element )
//            {
//                return;
//            }

//            RenderableSeriesSerializationHelper.Instance.DeserializeProperties( ( IRenderableSeries ) this, reader );
//        }

//        public virtual void WriteXml( XmlWriter writer )
//        {
//            RenderableSeriesSerializationHelper.Instance.SerializeProperties( ( IRenderableSeries ) this, writer );
//        }

//        private static void OnStrokeThicknessChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            Guard.Assert( ( IComparable ) ( int ) e.NewValue, "StrokeThickness" ).IsGreaterThanOrEqualTo( ( IComparable ) 0 );
//            BaseRenderableSeries.OnInvalidateParentSurface( d, e );
//        }

//        private static void OnSeriesColorPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            BaseRenderableSeries renderableSeries = d as BaseRenderableSeries;
//            if ( renderableSeries == null )
//            {
//                return;
//            }

//            renderableSeries.OnSeriesColorChanged();
//            renderableSeries.OnInvalidateParentSurface();
//        }

//        private static void OnSelectedSeriesStyleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            BaseRenderableSeries series = d as BaseRenderableSeries;
//            if ( series == null || !series.IsSelected || series.SelectedSeriesStyle == null )
//            {
//                return;
//            }

//            BaseRenderableSeries.ApplyStyle( series );
//        }

//        private static void OnIsSelectedChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            BaseRenderableSeries series = d as BaseRenderableSeries;
//            bool newValue = (bool) e.NewValue;
//            if ( series == null || ( bool ) e.OldValue == newValue )
//            {
//                return;
//            }

//            BaseRenderableSeries.ApplyStyle( series );
//            series.OnSelectionChanged( EventArgs.Empty );
//        }

//        private static void OnIsVisibleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            BaseRenderableSeries renderableSeries = d as BaseRenderableSeries;
//            bool newValue = (bool) e.NewValue;
//            if ( renderableSeries == null || ( bool ) e.OldValue == newValue )
//            {
//                return;
//            }

//            renderableSeries.OnVisibilityChanged( EventArgs.Empty );
//            BaseRenderableSeries.OnInvalidateParentSurface( d, e );
//        }

//        private static void ApplyStyle( BaseRenderableSeries series )
//        {
//            series.SetStyle( series.IsSelected ? series.SelectedSeriesStyle : ( Style ) series.GetValue( RenderableSeriesExtension.SeriesStyleProperty ) );
//            series.OnSeriesColorChanged();
//            series.OnInvalidateParentSurface();
//        }

//        private static void OnResamplingPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            ( d as BaseRenderableSeries )?.OnResamplingModeChanged();
//        }

//        private static void OnRolloverMarkerPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            BaseRenderableSeries renderableSeries = d as BaseRenderableSeries;
//            if ( renderableSeries == null )
//            {
//                return;
//            }

//            renderableSeries.CreateRolloverMarker();
//            renderableSeries.OnInvalidateParentSurface();
//        }

//        private static void OnPointMarkerTemplatePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            BaseRenderableSeries renderableSeries = (BaseRenderableSeries) d;
//            renderableSeries._pointMarkerFromTemplate = renderableSeries.CreatePointMarker( ( ControlTemplate ) e.NewValue, ( object ) renderableSeries );
//            renderableSeries.OnInvalidateParentSurface();
//        }

//        private IPointMarker CreatePointMarker( ControlTemplate template, object dataContext )
//        {
//            IPointMarker pointMarker = (IPointMarker) null;
//            if ( template != null )
//            {
//                BasePointMarker basePointMarker = fx.Xaml.Charting.PointMarker.CreateFromTemplate(template, dataContext).FindVisualChild<BasePointMarker>();
//                if ( basePointMarker == null )
//                {
//                    SpritePointMarker spritePointMarker = new SpritePointMarker();
//                    spritePointMarker.PointMarkerTemplate = template;
//                    spritePointMarker.DataContext = dataContext;
//                    basePointMarker = ( BasePointMarker ) spritePointMarker;
//                }
//                pointMarker = ( IPointMarker ) basePointMarker;
//            }
//            return pointMarker;
//        }

//        private static void OnPointMarkerPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            BaseRenderableSeries renderableSeries = (BaseRenderableSeries) d;
//            BasePointMarker newValue = e.NewValue as BasePointMarker;
//            if ( newValue != null )
//            {
//                newValue.DataContext = ( object ) renderableSeries;
//                BaseRenderableSeries parent = newValue.Parent as BaseRenderableSeries;
//                if ( parent != null )
//                {
//                    parent.Content = ( object ) null;
//                }

//                renderableSeries.Content = ( object ) newValue;
//            }
//            renderableSeries.OnInvalidateParentSurface();
//        }

//        [Obfuscation( Exclude = false, Feature = "encryptmethod" )]
//        private void Initialize( )
//        {
//            new LicenseManager().Validate<BaseRenderableSeries>( this, ( IProviderFactory ) new UltrachartLicenseProviderFactory() );
//        }

//        internal double HitTestRadius
//        {
//            get
//            {
//                return 7.07;
//            }
//        }


//    }
//}

// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.BaseRenderableSeries
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using StockSharp.Xaml.Licensing.Core;

namespace fx.Xaml.Charting
{
    [ContentProperty( "PointMarker" )]
    public abstract class BaseRenderableSeries : ContentControl, IRenderableSeries, IRenderableSeriesBase, IDrawable, IXmlSerializable
    {
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(nameof (StrokeThickness), typeof (int), typeof (BaseRenderableSeries), new PropertyMetadata((object) 1, new PropertyChangedCallback(BaseRenderableSeries.OnStrokeThicknessChanged)));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof (IsSelected), typeof (bool), typeof (BaseRenderableSeries), new PropertyMetadata(new PropertyChangedCallback(BaseRenderableSeries.OnIsSelectedChanged)));
        [Obsolete("We're sorry! The DataSeriesIndex property has been made obsolete. You can now bind RenderableSeries directly to DataSeries via the BaseRenderableSeries.DataSeries DependencyProperty", true)]
        public static readonly DependencyProperty DataSeriesIndexProperty = DependencyProperty.Register("DataSeriesIndex", typeof (int), typeof (BaseRenderableSeries), new PropertyMetadata((object) -1, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty DataSeriesProperty = DependencyProperty.Register(nameof (DataSeries), typeof (IDataSeries), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, (PropertyChangedCallback) ((s, e) => ((BaseRenderableSeries) s).OnDataSeriesDependencyPropertyChanged((IDataSeries) e.OldValue, (IDataSeries) e.NewValue))));
        public new static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(nameof (IsVisible), typeof (bool), typeof (BaseRenderableSeries), new PropertyMetadata((object) true, new PropertyChangedCallback(BaseRenderableSeries.OnIsVisibleChanged)));
        public static readonly DependencyProperty SeriesColorProperty = DependencyProperty.Register(nameof (SeriesColor), typeof (Color), typeof (BaseRenderableSeries), new PropertyMetadata((object) Colors.White, new PropertyChangedCallback(BaseRenderableSeries.OnSeriesColorPropertyChanged)));
        public static readonly DependencyProperty SelectedSeriesStyleProperty = DependencyProperty.Register(nameof (SelectedSeriesStyle), typeof (Style), typeof (BaseRenderableSeries), new PropertyMetadata(new PropertyChangedCallback(BaseRenderableSeries.OnSelectedSeriesStyleChanged)));
        public static readonly DependencyProperty ResamplingModeProperty = DependencyProperty.Register(nameof (ResamplingMode), typeof (ResamplingMode), typeof (BaseRenderableSeries), new PropertyMetadata((object) ResamplingMode.Auto, new PropertyChangedCallback(BaseRenderableSeries.OnResamplingPropertyChanged)));
        public static readonly DependencyProperty AntiAliasingProperty = DependencyProperty.Register(nameof (AntiAliasing), typeof (bool), typeof (BaseRenderableSeries), new PropertyMetadata((object) true, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty PointMarkerTemplateProperty = DependencyProperty.Register(nameof (PointMarkerTemplate), typeof (ControlTemplate), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnPointMarkerTemplatePropertyChanged)));
        public static readonly DependencyProperty PointMarkerProperty = DependencyProperty.Register(nameof (PointMarker), typeof (IPointMarker), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnPointMarkerPropertyChanged)));
        public static readonly DependencyProperty RolloverMarkerTemplateProperty = DependencyProperty.Register(nameof (RolloverMarkerTemplate), typeof (ControlTemplate), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnRolloverMarkerPropertyChanged)));
        public static readonly DependencyProperty LegendMarkerTemplateProperty = DependencyProperty.Register(nameof (LegendMarkerTemplate), typeof (DataTemplate), typeof (BaseRenderableSeries), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(nameof (YAxisId), typeof (string), typeof (BaseRenderableSeries), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(nameof (XAxisId), typeof (string), typeof (BaseRenderableSeries), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty PaletteProviderProperty = DependencyProperty.Register(nameof (PaletteProvider), typeof (IPaletteProvider), typeof (BaseRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty ZeroLineYProperty = DependencyProperty.Register(nameof (ZeroLineY), typeof (double), typeof (BaseRenderableSeries), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty DrawNaNAsProperty = DependencyProperty.Register(nameof (DrawNaNAs), typeof (LineDrawMode), typeof (BaseRenderableSeries), new PropertyMetadata((object) LineDrawMode.Gaps, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        private Size _lastViewportSize = Size.Empty;
        private FrameworkElement _rolloverMarkerCache;
        private IPointMarker _pointMarkerFromTemplate;
        private IDataSeries _dataSeries;
        private bool _useXCoordOnlyForHitTest;
        private IAxis _xAxis;
        private IAxis _yAxis;
        public const double DefaultHitTestRadius = 7.07;
        private const int DefaultDatapointWidth = 70;

        public event EventHandler SelectionChanged;

        public event EventHandler IsVisibleChanged;

        protected BaseRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( BaseRenderableSeries );
            this.Initialize();
        }

        internal bool IsLicenseValid
        {
            get; set;
        }

        protected internal virtual bool IsPartOfExtendedFeatures
        {
            get
            {
                return false;
            }
        }

        public IServiceContainer Services
        {
            get; set;
        }

        public double ZeroLineY
        {
            get
            {
                return ( double ) this.GetValue( BaseRenderableSeries.ZeroLineYProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.ZeroLineYProperty, ( object ) value );
            }
        }

        public new bool IsVisible
        {
            get
            {
                return ( bool ) this.GetValue( BaseRenderableSeries.IsVisibleProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.IsVisibleProperty, ( object ) value );
            }
        }

        public int StrokeThickness
        {
            get
            {
                return ( int ) this.GetValue( BaseRenderableSeries.StrokeThicknessProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.StrokeThicknessProperty, ( object ) value );
            }
        }

        [Obsolete( "The ResamplingResolution property is no longer used. Please remove from your code", true )]
        public int ResamplingResolution
        {
            get; set;
        }

        public IPaletteProvider PaletteProvider
        {
            get
            {
                return ( IPaletteProvider ) this.GetValue( BaseRenderableSeries.PaletteProviderProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.PaletteProviderProperty, ( object ) value );
            }
        }

        public FrameworkElement RolloverMarker
        {
            get
            {
                return this._rolloverMarkerCache;
            }
        }

        public ControlTemplate PointMarkerTemplate
        {
            get
            {
                return ( ControlTemplate ) this.GetValue( BaseRenderableSeries.PointMarkerTemplateProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.PointMarkerTemplateProperty, ( object ) value );
            }
        }

        public IPointMarker PointMarker
        {
            get
            {
                return ( IPointMarker ) this.GetValue( BaseRenderableSeries.PointMarkerProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.PointMarkerProperty, ( object ) value );
            }
        }

        public ControlTemplate RolloverMarkerTemplate
        {
            get
            {
                return ( ControlTemplate ) this.GetValue( BaseRenderableSeries.RolloverMarkerTemplateProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.RolloverMarkerTemplateProperty, ( object ) value );
            }
        }

        public DataTemplate LegendMarkerTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( BaseRenderableSeries.LegendMarkerTemplateProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.LegendMarkerTemplateProperty, ( object ) value );
            }
        }

        public string YAxisId
        {
            get
            {
                return ( string ) this.GetValue( BaseRenderableSeries.YAxisIdProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.YAxisIdProperty, ( object ) value );
            }
        }

        public string XAxisId
        {
            get
            {
                return ( string ) this.GetValue( BaseRenderableSeries.XAxisIdProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.XAxisIdProperty, ( object ) value );
            }
        }

        public bool AntiAliasing
        {
            get
            {
                return ( bool ) this.GetValue( BaseRenderableSeries.AntiAliasingProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.AntiAliasingProperty, ( object ) value );
            }
        }

        public ResamplingMode ResamplingMode
        {
            get
            {
                return ( ResamplingMode ) this.GetValue( BaseRenderableSeries.ResamplingModeProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.ResamplingModeProperty, ( object ) value );
            }
        }

        public virtual object PointSeriesArg
        {
            get
            {
                return ( object ) null;
            }
        }

        public Color SeriesColor
        {
            get
            {
                return ( Color ) this.GetValue( BaseRenderableSeries.SeriesColorProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.SeriesColorProperty, ( object ) value );
            }
        }

        public Style SelectedSeriesStyle
        {
            get
            {
                return ( Style ) this.GetValue( BaseRenderableSeries.SelectedSeriesStyleProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.SelectedSeriesStyleProperty, ( object ) value );
            }
        }

        public bool IsSelected
        {
            get
            {
                return ( bool ) this.GetValue( BaseRenderableSeries.IsSelectedProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.IsSelectedProperty, ( object ) value );
            }
        }

        public IDataSeries DataSeries
        {
            get
            {
                return ( IDataSeries ) this.GetValue( BaseRenderableSeries.DataSeriesProperty );
            }
            set
            {
                this.ThreadSafeSetValue( BaseRenderableSeries.DataSeriesProperty, ( object ) value );
            }
        }

        public LineDrawMode DrawNaNAs
        {
            get
            {
                return ( LineDrawMode ) this.GetValue( BaseRenderableSeries.DrawNaNAsProperty );
            }
            set
            {
                this.SetValue( BaseRenderableSeries.DrawNaNAsProperty, ( object ) value );
            }
        }

        public IRenderPassData CurrentRenderPassData
        {
            get; set;
        }

        public IAxis XAxis
        {
            get
            {
                return this._xAxis;
            }
            set
            {
                this._xAxis = BaseRenderableSeries.SetAndNotifyAxes( this._xAxis, value );
            }
        }

        public IAxis YAxis
        {
            get
            {
                return this._yAxis;
            }
            set
            {
                this._yAxis = BaseRenderableSeries.SetAndNotifyAxes( this._yAxis, value );
            }
        }

        private static IAxis SetAndNotifyAxes( IAxis currentAxis, IAxis newAxis )
        {
            if ( currentAxis != newAxis )
            {
                IAxis target = currentAxis;
                currentAxis = newAxis;
                AxisBase.NotifyDataRangeChanged( target );
                AxisBase.NotifyDataRangeChanged( currentAxis );
            }
            return currentAxis;
        }

        public virtual bool DisplaysDataAsXy
        {
            get; private set;
        }

        internal virtual bool IsValidForDrawing
        {
            get
            {
                if ( this.GetIsValidForDrawing() )
                    return this.IsLicenseValid;
                return false;
            }
        }

        protected virtual bool GetIsValidForDrawing()
        {
            if ( this.DataSeries != null && this.DataSeries.HasValues && ( this.IsVisible && this.CurrentRenderPassData != null ) )
                return this.CurrentRenderPassData.PointSeries != null;
            return false;
        }

        protected IPointMarker GetPointMarker()
        {
            return this.PointMarker ?? this._pointMarkerFromTemplate;
        }

        public virtual void OnInvalidateParentSurface()
        {
            if ( this.Services == null )
                return;
            this.Services.GetService<ISciChartSurface>().InvalidateElement();
        }

        protected virtual void OnSeriesColorChanged()
        {
        }

        protected virtual void OnDataSeriesDependencyPropertyChanged()
        {
        }

        protected void AssertDataPointType<TSeriesPoint>( string dataSeriesType ) where TSeriesPoint : ISeriesPoint<double>
        {
            if ( this.CurrentRenderPassData.PointSeries != null && this.CurrentRenderPassData.PointSeries.Count != 0 && !( this.CurrentRenderPassData.PointSeries[ 0 ] is GenericPoint2D<TSeriesPoint> ) )
                throw new InvalidOperationException( string.Format( "{0} is expecting data passed as {1}. Please use dataseries type {2}", ( object ) this.GetType(), ( object ) typeof( TSeriesPoint ), ( object ) dataSeriesType ) );
        }

        void IDrawable.OnDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            this.CurrentRenderPassData = renderPassData;
            if ( !this.IsValidForDrawing )
                return;
            if ( this._lastViewportSize != renderContext.ViewportSize )
            {
                this.OnParentSurfaceViewportSizeChanged();
                this._lastViewportSize = renderContext.ViewportSize;
            }
            if ( renderContext.ViewportSize.IsEmpty || renderContext.ViewportSize == new Size( 1.0, 1.0 ) )
            {
                UltrachartDebugLogger.Instance.WriteLine( "Aborting {0}.Draw() as ViewportSize is (1,1)", ( object ) this.GetType().Name );
            }
            else
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                this.InternalDraw( renderContext, renderPassData );
                stopwatch.Stop();
                UltrachartDebugLogger.Instance.WriteLine( "{0} DrawTime: {1}ms", ( object ) this.GetType().Name, ( object ) stopwatch.ElapsedMilliseconds );
            }
        }

        protected abstract void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData );

        public int GetDatapointWidth( ICoordinateCalculator<double> xCoordinateCalculator, IPointSeries pointSeries, double widthFraction )
        {
            return this.GetDatapointWidth( xCoordinateCalculator, pointSeries, ( double ) pointSeries.Count, widthFraction );
        }

        public int GetDatapointWidth( ICoordinateCalculator<double> xCoordinateCalculator, IPointSeries pointSeries, double barsAmount, double widthFraction )
        {
            if ( widthFraction < 0.0 || widthFraction > 1.0 )
                throw new ArgumentException( "WidthFraction should be between 0.0 and 1.0 inclusive", nameof( widthFraction ) );
            double num = xCoordinateCalculator.IsHorizontalAxisCalculator ? this._lastViewportSize.Width : this._lastViewportSize.Height;
            if ( barsAmount > 1.0 )
                num = Math.Abs( xCoordinateCalculator.GetCoordinate( pointSeries[ pointSeries.Count - 1 ].X ) - xCoordinateCalculator.GetCoordinate( pointSeries[ 0 ].X ) ) / ( barsAmount - 1.0 );
            return ( int ) ( num * widthFraction );
        }

        protected virtual double GetYZeroCoord()
        {
            double dataValue = (double) this.GetValue(BaseRenderableSeries.ZeroLineYProperty);
            if ( !this.CurrentRenderPassData.IsVerticalChart )
                return Math.Min( this._lastViewportSize.Height + 1.0, this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate( dataValue ) );
            return Math.Min( this._lastViewportSize.Width + 1.0, this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate( dataValue ) );
        }

        public HitTestInfo HitTest( Point rawPoint, bool interpolate = false )
        {
            return this.HitTest( rawPoint, 7.07, interpolate );
        }

        public HitTestInfo VerticalSliceHitTest( Point rawPoint, bool interpolate = false )
        {
            this._useXCoordOnlyForHitTest = true;
            HitTestInfo hitTestInfo = this.HitTest(rawPoint, 0.0, interpolate);
            this._useXCoordOnlyForHitTest = false;
            return hitTestInfo;
        }

        public virtual HitTestInfo HitTest( Point rawPoint, double hitTestRadius, bool interpolate = false )
        {
            HitTestInfo hitTestInfo = HitTestInfo.Empty;
            if ( this.CurrentRenderPassData != null && this.DataSeries != null && this.DataSeries.HasValues )
            {
                double hitTestRadius1 = hitTestRadius + (double) this.StrokeThickness / 2.0;
                ITransformationStrategy transformationStrategy = this.CurrentRenderPassData.TransformationStrategy;
                rawPoint = transformationStrategy.Transform( rawPoint );
                hitTestInfo = this.HitTestInternal( rawPoint, hitTestRadius1, interpolate );
                hitTestInfo.HitTestPoint = transformationStrategy.ReverseTransform( hitTestInfo.HitTestPoint );
                hitTestInfo.Y1HitTestPoint = transformationStrategy.ReverseTransform( hitTestInfo.Y1HitTestPoint );
            }
            return hitTestInfo;
        }

        protected virtual HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            SearchMode searchMode = interpolate ? SearchMode.RoundDown : SearchMode.Nearest;
            HitTestInfo nearestHitResult = this.NearestHitResult(rawPoint, hitTestRadius, searchMode, !this._useXCoordOnlyForHitTest);
            if ( interpolate )
                nearestHitResult = this.InterpolatePoint( rawPoint, nearestHitResult, hitTestRadius );
            return nearestHitResult;
        }

        protected double GetHitTestRadiusConsideringPointMarkerSize( double hitTestRadius )
        {
            IPointMarker pointMarker = this.GetPointMarker();
            if ( ( pointMarker == null || pointMarker.Height.IsNaN() ? 0 : ( !pointMarker.Width.IsNaN() ? 1 : 0 ) ) == 0 )
                return hitTestRadius;
            return Math.Max( pointMarker.Width, pointMarker.Height ) / 2.0 + hitTestRadius;
        }

        protected virtual HitTestInfo NearestHitResult( Point mouseRawPoint, double hitTestRadiusInPixels, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation )
        {
            if ( double.IsNaN( hitTestRadiusInPixels ) )
                throw new ArgumentException( "hitTestRadiusInPixels is NAN" );
            Tuple<IComparable, IComparable> hitDataValue = this.GetHitDataValue(mouseRawPoint);
            Point point = this.TransformPoint(mouseRawPoint, this.CurrentRenderPassData.IsVerticalChart);
            double num1 = Math.Abs(this.CurrentRenderPassData.XCoordinateCalculator.GetDataValue(point.X + 1.0) - this.CurrentRenderPassData.XCoordinateCalculator.GetDataValue(point.X));
            double num2 = Math.Abs(this.CurrentRenderPassData.YCoordinateCalculator.GetDataValue(point.Y + 1.0) - this.CurrentRenderPassData.YCoordinateCalculator.GetDataValue(point.Y));
            double xyScaleRatio = considerYCoordinateForDistanceCalculation ? num1 / num2 : 0.0;
            IDataSeries dataSeries = this.DataSeries;
            if ( dataSeries.Count < 2 )
                searchMode = SearchMode.Nearest;
            int nearestDataPointIndex;
            if ( searchMode != SearchMode.Nearest )
            {
                if ( searchMode != SearchMode.RoundDown )
                    throw new NotImplementedException();
                nearestDataPointIndex = dataSeries.FindClosestLine( hitDataValue.Item1, hitDataValue.Item2, xyScaleRatio, num1 * hitTestRadiusInPixels, this.DrawNaNAs );
            }
            else
            {
                if ( hitTestRadiusInPixels.CompareTo( 0.0 ) == 0 && !this.DataSeries.IsSorted )
                    hitTestRadiusInPixels = 7.07;
                nearestDataPointIndex = dataSeries.FindClosestPoint( hitDataValue.Item1, hitDataValue.Item2, xyScaleRatio, num1 * hitTestRadiusInPixels );
            }
            if ( nearestDataPointIndex == -1 || !( ( IComparable ) dataSeries.YValues[ nearestDataPointIndex ] ).IsDefined() )
                return HitTestInfo.Empty;
            return this.GetHitTestInfo( nearestDataPointIndex, mouseRawPoint, hitTestRadiusInPixels, hitDataValue.Item1 );
        }

        protected Tuple<IComparable, IComparable> GetHitDataValue( Point rawPoint )
        {
            rawPoint = this.TransformPoint( rawPoint, this.CurrentRenderPassData.IsVerticalChart );
            ICoordinateCalculator<double> xcoordinateCalculator = this.CurrentRenderPassData.XCoordinateCalculator;
            ICoordinateCalculator<double> ycoordinateCalculator = this.CurrentRenderPassData.YCoordinateCalculator;
            double dataValue = xcoordinateCalculator.GetDataValue(rawPoint.X);
            IComparable c = ComparableUtil.FromDouble(dataValue, this.DataSeries.XValues[0].GetType());
            ICategoryCoordinateCalculator coordinateCalculator = xcoordinateCalculator as ICategoryCoordinateCalculator;
            if ( coordinateCalculator != null )
            {
                int index1 = (int) dataValue;
                c = ( IComparable ) coordinateCalculator.TransformIndexToData( index1 );
                double coordinate1 = coordinateCalculator.GetCoordinate((double) index1);
                int index2 = coordinate1 <= rawPoint.X ? Math.Min(index1 + 1, this.DataSeries.XValues.Count - 1) : Math.Max(index1 - 1, 0);
                if ( index1 != index2 )
                {
                    double val1 = coordinateCalculator.TransformIndexToData(index2).ToDouble();
                    double coordinate2 = coordinateCalculator.GetCoordinate((double) index2);
                    double num = Math.Abs((rawPoint.X - Math.Min(coordinate1, coordinate2)) / (coordinate2 - coordinate1));
                    c = ( IComparable ) new DateTime( ( long ) ( Math.Min( val1, c.ToDouble() ) + Math.Abs( val1 - c.ToDouble() ) * num ) );
                }
            }
            IComparable comparable = ComparableUtil.FromDouble(ycoordinateCalculator.GetDataValue(rawPoint.Y), this.DataSeries.YValues[0].GetType());
            return new Tuple<IComparable, IComparable>( c, comparable );
        }

        protected virtual HitTestInfo ToHitTestInfoImpl( int nearestDataPointIndex )
        {
            return this.DataSeries.ToHitTestInfo( nearestDataPointIndex );
        }

        protected HitTestInfo GetHitTestInfo( int nearestDataPointIndex, Point rawPoint, double hitTestRadius, IComparable hitXValue )
        {
            HitTestInfo hitTestInfoImpl = this.ToHitTestInfoImpl(nearestDataPointIndex);
            lock ( this.DataSeries.SyncRoot )
                hitTestInfoImpl.IsWithinDataBounds = !this.DataSeries.IsSorted || this.DataSeries.HasValues && hitXValue.CompareTo( this.DataSeries.XValues[ 0 ] ) >= 0 && hitXValue.CompareTo( this.DataSeries.XValues[ this.DataSeries.XValues.Count - 1 ] ) <= 0;
            ICategoryCoordinateCalculator xcoordinateCalculator = this.CurrentRenderPassData.XCoordinateCalculator as ICategoryCoordinateCalculator;
            double x = xcoordinateCalculator != null ? xcoordinateCalculator.GetCoordinate((double) nearestDataPointIndex) : this.CurrentRenderPassData.XCoordinateCalculator.GetCoordinate(hitTestInfoImpl.XValue.ToDouble());
            double coordinate = this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate(hitTestInfoImpl.YValue.ToDouble());
            Point point = new Point(x, coordinate);
            rawPoint = this.TransformPoint( rawPoint, this.CurrentRenderPassData.IsVerticalChart );
            hitTestInfoImpl.HitTestPoint = hitTestInfoImpl.Y1HitTestPoint = this.TransformPoint( point, this.CurrentRenderPassData.IsVerticalChart );
            hitTestInfoImpl.IsVerticalHit = ( hitTestInfoImpl.IsWithinDataBounds |= Math.Abs( x - rawPoint.X ) < hitTestRadius );
            double num = this.XAxis == null || !this.XAxis.IsPolarAxis ? PointUtil.Distance(point, rawPoint) : PointUtil.PolarDistance(point, rawPoint);
            hitTestInfoImpl.IsHit = num < hitTestRadius;
            return hitTestInfoImpl;
        }

        protected virtual HitTestInfo InterpolatePoint( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius )
        {
            if ( !nearestHitResult.IsEmpty() )
            {
                int dataSeriesIndex = nearestHitResult.DataSeriesIndex;
                int num = nearestHitResult.DataSeriesIndex + 1;
                if ( num >= 0 && num < this.DataSeries.Count )
                {
                    Tuple<double, double> prevAndNextYvalues = this.GetPrevAndNextYValues(dataSeriesIndex, (Func<int, double>) (i => ((IComparable) this.DataSeries.YValues[i]).ToDouble()));
                    nearestHitResult = this.InterpolatePoint( rawPoint, nearestHitResult, hitTestRadius, prevAndNextYvalues, ( Tuple<double, double> ) null );
                }
            }
            return nearestHitResult;
        }

        protected HitTestInfo InterpolatePoint( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, Tuple<double, double> yValues, Tuple<double, double> y1Values = null )
        {
            Tuple<IComparable, IComparable> hitDataValue = this.GetHitDataValue(rawPoint);
            Point rawPoint1 = new Point(hitDataValue.Item1.ToDouble(), hitDataValue.Item2.ToDouble());
            int index = nearestHitResult.DataSeriesIndex + 1;
            Point pt1_1 = new Point(nearestHitResult.XValue.ToDouble(), yValues.Item1);
            Point previousDataPoint = pt1_1;
            Point point = new Point(((IComparable) this.DataSeries.XValues[index]).ToDouble(), yValues.Item2);
            Point pt1_2 = new Point();
            Point pt2 = new Point();
            if ( y1Values != null )
            {
                pt1_2 = new Point( pt1_1.X, y1Values.Item1 );
                pt2 = new Point( point.X, y1Values.Item2 );
            }
            if ( ( this.CurrentRenderPassData.IsVerticalChart ? Math.Abs( rawPoint.Y - nearestHitResult.HitTestPoint.Y ) : Math.Abs( rawPoint.X - nearestHitResult.HitTestPoint.X ) ) >= 2.0 )
            {
                pt1_1 = this.InterpolateAtPoint( rawPoint1, pt1_1, point );
                if ( y1Values != null )
                    pt1_2 = this.InterpolateAtPoint( rawPoint1, pt1_2, pt2 );
                nearestHitResult.HitTestPoint = this.GetCoordinateForDataPoint( pt1_1.X, pt1_1.Y );
            }
            if ( !nearestHitResult.IsHit )
                nearestHitResult.IsHit = this.IsHitTest( rawPoint, nearestHitResult, hitTestRadius, previousDataPoint, point );
            nearestHitResult.XValue = ComparableUtil.FromDouble( pt1_1.X, this.DataSeries.XValues[ 0 ].GetType() );
            nearestHitResult.YValue = ComparableUtil.FromDouble( pt1_1.Y, this.DataSeries.YValues[ 0 ].GetType() );
            if ( y1Values != null )
                nearestHitResult.Y1Value = ComparableUtil.FromDouble( pt1_2.Y, this.DataSeries.YValues[ 0 ].GetType() );
            return nearestHitResult;
        }

        protected Tuple<double, double> GetPrevAndNextYValues( int dataPointIndex, Func<int, double> getYValue )
        {
            double num = getYValue(dataPointIndex);
            ++dataPointIndex;
            double d = getYValue(dataPointIndex);
            if ( this.DrawNaNAs == LineDrawMode.ClosedLines )
            {
                for ( ; double.IsNaN( d ) && dataPointIndex < this.DataSeries.Count - 1 ; d = getYValue( dataPointIndex ) )
                    ++dataPointIndex;
            }
            return new Tuple<double, double>( num, d );
        }

        private Point InterpolateAtPoint( Point rawPoint, Point pt1, Point pt2 )
        {
            double x1 = pt1.X;
            double y1 = pt1.Y;
            double x2 = pt2.X;
            double y2 = pt2.Y;
            NumberUtil.SortedSwap( ref x1, ref x2, ref y1, ref y2 );
            double num = (rawPoint.X - x1) / (x2 - x1);
            if ( num > 1.0 )
                num = 1.0;
            else if ( num < 0.0 )
                num = 0.0;
            double x3 = x1 + (x2 - x1) * num;
            if ( !this.HasDigitalLine() )
                y1 += ( y2 - y1 ) * num;
            return new Point( x3, y1 );
        }

        protected Point GetCoordinateForDataPoint( double xDataValue, double yDataValue )
        {
            ICoordinateCalculator<double> xcoordinateCalculator = this.CurrentRenderPassData.XCoordinateCalculator;
            ICoordinateCalculator<double> ycoordinateCalculator = this.CurrentRenderPassData.YCoordinateCalculator;
            ICategoryCoordinateCalculator coordinateCalculator = xcoordinateCalculator as ICategoryCoordinateCalculator;
            if ( coordinateCalculator != null )
                xDataValue = ( double ) coordinateCalculator.TransformDataToIndex( ( IComparable ) xDataValue );
            return this.TransformPoint( new Point( xcoordinateCalculator.GetCoordinate( xDataValue ), ycoordinateCalculator.GetCoordinate( yDataValue ) ), this.CurrentRenderPassData.IsVerticalChart );
        }

        protected virtual bool IsHitTest( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, Point previousDataPoint, Point nextDataPoint )
        {
            Point coordinateForDataPoint1 = this.GetCoordinateForDataPoint(previousDataPoint.X, previousDataPoint.Y);
            Point coordinateForDataPoint2 = this.GetCoordinateForDataPoint(nextDataPoint.X, nextDataPoint.Y);
            bool flag1 = false;
            bool flag2;
            if ( this.HasDigitalLine() )
            {
                int index = nearestHitResult.DataSeriesIndex - 1;
                if ( index >= 0 && index < this.DataSeries.Count )
                {
                    Point point = new Point(((IComparable) this.DataSeries.XValues[index]).ToDouble(), previousDataPoint.Y);
                    Point coordinateForDataPoint3 = this.GetCoordinateForDataPoint(point.X, point.Y);
                    Point start = this.CurrentRenderPassData.IsVerticalChart ? new Point(coordinateForDataPoint3.X, coordinateForDataPoint1.Y) : new Point(coordinateForDataPoint1.X, coordinateForDataPoint3.Y);
                    flag1 = PointUtil.DistanceFromLine( rawPoint, start, coordinateForDataPoint1, true ) < hitTestRadius;
                }
                Point point1 = this.CurrentRenderPassData.IsVerticalChart ? new Point(coordinateForDataPoint1.X, coordinateForDataPoint2.Y) : new Point(coordinateForDataPoint2.X, coordinateForDataPoint1.Y);
                flag2 = ( ( flag1 ? 1 : 0 ) | ( PointUtil.DistanceFromLine( rawPoint, coordinateForDataPoint1, point1, true ) < hitTestRadius ? 1 : ( PointUtil.DistanceFromLine( rawPoint, point1, coordinateForDataPoint2, true ) < hitTestRadius ? 1 : 0 ) ) ) != 0;
            }
            else
                flag2 = PointUtil.DistanceFromLine( rawPoint, coordinateForDataPoint1, coordinateForDataPoint2, true ) < hitTestRadius;
            return flag2;
        }

        protected virtual HitTestInfo HitTestSeriesWithBody( Point rawPoint, HitTestInfo nearestHitPoint, double hitTestRadius )
        {
            if ( this.DataSeries != null && this.CurrentRenderPassData != null )
            {
                bool isVerticalChart = this.CurrentRenderPassData.IsVerticalChart;
                double coordinate1 = this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate(this.GetSeriesBodyLowerDataBound(nearestHitPoint));
                double coordinate2 = this.CurrentRenderPassData.YCoordinateCalculator.GetCoordinate(this.GetSeriesBodyUpperDataBound(nearestHitPoint));
                if ( coordinate2 < coordinate1 )
                    NumberUtil.Swap( ref coordinate2, ref coordinate1 );
                double num1 = (double) this.StrokeThickness * 0.5 + hitTestRadius;
                double y1 = coordinate1 - num1;
                double y2 = coordinate2 + num1;
                double num2 = this.GetSeriesBodyWidth(nearestHitPoint) * 0.5 + num1;
                double num3 = isVerticalChart ? nearestHitPoint.HitTestPoint.Y : nearestHitPoint.HitTestPoint.X;
                Point point1 = new Point(num3 - num2, y2);
                Point point2 = new Point(num3 + num2, y1);
                Rect boundaries = new Rect(this.TransformPoint(point1, isVerticalChart), this.TransformPoint(point2, isVerticalChart));
                nearestHitPoint.IsHit = this.IsBodyHit( rawPoint, boundaries, nearestHitPoint );
            }
            return nearestHitPoint;
        }

        protected virtual double GetSeriesBodyWidth( HitTestInfo nearestHitPoint )
        {
            return 0.0;
        }

        protected virtual double GetSeriesBodyLowerDataBound( HitTestInfo nearestHitPoint )
        {
            return 0.0;
        }

        protected virtual double GetSeriesBodyUpperDataBound( HitTestInfo nearestHitPoint )
        {
            return 0.0;
        }

        protected virtual bool IsBodyHit( Point pt, Rect boundaries, HitTestInfo nearestHitPoint )
        {
            return boundaries.Contains( pt );
        }

        protected static bool CheckIsInBounds( double coord, double lowerBound, double upperBound )
        {
            if ( lowerBound > upperBound )
                NumberUtil.Swap( ref lowerBound, ref upperBound );
            if ( coord >= lowerBound )
                return coord <= upperBound;
            return false;
        }

        internal SeriesInfo GetSeriesInfo( Point point )
        {
            return this.GetSeriesInfo( this.HitTest( point, false ) );
        }

        public virtual SeriesInfo GetSeriesInfo( HitTestInfo hitTestInfo )
        {
            return RenderableSeriesExtension.GetSeriesInfo( this, hitTestInfo );
        }

        public virtual IRange GetXRange()
        {
            return this.DataSeries.XRange;
        }

        public IRange GetYRange( IRange xRange )
        {
            return this.GetYRange( xRange, false );
        }

        public virtual IRange GetYRange( IRange xRange, bool getPositiveRange )
        {
            return this.DataSeries.GetWindowedYRange( xRange, getPositiveRange );
        }

        public virtual IndexRange GetExtendedXRange( IndexRange range )
        {
            return range;
        }

        public bool GetIncludeSeries( Modifier modifier )
        {
            bool flag = true;
            switch ( modifier )
            {
                case Modifier.Rollover:
                    flag = RolloverModifier.GetIncludeSeries( ( DependencyObject ) this );
                    break;
                case Modifier.Cursor:
                    flag = CursorModifier.GetIncludeSeries( ( DependencyObject ) this );
                    break;
                case Modifier.Tooltip:
                    flag = TooltipModifier.GetIncludeSeries( ( DependencyObject ) this );
                    break;
                case Modifier.VerticalSlice:
                    flag = VerticalSliceModifier.GetIncludeSeries( ( DependencyObject ) this );
                    break;
            }
            return flag;
        }

        protected virtual void OnResamplingModeChanged()
        {
            this.OnInvalidateParentSurface();
        }

        protected virtual void OnParentSurfaceViewportSizeChanged()
        {
        }

        private void OnSelectionChanged( EventArgs args )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler selectionChanged = this.SelectionChanged;
            if ( selectionChanged == null )
                return;
            selectionChanged( ( object ) this, args );
        }

        private void OnVisibilityChanged( EventArgs args )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler isVisibleChanged = this.IsVisibleChanged;
            if ( isVisibleChanged == null )
                return;
            isVisibleChanged( ( object ) this, args );
        }

        protected double GetChartRotationAngle( IRenderPassData renderPassData )
        {
            return ( renderPassData.IsVerticalChart ? Math.PI / 2.0 : 0.0 ) + ( renderPassData.YCoordinateCalculator.HasFlippedCoordinates ? Math.PI : 0.0 );
        }

        protected Point TransformPoint( float x, float y, bool isVerticalChart )
        {
            return DrawingHelper.TransformPoint( x, y, isVerticalChart );
        }

        protected Point TransformPoint( Point point, bool isVerticalChart )
        {
            return DrawingHelper.TransformPoint( point, isVerticalChart );
        }

        protected static void OnInvalidateParentSurface( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as BaseRenderableSeries )?.OnInvalidateParentSurface();
        }

        protected virtual void OnDataSeriesDependencyPropertyChanged( IDataSeries oldDataSeries, IDataSeries newDataSeries )
        {
            ISciChartSurface parentSurface = this.GetParentSurface();
            if ( parentSurface != null )
            {
                parentSurface.DetachDataSeries( oldDataSeries );
                parentSurface.AttachDataSeries( newDataSeries );
            }
            this._dataSeries = newDataSeries;
            if ( this.IsVisible )
                this.OnDataSeriesDependencyPropertyChanged();
            this.OnInvalidateParentSurface();
        }

        protected internal ISciChartSurface GetParentSurface()
        {
            if ( this.Services == null )
                return ( ISciChartSurface ) null;
            return this.Services.GetService<ISciChartSurface>();
        }

        protected virtual void CreateRolloverMarker()
        {
            this._rolloverMarkerCache = ( FrameworkElement ) fx.Xaml.Charting.PointMarker.CreateFromTemplate( this.RolloverMarkerTemplate, ( object ) this );
        }

        public XmlSchema GetSchema()
        {
            return ( XmlSchema ) null;
        }

        public virtual void ReadXml( XmlReader reader )
        {
            if ( reader.MoveToContent() != XmlNodeType.Element )
                return;
            RenderableSeriesSerializationHelper.Instance.DeserializeProperties( ( IRenderableSeries ) this, reader );
        }

        public virtual void WriteXml( XmlWriter writer )
        {
            RenderableSeriesSerializationHelper.Instance.SerializeProperties( ( IRenderableSeries ) this, writer );
        }

        private static void OnStrokeThicknessChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Guard.Assert( ( IComparable ) ( int ) e.NewValue, "StrokeThickness" ).IsGreaterThanOrEqualTo( ( IComparable ) 0 );
            BaseRenderableSeries.OnInvalidateParentSurface( d, e );
        }

        private static void OnSeriesColorPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseRenderableSeries renderableSeries = d as BaseRenderableSeries;
            if ( renderableSeries == null )
                return;
            renderableSeries.OnSeriesColorChanged();
            renderableSeries.OnInvalidateParentSurface();
        }

        private static void OnSelectedSeriesStyleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseRenderableSeries series = d as BaseRenderableSeries;
            if ( series == null || !series.IsSelected || series.SelectedSeriesStyle == null )
                return;
            BaseRenderableSeries.ApplyStyle( series );
        }

        private static void OnIsSelectedChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseRenderableSeries series = d as BaseRenderableSeries;
            bool newValue = (bool) e.NewValue;
            if ( series == null || ( bool ) e.OldValue == newValue )
                return;
            BaseRenderableSeries.ApplyStyle( series );
            series.OnSelectionChanged( EventArgs.Empty );
        }

        private static void OnIsVisibleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseRenderableSeries renderableSeries = d as BaseRenderableSeries;
            bool newValue = (bool) e.NewValue;
            if ( renderableSeries == null || ( bool ) e.OldValue == newValue )
                return;
            renderableSeries.OnVisibilityChanged( EventArgs.Empty );
            BaseRenderableSeries.OnInvalidateParentSurface( d, e );
        }

        private static void ApplyStyle( BaseRenderableSeries series )
        {
            series.SetStyle( series.IsSelected ? series.SelectedSeriesStyle : ( Style ) series.GetValue( RenderableSeriesExtension.SeriesStyleProperty ) );
            series.OnSeriesColorChanged();
            series.OnInvalidateParentSurface();
        }

        private static void OnResamplingPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as BaseRenderableSeries )?.OnResamplingModeChanged();
        }

        private static void OnRolloverMarkerPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseRenderableSeries renderableSeries = d as BaseRenderableSeries;
            if ( renderableSeries == null )
                return;
            renderableSeries.CreateRolloverMarker();
            renderableSeries.OnInvalidateParentSurface();
        }

        private static void OnPointMarkerTemplatePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseRenderableSeries renderableSeries = (BaseRenderableSeries) d;
            renderableSeries._pointMarkerFromTemplate = renderableSeries.CreatePointMarker( ( ControlTemplate ) e.NewValue, ( object ) renderableSeries );
            renderableSeries.OnInvalidateParentSurface();
        }

        private IPointMarker CreatePointMarker( ControlTemplate template, object dataContext )
        {
            IPointMarker pointMarker = (IPointMarker) null;
            if ( template != null )
            {
                BasePointMarker basePointMarker = fx.Xaml.Charting.PointMarker.CreateFromTemplate(template, dataContext).FindVisualChild<BasePointMarker>();
                if ( basePointMarker == null )
                {
                    SpritePointMarker spritePointMarker = new SpritePointMarker();
                    spritePointMarker.PointMarkerTemplate = template;
                    spritePointMarker.DataContext = dataContext;
                    basePointMarker = ( BasePointMarker ) spritePointMarker;
                }
                pointMarker = ( IPointMarker ) basePointMarker;
            }
            return pointMarker;
        }

        private static void OnPointMarkerPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseRenderableSeries renderableSeries = (BaseRenderableSeries) d;
            BasePointMarker newValue = e.NewValue as BasePointMarker;
            if ( newValue != null )
            {
                newValue.DataContext = ( object ) renderableSeries;
                BaseRenderableSeries parent = newValue.Parent as BaseRenderableSeries;
                if ( parent != null )
                    parent.Content = ( object ) null;
                renderableSeries.Content = ( object ) newValue;
            }
            renderableSeries.OnInvalidateParentSurface();
        }

        [Obfuscation( Exclude = false, Feature = "encryptmethod" )]
        private void Initialize()
        {
            new LicenseManager().Validate<BaseRenderableSeries>( this, ( IProviderFactory ) new UltrachartLicenseProviderFactory() );
        }

        internal double HitTestRadius
        {
            get
            {
                return 7.07;
            }
        }

        [SpecialName]
        Style IRenderableSeries.Style
        {
            get
            {
                return this.Style;
            }

            set
            {
                this.Style = value;
            }
        }


        [SpecialName]
        object IRenderableSeries.DataContext
        {
            get
            {
                return this.DataContext;
            }

            set
            {
                this.DataContext = value;
            }

        }
    }
}
