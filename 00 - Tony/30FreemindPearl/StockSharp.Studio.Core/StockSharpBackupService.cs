using Ecng.Backup;
using Ecng.Collections;
using StockSharp.Configuration;
using StockSharp.Studio.Community;
using StockSharp.Web.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static StockSharp.Configuration.Paths;

#pragma warning disable CS0618
#pragma warning disable CS1998


namespace StockSharp.Studio.Core
{
    public class StockSharpBackupService : IBackupService, IDisposable
    {
        private readonly SynchronizedDictionary<BackupEntry, long> _fileIds = new SynchronizedDictionary<BackupEntry, long>();

        void IDisposable.Dispose()
        {
        }

        Task<IEnumerable<BackupEntry>> IBackupService.FindAsync(
          BackupEntry parent,
          string criteria,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<IEnumerable<BackupEntry>> IBackupService.GetChildsAsync(
          BackupEntry parent,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task IBackupService.FillInfoAsync(
          BackupEntry entry,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task IBackupService.DeleteAsync(
          BackupEntry entry,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task IBackupService.DownloadAsync(
          BackupEntry entry,
          Stream stream,
          long? start,
          long? length,
          Action<int> progress,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        async Task IBackupService.UploadAsync( BackupEntry entry, Stream stream, Action<int> progress, CancellationToken cancellationToken )
        {
            //long len = stream.Length;
            //int prevProgress = 0;
            //IFileService service = CommunityServicesRegistry.GetService<IFileService>();
            //StockSharp.Web.DomainModel.File file1 = new StockSharp.Web.DomainModel.File();
            //file1.Name = entry.Name;
            //Stream body = stream;
            //Action<long> progress1 = new Action<long>( Progress );
            //CancellationToken cancellationToken1 = cancellationToken;
            //Compressions? compression = new Compressions?();
            //CancellationToken cancellationToken2 = cancellationToken1;
            //StockSharp.Web.DomainModel.File file2 = await service.UploadFullAsync( file1, body, 102400, progress1, compression, cancellationToken2 );
            //this._fileIds.Add( entry, file2.Id );

            //void Progress( long count )
            //{
            //    int num = ( int )( count * 100L / len );
            //    if ( num == prevProgress )
            //        return;
            //    Action<int> action = progress;
            //    if ( action != null )
            //        action( num );
            //    prevProgress = num;
            //}
        }

        async Task<string> IBackupService.PublishAsync( BackupEntry entry, CancellationToken cancellationToken )
        {
            long num;
            if ( !_fileIds.TryGetValue( entry, out num ) )
                return null;
            IFileShareService service = CommunityServicesRegistry.GetService<IFileShareService>();
            Web.DomainModel.FileShare entity = new Web.DomainModel.FileShare();
            Web.DomainModel.File file = new Web.DomainModel.File();
            file.Id = num;
            entity.File = file;
            CancellationToken cancellationToken1 = cancellationToken;
            return GetPageUrl( Pages.File, await service.AddAsync( entity, cancellationToken1 ) );
        }

        async Task IBackupService.UnPublishAsync(
          BackupEntry entry,
          CancellationToken cancellationToken )
        {
            long fileId;
            if ( !_fileIds.TryGetValue( entry, out fileId ) )
                return;
            await CommunityServicesRegistry.GetService<IFileShareService>().RemoveByFileIdAsync( fileId, cancellationToken );
        }

        bool IBackupService.CanPublish
        {
            get
            {
                return true;
            }
        }

        bool IBackupService.CanFolders
        {
            get
            {
                return false;
            }
        }
    }
}
