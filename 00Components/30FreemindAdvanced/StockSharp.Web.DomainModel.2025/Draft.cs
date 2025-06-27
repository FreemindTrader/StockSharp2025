// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Draft
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class Draft : BaseEntity, IClientEntity, INameEntity, IDescriptionEntity
    {
        public Client Client { get; set; }

        public string PageId { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string Tags { get; set; }

        public BaseEntitySet<File> Attachments { get; set; }

        string IDescriptionEntity.Description
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.PageId = ( string ) storage.GetValue<string>( "PageId", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Text = ( string ) storage.GetValue<string>( "Text", null );
            this.Tags = ( string ) storage.GetValue<string>( "Tags", null );
            this.Attachments = ( BaseEntitySet<File> ) storage.GetValue<BaseEntitySet<File>>( "Attachments", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<string>( "PageId", this.PageId ).Set<string>( "Name", this.Name ).Set<string>( "Text", this.Text ).Set<string>( "Tags", this.Tags ).Set<BaseEntitySet<File>>( "Attachments", this.Attachments );
        }
    }
}
