// fx.Xaml.Charting.Common.Extensions.DashSplitter
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using fx.Xaml.Charting;
internal sealed class DashSplitter : IEnumerator<LineD>, IDisposable, IEnumerator
{
    private sealed class LengthSplitter : IEnumerator<DashLength>, IDisposable, IEnumerator
    {
        public double length;

        public IDashSplittingContext dashSplittingContext;

        private DashLength _current;

        private int _state;

        private double passedLength;

        private double remainingLengthToPass;

        private double itemLength;

        private double itemPassedLength;

        private double remainingItemLength;

        private bool itemFlag;

        private double passedLength0;

        private double passedLength0_2;

        public DashLength Current => _current;

        object IEnumerator.Current
        {
            get
            {
                return _current;
            }
        }

        public bool MoveNext()
        {
            switch ( _state )
            {
                case 0:
                    _state = -1;
                    passedLength = 0.0;
                    remainingLengthToPass = length;
                    goto IL_00a6;
                case 1:
                    _state = -1;
                    goto IL_0057;
                case 2:
                    {
                        _state = -1;
                        break;
                    }
                IL_00a6:
                    itemLength = dashSplittingContext.StrokeDashArray[ dashSplittingContext.StrokeDashArrayIndex ];
                    itemPassedLength = dashSplittingContext.StrokeDashArrayItemPassedLength;
                    remainingItemLength = itemLength - itemPassedLength;
                    itemFlag = ( dashSplittingContext.StrokeDashArrayIndex % 2 == 0 );
                    if ( !( remainingLengthToPass >= remainingItemLength ) )
                    {
                        passedLength0_2 = passedLength;
                        passedLength += remainingLengthToPass;
                        dashSplittingContext.StrokeDashArrayItemPassedLength += remainingLengthToPass;
                        if ( itemFlag )
                        {
                            _current = new DashLength( passedLength0_2, passedLength );
                            _state = 2;
                            return true;
                        }
                        break;
                    }
                    passedLength0 = passedLength;
                    passedLength += remainingItemLength;
                    remainingLengthToPass -= remainingItemLength;
                    if ( itemFlag )
                    {
                        _current = new DashLength( passedLength0, passedLength );
                        _state = 1;
                        return true;
                    }
                    goto IL_0057;
                IL_0057:
                    dashSplittingContext.StrokeDashArrayIndex++;
                    dashSplittingContext.StrokeDashArrayItemPassedLength = 0.0;
                    if ( dashSplittingContext.StrokeDashArrayIndex >= dashSplittingContext.StrokeDashArray.Length )
                    {
                        dashSplittingContext.StrokeDashArrayIndex = 0;
                    }
                    goto IL_00a6;
            }
            return false;
        }

        public void Reset( double length, IDashSplittingContext dashSplittingContext )
        {
            this.dashSplittingContext = dashSplittingContext;
            this.dashSplittingContext.StrokeDashArrayIndex = 0;
            this.dashSplittingContext.StrokeDashArrayItemPassedLength = 0.0;
            this.length = length;
            _state = 0;
        }

        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
        }

        public LengthSplitter()
        {
            _state = 0;
        }
    }

    internal struct DashLength
    {
        internal readonly double Start;

        internal readonly double End;

        internal DashLength( double start, double end )
        {
            Start = start;
            End = end;
        }
    }

    private bool _hasDashes;

    private bool _isInViewport;

    private readonly LengthSplitter lengthSplitter = new LengthSplitter();

    private double _dx;

    private double _dy;

    private double _length;

    private int _index;

    private Point _pt1;

    private Point _pt2;

    private LineD _current;

    private double _oneOverLength;

    public LineD Current => _current;

    object IEnumerator.Current
    {
        get
        {
            return _current;
        }
    }

    internal DashSplitter( Point pt1, Point pt2, Size viewportSize, IDashSplittingContext dashSplittingContext )
    {
        Reset( pt1, pt2, viewportSize, dashSplittingContext );
    }

    internal DashSplitter()
    {
        _index = -1;
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        if ( _hasDashes )
        {
            if ( lengthSplitter.MoveNext() )
            {
                DashLength current = lengthSplitter.Current;
                double x = _pt1.X + current.Start * _oneOverLength * _dx;
                double y = _pt1.Y + current.Start * _oneOverLength * _dy;
                double x2 = _pt1.X + current.End * _oneOverLength * _dx;
                double y2 = _pt1.Y + current.End * _oneOverLength * _dy;
                _current = new LineD( x, y, x2, y2 );
                return true;
            }
            return false;
        }
        if ( _index < 0 )
        {
            _index++;
            _current = new LineD( _pt1.X, _pt1.Y, _pt2.X, _pt2.Y );
            return true;
        }
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    internal void Reset( Point pt1, Point pt2, Size viewportSize, IDashSplittingContext dashSplittingContext )
    {
        _hasDashes = dashSplittingContext.HasDashes;
        _pt1 = pt1;
        _pt2 = pt2;
        Rect extents = new Rect(0.0, 0.0, viewportSize.Width, viewportSize.Height);
        if ( _hasDashes )
        {
            double x = pt1.X;
            double y = pt1.Y;
            double x2 = pt2.X;
            double y2 = pt2.Y;
            _isInViewport = WriteableBitmapExtensions.CohenSutherlandLineClip( extents, ref x, ref y, ref x2, ref y2 );
            _dx = x2 - x;
            _dy = y2 - y;
            _length = ( double ) ( float ) Math.Sqrt( _dx * _dx + _dy * _dy );
            _oneOverLength = 1.0 / _length;
            lengthSplitter.Reset( _length, dashSplittingContext );
        }
        else
        {
            _isInViewport = true;
        }
        _index = -1;
    }
}
