using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using fxcore2;
using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM
{
    public class SymbolRequest
    {
        public SymbolRequest( TimeSpan period, string symbol )
        {
            _symbol = symbol;
            _period = period;
            _response = null;
        }

        string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
            }
        }

        O2GResponse _response;
        public O2GResponse Response
        {
            get { return _response; }
            set
            {
                _response = value;
            }
        }


        TimeSpan _period;
        public TimeSpan Period
        {
            get { return _period; }
            set
            {
                _period = value;
            }
        }

    }

    /// <summary>
    /// The Price History API communicator request result listener.
    /// </summary>
    public class PriceHistoryResponseListener : BaseLogReceiver, IO2GResponseListener
    {
        private SynchronizedDictionary<string, SymbolRequest> _requestToResponse = new SynchronizedDictionary<string, SymbolRequest>( );

        private SynchronizedDictionary<string, ManagedAutoResetEvent> _requestToEvent = new SynchronizedDictionary<string, ManagedAutoResetEvent>( );

        private SynchronizedDictionary<string, string> _requestToError = new SynchronizedDictionary<string, string>( );

        //private O2GSession _mSession;
        //private string _mRequestID;
        //private O2GResponse _mResponse;




        public PriceHistoryResponseListener( O2GSession session )
        {
            //_mSession = session;
        }

        /// <summary>
        /// Sets request.
        /// </summary>
        /// <param name   ="request"></param>
        public void SetRequestID( string sRequestID, TimeSpan period, string symbol )
        {
            _requestToResponse.TryAdd2( sRequestID, new SymbolRequest( period, symbol ) );

            if ( !_requestToEvent.ContainsKey( sRequestID ) )
            {
                var resetEvent = new ManagedAutoResetEvent( false );

                _requestToEvent.Add( sRequestID, resetEvent );
            }

        }




        /// <summary>
        /// Waits for a response event.
        /// </summary>
        public bool Wait( string requestId )
        {
            ManagedAutoResetEvent resetEvent = null;

            if ( _requestToEvent.TryGetValue( requestId, out resetEvent ) )
            {
                var waitResult = resetEvent.WaitOne( 30 * 1000 );

                _requestToEvent.Remove( requestId );

                return waitResult;
            }
            else
            {
                return false;
            }
        }

        public void Wait3Mins( string requestId )
        {
            ManagedAutoResetEvent resetEvent = null;

            if ( _requestToEvent.TryGetValue( requestId, out resetEvent ) )
            {
                resetEvent.WaitOne( 180 * 1000 );

                _requestToEvent.Remove( requestId );
            }
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        public O2GResponse GetResponse( string requestId )
        {
            O2GResponse output = null;
            SymbolRequest myResponse = _requestToResponse.TryGetAndRemove( requestId );

            if ( myResponse != null )
            {
                output = myResponse.Response;
            }

            return output;
        }



        public void onRequestCompleted( string requestId, O2GResponse response )
        {
            SymbolRequest myResponse = null;

            if ( _requestToResponse.TryGetValue( requestId, out myResponse ) )
            {
                myResponse.Response = response;

                ManagedAutoResetEvent resetEvent = null;

                if ( _requestToEvent.TryGetValue( requestId, out resetEvent ) )
                {
                    resetEvent.Set( );
                }


            }
            else
            {

            }
        }

        public void onRequestFailed( string requestId, string sError )
        {
            if ( _requestToResponse.ContainsKey( requestId ) )
            {
                var symbol = _requestToResponse[ requestId ].Symbol;
                var period = _requestToResponse[ requestId ].Period;


                if ( string.IsNullOrEmpty( sError ) )
                {
                    this.AddErrorLog( "There is no more data" );

                }
                else
                {
                    if ( !sError.ContainsIgnoreCase( "unsupported scope" ) )                    
                    {
                        string message = string.Format( "{0}: {1} Failed. Error = {2}", symbol, period, sError );

                        this.AddErrorLog( message );
                    }
                }

                _requestToError.TryAdd2( requestId, sError );

                ManagedAutoResetEvent resetEvent = null;

                if ( _requestToEvent.TryGetValue( requestId, out resetEvent ) )
                {
                    resetEvent.Set( );

                    _requestToEvent.Remove( requestId );
                }
            }
        }

        public string GetErrorString( string requestId )
        {
            string errorString = _requestToError.TryGetAndRemove( requestId );

            return errorString;
        }



        public void onTablesUpdates( O2GResponse data )
        {

        }
    }
}

