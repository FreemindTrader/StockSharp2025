
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class Product : ProductBase
    {
        public FileGroup Distributive { get; set; }

        public Client Client { get; set; }

        public string PackageId { get; set; }

        public string DocUrl { get; set; }

        public ProductContentTypes2 ContentType2 { get; set; }

        public Product SourcesProduct { get; set; }

        public ProductContentTypes2? SupportedPlugins { get; set; }

        public int SortOrder { get; set; }

        public int SortOrderEx { get; set; }

        public ProductFlags Flags { get; set; }

        public ProductOrder Executor { get; set; }

        public ProductFreelanceStages Stage { get; set; }

        public DateTime? PurchasedTill { get; set; }

        public Decimal? Rating { get; set; }

        public ProductScopes Scope { get; set; }

        public int DownloadsCount { get; set; }

        public PackageRepositories Repository { get; set; }

        public bool? IsTrialAllow { get; set; }

        public bool? IsTrialRequested { get; set; }

        public bool? IsRefundAllow { get; set; }

        public bool? IsRefundRequested { get; set; }

        public bool? IsPurchased { get; set; }

        public bool? IsStartAllow { get; set; }

        public bool? IsFinishAllow { get; set; }

        public bool? IsPayoutAllow { get; set; }

        public bool? IsMoneyCancelAllow { get; set; }

        public bool? IsCrowd { get; set; }

        public BaseEntitySet<Client> BlockedClients { get; set; }

        public BaseEntitySet<ProductBugReport> BugReports { get; set; }

        public BaseEntitySet<Client> Clients { get; set; }

        public BaseEntitySet<ProductDomain> Domains { get; set; }

        public BaseEntitySet<ProductFeedback> Feedbacks { get; set; }

        public BaseEntitySet<ProductGroup> Groups { get; set; }

        public BaseEntitySet<ProductOrder> Orders { get; set; }

        public BaseEntitySet<ProductOrder> OrdersActive { get; set; }

        public BaseEntitySet<ProductOrder> OrdersReferral { get; set; }

        public BaseEntitySet<ProductOrder> OrdersRepeat { get; set; }

        public BaseEntitySet<ProductOrder> OrdersTrial { get; set; }

        public BaseEntitySet<ProductOrder> OrdersRefund { get; set; }

        public BaseEntitySet<Payment> Payments { get; set; }

        public BaseEntitySet<ProductRole> Roles { get; set; }

        public BaseEntitySet<Session> Sessions { get; set; }

        public BaseEntitySet<ProductVersionMap> VersionMap { get; set; }

        
        public ProductContentTypes2 ContentType { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Distributive       = storage.GetValue("Distributive", (FileGroup)null);
            Client             = storage.GetValue("Client", (Client)null);
            PackageId          = storage.GetValue("PackageId", (string)null);
            DocUrl             = storage.GetValue("DocUrl", (string)null);
            ContentType2       = storage.GetValue("ContentType2", (ProductContentTypes2)0);
            SourcesProduct     = storage.GetValue("SourcesProduct", (Product)null);
            SupportedPlugins   = storage.GetValue("SupportedPlugins", new ProductContentTypes2?());
            SortOrder          = storage.GetValue("SortOrder", 0);
            SortOrderEx        = storage.GetValue("SortOrderEx", 0);
            Flags              = storage.GetValue("Flags", ProductFlags.None);
            Executor           = storage.GetValue("Executor", (ProductOrder)null);
            Stage              = storage.GetValue("Stage", ProductFreelanceStages.None);
            PurchasedTill      = storage.GetValue("PurchasedTill", new DateTime?());
            Rating             = storage.GetValue("Rating", new Decimal?());
            Scope              = storage.GetValue("Scope", ProductScopes.Public);
            DownloadsCount     = storage.GetValue("DownloadsCount", 0);
            Repository         = storage.GetValue("Repository", PackageRepositories.NuGet);
            IsTrialAllow       = storage.GetValue("IsTrialAllow", new bool?());
            IsTrialRequested   = storage.GetValue("IsTrialRequested", new bool?());
            IsRefundAllow      = storage.GetValue("IsRefundAllow", new bool?());
            IsRefundRequested  = storage.GetValue("IsRefundRequested", new bool?());
            IsPurchased        = storage.GetValue("IsPurchased", new bool?());
            IsStartAllow       = storage.GetValue("IsStartAllow", new bool?());
            IsFinishAllow      = storage.GetValue("IsFinishAllow", new bool?());
            IsPayoutAllow      = storage.GetValue("IsPayoutAllow", new bool?());
            IsMoneyCancelAllow = storage.GetValue("IsMoneyCancelAllow", new bool?());
            IsCrowd            = storage.GetValue("IsCrowd", new bool?());
            BlockedClients     = storage.GetValue("BlockedClients", (BaseEntitySet<Client>)null);
            BugReports         = storage.GetValue("BugReports", (BaseEntitySet<ProductBugReport>)null);
            Clients            = storage.GetValue("Clients", (BaseEntitySet<Client>)null);
            Domains            = storage.GetValue("Domains", (BaseEntitySet<ProductDomain>)null);
            Feedbacks          = storage.GetValue("Feedbacks", (BaseEntitySet<ProductFeedback>)null);
            Groups             = storage.GetValue("Groups", (BaseEntitySet<ProductGroup>)null);
            Orders             = storage.GetValue("Orders", (BaseEntitySet<ProductOrder>)null);
            OrdersActive       = storage.GetValue("OrdersActive", (BaseEntitySet<ProductOrder>)null);
            OrdersReferral     = storage.GetValue("OrdersReferral", (BaseEntitySet<ProductOrder>)null);
            OrdersRepeat       = storage.GetValue("OrdersRepeat", (BaseEntitySet<ProductOrder>)null);
            OrdersTrial        = storage.GetValue("OrdersTrial", (BaseEntitySet<ProductOrder>)null);
            OrdersRefund       = storage.GetValue("OrdersRefund", (BaseEntitySet<ProductOrder>)null);
            Payments           = storage.GetValue("Payments", (BaseEntitySet<Payment>)null);
            Roles              = storage.GetValue("Roles", (BaseEntitySet<ProductRole>)null);
            Sessions           = storage.GetValue("Sessions", (BaseEntitySet<Session>)null);
            VersionMap         = storage.GetValue("VersionMap", (BaseEntitySet<ProductVersionMap>)null);
            ContentType        = storage.GetValue("ContentType", ProductContentTypes2.SourceCode);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Distributive", Distributive).Set("Client", Client).Set("PackageId", PackageId).Set("DocUrl", DocUrl).Set("ContentType2", ContentType2).Set("SourcesProduct", SourcesProduct).Set("SupportedPlugins", SupportedPlugins).Set("SortOrder", SortOrder).Set("SortOrderEx", SortOrderEx).Set("Flags", Flags).Set("Executor", Executor).Set("Stage", Stage).Set("PurchasedTill", PurchasedTill).Set("Rating", Rating).Set("Scope", Scope).Set("DownloadsCount", DownloadsCount).Set("Repository", Repository).Set("IsTrialAllow", IsTrialAllow).Set("IsTrialRequested", IsTrialRequested).Set("IsRefundAllow", IsRefundAllow).Set("IsRefundRequested", IsRefundRequested).Set("IsPurchased", IsPurchased).Set("IsStartAllow", IsStartAllow).Set("IsFinishAllow", IsFinishAllow).Set("IsPayoutAllow", IsPayoutAllow).Set("IsMoneyCancelAllow", IsMoneyCancelAllow).Set("IsCrowd", IsCrowd).Set("BlockedClients", BlockedClients).Set("BugReports", BugReports).Set("Clients", Clients).Set("Domains", Domains).Set("Feedbacks", Feedbacks).Set("Groups", Groups).Set("Orders", Orders).Set("OrdersActive", OrdersActive).Set("OrdersReferral", OrdersReferral).Set("OrdersRepeat", OrdersRepeat).Set("OrdersTrial", OrdersTrial).Set("OrdersRefund", OrdersRefund).Set("Payments", Payments).Set("Roles", Roles).Set("Sessions", Sessions).Set("VersionMap", VersionMap).Set("ContentType", ContentType);
        }
    }
}
