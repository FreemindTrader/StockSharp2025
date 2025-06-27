// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Payment
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class Payment : BaseEntity, INameEntity, IDescriptionEntity, IStateEntity<PaymentStates>
    {
        public string Description { get; set; }

        public Decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Client From { get; set; }

        public Client To { get; set; }

        public Domain Domain { get; set; }

        public string PaySystemId { get; set; }

        public PaymentStates State { get; set; }

        public CurrencyTypes Currency { get; set; }

        public AccountRequisites BankAccount { get; set; }

        public ProductOrder ProductOrder { get; set; }

        public bool IsTest { get; set; }

        public PayGatewayDomain GatewayDomain { get; set; }

        public bool IsRecurrent { get; set; }

        public bool IsConfirmRequired { get; set; }

        public string StatusUrlPart { get; set; }

        public bool? IsApproveAllow { get; set; }

        public bool? IsCancelAllow { get; set; }

        public bool? IsAutoActionsAllow { get; set; }

        public FileGroup Files { get; set; }

        public BaseEntitySet<Error> Errors { get; set; }

        string INameEntity.Name { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.Amount = ( Decimal ) storage.GetValue<Decimal>( "Amount", Decimal.Zero );
            this.Date = ( DateTime ) storage.GetValue<DateTime>( "Date", new DateTime() );
            this.From = ( Client ) storage.GetValue<Client>( "From", null );
            this.To = ( Client ) storage.GetValue<Client>( "To", null );
            this.Domain = ( Domain ) storage.GetValue<Domain>( "Domain", null );
            this.PaySystemId = ( string ) storage.GetValue<string>( "PaySystemId", null );
            this.State = ( PaymentStates ) storage.GetValue<PaymentStates>( "State", 0 );
            this.Currency = ( CurrencyTypes ) storage.GetValue<CurrencyTypes>( "Currency", 0 );
            this.BankAccount = ( AccountRequisites ) storage.GetValue<AccountRequisites>( "BankAccount", null );
            this.ProductOrder = ( ProductOrder ) storage.GetValue<ProductOrder>( "ProductOrder", null );
            this.IsTest = ( bool ) storage.GetValue<bool>( "IsTest", false );
            this.GatewayDomain = ( PayGatewayDomain ) storage.GetValue<PayGatewayDomain>( "GatewayDomain", null );
            this.IsRecurrent = ( bool ) storage.GetValue<bool>( "IsRecurrent", false );
            this.IsConfirmRequired = ( bool ) storage.GetValue<bool>( "IsConfirmRequired", false );
            this.StatusUrlPart = ( string ) storage.GetValue<string>( "StatusUrlPart", null );
            this.IsApproveAllow = ( bool? ) storage.GetValue<bool?>( "IsApproveAllow", new bool?() );
            this.IsCancelAllow = ( bool? ) storage.GetValue<bool?>( "IsCancelAllow", new bool?() );
            this.IsAutoActionsAllow = ( bool? ) storage.GetValue<bool?>( "IsAutoActionsAllow", new bool?() );
            this.Files = ( FileGroup ) storage.GetValue<FileGroup>( "Files", null );
            this.Errors = ( BaseEntitySet<Error> ) storage.GetValue<BaseEntitySet<Error>>( "Errors", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Description", this.Description ).Set<Decimal>( "Amount", this.Amount ).Set<DateTime>( "Date", this.Date ).Set<Client>( "From", this.From ).Set<Client>( "To", this.To ).Set<Domain>( "Domain", this.Domain ).Set<string>( "PaySystemId", this.PaySystemId ).Set<PaymentStates>( "State", this.State ).Set<CurrencyTypes>( "Currency", this.Currency ).Set<AccountRequisites>( "BankAccount", this.BankAccount ).Set<ProductOrder>( "ProductOrder", this.ProductOrder ).Set<bool>( "IsTest", ( this.IsTest ) ).Set<PayGatewayDomain>( "GatewayDomain", this.GatewayDomain ).Set<bool>( "IsRecurrent", ( this.IsRecurrent ) ).Set<bool>( "IsConfirmRequired", ( this.IsConfirmRequired ) ).Set<string>( "StatusUrlPart", this.StatusUrlPart ).Set<bool?>( "IsApproveAllow", this.IsApproveAllow ).Set<bool?>( "IsCancelAllow", this.IsCancelAllow ).Set<bool?>( "IsAutoActionsAllow", this.IsAutoActionsAllow ).Set<FileGroup>( "Files", this.Files ).Set<BaseEntitySet<Error>>( "Errors", this.Errors );
        }
    }
}
