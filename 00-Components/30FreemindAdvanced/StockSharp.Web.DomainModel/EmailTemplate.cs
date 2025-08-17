// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.EmailTemplate
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class EmailTemplate : BaseEntity, INameEntity, IDomainsEntity<EmailTemplateDomain>
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string Alias { get; set; }

    public bool IsEnabled { get; set; }

    public BaseEntitySet<EmailTemplateDomain> Domains { get; set; }

    public BaseEntitySet<ProductGroup> ProductGroups { get; set; }

    public BaseEntitySet<Product> Products { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Address = storage.GetValue<string>("Address", (string)null);
        this.Alias = storage.GetValue<string>("Alias", (string)null);
        this.IsEnabled = storage.GetValue<bool>("IsEnabled", false);
        this.Domains = storage.GetValue<BaseEntitySet<EmailTemplateDomain>>("Domains", (BaseEntitySet<EmailTemplateDomain>)null);
        this.ProductGroups = storage.GetValue<BaseEntitySet<ProductGroup>>("ProductGroups", (BaseEntitySet<ProductGroup>)null);
        this.Products = storage.GetValue<BaseEntitySet<Product>>("Products", (BaseEntitySet<Product>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<string>("Address", this.Address).Set<string>("Alias", this.Alias).Set<bool>("IsEnabled", this.IsEnabled).Set<BaseEntitySet<EmailTemplateDomain>>("Domains", this.Domains).Set<BaseEntitySet<ProductGroup>>("ProductGroups", this.ProductGroups).Set<BaseEntitySet<Product>>("Products", this.Products);
    }
}
