
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class DynamicMenuApiClient : BaseApiEntityClient<DynamicMenu>, IDynamicMenuService, IBaseEntityService<DynamicMenu>, IBaseService
    {
        public DynamicMenuApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public DynamicMenuApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<DynamicMenu>> IDynamicMenuService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          DynamicMenuLocations? location,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<DynamicMenu>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)location);
        }

        Task IDynamicMenuService.AddRoleAsync(
          long menuId,
          long roleId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddRoleAsync"), cancellationToken, (object)menuId, (object)roleId);
        }

        Task<bool> IDynamicMenuService.RemoveRoleAsync(
          long menuId,
          long roleId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, (object)menuId, (object)roleId);
        }
    }
}
