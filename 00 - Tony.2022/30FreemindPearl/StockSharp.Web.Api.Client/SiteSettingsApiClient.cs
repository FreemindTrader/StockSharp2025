using Ecng.Net;
using Ecng.Net.Captcha;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class SiteSettingsApiClient : BaseApiEntityClient<SiteSettings>, ISiteSettingsService, IBaseEntityService<SiteSettings>, IBaseService, ICaptchaService, ICaptchaValidator<bool>
    {
        public SiteSettingsApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public SiteSettingsApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<SiteSettings>> ISiteSettingsService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<SiteSettings>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc );
        }

        Task ISiteSettingsService.ResetCacheAsync(
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ResetCacheAsync"), cancellationToken, Array.Empty<object>());
        }

        Task<byte[]> ISiteSettingsService.SignAsync(
          byte[] data,
          CancellationToken cancellationToken)
        {
            return Post<byte[]>( GetCurrentMethod( "SignAsync"), cancellationToken, data );
        }

        Task<(string value1, string value2, string value3)> ISiteSettingsService.EncryptUrlAsync(
          string value1,
          string value2,
          string value3,
          CancellationToken cancellationToken)
        {
            return Post<ValueTuple<string, string, string>>( GetCurrentMethod( "EncryptUrlAsync"), cancellationToken, value1, value2, value3 );
        }

        Task<(string value1, string value2, string value3)> ISiteSettingsService.DecryptUrlAsync(
          string value1,
          string value2,
          string value3,
          CancellationToken cancellationToken)
        {
            return Post<ValueTuple<string, string, string>>( GetCurrentMethod( "DecryptUrlAsync"), cancellationToken, value1, value2, value3 );
        }

        Task<float> ICaptchaService.GetScoreAsync(
          IPAddress address,
          CancellationToken cancellationToken)
        {
            return Get<float>( GetCurrentMethod( "GetScoreAsync"), cancellationToken, address );
        }

        Task<BaseEntitySet<(string address, DateTime time, float? score, string message)>> ICaptchaService.GetPendingAsync(
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<ValueTuple<string, DateTime, float?, string>>>( GetCurrentMethod( "GetPendingAsync"), cancellationToken);
        }

        Task<bool> ICaptchaValidator<bool>.ValidateAsync(
          string response,
          string address,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "ValidateAsync"), cancellationToken, response, address );
        }
    }
}
