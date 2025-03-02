using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class FileShareApiClient : BaseApiEntityClient<FileShare>, IFileShareService, IBaseEntityService<FileShare>, IBaseService
    {
        public FileShareApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public FileShareApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<FileShare>> IFileShareService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? fileId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<FileShare>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, fileId );
        }

        Task<FileShare> IFileShareService.GetByTokenAsync(
          string token,
          CancellationToken cancellationToken)
        {
            return Get<FileShare>( GetCurrentMethod( "GetByTokenAsync"), cancellationToken, token );
        }

        Task IFileShareService.RemoveByFileIdAsync(
          long fileId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "RemoveByFileIdAsync"), cancellationToken, fileId );
        }
    }
}
