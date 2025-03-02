
namespace StockSharp.Web.DomainModel
{
    public static class Ids
    {
        public static class Client
        {
            public const long StockSharp                = 1;
            public const long TestEmail                 = 39599;
            public const long NonExist                  = 97667;
            public const long ClientManagers            = 6528;
            public const long ClientEditors             = 141995;
            public const long Admins                    = 6527;
            public const long SettingsManagers          = 163685;
            public const long SettingsEditors           = 142445;
            public const long NugetPublisherAdmin       = 122584;
            public const long NugetPublisherAdminClient = 135204;
            public const long CanPublish                = 134199;
            public const long EmailManagers             = 132886;
            public const long EmailEditors              = 142005;
            public const long ContentManagers           = 133362;
            public const long AutoApproveProducts       = 134485;
            public const long SupportApiClients         = 97906;
            public const long Trial                     = 140676;
            public const long ErrorManagers             = 141998;
            public const long ErrorEditors              = 141999;
            public const long LicenseManagers           = 142001;
            public const long LicenseEditors            = 142002;
            public const long FileManagers              = 142003;
            public const long FileEditors               = 142004;
            public const long ProductManagers           = 141996;
            public const long ProductEditors            = 141997;
            public const long ProductOrderEditors       = 142000;
            public const long ShortUrlManagers          = 142006;
            public const long ShortUrlEditors           = 142007;
            public const long PaymentManagers           = 142008;
            public const long PaymentEditors            = 142009;
            public const long Anonymous                 = 145183;
            public const long LoggedIn                  = 145184;
            public const long DisableFreelance          = 145436;
            public const long Awards                    = 145496;
            public const long InstrumentManagers        = 164286;
            public const long InstrumentEditors         = 162513;
            public const long DiagramManagers           = 164299;
            public const long DiagramEditors            = 164300;
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
            public const long WealthLab              = 3;
            public const long Api                    = 5;
            public const long Studio                 = 7;
            public const long Hydra                  = 8;
            public const long Designer               = 9;
            public const long Terminal               = 10;
            public const long Shell                  = 11;
            public const long MatLab                 = 12;
            public const long Lci                    = 13;
            public const long Server                 = 14;
            public const long Installer              = 16;
            public const long HydraServer            = 363;
            public const long CGate                  = 75;
            public const long Fix                    = 76;
            public const long Itch                   = 77;
            public const long MT4                    = 20;
            public const long MT5                    = 21;
            public const long QuantFeed              = 78;
            public const long SpbEx                  = 79;
            public const long MicexTeap              = 80;
            public const long Twime                  = 81;
            public const long BitStamp               = 32;
            public const long CryptoBinaryAll        = 177;
            public const long CryptoSourceDevelopNew = 180;
            public const long FixBinaryAll           = 181;
            public const long ItchBinaryAll          = 182;
            public const long AllBinary              = 183;
            public const long AllBinaryAll           = 184;
            public const long Trial                  = 327;
            public const long Old                    = 328;
            public const long SupportForum           = 297;
            public const long Balance                = 326;
            public const long LicenseTool            = 194;
            public const long TerminalMobile         = 199;
            public const long Publisher              = 193;
        }

        public static class ProductGroup
        {
            public const long ProductsRoot = 2;
            public const long BrokersRoot  = 1;
            public const long Diagram      = 4;
            public const long Free         = 10;
            public const long EduRoot      = 23;
            public const long TrialAllow   = 24;
            public const long PaidMessages = 25;
            public const long Crowds       = 26;
            public const long Freelance    = 27;
            public const long RefundAllow  = 28;
            public const long TrialManual  = 29;
            public const long Develop      = 31;
            public const long Pricing      = 37;
            public const long OneApp       = 39;
        }

        public static class PayGateway
        {
            public const long Balance = 7;
            public const long None = 8;
        }

        public static class LicenseFeature
        {
            public const long None = 94;
        }

        public static class File
        {
            public const long NoAvatarWoman         = 101449;
            public const long NoAvatarMan           = 101448;
            public const long Logo                  = 101465;
            public const long NotFound              = 103644;
            public const long Forbidden             = 104253;
            public const long NotFoundIcon          = 120943;
            public const long ProductDefaultPicture = 116399;
            public const long LogoLarge             = 102332;
            public const long Diagram               = 132439;
        }

        public static class FileGroup
        {
            public const long Nuget              = 85;
            public const long Connectors         = 93;
            public const long Sources            = 94;
            public const long PublicNuget        = 88;
            public const long PrivateNuget       = 306;
            public const long Special            = 365;
            public const long EmailHtmlTemplate  = 366;
            public const long EmailPlainTemplate = 367;
            public const long Avatars            = 368;
            public const long WatermarkVideo     = 369;
            public const long Anonymous          = 370;
            public const long LoggedIn           = 371;
            public const long CommunityPublic    = 436;
            public const long CommunityPrivate   = 435;
        }

        public static class EmailTemplate
        {
            public const long RegisterActivation = 14;
            public const long RegisterWelcome    = 33;
            public const long ForgotPassword     = 17;
            public const long PrivateMessage     = 34;
            public const long License            = 35;
            public const long NewComment         = 36;
            public const long Starred            = 37;
            public const long SuspiciousMessage  = 45;
            public const long AttachChanged      = 46;
            public const long MessagePatch       = 47;
        }

        public static class DynamicPage
        {
            public const long RootPage          = 1;
            public const long AboutStockSharp   = 171;
            public const long BrokerPartnership = 142;
            public const long BrokerFaq         = 213;
            public const long Store             = 164;
            public const long Products          = 155;
            public const long Pricing           = 157;
            public const long Error             = 204;
            public const long Error403          = 212;
            public const long Error404          = 149;
            public const long Error500          = 150;
            public const long ErrorExpired      = 146;
            public const long Community         = 173;
            public const long PaymentOk         = 180;
            public const long PaymentError      = 181;
            public const long SmsActivation     = 207;
            public const long AntiBot           = 148;
            public const long Download          = 144;
            public const long SupportWarning    = 160;
            public const long Support           = 209;
            public const long Crypto            = 215;
            public const long Unsubscribe       = 216;
            public const long Away              = 221;
            public const long Pay               = 229;
            public const long PaymentPending    = 231;
            public const long PayStatus         = 230;
            public const long PayGate           = 232;
            public const long PaymentCancelled  = 233;
            public const long Referral          = 234;
            public const long Balance           = 235;
            public const long SelfDelete        = 236;
            public const long Broker            = 203;
            public const long Edu               = 228;
            public const long Freelance         = 227;
            public const long FreelanceFaq      = 238;
            public const long StoreFaq          = 239;
            public const long FreelanceCreate   = 240;
            public const long Develop           = 242;
            public const long Profile           = 243;
            public const long News              = 244;
            public const long Articles          = 245;
            public const long Users             = 246;
            public const long Forum             = 247;
            public const long My                = 248;
            public const long Search            = 249;
            public const long Sitemap           = 250;
            public const long Login             = 251;
            public const long Register          = 252;
            public const long Forgot            = 253;
            public const long PrivateMessages   = 254;
            public const long DevelopCreate     = 259;
            public const long BBCodes           = 260;
            public const long Topic             = 275;
            public const long File              = 276;
            public const long Posts             = 277;
            public const long Messages          = 278;
            public const long BugStatistics     = 279;
            public const long Edit              = 280;
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
    }
}
