using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
using fx.Collections;
using StockSharp.Charting;

namespace StockSharp.Xaml.Charting
{
    public interface IChartEx : IPersistable, IThemeableChart
    {
        INotifyList<ChartArea> ChartAreas
        {
            get;
        }

        bool IsAutoScroll
        {
            get; set;
        }

        bool IsAutoRange
        {
            get; set;
        }

        ChartAxisType XAxisType
        {
            get; set;
        }

        IList<IndicatorType> IndicatorTypes
        {
            get;
        }

        void AddArea( ChartArea area );

        void RemoveArea( ChartArea area );

        void ClearAreas();

        IIndicator GetIndicator( ChartIndicatorElement element );

        object GetSource( IChartElement element );

        void Reset( IEnumerable<IChartElement> elements );

        void Draw( ChartDrawData data );


        void AddElement( ChartArea area, IChartElement element );

        void AddElement( ChartArea area, OrdersUI element, Security security );

        void AddElement( ChartArea area, TradesUI element, Security security );

        void AddElement( ChartArea area, ChartCandleElement element, CandleSeries candleSeries );

        void AddElement( ChartArea area, ChartIndicatorElement element, CandleSeries candleSeries, IIndicator indicator );

        void RemoveElement( ChartArea area, IChartElement element );

        void InvokeAnnotationCreatedEvent( ChartAnnotation annotation );

        void InvokeAnnotationModifiedEvent( ChartAnnotation annotation, ChartDrawData.AnnotationData aData );

        void InvokeAnnotationSelectedEvent( ChartAnnotation annotation, ChartDrawData.AnnotationData aData );

        void InvokeAnnotationDeletedEvent( ChartAnnotation annotation );
    }
}
