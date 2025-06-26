using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class DivergenceInfo : IComparable, IComparable<DivergenceInfo>
    {
        public TimeSpan TimePeriod
        {
            get;
            set;
        }

        public TADivergence Divergence
        {
            get;
            set;
        }

        public long StartIndex
        {
            get;
            set;
        }

        public double StartValue
        {
            get;
            set;
        }

        public long EndIndex
        {
            get;
            set;
        }

        public double EndValue
        {
            get;
            set;
        }

        public DivergenceInfo( TimeSpan timePeriod, TADivergence divergence, long startIndex, double startValue, long endIndex, double endValue )
        {
            TimePeriod = timePeriod;
            Divergence = divergence;
            StartIndex = startIndex;
            StartValue = startValue;
            EndIndex = endIndex;
            EndValue = endValue;
        }

        public override string ToString()
        {
            string output = "[" + StartIndex + "-> " + EndIndex + "] " + "\r\nType = " + Divergence.ToDescription( );

            return output;
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            DivergenceInfo other = obj as DivergenceInfo;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( DivergenceInfo ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( DivergenceInfo other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = TimePeriod.CompareTo( other.TimePeriod );
            if ( result != 0 )
            {
                return result;
            }

            result = Divergence.CompareTo( other.Divergence );
            if ( result != 0 )
            {
                return result;
            }

            result = StartIndex.CompareTo( other.StartIndex );
            if ( result != 0 )
            {
                return result;
            }

            result = StartValue.CompareTo( other.StartValue );
            if ( result != 0 )
            {
                return result;
            }

            result = EndIndex.CompareTo( other.EndIndex );
            if ( result != 0 )
            {
                return result;
            }

            result = EndValue.CompareTo( other.EndValue );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }
    }
}
