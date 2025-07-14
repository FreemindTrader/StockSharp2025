using Ecng.Serialization;
using System;
using System.Collections.Generic; using fx.Collections;

namespace StockSharp.Xaml.Charting
{
    public interface IChartIndicatorPainter : ICloneable, IPersistable
    {
        ChartIndicatorElement Element { get; }

        IEnumerable< IChartElement > InnerElements { get; }

        bool Draw( ChartDrawData data );

        void Reset( );

        void OnAttached( ChartIndicatorElement element );

        void OnDetached( );
    }
}
