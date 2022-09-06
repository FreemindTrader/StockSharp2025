
using System;

namespace StockSharp.Web.DomainModel
{
    [Flags]
    public enum TopicFlags
    {
        None = 0,
        IsLocked = 1,
        Pinned = 2,
        Restricted = 4,
        [Obsolete] Paid = 8,
        [Obsolete] Crowd = 16, // 0x00000010
        [Obsolete] IsLoggedIn = 32, // 0x00000020
        [Obsolete] IsPersistent = 512, // 0x00000200
        [Obsolete] IsQuestion = 1024, // 0x00000400
    }
}
