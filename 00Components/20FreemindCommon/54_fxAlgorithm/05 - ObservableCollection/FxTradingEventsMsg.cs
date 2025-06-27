using System;
using System.ComponentModel;
using DevExpress.Mvvm;
using fx.Definitions;

namespace fx.Algorithm
{
    public class FxTradingEventsMsg : BindableBase
    {
        fxMsgType _lastMsgType;
        string _lastMessage;

        private TimeSpan _period;
        private string _periodString;
        //private DateTime _lastMsgTime;

        public FxTradingEventsMsg( TimeSpan period )
        {
            _period = period;
            _periodString = FinancialHelper.GetPeriodString( period );
        }

        public string LastMessage
        {
            get
            {
                return _lastMessage;
            }
            set
            {
                SetValue( ref _lastMessage, value );
            }
        }

        public fxMsgType LastMsgType
        {
            get
            {
                return _lastMsgType;
            }
            set
            {
                SetValue( ref _lastMsgType, value );
            }
        }

        public TimeSpan Period
        {
            get
            {
                return _period;
            }
            set
            {
                SetValue( ref _period, value );
            }
        }

        public string PeriodString
        {
            get
            {
                if( _period == TimeSpan.FromTicks( 1 ) )
                {
                    return "12 - 1t";
                }
                else if( _period == TimeSpan.FromMinutes( 1 ) )
                {
                    return "11 - m1";
                }
                else if( _period == TimeSpan.FromMinutes( 4 ) )
                {
                    return "10 - m4";
                }
                else if( _period == TimeSpan.FromMinutes( 5 ) )
                {
                    return "09 - m5";
                }

                else if( _period == TimeSpan.FromMinutes( 15 ) )
                {
                    return "08 - m15";
                }
                else if( _period == TimeSpan.FromMinutes( 30 ) )
                {
                    return "07 - m30";
                }
                else if( _period == TimeSpan.FromHours( 1 ) )
                {
                    return "06 - H1";
                }
                else if( _period == TimeSpan.FromHours( 2 ) )
                {
                    return "05 - H2";
                }
                else if( _period == TimeSpan.FromHours( 4 ) )
                {
                    return "04 - H4";
                }
                else if( _period == TimeSpan.FromDays( 1 ) )
                {
                    return "03 - 1D";
                }
                else if( _period == TimeSpan.FromDays( 7 ) )
                {
                    return "02 - 1W";
                }
                else if( _period == TimeSpan.FromDays( 30 ) )
                {
                    return "01 - 1M";
                }

                return "Unknown";
            }
        }
    }
}
