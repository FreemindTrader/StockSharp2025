// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DiagramCategory
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DiagramCategory : BaseEntity, INameEntity
{
    public string Name { get; set; }

    public BaseEntitySet<DiagramElement> Elements { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Elements = storage.GetValue<BaseEntitySet<DiagramElement>>("Elements", (BaseEntitySet<DiagramElement>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<BaseEntitySet<DiagramElement>>("Elements", this.Elements);
    }
}
