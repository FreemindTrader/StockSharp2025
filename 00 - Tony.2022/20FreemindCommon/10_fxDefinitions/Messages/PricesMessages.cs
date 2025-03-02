using fx.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum PriceLevelsType
    {
        TAKE_PROFIT = 1,
        STOP_LOSS = 2,
        SAFETY_NET = 3

    }

    [Serializable]
    public class ChangeStopLimitSafetyMsg
    {// Fields...

        public PriceLevelsType MessageType { get; set; }

        public PooledList<float> PriceLevels { get; set; }

        public string Symbol { get; set; }
        public ChangeStopLimitSafetyMsg( string symbol, PriceLevelsType messageType, PooledList<float> priceLevels )
        {
            Symbol      = symbol;

            MessageType = messageType;

            PriceLevels = priceLevels;
        }
    }
}


