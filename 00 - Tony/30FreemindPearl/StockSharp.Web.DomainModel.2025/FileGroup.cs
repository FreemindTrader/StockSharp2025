// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.FileGroup
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class FileGroup : BaseEntity, INameEntity, IDescriptionEntity
    {
        public string Name { get; set; }

        public Client Owner { get; set; }

        public string Description { get; set; }

        public BaseEntitySet<FileGroup> Child { get; set; }

        public BaseEntitySet<File> Files { get; set; }

        public BaseEntitySet<FileGroup> Parent { get; set; }

        public BaseEntitySet<Client> Roles { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Owner = ( Client ) storage.GetValue<Client>( "Owner", null );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.Child = ( BaseEntitySet<FileGroup> ) storage.GetValue<BaseEntitySet<FileGroup>>( "Child", null );
            this.Files = ( BaseEntitySet<File> ) storage.GetValue<BaseEntitySet<File>>( "Files", null );
            this.Parent = ( BaseEntitySet<FileGroup> ) storage.GetValue<BaseEntitySet<FileGroup>>( "Parent", null );
            this.Roles = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Roles", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<Client>( "Owner", this.Owner ).Set<string>( "Description", this.Description ).Set<BaseEntitySet<FileGroup>>( "Child", this.Child ).Set<BaseEntitySet<File>>( "Files", this.Files ).Set<BaseEntitySet<FileGroup>>( "Parent", this.Parent ).Set<BaseEntitySet<Client>>( "Roles", this.Roles );
        }
    }
}
