using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using fx.TimePeriod;
using fx.Definitions;
using System;
using System.Collections.Generic;

namespace fx.Common
{
    public class SRlevel : BindableBase, IComparable, IComparable<SRlevel>
    {
        public Tuple<SR1stType, SR2ndType, SR3rdType> UniqueKey( )
        {
            return new Tuple<SR1stType, SR2ndType, SR3rdType>( _sR1stType, _sR2ndType, _sR3rdType );
        }

        public decimal PriceStep
        {
            get;
            set;
        } = 5;

        
        private TimeBlockEx _responsibleTimeBlock;
        private TimeSpan _responsibleTimeSpan;
        private SR1stType _sR1stType;
        public SR1stType SR1stType
        {
            get { return _sR1stType; }

            set
            {
                SetValue( ref _sR1stType, value );
            }
        }

        private SR2ndType _sR2ndType;
        public SR2ndType SR2ndType
        {
            get { return _sR2ndType; }
            set
            {
                SetValue( ref _sR2ndType, value );
            }
        }

        private SR3rdType _sR3rdType;
        public SR3rdType SR3rdType
        {
            get { return _sR3rdType; }
            set
            {
                SetValue( ref _sR3rdType, value );
            }
        }

        private double _price;
        public double SRvalue
        {
            get
            {
                var rounded = Math.Round( _price, (int) PriceStep );
                return rounded;
            }
            set
            {
                SetValue( ref _price, value );
            }
        }

        private string _name;
        public string SRname
        {
            get { return _name; }
            set
            {
                SetValue( ref _name, value );
            }
        }

        private int _totalLines;
        // Total number of line within that confluence.
        public int TotalLines
        {
            get { return _totalLines; }

            set
            {
                SetValue( ref _totalLines, value );
            }
        }

        private bool _isBroken;
        // Total number of line within that confluence.
        public bool IsBroken
        {
            get { return _isBroken; }

            set
            {
                SetValue( ref _isBroken, value );
            }
        }

        private int _strength;
        public int SRstrength
        {
            get { return _strength; }

            set
            {
                SetValue( ref _strength, value );
            }
        }

        public BarPeriod SRsignificance
        {
            get { return _significance; }

            set
            {
                SetValue( ref _significance, value );
            }
        }

        public TimeBlockEx SRtimeBlock
        {
            get { return _responsibleTimeBlock; }

            set
            {
                _responsibleTimeBlock = value ;
            }
        }

        public TimeSpan SRtimeFrame
        {
            get { return _responsibleTimeSpan; }

            set
            {
                SetValue( ref _responsibleTimeSpan, value );
            }
        }


        private BarPeriod _significance;


        /// <summary>
        /// Construct empty BarData, for this dateTime.
        /// </summary>
        public SRlevel( SRlevel other )
        {
            _price                = other._price;
            _name                 = other._name;
            _totalLines           = other._totalLines;
            _strength             = other._strength;
            _significance         = other._significance;
            _sR1stType            = other._sR1stType;
            _sR2ndType            = other._sR2ndType;
            _sR3rdType            = other._sR3rdType;
            _responsibleTimeBlock = other._responsibleTimeBlock;
            _responsibleTimeSpan  = other._responsibleTimeSpan;
        }


        public void CopyChangable( SRlevel other )
        {
            SRvalue        = other._price;
            SRname         = other._name;
            TotalLines     = other._totalLines;
            SRstrength     = other._strength;
            SRsignificance = other._significance;
        }


        public SRlevel( ref TimeBlockEx responsibleDate, TimeSpan responsibleTimeSpan, double price, int strength, SR3rdType sR3rdType )
        {
            SRvalue        = price;
            TotalLines     = 1;
            SRtimeBlock    = responsibleDate;
            SRtimeFrame    = responsibleTimeSpan;
            SRstrength     = strength;
            SR3rdType      = sR3rdType;
            SR2ndType      = _sR3rdType.Sr2nd( );

            SRname         = string.Format( "{2}-{0}-{1}", sR3rdType.ToDescription( ), price, responsibleTimeSpan.ToShortForm() );
            SRsignificance = responsibleTimeSpan.ToBarPeriod( );
            SR1stType      = responsibleTimeSpan.Sr1st( );

            PriceStep      = 5;

        }

        public SRlevel( ref TimeBlockEx responsibleDate, TimeSpan responsibleTimeSpan, double price, int strength, SR1stType sr1stType, SR2ndType sr2ndType, SR3rdType sR3rdType )
        {
            SRvalue        = price;
            TotalLines     = 1;
            SRtimeBlock    = responsibleDate;
            SRtimeFrame    = responsibleTimeSpan;
            SRstrength     = strength;
            SR3rdType      = sR3rdType;
            SR2ndType      = sr2ndType;

            SRname         = string.Format( "{0}-{1}", sR3rdType.ToDescription( ), price );
            SRsignificance = responsibleTimeSpan.ToBarPeriod( );
            SR1stType      = sr1stType;

            PriceStep      = 5;
        }

        public SRlevel( ref TimeBlockEx responsibleDate, TimeSpan responsibleTimeSpan, double price, int strength, SR1stType sr1stType, SR2ndType sr2ndType, SR3rdType sR3rdType, string srName )
        {
            SRvalue        = price;
            TotalLines     = 1;
            SRtimeBlock    = responsibleDate;
            SRtimeFrame    = responsibleTimeSpan;
            SRstrength     = strength;
            SR3rdType      = sR3rdType;
            SR2ndType      = sr2ndType;

            SRname         = srName;
            SRsignificance = responsibleTimeSpan.ToBarPeriod( );
            SR1stType      = sr1stType;

            PriceStep      = 5;
        }



        public string[ ] ToStrings( )
        {
            return new string[ ]
            {
                _price.ToString( "F5" ), _name.ToString( ), _totalLines.ToString( ), _strength.ToString( )
            };
        }

        public override string ToString( )
        {
            return base.ToString( ) + "[Price: " + _price.ToString( "F5" ) + " ,Name: " + _name + " TotalLines: " + _totalLines + " ,Strength: " + _strength + "]";
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            SRlevel other = obj as SRlevel;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( SRlevel ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( SRlevel other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _responsibleTimeSpan.CompareTo( other._responsibleTimeSpan );
            if ( result != 0 )
            {
                return result;
            }

            result = _sR1stType.CompareTo( other._sR1stType );
            if ( result != 0 )
            {
                return result;
            }

            result = _sR2ndType.CompareTo( other._sR2ndType );
            if ( result != 0 )
            {
                return result;
            }

            result = _sR3rdType.CompareTo( other._sR3rdType );
            if ( result != 0 )
            {
                return result;
            }

            result = _price.CompareTo( other._price );
            if ( result != 0 )
            {
                return result;
            }

            result = _name.CompareTo( other._name );
            if ( result != 0 )
            {
                return result;
            }

            result = _totalLines.CompareTo( other._totalLines );
            if ( result != 0 )
            {
                return result;
            }

            result = _isBroken.CompareTo( other._isBroken );
            if ( result != 0 )
            {
                return result;
            }

            result = _strength.CompareTo( other._strength );
            if ( result != 0 )
            {
                return result;
            }

            result = _significance.CompareTo( other._significance );
            if ( result != 0 )
            {
                return result;
            }

            result = PriceStep.CompareTo( other.PriceStep );
            if ( result != 0 )
            {
                return result;
            }

            result = SRvalue.CompareTo( other.SRvalue );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }

        #region IComparable<SupportResistanceLevel> Members


        #endregion



    }
}