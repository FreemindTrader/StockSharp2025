// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DynamicPageDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DynamicPageDomain : BaseEntity, ITopicEntity, IDomainEntity
{
    public DynamicPage Page { get; set; }

    public Domain Domain { get; set; }

    public Topic Topic { get; set; }

    public string UrlPart { get; set; }

    public string UrlRelative { get; set; }

    public string RedirectUrl { get; set; }

    public File File { get; set; }

    public string ActionTitle { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Page = storage.GetValue<DynamicPage>("Page", (DynamicPage)null);
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.Topic = storage.GetValue<Topic>("Topic", (Topic)null);
        this.UrlPart = storage.GetValue<string>("UrlPart", (string)null);
        this.UrlRelative = storage.GetValue<string>("UrlRelative", (string)null);
        this.RedirectUrl = storage.GetValue<string>("RedirectUrl", (string)null);
        this.File = storage.GetValue<File>("File", (File)null);
        this.ActionTitle = storage.GetValue<string>("ActionTitle", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<DynamicPage>("Page", this.Page).Set<Domain>("Domain", this.Domain).Set<Topic>("Topic", this.Topic).Set<string>("UrlPart", this.UrlPart).Set<string>("UrlRelative", this.UrlRelative).Set<string>("RedirectUrl", this.RedirectUrl).Set<File>("File", this.File).Set<string>("ActionTitle", this.ActionTitle);
    }
}
