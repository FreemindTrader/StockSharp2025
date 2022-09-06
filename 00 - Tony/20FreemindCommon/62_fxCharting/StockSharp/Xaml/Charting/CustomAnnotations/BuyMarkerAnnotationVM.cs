using SciChart.Charting.Model.ChartSeries;
using SciChart.Examples.ExternalDependencies.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Charting.CustomAnnotations
{
    // Viewmodel for the annotation type BuyMarkerAnnotation
    public class BuyMarkerAnnotationVM : BaseAnnotationViewModel, IBuySellAnnotationViewModel
    {
        private Trade _tradeData;

        public Trade TradeData
        {
            get { return _tradeData; }
            set
            {
                _tradeData = value;
                OnPropertyChanged( "TradeData" );
            }
        }
        public override Type ViewType { get { return typeof( BuyMarkerAnnotation ); } }
    }    
}
