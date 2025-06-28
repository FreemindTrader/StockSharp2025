// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyCandle
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyCandle : INameEntity, IPersistable
{
    public string Name { get; set; }

    public string Security { get; set; }

    public string TypeName { get; set; }

    public string Arg { get; set; }

    public string Style { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Security = storage.GetValue<string>("Security", (string)null);
        this.TypeName = storage.GetValue<string>("TypeName", (string)null);
        this.Arg = storage.GetValue<string>("Arg", (string)null);
        this.Style = storage.GetValue<string>("Style", (string)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<string>("Name", this.Name).Set<string>("Security", this.Security).Set<string>("TypeName", this.TypeName).Set<string>("Arg", this.Arg).Set<string>("Style", this.Style);
    }
}
