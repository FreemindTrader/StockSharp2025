using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{

    [Flags]

    public enum WavePattern : long
    {
        UNKNOWN                         = 0,
        TrendingImpulse                 = 2L <<  0,
        TerminalImpulse                 = 2L <<  1,
        ZigZag                          = 2L <<  2,
        DoubleZigZag                    = 2L <<  3,
        TripleZigZag                    = 2L <<  4,
        ElongatedZigZag                 = 2L <<  5,                       

        Flat                            = 2L <<  6,
        DoubleFlat                      = 2L <<  7,
        TripleFlat                      = 2L <<  8,        
        ElongatedFlat                   = 2L <<  9,
        ExpandedFlat                    = 2L <<  10,
        RunningFlat                     = 2L <<  11,

        ContractingTriangle             = 2L <<  12,
        RunningTriangle                 = 2L <<  13,
        IrregularTriangle               = 2L <<  14,
        ExpandingTriangle               = 2L <<  15,

        DoubleThree                     = 2L <<  16,
        TripleThree                     = 2L <<  17,

        RunningCorrection               = 2L <<  18,

        DoubleCombination               = 2L <<  19,
        TripleCombination               = 2L <<  20,
        BFailure                        = 2L <<  21,
        Common                          = 2L <<  22,
        Irregular                       = 2L <<  23,
        CFailureFlat                    = 2L <<  24,
        IrregularFailureFlat            = 2L <<  25,                        
        DoubleThreeRunningCorrection    = 2L <<  26,
        TripleThreeRunningCorrect       = 2L <<  27,


        AnyCorrectivePattern            = 2L <<  28,
        AnyComplexCorrection            = 2L <<  29,
        MoreThanOneEWPatternEnd         = 2L <<  30,

        MissingXWaves                   = 2L <<  31,
        MissingBWaves                   = 2L <<  32,
        FailureImpulse5th               = 2L <<  33,
        TerminalPattern                 = 2L <<  34,


        TerminateMultiDegPattern        = 2L <<  63,

    }

    public enum WavePosition
    {
        Unknown = 0,
        Begin   = 1,
        Center  = 2,
        End     = 3
    }
    
}
