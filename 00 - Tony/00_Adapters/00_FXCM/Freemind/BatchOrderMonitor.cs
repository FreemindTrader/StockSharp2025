using fxcore2;
using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public class BatchOrderMonitor
    {
        private List< string >                _requestIDs;
        private List< OrderMonitor > _orderMonitors;

        /// <summary>
        /// ctor
        /// </summary>
        public BatchOrderMonitor( )
        {
            _requestIDs = new List<string>( );
            _orderMonitors = new List<OrderMonitor>( );
        }

        public void SetRequestIDs( IList<string> requestIDs )
        {
            _requestIDs.Clear( );

            foreach ( string sRequestID in requestIDs )
            {
                _requestIDs.Add( sRequestID );
            }

        }

        public void OnRequestCompleted( string sRequestID, O2GResponse response )
        {
            //STUB
        }

        public void OnRequestFailed( string sRequestID )
        {
            if ( IsOwnRequest( sRequestID ) )
                RemoveRequestID( sRequestID );
        }

        public void OnTradeAdded( O2GTradeRow tradeRow )
        {
            for ( int i = 0; i < _orderMonitors.Count; i++ )
            {
                _orderMonitors[ i ].OnTradeAdded( tradeRow );
            }
        }

        public void OnOrderAdded( O2GOrderRow order )
        {
            string sRequestID = order.RequestID;

            if ( IsOwnRequest( sRequestID ) )
            {
                if ( OrderMonitor.IsClosingOrder( order ) || OrderMonitor.IsOpeningOrder( order ) )
                {
                    AddToMonitoring( order );
                }
            }
        }

        public void OnOrderDeleted( O2GOrderRow order )
        {
            for ( int i = 0; i < _orderMonitors.Count; i++ )
            {
                _orderMonitors[ i ].OnOrderDeleted( order );
            }

        }

        public void OnMessageAdded( O2GMessageRow message )
        {
            for ( int i = 0; i < _orderMonitors.Count; i++ )
            {
                _orderMonitors[ i ].OnMessageAdded( message );
            }

        }

        public void OnClosedTradeAdded( O2GClosedTradeRow closedTrade )
        {
            for ( int i = 0; i < _orderMonitors.Count; i++ )
            {
                _orderMonitors[ i ].OnClosedTradeAdded( closedTrade );
            }
        }

        public bool IsBatchExecuted( )
        {
            bool bAllCompleted = true;

            for ( int i = 0; i < _orderMonitors.Count; i++ )
            {
                if ( !_orderMonitors[ i ].IsOrderCompleted )
                {
                    bAllCompleted = false;
                    break;
                }
            }

            bool result = _requestIDs.Count == 0 && bAllCompleted;
            return result;
        }

        public List<OrderMonitor> GetMonitors( )
        {
            List< OrderMonitor > result = new List< OrderMonitor >( );

            foreach ( OrderMonitor monitor in _orderMonitors )
            {
                result.Add( monitor );
            }
            return result;
        }

        public event EventHandler BatchOrderCompleted;


        private bool IsOwnRequest( string sRequestID )
        {
            return _requestIDs.Contains( sRequestID );
        }

        private void AddToMonitoring( O2GOrderRow order )
        {
            OrderMonitor monitor   = new OrderMonitor( order );
            monitor.OrderCompleted += new EventHandler( monitor_OrderCompleted );

            _orderMonitors.Add( monitor );
        }

        private void RemoveRequestID( string sRequestID )
        {
            if ( _requestIDs.Contains( sRequestID ) )
            {
                _requestIDs.Remove( sRequestID );
            }
        }

        #region order monitor event handlers

        private void monitor_OrderCompleted( object sender, EventArgs e )
        {
            OrderMonitor monitor = ( OrderMonitor ) sender;
            RemoveRequestID( monitor.Order.RequestID );

            if ( BatchOrderCompleted != null && IsBatchExecuted( ) )
            {
                BatchOrderCompleted( this, EventArgs.Empty );
            }
        }

        #endregion
    }
}