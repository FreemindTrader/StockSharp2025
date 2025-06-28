// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductBaseEmailTemplate
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductBaseEmailTemplate : IPersistable
{
    public EmailTemplate ToClient { get; set; }

    public EmailTemplate ToManager { get; set; }

    public EmailTemplate ToSite { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.ToClient = storage.GetValue<EmailTemplate>("ToClient", (EmailTemplate)null);
        this.ToManager = storage.GetValue<EmailTemplate>("ToManager", (EmailTemplate)null);
        this.ToSite = storage.GetValue<EmailTemplate>("ToSite", (EmailTemplate)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<EmailTemplate>("ToClient", this.ToClient).Set<EmailTemplate>("ToManager", this.ToManager).Set<EmailTemplate>("ToSite", this.ToSite);
    }
}
