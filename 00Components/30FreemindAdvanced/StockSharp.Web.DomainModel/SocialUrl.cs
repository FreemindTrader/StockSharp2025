// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SocialUrl
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class SocialUrl : IPersistable
{
    public string Base { get; set; }

    public string Mask { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Base = storage.GetValue<string>("Base", (string)null);
        this.Mask = storage.GetValue<string>("Mask", (string)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<string>("Base", this.Base).Set<string>("Mask", this.Mask);
    }
}
