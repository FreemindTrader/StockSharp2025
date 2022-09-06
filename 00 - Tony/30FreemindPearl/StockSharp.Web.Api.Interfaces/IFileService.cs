
using StockSharp.Web.DomainModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IFileService : IBaseEntityService<File>, IBaseService, IVideoService
    {
        Task<string> StartUploadAsync(
          File file,
          Compressions? compression = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task UploadAsync(string operationId, byte[] bodyPart, CancellationToken cancellationToken = default(CancellationToken));

        Task<File> FinishUploadAsync( string operationId, bool isCancel, CancellationToken cancellationToken = default(CancellationToken));

        Task<BaseEntitySet<File>> GetTempsAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool uploads = true,
          bool downloads = true,
          CancellationToken cancellationToken = default(CancellationToken));

        Task ClearTempsAsync(bool uploads, bool downloads, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveTempAsync(string operationId, CancellationToken cancellationToken = default(CancellationToken));

        Task<File> GetTempAsync(string operationId, CancellationToken cancellationToken = default(CancellationToken));

        Task<byte[]> GetBodyAsync(
          long fileId,
          long skip = 0,
          long? count = null,
          Compressions? compression = null,
          bool isSnapshot = true,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<BaseEntitySet<File>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? clientId = null,
          long? groupId = null,
          bool? nestedGroups = null,
          bool? includeHistory = null,
          long? messageId = null,
          long? draftId = null,
          long? emailId = null,
          bool? emailTemplateOnly = null,
          string like = null,
          LikeCompares? likeCompare = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<BaseEntitySet<File>> GetHistoryAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? fileId = null,
          CancellationToken cancellationToken = default(CancellationToken));
        
        Task<BaseEntitySet< (File, byte[] nuspec, string[] frameworks)>> GetNugetPackagesAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<File> AddNugetPackageAsync(byte[] package, CancellationToken cancellationToken = default(CancellationToken));

        Task<File> GetAvatarAsync(long clientId, CancellationToken cancellationToken = default(CancellationToken));

        Task AddGroupAsync(long fileId, long groupId, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveGroupAsync(long fileId, long groupId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
