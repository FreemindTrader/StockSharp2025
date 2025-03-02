﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.AccountRequisitesApiClient
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
    internal class AccountRequisitesApiClient : BaseApiEntityClient<AccountRequisites>, IAccountRequisitesService, IBaseEntityService<AccountRequisites>
    {
        public AccountRequisitesApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public AccountRequisitesApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<AccountRequisites>> IAccountRequisitesService.FindAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, bool? totalCount, DateTime? creationStart, DateTime? creationEnd, long? clientId, string bankId, string like, ComparisonOperator? likeCompare, CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<AccountRequisites>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [12]
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
         bankId,
         like,
         likeCompare
            } );
        }
    }
}
