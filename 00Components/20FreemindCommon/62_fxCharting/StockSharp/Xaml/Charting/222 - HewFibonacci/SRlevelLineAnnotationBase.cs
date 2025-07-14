using SciChart.Charting.DrawingTools.TradingAnnotations.Models;
using SciChart.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Xaml.Charting.HewFibonacci
{
    public class SRlevelLineAnnotationBase : LineAnnotation, IFibLevel
    {
        public static readonly DependencyProperty LineLevelProperty     = DependencyProperty.Register( nameof( LineLevel ),     typeof( double ), typeof( SRlevelLineAnnotationBase ), new PropertyMetadata(   0.0 ) );
        public static readonly DependencyProperty NextLineLevelProperty = DependencyProperty.Register( nameof( NextLineLevel ), typeof( double ), typeof( SRlevelLineAnnotationBase ), new PropertyMetadata(   0.0 ) );

        public double NextLineLevel
        {
            get
            {
                return ( double )GetValue( NextLineLevelProperty );
            }
            set
            {
                SetValue( NextLineLevelProperty, value );
            }
        }

        public double LineLevel
        {
            get
            {
                return ( double )GetValue( LineLevelProperty );
            }
            set
            {
                SetValue( LineLevelProperty, value );
            }
        }

        public bool LineSelected { get; set; }

    }    
}
