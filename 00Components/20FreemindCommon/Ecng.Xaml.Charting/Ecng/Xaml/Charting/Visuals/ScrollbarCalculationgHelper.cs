// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.ScrollbarCalculationgHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.Visuals
{
    internal class ScrollbarCalculationgHelper
    {
        private const double MinEdge = 0.0;
        private const double MaxEdge = 1.0;
        private readonly double _zooomConstrain;
        private readonly IAxis _axis;
        private readonly double _size;
        private double _start;
        private double _stop;

        public ScrollbarCalculationgHelper( IAxis axis, double actulaSize, double zoomConstrainPixels )
        {
            this._axis = axis;
            this._size = actulaSize;
            this._zooomConstrain = actulaSize > 0.0 ? zoomConstrainPixels / actulaSize : 0.0;
            this.UpdateRange( axis.VisibleRange );
        }

        public ScrollbarCalculationgHelper( IAxis axis, double actulaSize )
          : this( axis, actulaSize, 0.0 )
        {
        }

        public double Start
        {
            get
            {
                return this._start;
            }
        }

        public double Stop
        {
            get
            {
                return this._stop;
            }
        }

        public double StartOffset
        {
            get
            {
                return Math.Max( this._start * this._size, 0.0 ).RoundOff();
            }
        }

        public double StopOffset
        {
            get
            {
                return Math.Max( ( 1.0 - this._stop ) * this._size, 0.0 ).RoundOff();
            }
        }

        public void UpdateRange( IRange visibleRange )
        {
            if ( visibleRange == null )
            {
                this.ResetStartStop();
            }
            else
            {
                DoubleRange visibleRange1 = visibleRange.AsDoubleRange();
                IRange range = this._axis.DataRange;
                if ( range == null )
                {
                    this.ResetStartStop();
                }
                else
                {
                    if ( this._axis.GrowBy != null )
                        range = range.GrowBy( this._axis.GrowBy.Min, this._axis.GrowBy.Max );
                    DoubleRange maxRange = range.AsDoubleRange();
                    double diff = maxRange.Diff;
                    if ( diff.CompareTo( 0.0 ) == 0 )
                        this.ResetStartStop();
                    else
                        this.UpdateStartStop( maxRange, visibleRange1, diff );
                }
            }
        }

        private void ResetStartStop()
        {
            this._start = 0.0;
            this._stop = 1.0;
        }

        private void UpdateStartStop( DoubleRange maxRange, DoubleRange visibleRange, double diff )
        {
            double start;
            double stop;
            if ( this._axis.IsLogarithmicAxis )
            {
                double logarithmicBase = ((ILogarithmicAxis) this._axis).LogarithmicBase;
                double num1 = Math.Log(maxRange.Max, logarithmicBase) - Math.Log(maxRange.Min, logarithmicBase);
                double num2 = Math.Log(visibleRange.Min, logarithmicBase) - Math.Log(maxRange.Min, logarithmicBase);
                double num3 = Math.Log(visibleRange.Max, logarithmicBase) - Math.Log(maxRange.Min, logarithmicBase);
                start = num2 / num1;
                stop = num3 / num1;
            }
            else
            {
                start = ( visibleRange.Min - maxRange.Min ) / diff;
                stop = ( visibleRange.Max - maxRange.Min ) / diff;
            }
            this.TryCoerceEnds( ref start, ref stop );
            if ( this._axis.FlipCoordinates ^ !this._axis.IsXAxis )
            {
                this._start = 1.0 - stop;
                this._stop = 1.0 - start;
            }
            else
            {
                this._start = start;
                this._stop = stop;
            }
        }

        private void TryCoerceEnds( ref double start, ref double stop )
        {
            start = NumberUtil.Constrain( start, 0.0, 1.0 );
            stop = NumberUtil.Constrain( stop, 0.0, 1.0 );
            if ( Math.Abs( stop - start ) < this._zooomConstrain )
            {
                double num1 = start + (stop - start) / 2.0;
                double num2 = this._zooomConstrain / 2.0;
                start = num1 - num2;
                stop = num1 + num2;
            }
            if ( stop > 1.0 )
            {
                double num = stop - 1.0;
                stop -= num;
                start -= num;
            }
            else
            {
                if ( start >= 0.0 )
                    return;
                double num = 0.0 - start;
                stop += num;
                start += num;
            }
        }

        public IRange MoveTo( double coordinate )
        {
            double num1 = (this._start + (this._stop - this._start) / 2.0) * this._size;
            double num2 = coordinate - num1;
            return this.Resize( num2, num2 );
        }

        public IRange Resize( double start, double stop )
        {
            double offset1 = start / this._size;
            double offset2 = stop / this._size;
            double num1 = this._start + offset1;
            double num2 = this._stop + offset2;
            bool flag1 = Math.Abs(start) > double.Epsilon;
            bool flag2 = Math.Abs(stop) > double.Epsilon;
            if ( flag1 & flag2 )
            {
                num1 = this._start + this.ConstrainEndsOffset( offset1 );
                num2 = this._stop + this.ConstrainEndsOffset( offset2 );
            }
            else if ( flag1 )
                num1 = NumberUtil.Constrain( num1, 0.0, this._stop - this._zooomConstrain );
            else
                num2 = NumberUtil.Constrain( num2, this._start + this._zooomConstrain, 1.0 );
            this._start = num1;
            this._stop = num2;
            return this.CalculateRange();
        }

        private double ConstrainEndsOffset( double offset )
        {
            if ( offset < 0.0 && Math.Abs( offset ) > this._start )
                offset = -this._start;
            if ( offset > 1.0 - this._stop )
                offset = 1.0 - this._stop;
            return offset;
        }

        public IRange CalculateRange()
        {
            IRange range1 = (IRange) null;
            if ( this._axis != null && this._axis.VisibleRange != null )
            {
                IRange range2 = this._axis.DataRange;
                if ( range2 != null )
                {
                    if ( this._axis.GrowBy != null )
                        range2 = range2.GrowBy( this._axis.GrowBy.Min, this._axis.GrowBy.Max );
                    DoubleRange doubleRange = range2.AsDoubleRange();
                    double diff = doubleRange.Diff;
                    double num1;
                    double num2;
                    if ( this._axis.FlipCoordinates ^ !this._axis.IsXAxis )
                    {
                        num1 = 1.0 - this._stop;
                        num2 = 1.0 - this._start;
                    }
                    else
                    {
                        num2 = this._stop;
                        num1 = this._start;
                    }
                    double c1;
                    double c2;
                    if ( this._axis.IsLogarithmicAxis )
                    {
                        double logarithmicBase = ((ILogarithmicAxis) this._axis).LogarithmicBase;
                        double num3 = Math.Log(doubleRange.Max, logarithmicBase) - Math.Log(doubleRange.Min, logarithmicBase);
                        double num4 = num1 * num3;
                        double num5 = num2 * num3;
                        double num6 = Math.Log(doubleRange.Min, logarithmicBase);
                        c1 = Math.Pow( logarithmicBase, num4 + num6 );
                        c2 = Math.Pow( logarithmicBase, num5 + num6 );
                    }
                    else
                    {
                        c1 = num1 * diff + doubleRange.Min;
                        c2 = num2 * diff + doubleRange.Min;
                    }
                    if ( c1 < c2 && c1.IsDefined() && c2.IsDefined() )
                        range1 = RangeFactory.NewRange( this._axis.VisibleRange.GetType(), ( IComparable ) c1, ( IComparable ) c2 );
                }
                else
                    this.ResetStartStop();
            }
            return range1;
        }
    }
}
