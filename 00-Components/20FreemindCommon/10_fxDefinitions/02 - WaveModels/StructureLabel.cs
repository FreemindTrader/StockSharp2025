using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    

    

    [Flags]
    public enum StructureLabelEnum : long
    {
        NONE            = 0,
        F3              = 1,                    // First three ( 3 )
        c3              = 2L <<  1,             // Center Three ( 3 ) 
        x_c3            = 2L <<  2,             // center three (3) in the x-wave position
        sL3             = 2L <<  3,             // Second to Last three (3)
        L3              = 2L <<  4,             // Last three (3)
        _5              = 2L <<  5,             // Five
        s5              = 2L <<  6,             // Special Five
        L5              = 2L <<  7,             // Last Five ( 5 )

        F3_MAYBE        = 2L <<  8,             // First three ( 3 )
        c3_MAYBE        = 2L <<  9,             // Center Three ( 3 ) 
        x_c3_MAYBE      = 2L <<  10,            // center three (3) in the x-wave position
        sL3_MAYBE       = 2L <<  11,            // Second to Last three (3)
        L3_MAYBE        = 2L <<  12,            // Last three (3)
        _5_MAYBE        = 2L <<  13,            // Five
        s5_MAYBE        = 2L <<  14,            // Special Five
        L5_MAYBE        = 2L <<  15,            // Last Five ( 5 )

        F3_LESSLIKELY   = 2L <<  16,            // First three ( 3 )
        c3_LESSLIKELY   = 2L <<  17,            // Center Three ( 3 ) 
        x_c3_LESSLIKELY = 2L <<  18,            // center three (3) in the x-wave position
        sL3_LESSLIKELY  = 2L <<  19,            // Second to Last three (3)
        L3_LESSLIKELY   = 2L <<  20,            // Last three (3)
        _5_LESSLIKELY   = 2L <<  21,            // Five
        s5_LESSLIKELY   = 2L <<  22,            // Special Five
        L5_LESSLIKELY   = 2L <<  23,            // Last Five ( 5 )

        F3_RARE         = 2L <<  24,            // First three ( 3 )
        c3_RARE         = 2L <<  25,            // Center Three ( 3 ) 
        x_c3_RARE       = 2L <<  26,            // center three (3) in the x-wave position
        sL3_RARE        = 2L <<  27,            // Second to Last three (3)
        L3_RARE         = 2L <<  28,            // Last three (3)
        _5_RARE         = 2L <<  29,            // Five
        s5_RARE         = 2L <<  30,            // Special Five
        L5_RARE         = 2L <<  31,            // Last Five ( 5 )

        b_F3_MAYBE      = 2L <<  32,            // Last Five ( 5 )
        b_c3            = 2L <<  33,            // Last Five ( 5 )



    }

    
}
