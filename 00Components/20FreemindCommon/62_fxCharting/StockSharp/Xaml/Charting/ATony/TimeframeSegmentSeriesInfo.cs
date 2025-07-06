using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Visuals.RenderableSeries;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml.Charting.ATony
{
    public class TimeframeSegmentSeriesInfo : SeriesInfo
    {
        //private long _volume;

        public TimeframeSegmentSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            //this.Volume = hitTestInfo.Volume;
        }

        //public long Volume
        //{
        //    get
        //    {
        //        return this._volume;
        //    }
        //    set
        //    {
        //        this.SetField<long>( ref this._volume, value, nameof( Volume ) );
        //    }
        //}

        //public override void CopyFrom( SeriesInfo other )
        //{
        //    base.CopyFrom( other );
        //    this.Volume = ( ( TimeframeSegmentSeriesInfo )other ).Volume;
        //}
    }
}
