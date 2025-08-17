// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.EmailBulk
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class EmailBulk : BaseEntity, IDomainEntity, INameEntity
{
    public string FromAlias { get; set; }

    public string FromEmail { get; set; }

    public Domain Domain { get; set; }

    public string Name { get; set; }

    public File Html { get; set; }

    public File Text { get; set; }

    public bool IsShortLink { get; set; }

    public EmailPreferences EmailPreferences { get; set; }

    public string[] ParamKeys { get; set; }

    public string[] ParamValues { get; set; }

    public BaseEntitySet<Client> Included { get; set; }

    public BaseEntitySet<Client> Excluded { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.FromAlias = storage.GetValue<string>("FromAlias", (string)null);
        this.FromEmail = storage.GetValue<string>("FromEmail", (string)null);
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Html = storage.GetValue<File>("Html", (File)null);
        this.Text = storage.GetValue<File>("Text", (File)null);
        this.IsShortLink = storage.GetValue<bool>("IsShortLink", false);
        this.EmailPreferences = storage.GetValue<EmailPreferences>("EmailPreferences", (EmailPreferences)0);
        this.ParamKeys = storage.GetValue<string[]>("ParamKeys", (string[])null);
        this.ParamValues = storage.GetValue<string[]>("ParamValues", (string[])null);
        this.Included = storage.GetValue<BaseEntitySet<Client>>("Included", (BaseEntitySet<Client>)null);
        this.Excluded = storage.GetValue<BaseEntitySet<Client>>("Excluded", (BaseEntitySet<Client>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("FromAlias", this.FromAlias).Set<string>("FromEmail", this.FromEmail).Set<Domain>("Domain", this.Domain).Set<string>("Name", this.Name).Set<File>("Html", this.Html).Set<File>("Text", this.Text).Set<bool>("IsShortLink", this.IsShortLink).Set<EmailPreferences>("EmailPreferences", this.EmailPreferences).Set<string[]>("ParamKeys", this.ParamKeys).Set<string[]>("ParamValues", this.ParamValues).Set<BaseEntitySet<Client>>("Included", this.Included).Set<BaseEntitySet<Client>>("Excluded", this.Excluded);
    }
}
