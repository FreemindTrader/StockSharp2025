using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace fx.Algorithm
{
    public enum PivotPointName
    {
        R3,
        M5,
        R2,
        M4,
        R1,
        M3,
        Pivot,
        M2,
        S1,
        M1,
        S2,
        M0,
        S3,
        MDN
    }
    

    public class PivotPoint : IComparable, IComparable<PivotPoint>
    {        
        private double                _levelValue;
        private PivotPointName        _levelName;

        public PivotPointName LevelName
        {
            get { return _levelName; }
            set
            {
                _levelName = value;
            }
        }


        public double LevelValue
        {
            get { return _levelValue; }
            set
            {
                _levelValue = value;
            }
        }


        public PivotPoint( PivotPointName name, double level  )
        {
            _levelName = name;
            _levelValue = level;            
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            PivotPoint other = obj as PivotPoint;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( PivotPoint ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( PivotPoint other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _levelValue.CompareTo( other._levelValue );
            if ( result != 0 )
            {
                return result;
            }

            result = _levelName.CompareTo( other._levelName );
            if ( result != 0 )
            {
                return result;
            }           

            return result;
        }
    }

    
}


