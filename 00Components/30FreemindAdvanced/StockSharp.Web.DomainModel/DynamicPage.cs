// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicPage
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.Collections.Generic;
using System.Linq;
using System.Net;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DynamicPage :
  BaseEntity,
  IDomainsEntity<DynamicPageDomain>,
  INameEntity,
  IDescriptionEntity
{
    private string _name;
    private string _description;

    public DynamicPage Parent { get; set; }

    public bool IsEnabled { get; set; }

    public DynamicMenuGroup MenuGroup { get; set; }

    public bool IsSitemap { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public string StatusDescription { get; set; }

    public DynamicPageHandlers Handler { get; set; }

    public bool UseQueryString { get; set; }

    public BaseEntitySet<DynamicPage> Childs { get; set; }

    public BaseEntitySet<DynamicPageDomain> Domains { get; set; }

    string INameEntity.Name
    {
        get
        {
            string name = this._name;
            BaseEntitySet<DynamicPageDomain> domains = this.Domains;
            string str1;
            if (domains == null)
            {
                str1 = (string)null;
            }
            else
            {
                DynamicPageDomain[] items = domains.Items;
                str1 = items != null ? ((IEnumerable<DynamicPageDomain>)items).FirstOrDefault<DynamicPageDomain>()?.UrlPart : (string)null;
            }
            string str2 = Converter.To<string>((object)this.Id);
            string str3 = StringHelper.IsEmpty(str1, str2);
            return StringHelper.IsEmpty(name, str3);
        }
        set => this._name = value;
    }

    string IDescriptionEntity.Description
    {
        get
        {
            string description = this._description;
            BaseEntitySet<DynamicPageDomain> domains = this.Domains;
            string str;
            if (domains == null)
            {
                str = (string)null;
            }
            else
            {
                DynamicPageDomain[] items = domains.Items;
                str = items != null ? ((IEnumerable<DynamicPageDomain>)items).FirstOrDefault<DynamicPageDomain>()?.Topic?.Name : (string)null;
            }
            return StringHelper.IsEmpty(description, str);
        }
        set => this._description = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Parent = storage.GetValue<DynamicPage>("Parent", (DynamicPage)null);
        this.IsEnabled = storage.GetValue<bool>("IsEnabled", false);
        this.MenuGroup = storage.GetValue<DynamicMenuGroup>("MenuGroup", (DynamicMenuGroup)null);
        this.IsSitemap = storage.GetValue<bool>("IsSitemap", false);
        this.StatusCode = storage.GetValue<HttpStatusCode>("StatusCode", (HttpStatusCode)0);
        this.StatusDescription = storage.GetValue<string>("StatusDescription", (string)null);
        this.Handler = storage.GetValue<DynamicPageHandlers>("Handler", DynamicPageHandlers.Start);
        this.UseQueryString = storage.GetValue<bool>("UseQueryString", false);
        this.Childs = storage.GetValue<BaseEntitySet<DynamicPage>>("Childs", (BaseEntitySet<DynamicPage>)null);
        this.Domains = storage.GetValue<BaseEntitySet<DynamicPageDomain>>("Domains", (BaseEntitySet<DynamicPageDomain>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<DynamicPage>("Parent", this.Parent).Set<bool>("IsEnabled", this.IsEnabled).Set<DynamicMenuGroup>("MenuGroup", this.MenuGroup).Set<bool>("IsSitemap", this.IsSitemap).Set<HttpStatusCode>("StatusCode", this.StatusCode).Set<string>("StatusDescription", this.StatusDescription).Set<DynamicPageHandlers>("Handler", this.Handler).Set<bool>("UseQueryString", this.UseQueryString).Set<BaseEntitySet<DynamicPage>>("Childs", this.Childs).Set<BaseEntitySet<DynamicPageDomain>>("Domains", this.Domains);
    }
}
