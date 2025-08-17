// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyPnL
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyPnL : IPersistable
{
    public Decimal Realized { get; set; }

    public Decimal? Unrealized { get; set; }

    public Decimal? Commission { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Realized = storage.GetValue<Decimal>("Realized", 0M);
        this.Unrealized = storage.GetValue<Decimal?>("Unrealized", new Decimal?());
        this.Commission = storage.GetValue<Decimal?>("Commission", new Decimal?());
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<Decimal>("Realized", this.Realized).Set<Decimal?>("Unrealized", this.Unrealized).Set<Decimal?>("Commission", this.Commission);
    }
}
