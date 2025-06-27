namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using fx.Definitions;    

    public partial class DbLockedPositions : IFxcm
    {
        public DbLockedPositions( )
        {

        }

        public DbLockedPositions( DbSmartWaveCycles baseObject )
        {
            Id = baseObject.Id;


        }

        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id { get; set; }
        public long StartDate { get; set; }        
        public string TradeId { get; set; }
        




        public DbLockedPositions( string tradeId )
        {
            TradeId = tradeId;           
        }
    }
}


