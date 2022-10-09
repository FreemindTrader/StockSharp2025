﻿using fx.Definitions;
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
        public fxFibRatioModel( double value, Brush brush, FibonacciTargetType fibType )
        {
            FibValue = value;
            Brush = brush;
            FibTargetType = fibType;
        }
        //
        // Summary:
        //     Gets or sets the Value that is used for displaying Level of the (SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation.FibonacciRatioLine)
        public double FibValue { get; set; }
        //
        // Summary:
        //     Gets or sets brush that is used for coloring (SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation.FibonacciRatioLine)
        public Brush Brush { get; set; }

        public FibonacciTargetType FibTargetType { get; set; }
    }


}
