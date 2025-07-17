// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.FastHeatMapRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastHeatMapRenderableSeries : BaseRenderableSeries, INotifyPropertyChanged
    {
        public static readonly DependencyProperty DrawTextInCellProperty = DependencyProperty.Register(nameof (DrawTextInCell), typeof (bool), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) false));
        public static readonly DependencyProperty CellTextForegroundProperty = DependencyProperty.Register(nameof (CellTextForeground), typeof (Color), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) Colors.White));
        public static readonly DependencyProperty CellFontSizeProperty = DependencyProperty.Register(nameof (CellFontSize), typeof (float), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) 12f));
        public static readonly DependencyProperty ColorMapProperty = DependencyProperty.Register(nameof (ColorMap), typeof (LinearGradientBrush), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) FastHeatMapRenderableSeries.DefaultColorMap));
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(nameof (Minimum), typeof (double), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(FastHeatMapRenderableSeries.OnMinimumMaximumPropertyChanged)));
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof (Maximum), typeof (double), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) 1.0, new PropertyChangedCallback(FastHeatMapRenderableSeries.OnMinimumMaximumPropertyChanged)));
        private const double DefaultMaximum = 1.0;
        private const double DefaultMinimum = 0.0;
        private const float DefaultCellFontSize = 12f;

        public event PropertyChangedEventHandler PropertyChanged;

        public FastHeatMapRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( FastHeatMapRenderableSeries );
        }

        private static LinearGradientBrush DefaultColorMap
        {
            get
            {
                return new LinearGradientBrush( new GradientStopCollection() { new GradientStop() { Color = Colors.Blue, Offset = 0.0 }, new GradientStop() { Color = Colors.Red, Offset = 1.0 } }, 0.0 );
            }
        }

        public LinearGradientBrush ColorMap
        {
            get
            {
                return ( LinearGradientBrush ) this.GetValue( FastHeatMapRenderableSeries.ColorMapProperty );
            }
            set
            {
                this.SetValue( FastHeatMapRenderableSeries.ColorMapProperty, ( object ) value );
            }
        }

        public double Minimum
        {
            get
            {
                return ( double ) this.GetValue( FastHeatMapRenderableSeries.MinimumProperty );
            }
            set
            {
                this.SetValue( FastHeatMapRenderableSeries.MinimumProperty, ( object ) value );
            }
        }

        public double Maximum
        {
            get
            {
                return ( double ) this.GetValue( FastHeatMapRenderableSeries.MaximumProperty );
            }
            set
            {
                this.SetValue( FastHeatMapRenderableSeries.MaximumProperty, ( object ) value );
            }
        }

        public bool DrawTextInCell
        {
            get
            {
                return ( bool ) this.GetValue( FastHeatMapRenderableSeries.DrawTextInCellProperty );
            }
            set
            {
                this.SetValue( FastHeatMapRenderableSeries.DrawTextInCellProperty, ( object ) value );
            }
        }

        public Color CellTextForeground
        {
            get
            {
                return ( Color ) this.GetValue( FastHeatMapRenderableSeries.CellTextForegroundProperty );
            }
            set
            {
                this.SetValue( FastHeatMapRenderableSeries.CellTextForegroundProperty, ( object ) value );
            }
        }

        public float CellFontSize
        {
            get
            {
                return ( float ) this.GetValue( FastHeatMapRenderableSeries.CellFontSizeProperty );
            }
            set
            {
                this.SetValue( FastHeatMapRenderableSeries.CellFontSizeProperty, ( object ) value );
            }
        }

        private DoubleToColorMappingSettings MappingSettings
        {
            get
            {
                return new DoubleToColorMappingSettings() { GradientStops = this.ColorMap.GradientStops.ToArray<GradientStop>(), Minimum = this.Minimum, ScaleFactor = 1.0 / Math.Abs( this.Maximum - this.Minimum ) };
            }
        }

        public double MiddleValue
        {
            get
            {
                return ( this.Minimum + this.Maximum ) * 0.5;
            }
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            HitTestInfo hitTestInfo = HitTestInfo.Empty;
            IHeatmap2DArrayDataSeries dataSeries = this.DataSeries as IHeatmap2DArrayDataSeries;
            if ( dataSeries != null )
            {
                double dataValue1 = this.CurrentRenderPassData.XCoordinateCalculator.GetDataValue(rawPoint.X);
                double dataValue2 = this.CurrentRenderPassData.YCoordinateCalculator.GetDataValue(rawPoint.Y);
                hitTestInfo = dataSeries.ToHitTestInfo( dataValue1, dataValue2, interpolate );
                hitTestInfo.HitTestPoint = rawPoint;
            }
            return hitTestInfo;
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            if ( renderPassData.IsVerticalChart )
                throw new NotImplementedException( string.Format( "We are sorry! The vertical chart feature is not supported by the {0} currently.", ( object ) this.GetType().Name ) );
            DoubleToColorMappingSettings mappingSettings = this.MappingSettings;
            bool drawTextInCell = this.DrawTextInCell;
            Color cellTextForeground = this.CellTextForeground;
            float cellFontSize = this.CellFontSize;
            Size viewportSize = renderContext.ViewportSize;
            double height = viewportSize.Height;
            viewportSize = renderContext.ViewportSize;
            int width = (int) viewportSize.Width;
            IPointSeries pointSeries = this.CurrentRenderPassData.PointSeries;
            bool flippedCoordinates1 = this.CurrentRenderPassData.XCoordinateCalculator.HasFlippedCoordinates;
            bool flippedCoordinates2 = this.CurrentRenderPassData.YCoordinateCalculator.HasFlippedCoordinates;
            int count = pointSeries.Count;
            double opacity = this.Opacity;
            for ( int index1 = 0 ; index1 < count ; ++index1 )
            {
                I2DArraySegment obj = (I2DArraySegment) pointSeries[index1];
                double x1 = renderPassData.XCoordinateCalculator.GetCoordinate(obj.XValueAtLeft);
                double num1 = renderPassData.XCoordinateCalculator.GetCoordinate(obj.XValueAtRight);
                if ( ( x1 >= 0.0 || num1 >= 0.0 ) && ( x1 <= ( double ) width || num1 <= ( double ) width ) )
                {
                    if ( x1 < 0.0 )
                        x1 = 0.0;
                    if ( num1 > ( double ) width )
                        num1 = ( double ) width;
                    int coordinate1 = (int) renderPassData.YCoordinateCalculator.GetCoordinate(obj.YValueAtBottom);
                    int coordinate2 = (int) renderPassData.YCoordinateCalculator.GetCoordinate(obj.YValueAtTop);
                    IList<int> verticalPixelsArgb = obj.GetVerticalPixelsArgb(mappingSettings);
                    if ( flippedCoordinates1 )
                        NumberUtil.Swap( ref x1, ref num1 );
                    for ( int x2 = ( int ) x1 ; ( double ) x2 < num1 ; ++x2 )
                    {
                        if ( x2 >= 0 && x2 < width )
                            renderContext.DrawPixelsVertically( x2, coordinate1, coordinate2, verticalPixelsArgb, opacity, flippedCoordinates2 );
                    }
                    if ( drawTextInCell )
                    {
                        IList<double> verticalPixelValues = obj.GetVerticalPixelValues();
                        int num2 = (coordinate1 - coordinate2) / verticalPixelValues.Count;
                        if ( flippedCoordinates2 )
                            num2 *= -1;
                        for ( int index2 = 0 ; index2 < verticalPixelValues.Count ; ++index2 )
                        {
                            if ( num2 > 0 && num1 > x1 )
                            {
                                int num3 = verticalPixelValues.Count - 1 - index2;
                                int num4 = coordinate2 + (coordinate1 - coordinate2) * (flippedCoordinates2 ? index2 + 1 : num3) / verticalPixelValues.Count;
                                if ( ( double ) num4 <= height && num4 + num2 >= 0 )
                                {
                                    double cellValue = verticalPixelValues[flippedCoordinates2 ? num3 : index2];
                                    renderContext.DrawText( this.FormatCellToString( cellValue ), new Rect( x1, ( double ) num4, num1 - x1, ( double ) num2 ), AlignmentX.Center, AlignmentY.Center, cellTextForeground, cellFontSize, ( string ) null, new FontWeight() );
                                }
                            }
                        }
                    }
                }
            }
        }

        protected virtual string FormatCellToString( double cellValue )
        {
            return cellValue.ToString( "N2" );
        }

        private void OnPropertyChanged( string propertyName )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged( ( object ) this, new PropertyChangedEventArgs( propertyName ) );
        }

        private static void OnMinimumMaximumPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as FastHeatMapRenderableSeries )?.OnPropertyChanged( "MiddleValue" );
        }
    }
}
