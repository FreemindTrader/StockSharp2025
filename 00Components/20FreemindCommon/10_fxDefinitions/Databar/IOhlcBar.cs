using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IOhlcBar
    {
        
        double Open  { get; set;  }    // Open (the first price) of the time period        
        double High  { get; set;  }    // High (the greatest price) of the time period
        double Low   { get; set;  }    // Low (the smallest price) of the time period
        double Close { get; set;  }    // Close (the latest price) of the time period
    }
}
