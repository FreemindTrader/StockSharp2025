// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.App
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class App :
  BaseEntity,
  IClientEntity,
  INameEntity,
  IUserIdEntity,
  IStateEntity<SubscriptionStates?>,
  ILastTimeEntity
{
    public File Picture { get; set; }

    public Client Client { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }

    public string LocalPath { get; set; }

    public string Version { get; set; }

    public DateTime? LastCommandTime { get; set; }

    public DateTime? LastStatusTime { get; set; }

    public SubscriptionStates? State { get; set; }

    public BaseEntitySet<HydraTask> Tasks { get; set; }

    public BaseEntitySet<Strategy> Strategies { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Picture = storage.GetValue<File>("Picture", (File)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.LocalPath = storage.GetValue<string>("LocalPath", (string)null);
        this.Version = storage.GetValue<string>("Version", (string)null);
        this.LastCommandTime = storage.GetValue<DateTime?>("LastCommandTime", new DateTime?());
        this.LastStatusTime = storage.GetValue<DateTime?>("LastStatusTime", new DateTime?());
        this.State = storage.GetValue<SubscriptionStates?>("State", new SubscriptionStates?());
        this.Tasks = storage.GetValue<BaseEntitySet<HydraTask>>("Tasks", (BaseEntitySet<HydraTask>)null);
        this.Strategies = storage.GetValue<BaseEntitySet<Strategy>>("Strategies", (BaseEntitySet<Strategy>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<File>("Picture", this.Picture).Set<Client>("Client", this.Client).Set<string>("UserId", this.UserId).Set<string>("Name", this.Name).Set<string>("LocalPath", this.LocalPath).Set<string>("Version", this.Version).Set<DateTime?>("LastCommandTime", this.LastCommandTime).Set<DateTime?>("LastStatusTime", this.LastStatusTime).Set<SubscriptionStates?>("State", this.State).Set<BaseEntitySet<HydraTask>>("Tasks", this.Tasks).Set<BaseEntitySet<Strategy>>("Strategies", this.Strategies);
    }
}
