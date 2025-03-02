// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileBody
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class FileBody : BaseEntity, IFileEntity, INameEntity, IDescriptionEntity
    {
        public bool? Latest { get; set; }

        public File File { get; set; }

        public string Hash { get; set; }

        public long Length { get; set; }

        public BaseEntitySet<FileDownload> Downloads { get; set; }

        string INameEntity.Name
        {
            get
            {
                return this.File?.Name;
            }
            set
            {
                this.File = new File() { Name = value };
            }
        }

        string IDescriptionEntity.Description
        {
            get
            {
                return ( ( IDescriptionEntity ) this.File )?.Description;
            }
            set
            {
                ( ( IDescriptionEntity ) this.File ).Description = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Latest = ( bool? ) storage.GetValue<bool?>( "Latest", new bool?() );
            this.File = ( File ) storage.GetValue<File>( "File", null );
            this.Hash = ( string ) storage.GetValue<string>( "Hash", null );
            this.Length = ( long ) storage.GetValue<long>( "Length", 0L );
            this.Downloads = ( BaseEntitySet<FileDownload> ) storage.GetValue<BaseEntitySet<FileDownload>>( "Downloads", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<bool?>( "Latest", this.Latest ).Set<File>( "File", this.File ).Set<string>( "Hash", this.Hash ).Set<long>( "Length", this.Length ).Set<BaseEntitySet<FileDownload>>( "Downloads", this.Downloads );
        }
    }
}
