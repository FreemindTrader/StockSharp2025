// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileBody
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class FileBody : BaseEntity, IFileEntity, INameEntity, IDescriptionEntity
{
    public bool? Latest { get; set; }

    public File File { get; set; }

    public string Hash { get; set; }

    public long Length { get; set; }

    public BaseEntitySet<FileDownload> Downloads { get; set; }

    string INameEntity.Name
    {
        get => this.File?.Name;
        set => this.File = new File() { Name = value };
    }

    string IDescriptionEntity.Description
    {
        get => ((IDescriptionEntity)this.File)?.Description;
        set => ((IDescriptionEntity)this.File).Description = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Latest = storage.GetValue<bool?>("Latest", new bool?());
        this.File = storage.GetValue<File>("File", (File)null);
        this.Hash = storage.GetValue<string>("Hash", (string)null);
        this.Length = storage.GetValue<long>("Length", 0L);
        this.Downloads = storage.GetValue<BaseEntitySet<FileDownload>>("Downloads", (BaseEntitySet<FileDownload>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<bool?>("Latest", this.Latest).Set<File>("File", this.File).Set<string>("Hash", this.Hash).Set<long>("Length", this.Length).Set<BaseEntitySet<FileDownload>>("Downloads", this.Downloads);
    }
}
