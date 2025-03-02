
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
    public class SessionApiClient : BaseApiEntityClient<Session>, ISessionService, IBaseEntityService<Session>, IBaseService
    {
        public SessionApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public SessionApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Session>> ISessionService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? productId,
          DateTime? day,
          bool? aggregated,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<Session>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, productId, day, aggregated );
        }

        Task ISessionService.RemoveAllSessionAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "RemoveAllSessionAsync"), cancellationToken, clientId );
        }
    }
}
