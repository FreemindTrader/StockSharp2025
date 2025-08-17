// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyPositionChange
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyPositionChange : BaseEntity
{
    public StrategyPosition Position { get; set; }

    public Decimal Value { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Position = storage.GetValue<StrategyPosition>("Position", (StrategyPosition)null);
        this.Value = storage.GetValue<Decimal>("Value", 0M);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<StrategyPosition>("Position", this.Position).Set<Decimal>("Value", this.Value);
    }
}
