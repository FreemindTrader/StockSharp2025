// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientBalance
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ClientBalance : BaseEntity, IClientEntity
{
    public Client Client { get; set; }

    public CurrencyTypes Currency { get; set; }

    public Decimal Amount { get; set; }

    public BaseEntitySet<ClientBalanceHistory> History { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Currency = storage.GetValue<CurrencyTypes>("Currency", (CurrencyTypes)0);
        this.Amount = storage.GetValue<Decimal>("Amount", 0M);
        this.History = storage.GetValue<BaseEntitySet<ClientBalanceHistory>>("History", (BaseEntitySet<ClientBalanceHistory>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<CurrencyTypes>("Currency", this.Currency).Set<Decimal>("Amount", this.Amount).Set<BaseEntitySet<ClientBalanceHistory>>("History", this.History);
    }
}
