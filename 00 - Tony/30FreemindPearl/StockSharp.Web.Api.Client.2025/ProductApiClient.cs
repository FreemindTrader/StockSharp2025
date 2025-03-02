// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProductApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using static StockSharp.Web.DomainModel.Ids;
using static System.Net.Mime.MediaTypeNames;
using Product = StockSharp.Web.DomainModel.Product;

namespace StockSharp.Web.Api.Client
{
    internal class ProductApiClient : BaseApiEntityClient<Product>, IProductService, IBaseEntityService<Product>
    {
        public ProductApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public ProductApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<Product>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [18]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         clientId,
         managerId,
         groupId,
         groupIds,
         contentType,
         emailTemplateId,
         flags,
         userId,
         like,
         likeCompare
            } );
        }

        Task IProductService.AddPermissionAsync(
          long productId,
          long clientId,
          bool isManager,
          DateTime? till,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddPermissionAsync" ), cancellationToken, new object [4]
            {
         productId,
         clientId,
         isManager,
         till
            } );
        }

        Task IProductService.RemovePermissionAsync(
          long productId,
          long clientId,
          bool isManager,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "RemovePermissionAsync" ), cancellationToken, new object [3]
            {
         productId,
         clientId,
         isManager
            } );
        }

        
        Task<BaseEntitySet<(StockSharp.Web.DomainModel.Client client, bool isManager, DateTime? till)>> IProductService.FindPermissionsAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          long? productId,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<ValueTuple<StockSharp.Web.DomainModel.Client, bool, DateTime?>>>( RestBaseApiClient.GetCurrentMethod( "FindPermissionsAsync" ), cancellationToken, new object [7]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         productId
            } );
        }

        Task<BaseEntitySet<ProductPermission>> IProductService.FindPermissions2Async(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          long? productId,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<ProductPermission>>( RestBaseApiClient.GetCurrentMethod( "FindPermissions2Async" ), cancellationToken, new object [7]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         productId
            } );
        }

        Task<ProductOrder> IProductService.RequestTrialAsync(
          long productId,
          string hardwareId,
          CancellationToken cancellationToken )
        {
            return this.Post<ProductOrder>( RestBaseApiClient.GetCurrentMethod( "RequestTrialAsync" ), cancellationToken, new object [2]
            {
         productId,
         hardwareId
            } );
        }

        Task<ProductOrder> IProductService.RequestRefundAsync(
          long productId,
          CancellationToken cancellationToken )
        {
            return this.Post<ProductOrder>( RestBaseApiClient.GetCurrentMethod( "RequestRefundAsync" ), cancellationToken, new object [1]
            {
         productId
            } );
        }

        Task IProductService.ValidatePackageIdAsync(
          string packageId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "ValidatePackageIdAsync" ), cancellationToken, new object [1]
            {
         packageId
            } );
        }
        Task IProductService.AddReleaseNotesAsync( (long productId, string version, string text) [ ] releaseNotes, CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddReleaseNotesAsync" ), cancellationToken, new object [1]
            {
         releaseNotes
            } );
        }

        Task IProductService.ResetNugetCacheAsync( CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "ResetNugetCacheAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task<Product> IProductService.SelectExecutorAsync(
          long messageId,
          CancellationToken cancellationToken )
        {
            return this.Post<Product>( RestBaseApiClient.GetCurrentMethod( "SelectExecutorAsync" ), cancellationToken, new object [1]
            {
         messageId
            } );
        }

        Task<ProductOrder> IProductService.ReserveMoneyAsync(
          long productId,
          Decimal amount,
          CancellationToken cancellationToken )
        {
            return this.Post<ProductOrder>( RestBaseApiClient.GetCurrentMethod( "ReserveMoneyAsync" ), cancellationToken, new object [2]
            {
         productId,
         amount
            } );
        }

        Task IProductService.CancelMoneyAsync(
          long productId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "CancelMoneyAsync" ), cancellationToken, new object [1]
            {
         productId
            } );
        }

        Task IProductService.StartAsync(
          long productId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "StartAsync" ), cancellationToken, new object [1]
            {
         productId
            } );
        }

        Task IProductService.FinishAsync(
          long productId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "FinishAsync" ), cancellationToken, new object [1]
            {
         productId
            } );
        }

        Task IProductService.PayoutAsync(
          long productId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "PayoutAsync" ), cancellationToken, new object [1]
            {
         productId
            } );
        }

        Task<string> IProductService.OpenAccountAsync(
          long domainId,
          long productId,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "OpenAccountAsync" ), cancellationToken, new object [2]
            {
         domainId,
         productId
            } );
        }

        Task<Product> IProductService.GetProductByUrlPartAsync(
          string urlPart,
          CancellationToken cancellationToken )
        {
            return this.Get<Product>( RestBaseApiClient.GetCurrentMethod( "GetProductByUrlPartAsync" ), cancellationToken, new object [1]
            {
         urlPart
            } );
        }

        Task<ProductRole> IProductService.AddRoleAsync(
          long productId,
          long roleId,
          TimeSpan? till,
          ProductPriceTypes? priceType,
          CancellationToken cancellationToken )
        {
            return this.Post<ProductRole>( RestBaseApiClient.GetCurrentMethod( "AddRoleAsync" ), cancellationToken, new object [4]
            {
         productId,
         roleId,
         till,
         priceType
            } );
        }

        Task<bool> IProductService.RemoveRoleAsync(
          long roleId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveRoleAsync" ), cancellationToken, new object [1]
            {
         roleId
            } );
        }

        Task IProductService.BlockClientAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "BlockClientAsync" ), cancellationToken, new object [2]
            {
         productId,
         clientId
            } );
        }

        Task IProductService.UnBlockClientAsync(
          long productId,
          long clientId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "UnBlockClientAsync" ), cancellationToken, new object [2]
            {
         productId,
         clientId
            } );
        }

        Task IProductService.AddGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddGroupAsync" ), cancellationToken, new object [2]
            {
         productId,
         groupId
            } );
        }

        Task<bool> IProductService.RemoveGroupAsync(
          long productId,
          long groupId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveGroupAsync" ), cancellationToken, new object [2]
            {
         productId,
         groupId
            } );
        }

        Task IProductService.RequestAccessAsync(
          long productId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "RequestAccessAsync" ), cancellationToken, new object [1]
            {
         productId
            } );
        }

        Task<ProductPlugin> IProductService.AddPluginAsync(
          long productId,
          long groupId,
          ProductContentTypes2 contentType,
          CancellationToken cancellationToken )
        {
            return this.Post<ProductPlugin>( RestBaseApiClient.GetCurrentMethod( "AddPluginAsync" ), cancellationToken, new object [3]
            {
         productId,
         groupId,
         contentType
            } );
        }

        Task IProductService.RemovePluginAsync(
          long pluginId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "RemovePluginAsync" ), cancellationToken, new object [1]
            {
         pluginId
            } );
        }

        Task<string [ ]> IProductService.GetSolutionsAsync(
          CancellationToken cancellationToken )
        {
            return this.Get<string [ ]>( RestBaseApiClient.GetCurrentMethod( "GetSolutionsAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task<PublishProject [ ]> IProductService.PreviewProjectsAsync(
          string sln,
          PublishDetails details,
          CancellationToken cancellationToken )
        {
            return this.Post<PublishProject [ ]>( RestBaseApiClient.GetCurrentMethod( "PreviewProjectsAsync" ), cancellationToken, new object [2]
            {
         sln,
         details
            } );
        }

        Task IProductService.PublishAsync(
          string sln,
          PublishDetails details,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "PublishAsync" ), cancellationToken, new object [2]
            {
         sln,
         details
            } );
        }

        Task<string> IProductService.DecodeAsync(
          string value,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "DecodeAsync" ), cancellationToken, new object [1]
            {
         value
            } );
        }

        Task IProductService.UploadSolutionsAsync(
          Guid requestId,
          string [ ] slns,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "UploadSolutionsAsync" ), cancellationToken, new object [2]
            {
         requestId,
         slns
            } );
        }

        Task IProductService.UploadProjectsAsync(
          Guid requestId,
          PublishProject [ ] projects,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "UploadProjectsAsync" ), cancellationToken, new object [2]
            {
         requestId,
         projects
            } );
        }

        Task IProductService.UploadPublishAsync(
          Guid requestId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "UploadPublishAsync" ), cancellationToken, new object [1]
            {
         requestId
            } );
        }

        Task IProductService.UploadDecodeAsync(
          Guid requestId,
          string value,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "UploadDecodeAsync" ), cancellationToken, new object [2]
            {
         requestId,
         value
            } );
        }

        Task IProductService.UploadErrorAsync(
          Guid requestId,
          string error,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "UploadErrorAsync" ), cancellationToken, new object [2]
            {
         requestId,
         error
            } );
        }

        Task<string> IProductService.RefreshFrameworksAsync(
          long productId,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "RefreshFrameworksAsync" ), cancellationToken, new object [1]
            {
         productId
            } );
        }
    }
}
