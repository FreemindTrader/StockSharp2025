// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.AccountRequisites
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class AccountRequisites : BaseEntity, IClientEntity, INameEntity, IDescriptionEntity
{
    public string BankId { get; set; }

    public string BankName { get; set; }

    public string AccountHolder { get; set; }

    public string AccountNo { get; set; }

    public Client Client { get; set; }

    public BaseEntitySet<Payment> Payments { get; set; }

    string INameEntity.Name
    {
        get => this.AccountNo;
        set => this.AccountNo = value;
    }

    string IDescriptionEntity.Description { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.BankId = storage.GetValue<string>("BankId", (string)null);
        this.BankName = storage.GetValue<string>("BankName", (string)null);
        this.AccountHolder = storage.GetValue<string>("AccountHolder", (string)null);
        this.AccountNo = storage.GetValue<string>("AccountNo", (string)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Payments = storage.GetValue<BaseEntitySet<Payment>>("Payments", (BaseEntitySet<Payment>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("BankId", this.BankId).Set<string>("BankName", this.BankName).Set<string>("AccountHolder", this.AccountHolder).Set<string>("AccountNo", this.AccountNo).Set<Client>("Client", this.Client).Set<BaseEntitySet<Payment>>("Payments", this.Payments);
    }
}
