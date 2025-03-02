// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductDomain : ProductDomainBase, ITopicEntity, IProductEntity
    {
        public Product Product { get; set; }

        public Topic Topic { get; set; }

        public Topic Instruction { get; set; }

        public Topic Extra { get; set; }

        public string Homepage { get; set; }

        public string UrlAlias { get; set; }

        public string UrlRelative { get; set; }

        public Price ReceivedAmount { get; set; }

        public ProductDomainPrices DiscountAllApps { get; set; } = new ProductDomainPrices();

        public ProductDomainPrices DiscountOneApps { get; set; } = new ProductDomainPrices();

        [Obsolete]
        public Price DiscountMonthlyPrice { get; set; }

        [Obsolete]
        public Price DiscountAnnualPrice { get; set; }

        [Obsolete]
        public Price DiscountLifetimePrice { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Product = ( Product ) storage.GetValue<Product>( "Product", null );
            this.Topic = ( Topic ) storage.GetValue<Topic>( "Topic", null );
            this.Instruction = ( Topic ) storage.GetValue<Topic>( "Instruction", null );
            this.Extra = ( Topic ) storage.GetValue<Topic>( "Extra", null );
            this.Homepage = ( string ) storage.GetValue<string>( "Homepage", null );
            this.UrlAlias = ( string ) storage.GetValue<string>( "UrlAlias", null );
            this.UrlRelative = ( string ) storage.GetValue<string>( "UrlRelative", null );
            this.ReceivedAmount = ( Price ) storage.GetValue<Price>( "ReceivedAmount", null );
            this.DiscountAllApps = ( ProductDomainPrices ) storage.GetValue<ProductDomainPrices>( "DiscountAllApps", null );
            this.DiscountOneApps = ( ProductDomainPrices ) storage.GetValue<ProductDomainPrices>( "DiscountOneApps", null );
            this.DiscountMonthlyPrice = ( Price ) storage.GetValue<Price>( "DiscountMonthlyPrice", null );
            this.DiscountAnnualPrice = ( Price ) storage.GetValue<Price>( "DiscountAnnualPrice", null );
            this.DiscountLifetimePrice = ( Price ) storage.GetValue<Price>( "DiscountLifetimePrice", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Product>( "Product", this.Product ).Set<Topic>( "Topic", this.Topic ).Set<Topic>( "Instruction", this.Instruction ).Set<Topic>( "Extra", this.Extra ).Set<string>( "Homepage", this.Homepage ).Set<string>( "UrlAlias", this.UrlAlias ).Set<string>( "UrlRelative", this.UrlRelative ).Set<Price>( "ReceivedAmount", this.ReceivedAmount ).Set<ProductDomainPrices>( "DiscountAllApps", this.DiscountAllApps ).Set<ProductDomainPrices>( "DiscountOneApps", this.DiscountOneApps ).Set<Price>( "DiscountMonthlyPrice", this.DiscountMonthlyPrice ).Set<Price>( "DiscountAnnualPrice", this.DiscountAnnualPrice ).Set<Price>( "DiscountLifetimePrice", this.DiscountLifetimePrice );
        }
    }
}
