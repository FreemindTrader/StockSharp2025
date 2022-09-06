using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum fxMsgType
    {
        DONE = 0,
        MSG = 1,
        CALC = 2,
        SAVING = 3,
        DB = 4,
        NETWORK = 5,
        BasicTask = 6,
        AdvancedTask = 7,
        EWTask = 8

    }
    public interface ITradingEvent
    {
        TimeSpan Period               { get; set; }

        int MacdCrossSignal                { get; set; }

        int BarsFromLastSignal        { get; set; }

        string CandleSignal      { get; set; }

        int BarsFromLastCandlePattern { get; set; }

        int BarsFromLastExtremum      { get; set; }

        string NextTarget             { get; set; }

        double PipsFromTarget         { get; set; }

        string PeriodString           { get; }

        string LastMessage { get; set; }

    }
}
