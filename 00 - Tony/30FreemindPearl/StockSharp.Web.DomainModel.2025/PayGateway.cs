// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PayGateway
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Code = ( string ) storage.GetValue<string>( "Code", null );
            this.Picture = ( File ) storage.GetValue<File>( "Picture", null );
            this.IpAddressList = ( string ) storage.GetValue<string>( "IpAddressList", null );
            this.Email = ( string ) storage.GetValue<string>( "Email", null );
            this.IsRepeatSupported = ( bool ) storage.GetValue<bool>( "IsRepeatSupported", false );
            this.Domains = ( BaseEntitySet<PayGatewayDomain> ) storage.GetValue<BaseEntitySet<PayGatewayDomain>>( "Domains", null );
            this.Payments = ( BaseEntitySet<Payment> ) storage.GetValue<BaseEntitySet<Payment>>( "Payments", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Name", this.Name ).Set<string>( "Code", this.Code ).Set<File>( "Picture", this.Picture ).Set<string>( "IpAddressList", this.IpAddressList ).Set<string>( "Email", this.Email ).Set<bool>( "IsRepeatSupported", ( this.IsRepeatSupported ) ).Set<BaseEntitySet<PayGatewayDomain>>( "Domains", this.Domains ).Set<BaseEntitySet<Payment>>( "Payments", this.Payments );
        }
    }
}
