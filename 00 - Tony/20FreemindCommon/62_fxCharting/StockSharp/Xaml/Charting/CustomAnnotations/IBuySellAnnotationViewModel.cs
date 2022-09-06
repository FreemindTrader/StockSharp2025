using SciChart.Charting.Model.ChartSeries;
using SciChart.Examples.ExternalDependencies.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Charting.CustomAnnotations
{   
    public interface IBuySellAnnotationViewModel : IAnnotationViewModel
    {
        Trade TradeData { get; set; }
    }
}
