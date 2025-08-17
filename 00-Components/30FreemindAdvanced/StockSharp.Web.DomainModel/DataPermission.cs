// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DataPermission
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class DataPermission : BaseEntity, IClientEntity, IInstrumentEntity
{
    public Client Client { get; set; }

    public InstrumentInfo Security { get; set; }

    public UserPermissions Value { get; set; }

    public DateTime Begin { get; set; }

    public DateTime End { get; set; }

    public DataType DataType { get; set; }

    public bool IsDownload { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Security = storage.GetValue<InstrumentInfo>("Security", (InstrumentInfo)null);
        this.Value = storage.GetValue<UserPermissions>("Value", (UserPermissions)0);
        this.Begin = storage.GetValue<DateTime>("Begin", new DateTime());
        this.End = storage.GetValue<DateTime>("End", new DateTime());
        this.DataType = storage.GetValue<DataType>("DataType", (DataType)null);
        this.IsDownload = storage.GetValue<bool>("IsDownload", false);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<InstrumentInfo>("Security", this.Security).Set<UserPermissions>("Value", this.Value).Set<DateTime>("Begin", this.Begin).Set<DateTime>("End", this.End).Set<DataType>("DataType", this.DataType).Set<bool>("IsDownload", this.IsDownload);
    }
}
