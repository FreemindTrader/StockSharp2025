// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Product
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.DomainModel
{
    public class Product : ProductBase, IClientEntity, INameEntity, IDescriptionEntity, IDomainsEntity<ProductDomain>, IProductContentTypeEntity, IUserIdEntity
    {
        private string _name;

        public FileGroup Distributive { get; set; }

        public Client Client { get; set; }

        public string PackageId { get; set; }

        public string DocUrl { get; set; }

        public ProductContentTypes2 ContentType { get; set; }

        public Product SourcesProduct { get; set; }

        public int SortOrder { get; set; }

        public int SortOrderEx { get; set; }

        public ProductFlags Flags { get; set; }

        public ProductOrder Executor { get; set; }

        public ProductFreelanceStages Stage { get; set; }

        public DateTime? PurchasedTill { get; set; }

        public Decimal? Rating { get; set; }

        public ProductScopes Scope { get; set; }

        [Obsolete( "Use NugetInfo.Repository property." )]
        public PackageRepositories Repository { get; set; }

        public bool? IsTrialRequested { get; set; }

        public bool? IsRefundAllow { get; set; }

        public bool? IsRefundRequested { get; set; }

        public bool? IsPurchased { get; set; }

        public bool? IsStartAllow { get; set; }

        public bool? IsFinishAllow { get; set; }

        public bool? IsPayoutAllow { get; set; }

        public bool? IsMoneyCancelAllow { get; set; }

        public bool? IsCrowd { get; set; }

        public bool? CanEdit { get; set; }

        public bool? CanOpenAccount { get; set; }

        public bool? IsFreelance { get; set; }

        public bool? IsDevelopment { get; set; }

        public bool? IsBroker { get; set; }

        public string UserId { get; set; }

        public string Frameworks { get; set; }

        public File Screenshot { get; set; }

        public string SourcesLink { get; set; }

        [Obsolete( "Use Nuget.DownloadsCount." )]
        public int DownloadsCount { get; set; }

        [Obsolete( "Use TrialPeriod instead." )]
        public bool? IsTrialAllow { get; set; }

        [Obsolete( "Use ContentType instead." )]
        public ProductContentTypes2 ContentType2 { get; set; }

        public ProductNugetInfo NugetInfo { get; set; }

        public string AvailableVersion { get; set; }

        [Obsolete( "Use Plugins instead." )]
        public ProductContentTypes2 SupportedPlugins
        {
            get
            {
                BaseEntitySet<ProductPlugin> plugins = this.Plugins;
                ProductContentTypes2? nullable;
                if ( plugins == null )
                {
                    nullable = new ProductContentTypes2?();
                }
                else
                {
                    ProductPlugin[] items = plugins.Items;
                    nullable = items != null ? new ProductContentTypes2?( ( ProductContentTypes2 ) Enumerator.JoinMask<ProductContentTypes2>( ( ( IEnumerable<ProductPlugin> ) items ).Select<ProductPlugin, ProductContentTypes2>( ( Func<ProductPlugin, ProductContentTypes2> ) ( p => p.ContentType ) ) ) ) : new ProductContentTypes2?();
                }
                return nullable.GetValueOrDefault();
            }
        }

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

        public BaseEntitySet<ProductPlugin> Plugins { get; set; }

        public BaseEntitySet<ProductPermission> Permissions { get; set; }

        string INameEntity.Name
        {
            get
            {
                string name = this._name;
                BaseEntitySet<ProductDomain> domains = this.Domains;
                string str1;
                if ( domains == null )
                {
                    str1 = ( string ) null;
                }
                else
                {
                    ProductDomain[] items = domains.Items;
                    str1 = items != null ? ( ( IEnumerable<ProductDomain> ) items ).FirstOrDefault<ProductDomain>()?.Name : ( string ) null;
                }
                string str2 = StringHelper.IsEmpty(this.PackageId, (string) Converter.To<string>( this.Id));
                string str3 = StringHelper.IsEmpty(str1, str2);
                return StringHelper.IsEmpty( name, str3 );
            }
            set
            {
                this._name = value;
            }
        }

        string IDescriptionEntity.Description { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Distributive = ( FileGroup ) storage.GetValue<FileGroup>( "Distributive", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.PackageId = ( string ) storage.GetValue<string>( "PackageId", null );
            this.DocUrl = ( string ) storage.GetValue<string>( "DocUrl", null );
            this.ContentType = ( ProductContentTypes2 ) storage.GetValue<ProductContentTypes2>( "ContentType", 0L );
            this.SourcesProduct = ( Product ) storage.GetValue<Product>( "SourcesProduct", null );
            this.SortOrder = ( int ) storage.GetValue<int>( "SortOrder", 0 );
            this.SortOrderEx = ( int ) storage.GetValue<int>( "SortOrderEx", 0 );
            this.Flags = ( ProductFlags ) storage.GetValue<ProductFlags>( "Flags", 0 );
            this.Executor = ( ProductOrder ) storage.GetValue<ProductOrder>( "Executor", null );
            this.Stage = ( ProductFreelanceStages ) storage.GetValue<ProductFreelanceStages>( "Stage", 0 );
            this.PurchasedTill = ( DateTime? ) storage.GetValue<DateTime?>( "PurchasedTill", new DateTime?() );
            this.Rating = ( Decimal? ) storage.GetValue<Decimal?>( "Rating", new Decimal?() );
            this.Scope = ( ProductScopes ) storage.GetValue<ProductScopes>( "Scope", 0 );
            this.Repository = ( PackageRepositories ) storage.GetValue<PackageRepositories>( "Repository", 0 );
            this.IsTrialRequested = ( bool? ) storage.GetValue<bool?>( "IsTrialRequested", new bool?() );
            this.IsRefundAllow = ( bool? ) storage.GetValue<bool?>( "IsRefundAllow", new bool?() );
            this.IsRefundRequested = ( bool? ) storage.GetValue<bool?>( "IsRefundRequested", new bool?() );
            this.IsPurchased = ( bool? ) storage.GetValue<bool?>( "IsPurchased", new bool?() );
            this.IsStartAllow = ( bool? ) storage.GetValue<bool?>( "IsStartAllow", new bool?() );
            this.IsFinishAllow = ( bool? ) storage.GetValue<bool?>( "IsFinishAllow", new bool?() );
            this.IsPayoutAllow = ( bool? ) storage.GetValue<bool?>( "IsPayoutAllow", new bool?() );
            this.IsMoneyCancelAllow = ( bool? ) storage.GetValue<bool?>( "IsMoneyCancelAllow", new bool?() );
            this.IsCrowd = ( bool? ) storage.GetValue<bool?>( "IsCrowd", new bool?() );
            this.CanEdit = ( bool? ) storage.GetValue<bool?>( "CanEdit", new bool?() );
            this.CanOpenAccount = ( bool? ) storage.GetValue<bool?>( "CanOpenAccount", new bool?() );
            this.IsFreelance = ( bool? ) storage.GetValue<bool?>( "IsFreelance", new bool?() );
            this.IsDevelopment = ( bool? ) storage.GetValue<bool?>( "IsDevelopment", new bool?() );
            this.IsBroker = ( bool? ) storage.GetValue<bool?>( "IsBroker", new bool?() );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.Frameworks = ( string ) storage.GetValue<string>( "Frameworks", null );
            this.Screenshot = ( File ) storage.GetValue<File>( "Screenshot", null );
            this.SourcesLink = ( string ) storage.GetValue<string>( "SourcesLink", null );
            this.DownloadsCount = ( int ) storage.GetValue<int>( "DownloadsCount", 0 );
            this.IsTrialAllow = ( bool? ) storage.GetValue<bool?>( "IsTrialAllow", new bool?() );
            this.ContentType2 = ( ProductContentTypes2 ) storage.GetValue<ProductContentTypes2>( "ContentType2", 0L );
            this.NugetInfo = ( ProductNugetInfo ) storage.GetValue<ProductNugetInfo>( "NugetInfo", null );
            this.AvailableVersion = ( string ) storage.GetValue<string>( "AvailableVersion", null );
            this.BlockedClients = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "BlockedClients", null );
            this.BugReports = ( BaseEntitySet<ProductBugReport> ) storage.GetValue<BaseEntitySet<ProductBugReport>>( "BugReports", null );
            this.Clients = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Clients", null );
            this.Domains = ( BaseEntitySet<ProductDomain> ) storage.GetValue<BaseEntitySet<ProductDomain>>( "Domains", null );
            this.Feedbacks = ( BaseEntitySet<ProductFeedback> ) storage.GetValue<BaseEntitySet<ProductFeedback>>( "Feedbacks", null );
            this.Groups = ( BaseEntitySet<ProductGroup> ) storage.GetValue<BaseEntitySet<ProductGroup>>( "Groups", null );
            this.Orders = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "Orders", null );
            this.OrdersActive = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "OrdersActive", null );
            this.OrdersReferral = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "OrdersReferral", null );
            this.OrdersRepeat = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "OrdersRepeat", null );
            this.OrdersTrial = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "OrdersTrial", null );
            this.OrdersRefund = ( BaseEntitySet<ProductOrder> ) storage.GetValue<BaseEntitySet<ProductOrder>>( "OrdersRefund", null );
            this.Payments = ( BaseEntitySet<Payment> ) storage.GetValue<BaseEntitySet<Payment>>( "Payments", null );
            this.Roles = ( BaseEntitySet<ProductRole> ) storage.GetValue<BaseEntitySet<ProductRole>>( "Roles", null );
            this.Sessions = ( BaseEntitySet<Session> ) storage.GetValue<BaseEntitySet<Session>>( "Sessions", null );
            this.Plugins = ( BaseEntitySet<ProductPlugin> ) storage.GetValue<BaseEntitySet<ProductPlugin>>( "Plugins", null );
            this.Permissions = ( BaseEntitySet<ProductPermission> ) storage.GetValue<BaseEntitySet<ProductPermission>>( "Permissions", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<FileGroup>( "Distributive", this.Distributive ).Set<Client>( "Client", this.Client ).Set<string>( "PackageId", this.PackageId ).Set<string>( "DocUrl", this.DocUrl ).Set<ProductContentTypes2>( "ContentType", this.ContentType ).Set<Product>( "SourcesProduct", this.SourcesProduct ).Set<int>( "SortOrder", this.SortOrder ).Set<int>( "SortOrderEx", this.SortOrderEx ).Set<ProductFlags>( "Flags", this.Flags ).Set<ProductOrder>( "Executor", this.Executor ).Set<ProductFreelanceStages>( "Stage", this.Stage ).Set<DateTime?>( "PurchasedTill", this.PurchasedTill ).Set<Decimal?>( "Rating", this.Rating ).Set<ProductScopes>( "Scope", this.Scope ).Set<PackageRepositories>( "Repository", this.Repository ).Set<bool?>( "IsTrialRequested", this.IsTrialRequested ).Set<bool?>( "IsRefundAllow", this.IsRefundAllow ).Set<bool?>( "IsRefundRequested", this.IsRefundRequested ).Set<bool?>( "IsPurchased", this.IsPurchased ).Set<bool?>( "IsStartAllow", this.IsStartAllow ).Set<bool?>( "IsFinishAllow", this.IsFinishAllow ).Set<bool?>( "IsPayoutAllow", this.IsPayoutAllow ).Set<bool?>( "IsMoneyCancelAllow", this.IsMoneyCancelAllow ).Set<bool?>( "IsCrowd", this.IsCrowd ).Set<bool?>( "CanEdit", this.CanEdit ).Set<bool?>( "CanOpenAccount", this.CanOpenAccount ).Set<bool?>( "IsFreelance", this.IsFreelance ).Set<bool?>( "IsDevelopment", this.IsDevelopment ).Set<bool?>( "IsBroker", this.IsBroker ).Set<string>( "UserId", this.UserId ).Set<string>( "Frameworks", this.Frameworks ).Set<File>( "Screenshot", this.Screenshot ).Set<string>( "SourcesLink", this.SourcesLink ).Set<int>( "DownloadsCount", this.DownloadsCount ).Set<bool?>( "IsTrialAllow", this.IsTrialAllow ).Set<ProductContentTypes2>( "ContentType2", this.ContentType2 ).Set<ProductNugetInfo>( "NugetInfo", this.NugetInfo ).Set<string>( "AvailableVersion", this.AvailableVersion ).Set<BaseEntitySet<Client>>( "BlockedClients", this.BlockedClients ).Set<BaseEntitySet<ProductBugReport>>( "BugReports", this.BugReports ).Set<BaseEntitySet<Client>>( "Clients", this.Clients ).Set<BaseEntitySet<ProductDomain>>( "Domains", this.Domains ).Set<BaseEntitySet<ProductFeedback>>( "Feedbacks", this.Feedbacks ).Set<BaseEntitySet<ProductGroup>>( "Groups", this.Groups ).Set<BaseEntitySet<ProductOrder>>( "Orders", this.Orders ).Set<BaseEntitySet<ProductOrder>>( "OrdersActive", this.OrdersActive ).Set<BaseEntitySet<ProductOrder>>( "OrdersReferral", this.OrdersReferral ).Set<BaseEntitySet<ProductOrder>>( "OrdersRepeat", this.OrdersRepeat ).Set<BaseEntitySet<ProductOrder>>( "OrdersTrial", this.OrdersTrial ).Set<BaseEntitySet<ProductOrder>>( "OrdersRefund", this.OrdersRefund ).Set<BaseEntitySet<Payment>>( "Payments", this.Payments ).Set<BaseEntitySet<ProductRole>>( "Roles", this.Roles ).Set<BaseEntitySet<Session>>( "Sessions", this.Sessions ).Set<BaseEntitySet<ProductPlugin>>( "Plugins", this.Plugins ).Set<BaseEntitySet<ProductPermission>>( "Permissions", this.Permissions );
        }
    }
}
