using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class MessageApiClient : BaseApiEntityClient<Message>, IMessageService, IBaseEntityService<Message>, IBaseService
    {
        public MessageApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public MessageApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Message>> IMessageService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? pm,
          long? domainId,
          long? clientId,
          long? topicId,
          long? parentId,
          bool? excludeRoot,
          bool? rootsOnly,
          bool? convertBodyToHtml,
          int? truncate,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<Message>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, pm, domainId, clientId, topicId, parentId, excludeRoot, rootsOnly, convertBodyToHtml, truncate, like, likeCompare );
        }

        Task<BaseEntitySet<SystemMessage>> IMessageService.FindSystemAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? domainId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<SystemMessage>>( GetCurrentMethod( "FindSystemAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, domainId );
        }

        Task<string> IMessageService.BodyCleanAsync(
          string body,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "BodyCleanAsync"), cancellationToken, body );
        }

        Task<string> IMessageService.MessageCleanAsync(
          long messageId,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "MessageCleanAsync"), cancellationToken, messageId );
        }

        Task<string> IMessageService.BodyToHtmlAsync(
          string body,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "BodyToHtmlAsync"), cancellationToken, body, domainId, truncate, preventScaling );
        }

        Task<string> IMessageService.MessageToHtmlAsync(
          long messageId,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "MessageToHtmlAsync"), cancellationToken, messageId, domainId, truncate, preventScaling );
        }

        Task<string> IMessageService.PageToHtmlAsync(
          long pageId,
          long domainId,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "PageToHtmlAsync"), cancellationToken, pageId, domainId );
        }

        Task<object> IMessageService.GetJsonLDAsync(
          long domainId,
          long? pageId,
          long? productId,
          long? clientId,
          long? topicId,
          CancellationToken cancellationToken)
        {
            return Get<object>( GetCurrentMethod( "GetJsonLDAsync"), cancellationToken, domainId, pageId, productId, clientId, topicId );
        }

        Task<Message> IMessageService.GetWithPageAsync(
          long messageId,
          int pageSize,
          CancellationToken cancellationToken)
        {
            return Get<Message>( GetCurrentMethod( "GetWithPageAsync"), cancellationToken, messageId, pageSize );
        }
    }
}
