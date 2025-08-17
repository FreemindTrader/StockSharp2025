using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface ISmartWaveCycles
    {
        long StartDate { get; set; }
        string Period { get; set; }

        int OfferID { get; set; }

        int WaveImportance { get; set; }

        ElliottWaveCycle WaveCycle1 { get; set; }
        ElliottWaveCycle? WaveCycle2 { get; set; }
        ElliottWaveCycle? WaveCycle3 { get; set; }
        ElliottWaveCycle? WaveCycle4 { get; set; }

    }

    
}
