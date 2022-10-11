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
        public static readonly fxFibLevel[ ] Wave2RetracementLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_9_02,      9.02f,       FibonacciTargetType.AroundMinimumTarget,  5 ),
            new fxFibLevel( FibPercentage.Fib_14_6,      14.6f,       FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_23_6,      23.6f,       FibonacciTargetType.AroundWaveA,  10 ),
            new fxFibLevel( FibPercentage.Fib_30 ,       30.0f,       FibonacciTargetType.MinimumTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_33_3,      33.3f,       FibonacciTargetType.MinimumTarget,  45 ),
            new fxFibLevel( FibPercentage.Fib_38_2,      38.2f,       FibonacciTargetType.AroundMinimumTarget,  15 ),
            new fxFibLevel( FibPercentage.Fib_41_4,      41.4f,       FibonacciTargetType.AroundAvgTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_50,        50f,         FibonacciTargetType.AverageTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_58_6,      58.6f,       FibonacciTargetType.AroundAvgTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_61_8,      61.8f,       FibonacciTargetType.AroundStrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_66_66,     66.66f,      FibonacciTargetType.StrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_76_4,      76.4f,       FibonacciTargetType.StrongTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_85_4,      85.4f,       FibonacciTargetType.AroundStrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_90_02,     90.2f,       FibonacciTargetType.ExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_94_4,      94.4f,       FibonacciTargetType.ExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_4,      98.4f,       FibonacciTargetType.ExtremeTarget,  10 ),
        };

        public static readonly fxFibLevel[ ] Wave3BRetracementLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_9_02,      9.02f,       FibonacciTargetType.AroundMinimumTarget,  5 ),
            new fxFibLevel( FibPercentage.Fib_14_6,      14.6f,       FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_23_6,      23.6f,       FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_30 ,       30.0f,       FibonacciTargetType.MinimumTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_33_3,      33.3f,       FibonacciTargetType.MinimumTarget,  45 ),
            new fxFibLevel( FibPercentage.Fib_38_2,      38.2f,       FibonacciTargetType.AroundMinimumTarget,  15 ),
            new fxFibLevel( FibPercentage.Fib_41_4,      41.4f,       FibonacciTargetType.AverageTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_44_1,      44.1f,       FibonacciTargetType.AroundAvgTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_50,        50f,         FibonacciTargetType.StrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_58_6,      58.6f,       FibonacciTargetType.AroundStrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_61_8,      61.8f,       FibonacciTargetType.AroundStrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_66_66,     66.66f,      FibonacciTargetType.ExtremeTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_76_4,      76.4f,       FibonacciTargetType.ExtremeTarget,  40 ),            
        };

        public static readonly fxFibLevel[ ] Wave5BRetracementLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_9_02,      9.02f,       FibonacciTargetType.AroundMinimumTarget,  5 ),
            new fxFibLevel( FibPercentage.Fib_14_6,      14.6f,       FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_23_6,      23.6f,       FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_30 ,       30.0f,       FibonacciTargetType.MinimumTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_33_3,      33.3f,       FibonacciTargetType.MinimumTarget,  45 ),
            new fxFibLevel( FibPercentage.Fib_38_2,      38.2f,       FibonacciTargetType.AroundMinimumTarget,  15 ),
            new fxFibLevel( FibPercentage.Fib_41_4,      41.4f,       FibonacciTargetType.AverageTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_44_1,      44.1f,       FibonacciTargetType.AroundAvgTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_50,        50f,         FibonacciTargetType.StrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_58_6,      58.6f,       FibonacciTargetType.AroundStrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_61_8,      61.8f,       FibonacciTargetType.AroundStrongTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_66_66,     66.66f,      FibonacciTargetType.ExtremeTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_76_4,      76.4f,       FibonacciTargetType.ExtremeTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_85_4,      85.4f,       FibonacciTargetType.AroundExtremeTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_90_02,     90.2f,       FibonacciTargetType.AroundExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_94_4,      94.4f,       FibonacciTargetType.AroundExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_4,      98.4f,       FibonacciTargetType.AroundExtremeTarget,  10 ),
        };


        public static readonly fxFibLevel[ ] Wave4RetracementLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_9_02,      9.02f,       FibonacciTargetType.AroundMinimumTarget,  5 ),
            new fxFibLevel( FibPercentage.Fib_14_6,      14.6f,       FibonacciTargetType.AroundMinimumTarget,  5 ),
            new fxFibLevel( FibPercentage.Fib_23_6,      23.6f,       FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_30,        30.0f,       FibonacciTargetType.MinimumTarget,  15 ),
            new fxFibLevel( FibPercentage.Fib_33_3,      33.3f,       FibonacciTargetType.AroundMinimumTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_38_2,      38.2f,       FibonacciTargetType.AroundAvgTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_41_4,      41.4f,       FibonacciTargetType.AverageTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_44_1,      44.1f,       FibonacciTargetType.AroundAvgTarget,  40 ),
            new fxFibLevel( FibPercentage.Fib_50,        50.0f,       FibonacciTargetType.AverageTarget,  30 ),
            new fxFibLevel( FibPercentage.Fib_58_6,      58.6f,       FibonacciTargetType.StrongTarget,  20 ),
            new fxFibLevel( FibPercentage.Fib_61_8,      61.8f,       FibonacciTargetType.AroundStrongTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_66_66,     66.66f,      FibonacciTargetType.ExtremeTarget,  10 ),
            new fxFibLevel( FibPercentage.Fib_76_4,      76.4f,       FibonacciTargetType.ExtremeTarget,  5 ),

        };

        public static readonly fxFibLevel[ ] WaveXRetracementLevels = new fxFibLevel[ ]
        {            
            new fxFibLevel( FibPercentage.Fib_30,        30.0f,       FibonacciTargetType.WaveX,  40 ),
            new fxFibLevel( FibPercentage.Fib_33_3,      33.3f,       FibonacciTargetType.WaveX,  10 ),
            new fxFibLevel( FibPercentage.Fib_38_2,      38.2f,       FibonacciTargetType.WaveX,  20 ),
            new fxFibLevel( FibPercentage.Fib_41_4,      41.4f,       FibonacciTargetType.WaveX,  10 ),
            new fxFibLevel( FibPercentage.Fib_44_1,      44.1f,       FibonacciTargetType.WaveX,  40 ),
            new fxFibLevel( FibPercentage.Fib_50,        50.0f,       FibonacciTargetType.WaveX,  40 ),
            new fxFibLevel( FibPercentage.Fib_58_6,      58.6f,       FibonacciTargetType.WaveX,  40 ),
            new fxFibLevel( FibPercentage.Fib_61_8,      61.8f,       FibonacciTargetType.WaveX,  40 ),
            new fxFibLevel( FibPercentage.Fib_66_66,     66.66f,      FibonacciTargetType.WaveX,  10 ),
            new fxFibLevel( FibPercentage.Fib_76_4,      76.4f,       FibonacciTargetType.WaveX,  5 ),

        };

        public static readonly fxFibLevel[ ] ABCWaveBRetracementLevels = new fxFibLevel[ ]
        {
            new fxFibLevel( FibPercentage.Fib_9_02,      9.02f,       FibonacciTargetType.WaveA,  0 ),
            new fxFibLevel( FibPercentage.Fib_14_6,      14.6f,       FibonacciTargetType.WaveA,  0 ),
            new fxFibLevel( FibPercentage.Fib_23_6,      23.6f,       FibonacciTargetType.WaveA,  10 ),
            new fxFibLevel( FibPercentage.Fib_30 ,       30.0f,       FibonacciTargetType.WaveA,  40 ),
            new fxFibLevel( FibPercentage.Fib_33_3,      33.3f,       FibonacciTargetType.WaveA,  0 ),
            new fxFibLevel( FibPercentage.Fib_38_2,      38.2f,       FibonacciTargetType.WaveA,  5 ),
            new fxFibLevel( FibPercentage.Fib_41_4,      41.4f,       FibonacciTargetType.WaveA,  10 ),
            new fxFibLevel( FibPercentage.Fib_44_1,      44.1f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevel( FibPercentage.Fib_50,        50.0f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevel( FibPercentage.Fib_58_6,      58.6f,       FibonacciTargetType.WaveC,  20 ),
            new fxFibLevel( FibPercentage.Fib_61_8,      61.8f,       FibonacciTargetType.WaveC,  20 ),
            new fxFibLevel( FibPercentage.Fib_66_66,     66.66f,      FibonacciTargetType.WaveC,  20 ),
            new fxFibLevel( FibPercentage.Fib_76_4,      76.4f,       FibonacciTargetType.WaveC,  30 ),
            new fxFibLevel( FibPercentage.Fib_85_4,      85.4f,       FibonacciTargetType.WaveC,  30 ),
            new fxFibLevel( FibPercentage.Fib_90_02,     90.2f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevel( FibPercentage.Fib_94_4,      94.4f,       FibonacciTargetType.WaveC,  10 ),
            new fxFibLevel( FibPercentage.Fib_98_4,      98.4f,       FibonacciTargetType.WaveC,  10 ),

        };
    }
}
