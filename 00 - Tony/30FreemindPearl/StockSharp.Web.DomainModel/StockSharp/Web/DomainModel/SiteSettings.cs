
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class SiteSettings : BaseEntity
    {
        public string WhoIsUrl { get; set; }

        public bool ProductAutoApprove { get; set; }

        public string CommunityNuGetPrefix { get; set; }

        public int MailRuCounter { get; set; }

        public int PlusoCounter { get; set; }

        public bool ForumOnlySupport { get; set; }

        public bool PrivateMessages { get; set; }

        public long FileUploadTotalMaxBytes { get; set; }

        public long FileUploadDbLimitBytes { get; set; }

        public Decimal UsdRubRate { get; set; }

        public KeySecret ReCaptcha { get; set; }

        public Decimal ReCaptchaLimit { get; set; }

        public KeySecret SmsService { get; set; }

        public AmazonSettings AmazonBackup { get; set; }

        public AmazonSettings AmazonEmail { get; set; }

        public string EmailCryptoKey { get; set; }

        public KeySecret License { get; set; }

        public int MaxVisitsPerMinute { get; set; }

        public int MaxOrdersPerMinute { get; set; }

        public int MaxSuspicious { get; set; }

        public PaymentEncryptModes PaymentEncryptMode { get; set; }

        public int PaymentRepeatMaxError { get; set; }

        public TimeSpan ReferralTimeOut { get; set; }

        public TimeSpan CacheTimeOut { get; set; }

        public int SubscriptionsDayOffset { get; set; }

        public string NugetOrgToken { get; set; }

        public string NugetStockSharpToken { get; set; }

        public TimeSpan LicenseRefreshOffset { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            WhoIsUrl                = storage.GetValue("WhoIsUrl", (string)null);
            ProductAutoApprove      = storage.GetValue("ProductAutoApprove", false);
            CommunityNuGetPrefix    = storage.GetValue("CommunityNuGetPrefix", (string)null);
            MailRuCounter           = storage.GetValue("MailRuCounter", 0);
            PlusoCounter            = storage.GetValue("PlusoCounter", 0);
            ForumOnlySupport        = storage.GetValue("ForumOnlySupport", false);
            PrivateMessages         = storage.GetValue("PrivateMessages", false);
            FileUploadTotalMaxBytes = storage.GetValue("FileUploadTotalMaxBytes", 0L);
            FileUploadDbLimitBytes  = storage.GetValue("FileUploadDbLimitBytes", 0L);
            UsdRubRate              = storage.GetValue("UsdRubRate", Decimal.Zero);
            ReCaptcha               = storage.GetValue("ReCaptcha", (KeySecret)null);
            ReCaptchaLimit          = storage.GetValue("ReCaptchaLimit", Decimal.Zero);
            SmsService              = storage.GetValue("SmsService", (KeySecret)null);
            AmazonBackup            = storage.GetValue("AmazonBackup", (AmazonSettings)null);
            AmazonEmail             = storage.GetValue("AmazonEmail", (AmazonSettings)null);
            EmailCryptoKey          = storage.GetValue("EmailCryptoKey", (string)null);
            License                 = storage.GetValue("License", (KeySecret)null);
            MaxVisitsPerMinute      = storage.GetValue("MaxVisitsPerMinute", 0);
            MaxOrdersPerMinute      = storage.GetValue("MaxOrdersPerMinute", 0);
            MaxSuspicious           = storage.GetValue("MaxSuspicious", 0);
            PaymentEncryptMode      = storage.GetValue("PaymentEncryptMode", PaymentEncryptModes.None);
            PaymentRepeatMaxError   = storage.GetValue("PaymentRepeatMaxError", 0);
            ReferralTimeOut         = storage.GetValue("ReferralTimeOut", new TimeSpan());
            CacheTimeOut            = storage.GetValue("CacheTimeOut", new TimeSpan());
            SubscriptionsDayOffset  = storage.GetValue("SubscriptionsDayOffset", 0);
            NugetOrgToken           = storage.GetValue("NugetOrgToken", (string)null);
            NugetStockSharpToken    = storage.GetValue("NugetStockSharpToken", (string)null);
            LicenseRefreshOffset    = storage.GetValue("LicenseRefreshOffset", new TimeSpan());
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("WhoIsUrl", WhoIsUrl).Set("ProductAutoApprove", ProductAutoApprove).Set("CommunityNuGetPrefix", CommunityNuGetPrefix).Set("MailRuCounter", MailRuCounter).Set("PlusoCounter", PlusoCounter).Set("ForumOnlySupport", ForumOnlySupport).Set("PrivateMessages", PrivateMessages).Set("FileUploadTotalMaxBytes", FileUploadTotalMaxBytes).Set("FileUploadDbLimitBytes", FileUploadDbLimitBytes).Set("UsdRubRate", UsdRubRate).Set("ReCaptcha", ReCaptcha).Set("ReCaptchaLimit", ReCaptchaLimit).Set("SmsService", SmsService).Set("AmazonBackup", AmazonBackup).Set("AmazonEmail", AmazonEmail).Set("EmailCryptoKey", EmailCryptoKey).Set("License", License).Set("MaxVisitsPerMinute", MaxVisitsPerMinute).Set("MaxOrdersPerMinute", MaxOrdersPerMinute).Set("MaxSuspicious", MaxSuspicious).Set("PaymentEncryptMode", PaymentEncryptMode).Set("PaymentRepeatMaxError", PaymentRepeatMaxError).Set("ReferralTimeOut", ReferralTimeOut).Set("CacheTimeOut", CacheTimeOut).Set("SubscriptionsDayOffset", SubscriptionsDayOffset).Set("NugetOrgToken", NugetOrgToken).Set("NugetStockSharpToken", NugetStockSharpToken).Set("LicenseRefreshOffset", LicenseRefreshOffset);
        }
    }
}
