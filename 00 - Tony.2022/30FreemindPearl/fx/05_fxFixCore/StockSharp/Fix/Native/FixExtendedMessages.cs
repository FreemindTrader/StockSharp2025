namespace StockSharp.Fix.Native
{
    /// <summary>FIX extended messages codes.</summary>
    public static class FixExtendedMessages
    {
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SubscriptionResponseMessage" />.
        /// </summary>
        public const string SubscriptionResponse = "MO";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SubscriptionFinishedMessage" />.
        /// </summary>
        public const string SubscriptionFinished = "MF";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SubscriptionOnlineMessage" />.
        /// </summary>
        public const string SubscriptionOnline = "SO";
        /// <summary>The request for instruments uploading.</summary>
        public const string SecurityListUpload = "xu";
        /// <summary>The request for instruments deleting.</summary>
        public const string SecurityListDelete = "xd";
        /// <summary>The response of board lookup.</summary>
        public const string Board = "bb";
        /// <summary>The request for board update.</summary>
        public const string BoardUpdate = "bu";
        /// <summary>The request for start historical data.</summary>
        public const string HistoryStart = "hs";
        /// <summary>The request for stop historical data.</summary>
        public const string HistoryEnd = "he";
        /// <summary>The request for change historical interval.</summary>
        public const string HistoryInterval = "hi";
        /// <summary>The request for board lookup.</summary>
        public const string BoardLookup = "bl";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.UserRequestMessage" />.
        /// </summary>
        public const string UserRequest = "ur";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.UserInfoMessage" />.
        /// </summary>
        public const string UserInfo = "ui";
        /// <summary>The request for available time-frames.</summary>
        public const string TimeFramesRequest = "TR";
        /// <summary>
        /// The response for <see cref="F:StockSharp.Fix.Native.FixExtendedMessages.TimeFramesRequest" />.
        /// </summary>
        public const string TimeFramesInfo = "TS";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyInfoMessage" />.
        /// </summary>
        public const string StrategyInfo = "SI";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyTypeMessage" />.
        /// </summary>
        public const string StrategyType = "ST";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyStateMessage" />.
        /// </summary>
        public const string StrategyState = "SS";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SecurityMappingRequestMessage" />.
        /// </summary>
        public const string SecurityMappingRequest = "MQ";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SecurityMappingInfoMessage" />.
        /// </summary>
        public const string SecurityMappingInfo = "MR";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SecurityLegsRequestMessage" />.
        /// </summary>
        public const string SecurityLegsRequest = "LQ";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SecurityLegsInfoMessage" />.
        /// </summary>
        public const string SecurityLegsInfo = "LR";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.AdapterListRequestMessage" />.
        /// </summary>
        public const string AdapterListRequest = "DR";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.CommandMessage" />.
        /// </summary>
        public const string Command = "DC";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.AdapterResponseMessage" />.
        /// </summary>
        public const string AdapterResponse = "DO";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SubscriptionListRequestMessage" />.
        /// </summary>
        public const string SubscriptionListRequest = "CR";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.MarketDataMessage" />.
        /// </summary>
        public const string Subscription = "CC";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SecurityMappingMessage" />.
        /// </summary>
        public const string SecurityMapping = "SM";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SecurityRouteListRequestMessage" />.
        /// </summary>
        public const string SecurityRouteListRequest = "RR";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.PortfolioRouteListRequestMessage" />.
        /// </summary>
        public const string PortfolioRouteListRequest = "PR";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.SecurityRouteMessage" />.
        /// </summary>
        public const string SecurityRoute = "sr";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.PortfolioRouteMessage" />.
        /// </summary>
        public const string PortfolioRoute = "pr";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Messages.RemoveMessage" />.
        /// </summary>
        public const string Remove = "bd";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.LicenseRequestMessage" />.
        /// </summary>
        public const string LicenseRequest = "LS";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.LicenseInfoMessage" />.
        /// </summary>
        public const string LicenseInfo = "LI";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.LicenseFeatureMessage" />.
        /// </summary>
        public const string LicenseFeature = "LF";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Algo.Storages.Remote.Messages.RemoteFileCommandMessage" />.
        /// </summary>
        public const string RemoteFileCommand = "RC";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Algo.Storages.Remote.Messages.RemoteFileMessage" />.
        /// </summary>
        public const string RemoteFile = "RF";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Algo.Storages.Remote.Messages.AvailableDataRequestMessage" />.
        /// </summary>
        public const string AvailableDataRequest = "ADR";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Algo.Storages.Remote.Messages.AvailableDataInfoMessage" />.
        /// </summary>
        public const string AvailableDataInfo = "ADI";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.FileInfoMessage" />.
        /// </summary>
        public const string FileInfo = "FI";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.ProductInfoMessage" />.
        /// </summary>
        public const string ProductInfo = "PI";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.ProductFeedbackMessage" />.
        /// </summary>
        public const string ProductFeedback = "PF";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.ProductPermissionMessage" />.
        /// </summary>
        public const string ProductPermission = "PPR";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.ProductPublishMessage" />.
        /// </summary>
        public const string ProductPublish = "PUB";
        /// <summary>
        /// Message type for <see cref="T:StockSharp.Community.ProductCategoryMessage" />.
        /// </summary>
        public const string ProductCategory = "PC";
    }
}
