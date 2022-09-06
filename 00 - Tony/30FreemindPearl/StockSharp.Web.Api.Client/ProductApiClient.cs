using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ProductApiClient : BaseApiEntityClient<Product>, IProductService, IBaseEntityService<Product>, IBaseService
    {
        public ProductApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ProductApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Product>> IProductService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? managerId,
          long? groupId,
          bool? nestedGroups,
          string groupIds,
          ProductContentTypes2? contentType,
          long? emailTemplateId,
          ProductFlags? flags,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<Product>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)managerId, (object)groupId, (object)nestedGroups, (object)groupIds, (object)contentType, (object)emailTemplateId, (object)flags, (object)like, (object)likeCompare);
        }

        Task IProductService.AddPermissionAsync(
          long productId,
          long clientId,
          bool isManager,
          DateTime? till,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddPermissionAsync"), cancellationToken, (object)productId, (object)clientId, (object)isManager, (object)till);
        }

        Task IProductService.RemovePermissionAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("RemovePermissionAsync"), cancellationToken, (object)productId, (object)clientId);
        }

        

        Task<ProductOrder> IProductService.RequestTrialAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("RequestTrialAsync"), cancellationToken, (object)productId);
        }

        Task<ProductOrder> IProductService.RequestRefundAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("RequestRefundAsync"), cancellationToken, (object)productId);
        }

        

        Task<Product> IProductService.SelectExecutorAsync(
          long messageId,
          CancellationToken cancellationToken)
        {
            return this.Post<Product>(RestBaseApiClient.GetCurrentMethod("SelectExecutorAsync"), cancellationToken, (object)messageId);
        }

        Task<ProductOrder> IProductService.ReserveMoneyAsync(
          long productId,
          Decimal amount,
          CancellationToken cancellationToken)
        {
            return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("ReserveMoneyAsync"), cancellationToken, (object)productId, (object)amount);
        }

        Task IProductService.CancelMoneyAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("CancelMoneyAsync"), cancellationToken, (object)productId);
        }

        Task IProductService.StartAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("StartAsync"), cancellationToken, (object)productId);
        }

        Task IProductService.FinishAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("FinishAsync"), cancellationToken, (object)productId);
        }

        Task IProductService.PayoutAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("PayoutAsync"), cancellationToken, (object)productId);
        }

        Task IProductService.OpenAccountAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("OpenAccountAsync"), cancellationToken, (object)productId);
        }

        Task<long?> IProductService.GetIdByUrlPart(
          string urlPart,
          CancellationToken cancellationToken)
        {
            return this.Get<long?>(RestBaseApiClient.GetCurrentMethod("GetIdByUrlPart"), cancellationToken, (object)urlPart);
        }

        Task IProductService.AddRoleAsync(
          ProductRole role,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddRoleAsync"), cancellationToken, (object)role);
        }

        Task<bool> IProductService.RemoveRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, (object)roleId);
        }

        Task IProductService.BlockClientAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("BlockClientAsync"), cancellationToken, (object)productId, (object)clientId);
        }

        Task IProductService.UnBlockClientAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("UnBlockClientAsync"), cancellationToken, (object)productId, (object)clientId);
        }

        Task IProductService.AddGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddGroupAsync"), cancellationToken, (object)productId, (object)groupId);
        }

        Task<bool> IProductService.RemoveGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveGroupAsync"), cancellationToken, (object)productId, (object)groupId);
        }

        Task<BaseEntitySet<(DomainModel.Client client, bool isManager, DateTime? till)>> IProductService.FindPermissionsAsync(long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, long? productId, CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<(StockSharp.Web.DomainModel.Client client, bool isManager, DateTime? till)>>(RestBaseApiClient.GetCurrentMethod("FindPermissionsAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)productId);
        }

        

        Task IProductService.AddReleaseNotesAsync((long productId, string version, (long domainId, string releaseNotes)[])[] releaseNotes, CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddReleaseNotesAsync"), cancellationToken, (object)releaseNotes);
        }

        
    }
}
