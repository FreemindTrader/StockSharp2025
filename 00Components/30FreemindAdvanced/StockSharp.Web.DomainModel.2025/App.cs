// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.App
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class App : BaseEntity, IClientEntity, INameEntity, IUserIdEntity, IStateEntity<SubscriptionStates?>, ILastTimeEntity
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Picture = ( File ) storage.GetValue<File>( "Picture", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.LocalPath = ( string ) storage.GetValue<string>( "LocalPath", null );
            this.Version = ( string ) storage.GetValue<string>( "Version", null );
            this.LastCommandTime = ( DateTime? ) storage.GetValue<DateTime?>( "LastCommandTime", new DateTime?() );
            this.LastStatusTime = ( DateTime? ) storage.GetValue<DateTime?>( "LastStatusTime", new DateTime?() );
            this.State = ( SubscriptionStates? ) storage.GetValue<SubscriptionStates?>( "State", new SubscriptionStates?() );
            this.Tasks = ( BaseEntitySet<HydraTask> ) storage.GetValue<BaseEntitySet<HydraTask>>( "Tasks", null );
            this.Strategies = ( BaseEntitySet<Strategy> ) storage.GetValue<BaseEntitySet<Strategy>>( "Strategies", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<File>( "Picture", this.Picture ).Set<Client>( "Client", this.Client ).Set<string>( "UserId", this.UserId ).Set<string>( "Name", this.Name ).Set<string>( "LocalPath", this.LocalPath ).Set<string>( "Version", this.Version ).Set<DateTime?>( "LastCommandTime", this.LastCommandTime ).Set<DateTime?>( "LastStatusTime", this.LastStatusTime ).Set<SubscriptionStates?>( "State", this.State ).Set<BaseEntitySet<HydraTask>>( "Tasks", this.Tasks ).Set<BaseEntitySet<Strategy>>( "Strategies", this.Strategies );
        }
    }
}
