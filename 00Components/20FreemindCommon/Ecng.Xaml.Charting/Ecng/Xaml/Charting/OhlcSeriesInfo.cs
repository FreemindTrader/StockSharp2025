// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.OhlcSeriesInfo
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    public class OhlcSeriesInfo : HlcSeriesInfo
    {
        private double _openValue;

        public OhlcSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            this.OpenValue = Convert.ToDouble( ( object ) hitTestInfo.OpenValue );
            this.HighValue = Convert.ToDouble( ( object ) hitTestInfo.HighValue );
            this.LowValue = Convert.ToDouble( ( object ) hitTestInfo.LowValue );
            this.CloseValue = Convert.ToDouble( ( object ) hitTestInfo.CloseValue );
        }

        public double OpenValue
        {
            get
            {
                return this._openValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._openValue, value, nameof( OpenValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedOpenValue" );
            }
        }

        public string FormattedOpenValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.OpenValue );
            }
        }

        public override void CopyFrom( SeriesInfo other )
        {
            base.CopyFrom( other );
            this.OpenValue = ( ( OhlcSeriesInfo ) other ).OpenValue;
        }
    }
}
