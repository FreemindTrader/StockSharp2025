using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public struct SimpleABC
    {
        /* ---------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Since Every thing is ABC or in triangle with DE, so 3 bits can represent this.
         *  None                                00
         *  Wave A = WaveEFA =  TriA = WaveX    01       
         *  Wave2                               02
         *  Wave B = WaveEFB =  TriB = WaveY    03      
         *  Wave4                               04                            
         *  Wave C = WaveEFC =  TriC = WaveZ    05      
         *                      TriD            06
         *                      TriE            07  
         *                      
         *  the type of ABC can be expressed as.
         *  
         *  Correction          00
         *  Wave 1      - ABC   01
         *  FLAT        -       02
         *  Wave 3      - ABC   03
         *  Triangle    -       04 
         *  Wave 5      - ABC   05
         *  
         
         *  
         * --------------------------------------------------------------------------------------------------------------------------------------------- 
         */

    }
}
