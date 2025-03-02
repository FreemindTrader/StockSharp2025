// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductDomainPrices
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class ProductDomainPrices : IPersistable
    {
        public Price MonthlyPrice { get; set; }

        public Price AnnualPrice { get; set; }

        public Price LifetimePrice { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.MonthlyPrice = ( Price ) storage.GetValue<Price>( "MonthlyPrice", null );
            this.AnnualPrice = ( Price ) storage.GetValue<Price>( "AnnualPrice", null );
            this.LifetimePrice = ( Price ) storage.GetValue<Price>( "LifetimePrice", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<Price>( "MonthlyPrice", this.MonthlyPrice ).Set<Price>( "AnnualPrice", this.AnnualPrice ).Set<Price>( "LifetimePrice", this.LifetimePrice );
        }
    }
}
