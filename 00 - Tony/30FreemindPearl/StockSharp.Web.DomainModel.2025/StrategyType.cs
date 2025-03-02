// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyType
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyType : BaseEntity, IClientEntity, INameEntity, IProductContentTypeEntity
    {
        public Client Client { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public string Password { get; set; }

        public ProductContentTypes2 ContentType { get; set; }

        public File Content { get; set; }

        public BaseEntitySet<Strategy> Strategies { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.TypeName = ( string ) storage.GetValue<string>( "TypeName", null );
            this.Password = ( string ) storage.GetValue<string>( "Password", null );
            this.ContentType = ( ProductContentTypes2 ) storage.GetValue<ProductContentTypes2>( "ContentType", 0L );
            this.Content = ( File ) storage.GetValue<File>( "Content", null );
            this.Strategies = ( BaseEntitySet<Strategy> ) storage.GetValue<BaseEntitySet<Strategy>>( "Strategies", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<string>( "Name", this.Name ).Set<string>( "TypeName", this.TypeName ).Set<string>( "Password", this.Password ).Set<ProductContentTypes2>( "ContentType", this.ContentType ).Set<File>( "Content", this.Content ).Set<BaseEntitySet<Strategy>>( "Strategies", this.Strategies );
        }
    }
}
