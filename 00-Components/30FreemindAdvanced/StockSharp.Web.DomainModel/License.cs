// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.License
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class License : BaseEntity, IClientEntity, IFileEntity, IDescriptionEntity
{
    public Client Client { get; set; }

    public File File { get; set; }

    public string Description { get; set; }

    public byte[] Body { get; set; }

    public BaseEntitySet<LicenseFeatureEx> Features { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.File = storage.GetValue<File>("File", (File)null);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.Body = storage.GetValue<byte[]>("Body", (byte[])null);
        this.Features = storage.GetValue<BaseEntitySet<LicenseFeatureEx>>("Features", (BaseEntitySet<LicenseFeatureEx>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<File>("File", this.File).Set<string>("Description", this.Description).Set<byte[]>("Body", this.Body).Set<BaseEntitySet<LicenseFeatureEx>>("Features", this.Features);
    }
}
