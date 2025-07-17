// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.AxisInteractivityHelper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
namespace fx.Xaml.Charting
{
    internal class AxisInteractivityHelper : IAxisInteractivityHelper
    {
        private readonly ICoordinateCalculator<double> _coordCalculator;

        public AxisInteractivityHelper( ICoordinateCalculator<double> coordinateCalculator )
        {
            this._coordCalculator = coordinateCalculator;
        }

        public IRange Zoom( IRange initialRange, double fromCoord, double toCoord )
        {
            double dataValue1 = this._coordCalculator.GetDataValue(fromCoord);
            double dataValue2 = this._coordCalculator.GetDataValue(toCoord);
            if ( dataValue1 >= dataValue2 )
                NumberUtil.Swap( ref dataValue1, ref dataValue2 );
            return RangeFactory.NewWithMinMax( initialRange, ( IComparable ) dataValue1, ( IComparable ) dataValue2 );
        }

        public IRange ZoomBy( IRange initialRange, double minFraction, double maxFraction )
        {
            DoubleRange doubleRange = this._coordCalculator.TranslateBy(minFraction, maxFraction, initialRange);
            return RangeFactory.NewWithMinMax( initialRange, ( IComparable ) doubleRange.Min, ( IComparable ) doubleRange.Max );
        }

        public IRange ScrollInMinDirection( IRange rangeToScroll, double pixels )
        {
            IRange range = this.Scroll(rangeToScroll, pixels);
            return this.ConstrainScrolledRange( rangeToScroll, range.Min, rangeToScroll.Max );
        }

        private IRange ConstrainScrolledRange( IRange rangeToScroll, IComparable min, IComparable max )
        {
            IRange range = rangeToScroll;
            if ( min.CompareTo( ( object ) max ) < 0 )
                range = RangeFactory.NewWithMinMax( rangeToScroll, min, max );
            return range;
        }

        public IRange ScrollInMaxDirection( IRange rangeToScroll, double pixels )
        {
            IRange range = this.Scroll(rangeToScroll, pixels);
            return this.ConstrainScrolledRange( rangeToScroll, rangeToScroll.Min, range.Max );
        }

        public IRange Scroll( IRange rangeToScroll, double pixels )
        {
            DoubleRange doubleRange = this._coordCalculator.TranslateBy(pixels, rangeToScroll.AsDoubleRange());
            return RangeFactory.NewWithMinMax( rangeToScroll, ( IComparable ) doubleRange.Min, ( IComparable ) doubleRange.Max );
        }

        [Obsolete( "The ScrollBy method is Obsolete as it is only really possible to implement on Category Axis. For this axis type just update the IndexRange (visibleRange) by N to scroll the axis", true )]
        public IRange ScrollBy( IRange rangeToScroll, int pointAmount )
        {
            throw new NotImplementedException();
        }

        public IRange ClipRange( IRange rangeToClip, IRange maximumRange, ClipMode clipMode )
        {
            IRange range1 = rangeToClip;
            if ( clipMode != ClipMode.None )
            {
                IRange range2 = ((IRange) range1.Clone()).ClipTo(maximumRange);
                bool flag1 = (uint) range2.Min.CompareTo((object) range1.Min) > 0U;
                bool flag2 = (uint) range2.Max.CompareTo((object) range1.Max) > 0U;
                DoubleRange doubleRange1 = rangeToClip.AsDoubleRange();
                DoubleRange doubleRange2 = range2.AsDoubleRange();
                double num1 = flag2 ? doubleRange1.Max - doubleRange2.Max : doubleRange1.Min - doubleRange2.Min;
                double min = doubleRange1.Min - num1;
                double max = min + doubleRange1.Diff;
                if ( this._coordCalculator.IsLogarithmicAxisCalculator )
                {
                    ILogarithmicCoordinateCalculator coordCalculator = (ILogarithmicCoordinateCalculator) this._coordCalculator;
                    double num2 = Math.Log(doubleRange1.Min, coordCalculator.LogarithmicBase);
                    double num3 = Math.Log(doubleRange1.Max, coordCalculator.LogarithmicBase);
                    double num4 = flag2 ? num3 - Math.Log(doubleRange2.Max, coordCalculator.LogarithmicBase) : num2 - Math.Log(doubleRange2.Min, coordCalculator.LogarithmicBase);
                    min = Math.Pow( coordCalculator.LogarithmicBase, num2 - num4 );
                    double num5 = num3 - num2;
                    max = Math.Pow( coordCalculator.LogarithmicBase, num2 - num4 + num5 );
                }
                switch ( clipMode )
                {
                    case ClipMode.StretchAtExtents:
                        range1 = range2;
                        break;
                    case ClipMode.ClipAtMin:
                        range1 = flag1 ? RangeFactory.NewWithMinMax( maximumRange, min, max, maximumRange ) : RangeFactory.NewWithMinMax( maximumRange, range2.Min, range1.Max );
                        break;
                    case ClipMode.ClipAtExtents:
                        if ( flag1 & flag2 )
                        {
                            range1 = range2;
                            break;
                        }
                        if ( flag1 | flag2 )
                        {
                            range1 = RangeFactory.NewWithMinMax( maximumRange, min, max, maximumRange );
                            break;
                        }
                        break;
                }
            }
            if ( range1.Min.CompareTo( ( object ) range1.Max ) > 0 )
                range1 = RangeFactory.NewRange( range1.Max, range1.Min );
            Guard.Assert( range1.Min, "min" ).IsLessThanOrEqualTo( range1.Max, "max" );
            return range1;
        }
    }
}
