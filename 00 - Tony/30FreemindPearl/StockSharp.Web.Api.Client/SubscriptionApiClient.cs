using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class SubscriptionApiClient : BaseApiEntityClient<Subscription>, ISubscriptionService, IBaseEntityService<Subscription>, IBaseService
    {
        public SubscriptionApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public SubscriptionApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Subscription>> ISubscriptionService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? unionOrJoin,
          long? clientId,
          long? topicId,
          bool? includeTags,
          TopicTypes? topicType,
          long? tagId,
          long? author,
          long? domainId,
          bool? checkCanSend,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<Subscription>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, unionOrJoin, clientId, topicId, includeTags, topicType, tagId, author, domainId, checkCanSend );
        }
    }
}
