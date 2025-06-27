// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.AppApiClient
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
    internal class AppApiClient : BaseApiEntityClient<App>, IAppService, IBaseEntityService<App>, ICommandService<App>
    {
        public AppApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public AppApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<App>> IAppService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? clientId,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<App>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [11]
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
         like,
         likeCompare
            } );
        }

        Task<bool> IAppService.DeleteByUserIdAsync(
          string userId,
          CancellationToken cancellationToken )
        {
            return this.Delete<bool>( RestBaseApiClient.GetCurrentMethod( "DeleteByUserIdAsync" ), cancellationToken, new object [1]
            {
         userId
            } );
        }

        Task ICommandService<App>.SendAsync(
          long? id,
          string userId,
          CommandInfo command,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "SendAsync" ), cancellationToken, new object [3]
            {
         id,
         userId,
         command
            } );
        }

        Task ICommandService<App>.UpdateStatusAsync(
          App status,
          CancellationToken cancellationToken )
        {
            return this.Put( RestBaseApiClient.GetCurrentMethod( "UpdateStatusAsync" ), cancellationToken, new object [1]
            {
         status
            } );
        }
    }
}
