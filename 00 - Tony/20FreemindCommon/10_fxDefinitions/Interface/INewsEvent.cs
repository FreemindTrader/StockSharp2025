using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum EventImpact
	{
        Unknown = 0,
        Low = 1,
		Medium = 2,
		High = 3,
		Holiday = 4,
		
	}

    public interface INewsEvent
    {
        DateTime NewsTime { get; set; }
        DateTime NewsTimeUTC { get; set; }
        string   Currency { get; set; }
        string   Description     { get; set; }
        string   Impact { get; set; }

        string   Actual { get; set; }
        string   ForeCast { get; set; }
        string   Previous { get; set; }
        string   Revised { get; set; }
        int      FfNewsId   { get; set; }          
    }
}
