using System;
using System.Runtime.Serialization;

namespace fx.Charting
{
    [DataContract]
    [Serializable]
    public class LineData< TKey >
    {
        [DataMember]
        public TKey X { get; set; }

        [DataMember]
        public Decimal Y { get; set; }
    }
}
