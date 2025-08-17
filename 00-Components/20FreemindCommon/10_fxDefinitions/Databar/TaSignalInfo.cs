using System;
using System.Collections.Generic;


namespace fx.Definitions
{
    [Flags]
    public enum TAToppingSignal
    {
        NONE                  = 0,
        MACD_CROSS_DOWN       = 1 << 1,
        WAVE_PEAK             = 1 << 2,
        GANN_PEAK             = 1 << 3,
        ExitOverBought        = 1 << 4,
        OscillatorCrossDown   = 1 << 5,
        OscNegativeDivergence = 1 << 6,

        MAXIMUM_MACD          = 1 << 7,
        
        
        ComasTurnDown         = 1 << 8,
        ComasCrossDown        = 1 << 9,
        OscillatorSmoothDown  = 1 << 10
    }

    [Flags]
    public enum TABottomingSignal
    {
        NONE                  = 0,
        MACD_CROSS_UP         = 1 << 1,
        WAVE_TROUGH           = 1 << 2,
        GANN_TROUGH           = 1 << 3,
        ExitOverSold          = 1 << 4,
        OscillatorCrossUp     = 1 << 5,
        OscPositiveDivergence = 1 << 6,

        MINIMUM_MACD          = 1 << 7,
        
        ComasTurnUp           = 1 << 8,
        ComasCrossUp          = 1 << 9,
        OscillatorSmoothUp    = 1 << 10,
    }

    public enum MarketDirection
    {
        Unknown           = 0,
        Bearish = 20,
        BearishCorrection = 30,
        
        BullishCorrection = 80,
        Bullish           = 90,
        
        
        
        Neutral = 99
    }

    public class TaSignalInfo
    {
    }
}
