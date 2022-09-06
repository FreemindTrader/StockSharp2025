
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
            return this.Get<BaseEntitySet<Session>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)productId, (object)day, (object)aggregated);
        }

        Task ISessionService.RemoveAllSessionAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("RemoveAllSessionAsync"), cancellationToken, (object)clientId);
        }
    }
}
