using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum HHLLEnum
    {                
        LowLow   = 0,
        LowHigh  = 1,
        HighLow  = 2,
        HighHigh = 3
    }

    /// <summary>
    /// 
    /// Bit 31 30     = Use the top 2 bits to reprsent High Low, when the bit is on, it means High, so 1 1 = High High 
    /// Bit 0 - 29    = the index different between the two extremums

    /// </summary>
    public struct HHLL
    {
        private uint _hhll;
        

        public HHLL( HHLLEnum other, uint diff )
        {
            _hhll = (uint) other;
            _hhll = _hhll << 30;
            _hhll |= diff;
        }

        public HHLLEnum ExtremumType
        {
            get
            {
                return ( HHLLEnum ) ( _hhll >> 30 );
            }            
        }

        public uint Diff
        {
            get
            {

                uint output = _hhll & BarBits.TurnOffTop2Bits;

                return output;
            }
        }
    }
}
