namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum PartyRole
    {
        /// <summary>
        /// </summary>
        ExecutingFirm = 1,
        /// <summary>
        /// </summary>
        BrokerOfCredit = 2,
        /// <summary>
        /// </summary>
        ClientId = 3,
        /// <summary>
        /// </summary>
        ClearingFirm = 4,
        /// <summary>
        /// </summary>
        InvestorId = 5,
        /// <summary>
        /// </summary>
        IntroducingFirm = 6,
        /// <summary>
        /// </summary>
        EnteringFirm = 7,
        /// <summary>
        /// </summary>
        Locate = 8,
        /// <summary>
        /// </summary>
        LocateLendingFirm = 8,
        /// <summary>
        /// </summary>
        FundManagerClientId = 9,
        /// <summary>
        /// </summary>
        SettlementLocation = 10, // 0x0000000A
        /// <summary>
        /// </summary>
        OrderOriginationTrader = 11, // 0x0000000B
        /// <summary>
        /// </summary>
        ExecutingTrader = 12, // 0x0000000C
        /// <summary>
        /// </summary>
        OrderOriginationFirm = 13, // 0x0000000D
        /// <summary>
        /// </summary>
        GiveupClearingFirm = 14, // 0x0000000E
        /// <summary>
        /// </summary>
        CorrespondantClearingFirm = 15, // 0x0000000F
        /// <summary>
        /// </summary>
        ExecutingSystem = 16, // 0x00000010
        /// <summary>
        /// </summary>
        ContraFirm = 17, // 0x00000011
        /// <summary>
        /// </summary>
        ContraClearingFirm = 18, // 0x00000012
        /// <summary>
        /// </summary>
        SponsoringFirm = 19, // 0x00000013
        /// <summary>
        /// </summary>
        UnderlyingContraFirm = 20, // 0x00000014
        /// <summary>
        /// </summary>
        ClearingOrganization = 21, // 0x00000015
        /// <summary>
        /// </summary>
        Exchange = 22, // 0x00000016
        /// <summary>
        /// </summary>
        CustomerAccount = 24, // 0x00000018
        /// <summary>
        /// </summary>
        CorrespondentClearingOrganization = 25, // 0x00000019
        /// <summary>
        /// </summary>
        CorrespondentBroker = 26, // 0x0000001A
        /// <summary>
        /// </summary>
        BuyerSeller = 27, // 0x0000001B
        /// <summary>
        /// </summary>
        Custodian = 28, // 0x0000001C
        /// <summary>
        /// </summary>
        Intermediary = 29, // 0x0000001D
        /// <summary>
        /// </summary>
        Agent = 30, // 0x0000001E
        /// <summary>
        /// </summary>
        SubCustodian = 31, // 0x0000001F
        /// <summary>
        /// </summary>
        Beneficiary = 32, // 0x00000020
        /// <summary>
        /// </summary>
        InterestedParty = 33, // 0x00000021
        /// <summary>
        /// </summary>
        RegulatoryBody = 34, // 0x00000022
        /// <summary>
        /// </summary>
        LiquidityProvider = 35, // 0x00000023
        /// <summary>
        /// </summary>
        EnteringTrader = 36, // 0x00000024
        /// <summary>
        /// </summary>
        ContraTrader = 37, // 0x00000025
        /// <summary>
        /// </summary>
        PositionAccount = 38, // 0x00000026
        /// <summary>
        /// </summary>
        ContraInvestorId = 39, // 0x00000027
        /// <summary>
        /// </summary>
        TransferToFirm = 40, // 0x00000028
        /// <summary>
        /// </summary>
        ContraPositionAccount = 41, // 0x00000029
        /// <summary>
        /// </summary>
        ContraExchange = 42, // 0x0000002A
        /// <summary>
        /// </summary>
        InternalCarryAccount = 43, // 0x0000002B
        /// <summary>
        /// </summary>
        OrderEntryOperatorId = 44, // 0x0000002C
        /// <summary>
        /// </summary>
        SecondaryAccountNumber = 45, // 0x0000002D
        /// <summary>
        /// </summary>
        ForeignFirm = 46, // 0x0000002E
        /// <summary>
        /// </summary>
        ForiegnFirm = 46, // 0x0000002E
        /// <summary>
        /// </summary>
        ThirdPartyAllocationFirm = 47, // 0x0000002F
        /// <summary>
        /// </summary>
        ClaimingAccount = 48, // 0x00000030
        /// <summary>
        /// </summary>
        AssetManager = 49, // 0x00000031
        /// <summary>
        /// </summary>
        PledgorAccount = 50, // 0x00000032
        /// <summary>
        /// </summary>
        PledgeeAccount = 51, // 0x00000033
        /// <summary>
        /// </summary>
        LargeTraderReportableAccount = 52, // 0x00000034
        /// <summary>
        /// </summary>
        TraderMnemonic = 53, // 0x00000035
        /// <summary>
        /// </summary>
        SenderLocation = 54, // 0x00000036
        /// <summary>
        /// </summary>
        SessionId = 55, // 0x00000037
        /// <summary>
        /// </summary>
        AcceptableCounterparty = 56, // 0x00000038
        /// <summary>
        /// </summary>
        UnacceptableCounterparty = 57, // 0x00000039
        /// <summary>
        /// </summary>
        EnteringUnit = 58, // 0x0000003A
        /// <summary>
        /// </summary>
        ExecutingUnit = 59, // 0x0000003B
        /// <summary>
        /// </summary>
        IntroducingBroker = 60, // 0x0000003C
        /// <summary>
        /// </summary>
        QuoteOriginator = 61, // 0x0000003D
        /// <summary>
        /// </summary>
        ReportOriginator = 62, // 0x0000003E
        /// <summary>
        /// </summary>
        SystematicInternaliser = 63, // 0x0000003F
        /// <summary>
        /// </summary>
        MultilateralTradingFacility = 64, // 0x00000040
        /// <summary>
        /// </summary>
        RegulatedMarket = 65, // 0x00000041
        /// <summary>
        /// </summary>
        MarketMaker = 66, // 0x00000042
        /// <summary>
        /// </summary>
        InvestmentFirm = 67, // 0x00000043
        /// <summary>
        /// </summary>
        HostCompetentAuthority = 68, // 0x00000044
        /// <summary>
        /// </summary>
        HomeCompetentAuthority = 69, // 0x00000045
        /// <summary>
        /// </summary>
        CompetentAuthorityOfTheMostRelevantMarketInTermsOfLiquidity = 70, // 0x00000046
        /// <summary>
        /// </summary>
        CompetentAuthorityOfTheTransaction = 71, // 0x00000047
        /// <summary>
        /// </summary>
        ReportingIntermediary = 72, // 0x00000048
        /// <summary>
        /// </summary>
        ExecutionVenue = 73, // 0x00000049
        /// <summary>
        /// </summary>
        MarketDataEntryOriginator = 74, // 0x0000004A
        /// <summary>
        /// </summary>
        LocationId = 75, // 0x0000004B
        /// <summary>
        /// </summary>
        DeskId = 76, // 0x0000004C
        /// <summary>
        /// </summary>
        MarketDataMarket = 77, // 0x0000004D
        /// <summary>
        /// </summary>
        AllocationEntity = 78, // 0x0000004E
        /// <summary>
        /// </summary>
        PrimeBrokerProvidingGeneralTradeServices = 79, // 0x0000004F
        /// <summary>
        /// </summary>
        StepOutFirm = 80, // 0x00000050
        /// <summary>
        /// </summary>
        Brokerclearingid = 81, // 0x00000051
        /// <summary>
        /// </summary>
        CentralRegistrationDepository = 82, // 0x00000052
        /// <summary>
        /// </summary>
        ClearingAccount = 83, // 0x00000053
        /// <summary>
        /// </summary>
        AcceptableSettlingCounterparty = 84, // 0x00000054
        /// <summary>
        /// </summary>
        UnacceptableSettlingCounterparty = 85, // 0x00000055
    }
}
