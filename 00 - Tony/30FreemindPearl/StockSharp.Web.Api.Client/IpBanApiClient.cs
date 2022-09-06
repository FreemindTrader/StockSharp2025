
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class IpBanApiClient : BaseApiEntityClient<IpBan>, IIpBanService, IBaseEntityService<IpBan>, IBaseService
    {
        public IpBanApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public IpBanApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<IpBan>> IIpBanService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          string address,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<IpBan>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)address);
        }
    }
}
