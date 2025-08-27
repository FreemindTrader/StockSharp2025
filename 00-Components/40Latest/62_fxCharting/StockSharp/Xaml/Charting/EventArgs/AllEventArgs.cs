using StockSharp.Algo.Candles;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockSharp.BusinessEntities;

namespace StockSharp.Xaml.Charting
{
    public class AddCandlesEventArgs : EventArgs
    {
        bool _useFifo;
        int _fifoCapcity = -1;

        Subscription _subscription;
        ChartArea _chartArea;

        public ChartArea ChartArea
        {
            get => _chartArea;
            set => _chartArea = value;
        }

        public Subscription CandleSubscription
        {
            get => _subscription;
            set => _subscription = value;
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

        public AddCandlesEventArgs( ChartArea area, Subscription series )
        {
            ChartArea   = area;
            CandleSubscription = series;
            UseFifo     = false;
        }

        public AddCandlesEventArgs( ChartArea area, Subscription series, int fifoCapacity )
        {
            ChartArea   = area;
            CandleSubscription = series;
            FifoCapcity = fifoCapacity;
            UseFifo     = true;
        }
    }


    
}
