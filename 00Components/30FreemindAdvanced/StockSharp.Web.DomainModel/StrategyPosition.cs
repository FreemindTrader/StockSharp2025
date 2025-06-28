// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyPosition
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyPosition : BaseEntity, IInstrumentEntity, IStrategyEntity
{
    public Strategy Strategy { get; set; }

    public InstrumentInfo Security { get; set; }

    public StrategyAccount Account { get; set; }

    public StrategyPositionChange[] Changes { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Strategy = storage.GetValue<Strategy>("Strategy", (Strategy)null);
        this.Security = storage.GetValue<InstrumentInfo>("Security", (InstrumentInfo)null);
        this.Account = storage.GetValue<StrategyAccount>("Account", (StrategyAccount)null);
        this.Changes = storage.GetValue<StrategyPositionChange[]>("Changes", (StrategyPositionChange[])null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Strategy>("Strategy", this.Strategy).Set<InstrumentInfo>("Security", this.Security).Set<StrategyAccount>("Account", this.Account).Set<StrategyPositionChange[]>("Changes", this.Changes);
    }
}
