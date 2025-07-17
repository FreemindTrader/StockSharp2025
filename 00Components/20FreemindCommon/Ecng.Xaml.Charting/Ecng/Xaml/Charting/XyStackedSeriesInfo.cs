// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.XyStackedSeriesInfo
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    public class XyStackedSeriesInfo : SeriesInfo
    {
        private IComparable _accumulated;

        public XyStackedSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            this.AccumulatedValue = hitTestInfo.YValue;
            this.YValue = hitTestInfo.Y1Value;
            this.Value = this.YValue.ToDouble();
        }

        public IComparable AccumulatedValue
        {
            get
            {
                return this._accumulated;
            }
            set
            {
                this.SetField<IComparable>( ref this._accumulated, value, nameof( AccumulatedValue ) );
            }
        }

        public override void CopyFrom( SeriesInfo other )
        {
            base.CopyFrom( other );
            this.AccumulatedValue = ( ( XyStackedSeriesInfo ) other ).AccumulatedValue;
        }
    }
}
