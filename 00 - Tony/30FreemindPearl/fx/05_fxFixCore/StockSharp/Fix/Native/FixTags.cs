namespace StockSharp.Fix.Native
{
    /// <summary>FIX protocol tags.</summary>
    public enum FixTags
    {
        /// <summary>
        /// </summary>
        Account = 1,
        /// <summary>
        /// </summary>
        AvgPx = 6,
        /// <summary>
        /// </summary>
        BeginSeqNo = 7,
        /// <summary>
        /// </summary>
        BeginString = 8,
        /// <summary>
        /// </summary>
        BodyLength = 9,
        /// <summary>
        /// </summary>
        CheckSum = 10, // 0x0000000A
        /// <summary>
        /// </summary>
        ClOrdID = 11, // 0x0000000B
        /// <summary>
        /// </summary>
        Commission = 12, // 0x0000000C
        /// <summary>
        /// </summary>
        CommType = 13, // 0x0000000D
        /// <summary>
        /// </summary>
        CumQty = 14, // 0x0000000E
        /// <summary>
        /// </summary>
        Currency = 15, // 0x0000000F
        /// <summary>
        /// </summary>
        EndSeqNo = 16, // 0x00000010
        /// <summary>
        /// </summary>
        ExecID = 17, // 0x00000011
        /// <summary>
        /// </summary>
        ExecInst = 18, // 0x00000012
        /// <summary>
        /// </summary>
        HandlInst = 21, // 0x00000015
        /// <summary>
        /// </summary>
        IDSource = 22, // 0x00000016
        /// <summary>
        /// </summary>
        LastCapacity = 29, // 0x0000001D
        /// <summary>
        /// </summary>
        LastPx = 31, // 0x0000001F
        /// <summary>
        /// </summary>
        LastQty = 32, // 0x00000020
        /// <summary>
        /// </summary>
        MsgSeqNum = 34, // 0x00000022
        /// <summary>
        /// </summary>
        MsgType = 35, // 0x00000023
        /// <summary>
        /// </summary>
        NewSeqNo = 36, // 0x00000024
        /// <summary>
        /// </summary>
        OrderID = 37, // 0x00000025
        /// <summary>
        /// </summary>
        OrderQty = 38, // 0x00000026
        /// <summary>
        /// </summary>
        OrdStatus = 39, // 0x00000027
        /// <summary>
        /// </summary>
        OrdType = 40, // 0x00000028
        /// <summary>
        /// </summary>
        OrigClOrdID = 41, // 0x00000029
        /// <summary>
        /// </summary>
        OrigTime = 42, // 0x0000002A
        /// <summary>
        /// Indicates possible retransmission of message with this sequence number.
        /// </summary>
        PossDupFlag = 43, // 0x0000002B
        /// <summary>
        /// </summary>
        Price = 44, // 0x0000002C
        /// <summary>
        /// </summary>
        RefSeqNum = 45, // 0x0000002D
        /// <summary>
        /// </summary>
        SecurityID = 48, // 0x00000030
        /// <summary>
        /// </summary>
        SenderCompID = 49, // 0x00000031
        /// <summary>
        /// </summary>
        SenderSubID = 50, // 0x00000032
        /// <summary>
        /// </summary>
        SendingTime = 52, // 0x00000034
        /// <summary>
        /// </summary>
        Side = 54, // 0x00000036
        /// <summary>
        /// </summary>
        Symbol = 55, // 0x00000037
        /// <summary>
        /// </summary>
        TargetCompID = 56, // 0x00000038
        /// <summary>
        /// </summary>
        Text = 58, // 0x0000003A
        /// <summary>
        /// </summary>
        TimeInForce = 59, // 0x0000003B
        /// <summary>
        /// </summary>
        TransactTime = 60, // 0x0000003C
        /// <summary>
        /// </summary>
        Urgency = 61, // 0x0000003D
        /// <summary>
        /// </summary>
        SettlDate = 64, // 0x00000040
        /// <summary>
        /// </summary>
        SymbolSfx = 65, // 0x00000041
        /// <summary>
        /// </summary>
        TradeDate = 75, // 0x0000004B
        /// <summary>
        /// </summary>
        ExecBroker = 76, // 0x0000004C
        /// <summary>
        /// </summary>
        PositionEffect = 77, // 0x0000004D
        /// <summary>
        /// </summary>
        RptSeq = 83, // 0x00000053
        /// <summary>
        /// </summary>
        RawDataLength = 95, // 0x0000005F
        /// <summary>
        /// </summary>
        RawData = 96, // 0x00000060
        /// <summary>
        /// Indicates that message may contain information that has been sent under another sequence number.
        /// </summary>
        PossResend = 97, // 0x00000061
        /// <summary>
        /// </summary>
        EncryptMethod = 98, // 0x00000062
        /// <summary>
        /// </summary>
        StopPx = 99, // 0x00000063
        /// <summary>
        /// </summary>
        ExDestination = 100, // 0x00000064
        /// <summary>
        /// </summary>
        CxlRejReason = 102, // 0x00000066
        /// <summary>
        /// </summary>
        SecurityDesc = 107, // 0x0000006B
        /// <summary>
        /// </summary>
        HeartBtInt = 108, // 0x0000006C
        /// <summary>
        /// </summary>
        ClientID = 109, // 0x0000006D
        /// <summary>
        /// </summary>
        MinQty = 110, // 0x0000006E
        /// <summary>
        /// </summary>
        MaxFloor = 111, // 0x0000006F
        /// <summary>
        /// </summary>
        TestReqID = 112, // 0x00000070
        /// <summary>
        /// </summary>
        QuoteID = 117, // 0x00000075
        /// <summary>
        /// </summary>
        SettlCurrency = 120, // 0x00000078
        /// <summary>
        /// Original time of message transmission (always expressed in UTC) when transmitting orders as the result of a resend request.
        /// </summary>
        OrigSendingTime = 122, // 0x0000007A
        /// <summary>
        /// </summary>
        GapFillFlag = 123, // 0x0000007B
        /// <summary>
        /// </summary>
        ExpireTime = 126, // 0x0000007E
        /// <summary>
        /// </summary>
        QuoteReqID = 131, // 0x00000083
        /// <summary>
        /// </summary>
        BidPx = 132, // 0x00000084
        /// <summary>
        /// </summary>
        OfferPx = 133, // 0x00000085
        /// <summary>
        /// </summary>
        BidSize = 134, // 0x00000086
        /// <summary>
        /// </summary>
        OfferSize = 135, // 0x00000087
        /// <summary>
        /// </summary>
        PrevClosePx = 140, // 0x0000008C
        /// <summary>
        /// </summary>
        ResetSeqNumFlag = 141, // 0x0000008D
        /// <summary>
        /// </summary>
        NoRelatedSym = 146, // 0x00000092
        /// <summary>
        /// </summary>
        Headline = 148, // 0x00000094
        /// <summary>
        /// </summary>
        URLLink = 149, // 0x00000095
        /// <summary>
        /// </summary>
        ExecType = 150, // 0x00000096
        /// <summary>
        /// </summary>
        LeavesQty = 151, // 0x00000097
        /// <summary>
        /// </summary>
        SecurityType = 167, // 0x000000A7
        /// <summary>
        /// </summary>
        EffectiveTime = 168, // 0x000000A8
        /// <summary>
        /// </summary>
        SecondaryOrderID = 198, // 0x000000C6
        /// <summary>
        /// </summary>
        MaturityMonthYear = 200, // 0x000000C8
        /// <summary>
        /// </summary>
        PutOrCall = 201, // 0x000000C9
        /// <summary>
        /// </summary>
        StrikePrice = 202, // 0x000000CA
        /// <summary>
        /// </summary>
        MaturityDay = 205, // 0x000000CD
        /// <summary>
        /// </summary>
        SecurityExchange = 207, // 0x000000CF
        /// <summary>
        /// </summary>
        PegOffsetValue = 211, // 0x000000D3
        /// <summary>
        /// </summary>
        XmlDataLen = 212, // 0x000000D4
        /// <summary>
        /// </summary>
        XmlData = 213, // 0x000000D5
        /// <summary>
        /// </summary>
        IssueDate = 225, // 0x000000E1
        /// <summary>
        /// </summary>
        Factor = 228, // 0x000000E4
        /// <summary>
        /// </summary>
        ContractMultiplier = 231, // 0x000000E7
        /// <summary>
        /// </summary>
        Yield = 236, // 0x000000EC
        /// <summary>
        /// </summary>
        RedemptionDate = 240, // 0x000000F0
        /// <summary>
        /// </summary>
        LegIssueDate = 249, // 0x000000F9
        /// <summary>
        /// </summary>
        MDReqID = 262, // 0x00000106
        /// <summary>
        /// </summary>
        SubscriptionRequestType = 263, // 0x00000107
        /// <summary>
        /// </summary>
        MarketDepth = 264, // 0x00000108
        /// <summary>
        /// </summary>
        MDUpdateType = 265, // 0x00000109
        /// <summary>
        /// </summary>
        NoMDEntryTypes = 267, // 0x0000010B
        /// <summary>
        /// </summary>
        NoMDEntries = 268, // 0x0000010C
        /// <summary>
        /// </summary>
        MDEntryType = 269, // 0x0000010D
        /// <summary>
        /// </summary>
        MDEntryPx = 270, // 0x0000010E
        /// <summary>
        /// </summary>
        MDEntrySize = 271, // 0x0000010F
        /// <summary>
        /// </summary>
        MDEntryDate = 272, // 0x00000110
        /// <summary>
        /// </summary>
        MDEntryTime = 273, // 0x00000111
        /// <summary>
        /// </summary>
        TickDirection = 274, // 0x00000112
        /// <summary>
        /// </summary>
        QuoteCondition = 276, // 0x00000114
        /// <summary>
        /// </summary>
        MDEntryId = 278, // 0x00000116
        /// <summary>
        /// </summary>
        MDUpdateAction = 279, // 0x00000117
        /// <summary>
        /// </summary>
        MDReqRejReason = 281, // 0x00000119
        /// <summary>
        /// </summary>
        MDEntryOriginator = 282, // 0x0000011A
        /// <summary>
        /// </summary>
        OpenCloseSettleFlag = 286, // 0x0000011E
        /// <summary>
        /// </summary>
        MDEntryBuyer = 288, // 0x00000120
        /// <summary>
        /// </summary>
        MDEntrySeller = 289, // 0x00000121
        /// <summary>
        /// </summary>
        MDEntryPositionNo = 290, // 0x00000122
        /// <summary>
        /// </summary>
        NoQuoteEntries = 295, // 0x00000127
        /// <summary>
        /// </summary>
        NoQuoteSets = 296, // 0x00000128
        /// <summary>
        /// </summary>
        QuoteCancelType = 298, // 0x0000012A
        /// <summary>
        /// </summary>
        QuoteEntryID = 299, // 0x0000012B
        /// <summary>
        /// </summary>
        QuoteSetID = 302, // 0x0000012E
        /// <summary>
        /// </summary>
        TotNoQuoteEntries = 304, // 0x00000130
        /// <summary>
        /// </summary>
        UnderlyingIDSource = 305, // 0x00000131
        /// <summary>
        /// </summary>
        UnderlyingSecurityDesc = 307, // 0x00000133
        /// <summary>
        /// </summary>
        UnderlyingSecurityExchange = 308, // 0x00000134
        /// <summary>
        /// </summary>
        UnderlyingSecurityID = 309, // 0x00000135
        /// <summary>
        /// </summary>
        UnderlyingSecurityType = 310, // 0x00000136
        /// <summary>
        /// </summary>
        UnderlyingSymbol = 311, // 0x00000137
        /// <summary>
        /// </summary>
        UnderlyingSymbolSfx = 312, // 0x00000138
        /// <summary>
        /// </summary>
        UnderlyingMaturityMonthYear = 313, // 0x00000139
        /// <summary>
        /// </summary>
        UnderlyingPutOrCall = 315, // 0x0000013B
        /// <summary>
        /// </summary>
        UnderlyingStrikePrice = 316, // 0x0000013C
        /// <summary>
        /// </summary>
        UnderlyingCurrency = 318, // 0x0000013E
        /// <summary>
        /// </summary>
        RatioQty = 319, // 0x0000013F
        /// <summary>
        /// </summary>
        SecurityReqID = 320, // 0x00000140
        /// <summary>
        /// </summary>
        SecurityRequestType = 321, // 0x00000141
        /// <summary>
        /// </summary>
        SecurityResponseID = 322, // 0x00000142
        /// <summary>
        /// </summary>
        SecurityResponseType = 323, // 0x00000143
        /// <summary>
        /// </summary>
        SecurityStatusReqID = 324, // 0x00000144
        /// <summary>
        /// </summary>
        UnsolicitedIndicator = 325, // 0x00000145
        /// <summary>
        /// </summary>
        SecurityTradingStatus = 326, // 0x00000146
        /// <summary>
        /// </summary>
        HighPx = 332, // 0x0000014C
        /// <summary>
        /// </summary>
        LowPx = 333, // 0x0000014D
        /// <summary>
        /// </summary>
        TradSesReqID = 335, // 0x0000014F
        /// <summary>
        /// </summary>
        TradingSessionID = 336, // 0x00000150
        /// <summary>
        /// </summary>
        TradSesStatus = 340, // 0x00000154
        /// <summary>
        /// </summary>
        NumberOfOrders = 346, // 0x0000015A
        /// <summary>
        /// </summary>
        RefTagID = 371, // 0x00000173
        /// <summary>
        /// </summary>
        RefMsgType = 372, // 0x00000174
        /// <summary>
        /// </summary>
        SessionRejectReason = 373, // 0x00000175
        /// <summary>
        /// </summary>
        ExecRestatementReason = 378, // 0x0000017A
        /// <summary>
        /// </summary>
        BusinessRejectReason = 380, // 0x0000017C
        /// <summary>
        /// </summary>
        GrossTradeAmt = 381, // 0x0000017D
        /// <summary>
        /// </summary>
        NoTradingSessions = 386, // 0x00000182
        /// <summary>
        /// </summary>
        TotalVolumeTraded = 387, // 0x00000183
        /// <summary>
        /// </summary>
        TotNoRelatedSym = 393, // 0x00000189
        /// <summary>
        /// </summary>
        PriceType = 423, // 0x000001A7
        /// <summary>
        /// </summary>
        ExpireDate = 432, // 0x000001B0
        /// <summary>
        /// </summary>
        CxlRejResponseTo = 434, // 0x000001B2
        /// <summary>
        /// </summary>
        PartyIDSource = 447, // 0x000001BF
        /// <summary>
        /// </summary>
        PartyID = 448, // 0x000001C0
        /// <summary>
        /// </summary>
        PartyRole = 452, // 0x000001C4
        /// <summary>
        /// </summary>
        NoPartyIDs = 453, // 0x000001C5
        /// <summary>
        /// </summary>
        NoSecurityAltID = 454, // 0x000001C6
        /// <summary>
        /// </summary>
        SecurityAltID = 455, // 0x000001C7
        /// <summary>
        /// </summary>
        SecurityAltIDSource = 456, // 0x000001C8
        /// <summary>
        /// </summary>
        Product = 460, // 0x000001CC
        /// <summary>
        /// </summary>
        CFICode = 461, // 0x000001CD
        /// <summary>
        /// </summary>
        CommCurrency = 479, // 0x000001DF
        /// <summary>
        /// </summary>
        NestedPartyID = 524, // 0x0000020C
        /// <summary>
        /// </summary>
        SecondaryClOrdID = 526, // 0x0000020E
        /// <summary>
        /// </summary>
        OrderCapacity = 528, // 0x00000210
        /// <summary>
        /// </summary>
        OrderRestrictions = 529, // 0x00000211
        /// <summary>
        /// </summary>
        MassCancelRequestType = 530, // 0x00000212
        /// <summary>
        /// </summary>
        MassCancelResponse = 531, // 0x00000213
        /// <summary>
        /// </summary>
        MassCancelRejectReason = 532, // 0x00000214
        /// <summary>
        /// </summary>
        QuoteType = 537, // 0x00000219
        /// <summary>
        /// </summary>
        NestedPartyRole = 538, // 0x0000021A
        /// <summary>
        /// </summary>
        NoNestedPartyIDs = 539, // 0x0000021B
        /// <summary>
        /// </summary>
        CashMargin = 544, // 0x00000220
        /// <summary>
        /// </summary>
        Scope = 546, // 0x00000222
        /// <summary>
        /// </summary>
        Username = 553, // 0x00000229
        /// <summary>
        /// </summary>
        Password = 554, // 0x0000022A
        /// <summary>
        /// </summary>
        NoLegs = 555, // 0x0000022B
        /// <summary>
        /// </summary>
        LegCurrency = 556, // 0x0000022C
        /// <summary>
        /// </summary>
        NoSecurityTypes = 558, // 0x0000022E
        /// <summary>
        /// </summary>
        SecurityListRequestType = 559, // 0x0000022F
        /// <summary>
        /// </summary>
        SecurityRequestResult = 560, // 0x00000230
        /// <summary>
        /// </summary>
        RoundLot = 561, // 0x00000231
        /// <summary>
        /// </summary>
        MinTradeVol = 562, // 0x00000232
        /// <summary>
        /// </summary>
        TradSesStatusRejReason = 567, // 0x00000237
        /// <summary>
        /// </summary>
        AccountType = 581, // 0x00000245
        /// <summary>
        /// </summary>
        MassStatusReqID = 584, // 0x00000248
        /// <summary>
        /// </summary>
        MassStatusReqType = 585, // 0x00000249
        /// <summary>
        /// </summary>
        LegSymbol = 600, // 0x00000258
        /// <summary>
        /// </summary>
        LegSymbolSfx = 601, // 0x00000259
        /// <summary>
        /// </summary>
        LegSecurityID = 602, // 0x0000025A
        /// <summary>
        /// </summary>
        LegSecurityIDSource = 603, // 0x0000025B
        /// <summary>
        /// </summary>
        NoLegSecurityAltID = 604, // 0x0000025C
        /// <summary>
        /// </summary>
        LegSecurityAltID = 605, // 0x0000025D
        /// <summary>
        /// </summary>
        LegSecurityAltIDSource = 606, // 0x0000025E
        /// <summary>
        /// </summary>
        LegProduct = 607, // 0x0000025F
        /// <summary>
        /// </summary>
        LegCFICode = 608, // 0x00000260
        /// <summary>
        /// </summary>
        LegSecurityType = 609, // 0x00000261
        /// <summary>
        /// </summary>
        LegMaturityMonthYear = 610, // 0x00000262
        /// <summary>
        /// </summary>
        LegMaturityDate = 611, // 0x00000263
        /// <summary>
        /// </summary>
        LegStrikePrice = 612, // 0x00000264
        /// <summary>
        /// </summary>
        LegOptAttribute = 613, // 0x00000265
        /// <summary>
        /// </summary>
        LegContractMultiplier = 614, // 0x00000266
        /// <summary>
        /// </summary>
        LegCouponRate = 615, // 0x00000267
        /// <summary>
        /// </summary>
        LegSecurityExchange = 616, // 0x00000268
        /// <summary>
        /// </summary>
        LegIssuer = 617, // 0x00000269
        /// <summary>
        /// </summary>
        EncodedLegIssuerLen = 618, // 0x0000026A
        /// <summary>
        /// </summary>
        EncodedLegIssuer = 619, // 0x0000026B
        /// <summary>
        /// </summary>
        LegSecurityDesc = 620, // 0x0000026C
        /// <summary>
        /// </summary>
        EncodedLegSecurityDescLen = 621, // 0x0000026D
        /// <summary>
        /// </summary>
        EncodedLegSecurityDesc = 622, // 0x0000026E
        /// <summary>
        /// </summary>
        LegRatioQty = 623, // 0x0000026F
        /// <summary>
        /// </summary>
        LegSide = 624, // 0x00000270
        /// <summary>
        /// </summary>
        TradingSessionSubID = 625, // 0x00000271
        /// <summary>
        /// </summary>
        Price2 = 640, // 0x00000280
        /// <summary>
        /// </summary>
        QuoteStatusReqID = 649, // 0x00000289
        /// <summary>
        /// </summary>
        QuoteRequestRejectReason = 658, // 0x00000292
        /// <summary>
        /// </summary>
        AcctIDSource = 660, // 0x00000294
        /// <summary>
        /// </summary>
        ContractSettlMonth = 667, // 0x0000029B
        /// <summary>
        /// </summary>
        NoPositions = 702, // 0x000002BE
        /// <summary>
        /// </summary>
        PosType = 703, // 0x000002BF
        /// <summary>
        /// </summary>
        LongQty = 704, // 0x000002C0
        /// <summary>
        /// </summary>
        ShortQty = 705, // 0x000002C1
        /// <summary>
        /// </summary>
        PosQtyStatus = 706, // 0x000002C2
        /// <summary>
        /// </summary>
        PosAmtType = 707, // 0x000002C3
        /// <summary>
        /// </summary>
        PosAmt = 708, // 0x000002C4
        /// <summary>
        /// </summary>
        PosReqID = 710, // 0x000002C6
        /// <summary>
        /// </summary>
        NoUnderlyings = 711, // 0x000002C7
        /// <summary>
        /// </summary>
        ClearingBusinessDate = 715, // 0x000002CB
        /// <summary>
        /// </summary>
        PosMaintRptID = 721, // 0x000002D1
        /// <summary>
        /// </summary>
        PosReqType = 724, // 0x000002D4
        /// <summary>
        /// </summary>
        PosReqResult = 728, // 0x000002D8
        /// <summary>
        /// </summary>
        PosReqStatus = 729, // 0x000002D9
        /// <summary>
        /// </summary>
        NoPosAmt = 753, // 0x000002F1
        /// <summary>
        /// </summary>
        SecuritySubType = 762, // 0x000002FA
        /// <summary>
        /// </summary>
        NoTrdRegTimestamps = 768, // 0x00000300
        /// <summary>
        /// </summary>
        TrdRegTimestamp = 769, // 0x00000301
        /// <summary>
        /// </summary>
        TrdRegTimestampType = 770, // 0x00000302
        /// <summary>
        /// </summary>
        NextExpectedMsgSeqNum = 789, // 0x00000315
        /// <summary>
        /// </summary>
        OrdStatusReqID = 790, // 0x00000316
        /// <summary>
        /// </summary>
        ApplQueueDepth = 813, // 0x0000032D
        /// <summary>
        /// </summary>
        ApplQueueResolution = 814, // 0x0000032E
        /// <summary>
        /// </summary>
        TrdType = 828, // 0x0000033C
        /// <summary>
        /// </summary>
        PegOffsetType = 836, // 0x00000344
        /// <summary>
        /// </summary>
        LastLiquidityInd = 851, // 0x00000353
        /// <summary>
        /// </summary>
        PublishTrdIndicator = 852, // 0x00000354
        /// <summary>
        /// </summary>
        NoInstrAttrib = 870, // 0x00000366
        /// <summary>
        /// </summary>
        InstrAttribType = 871, // 0x00000367
        /// <summary>
        /// </summary>
        InstrAttribValue = 872, // 0x00000368
        /// <summary>
        /// </summary>
        LastFragment = 893, // 0x0000037D
        /// <summary>
        /// </summary>
        TotNumReports = 911, // 0x0000038F
        /// <summary>
        /// </summary>
        AgreementID = 914, // 0x00000392
        /// <summary>
        /// </summary>
        StartDate = 916, // 0x00000394
        /// <summary>
        /// </summary>
        EndDate = 917, // 0x00000395
        /// <summary>
        /// </summary>
        UserRequestID = 923, // 0x0000039B
        /// <summary>
        /// </summary>
        UserRequestType = 924, // 0x0000039C
        /// <summary>
        /// </summary>
        NewPassword = 925, // 0x0000039D
        /// <summary>
        /// </summary>
        UserStatus = 926, // 0x0000039E
        /// <summary>
        /// </summary>
        UserStatusText = 927, // 0x0000039F
        /// <summary>
        /// </summary>
        NoStrategyParameters = 957, // 0x000003BD
        /// <summary>
        /// </summary>
        StrategyParameterName = 958, // 0x000003BE
        /// <summary>
        /// </summary>
        StrategyParameterType = 959, // 0x000003BF
        /// <summary>
        /// </summary>
        StrategyParameterValue = 960, // 0x000003C0
        /// <summary>
        /// </summary>
        SecurityStatus = 965, // 0x000003C5
        /// <summary>
        /// </summary>
        MinPriceIncrement = 969, // 0x000003C9
        /// <summary>
        /// </summary>
        TradeVolume = 1020, // 0x000003FC
        /// <summary>
        /// </summary>
        MDEntryForwardPoints = 1027, // 0x00000403
        /// <summary>
        /// </summary>
        ManualOrderIndicator = 1028, // 0x00000404
        /// <summary>
        /// </summary>
        AggressorIndicator = 1057, // 0x00000421
        /// <summary>
        /// </summary>
        MatchIncrement = 1089, // 0x00000441
        /// <summary>
        /// </summary>
        OrderCategory = 1115, // 0x0000045B
        /// <summary>
        /// </summary>
        ApplVerId = 1128, // 0x00000468
        /// <summary>
        /// </summary>
        CstmApplVerID = 1129, // 0x00000469
        /// <summary>
        /// </summary>
        DefaultApplVerID = 1137, // 0x00000471
        /// <summary>
        /// </summary>
        DisplayQty = 1138, // 0x00000472
        /// <summary>
        /// </summary>
        MaxTradeVol = 1140, // 0x00000474
        /// <summary>
        /// </summary>
        SecurityGroup = 1151, // 0x0000047F
        /// <summary>
        /// </summary>
        ApplId = 1180, // 0x0000049C
        /// <summary>
        /// </summary>
        ApplSeqNum = 1181, // 0x0000049D
        /// <summary>
        /// </summary>
        ApplBegSeqNum = 1182, // 0x0000049E
        /// <summary>
        /// </summary>
        ApplEndSeqNum = 1183, // 0x0000049F
        /// <summary>
        /// </summary>
        ApplReqID = 1346, // 0x00000542
        /// <summary>
        /// </summary>
        ApplReqType = 1347, // 0x00000543
        /// <summary>
        /// </summary>
        ApplRespType = 1348, // 0x00000544
        /// <summary>
        /// </summary>
        ApplLastSeqNum = 1350, // 0x00000546
        /// <summary>
        /// </summary>
        NoApplIDs = 1351, // 0x00000547
        /// <summary>
        /// </summary>
        ApplResendFlag = 1352, // 0x00000548
        /// <summary>
        /// </summary>
        ApplRespID = 1353, // 0x00000549
        /// <summary>
        /// </summary>
        ApplRespError = 1354, // 0x0000054A
        /// <summary>
        /// </summary>
        RefApplID = 1355, // 0x0000054B
        /// <summary>
        /// </summary>
        ApplReportID = 1356, // 0x0000054C
        /// <summary>
        /// </summary>
        RefApplLastSeqNum = 1357, // 0x0000054D
        /// <summary>
        /// </summary>
        ApplReportType = 1426, // 0x00000592
        /// <summary>
        /// </summary>
        MDStreamID = 1500, // 0x000005DC
        /// <summary>
        /// </summary>
        MDEntryArg = 10000, // 0x00002710
        /// <summary>
        /// </summary>
        MDOtherValue = 15000, // 0x00003A98
        /// <summary>
        /// </summary>
        ExtendedOrderStatus = 17000, // 0x00004268
        /// <summary>
        /// </summary>
        ExtendedTradeStatus = 17001, // 0x00004269
        /// <summary>
        /// </summary>
        CandleState = 20002, // 0x00004E22
        /// <summary>
        /// </summary>
        IssueSize = 20003, // 0x00004E23
        /// <summary>
        /// </summary>
        Formula = 20004, // 0x00004E24
        /// <summary>
        /// </summary>
        LegRoundLot = 20005, // 0x00004E25
        /// <summary>
        /// </summary>
        LegMinTradeVol = 20006, // 0x00004E26
        /// <summary>
        /// </summary>
        LegNoInstrAttrib = 20007, // 0x00004E27
        /// <summary>
        /// </summary>
        LegInstrAttribType = 20008, // 0x00004E28
        /// <summary>
        /// </summary>
        LegInstrAttribValue = 20009, // 0x00004E29
        /// <summary>
        /// </summary>
        LegEndDate = 20010, // 0x00004E2A
        /// <summary>
        /// </summary>
        LegIssueSize = 20011, // 0x00004E2B
        /// <summary>
        /// </summary>
        SessionPeriods = 20012, // 0x00004E2C
        /// <summary>
        /// </summary>
        SessionSpecialDays = 20013, // 0x00004E2D
        /// <summary>
        /// </summary>
        TimeFrames = 20014, // 0x00004E2E
        /// <summary>
        /// </summary>
        Name = 20015, // 0x00004E2F
        /// <summary>
        /// </summary>
        StrategyTypeId = 20016, // 0x00004E30
        /// <summary>
        /// </summary>
        Id = 20017, // 0x00004E31
        /// <summary>
        /// </summary>
        GroupId = 20018, // 0x00004E32
        /// <summary>
        /// </summary>
        Command = 20022, // 0x00004E36
        /// <summary>
        /// </summary>
        Shortable = 20024, // 0x00004E38
        /// <summary>
        /// </summary>
        AllowBuildFromSmallerTimeFrame = 20026, // 0x00004E3A
        /// <summary>
        /// </summary>
        CalcVolumeProfile = 20027, // 0x00004E3B
        /// <summary>
        /// </summary>
        FinishedCandles = 20028, // 0x00004E3C
        /// <summary>
        /// </summary>
        MarketDataBuildMode = 20029, // 0x00004E3D
        /// <summary>
        /// </summary>
        MarketDataBuildFrom = 20030, // 0x00004E3E
        /// <summary>
        /// </summary>
        MarketDataBuildField = 20031, // 0x00004E3F
        /// <summary>
        /// </summary>
        RegularTradingHours = 20032, // 0x00004E40
        /// <summary>
        /// </summary>
        MarketDataCount = 20033, // 0x00004E41
        /// <summary>
        /// </summary>
        NoIpRestrictions = 20034, // 0x00004E42
        /// <summary>
        /// </summary>
        IpRestrictions = 20035, // 0x00004E43
        /// <summary>
        /// </summary>
        NoPermissions = 20036, // 0x00004E44
        /// <summary>
        /// </summary>
        Permissions = 20037, // 0x00004E45
        /// <summary>
        /// </summary>
        NoPermissionsValues = 20038, // 0x00004E46
        /// <summary>
        /// </summary>
        FaceValue = 20039, // 0x00004E47
        /// <summary>
        /// </summary>
        MDResponseID = 20040, // 0x00004E48
        /// <summary>
        /// </summary>
        Language = 20041, // 0x00004E49
        /// <summary>
        /// </summary>
        Topic = 20042, // 0x00004E4A
        /// <summary>
        /// </summary>
        ContentType = 20043, // 0x00004E4B
        /// <summary>
        /// </summary>
        Content = 20044, // 0x00004E4C
        /// <summary>
        /// </summary>
        Owner = 20045, // 0x00004E4D
        /// <summary>
        /// </summary>
        Picture = 20046, // 0x00004E4E
        /// <summary>
        /// </summary>
        Revision = 20047, // 0x00004E4F
        /// <summary>
        /// </summary>
        Private = 20048, // 0x00004E50
        /// <summary>
        /// </summary>
        Colocation = 20049, // 0x00004E51
        /// <summary>
        /// </summary>
        PromoPrice = 20050, // 0x00004E52
        /// <summary>
        /// </summary>
        PromoEnd = 20051, // 0x00004E53
        /// <summary>
        /// </summary>
        DisplayName = 20052, // 0x00004E54
        /// <summary>
        /// </summary>
        Phone = 20053, // 0x00004E55
        /// <summary>
        /// </summary>
        Homepage = 20054, // 0x00004E56
        /// <summary>
        /// </summary>
        Skype = 20055, // 0x00004E57
        /// <summary>
        /// </summary>
        City = 20056, // 0x00004E58
        /// <summary>
        /// </summary>
        Gender = 20057, // 0x00004E59
        /// <summary>
        /// </summary>
        Subscription = 20058, // 0x00004E5A
        /// <summary>
        /// </summary>
        Token = 20059, // 0x00004E5B
        /// <summary>
        /// </summary>
        PrimaryCode = 20060, // 0x00004E5C
        /// <summary>
        /// </summary>
        PrimaryBoard = 20061, // 0x00004E5D
        /// <summary>
        /// </summary>
        Format = 20062, // 0x00004E5E
        /// <summary>
        /// </summary>
        Hash = 20063, // 0x00004E5F
        /// <summary>
        /// </summary>
        DownloadCount = 20064, // 0x00004E60
        /// <summary>
        /// </summary>
        Rating = 20065, // 0x00004E61
        /// <summary>
        /// </summary>
        DocUrl = 20066, // 0x00004E62
        /// <summary>
        /// </summary>
        SupportedPlugins = 20067, // 0x00004E63
        /// <summary>
        /// </summary>
        Platform = 20068, // 0x00004E64
        /// <summary>
        /// </summary>
        HardwareId = 20069, // 0x00004E65
        /// <summary>
        /// </summary>
        OnlyId = 20070, // 0x00004E66
        /// <summary>
        /// </summary>
        UpTicks = 20071, // 0x00004E67
        /// <summary>
        /// </summary>
        DownTicks = 20072, // 0x00004E68
        /// <summary>
        /// </summary>
        TotalTicks = 20073, // 0x00004E69
        /// <summary>
        /// </summary>
        LevelPrice = 20074, // 0x00004E6A
        /// <summary>
        /// </summary>
        LevelType = 20075, // 0x00004E6B
        /// <summary>
        /// </summary>
        PostOnly = 20076, // 0x00004E6C
        /// <summary>
        /// </summary>
        Slippage = 20077, // 0x00004E6D
        /// <summary>
        /// </summary>
        MDEntryPositionExtra = 20078, // 0x00004E6E
        /// <summary>
        /// </summary>
        LevelPriceBuyCount = 20079, // 0x00004E6F
        /// <summary>
        /// </summary>
        LevelPriceSellCount = 20080, // 0x00004E70
        /// <summary>
        /// </summary>
        LevelPriceBuyVolume = 20081, // 0x00004E71
        /// <summary>
        /// </summary>
        LevelPriceSellVolume = 20082, // 0x00004E72
        /// <summary>
        /// </summary>
        LevelPriceTotalVolume = 20083, // 0x00004E73
        /// <summary>
        /// </summary>
        BuildFromType = 20084, // 0x00004E74
        /// <summary>
        /// </summary>
        BuildFromArg = 20085, // 0x00004E75
        /// <summary>
        /// </summary>
        OldPrice = 20086, // 0x00004E76
        /// <summary>
        /// </summary>
        OldVolume = 20087, // 0x00004E77
        /// <summary>
        /// </summary>
        Leverage = 20088, // 0x00004E78
        /// <summary>
        /// </summary>
        MarketDataSkip = 20089, // 0x00004E79
        /// <summary>
        /// </summary>
        Repository = 20090, // 0x00004E7A
        /// <summary>
        /// </summary>
        Currency2 = 20091, // 0x00004E7B
        /// <summary>
        /// </summary>
        MonthlyPrice = 20092, // 0x00004E7C
        /// <summary>
        /// </summary>
        MonthlyPriceCurrency = 20093, // 0x00004E7D
        /// <summary>
        /// </summary>
        LifetimePrice = 20094, // 0x00004E7E
        /// <summary>
        /// </summary>
        LifetimePriceCurrency = 20095, // 0x00004E7F
        /// <summary>
        /// </summary>
        NoStubCount = 20096, // 0x00004E80
        /// <summary>
        /// </summary>
        RealVersion = 20097, // 0x00004E81
        /// <summary>
        /// </summary>
        StubVersion = 20098, // 0x00004E82
        /// <summary>
        /// </summary>
        DiscountMonthlyPrice = 20099, // 0x00004E83
        /// <summary>
        /// </summary>
        DiscountMonthlyPriceCurrency = 20100, // 0x00004E84
        /// <summary>
        /// </summary>
        DiscountAnnualPrice = 20101, // 0x00004E85
        /// <summary>
        /// </summary>
        DiscountAnnualPriceCurrency = 20102, // 0x00004E86
        /// <summary>
        /// </summary>
        DiscountLifetimePrice = 20103, // 0x00004E87
        /// <summary>
        /// </summary>
        DiscountLifetimePriceCurrency = 20104, // 0x00004E88
        /// <summary>
        /// </summary>
        RenewMonthlyPrice = 20105, // 0x00004E89
        /// <summary>
        /// </summary>
        RenewMonthlyPriceCurrency = 20106, // 0x00004E8A
        /// <summary>
        /// </summary>
        TrialAllow = 20107, // 0x00004E8B
        /// <summary>
        /// </summary>
        TrialRequested = 20108, // 0x00004E8C
        /// <summary>
        /// </summary>
        Text2 = 20109, // 0x00004E8D
        /// <summary>
        /// </summary>
        ProductCategories = 20110, // 0x00004E8E
        /// <summary>
        /// </summary>
        RenewAnnualPrice = 20111, // 0x00004E8F
        /// <summary>
        /// </summary>
        RenewAnnualPriceCurrency = 20112, // 0x00004E90
    }
}
