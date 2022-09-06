namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class DbSafetyNetPips : IFxcm
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id { get; set; }

        public long StartDate { get; set; }

        public string OfferID { get; set; }

        public double SafetyPips { get; set; }

        public string MainLoginName { get; set; }

        public DbSafetyNetPips( )
        {

        }

        public DbSafetyNetPips( DbSafetyNetPips baseObject )
        {
            Id            = baseObject.Id;
            OfferID       = baseObject.OfferID;
            SafetyPips    = baseObject.SafetyPips;
            MainLoginName = baseObject.MainLoginName;            
        }

    } 
}
