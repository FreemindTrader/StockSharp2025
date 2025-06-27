using fx.Definitions;
using fx.Definitions.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public interface INeelyWave : IWave
    {
        Vector BeginVector { get; }
        Vector EndVector { get; }

        Vector BeginEndBarIndex { get; }
        TrendLine TrendLine { get; }

        int MonowaveIndex { get; set; }
        int MonoWavesCount { get; }

        int AllWaveImptCount { get;  }
        MonoWaveKey Key { get; }       

        bool BreakTrendLineBtwXYInLETimeToZ( INeelyWave m2_, INeelyWave m0, INeelyWave m1 );

        bool BreakTrendLineBtwXY( INeelyWave trendLineBegin, INeelyWave trendLineEnd );

        
        
        bool PriceTimeAlternation( INeelyWave other );

        MonoWaveGroup Combine( INeelyWave m2, INeelyWave m3 );

        MonoWaveGroup Combine( INeelyWave m1, INeelyWave m2, INeelyWave m3, INeelyWave m4 );


        (long start, long end) First3Monowaves( );

        (long start, long end) FirstNthMonowaves( int monoWaveCount );

        double First3MonowavesPips( );

        double FirstNthMonowavesPips( int monoWaveCount );

        INeelyWave GetNext( );

        INeelyWave GetPrevious( );

        bool PreferableStructureIs( StructureLabelEnum s5 );

        int ComplexityLevel { get; set; }

    }
        
}
