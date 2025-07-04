using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using System.Collections.Generic; using fx.Collections;

namespace fx.Charting
{
    public interface IChart : IPersistable, IThemeableChart
    {
        INotifyList< ChartArea > ChartAreas   { get;      }

        bool IsAutoScroll                     { get; set; }

        bool IsAutoRange                      { get; set; }

        ChartAxisType XAxisType               { get; set; }

        IList< IndicatorType > IndicatorTypes { get;      }

        void AddArea( ChartArea area );

        void RemoveArea( ChartArea area );

        void ClearAreas( );
        
        IIndicator GetIndicator( IndicatorUI element );

        object GetSource( IChartElement element );

        void Reset( IEnumerable<IChartElement> elements );

        void Draw( ChartDrawDataEx data );


        void AddElement   ( ChartArea area, IChartElement element );

        void AddElement   ( ChartArea area, OrdersUI      element, Security security );

        void AddElement   ( ChartArea area, TradesUI      element, Security security );

        void AddElement   ( ChartArea area, CandlestickUI element, CandleSeries candleSeries );

        void AddElement   ( ChartArea area, IndicatorUI   element, CandleSeries candleSeries, IIndicator indicator );

        void RemoveElement( ChartArea area, IChartElement element );

        void InvokeAnnotationCreatedEvent ( AnnotationUI annotation );

        void InvokeAnnotationModifiedEvent( AnnotationUI annotation, ChartDrawDataEx.sAnnotation aData );

        void InvokeAnnotationSelectedEvent( AnnotationUI annotation, ChartDrawDataEx.sAnnotation aData );

        void InvokeAnnotationDeletedEvent ( AnnotationUI annotation );
    }
}
