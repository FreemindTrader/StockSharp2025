using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using fx.Definitions.Messages;
using fx.Collections;

namespace fx.Definitions
{
    public static class Wave3Compressed
    {
        public static readonly fxFibLevels[ ] CompressWave3 = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_50,      50f,      FibonacciTargetType.Wave3B,  10 ),
            new fxFibLevels( FibPercentage.Fib_61_8,    61.8f,    FibonacciTargetType.Wave3B,  10 ),
            new fxFibLevels( FibPercentage.Fib_66_66,   66.7f,    FibonacciTargetType.Wave3B,  10 ),
            new fxFibLevels( FibPercentage.Fib_76_4,    76.4f,    FibonacciTargetType.Wave3B,  10 ),

            new fxFibLevels( FibPercentage.Fib_90_02,   90.02f,   FibonacciTargetType.Wave3A,        75 ),
            new fxFibLevels( FibPercentage.Fib_94_4,    94.40f,   FibonacciTargetType.Wave3A,        140 ),
            new fxFibLevels( FibPercentage.Fib_98_4,    98.40f,   FibonacciTargetType.Wave3A,        205 ),
            new fxFibLevels( FibPercentage.Fib_98_68,   98.68,    FibonacciTargetType.Wave3A,        75 ),
            new fxFibLevels( FibPercentage.Fib_100,     100.00f,  FibonacciTargetType.Wave3A,  75 ),
            new fxFibLevels( FibPercentage.Fib_105_6,   105.6f,   FibonacciTargetType.Wave3A,  75 ),
            new fxFibLevels( FibPercentage.Fib_109_2,   109.2f,   FibonacciTargetType.Wave3A,  75 ),

            new fxFibLevels( FibPercentage.Fib_123_6,   123.60f,  FibonacciTargetType.Wave3A_Wave4,        75 ),
            new fxFibLevels( FibPercentage.Fib_133_3,   133.30f,  FibonacciTargetType.Wave3A_Wave4,        75 ),
            new fxFibLevels( FibPercentage.Fib_138_2,   138.20f,  FibonacciTargetType.Wave3A_Wave4,        75 ),
            new fxFibLevels( FibPercentage.Fib_141_4,   141.40f,  FibonacciTargetType.Wave3A_Wave4,        75 ),
            new fxFibLevels( FibPercentage.Fib_150,     150.00f,  FibonacciTargetType.Wave3A_Wave4,        75 ),



            new fxFibLevels( FibPercentage.Fib_176_4,   176.40f,  FibonacciTargetType.Wave5, 75 ),
            new fxFibLevels( FibPercentage.Fib_185_4,   185.40f,  FibonacciTargetType.Wave5, 75 ),
            new fxFibLevels( FibPercentage.Fib_190_2,   190.20f,  FibonacciTargetType.Wave5, 75 ),
            new fxFibLevels( FibPercentage.Fib_194_4,   194.4f,   FibonacciTargetType.Wave5, 75 ),
            new fxFibLevels( FibPercentage.Fib_198_4,   198.4f,   FibonacciTargetType.Wave5, 75 ),
            new fxFibLevels( FibPercentage.Fib_200,     200.0f,   FibonacciTargetType.Wave5, 75 ),
            
        };
    }
}

