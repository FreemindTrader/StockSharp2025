using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{    
    [Flags]
    public enum TASignal : uint
    {
        NONE = 0,

        HAS_BOTTOMING_SIGNAL =        1 << ( BarBits.POS_BOTTOMING_SIGNAL - 1 ),
        HAS_TOPPING_SIGNAL   =        1 << ( BarBits.POS_TOPPING_SIGNAL   - 1 ),
        HAS_DIVERGENCE       =        1 << ( BarBits.POS_DIVERGENCE       - 1 ),
        HAS_TIME_ROTATION    =        1 << ( BarBits.POS_TIME_ROTATION    - 1 ),
        HAS_GANN_SQUARE      =        1 << ( BarBits.POS_GANN_SQUARE      - 1 ),
        HAS_PIVOT_RELATION   =        1 << ( BarBits.POS_PIVOT_RELATION   - 1 ),
        WAVE_TROUGH          =        1 << ( BarBits.POS_WAVE_TROUGH      - 1 ),
        GANN_TROUGH          =        1 << ( BarBits.POS_GANN_TROUGH      - 1 ),
        WAVE_PEAK            =        1 << ( BarBits.POS_WAVE_PEAK        - 1 ),
        GANN_PEAK            =        1 << ( BarBits.POS_GANN_PEAK        - 1 ),
        HAS_STRUCT_LABEL     =        1 << ( BarBits.POS_STRUCT_LABEL     - 1 ),
        HAS_ELLIOTT_WAVE     =        1 << ( BarBits.POS_ELLIOTT_WAVE     - 1 ),
        ExitOverSold         =        1 << ( BarBits.POS_ExitOverSold     - 1 ),
        ExitOverBought       =        1 << ( BarBits.POS_ExitOverBought   - 1 ),
        FibRetExpInfo        =        1 << ( BarBits.POS_FibRetExpInfo    - 1 ),
        ComasCrossUp         =        1 << ( BarBits.POS_ComasCrossUp     - 1 ),
        ComasCrossDown       =        1 << ( BarBits.POS_ComasCrossDown   - 1 ),
        ComasTurnUp          =        1 << ( BarBits.POS_ComasTurnUp      - 1 ),
        ComasTurnDown        =        1 << ( BarBits.POS_ComasTurnDown    - 1 ),
        WaveDirty            =        1 << ( BarBits.POS_WaveDirty        - 1 ),
        BAZI                 =        1 << ( BarBits.POS_BAZI             - 1 ),
        IsSpecial            =        1 << ( BarBits.POS_IsSpecialBar     - 1 ),
        SelectionChanged     =        1 << ( BarBits.POS_SelectionChanged - 1 ),
        IsSelected           = (uint) 1 << ( BarBits.POS_IsSelected       - 1 )
    }

    public enum WaveDirtyEnum
    {
        NONE,
        Add,
        Change,
        DeleteAll,
        DeleteSingle
    }

    public static class BarBits
    {
        public const uint TurnOffTop2Bits     = 0x3FFFFFFF;
        public const uint TurnOffTop4Bits     = 0x0FFFFFFF;
        public const uint TurnOff27Bits       = 0x000000FF;
        public const uint TurnOffSessionBits  = 0xF0FFFFFF;
        public const uint TradingSessionBits  = 0x0F000000;
        public const uint IsSelectedMask      = 0x0F000000;
        public const uint TaSignalBitsMask    = 0xFFFFFF00;

        public const int POS_SYM_TIME        = 1;
        public const int POS_SYM_TYPE        = 5;
        public const int POS_SYM_OFFERID     = 9;

        public const int POS_BAR_STATE        =  1;
        public const int POS_IsSelected       =  2;
        public const int POS_SelectionChanged =  3;
        public const int POS_BAR_SESSION      =  4;

        public const int POS_TASIGNAL_BEGIN   =  8;
        public const int POS_BOTTOMING_SIGNAL =  POS_TASIGNAL_BEGIN + 1;
        public const int POS_TOPPING_SIGNAL   =  POS_TASIGNAL_BEGIN + 2;
        public const int POS_DIVERGENCE       =  POS_TASIGNAL_BEGIN + 3;
        public const int POS_TIME_ROTATION    =  POS_TASIGNAL_BEGIN + 4;
        public const int POS_GANN_SQUARE      =  POS_TASIGNAL_BEGIN + 5;
        public const int POS_PIVOT_RELATION   =  POS_TASIGNAL_BEGIN + 6;
        public const int POS_WAVE_TROUGH      =  POS_TASIGNAL_BEGIN + 7;
        public const int POS_GANN_TROUGH      =  POS_TASIGNAL_BEGIN + 8;
        public const int POS_WAVE_PEAK        =  POS_TASIGNAL_BEGIN + 9;
        public const int POS_GANN_PEAK        =  POS_TASIGNAL_BEGIN + 10;
        public const int POS_STRUCT_LABEL     =  POS_TASIGNAL_BEGIN + 11;
        public const int POS_ELLIOTT_WAVE     =  POS_TASIGNAL_BEGIN + 12;
        public const int POS_CANDLEPATTERN    =  POS_TASIGNAL_BEGIN + 13;
        public const int POS_ExitOverSold     =  POS_TASIGNAL_BEGIN + 14;
        public const int POS_ExitOverBought   =  POS_TASIGNAL_BEGIN + 15;
        public const int POS_FibRetExpInfo    =  POS_TASIGNAL_BEGIN + 16;
        public const int POS_ComasCrossUp     =  POS_TASIGNAL_BEGIN + 17;
        public const int POS_ComasCrossDown   =  POS_TASIGNAL_BEGIN + 18;
        public const int POS_ComasTurnUp      =  POS_TASIGNAL_BEGIN + 19;
        public const int POS_ComasTurnDown    =  POS_TASIGNAL_BEGIN + 20;        
        public const int POS_WaveDirty        =  POS_TASIGNAL_BEGIN + 21;
        public const int POS_BAZI             =  POS_TASIGNAL_BEGIN + 22;

        public const int POS_IsSpecialBar     =  30;
        


        public const int LENGTH_SIGNAL       =  24;
    }
    
}
