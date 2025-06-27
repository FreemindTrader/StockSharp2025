// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileDownload
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class FileDownload : BaseEntity, IClientEntity
    {
        public Client Client { get; set; }

        public FileBody FileBody { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.FileBody = ( FileBody ) storage.GetValue<FileBody>( "FileBody", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<FileBody>( "FileBody", this.FileBody );
        }
    }
}
