using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class MessageHistoryApiClient : BaseApiEntityClient<MessageHistory>, IMessageHistoryService, IBaseEntityService<MessageHistory>, IBaseService
    {
        public MessageHistoryApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public MessageHistoryApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<MessageHistory>> IMessageHistoryService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? messageId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<MessageHistory>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, messageId );
        }
    }
}
