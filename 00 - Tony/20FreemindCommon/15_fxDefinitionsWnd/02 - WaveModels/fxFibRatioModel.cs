using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace fx.DefinitionsWnd
{
    public class fxFibRatioModel
    {
        //
        // Summary:
        //     Initializes a new instance of the SciChart.Charting.DrawingTools.TradingAnnotations.Models.RatioModel
        //     class.
        public fxFibRatioModel( double value, Brush brush )
        {
            Value = value;
            Brush = brush;
        }
        //
        // Summary:
        //     Gets or sets the Value that is used for displaying Level of the (SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation.FibonacciRatioLine)
        public double Value { get; set; }
        //
        // Summary:
        //     Gets or sets brush that is used for coloring (SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation.FibonacciRatioLine)
        public Brush Brush { get; set; }
    }


}
