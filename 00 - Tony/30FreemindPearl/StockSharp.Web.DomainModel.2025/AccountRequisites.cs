// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.AccountRequisites
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class AccountRequisites : BaseEntity, IClientEntity, INameEntity, IDescriptionEntity
    {
        public string BankId { get; set; }

        public string BankName { get; set; }

        public string AccountHolder { get; set; }

        public string AccountNo { get; set; }

        public Client Client { get; set; }

        public BaseEntitySet<Payment> Payments { get; set; }

        string INameEntity.Name
        {
            get
            {
                return this.AccountNo;
            }
            set
            {
                this.AccountNo = value;
            }
        }

        string IDescriptionEntity.Description { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.BankId = ( string ) storage.GetValue<string>( "BankId", null );
            this.BankName = ( string ) storage.GetValue<string>( "BankName", null );
            this.AccountHolder = ( string ) storage.GetValue<string>( "AccountHolder", null );
            this.AccountNo = ( string ) storage.GetValue<string>( "AccountNo", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Payments = ( BaseEntitySet<Payment> ) storage.GetValue<BaseEntitySet<Payment>>( "Payments", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "BankId", this.BankId ).Set<string>( "BankName", this.BankName ).Set<string>( "AccountHolder", this.AccountHolder ).Set<string>( "AccountNo", this.AccountNo ).Set<Client>( "Client", this.Client ).Set<BaseEntitySet<Payment>>( "Payments", this.Payments );
        }
    }
}
