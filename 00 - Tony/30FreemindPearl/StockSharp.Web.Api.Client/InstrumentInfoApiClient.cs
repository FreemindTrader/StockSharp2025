
using Ecng.Net;
using StockSharp.Messages;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class InstrumentInfoApiClient : BaseApiEntityClient<InstrumentInfo>, IInstrumentInfoService, IBaseEntityService<InstrumentInfo>, IBaseService
    {
        public InstrumentInfoApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public InstrumentInfoApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<InstrumentInfo>> IInstrumentInfoService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          SecurityTypes? type,
          string assetLike,
          LikeCompares? assetLikeCompare,
          DateTime? expiryDate,
          DateTime? settlDate,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<InstrumentInfo>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)type, (object)assetLike, (object)assetLikeCompare, (object)expiryDate, (object)settlDate, (object)like, (object)likeCompare);
        }
    }
}
