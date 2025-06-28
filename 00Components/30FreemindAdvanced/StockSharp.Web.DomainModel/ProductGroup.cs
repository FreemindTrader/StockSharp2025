// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductGroup
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductGroup :
  ProductBase,
  INameEntity,
  IDescriptionEntity,
  IDomainsEntity<ProductGroupDomain>
{
    private string _name;

    public string Description { get; set; }

    public BaseEntitySet<ProductGroupDomain> Domains { get; set; }

    public BaseEntitySet<ProductGroup> Child { get; set; }

    public BaseEntitySet<ProductGroup> Parent { get; set; }

    public BaseEntitySet<Product> Products { get; set; }

    public BaseEntitySet<ProductGroupReferral> Referral { get; set; }

    string INameEntity.Name
    {
        get
        {
            string name = this._name;
            BaseEntitySet<ProductGroupDomain> domains = this.Domains;
            string str;
            if (domains == null)
            {
                str = (string)null;
            }
            else
            {
                ProductGroupDomain[] items = domains.Items;
                str = items != null ? ((IEnumerable<ProductGroupDomain>)items).FirstOrDefault<ProductGroupDomain>()?.Name : (string)null;
            }
            return StringHelper.IsEmpty(name, str);
        }
        set => this._name = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.Domains = storage.GetValue<BaseEntitySet<ProductGroupDomain>>("Domains", (BaseEntitySet<ProductGroupDomain>)null);
        this.Child = storage.GetValue<BaseEntitySet<ProductGroup>>("Child", (BaseEntitySet<ProductGroup>)null);
        this.Parent = storage.GetValue<BaseEntitySet<ProductGroup>>("Parent", (BaseEntitySet<ProductGroup>)null);
        this.Products = storage.GetValue<BaseEntitySet<Product>>("Products", (BaseEntitySet<Product>)null);
        this.Referral = storage.GetValue<BaseEntitySet<ProductGroupReferral>>("Referral", (BaseEntitySet<ProductGroupReferral>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Description", this.Description).Set<BaseEntitySet<ProductGroupDomain>>("Domains", this.Domains).Set<BaseEntitySet<ProductGroup>>("Child", this.Child).Set<BaseEntitySet<ProductGroup>>("Parent", this.Parent).Set<BaseEntitySet<Product>>("Products", this.Products).Set<BaseEntitySet<ProductGroupReferral>>("Referral", this.Referral);
    }
}
