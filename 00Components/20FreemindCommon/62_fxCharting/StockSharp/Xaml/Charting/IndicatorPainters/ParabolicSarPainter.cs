using StockSharp.Algo.Indicators;

namespace StockSharp.Xaml.Charting.IndicatorPainters
{
    [Indicator( typeof( ParabolicSar ) )]
    public class ParabolicSarPainter : DefaultPainter
    {
        public ParabolicSarPainter( )
        {
            Line.Style = ChartIndicatorDrawStyles.Dot;
            Line.StrokeThickness = 4;
        }
    }
}
