// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.EmailApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class EmailApiClient : BaseApiEntityClient<Email>, IEmailService, IBaseEntityService<Email>
{
    public EmailApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public EmailApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
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
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Email>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[9]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) maxErrorCount
        });
    }

    Task<(File mjml, File html)> IEmailService.SaveMjmlAsync(
      string mjmlName,
      long? mjmlId,
      string mjmlText,
      CancellationToken cancellationToken)
    {
        return this.Post<(File, File)>(RestBaseApiClient.GetCurrentMethod("SaveMjmlAsync"), cancellationToken, new object[3]
        {
      (object) mjmlName,
      (object) mjmlId,
      (object) mjmlText
        });
    }

    Task<File> IEmailService.TryGetMjmlAsync(long htmlId, CancellationToken cancellationToken)
    {
        return this.Get<File>(RestBaseApiClient.GetCurrentMethod("TryGetMjmlAsync"), cancellationToken, new object[1]
        {
      (object) htmlId
        });
    }

    Task<File> IEmailService.GetHtmlAsync(long mjmlId, CancellationToken cancellationToken)
    {
        return this.Get<File>(RestBaseApiClient.GetCurrentMethod("GetHtmlAsync"), cancellationToken, new object[1]
        {
      (object) mjmlId
        });
    }

    Task<string> IEmailService.RenderMjmlAsync(string mjml, CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("RenderMjmlAsync"), cancellationToken, new object[1]
        {
      (object) mjml
        });
    }

    Task IEmailService.EmailAsSpamAsync(string email, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("EmailAsSpamAsync"), cancellationToken, new object[1]
        {
      (object) email
        });
    }

    Task IEmailService.EmailBouncedAsync(string email, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("EmailBouncedAsync"), cancellationToken, new object[1]
        {
      (object) email
        });
    }

    Task IEmailService.EmailDeliveredAsync(string email, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("EmailDeliveredAsync"), cancellationToken, new object[1]
        {
      (object) email
        });
    }
}
