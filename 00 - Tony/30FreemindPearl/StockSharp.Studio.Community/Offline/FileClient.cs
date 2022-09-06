// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Community.Offline.FileClient
// Assembly: StockSharp.Studio.Community, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E56F9613-66A7-41D4-9472-F9197F913918
// Assembly location: T:\00-StockSharp\Data\StockSharp.Studio.Community.dll

using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class FileClient : BaseOfflineClient<File>, IFileService, IBaseEntityService<File>, IBaseService, IVideoService
    {
        Task IFileService.AddGroupAsync( long fileId, long groupId, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<File> IFileService.AddNugetPackageAsync( byte[ ] package, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task IFileService.ClearTempsAsync( bool uploads, bool downloads, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task IFileService.RemoveTempAsync( string operationId, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<BaseEntitySet<File>> IFileService.FindAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, long? clientId, long? groupId, bool? nestedGroups, bool? includeHistory, long? messageId, long? draftId, long? emailId, bool? emailTemplateOnly, string like, LikeCompares? likeCompare, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<File> IFileService.FinishUploadAsync( string operationId, bool isCancel, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<File> IFileService.GetAvatarAsync( long clientId, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<byte[ ]> IFileService.GetBodyAsync( long fileId, long skip, long? count, Compressions? compression, bool isSnapshot, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<BaseEntitySet<File>> IFileService.GetHistoryAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, long? fileId, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<(string path, long length)> IVideoService.GetInfoAsync( long fileId, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<BaseEntitySet<(File, byte[ ] nuspec, string[ ] frameworks)>> IFileService.GetNugetPackagesAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<(string clientToken, long fileId)[ ]> IVideoService.GetPendingRequestsAsync( CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<File> IFileService.GetTempAsync( string operationId, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<BaseEntitySet<File>> IFileService.GetTempsAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, bool uploads, bool downloads, CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<bool> IFileService.RemoveGroupAsync(
          long fileId,
          long groupId,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task<string> IFileService.StartUploadAsync(
          File file,
          Compressions? compression,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task IVideoService.UpdateInfoAsync(
          string clientToken,
          long fileId,
          string path,
          long length,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }

        Task IFileService.UploadAsync(
          string operationId,
          byte[ ] bodyPart,
          CancellationToken cancellationToken )
        {
            throw new NotSupportedException();
        }
    }
}
