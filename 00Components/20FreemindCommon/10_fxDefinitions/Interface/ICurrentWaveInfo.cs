using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fx.Definitions
{     
    public interface ICurrentWaveInfo
    {        
        TrendDirection WaveDirection { get; set; }

        ElliottWaveCycle Cycle { get; set; }

        ElliottWaveEnum PreviousWave { get; set; }

        ElliottWaveEnum CurrentWave { get; set; }

        float NextLevel { get; set; }

        float Price { get; set; }

        string SpecialNumber { get; set; }

        int WaveRotationBar { get; set; }
    }
}
