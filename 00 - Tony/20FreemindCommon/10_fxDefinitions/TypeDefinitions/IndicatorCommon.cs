using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public enum IndicatorReturn
    {
        // In order of importance.
        Success                = 0 ,
        LibNotInitialize       = 1,
        BadParam               = 2,
        AllocErr               = 3,
        GroupNotFound          = 4,
        FuncNotFound           = 5,
        InvalidHandle          = 6,
        InvalidParamHolder     = 7,
        InvalidParamHolderType = 8,
        InvalidParamFunction   = 9,
        InputNotAllInitialize  = 10,
        OutputNotAllInitialize = 11,
        OutOfRangeStartIndex   = 12,
        OutOfRangeEndIndex     = 13,
        InvalidListType        = 14,
        BadObject              = 15,
        NotSupported           = 16,
        InternalError          = 5000,
        UnknownErr             = 0xFFFF

    }

    public enum linesCrossEnum
    {
        Down_Pos0Neg   = -22,
        DownTrend      = -11,
        CrossDown      = -1,
        Unknown        = 0,
        CrossUp        = 1,
        UpTrend        = 11,
        Up_Neg0Pos     = 22

    }

    public enum OscillatorEnum
    {
        PositiveDiv    = -33,
        ExitOversold   = -22,
        DownTrend      = -11,
        CrossDown      = -1,
        Unknown        = 0,
        CrossUp        = 1,
        UpTrend        = 11,
        ExitOverbought = 22,
        NegativeDiv    = 33
    }
}
