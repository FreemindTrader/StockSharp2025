// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductBase
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public abstract class ProductBase : BaseEntity, IPictureEntity
    {
        public File Picture { get; set; }

        public Price Cashback { get; set; }

        public Price TopupFee { get; set; }

        public Price WithdrawFee { get; set; }

        public Price Discount { get; set; }

        public DateTime? DiscountEnd { get; set; }

        public TimeSpan? TrialPeriod { get; set; }

        public TimeSpan? RawTrialPeriod { get; set; }

        public BaseEntitySet<Client> Managers { get; set; }

        public ProductBaseEmailTemplate OrderCreated { get; set; }

        public ProductBaseEmailTemplate OrderPaid { get; set; }

        public ProductBaseEmailTemplate OrderExpired { get; set; }

        public ProductBaseEmailTemplate OrderExpiringMonth { get; set; }

        public ProductBaseEmailTemplate OrderExpiringWeek { get; set; }

        public ProductBaseEmailTemplate OrderExpiringDay3 { get; set; }

        public ProductBaseEmailTemplate OrderExpiringDay1 { get; set; }

        public ProductBaseEmailTemplate OrderExpiringManualMonth { get; set; }

        public ProductBaseEmailTemplate OrderExpiringManualWeek { get; set; }

        public ProductBaseEmailTemplate OrderExpiringManualDay3 { get; set; }

        public ProductBaseEmailTemplate OrderExpiringManualDay1 { get; set; }

        public ProductBaseEmailTemplate OrderRefundRequested { get; set; }

        public ProductBaseEmailTemplate OrderRefundApproved { get; set; }

        public ProductBaseEmailTemplate OrderRefundRejected { get; set; }

        public ProductBaseEmailTemplate OrderTrialRequested { get; set; }

        public ProductBaseEmailTemplate OrderTrialApproved { get; set; }

        public ProductBaseEmailTemplate OrderTrialRejected { get; set; }

        public ProductBaseEmailTemplate ProductApproved { get; set; }

        public ProductBaseEmailTemplate ProductUnApproved { get; set; }

        public ProductBaseEmailTemplate ProductUpdated { get; set; }

        public ProductBaseEmailTemplate ProductFeedback { get; set; }

        public ProductBaseEmailTemplate ProductBugReport { get; set; }

        public ProductBaseEmailTemplate ProductCreated { get; set; }

        public ProductBaseEmailTemplate ProductStarted { get; set; }

        public ProductBaseEmailTemplate ProductFinished { get; set; }

        public ProductBaseEmailTemplate ProductClientAdded { get; set; }

        public ProductBaseEmailTemplate ProductClientRemoved { get; set; }

        public ProductBaseEmailTemplate ProductExecutorPay { get; set; }

        public ProductBaseEmailTemplate PaymentRepeatAlready { get; set; }

        public ProductBaseEmailTemplate PaymentFailed { get; set; }

        public ProductBaseEmailTemplate PaymentRepeatFailed { get; set; }

        public ProductBaseEmailTemplate OrderStoppedOk { get; set; }

        public ProductBaseEmailTemplate OrderStoppedError { get; set; }

        public ProductBaseEmailTemplate ProductMoneyReserved { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Picture = ( File ) storage.GetValue<File>( "Picture", null );
            this.Cashback = ( Price ) storage.GetValue<Price>( "Cashback", null );
            this.TopupFee = ( Price ) storage.GetValue<Price>( "TopupFee", null );
            this.WithdrawFee = ( Price ) storage.GetValue<Price>( "WithdrawFee", null );
            this.Discount = ( Price ) storage.GetValue<Price>( "Discount", null );
            this.DiscountEnd = ( DateTime? ) storage.GetValue<DateTime?>( "DiscountEnd", new DateTime?() );
            this.TrialPeriod = ( TimeSpan? ) storage.GetValue<TimeSpan?>( "TrialPeriod", new TimeSpan?() );
            this.RawTrialPeriod = ( TimeSpan? ) storage.GetValue<TimeSpan?>( "RawTrialPeriod", new TimeSpan?() );
            this.Managers = ( BaseEntitySet<Client> ) storage.GetValue<BaseEntitySet<Client>>( "Managers", null );
            this.OrderCreated = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderCreated", null );
            this.OrderPaid = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderPaid", null );
            this.OrderExpired = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpired", null );
            this.OrderExpiringMonth = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpiringMonth", null );
            this.OrderExpiringWeek = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpiringWeek", null );
            this.OrderExpiringDay3 = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpiringDay3", null );
            this.OrderExpiringDay1 = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpiringDay1", null );
            this.OrderExpiringManualMonth = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpiringManualMonth", null );
            this.OrderExpiringManualWeek = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpiringManualWeek", null );
            this.OrderExpiringManualDay3 = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpiringManualDay3", null );
            this.OrderExpiringManualDay1 = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderExpiringManualDay1", null );
            this.OrderRefundRequested = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderRefundRequested", null );
            this.OrderRefundApproved = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderRefundApproved", null );
            this.OrderRefundRejected = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderRefundRejected", null );
            this.OrderTrialRequested = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderTrialRequested", null );
            this.OrderTrialApproved = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderTrialApproved", null );
            this.OrderTrialRejected = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderTrialRejected", null );
            this.ProductApproved = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductApproved", null );
            this.ProductUnApproved = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductUnApproved", null );
            this.ProductUpdated = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductUpdated", null );
            this.ProductFeedback = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductFeedback", null );
            this.ProductBugReport = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductBugReport", null );
            this.ProductCreated = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductCreated", null );
            this.ProductStarted = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductStarted", null );
            this.ProductFinished = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductFinished", null );
            this.ProductClientAdded = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductClientAdded", null );
            this.ProductClientRemoved = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductClientRemoved", null );
            this.ProductExecutorPay = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductExecutorPay", null );
            this.PaymentRepeatAlready = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "PaymentRepeatAlready", null );
            this.PaymentFailed = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "PaymentFailed", null );
            this.PaymentRepeatFailed = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "PaymentRepeatFailed", null );
            this.OrderStoppedOk = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderStoppedOk", null );
            this.OrderStoppedError = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "OrderStoppedError", null );
            this.ProductMoneyReserved = ( ProductBaseEmailTemplate ) storage.GetValue<ProductBaseEmailTemplate>( "ProductMoneyReserved", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<File>( "Picture", this.Picture ).Set<Price>( "Cashback", this.Cashback ).Set<Price>( "TopupFee", this.TopupFee ).Set<Price>( "WithdrawFee", this.WithdrawFee ).Set<Price>( "Discount", this.Discount ).Set<DateTime?>( "DiscountEnd", this.DiscountEnd ).Set<TimeSpan?>( "TrialPeriod", this.TrialPeriod ).Set<TimeSpan?>( "RawTrialPeriod", this.RawTrialPeriod ).Set<BaseEntitySet<Client>>( "Managers", this.Managers ).Set<ProductBaseEmailTemplate>( "OrderCreated", this.OrderCreated ).Set<ProductBaseEmailTemplate>( "OrderPaid", this.OrderPaid ).Set<ProductBaseEmailTemplate>( "OrderExpired", this.OrderExpired ).Set<ProductBaseEmailTemplate>( "OrderExpiringMonth", this.OrderExpiringMonth ).Set<ProductBaseEmailTemplate>( "OrderExpiringWeek", this.OrderExpiringWeek ).Set<ProductBaseEmailTemplate>( "OrderExpiringDay3", this.OrderExpiringDay3 ).Set<ProductBaseEmailTemplate>( "OrderExpiringDay1", this.OrderExpiringDay1 ).Set<ProductBaseEmailTemplate>( "OrderExpiringManualMonth", this.OrderExpiringManualMonth ).Set<ProductBaseEmailTemplate>( "OrderExpiringManualWeek", this.OrderExpiringManualWeek ).Set<ProductBaseEmailTemplate>( "OrderExpiringManualDay3", this.OrderExpiringManualDay3 ).Set<ProductBaseEmailTemplate>( "OrderExpiringManualDay1", this.OrderExpiringManualDay1 ).Set<ProductBaseEmailTemplate>( "OrderRefundRequested", this.OrderRefundRequested ).Set<ProductBaseEmailTemplate>( "OrderRefundApproved", this.OrderRefundApproved ).Set<ProductBaseEmailTemplate>( "OrderRefundRejected", this.OrderRefundRejected ).Set<ProductBaseEmailTemplate>( "OrderTrialRequested", this.OrderTrialRequested ).Set<ProductBaseEmailTemplate>( "OrderTrialApproved", this.OrderTrialApproved ).Set<ProductBaseEmailTemplate>( "OrderTrialRejected", this.OrderTrialRejected ).Set<ProductBaseEmailTemplate>( "ProductApproved", this.ProductApproved ).Set<ProductBaseEmailTemplate>( "ProductUnApproved", this.ProductUnApproved ).Set<ProductBaseEmailTemplate>( "ProductUpdated", this.ProductUpdated ).Set<ProductBaseEmailTemplate>( "ProductFeedback", this.ProductFeedback ).Set<ProductBaseEmailTemplate>( "ProductBugReport", this.ProductBugReport ).Set<ProductBaseEmailTemplate>( "ProductCreated", this.ProductCreated ).Set<ProductBaseEmailTemplate>( "ProductStarted", this.ProductStarted ).Set<ProductBaseEmailTemplate>( "ProductFinished", this.ProductFinished ).Set<ProductBaseEmailTemplate>( "ProductClientAdded", this.ProductClientAdded ).Set<ProductBaseEmailTemplate>( "ProductClientRemoved", this.ProductClientRemoved ).Set<ProductBaseEmailTemplate>( "ProductExecutorPay", this.ProductExecutorPay ).Set<ProductBaseEmailTemplate>( "PaymentRepeatAlready", this.PaymentRepeatAlready ).Set<ProductBaseEmailTemplate>( "PaymentFailed", this.PaymentFailed ).Set<ProductBaseEmailTemplate>( "PaymentRepeatFailed", this.PaymentRepeatFailed ).Set<ProductBaseEmailTemplate>( "OrderStoppedOk", this.OrderStoppedOk ).Set<ProductBaseEmailTemplate>( "OrderStoppedError", this.OrderStoppedError ).Set<ProductBaseEmailTemplate>( "ProductMoneyReserved", this.ProductMoneyReserved );
        }
    }
}
