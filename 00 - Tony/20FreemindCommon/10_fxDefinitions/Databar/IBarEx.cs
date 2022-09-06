using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IBarEx : ITradingSessionBar,
                              ICandlestickBar,
                              ITechnicalBar,
                              IElliottWaveBar
    {
        
    }
}
