
using StockSharp.Web.DomainModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IClientContext
    {
        string ClientToken { get; }

        bool AsUser { get; }

        IPAddress Address { get; }

        IPAddress ConnectionAddress { get; }

        bool IsSafeAddress { get; }

        bool IsExtended { get; }

        Task<Client> GetCurrentClientAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
