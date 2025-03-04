using Ecng.Logging;
using fxcore2;
using StockSharp.Messages;
using System;
using System.Collections.Generic; 
using System.Net;
using System.Text;

namespace StockSharp.FxConnectFXCM.MainManager
{
    public class FxConnectionManager : BaseLogReceiver, IO2GSessionStatus
	{
        private FxConnectFxcmMsgAdapter    _msgAdapter;
        private O2GSession            _fxSessionId; // The handle of the ForexConnect trading session.        
        private ManagedAutoResetEvent _syncSessionEvent = new ManagedAutoResetEvent( false ); // The event of the ForexConnect trading session.        

        public FxConnectionManager( FxConnectFxcmMsgAdapter parentAdapter )
        {
            _msgAdapter      = parentAdapter;            
        
        }

		public void Reset( )
		{
			//if ( _fxDataManager != null )
   //         {
   //             _fxDataManager.Reset( );
   //         }

			//if ( _fxTradesManager != null )
   //         {
			//	_fxTradesManager.Reset( );
			//}
		}

		public void Login( O2GSession session, string login, string password, string address, string isDemo, string proxyIp, int proxyPort )
        {
			this.AddInfoLog( "Fxcm Server = {0}, User = {1}, Proxy={2}:{3}", address, login, proxyIp, proxyPort );

			if ( ! string.IsNullOrEmpty( proxyIp ) && proxyPort > 0 )
            {
				O2GTransport.setProxy( proxyIp, proxyPort, "", "" );
			}
            

            // create the trading session and subscribe for it's events
            _fxSessionId = session;
            _fxSessionId.subscribeSessionStatus( this );

			_fxSessionId.login( login, password, address, isDemo );
		}
        public void onLoginFailed( string failedString )
        {			
			ConnectMessage msg = new ConnectMessage( );
			msg.Error = new InvalidOperationException( failedString );
			_msgAdapter.SendOutMessage( msg );
		}

        public void onSessionStatusChanged( O2GSessionStatusCode code )
        {
			switch ( code )
			{
				case O2GSessionStatusCode.Disconnected:
				{
					this.AddInfoLog( "Session Status Changed = Disconnected" );

					_msgAdapter.Reset( );
					_msgAdapter.SendOutMessage( new DisconnectMessage( ) );
				}
				break;

				case O2GSessionStatusCode.Connecting:
                {
					this.AddInfoLog( "Session Status Changed = Connecting" );
				}
					break;

				case O2GSessionStatusCode.TradingSessionRequested:
				{
					this.AddInfoLog( "Session Status Changed = TradingSessionRequested" );

					var o2Gsession = _msgAdapter.GetSession( );

					if ( o2Gsession == null )
						return;

					using ( var enumerator = ( ( IEnumerable<O2GSessionDescriptor> ) o2Gsession.getTradingSessionDescriptors( ) ).GetEnumerator( ) )
					{
						while ( enumerator.MoveNext( ) )
						{
							O2GSessionDescriptor current = enumerator.Current;
							if ( current.RequiresPin )
							{
								//o2Gsession.setTradingSession( current.Id, _msgAdapter.Pin );
							}
						}
						break;
					}
				}


				case O2GSessionStatusCode.Connected:
				{
					this.AddInfoLog( "Session Status Changed = Connected" );
					_msgAdapter.ProcessConnectedToServer( );
				}
				break;

				case O2GSessionStatusCode.Reconnecting:
                {
					this.AddInfoLog( "Session Status Changed = Reconnecting" );
				}
				break;

				case O2GSessionStatusCode.Disconnecting:
                {
					this.AddInfoLog( "Session Status Changed = Disconnecting" );
				}
				break;

				case O2GSessionStatusCode.SessionLost:
				{
					this.AddInfoLog( "Session Status Changed = SessionLost" );

					var msg = new ConnectMessage( );
					msg.Error = new InvalidOperationException( code.ToString( ) );
					_msgAdapter.SendOutMessage( msg );
				}
				break;

				case O2GSessionStatusCode.PriceSessionReconnecting:
                {
					this.AddInfoLog( "Session Status Changed = PriceSessionReconnecting" );
				}
				break;

				case O2GSessionStatusCode.Unknown:
				{
					this.AddInfoLog( "Session Status Changed = Unknown" );

					var UMsg = new ConnectMessage( );
					UMsg.Error = new InvalidOperationException( code.ToString( ) );
					_msgAdapter.SendOutMessage( UMsg );
				}
				break;

				default:
                {
					throw new ArgumentOutOfRangeException( "status", code, null );
				}
					
			}
		}

        
    }
}
