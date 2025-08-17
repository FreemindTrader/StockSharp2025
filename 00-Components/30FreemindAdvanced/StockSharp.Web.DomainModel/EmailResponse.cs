// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.EmailResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class EmailResponse : BaseResponse
{
    public EmailResponse()
      : base(SubscriptionTypes.Email)
    {
    }

    public bool IsCancelled { get; set; }

    public EmailBulk Bulk { get; set; }

    public Client[] Clients { get; set; }

    public string Html { get; set; }

    public string Text { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.IsCancelled = storage.GetValue<bool>("IsCancelled", false);
        this.Bulk = storage.GetValue<EmailBulk>("Bulk", (EmailBulk)null);
        this.Clients = storage.GetValue<Client[]>("Clients", (Client[])null);
        this.Html = storage.GetValue<string>("Html", (string)null);
        this.Text = storage.GetValue<string>("Text", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<bool>("IsCancelled", this.IsCancelled).Set<EmailBulk>("Bulk", this.Bulk).Set<Client[]>("Clients", this.Clients).Set<string>("Html", this.Html).Set<string>("Text", this.Text);
    }
}
