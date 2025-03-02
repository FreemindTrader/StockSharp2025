// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Ids
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

namespace StockSharp.Web.DomainModel
{
    public static class Ids
    {
        public static class Client
        {
            public const long StockSharp = 1;
            public const long NonExist = 97667;
            public const long ClientManagers = 6528;
            public const long ClientEditors = 141995;
            public const long Admins = 6527;
            public const long SettingsManagers = 163685;
            public const long SettingsEditors = 142445;
            public const long NugetPublisherAdmin = 122584;
            public const long NugetPublisherAdminClient = 135204;
            public const long CanPublish = 134199;
            public const long EmailManagers = 132886;
            public const long EmailEditors = 142005;
            public const long ContentManagers = 133362;
            public const long AutoApproveProducts = 134485;
            public const long SupportApiClients = 97906;
            public const long Trial = 140676;
            public const long ErrorManagers = 141998;
            public const long ErrorEditors = 141999;
            public const long LicenseManagers = 142001;
            public const long LicenseEditors = 142002;
            public const long FileManagers = 142003;
            public const long FileEditors = 142004;
            public const long ProductManagers = 141996;
            public const long ProductEditors = 141997;
            public const long ProductOrderEditors = 142000;
            public const long ShortUrlManagers = 142006;
            public const long ShortUrlEditors = 142007;
            public const long PaymentManagers = 142008;
            public const long PaymentEditors = 142009;
            public const long Anonymous = 145183;
            public const long LoggedIn = 145184;
            public const long DisableFreelance = 145436;
            public const long Awards = 145496;
            public const long InstrumentManagers = 164286;
            public const long InstrumentEditors = 162513;
            public const long DiagramManagers = 164299;
            public const long DiagramEditors = 164300;
            public const long StrategyManagers = 165502;
            public const long StrategyEditors = 165503;
            public const long Tags = 94049;
            public const long AwardsBroker = 145503;
            public const long TrustLow = 174209;
            public const long TrustHigh = 174208;
            public const long VideoServer = 175970;
            public const long MailServer = 175971;
            public const long TelegramServer = 175972;
            public const long TariffFree = 176021;
            public const long PublisherServer = 176169;
            public const long Tariffs = 176651;
            public const long TariffDev = 176041;
            public const long TariffCorporate = 176038;
            public const long TariffPremium = 176036;
            public const long TariffApi = 176034;
            public const long TariffDesigner = 176027;
            public const long BagetServer = 177082;
            public const long TelegramRobots = 176136;
            public const long TelegramAlerts = 177977;
            public const long ConnectionVerifier = 178747;
            public const long StrategyServer = 178834;
        }

        public static class DataType
        {
            public const long Min1 = 1;
            public const long Min5 = 2;
            public const long Min10 = 3;
            public const long Min30 = 4;
            public const long H1 = 5;
            public const long D1 = 6;
            public const long Level1 = 7;
            public const long Ticks = 8;
            public const long OrderBook = 9;
            public const long OrderLog = 10;
        }

        public static class TempSalt
        {
            public const long AwayUrls = 1;
            public const long Payments = 2;
            public const long Subscriptions = 14608;
            public const long Activations = 14609;
        }

        public static class Product
        {
            public const long WealthLab = 3;
            public const long Api = 5;
            public const long Studio = 7;
            public const long Hydra = 8;
            public const long Designer = 9;
            public const long Terminal = 10;
            public const long Shell = 11;
            public const long MatLab = 12;
            public const long Lci = 13;
            public const long Server = 14;
            public const long Installer = 16;
            public const long HydraServer = 363;
            public const long CGate = 75;
            public const long Fix = 76;
            public const long Itch = 77;
            public const long QuikLua = 19;
            public const long MT4 = 20;
            public const long MT5 = 21;
            public const long QuantFeed = 78;
            public const long SpbEx = 79;
            public const long MicexTeap = 80;
            public const long Twime = 81;
            public const long HydraSources = 89;
            public const long DesignerSources = 90;
            public const long TerminalSources = 91;
            public const long BitStamp = 32;
            public const long ApiSources = 158;
            public const long CryptoBinaryAll = 177;
            public const long CryptoSourceDevelopNew = 180;
            public const long FixBinaryAll = 181;
            public const long ItchBinaryAll = 182;
            public const long AllBinary = 183;
            public const long AllBinaryAll = 184;
            public const long Trial = 327;
            public const long Old = 328;
            public const long ApiVideo = 271;
            public const long DesignerBasic = 294;
            public const long DesignerVideo = 295;
            public const long SupportForum = 297;
            public const long Balance = 326;
            public const long LicenseTool = 194;
            public const long TerminalMobile = 199;
            public const long Publisher = 193;
            public const long InteractiveBrokers = 1038;
            public const long IQFeed = 1082;
            public const long Runner = 1137;
            public const long TelegramAlerts = 1138;
            public const long TariffFree = 1150;
            public const long TariffDesignerPro = 1151;
            public const long TariffApiPro = 1152;
            public const long TariffPremium = 1153;
            public const long TariffCorporate = 1154;
            public const long TariffDev = 1155;
            public const long Offline = 1170;
            public const long TelegramRobots = 1173;
            public const long TariffConnector = 1181;
            public const long BacktestNoQueue = 1182;
            public const long BacktestOwnQueue = 1185;
            public const long BacktestAllowCode = 1189;
            public const long BacktestOptimizationBruteForce = 1190;
            public const long BacktestOptimizationGenetic = 1192;
            public const long Backtest100PerDay = 1193;
            public const long Backtest1000Iterations = 1194;
            public const long BacktestNoDelay = 1195;
            public const long BacktestOptimization100Generations = 1196;
            public const long BacktestAllowCustomRefs = 1201;
            public const long BacktestChartInfinite = 1205;
            public const long BacktestMaxPeriod = 1255;
            public const long BacktestTicks = 1256;
            public const long BacktestLevel1 = 1257;
            public const long BacktestCloud = 1258;
            public const long BacktestMaxPerDay = 1259;
            public const long Backtest2Parallel = 1260;
            public const long Backtest10Parallel = 1261;
            public const long BacktestOptimizer2Parallel = 1262;
            public const long BacktestOptimizer10Parallel = 1263;
            public const long Backtest20MinutesDuration = 1264;
            public const long BacktestOptimizer60MinutesDuration = 1265;
            public const long Backtest5MinutesDuration = 1266;
            public const long BacktestOptimizer20MinutesDuration = 1267;
            public const long BacktestOptimizer5MinutesDuration = 1268;
            public const long TariffWhiteLabel = 1288;
        }

        public static class ProductGroup
        {
            public const long ProductsRoot = 2;
            public const long BrokersRoot = 1;
            public const long Strategies = 4;
            public const long Programms = 5;
            public const long Indicators = 6;
            public const long ConnectorsCrypto = 8;
            public const long Free = 10;
            public const long Connectors = 21;
            public const long Sources = 22;
            public const long EduRoot = 23;
            public const long TrialAllow = 24;
            public const long PaidMessages = 25;
            public const long Crowds = 26;
            public const long FreelanceRoot = 27;
            public const long RefundAllow = 28;
            public const long TrialManual = 29;
            public const long FreelanceContents = 30;
            public const long Develop = 31;
            public const long ConnectorsStock = 33;
            public const long ConnectorsForex = 36;
            public const long Pricing = 37;
            public const long OneApp = 39;
            public const long HFT = 41;
            public const long Tariffs = 45;
            public const long MarketData = 46;
            public const long TariffFreeConnectors = 49;
            public const long FreeConnectorApps = 50;
            public const long Cubes = 53;
            public const long LogoMainPage = 55;
            public const long CounterClients = 56;
            public const long AppsMainPage = 57;
        }

        public static class PayGateway
        {
            public const long MDM = 1;
            public const long IK = 2;
            public const long PT = 3;
            public const long PP = 4;
            public const long BankWire = 5;
            public const long Crypto = 6;
            public const long Balance = 7;
            public const long None = 8;
            public const long Sber = 9;
            public const long UP = 10;
        }

        public static class LicenseFeature
        {
            public const long None = 94;
        }

        public static class File
        {
            public const long NoAvatarMan = 101448;
            public const long NoAvatarWoman = 101449;
            public const long Logo = 101465;
            public const long LogoLarge = 102332;
            public const long NotFound = 103644;
            public const long Forbidden = 104253;
            public const long InstallerConsole = 115526;
            public const long ProductDefaultPicture = 116399;
            public const long Diagram = 116401;
            public const long HydraIcon = 116403;
            public const long NotFoundIcon = 120943;
            public const long CSharp = 121100;
            public const long LogoFlat = 134749;
            public const long Setup = 142263;
            public const long IndicatorIcon = 143320;
            public const long DllIcon = 143345;
            public const long RunnerIcon = 143701;
            public const long LogsDeletedBody = 150458;
            public const long FSharp = 158300;
            public const long Python = 158301;
            public const long VisualBasic = 158302;
        }

        public static class FileGroup
        {
            public const long Nuget = 85;
            public const long Connectors = 93;
            public const long Sources = 94;
            public const long PublicNuget = 88;
            public const long PrivateNuget = 306;
            public const long Special = 365;
            public const long EmailHtmlTemplate = 366;
            public const long EmailTextTemplate = 367;
            public const long Avatars = 368;
            public const long WatermarkVideo = 369;
            public const long Anonymous = 370;
            public const long LoggedIn = 371;
            public const long CommunityPublic = 436;
            public const long CommunityPrivate = 435;
            public const long Payments = 440;
            public const long Mjml = 493;
            public const long MjmlRender = 494;
            public const long PrivateFilters = 506;
            public const long WatermarkImage = 564;
        }

        public static class EmailTemplate
        {
            public const long RegisterActivation = 14;
            public const long RegisterWelcome = 33;
            public const long ForgotPassword = 17;
            public const long PrivateMessage = 34;
            public const long License = 35;
            public const long NewComment = 36;
            public const long Starred = 37;
            public const long SuspiciousMessage = 45;
            public const long AttachChanged = 46;
            public const long MessagePatch = 47;
            public const long AddToProduct = 49;
            public const long RemoveFromProduct = 50;
            public const long MoneyDeclared = 51;
            public const long MoneyReserved = 52;
            public const long ExecSelected = 53;
            public const long ExecStarted = 54;
            public const long ExecFinished = 55;
            public const long RefundRequested = 56;
            public const long ExecutorPay = 57;
            public const long CancelledPay = 58;
            public const long CrowdDeclared = 59;
            public const long ProdPaid = 60;
            public const long ProdPaidToS = 61;
            public const long LessonOrderToS = 62;
            public const long LessonPaidVideoAPIToU = 63;
            public const long LessonPaidFullAPIToU = 64;
            public const long LessonPaidToM = 65;
            public const long LessonPaidDesBasicToU = 66;
            public const long LessonPaidDesFullToU = 67;
            public const long ProductOrderToM = 68;
            public const long CryptoConnectorPaidToU = 69;
            public const long Days7AutoToU = 70;
            public const long Days1AutoToU = 71;
            public const long Days7ManToU = 72;
            public const long Days1ManToU = 73;
            public const long ExpToU = 74;
            public const long ExpToM = 75;
            public const long TrialReqToU = 76;
            public const long TrialReqToM = 77;
            public const long CryptoTrialApprovedToU = 78;
            public const long TrialApprovedToM = 79;
            public const long TrialRejToU = 80;
            public const long PayIterDoneToU = 81;
            public const long PayIterDoneToM = 82;
            public const long PayIterErrToU = 83;
            public const long PayIterErrToM = 84;
            public const long SubscrStopToU = 85;
            public const long SubscrStopToM = 86;
            public const long StockConnectorPaidToU = 87;
            public const long StockTrialApprovedToU = 88;
            public const long PaidQuesToS = 89;
            public const long SourcePaidToU = 90;
            public const long ExecSelectedToU = 91;
            public const long ProductStartedToM = 92;
            public const long ProductFinishedToM = 93;
            public const long FLExecutorPayToS = 94;
            public const long FLRefundReqToU = 95;
            public const long ProductPaidToM = 96;
            public const long CrowdToS = 97;
            public const long CrowdProductStartedToU = 98;
            public const long CrowdProductStartedToS = 99;
            public const long CrowdProductFinishedToU = 100;
            public const long CrowdProductFinishedToM = 101;
            public const long CrowdProductFinishedToS = 102;
        }

        public static class DynamicPage
        {
            public const long RootPage = 1;
            public const long AboutStockSharp = 171;
            public const long BrokerPartnership = 142;
            public const long BrokerFaq = 213;
            public const long Store = 164;
            public const long Products = 155;
            public const long Pricing = 157;
            public const long Error = 204;
            public const long Error403 = 212;
            public const long Error404 = 149;
            public const long Error500 = 150;
            public const long ErrorExpired = 146;
            public const long Community = 173;
            public const long PaymentOk = 180;
            public const long PaymentError = 181;
            public const long SmsActivation = 207;
            public const long AntiBot = 148;
            public const long Download = 144;
            public const long SupportWarning = 160;
            public const long Support = 209;
            public const long Crypto = 215;
            public const long Unsubscribe = 216;
            public const long Away = 221;
            public const long Pay = 229;
            public const long PaymentPending = 231;
            public const long PayStatus = 230;
            public const long PayGate = 232;
            public const long PaymentCancelled = 233;
            public const long Referral = 234;
            public const long Balance = 235;
            public const long SelfDelete = 236;
            public const long Broker = 203;
            public const long Edu = 228;
            public const long Freelance = 227;
            public const long FreelanceFaq = 238;
            public const long StoreFaq = 239;
            public const long FreelanceCreate = 240;
            public const long NuGetManual = 241;
            public const long Develop = 242;
            public const long Profile = 243;
            public const long News = 244;
            public const long Articles = 245;
            public const long Users = 246;
            public const long Forum = 247;
            public const long Search = 249;
            public const long Sitemap = 250;
            public const long Login = 251;
            public const long Register = 252;
            public const long Forgot = 253;
            public const long PrivateMessages = 254;
            public const long DevelopCreate = 259;
            public const long BBCodes = 260;
            public const long Eula = 274;
            public const long Topic = 275;
            public const long File = 276;
            public const long Posts = 277;
            public const long Messages = 278;
            public const long BugStatistics = 279;
            public const long Edit = 280;
            public const long Video = 281;
            public const long ShortUrl = 282;
            public const long Logoff = 287;
            public const long Blog = 291;
            public const long Welcome = 294;
        }

        public static class DynamicMenuGroup
        {
            public const long Main = 1;
            public const long Community = 2;
            public const long Products = 3;
            public const long Footer = 4;
            public const long Start = 5;
        }

        public static class SiteSettings
        {
            public const long Default = 1;
        }

        public static class Domain
        {
            public const long Ru = 1;
            public const long Com = 2;
        }

        public static class TopicGroup
        {
            public const long Pinned = 5;
        }

        public static class Social
        {
            public const long Telegram = 4;
            public const long YandexDisk = 9;
            public const long Tradier = 10;
            public const long Claude = 12;
            public const long GPT = 13;
            public const long cTrader = 14;
            public const long Alor = 16;
        }
    }
}
