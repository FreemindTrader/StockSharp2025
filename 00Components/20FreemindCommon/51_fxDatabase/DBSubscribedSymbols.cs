namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    

    public partial class DBSubscribedSymbols : IFxcm
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id { get; set; }

        public long StartDate { get; set; }

        public string AccountName { get; set; }

        public int OfferID { get; set; }

        public int BaseUnitSize { get; set; }

        public double EMR { get; set; }
        public double MMR { get; set; }

        public double LMR { get; set; }

        public DBSubscribedSymbols( )
        {

        }

        public DBSubscribedSymbols( DBSubscribedSymbols baseObject )
        {
            Id        = baseObject.Id;
            OfferID   = baseObject.OfferID;
            AccountName = baseObject.AccountName;
            BaseUnitSize = baseObject.BaseUnitSize;
        }

        

        
        public DBSubscribedSymbols( string offerId, string accountName )
        {
            Id        = 0;
            OfferID   = int.Parse( offerId );
            AccountName = accountName;
            BaseUnitSize = 0;
            EMR = 0;
            MMR = 0;
            LMR = 0;


        }
    }
}
