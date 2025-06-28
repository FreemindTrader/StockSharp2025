// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseEntity
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public abstract class BaseEntity : IEquatable<BaseEntity>, IPersistable
{
    public long Id { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public bool Deleted { get; set; }

    public Client CreatedBy { get; set; }

    public DateTime CreationDateLocal => this.CreationDate.ToLocalTime();

    public DateTime ModificationDateLocal => this.ModificationDate.ToLocalTime();

    public string IP { get; set; }

    public override string ToString() => $"{{Type={this.GetType()}, Id={this.Id}}}";

    public override bool Equals(object obj) => obj is BaseEntity other && this.Equals(other);

    public override int GetHashCode() => this.Id.GetHashCode();

    public bool Equals(BaseEntity other) => this.GetType() == other.GetType() && this.Id == other.Id;

    public virtual void Load(SettingsStorage storage)
    {
        this.Id = storage.GetValue<long>("Id", 0L);
        this.CreationDate = storage.GetValue<DateTime>("CreationDate", new DateTime());
        this.ModificationDate = storage.GetValue<DateTime>("ModificationDate", new DateTime());
        this.Deleted = storage.GetValue<bool>("Deleted", false);
        this.CreatedBy = storage.GetValue<Client>("CreatedBy", (Client)null);
        this.IP = storage.GetValue<string>("IP", (string)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<long>("Id", this.Id).Set<DateTime>("CreationDate", this.CreationDate).Set<DateTime>("ModificationDate", this.ModificationDate).Set<bool>("Deleted", this.Deleted).Set<Client>("CreatedBy", this.CreatedBy).Set<string>("IP", this.IP);
    }
}
