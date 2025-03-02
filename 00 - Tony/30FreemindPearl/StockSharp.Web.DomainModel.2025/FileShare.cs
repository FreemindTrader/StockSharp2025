// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileShare
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class FileShare : BaseEntity, IClientEntity, IFileEntity, IExpiryEntity
    {
        public Client Client { get; set; }

        public File File { get; set; }

        public string Token { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.MaxValue;

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.File = ( File ) storage.GetValue<File>( "File", null );
            this.Token = ( string ) storage.GetValue<string>( "Token", null );
            this.ExpiryDate = ( DateTime ) storage.GetValue<DateTime>( "ExpiryDate", new DateTime() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<File>( "File", this.File ).Set<string>( "Token", this.Token ).Set<DateTime>( "ExpiryDate", this.ExpiryDate );
        }
    }
}
