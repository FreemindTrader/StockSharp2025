﻿using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class DynamicPageApiClient : BaseApiEntityClient<DynamicPage>, IDynamicPageService, IBaseEntityService<DynamicPage>, IBaseService
    {
        public DynamicPageApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public DynamicPageApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<DynamicPage>> IDynamicPageService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? parentId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<DynamicPage>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, parentId );
        }

        Task<string> IDynamicPageService.GetFullUrlAsync(
          long pageId,
          long domainId,
          CancellationToken cancellationToken)
        {
            return Get<string>( GetCurrentMethod( "GetFullUrlAsync"), cancellationToken, pageId, domainId );
        }
    }
}
