
using System;


namespace fx.Definitions
{
    /// <summary>
    /// One tick of trading dataSource.
    /// </summary>
    [ Serializable ]
    public struct DataTick
    {

        public DataTick( DateTime tickTime, double ask, double bid )
        {
            _tickLinuxTime = tickTime.ToLinuxTime( );            
            Ask           = ask;
            Bid           = bid;
            Volume        = 0;
        }
        public enum DataValueEnum
        {
            Ask,
            Bid,
            Volume
        }

        private long _tickLinuxTime;

        /// <summary>
        ///
        /// </summary>
        public DateTime TickTime
        {
            get { return _tickLinuxTime.FromLinuxTime(); }
            set { _tickLinuxTime = value.ToLinuxTime( ); }
        }

        /// <summary>
        ///
        /// </summary>
        public double Ask { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Bid { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Provide enum based access to values.
        /// </summary>
        public double GetValue( DataValueEnum value )
        {
            switch( value )
            {
                case DataValueEnum.Ask:
                    return Ask;
                case DataValueEnum.Bid:
                    return Bid;
                case DataValueEnum.Volume:
                    return Volume;
            }            

            return 0;
        }
    }
}