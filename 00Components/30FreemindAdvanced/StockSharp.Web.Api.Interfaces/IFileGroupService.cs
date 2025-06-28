// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IFileGroupService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface IFileGroupService : IBaseEntityService<FileGroup>
{
    Task<BaseEntitySet<FileGroup>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? parentId = null,
      long? childId = null,
      long? fileId = null,
      long? clientId = null,
      long? ownerId = null,
      bool? distributiveOnly = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task AddChildAsync(long parentId, long childId, CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> RemoveChildAsync(long parentId, long childId, CancellationToken cancellationToken = default(CancellationToken));

    Task AddFileAsync(long groupId, long fileId, CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> RemoveFileAsync(long groupId, long fileId, CancellationToken cancellationToken = default(CancellationToken));

    Task AddRoleAsync(long groupId, long roleId, CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> RemoveRoleAsync(long groupId, long roleId, CancellationToken cancellationToken = default(CancellationToken));
}
