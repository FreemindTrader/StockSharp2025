// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PayGatewayDomain
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class PayGatewayDomain : BaseEntity, IDomainEntity
{
    public Domain Domain { get; set; }

    public PayGateway Gateway { get; set; }

    public bool IsEnabled { get; set; }

    public string SupportedCurrencies { get; set; }

    public PayGatewayDomainSettings Demo { get; set; }

    public PayGatewayDomainSettings Real { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Domain = storage.GetValue<Domain>("Domain", (Domain)null);
        this.Gateway = storage.GetValue<PayGateway>("Gateway", (PayGateway)null);
        this.IsEnabled = storage.GetValue<bool>("IsEnabled", false);
        this.SupportedCurrencies = storage.GetValue<string>("SupportedCurrencies", (string)null);
        this.Demo = storage.GetValue<PayGatewayDomainSettings>("Demo", (PayGatewayDomainSettings)null);
        this.Real = storage.GetValue<PayGatewayDomainSettings>("Real", (PayGatewayDomainSettings)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Domain>("Domain", this.Domain).Set<PayGateway>("Gateway", this.Gateway).Set<bool>("IsEnabled", this.IsEnabled).Set<string>("SupportedCurrencies", this.SupportedCurrencies).Set<PayGatewayDomainSettings>("Demo", this.Demo).Set<PayGatewayDomainSettings>("Real", this.Real);
    }
}
