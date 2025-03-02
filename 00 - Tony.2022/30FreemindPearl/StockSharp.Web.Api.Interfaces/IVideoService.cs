using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IVideoService
    {
        Task UpdateInfoAsync(
          string clientToken,
          long fileId,
          string path,
          long length,
          CancellationToken cancellationToken);

        Task<( string path, long length )> GetInfoAsync( long fileId, CancellationToken cancellationToken = default(CancellationToken));

        Task<( string clientToken, long fileId )[]> GetPendingRequestsAsync( CancellationToken cancellationToken = default(CancellationToken));

        //[return: TupleElementNames(new string[] { "clientToken", "fileId" })]
        //Task<ValueTuple<string, long>[]> GetPendingRequestsAsync(
        //  CancellationToken cancellationToken = default(CancellationToken));
    }
}
