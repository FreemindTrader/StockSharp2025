﻿using System;
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
        public static readonly fxFibLevel[ ] TonyDiscoveryLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_109_2,    109.20f,                 FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevel( FibPercentage.Fib_127_2,    127.20f,                 FibonacciTargetType.InverseRetracement,  20 ),
            new fxFibLevel( FibPercentage.Fib_150,      150.00f,                 FibonacciTargetType.InverseRetracement,  40 ),
            new fxFibLevel( FibPercentage.Fib_161_8,    161.80f,                 FibonacciTargetType.InverseRetracement,  40 ),
            new fxFibLevel( FibPercentage.Fib_200,      200.00f,                 FibonacciTargetType.InverseRetracement,  40 ),
            new fxFibLevel( FibPercentage.Fib_209_2,    209.20f,                 FibonacciTargetType.InverseRetracement,  20 ),
            new fxFibLevel( FibPercentage.Fib_214_6,    214.60f,                 FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevel( FibPercentage.Fib_227_2,    227.20f,                 FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevel( FibPercentage.Fib_250,      250.00f,                 FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevel( FibPercentage.Fib_261_8,    261.80f,                 FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevel( FibPercentage.Fib_276_4,    276.40f,                 FibonacciTargetType.InverseRetracement,  5 ),
            new fxFibLevel( FibPercentage.Fib_285_4,    285.40f,                 FibonacciTargetType.InverseRetracement,  5 ),
            new fxFibLevel( FibPercentage.Fib_290_2,    290.20f,                 FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevel( FibPercentage.Fib_294_4,    294.40f,                 FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevel( FibPercentage.Fib_298_4,    298.40f,                 FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevel( FibPercentage.Fib_300,      300.00f,                 FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevel( FibPercentage.Fib_327_2,    327.20f,                 FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevel( FibPercentage.Fib_350,      350.00f,                 FibonacciTargetType.InverseRetracement,  2 ),
            new fxFibLevel( FibPercentage.Fib_361_8,    361.80f,                 FibonacciTargetType.InverseRetracement,  10 ),
            new fxFibLevel( FibPercentage.Fib_400,      400.00f,                 FibonacciTargetType.InverseRetracement,  2 ),

        };

        public static readonly fxFibLevel[ ] Wave5ProjectionFibLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_33_3,     33.3f,                   FibonacciTargetType.MinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_38_2,     38.2f,                   FibonacciTargetType.AroundMinimumTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_41_4,     41.4f,                   FibonacciTargetType.AverageTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_50,       50f,                     FibonacciTargetType.AverageTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_61_8,     61.8f,                   FibonacciTargetType.StrongTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_66_66,    66.7f,                   FibonacciTargetType.AroundStrongTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_76_4,     76.4f,                   FibonacciTargetType.AroundStrongTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_85_4,     85.4f,                   FibonacciTargetType.ExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_90_02,    90.02f,                  FibonacciTargetType.ExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_94_4,     94.4f,                   FibonacciTargetType.ExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_4,     98.4f,                   FibonacciTargetType.AroundExtremeTarget,  5 ),
            new fxFibLevel( FibPercentage.Fib_98_68,    98.68f,                  FibonacciTargetType.AroundExtremeTarget,  5 ),
            new fxFibLevel( FibPercentage.Fib_100,      100.0f,                  FibonacciTargetType.AroundExtremeTarget,  2 ),
            new fxFibLevel( FibPercentage.Fib_105_6,    105.6f,                  FibonacciTargetType.AroundExtremeTarget,  2 ),
        };

        public static readonly fxFibLevel[ ] Wave5CProjectionFibLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_50,       50f,                     FibonacciTargetType.Wave5C,  40 ),
            new fxFibLevel( FibPercentage.Fib_61_8,     61.8f,                   FibonacciTargetType.Wave5C,  40 ),
            new fxFibLevel( FibPercentage.Fib_66_66,    66.7f,                   FibonacciTargetType.Wave5C,  20 ),
            new fxFibLevel( FibPercentage.Fib_76_4,     76.4f,                   FibonacciTargetType.Wave5C,  10 ),
            new fxFibLevel( FibPercentage.Fib_85_4,     85.4f,                   FibonacciTargetType.Wave5C,  10 ),
            new fxFibLevel( FibPercentage.Fib_90_02,    90.02f,                  FibonacciTargetType.Wave5C,  10 ),
            new fxFibLevel( FibPercentage.Fib_94_4,     94.4f,                   FibonacciTargetType.Wave5C,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_4,     98.4f,                   FibonacciTargetType.Wave5C,  5 ),
            new fxFibLevel( FibPercentage.Fib_100,      100.0f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_105_6,    105.6f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_109_2,    109.2f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_114_6,    114.6f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_123_6,    123.6f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_127_2,    127.2f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_133_3,    133.3f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_138_2,    138.2f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_141_4,    141.4f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_150,      150.4f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_161_8,    161.8f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_166_7,    166.7f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_176_4,    176.4f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_185_4,    185.4f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_190_2,    190.2f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_194_4,    194.4f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_198_4,    198.4f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_200,      200.0f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_205_6,    205.6f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_209_2,    209.2f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_214_6,    214.6f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_223_6,    223.6f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_227_2,    227.2f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_238_2,    238.2f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_241_4,    241.4f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_242_7,    242.7f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_250,      250.0f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_261_8,    261.8f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_266_7,    266.7f,                  FibonacciTargetType.Wave5C,  2 ),
            new fxFibLevel( FibPercentage.Fib_276_4,    276.4f,                  FibonacciTargetType.Wave5C,  2 ),
            
        };
    }
}
