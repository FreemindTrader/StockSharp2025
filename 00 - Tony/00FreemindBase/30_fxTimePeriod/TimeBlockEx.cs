using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.TimePeriod
{
    public struct TimeBlockEx : IEquatable<TimeBlockEx>
    {
        public DateTime Start;
        public DateTime End;
		public TimeSpan Duration
        {
            get
            {
                return End - Start;
            }
        }

		public static TimeBlockEx  EmptyBlock = new TimeBlockEx( DateTime.MinValue, DateTime.MinValue );

		public TimeBlockEx( DateTime start, DateTime end )
		{
			if ( start <= end )
			{
				Start = start;
				End = end;
			}
			else
			{
				Start = start;
				End = end;
			}			
		}

        public TimeBlockEx( DateTime start, TimeSpan period )
        {
            Start = start;
            End = start + period;            
        }

        public bool HasInside( DateTime test )
		{
			return test >= Start && test <= End;			
		}

        public override bool Equals( object obj )
        {
            if ( obj is TimeBlockEx )
            {
                return Equals( ( TimeBlockEx ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( TimeBlockEx first, TimeBlockEx second )
        {
            return first.Equals( second );
        }

        public static bool operator !=( TimeBlockEx first, TimeBlockEx second )
        {
            return !( first == second );
        }

        public bool Equals( TimeBlockEx other )
        {
            return Start.Equals( other.Start ) && End.Equals( other.End ) && Duration.Equals( other.Duration );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ Start.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ End.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<TimeSpan>.Default.GetHashCode( Duration );
                return hashCode;
            }
        }
    }

    
}
