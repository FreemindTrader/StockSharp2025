// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Error
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Error : BaseEntity, IClientEntity, INameEntity, IDescriptionEntity
{
    public Client Client { get; set; }

    public Payment Payment { get; set; }

    public string Text { get; set; }

    public string Url { get; set; }

    public string Referer { get; set; }

    public int Priority { get; set; }

    public BaseEntitySet<ProductBugReport> ProductBugReports { get; set; }

    string INameEntity.Name
    {
        get => this.Text;
        set => this.Text = value;
    }

    string IDescriptionEntity.Description
    {
        get => this.Text;
        set => this.Text = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Payment = storage.GetValue<Payment>("Payment", (Payment)null);
        this.Text = storage.GetValue<string>("Text", (string)null);
        this.Url = storage.GetValue<string>("Url", (string)null);
        this.Referer = storage.GetValue<string>("Referer", (string)null);
        this.Priority = storage.GetValue<int>("Priority", 0);
        this.ProductBugReports = storage.GetValue<BaseEntitySet<ProductBugReport>>("ProductBugReports", (BaseEntitySet<ProductBugReport>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<Payment>("Payment", this.Payment).Set<string>("Text", this.Text).Set<string>("Url", this.Url).Set<string>("Referer", this.Referer).Set<int>("Priority", this.Priority).Set<BaseEntitySet<ProductBugReport>>("ProductBugReports", this.ProductBugReports);
    }
}
