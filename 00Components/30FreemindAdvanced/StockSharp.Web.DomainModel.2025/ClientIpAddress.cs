// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ClientIpAddress
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ClientIpAddress : BaseEntity
    {
        public bool IsRegistration { get; set; }

        public Client Client { get; set; }

        public ClientIpAddressEntityTypes EntityType { get; set; }

        public long EntityId { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.IsRegistration = ( bool ) storage.GetValue<bool>( "IsRegistration", false );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.EntityType = ( ClientIpAddressEntityTypes ) storage.GetValue<ClientIpAddressEntityTypes>( "EntityType", 0 );
            this.EntityId = ( long ) storage.GetValue<long>( "EntityId", 0L );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<bool>( "IsRegistration", ( this.IsRegistration ) ).Set<Client>( "Client", this.Client ).Set<ClientIpAddressEntityTypes>( "EntityType", this.EntityType ).Set<long>( "EntityId", this.EntityId );
        }
    }
}
