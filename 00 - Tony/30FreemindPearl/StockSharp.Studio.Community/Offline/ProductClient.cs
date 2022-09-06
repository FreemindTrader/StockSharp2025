
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1066

namespace StockSharp.Studio.Community.Offline
{
    internal class ProductClient : BaseOfflineClient<Product>, IProductService, IBaseEntityService<Product>, IBaseService
    {
        Task IProductService.AddPermissionAsync(
          long productId,
          long clientId,
          bool isManager,
          DateTime? till,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.AddReleaseNotesAsync((long productId, string version, (long domainId, string releaseNotes)[])[] releaseNotes, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotSupportedException();
        }

        Task IProductService.CancelMoneyAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
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
            return Task.FromResult<BaseEntitySet<Product>>(new BaseEntitySet<Product>());
        }

        Task<BaseEntitySet<(Client client, bool isManager, DateTime? till)>> IProductService.FindPermissionsAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? productId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.FinishAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<long?> IProductService.GetIdByUrlPart(
          string urlPart,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.AddRoleAsync(
          ProductRole role,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> IProductService.RemoveRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.BlockClientAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.UnBlockClientAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.AddGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> IProductService.RemoveGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.OpenAccountAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.PayoutAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.RemovePermissionAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<ProductOrder> IProductService.RequestRefundAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<ProductOrder> IProductService.RequestTrialAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<ProductOrder> IProductService.ReserveMoneyAsync(
          long productId,
          Decimal amount,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<Product> IProductService.SelectExecutorAsync(
          long messageId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IProductService.StartAsync(
          long productId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}
