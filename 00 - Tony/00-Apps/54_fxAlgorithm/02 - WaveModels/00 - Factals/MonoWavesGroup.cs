using fx.Bars;
using fx.Collections;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{    
    public class MonoWavesGroup
    {
        SBar _beginBar;
        SBar _endBar;

        public MonoWavesGroup( KeyValuePair<long, WavePointImportance> beginPt, ref SBar beginBar, KeyValuePair<long, WavePointImportance> endPt, ref  SBar endBar, TrendDirection direction )
        {
            BeginTime           = beginPt.Key.FromLinuxTime();
            BeginWaveImportance = beginPt.Value;
            EndTime             = endPt.Key.FromLinuxTime();
            EndWaveImportance   = endPt.Value;
            _beginBar = beginBar;
            _endBar = endBar;
            Direction           = direction;
        }

        public MonoWavesGroup( ElliottWaveEnum beginWaveName, DateTime beginTime, ElliottWaveEnum endWaveName, DateTime endTime, ElliottWaveCycle waveCycle )
        {
            BeginWaveName       = beginWaveName;
            BeginTime           = beginTime;
            EndWaveName         = endWaveName;
            EndTime             = endTime;
            WaveCycle           = waveCycle;            
        }

        public void AddInnerWaves( MonoWavesGroup innerCorrect )
        {
            if ( !_innerWaves.Contains( innerCorrect ) )
            {
                _innerWaves.Add( innerCorrect );
            }
        }

        public DateTime            BeginTime           { get; set; }
        public ElliottWaveEnum     BeginWaveName       { get; set; }
        public WavePointImportance BeginWaveImportance { get; set; }
        
        public ref SBar BeginBar
        {
            get
            {
                return ref _beginBar;
            }
        }

        public ref SBar EndBar
        {
            get
            {
                return ref _endBar;
            }
        }

        public DateTime            EndTime             { get; set; }              
        public ElliottWaveEnum     EndWaveName         { get; set; }
        public WavePointImportance EndWaveImportance   { get; set; }
        

        public TrendDirection      Direction           { get; set; }
        public PriceActionThruTime PriceAction         { get; set; }
        public WaveType            WaveType            { get; set; }
        public MonoWavesGroup      NextWave            { get; set; }
        public MonoWavesGroup      PreviousWave        { get; set; }

        public ElliottWaveCycle    WaveCycle           { get; set; }


        protected PooledList< MonoWavesGroup > _innerWaves = new PooledList< MonoWavesGroup >( );

        
    }
}
