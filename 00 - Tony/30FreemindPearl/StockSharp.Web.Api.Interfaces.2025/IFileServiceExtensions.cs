using Ecng.IO;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public static class IFileServiceExtensions
    {
        public const int DefaultBufferSize = 102400;

        public static async Task<DomainModel.File> UploadFullAsync( this IFileService service, string path, int bufferSize = 102400, Action<long> progress = null, Compressions? compression = null, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            FileStream body;
            string fileName = Path.GetFileName( path );
            body = File.OpenRead( path );
            var file = new DomainModel.File();
            file.Name = fileName;


            var result = await service.UploadFullAsync( file, body, bufferSize, progress, compression, cancellationToken );

            return result;
        }

        public static async Task<DomainModel.File> UploadFullAsync( this IFileService service, DomainModel.File file, Stream body, int bufferSize = 102400, Action<long> progress = null, Compressions? compression = null, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            string operationId;
            DomainModel.File result = null;

            if ( service == null )
            {
                throw new ArgumentNullException( nameof( service ) );
            }

            if ( file == null )
            {
                throw new ArgumentNullException( nameof( file ) );
            }

            if ( body == null )
            {
                throw new ArgumentNullException( nameof( body ) );
            }

            if ( compression.HasValue )
            {
                MemoryStream output = new MemoryStream();
                switch ( compression.Value )
                {
                    case Compressions.GZip:
                    await body.CompressAsync<GZipStream>( output, CompressionLevel.Optimal, true, 81920, cancellationToken );
                    break;

                    case Compressions.Deflate:
                    await body.CompressAsync<DeflateStream>( output, CompressionLevel.Optimal, true, 81920, cancellationToken );
                    break;

                    default:
                    throw new ArgumentOutOfRangeException( nameof( compression ), compression.Value, "Invalid compression." );
                }
                output.Position = 0L;
                body = output;
                output = null;
            }

            operationId = await service.StartUploadAsync( file, compression, cancellationToken );

            long sent = 0;
            byte[ ] buffer = new byte[bufferSize];
            while ( true )
            {
                Action<long> action = null;
                do
                {
                    int read = await body.ReadAsync( buffer, 0, bufferSize );

                    if ( read != 0 )
                    {
                        await service.UploadAsync( operationId, buffer.Take( read ).ToArray(), cancellationToken );

                        sent += read;
                        action = progress;
                    }
                    else
                    {
                        result = await service.FinishUploadAsync( operationId, false, cancellationToken );
                    }
                }
                while ( action == null );

                action( sent );

                return result;
            }
        }

        public static async Task DownloadFullAsync( this IFileService service, long fileId, Stream body, int bufferSize = 102400, Action<long> progress = null, Compressions? compression = null, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            if ( service == null )
            {
                throw new ArgumentNullException( nameof( service ) );
            }

            if ( fileId == 0L )
            {
                throw new ArgumentNullException( nameof( fileId ) );
            }

            if ( body == null )
            {
                throw new ArgumentNullException( nameof( body ) );
            }

            long received = 0;

            while ( true )
            {
                byte[ ] part = await service.GetBodyAsync( fileId, received, new long?( bufferSize ), compression, null, null, cancellationToken );

                if ( part.Length != 0 )
                {
                    await body.WriteAsync( part, 0, part.Length );
                    received += part.Length;
                    Action<long> action = progress;
                    if ( action != null )
                        action( received );
                    if ( part.Length >= bufferSize )
                        part = null;
                    else
                        return;
                }
                else
                    break;
            }

            return;
        }
    }
}
