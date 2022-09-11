
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
            return Get<BaseEntitySet<FileGroup>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, parentId, childId, fileId, nestedGroups, clientId, distributiveOnly, like, likeCompare );
        }

        Task IFileGroupService.AddChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddChildAsync"), cancellationToken, parentId, childId );
        }

        Task<bool> IFileGroupService.RemoveChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveChildAsync"), cancellationToken, parentId, childId );
        }

        Task IFileGroupService.AddFileAsync(
          long groupId,
          long fileId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddFileAsync"), cancellationToken, groupId, fileId );
        }

        Task<bool> IFileGroupService.RemoveFileAsync(
          long groupId,
          long fileId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveFileAsync"), cancellationToken, groupId, fileId );
        }

        Task IFileGroupService.AddRoleAsync(
          long groupId,
          long roleId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddRoleAsync"), cancellationToken, groupId, roleId );
        }

        Task<bool> IFileGroupService.RemoveRoleAsync(
          long groupId,
          long roleId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveRoleAsync"), cancellationToken, groupId, roleId );
        }
    }
}
