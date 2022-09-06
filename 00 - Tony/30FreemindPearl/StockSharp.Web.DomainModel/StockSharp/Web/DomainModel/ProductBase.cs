using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class ProductBase : BaseEntity
    {
        public File Picture { get; set; }

        public Unit Cashback { get; set; }

        public Unit TopupFee { get; set; }

        public Unit WithdrawFee { get; set; }

        public Unit Discount { get; set; }

        public DateTime? DiscountEnd { get; set; }

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

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Picture                  = storage.GetValue("Picture", (File)null);
            Cashback                 = storage.GetValue("Cashback", (Unit)null);
            TopupFee                 = storage.GetValue("TopupFee", (Unit)null);
            WithdrawFee              = storage.GetValue("WithdrawFee", (Unit)null);
            Discount                 = storage.GetValue("Discount", (Unit)null);
            DiscountEnd              = storage.GetValue("DiscountEnd", new DateTime?());
            Managers                 = storage.GetValue("Managers", (BaseEntitySet<Client>)null);
            OrderCreated             = storage.GetValue("OrderCreated", (ProductBaseEmailTemplate)null);
            OrderPaid                = storage.GetValue("OrderPaid", (ProductBaseEmailTemplate)null);
            OrderExpired             = storage.GetValue("OrderExpired", (ProductBaseEmailTemplate)null);
            OrderExpiringMonth       = storage.GetValue("OrderExpiringMonth", (ProductBaseEmailTemplate)null);
            OrderExpiringWeek        = storage.GetValue("OrderExpiringWeek", (ProductBaseEmailTemplate)null);
            OrderExpiringDay3        = storage.GetValue("OrderExpiringDay3", (ProductBaseEmailTemplate)null);
            OrderExpiringDay1        = storage.GetValue("OrderExpiringDay1", (ProductBaseEmailTemplate)null);
            OrderExpiringManualMonth = storage.GetValue("OrderExpiringManualMonth", (ProductBaseEmailTemplate)null);
            OrderExpiringManualWeek  = storage.GetValue("OrderExpiringManualWeek", (ProductBaseEmailTemplate)null);
            OrderExpiringManualDay3  = storage.GetValue("OrderExpiringManualDay3", (ProductBaseEmailTemplate)null);
            OrderExpiringManualDay1  = storage.GetValue("OrderExpiringManualDay1", (ProductBaseEmailTemplate)null);
            OrderRefundRequested     = storage.GetValue("OrderRefundRequested", (ProductBaseEmailTemplate)null);
            OrderRefundApproved      = storage.GetValue("OrderRefundApproved", (ProductBaseEmailTemplate)null);
            OrderRefundRejected      = storage.GetValue("OrderRefundRejected", (ProductBaseEmailTemplate)null);
            OrderTrialRequested      = storage.GetValue("OrderTrialRequested", (ProductBaseEmailTemplate)null);
            OrderTrialApproved       = storage.GetValue("OrderTrialApproved", (ProductBaseEmailTemplate)null);
            OrderTrialRejected       = storage.GetValue("OrderTrialRejected", (ProductBaseEmailTemplate)null);
            ProductApproved          = storage.GetValue("ProductApproved", (ProductBaseEmailTemplate)null);
            ProductUnApproved        = storage.GetValue("ProductUnApproved", (ProductBaseEmailTemplate)null);
            ProductUpdated           = storage.GetValue("ProductUpdated", (ProductBaseEmailTemplate)null);
            ProductFeedback          = storage.GetValue("ProductFeedback", (ProductBaseEmailTemplate)null);
            ProductBugReport         = storage.GetValue("ProductBugReport", (ProductBaseEmailTemplate)null);
            ProductCreated           = storage.GetValue("ProductCreated", (ProductBaseEmailTemplate)null);
            ProductStarted           = storage.GetValue("ProductStarted", (ProductBaseEmailTemplate)null);
            ProductFinished          = storage.GetValue("ProductFinished", (ProductBaseEmailTemplate)null);
            ProductClientAdded       = storage.GetValue("ProductClientAdded", (ProductBaseEmailTemplate)null);
            ProductClientRemoved     = storage.GetValue("ProductClientRemoved", (ProductBaseEmailTemplate)null);
            ProductExecutorPay       = storage.GetValue("ProductExecutorPay", (ProductBaseEmailTemplate)null);
            PaymentRepeatAlready     = storage.GetValue("PaymentRepeatAlready", (ProductBaseEmailTemplate)null);
            PaymentFailed            = storage.GetValue("PaymentFailed", (ProductBaseEmailTemplate)null);
            PaymentRepeatFailed      = storage.GetValue("PaymentRepeatFailed", (ProductBaseEmailTemplate)null);
            OrderStoppedOk           = storage.GetValue("OrderStoppedOk", (ProductBaseEmailTemplate)null);
            OrderStoppedError        = storage.GetValue("OrderStoppedError", (ProductBaseEmailTemplate)null);
            ProductMoneyReserved     = storage.GetValue("ProductMoneyReserved", (ProductBaseEmailTemplate)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Picture", Picture).Set("Cashback", Cashback).Set("TopupFee", TopupFee).Set("WithdrawFee", WithdrawFee).Set("Discount", Discount).Set("DiscountEnd", DiscountEnd).Set("Managers", Managers).Set("OrderCreated", OrderCreated).Set("OrderPaid", OrderPaid).Set("OrderExpired", OrderExpired).Set("OrderExpiringMonth", OrderExpiringMonth).Set("OrderExpiringWeek", OrderExpiringWeek).Set("OrderExpiringDay3", OrderExpiringDay3).Set("OrderExpiringDay1", OrderExpiringDay1).Set("OrderExpiringManualMonth", OrderExpiringManualMonth).Set("OrderExpiringManualWeek", OrderExpiringManualWeek).Set("OrderExpiringManualDay3", OrderExpiringManualDay3).Set("OrderExpiringManualDay1", OrderExpiringManualDay1).Set("OrderRefundRequested", OrderRefundRequested).Set("OrderRefundApproved", OrderRefundApproved).Set("OrderRefundRejected", OrderRefundRejected).Set("OrderTrialRequested", OrderTrialRequested).Set("OrderTrialApproved", OrderTrialApproved).Set("OrderTrialRejected", OrderTrialRejected).Set("ProductApproved", ProductApproved).Set("ProductUnApproved", ProductUnApproved).Set("ProductUpdated", ProductUpdated).Set("ProductFeedback", ProductFeedback).Set("ProductBugReport", ProductBugReport).Set("ProductCreated", ProductCreated).Set("ProductStarted", ProductStarted).Set("ProductFinished", ProductFinished).Set("ProductClientAdded", ProductClientAdded).Set("ProductClientRemoved", ProductClientRemoved).Set("ProductExecutorPay", ProductExecutorPay).Set("PaymentRepeatAlready", PaymentRepeatAlready).Set("PaymentFailed", PaymentFailed).Set("PaymentRepeatFailed", PaymentRepeatFailed).Set("OrderStoppedOk", OrderStoppedOk).Set("OrderStoppedError", OrderStoppedError).Set("ProductMoneyReserved", ProductMoneyReserved);
        }
    }
}
