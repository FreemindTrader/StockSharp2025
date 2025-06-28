// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.AmazonSettings
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class AmazonSettings : KeySecret
{
    public string Endpoint { get; set; }

    public string Bucket { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Endpoint = storage.GetValue<string>("Endpoint", (string)null);
        this.Bucket = storage.GetValue<string>("Bucket", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Endpoint", this.Endpoint).Set<string>("Bucket", this.Bucket);
    }
}
