using StockSharp.Algo.Candles;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockSharp.Algo.Indicators;

namespace StockSharp.Xaml.Charting
{
    public class AddIndicatorFifoEventArgs : EventArgs
    {
        bool _useFifo;
        int _fifoCapcity = -1;

        ChartArea _chartArea;

        public ChartArea ChartArea
        {
            get => _chartArea;
            set => _chartArea = value;
        }

        public int FifoCapcity
        {
            get => _fifoCapcity;
            set => _fifoCapcity = value;
        }


        public bool UseFifo
        {
            get => _useFifo;
            set => _useFifo = value;
        }

        public AddIndicatorFifoEventArgs( ChartArea area, bool useFifo = true )
        {
            ChartArea = area;
            UseFifo = useFifo;
            FifoCapcity = 5000;
        }
    }

    public class AddIndicatorEventArgs : EventArgs
    {
        CandleSeries _candleSerie;
        ChartArea _chartArea;

        public ChartArea ChartArea
        {
            get => _chartArea;
            set => _chartArea = value;
        }

        public CandleSeries CandleSerie
        {
            get => _candleSerie;
            set => _candleSerie = value;
        }


        public AddIndicatorEventArgs( ChartArea area, CandleSeries series )
        {
            ChartArea = area;
            CandleSerie = series;
        }
    }
}
