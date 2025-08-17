// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PayGateway
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class PayGateway : BaseEntity, INameEntity, IDomainsEntity<PayGatewayDomain>, IPictureEntity
{
    public string Name { get; set; }

    public string Code { get; set; }

    public File Picture { get; set; }

    public string IpAddressList { get; set; }

    public string Email { get; set; }

    public bool IsRepeatSupported { get; set; }

    public BaseEntitySet<PayGatewayDomain> Domains { get; set; }

    public BaseEntitySet<Payment> Payments { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Code = storage.GetValue<string>("Code", (string)null);
        this.Picture = storage.GetValue<File>("Picture", (File)null);
        this.IpAddressList = storage.GetValue<string>("IpAddressList", (string)null);
        this.Email = storage.GetValue<string>("Email", (string)null);
        this.IsRepeatSupported = storage.GetValue<bool>("IsRepeatSupported", false);
        this.Domains = storage.GetValue<BaseEntitySet<PayGatewayDomain>>("Domains", (BaseEntitySet<PayGatewayDomain>)null);
        this.Payments = storage.GetValue<BaseEntitySet<Payment>>("Payments", (BaseEntitySet<Payment>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<string>("Code", this.Code).Set<File>("Picture", this.Picture).Set<string>("IpAddressList", this.IpAddressList).Set<string>("Email", this.Email).Set<bool>("IsRepeatSupported", this.IsRepeatSupported).Set<BaseEntitySet<PayGatewayDomain>>("Domains", this.Domains).Set<BaseEntitySet<Payment>>("Payments", this.Payments);
    }
}
