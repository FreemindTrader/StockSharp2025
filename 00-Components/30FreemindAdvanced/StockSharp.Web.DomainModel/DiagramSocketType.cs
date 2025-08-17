// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DiagramSocketType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DiagramSocketType : BaseEntity, INameEntity
{
    public string Name { get; set; }

    public string Type { get; set; }

    public string Color { get; set; }

    public BaseEntitySet<DiagramSocket> Sockets { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Type = storage.GetValue<string>("Type", (string)null);
        this.Color = storage.GetValue<string>("Color", (string)null);
        this.Sockets = storage.GetValue<BaseEntitySet<DiagramSocket>>("Sockets", (BaseEntitySet<DiagramSocket>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<string>("Type", this.Type).Set<string>("Color", this.Color).Set<BaseEntitySet<DiagramSocket>>("Sockets", this.Sockets);
    }
}
