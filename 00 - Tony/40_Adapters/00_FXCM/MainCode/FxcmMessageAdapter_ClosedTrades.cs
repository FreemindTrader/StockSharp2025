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
		private SynchronizedDictionary<string, FxClosedTradesCollection> _recentClosedTrades        = new SynchronizedDictionary<string, FxClosedTradesCollection>();
		private void OnClosedTradesTableLoaded( ISubscriptionMessage msg, O2GResponse response )
		{
			if ( response.Type == O2GResponseType.GetTrades )
			{				
				var o2Gsession = GetSession( );

				if ( o2Gsession == null )
					return;

				var factory = o2Gsession.getResponseReaderFactory( );

				string accountName = string.Empty;

				if ( factory != null )
				{
					var reader = factory.createClosedTradesTableReader( response );

					string instrument = null;

					FxClosedTradesCollection closedTradesCollection = new FxClosedTradesCollection();

					for ( int i = 0; i < reader.Count; i++ )
					{
						var row = reader.getRow( i );

						instrument = GFMgr.GetSymbolFromOfferId(  row.OfferID );

						closedTradesCollection.Add( Login, instrument, row );

						accountName = row.AccountName;
					}

					
					_recentClosedTrades.TryAdd2( accountName, closedTradesCollection );

					if ( closedTradesCollection.Count > 0 )
					{						
						var tradeMsgs = closedTradesCollection.GetClosedTradesMessage( Login, accountName );
						
						foreach ( var posMsg in tradeMsgs )
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

		private void OnClosedTradesTableUpdate( O2GClosedTradeRow row, O2GTableUpdateType updateType, DateTimeOffset serverTime )
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
			
			FxClosedTradesCollection closedTradesCollection = new FxClosedTradesCollection();			

			closedTradesCollection.Add( Login, instrument, row );

			SubaccountTradeDataRepo tradeMgr = GFMgr.CreateSubaccountTradeDataRepoByAccountName( Login, accountName );

			/* -------------------------------------------------------------------------------------------------------------------------------------------
			* 
			*  Now that we have the Profit for each position, we should 
			* 
			* ------------------------------------------------------------------------------------------------------------------------------------------- */


			FxPositionsSummary deleted = null;

			if ( updateType == O2GTableUpdateType.Insert )
			{
				deleted = tradeMgr.RemovePosition( accountName, row.AccountID, instrument, row.TradeID );
			}
			else if ( updateType == O2GTableUpdateType.Update )
			{
				
			}
			else if ( updateType == O2GTableUpdateType.Delete )
			{
				
			}

			if ( closedTradesCollection.Count > 0 )
			{
				var tradeMsgs = closedTradesCollection.GetClosedTradesMessage( Login, accountName );
			
				tradeMgr.CalculatePositionsSummary( accountName, row.AccountID );

				tradeMgr.CalculateMargin( accountName );

				tradeMgr.StartInitialPnLTask( accountName );

                tradeMgr.FinalizeAccountSummary( accountName, row );

				foreach ( var tradeMsg in tradeMsgs )
				{
					SendOutMessage( tradeMsg );
				}

				if ( deleted != null )
                {
					var msg                   = new PositionChangeMessage();
					msg.SecurityId            = row.OfferID.ToSecurityId();
					msg.PortfolioName         = accountName;
					msg.ServerTime            = row.CloseTime;
                    msg.OriginalTransactionId = myRequestId;

					int offerId               = Int32.Parse( row.OfferID );

					var subscribedSymbol      = GFMgr.GetSubscribedSymbolsByAccountNameAndId( Login, offerId );

					int baseUnit = 1;

					if ( subscribedSymbol != null )
					{
						baseUnit = subscribedSymbol.BaseUnitSize;
					}

					if ( deleted.IsNetLong( ) )
					{
						msg.Side = Sides.Buy;
						msg.TryAdd( PositionChangeTypes.AveragePrice, ( decimal ) deleted.BuyAvgOpen );						
						msg.TryAdd( PositionChangeTypes.SettlementPrice, ( decimal ) deleted.BuyClose.Value );						
					}
					else
					{
						msg.Side = Sides.Sell;
						msg.TryAdd( PositionChangeTypes.AveragePrice, ( decimal ) deleted.SellAvgOpen );						
						msg.TryAdd( PositionChangeTypes.SettlementPrice, ( decimal ) deleted.SellClose.Value );						
					}

					msg.TryAdd( PositionChangeTypes.CurrentValueInLots, ( decimal ) 0, true );
					msg.TryAdd( PositionChangeTypes.RealizedPnL, ( decimal ) deleted.GrossPl );
					msg.TryAdd( PositionChangeTypes.UnrealizedPnL, ( decimal ) 0, true );
					msg.TryAdd( PositionChangeTypes.BlockedValue, ( decimal ) 0, true );

					SendOutMessage( msg );
				}

				var accMsg = GFMgr.GetAccountSummaryMessage( Login, accountName, CurrentTime );

				SendOutMessage( accMsg );
			}
		}
	}
}
