using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public class Vector 
    {
        public double X;
        public double Y;

        // Constructors.
        public Vector( double x, double y ) { X = x; Y = y; }
        public Vector( ) : this( double.NaN, double.NaN ) { }

        public static Vector operator -( Vector v, Vector w )
        {
            return new Vector( v.X - w.X, v.Y - w.Y );
        }

        public static Vector operator +( Vector v, Vector w )
        {
            return new Vector( v.X + w.X, v.Y + w.Y );
        }

        public static double operator *( Vector v, Vector w )
        {
            return v.X * w.X + v.Y * w.Y;
        }

        public static Vector operator *( Vector v, double mult )
        {
            return new Vector( v.X * mult, v.Y * mult );
        }

        public static Vector operator *( double mult, Vector v )
        {
            return new Vector( v.X * mult, v.Y * mult );
        }

        public double Cross( Vector v )
        {
            return X * v.Y - Y * v.X;
        }

        

        public override string ToString( )
        {
            return string.Format( "({0};{1})", X, Y );
        }       

        public override bool Equals( object obj )
        {
            var v = (Vector)obj;
            return ( X - v.X ).IsZero( ) && ( Y - v.Y ).IsZero( );
        }

        

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ X.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ Y.GetHashCode( );
                return hashCode;
            }
        }
    }

    public static class Extensions
    {
        private const double Epsilon = 1e-10;

        public static bool IsZero( this double d )
        {
            return Math.Abs( d ) < Epsilon;
        }
    }
    public class TrendLine
    {

        Vector _end;
        Vector _begin;

        public Vector Begin
        {
            get { return _begin; }
            set
            {
                _begin = value;
            }
        }

        
        public Vector End
        {
            get { return _end; }
            set
            {
                _end = value;
            }
        }
        

        public TrendLine( INeelyWave begin, INeelyWave end )
        {


            // ![](7BF8C061AF882B477DC907D3FF98E31A.png;;;0.01629,0.01225)
            // it seems like for uptrend or downtrend , we should always use the EndVector to draw trendlines.

            _begin = begin.EndVector;
            _end = end.EndVector;
        }

        public TrendLine( Vector begin, Vector end )
        {
            _begin = begin;
            _end = end;
        }

        public bool Intersect( TrendLine other, out Vector intersection, bool considerOverlapAsIntersect = false )
        {
            intersection = new Vector( );

            var p    = Begin;
            var p2   = End;
            var q    = other.Begin;
            var q2   = other.End;

            var r    = p2 - p;
            var s    = q2 - q;
            var rxs  = r.Cross(s);
            var qpxr = (q - p).Cross(r);

            // If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
            if ( rxs.IsZero( ) && qpxr.IsZero( ) )
            {
                // 1. If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s
                // then the two lines are overlapping,
                if ( considerOverlapAsIntersect )
                    if ( ( 0 <= ( q - p ) * r && ( q - p ) * r <= r * r ) || ( 0 <= ( p - q ) * s && ( p - q ) * s <= s * s ) )
                        return true;

                // 2. If neither 0 <= (q - p) * r ≤ r * r nor 0 <= (p - q) * s <= s * s
                // then the two lines are collinear but disjoint.
                // No need to implement this expression, as it follows from the expression above.
                return false;
            }

            // 3. If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
            if ( rxs.IsZero( ) && !qpxr.IsZero( ) )
                return false;

            // t = (q - p) x s / (r x s)
            var t = (q - p).Cross(s) / rxs;

            // u = (q - p) x r / (r x s)

            var u = (q - p).Cross(r) / rxs;

            // 4. If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            // the two line segments meet at the point p + t r = q + u s.
            if ( !rxs.IsZero( ) && ( 0 <= t && t <= 1 ) && ( 0 <= u && u <= 1 ) )
            {
                // We can calculate the intersection point using either t or u.
                intersection = p + t * r;

                // An intersection was found.
                return true;
            }

            // 5. Otherwise, the two line segments are not parallel but do not intersect.
            return false;
        }

    }
}
