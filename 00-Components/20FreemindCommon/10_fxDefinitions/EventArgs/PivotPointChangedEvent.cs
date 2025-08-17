using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public class PPChangedEventArgs : EventArgs
    {
        double _m5;
        double _m4;
        double _m3;
        double _m2;
        double _m1;
        double _m0;
        double _mdn;
        double _s3;
        double _s2;
        double _s1;
        double _pivot;
        double _r3;
        double _r2;
        double _r1;
        private TimeSpan _period;

        string _security;

        public string Security
        {
            get => _security;
            set => _security = value;
        }

        public TimeSpan Period
        {
            get
            {
                return _period;
            }

            set
            {
                _period = value;
            }
        }


        public double R1
        {
            get => _r1;
            set => _r1 = value;
        }


        public double R2
        {
            get => _r2;
            set => _r2 = value;
        }


        public double R3
        {
            get => _r3;
            set => _r3 = value;
        }


        public double Pivot
        {
            get => _pivot;
            set => _pivot = value;
        }


        public double S1
        {
            get => _s1;
            set => _s1 = value;
        }


        public double S2
        {
            get => _s2;
            set => _s2 = value;
        }


        public double S3
        {
            get => _s3;
            set => _s3 = value;
        }


        public double Mdn
        {
            get => _mdn;
            set => _mdn = value;
        }


        public double M0
        {
            get => _m0;
            set => _m0 = value;
        }


        public double M1
        {
            get => _m1;
            set => _m1 = value;
        }


        public double M2
        {
            get => _m2;
            set => _m2 = value;
        }


        public double M3
        {
            get => _m3;
            set => _m3 = value;
        }


        public double M4
        {
            get => _m4;
            set => _m4 = value;
        }

        
        public double M5
        {
            get => _m5;
            set => _m5 = value;
        }
        

    }
}
