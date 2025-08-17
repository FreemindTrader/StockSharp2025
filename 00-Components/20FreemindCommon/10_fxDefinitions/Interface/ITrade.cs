using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface ITrade
    {
        string MainLoginName { get; }
        string    AccountID              { get; }
        string    AccountKind            { get; }
        string    AccountName            { get; }
        int       Amount                 { get; }
        string    BuySell                { get; }

        bool IsBuy{ get; }

        double    Commission             { get; }

        string    OfferID                { get; }

        string    OpenOrderID            { get; }

        string    OpenOrderParties       { get; }

        string    OpenOrderReqID         { get; }

        string    OpenOrderRequestTXT    { get; } 

        string    OpenQuoteID            { get; }

        double    OpenRate               { get; }
        DateTime  OpenTime               { get; }

        double    RolloverInterest       { get; }

        string    TradeID                { get; }

        string    TradeIDOrigin          { get; }
    }
}
