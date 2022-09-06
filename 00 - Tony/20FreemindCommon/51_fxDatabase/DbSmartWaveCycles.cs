namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using fx.Definitions; 

    public partial class DbSmartWaveCycles : ISmartWaveCycles, IFxcm
    {
        public DbSmartWaveCycles( )
        {

        }

        public DbSmartWaveCycles( DbSmartWaveCycles baseObject )
        {
            Id = baseObject.Id;


        }

        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id                         { get; set; }
        public long StartDate                  { get; set; }
        public string Period                   { get; set; }
        public int OfferID                     { get; set; }
        public int WaveImportance              { get; set; }

        public ElliottWaveCycle WaveCycle1     { get; set; }

        public ElliottWaveCycle? WaveCycle2    { get; set; }

        public ElliottWaveCycle? WaveCycle3    { get; set; }

        public ElliottWaveCycle? WaveCycle4    { get; set; }





        public DbSmartWaveCycles( int offerId , string period , int waveImportance, ElliottWaveCycle waveCycle )
        {
            OfferID        = offerId;

            Period         = period;

            WaveImportance = waveImportance;

            WaveCycle1     = waveCycle;
           
        }
    }
}
