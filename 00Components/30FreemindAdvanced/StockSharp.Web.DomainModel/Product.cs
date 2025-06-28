// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Product
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Product :
  ProductBase,
  IClientEntity,
  INameEntity,
  IDescriptionEntity,
  IDomainsEntity<ProductDomain>,
  IProductContentTypeEntity,
  IUserIdEntity
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

    [Obsolete("Use NugetInfo.Repository property.")]
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

    [Obsolete("Use Nuget.DownloadsCount.")]
    public int DownloadsCount { get; set; }

    [Obsolete("Use TrialPeriod instead.")]
    public bool? IsTrialAllow { get; set; }

    [Obsolete("Use ContentType instead.")]
    public ProductContentTypes2 ContentType2 { get; set; }

    public ProductNugetInfo NugetInfo { get; set; }

    public string AvailableVersion { get; set; }

    [Obsolete("Use Plugins instead.")]
    public ProductContentTypes2 SupportedPlugins
    {
        get
        {
            BaseEntitySet<ProductPlugin> plugins = this.Plugins;
            ProductContentTypes2? nullable;
            if (plugins == null)
            {
                nullable = new ProductContentTypes2?();
            }
            else
            {
                ProductPlugin[] items = plugins.Items;
                nullable = items != null ? new ProductContentTypes2?(Enumerator.JoinMask<ProductContentTypes2>(((IEnumerable<ProductPlugin>)items).Select<ProductPlugin, ProductContentTypes2>((Func<ProductPlugin, ProductContentTypes2>)(p => p.ContentType)))) : new ProductContentTypes2?();
            }
            return nullable.GetValueOrDefault();
        }
    }

    public BaseEntitySet<Client> BlockedClients { get; set; }

    public BaseEntitySet<StockSharp.Web.DomainModel.ProductBugReport> BugReports { get; set; }

    public BaseEntitySet<Client> Clients { get; set; }

    public BaseEntitySet<ProductDomain> Domains { get; set; }

    public BaseEntitySet<StockSharp.Web.DomainModel.ProductFeedback> Feedbacks { get; set; }

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
            if (domains == null)
            {
                str1 = (string)null;
            }
            else
            {
                ProductDomain[] items = domains.Items;
                str1 = items != null ? ((IEnumerable<ProductDomain>)items).FirstOrDefault<ProductDomain>()?.Name : (string)null;
            }
            string str2 = StringHelper.IsEmpty(this.PackageId, Converter.To<string>((object)this.Id));
            string str3 = StringHelper.IsEmpty(str1, str2);
            return StringHelper.IsEmpty(name, str3);
        }
        set => this._name = value;
    }

    string IDescriptionEntity.Description { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Distributive = storage.GetValue<FileGroup>("Distributive", (FileGroup)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.PackageId = storage.GetValue<string>("PackageId", (string)null);
        this.DocUrl = storage.GetValue<string>("DocUrl", (string)null);
        this.ContentType = storage.GetValue<ProductContentTypes2>("ContentType", (ProductContentTypes2)0);
        this.SourcesProduct = storage.GetValue<Product>("SourcesProduct", (Product)null);
        this.SortOrder = storage.GetValue<int>("SortOrder", 0);
        this.SortOrderEx = storage.GetValue<int>("SortOrderEx", 0);
        this.Flags = storage.GetValue<ProductFlags>("Flags", ProductFlags.None);
        this.Executor = storage.GetValue<ProductOrder>("Executor", (ProductOrder)null);
        this.Stage = storage.GetValue<ProductFreelanceStages>("Stage", ProductFreelanceStages.None);
        this.PurchasedTill = storage.GetValue<DateTime?>("PurchasedTill", new DateTime?());
        this.Rating = storage.GetValue<Decimal?>("Rating", new Decimal?());
        this.Scope = storage.GetValue<ProductScopes>("Scope", ProductScopes.Public);
        this.Repository = storage.GetValue<PackageRepositories>("Repository", PackageRepositories.NuGet);
        this.IsTrialRequested = storage.GetValue<bool?>("IsTrialRequested", new bool?());
        this.IsRefundAllow = storage.GetValue<bool?>("IsRefundAllow", new bool?());
        this.IsRefundRequested = storage.GetValue<bool?>("IsRefundRequested", new bool?());
        this.IsPurchased = storage.GetValue<bool?>("IsPurchased", new bool?());
        this.IsStartAllow = storage.GetValue<bool?>("IsStartAllow", new bool?());
        this.IsFinishAllow = storage.GetValue<bool?>("IsFinishAllow", new bool?());
        this.IsPayoutAllow = storage.GetValue<bool?>("IsPayoutAllow", new bool?());
        this.IsMoneyCancelAllow = storage.GetValue<bool?>("IsMoneyCancelAllow", new bool?());
        this.IsCrowd = storage.GetValue<bool?>("IsCrowd", new bool?());
        this.CanEdit = storage.GetValue<bool?>("CanEdit", new bool?());
        this.CanOpenAccount = storage.GetValue<bool?>("CanOpenAccount", new bool?());
        this.IsFreelance = storage.GetValue<bool?>("IsFreelance", new bool?());
        this.IsDevelopment = storage.GetValue<bool?>("IsDevelopment", new bool?());
        this.IsBroker = storage.GetValue<bool?>("IsBroker", new bool?());
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.Frameworks = storage.GetValue<string>("Frameworks", (string)null);
        this.Screenshot = storage.GetValue<File>("Screenshot", (File)null);
        this.SourcesLink = storage.GetValue<string>("SourcesLink", (string)null);
        this.DownloadsCount = storage.GetValue<int>("DownloadsCount", 0);
        this.IsTrialAllow = storage.GetValue<bool?>("IsTrialAllow", new bool?());
        this.ContentType2 = storage.GetValue<ProductContentTypes2>("ContentType2", (ProductContentTypes2)0);
        this.NugetInfo = storage.GetValue<ProductNugetInfo>("NugetInfo", (ProductNugetInfo)null);
        this.AvailableVersion = storage.GetValue<string>("AvailableVersion", (string)null);
        this.BlockedClients = storage.GetValue<BaseEntitySet<Client>>("BlockedClients", (BaseEntitySet<Client>)null);
        this.BugReports = storage.GetValue<BaseEntitySet<StockSharp.Web.DomainModel.ProductBugReport>>("BugReports", (BaseEntitySet<StockSharp.Web.DomainModel.ProductBugReport>)null);
        this.Clients = storage.GetValue<BaseEntitySet<Client>>("Clients", (BaseEntitySet<Client>)null);
        this.Domains = storage.GetValue<BaseEntitySet<ProductDomain>>("Domains", (BaseEntitySet<ProductDomain>)null);
        this.Feedbacks = storage.GetValue<BaseEntitySet<StockSharp.Web.DomainModel.ProductFeedback>>("Feedbacks", (BaseEntitySet<StockSharp.Web.DomainModel.ProductFeedback>)null);
        this.Groups = storage.GetValue<BaseEntitySet<ProductGroup>>("Groups", (BaseEntitySet<ProductGroup>)null);
        this.Orders = storage.GetValue<BaseEntitySet<ProductOrder>>("Orders", (BaseEntitySet<ProductOrder>)null);
        this.OrdersActive = storage.GetValue<BaseEntitySet<ProductOrder>>("OrdersActive", (BaseEntitySet<ProductOrder>)null);
        this.OrdersReferral = storage.GetValue<BaseEntitySet<ProductOrder>>("OrdersReferral", (BaseEntitySet<ProductOrder>)null);
        this.OrdersRepeat = storage.GetValue<BaseEntitySet<ProductOrder>>("OrdersRepeat", (BaseEntitySet<ProductOrder>)null);
        this.OrdersTrial = storage.GetValue<BaseEntitySet<ProductOrder>>("OrdersTrial", (BaseEntitySet<ProductOrder>)null);
        this.OrdersRefund = storage.GetValue<BaseEntitySet<ProductOrder>>("OrdersRefund", (BaseEntitySet<ProductOrder>)null);
        this.Payments = storage.GetValue<BaseEntitySet<Payment>>("Payments", (BaseEntitySet<Payment>)null);
        this.Roles = storage.GetValue<BaseEntitySet<ProductRole>>("Roles", (BaseEntitySet<ProductRole>)null);
        this.Sessions = storage.GetValue<BaseEntitySet<Session>>("Sessions", (BaseEntitySet<Session>)null);
        this.Plugins = storage.GetValue<BaseEntitySet<ProductPlugin>>("Plugins", (BaseEntitySet<ProductPlugin>)null);
        this.Permissions = storage.GetValue<BaseEntitySet<ProductPermission>>("Permissions", (BaseEntitySet<ProductPermission>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<FileGroup>("Distributive", this.Distributive).Set<Client>("Client", this.Client).Set<string>("PackageId", this.PackageId).Set<string>("DocUrl", this.DocUrl).Set<ProductContentTypes2>("ContentType", this.ContentType).Set<Product>("SourcesProduct", this.SourcesProduct).Set<int>("SortOrder", this.SortOrder).Set<int>("SortOrderEx", this.SortOrderEx).Set<ProductFlags>("Flags", this.Flags).Set<ProductOrder>("Executor", this.Executor).Set<ProductFreelanceStages>("Stage", this.Stage).Set<DateTime?>("PurchasedTill", this.PurchasedTill).Set<Decimal?>("Rating", this.Rating).Set<ProductScopes>("Scope", this.Scope).Set<PackageRepositories>("Repository", this.Repository).Set<bool?>("IsTrialRequested", this.IsTrialRequested).Set<bool?>("IsRefundAllow", this.IsRefundAllow).Set<bool?>("IsRefundRequested", this.IsRefundRequested).Set<bool?>("IsPurchased", this.IsPurchased).Set<bool?>("IsStartAllow", this.IsStartAllow).Set<bool?>("IsFinishAllow", this.IsFinishAllow).Set<bool?>("IsPayoutAllow", this.IsPayoutAllow).Set<bool?>("IsMoneyCancelAllow", this.IsMoneyCancelAllow).Set<bool?>("IsCrowd", this.IsCrowd).Set<bool?>("CanEdit", this.CanEdit).Set<bool?>("CanOpenAccount", this.CanOpenAccount).Set<bool?>("IsFreelance", this.IsFreelance).Set<bool?>("IsDevelopment", this.IsDevelopment).Set<bool?>("IsBroker", this.IsBroker).Set<string>("UserId", this.UserId).Set<string>("Frameworks", this.Frameworks).Set<File>("Screenshot", this.Screenshot).Set<string>("SourcesLink", this.SourcesLink).Set<int>("DownloadsCount", this.DownloadsCount).Set<bool?>("IsTrialAllow", this.IsTrialAllow).Set<ProductContentTypes2>("ContentType2", this.ContentType2).Set<ProductNugetInfo>("NugetInfo", this.NugetInfo).Set<string>("AvailableVersion", this.AvailableVersion).Set<BaseEntitySet<Client>>("BlockedClients", this.BlockedClients).Set<BaseEntitySet<StockSharp.Web.DomainModel.ProductBugReport>>("BugReports", this.BugReports).Set<BaseEntitySet<Client>>("Clients", this.Clients).Set<BaseEntitySet<ProductDomain>>("Domains", this.Domains).Set<BaseEntitySet<StockSharp.Web.DomainModel.ProductFeedback>>("Feedbacks", this.Feedbacks).Set<BaseEntitySet<ProductGroup>>("Groups", this.Groups).Set<BaseEntitySet<ProductOrder>>("Orders", this.Orders).Set<BaseEntitySet<ProductOrder>>("OrdersActive", this.OrdersActive).Set<BaseEntitySet<ProductOrder>>("OrdersReferral", this.OrdersReferral).Set<BaseEntitySet<ProductOrder>>("OrdersRepeat", this.OrdersRepeat).Set<BaseEntitySet<ProductOrder>>("OrdersTrial", this.OrdersTrial).Set<BaseEntitySet<ProductOrder>>("OrdersRefund", this.OrdersRefund).Set<BaseEntitySet<Payment>>("Payments", this.Payments).Set<BaseEntitySet<ProductRole>>("Roles", this.Roles).Set<BaseEntitySet<Session>>("Sessions", this.Sessions).Set<BaseEntitySet<ProductPlugin>>("Plugins", this.Plugins).Set<BaseEntitySet<ProductPermission>>("Permissions", this.Permissions);
    }
}
