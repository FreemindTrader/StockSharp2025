using Ecng.Net;
using StockSharp.Web.Api.Interfaces;

using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class TopicTagApiClient : BaseApiEntityClient<TopicTag>, ITopicTagService, IBaseEntityService<TopicTag>, IBaseService
    {
        public TopicTagApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public TopicTagApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<TopicTag>> ITopicTagService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? topicId,
          TopicTypes? topicType,
          long? domainId,
          bool? my,
          bool? aggregated,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<TopicTag>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, topicId, topicType, domainId, my, aggregated, like, likeCompare );
        }

        Task<TopicTag> ITopicTagService.GetByNameAsync(
          string name,
          CancellationToken cancellationToken)
        {
            return Get<TopicTag>( GetCurrentMethod( "GetByNameAsync"), cancellationToken, name );
        }
    }
}
