// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileGroup
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class FileGroup : BaseEntity, INameEntity, IDescriptionEntity
{
    public string Name { get; set; }

    public Client Owner { get; set; }

    public string Description { get; set; }

    public BaseEntitySet<FileGroup> Child { get; set; }

    public BaseEntitySet<File> Files { get; set; }

    public BaseEntitySet<FileGroup> Parent { get; set; }

    public BaseEntitySet<Client> Roles { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Owner = storage.GetValue<Client>("Owner", (Client)null);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.Child = storage.GetValue<BaseEntitySet<FileGroup>>("Child", (BaseEntitySet<FileGroup>)null);
        this.Files = storage.GetValue<BaseEntitySet<File>>("Files", (BaseEntitySet<File>)null);
        this.Parent = storage.GetValue<BaseEntitySet<FileGroup>>("Parent", (BaseEntitySet<FileGroup>)null);
        this.Roles = storage.GetValue<BaseEntitySet<Client>>("Roles", (BaseEntitySet<Client>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<Client>("Owner", this.Owner).Set<string>("Description", this.Description).Set<BaseEntitySet<FileGroup>>("Child", this.Child).Set<BaseEntitySet<File>>("Files", this.Files).Set<BaseEntitySet<FileGroup>>("Parent", this.Parent).Set<BaseEntitySet<Client>>("Roles", this.Roles);
    }
}
