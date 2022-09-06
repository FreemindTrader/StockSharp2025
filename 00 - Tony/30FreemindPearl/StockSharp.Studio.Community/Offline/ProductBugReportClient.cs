
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class ProductBugReportClient : BaseOfflineClient<ProductBugReport>, IProductBugReportService, IBaseEntityService<ProductBugReport>, IBaseService
    {
        Task<BaseEntitySet<ProductBugReport>> IProductBugReportService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? productId,
          long? errorId,
          bool? aggregated,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<BaseEntitySet<ProductBugReport>>(((IEnumerable<ProductBugReport>)Array.Empty<ProductBugReport>()).ToEntitySet<ProductBugReport>(0));
        }

        Task<ProductBugReport> IProductBugReportService.TryProposeAsync(
          ProductBugReport entity,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<ProductBugReport>((ProductBugReport)null);
        }
    }
}
