// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DiagramElement
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DiagramElement :
  BaseEntity,
  IPictureEntity,
  IClientEntity,
  INameEntity,
  IDescriptionEntity
{
    public bool? IsPublic { get; set; }

    public Client Client { get; set; }

    public File Picture { get; set; }

    public string TypeId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DiagramCategory Category { get; set; }

    public BaseEntitySet<DiagramSocket> Sockets { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.IsPublic = storage.GetValue<bool?>("IsPublic", new bool?());
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Picture = storage.GetValue<File>("Picture", (File)null);
        this.TypeId = storage.GetValue<string>("TypeId", (string)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.Category = storage.GetValue<DiagramCategory>("Category", (DiagramCategory)null);
        this.Sockets = storage.GetValue<BaseEntitySet<DiagramSocket>>("Sockets", (BaseEntitySet<DiagramSocket>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<bool?>("IsPublic", this.IsPublic).Set<Client>("Client", this.Client).Set<File>("Picture", this.Picture).Set<string>("TypeId", this.TypeId).Set<string>("Name", this.Name).Set<string>("Description", this.Description).Set<DiagramCategory>("Category", this.Category).Set<BaseEntitySet<DiagramSocket>>("Sockets", this.Sockets);
    }
}
