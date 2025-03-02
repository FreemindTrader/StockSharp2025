using Ecng.Net.Captcha;
using StockSharp.Web.DomainModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface ISiteSettingsService : IBaseEntityService<SiteSettings>, IBaseService, ICaptchaService, ICaptchaValidator<bool>
    {
        Task<BaseEntitySet<SiteSettings>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task ResetCacheAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<byte[]> SignAsync(byte[] data, CancellationToken cancellationToken = default(CancellationToken));

        Task<(string value1, string value2, string value3)> EncryptUrlAsync(string value1, string value2, string value3, CancellationToken cancellationToken = default(CancellationToken));

        Task<(string value1, string value2, string value3)> DecryptUrlAsync(string value1, string value2, string value3, CancellationToken cancellationToken = default(CancellationToken));        
    }
}
