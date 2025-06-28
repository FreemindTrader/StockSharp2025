// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyIndicatorValue
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyIndicatorValue : IPersistable
{
    public int Index { get; set; }

    public object[] Values { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Index = storage.GetValue<int>("Index", 0);
        this.Values = storage.GetValue<object[]>("Values", (object[])null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<int>("Index", this.Index).Set<object[]>("Values", this.Values);
    }
}
