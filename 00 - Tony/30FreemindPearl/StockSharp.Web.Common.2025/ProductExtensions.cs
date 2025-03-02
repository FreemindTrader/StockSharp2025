// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.ProductExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Web.DomainModel;
using System;

namespace StockSharp.Web.Common
{
    public static class ProductExtensions
    {
        public static readonly DateTime ExpiredTill = TimeHelper.UtcKind(new DateTime(2000, 1, 1));
        public static readonly DateTime NeverExpiredTill = TimeHelper.UtcKind(new DateTime(2100, 1, 1));

        public static bool IsPackageIdRequired( this ProductContentTypes2 type )
        {
            bool flag;
            switch ( type )
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

        public static bool IsDevelopmentOrFreelance( this Product product )
        {
            if ( !product.IsDevelopment.GetValueOrDefault() )
                return product.IsFreelance.GetValueOrDefault();
            return true;
        }

        public static TimeSpan? GetLength( this ProductOrder order )
        {
            if ( order == null )
                throw new ArgumentNullException( nameof( order ) );
            DateTime? subscriptionEnd = order.SubscriptionEnd;
            DateTime? subscriptionStart = order.SubscriptionStart;
            if ( !( subscriptionEnd.HasValue & subscriptionStart.HasValue ) )
                return new TimeSpan?();
            return new TimeSpan?( subscriptionEnd.GetValueOrDefault() - subscriptionStart.GetValueOrDefault() );
        }

        public static TimeSpan? GetRemains( this ProductOrder order )
        {
            if ( order == null )
                throw new ArgumentNullException( nameof( order ) );
            DateTime? subscriptionEnd = order.SubscriptionEnd;
            DateTime utcNow = DateTime.UtcNow;
            TimeSpan? nullable1 = subscriptionEnd.HasValue ? new TimeSpan?(subscriptionEnd.GetValueOrDefault() - utcNow) : new TimeSpan?();
            TimeSpan? nullable2 = nullable1;
            TimeSpan zero = TimeSpan.Zero;
            if ( ( nullable2.HasValue ? ( nullable2.GetValueOrDefault() < zero ? 1 : 0 ) : 0 ) != 0 )
                nullable1 = new TimeSpan?( TimeSpan.Zero );
            return nullable1;
        }

        public static bool IsTrialApproveRequired( this ProductOrder order )
        {
            if ( order.IsTrial() )
                return !order.SubscriptionEnd.HasValue;
            return false;
        }

        public static bool IsTrial( this ProductOrder order )
        {
            return order.Is( ProductOrderFlags.Trial );
        }

        public static bool IsRefund( this ProductOrder order )
        {
            return order.Is( ProductOrderFlags.Refund );
        }

        public static bool IsTest( this ProductOrder order )
        {
            return order.Is( ProductOrderFlags.Test );
        }

        public static bool IsCancelled( this ProductOrder order )
        {
            return order.Is( ProductOrderFlags.Cancelled );
        }

        public static bool Is( this ProductOrder order, ProductOrderFlags flags )
        {
            return ( ( ProductOrder ) TypeHelper.CheckOnNull<ProductOrder>(  order, nameof( order ) ) ).Flags.HasFlag( ( Enum ) flags );
        }

        public static ProductOrder SetTrial( this ProductOrder order, bool value )
        {
            return order.Set( ProductOrderFlags.Trial, value );
        }

        public static ProductOrder SetRefund( this ProductOrder order, bool value )
        {
            return order.Set( ProductOrderFlags.Refund, value );
        }

        public static ProductOrder SetTest( this ProductOrder order, bool value )
        {
            return order.Set( ProductOrderFlags.Test, value );
        }

        public static ProductOrder SetCancelled( this ProductOrder order, bool value )
        {
            return order.Set( ProductOrderFlags.Cancelled, value );
        }

        public static ProductOrder Set(
          this ProductOrder order,
          ProductOrderFlags flags,
          bool value )
        {
            if ( order == null )
                throw new ArgumentNullException( nameof( order ) );
            if ( value )
                order.Flags |= flags;
            else
                order.Flags = ( ProductOrderFlags ) Enumerator.Remove<ProductOrderFlags>( ( Enum ) order.Flags,  flags );
            return order;
        }

        public static bool IsRepeat( this ProductOrder order )
        {
            return ( ( ProductOrder ) TypeHelper.CheckOnNull<ProductOrder>(  order, nameof( order ) ) ).RepeatAmount != Decimal.Zero;
        }

        public static bool IsRecurrent( this ProductOrder order )
        {
            return !StringHelper.IsEmpty( ( ( ProductOrder ) TypeHelper.CheckOnNull<ProductOrder>(  order, nameof( order ) ) ).RepeatToken );
        }

        public static bool CanRenew( this ProductOrder order )
        {
            if ( order == null )
                throw new ArgumentNullException( nameof( order ) );
            return order.PriceType.CanRenew( order.SubscriptionEnd );
        }

        public static bool CanRenew( this ProductPriceTypes type, DateTime? subscriptionEnd )
        {
            switch ( type )
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
                throw new ArgumentOutOfRangeException( ( string ) Converter.To<string>( ( object ) type ) );
            }
        }

        public static bool IsApprovedAndCatalog( this Product product )
        {
            if ( product.IsApproved() )
                return product.IsCatalog();
            return false;
        }

        public static bool IsApproved( this Product product )
        {
            if ( product == null )
                throw new ArgumentNullException( nameof( product ) );
            return product.Is( ProductFlags.IsApproved );
        }

        public static void SetApproved( this Product product, bool value )
        {
            if ( product == null )
                throw new ArgumentNullException( nameof( product ) );
            product.Set( ProductFlags.IsApproved, value );
        }

        public static bool IsCatalog( this Product product )
        {
            if ( product == null )
                throw new ArgumentNullException( nameof( product ) );
            return product.Is( ProductFlags.IsCatalog );
        }

        public static void SetCatalog( this Product product, bool value )
        {
            if ( product == null )
                throw new ArgumentNullException( nameof( product ) );
            product.Set( ProductFlags.IsCatalog, value );
        }

        public static bool Is( this Product product, ProductFlags flags )
        {
            return ( ( Product ) TypeHelper.CheckOnNull<Product>(  product, nameof( product ) ) ).Flags.HasFlag( ( Enum ) flags );
        }

        public static void Set( this Product product, ProductFlags flags, bool value )
        {
            if ( product == null )
                throw new ArgumentNullException( nameof( product ) );
            if ( value )
                product.Flags |= flags;
            else
                product.Flags = ( ProductFlags ) Enumerator.Remove<ProductFlags>( ( Enum ) product.Flags,  flags );
        }

        public static bool IsFree( this Price price )
        {
            if ( price != null )
                return price.Value == Decimal.Zero;
            return true;
        }

        public static bool IsFree( this ProductDomainBasePrices prices )
        {
            if ( prices != null && prices.LifetimePrice.IsFree() && prices.AnnualPrice.IsFree() )
                return prices.MonthlyPrice.IsFree();
            return false;
        }

        public static bool IsFree( this ProductDomain prodDomain )
        {
            if ( prodDomain != null && prodDomain.AllApps.IsFree() )
                return prodDomain.OneApps.IsFree();
            return false;
        }

        public static DateTime CalcEnd( this DateTime begin, ProductPriceTypes priceType )
        {
            switch ( priceType )
            {
                case ProductPriceTypes.Lifetime:
                return ProductExtensions.NeverExpiredTill;
                case ProductPriceTypes.PerMonth:
                return begin.AddMonths( 1 );
                case ProductPriceTypes.Annual:
                return begin.AddYears( 1 );
                default:
                throw new ArgumentOutOfRangeException( ( string ) Converter.To<string>( ( object ) priceType ) );
            }
        }

        public static bool CanStopSubscription( this ProductOrder order )
        {
            if ( !order.IsRepeat() )
                return false;
            DateTime? subscriptionEnd = order.SubscriptionEnd;
            DateTime utcNow = DateTime.UtcNow;
            if ( !subscriptionEnd.HasValue )
                return false;
            return subscriptionEnd.GetValueOrDefault() > utcNow;
        }

        public static int GetMinutes( this Session session )
        {
            return ( int ) ( ( ( BaseEntity ) TypeHelper.CheckOnNull<Session>(  session, nameof( session ) ) ).ModificationDate - session.CreationDate ).TotalMinutes;
        }

        public static ProductDomain TryGetDomain(
          this Product product,
          long domainId,
          Func<ProductDomain, bool> filter )
        {
            return product.TryGetEntityDomain<Product, ProductDomain>( domainId, filter );
        }

        public static Topic GetContent( this Product product, long domainId )
        {
            return product.TryGetDomain( domainId, ( Func<ProductDomain, bool> ) ( d => d.Topic != null ) )?.Topic;
        }

        public static string GetName( this Product product, long domainId )
        {
            return StringHelper.IsEmpty( product.TryGetDomain( domainId, ( Func<ProductDomain, bool> ) ( d => !StringHelper.IsEmpty( d.Name ) ) )?.Name, ( string ) Converter.To<string>( ( object ) product.Id ) );
        }

        public static string GetUrlPart( this Product product, long domainId )
        {
            return StringHelper.IsEmpty( product.TryGetDomain( domainId, ( Func<ProductDomain, bool> ) ( d => !StringHelper.IsEmpty( d.UrlRelative ) ) )?.UrlRelative, ( string ) Converter.To<string>( ( object ) product.Id ) );
        }

        public static string GetHomepage( this Product product, long domainId )
        {
            return product.TryGetDomain( domainId, ( Func<ProductDomain, bool> ) ( d => !StringHelper.IsEmpty( d.Homepage ) ) )?.Homepage;
        }

        public static ProductGroupDomain TryGetDomain( this ProductGroup group, long domainId, Func<ProductGroupDomain, bool> filter )
        {
            return group.TryGetEntityDomain<ProductGroup, ProductGroupDomain>( domainId, filter );
        }

        public static string GetName( this ProductGroup group, long domainId )
        {
            return StringHelper.IsEmpty( group.TryGetDomain( domainId, ( Func<ProductGroupDomain, bool> ) ( d => !StringHelper.IsEmpty( d.Name ) ) )?.Name, ( string ) Converter.To<string>( ( object ) group.Id ) );
        }
    }
}
