// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientBalanceHistory
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ClientBalanceHistory : BaseEntity
{
    public Payment Payment { get; set; }

    public ClientBalance Balance { get; set; }

    public Decimal Diff { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Payment = storage.GetValue<Payment>("Payment", (Payment)null);
        this.Balance = storage.GetValue<ClientBalance>("Balance", (ClientBalance)null);
        this.Diff = storage.GetValue<Decimal>("Diff", 0M);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Payment>("Payment", this.Payment).Set<ClientBalance>("Balance", this.Balance).Set<Decimal>("Diff", this.Diff);
    }
}
