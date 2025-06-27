// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PayGatewayDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PayGatewayDomain : BaseEntity, IDomainEntity
    {
        public Domain Domain { get; set; }

        public PayGateway Gateway { get; set; }

        public bool IsEnabled { get; set; }

        public string SupportedCurrencies { get; set; }

        public PayGatewayDomainSettings Demo { get; set; }

        public PayGatewayDomainSettings Real { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.Gateway = ( PayGateway ) storage.GetValue<PayGateway>( "Gateway", null );
            this.IsEnabled = ( bool ) storage.GetValue<bool>( "IsEnabled", false );
            this.SupportedCurrencies = ( string ) storage.GetValue<string>( "SupportedCurrencies", null );
            this.Demo = ( PayGatewayDomainSettings ) storage.GetValue<PayGatewayDomainSettings>( "Demo", null );
            this.Real = ( PayGatewayDomainSettings ) storage.GetValue<PayGatewayDomainSettings>( "Real", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Domain>( "Domain", this.Domain ).Set<PayGateway>( "Gateway", this.Gateway ).Set<bool>( "IsEnabled", ( this.IsEnabled  ) ).Set<string>( "SupportedCurrencies", this.SupportedCurrencies ).Set<PayGatewayDomainSettings>( "Demo", this.Demo ).Set<PayGatewayDomainSettings>( "Real", this.Real );
        }
    }
}
