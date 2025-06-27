using Ecng.Net.Captcha;
using StockSharp.Web.DomainModel;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface ICaptchaService : ICaptchaValidator<bool>
    {
        public interface ICaptchaService : ICaptchaValidator<bool>
        {
            Task<float> GetScoreAsync( IPAddress address, CancellationToken cancellationToken = default( CancellationToken ) );

            Task<BaseEntitySet<(string address, DateTime time, float? score, string message)>> GetPendingAsync( CancellationToken cancelllationToken = default( CancellationToken ) );
        }
    }
}
