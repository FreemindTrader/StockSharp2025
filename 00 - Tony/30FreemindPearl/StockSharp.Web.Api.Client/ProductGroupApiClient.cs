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
            return this.Get<BaseEntitySet<ProductGroup>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)productId, (object)childId, (object)parentId, (object)managerId, (object)emailTemplateId, (object)expandInner, (object)like, (object)likeCompare);
        }

        Task IProductGroupService.AddChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddChildAsync"), cancellationToken, (object)parentId, (object)childId);
        }

        Task<bool> IProductGroupService.RemoveChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveChildAsync"), cancellationToken, (object)parentId, (object)childId);
        }

        Task IProductGroupService.AddManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddManagerAsync"), cancellationToken, (object)groupId, (object)clientId);
        }

        Task<bool> IProductGroupService.RemoveManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveManagerAsync"), cancellationToken, (object)groupId, (object)clientId);
        }
    }
}
