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
        public static readonly fxFibLevels[ ] WaveEFBRetracementLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_9_02,      9.02f,         FibonacciTargetType.WaveA,  5 ),
            new fxFibLevels( FibPercentage.Fib_14_6,      14.6f,         FibonacciTargetType.WaveA,  10 ),
            new fxFibLevels( FibPercentage.Fib_23_6,      23.6f,         FibonacciTargetType.WaveA,  10 ),
            new fxFibLevels( FibPercentage.Fib_30 ,       30.0f,         FibonacciTargetType.WaveA,  40 ),
            new fxFibLevels( FibPercentage.Fib_33_3,      33.3f,         FibonacciTargetType.WaveA,  45 ),
            new fxFibLevels( FibPercentage.Fib_38_2,      38.2f,         FibonacciTargetType.WaveA,  15 ),
            new fxFibLevels( FibPercentage.Fib_41_4,      41.4f,         FibonacciTargetType.WaveA,  20 ),
            new fxFibLevels( FibPercentage.Fib_50,        50f,           FibonacciTargetType.WaveA,  30 ),
            new fxFibLevels( FibPercentage.Fib_58_6,      58.6f,         FibonacciTargetType.WaveA,  30 ),
            new fxFibLevels( FibPercentage.Fib_61_8,      61.8f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_66_66,     66.66f,        FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_76_4,      76.4f,         FibonacciTargetType.WaveC,  40 ),
            new fxFibLevels( FibPercentage.Fib_85_4,      85.4f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_90_02,     90.2f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,      94.4f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_4,      98.4f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_68,     98.68f,        FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_100,        100.0f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_105_6,      105.6f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_109_2,      109.2f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_114_6,      114.6f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_123_6,      123.6f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_127_2,      127.2f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_133_3,      133.3f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_138_2,      138.2f,       FibonacciTargetType.WaveC,  10 ),
        };

        public static readonly fxFibLevels[ ] WaveEFCProjectionLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_76_4,      76.4f,         FibonacciTargetType.WaveEFC,  40 ),
            new fxFibLevels( FibPercentage.Fib_85_4,      85.4f,         FibonacciTargetType.WaveEFC,  30 ),
            new fxFibLevels( FibPercentage.Fib_90_02,     90.2f,         FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,      94.4f,         FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_4,      98.4f,         FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_68,     98.68f,        FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_100,        100.0f,       FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_105_6,      105.6f,       FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_109_2,      109.2f,       FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_114_6,      114.6f,       FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_123_6,      123.6f,       FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_127_2,      127.2f,       FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_133_3,      133.3f,       FibonacciTargetType.WaveEFC,  10 ),
            new fxFibLevels( FibPercentage.Fib_138_2,      138.2f,       FibonacciTargetType.WaveEFC,  10 ),
        };

        public static readonly fxFibLevels[ ] WaveTriBRetracementLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_58_6,      58.6f,         FibonacciTargetType.WaveA,  30 ),
            new fxFibLevels( FibPercentage.Fib_61_8,      61.8f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_66_66,     66.66f,        FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_76_4,      76.4f,         FibonacciTargetType.WaveC,  40 ),
            new fxFibLevels( FibPercentage.Fib_85_4,      85.4f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_90_02,     90.2f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,      94.4f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_4,      98.4f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_68,     98.68f,        FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_100,        100.0f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_105_6,      105.6f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_109_2,      109.2f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_114_6,      114.6f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_123_6,      123.6f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_127_2,      127.2f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_133_3,      133.3f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_138_2,      138.2f,       FibonacciTargetType.WaveC,  10 ),
        };

        public static readonly fxFibLevels[ ] WaveTriCRetracementLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_61_8,      61.8f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_66_66,     66.66f,        FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_76_4,      76.4f,         FibonacciTargetType.WaveC,  40 ),
            new fxFibLevels( FibPercentage.Fib_85_4,      85.4f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_90_02,     90.2f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,      94.4f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_4,      98.4f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_98_68,     98.68f,        FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_100,        100.0f,       FibonacciTargetType.WaveC,  10 ),            
        };

        public static readonly fxFibLevels[ ] WaveTriDRetracementLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_61_8,      61.8f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_66_66,     66.66f,        FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_76_4,      76.4f,         FibonacciTargetType.WaveC,  40 ),
            new fxFibLevels( FibPercentage.Fib_85_4,      85.4f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_90_02,     90.2f,         FibonacciTargetType.WaveC,  10 ),
            new fxFibLevels( FibPercentage.Fib_94_4,      94.4f,         FibonacciTargetType.WaveC,  10 ),            
        };

        public static readonly fxFibLevels[ ] WaveTriERetracementLevels = new fxFibLevels[ ]
        {
            new fxFibLevels( FibPercentage.Fib_38_2,      38.2f,         FibonacciTargetType.WaveA,  15 ),
            new fxFibLevels( FibPercentage.Fib_41_4,      41.4f,         FibonacciTargetType.WaveA,  20 ),
            new fxFibLevels( FibPercentage.Fib_50,        50f,           FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_58_6,      58.6f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_61_8,      61.8f,         FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_66_66,     66.66f,        FibonacciTargetType.WaveC,  30 ),
            new fxFibLevels( FibPercentage.Fib_76_4,      76.4f,         FibonacciTargetType.WaveC,  40 ),
            new fxFibLevels( FibPercentage.Fib_85_4,      85.4f,         FibonacciTargetType.WaveC,  30 ),            
        };
    }
}
