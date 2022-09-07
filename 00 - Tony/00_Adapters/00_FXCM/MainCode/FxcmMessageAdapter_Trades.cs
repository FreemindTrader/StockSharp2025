namespace StockSharp.FxConnectFXCM
{
	using System;
	using System.Collections.Generic; 
	using System.Linq;

	using Ecng.Collections;
	using Ecng.Common;
	using fxcore2;
	using MoreLinq;
    using StockSharp.FxConnectFXCM.Freemind;
    using StockSharp.Localization;
	using StockSharp.Messages;

	partial class FxConnectFxcmMsgAdapter
	{
		private void OnTradesTableLoaded( ISubscriptionMessage msg, O2GResponse response )
		{
			if ( response.Type == O2GResponseType.GetTrades )
			{
                SubaccountTradeDataRepo tradeMgr = null;
				var o2Gsession = GetSession( );

				if ( o2Gsession == null )
					return;

				var factory = o2Gsession.getResponseReaderFactory( );

				string accountName = string.Empty;
				string accountId = string.Empty;

				if ( factory != null )
				{
					var reader = factory.createTradesTableReader( response );
				
					string instrument = null;

					for ( int i = 0; i < reader.Count; ++i )
					{
						var row = reader.getRow( i );

						accountName = row.AccountName;
                        accountId = row.AccountID;

						instrument = GFMgr.GetSymbolFromOfferId( row.OfferID );

                        var pos = new FxDetailedPosition( Login, row );

						if ( tradeMgr == null )
                        {
							tradeMgr = GFMgr.CreateSubaccountTradeDataRepoByAccountName( Login, row.AccountName );							
						}

						tradeMgr.AddPosition( pos );
					}


					if ( reader.Count > 0 )
                    {
						tradeMgr.CalculatePositionsSummary( accountName, accountId );

						tradeMgr.CalculateMargin( accountName );

						tradeMgr.StartInitialPnLTask( accountName );

						var posMsgs = GFMgr.GetPositionSummaryMessage( Login, accountName );

						foreach ( var posMsg in posMsgs )
						{
							SendOutMessage( posMsg );
						}

						var accMsg = GFMgr.GetAccountSummaryMessage( Login, accountName, CurrentTime );

						SendOutMessage( accMsg );
					}
				}

				SendSubscriptionResult( msg );
			}					
		}

		private void OnTradesTableUpdate( O2GTradeRow row, O2GTableUpdateType updateType, DateTimeOffset serverTime )
		{
			string accountName = row.AccountName;

			if ( string.IsNullOrEmpty( accountName ) )
			{
				return;
			}

			string instrument = GFMgr.GetSymbolFromOfferId( row.OfferID );

			var customId = row.OpenOrderRequestTXT.Split(',').Select(x => x.Split('=')).Where(x => x.Length > 1 && !String.IsNullOrEmpty(x[0].Trim()) && !String.IsNullOrEmpty(x[1].Trim())).ToDictionary(x => x[0].Trim(), x => x[1].Trim());

			long myRequestId = 0;

			if ( customId.ContainsKey( "ID" ) )
			{
				myRequestId = customId[ "ID" ].To<int>( );
			}			

            var fxPosition    = new FxDetailedPosition( Login, row );

			var tradeMsg      = GFMgr.GetTradeMessage( fxPosition, myRequestId, updateType );

			var tradeMgr      = GFMgr.CreateSubaccountTradeDataRepoByAccountName( Login, accountName );

			if ( updateType == O2GTableUpdateType.Insert )
            {
                tradeMgr.AddPosition( fxPosition );
			}
			else if ( updateType == O2GTableUpdateType.Update )
			{				
				tradeMgr.TryUpdatePosition( fxPosition );
			}
			else if ( updateType == O2GTableUpdateType.Delete )
            {				
				tradeMgr.RemovePosition( accountName, row.AccountID, instrument, row.TradeID );

                return;
			}

			SendOutMessage( tradeMsg );

			tradeMgr.CalculatePositionsSummary( accountName, row.AccountID );

			tradeMgr.CalculateMargin( accountName );

			tradeMgr.StartInitialPnLTask( accountName );

			var posMsgs = GFMgr.GetPositionSummaryMessage( Login, accountName );

			foreach ( var posMsg in posMsgs )
			{
				SendOutMessage( posMsg );
			}

			var accMsg = GFMgr.GetAccountSummaryMessage( Login, accountName, CurrentTime );

			SendOutMessage( accMsg );			
		}
	}
}
