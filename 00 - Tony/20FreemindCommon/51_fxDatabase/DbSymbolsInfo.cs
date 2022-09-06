namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class DbSymbolsInfo : IFxcm
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id { get; set; }     

        public long StartDate { get; set; }

        public DbSymbolsInfo( )
        {

        }

        public DbSymbolsInfo( DbSymbolsInfo baseObject )
        {
            Id                 = baseObject.Id;
            OfferID            = baseObject.OfferID;
            Instrument         = baseObject.Instrument;
            BuyInterest        = baseObject.BuyInterest;
            SellInterest       = baseObject.SellInterest;
            Digits             = baseObject.Digits;
            PointSize          = baseObject.PointSize;
            SubscriptionStatus = baseObject.SubscriptionStatus;
            TradingStatus      = baseObject.TradingStatus;
            InstrumentType     = baseObject.InstrumentType;            
        }

        public string OfferID            { get; set; }

        public string Instrument         { get; set; }

        public double BuyInterest        { get; set; }


        public double SellInterest       { get; set; }

        public int Digits                { get; set; }

        public double PointSize          { get; set; }

        public string SubscriptionStatus { get; set; }

        public string TradingStatus      { get; set; }

        public int InstrumentType        { get; set; }

        public double PipCost            { get; set; }


        public double SafetyNetPips      { get; set; }



        public DbSymbolsInfo( string offerId, string instrument, double buyInterest, double sellInterest, int digits, double pointSize, string subscriptionStatus, string tradingStatus, int instrumentType, double safetyPips )            
        {
            Id                 = 0;
            OfferID            = offerId;
            Instrument         = instrument;
            BuyInterest        = buyInterest;
            SellInterest       = sellInterest;
            Digits             = digits;
            PointSize          = pointSize;
            SubscriptionStatus = subscriptionStatus;
            TradingStatus      = tradingStatus;
            InstrumentType     = instrumentType;
            SafetyNetPips      = safetyPips;
            
        }        
    }
}
