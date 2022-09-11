
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
            return Get<BaseEntitySet<Draft>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId );
        }

        Task<Draft> IDraftService.TryGetByPageIdAsync(
          string pageId,
          CancellationToken cancellationToken)
        {
            return Post<Draft>( GetCurrentMethod( "TryGetByPageIdAsync"), cancellationToken, pageId );
        }

        Task<bool> IDraftService.RemoveByPageAsync(
          string pageId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveByPageAsync"), cancellationToken, pageId );
        }

        Task<bool> IDraftService.RemoveFileAsync(
          long fileId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveFileAsync"), cancellationToken, fileId );
        }
    }
}
