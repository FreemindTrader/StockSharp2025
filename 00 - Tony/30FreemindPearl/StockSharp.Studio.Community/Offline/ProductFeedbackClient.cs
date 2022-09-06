
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class ProductFeedbackClient : BaseOfflineClient<ProductFeedback>, IProductFeedbackService, IBaseEntityService<ProductFeedback>, IBaseService
    {
        Task<BaseEntitySet<ProductFeedback>> IProductFeedbackService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? productId,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<BaseEntitySet<ProductFeedback>>(((IEnumerable<ProductFeedback>)Array.Empty<ProductFeedback>()).ToEntitySet<ProductFeedback>(0));
        }

        Task<ProductFeedback> IProductFeedbackService.GetByProductAndClientAsync(
          long productId,
          long? clientId,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<ProductFeedback>(new ProductFeedback());
        }
    }
}
