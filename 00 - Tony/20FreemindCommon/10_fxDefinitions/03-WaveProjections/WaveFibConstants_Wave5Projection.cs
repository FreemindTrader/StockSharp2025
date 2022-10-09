using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using fx.Definitions.Messages;
using fx.Collections;

namespace fx.Definitions
{
    public static partial class WaveFibConstants
    {
        public static readonly fxFibLevels[ ] TonyDiscoveryLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_109_2,    109.20f,             FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevels( FibPercentage.Fib_127_2,    127.20f,             FibonacciTargetType.InverseRetracement,  20 ),
            new fxFibLevels( FibPercentage.Fib_150,    150.00f,               FibonacciTargetType.InverseRetracement,  40 ),
            new fxFibLevels( FibPercentage.Fib_161_8,      161.80f,           FibonacciTargetType.InverseRetracement,  40 ),
            new fxFibLevels( FibPercentage.Fib_200,    200.00f,               FibonacciTargetType.InverseRetracement,  40 ),
            new fxFibLevels( FibPercentage.Fib_209_2,   209.20f,              FibonacciTargetType.InverseRetracement,  20 ),
            new fxFibLevels( FibPercentage.Fib_214_6,    214.60f,             FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevels( FibPercentage.Fib_227_2,    227.20f,             FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevels( FibPercentage.Fib_250,    250.00f,               FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevels( FibPercentage.Fib_261_8,    261.80f,             FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevels( FibPercentage.Fib_276_4,    276.40f,             FibonacciTargetType.InverseRetracement,  5 ),
            new fxFibLevels( FibPercentage.Fib_285_4,   285.40f,              FibonacciTargetType.InverseRetracement,  5 ),
            new fxFibLevels( FibPercentage.Fib_290_2,    290.20f,             FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevels( FibPercentage.Fib_294_4,  294.40f,               FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevels( FibPercentage.Fib_298_4,  298.40f,               FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevels( FibPercentage.Fib_300,    300.00f,               FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevels( FibPercentage.Fib_327_2,  327.20f,               FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevels( FibPercentage.Fib_350,  350.00f,                 FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevels( FibPercentage.Fib_361_8,    361.80f,             FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevels( FibPercentage.Fib_400,    400.00f,               FibonacciTargetType.InverseRetracement,  2 ),

        };

        public static readonly fxFibLevels[ ] Wave5ProjectionFibLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_33_3,    33.3f,         FibonacciTargetType.Wave5,  10 ),
            new fxFibLevels( FibPercentage.Fib_38_2,    38.2f,         FibonacciTargetType.Wave5,  20 ),
            new fxFibLevels( FibPercentage.Fib_41_4,    41.4f,         FibonacciTargetType.Wave5,  40 ),
            new fxFibLevels( FibPercentage.Fib_50,      50f,           FibonacciTargetType.Wave5,  40 ),
            new fxFibLevels( FibPercentage.Fib_61_8,    61.8f,         FibonacciTargetType.Wave5,  40 ),
            new fxFibLevels( FibPercentage.Fib_66_66,   66.7f,         FibonacciTargetType.Wave5,  20 ),
            new fxFibLevels( FibPercentage.Fib_76_4,    76.4f,         FibonacciTargetType.Wave5,  10 ),
            new fxFibLevels( FibPercentage.Fib_85_4,    85.4f,         FibonacciTargetType.Wave5,  10 ),
            new fxFibLevels( FibPercentage.Fib_90_02,    90.02f,       FibonacciTargetType.Wave5,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,    94.4f,         FibonacciTargetType.Wave5,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_4,    98.4f,         FibonacciTargetType.Wave5,  5 ),
            new fxFibLevels( FibPercentage.Fib_98_68,   98.68f,        FibonacciTargetType.Wave5,  5 ),
            new fxFibLevels( FibPercentage.Fib_100,    100.0f,         FibonacciTargetType.Wave5,  2 ),
            new fxFibLevels( FibPercentage.Fib_105_6,  105.6f,         FibonacciTargetType.Wave5,  2 ),
        };

        public static readonly fxFibLevels[ ] Wave5CProjectionFibLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_50,      50f,             FibonacciTargetType.Wave5C,  40 ),
            new fxFibLevels( FibPercentage.Fib_61_8,    61.8f,           FibonacciTargetType.Wave5C,  40 ),
            new fxFibLevels( FibPercentage.Fib_66_66,   66.7f,           FibonacciTargetType.Wave5C,  20 ),
            new fxFibLevels( FibPercentage.Fib_76_4,    76.4f,           FibonacciTargetType.Wave5C,  10 ),
            new fxFibLevels( FibPercentage.Fib_85_4,    85.4f,           FibonacciTargetType.Wave5C,  10 ),
            new fxFibLevels( FibPercentage.Fib_90_02,    90.02f,         FibonacciTargetType.Wave5C,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,    94.4f,           FibonacciTargetType.Wave5C,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_4,    98.4f,           FibonacciTargetType.Wave5C,  5 ),
            new fxFibLevels( FibPercentage.Fib_100,    100.0f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_105_6,  105.6f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_109_2,  109.2f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_114_6,  114.6f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_123_6,  123.6f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_127_2,  127.2f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_133_3,  133.3f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_138_2,  138.2f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_141_4,  141.4f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_150,  150.4f,             FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_161_8,  161.8f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_166_7,  166.7f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_176_4,  176.4f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_185_4,  185.4f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_190_2,  190.2f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_194_4,  194.4f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_198_4,  198.4f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_200,    200.0f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_205_6,  205.6f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_209_2,  209.2f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_214_6,  214.6f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_223_6,  223.6f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_227_2,  227.2f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_238_2,  238.2f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_241_4,  241.4f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_242_7,  242.7f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_250,    250.0f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_261_8,  261.8f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_266_7,  266.7f,           FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevels( FibPercentage.Fib_276_4,  276.4f,           FibonacciTargetType.Wave5C,  2 ),
            
        };
    }
}
