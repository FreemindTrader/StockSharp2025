//// Decompiled with JetBrains decompiler
//// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.StackedMountainRenderableSeries
//// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

//using StockSharp.Xaml.Charting.Common.Extensions;
//using StockSharp.Xaml.Charting.Licensing;
//using StockSharp.Xaml.Charting.Model.DataSeries;
//using StockSharp.Xaml.Charting.Rendering.Common;
//using StockSharp.Xaml.Charting.Visuals.PointMarkers;
//using StockSharp.Xaml.Licensing.Core;
//using System;
//using System.Runtime.CompilerServices;
//using System.Windows;
//using System.Windows.Media;
//using System.Xml.Serialization;

//namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
//{
//    
//    public class StackedMountainRenderableSeries : BaseMountainRenderableSeries, IStackedMountainRenderableSeries, IStackedRenderableSeries, IRenderableSeries, IRenderableSeriesBase, IDrawable, IXmlSerializable
//    {
//        public static readonly DependencyProperty StackedGroupIdProperty = DependencyProperty.Register(nameof (StackedGroupId), typeof (string), typeof (StackedMountainRenderableSeries), new PropertyMetadata((object) "DefaultStackedGroupId", new PropertyChangedCallback(StackedMountainRenderableSeries.StackedGroupIdPropertyChanged)));
//        public static readonly DependencyProperty IsOneHundredPercentProperty = DependencyProperty.Register(nameof (IsOneHundredPercent), typeof (bool), typeof (StackedMountainRenderableSeries), new PropertyMetadata((object) false));

//        public StackedMountainRenderableSeries( )
//        {
//            DefaultStyleKey = ( object ) typeof( StackedMountainRenderableSeries );
//        }

//        public IStackedMountainsWrapper Wrapper
//        {
//            get
//            {
//                return ( ( UltrachartSurface ) GetParentSurface() )?.StackedMountainsWrapper;
//            }
//        }

//        public string StackedGroupId
//        {
//            get
//            {
//                return ( string ) GetValue( StackedMountainRenderableSeries.StackedGroupIdProperty );
//            }
//            set
//            {
//                SetValue( StackedMountainRenderableSeries.StackedGroupIdProperty, ( object ) value );
//            }
//        }

//        public bool IsOneHundredPercent
//        {
//            get
//            {
//                return ( bool ) GetValue( StackedMountainRenderableSeries.IsOneHundredPercentProperty );
//            }
//            set
//            {
//                SetValue( StackedMountainRenderableSeries.IsOneHundredPercentProperty, ( object ) value );
//                GetParentSurface()?.InvalidateElement();
//            }
//        }

//        public override IRange GetYRange( IRange xRange, bool getPositiveRange )
//        {
//            if ( xRange == null )
//            {
//                throw new ArgumentNullException( nameof( xRange ) );
//            }

//            return ( IRange ) Wrapper.CalculateYRange( ( IStackedMountainRenderableSeries ) this, DataSeries.GetIndicesRange( xRange ) );
//        }

//        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
//        {
//            Wrapper.DrawStackedSeries( renderContext );
//        }

//        void IStackedMountainRenderableSeries.DrawMountain( IRenderContext2D renderContext, bool isPreviousSeriesDigital )
//        {
//            IRenderPassData currentRenderPassData = CurrentRenderPassData;
//            double chartRotationAngle = GetChartRotationAngle(CurrentRenderPassData);
//            using ( IBrush2D brush = renderContext.CreateBrush( AreaBrush, Opacity, TextureMappingMode.PerPrimitive ) )
//            {
//                using ( IPen2D pen = renderContext.CreatePen( SeriesColor, AntiAliasing, ( float ) StrokeThickness, Opacity, ( double[ ] ) null, PenLineCap.Round ) )
//                {
//                    IPointSeries linePointSeries;
//                    Point2DSeries point2Dseries = FillWithPoint2DSeries(isPreviousSeriesDigital, out linePointSeries);
//                    IPathContextFactory linesPathFactory = SeriesDrawingHelpersFactory.GetLinesPathFactory(renderContext, currentRenderPassData);
//                    FastLinesHelper.IterateLines( SeriesDrawingHelpersFactory.GetStackedMountainAreaPathFactory( renderContext, currentRenderPassData, chartRotationAngle ), brush, ( IPointSeries ) point2Dseries, currentRenderPassData.XCoordinateCalculator, currentRenderPassData.YCoordinateCalculator, false, false );
//                    FastLinesHelper.IterateLines( linesPathFactory, pen, linePointSeries, currentRenderPassData.XCoordinateCalculator, currentRenderPassData.YCoordinateCalculator, IsDigitalLine, false );
//                    IPointMarker pointMarker = GetPointMarker();
//                    if ( pointMarker == null )
//                    {
//                        return;
//                    }

//                    FastPointsHelper.IteratePoints( SeriesDrawingHelpersFactory.GetPointMarkerPathFactory( renderContext, currentRenderPassData, pointMarker ), linePointSeries, currentRenderPassData.XCoordinateCalculator, currentRenderPassData.YCoordinateCalculator );
//                }
//            }
//        }

//        private Point2DSeries FillWithPoint2DSeries( bool isPreviousSeriesDigital, out IPointSeries linePointSeries )
//        {
//            IRenderPassData currentRenderPassData = CurrentRenderPassData;
//            int count = currentRenderPassData.PointSeries.Count;
//            int num1 = IsDigitalLine ? count * 2 - 1 : count;
//            int num2 = isPreviousSeriesDigital ? count * 2 - 1 : count;
//            int num3 = num1 + num2;
//            Point2DSeries point2Dseries1 = new Point2DSeries(num3);
//            point2Dseries1.XValues.SetCount( num3 );
//            point2Dseries1.YValues.SetCount( num3 );
//            Point2DSeries point2Dseries2 = new Point2DSeries(num1);
//            point2Dseries2.XValues.SetCount( num1 );
//            point2Dseries2.YValues.SetCount( num1 );
//            int index1 = 0;
//            int index2 = num3 - 1;
//            for ( int index3 = 0; index3 < count; ++index3 )
//            {
//                IPoint point = currentRenderPassData.PointSeries[index3];
//                Tuple<double, double> tuple = Wrapper.AccumulateYValueAtX((IStackedMountainRenderableSeries) this, index3, true);
//                if ( IsDigitalLine && index3 != 0 )
//                {
//                    point2Dseries2.XValues[ index1 ] = point2Dseries1.XValues[ index1 ] = point.X;
//                    point2Dseries2.YValues[ index1 ] = point2Dseries1.YValues[ index1 ] = point2Dseries1.YValues[ index1 - 1 ];
//                    ++index1;
//                }
//                point2Dseries2.XValues[ index1 ] = point2Dseries1.XValues[ index1 ] = point.X;
//                point2Dseries2.YValues[ index1 ] = point2Dseries1.YValues[ index1 ] = tuple.Item1;
//                ++index1;
//                if ( isPreviousSeriesDigital && index3 != 0 )
//                {
//                    point2Dseries1.XValues[ index2 ] = point.X;
//                    point2Dseries1.YValues[ index2 ] = point2Dseries1.YValues[ index2 + 1 ];
//                    --index2;
//                }
//                point2Dseries1.XValues[ index2 ] = point.X;
//                point2Dseries1.YValues[ index2 ] = tuple.Item2;
//                --index2;
//            }
//            linePointSeries = ( IPointSeries ) point2Dseries2;
//            return point2Dseries1;
//        }

//        protected override HitTestInfo NearestHitResult( Point rawPoint, double hitTestRadius, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation )
//        {
//            HitTestInfo hitTestInfo = HitTestInfo.Empty;
//            if ( IsVisible )
//            {
//                HitTestInfo nearestHitResult = base.NearestHitResult(rawPoint, hitTestRadius, searchMode, considerYCoordinateForDistanceCalculation);
//                hitTestInfo = Wrapper.ShiftHitTestInfo( rawPoint, nearestHitResult, hitTestRadius, ( IStackedMountainRenderableSeries ) this );
//            }
//            return hitTestInfo;
//        }

//        protected override HitTestInfo InterpolatePoint( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius )
//        {
//            if ( !nearestHitResult.IsEmpty() )
//            {
//                int dataSeriesIndex = nearestHitResult.DataSeriesIndex;
//                int num = nearestHitResult.DataSeriesIndex + 1;
//                if ( num >= 0 && num < DataSeries.Count )
//                {
//                    Tuple<double, double> prevAndNextYvalues1 = GetPrevAndNextYValues(dataSeriesIndex, (Func<int, double>) (i => ((IComparable) DataSeries.YValues[i]).ToDouble()));
//                    Tuple<double, double> prevAndNextYvalues2 = GetPrevAndNextYValues(dataSeriesIndex, (Func<int, double>) (i => Wrapper.AccumulateYValueAtX((IStackedMountainRenderableSeries) this, i, false).Item1));
//                    nearestHitResult = InterpolatePoint( rawPoint, nearestHitResult, hitTestRadius, prevAndNextYvalues2, prevAndNextYvalues1 );
//                }
//            }
//            return nearestHitResult;
//        }

//        protected override bool IsHitTest( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, Point previousDataPoint, Point nextDataPoint )
//        {
//            bool flag = base.IsHitTest(rawPoint, nearestHitResult, hitTestRadius, previousDataPoint, nextDataPoint);
//            Tuple<IComparable, IComparable> hitDataValue = GetHitDataValue(rawPoint);
//            if ( !flag )
//            {
//                flag = Wrapper.IsHitTest( rawPoint, nearestHitResult, hitTestRadius, hitDataValue, ( IStackedMountainRenderableSeries ) this );
//            }

//            return flag;
//        }

//        private static void StackedGroupIdPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            StackedMountainRenderableSeries renderableSeries = d as StackedMountainRenderableSeries;
//            if ( renderableSeries == null || renderableSeries.Wrapper == null )
//            {
//                return;
//            }

//            renderableSeries.Wrapper.MoveSeriesToAnotherGroup( ( IStackedMountainRenderableSeries ) renderableSeries, ( string ) e.OldValue, ( string ) e.NewValue );
//        }


//    }
//}

// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.StackedMountainRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Visuals.PointMarkers;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class StackedMountainRenderableSeries : BaseMountainRenderableSeries, IStackedMountainRenderableSeries, IStackedRenderableSeries, IRenderableSeries, IRenderableSeriesBase, IDrawable, IXmlSerializable
    {
        public static readonly DependencyProperty StackedGroupIdProperty = DependencyProperty.Register(nameof (StackedGroupId), typeof (string), typeof (StackedMountainRenderableSeries), new PropertyMetadata((object) "DefaultStackedGroupId", new PropertyChangedCallback(StackedMountainRenderableSeries.StackedGroupIdPropertyChanged)));
        public static readonly DependencyProperty IsOneHundredPercentProperty = DependencyProperty.Register(nameof (IsOneHundredPercent), typeof (bool), typeof (StackedMountainRenderableSeries), new PropertyMetadata((object) false));

        public StackedMountainRenderableSeries()
        {
            DefaultStyleKey = ( object ) typeof( StackedMountainRenderableSeries );
        }

        public IStackedMountainsWrapper Wrapper
        {
            get
            {
                return ( ( UltrachartSurface ) GetParentSurface() )?.StackedMountainsWrapper;
            }
        }

        public string StackedGroupId
        {
            get
            {
                return ( string ) GetValue( StackedMountainRenderableSeries.StackedGroupIdProperty );
            }
            set
            {
                SetValue( StackedMountainRenderableSeries.StackedGroupIdProperty, ( object ) value );
            }
        }

        public bool IsOneHundredPercent
        {
            get
            {
                return ( bool ) GetValue( StackedMountainRenderableSeries.IsOneHundredPercentProperty );
            }
            set
            {
                SetValue( StackedMountainRenderableSeries.IsOneHundredPercentProperty, ( object ) value );
                GetParentSurface()?.InvalidateElement();
            }
        }

        public override IRange GetYRange( IRange xRange, bool getPositiveRange )
        {
            if ( xRange == null )
                throw new ArgumentNullException( nameof( xRange ) );
            return ( IRange ) Wrapper.CalculateYRange( ( IStackedMountainRenderableSeries ) this, DataSeries.GetIndicesRange( xRange ) );
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            Wrapper.DrawStackedSeries( renderContext );
        }

        void IStackedMountainRenderableSeries.DrawMountain( IRenderContext2D renderContext, bool isPreviousSeriesDigital )
        {
            IRenderPassData currentRenderPassData = CurrentRenderPassData;
            double chartRotationAngle = GetChartRotationAngle(CurrentRenderPassData);
            using ( IBrush2D brush = renderContext.CreateBrush( AreaBrush, Opacity, TextureMappingMode.PerPrimitive ) )
            {
                using ( IPen2D pen1 = renderContext.CreatePen( SeriesColor, AntiAliasing, ( float ) StrokeThickness, Opacity, ( double[ ] ) null, PenLineCap.Round ) )
                {
                    IPointSeries linePointSeries;
                    Point2DSeries point2Dseries = FillWithPoint2DSeries(isPreviousSeriesDigital, out linePointSeries);
                    IPathContextFactory linesPathFactory = SeriesDrawingHelpersFactory.GetLinesPathFactory(renderContext, currentRenderPassData);
                    FastLinesHelper.IterateLines( SeriesDrawingHelpersFactory.GetStackedMountainAreaPathFactory( renderContext, currentRenderPassData, chartRotationAngle ), brush, ( IPointSeries ) point2Dseries, currentRenderPassData.XCoordinateCalculator, currentRenderPassData.YCoordinateCalculator, false, false );
                    IPen2D pen2 = pen1;
                    IPointSeries pointSeries = linePointSeries;
                    ICoordinateCalculator<double> xcoordinateCalculator = currentRenderPassData.XCoordinateCalculator;
                    ICoordinateCalculator<double> ycoordinateCalculator = currentRenderPassData.YCoordinateCalculator;
                    int num1 = IsDigitalLine ? 1 : 0;
                    int num2 = 0;
                    FastLinesHelper.IterateLines( linesPathFactory, pen2, pointSeries, xcoordinateCalculator, ycoordinateCalculator, num1 != 0, num2 != 0 );
                    IPointMarker pointMarker = GetPointMarker();
                    if ( pointMarker == null )
                        return;
                    FastPointsHelper.IteratePoints( SeriesDrawingHelpersFactory.GetPointMarkerPathFactory( renderContext, currentRenderPassData, pointMarker ), linePointSeries, currentRenderPassData.XCoordinateCalculator, currentRenderPassData.YCoordinateCalculator );
                }
            }
        }

        private Point2DSeries FillWithPoint2DSeries( bool isPreviousSeriesDigital, out IPointSeries linePointSeries )
        {
            IRenderPassData currentRenderPassData = CurrentRenderPassData;
            int count = currentRenderPassData.PointSeries.Count;
            int num1 = IsDigitalLine ? count * 2 - 1 : count;
            int num2 = isPreviousSeriesDigital ? count * 2 - 1 : count;
            int num3 = num1 + num2;
            Point2DSeries point2Dseries1 = new Point2DSeries(num3);
            point2Dseries1.XValues.SetCount( num3 );
            point2Dseries1.YValues.SetCount( num3 );
            Point2DSeries point2Dseries2 = new Point2DSeries(num1);
            point2Dseries2.XValues.SetCount( num1 );
            point2Dseries2.YValues.SetCount( num1 );
            int index1 = 0;
            int index2 = num3 - 1;
            for ( int index3 = 0 ; index3 < count ; ++index3 )
            {
                IPoint point = currentRenderPassData.PointSeries[index3];
                Tuple<double, double> tuple = Wrapper.AccumulateYValueAtX((IStackedMountainRenderableSeries) this, index3, true);
                if ( IsDigitalLine && index3 != 0 )
                {
                    point2Dseries2.XValues[ index1 ] = point2Dseries1.XValues[ index1 ] = point.X;
                    point2Dseries2.YValues[ index1 ] = point2Dseries1.YValues[ index1 ] = point2Dseries1.YValues[ index1 - 1 ];
                    ++index1;
                }
                point2Dseries2.XValues[ index1 ] = point2Dseries1.XValues[ index1 ] = point.X;
                point2Dseries2.YValues[ index1 ] = point2Dseries1.YValues[ index1 ] = tuple.Item1;
                ++index1;
                if ( isPreviousSeriesDigital && index3 != 0 )
                {
                    point2Dseries1.XValues[ index2 ] = point.X;
                    point2Dseries1.YValues[ index2 ] = point2Dseries1.YValues[ index2 + 1 ];
                    --index2;
                }
                point2Dseries1.XValues[ index2 ] = point.X;
                point2Dseries1.YValues[ index2 ] = tuple.Item2;
                --index2;
            }
            linePointSeries = ( IPointSeries ) point2Dseries2;
            return point2Dseries1;
        }

        protected override HitTestInfo NearestHitResult( Point rawPoint, double hitTestRadius, SearchMode searchMode, bool considerYCoordinateForDistanceCalculation )
        {
            HitTestInfo hitTestInfo = HitTestInfo.Empty;
            if ( IsVisible )
            {
                HitTestInfo nearestHitResult = base.NearestHitResult(rawPoint, hitTestRadius, searchMode, considerYCoordinateForDistanceCalculation);
                hitTestInfo = Wrapper.ShiftHitTestInfo( rawPoint, nearestHitResult, hitTestRadius, ( IStackedMountainRenderableSeries ) this );
            }
            return hitTestInfo;
        }

        protected override HitTestInfo InterpolatePoint( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius )
        {
            if ( !nearestHitResult.IsEmpty() )
            {
                int dataSeriesIndex = nearestHitResult.DataSeriesIndex;
                int num = nearestHitResult.DataSeriesIndex + 1;
                if ( num >= 0 && num < DataSeries.Count )
                {
                    Tuple<double, double> prevAndNextYvalues1 = GetPrevAndNextYValues(dataSeriesIndex, (Func<int, double>) (i => ((IComparable) DataSeries.YValues[i]).ToDouble()));
                    Tuple<double, double> prevAndNextYvalues2 = GetPrevAndNextYValues(dataSeriesIndex, (Func<int, double>) (i => Wrapper.AccumulateYValueAtX((IStackedMountainRenderableSeries) this, i, false).Item1));
                    nearestHitResult = InterpolatePoint( rawPoint, nearestHitResult, hitTestRadius, prevAndNextYvalues2, prevAndNextYvalues1 );
                }
            }
            return nearestHitResult;
        }

        protected override bool IsHitTest( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, Point previousDataPoint, Point nextDataPoint )
        {
            bool flag = base.IsHitTest(rawPoint, nearestHitResult, hitTestRadius, previousDataPoint, nextDataPoint);
            Tuple<IComparable, IComparable> hitDataValue = GetHitDataValue(rawPoint);
            if ( !flag )
                flag = Wrapper.IsHitTest( rawPoint, nearestHitResult, hitTestRadius, hitDataValue, ( IStackedMountainRenderableSeries ) this );
            return flag;
        }

        private static void StackedGroupIdPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            StackedMountainRenderableSeries renderableSeries = d as StackedMountainRenderableSeries;
            if ( renderableSeries == null || renderableSeries.Wrapper == null )
                return;
            renderableSeries.Wrapper.MoveSeriesToAnotherGroup( ( IStackedMountainRenderableSeries ) renderableSeries, ( string ) e.OldValue, ( string ) e.NewValue );
        }

        [SpecialName]
        Style IRenderableSeries.Style
        {
            get
            {
                return Style;
            }

            set
            {
                Style = value;
            }
        }


        [SpecialName]
        object IRenderableSeries.DataContext
        {
            get
            {
                return DataContext;
            }

            set
            {
                DataContext = value;
            }

        }
    }
}

