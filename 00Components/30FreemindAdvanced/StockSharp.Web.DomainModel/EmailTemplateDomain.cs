// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.EmailTemplateDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class EmailTemplateDomain : BaseEntity, IDomainEntity
{
    public EmailTemplate Template { get; set; }

    public Domain Domain { get; set; }

    public File Html { get; set; }

    public Topic Text { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Template = storage.GetValue<EmailTemplate>("Template", (EmailTemplate)null);
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.Html = storage.GetValue<File>("Html", (File)null);
        this.Text = storage.GetValue<Topic>("Text", (Topic)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<EmailTemplate>("Template", this.Template).Set<Domain>("Domain", this.Domain).Set<File>("Html", this.Html).Set<Topic>("Text", this.Text);
    }
}
