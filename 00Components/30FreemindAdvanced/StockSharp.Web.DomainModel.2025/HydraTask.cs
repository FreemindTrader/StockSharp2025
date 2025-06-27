// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.HydraTask
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class HydraTask : BaseEntity, IClientEntity, INameEntity, IUserIdEntity, IStateEntity<SubscriptionStates?>, ILastTimeEntity
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.App = ( App ) storage.GetValue<App>( "App", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.CandlesCount = ( long? ) storage.GetValue<long?>( "CandlesCount", new long?() );
            this.TicksCount = ( long? ) storage.GetValue<long?>( "TicksCount", new long?() );
            this.BooksCount = ( long? ) storage.GetValue<long?>( "BooksCount", new long?() );
            this.OrderLogsCount = ( long? ) storage.GetValue<long?>( "OrderLogsCount", new long?() );
            this.Level1Count = ( long? ) storage.GetValue<long?>( "Level1Count", new long?() );
            this.State = ( SubscriptionStates? ) storage.GetValue<SubscriptionStates?>( "State", new SubscriptionStates?() );
            this.LastCommandTime = ( DateTime? ) storage.GetValue<DateTime?>( "LastCommandTime", new DateTime?() );
            this.LastStatusTime = ( DateTime? ) storage.GetValue<DateTime?>( "LastStatusTime", new DateTime?() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<string>( "UserId", this.UserId ).Set<App>( "App", this.App ).Set<string>( "Name", this.Name ).Set<long?>( "CandlesCount", this.CandlesCount ).Set<long?>( "TicksCount", this.TicksCount ).Set<long?>( "BooksCount", this.BooksCount ).Set<long?>( "OrderLogsCount", this.OrderLogsCount ).Set<long?>( "Level1Count", this.Level1Count ).Set<SubscriptionStates?>( "State", this.State ).Set<DateTime?>( "LastCommandTime", this.LastCommandTime ).Set<DateTime?>( "LastStatusTime", this.LastStatusTime );
        }
    }
}
