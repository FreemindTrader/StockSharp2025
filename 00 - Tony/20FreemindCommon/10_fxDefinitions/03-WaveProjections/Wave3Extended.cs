using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using fx.Definitions.Messages;
using fx.Collections;

namespace fx.Definitions
{
    public static class Wave3Extended
    {
        public static readonly fxFibLevels[ ] ExtendedWave3 = new fxFibLevels[ ]
        {            
            new fxFibLevels( FibPercentage.Fib_100,     100.00f,  FibonacciTargetType.Wave3B,  10 ),
            new fxFibLevels( FibPercentage.Fib_109_2,   109.20f,  FibonacciTargetType.Wave3B,  75 ),
            
            new fxFibLevels( FibPercentage.Fib_123_6,   123.60f,  FibonacciTargetType.Wave3A,  75 ),
            new fxFibLevels( FibPercentage.Fib_138_2,   138.20f,  FibonacciTargetType.Wave3A,  75 ),
            new fxFibLevels( FibPercentage.Fib_141_4,   141.4f,   FibonacciTargetType.Wave4,   75 ),

            
            new fxFibLevels( FibPercentage.Fib_223_6,   223.60f,  FibonacciTargetType.Wave3C,  75 ),
            new fxFibLevels( FibPercentage.Fib_238_2,   238.20f,  FibonacciTargetType.Wave3C,  75 ),

            new fxFibLevels( FibPercentage.Fib_285_4,   285.40f,  FibonacciTargetType.Wave5,   75 ),
            new fxFibLevels( FibPercentage.Fib_290_2,   290.2f,   FibonacciTargetType.Wave5,   75 ),
            new fxFibLevels( FibPercentage.Fib_298_4,   298.4f,   FibonacciTargetType.Wave5,   75 ),
            new fxFibLevels( FibPercentage.Fib_305_6,   305.6f,   FibonacciTargetType.Wave5,   75 ),
            new fxFibLevels( FibPercentage.Fib_314_6,   314.6f,   FibonacciTargetType.Wave5,   75 ),
            new fxFibLevels( FibPercentage.Fib_327_2,   323.6f,   FibonacciTargetType.Wave5,   75 ),

        };
    }
    public static partial class GlobalConstants
    {
        public static readonly float[ ] ExtendedWave3ClassicProjectionLevels = new float[ ]
        {
            50f,            // Wave 3B Retracement Level
            61.8f,          // Wave 3B Retracement Level
            66.7f,          // Wave 3B Retracement Level
            76.4f,          // Wave 3B Retracement Level
            90.02f,
            94.4f,
            98.68f,

            100.0f,
            105.6f,
            109.20f,
            114.6f,
            123.60f,
            138.20f,
            141.40f,
            150.00f,
            161.80f,
            166.70f,
            176.40f,        // Projection Cluster
            185.40f,
            190.20f,
            198.40f,

            200.0f,
            205.6f,
            209.20f,
            214.6f,
            223.60f,
            238.20f,
            241.40f,
            250.00f,
            261.80f,
            266.70f,
            276.40f,        // Projection Cluster
            285.40f,
            290.20f,
            298.40f,

            300.0f,
            305.6f,
            309.20f,
            314.6f,
            323.60f,
            338.20f,
            341.40f,
            350.00f,
            361.80f,
            366.70f,
            376.40f,        // Projection Cluster
            385.40f,
            390.20f,
            398.40f,

            400.0f,
            405.6f,
            409.20f,
            414.6f,
            423.60f,
            438.20f,
            441.40f,
            450.00f,
            461.80f,
            466.70f,
            476.40f,        // Projection Cluster
            485.40f,
            490.20f,
            498.40f,
        };

        public static readonly int[ ] ExtendedWave3ClassicProjectionStrength = new int[ ]
        {
            0,                      // 50
            0,                      // 61.8
            0,                      // 66.7
            0,                      // 76.4
            10,                     //90.02f,
            10,                     // 94.4f,
            10,                     // 98.68f,


            10,                     // 100.0f,
            10,                     // 105.6f,
            75,                     // 109.20
            75,                     // 114.6
            75,                     // 123.6
            10,                     // 138.2
            10,                     // 141.4
            10,                     // 150.00
            10,                     // 161.8
            10,                     // 166.7
            75,                     // 176.4
            10,                     // 185.40
            10,                     // 190.20
            85,                     // 198.40

            10,                     // 200.0f,
            10,                     // 205.6f,
            10,                     // 209.2f,
            85,                     //214.60
            85,                     // 223.60
            10,                     // 238.20
            85,                     // 241.40
            10,                     // 250.0
            10,                     // 261.80
            10,                     // 266.7
            10,                     // 276.40
            85,                     // 285.40
            10,                     // 290.00                                                                
            10,                     // 298.40

            10,                     // 300.0f,
            10,                     // 305.6f,
            75,                     // 309.20
            75,                     // 314.6
            75,                     // 323.6
            10,                     // 338.2
            10,                     // 341.4
            10,                     // 350.00
            10,                     // 361.8
            10,                     // 366.7
            75,                     // 376.4
            10,                     // 385.40
            10,                     // 390.20
            85,                     // 398.40

            10,                     // 400.0f,
            10,                     // 405.6f,
            10,                     // 409.2f,
            85,                     // 414.60
            85,                     // 423.60
            10,                     // 438.20
            85,                     // 441.40
            10,                     // 450.0
            10,                     // 461.80
            10,                     // 466.7
            10,                     // 476.40
            85,                     // 485.40
            10,                     // 490.00                                                                
            10,                     // 498.40

            10,                     // 500.0f,
            10,                     // 505.6f,
            75,                     // 509.20
            75,                     // 514.6
            75,                     // 523.6
            10,                     // 538.2
            10,                     // 541.4
            10,                     // 550.00
            10,                     // 561.8
            10,                     // 566.7
            75,                     // 576.4
            10,                     // 585.40
            10,                     // 590.20
            85,                     // 598.40
        };



        public static readonly FibPercentage[ ] ExtendedWave3FibLevelType = new FibPercentage[ ]
        {
            FibPercentage.Fib_50,
            FibPercentage.Fib_61_8,
            FibPercentage.Fib_66_66,
            FibPercentage.Fib_76_4,
            FibPercentage.Fib_90_02,
            FibPercentage.Fib_94_4,
            FibPercentage.Fib_98_68,

            FibPercentage.Fib_100,
            FibPercentage.Fib_105_6,
            FibPercentage.Fib_109_2,
            FibPercentage.Fib_114_6,
            FibPercentage.Fib_123_6,
            FibPercentage.Fib_138_2,
            FibPercentage.Fib_141_4,
            FibPercentage.Fib_150,
            FibPercentage.Fib_161_8,
            FibPercentage.Fib_166_7,
            FibPercentage.Fib_176_4,
            FibPercentage.Fib_185_4,
            FibPercentage.Fib_190_2,
            FibPercentage.Fib_198_4,

            FibPercentage.Fib_200,
            FibPercentage.Fib_205_6,
            FibPercentage.Fib_209_2,
            FibPercentage.Fib_214_6,
            FibPercentage.Fib_223_6,
            FibPercentage.Fib_238_2,
            FibPercentage.Fib_241_4,
            FibPercentage.Fib_250,
            FibPercentage.Fib_261_8,
            FibPercentage.Fib_266_7,
            FibPercentage.Fib_276_4,
            FibPercentage.Fib_285_4,
            FibPercentage.Fib_290_2,
            FibPercentage.Fib_298_4,

            FibPercentage.Fib_300,
            FibPercentage.Fib_305_6,
            FibPercentage.Fib_309_2,
            FibPercentage.Fib_314_6,
            FibPercentage.Fib_323_6,
            FibPercentage.Fib_338_2,
            FibPercentage.Fib_341_4,
            FibPercentage.Fib_350,
            FibPercentage.Fib_361_8,
            FibPercentage.Fib_366_7,
            FibPercentage.Fib_376_4,
            FibPercentage.Fib_385_4,
            FibPercentage.Fib_390_2,
            FibPercentage.Fib_398_4,

            FibPercentage.Fib_400,
            FibPercentage.Fib_405_6,
            FibPercentage.Fib_409_2,
            FibPercentage.Fib_414_6,
            FibPercentage.Fib_423_6,
            FibPercentage.Fib_438_2,
            FibPercentage.Fib_441_4,
            FibPercentage.Fib_450,
            FibPercentage.Fib_461_8,
            FibPercentage.Fib_466_7,
            FibPercentage.Fib_476_4,
            FibPercentage.Fib_485_4,
            FibPercentage.Fib_490_2,
            FibPercentage.Fib_498_4,
        };
    }
;
}