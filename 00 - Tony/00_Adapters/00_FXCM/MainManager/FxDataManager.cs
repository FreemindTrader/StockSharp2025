using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.MainManager
{
    public class FxDataManager
    {
        private FxConnectFxcmMsgAdapter    _msgAdapter;
        public FxDataManager( FxConnectFxcmMsgAdapter adapter )
        {
            _msgAdapter = adapter;            
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
