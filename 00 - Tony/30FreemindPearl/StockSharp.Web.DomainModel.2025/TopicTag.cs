// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TopicTag
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class TopicTag : BaseEntity, INameEntity
    {
        public string Name { get; set; }

        public bool? IsMySubscription { get; set; }

        public BaseEntitySet<Subscription> Subscriptions { get; set; }

        public BaseEntitySet<Topic> Topics { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.IsMySubscription = ( bool? ) storage.GetValue<bool?>( "IsMySubscription", new bool?() );
            this.Subscriptions = ( BaseEntitySet<Subscription> ) storage.GetValue<BaseEntitySet<Subscription>>( "Subscriptions", null );
            this.Topics = ( BaseEntitySet<Topic> ) storage.GetValue<BaseEntitySet<Topic>>( "Topics", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<bool?>( "IsMySubscription", this.IsMySubscription ).Set<BaseEntitySet<Subscription>>( "Subscriptions", this.Subscriptions ).Set<BaseEntitySet<Topic>>( "Topics", this.Topics );
        }
    }
}
