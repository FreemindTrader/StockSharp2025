
using System;
using System.Runtime.Serialization;

namespace StockSharp.Web.DomainModel
{
    [DataContract]
    [Serializable]
    public enum PackageRepositories
    {
        [EnumMember] NuGet,
        [EnumMember] StockSharp,
    }
}
