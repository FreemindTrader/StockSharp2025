// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Range`1
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.ComponentModel;
namespace fx.Xaml.Charting
{
    public abstract class Range<T> : BindableObject, IRange<T>, IRange, ICloneable, INotifyPropertyChanged where T : IComparable
    {
        private static IMath<T> _math = GenericMathFactory.New<T>();
        private T _min;
        private T _max;

        protected Range()
        {
        }

        protected Range( T min, T max )
        {
            this.Min = min;
            this.Max = max;
        }

        public virtual bool IsDefined
        {
            get
            {
                if ( this.Max.IsDefined() )
                    return this.Min.IsDefined();
                return false;
            }
        }

        IComparable IRange.Min
        {
            get
            {
                return ( IComparable ) this.Min;
            }
            set
            {
                this.Min = ( T ) value;
            }
        }

        IComparable IRange.Max
        {
            get
            {
                return ( IComparable ) this.Max;
            }
            set
            {
                this.Max = ( T ) value;
            }
        }

        IComparable IRange.Diff
        {
            get
            {
                return ( IComparable ) this.Diff;
            }
        }

        public abstract bool IsZero
        {
            get;
        }

        public T Min
        {
            get
            {
                return this._min;
            }
            set
            {
                T min = this._min;
                this._min = value;
                this.OnPropertyChanged( nameof( Min ), ( object ) min, ( object ) value );
            }
        }

        public T Max
        {
            get
            {
                return this._max;
            }
            set
            {
                T max = this._max;
                this._max = value;
                this.OnPropertyChanged( nameof( Max ), ( object ) max, ( object ) value );
            }
        }

        public abstract T Diff
        {
            get;
        }

        public abstract object Clone();

        public abstract IRange<T> GrowBy( double minFraction, double maxFraction );

        public abstract IRange<T> ClipTo( IRange<T> maximumRange );

        public abstract DoubleRange AsDoubleRange();

        public abstract IRange<T> SetMinMax( double min, double max );

        public abstract IRange<T> SetMinMax( double min, double max, IRange<T> maxRange );

        protected void SetMinMaxInternal( T min, T max )
        {
            if ( this.Max.CompareTo( ( object ) min ) < 0 )
            {
                this.Max = max;
                this.Min = min;
            }
            else
            {
                this.Min = min;
                this.Max = max;
            }
        }

        public IRange ClipTo( IRange maximumRange )
        {
            return ( IRange ) this.ClipTo( ( IRange<T> ) maximumRange );
        }

        public IRange ClipTo( IRange maximumRange, RangeClipMode clipMode )
        {
            IRange range = (IRange) null;
            switch ( clipMode )
            {
                case RangeClipMode.MinMax:
                    range = maximumRange;
                    break;
                case RangeClipMode.Max:
                    T obj1 = ComparableUtil.MinValue<T>();
                    range = RangeFactory.NewWithMinMax( maximumRange, ( IComparable ) obj1, maximumRange.Max );
                    break;
                case RangeClipMode.Min:
                    T obj2 = ComparableUtil.MaxValue<T>();
                    range = RangeFactory.NewWithMinMax( maximumRange, maximumRange.Min, ( IComparable ) obj2 );
                    break;
            }
            return ( IRange ) this.ClipTo( ( IRange<T> ) range );
        }

        public IRange Union( IRange range )
        {
            return ( IRange ) this.Union( ( IRange<T> ) range );
        }

        public bool IsValueWithinRange( IComparable value )
        {
            if ( this.Min.CompareTo( ( object ) value ) <= 0 )
                return this.Max.CompareTo( ( object ) value ) >= 0;
            return false;
        }

        public IRange<T> Union( IRange<T> range )
        {
            return ( IRange<T> ) RangeFactory.NewRange( ( IComparable ) Range<T>._math.Min( this.Min, range.Min ), ( IComparable ) Range<T>._math.Max( this.Max, range.Max ) );
        }

        IRange IRange.SetMinMax( double min, double max )
        {
            return ( IRange ) this.SetMinMax( min, max );
        }

        IRange IRange.SetMinMaxWithLimit( double min, double max, IRange maxRange )
        {
            return ( IRange ) this.SetMinMax( min, max, ( IRange<T> ) maxRange );
        }

        IRange IRange.GrowBy( double minFraction, double maxFraction )
        {
            return ( IRange ) this.GrowBy( minFraction, maxFraction );
        }

        public override string ToString()
        {
            return string.Format( "{0} {{Min={1}, Max={2}}}", ( object ) this.GetType(), ( object ) this.Min, ( object ) this.Max );
        }

        public override int GetHashCode()
        {
            T obj = this.Min;
            int num = obj.GetHashCode() * 397;
            obj = this.Max;
            int hashCode = obj.GetHashCode();
            return num ^ hashCode;
        }

        public override bool Equals( object obj )
        {
            if ( !( obj is IRange<T> ) )
                return false;
            return this.Equals( ( IRange<T> ) obj );
        }

        public bool Equals( IRange<T> other )
        {
            if ( other == null )
                return false;
            if ( this == other )
                return true;
            T obj = other.Min;
            if ( !obj.Equals( ( object ) this.Min ) )
                return false;
            obj = other.Max;
            return obj.Equals( ( object ) this.Max );
        }

        internal void AssertMinLessOrEqualToThanMax()
        {
            Guard.Assert( ( IComparable ) this.Min, "Min" ).IsLessThanOrEqualTo( ( IComparable ) this.Max, "Max" );
        }
    }
}
