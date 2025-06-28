// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductBase
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.ComponentModel;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

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

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Picture = storage.GetValue<File>("Picture", (File)null);
        this.Cashback = storage.GetValue<Price>("Cashback", (Price)null);
        this.TopupFee = storage.GetValue<Price>("TopupFee", (Price)null);
        this.WithdrawFee = storage.GetValue<Price>("WithdrawFee", (Price)null);
        this.Discount = storage.GetValue<Price>("Discount", (Price)null);
        this.DiscountEnd = storage.GetValue<DateTime?>("DiscountEnd", new DateTime?());
        this.TrialPeriod = storage.GetValue<TimeSpan?>("TrialPeriod", new TimeSpan?());
        this.RawTrialPeriod = storage.GetValue<TimeSpan?>("RawTrialPeriod", new TimeSpan?());
        this.Managers = storage.GetValue<BaseEntitySet<Client>>("Managers", (BaseEntitySet<Client>)null);
        this.OrderCreated = storage.GetValue<ProductBaseEmailTemplate>("OrderCreated", (ProductBaseEmailTemplate)null);
        this.OrderPaid = storage.GetValue<ProductBaseEmailTemplate>("OrderPaid", (ProductBaseEmailTemplate)null);
        this.OrderExpired = storage.GetValue<ProductBaseEmailTemplate>("OrderExpired", (ProductBaseEmailTemplate)null);
        this.OrderExpiringMonth = storage.GetValue<ProductBaseEmailTemplate>("OrderExpiringMonth", (ProductBaseEmailTemplate)null);
        this.OrderExpiringWeek = storage.GetValue<ProductBaseEmailTemplate>("OrderExpiringWeek", (ProductBaseEmailTemplate)null);
        this.OrderExpiringDay3 = storage.GetValue<ProductBaseEmailTemplate>("OrderExpiringDay3", (ProductBaseEmailTemplate)null);
        this.OrderExpiringDay1 = storage.GetValue<ProductBaseEmailTemplate>("OrderExpiringDay1", (ProductBaseEmailTemplate)null);
        this.OrderExpiringManualMonth = storage.GetValue<ProductBaseEmailTemplate>("OrderExpiringManualMonth", (ProductBaseEmailTemplate)null);
        this.OrderExpiringManualWeek = storage.GetValue<ProductBaseEmailTemplate>("OrderExpiringManualWeek", (ProductBaseEmailTemplate)null);
        this.OrderExpiringManualDay3 = storage.GetValue<ProductBaseEmailTemplate>("OrderExpiringManualDay3", (ProductBaseEmailTemplate)null);
        this.OrderExpiringManualDay1 = storage.GetValue<ProductBaseEmailTemplate>("OrderExpiringManualDay1", (ProductBaseEmailTemplate)null);
        this.OrderRefundRequested = storage.GetValue<ProductBaseEmailTemplate>("OrderRefundRequested", (ProductBaseEmailTemplate)null);
        this.OrderRefundApproved = storage.GetValue<ProductBaseEmailTemplate>("OrderRefundApproved", (ProductBaseEmailTemplate)null);
        this.OrderRefundRejected = storage.GetValue<ProductBaseEmailTemplate>("OrderRefundRejected", (ProductBaseEmailTemplate)null);
        this.OrderTrialRequested = storage.GetValue<ProductBaseEmailTemplate>("OrderTrialRequested", (ProductBaseEmailTemplate)null);
        this.OrderTrialApproved = storage.GetValue<ProductBaseEmailTemplate>("OrderTrialApproved", (ProductBaseEmailTemplate)null);
        this.OrderTrialRejected = storage.GetValue<ProductBaseEmailTemplate>("OrderTrialRejected", (ProductBaseEmailTemplate)null);
        this.ProductApproved = storage.GetValue<ProductBaseEmailTemplate>("ProductApproved", (ProductBaseEmailTemplate)null);
        this.ProductUnApproved = storage.GetValue<ProductBaseEmailTemplate>("ProductUnApproved", (ProductBaseEmailTemplate)null);
        this.ProductUpdated = storage.GetValue<ProductBaseEmailTemplate>("ProductUpdated", (ProductBaseEmailTemplate)null);
        this.ProductFeedback = storage.GetValue<ProductBaseEmailTemplate>("ProductFeedback", (ProductBaseEmailTemplate)null);
        this.ProductBugReport = storage.GetValue<ProductBaseEmailTemplate>("ProductBugReport", (ProductBaseEmailTemplate)null);
        this.ProductCreated = storage.GetValue<ProductBaseEmailTemplate>("ProductCreated", (ProductBaseEmailTemplate)null);
        this.ProductStarted = storage.GetValue<ProductBaseEmailTemplate>("ProductStarted", (ProductBaseEmailTemplate)null);
        this.ProductFinished = storage.GetValue<ProductBaseEmailTemplate>("ProductFinished", (ProductBaseEmailTemplate)null);
        this.ProductClientAdded = storage.GetValue<ProductBaseEmailTemplate>("ProductClientAdded", (ProductBaseEmailTemplate)null);
        this.ProductClientRemoved = storage.GetValue<ProductBaseEmailTemplate>("ProductClientRemoved", (ProductBaseEmailTemplate)null);
        this.ProductExecutorPay = storage.GetValue<ProductBaseEmailTemplate>("ProductExecutorPay", (ProductBaseEmailTemplate)null);
        this.PaymentRepeatAlready = storage.GetValue<ProductBaseEmailTemplate>("PaymentRepeatAlready", (ProductBaseEmailTemplate)null);
        this.PaymentFailed = storage.GetValue<ProductBaseEmailTemplate>("PaymentFailed", (ProductBaseEmailTemplate)null);
        this.PaymentRepeatFailed = storage.GetValue<ProductBaseEmailTemplate>("PaymentRepeatFailed", (ProductBaseEmailTemplate)null);
        this.OrderStoppedOk = storage.GetValue<ProductBaseEmailTemplate>("OrderStoppedOk", (ProductBaseEmailTemplate)null);
        this.OrderStoppedError = storage.GetValue<ProductBaseEmailTemplate>("OrderStoppedError", (ProductBaseEmailTemplate)null);
        this.ProductMoneyReserved = storage.GetValue<ProductBaseEmailTemplate>("ProductMoneyReserved", (ProductBaseEmailTemplate)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<File>("Picture", this.Picture).Set<Price>("Cashback", this.Cashback).Set<Price>("TopupFee", this.TopupFee).Set<Price>("WithdrawFee", this.WithdrawFee).Set<Price>("Discount", this.Discount).Set<DateTime?>("DiscountEnd", this.DiscountEnd).Set<TimeSpan?>("TrialPeriod", this.TrialPeriod).Set<TimeSpan?>("RawTrialPeriod", this.RawTrialPeriod).Set<BaseEntitySet<Client>>("Managers", this.Managers).Set<ProductBaseEmailTemplate>("OrderCreated", this.OrderCreated).Set<ProductBaseEmailTemplate>("OrderPaid", this.OrderPaid).Set<ProductBaseEmailTemplate>("OrderExpired", this.OrderExpired).Set<ProductBaseEmailTemplate>("OrderExpiringMonth", this.OrderExpiringMonth).Set<ProductBaseEmailTemplate>("OrderExpiringWeek", this.OrderExpiringWeek).Set<ProductBaseEmailTemplate>("OrderExpiringDay3", this.OrderExpiringDay3).Set<ProductBaseEmailTemplate>("OrderExpiringDay1", this.OrderExpiringDay1).Set<ProductBaseEmailTemplate>("OrderExpiringManualMonth", this.OrderExpiringManualMonth).Set<ProductBaseEmailTemplate>("OrderExpiringManualWeek", this.OrderExpiringManualWeek).Set<ProductBaseEmailTemplate>("OrderExpiringManualDay3", this.OrderExpiringManualDay3).Set<ProductBaseEmailTemplate>("OrderExpiringManualDay1", this.OrderExpiringManualDay1).Set<ProductBaseEmailTemplate>("OrderRefundRequested", this.OrderRefundRequested).Set<ProductBaseEmailTemplate>("OrderRefundApproved", this.OrderRefundApproved).Set<ProductBaseEmailTemplate>("OrderRefundRejected", this.OrderRefundRejected).Set<ProductBaseEmailTemplate>("OrderTrialRequested", this.OrderTrialRequested).Set<ProductBaseEmailTemplate>("OrderTrialApproved", this.OrderTrialApproved).Set<ProductBaseEmailTemplate>("OrderTrialRejected", this.OrderTrialRejected).Set<ProductBaseEmailTemplate>("ProductApproved", this.ProductApproved).Set<ProductBaseEmailTemplate>("ProductUnApproved", this.ProductUnApproved).Set<ProductBaseEmailTemplate>("ProductUpdated", this.ProductUpdated).Set<ProductBaseEmailTemplate>("ProductFeedback", this.ProductFeedback).Set<ProductBaseEmailTemplate>("ProductBugReport", this.ProductBugReport).Set<ProductBaseEmailTemplate>("ProductCreated", this.ProductCreated).Set<ProductBaseEmailTemplate>("ProductStarted", this.ProductStarted).Set<ProductBaseEmailTemplate>("ProductFinished", this.ProductFinished).Set<ProductBaseEmailTemplate>("ProductClientAdded", this.ProductClientAdded).Set<ProductBaseEmailTemplate>("ProductClientRemoved", this.ProductClientRemoved).Set<ProductBaseEmailTemplate>("ProductExecutorPay", this.ProductExecutorPay).Set<ProductBaseEmailTemplate>("PaymentRepeatAlready", this.PaymentRepeatAlready).Set<ProductBaseEmailTemplate>("PaymentFailed", this.PaymentFailed).Set<ProductBaseEmailTemplate>("PaymentRepeatFailed", this.PaymentRepeatFailed).Set<ProductBaseEmailTemplate>("OrderStoppedOk", this.OrderStoppedOk).Set<ProductBaseEmailTemplate>("OrderStoppedError", this.OrderStoppedError).Set<ProductBaseEmailTemplate>("ProductMoneyReserved", this.ProductMoneyReserved);
    }
}
