// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.FastColumnRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class FastColumnRenderableSeries : BaseColumnRenderableSeries
    {
        public FastColumnRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( FastColumnRenderableSeries );
        }

        protected override bool GetIsValidForDrawing()
        {
            if ( !base.GetIsValidForDrawing() )
                return false;
            if ( this.SeriesColor.A == ( byte ) 0 || this.StrokeThickness <= 0 )
                return this.FillBrush != null;
            return true;
        }

        public override IRange GetYRange( IRange xRange, bool getPositiveRange )
        {
            IRange yrange = base.GetYRange(xRange, getPositiveRange);
            double zeroLineY = this.ZeroLineY;
            if ( getPositiveRange && zeroLineY <= 0.0 )
                zeroLineY = yrange.Min.ToDouble();
            return RangeFactory.NewRange( ( IComparable ) Math.Min( yrange.Min.ToDouble(), zeroLineY ), ( IComparable ) Math.Max( yrange.Max.ToDouble(), zeroLineY ) );
        }

        protected override double GetSeriesBodyLowerDataBound( HitTestInfo nearestHitPoint )
        {
            IComparable yvalue = (IComparable) this.DataSeries.YValues[nearestHitPoint.DataSeriesIndex];
            if ( yvalue.ToDouble().CompareTo( this.ZeroLineY ) <= 0 )
                return yvalue.ToDouble();
            return this.ZeroLineY;
        }

        protected override double GetSeriesBodyUpperDataBound( HitTestInfo nearestHitPoint )
        {
            IComparable yvalue = (IComparable) this.DataSeries.YValues[nearestHitPoint.DataSeriesIndex];
            if ( yvalue.ToDouble().CompareTo( this.ZeroLineY ) <= 0 )
                return this.ZeroLineY;
            return yvalue.ToDouble();
        }

        protected override double GetColumnWidth( IPointSeries points, IRenderPassData renderPassData )
        {
            return ( double ) this.GetDatapointWidth( renderPassData.XCoordinateCalculator, points, this.DataPointWidth );
        }

        public override IRange GetXRange()
        {
            IRange xrange = base.GetXRange();
            if ( !xrange.IsDefined )
                return ( IRange ) DoubleRange.UndefinedRange;
            int count = this.DataSeries.Count;
            DoubleRange doubleRange = xrange.AsDoubleRange();
            double num = count > 1 ? doubleRange.Diff / (double) (count - 1) / 2.0 * this.DataPointWidth : this.DataPointWidth / 2.0;
            doubleRange.Max += num;
            doubleRange.Min -= num;
            return xrange;
        }
    }
}
