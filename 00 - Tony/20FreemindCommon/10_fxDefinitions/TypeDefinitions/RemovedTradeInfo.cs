using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    [Serializable]
    public class RemovedTradeInfo
    {
        public string AccountID { get; set; }
        public string AccountName { get; set; }

        public string Instrument { get; set; }

        public string TradeId { get; set; }

        public IDetailedPosition ClosedPosition { get; set; }

        public RemovedTradeInfo( )
        {
        }

        public RemovedTradeInfo( string accountId,                                    
                                    string accountName,
                                    string instrument,
                                    string tradeId, 
                                    IDetailedPosition closedPos )
        {
            AccountID      = accountId;
            AccountName    = accountName;
            Instrument     = instrument;
            TradeId        = tradeId;
            ClosedPosition = closedPos;


        }
    }
}
