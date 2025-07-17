// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartDataObject
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StockSharp.Xaml.Charting.ChartModifiers;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting
{
    public class ChartDataObject : BindableObject
    {
        private bool _showVisibilityCheckboxes;

        public ChartDataObject()
        {
            this.SeriesInfo = new ObservableCollection<StockSharp.Xaml.Charting.SeriesInfo>();
        }

        public ChartDataObject( IEnumerable<StockSharp.Xaml.Charting.SeriesInfo> seriesInfos )
          : this()
        {
            this.UpdateSeriesInfo( seriesInfos );
        }

        public bool ShowVisibilityCheckboxes
        {
            get
            {
                return this._showVisibilityCheckboxes;
            }
            set
            {
                if ( this._showVisibilityCheckboxes == value )
                    return;
                this._showVisibilityCheckboxes = value;
                this.OnPropertyChanged( nameof( ShowVisibilityCheckboxes ) );
            }
        }

        public ObservableCollection<StockSharp.Xaml.Charting.SeriesInfo> SeriesInfo
        {
            get;
        }

        public void UpdateSeriesInfo( IEnumerable<StockSharp.Xaml.Charting.SeriesInfo> newInfos )
        {
            Dictionary<object, StockSharp.Xaml.Charting.SeriesInfo> newInfosDict = newInfos.ToDictionary<StockSharp.Xaml.Charting.SeriesInfo, object>((Func<StockSharp.Xaml.Charting.SeriesInfo, object>) (si => si.SeriesInfoKey));
            this.SeriesInfo.RemoveWhere<StockSharp.Xaml.Charting.SeriesInfo>( ( Predicate<StockSharp.Xaml.Charting.SeriesInfo> ) ( si =>
            {
                StockSharp.Xaml.Charting.SeriesInfo seriesInfo;
                if ( !newInfosDict.TryGetValue( si.SeriesInfoKey, out seriesInfo ) )
                    return true;
                return seriesInfo.GetType() != si.GetType();
            } ) );
            Dictionary<object, StockSharp.Xaml.Charting.SeriesInfo> oldInfos = this.SeriesInfo.ToDictionary<StockSharp.Xaml.Charting.SeriesInfo, object>((Func<StockSharp.Xaml.Charting.SeriesInfo, object>) (si => si.SeriesInfoKey));
            newInfosDict.Values.Where<StockSharp.Xaml.Charting.SeriesInfo>( ( Func<StockSharp.Xaml.Charting.SeriesInfo, bool> ) ( si =>
            {
                if ( si.RenderableSeries.GetIncludeSeries( Modifier.Cursor ) )
                    return !oldInfos.ContainsKey( si.SeriesInfoKey );
                return false;
            } ) ).ForEachDo<StockSharp.Xaml.Charting.SeriesInfo>( ( Action<StockSharp.Xaml.Charting.SeriesInfo> ) ( si => this.SeriesInfo.Add( si ) ) );
            this.SeriesInfo.ForEachDo<StockSharp.Xaml.Charting.SeriesInfo>( ( Action<StockSharp.Xaml.Charting.SeriesInfo> ) ( si => si.CopyFrom( newInfosDict[ si.SeriesInfoKey ] ) ) );
        }
    }
}
