using fxcore2;
using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public class OrderMonitor
    {
        public static bool IsOpeningOrder( O2GOrderRow order )
        {
            return order.Type.StartsWith( "O" );
        }

        public static bool IsClosingOrder( O2GOrderRow order )
        {
            return order.Type.StartsWith( "C" );
        }


        private enum OrderState
        {
            OrderExecuting,
            OrderExecuted,
            OrderCanceled,
            OrderRejected
        }

        private volatile OrderState                 _orderState;
        private List< O2GTradeRow >        _openPositions;
        private List< O2GClosedTradeRow  > _closedTrades;
        private volatile int                        _totalAmount;
        private volatile int                        _rejectedAmount;
        private O2GOrderRow                         _order;
        private string                              _rejectedReason;

        public enum ExecutionResult
        {
            Executing,
            Executed,
            PartialRejected,
            FullyRejected,
            Canceled
        };


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="order">Order for monitoring of execution</param>
        public OrderMonitor( O2GOrderRow order )
        {
            _order           = order;
            _openPositions   = new List<O2GTradeRow>( );
            _closedTrades    = new List<O2GClosedTradeRow>( );
            _orderState      = OrderState.OrderExecuting;
            _executionResult = ExecutionResult.Executing;
            _totalAmount     = 0;
            _rejectedAmount  = 0;
            _rejectedReason  = "";
        }

        /// <summary>
        /// Process trade adding during order execution
        /// </summary>
        public void OnTradeAdded( O2GTradeRow tradeRow )
        {
            string sTradeOrderID = tradeRow.OpenOrderID;
            string sOrderID      = _order.OrderID;

            if ( sTradeOrderID.Equals( sOrderID ) )
            {
                _openPositions.Add( tradeRow );

                if ( _orderState == OrderState.OrderExecuted ||
                     _orderState == OrderState.OrderRejected ||
                     _orderState == OrderState.OrderCanceled )
                {
                    if ( IsAllTradesReceived( ) )
                    {
                        SetResult( true );
                    }
                }
            }
        }

        /// <summary>
        /// Process order data changing during execution
        /// </summary>
        public void OnOrderChanged( O2GOrderRow orderRow )
        {
            //STUB
        }

        /// <summary>
        /// Process order deletion as result of execution
        /// </summary>
        public void OnOrderDeleted( O2GOrderRow orderRow )
        {
            string sDeletedOrderID = orderRow.OrderID;
            string sOrderID        = _order.OrderID;

            if ( sDeletedOrderID.Equals( sOrderID ) )
            {
                // Store Reject amount
                if ( OrderRowStatus.Rejected.Equals( orderRow.Status ) )
                {
                    _orderState = OrderState.OrderRejected;
                    _rejectedAmount = orderRow.Amount;
                    _totalAmount = orderRow.OriginAmount - _rejectedAmount;

                    if ( !string.IsNullOrEmpty( _rejectedReason ) && IsAllTradesReceived( ) )
                    {
                        SetResult( true );
                    }
                }
                else if ( OrderRowStatus.Canceled.Equals( orderRow.Status ) )
                {
                    _orderState = OrderState.OrderCanceled;
                    _rejectedAmount = orderRow.Amount;
                    _totalAmount = orderRow.OriginAmount - _rejectedAmount;

                    if ( IsAllTradesReceived( ) )
                    {
                        SetResult( false );
                    }
                }
                else
                {
                    _rejectedAmount = 0;
                    _totalAmount = orderRow.OriginAmount;
                    _orderState = OrderState.OrderExecuted;

                    if ( IsAllTradesReceived( ) )
                    {
                        SetResult( true );
                    }

                }
            }
        }

        /// <summary>
        /// Process reject message as result of order execution
        /// </summary>
        public void OnMessageAdded( O2GMessageRow messageRow )
        {
            if ( _orderState == OrderState.OrderRejected ||
                 _orderState == OrderState.OrderExecuting )
            {
                bool IsRejectMessage = CheckAndStoreMessage( messageRow );
                if ( _orderState == OrderState.OrderRejected && IsRejectMessage )
                    SetResult( true );
            }
        }

        /// <summary>
        /// Process trade closing during order execution
        /// </summary>
        public void OnClosedTradeAdded( O2GClosedTradeRow closedTradeRow )
        {
            string sOrderID            = _order.OrderID;
            string sClosedTradeOrderID = closedTradeRow.CloseOrderID;

            if ( sClosedTradeOrderID.Equals( sOrderID ) )
            {
                _closedTrades.Add( closedTradeRow );

                if ( _orderState == OrderState.OrderExecuted ||
                     _orderState == OrderState.OrderRejected ||
                     _orderState == OrderState.OrderCanceled )
                {
                    if ( IsAllTradesReceived( ) )
                        SetResult( true );
                }
            }
        }

        /// <summary>
        /// Event about order execution is completed and all affected trades as opened/closed, all reject/cancel processed
        /// </summary>
        public event EventHandler OrderCompleted;

        /// <summary>
        /// Result of Order execution
        /// </summary>
        public ExecutionResult Result
        {
            get { return _executionResult; }
        }

        private volatile ExecutionResult _executionResult;

        /// <summary>
        /// Order execution is completed (with any result)
        /// </summary>
        public bool IsOrderCompleted
        {
            get { return ( _executionResult != ExecutionResult.Executing ); }
        }

        /// <summary>
        /// Monitored order
        /// </summary>
        public O2GOrderRow Order
        {
            get { return _order; }
        }

        /// <summary>
        /// List of Trades which were opened as effects of order execution
        /// </summary>
        //public IReadOnlyCollection<O2GTradeRow> Trades
        //{
        //    get { return _openPositions.AsReadOnlyCollection( ); }
        //}

        /// <summary>
        /// List of Trades which were closed as effects of order execution
        /// </summary>
        //public IReadOnlyCollection<O2GClosedTradeRow> ClosedTrades
        //{
        //    get { return _closedTrades.AsReadOnlyCollection( ); }
        //}

        /// <summary>
        /// Amount of rejected part of order
        /// </summary>
        public int RejectAmount
        {
            get { return _rejectedAmount; }
        }

        /// <summary>
        /// Info message with a reason of reject
        /// </summary>
        public string RejectMessage
        {
            get { return _rejectedReason; }
        }


        private void SetResult( bool bSuccess )
        {
            if ( bSuccess )
            {
                if ( _rejectedAmount == 0 )
                {
                    _executionResult = ExecutionResult.Executed;
                }
                else
                {
                    _executionResult = ( _openPositions.Count == 0 && _closedTrades.Count == 0 )
                                        ? ExecutionResult.FullyRejected
                                        : ExecutionResult.PartialRejected;
                }

            }
            else
            {
                _executionResult = ExecutionResult.Canceled;
            }


            if ( OrderCompleted != null )
                OrderCompleted( this, EventArgs.Empty );
        }

        private bool IsAllTradesReceived( )
        {
            if ( _orderState == OrderState.OrderExecuting )
            {
                return false;
            }

            int iCurrentTotalAmount = 0;

            for ( int i = 0; i < _openPositions.Count; i++ )
            {
                iCurrentTotalAmount += _openPositions[ i ].Amount;
            }

            for ( int i = 0; i < _closedTrades.Count; i++ )
            {
                iCurrentTotalAmount += _closedTrades[ i ].Amount;
            }

            return iCurrentTotalAmount == _totalAmount;
        }

        private bool CheckAndStoreMessage( O2GMessageRow message )
        {
            string sFeature = message.Feature;

            if ( MessageFeature.MarketCondition.Equals( sFeature ) )
            {
                string sText = message.Text;
                int findPos  = sText.IndexOf( _order.OrderID );

                if ( findPos > -1 )
                {
                    _rejectedReason = sText;
                    return true;
                }
            }
            return false;
        }
    }

    internal class OrderRowStatus
    {
        public static string Rejected = "R";
        public static string Canceled = "C";
        public static string Executed = "F";
        //...
    }

    internal class MessageFeature
    {
        public static String MarketCondition = "5";
        //...
    }
}
