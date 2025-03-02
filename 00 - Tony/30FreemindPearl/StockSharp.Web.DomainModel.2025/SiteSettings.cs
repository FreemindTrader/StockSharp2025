// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SiteSettings
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Licensing;
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.WhoIsUrl = ( string ) storage.GetValue<string>( "WhoIsUrl", null );
            this.ProductAutoApprove = ( bool ) storage.GetValue<bool>( "ProductAutoApprove", false );
            this.CommunityNuGetPrefix = ( string ) storage.GetValue<string>( "CommunityNuGetPrefix", null );
            this.MailRuCounter = ( int ) storage.GetValue<int>( "MailRuCounter", 0 );
            this.PlusoCounter = ( int ) storage.GetValue<int>( "PlusoCounter", 0 );
            this.ForumOnlySupport = ( bool ) storage.GetValue<bool>( "ForumOnlySupport", false );
            this.PrivateMessages = ( bool ) storage.GetValue<bool>( "PrivateMessages", false );
            this.FileUploadTotalMaxBytes = ( long ) storage.GetValue<long>( "FileUploadTotalMaxBytes", 0L );
            this.FileUploadDbLimitBytes = ( long ) storage.GetValue<long>( "FileUploadDbLimitBytes", 0L );
            this.UsdRubRate = ( Decimal ) storage.GetValue<Decimal>( "UsdRubRate", Decimal.Zero );
            this.ReCaptcha = ( KeySecret ) storage.GetValue<KeySecret>( "ReCaptcha", null );
            this.ReCaptchaLimit = ( Decimal ) storage.GetValue<Decimal>( "ReCaptchaLimit", Decimal.Zero );
            this.SmsService = ( KeySecret ) storage.GetValue<KeySecret>( "SmsService", null );
            this.AmazonBackup = ( AmazonSettings ) storage.GetValue<AmazonSettings>( "AmazonBackup", null );
            this.AmazonEmail = ( AmazonSettings ) storage.GetValue<AmazonSettings>( "AmazonEmail", null );
            this.EmailCryptoKey = ( string ) storage.GetValue<string>( "EmailCryptoKey", null );
            this.License = ( KeySecret ) storage.GetValue<KeySecret>( "License", null );
            this.MaxVisitsPerMinute = ( int ) storage.GetValue<int>( "MaxVisitsPerMinute", 0 );
            this.MaxOrdersPerMinute = ( int ) storage.GetValue<int>( "MaxOrdersPerMinute", 0 );
            this.MaxSuspicious = ( int ) storage.GetValue<int>( "MaxSuspicious", 0 );
            this.PaymentRepeatMaxError = ( int ) storage.GetValue<int>( "PaymentRepeatMaxError", 0 );
            this.ReferralTimeOut = ( TimeSpan ) storage.GetValue<TimeSpan>( "ReferralTimeOut", new TimeSpan() );
            this.CacheTimeOut = ( TimeSpan ) storage.GetValue<TimeSpan>( "CacheTimeOut", new TimeSpan() );
            this.SubscriptionsOffset = ( TimeSpan ) storage.GetValue<TimeSpan>( "SubscriptionsOffset", new TimeSpan() );
            this.NugetOrgToken = ( string ) storage.GetValue<string>( "NugetOrgToken", null );
            this.NugetStockSharpToken = ( string ) storage.GetValue<string>( "NugetStockSharpToken", null );
            this.LicenseRefreshOffset = ( TimeSpan ) storage.GetValue<TimeSpan>( "LicenseRefreshOffset", new TimeSpan() );
            this.Mjml = ( KeySecret ) storage.GetValue<KeySecret>( "Mjml", null );
            this.SocialMaxLength = ( int ) storage.GetValue<int>( "SocialMaxLength", 0 );
            this.GoogleSearchToken = ( string ) storage.GetValue<string>( "GoogleSearchToken", null );
            this.TempSessionTimeOut = ( TimeSpan ) storage.GetValue<TimeSpan>( "TempSessionTimeOut", new TimeSpan() );
            this.TelegramBot = ( string ) storage.GetValue<string>( "TelegramBot", null );
            this.Backtest = ( StrategyBacktestOptions ) storage.GetValue<StrategyBacktestOptions>( "Backtest", null );
            this.Optimization = ( StrategyOptimizationOptions ) storage.GetValue<StrategyOptimizationOptions>( "Optimization", null );
            this.Live = ( StrategyLiveOptions ) storage.GetValue<StrategyLiveOptions>( "Live", null );
            this.GeneticPopulationMax = ( int ) storage.GetValue<int>( "GeneticPopulationMax", 0 );
            this.GeneticGenerationsMax = ( int ) storage.GetValue<int>( "GeneticGenerationsMax", 0 );
            this.MaxSettingsLength = ( int ) storage.GetValue<int>( "MaxSettingsLength", 0 );
            this.LicenseExpireAction = ( LicenseExpireActions ) storage.GetValue<LicenseExpireActions>( "LicenseExpireAction", 0 );
            this.LicenseMaxRenew = ( int ) storage.GetValue<int>( "LicenseMaxRenew", 0 );
            this.AIStrategyGenerate = ( string ) storage.GetValue<string>( "AIStrategyGenerate", null );
            this.AIStrategyBodyAnalyze = ( string ) storage.GetValue<string>( "AIStrategyBodyAnalyze", null );
            this.AIStrategyResultAnalyze = ( string ) storage.GetValue<string>( "AIStrategyResultAnalyze", null );
            this.AISocial = ( string ) storage.GetValue<string>( "AISocial", null );
            this.AIMaxChars = ( int ) storage.GetValue<int>( "AIMaxChars", 0 );
            this.PricingDiscountTimeOut = ( TimeSpan ) storage.GetValue<TimeSpan>( "PricingDiscountTimeOut", new TimeSpan() );
            this.PricingDiscount = ( Price ) storage.GetValue<Price>( "PricingDiscount", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "WhoIsUrl", this.WhoIsUrl ).Set<bool>( "ProductAutoApprove", ( this.ProductAutoApprove ) ).Set<string>( "CommunityNuGetPrefix", this.CommunityNuGetPrefix ).Set<int>( "MailRuCounter", this.MailRuCounter ).Set<int>( "PlusoCounter", this.PlusoCounter ).Set<bool>( "ForumOnlySupport", ( this.ForumOnlySupport  ) ).Set<bool>( "PrivateMessages", ( this.PrivateMessages  ) ).Set<long>( "FileUploadTotalMaxBytes", this.FileUploadTotalMaxBytes ).Set<long>( "FileUploadDbLimitBytes", this.FileUploadDbLimitBytes ).Set<Decimal>( "UsdRubRate", this.UsdRubRate ).Set<KeySecret>( "ReCaptcha", this.ReCaptcha ).Set<Decimal>( "ReCaptchaLimit", this.ReCaptchaLimit ).Set<KeySecret>( "SmsService", this.SmsService ).Set<AmazonSettings>( "AmazonBackup", this.AmazonBackup ).Set<AmazonSettings>( "AmazonEmail", this.AmazonEmail ).Set<string>( "EmailCryptoKey", this.EmailCryptoKey ).Set<KeySecret>( "License", this.License ).Set<int>( "MaxVisitsPerMinute", this.MaxVisitsPerMinute ).Set<int>( "MaxOrdersPerMinute", this.MaxOrdersPerMinute ).Set<int>( "MaxSuspicious", this.MaxSuspicious ).Set<int>( "PaymentRepeatMaxError", this.PaymentRepeatMaxError ).Set<TimeSpan>( "ReferralTimeOut", this.ReferralTimeOut ).Set<TimeSpan>( "CacheTimeOut", this.CacheTimeOut ).Set<TimeSpan>( "SubscriptionsOffset", this.SubscriptionsOffset ).Set<string>( "NugetOrgToken", this.NugetOrgToken ).Set<string>( "NugetStockSharpToken", this.NugetStockSharpToken ).Set<TimeSpan>( "LicenseRefreshOffset", this.LicenseRefreshOffset ).Set<KeySecret>( "Mjml", this.Mjml ).Set<int>( "SocialMaxLength", this.SocialMaxLength ).Set<string>( "GoogleSearchToken", this.GoogleSearchToken ).Set<TimeSpan>( "TempSessionTimeOut", this.TempSessionTimeOut ).Set<string>( "TelegramBot", this.TelegramBot ).Set<StrategyBacktestOptions>( "Backtest", this.Backtest ).Set<StrategyOptimizationOptions>( "Optimization", this.Optimization ).Set<StrategyLiveOptions>( "Live", this.Live ).Set<int>( "GeneticPopulationMax", this.GeneticPopulationMax ).Set<int>( "GeneticGenerationsMax", this.GeneticGenerationsMax ).Set<int>( "MaxSettingsLength", this.MaxSettingsLength ).Set<LicenseExpireActions>( "LicenseExpireAction", this.LicenseExpireAction ).Set<int>( "LicenseMaxRenew", this.LicenseMaxRenew ).Set<string>( "AIStrategyGenerate", this.AIStrategyGenerate ).Set<string>( "AIStrategyBodyAnalyze", this.AIStrategyBodyAnalyze ).Set<string>( "AIStrategyResultAnalyze", this.AIStrategyResultAnalyze ).Set<string>( "AISocial", this.AISocial ).Set<int>( "AIMaxChars", this.AIMaxChars ).Set<TimeSpan>( "PricingDiscountTimeOut", this.PricingDiscountTimeOut ).Set<Price>( "PricingDiscount", this.PricingDiscount );
        }
    }
}
