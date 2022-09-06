using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class DiagramStrategyApiClient : BaseApiEntityClient<DiagramStrategy>, IDiagramStrategyService, IBaseEntityService<DiagramStrategy>, IBaseService
    {
        public DiagramStrategyApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public DiagramStrategyApiClient(HttpMessageInvoker http, string login, SecureString password) : base(http, login, password)
        {
        }

        Task<BaseEntitySet<DiagramStrategy>> IDiagramStrategyService.FindAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<DiagramStrategy>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc);
        }
    }
}
