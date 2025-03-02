// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DiagramElement
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class DiagramElement : BaseEntity, IPictureEntity, IClientEntity, INameEntity, IDescriptionEntity
    {
        public bool? IsPublic { get; set; }

        public Client Client { get; set; }

        public File Picture { get; set; }

        public string TypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DiagramCategory Category { get; set; }

        public BaseEntitySet<DiagramSocket> Sockets { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.IsPublic = ( bool? ) storage.GetValue<bool?>( "IsPublic", new bool?() );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Picture = ( File ) storage.GetValue<File>( "Picture", null );
            this.TypeId = ( string ) storage.GetValue<string>( "TypeId", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.Category = ( DiagramCategory ) storage.GetValue<DiagramCategory>( "Category", null );
            this.Sockets = ( BaseEntitySet<DiagramSocket> ) storage.GetValue<BaseEntitySet<DiagramSocket>>( "Sockets", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<bool?>( "IsPublic", this.IsPublic ).Set<Client>( "Client", this.Client ).Set<File>( "Picture", this.Picture ).Set<string>( "TypeId", this.TypeId ).Set<string>( "Name", this.Name ).Set<string>( "Description", this.Description ).Set<DiagramCategory>( "Category", this.Category ).Set<BaseEntitySet<DiagramSocket>>( "Sockets", this.Sockets );
        }
    }
}
