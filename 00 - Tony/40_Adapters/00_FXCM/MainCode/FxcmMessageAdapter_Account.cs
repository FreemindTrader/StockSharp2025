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
		private MainLoginToSubAccountsRepo _subAccountsRepo;

        //private O2GAccountRow _accountsRow = null;

		private void OnAccountsTableLoaded( ISubscriptionMessage msg, O2GResponse response )
		{
			var o2Gsession = GetSession( );
			if ( o2Gsession == null )
				return;

			O2GResponseReaderFactory factory = o2Gsession.getResponseReaderFactory( );

			if ( factory == null )
			{
				if ( _canProcess != null && !_canProcess( false ) )
				{
					return;
				}

				throw new InvalidOperationException( );
			}

			if ( response.Type == O2GResponseType.GetAccounts )
			{
				var reader         = factory.createAccountsTableReader( response );

				int subAccountType = 1;

				int marginLeverage = 100;

				string accountType = IsDemo ? "Demo" : "Real";

				string currency    = "USD";

				o2Gsession     = GetSession( );

				if ( o2Gsession == null )
					return;

				var loginRules     = o2Gsession.getLoginRules( );

				var tradingSetting = loginRules.getTradingSettingsProvider();

				bool foundChanged  = false;

				for ( int i = 0; i < reader.Count; ++i )
				{
					var subAccount       = reader.getRow( i );

					var subscribedSymbol = GFMgr.GetSubscribedSymbolsByAccountName( Login );

					
                    {
						double dMmr          = 0, dEmr = 0, dLmr = 0;

						foreach ( var symbol in subscribedSymbol )
						{
							var tradeSymbol = GFMgr.GetSymbolFromOfferId( symbol.OfferID.ToString() );
							var baseUnit    = tradingSetting.getBaseUnitSize( tradeSymbol, subAccount );


							/* --------------------------------------------------------------------------------------------------------------------------------------------
							 * 
							 * EMR = Entry Margin Level
							 * MMR = Maintenance margin level.
							 * LMR = Limitation margin level. 
							 * 
							 * --------------------------------------------------------------------------------------------------------------------------------------------
							 */

							tradingSetting.getMargins( tradeSymbol, subAccount, ref dMmr, ref dEmr, ref dLmr );


							if ( ( symbol.BaseUnitSize != baseUnit ) || ( symbol.EMR != dEmr ) || ( symbol.MMR != dMmr ) || ( symbol.LMR != dLmr ) )
							{
								symbol.BaseUnitSize = baseUnit;
								symbol.EMR = dEmr;
								symbol.MMR = dMmr;
								symbol.LMR = dLmr;
								foundChanged = true;
							}
						}

						if ( foundChanged )
						{
							UpdateSubscribedSymbolMargin( Login, subscribedSymbol );
						}
					}

					

					var fxAccount = new FxDetailedAccount(
																subAccount.AccountID,
																subAccount.AccountKind,
																subAccount.AccountName,
																subAccount.AmountLimit,
																subAccount.Balance,
																subAccount.BaseUnitSize,
																subAccount.LastMarginCallDate,
																subAccount.LeverageProfileID,
																subAccount.M2MEquity,
																subAccount.MaintenanceFlag,
																subAccount.MaintenanceType,
																subAccount.ManagerAccountID,
																subAccount.MarginCallFlag,
																subAccount.NonTradeEquity,
																0.0,
																0.0

															);

					fxAccount.SubAccountType  = subAccountType;
					fxAccount.AccountCurrency = currency;
					fxAccount.AccountType     = accountType;
					fxAccount.MarginLeverage  = marginLeverage;

					if ( _subAccountsRepo == null )
					{
						_subAccountsRepo = GFMgr.GetOrCreateSubAccountsRepo( Login );
					}

					_subAccountsRepo.AddSubAccount( Login, fxAccount );

					long? accountId;

					if ( subAccount.AccountName.IsEmpty( ) )
					{
						if ( subAccount.AccountID.IsEmpty( ) )
						{
							return;
						}
					}

					accountId = subAccount.AccountID.To<int>( );
					string accountName;

					if ( subAccount.AccountName.IsEmpty( ) && accountId.HasValue )
					{
						accountName = GetAccountNameFromId( accountId.Value );
					}
					else
					{
						accountName = subAccount.AccountName;
					}

					_accountIdToName[ accountId.Value ] = accountName;

					

					var pMsg = new PortfolioMessage();
					pMsg.PortfolioName = subAccount.AccountName;
					pMsg.OriginalTransactionId = pMsg.TransactionId;

					SendOutMessage( pMsg );

                    var posMsg = GFMgr.GetAccountSummaryMessage( Login, accountName, CurrentTime );

					SendOutMessage( posMsg );
				}

				// We will wait till all the accounts have been loaded, before we retrieve all the Positions
				SetupTableResponseHandler( O2GTableType.Trades,       msg, null, new Action<ISubscriptionMessage, O2GResponse>( OnTradesTableLoaded       ) );
				SetupTableResponseHandler( O2GTableType.ClosedTrades, msg, null, new Action<ISubscriptionMessage, O2GResponse>( OnClosedTradesTableLoaded ) );
				

				SendSubscriptionResult( msg );
			}					
		}

		private void OnAccountsTableUpdate( O2GAccountRow row, O2GTableUpdateType updateType )
		{
			long? accountId;

			if ( row.AccountName.IsEmpty( ) )
			{
				if ( row.AccountID.IsEmpty( ) )
				{
					return;
				}
			}

			accountId = row.AccountID.To<int>( );
			string accountName;

			if ( row.AccountName.IsEmpty( ) && accountId.HasValue )
			{
				accountName = GetAccountNameFromId( accountId.Value );
			}
			else
			{
				accountName = row.AccountName;
			}

			FxDetailedAccount fxAccount = new FxDetailedAccount(    row.AccountID,
																		row.AccountKind,
																		row.AccountName,
																		row.AmountLimit,
																		row.Balance,
																		row.BaseUnitSize,
																		row.LastMarginCallDate,
																		row.LeverageProfileID,
																		row.M2MEquity,
																		row.MaintenanceFlag,
																		row.MaintenanceType,
																		row.ManagerAccountID,
																		row.MarginCallFlag,
																		row.NonTradeEquity,
																		0.0,
																		0.0
																   );

			fxAccount.SubAccountType = 1;
			fxAccount.AccountCurrency = "USD";

			if ( _subAccountsRepo == null )
            {
				_subAccountsRepo = GFMgr.GetOrCreateSubAccountsRepo( Login );
			}

            _subAccountsRepo.TryUpdateSubAccount( Login, fxAccount );

			_accountIdToName[ accountId.Value ] = accountName;

			var msg              = new PortfolioMessage( );
			msg.PortfolioName    = row.AccountName;
			SendOutMessage( msg );

			var posMsg           = new PositionChangeMessage();
			posMsg.SecurityId    = SecurityId.Money;
			posMsg.PortfolioName = row.AccountName;
			posMsg.ServerTime    = CurrentTime;


			var balance          = row.Balance;
			var beginningEquity  = row.M2MEquity;
			var dayPl            = Math.Round( ( balance - beginningEquity ), 2 );
			var equity           = Math.Round( ( balance ), 2 );


			posMsg.TryAdd( PositionChangeTypes.BeginValue, beginningEquity.ToDecimal( ), true );
			posMsg.TryAdd( PositionChangeTypes.CurrentValue, equity.ToDecimal( ), true );
			posMsg.TryAdd( PositionChangeTypes.UnrealizedPnL, dayPl.ToDecimal( ), true );

			SendOutMessage( posMsg );
		}

		private void UpdateSubscribedSymbolMargin( string accountName, List<DBSubscribedSymbols> subscribedSymbols )
		{
			foreach ( DBSubscribedSymbols subscribedSymbol in subscribedSymbols )
			{
				var foundSymbol = GFMgr.GetSubscribedSymbolsByAccountNameAndId( Login, subscribedSymbol.OfferID );				

				if ( foundSymbol != null )
				{
					foundSymbol.BaseUnitSize = subscribedSymbol.BaseUnitSize;
					foundSymbol.EMR = subscribedSymbol.EMR;
					foundSymbol.MMR = subscribedSymbol.MMR;
					foundSymbol.LMR = subscribedSymbol.LMR;
				}
			}			
		}
	}
}
