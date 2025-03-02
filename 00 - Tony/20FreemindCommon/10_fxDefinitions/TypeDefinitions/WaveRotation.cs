using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum WaveRotationType
    {
        INVALID  = 0,
        HighHigh = 1,
        HighLow  = 2,
        LowLow   = 3
    }

    
    public static class WaveRotation
    {
        public static int [ ] FibsSeq        = {  0,  1,  1,  2,  3,  5,  8, 13, 21,  34,  55,  89, 144,  233,  377,  610,  987, 1597, 2584, 4181,  6765, 10946, 17711,   28657, 46368, 75025 };
        public static int [ ] LucasSeq       = {  2,  1,  3,  4,  7, 11, 18, 29, 47,  76, 123, 199, 322,  521,  843, 1364, 2207, 3571, 5778, 9349, 15127 };
        public static int [ ] FibRatio       = { 15, 24, 33, 38, 41, 50, 59, 62, 67,  76,  85,  90,  99,  127,  138,  162,  167,  176,  185,  190, 198, 224, 238, 243, 262, 276, 285, 290, 294, 298, 362, 424, 462, 498, 662, 685 };

        public static int [ ] FibsSeqNearby  = { 20, 22, 33, 35, 54, 56, 88, 90, 143, 145, 232, 234, 376, 378, 609, 611, 986, 988 };
        public static int [ ] LucasSeqNearby = { 17, 19, 28, 30, 46, 48, 75, 77, 122, 124, 198, 200, 321, 323, 520, 522, 842, 844 };

        public static int []  DoubleFibsSeq  = {  0,  2,  2,  4,  6, 10, 16, 26, 42,  68, 110, 178, 288,  466,  754, 1220, 1974, 3194,  5168, 8362, 13530, 21892, 17711*2, 28657*2, 46368*2, 75025*2 };
        public static int [ ] DoubleLucasSeq = {  4,  2,  6,  8, 14, 22, 36, 58, 94, 152, 246, 398, 644, 1042, 1686, 2728, 4414, 7142, 11556, 9349 * 2, 15127*2 };


        public static int [ ] GannNumbers    = {  36,  45, 72, 90, 144, 180, 225, 270, 315, 360 };
        public static int [ ] SqRootNumbers  = {  38,  49, 62, 79, 89, 112, 127, 162, 206, 262 };
        public static int [ ] DerRootNumbers = { 118, 121, 129, 134, 141, 147, 155, 176, 189, 236 };
        public static int [ ] Numbers144     = { 144, 288, 432, 576, 720, 864, 1008, 1152, 1296, 1440 };

        public static int [ ] SpecialGannSeq  = { 36, 38, 62, 138, 144, 216 };
    }

    //public static class GannSquareOf9
    //{
    //    public static int [ ] MostImportantDegrees = { 45, 90, 135, 180, 225, 270, 360 };
    //    public static int [ ] SecondImportantDegrees = { 30, 60, 120, 150, 210, 240, 270, 300, 330 };
    //}
}
