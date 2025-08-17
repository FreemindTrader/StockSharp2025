// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IFileServiceExtensions
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.IO;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public static class IFileServiceExtensions
{
    public const int DefaultBufferSize = 102400 /*0x019000*/;

    public static async Task<StockSharp.Web.DomainModel.File> UploadFullAsync(this IFileService service, string path, int bufferSize = 102400, Action<long> progress = null, Compressions? compression = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        string fileName = Path.GetFileName(path);
        StockSharp.Web.DomainModel.File result;

        using (FileStream body = System.IO.File.OpenRead(path))
        {
            
            var file = new StockSharp.Web.DomainModel.File();
            file.Name = fileName;

            result = await service.UploadFullAsync(file, body, bufferSize, progress, compression, cancellationToken);
        }
        return result;
    }

    public static async Task<DomainModel.File> UploadFullAsync(this IFileService service, DomainModel.File file, Stream body, int bufferSize = 102400, Action<long> progress = null, Compressions? compression = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        string operationId;
        DomainModel.File result = null;

        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        if (body == null)
        {
            throw new ArgumentNullException(nameof(body));
        }

        if (compression.HasValue)
        {
            MemoryStream output = new MemoryStream();
            switch (compression.Value)
            {
                case Compressions.GZip:
                    await body.CompressAsync<GZipStream>(output, CompressionLevel.Optimal, true, 81920, cancellationToken);
                    break;

                case Compressions.Deflate:
                    await body.CompressAsync<DeflateStream>(output, CompressionLevel.Optimal, true, 81920, cancellationToken);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(compression), compression.Value, "Invalid compression.");
            }
            output.Position = 0L;
            body = output;
            output = null;
        }

        operationId = await service.StartUploadAsync(file, compression, cancellationToken);

        long sent = 0;
        byte[] buffer = new byte[bufferSize];
        while (true)
        {
            Action<long> action = null;
            do
            {
                int read = await body.ReadAsync(buffer, 0, bufferSize);

                if (read != 0)
                {
                    await service.UploadAsync(operationId, buffer.Take(read).ToArray(), cancellationToken);

                    sent += read;
                    action = progress;
                }
                else
                {
                    result = await service.FinishUploadAsync(operationId, false, cancellationToken);
                }
            }
            while (action == null);

            action(sent);

            return result;
        }
    }

    public static async Task<byte[]> DownloadFullBytesAsync(this IFileService service, long id, bool version = false, int bufferSize = 102400 /*0x019000*/, long maxSize = 9223372036854775807 /*0x7FFFFFFFFFFFFFFF*/, Action<long> progress = null, Compressions? compression = null, int? width = null, int? height = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        return Converter.To<byte[]>((object)await service.DownloadFullAsync(id, version, bufferSize, maxSize, progress, compression, width, height, cancellationToken));
    }

    public static async Task<Stream> DownloadFullAsync(this IFileService service, long id, bool version = false, int bufferSize = 102400 /*0x019000*/, long maxSize = 9223372036854775807 /*0x7FFFFFFFFFFFFFFF*/, Action<long> progress = null, Compressions? compression = null, int? width = null, int? height = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        MemoryStream body = new MemoryStream();
        await service.DownloadFullAsync(id, (Stream)body, version, bufferSize, maxSize, progress, compression, width, height, cancellationToken);
        body.Position = 0L;
        Stream stream = (Stream)body;
        body = (MemoryStream)null;
        return stream;
    }

    public static async Task DownloadFullAsync(this IFileService service, long id, Stream body, bool version = false, int bufferSize = 102400 /*0x019000*/, long maxSize = 9223372036854775807 /*0x7FFFFFFFFFFFFFFF*/, Action<long> progress = null, Compressions? compression = null, int? width = null, int? height = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));

        if (id == 0L)
            throw new ArgumentNullException(nameof(id));

        if (body == null)
            throw new ArgumentNullException(nameof(body));

        string operationId = await service.StartDownloadAsync(version ? new long?() : new long?(id), version ? new long?(id) : new long?(), new bool?(true), compression, width, height, cancellationToken);
        long received = 0;

        while (true)
        {
            byte[] part = await service.GetPartAsync(operationId, received, new long?((long)bufferSize), cancellationToken);
            if (part.Length != 0)
            {
                await body.WriteAsync(part, 0, part.Length);
                received += (long)part.Length;

                Action<long> action = progress;
                if (action != null)
                    action(received);

                if (part.Length >= bufferSize)
                {
                    if (received < maxSize)
                        part = (byte[])null;
                    else
                        break;
                }
                else
                    goto label_15;
            }
            else
                goto label_15;
        }
        throw new InvalidOperationException($"received={received} >= max={maxSize}");

    label_15:
        if (!compression.HasValue)
        {
            operationId = null;
        }
        else
        {
            body.Position = 0L;
            MemoryStream output = new MemoryStream();
            switch (compression.Value)
            {
                case Compressions.GZip:
                    await CompressionHelper.UncompressAsync<GZipStream>(body, (Stream)output, true, 81920 /*0x014000*/, cancellationToken);
                    break;
                case Compressions.Deflate:
                    await CompressionHelper.UncompressAsync<DeflateStream>(body, (Stream)output, true, 81920 /*0x014000*/, cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compression), (object)compression.Value, "Invalid compression.");
            }
            body.Position = 0L;
            body.SetLength(0L);
            await output.CopyToAsync(body, bufferSize, cancellationToken);
            output = (MemoryStream)null;
            operationId = (string)null;
        }
    }
}
