// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartSeriesViewModel
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.ComponentModel;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting
{
    public class ChartSeriesViewModel : BindableObject, IChartSeriesViewModel, INotifyPropertyChanged
    {
        private IDataSeries _dataSeries;
        private IRenderableSeries _renderSeries;

        public ChartSeriesViewModel( IDataSeries dataSeries, IRenderableSeries renderSeries )
        {
            _dataSeries = dataSeries;
            _renderSeries = renderSeries;
        }

        public IDataSeries DataSeries
        {
            get
            {
                return _dataSeries;
            }
            set
            {
                _dataSeries = value;
                OnPropertyChanged( nameof( DataSeries ) );
            }
        }

        public IRenderableSeries RenderSeries
        {
            get
            {
                return _renderSeries;
            }
            set
            {
                _renderSeries = value;
                OnPropertyChanged( nameof( RenderSeries ) );
            }
        }
    }
}
