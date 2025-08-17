// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.MessagePatch
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class MessagePatch : BaseEntity, IMessageEntity, IDescriptionEntity
{
    public Message Message { get; set; }

    public string PackageId { get; set; }

    public string PackageUrl { get; set; }

    public string Version { get; set; }

    public string Description { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Message = storage.GetValue<Message>("Message", (Message)null);
        this.PackageId = storage.GetValue<string>("PackageId", (string)null);
        this.PackageUrl = storage.GetValue<string>("PackageUrl", (string)null);
        this.Version = storage.GetValue<string>("Version", (string)null);
        this.Description = storage.GetValue<string>("Description", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Message>("Message", this.Message).Set<string>("PackageId", this.PackageId).Set<string>("PackageUrl", this.PackageUrl).Set<string>("Version", this.Version).Set<string>("Description", this.Description);
    }
}
