// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DataType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DataType : BaseEntity, INameEntity
{
    public string Name { get; set; }

    public DataTypes Type { get; set; }

    public long Value { get; set; }

    public BaseEntitySet<InstrumentDataType> InstrumentDataTypes { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Type = storage.GetValue<DataTypes>("Type", DataTypes.TF);
        this.Value = storage.GetValue<long>("Value", 0L);
        this.InstrumentDataTypes = storage.GetValue<BaseEntitySet<InstrumentDataType>>("InstrumentDataTypes", (BaseEntitySet<InstrumentDataType>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<DataTypes>("Type", this.Type).Set<long>("Value", this.Value).Set<BaseEntitySet<InstrumentDataType>>("InstrumentDataTypes", this.InstrumentDataTypes);
    }
}
