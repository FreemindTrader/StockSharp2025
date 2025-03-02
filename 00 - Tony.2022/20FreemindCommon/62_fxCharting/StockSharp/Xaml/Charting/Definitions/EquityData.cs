using System;
using System.Runtime.Serialization;

namespace fx.Charting
{
    [DataContract]
    [Serializable]
    public class EquityData : LineData< DateTime >
    {
        [DataMember]
        public DateTimeOffset Time
        {
            get
            {
                return X.ToUniversalTime( );
            }
            set
            {
                X = value.LocalDateTime;
            }
        }

        [DataMember]
        public Decimal Value
        {
            get
            {
                return Y;
            }
            set
            {
                Y = value;
            }
        }
    }
}
