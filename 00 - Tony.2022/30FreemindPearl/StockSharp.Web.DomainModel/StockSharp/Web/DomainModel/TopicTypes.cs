
using System;

namespace StockSharp.Web.DomainModel
{
    public enum TopicTypes
    {
        Article,
        Page,
        News,
        [Obsolete] MajorNews,
        [Obsolete] Event,
        Email,
        [Obsolete] Doc,
        PrivateMessage,
        [Obsolete] Support,
        Forum,
        PublicDescription,
        PrivateDescription,
        Product,
        [Obsolete] Broker,
        [Obsolete] ProductOrder,
        [Obsolete] BrokerAccount,
        [Obsolete] Lesson,
    }
}
