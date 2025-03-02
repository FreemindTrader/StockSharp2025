
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class FileDownloadApiClient : BaseApiEntityClient<FileDownload>, IFileDownloadService, IBaseEntityService<FileDownload>, IBaseService
    {
        public FileDownloadApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public FileDownloadApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<FileDownload>> IFileDownloadService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? fileId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<FileDownload>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, fileId );
        }
    }
}
