// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicMenu
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DynamicMenu : BaseEntity, IProductEntity, INameEntity, IDomainsEntity<DynamicMenuDomain>
{
    private string _name;

    public bool IsBlank { get; set; }

    public DynamicMenuGroup Group { get; set; }

    public int SortOrder { get; set; }

    public string Style { get; set; }

    public DynamicPage Page { get; set; }

    public Product Product { get; set; }

    public BaseEntitySet<DynamicMenuDomain> Domains { get; set; }

    public BaseEntitySet<Client> Roles { get; set; }

    string INameEntity.Name
    {
        get
        {
            string name = this._name;
            BaseEntitySet<DynamicMenuDomain> domains1 = this.Domains;
            string str1;
            if (domains1 == null)
            {
                str1 = (string)null;
            }
            else
            {
                DynamicMenuDomain[] items = domains1.Items;
                str1 = items != null ? ((IEnumerable<DynamicMenuDomain>)items).FirstOrDefault<DynamicMenuDomain>()?.UrlAbsolute : (string)null;
            }
            string str2 = StringHelper.IsEmpty(name, str1);
            BaseEntitySet<DynamicMenuDomain> domains2 = this.Domains;
            string str3;
            if (domains2 == null)
            {
                str3 = (string)null;
            }
            else
            {
                DynamicMenuDomain[] items = domains2.Items;
                str3 = items != null ? ((IEnumerable<DynamicMenuDomain>)items).FirstOrDefault<DynamicMenuDomain>()?.UrlRelative : (string)null;
            }
            return StringHelper.IsEmpty(StringHelper.IsEmpty(str2, str3), Converter.To<string>((object)this.Id));
        }
        set => this._name = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.IsBlank = storage.GetValue<bool>("IsBlank", false);
        this.Group = storage.GetValue<DynamicMenuGroup>("Group", (DynamicMenuGroup)null);
        this.SortOrder = storage.GetValue<int>("SortOrder", 0);
        this.Style = storage.GetValue<string>("Style", (string)null);
        this.Page = storage.GetValue<DynamicPage>("Page", (DynamicPage)null);
        this.Product = storage.GetValue<Product>("Product", (Product)null);
        this.Domains = storage.GetValue<BaseEntitySet<DynamicMenuDomain>>("Domains", (BaseEntitySet<DynamicMenuDomain>)null);
        this.Roles = storage.GetValue<BaseEntitySet<Client>>("Roles", (BaseEntitySet<Client>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<bool>("IsBlank", this.IsBlank).Set<DynamicMenuGroup>("Group", this.Group).Set<int>("SortOrder", this.SortOrder).Set<string>("Style", this.Style).Set<DynamicPage>("Page", this.Page).Set<Product>("Product", this.Product).Set<BaseEntitySet<DynamicMenuDomain>>("Domains", this.Domains).Set<BaseEntitySet<Client>>("Roles", this.Roles);
    }
}
