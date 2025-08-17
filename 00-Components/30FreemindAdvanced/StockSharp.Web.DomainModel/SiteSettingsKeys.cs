// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SiteSettingsKeys
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class SiteSettingsKeys : BaseEntity
{
    public SiteSettings Settings { get; set; }

    public string AppName { get; set; }

    public string Keys { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Settings = storage.GetValue<SiteSettings>("Settings", (SiteSettings)null);
        this.AppName = storage.GetValue<string>("AppName", (string)null);
        this.Keys = storage.GetValue<string>("Keys", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<SiteSettings>("Settings", this.Settings).Set<string>("AppName", this.AppName).Set<string>("Keys", this.Keys);
    }
}
