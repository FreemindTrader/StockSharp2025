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
            return this.Get<BaseEntitySet<FileShare>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)fileId);
        }

        Task<FileShare> IFileShareService.GetByTokenAsync(
          string token,
          CancellationToken cancellationToken)
        {
            return this.Get<FileShare>(RestBaseApiClient.GetCurrentMethod("GetByTokenAsync"), cancellationToken, (object)token);
        }

        Task IFileShareService.RemoveByFileIdAsync(
          long fileId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("RemoveByFileIdAsync"), cancellationToken, (object)fileId);
        }
    }
}
