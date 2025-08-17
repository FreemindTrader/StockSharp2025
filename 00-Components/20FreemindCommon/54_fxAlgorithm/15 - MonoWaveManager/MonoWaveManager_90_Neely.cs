using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public partial class MonoWaveManager
    {
        public void Process7Rules( Security symbol, TimeSpan period, PeriodXTaManager taManager, INeelyWave m3_, INeelyWave m2_, INeelyWave m1_, INeelyWave m0, INeelyWave m1, INeelyWave m2, INeelyWave m3, double m2Retrace )
        {
            switch ( m2Retrace )
            {
                case double p when ( p < 38.2 ):
                {
                    Rule1( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 38.2 && p < 61.5 ):
                {
                    Rule2( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p> 61.5 && p < 62 ):
                {
                    Rule3( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 62 && p < 100 ):
                {
                    Rule4( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 100 && p < 161.8 ):
                {
                    Rule5( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p >= 161.8 && p <= 261.8 ):
                {
                    Rule6( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;

                case double p when ( p > 261.8 ):
                {
                    Rule7( symbol, period, taManager, m3_, m2_, m1_, m0, m1, m2, m3 );
                }
                break;
            }
        }
    }
}
