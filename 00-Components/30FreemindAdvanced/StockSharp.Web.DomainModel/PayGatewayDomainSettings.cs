// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PayGatewayDomainSettings
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class PayGatewayDomainSettings : IPersistable
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string Url { get; set; }

    public string Token { get; set; }

    public File Certificate { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Login = storage.GetValue<string>("Login", (string)null);
        this.Password = storage.GetValue<string>("Password", (string)null);
        this.Url = storage.GetValue<string>("Url", (string)null);
        this.Token = storage.GetValue<string>("Token", (string)null);
        this.Certificate = storage.GetValue<File>("Certificate", (File)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<string>("Login", this.Login).Set<string>("Password", this.Password).Set<string>("Url", this.Url).Set<string>("Token", this.Token).Set<File>("Certificate", this.Certificate);
    }
}
