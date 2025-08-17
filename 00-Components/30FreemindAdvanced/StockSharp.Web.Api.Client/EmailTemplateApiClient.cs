// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.EmailTemplateApiClient
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

internal class EmailTemplateApiClient :
  BaseApiEntityClient<EmailTemplate>,
  IEmailTemplateService,
  IBaseEntityService<EmailTemplate>
{
    public EmailTemplateApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public EmailTemplateApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<EmailTemplate>> IEmailTemplateService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? productId,
      long? productGroupId,
      long? textId,
      long? htmlId,
      bool? enabled,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<EmailTemplate>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[15]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) productId,
      (object) productGroupId,
      (object) textId,
      (object) htmlId,
      (object) enabled,
      (object) like,
      (object) likeCompare
        });
    }

    Task IEmailTemplateService.TestProductAsync(
      string templateName,
      CurrencyTypes currency,
      long? productId,
      long? groupId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("TestProductAsync"), cancellationToken, new object[4]
        {
      (object) templateName,
      (object) currency,
      (object) productId,
      (object) groupId
        });
    }

    Task IEmailTemplateService.TestAsync(
      long templateId,
      long? domainId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("TestAsync"), cancellationToken, new object[2]
        {
      (object) templateId,
      (object) domainId
        });
    }

    Task<BaseEntitySet<(string name, string defaultValue)>> IEmailTemplateService.GetTemplateParamsAsync(
      long templateId,
      long domainId,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<(string, string)>>(RestBaseApiClient.GetCurrentMethod("GetTemplateParamsAsync"), cancellationToken, new object[2]
        {
      (object) templateId,
      (object) domainId
        });
    }
}
