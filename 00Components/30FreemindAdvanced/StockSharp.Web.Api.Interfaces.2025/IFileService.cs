// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IFileService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IFileService : IBaseEntityService<File>, IVideoService
    {
        Task<string> StartUploadAsync(
          File file,
          Compressions? compression = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task UploadAsync( string operationId, byte [ ] bodyPart, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<File> FinishUploadAsync(
          string operationId,
          bool isCancel,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task ClearTempsAsync( bool? uploads = null, bool? downloads = null, CancellationToken cancellationToken = default( CancellationToken ) );

        Task RemoveTempAsync( string operationId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<File> GetTempAsync( string operationId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<string> StartDownloadAsync(
          long? fileId,
          long? bodyId,
          bool? isSnapshot = null,
          Compressions? compression = null,
          int? width = null,
          int? height = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<byte [ ]> GetPartAsync(
          string operationId,
          long skip = 0,
          long? count = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        [Obsolete]
        Task<byte [ ]> GetBodyAsync(
          long fileId,
          long skip = 0,
          long? count = null,
          Compressions? compression = null,
          int? width = null,
          int? height = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        [Obsolete]
        Task<byte [ ]> GetVersionBodyAsync(
          long bodyId,
          bool? isSnapshot = null,
          long skip = 0,
          long? count = null,
          Compressions? compression = null,
          int? width = null,
          int? height = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<FileBody> GetFileBodyAsync( long bodyId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<File> GetFileByBodyAsync( long bodyId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<BaseEntitySet<File>> FindAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          long? clientId = null,
          long? groupId = null,
          long? messageId = null,
          long? draftId = null,
          long? emailId = null,
          bool? tempUploads = null,
          bool? tempDownloads = null,
          bool? cloud = null,
          string like = null,
          ComparisonOperator? likeCompare = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<BaseEntitySet<FileBody>> FindVersionsAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          long? fileId = null,
          long? groupId = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<BaseEntitySet<NugetSpecification>> GetNugetPackagesAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<FileBody> AddNugetPackageAsync(
          byte [ ] package,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<File> GetAvatarAsync( long clientId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task AddGroupAsync( long fileId, long groupId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool> RemoveGroupAsync( long fileId, long groupId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<int> ClearLogsAsync(
          int daysFrom,
          int daysTo,
          int maxCount,
          CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
