// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.MessagePatch
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class MessagePatch : BaseEntity, IMessageEntity, IDescriptionEntity
    {
        public Message Message { get; set; }

        public string PackageId { get; set; }

        public string PackageUrl { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Message = ( Message ) storage.GetValue<Message>( "Message", null );
            this.PackageId = ( string ) storage.GetValue<string>( "PackageId", null );
            this.PackageUrl = ( string ) storage.GetValue<string>( "PackageUrl", null );
            this.Version = ( string ) storage.GetValue<string>( "Version", null );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Message>( "Message", this.Message ).Set<string>( "PackageId", this.PackageId ).Set<string>( "PackageUrl", this.PackageUrl ).Set<string>( "Version", this.Version ).Set<string>( "Description", this.Description );
        }
    }
}
