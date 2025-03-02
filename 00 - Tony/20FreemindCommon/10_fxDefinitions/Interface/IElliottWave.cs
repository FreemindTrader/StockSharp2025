using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fx.Definitions
{
    public interface IElliottWave
    {
        long HarmonicElliottWaveBit            { get; set; }

        long? AlternativeHewBit                { get; set; }

        long? HarmonicElliottWaveExtraBit       { get; set; }

        long? AlternativeHewExtraBit           { get; set; }

        WaveLabelPosition    WaveLabelPosition { get; set; }

        long StartDate                         { get; set; }
        
        DateTime BeginTimeUTC                  { get; set; }

        ElliottWaveCycle HighestWaveCycle      { get; set; }

        //IElliottWaveBar OwningBar              { get; }

        int OfferId                            { get; set; }

        string Period                          { get; set; }

        ref HewLong GetWaveFromScenario( int no );

        bool IsSpecialWave( );

        //void AttachOwningBar( IElliottWaveBar tmpBar );


        // void ChangeOwningBar( IElliottWaveBar tmpBar );


        //void SwapWaves( int waveScenarioNoX, int waveScenarioNoY );

        void SwapMainWavesWithAlternative( HewLong wavesToBeChanged );        
    }
}
