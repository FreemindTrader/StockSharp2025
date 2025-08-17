namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class DbBaZi : IFxcm, IEquatable<DbBaZi>, IComparable, IComparable<DbBaZi>
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id { get; set; }

        public long StartDate { get; set; }

        public DateTime UtcDate { get; set; }
        public DateTime LocalDate { get; set; }

        string _hourlyBaZi;
        string _dailyBaZi;
        string _monthlyBaZi;
        string _yearlyBaZi;
        bool _topBottom;
        string _symbol;
        string _belongsTo;
        int _waveImportance;
        double _difference;
        double _same;
        double _earth;
        double _fire;
        double _water;
        double _wood;
        double _gold;
        string _baZiEval;


        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
            }
        }


        public bool TopBottom
        {
            get { return _topBottom; }
            set
            {
                _topBottom = value;
            }
        }

        public DbBaZi( )
        {
        }

        private string _period;

        public DbBaZi( DateTime utcDate, DateTime localDate, string symbol, string period, bool topBottom, int waveImportance, string yearlyBaZi, string monthlyBaZi, string dailyBaZi, string hourlyBaZi, string belongsTo, string baZiEval, double gold, double wood, double water, double fire, double earth, double same, double difference )
        {
            Id              = 0;
            UtcDate         = utcDate;
            LocalDate       = localDate;
            Symbol          = symbol;
            _period         = period;
            _waveImportance = waveImportance;
            _topBottom      = topBottom;
            YearlyBaZi      = yearlyBaZi;
            MonthlyBaZi     = monthlyBaZi;
            DailyBaZi       = dailyBaZi;
            HourlyBaZi      = hourlyBaZi;
            _belongsTo      = belongsTo;
            _baZiEval       = baZiEval;
            _gold           = gold;
            _wood           = wood;
            _water          = water;
            _fire           = fire;
            _earth          = earth;
            _same           = same;
            _difference     = difference;
        }

        public string Period
        {
            get { return _period; }

            set { _period = value; }
        }


        public int WaveImportance
        {
            get { return _waveImportance; }
            set
            {
                _waveImportance = value;
            }
        }




        public string YearlyBaZi
        {
            get { return _yearlyBaZi; }
            set
            {
                _yearlyBaZi = value;
            }
        }


        public string MonthlyBaZi
        {
            get { return _monthlyBaZi; }
            set
            {
                _monthlyBaZi = value;
            }
        }


        public string DailyBaZi
        {
            get { return _dailyBaZi; }
            set
            {
                _dailyBaZi = value;
            }
        }

        
        public string HourlyBaZi
        {
            get { return _hourlyBaZi; }
            set
            {
                _hourlyBaZi = value;
            }
        }
        



        public string BelongsTo
        {
            get { return _belongsTo; }
            set
            {
                _belongsTo = value;
            }
        }



        public string BaZiEval
        {
            get { return _baZiEval; }
            set
            {
                _baZiEval = value;
            }
        }


        public double Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
            }
        }


        public double Wood
        {
            get { return _wood; }
            set
            {
                _wood = value;
            }
        }


        public double Water
        {
            get { return _water; }
            set
            {
                _water = value;
            }
        }


        public double Fire
        {
            get { return _fire; }
            set
            {
                _fire = value;
            }
        }


        public double Earth
        {
            get { return _earth; }
            set
            {
                _earth = value;
            }
        }


        public double Same
        {
            get { return _same; }
            set
            {
                _same = value;
            }
        }


        public double Difference
        {
            get { return _difference; }
            set
            {
                _difference = value;
            }
        }

        public override bool Equals( object obj )
        {
            if ( obj is DbBaZi )
            {
                return Equals( ( DbBaZi )obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( DbBaZi first, DbBaZi second )
        {
            if ( ( object )first == null )
            {
                return ( object )second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( DbBaZi first, DbBaZi second )
        {
            return !( first == second );
        }

        public bool Equals( DbBaZi other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return Equals( _hourlyBaZi, other._hourlyBaZi ) && Equals( _dailyBaZi, other._dailyBaZi ) && Equals( _monthlyBaZi, other._monthlyBaZi ) && Equals( _yearlyBaZi, other._yearlyBaZi ) && _topBottom.Equals( other._topBottom ) && Equals( _symbol, other._symbol ) && Equals( _belongsTo, other._belongsTo ) && _waveImportance.Equals( other._waveImportance ) && _difference.Equals( other._difference ) && _same.Equals( other._same ) && _earth.Equals( other._earth ) && _fire.Equals( other._fire ) && _water.Equals( other._water ) && _wood.Equals( other._wood ) && _gold.Equals( other._gold ) && Equals( _baZiEval, other._baZiEval ) && Equals( _period, other._period ) && Id.Equals( other.Id ) && StartDate.Equals( other.StartDate ) && UtcDate.Equals( other.UtcDate ) && LocalDate.Equals( other.LocalDate );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                if ( _hourlyBaZi != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _hourlyBaZi );
                }

                if ( _dailyBaZi != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _dailyBaZi );
                }

                if ( _monthlyBaZi != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _monthlyBaZi );
                }

                if ( _yearlyBaZi != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _yearlyBaZi );
                }

                hashCode = ( hashCode * 53 ) ^ _topBottom.GetHashCode( );
                if ( _symbol != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _symbol );
                }

                if ( _belongsTo != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _belongsTo );
                }

                hashCode = ( hashCode * 53 ) ^ _waveImportance.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _difference.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _same.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _earth.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _fire.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _water.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _wood.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _gold.GetHashCode( );
                if ( _baZiEval != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _baZiEval );
                }

                if ( _period != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _period );
                }

                hashCode = ( hashCode * 53 ) ^ Id.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ StartDate.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ UtcDate.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ LocalDate.GetHashCode( );
                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            DbBaZi other = obj as DbBaZi;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( DbBaZi ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( DbBaZi other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _hourlyBaZi.CompareTo( other._hourlyBaZi );
            if ( result != 0 )
            {
                return result;
            }

            result = _dailyBaZi.CompareTo( other._dailyBaZi );
            if ( result != 0 )
            {
                return result;
            }

            result = _monthlyBaZi.CompareTo( other._monthlyBaZi );
            if ( result != 0 )
            {
                return result;
            }

            result = _yearlyBaZi.CompareTo( other._yearlyBaZi );
            if ( result != 0 )
            {
                return result;
            }

            result = _topBottom.CompareTo( other._topBottom );
            if ( result != 0 )
            {
                return result;
            }

            result = _symbol.CompareTo( other._symbol );
            if ( result != 0 )
            {
                return result;
            }

            result = _belongsTo.CompareTo( other._belongsTo );
            if ( result != 0 )
            {
                return result;
            }

            result = _waveImportance.CompareTo( other._waveImportance );
            if ( result != 0 )
            {
                return result;
            }

            result = _difference.CompareTo( other._difference );
            if ( result != 0 )
            {
                return result;
            }

            result = _same.CompareTo( other._same );
            if ( result != 0 )
            {
                return result;
            }

            result = _earth.CompareTo( other._earth );
            if ( result != 0 )
            {
                return result;
            }

            result = _fire.CompareTo( other._fire );
            if ( result != 0 )
            {
                return result;
            }

            result = _water.CompareTo( other._water );
            if ( result != 0 )
            {
                return result;
            }

            result = _wood.CompareTo( other._wood );
            if ( result != 0 )
            {
                return result;
            }

            result = _gold.CompareTo( other._gold );
            if ( result != 0 )
            {
                return result;
            }

            result = _baZiEval.CompareTo( other._baZiEval );
            if ( result != 0 )
            {
                return result;
            }

            result = _period.CompareTo( other._period );
            if ( result != 0 )
            {
                return result;
            }

            result = Id.CompareTo( other.Id );
            if ( result != 0 )
            {
                return result;
            }

            result = StartDate.CompareTo( other.StartDate );
            if ( result != 0 )
            {
                return result;
            }

            result = UtcDate.CompareTo( other.UtcDate );
            if ( result != 0 )
            {
                return result;
            }

            result = LocalDate.CompareTo( other.LocalDate );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }
    }
}
