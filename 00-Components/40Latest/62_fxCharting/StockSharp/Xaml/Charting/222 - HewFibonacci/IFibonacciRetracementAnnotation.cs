using fx.Algorithm;
using fx.Common;
using SciChart.Charting.DrawingTools.TradingAnnotations;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Data.Model;
using fx.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using fx.Bars;

namespace StockSharp.Xaml.Charting.HewFibonacci
{
    public interface IfxImportantLevel
    {
        void HighlightConfluence( double fibLine );

        void HighlightComingSRLines( ref SBar bar, double fibLine );

        void HighlightSingleConfluence( double fibLine );

        PooledList<double> HighlightedSelected( bool CTRLKEY, IRange xRange, PooledDictionary<string, IRange> yRange );

        void DimAllImportantLines( );

        IList<SRlevel> ImportantLines { get; set; }

        Guid LineGuid { get; set; }

        bool IsLocked { get; set; }

        void UpdateLastX( ref SBar bar );

        PooledList<double> GetSelectedLines( );

        
    }

    public interface ITradingAnnotationEx : ITradingAnnotation
    {
        IEnumerable<IAnnotation> MovingLinesPartAnnotations
        {
            get;
        }

        int BasePointsCount { get; }
    }

    public interface IfxFibonacciAnnotation : ITradingAnnotation
    {                      
        HewFibGannTargets FibTarget { get; }

        TrendDirection Trend { get; }

        DateTime EndingTime { get; }
        int BasePointsCount { get; }
    }    
}
