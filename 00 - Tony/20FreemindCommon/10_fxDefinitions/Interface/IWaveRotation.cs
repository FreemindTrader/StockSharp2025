using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fx.Definitions
{
    public enum WaveRotationEnum
    {
        Low2Low,
        Low2High,
        High2High,
        High2Low                
    }

    public interface IWaveRotation
    {        
        bool IsLucasCount( );

        bool IsFibCount( );

        bool IsGannCount( );

        bool IsSpecialCount( );

        WaveRotationEnum WaveRotationType { get; set; }

        IElliottWave ReferenceWave { get; set; }

        int BarCount { get; set; }

        TimeSpan CurrentTimeFrame { get; set; }
    }
}
