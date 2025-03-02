
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class SessionClient : BaseOfflineClient<Session>, ISessionService, IBaseEntityService<Session>, IBaseService
    {
        Task ISessionService.RemoveAllSessionAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
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
            throw new NotSupportedException();
        }
    }
}
