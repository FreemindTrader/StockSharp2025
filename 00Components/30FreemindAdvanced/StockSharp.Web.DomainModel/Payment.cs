// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Payment
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Payment : BaseEntity, INameEntity, IDescriptionEntity, IStateEntity<PaymentStates>
{
    public string Description { get; set; }

    public Decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public Client From { get; set; }

    public Client To { get; set; }

    public Domain Domain { get; set; }

    public string PaySystemId { get; set; }

    public PaymentStates State { get; set; }

    public CurrencyTypes Currency { get; set; }

    public AccountRequisites BankAccount { get; set; }

    public ProductOrder ProductOrder { get; set; }

    public bool IsTest { get; set; }

    public PayGatewayDomain GatewayDomain { get; set; }

    public bool IsRecurrent { get; set; }

    public bool IsConfirmRequired { get; set; }

    public string StatusUrlPart { get; set; }

    public bool? IsApproveAllow { get; set; }

    public bool? IsCancelAllow { get; set; }

    public bool? IsAutoActionsAllow { get; set; }

    public FileGroup Files { get; set; }

    public BaseEntitySet<Error> Errors { get; set; }

    string INameEntity.Name { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.Amount = storage.GetValue<Decimal>("Amount", 0M);
        this.Date = storage.GetValue<DateTime>("Date", new DateTime());
        this.From = storage.GetValue<Client>("From", (Client)null);
        this.To = storage.GetValue<Client>("To", (Client)null);
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.PaySystemId = storage.GetValue<string>("PaySystemId", (string)null);
        this.State = storage.GetValue<PaymentStates>("State", PaymentStates.Done);
        this.Currency = storage.GetValue<CurrencyTypes>("Currency", (CurrencyTypes)0);
        this.BankAccount = storage.GetValue<AccountRequisites>("BankAccount", (AccountRequisites)null);
        this.ProductOrder = storage.GetValue<ProductOrder>("ProductOrder", (ProductOrder)null);
        this.IsTest = storage.GetValue<bool>("IsTest", false);
        this.GatewayDomain = storage.GetValue<PayGatewayDomain>("GatewayDomain", (PayGatewayDomain)null);
        this.IsRecurrent = storage.GetValue<bool>("IsRecurrent", false);
        this.IsConfirmRequired = storage.GetValue<bool>("IsConfirmRequired", false);
        this.StatusUrlPart = storage.GetValue<string>("StatusUrlPart", (string)null);
        this.IsApproveAllow = storage.GetValue<bool?>("IsApproveAllow", new bool?());
        this.IsCancelAllow = storage.GetValue<bool?>("IsCancelAllow", new bool?());
        this.IsAutoActionsAllow = storage.GetValue<bool?>("IsAutoActionsAllow", new bool?());
        this.Files = storage.GetValue<FileGroup>("Files", (FileGroup)null);
        this.Errors = storage.GetValue<BaseEntitySet<Error>>("Errors", (BaseEntitySet<Error>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Description", this.Description).Set<Decimal>("Amount", this.Amount).Set<DateTime>("Date", this.Date).Set<Client>("From", this.From).Set<Client>("To", this.To).Set<Domain>("Domain", this.Domain).Set<string>("PaySystemId", this.PaySystemId).Set<PaymentStates>("State", this.State).Set<CurrencyTypes>("Currency", this.Currency).Set<AccountRequisites>("BankAccount", this.BankAccount).Set<ProductOrder>("ProductOrder", this.ProductOrder).Set<bool>("IsTest", this.IsTest).Set<PayGatewayDomain>("GatewayDomain", this.GatewayDomain).Set<bool>("IsRecurrent", this.IsRecurrent).Set<bool>("IsConfirmRequired", this.IsConfirmRequired).Set<string>("StatusUrlPart", this.StatusUrlPart).Set<bool?>("IsApproveAllow", this.IsApproveAllow).Set<bool?>("IsCancelAllow", this.IsCancelAllow).Set<bool?>("IsAutoActionsAllow", this.IsAutoActionsAllow).Set<FileGroup>("Files", this.Files).Set<BaseEntitySet<Error>>("Errors", this.Errors);
    }
}
