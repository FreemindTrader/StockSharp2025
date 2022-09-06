
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class TempSaltApiClient : BaseApiEntityClient<TempSalt>, ITempSaltService, IBaseEntityService<TempSalt>, IBaseService
    {
        public TempSaltApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public TempSaltApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<TempSalt>> ITempSaltService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<TempSalt>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc);
        }
    }
}
