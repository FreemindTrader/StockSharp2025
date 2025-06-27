// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.File
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class File : BaseEntity, IClientEntity, IMessageEntity, INameEntity, IDescriptionEntity
    {
        public string Name { get; set; }

        public Client Client { get; set; }

        public bool IsCloud { get; set; }

        public string Hash { get; set; }

        public long BodyLength { get; set; }

        public DateTime Till { get; set; }

        public string UrlRelative { get; set; }

        public bool AsAvatar { get; set; }

        public string PageId { get; set; }

        public string Source { get; set; }

        public Message Message { get; set; }

        public FileShare Share { get; set; }

        public byte [ ] Body { get; set; }

        public bool? IsLoggedIn { get; set; }

        public BaseEntitySet<FileDownload> Downloads { get; set; }

        public BaseEntitySet<FileGroup> Groups { get; set; }

        public BaseEntitySet<FileBody> BodyVersions { get; set; }

        string IDescriptionEntity.Description { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.IsCloud = ( bool ) storage.GetValue<bool>( "IsCloud", false );
            this.Hash = ( string ) storage.GetValue<string>( "Hash", null );
            this.BodyLength = ( long ) storage.GetValue<long>( "BodyLength", 0L );
            this.Till = ( DateTime ) storage.GetValue<DateTime>( "Till", new DateTime() );
            this.UrlRelative = ( string ) storage.GetValue<string>( "UrlRelative", null );
            this.AsAvatar = ( bool ) storage.GetValue<bool>( "AsAvatar", false  );
            this.PageId = ( string ) storage.GetValue<string>( "PageId", null );
            this.Source = ( string ) storage.GetValue<string>( "Source", null );
            this.Message = ( Message ) storage.GetValue<Message>( "Message", null );
            this.Share = ( FileShare ) storage.GetValue<FileShare>( "Share", null );
            this.Body = ( byte [ ] ) storage.GetValue<byte [ ]>( "Body", null );
            this.IsLoggedIn = ( bool? ) storage.GetValue<bool?>( "IsLoggedIn", new bool?() );
            this.Downloads = ( BaseEntitySet<FileDownload> ) storage.GetValue<BaseEntitySet<FileDownload>>( "Downloads", null );
            this.Groups = ( BaseEntitySet<FileGroup> ) storage.GetValue<BaseEntitySet<FileGroup>>( "Groups", null );
            this.BodyVersions = ( BaseEntitySet<FileBody> ) storage.GetValue<BaseEntitySet<FileBody>>( "BodyVersions", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<Client>( "Client", this.Client ).Set<bool>( "IsCloud", ( this.IsCloud ) ).Set<string>( "Hash", this.Hash ).Set<long>( "BodyLength", this.BodyLength ).Set<DateTime>( "Till", this.Till ).Set<string>( "UrlRelative", this.UrlRelative ).Set<bool>( "AsAvatar", ( this.AsAvatar  ) ).Set<string>( "PageId", this.PageId ).Set<string>( "Source", this.Source ).Set<Message>( "Message", this.Message ).Set<FileShare>( "Share", this.Share ).Set<byte [ ]>( "Body", this.Body ).Set<bool?>( "IsLoggedIn", this.IsLoggedIn ).Set<BaseEntitySet<FileDownload>>( "Downloads", this.Downloads ).Set<BaseEntitySet<FileGroup>>( "Groups", this.Groups ).Set<BaseEntitySet<FileBody>>( "BodyVersions", this.BodyVersions );
        }
    }
}
