// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SiteSettings
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Licensing;

#nullable disable
namespace StockSharp.Web.DomainModel;

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

    public int PaymentRepeatMaxError { get; set; }

    public TimeSpan ReferralTimeOut { get; set; }

    public TimeSpan CacheTimeOut { get; set; }

    public TimeSpan SubscriptionsOffset { get; set; }

    public string NugetOrgToken { get; set; }

    public string NugetStockSharpToken { get; set; }

    public TimeSpan LicenseRefreshOffset { get; set; }

    public KeySecret Mjml { get; set; }

    public int SocialMaxLength { get; set; }

    public string GoogleSearchToken { get; set; }

    public TimeSpan TempSessionTimeOut { get; set; }

    public string TelegramBot { get; set; }

    public StrategyBacktestOptions Backtest { get; set; }

    public StrategyOptimizationOptions Optimization { get; set; }

    public StrategyLiveOptions Live { get; set; }

    public int GeneticPopulationMax { get; set; }

    public int GeneticGenerationsMax { get; set; }

    public int MaxSettingsLength { get; set; }

    public LicenseExpireActions LicenseExpireAction { get; set; }

    public int LicenseMaxRenew { get; set; }

    public string AIStrategyGenerate { get; set; }

    public string AIStrategyBodyAnalyze { get; set; }

    public string AIStrategyResultAnalyze { get; set; }

    public string AISocial { get; set; }

    public int AIMaxChars { get; set; }

    public TimeSpan PricingDiscountTimeOut { get; set; }

    public Price PricingDiscount { get; set; }

    public File Cert { get; set; }

    public string CertPassword { get; set; }

    public string DotNetVersion { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.WhoIsUrl = storage.GetValue<string>("WhoIsUrl", (string)null);
        this.ProductAutoApprove = storage.GetValue<bool>("ProductAutoApprove", false);
        this.CommunityNuGetPrefix = storage.GetValue<string>("CommunityNuGetPrefix", (string)null);
        this.MailRuCounter = storage.GetValue<int>("MailRuCounter", 0);
        this.PlusoCounter = storage.GetValue<int>("PlusoCounter", 0);
        this.ForumOnlySupport = storage.GetValue<bool>("ForumOnlySupport", false);
        this.PrivateMessages = storage.GetValue<bool>("PrivateMessages", false);
        this.FileUploadTotalMaxBytes = storage.GetValue<long>("FileUploadTotalMaxBytes", 0L);
        this.FileUploadDbLimitBytes = storage.GetValue<long>("FileUploadDbLimitBytes", 0L);
        this.UsdRubRate = storage.GetValue<Decimal>("UsdRubRate", 0M);
        this.ReCaptcha = storage.GetValue<KeySecret>("ReCaptcha", (KeySecret)null);
        this.ReCaptchaLimit = storage.GetValue<Decimal>("ReCaptchaLimit", 0M);
        this.SmsService = storage.GetValue<KeySecret>("SmsService", (KeySecret)null);
        this.AmazonBackup = storage.GetValue<AmazonSettings>("AmazonBackup", (AmazonSettings)null);
        this.AmazonEmail = storage.GetValue<AmazonSettings>("AmazonEmail", (AmazonSettings)null);
        this.EmailCryptoKey = storage.GetValue<string>("EmailCryptoKey", (string)null);
        this.License = storage.GetValue<KeySecret>("License", (KeySecret)null);
        this.MaxVisitsPerMinute = storage.GetValue<int>("MaxVisitsPerMinute", 0);
        this.MaxOrdersPerMinute = storage.GetValue<int>("MaxOrdersPerMinute", 0);
        this.MaxSuspicious = storage.GetValue<int>("MaxSuspicious", 0);
        this.PaymentRepeatMaxError = storage.GetValue<int>("PaymentRepeatMaxError", 0);
        this.ReferralTimeOut = storage.GetValue<TimeSpan>("ReferralTimeOut", new TimeSpan());
        this.CacheTimeOut = storage.GetValue<TimeSpan>("CacheTimeOut", new TimeSpan());
        this.SubscriptionsOffset = storage.GetValue<TimeSpan>("SubscriptionsOffset", new TimeSpan());
        this.NugetOrgToken = storage.GetValue<string>("NugetOrgToken", (string)null);
        this.NugetStockSharpToken = storage.GetValue<string>("NugetStockSharpToken", (string)null);
        this.LicenseRefreshOffset = storage.GetValue<TimeSpan>("LicenseRefreshOffset", new TimeSpan());
        this.Mjml = storage.GetValue<KeySecret>("Mjml", (KeySecret)null);
        this.SocialMaxLength = storage.GetValue<int>("SocialMaxLength", 0);
        this.GoogleSearchToken = storage.GetValue<string>("GoogleSearchToken", (string)null);
        this.TempSessionTimeOut = storage.GetValue<TimeSpan>("TempSessionTimeOut", new TimeSpan());
        this.TelegramBot = storage.GetValue<string>("TelegramBot", (string)null);
        this.Backtest = storage.GetValue<StrategyBacktestOptions>("Backtest", (StrategyBacktestOptions)null);
        this.Optimization = storage.GetValue<StrategyOptimizationOptions>("Optimization", (StrategyOptimizationOptions)null);
        this.Live = storage.GetValue<StrategyLiveOptions>("Live", (StrategyLiveOptions)null);
        this.GeneticPopulationMax = storage.GetValue<int>("GeneticPopulationMax", 0);
        this.GeneticGenerationsMax = storage.GetValue<int>("GeneticGenerationsMax", 0);
        this.MaxSettingsLength = storage.GetValue<int>("MaxSettingsLength", 0);
        this.LicenseExpireAction = storage.GetValue<LicenseExpireActions>("LicenseExpireAction", LicenseExpireActions.PreventWork);
        this.LicenseMaxRenew = storage.GetValue<int>("LicenseMaxRenew", 0);
        this.AIStrategyGenerate = storage.GetValue<string>("AIStrategyGenerate", (string)null);
        this.AIStrategyBodyAnalyze = storage.GetValue<string>("AIStrategyBodyAnalyze", (string)null);
        this.AIStrategyResultAnalyze = storage.GetValue<string>("AIStrategyResultAnalyze", (string)null);
        this.AISocial = storage.GetValue<string>("AISocial", (string)null);
        this.AIMaxChars = storage.GetValue<int>("AIMaxChars", 0);
        this.PricingDiscountTimeOut = storage.GetValue<TimeSpan>("PricingDiscountTimeOut", new TimeSpan());
        this.PricingDiscount = storage.GetValue<Price>("PricingDiscount", (Price)null);
        this.Cert = storage.GetValue<File>("Cert", (File)null);
        this.CertPassword = storage.GetValue<string>("CertPassword", (string)null);
        this.DotNetVersion = storage.GetValue<string>("DotNetVersion", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("WhoIsUrl", this.WhoIsUrl).Set<bool>("ProductAutoApprove", this.ProductAutoApprove).Set<string>("CommunityNuGetPrefix", this.CommunityNuGetPrefix).Set<int>("MailRuCounter", this.MailRuCounter).Set<int>("PlusoCounter", this.PlusoCounter).Set<bool>("ForumOnlySupport", this.ForumOnlySupport).Set<bool>("PrivateMessages", this.PrivateMessages).Set<long>("FileUploadTotalMaxBytes", this.FileUploadTotalMaxBytes).Set<long>("FileUploadDbLimitBytes", this.FileUploadDbLimitBytes).Set<Decimal>("UsdRubRate", this.UsdRubRate).Set<KeySecret>("ReCaptcha", this.ReCaptcha).Set<Decimal>("ReCaptchaLimit", this.ReCaptchaLimit).Set<KeySecret>("SmsService", this.SmsService).Set<AmazonSettings>("AmazonBackup", this.AmazonBackup).Set<AmazonSettings>("AmazonEmail", this.AmazonEmail).Set<string>("EmailCryptoKey", this.EmailCryptoKey).Set<KeySecret>("License", this.License).Set<int>("MaxVisitsPerMinute", this.MaxVisitsPerMinute).Set<int>("MaxOrdersPerMinute", this.MaxOrdersPerMinute).Set<int>("MaxSuspicious", this.MaxSuspicious).Set<int>("PaymentRepeatMaxError", this.PaymentRepeatMaxError).Set<TimeSpan>("ReferralTimeOut", this.ReferralTimeOut).Set<TimeSpan>("CacheTimeOut", this.CacheTimeOut).Set<TimeSpan>("SubscriptionsOffset", this.SubscriptionsOffset).Set<string>("NugetOrgToken", this.NugetOrgToken).Set<string>("NugetStockSharpToken", this.NugetStockSharpToken).Set<TimeSpan>("LicenseRefreshOffset", this.LicenseRefreshOffset).Set<KeySecret>("Mjml", this.Mjml).Set<int>("SocialMaxLength", this.SocialMaxLength).Set<string>("GoogleSearchToken", this.GoogleSearchToken).Set<TimeSpan>("TempSessionTimeOut", this.TempSessionTimeOut).Set<string>("TelegramBot", this.TelegramBot).Set<StrategyBacktestOptions>("Backtest", this.Backtest).Set<StrategyOptimizationOptions>("Optimization", this.Optimization).Set<StrategyLiveOptions>("Live", this.Live).Set<int>("GeneticPopulationMax", this.GeneticPopulationMax).Set<int>("GeneticGenerationsMax", this.GeneticGenerationsMax).Set<int>("MaxSettingsLength", this.MaxSettingsLength).Set<LicenseExpireActions>("LicenseExpireAction", this.LicenseExpireAction).Set<int>("LicenseMaxRenew", this.LicenseMaxRenew).Set<string>("AIStrategyGenerate", this.AIStrategyGenerate).Set<string>("AIStrategyBodyAnalyze", this.AIStrategyBodyAnalyze).Set<string>("AIStrategyResultAnalyze", this.AIStrategyResultAnalyze).Set<string>("AISocial", this.AISocial).Set<int>("AIMaxChars", this.AIMaxChars).Set<TimeSpan>("PricingDiscountTimeOut", this.PricingDiscountTimeOut).Set<Price>("PricingDiscount", this.PricingDiscount).Set<File>("Cert", this.Cert).Set<string>("CertPassword", this.CertPassword).Set<string>("DotNetVersion", this.DotNetVersion);
    }
}
