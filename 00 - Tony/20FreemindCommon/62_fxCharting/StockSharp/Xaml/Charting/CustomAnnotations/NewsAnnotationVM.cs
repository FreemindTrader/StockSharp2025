using SciChart.Charting.Model.ChartSeries;
using SciChart.Examples.ExternalDependencies.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Charting.CustomAnnotations
{
    public class NewsAnnotationVM : BaseAnnotationViewModel
    {
        private NewsEvent _newsEvent;

        public NewsEvent NewsData
        {
            get { return _newsEvent; }
            set
            {
                _newsEvent = value;
                OnPropertyChanged( "NewsData" );
            }
        }

        public override Type ViewType { get { return typeof( NewsAnnotation ); } }
    }    
}
