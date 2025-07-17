// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.Array2DPointSeries`2
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    internal class Array2DPointSeries<TX, TY> : GenericPointSeriesBase<XyySeriesPoint> where TX : IComparable where TY : IComparable
    {
        private readonly IHeatmap2DArrayDataSeries _dataSeries;
        private readonly Func<int, TX> _xMapping;
        private readonly Func<int, TY> _yMapping;

        public Array2DPointSeries( IHeatmap2DArrayDataSeries dataSeries, Func<int, TX> xMapping, Func<int, TY> yMapping )
          : base( ( IPointSeries ) null )
        {
            this._xMapping = xMapping;
            this._yMapping = yMapping;
            this._dataSeries = dataSeries;
        }

        public override IPoint this[ int index ]
        {
            get
            {
                return ( IPoint ) new Array2DSegment<TX, TY>( this._dataSeries, this._xMapping, this._yMapping, index );
            }
        }

        public override int Count
        {
            get
            {
                return this._dataSeries.ArrayWidth;
            }
        }

        public override DoubleRange GetYRange()
        {
            return new DoubleRange( this._yMapping( 0 ).ToDouble(), this._yMapping( this._dataSeries.ArrayHeight ).ToDouble() );
        }
    }
}
