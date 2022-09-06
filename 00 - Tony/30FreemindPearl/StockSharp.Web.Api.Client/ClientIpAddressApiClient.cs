
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ClientIpAddressApiClient : BaseApiEntityClient<ClientIpAddress>, IClientIpAddressService, IBaseEntityService<ClientIpAddress>, IBaseService
    {
        public ClientIpAddressApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ClientIpAddressApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ClientIpAddress>> IClientIpAddressService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          string address,
          long? clientId,
          long? productId,
          long? messageId,
          long? fileId,
          long? paymentId,
          long? orderId,
          long? licenseId,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<ClientIpAddress>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)address, (object)clientId, (object)productId, (object)messageId, (object)fileId, (object)paymentId, (object)orderId, (object)licenseId, (object)like, (object)likeCompare);
        }
    }
}
