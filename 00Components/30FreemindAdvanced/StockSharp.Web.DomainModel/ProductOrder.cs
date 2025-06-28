// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductOrder
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class ProductOrder :
  BaseEntity,
  IClientEntity,
  IProductEntity,
  IMessageEntity,
  INameEntity,
  IDescriptionEntity
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
            var id = (ValueType)this.Id;
            Product product1 = this.Product;
            string str1;
            if (product1 == null)
            {
                str1 = (string)null;
            }
            else
            {
                string name2 = ((INameEntity)product1).Name;
                Product product2 = this.Product;
                string str2 = product2 != null ? Converter.To<string>((product2.Id)) : (string)null;
                str1 = StringHelper.IsEmpty(name2, str2);
            }
            string str3 = $"Order {id} Product {str1}";
            return StringHelper.IsEmpty(name1, str3);
        }
        set => this._name = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Product = storage.GetValue<Product>("Product", (Product)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Amount = storage.GetValue<Decimal>("Amount", 0M);
        this.Description = storage.GetValue<string>("Description", (string)null);
        this.SubscriptionStart = storage.GetValue<DateTime?>("SubscriptionStart", new DateTime?());
        this.SubscriptionEnd = storage.GetValue<DateTime?>("SubscriptionEnd", new DateTime?());
        this.HardwareId = storage.GetValue<string>("HardwareId", (string)null);
        this.Account = storage.GetValue<string>("Account", (string)null);
        this.RepeatAmount = storage.GetValue<Decimal>("RepeatAmount", 0M);
        this.RepeatToken = storage.GetValue<string>("RepeatToken", (string)null);
        this.Currency = storage.GetValue<CurrencyTypes>("Currency", (CurrencyTypes)0);
        this.Message = storage.GetValue<Message>("Message", (Message)null);
        this.Referral = storage.GetValue<ProductGroupReferral>("Referral", (ProductGroupReferral)null);
        this.ReferralClient = storage.GetValue<Client>("ReferralClient", (Client)null);
        this.PriceType = storage.GetValue<ProductPriceTypes>("PriceType", ProductPriceTypes.Lifetime);
        this.Flags = storage.GetValue<ProductOrderFlags>("Flags", ProductOrderFlags.None);
        this.PayUrl = storage.GetValue<string>("PayUrl", (string)null);
        this.OneApp = storage.GetValue<Product>("OneApp", (Product)null);
        this.Payments = storage.GetValue<BaseEntitySet<Payment>>("Payments", (BaseEntitySet<Payment>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Product>("Product", this.Product).Set<Client>("Client", this.Client).Set<Decimal>("Amount", this.Amount).Set<string>("Description", this.Description).Set<DateTime?>("SubscriptionStart", this.SubscriptionStart).Set<DateTime?>("SubscriptionEnd", this.SubscriptionEnd).Set<string>("HardwareId", this.HardwareId).Set<string>("Account", this.Account).Set<Decimal>("RepeatAmount", this.RepeatAmount).Set<string>("RepeatToken", this.RepeatToken).Set<CurrencyTypes>("Currency", this.Currency).Set<Message>("Message", this.Message).Set<ProductGroupReferral>("Referral", this.Referral).Set<Client>("ReferralClient", this.ReferralClient).Set<ProductPriceTypes>("PriceType", this.PriceType).Set<ProductOrderFlags>("Flags", this.Flags).Set<string>("PayUrl", this.PayUrl).Set<Product>("OneApp", this.OneApp).Set<BaseEntitySet<Payment>>("Payments", this.Payments);
    }
}
