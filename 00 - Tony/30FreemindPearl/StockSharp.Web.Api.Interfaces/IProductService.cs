// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IProductService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IProductService : IBaseEntityService<Product>, IBaseService
    {
        Task<BaseEntitySet<Product>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? clientId = null,
          long? managerId = null,
          long? groupId = null,
          bool? nestedGroups = null,
          string groupIds = null,
          ProductContentTypes2? contentType = null,
          long? emailTemplateId = null,
          ProductFlags? flags = null,
          string like = null,
          LikeCompares? likeCompare = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task AddPermissionAsync(
          long productId,
          long clientId,
          bool isManager,
          DateTime? till,
          CancellationToken cancellationToken = default(CancellationToken));

        Task RemovePermissionAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<BaseEntitySet<(Client client, bool isManager, DateTime? till) >>FindPermissionsAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? productId = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<ProductOrder> RequestTrialAsync(
          long productId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<ProductOrder> RequestRefundAsync(
          long productId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task AddReleaseNotesAsync( (long productId, string version, (long domainId, string releaseNotes )[])[] releaseNotes, CancellationToken cancellationToken = default(CancellationToken));

        Task<Product> SelectExecutorAsync(
          long messageId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<ProductOrder> ReserveMoneyAsync(
          long productId,
          Decimal amount,
          CancellationToken cancellationToken = default(CancellationToken));

        Task CancelMoneyAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

        Task StartAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

        Task FinishAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

        Task PayoutAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

        Task OpenAccountAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

        Task<long?> GetIdByUrlPart(string urlPart, CancellationToken cancellationToken = default(CancellationToken));

        Task AddRoleAsync(ProductRole role, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task BlockClientAsync(long productId, long clientId, CancellationToken cancellationToken = default(CancellationToken));

        Task UnBlockClientAsync(long productId, long clientId, CancellationToken cancellationToken = default(CancellationToken));

        Task AddGroupAsync(long productId, long groupId, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
