using Ecng.Common;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
    public abstract class Serializer<T> : ISerializer<T>, ISerializer
    {
        public abstract string FileExtension { get; }

        string ISerializer.FileExtension
        {
            get
            {
                return this.FileExtension;
            }
        }

        Type ISerializer.Type
        {
            get
            {
                return typeof( T );
            }
        }

        public Serializer<TData> GetSerializer<TData>()
        {
            return ( Serializer<TData> )this.GetSerializer( typeof( TData ) );
        }

        public virtual ISerializer GetSerializer( Type entityType )
        {
            return this.GetType().GetGenericTypeDefinition().Make( entityType ).CreateInstance<ISerializer>();
        }

        public abstract ValueTask SerializeAsync( T graph, Stream stream, CancellationToken cancellationToken );

        public abstract ValueTask<T> DeserializeAsync( Stream stream, CancellationToken cancellationToken );

        ValueTask ISerializer.SerializeAsync( object graph, Stream stream, CancellationToken cancellationToken )
        {
            return this.SerializeAsync( ( T )graph, stream, cancellationToken );
        }

        async ValueTask<object> ISerializer.DeserializeAsync( Stream stream, CancellationToken cancellationToken )
        {
            return ( object )await this.DeserializeAsync( stream, cancellationToken );
        }
    }
}
