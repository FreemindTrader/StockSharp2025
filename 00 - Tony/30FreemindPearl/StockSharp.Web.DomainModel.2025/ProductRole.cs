// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductRole
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductRole : BaseEntity, IProductEntity
    {
        public Product Product { get; set; }

        public Client Role { get; set; }

        public TimeSpan Till { get; set; }

        public ProductPriceTypes? PriceType { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Product = ( Product ) storage.GetValue<Product>( "Product", null );
            this.Role = ( Client ) storage.GetValue<Client>( "Role", null );
            this.Till = ( TimeSpan ) storage.GetValue<TimeSpan>( "Till", new TimeSpan() );
            this.PriceType = ( ProductPriceTypes? ) storage.GetValue<ProductPriceTypes?>( "PriceType", new ProductPriceTypes?() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Product>( "Product", this.Product ).Set<Client>( "Role", this.Role ).Set<TimeSpan>( "Till", this.Till ).Set<ProductPriceTypes?>( "PriceType", this.PriceType );
        }
    }
}
