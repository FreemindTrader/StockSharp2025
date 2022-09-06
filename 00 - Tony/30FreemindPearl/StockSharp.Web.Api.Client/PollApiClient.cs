
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class PollApiClient : BaseApiEntityClient<Poll>, IPollService, IBaseEntityService<Poll>, IBaseService
    {
        public PollApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public PollApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Poll>> IPollService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<Poll>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId);
        }

        Task<PollChoice> IPollService.AddChoiceAsync(
          PollChoice choice,
          CancellationToken cancellationToken)
        {
            return this.Post<PollChoice>(RestBaseApiClient.GetCurrentMethod("AddChoiceAsync"), cancellationToken, (object)choice);
        }

        Task<bool> IPollService.RemoveChoiceAsync(
          long choiceId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveChoiceAsync"), cancellationToken, (object)choiceId);
        }

        Task<PollVote> IPollService.AddVoteAsync(
          long choiceId,
          CancellationToken cancellationToken)
        {
            return this.Post<PollVote>(RestBaseApiClient.GetCurrentMethod("AddVoteAsync"), cancellationToken, (object)choiceId);
        }

        Task<bool> IPollService.RemoveVoteAsync(
          long voteId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveVoteAsync"), cancellationToken, (object)voteId);
        }
    }
}
