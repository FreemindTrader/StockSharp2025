// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.LicenseFeatureEx
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;
using StockSharp.Licensing;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class LicenseFeatureEx : BaseEntity, IExpiryEntity
{
    public License License { get; set; }

    public LicenseFeature Feature { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Account { get; set; }

    public string HardwareId { get; set; }

    public Product OneApp { get; set; }

    public string Platform { get; set; }

    public LicenseExpireActions ExpireAction { get; set; }

    public int RenewCount { get; set; }

    public int MaxRenewCount { get; set; }

    public Product Broker { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.License = storage.GetValue<License>("License", (License)null);
        this.Feature = storage.GetValue<LicenseFeature>("Feature", (LicenseFeature)null);
        this.ExpiryDate = storage.GetValue<DateTime>("ExpiryDate", new DateTime());
        this.Account = storage.GetValue<string>("Account", (string)null);
        this.HardwareId = storage.GetValue<string>("HardwareId", (string)null);
        this.OneApp = storage.GetValue<Product>("OneApp", (Product)null);
        this.Platform = storage.GetValue<string>("Platform", (string)null);
        this.ExpireAction = storage.GetValue<LicenseExpireActions>("ExpireAction", LicenseExpireActions.PreventWork);
        this.RenewCount = storage.GetValue<int>("RenewCount", 0);
        this.MaxRenewCount = storage.GetValue<int>("MaxRenewCount", 0);
        this.Broker = storage.GetValue<Product>("Broker", (Product)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<License>("License", this.License).Set<LicenseFeature>("Feature", this.Feature).Set<DateTime>("ExpiryDate", this.ExpiryDate).Set<string>("Account", this.Account).Set<string>("HardwareId", this.HardwareId).Set<Product>("OneApp", this.OneApp).Set<string>("Platform", this.Platform).Set<LicenseExpireActions>("ExpireAction", this.ExpireAction).Set<int>("RenewCount", this.RenewCount).Set<int>("MaxRenewCount", this.MaxRenewCount).Set<Product>("Broker", this.Broker);
    }
}
