namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using fx.Definitions;

    public partial class DbForexNews : IFxcm, INewsEvent
    {
        private long _newsTime;

        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id { get; set; }

        public long StartDate { get; set; }

        public DateTime NewsTime
        {
            get { return _newsTime.FromLinuxTime( ); }

            set 
            {
                _newsTime = value.ToLinuxTime( );
                TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                NewsTimeUTC = TimeZoneInfo.ConvertTimeToUtc(value, est);
                
                StartDate = value.ToLinuxTime();
            }
        }

        DateTime _newsLocalTime = DateTime.MinValue;

        public DateTime NewsTimeUTC
        {
            get
            {
                return _newsLocalTime;
            }
            set
            {
                _newsLocalTime = value;
            }
        }

        public string Currency { get; set; }
        public string Description { get; set; }
        public string Impact { get; set; }

        public string Actual { get; set; }
        public string ForeCast { get; set; }
        public string Previous { get; set; }
        public string Revised { get; set; }
        public int FfNewsId { get; set; }
    }
}


