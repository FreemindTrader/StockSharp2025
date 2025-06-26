//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace fx.Definitions
//{
//    public abstract class AWaveBase : INeelyWave
//    {
//        protected WaveLabelPosition    _labelPosition;
//        protected DateTime             _beginTime;
//        protected ElliottWaveEnum      _beginWaveName;
//        protected WaveType             _type;
//        protected ElliottWaveCycle     _waveCycle;
//        protected ElliottWaveEnum      _endWaveName;
//        public DateTime BeginTime
//        {
//            get { return _beginTime; }
//            set
//            {
//                _beginTime = value;
//            }
//        }

//        public ElliottWaveEnum BeginWaveName
//        {
//            get { return _beginWaveName; }
//            set
//            {
//                _beginWaveName = value;
//            }
//        }

//        public WaveType WaveType
//        {
//            get { return _type; }
//            set
//            {
//                _type = value;
//            }
//        }

//        public ElliottWaveCycle WaveCycle
//        {
//            get { return _waveCycle; }
//            set
//            {
//                _waveCycle = value;
//            }
//        }


//        public WaveLabelPosition LabelPosition
//        {
//            get { return _labelPosition; }
//            set
//            {
//                _labelPosition = value;
//            }
//        }

//        public ElliottWaveEnum EndWaveName
//        {
//            get { return _endWaveName; }
//            set
//            {
//                _endWaveName = value;
//            }
//        }

//        protected DateTime? _endTime;
//        public DateTime? EndTime
//        {
//            get { return _endTime; }
//            set
//            {
//                _endTime = value;
//            }
//        }


//        private TimeSpan _duration;
//        public TimeSpan Duration
//        {
//            get { return _duration; }
//        }
//    }
//}
