using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace fx.Algorithm
{
    public class MonoLine
    {

        public MonoLine( Vector beginPtVector, Vector endPtVector, Vector indexVector, MonoWaveNumber monoWaveNumber )
        {
            _beginPtVector  = beginPtVector;
            _endPtVector    = endPtVector;
            _indexVector    = indexVector;
            _monoWaveNumber = monoWaveNumber;
        }
        private Vector _beginPtVector;
        private Vector _endPtVector;
        private Vector _indexVector;
        private MonoWaveNumber _monoWaveNumber;

        public Vector BeginVector
        {
            get
            {
                return _beginPtVector;
            }
        }

        public Vector IndexVector
        {
            get
            {
                return _indexVector;
            }
        }

        public Vector EndVector
        {
            get
            {
                return _endPtVector;
            }
        }

        
        public MonoWaveNumber MonoWaveNumber
        {
            get { return _monoWaveNumber; }            
        }
        
        public DateTime GetBeginDate()
        {
            long dateTime = (long) _beginPtVector.X;
            return dateTime.FromLinuxTime( );
        }

        public DateTime GetMidDate()
        {
            var beginDate = GetBeginDate( );
            var endDate = GetEndDate( );

            TimeSpan diff = endDate - beginDate;

            TimeSpan half = new TimeSpan(diff.Ticks / 2);

            return ( beginDate + half );
        }

        public bool RuleCalcWaves()
        {
            if ( _monoWaveNumber == MonoWaveNumber.m0 || _monoWaveNumber == MonoWaveNumber.m1 || _monoWaveNumber == MonoWaveNumber.m2)
            {
                return true;
            }

            return false;
        }

        public DateTime GetEndDate()
        {
            long dateTime = (long) _endPtVector.X;
            return dateTime.FromLinuxTime( );
        }

        public double MidY
        {
            get
            {
                return ( BeginY + EndY ) / 2;
            }
        }

        public double BeginY
        {
            get
            {
                return _beginPtVector.Y;
            }
        }

        public double EndY
        {
            get
            {
                return _endPtVector.Y;
            }
        }

        public string GetWaveNumberString()
        {
            string output = "";
            switch ( _monoWaveNumber )
            {                
                case MonoWaveNumber.m8_:
                    output = string.Format( "m(-8) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m7_:
                    output = string.Format( "m(-7) \r\n[{0}]", _indexVector.X );                    
                    break;
                case MonoWaveNumber.m6_:
                    output = string.Format( "m(-6) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m5_:
                    output = string.Format( "m(-5) \r\n[{0}]", _indexVector.X );                    
                    break;
                case MonoWaveNumber.m4_:
                    output = string.Format( "m(-4) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m3_:
                    output = string.Format( "m(-3) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m2_:
                    output = string.Format( "m(-2) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m1_:
                    output = string.Format( "m(-1) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m0:
                    output = string.Format( "m(0) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m1:
                    output = string.Format( "m(1) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m2:
                    output = string.Format( "m(2) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m3:
                    output = string.Format( "m(3) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m4:
                    output = string.Format( "m(4) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m5:
                    output = string.Format( "m(5) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m6:
                    output = string.Format( "m(6) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m7:
                    output = string.Format( "m(7) \r\n[{0}]", _indexVector.X );
                    break;
                case MonoWaveNumber.m8:
                    output = string.Format( "m(8) \r\n[{0}]", _indexVector.X );
                    break;
            }

            return output;
        }

        public SolidColorBrush GetBrushColor()
        {
            if ( _monoWaveNumber == MonoWaveNumber.m1 )
            {
                return new SolidColorBrush( Color.FromArgb( byte.MaxValue, 255, 0, 0 ) );
            }
            else if ( _monoWaveNumber == MonoWaveNumber.m0 ||  _monoWaveNumber == MonoWaveNumber.m2 )                
            {
                return new SolidColorBrush( Color.FromArgb( byte.MaxValue, 0, 0, 255 ) );
            }
            else
            {
                return new SolidColorBrush( Colors.DarkGray );
            }                        
        }

        public SolidColorBrush GetBackgroundBrushColor( )
        {
            if ( _monoWaveNumber == MonoWaveNumber.m0 )
            {
                return new SolidColorBrush( Colors.Gold );
            }
            else if ( _monoWaveNumber == MonoWaveNumber.m1 || _monoWaveNumber == MonoWaveNumber.m2 || _monoWaveNumber == MonoWaveNumber.m3 || _monoWaveNumber == MonoWaveNumber.m4 )
            {
                return new SolidColorBrush( Colors.MediumSpringGreen );
            }
            else if ( _monoWaveNumber == MonoWaveNumber.m1_ || _monoWaveNumber == MonoWaveNumber.m2_ || _monoWaveNumber == MonoWaveNumber.m3_ || _monoWaveNumber == MonoWaveNumber.m4_ )
            {
                return new SolidColorBrush( Colors.Cyan );
            }

            return new SolidColorBrush( Color.FromArgb( byte.MaxValue, 186, 186, 186 ) );
        }
    }
}
