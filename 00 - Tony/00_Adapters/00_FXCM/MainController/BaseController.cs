using fxcore2;
using StockSharp.FxConnectFXCM.Freemind;
using System;
using System.Collections.Generic; 
using System.Text;

#pragma warning disable CS3003 // Type is not CLS-compliant
#pragma warning disable CS3008 
#pragma warning disable CS3001 

namespace StockSharp.FxConnectFXCM
{
    public struct Offer
    {
        /// <summary>
        /// Current ask level. May be NaN(MinValue) when no value established.
        /// </summary>
        private double? _ask;

        public double? Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }

        /// <summary>
        /// Current bid level. May be NaN(MinValue) when no value established.
        /// </summary>
        private double? _bid;

        public double? Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        private int? _volume;

        /// <summary>
        ///
        /// </summary>
        public int? Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private double? _open;

        /// <summary>
        ///
        /// </summary>
        public double? Open
        {
            get { return _open; }
            set { _open = value; }
        }

        private double? _high;

        /// <summary>
        ///
        /// </summary>
        public double? High
        {
            get { return _high; }
            set { _high = value; }
        }

        private double? _low;

        /// <summary>
        ///
        /// </summary>
        public double? Low
        {
            get { return _low; }
            set { _low = value; }
        }

        /// <summary>
        /// Current spread level. May be NaN(MinValue) when no value established.
        /// </summary>
        public double? Spread
        {
            get
            {
                double? ask = _ask;
                double? bid = _bid;

                if ( ask.HasValue &&
                     bid.HasValue )
                {
                    return ask.Value - bid.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The current time on the quotation provider.
        /// </summary>
        private DateTime? _time;

        public DateTime? Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public bool IsFullySet
        {
            get { return _ask.HasValue && _bid.HasValue && _high.HasValue && _low.HasValue && _open.HasValue && _time.HasValue && _volume.HasValue; }
        }

        /// <summary>
        ///
        /// </summary>
        public Offer( double? ask, double? bid, int? volume, DateTime? time )
        {
            _ask = ask;
            _bid = bid;
            _volume = volume;
            _time = time;

            _low = null;
            _high = null;
            _open = null;
        }

        public Offer( IOffer input )
        {
            _ask = input.Ask;
            _bid = input.Bid;
            _volume = input.MinuteVolume;
            _time = input.LastUpdate;

            _low = null;
            _high = null;
            _open = null;
        }

        /// <summary>
        ///
        /// </summary>
        public Offer( double? ask, double? bid, double? open, double? close, double? high, double? low, int? volume, DateTime? time )
        {
            _ask = ask;
            _bid = bid;
            _high = high;
            _low = low;
            _open = open;
            _volume = volume;
            _time = time;
        }        
    }

    public interface IController
    {
        O2GSession GetTradingSession { get; }

        bool Wait( int second );
    }

    public interface IBaseController : IDisposable
    {
        bool StartWork( bool isMainDataSource, bool weekendMode );

        void StopWork( );

        string MainLoginName { get; set; }

        bool InitializeTimeZone( );

        bool Wait( int second );
    }

    public interface IOfferController : IBaseController
    {
        bool SubscribeSymbol( string symbolName );

        event ItemEventHandler< IOffer > OfferUpdatedEvent;                    // Raised on a thread pool call.              
    }

    public class BaseController : IController
    {
        protected readonly O2GSession                                             _fxSessionId;

        protected O2GLoginRules                                                   _loginRules;

        protected bool                                                            _isInitFailed;

        /// <summary>
        /// The event used to wait for Offers loading state.
        /// </summary>
        protected readonly ManagedAutoResetEvent                                  _syncEventReceived = new ManagedAutoResetEvent( false );

        /// <summary>
        /// The handle of the ForexConnect trading session
        /// </summary>
        public O2GSession GetTradingSession
        {
            get
            {
                return _fxSessionId;
            }
        }

        public BaseController( O2GSession session )
        {
            _fxSessionId = session;
            _isInitFailed = false;
            _loginRules = _fxSessionId.getLoginRules( );
        }

        public bool Wait( int second )
        {
            if ( _isInitFailed )
                return false;

            bool eventOcurred = _syncEventReceived.WaitOne( second * 1000 );

            if ( !eventOcurred )
            {
                _isInitFailed = true;
                //SystemMonitor.Error( "Timeout occurred during waiting for loading of Offers table" );
            }


            if ( _isInitFailed )
                return false;

            return eventOcurred;
        }

    }
}


