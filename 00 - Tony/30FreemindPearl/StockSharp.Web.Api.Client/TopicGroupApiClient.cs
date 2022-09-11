using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class TopicGroupApiClient : BaseApiEntityClient<TopicGroup>, ITopicGroupService, IBaseEntityService<TopicGroup>, IBaseService
    {
        public TopicGroupApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public TopicGroupApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<TopicGroup>> ITopicGroupService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? roleId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<TopicGroup>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, roleId );
        }

        Task ITopicGroupService.AddRoleAsync(
          long groupId,
          long roleId,
          bool isRead,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddRoleAsync"), cancellationToken, groupId, roleId, isRead );
        }

        Task<bool> ITopicGroupService.RemoveRoleAsync(
          long groupId,
          long roleId,
          bool isRead,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveRoleAsync"), cancellationToken, groupId, roleId, isRead );
        }
    }
}
