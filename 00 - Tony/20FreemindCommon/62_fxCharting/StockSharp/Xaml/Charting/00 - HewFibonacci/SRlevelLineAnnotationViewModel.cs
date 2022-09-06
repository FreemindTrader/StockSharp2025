using System.Windows.Media;

namespace fx.Charting.HewFibonacci
{
    public class SRlevelLineAnnotationViewModel
    {
        public SRlevelLineAnnotationViewModel( double y1, Brush stroke )
        {
            LineLevel = y1;
            Stroke = stroke;
        }

        public double LineLevel
        {
            get;
            set;
        }

        public Brush Stroke
        {
            get;
            set;
        }
    }
}
