
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class ProductGroupClient : BaseOfflineClient<ProductGroup>, IProductGroupService, IBaseEntityService<ProductGroup>, IBaseService
    {
        Task<BaseEntitySet<ProductGroup>> IProductGroupService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? productId,
          long? childId,
          long? parentId,
          long? managerId,
          long? emailTemplateId,
          bool? expandInner,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<BaseEntitySet<ProductGroup>>(((IEnumerable<ProductGroup>)Array.Empty<ProductGroup>()).ToEntitySet<ProductGroup>(0));
        }

        Task IProductGroupService.AddChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> IProductGroupService.RemoveChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductGroupService.AddManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> IProductGroupService.RemoveManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}
