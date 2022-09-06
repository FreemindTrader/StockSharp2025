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
            return this.Get<BaseEntitySet<TopicTag>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)topicId, (object)topicType, (object)domainId, (object)my, (object)aggregated, (object)like, (object)likeCompare);
        }

        Task<TopicTag> ITopicTagService.GetByNameAsync(
          string name,
          CancellationToken cancellationToken)
        {
            return this.Get<TopicTag>(RestBaseApiClient.GetCurrentMethod("GetByNameAsync"), cancellationToken, (object)name);
        }
    }
}
