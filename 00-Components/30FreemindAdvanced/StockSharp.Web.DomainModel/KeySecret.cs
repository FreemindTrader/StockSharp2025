// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.KeySecret
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class KeySecret : IPersistable
{
    public string Key { get; set; }

    public string Secret { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Key = storage.GetValue<string>("Key", (string)null);
        this.Secret = storage.GetValue<string>("Secret", (string)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<string>("Key", this.Key).Set<string>("Secret", this.Secret);
    }
}
