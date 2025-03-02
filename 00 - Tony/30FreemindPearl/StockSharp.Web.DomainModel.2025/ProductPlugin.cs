// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductPlugin
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductPlugin : BaseEntity, IProductContentTypeEntity
    {
        public ProductGroup Group { get; set; }

        public ProductContentTypes2 ContentType { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Group = ( ProductGroup ) storage.GetValue<ProductGroup>( "Group", null );
            this.ContentType = ( ProductContentTypes2 ) storage.GetValue<ProductContentTypes2>( "ContentType", 0L );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<ProductGroup>( "Group", this.Group ).Set<ProductContentTypes2>( "ContentType", this.ContentType );
        }
    }
}
