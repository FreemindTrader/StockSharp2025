// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyCommandData
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyCommandData : IPersistable
{
    public Strategy Strategy { get; set; }

    public CommandInfo Command { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Strategy = storage.GetValue<Strategy>("Strategy", (Strategy)null);
        this.Command = storage.GetValue<CommandInfo>("Command", (CommandInfo)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<Strategy>("Strategy", this.Strategy).Set<CommandInfo>("Command", this.Command);
    }
}
