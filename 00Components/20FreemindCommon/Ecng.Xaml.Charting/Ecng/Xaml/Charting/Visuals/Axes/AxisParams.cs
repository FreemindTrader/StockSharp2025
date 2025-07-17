// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.AxisParams
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections;
using StockSharp.Xaml.Charting.Model.DataSeries;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public struct AxisParams
    {
        private const double EPSILON = 4.94065645841247E-324;
        internal bool FlipCoordinates;
        internal double Size;
        internal double Offset;
        internal double VisibleMax;
        internal double VisibleMin;
        internal bool IsPolarAxis;
        internal bool IsCategoryAxis;
        internal bool IsLogarithmicAxis;
        internal double LogarithmicBase;
        internal bool IsXAxis;
        internal bool IsHorizontal;
        internal bool IsBaseXValuesSorted;
        internal IList BaseXValues;
        public IPointSeries CategoryPointSeries;
        public IndexRange PointRange;
        internal double DataPointPixelSize;
        internal double DataPointStep;

        public override bool Equals( object obj )
        {
            if ( obj == null || obj.GetType() != typeof( AxisParams ) )
                return false;
            return this.Equals( ( AxisParams ) obj );
        }

        public bool Equals( AxisParams other )
        {
            if ( other.Size.Equals( this.Size ) && other.VisibleMin.Equals( this.VisibleMin ) && ( other.VisibleMax.Equals( this.VisibleMax ) && other.IsCategoryAxis.Equals( this.IsCategoryAxis ) ) && ( other.IsLogarithmicAxis.Equals( this.IsLogarithmicAxis ) && object.Equals( ( object ) other.CategoryPointSeries, ( object ) this.CategoryPointSeries ) && ( other.IsHorizontal.Equals( this.IsHorizontal ) && other.FlipCoordinates.Equals( this.FlipCoordinates ) ) ) && ( object.Equals( ( object ) other.BaseXValues, ( object ) this.BaseXValues ) && object.Equals( ( object ) other.PointRange, ( object ) this.PointRange ) && ( other.DataPointPixelSize.Equals( this.DataPointPixelSize ) && other.DataPointStep.Equals( this.DataPointStep ) ) && ( other.LogarithmicBase.Equals( this.LogarithmicBase ) && other.IsBaseXValuesSorted.Equals( this.IsBaseXValuesSorted ) ) ) )
                return other.IsXAxis.Equals( this.IsXAxis );
            return false;
        }

        public override int GetHashCode()
        {
            return ( ( ( ( ( ( ( ( ( ( ( ( this.Size.GetHashCode() * 397 ^ this.VisibleMin.GetHashCode() ) * 397 ^ this.VisibleMax.GetHashCode() ) * 397 ^ this.IsXAxis.GetHashCode() ) * 397 ^ this.IsCategoryAxis.GetHashCode() ) * 397 ^ ( this.CategoryPointSeries != null ? this.CategoryPointSeries.GetHashCode() : 0 ) ) * 397 ^ this.FlipCoordinates.GetHashCode() ) * 397 ^ this.IsHorizontal.GetHashCode() ) * 397 ^ ( this.BaseXValues != null ? this.BaseXValues.GetHashCode() : 0 ) ) * 397 ^ ( this.PointRange != null ? this.PointRange.GetHashCode() : 0 ) ) * 397 ^ this.DataPointPixelSize.GetHashCode() ) * 397 ^ this.DataPointStep.GetHashCode() ) * 397 ^ this.IsBaseXValuesSorted.GetHashCode() ) * 397 ^ this.LogarithmicBase.GetHashCode();
        }
    }
}
