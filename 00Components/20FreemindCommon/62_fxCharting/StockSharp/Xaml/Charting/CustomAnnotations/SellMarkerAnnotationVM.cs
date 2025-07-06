using SciChart.Charting.Model.ChartSeries;
using SciChart.Examples.ExternalDependencies.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StockSharp.Xaml.Charting.CustomAnnotations
{
    // https://github.com/ElinamLLC/SharpVectors
    // The SharpVectors is a .NET 4.0 or up library and applications. The library can be used in WPF and Windows Forms applications.
    public class SellMarkerAnnotationVM : BaseAnnotationViewModel, IBuySellAnnotationViewModel
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
        public override Type ViewType { get { return typeof( SellMarkerAnnotation ); } }
    }
}
