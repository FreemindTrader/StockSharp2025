using fx.Bars;
using fx.Collections;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Algorithm
{    
    public class PivotPointsInfo
    {
        double _marketDirectionNumber;
        double _s3;
        double _m0;
        double _s2;
        double _m1;
        double _s1;
        double _m2;
        double _pivot;
        double _m3;
        double _r1;
        double _m4;
        double _r2;
        double _m5;
        double _r3;
        TimeSpan _timePeriod;

        private SortedDictionary< PivotPointName, double >                _pivotsDictionary            = new SortedDictionary< PivotPointName, double >();        

        public TimeSpan TimePeriod
        {
            get { return _timePeriod; }
            set
            {
                _timePeriod = value;
            }
        }

        public SortedDictionary<PivotPointName, double> AllPivotPoints
        {
            get
            {
                return _pivotsDictionary;
            }
        }


        public double R3
        {
            get { return _r3; }
            set
            {
                _r3 = value;
            }
        }


        public double M5
        {
            get { return _m5; }
            set
            {
                _m5 = value;
            }
        }


        public double R2
        {
            get { return _r2; }
            set
            {
                _r2 = value;
            }
        }


        public double M4
        {
            get { return _m4; }
            set
            {
                _m4 = value;
            }
        }

        public double R1
        {
            get { return _r1; }
            set
            {
                _r1 = value;
            }
        }

        public double M3
        {
            get { return _m3; }
            set
            {
                _m3 = value;
            }
        }

        public double Pivot
        {
            get { return _pivot; }
            set
            {
                _pivot = value;
            }
        }

        public double M2
        {
            get { return _m2; }
            set
            {
                _m2 = value;
            }
        }

        public double S1
        {
            get { return _s1; }
            set
            {
                _s1 = value;
            }
        }

        public double M1
        {
            get { return _m1; }
            set
            {
                _m1 = value;
            }
        }

        public double S2
        {
            get { return _s2; }
            set
            {
                _s2 = value;
            }
        }

        public double M0
        {
            get { return _m0; }
            set
            {
                _m0 = value;
            }
        }

        public double S3
        {
            get { return _s3; }
            set
            {
                _s3 = value;
            }
        }
        
        //public double Mdn
        //{
        //    get { return _marketDirectionNumber; }
        //    set
        //    {
        //        _marketDirectionNumber = value;
        //    }
        //}

        DateTime _dataTime;
        public DateTime DataTime
        {
            get { return _dataTime; }
            set
            {
                _dataTime = value;
            }
        }


        public PivotPointsInfo( DateTime dataTime, TimeSpan timePeriod, double r3, double m5, double r2, double m4, double r1, double m3, double pivot, double m2, double s1, double m1, double s2, double m0, double s3, double mdn )
        {
            _dataTime = dataTime;
            _timePeriod            = timePeriod;
            _r3                    = r3;
            _m5                    = m5;
            _r2                    = r2;
            _m4                    = m4;
            _r1                    = r1;
            _m3                    = m3;
            _pivot                 = pivot;
            _m2                    = m2;
            _s1                    = s1;
            _m1                    = m1;
            _s2                    = s2;
            _m0                    = m0;
            _s3                    = s3;
            _marketDirectionNumber = mdn;

            _pivotsDictionary.Add( PivotPointName.R3, _r3 );
            _pivotsDictionary.Add( PivotPointName.M5, _m5 );
            _pivotsDictionary.Add( PivotPointName.R2, _r2 );
            _pivotsDictionary.Add( PivotPointName.M4, _m4 );
            _pivotsDictionary.Add( PivotPointName.R1, _r1  );
            _pivotsDictionary.Add( PivotPointName.M3, _m3  );
            _pivotsDictionary.Add( PivotPointName.Pivot, _pivot );
            _pivotsDictionary.Add( PivotPointName.M2, _m2 );
            _pivotsDictionary.Add( PivotPointName.S1, _s1 );
            _pivotsDictionary.Add( PivotPointName.M1, _m1 );
            _pivotsDictionary.Add( PivotPointName.S2, _s2 );
            _pivotsDictionary.Add( PivotPointName.M0, _m0 );
            _pivotsDictionary.Add( PivotPointName.S3, _s3 );
            _pivotsDictionary.Add( PivotPointName.MDN, _marketDirectionNumber );            
        }

        public PPChangedEventArgs ToEventArgs()
        {
            var e    = new PPChangedEventArgs( );

            e.M0     = M0;
            e.M1     = M1;
            e.M2     = M2;
            e.M3     = M3;
            e.M4     = M4;
            e.M5     = M5;
            e.S1     = S1;
            e.S2     = S2;
            e.S3     = S3;
            e.R1     = R1;
            e.R2     = R2;
            e.R3     = R3;            
            e.Pivot  = Pivot;
            e.Period = TimePeriod;

            return e;
        }

        public string PivotString
        {
            get
            {
                string output = "";

                if ( _timePeriod == TimeSpan.FromDays( 1 ) )
                {
                    output = "Daily";
                }
                else if ( _timePeriod == TimeSpan.FromDays( 7 ) )
                {
                    output = "Weekly";
                }
                else if ( _timePeriod == TimeSpan.FromDays( 30 ) )
                {
                    output = "Monthly";
                }

                return output;
            }
        }

        public override string ToString( )
        {
            var closedTime = _dataTime + _timePeriod;
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
            DateTime barTimeEst = TimeZoneInfo.ConvertTimeFromUtc( closedTime , est );

            string output = "";

            if ( _timePeriod == TimeSpan.FromDays( 1 ) )
            {
                output = "Daily " + barTimeEst.DayOfWeek.ToString( ) + " " + barTimeEst.ToString();
            }
            else if ( _timePeriod == TimeSpan.FromDays( 7 ) )
            {
                output = "Weekly";
            }
            else if ( _timePeriod == TimeSpan.FromDays( 30 ) )
            {
                output = "Monthly";
            }            

            return output;
        }

        public PooledList<MatchedSRinfo> GetWavePeakToPivotStatus( ref SBar bar )
        {
            var output = new PooledList< MatchedSRinfo >( );
            var candleRange = bar.WholeCandle;

            foreach ( var pivot in _pivotsDictionary )
            {
                var matched = bar.GetUpperShadowSRInfo( _timePeriod, pivot.Key.ToString( ), (float) pivot.Value );

                if ( matched != null )
                {
                    output.Add( matched );
                }                
            }

            return output;
        }


        public PooledList<MatchedSRinfo> GetWaveTroughToPivotStatus( ref SBar bar )
        {
            var output = new PooledList< MatchedSRinfo >( );
            var candleRange = bar.WholeCandle;

            foreach ( var pivot in _pivotsDictionary )
            {
                var matched = bar.GetLowerShadowSRInfo( _timePeriod, pivot.Key.ToString( ), (float)pivot.Value );
                if (matched != null)
                {
                    output.Add(matched);
                }
            }

            return output;
        }

    }
}
