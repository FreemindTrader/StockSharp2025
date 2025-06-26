using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Bars
{
    public delegate void IndicatorUpdateDelegate( IIndicatorManager session, IMyIndicator indicator );
    public delegate void IndicatorCalculatedDelegate( IMyIndicator indicator, bool fullRecalculation );
    public interface IIndicatorManager
    {
        bool Initialize();

        TimeSpan Period { get;  }

        bool AddIndicator( IMyIndicator indicator );

        event IndicatorUpdateDelegate IndicatorManagerFullCalculationDoneEvent;
    }

    public interface IMyIndicator
    {
        void FreemindCalculateIndicators( bool fullRecalculation, HistoricBarsUpdateEventArg e );

        bool AttachDatasource( IHistoricBarsRepo dataProvider );

        void Dispose();

        event EventHandler< HistoricBarsUpdateEventArg > IndicatorCalculatedEvent;
        event EventHandler< HistoricBarsUpdateEventArg > FullCalculationDoneEvent;
    }
}
