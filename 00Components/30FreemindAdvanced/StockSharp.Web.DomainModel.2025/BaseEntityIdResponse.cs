// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseEntityIdResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public abstract class BaseEntityIdResponse : BaseResponse
    {
        protected BaseEntityIdResponse( SubscriptionTypes type )
          : base( type )
        {
        }

        public long? EntityId { get; set; }

        public long? ClientId { get; set; }

        public bool Removed { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.EntityId = ( long? ) storage.GetValue<long?>( "EntityId", new long?() );
            this.ClientId = ( long? ) storage.GetValue<long?>( "ClientId", new long?() );
            this.Removed = ( bool ) storage.GetValue<bool>( "Removed", false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<long?>( "EntityId", this.EntityId ).Set<long?>( "ClientId", this.ClientId ).Set<bool>( "Removed", ( this.Removed) );
        }
    }
}
