// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProductGroupApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    internal class ProductGroupApiClient : BaseApiEntityClient<ProductGroup>, IProductGroupService, IBaseEntityService<ProductGroup>
    {
        public ProductGroupApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public ProductGroupApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<ProductGroup>> IProductGroupService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? productId,
          long? childId,
          long? parentId,
          long? managerId,
          long? emailTemplateId,
          bool? expandInner,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<ProductGroup>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [16]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         productId,
         childId,
         parentId,
         managerId,
         emailTemplateId,
         expandInner,
         like,
         likeCompare
            } );
        }

        Task IProductGroupService.AddChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddChildAsync" ), cancellationToken, new object [2]
            {
         parentId,
         childId
            } );
        }

        Task<bool> IProductGroupService.RemoveChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveChildAsync" ), cancellationToken, new object [2]
            {
         parentId,
         childId
            } );
        }

        Task IProductGroupService.AddManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddManagerAsync" ), cancellationToken, new object [2]
            {
         groupId,
         clientId
            } );
        }

        Task<bool> IProductGroupService.RemoveManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveManagerAsync" ), cancellationToken, new object [2]
            {
         groupId,
         clientId
            } );
        }
    }
}
