
using System.Runtime.Serialization;

namespace StockSharp.Web.DomainModel
{
    [DataContract]
    public enum ProductScopes
    {
        [EnumMember] Public,
        [EnumMember] Restricted,
        [EnumMember] Private,
    }
}
