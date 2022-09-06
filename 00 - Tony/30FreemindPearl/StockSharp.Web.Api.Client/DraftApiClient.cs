
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class DraftApiClient : BaseApiEntityClient<Draft>, IDraftService, IBaseEntityService<Draft>, IBaseService
    {
        public DraftApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public DraftApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Draft>> IDraftService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<Draft>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId);
        }

        Task<Draft> IDraftService.TryGetByPageIdAsync(
          string pageId,
          CancellationToken cancellationToken)
        {
            return this.Post<Draft>(RestBaseApiClient.GetCurrentMethod("TryGetByPageIdAsync"), cancellationToken, (object)pageId);
        }

        Task<bool> IDraftService.RemoveByPageAsync(
          string pageId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveByPageAsync"), cancellationToken, (object)pageId);
        }

        Task<bool> IDraftService.RemoveFileAsync(
          long fileId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveFileAsync"), cancellationToken, (object)fileId);
        }
    }
}
