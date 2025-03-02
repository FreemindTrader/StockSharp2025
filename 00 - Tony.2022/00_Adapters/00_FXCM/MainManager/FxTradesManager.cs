using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.MainManager
{    
    public class FxTradesManager
    {
        private FxConnectFxcmMsgAdapter    _adapter;

        public FxTradesManager( FxConnectFxcmMsgAdapter adapter )
        {
            _adapter = adapter;            
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Here we will reset everything after receive a MessageTypes.Reset message
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */
        public void Reset( )
        {
            
        }
    }
}
