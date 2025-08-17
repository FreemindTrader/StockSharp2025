// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.HydraTask
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class HydraTask :
  BaseEntity,
  IClientEntity,
  INameEntity,
  IUserIdEntity,
  IStateEntity<SubscriptionStates?>,
  ILastTimeEntity
{
    public Client Client { get; set; }

    public string UserId { get; set; }

    public App App { get; set; }

    public string Name { get; set; }

    public long? CandlesCount { get; set; }

    public long? TicksCount { get; set; }

    public long? BooksCount { get; set; }

    public long? OrderLogsCount { get; set; }

    public long? Level1Count { get; set; }

    public SubscriptionStates? State { get; set; }

    public DateTime? LastCommandTime { get; set; }

    public DateTime? LastStatusTime { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.App = storage.GetValue<App>("App", (App)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.CandlesCount = storage.GetValue<long?>("CandlesCount", new long?());
        this.TicksCount = storage.GetValue<long?>("TicksCount", new long?());
        this.BooksCount = storage.GetValue<long?>("BooksCount", new long?());
        this.OrderLogsCount = storage.GetValue<long?>("OrderLogsCount", new long?());
        this.Level1Count = storage.GetValue<long?>("Level1Count", new long?());
        this.State = storage.GetValue<SubscriptionStates?>("State", new SubscriptionStates?());
        this.LastCommandTime = storage.GetValue<DateTime?>("LastCommandTime", new DateTime?());
        this.LastStatusTime = storage.GetValue<DateTime?>("LastStatusTime", new DateTime?());
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<string>("UserId", this.UserId).Set<App>("App", this.App).Set<string>("Name", this.Name).Set<long?>("CandlesCount", this.CandlesCount).Set<long?>("TicksCount", this.TicksCount).Set<long?>("BooksCount", this.BooksCount).Set<long?>("OrderLogsCount", this.OrderLogsCount).Set<long?>("Level1Count", this.Level1Count).Set<SubscriptionStates?>("State", this.State).Set<DateTime?>("LastCommandTime", this.LastCommandTime).Set<DateTime?>("LastStatusTime", this.LastStatusTime);
    }
}
