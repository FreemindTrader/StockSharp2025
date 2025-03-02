using Ecng.Serialization;
using System;
using System.Collections.Generic; using fx.Collections;

namespace fx.Charting
{
    public interface IChartIndicatorPainter : ICloneable, IPersistable
    {
        IndicatorUI Element { get; }

        IEnumerable< IfxChartElement > InnerElements { get; }

        bool Draw( ChartDrawDataEx data );

        void Reset( );

        void OnAttached( IndicatorUI element );

        void OnDetached( );
    }
}
