// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileDownload
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class FileDownload : BaseEntity, IClientEntity
{
    public Client Client { get; set; }

    public FileBody FileBody { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.FileBody = storage.GetValue<FileBody>("FileBody", (FileBody)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<FileBody>("FileBody", this.FileBody);
    }
}
