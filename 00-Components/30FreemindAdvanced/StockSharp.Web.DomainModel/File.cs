// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.File
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class File : BaseEntity, IClientEntity, IMessageEntity, INameEntity, IDescriptionEntity
{
    public string Name { get; set; }

    public Client Client { get; set; }

    public bool IsCloud { get; set; }

    public string Hash { get; set; }

    public long BodyLength { get; set; }

    public DateTime Till { get; set; }

    public string UrlRelative { get; set; }

    public bool AsAvatar { get; set; }

    public string PageId { get; set; }

    public string Source { get; set; }

    public Message Message { get; set; }

    public FileShare Share { get; set; }

    public byte[] Body { get; set; }

    public bool? IsLoggedIn { get; set; }

    public BaseEntitySet<FileDownload> Downloads { get; set; }

    public BaseEntitySet<FileGroup> Groups { get; set; }

    public BaseEntitySet<FileBody> BodyVersions { get; set; }

    string IDescriptionEntity.Description { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.IsCloud = storage.GetValue<bool>("IsCloud", false);
        this.Hash = storage.GetValue<string>("Hash", (string)null);
        this.BodyLength = storage.GetValue<long>("BodyLength", 0L);
        this.Till = storage.GetValue<DateTime>("Till", new DateTime());
        this.UrlRelative = storage.GetValue<string>("UrlRelative", (string)null);
        this.AsAvatar = storage.GetValue<bool>("AsAvatar", false);
        this.PageId = storage.GetValue<string>("PageId", (string)null);
        this.Source = storage.GetValue<string>("Source", (string)null);
        this.Message = storage.GetValue<Message>("Message", (Message)null);
        this.Share = storage.GetValue<FileShare>("Share", (FileShare)null);
        this.Body = storage.GetValue<byte[]>("Body", (byte[])null);
        this.IsLoggedIn = storage.GetValue<bool?>("IsLoggedIn", new bool?());
        this.Downloads = storage.GetValue<BaseEntitySet<FileDownload>>("Downloads", (BaseEntitySet<FileDownload>)null);
        this.Groups = storage.GetValue<BaseEntitySet<FileGroup>>("Groups", (BaseEntitySet<FileGroup>)null);
        this.BodyVersions = storage.GetValue<BaseEntitySet<FileBody>>("BodyVersions", (BaseEntitySet<FileBody>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<Client>("Client", this.Client).Set<bool>("IsCloud", this.IsCloud).Set<string>("Hash", this.Hash).Set<long>("BodyLength", this.BodyLength).Set<DateTime>("Till", this.Till).Set<string>("UrlRelative", this.UrlRelative).Set<bool>("AsAvatar", this.AsAvatar).Set<string>("PageId", this.PageId).Set<string>("Source", this.Source).Set<Message>("Message", this.Message).Set<FileShare>("Share", this.Share).Set<byte[]>("Body", this.Body).Set<bool?>("IsLoggedIn", this.IsLoggedIn).Set<BaseEntitySet<FileDownload>>("Downloads", this.Downloads).Set<BaseEntitySet<FileGroup>>("Groups", this.Groups).Set<BaseEntitySet<FileBody>>("BodyVersions", this.BodyVersions);
    }
}
