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
            return Get<BaseEntitySet<Product>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, managerId, groupId, nestedGroups, groupIds, contentType, emailTemplateId, flags, like, likeCompare );
        }

        Task IProductService.AddPermissionAsync(
          long productId,
          long clientId,
          bool isManager,
          DateTime? till,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddPermissionAsync"), cancellationToken, productId, clientId, isManager, till );
        }

        Task IProductService.RemovePermissionAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "RemovePermissionAsync"), cancellationToken, productId, clientId );
        }

        

        Task<ProductOrder> IProductService.RequestTrialAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return Post<ProductOrder>( GetCurrentMethod( "RequestTrialAsync"), cancellationToken, productId );
        }

        Task<ProductOrder> IProductService.RequestRefundAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return Post<ProductOrder>( GetCurrentMethod( "RequestRefundAsync"), cancellationToken, productId );
        }

        

        Task<Product> IProductService.SelectExecutorAsync(
          long messageId,
          CancellationToken cancellationToken)
        {
            return Post<Product>( GetCurrentMethod( "SelectExecutorAsync"), cancellationToken, messageId );
        }

        Task<ProductOrder> IProductService.ReserveMoneyAsync(
          long productId,
          Decimal amount,
          CancellationToken cancellationToken)
        {
            return Post<ProductOrder>( GetCurrentMethod( "ReserveMoneyAsync"), cancellationToken, productId, amount );
        }

        Task IProductService.CancelMoneyAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "CancelMoneyAsync"), cancellationToken, productId );
        }

        Task IProductService.StartAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "StartAsync"), cancellationToken, productId );
        }

        Task IProductService.FinishAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "FinishAsync"), cancellationToken, productId );
        }

        Task IProductService.PayoutAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "PayoutAsync"), cancellationToken, productId );
        }

        Task IProductService.OpenAccountAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "OpenAccountAsync"), cancellationToken, productId );
        }

        Task<long?> IProductService.GetIdByUrlPart(
          string urlPart,
          CancellationToken cancellationToken)
        {
            return Get<long?>( GetCurrentMethod( "GetIdByUrlPart"), cancellationToken, urlPart );
        }

        Task IProductService.AddRoleAsync(
          ProductRole role,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddRoleAsync"), cancellationToken, role );
        }

        Task<bool> IProductService.RemoveRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveRoleAsync"), cancellationToken, roleId );
        }

        Task IProductService.BlockClientAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "BlockClientAsync"), cancellationToken, productId, clientId );
        }

        Task IProductService.UnBlockClientAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "UnBlockClientAsync"), cancellationToken, productId, clientId );
        }

        Task IProductService.AddGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddGroupAsync"), cancellationToken, productId, groupId );
        }

        Task<bool> IProductService.RemoveGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveGroupAsync"), cancellationToken, productId, groupId );
        }

        Task<BaseEntitySet<(DomainModel.Client client, bool isManager, DateTime? till)>> IProductService.FindPermissionsAsync(long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, long? productId, CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<(DomainModel.Client client, bool isManager, DateTime? till)>>( GetCurrentMethod( "FindPermissionsAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, productId );
        }

        

        Task IProductService.AddReleaseNotesAsync((long productId, string version, (long domainId, string releaseNotes)[])[] releaseNotes, CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddReleaseNotesAsync"), cancellationToken, releaseNotes );
        }

        
    }
}
