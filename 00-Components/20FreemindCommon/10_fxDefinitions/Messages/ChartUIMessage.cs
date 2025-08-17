using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions.Messages
{
    public enum ChartUIMessageType
    {
        AddArea = 1,
        AddCandle = 2,
        AddIndicator = 3,
    }

    public class ChartUIMessage
    {
        public ChartUIMessageType MsgType
        {
            get;
            set;
        }
    }
}
