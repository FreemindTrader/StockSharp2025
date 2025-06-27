// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Subscription
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Subscription : BaseEntity, IClientEntity, ITopicEntity
    {
        public Client Client { get; set; }

        public Topic Topic { get; set; }

        public TopicTag TopicTag { get; set; }

        public TopicTypes? TopicType { get; set; }

        public Client Author { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Topic = ( Topic ) storage.GetValue<Topic>( "Topic", null );
            this.TopicTag = ( TopicTag ) storage.GetValue<TopicTag>( "TopicTag", null );
            this.TopicType = ( TopicTypes? ) storage.GetValue<TopicTypes?>( "TopicType", new TopicTypes?() );
            this.Author = ( Client ) storage.GetValue<Client>( "Author", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<Topic>( "Topic", this.Topic ).Set<TopicTag>( "TopicTag", this.TopicTag ).Set<TopicTypes?>( "TopicType", this.TopicType ).Set<Client>( "Author", this.Author );
        }
    }
}
