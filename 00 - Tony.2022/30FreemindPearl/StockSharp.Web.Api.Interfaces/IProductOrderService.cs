// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IProductOrderService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IProductOrderService : IBaseEntityService<ProductOrder>, IBaseService
    {
        Task<BaseEntitySet<ProductOrder>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? clientId = null,
          long? productId = null,
          ProductContentTypes2? contentType = null,
          Decimal? repeatAmountLow = null,
          Decimal? repeatAmountHigh = null,
          DateTime? endLow = null,
          DateTime? endHigh = null,
          ProductOrderFlags? flags = null,
          bool? isActiveOnly = null,
          bool? isReferralOnly = null,
          bool? isRepeatOnly = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<Payment> MakeRecurrentAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));

        Task MakeRecurrentsAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<ProductOrder> RenewAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));

        Task<ProductOrder> StopAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));

        Task TrialRejectAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));

        Task TrialApproveAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));

        Task RefundRejectAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));

        Task RefundApproveAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
