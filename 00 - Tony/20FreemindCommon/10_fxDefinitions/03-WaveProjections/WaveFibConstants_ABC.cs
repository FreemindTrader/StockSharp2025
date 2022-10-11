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
        public static readonly fxFibLevel[ ] ABCWaveCProjectionFibLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_76_4,    76.4f,           FibonacciTargetType.MinimumTarget,  90 ),
            new fxFibLevel( FibPercentage.Fib_85_4,    85.4f,           FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_90_02,   90.02f,          FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_94_4,    94.40f,          FibonacciTargetType.AroundAvgTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_4,    98.4f,           FibonacciTargetType.AroundAvgTarget,  10 ),            
            new fxFibLevel( FibPercentage.Fib_100,     100.0f,          FibonacciTargetType.AverageTarget,  90 ),
            new fxFibLevel( FibPercentage.Fib_105_6,   105.6f,          FibonacciTargetType.AroundAvgTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_109_2,   109.2f,          FibonacciTargetType.AverageTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_114_6,   114.6f,          FibonacciTargetType.AroundAvgTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_123_6,   123.6f,          FibonacciTargetType.AroundAvgTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_138_2,   138.20f,         FibonacciTargetType.AverageTarget,  90 ),
            new fxFibLevel( FibPercentage.Fib_141_4,   141.40f,         FibonacciTargetType.AverageTarget,  90 ),
            new fxFibLevel( FibPercentage.Fib_150,     150.00f,         FibonacciTargetType.AroundAvgTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_161_8,   161.8f,          FibonacciTargetType.AroundStrongTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_166_7,   166.70f,         FibonacciTargetType.AroundStrongTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_176_4,   176.40f,         FibonacciTargetType.StrongTarget,  90 ),
            new fxFibLevel( FibPercentage.Fib_185_4,   185.40f,         FibonacciTargetType.StrongTarget, 90 ),
            new fxFibLevel( FibPercentage.Fib_190_2,   190.20f,         FibonacciTargetType.AroundStrongTarget, 20 ),
            new fxFibLevel( FibPercentage.Fib_198_4,   198.40f,         FibonacciTargetType.AroundStrongTarget, 20 ),
            new fxFibLevel( FibPercentage.Fib_200,     200.0f,          FibonacciTargetType.AroundStrongTarget, 20 ),
            new fxFibLevel( FibPercentage.Fib_205_6,   205.6f,          FibonacciTargetType.AroundExtremeTarget, 20 ),
            new fxFibLevel( FibPercentage.Fib_209_2,   209.20f,         FibonacciTargetType.AroundExtremeTarget, 20 ),
            new fxFibLevel( FibPercentage.Fib_214_6,   214.6f,          FibonacciTargetType.AroundExtremeTarget, 20 ),
            new fxFibLevel( FibPercentage.Fib_223_6,   223.60f,         FibonacciTargetType.ExtremeTarget, 90 ),
            new fxFibLevel( FibPercentage.Fib_227_2,   227.2f,          FibonacciTargetType.ExtremeTarget, 75 ),
            new fxFibLevel( FibPercentage.Fib_238_2,   238.2f,          FibonacciTargetType.ExtremeTarget, 90 ),
            new fxFibLevel( FibPercentage.Fib_241_4,   241.4f,          FibonacciTargetType.AroundExtremeTarget, 20 ),


        };

        public static readonly fxFibLevel[ ] ABCWaveBRetracementFibLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_9_02,      9.02f,        FibonacciTargetType.AroundMinimumTarget,  5 ),
            new fxFibLevel( FibPercentage.Fib_14_6,      14.6f,        FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_23_6,      23.6f,        FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_30 ,       30.0f,        FibonacciTargetType.MinimumTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_33_3,      33.3f,        FibonacciTargetType.AroundMinimumTarget,  45 ),
            new fxFibLevel( FibPercentage.Fib_38_2,      38.2f,        FibonacciTargetType.AroundAvgTarget,  15 ),
            new fxFibLevel( FibPercentage.Fib_41_4,      41.4f,        FibonacciTargetType.AverageTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_44_1,      44.1f,        FibonacciTargetType.AverageTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_50,        50f,          FibonacciTargetType.AverageTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_58_6,      58.6f,        FibonacciTargetType.StrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_61_8,      61.8f,        FibonacciTargetType.StrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_66_66,     66.66f,       FibonacciTargetType.AroundStrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_76_4,      76.4f,        FibonacciTargetType.ExtremeTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_85_4,      85.4f,        FibonacciTargetType.AroundExtremeTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_90_02,     90.2f,        FibonacciTargetType.AroundExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_94_4,      94.4f,        FibonacciTargetType.AroundExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_4,      98.4f,        FibonacciTargetType.AroundExtremeTarget,  10 ),
        };

        public static readonly fxFibLevel[ ] FirstXProjectionLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_76_4,    76.4f,          FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_90_02,   90.02f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_94_4,    94.40f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_4,    98.4f,          FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_68,   98.68f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_100,     100.0f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_105_6,   105.6f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_109_2,   109.2f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_114_6,   114.6f,         FibonacciTargetType.WaveXYZ,  10 ),
            new fxFibLevel( FibPercentage.Fib_123_6,   123.6f,         FibonacciTargetType.WaveXYZ,  10 ),

            new fxFibLevel( FibPercentage.Fib_138_2,   138.20f,        FibonacciTargetType.WaveXYZ,   90 ),
            new fxFibLevel( FibPercentage.Fib_141_4,   141.40f,        FibonacciTargetType.WaveXYZ,   20 ),
            new fxFibLevel( FibPercentage.Fib_150,     150.00f,        FibonacciTargetType.WaveXYZ,   20 ),
            new fxFibLevel( FibPercentage.Fib_161_8,   161.8f,         FibonacciTargetType.WaveXYZ,   20 ),
            new fxFibLevel( FibPercentage.Fib_166_7,   166.70f,        FibonacciTargetType.WaveXYZ,   20 ),
            
            new fxFibLevel( FibPercentage.Fib_176_4,   176.40f,        FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevel( FibPercentage.Fib_185_4,   185.40f,        FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevel( FibPercentage.Fib_190_2,   190.20f,        FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevel( FibPercentage.Fib_198_4,   198.40f,        FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevel( FibPercentage.Fib_200,     200.0f,         FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevel( FibPercentage.Fib_214_6,   214.6f,         FibonacciTargetType.WaveXYZ, 20 ),
            new fxFibLevel( FibPercentage.Fib_223_6,   223.60f,        FibonacciTargetType.WaveXYZ, 90 ),
            new fxFibLevel( FibPercentage.Fib_238_2,   238.2f,         FibonacciTargetType.WaveXYZ, 75 ),
            new fxFibLevel( FibPercentage.Fib_241_4,   241.4f,         FibonacciTargetType.WaveXYZ, 75 ),

        };
    }
}
