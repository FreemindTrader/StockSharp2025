// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.EmailTemplate
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class EmailTemplate : BaseEntity, INameEntity, IDomainsEntity<EmailTemplateDomain>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Alias { get; set; }

        public bool IsEnabled { get; set; }

        public BaseEntitySet<EmailTemplateDomain> Domains { get; set; }

        public BaseEntitySet<ProductGroup> ProductGroups { get; set; }

        public BaseEntitySet<Product> Products { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Address = ( string ) storage.GetValue<string>( "Address", null );
            this.Alias = ( string ) storage.GetValue<string>( "Alias", null );
            this.IsEnabled = ( bool ) storage.GetValue<bool>( "IsEnabled", false );
            this.Domains = ( BaseEntitySet<EmailTemplateDomain> ) storage.GetValue<BaseEntitySet<EmailTemplateDomain>>( "Domains", null );
            this.ProductGroups = ( BaseEntitySet<ProductGroup> ) storage.GetValue<BaseEntitySet<ProductGroup>>( "ProductGroups", null );
            this.Products = ( BaseEntitySet<Product> ) storage.GetValue<BaseEntitySet<Product>>( "Products", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<string>( "Address", this.Address ).Set<string>( "Alias", this.Alias ).Set<bool>( "IsEnabled", ( this.IsEnabled) ).Set<BaseEntitySet<EmailTemplateDomain>>( "Domains", this.Domains ).Set<BaseEntitySet<ProductGroup>>( "ProductGroups", this.ProductGroups ).Set<BaseEntitySet<Product>>( "Products", this.Products );
        }
    }
}
