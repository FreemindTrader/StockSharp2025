
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class MessageVoteApiClient : BaseApiEntityClient<MessageVote>, IMessageVoteService, IBaseEntityService<MessageVote>, IBaseService
    {
        public MessageVoteApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public MessageVoteApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<MessageVote>> IMessageVoteService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? messageId,
          long? topicId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<MessageVote>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)messageId, (object)topicId);
        }
    }
}
