// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.ProductExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public static class ProductExtensions
{
    public static readonly DateTime ExpiredTill = TimeHelper.UtcKind(new DateTime(2000, 1, 1));
    public static readonly DateTime NeverExpiredTill = TimeHelper.UtcKind(new DateTime(2100, 1, 1));

    public static bool IsPackageIdRequired(this ProductContentTypes2 type)
    {
        bool flag;
        switch (type)
        {
            case ProductContentTypes2.SourceCode:
            case ProductContentTypes2.CompiledAssembly:
            case ProductContentTypes2.Schema:
            case ProductContentTypes2.StandaloneApp:
                flag = true;
                break;
            default:
                flag = false;
                break;
        }
        return flag;
    }

    public static bool IsDevelopmentOrFreelance(this Product product)
    {
        return product.IsDevelopment.GetValueOrDefault() || product.IsFreelance.GetValueOrDefault();
    }

    public static TimeSpan? GetLength(this ProductOrder order)
    {
        DateTime? nullable = order != null ? order.SubscriptionEnd : throw new ArgumentNullException(nameof(order));
        DateTime? subscriptionStart = order.SubscriptionStart;
        return !(nullable.HasValue & subscriptionStart.HasValue) ? new TimeSpan?() : new TimeSpan?(nullable.GetValueOrDefault() - subscriptionStart.GetValueOrDefault());
    }

    public static TimeSpan? GetRemains(this ProductOrder order)
    {
        DateTime? nullable1 = order != null ? order.SubscriptionEnd : throw new ArgumentNullException(nameof(order));
        DateTime utcNow = DateTime.UtcNow;
        TimeSpan? remains = nullable1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - utcNow) : new TimeSpan?();
        TimeSpan? nullable2 = remains;
        TimeSpan zero = TimeSpan.Zero;
        if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < zero ? 1 : 0) : 0) != 0)
            remains = new TimeSpan?(TimeSpan.Zero);
        return remains;
    }

    public static bool IsTrialApproveRequired(this ProductOrder order)
    {
        return order.IsTrial() && !order.SubscriptionEnd.HasValue;
    }

    public static bool IsTrial(this ProductOrder order) => order.Is(ProductOrderFlags.Trial);

    public static bool IsRefund(this ProductOrder order) => order.Is(ProductOrderFlags.Refund);

    public static bool IsTest(this ProductOrder order) => order.Is(ProductOrderFlags.Test);

    public static bool IsCancelled(this ProductOrder order) => order.Is(ProductOrderFlags.Cancelled);

    public static bool Is(this ProductOrder order, ProductOrderFlags flags)
    {
        return TypeHelper.CheckOnNull<ProductOrder>(order, nameof(order)).Flags.HasFlag((Enum)flags);
    }

    public static ProductOrder SetTrial(this ProductOrder order, bool value)
    {
        return order.Set(ProductOrderFlags.Trial, value);
    }

    public static ProductOrder SetRefund(this ProductOrder order, bool value)
    {
        return order.Set(ProductOrderFlags.Refund, value);
    }

    public static ProductOrder SetTest(this ProductOrder order, bool value)
    {
        return order.Set(ProductOrderFlags.Test, value);
    }

    public static ProductOrder SetCancelled(this ProductOrder order, bool value)
    {
        return order.Set(ProductOrderFlags.Cancelled, value);
    }

    public static ProductOrder Set(this ProductOrder order, ProductOrderFlags flags, bool value)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        if (value)
            order.Flags |= flags;
        else
            order.Flags = Enumerator.Remove<ProductOrderFlags>((Enum)order.Flags, flags);
        return order;
    }

    public static bool IsRepeat(this ProductOrder order)
    {
        return TypeHelper.CheckOnNull<ProductOrder>(order, nameof(order)).RepeatAmount != 0M;
    }

    public static bool IsRecurrent(this ProductOrder order)
    {
        return !StringHelper.IsEmpty(TypeHelper.CheckOnNull<ProductOrder>(order, nameof(order)).RepeatToken);
    }

    public static bool CanRenew(this ProductOrder order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        return order.PriceType.CanRenew(order.SubscriptionEnd);
    }

    public static bool CanRenew(this ProductPriceTypes type, DateTime? subscriptionEnd)
    {
        switch (type)
        {
            case ProductPriceTypes.Lifetime:
                return false;
            case ProductPriceTypes.PerMonth:
                DateTime dateTime1 = DateTime.UtcNow.AddDays(7.0);
                DateTime? nullable1 = subscriptionEnd;
                return nullable1.HasValue && dateTime1 >= nullable1.GetValueOrDefault();
            case ProductPriceTypes.Annual:
                DateTime dateTime2 = DateTime.UtcNow.AddMonths(1);
                DateTime? nullable2 = subscriptionEnd;
                return nullable2.HasValue && dateTime2 >= nullable2.GetValueOrDefault();
            default:
                throw new ArgumentOutOfRangeException(Converter.To<string>((object)type));
        }
    }

    public static bool IsApprovedAndCatalog(this Product product)
    {
        return product.IsApproved() && product.IsCatalog();
    }

    public static bool IsApproved(this Product product)
    {
        return product != null ? product.Is(ProductFlags.IsApproved) : throw new ArgumentNullException(nameof(product));
    }

    public static void SetApproved(this Product product, bool value)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));
        product.Set(ProductFlags.IsApproved, value);
    }

    public static bool IsCatalog(this Product product)
    {
        return product != null ? product.Is(ProductFlags.IsCatalog) : throw new ArgumentNullException(nameof(product));
    }

    public static void SetCatalog(this Product product, bool value)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));
        product.Set(ProductFlags.IsCatalog, value);
    }

    public static bool Is(this Product product, ProductFlags flags)
    {
        return TypeHelper.CheckOnNull<Product>(product, nameof(product)).Flags.HasFlag((Enum)flags);
    }

    public static void Set(this Product product, ProductFlags flags, bool value)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));
        if (value)
            product.Flags |= flags;
        else
            product.Flags = Enumerator.Remove<ProductFlags>((Enum)product.Flags, flags);
    }

    public static bool IsFree(this Price price) => price == null || price.Value == 0M;

    public static bool IsFree(this ProductDomainBasePrices prices)
    {
        return prices != null && prices.LifetimePrice.IsFree() && prices.AnnualPrice.IsFree() && prices.MonthlyPrice.IsFree();
    }

    public static bool IsFree(this ProductDomain prodDomain)
    {
        return prodDomain != null && prodDomain.AllApps.IsFree() && prodDomain.OneApps.IsFree();
    }

    public static DateTime CalcEnd(this DateTime begin, ProductPriceTypes priceType)
    {
        switch (priceType)
        {
            case ProductPriceTypes.Lifetime:
                return ProductExtensions.NeverExpiredTill;
            case ProductPriceTypes.PerMonth:
                return begin.AddMonths(1);
            case ProductPriceTypes.Annual:
                return begin.AddYears(1);
            default:
                throw new ArgumentOutOfRangeException(Converter.To<string>((object)priceType));
        }
    }

    public static bool CanStopSubscription(this ProductOrder order)
    {
        if (!order.IsRepeat())
            return false;
        DateTime? subscriptionEnd = order.SubscriptionEnd;
        DateTime utcNow = DateTime.UtcNow;
        return subscriptionEnd.HasValue && subscriptionEnd.GetValueOrDefault() > utcNow;
    }

    public static int GetMinutes(this Session session)
    {
        return (int)(TypeHelper.CheckOnNull<Session>(session, nameof(session)).ModificationDate - session.CreationDate).TotalMinutes;
    }

    public static ProductDomain TryGetDomain(
      this Product product,
      long domainId,
      Func<ProductDomain, bool> filter)
    {
        return product.TryGetEntityDomain<Product, ProductDomain>(domainId, filter);
    }

    public static Topic GetContent(this Product product, long domainId)
    {
        return product.TryGetDomain(domainId, (Func<ProductDomain, bool>)(d => d.Topic != null))?.Topic;
    }

    public static string GetName(this Product product, long domainId)
    {
        return StringHelper.IsEmpty(product.TryGetDomain(domainId, (Func<ProductDomain, bool>)(d => !StringHelper.IsEmpty(d.Name)))?.Name, Converter.To<string>((object)product.Id));
    }

    public static string GetUrlPart(this Product product, long domainId)
    {
        return StringHelper.IsEmpty(product.TryGetDomain(domainId, (Func<ProductDomain, bool>)(d => !StringHelper.IsEmpty(d.UrlRelative)))?.UrlRelative, Converter.To<string>((object)product.Id));
    }

    public static string GetHomepage(this Product product, long domainId)
    {
        return product.TryGetDomain(domainId, (Func<ProductDomain, bool>)(d => !StringHelper.IsEmpty(d.Homepage)))?.Homepage;
    }

    public static ProductGroupDomain TryGetDomain(
      this ProductGroup group,
      long domainId,
      Func<ProductGroupDomain, bool> filter)
    {
        return group.TryGetEntityDomain<ProductGroup, ProductGroupDomain>(domainId, filter);
    }

    public static string GetName(this ProductGroup group, long domainId)
    {
        return StringHelper.IsEmpty(group.TryGetDomain(domainId, (Func<ProductGroupDomain, bool>)(d => !StringHelper.IsEmpty(d.Name)))?.Name, Converter.To<string>((object)group.Id));
    }
}
