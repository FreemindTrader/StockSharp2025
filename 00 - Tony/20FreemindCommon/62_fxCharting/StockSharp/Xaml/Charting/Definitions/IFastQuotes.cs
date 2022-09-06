using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Charting
{
    public interface IFastQuotes
    {
        bool CanUpdateQuotes { get; set; }
        void UpdateQuotes( double bid, double ask );
    }

    public interface INullBar
    {
        bool CanUpdateNullBar { get; set; }
        bool UpdateNullBar( DateTime barTime, double bid, double ask );
    }
}
