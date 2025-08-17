using System;
using System.ComponentModel;
using fx.Definitions;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using fx.Collections;
using System.Collections.ObjectModel;
using Ecng.Xaml;
using Ecng.ComponentModel;

namespace fx.Algorithm
{
    public class FxNewsEvent : BindableBase, IEquatable<FxNewsEvent>, IComparable, IComparable<FxNewsEvent>
    {
        string _title = string.Empty;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetValue( ref _title, value );
            }
        }

        string _country = string.Empty;

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                SetValue( ref _country, value );
            }
        }

        bool _hasTime = false;

        public bool HasTime
        {
            get
            {
                return _hasTime;
            }
            set
            {
                SetValue( ref _hasTime, value );
            }
        }

        string _countDown = string.Empty;

        public string CountDown
        {
            get
            {
                return _countDown;
            }
            set
            {
                SetValue( ref _countDown, value );
            }
        }

        DateTime _EventTimeUTC = DateTime.MinValue;

        public DateTime EventTimeUTC
        {
            get
            {
                return _EventTimeUTC;
            }
            set
            {
                SetValue( ref _EventTimeUTC, value );

                NewsLocalTime = _EventTimeUTC.ToLocalTime( );
            }
        }

        DateTime _newsLocalTime = DateTime.MinValue;

        public DateTime NewsLocalTime
        {
            get
            {
                return _newsLocalTime;
            }
            set
            {
                SetValue( ref _newsLocalTime, value );
            }
        }

        

        EventImpact _impact = EventImpact.Unknown;

        public EventImpact Impact
        {
            get
            {
                return _impact;
            }
            set
            {
                SetValue( ref _impact, value );
            }
        }

        string _forecast = string.Empty;

        public string Forecast
        {
            get
            {
                return _forecast;
            }
            set
            {
                SetValue( ref _forecast, value );
            }
        }

        string _previous = string.Empty;

        public string Previous
        {
            get
            {
                return _previous;
            }
            set
            {
                SetValue( ref _previous, value );
            }
        }

        public FxNewsEvent( string title, string country, bool hasTime, DateTime dateTime, string countdown, EventImpact impact, string forecast, string previous )
        {
            Title = title;
            Country = country;
            HasTime = hasTime;
            EventTimeUTC = dateTime;
            CountDown = countdown;
            Impact = impact;
            Forecast = forecast;
            Previous = previous;
        }

        //public FxNewsEvent( INewsEvent other )
        //{
        //    Title = other.Title;
        //    Country = other.Country;
        //    HasTime = other.HasTime;
        //    EventTimeUTC = other.EventTimeUTC;
        //    CountDown = other.CountDown;
        //    Impact = other.Impact;
        //    Forecast = other.Forecast;
        //    Previous = other.Previous;
        //}

        public override bool Equals(object obj)
        {
            if (obj is FxNewsEvent)
            {
                return Equals((FxNewsEvent)obj);
            }

            return base.Equals(obj);
        }

        public static bool operator ==(FxNewsEvent first, FxNewsEvent second)
        {
            if (first == null)
            {
                return second == null;
            }

            return first.Equals(second);
        }

        public static bool operator !=(FxNewsEvent first, FxNewsEvent second)
        {
            return !(first == second);
        }

        public bool Equals(FxNewsEvent other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(_title, other._title) && Equals(_country, other._country) && _hasTime.Equals(other._hasTime) && Equals(_countDown, other._countDown) && _EventTimeUTC.Equals(other._EventTimeUTC) && _impact.Equals(other._impact) && Equals(_forecast, other._forecast) && Equals(_previous, other._previous);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                if (_title != null)
                {
                    hashCode = (hashCode * 53) ^ EqualityComparer<string>.Default.GetHashCode(_title);
                }

                if (_country != null)
                {
                    hashCode = (hashCode * 53) ^ EqualityComparer<string>.Default.GetHashCode(_country);
                }

                hashCode = (hashCode * 53) ^ _hasTime.GetHashCode();
                if (_countDown != null)
                {
                    hashCode = (hashCode * 53) ^ EqualityComparer<string>.Default.GetHashCode(_countDown);
                }

                hashCode = (hashCode * 53) ^ _EventTimeUTC.GetHashCode();
                hashCode = (hashCode * 53) ^ (int)_impact;
                if (_forecast != null)
                {
                    hashCode = (hashCode * 53) ^ EqualityComparer<string>.Default.GetHashCode(_forecast);
                }

                if (_previous != null)
                {
                    hashCode = (hashCode * 53) ^ EqualityComparer<string>.Default.GetHashCode(_previous);
                }

                return hashCode;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            FxNewsEvent other = obj as FxNewsEvent;
            if (other == null)
            {
                throw new ArgumentException(nameof(obj) + " is not a " + nameof(FxNewsEvent));
            }

            return CompareTo(other);
        }

        public int CompareTo(FxNewsEvent other)
        {
            if (other == null)
            {
                return 1;
            }

            int result = 0;
            result = _title.CompareTo(other._title);
            if (result != 0)
            {
                return result;
            }

            result = _country.CompareTo(other._country);
            if (result != 0)
            {
                return result;
            }

            result = _hasTime.CompareTo(other._hasTime);
            if (result != 0)
            {
                return result;
            }

            result = _countDown.CompareTo(other._countDown);
            if (result != 0)
            {
                return result;
            }

            result = _EventTimeUTC.CompareTo(other._EventTimeUTC);
            if (result != 0)
            {
                return result;
            }

            result = _impact.CompareTo(other._impact);
            if (result != 0)
            {
                return result;
            }

            result = _forecast.CompareTo(other._forecast);
            if (result != 0)
            {
                return result;
            }

            result = _previous.CompareTo(other._previous);
            if (result != 0)
            {
                return result;
            }

            return result;
        }
    }

    public class ImportantNewsComingMessage
    {
        private FxNewsEvent _newsEvent;

        public FxNewsEvent NewsEvent
        {
            get
            {
                return _newsEvent;
            }
            set
            {
                _newsEvent = value;
            }
        }

        public ImportantNewsComingMessage( FxNewsEvent news )
        {
            _newsEvent = news;
        }
    }

    public class EconomicCalenderDataSourceUpdateMessage
    {
        public string Symbol
        {
            get;
            set;
        }

        public ObservableCollectionEx< FxNewsEvent > NewBindingList
        {
            get;
            set;
        }

        public EconomicCalenderDataSourceUpdateMessage( string symbol, ObservableCollectionEx< FxNewsEvent > bindingList )
        {
            Symbol = symbol;

            NewBindingList = bindingList;
        }
    }
}
