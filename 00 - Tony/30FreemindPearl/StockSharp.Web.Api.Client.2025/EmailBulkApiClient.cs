// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.EmailBulkApiClient
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
    internal class EmailBulkApiClient : BaseApiEntityClient<EmailBulk>, IEmailBulkService, IBaseEntityService<EmailBulk>
    {
        public EmailBulkApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public EmailBulkApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<EmailBulk>> IEmailBulkService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? domainId,
          EmailPreferences? emailPreferences,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<EmailBulk>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [12]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         domainId,
         emailPreferences,
         like,
         likeCompare
            } );
        }

        Task<int> IEmailBulkService.StartAsync(
          long bulkId,
          CancellationToken cancellationToken )
        {
            return this.Post<int>( RestBaseApiClient.GetCurrentMethod( "StartAsync" ), cancellationToken, new object [1]
            {
         bulkId
            } );
        }

        Task IEmailBulkService.StopAsync(
          long bulkId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "StopAsync" ), cancellationToken, new object [1]
            {
         bulkId
            } );
        }

        Task IEmailBulkService.FinishAsync(
          long bulkId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "FinishAsync" ), cancellationToken, new object [1]
            {
         bulkId
            } );
        }

        Task<EmailBulk> IEmailBulkService.ShortUrlsAsync(
          long bulkId,
          CancellationToken cancellationToken )
        {
            return this.Post<EmailBulk>( RestBaseApiClient.GetCurrentMethod( "ShortUrlsAsync" ), cancellationToken, new object [1]
            {
         bulkId
            } );
        }
    }
}
