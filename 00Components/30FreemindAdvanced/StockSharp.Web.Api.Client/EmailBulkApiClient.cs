// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.EmailBulkApiClient
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

internal class EmailBulkApiClient :
  BaseApiEntityClient<EmailBulk>,
  IEmailBulkService,
  IBaseEntityService<EmailBulk>
{
    public EmailBulkApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public EmailBulkApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
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
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<EmailBulk>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[12]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) domainId,
      (object) emailPreferences,
      (object) like,
      (object) likeCompare
        });
    }

    Task<int> IEmailBulkService.StartAsync(long bulkId, CancellationToken cancellationToken)
    {
        return this.Post<int>(RestBaseApiClient.GetCurrentMethod("StartAsync"), cancellationToken, new object[1]
        {
      (object) bulkId
        });
    }

    Task IEmailBulkService.StopAsync(long bulkId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("StopAsync"), cancellationToken, new object[1]
        {
      (object) bulkId
        });
    }

    Task IEmailBulkService.FinishAsync(long bulkId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("FinishAsync"), cancellationToken, new object[1]
        {
      (object) bulkId
        });
    }

    Task<EmailBulk> IEmailBulkService.ShortUrlsAsync(long bulkId, CancellationToken cancellationToken)
    {
        return this.Post<EmailBulk>(RestBaseApiClient.GetCurrentMethod("ShortUrlsAsync"), cancellationToken, new object[1]
        {
      (object) bulkId
        });
    }
}
