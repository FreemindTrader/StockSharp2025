// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.InstrumentDataType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class InstrumentDataType : BaseEntity, IInstrumentEntity
{
    public InstrumentInfo Security { get; set; }

    public DataType DataType { get; set; }

    public DateTime Begin { get; set; }

    public DateTime End { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Security = storage.GetValue<InstrumentInfo>("Security", (InstrumentInfo)null);
        this.DataType = storage.GetValue<DataType>("DataType", (DataType)null);
        this.Begin = storage.GetValue<DateTime>("Begin", new DateTime());
        this.End = storage.GetValue<DateTime>("End", new DateTime());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<InstrumentInfo>("Security", this.Security).Set<DataType>("DataType", this.DataType).Set<DateTime>("Begin", this.Begin).Set<DateTime>("End", this.End);
    }
}
