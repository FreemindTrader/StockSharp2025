using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public class SquareOf9Target
    {
        int _gannDegrees;
        private GannDegreesType _gannDegreeType;

        public GannDegreesType GannDegreeType
        {
            get { return _gannDegreeType; }
            set
            {
                _gannDegreeType = value;
            }
        }

        
        public int GannDegrees
        {
            get { return _gannDegrees; }
            set
            {
                _gannDegrees = value;
            }
        }
        
        public TrendDirection TrendDirection { get; set; }

    }
}
