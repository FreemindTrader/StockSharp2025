using Ecng.Collections;
using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public class FxDetailedAccountsCollection : IDetailedAccountsCollection
    {
        private readonly SynchronizedDictionary< string, FxDetailedAccount > _idToAccountInfo = new SynchronizedDictionary< string, FxDetailedAccount >();

        private readonly SynchronizedDictionary< string, string > _accountNameToId = new SynchronizedDictionary< string, string >();

        public int Count => _idToAccountInfo.Count;

        //public IDetailedAccount this[ int index ]
        //{
        //    get { return _idToAccountInfo.ValueAt( index ); }
        //}

        public IDetailedAccount this[ string accountId ]
        {
            get
            {
                return FindAccountById( accountId );
            }
        }

        public IEnumerator<IDetailedAccount> GetEnumerator( )
        {
            return new EnumeratorHelper<FxDetailedAccount, IDetailedAccount>( _idToAccountInfo.Values.GetEnumerator( ) );
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator( )
        {
            return _idToAccountInfo.GetEnumerator( );
        }

        


        public void AddAccount( string accountId,
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

            AddAccount( fxAccount );
        }

        public void AddAccount( FxDetailedAccount iAccount )
        {
            if ( !_idToAccountInfo.ContainsKey( iAccount.AccountID ) )
            {
                _idToAccountInfo.TryAdd2( iAccount.AccountID, iAccount );
            }
            else
            {
                FxDetailedAccount foundItem = _idToAccountInfo[ iAccount.AccountID ];

                if ( !foundItem.Equals( iAccount ) )
                {
                    foundItem.CopyFrom( iAccount );
                }
            }


            if ( !_accountNameToId.ContainsKey( iAccount.AccountName ) )
            {
                _accountNameToId.TryAdd2( iAccount.AccountName, iAccount.AccountID );
            }
            else
            {
                _accountNameToId[ iAccount.AccountName ] = iAccount.AccountID;
            }



        }

        public void TryUpdateAccount( FxDetailedAccount iAccount )
        {
            if ( _idToAccountInfo.ContainsKey( iAccount.AccountID ) )
            {
                FxDetailedAccount foundItem = _idToAccountInfo[ iAccount.AccountID ];

                if ( !foundItem.Equals( iAccount ) )
                {
                    foundItem.CopyFrom( iAccount );
                }
            }


        }

        public void RemoveAccount( string accountId )
        {
            if ( _idToAccountInfo.ContainsKey( accountId ) )
            {
                _idToAccountInfo.Remove( accountId );
            }

            if ( _accountNameToId.ContainsKey( accountId ) )
            {
                _accountNameToId.Remove( accountId );
            }

        }

        /// <summary>
        /// Removes all offers from the collection
        /// </summary>
        public void Clear( )
        {
            _idToAccountInfo.Clear( );
        }

        public IDetailedAccount FindAccountById( string accountId )
        {
            FxDetailedAccount accountInfo;

            if ( _idToAccountInfo.TryGetValue( accountId, out accountInfo ) )
            {
                return accountInfo;
            }

            return null;
        }


        public string FindAccountIdByName( string accountName )
        {
            if ( _accountNameToId.ContainsKey( accountName ) )
            {
                return _accountNameToId[ accountName ];
            }

            return null;
        }

        
    }
}

