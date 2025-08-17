// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductDomainPrices
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductDomainPrices : IPersistable
{
    public Price MonthlyPrice { get; set; }

    public Price AnnualPrice { get; set; }

    public Price LifetimePrice { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.MonthlyPrice = storage.GetValue<Price>("MonthlyPrice", (Price)null);
        this.AnnualPrice = storage.GetValue<Price>("AnnualPrice", (Price)null);
        this.LifetimePrice = storage.GetValue<Price>("LifetimePrice", (Price)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<Price>("MonthlyPrice", this.MonthlyPrice).Set<Price>("AnnualPrice", this.AnnualPrice).Set<Price>("LifetimePrice", this.LifetimePrice);
    }
}
