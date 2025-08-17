using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    /// <summary>
    /// The interface for the price information in the time period
    /// </summary>
    public interface IAskBidBar
    {        
        
        
        
        

        bool isValidBar { get; }

        ( DateTime, float ) OpenTimeValue { get;  }
        ( DateTime, float ) CloseTimeValue { get; }
        ( DateTime, float ) HighTimeValue { get; }
        ( DateTime, float ) LowTimeValue { get; }

        DateTime LocalTime { get;  }


    }
}
