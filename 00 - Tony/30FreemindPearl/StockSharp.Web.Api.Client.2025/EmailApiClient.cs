// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.EmailApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

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
    internal class EmailApiClient : BaseApiEntityClient<Email>, IEmailService, IBaseEntityService<Email>
    {
        public EmailApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public EmailApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<Email>> IEmailService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          int? maxErrorCount,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<Email>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [9]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         maxErrorCount
            } );
        }

        
        Task<(File mjml, File html)> IEmailService.SaveMjmlAsync(
          string mjmlName,
          long? mjmlId,
          string mjmlText,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<File, File>>( RestBaseApiClient.GetCurrentMethod( "SaveMjmlAsync" ), cancellationToken, new object [3]
            {
         mjmlName,
         mjmlId,
         mjmlText
            } );
        }

        Task<File> IEmailService.TryGetMjmlAsync(
          long htmlId,
          CancellationToken cancellationToken )
        {
            return this.Get<File>( RestBaseApiClient.GetCurrentMethod( "TryGetMjmlAsync" ), cancellationToken, new object [1]
            {
         htmlId
            } );
        }

        Task<File> IEmailService.GetHtmlAsync(
          long mjmlId,
          CancellationToken cancellationToken )
        {
            return this.Get<File>( RestBaseApiClient.GetCurrentMethod( "GetHtmlAsync" ), cancellationToken, new object [1]
            {
         mjmlId
            } );
        }

        Task<string> IEmailService.RenderMjmlAsync(
          string mjml,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "RenderMjmlAsync" ), cancellationToken, new object [1]
            {
         mjml
            } );
        }

        Task IEmailService.EmailAsSpamAsync(
          string email,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "EmailAsSpamAsync" ), cancellationToken, new object [1]
            {
         email
            } );
        }

        Task IEmailService.EmailBouncedAsync(
          string email,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "EmailBouncedAsync" ), cancellationToken, new object [1]
            {
         email
            } );
        }

        Task IEmailService.EmailDeliveredAsync(
          string email,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "EmailDeliveredAsync" ), cancellationToken, new object [1]
            {
         email
            } );
        }
    }
}
