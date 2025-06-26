using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    [Serializable]
    public enum PositionsType
    {
        All          = 0,
        Lossing      = 1,
        Winning      = 2,
        Long         = 3,
        Short        = 4,
        LongHedge    = 5,
        ShortHedge   = 6,
        WinningHedge = 7,
        LossingHedge = 8
    }

}
