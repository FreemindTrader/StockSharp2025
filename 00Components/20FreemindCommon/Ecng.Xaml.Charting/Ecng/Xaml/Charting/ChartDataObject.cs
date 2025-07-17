// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ChartDataObject
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace Ecng.Xaml.Charting
{
    public class ChartDataObject : BindableObject
    {
        private bool _showVisibilityCheckboxes;

        public ChartDataObject()
        {
            this.SeriesInfo = new ObservableCollection<Ecng.Xaml.Charting.SeriesInfo>();
        }

        public ChartDataObject( IEnumerable<Ecng.Xaml.Charting.SeriesInfo> seriesInfos )
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

        public ObservableCollection<Ecng.Xaml.Charting.SeriesInfo> SeriesInfo
        {
            get;
        }

        public void UpdateSeriesInfo( IEnumerable<Ecng.Xaml.Charting.SeriesInfo> newInfos )
        {
            Dictionary<object, Ecng.Xaml.Charting.SeriesInfo> newInfosDict = newInfos.ToDictionary<Ecng.Xaml.Charting.SeriesInfo, object>((Func<Ecng.Xaml.Charting.SeriesInfo, object>) (si => si.SeriesInfoKey));
            this.SeriesInfo.RemoveWhere<Ecng.Xaml.Charting.SeriesInfo>( ( Predicate<Ecng.Xaml.Charting.SeriesInfo> ) ( si =>
            {
                Ecng.Xaml.Charting.SeriesInfo seriesInfo;
                if ( !newInfosDict.TryGetValue( si.SeriesInfoKey, out seriesInfo ) )
                    return true;
                return seriesInfo.GetType() != si.GetType();
            } ) );
            Dictionary<object, Ecng.Xaml.Charting.SeriesInfo> oldInfos = this.SeriesInfo.ToDictionary<Ecng.Xaml.Charting.SeriesInfo, object>((Func<Ecng.Xaml.Charting.SeriesInfo, object>) (si => si.SeriesInfoKey));
            newInfosDict.Values.Where<Ecng.Xaml.Charting.SeriesInfo>( ( Func<Ecng.Xaml.Charting.SeriesInfo, bool> ) ( si =>
            {
                if ( si.RenderableSeries.GetIncludeSeries( Modifier.Cursor ) )
                    return !oldInfos.ContainsKey( si.SeriesInfoKey );
                return false;
            } ) ).ForEachDo<Ecng.Xaml.Charting.SeriesInfo>( ( Action<Ecng.Xaml.Charting.SeriesInfo> ) ( si => this.SeriesInfo.Add( si ) ) );
            this.SeriesInfo.ForEachDo<Ecng.Xaml.Charting.SeriesInfo>( ( Action<Ecng.Xaml.Charting.SeriesInfo> ) ( si => si.CopyFrom( newInfosDict[ si.SeriesInfoKey ] ) ) );
        }
    }
}
