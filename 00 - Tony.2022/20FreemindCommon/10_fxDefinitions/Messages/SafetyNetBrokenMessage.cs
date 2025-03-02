using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum SafetyNetAction
    {
        WAITING_ACTION = 0,
        HEDGE_ALL     = 1,
        HEDGE_EXTREME = 2,
        CLOSE_ALL     = 3,
        MESSAGE_SENT  = 4,
        PROCESSED_ALL = 10

    }


    [Serializable]
    public class SafetyNetBrokenMessage
    {// Fields...

        public string AccountName { get; set; }

        public double BrokenAmount { get; set; }
        public string Symbol { get; set; }
        public SafetyNetBrokenMessage( string accountName, string symbol, double brokenAmount )
        {
            Symbol       = symbol;

            AccountName  = accountName;

            BrokenAmount = brokenAmount;
        }
    }


    [Serializable]
    public class SafetyNetActionMessage
    {// Fields...

        public string AccountName { get; set; }

        public SafetyNetAction Action { get; set; }
        public string Symbol { get; set; }
        public SafetyNetActionMessage( string symbol, SafetyNetAction action )
        {
            Symbol = symbol;            

            Action = action;
        }
    }
}

