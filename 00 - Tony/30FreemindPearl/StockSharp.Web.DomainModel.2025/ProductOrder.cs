// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductOrder
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductOrder : BaseEntity, IClientEntity, IProductEntity, IMessageEntity, INameEntity, IDescriptionEntity
    {
        private string _name;

        public Product Product { get; set; }

        public Client Client { get; set; }

        public Decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime? SubscriptionStart { get; set; }

        public DateTime? SubscriptionEnd { get; set; }

        public string HardwareId { get; set; }

        public string Account { get; set; }

        public Decimal RepeatAmount { get; set; }

        public string RepeatToken { get; set; }

        public CurrencyTypes Currency { get; set; }

        public Message Message { get; set; }

        public ProductGroupReferral Referral { get; set; }

        public Client ReferralClient { get; set; }

        public ProductPriceTypes PriceType { get; set; }

        public ProductOrderFlags Flags { get; set; }

        public string PayUrl { get; set; }

        public Product OneApp { get; set; }

        public BaseEntitySet<Payment> Payments { get; set; }

        string INameEntity.Name
        {
            get
            {
                string name1 = this._name;
                var id = (ValueType) this.Id;
                Product product1 = this.Product;
                string str1;
                if ( product1 == null )
                {
                    str1 = ( string ) null;
                }
                else
                {
                    string name2 = ((INameEntity) product1).Name;
                    Product product2 = this.Product;
                    var m0 = product2 != null ? Converter.To<string>( (product2.Id)) : null;
                    str1 = StringHelper.IsEmpty( name2, ( string ) m0 );
                }
                string str2 = string.Format("Order {0} Product {1}",  id,  str1);
                return StringHelper.IsEmpty( name1, str2 );
            }
            set
            {
                this._name = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Product = ( Product ) storage.GetValue<Product>( "Product", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Amount = ( Decimal ) storage.GetValue<Decimal>( "Amount", Decimal.Zero );
            this.Description = ( string ) storage.GetValue<string>( "Description", null );
            this.SubscriptionStart = ( DateTime? ) storage.GetValue<DateTime?>( "SubscriptionStart", new DateTime?() );
            this.SubscriptionEnd = ( DateTime? ) storage.GetValue<DateTime?>( "SubscriptionEnd", new DateTime?() );
            this.HardwareId = ( string ) storage.GetValue<string>( "HardwareId", null );
            this.Account = ( string ) storage.GetValue<string>( "Account", null );
            this.RepeatAmount = ( Decimal ) storage.GetValue<Decimal>( "RepeatAmount", Decimal.Zero );
            this.RepeatToken = ( string ) storage.GetValue<string>( "RepeatToken", null );
            this.Currency = ( CurrencyTypes ) storage.GetValue<CurrencyTypes>( "Currency", 0 );
            this.Message = ( Message ) storage.GetValue<Message>( "Message", null );
            this.Referral = ( ProductGroupReferral ) storage.GetValue<ProductGroupReferral>( "Referral", null );
            this.ReferralClient = ( Client ) storage.GetValue<Client>( "ReferralClient", null );
            this.PriceType = ( ProductPriceTypes ) storage.GetValue<ProductPriceTypes>( "PriceType", 0 );
            this.Flags = ( ProductOrderFlags ) storage.GetValue<ProductOrderFlags>( "Flags", 0 );
            this.PayUrl = ( string ) storage.GetValue<string>( "PayUrl", null );
            this.OneApp = ( Product ) storage.GetValue<Product>( "OneApp", null );
            this.Payments = ( BaseEntitySet<Payment> ) storage.GetValue<BaseEntitySet<Payment>>( "Payments", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Product>( "Product", this.Product ).Set<Client>( "Client", this.Client ).Set<Decimal>( "Amount", this.Amount ).Set<string>( "Description", this.Description ).Set<DateTime?>( "SubscriptionStart", this.SubscriptionStart ).Set<DateTime?>( "SubscriptionEnd", this.SubscriptionEnd ).Set<string>( "HardwareId", this.HardwareId ).Set<string>( "Account", this.Account ).Set<Decimal>( "RepeatAmount", this.RepeatAmount ).Set<string>( "RepeatToken", this.RepeatToken ).Set<CurrencyTypes>( "Currency", this.Currency ).Set<Message>( "Message", this.Message ).Set<ProductGroupReferral>( "Referral", this.Referral ).Set<Client>( "ReferralClient", this.ReferralClient ).Set<ProductPriceTypes>( "PriceType", this.PriceType ).Set<ProductOrderFlags>( "Flags", this.Flags ).Set<string>( "PayUrl", this.PayUrl ).Set<Product>( "OneApp", this.OneApp ).Set<BaseEntitySet<Payment>>( "Payments", this.Payments );
        }
    }
}
