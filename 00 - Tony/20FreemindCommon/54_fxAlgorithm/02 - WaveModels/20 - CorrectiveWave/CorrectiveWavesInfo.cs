using fx.Bars;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public class CorrectiveWavesInfo: ElliottWavesGroup
    {
        public CorrectiveWavesInfo( ElliottWaveEnum beginWaveName, DateTime beginTime, ElliottWaveEnum endWaveName, DateTime endTime, ElliottWaveCycle waveCycle ) : base ( beginWaveName, beginTime, endWaveName, endTime, waveCycle )
        {
           // WaveType = WaveType.Correction;
        }

        public CorrectiveWavesInfo( KeyValuePair<long, WavePointImportance> beginPt, ref SBar beginBar, KeyValuePair<long, WavePointImportance> endPt, ref SBar endBar, TrendDirection direction ) : base( beginPt, ref beginBar, endPt, ref endBar, direction )
        {
            //WaveType = WaveType.Correction;
        }

        public WavePattern   WavePattern        { get; set; }       
        public CorrectionDepth  Depth                 { get; set; }       
        public double           RetracementPercentage { get; set; }               
        public ElliottWaveEnum  ContainingWave        { get; set; }                
    }
}
