// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyIndicator
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyIndicator : IUserIdEntity, INameEntity, IPersistable
{
    public string Name { get; set; }

    public string IndicatorName { get; set; }

    public string TypeName { get; set; }

    public string UserId { get; set; }

    public StrategyIndicatorParam[] Params { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.IndicatorName = storage.GetValue<string>("IndicatorName", (string)null);
        this.TypeName = storage.GetValue<string>("TypeName", (string)null);
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.Params = storage.GetValue<StrategyIndicatorParam[]>("Params", (StrategyIndicatorParam[])null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<string>("Name", this.Name).Set<string>("IndicatorName", this.IndicatorName).Set<string>("TypeName", this.TypeName).Set<string>("UserId", this.UserId).Set<StrategyIndicatorParam[]>("Params", this.Params);
    }
}
