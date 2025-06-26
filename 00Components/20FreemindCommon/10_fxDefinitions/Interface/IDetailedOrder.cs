using System;
using System.ComponentModel;

namespace fx.Definitions
{
    public interface IDetailedOrder : INotifyPropertyChanged, IFxcm
    {
        
        string   AccountID              { get; set; }
        string   AccountKind            { get; set; }
        string   MainLoginName          { get; set; }
        string   AccountName            { get; set; }
        int      Amount                 { get; set; }        
        double   AtMarket               { get; set; }
        string   BuySell                { get; set; }
        int      ContingencyType        { get; set; }
        string   ContingentOrderID      { get; set; }
        double   ExecutionRate          { get; set; }
        long     ExpireDate             { get; set; }
        DateTime ExpireDateDT           { get; set; }
        int      FilledAmount           { get; set; }
        bool     NetQuantity            { get; set; }
        string   OfferID                { get; set; }
        string   OrderID                { get; set; }
        int      OriginAmount           { get; set; }
        string   Parties                { get; set; }
        double   PegOffset              { get; set; }
        string   PegType                { get; set; }
        string   PrimaryID              { get; set; }
        double   Rate                   { get; set; }
        double   RateMax                { get; set; }
        double   RateMin                { get; set; }
        string   RequestID              { get; set; }
        string   RequestTXT             { get; set; }
        string   Stage                  { get; set; }
        string   Status                 { get; set; }
        DateTime StatusTimeDT           { get; set; }
        long     StatusTime             { get; set; }
        string   TimeInForce            { get; set; }
        string   TradeID                { get; set; }
        double   TrailRate              { get; set; }
        int      TrailStep              { get; set; }
        string   Type                   { get; set; }
        string   ValueDate              { get; set; }
        bool     WorkingIndicator       { get; set; }

        void CopyFrom( IDetailedOrder other );

        bool isMarketOrder( );

        IDetailedOrder Clone( );
    }
}