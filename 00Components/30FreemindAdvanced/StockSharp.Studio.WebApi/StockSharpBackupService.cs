// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.StockSharpBackupService
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B97A7121-FFB7-49F4-8E30-FC5C471868BC
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Backup;
using Ecng.Collections;
using Ecng.Common;
using StockSharp.Configuration;
using StockSharp.Web.Api.Interfaces;

#nullable enable
namespace StockSharp.Studio.WebApi;

/// <summary>
/// StockSharp implementation <see cref="T:Ecng.Backup.IBackupService" />.
/// </summary>
public class StockSharpBackupService : Disposable, IBackupService, IDisposable
{
    private readonly
#nullable disable
    SynchronizedDictionary<BackupEntry, long> _fileIds = new SynchronizedDictionary<BackupEntry, long>();

    IAsyncEnumerable<BackupEntry> IBackupService.FindAsync(
      BackupEntry parent,
      string criteria,
      CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }

    Task IBackupService.FillInfoAsync(BackupEntry entry, CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }

    Task IBackupService.DeleteAsync(BackupEntry entry, CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }

    Task IBackupService.DownloadAsync(
      BackupEntry entry,
      Stream stream,
      long? start,
      long? length,
      Action<int> progress,
      CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }

    async Task IBackupService.UploadAsync(
      BackupEntry entry,
      Stream stream,
      Action<int> progress,
      CancellationToken cancellationToken)
    {
        long len = stream.Length;
        int prevProgress = 0;
        IFileService service = WebApiServicesRegistry.GetService<IFileService>();
        StockSharp.Web.DomainModel.File file1 = new StockSharp.Web.DomainModel.File();
        file1.Name = entry.Name;
        Stream body = stream;
        Action<long> progress1 = new Action<long>(Progress);
        CancellationToken cancellationToken1 = cancellationToken;
        Compressions? compression = new Compressions?();
        CancellationToken cancellationToken2 = cancellationToken1;
        StockSharp.Web.DomainModel.File file2 = await service.UploadFullAsync(file1, body, progress: progress1, compression: compression, cancellationToken: cancellationToken2);
        this._fileIds.Add(entry, file2.Id);

        void Progress(long count)
        {
            int num = (int)(count * 100L / len);
            if (num == prevProgress)
                return;
            Action<int> action = progress;
            if (action != null)
                action(num);
            prevProgress = num;
        }
    }

    async Task<string> IBackupService.PublishAsync(
      BackupEntry entry,
      CancellationToken cancellationToken)
    {
        long num;
        if (!this._fileIds.TryGetValue(entry, out num))
            return (string)null;
        IFileShareService service = WebApiServicesRegistry.GetService<IFileShareService>();
        StockSharp.Web.DomainModel.FileShare entity = new StockSharp.Web.DomainModel.FileShare();
        StockSharp.Web.DomainModel.File file = new StockSharp.Web.DomainModel.File();
        file.Id = num;
        entity.File = file;
        CancellationToken cancellationToken1 = cancellationToken;
        return Paths.GetPageUrl(276L, (object)(await service.AddAsync(entity, cancellationToken1)).Token);
    }

    Task IBackupService.UnPublishAsync(BackupEntry entry, CancellationToken cancellationToken)
    {
        long fileId;
        return !this._fileIds.TryGetValue(entry, out fileId) ? Task.CompletedTask : WebApiServicesRegistry.GetService<IFileShareService>().RemoveByFileIdAsync(fileId, cancellationToken);
    }

    Task IBackupService.CreateFolder(BackupEntry entry, CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }

    bool IBackupService.CanPublish => true;

    bool IBackupService.CanFolders => false;

    bool IBackupService.CanPartialDownload => true;
}
