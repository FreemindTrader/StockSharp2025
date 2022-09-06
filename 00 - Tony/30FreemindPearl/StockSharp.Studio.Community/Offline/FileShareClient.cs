
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class FileShareClient : BaseOfflineClient<FileShare>, IFileShareService, IBaseEntityService<FileShare>, IBaseService
    {
        Task IFileShareService.RemoveByFileIdAsync(
          long fileId,
          CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        Task<BaseEntitySet<FileShare>> IFileShareService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? fileId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<FileShare> IFileShareService.GetByTokenAsync(
          string token,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}
