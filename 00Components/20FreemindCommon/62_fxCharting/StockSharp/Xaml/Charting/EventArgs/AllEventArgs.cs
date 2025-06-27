using StockSharp.Algo.Candles;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Charting
{
    public class AddCandlesEventArgs : EventArgs
    {
        bool _useFifo;
        int _fifoCapcity = -1;

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

        public AddCandlesEventArgs( ChartArea area, CandleSeries series )
        {
            ChartArea   = area;
            CandleSerie = series;
            UseFifo     = false;
        }

        public AddCandlesEventArgs( ChartArea area, CandleSeries series, int fifoCapacity )
        {
            ChartArea   = area;
            CandleSerie = series;
            FifoCapcity = fifoCapacity;
            UseFifo     = true;
        }
    }


    
}
