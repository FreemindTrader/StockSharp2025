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
        public static readonly fxFibLevels[ ] ABCWaveCProjectionFibLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_50,      50f,            FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_61_8,    61.8f,          FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_66_66,   66.7f,          FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_76_4,    76.4f,          FibonacciTargetType.WaveC,  10 ),

            new fxFibLevels( FibPercentage.Fib_138_2,   138.20f,        FibonacciTargetType.WaveC,   90 ),
            new fxFibLevels( FibPercentage.Fib_141_4,   141.40f,        FibonacciTargetType.WaveC,   20 ),
            new fxFibLevels( FibPercentage.Fib_150,     150.00f,        FibonacciTargetType.WaveC,   20 ),
            new fxFibLevels( FibPercentage.Fib_161_8,   161.8f,         FibonacciTargetType.WaveC,   20 ),
            new fxFibLevels( FibPercentage.Fib_166_7,   166.70f,        FibonacciTargetType.WaveC,   20 ),
            new fxFibLevels( FibPercentage.Fib_176_4,   176.40f,        FibonacciTargetType.WaveC,   90 ),

            new fxFibLevels( FibPercentage.Fib_176_4,   176.40f,        FibonacciTargetType.WaveC, 20 ),
            new fxFibLevels( FibPercentage.Fib_185_4,   185.40f,        FibonacciTargetType.WaveC, 20 ),
            new fxFibLevels( FibPercentage.Fib_190_2,   190.20f,        FibonacciTargetType.WaveC, 20 ),
            new fxFibLevels( FibPercentage.Fib_198_4,   198.40f,        FibonacciTargetType.WaveC, 20 ),
            new fxFibLevels( FibPercentage.Fib_200,     200.0f,         FibonacciTargetType.WaveC, 20 ),
            new fxFibLevels( FibPercentage.Fib_205_6,   205.6f,         FibonacciTargetType.WaveC, 20 ),
            new fxFibLevels( FibPercentage.Fib_209_2,   209.20f,        FibonacciTargetType.WaveC, 20 ),
            new fxFibLevels( FibPercentage.Fib_214_6,   214.6f,         FibonacciTargetType.WaveC, 20 ),
            new fxFibLevels( FibPercentage.Fib_223_6,   223.60f,        FibonacciTargetType.WaveC, 90 ),
            new fxFibLevels( FibPercentage.Fib_227_2,   227.2f,         FibonacciTargetType.WaveC, 75 ),
            new fxFibLevels( FibPercentage.Fib_238_2,   238.2f,         FibonacciTargetType.WaveC, 75 ),
            new fxFibLevels( FibPercentage.Fib_241_4,   241.4f,         FibonacciTargetType.WaveC, 75 ),


        };

        public static readonly fxFibLevels[ ] ABCWaveBRetracementFibLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_9_02,      9.02f,        FibonacciTargetType.WaveA,  5 ),
            new fxFibLevels( FibPercentage.Fib_14_6,      14.6f,        FibonacciTargetType.WaveA,  10 ),
            new fxFibLevels( FibPercentage.Fib_23_6,      23.6f,        FibonacciTargetType.WaveA,  10 ),
            new fxFibLevels( FibPercentage.Fib_30 ,       30.0f,        FibonacciTargetType.WaveA,  40 ),
            new fxFibLevels( FibPercentage.Fib_33_3,      33.3f,        FibonacciTargetType.WaveA,  45 ),
            new fxFibLevels( FibPercentage.Fib_38_2,      38.2f,        FibonacciTargetType.WaveA,  15 ),
            new fxFibLevels( FibPercentage.Fib_41_4,      41.4f,        FibonacciTargetType.WaveA,  20 ),
            new fxFibLevels( FibPercentage.Fib_50,        50f,          FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_58_6,      58.6f,        FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_61_8,      61.8f,        FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_66_66,     66.66f,       FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_76_4,      76.4f,        FibonacciTargetType.WaveC,  40 ),
            new fxFibLevels( FibPercentage.Fib_85_4,      85.4f,        FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_90_02,     90.2f,        FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,      94.4f,        FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_4,      98.4f,        FibonacciTargetType.WaveC,  10 ),
        };

        public static readonly fxFibLevels[ ] FirstXProjectionLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_76_4,    76.4f,          FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_90_02,   90.02f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,    94.40f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_4,    98.4f,          FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_68,   98.68f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_100,     100.0f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_105_6,   105.6f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_109_2,   109.2f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_114_6,   114.6f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevels( FibPercentage.Fib_123_6,   123.6f,         FibonacciTargetType.WaveXYZ,  10 ),

            new fxFibLevels( FibPercentage.Fib_138_2,   138.20f,        FibonacciTargetType.WaveXYZ,   90 ),
            new fxFibLevels( FibPercentage.Fib_141_4,   141.40f,        FibonacciTargetType.WaveXYZ,   20 ),
            new fxFibLevels( FibPercentage.Fib_150,     150.00f,        FibonacciTargetType.WaveXYZ,   20 ),
            new fxFibLevels( FibPercentage.Fib_161_8,   161.8f,         FibonacciTargetType.WaveXYZ,   20 ),
            new fxFibLevels( FibPercentage.Fib_166_7,   166.70f,        FibonacciTargetType.WaveXYZ,   20 ),
            
            new fxFibLevels( FibPercentage.Fib_176_4,   176.40f,        FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevels( FibPercentage.Fib_185_4,   185.40f,        FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevels( FibPercentage.Fib_190_2,   190.20f,        FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevels( FibPercentage.Fib_198_4,   198.40f,        FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevels( FibPercentage.Fib_200,     200.0f,         FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevels( FibPercentage.Fib_214_6,   214.6f,         FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevels( FibPercentage.Fib_223_6,   223.60f,        FibonacciTargetType.WaveXYZ, 90 ),
            new fxFibLevels( FibPercentage.Fib_238_2,   238.2f,         FibonacciTargetType.WaveXYZ, 75 ),
            new fxFibLevels( FibPercentage.Fib_241_4,   241.4f,         FibonacciTargetType.WaveXYZ, 75 ),

        };
    }
}
