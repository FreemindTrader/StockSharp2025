// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyStatParameter
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyStatParameter : IPersistable
{
    public StatisticParameterTypes Type { get; set; }

    public object Value { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Type = storage.GetValue<StatisticParameterTypes>("Type", (StatisticParameterTypes)0);
        this.Value = storage.GetValue<object>("Value", (object)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<StatisticParameterTypes>("Type", this.Type).Set<object>("Value", this.Value);
    }
}
