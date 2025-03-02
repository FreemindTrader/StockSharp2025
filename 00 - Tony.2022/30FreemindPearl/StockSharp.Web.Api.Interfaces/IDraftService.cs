// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IDraftService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IDraftService : IBaseEntityService<Draft>, IBaseService
    {
        Task<BaseEntitySet<Draft>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? clientId = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<Draft> TryGetByPageIdAsync(string pageId, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveByPageAsync(string pageId, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveFileAsync(long fileId, CancellationToken cancellationToken);
    }
}
