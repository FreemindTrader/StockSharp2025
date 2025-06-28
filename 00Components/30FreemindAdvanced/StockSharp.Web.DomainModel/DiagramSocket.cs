// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DiagramSocket
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DiagramSocket : BaseEntity, INameEntity, IUserIdEntity
{
    public string Name { get; set; }

    public string UserId { get; set; }

    public bool? Direction { get; set; }

    public int? MaxLinks { get; set; }

    public DiagramElement Element { get; set; }

    public DiagramSocketType Type { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.Direction = storage.GetValue<bool?>("Direction", new bool?());
        this.MaxLinks = storage.GetValue<int?>("MaxLinks", new int?());
        this.Element = storage.GetValue<DiagramElement>("Element", (DiagramElement)null);
        this.Type = storage.GetValue<DiagramSocketType>("Type", (DiagramSocketType)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<string>("UserId", this.UserId).Set<bool?>("Direction", this.Direction).Set<int?>("MaxLinks", this.MaxLinks).Set<DiagramElement>("Element", this.Element).Set<DiagramSocketType>("Type", this.Type);
    }
}
