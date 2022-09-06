
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class FileGroupApiClient : BaseApiEntityClient<FileGroup>, IFileGroupService, IBaseEntityService<FileGroup>, IBaseService
    {
        public FileGroupApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public FileGroupApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<FileGroup>> IFileGroupService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? parentId,
          long? childId,
          long? fileId,
          bool? nestedGroups,
          long? clientId,
          bool? distributiveOnly,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<FileGroup>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)parentId, (object)childId, (object)fileId, (object)nestedGroups, (object)clientId, (object)distributiveOnly, (object)like, (object)likeCompare);
        }

        Task IFileGroupService.AddChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddChildAsync"), cancellationToken, (object)parentId, (object)childId);
        }

        Task<bool> IFileGroupService.RemoveChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveChildAsync"), cancellationToken, (object)parentId, (object)childId);
        }

        Task IFileGroupService.AddFileAsync(
          long groupId,
          long fileId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddFileAsync"), cancellationToken, (object)groupId, (object)fileId);
        }

        Task<bool> IFileGroupService.RemoveFileAsync(
          long groupId,
          long fileId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveFileAsync"), cancellationToken, (object)groupId, (object)fileId);
        }

        Task IFileGroupService.AddRoleAsync(
          long groupId,
          long roleId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddRoleAsync"), cancellationToken, (object)groupId, (object)roleId);
        }

        Task<bool> IFileGroupService.RemoveRoleAsync(
          long groupId,
          long roleId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, (object)groupId, (object)roleId);
        }
    }
}
