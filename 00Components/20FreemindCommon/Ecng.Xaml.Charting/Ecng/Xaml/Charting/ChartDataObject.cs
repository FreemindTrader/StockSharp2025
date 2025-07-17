// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ChartDataObject
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace fx.Xaml.Charting
{
    public class ChartDataObject : BindableObject
    {
        private bool _showVisibilityCheckboxes;

        public ChartDataObject()
        {
            this.SeriesInfo = new ObservableCollection<fx.Xaml.Charting.SeriesInfo>();
        }

        public ChartDataObject( IEnumerable<fx.Xaml.Charting.SeriesInfo> seriesInfos )
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

        public ObservableCollection<fx.Xaml.Charting.SeriesInfo> SeriesInfo
        {
            get;
        }

        public void UpdateSeriesInfo( IEnumerable<fx.Xaml.Charting.SeriesInfo> newInfos )
        {
            Dictionary<object, fx.Xaml.Charting.SeriesInfo> newInfosDict = newInfos.ToDictionary<fx.Xaml.Charting.SeriesInfo, object>((Func<fx.Xaml.Charting.SeriesInfo, object>) (si => si.SeriesInfoKey));
            this.SeriesInfo.RemoveWhere<fx.Xaml.Charting.SeriesInfo>( ( Predicate<fx.Xaml.Charting.SeriesInfo> ) ( si =>
            {
                fx.Xaml.Charting.SeriesInfo seriesInfo;
                if ( !newInfosDict.TryGetValue( si.SeriesInfoKey, out seriesInfo ) )
                    return true;
                return seriesInfo.GetType() != si.GetType();
            } ) );
            Dictionary<object, fx.Xaml.Charting.SeriesInfo> oldInfos = this.SeriesInfo.ToDictionary<fx.Xaml.Charting.SeriesInfo, object>((Func<fx.Xaml.Charting.SeriesInfo, object>) (si => si.SeriesInfoKey));
            newInfosDict.Values.Where<fx.Xaml.Charting.SeriesInfo>( ( Func<fx.Xaml.Charting.SeriesInfo, bool> ) ( si =>
            {
                if ( si.RenderableSeries.GetIncludeSeries( Modifier.Cursor ) )
                    return !oldInfos.ContainsKey( si.SeriesInfoKey );
                return false;
            } ) ).ForEachDo<fx.Xaml.Charting.SeriesInfo>( ( Action<fx.Xaml.Charting.SeriesInfo> ) ( si => this.SeriesInfo.Add( si ) ) );
            this.SeriesInfo.ForEachDo<fx.Xaml.Charting.SeriesInfo>( ( Action<fx.Xaml.Charting.SeriesInfo> ) ( si => si.CopyFrom( newInfosDict[ si.SeriesInfoKey ] ) ) );
        }
    }
}
