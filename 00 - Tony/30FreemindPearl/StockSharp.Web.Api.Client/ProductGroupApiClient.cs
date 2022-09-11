using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ProductGroupApiClient : BaseApiEntityClient<ProductGroup>, IProductGroupService, IBaseEntityService<ProductGroup>, IBaseService
    {
        public ProductGroupApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ProductGroupApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

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
            return Get<BaseEntitySet<ProductGroup>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, productId, childId, parentId, managerId, emailTemplateId, expandInner, like, likeCompare );
        }

        Task IProductGroupService.AddChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddChildAsync"), cancellationToken, parentId, childId );
        }

        Task<bool> IProductGroupService.RemoveChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveChildAsync"), cancellationToken, parentId, childId );
        }

        Task IProductGroupService.AddManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddManagerAsync"), cancellationToken, groupId, clientId );
        }

        Task<bool> IProductGroupService.RemoveManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveManagerAsync"), cancellationToken, groupId, clientId );
        }
    }
}
