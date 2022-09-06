
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class MessageClient : BaseOfflineClient<Message>, IMessageService, IBaseEntityService<Message>, IBaseService
    {
        Task<Message> IBaseEntityService<Message>.AddAsync(
          Message entity,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<Message[]> IBaseEntityService<Message>.AddBatchAsync(
          Message[] entities,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<string> IMessageService.BodyCleanAsync(
          string body,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(body);
        }

        Task<string> IMessageService.BodyToHtmlAsync(
          string body,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(body);
        }

        Task<bool> IBaseEntityService<Message>.DeleteAsync(
          long id,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
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
            throw new NotSupportedException();
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
            return Task.FromResult<BaseEntitySet<SystemMessage>>(((IEnumerable<SystemMessage>)Array.Empty<SystemMessage>()).ToEntitySet<SystemMessage>(0));
        }

        Task<Message> IBaseEntityService<Message>.GetAsync(
          long id,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<Message[]> IBaseEntityService<Message>.GetMultiAsync(
          long[] ids,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<BaseEntitySet<Message>> IBaseEntityService<Message>.GetRangeAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<string> IMessageService.MessageCleanAsync(
          long messageId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<string> IMessageService.MessageToHtmlAsync(
          long messageId,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<string> IMessageService.PageToHtmlAsync(
          long pageId,
          long domainId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<object> IMessageService.GetJsonLDAsync(
          long domainId,
          long? pageId,
          long? productId,
          long? clientId,
          long? topicId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<Message> IMessageService.GetWithPageAsync(
          long messageId,
          int pageSize,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IBaseEntityService<Message>.RestoreAsync(
          long id,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<Message> IBaseEntityService<Message>.UpdateAsync(
          Message entity,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}
