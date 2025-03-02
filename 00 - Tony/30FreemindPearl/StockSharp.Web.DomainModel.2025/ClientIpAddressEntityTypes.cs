// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientIpAddressEntityTypes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using System;

namespace StockSharp.Web.DomainModel
{
    public enum ClientIpAddressEntityTypes : byte
    {
        Client,
        [Obsolete("Use File instead.")] License,
        Payment,
        ProductOrder,
        ProductGroup,
        ShortUrlVisit,
        Message,
        PollVote,
        File,
        FileDownload,
        MessageVote,
        TopicVisit,
        ProfileVisit,
        ProductFeedback,
        Session,
        FileGroup,
        Product,
        EmailTemplate,
        TopicGroup,
        FileShare,
        ClientBalanceHistory,
        Favorite,
        AccountRequisites,
        MessageHistory,
        ShortUrl,
        MessagePatch,
        FileShareVisit,
        FileBody,
        StrategyAccount,
        Strategy,
        StrategyType,
        ConnectionType,
        Connection,
        ClientSocial,
        AccessToken,
        LicenseFeature,
        App,
    }
}
