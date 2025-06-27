// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PublishResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class PublishResponse : BaseEntityIdResponse
    {
        public PublishResponse()
          : base( SubscriptionTypes.Publish )
        {
        }

        public Guid PublishId { get; set; }

        public PublishActions Action { get; set; }

        public string Value { get; set; }

        public PublishDetails Details { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.PublishId = ( Guid ) storage.GetValue<Guid>( "PublishId", new Guid() );
            this.Action = ( PublishActions ) storage.GetValue<PublishActions>( "Action", 0 );
            this.Value = ( string ) storage.GetValue<string>( "Value", null );
            this.Details = ( PublishDetails ) storage.GetValue<PublishDetails>( "Details", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Guid>( "PublishId", this.PublishId ).Set<PublishActions>( "Action", this.Action ).Set<string>( "Value", this.Value ).Set<PublishDetails>( "Details", this.Details );
        }
    }
}
