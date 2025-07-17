using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ecng.Xaml.Charting.Rendering.Common
{
    internal static class RenderContextExtensions
    {
        internal static IPen2D GetStyledPen( this IRenderContext2D renderContext, Line styledLine, bool isAntiAliased = false )
        {
            double[] array = styledLine.StrokeDashArray.ToArray<double>();
            Color color = new Color();
            SolidColorBrush stroke = styledLine.Stroke as SolidColorBrush;
            if ( stroke != null )
            {
                color = stroke.Color;
            }
            float strokeThickness = (float)styledLine.StrokeThickness;
            return renderContext.CreatePen( color, isAntiAliased, strokeThickness, styledLine.Opacity, array, styledLine.StrokeEndLineCap );

        }
    }
}
