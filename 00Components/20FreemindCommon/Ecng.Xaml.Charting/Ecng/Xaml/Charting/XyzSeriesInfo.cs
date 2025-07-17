// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.XyzSeriesInfo
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    public class XyzSeriesInfo : SeriesInfo
    {
        private IComparable _zValue;

        public XyzSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            this.ZValue = hitTestInfo.ZValue;
        }

        public IComparable ZValue
        {
            get
            {
                return this._zValue;
            }
            set
            {
                this.SetField<IComparable>( ref this._zValue, value, nameof( ZValue ) );
            }
        }

        public override void CopyFrom( SeriesInfo other )
        {
            base.CopyFrom( other );
            this.ZValue = ( ( XyzSeriesInfo ) other ).ZValue;
        }
    }
}
