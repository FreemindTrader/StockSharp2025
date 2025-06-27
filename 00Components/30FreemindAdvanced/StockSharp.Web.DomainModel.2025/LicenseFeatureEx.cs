// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.LicenseFeatureEx
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Licensing;
using System;

namespace StockSharp.Web.DomainModel
{
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.License = ( License ) storage.GetValue<License>( "License", null );
            this.Feature = ( LicenseFeature ) storage.GetValue<LicenseFeature>( "Feature", null );
            this.ExpiryDate = ( DateTime ) storage.GetValue<DateTime>( "ExpiryDate", new DateTime() );
            this.Account = ( string ) storage.GetValue<string>( "Account", null );
            this.HardwareId = ( string ) storage.GetValue<string>( "HardwareId", null );
            this.OneApp = ( Product ) storage.GetValue<Product>( "OneApp", null );
            this.Platform = ( string ) storage.GetValue<string>( "Platform", null );
            this.ExpireAction = ( LicenseExpireActions ) storage.GetValue<LicenseExpireActions>( "ExpireAction", 0 );
            this.RenewCount = ( int ) storage.GetValue<int>( "RenewCount", 0 );
            this.MaxRenewCount = ( int ) storage.GetValue<int>( "MaxRenewCount", 0 );
            this.Broker = ( Product ) storage.GetValue<Product>( "Broker", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<License>( "License", this.License ).Set<LicenseFeature>( "Feature", this.Feature ).Set<DateTime>( "ExpiryDate", this.ExpiryDate ).Set<string>( "Account", this.Account ).Set<string>( "HardwareId", this.HardwareId ).Set<Product>( "OneApp", this.OneApp ).Set<string>( "Platform", this.Platform ).Set<LicenseExpireActions>( "ExpireAction", this.ExpireAction ).Set<int>( "RenewCount", this.RenewCount ).Set<int>( "MaxRenewCount", this.MaxRenewCount ).Set<Product>( "Broker", this.Broker );
        }
    }
}
