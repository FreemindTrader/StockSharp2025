using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class TopicApiClient : BaseApiEntityClient<Topic>, ITopicService, IBaseEntityService<Topic>, IBaseService
    {
        public TopicApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public TopicApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Topic>> ITopicService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? domainId,
          long? clientId,
          long? groupId,
          long? tagId,
          string tagName,
          TopicTypes? topicType,
          TopicFlags? flags,
          bool? my,
          bool? hasGroups,
          bool? convertBodyToHtml,
          int? truncate,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<Topic>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)domainId, (object)clientId, (object)groupId, (object)tagId, (object)tagName, (object)topicType, (object)flags, (object)my, (object)hasGroups, (object)convertBodyToHtml, (object)truncate);
        }

        Task<Topic> ITopicService.UpdateFlagsAsync(
          long topicId,
          TopicFlags flags,
          CancellationToken cancellationToken)
        {
            return this.Put<Topic>(RestBaseApiClient.GetCurrentMethod("UpdateFlagsAsync"), cancellationToken, (object)topicId, (object)flags);
        }
    }
}
