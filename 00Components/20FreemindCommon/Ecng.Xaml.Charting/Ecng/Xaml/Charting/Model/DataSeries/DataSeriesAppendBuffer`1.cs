// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.DataSeriesAppendBuffer`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;

namespace Ecng.Xaml.Charting.Model.DataSeries
{
    internal class DataSeriesAppendBuffer<TPoint>
    {
        private List<TPoint> _points = new List<TPoint>();
        private readonly Action<IList<TPoint>> _flushAction;

        public object SyncRoot { get; } = new object();

        public DataSeriesAppendBuffer( Action<IList<TPoint>> flushAction )
        {
            this._flushAction = flushAction;
        }

        public void Clear()
        {
            lock ( this.SyncRoot )
                this._points.Clear();
        }

        public void Flush()
        {
            if ( this._points.Count == 0 )
                return;
            List<TPoint> points;
            lock ( this.SyncRoot )
            {
                points = this._points;
                this._points = new List<TPoint>();
            }
            if ( points.Count <= 0 )
                return;
            this._flushAction( ( IList<TPoint> ) points );
        }

        public void Append( TPoint newPoint )
        {
            lock ( this.SyncRoot )
                this._points.Add( newPoint );
        }
    }
}
