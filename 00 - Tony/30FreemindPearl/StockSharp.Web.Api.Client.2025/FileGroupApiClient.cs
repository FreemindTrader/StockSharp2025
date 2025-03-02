// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.FileGroupApiClient
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
    internal class FileGroupApiClient : BaseApiEntityClient<FileGroup>, IFileGroupService, IBaseEntityService<FileGroup>
    {
        public FileGroupApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public FileGroupApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<FileGroup>> IFileGroupService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? parentId,
          long? childId,
          long? fileId,
          long? clientId,
          long? ownerId,
          bool? distributiveOnly,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<FileGroup>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [16]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         parentId,
         childId,
         fileId,
         clientId,
         ownerId,
         distributiveOnly,
         like,
         likeCompare
            } );
        }

        Task IFileGroupService.AddChildAsync(
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

        Task<bool> IFileGroupService.RemoveChildAsync(
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

        Task IFileGroupService.AddFileAsync(
          long groupId,
          long fileId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddFileAsync" ), cancellationToken, new object [2]
            {
         groupId,
         fileId
            } );
        }

        Task<bool> IFileGroupService.RemoveFileAsync(
          long groupId,
          long fileId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveFileAsync" ), cancellationToken, new object [2]
            {
         groupId,
         fileId
            } );
        }

        Task IFileGroupService.AddRoleAsync(
          long groupId,
          long roleId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddRoleAsync" ), cancellationToken, new object [2]
            {
         groupId,
         roleId
            } );
        }

        Task<bool> IFileGroupService.RemoveRoleAsync(
          long groupId,
          long roleId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveRoleAsync" ), cancellationToken, new object [2]
            {
         groupId,
         roleId
            } );
        }
    }
}
