// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.PointSeriesEnumerator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace fx.Xaml.Charting
{
    public class PointSeriesEnumerator : IEnumerator<IPoint>, IDisposable, IEnumerator, IEnumerator<Point2D>
    {
        private readonly IPointSeries _pointSeries;
        private readonly int _count;
        private int _index;
        private bool _hasCurrent;
        private IPoint _current;
        private Point2D _current2d;

        public bool IsReset
        {
            get
            {
                return this._index < 0;
            }
        }

        public IPoint Current
        {
            get
            {
                if ( !this._hasCurrent )
                    this._current = ( IPoint ) this._current2d;
                return this._current;
            }
        }

        Point2D IEnumerator<Point2D>.Current
        {
            get
            {
                return this._current2d;
            }
        }

        public Point2D CurrentPoint2D
        {
            get
            {
                return this._current2d;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return ( object ) this.Current;
            }
        }

        public PointSeriesEnumerator( IPointSeries pointSeries )
        {
            this._pointSeries = pointSeries;
            this._count = pointSeries.Count;
            this.Reset();
        }

        public virtual bool MoveNext()
        {
            if ( ++this._index >= this._count )
                return false;
            this._current = this._pointSeries[ this._index ];
            this._current2d = new Point2D( this._current.X, this._current.Y );
            this._hasCurrent = true;
            return true;
        }

        public virtual void Reset()
        {
            this._index = -1;
            this._current = ( IPoint ) null;
            this._current2d = new Point2D();
        }

        public virtual void Dispose()
        {
        }

        private void CheckIndex()
        {
            if ( this._index < 0 )
                throw new InvalidOperationException();
        }
    }
}
