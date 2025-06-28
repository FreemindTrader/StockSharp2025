// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProductApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class ProductApiClient :
  BaseApiEntityClient<Product>,
  IProductService,
  IBaseEntityService<Product>
{
    public ProductApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public ProductApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<Product>> IProductService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? clientId,
      long? managerId,
      long? groupId,
      string groupIds,
      ProductContentTypes2? contentType,
      long? emailTemplateId,
      ProductFlags? flags,
      string userId,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Product>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[18]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) clientId,
      (object) managerId,
      (object) groupId,
      (object) groupIds,
      (object) contentType,
      (object) emailTemplateId,
      (object) flags,
      (object) userId,
      (object) like,
      (object) likeCompare
        });
    }

    Task IProductService.AddPermissionAsync(
      long productId,
      long clientId,
      bool isManager,
      DateTime? till,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddPermissionAsync"), cancellationToken, new object[4]
        {
      (object) productId,
      (object) clientId,
      (object) isManager,
      (object) till
        });
    }

    Task IProductService.RemovePermissionAsync(
      long productId,
      long clientId,
      bool isManager,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("RemovePermissionAsync"), cancellationToken, new object[3]
        {
      (object) productId,
      (object) clientId,
      (object) isManager
        });
    }

    Task<BaseEntitySet<(StockSharp.Web.DomainModel.Client client, bool isManager, DateTime? till)>> IProductService.FindPermissionsAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      long? productId,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<(StockSharp.Web.DomainModel.Client, bool, DateTime?)>>(RestBaseApiClient.GetCurrentMethod("FindPermissionsAsync"), cancellationToken, new object[7]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) productId
        });
    }

    Task<BaseEntitySet<ProductPermission>> IProductService.FindPermissions2Async(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      long? productId,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<ProductPermission>>(RestBaseApiClient.GetCurrentMethod("FindPermissions2Async"), cancellationToken, new object[7]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) productId
        });
    }

    Task<ProductOrder> IProductService.RequestTrialAsync(
      long productId,
      string hardwareId,
      CancellationToken cancellationToken)
    {
        return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("RequestTrialAsync"), cancellationToken, new object[2]
        {
      (object) productId,
      (object) hardwareId
        });
    }

    Task<ProductOrder> IProductService.RequestRefundAsync(
      long productId,
      CancellationToken cancellationToken)
    {
        return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("RequestRefundAsync"), cancellationToken, new object[1]
        {
      (object) productId
        });
    }

    Task IProductService.ValidatePackageIdAsync(string packageId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("ValidatePackageIdAsync"), cancellationToken, new object[1]
        {
      (object) packageId
        });
    }

    Task IProductService.AddReleaseNotesAsync(
      (long productId, string version, string text)[] releaseNotes,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddReleaseNotesAsync"), cancellationToken, new object[1]
        {
      (object) releaseNotes
        });
    }

    Task IProductService.ResetNugetCacheAsync(CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("ResetNugetCacheAsync"), cancellationToken, Array.Empty<object>());
    }

    Task<Product> IProductService.SelectExecutorAsync(
      long messageId,
      CancellationToken cancellationToken)
    {
        return this.Post<Product>(RestBaseApiClient.GetCurrentMethod("SelectExecutorAsync"), cancellationToken, new object[1]
        {
      (object) messageId
        });
    }

    Task<ProductOrder> IProductService.ReserveMoneyAsync(
      long productId,
      Decimal amount,
      CancellationToken cancellationToken)
    {
        return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("ReserveMoneyAsync"), cancellationToken, new object[2]
        {
      (object) productId,
      (object) amount
        });
    }

    Task IProductService.CancelMoneyAsync(long productId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("CancelMoneyAsync"), cancellationToken, new object[1]
        {
      (object) productId
        });
    }

    Task IProductService.StartAsync(long productId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("StartAsync"), cancellationToken, new object[1]
        {
      (object) productId
        });
    }

    Task IProductService.FinishAsync(long productId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("FinishAsync"), cancellationToken, new object[1]
        {
      (object) productId
        });
    }

    Task IProductService.PayoutAsync(long productId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("PayoutAsync"), cancellationToken, new object[1]
        {
      (object) productId
        });
    }

    Task<string> IProductService.OpenAccountAsync(
      long domainId,
      long productId,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("OpenAccountAsync"), cancellationToken, new object[2]
        {
      (object) domainId,
      (object) productId
        });
    }

    Task<Product> IProductService.GetProductByUrlPartAsync(
      string urlPart,
      CancellationToken cancellationToken)
    {
        return this.Get<Product>(RestBaseApiClient.GetCurrentMethod("GetProductByUrlPartAsync"), cancellationToken, new object[1]
        {
      (object) urlPart
        });
    }

    Task<ProductRole> IProductService.AddRoleAsync(
      long productId,
      long roleId,
      TimeSpan? till,
      ProductPriceTypes? priceType,
      CancellationToken cancellationToken)
    {
        return this.Post<ProductRole>(RestBaseApiClient.GetCurrentMethod("AddRoleAsync"), cancellationToken, new object[4]
        {
      (object) productId,
      (object) roleId,
      (object) till,
      (object) priceType
        });
    }

    Task<bool> IProductService.RemoveRoleAsync(long roleId, CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, new object[1]
        {
      (object) roleId
        });
    }

    Task IProductService.BlockClientAsync(
      long productId,
      long clientId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("BlockClientAsync"), cancellationToken, new object[2]
        {
      (object) productId,
      (object) clientId
        });
    }

    Task IProductService.UnBlockClientAsync(
      long productId,
      long clientId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("UnBlockClientAsync"), cancellationToken, new object[2]
        {
      (object) productId,
      (object) clientId
        });
    }

    Task IProductService.AddGroupAsync(
      long productId,
      long groupId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddGroupAsync"), cancellationToken, new object[2]
        {
      (object) productId,
      (object) groupId
        });
    }

    Task<bool> IProductService.RemoveGroupAsync(
      long productId,
      long groupId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveGroupAsync"), cancellationToken, new object[2]
        {
      (object) productId,
      (object) groupId
        });
    }

    Task IProductService.RequestAccessAsync(long productId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("RequestAccessAsync"), cancellationToken, new object[1]
        {
      (object) productId
        });
    }

    Task<ProductPlugin> IProductService.AddPluginAsync(
      long productId,
      long groupId,
      ProductContentTypes2 contentType,
      CancellationToken cancellationToken)
    {
        return this.Post<ProductPlugin>(RestBaseApiClient.GetCurrentMethod("AddPluginAsync"), cancellationToken, new object[3]
        {
      (object) productId,
      (object) groupId,
      (object) contentType
        });
    }

    Task IProductService.RemovePluginAsync(long pluginId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("RemovePluginAsync"), cancellationToken, new object[1]
        {
      (object) pluginId
        });
    }

    Task<string[]> IProductService.GetSolutionsAsync(CancellationToken cancellationToken)
    {
        return this.Get<string[]>(RestBaseApiClient.GetCurrentMethod("GetSolutionsAsync"), cancellationToken, Array.Empty<object>());
    }

    Task<PublishProject[]> IProductService.PreviewProjectsAsync(
      string sln,
      PublishDetails details,
      CancellationToken cancellationToken)
    {
        return this.Post<PublishProject[]>(RestBaseApiClient.GetCurrentMethod("PreviewProjectsAsync"), cancellationToken, new object[2]
        {
      (object) sln,
      (object) details
        });
    }

    Task IProductService.PublishAsync(
      string sln,
      PublishDetails details,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("PublishAsync"), cancellationToken, new object[2]
        {
      (object) sln,
      (object) details
        });
    }

    Task<string> IProductService.DecodeAsync(string value, CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("DecodeAsync"), cancellationToken, new object[1]
        {
      (object) value
        });
    }

    Task IProductService.UploadSolutionsAsync(
      Guid requestId,
      string[] slns,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("UploadSolutionsAsync"), cancellationToken, new object[2]
        {
      (object) requestId,
      (object) slns
        });
    }

    Task IProductService.UploadProjectsAsync(
      Guid requestId,
      PublishProject[] projects,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("UploadProjectsAsync"), cancellationToken, new object[2]
        {
      (object) requestId,
      (object) projects
        });
    }

    Task IProductService.UploadPublishAsync(Guid requestId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("UploadPublishAsync"), cancellationToken, new object[1]
        {
      (object) requestId
        });
    }

    Task IProductService.UploadDecodeAsync(
      Guid requestId,
      string value,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("UploadDecodeAsync"), cancellationToken, new object[2]
        {
      (object) requestId,
      (object) value
        });
    }

    Task IProductService.UploadErrorAsync(
      Guid requestId,
      string error,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("UploadErrorAsync"), cancellationToken, new object[2]
        {
      (object) requestId,
      (object) error
        });
    }

    Task<string> IProductService.RefreshFrameworksAsync(
      long productId,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("RefreshFrameworksAsync"), cancellationToken, new object[1]
        {
      (object) productId
        });
    }

    Task IProductService.SignAsync(Guid requestId, long fileId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("SignAsync"), cancellationToken, new object[2]
        {
      (object) requestId,
      (object) fileId
        });
    }

    Task IProductService.SignResultAsync(
      Guid requestId,
      long clientId,
      long resultId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("SignResultAsync"), cancellationToken, new object[3]
        {
      (object) requestId,
      (object) clientId,
      (object) resultId
        });
    }

    Task<string> IProductService.GetDotNetVersionAsync(CancellationToken cancellationToken)
    {
        return this.Get<string>(RestBaseApiClient.GetCurrentMethod("GetDotNetVersionAsync"), cancellationToken, Array.Empty<object>());
    }
}
