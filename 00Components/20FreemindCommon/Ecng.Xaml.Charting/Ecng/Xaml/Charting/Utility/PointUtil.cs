// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Utility.PointUtil
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Ecng.Xaml.Charting.Utility
{
    public static class PointUtil
    {
        public static bool LineSegmentsIntersection2D( PointUtil.Line firstLine, PointUtil.Line secondLine, out Point intersectionPoint )
        {
            return PointUtil.LineIntersection2D( firstLine, secondLine, out intersectionPoint, false );
        }

        public static bool LineIntersection2D( PointUtil.Line firstLine, PointUtil.Line secondLine, out Point intersectionPoint )
        {
            return PointUtil.LineIntersection2D( firstLine, secondLine, out intersectionPoint, true );
        }

        private static bool LineIntersection2D( PointUtil.Line firstLine, PointUtil.Line secondLine, out Point intersectionPoint, bool intersectAtInfinity )
        {
            bool flag1 = PointUtil.IsPoint(firstLine);
            bool flag2 = PointUtil.IsPoint(secondLine);
            if ( flag1 & flag2 )
                return PointUtil.PointsIntersection( firstLine, secondLine, out intersectionPoint );
            if ( flag1 )
                return PointUtil.PointAndLineIntersection( firstLine, secondLine, out intersectionPoint );
            if ( flag2 )
                return PointUtil.PointAndLineIntersection( secondLine, firstLine, out intersectionPoint );
            double num1 = (secondLine.Y2 - secondLine.Y1) * (firstLine.X2 - firstLine.X1) - (secondLine.X2 - secondLine.X1) * (firstLine.Y2 - firstLine.Y1);
            double num2 = (secondLine.X2 - secondLine.X1) * (firstLine.Y1 - secondLine.Y1) - (secondLine.Y2 - secondLine.Y1) * (firstLine.X1 - secondLine.X1);
            double num3 = (firstLine.X2 - firstLine.X1) * (firstLine.Y1 - secondLine.Y1) - (firstLine.Y2 - firstLine.Y1) * (firstLine.X1 - secondLine.X1);
            if ( num2 == 0.0 || num3 == 0.0 )
            {
                Point[] coincidentSubset = PointUtil.GetCoincidentSubset(new Point(firstLine.X1, firstLine.Y1), new Point(firstLine.X2, firstLine.Y2), new Point(secondLine.X1, secondLine.Y1), new Point(secondLine.X2, secondLine.Y2));
                if ( coincidentSubset.Length != 0 )
                {
                    intersectionPoint = coincidentSubset[ 0 ];
                    return true;
                }
            }
            if ( num1 == 0.0 )
            {
                intersectionPoint = new Point();
                return false;
            }
            double num4 = num2 / num1;
            double num5 = num3 / num1;
            if ( intersectAtInfinity || num4 >= 0.0 && num4 <= 1.0 && ( num5 >= 0.0 && num5 <= 1.0 ) )
            {
                intersectionPoint = new Point( firstLine.X1 + num4 * ( firstLine.X2 - firstLine.X1 ), firstLine.Y1 + num4 * ( firstLine.Y2 - firstLine.Y1 ) );
                return true;
            }
            intersectionPoint = new Point();
            return false;
        }

        private static bool PointsIntersection( PointUtil.Line L1, PointUtil.Line L2, out Point ptIntersection )
        {
            Point point1 = new Point(L1.X1, L1.Y1);
            Point point2 = new Point(L2.X1, L2.Y1);
            ptIntersection = new Point( point1.X, point1.Y );
            return PointUtil.Distance( point1, point2 ).CompareTo( 0.0 ) == 0;
        }

        public static double Distance( Point point1, Point point2 )
        {
            double num1 = point1.X - point2.X;
            double num2 = point1.Y - point2.Y;
            return Math.Sqrt( num1 * num1 + num2 * num2 );
        }

        public static double PolarDistance( Point point1, Point point2 )
        {
            double d = Math.PI / 180.0 * (point2.X - point1.X);
            double num = 2.0 * point1.Y * point2.Y * Math.Cos(d);
            return Math.Sqrt( point1.Y * point1.Y + point2.Y * point2.Y - num );
        }

        private static bool PointAndLineIntersection( PointUtil.Line l1, PointUtil.Line l2, out Point ptIntersection )
        {
            Point pt = new Point(l1.X1, l1.Y1);
            Point start = new Point(l2.X1, l2.Y1);
            Point end = new Point(l2.X2, l2.Y2);
            ptIntersection = new Point( pt.X, pt.Y );
            return PointUtil.DistanceFromLine( pt, start, end, true ).CompareTo( 0.0 ) == 0;
        }

        internal static double DistanceFromLine( Point pt, Point start, Point end, bool isSegment = true )
        {
            double num = PointUtil.CrossProduct(start, end, pt) / PointUtil.Distance(start, end);
            if ( isSegment )
            {
                if ( PointUtil.DotProduct( start, end, pt ) > 0.0 )
                    return PointUtil.Distance( end, pt );
                if ( PointUtil.DotProduct( end, start, pt ) > 0.0 )
                    return PointUtil.Distance( start, pt );
            }
            return Math.Abs( num );
        }

        private static double CrossProduct( Point pointA, Point pointB, Point pointC )
        {
            return ( pointB.X - pointA.X ) * ( pointC.Y - pointA.Y ) - ( pointB.Y - pointA.Y ) * ( pointC.X - pointA.X );
        }

        private static double DotProduct( Point pointA, Point pointB, Point pointC )
        {
            Point point1 = new Point();
            Point point2 = new Point();
            point1.X = pointB.X - pointA.X;
            point1.Y = pointB.Y - pointA.Y;
            point2.X = pointC.X - pointB.X;
            point2.Y = pointC.Y - pointB.Y;
            return point1.X * point2.X + point1.Y * point2.Y;
        }

        private static Point[ ] GetCoincidentSubset( Point a1, Point a2, Point b1, Point b2 )
        {
            double num1 = a2.X - a1.X;
            double num2 = a2.Y - a1.Y;
            double ub1;
            double ub2;
            if ( Math.Abs( num1 ) > Math.Abs( num2 ) )
            {
                ub1 = ( b1.X - a1.X ) / num1;
                ub2 = ( b2.X - a1.X ) / num1;
            }
            else
            {
                ub1 = ( b1.Y - a1.Y ) / num2;
                ub2 = ( b2.Y - a1.Y ) / num2;
            }
            List<Point> pointList = new List<Point>();
            foreach ( double overlapInterval in PointUtil.OverlapIntervals( ub1, ub2 ) )
            {
                Point point = new Point(a2.X * overlapInterval + a1.X * (1.0 - overlapInterval), a2.Y * overlapInterval + a1.Y * (1.0 - overlapInterval));
                pointList.Add( point );
            }
            return pointList.ToArray();
        }

        private static double[ ] OverlapIntervals( double ub1, double ub2 )
        {
            double val2_1 = Math.Min(ub1, ub2);
            double val2_2 = Math.Max(ub1, ub2);
            double num1 = Math.Max(0.0, val2_1);
            double num2 = Math.Min(1.0, val2_2);
            if ( num1 > num2 )
                return new double[ 0 ];
            if ( num1 == num2 )
                return new double[ 1 ] { num1 };
            return new double[ 2 ] { num1, num2 };
        }

        internal static bool LiesToTheLeft( Point pointToCheck, Point lineEnd1, Point lineEnd2 )
        {
            return PointUtil.CrossProduct( lineEnd1, lineEnd2, pointToCheck ) > 0.0;
        }

        internal static bool IsPointInTriangle( Point checkPt, Point pointA, Point pointB, Point pointC )
        {
            if ( pointA == pointB && pointB == pointC )
                return checkPt == pointA;
            if ( pointA == pointB || pointB == pointC )
                return PointUtil.DistanceFromLine( checkPt, pointA, pointC, true ) < double.Epsilon;
            if ( pointA == pointC )
                return PointUtil.DistanceFromLine( checkPt, pointA, pointB, true ) < double.Epsilon;
            bool flag1 = PointUtil.CrossProduct(checkPt, pointA, pointB) < double.Epsilon;
            bool flag2 = PointUtil.CrossProduct(checkPt, pointB, pointC) < double.Epsilon;
            bool flag3 = PointUtil.CrossProduct(checkPt, pointC, pointA) < double.Epsilon;
            if ( flag1 == flag2 )
                return flag2 == flag3;
            return false;
        }

        private static bool IsPoint( PointUtil.Line line )
        {
            if ( Math.Abs( line.X1 - line.X2 ) < double.Epsilon )
                return Math.Abs( line.Y1 - line.Y2 ) < double.Epsilon;
            return false;
        }

        internal static bool IsInBounds( this Point point, Size viewportSize )
        {
            if ( point.X >= 0.0 && point.X <= viewportSize.Width && point.Y >= 0.0 )
                return point.Y <= viewportSize.Height;
            return false;
        }

        internal static bool ArePointsOnSameLine( Point pt1, Point pt2, Point pt3 )
        {
            return ( pt1.X * ( pt2.Y - pt3.Y ) + pt2.X * ( pt3.Y - pt1.Y ) + pt3.X * ( pt1.Y - pt2.Y ) ).Equals( 0.0 );
        }

        internal static Point ClipPoint( this Point pt, Size viewportSize, int yExtension = 0, int xExtension = 0 )
        {
            double num1 = viewportSize.Width + (double) xExtension;
            double num2 = viewportSize.Height + (double) yExtension;
            if ( pt.X < ( double ) -xExtension )
                pt.X = ( double ) -xExtension;
            else if ( pt.X > num1 )
                pt.X = num1;
            if ( pt.Y < ( double ) -yExtension )
                pt.Y = ( double ) -yExtension;
            else if ( pt.Y > num2 )
                pt.Y = num2;
            return pt;
        }

        internal static IEnumerable<Point> ClipPolygon( IEnumerable<Point> points, Size viewportSize, int xExtension = 0, int yExtension = 0 )
        {
            Point? nullable = new Point?();
            Rect extents = new Rect((double) -xExtension, (double) -yExtension, viewportSize.Width + (double) xExtension, viewportSize.Height + (double) yExtension);
            IEnumerator<Point> enumerator = points.GetEnumerator();
            while ( enumerator.MoveNext() )
            {
                Point currPoint = enumerator.Current;
                if ( nullable.HasValue )
                {
                    Point point1 = nullable.Value;
                    if ( !point1.IsInBounds( viewportSize ) || !currPoint.IsInBounds( viewportSize ) )
                    {
                        double x1 = point1.X;
                        double y1 = point1.Y;
                        double x2 = currPoint.X;
                        double y2 = currPoint.Y;
                        if ( WriteableBitmapExtensions.CohenSutherlandLineClip( extents, ref x1, ref y1, ref x2, ref y2 ) )
                        {
                            yield return point1.ClipPoint( viewportSize, yExtension, xExtension );
                            yield return new Point( x1, y1 );
                            yield return new Point( x2, y2 );
                        }
                        else
                        {
                            Point point2 = point1.ClipPoint(viewportSize, yExtension, xExtension);
                            Point currClipped = currPoint.ClipPoint(viewportSize, yExtension, xExtension);
                            bool theLeft = PointUtil.LiesToTheLeft(new Point(0.0, 0.0), point1, currPoint);
                            Point nearestCorner = theLeft && currPoint.Y < point1.Y || !theLeft && currPoint.Y > point1.Y ? new Point(point2.X, currClipped.Y) : new Point(currClipped.X, point2.Y);
                            yield return point2;
                            yield return nearestCorner;
                            yield return currClipped;
                            currClipped = new Point();
                            nearestCorner = new Point();
                        }
                    }
                    else
                        yield return point1;
                }
                nullable = new Point?( currPoint );
                currPoint = new Point();
            }
            enumerator = ( IEnumerator<Point> ) null;
            if ( nullable.HasValue )
                yield return nullable.Value.ClipPoint( viewportSize, yExtension, xExtension );
        }

        public struct Line
        {
            public double X1;
            public double Y1;
            public double X2;
            public double Y2;

            public Line( Point pt1, Point pt2 )
            {
                this = new PointUtil.Line( pt1.X, pt1.Y, pt2.X, pt2.Y );
            }

            public Line( double x1, double y1, double x2, double y2 )
            {
                this = new PointUtil.Line();
                this.X1 = x1;
                this.Y1 = y1;
                this.X2 = x2;
                this.Y2 = y2;
            }
        }
    }
}
