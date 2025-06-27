// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProductBugReportApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

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
    internal class ProductBugReportApiClient : BaseApiEntityClient<ProductBugReport>, IProductBugReportService, IBaseEntityService<ProductBugReport>
    {
        public ProductBugReportApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public ProductBugReportApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<ProductBugReport>> IProductBugReportService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? clientId,
          long? productId,
          long? errorId,
          bool? aggregated,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<ProductBugReport>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [12]
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
         productId,
         errorId,
         aggregated
            } );
        }

        Task<ProductBugReport> IProductBugReportService.TryProposeAsync(
          ProductBugReport entity,
          CancellationToken cancellationToken )
        {
            return this.Post<ProductBugReport>( RestBaseApiClient.GetCurrentMethod( "TryProposeAsync" ), cancellationToken, new object [1]
            {
         entity
            } );
        }
    }
}
