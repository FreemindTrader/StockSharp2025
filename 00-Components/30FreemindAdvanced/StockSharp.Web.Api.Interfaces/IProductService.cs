// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IProductService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface IProductService : IBaseEntityService<Product>
{
    Task<BaseEntitySet<Product>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? clientId = null,
      long? managerId = null,
      long? groupId = null,
      string groupIds = null,
      ProductContentTypes2? contentType = null,
      long? emailTemplateId = null,
      ProductFlags? flags = null,
      string userId = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
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
      bool isManager,
      CancellationToken cancellationToken = default(CancellationToken));

    [Obsolete("Use FindPermissions2Async method.")]
    Task<BaseEntitySet<(Client client, bool isManager, DateTime? till)>> FindPermissionsAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      long? productId = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<BaseEntitySet<ProductPermission>> FindPermissions2Async(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      long? productId = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<ProductOrder> RequestTrialAsync(
      long productId,
      string hardwareId,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<ProductOrder> RequestRefundAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

    Task ValidatePackageIdAsync(string packageId, CancellationToken cancellationToken);

    Task AddReleaseNotesAsync(
      (long productId, string version, string text)[] releaseNotes,
      CancellationToken cancellationToken = default(CancellationToken));

    Task ResetNugetCacheAsync(CancellationToken cancellationToken);

    Task<Product> SelectExecutorAsync(long messageId, CancellationToken cancellationToken = default(CancellationToken));

    Task<ProductOrder> ReserveMoneyAsync(
      long productId,
      Decimal amount,
      CancellationToken cancellationToken = default(CancellationToken));

    Task CancelMoneyAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

    Task StartAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

    Task FinishAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

    Task PayoutAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

    Task<string> OpenAccountAsync(long domainId, long productId, CancellationToken cancellationToken = default(CancellationToken));

    Task<Product> GetProductByUrlPartAsync(string urlPart, CancellationToken cancellationToken = default(CancellationToken));

    Task<ProductRole> AddRoleAsync(
      long productId,
      long roleId,
      TimeSpan? till,
      ProductPriceTypes? priceType,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> RemoveRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));

    Task BlockClientAsync(long productId, long clientId, CancellationToken cancellationToken = default(CancellationToken));

    Task UnBlockClientAsync(long productId, long clientId, CancellationToken cancellationToken = default(CancellationToken));

    Task AddGroupAsync(long productId, long groupId, CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> RemoveGroupAsync(long productId, long groupId, CancellationToken cancellationToken = default(CancellationToken));

    Task RequestAccessAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

    Task<ProductPlugin> AddPluginAsync(
      long productId,
      long groupId,
      ProductContentTypes2 contentType,
      CancellationToken cancellationToken = default(CancellationToken));

    Task RemovePluginAsync(long pluginId, CancellationToken cancellationToken = default(CancellationToken));

    Task<string[]> GetSolutionsAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<PublishProject[]> PreviewProjectsAsync(
      string sln,
      PublishDetails details,
      CancellationToken cancellationToken = default(CancellationToken));

    Task PublishAsync(string sln, PublishDetails details, CancellationToken cancellationToken = default(CancellationToken));

    Task<string> DecodeAsync(string value, CancellationToken cancellationToken = default(CancellationToken));

    Task UploadSolutionsAsync(Guid requestId, string[] slns, CancellationToken cancellationToken = default(CancellationToken));

    Task UploadProjectsAsync(
      Guid requestId,
      PublishProject[] projects,
      CancellationToken cancellationToken = default(CancellationToken));

    Task UploadPublishAsync(Guid requestId, CancellationToken cancellationToken = default(CancellationToken));

    Task UploadDecodeAsync(Guid requestId, string value, CancellationToken cancellationToken = default(CancellationToken));

    Task UploadErrorAsync(Guid requestId, string error, CancellationToken cancellationToken = default(CancellationToken));

    Task<string> RefreshFrameworksAsync(long productId, CancellationToken cancellationToken = default(CancellationToken));

    Task SignAsync(Guid requestId, long fileId, CancellationToken cancellationToken = default(CancellationToken));

    Task SignResultAsync(
      Guid requestId,
      long clientId,
      long resultId,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<string> GetDotNetVersionAsync(CancellationToken cancellationToken = default(CancellationToken));
}
