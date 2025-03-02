
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
            return Get<BaseEntitySet<DynamicMenu>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, location );
        }

        Task IDynamicMenuService.AddRoleAsync(
          long menuId,
          long roleId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddRoleAsync"), cancellationToken, menuId, roleId );
        }

        Task<bool> IDynamicMenuService.RemoveRoleAsync(
          long menuId,
          long roleId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveRoleAsync"), cancellationToken, menuId, roleId );
        }
    }
}
