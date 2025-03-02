// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TopicGroup
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class TopicGroup : BaseEntity, INameEntity, IDescriptionEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public BaseEntitySet<Client> RolesRead { get; set; }

        public BaseEntitySet<Client> RolesWrite { get; set; }

        public BaseEntitySet<Topic> Topics { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.RolesRead = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "RolesRead", null );
            this.RolesWrite = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "RolesWrite", null );
            this.Topics = ( BaseEntitySet<Topic> ) storage.GetValue<BaseEntitySet<Topic>>( "Topics", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<string>( "Description", this.Description ).Set<BaseEntitySet<Client>>( "RolesRead", this.RolesRead ).Set<BaseEntitySet<Client>>( "RolesWrite", this.RolesWrite ).Set<BaseEntitySet<Topic>>( "Topics", this.Topics );
        }
    }
}
