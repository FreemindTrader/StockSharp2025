// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileShareVisit
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class FileShareVisit : BaseEntity, IClientEntity
    {
        public FileShare FileShare { get; set; }

        public Client Client { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.FileShare = ( FileShare ) storage.GetValue<FileShare>( "FileShare", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<FileShare>( "FileShare", this.FileShare ).Set<Client>( "Client", this.Client );
        }
    }
}
