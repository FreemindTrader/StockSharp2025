using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Common
{
    public interface IfxIndicatorManager
    {
        ReadOnlyCollection<IfxIndicator> IndicatorsUnsafe { get;  }
    }
}
