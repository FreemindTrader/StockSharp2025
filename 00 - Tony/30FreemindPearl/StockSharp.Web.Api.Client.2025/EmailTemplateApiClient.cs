// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.EmailTemplateApiClient
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
using System.Xml.Linq;

namespace StockSharp.Web.Api.Client
{
    internal class EmailTemplateApiClient : BaseApiEntityClient<EmailTemplate>, IEmailTemplateService, IBaseEntityService<EmailTemplate>
    {
        public EmailTemplateApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public EmailTemplateApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<EmailTemplate>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [15]
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
         productGroupId,
         textId,
         htmlId,
         enabled,
         like,
         likeCompare
            } );
        }

        Task IEmailTemplateService.TestProductAsync(
          string templateName,
          CurrencyTypes currency,
          long? productId,
          long? groupId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "TestProductAsync" ), cancellationToken, new object [4]
            {
         templateName,
         currency,
         productId,
         groupId
            } );
        }

        Task IEmailTemplateService.TestAsync(
          long templateId,
          long? domainId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "TestAsync" ), cancellationToken, new object [2]
            {
         templateId,
         domainId
            } );
        }
        
        Task<BaseEntitySet<(string name, string defaultValue)>> IEmailTemplateService.GetTemplateParamsAsync(
          long templateId,
          long domainId,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<ValueTuple<string, string>>>( RestBaseApiClient.GetCurrentMethod( "GetTemplateParamsAsync" ), cancellationToken, new object [2]
            {
         templateId,
         domainId
            } );
        }
    }
}
