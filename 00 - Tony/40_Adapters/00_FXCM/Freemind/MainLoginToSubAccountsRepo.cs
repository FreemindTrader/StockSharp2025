using StockSharp.Messages;
using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public class MainLoginToSubAccountsRepo
    {
        string _mainLoginName = string.Empty;
        public MainLoginToSubAccountsRepo( string mainLoginName )
        {
            _mainLoginName = mainLoginName;
        }
        #region Data fields

        private readonly FxDetailedAccountsCollection                          _detailedAccountsCollection    = new FxDetailedAccountsCollection();


        public int AccountsCount( )
        {
            return _detailedAccountsCollection.Count;
        }


        public IEnumerable<IDetailedAccount> AccountsList
        {
            get
            {
                return _detailedAccountsCollection;
            }
        }

        private bool _subAccountsLoaded = false;
        public bool SubAccountsLoaded
        {
            get { return _subAccountsLoaded; }
        }


        #endregion

        public void Clear( )
        {

            _detailedAccountsCollection.Clear( );
        }

        public FxDetailedAccountsCollection DetailedAccountsCollection => _detailedAccountsCollection;



        public void AddSubAccount( string mainLoginName,
                                    string accountId,
                                    string accountKind,
                                    string accountName,
                                    int amountLimit,
                                    double balance,
                                    int baseUnitSize,
                                    DateTime lastMarginCallDate,
                                    string leverageProfileID,
                                    double m2MEquity,
                                    bool maintenanceFlag,
                                    string maintenanceType,
                                    string managerAccountID,
                                    string marginCallFlag,
                                    double nonTradeEquity,
                                    double usedMargin,
                                    double usedMargin3,
                                    int subAccountType,
                                    string accountCurrency
                       )
        {

            FxDetailedAccount fxAccount = new FxDetailedAccount( accountId,
                                                                 accountKind,
                                                                 accountName,
                                                                 amountLimit,
                                                                 balance,
                                                                 baseUnitSize,
                                                                 lastMarginCallDate,
                                                                 leverageProfileID,
                                                                 m2MEquity,
                                                                 maintenanceFlag,
                                                                 maintenanceType,
                                                                 managerAccountID,
                                                                 marginCallFlag,
                                                                 nonTradeEquity,
                                                                 usedMargin,
                                                                 usedMargin3,
                                                                 subAccountType,
                                                                 accountCurrency

                                         );



            AddSubAccount( mainLoginName, fxAccount );
        }

        public void TryUpdateSubAccount( string mainLoginName,

                                        string accountId,
                                        string accountKind,
                                        string accountName,
                                        int amountLimit,
                                        double balance,
                                        int baseUnitSize,
                                        DateTime lastMarginCallDate,
                                        string leverageProfileID,
                                        double m2MEquity,
                                        bool maintenanceFlag,
                                        string maintenanceType,
                                        string managerAccountID,
                                        string marginCallFlag,
                                        double nonTradeEquity,
                                        double usedMargin,
                                        double usedMargin3
                       )
        {

            FxDetailedAccount fxAccount = new FxDetailedAccount( accountId,
                                                                 accountKind,
                                                                 accountName,
                                                                 amountLimit,
                                                                 balance,
                                                                 baseUnitSize,
                                                                 lastMarginCallDate,
                                                                 leverageProfileID,
                                                                 m2MEquity,
                                                                 maintenanceFlag,
                                                                 maintenanceType,
                                                                 managerAccountID,
                                                                 marginCallFlag,
                                                                 nonTradeEquity,
                                                                 usedMargin,
                                                                 usedMargin3
                                         );

            TryUpdateSubAccount( mainLoginName, fxAccount );


        }

        

        public void AddSubAccount( string mainLoginName, IDetailedAccount iAccount )
        {
            // Tony Lam: When I add an account, I will need to create the corresponding Trade StrategySpecialist and also set its account details so that we can have
            // calculate the account summary inside tradeDataRepo

            SubaccountTradeDataRepo tradeManager = GFMgr.CreateSubaccountTradeDataRepoByAccountName( mainLoginName, iAccount.AccountName );

            FxDetailedAccount fxAccount         = ( FxDetailedAccount ) iAccount;

            _detailedAccountsCollection.AddAccount( fxAccount );

            _subAccountsLoaded = true;

            tradeManager.AddDetailedAccountsCollection( _detailedAccountsCollection );
        }

        public void TryUpdateSubAccount( string mainLoginName, IDetailedAccount iAccount )
        {
            SubaccountTradeDataRepo tradeManager = GFMgr.CreateSubaccountTradeDataRepoByAccountName( mainLoginName, iAccount.AccountName );

            FxDetailedAccount fxAccount         = ( FxDetailedAccount ) iAccount;

            _detailedAccountsCollection.TryUpdateAccount( fxAccount );

            tradeManager.UpdateAccountSummaryBindingList( mainLoginName, fxAccount );
        }

        public void RemoveAccount( string mainLoginName, string accountName )
        {
            _detailedAccountsCollection.RemoveAccount( accountName );

            SubaccountTradeDataRepo tradeManager = GFMgr.CreateSubaccountTradeDataRepoByAccountName( mainLoginName, accountName );

            tradeManager.RemoveAccountFromBindingList( accountName );
        }






        //public void HandleOffer( FxOffer offerEvent )
        //{            
        //    TaskCalcAccountSummary( offerEvent );         
        //}

        //
        //

        //protected Task TaskCalcAccountSummary( FxOffer offerEvent )
        //{
        //    Task detectTask  = new Task(
        //                                    () =>
        //                                    {
        //                                        foreach ( IAccountSummary accountSummary in _accountsSummaryBindingList )
        //                                        {
        //                                            
        //                                        }

        //                                    }, GeneralHelper.GlobalExitToken()
        //                                );

        //    detectTask.Start();

        //    return detectTask;
        //}




    }


}

