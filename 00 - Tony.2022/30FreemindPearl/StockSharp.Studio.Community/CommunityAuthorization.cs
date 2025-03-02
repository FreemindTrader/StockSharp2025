
using Ecng.Common;
using Ecng.Security;
using Nito.AsyncEx;
using StockSharp.Localization;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community
{
    /// <summary>
    /// The module of the connection access check based on the StockSharp authorization.
    /// </summary>
    public class CommunityAuthorization : IAuthorization
    {
        private readonly long _productId;
        private readonly string _version;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Studio.Community.CommunityAuthorization" />.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="version">Version.</param>
        public CommunityAuthorization(long productId, string version)
        {
            if (version.IsEmpty())
                throw new ArgumentNullException(nameof(version));
            _productId = productId;
            _version = version;
        }

        /// <summary>To check the username and password on correctness.</summary>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <param name="clientAddress">Remote network address.</param>
        /// <returns>Session ID.</returns>
        public virtual string ValidateCredentials( string login, SecureString password, IPAddress clientAddress)
        {
            ISessionService svc = CommunityServicesRegistry.ApiServiceProvider.GetService<ISessionService>(login, password);
            try
            {
                return AsyncContext.Run( () =>
               {
                   ISessionService sessionService = svc;
                   Session entity = new Session();
                   entity.Product = new Product()
                   {
                       Id = _productId
                   };
                   entity.Version = _version;
                   CancellationToken cancellationToken = new CancellationToken();
                   return sessionService.AddAsync( entity, cancellationToken );
               } ).Id.To<string>();
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException(LocalizedStrings.WrongLoginOrPassword, ex);
            }
        }
    }
}
