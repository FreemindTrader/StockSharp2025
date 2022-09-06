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
            return this.Get<BaseEntitySet<Message>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)pm, (object)domainId, (object)clientId, (object)topicId, (object)parentId, (object)excludeRoot, (object)rootsOnly, (object)convertBodyToHtml, (object)truncate, (object)like, (object)likeCompare);
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
            return this.Get<BaseEntitySet<SystemMessage>>(RestBaseApiClient.GetCurrentMethod("FindSystemAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)domainId);
        }

        Task<string> IMessageService.BodyCleanAsync(
          string body,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("BodyCleanAsync"), cancellationToken, (object)body);
        }

        Task<string> IMessageService.MessageCleanAsync(
          long messageId,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("MessageCleanAsync"), cancellationToken, (object)messageId);
        }

        Task<string> IMessageService.BodyToHtmlAsync(
          string body,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("BodyToHtmlAsync"), cancellationToken, (object)body, (object)domainId, (object)truncate, (object)preventScaling);
        }

        Task<string> IMessageService.MessageToHtmlAsync(
          long messageId,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("MessageToHtmlAsync"), cancellationToken, (object)messageId, (object)domainId, (object)truncate, (object)preventScaling);
        }

        Task<string> IMessageService.PageToHtmlAsync(
          long pageId,
          long domainId,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("PageToHtmlAsync"), cancellationToken, (object)pageId, (object)domainId);
        }

        Task<object> IMessageService.GetJsonLDAsync(
          long domainId,
          long? pageId,
          long? productId,
          long? clientId,
          long? topicId,
          CancellationToken cancellationToken)
        {
            return this.Get<object>(RestBaseApiClient.GetCurrentMethod("GetJsonLDAsync"), cancellationToken, (object)domainId, (object)pageId, (object)productId, (object)clientId, (object)topicId);
        }

        Task<Message> IMessageService.GetWithPageAsync(
          long messageId,
          int pageSize,
          CancellationToken cancellationToken)
        {
            return this.Get<Message>(RestBaseApiClient.GetCurrentMethod("GetWithPageAsync"), cancellationToken, (object)messageId, (object)pageSize);
        }
    }
}
