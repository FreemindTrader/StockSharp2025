using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
    public interface ISerializer<T> : ISerializer
    {
        ValueTask SerializeAsync( T graph, Stream stream, CancellationToken cancellationToken );

        new ValueTask<T> DeserializeAsync( Stream stream, CancellationToken cancellationToken );
    }
}
