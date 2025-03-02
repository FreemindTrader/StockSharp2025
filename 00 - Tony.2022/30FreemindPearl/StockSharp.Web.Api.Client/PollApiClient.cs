
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
            return Get<BaseEntitySet<Poll>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId );
        }

        Task<PollChoice> IPollService.AddChoiceAsync(
          PollChoice choice,
          CancellationToken cancellationToken)
        {
            return Post<PollChoice>( GetCurrentMethod( "AddChoiceAsync"), cancellationToken, choice );
        }

        Task<bool> IPollService.RemoveChoiceAsync(
          long choiceId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveChoiceAsync"), cancellationToken, choiceId );
        }

        Task<PollVote> IPollService.AddVoteAsync(
          long choiceId,
          CancellationToken cancellationToken)
        {
            return Post<PollVote>( GetCurrentMethod( "AddVoteAsync"), cancellationToken, choiceId );
        }

        Task<bool> IPollService.RemoveVoteAsync(
          long voteId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveVoteAsync"), cancellationToken, voteId );
        }
    }
}
