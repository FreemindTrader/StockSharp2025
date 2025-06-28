// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductGroupReferral
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductGroupReferral :
  BaseEntity,
  IDescriptionEntity,
  INameEntity,
  IDomainsEntity<ProductGroupReferralDomain>
{
    private string _name;

    public ProductGroup Group { get; set; }

    public Client Referral { get; set; }

    public string Description { get; set; }

    public Price Reward { get; set; }

    public BaseEntitySet<ProductGroupReferralDomain> Domains { get; set; }

    string INameEntity.Name
    {
        get => StringHelper.IsEmpty(this._name, ((INameEntity)this.Group)?.Name);
        set => this._name = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Group = storage.GetValue<ProductGroup>("Group", (ProductGroup)null);
        this.Referral = storage.GetValue<Client>("Referral", (Client)null);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.Reward = storage.GetValue<Price>("Reward", (Price)null);
        this.Domains = storage.GetValue<BaseEntitySet<ProductGroupReferralDomain>>("Domains", (BaseEntitySet<ProductGroupReferralDomain>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<ProductGroup>("Group", this.Group).Set<Client>("Referral", this.Referral).Set<string>("Description", this.Description).Set<Price>("Reward", this.Reward).Set<BaseEntitySet<ProductGroupReferralDomain>>("Domains", this.Domains);
    }
}
