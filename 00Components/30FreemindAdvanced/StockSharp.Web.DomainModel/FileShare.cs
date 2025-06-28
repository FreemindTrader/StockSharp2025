// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileShare
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class FileShare : BaseEntity, IClientEntity, IFileEntity, IExpiryEntity
{
    public Client Client { get; set; }

    public File File { get; set; }

    public string Token { get; set; }

    public DateTime ExpiryDate { get; set; } = DateTime.MaxValue;

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.File = storage.GetValue<File>("File", (File)null);
        this.Token = storage.GetValue<string>("Token", (string)null);
        this.ExpiryDate = storage.GetValue<DateTime>("ExpiryDate", new DateTime());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<File>("File", this.File).Set<string>("Token", this.Token).Set<DateTime>("ExpiryDate", this.ExpiryDate);
    }
}
