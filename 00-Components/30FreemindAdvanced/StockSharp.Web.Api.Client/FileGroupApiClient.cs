// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.FileGroupApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class FileGroupApiClient :
  BaseApiEntityClient<FileGroup>,
  IFileGroupService,
  IBaseEntityService<FileGroup>
{
    public FileGroupApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public FileGroupApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<FileGroup>> IFileGroupService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? parentId,
      long? childId,
      long? fileId,
      long? clientId,
      long? ownerId,
      bool? distributiveOnly,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<FileGroup>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[16 /*0x10*/]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) parentId,
      (object) childId,
      (object) fileId,
      (object) clientId,
      (object) ownerId,
      (object) distributiveOnly,
      (object) like,
      (object) likeCompare
        });
    }

    Task IFileGroupService.AddChildAsync(
      long parentId,
      long childId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddChildAsync"), cancellationToken, new object[2]
        {
      (object) parentId,
      (object) childId
        });
    }

    Task<bool> IFileGroupService.RemoveChildAsync(
      long parentId,
      long childId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveChildAsync"), cancellationToken, new object[2]
        {
      (object) parentId,
      (object) childId
        });
    }

    Task IFileGroupService.AddFileAsync(
      long groupId,
      long fileId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddFileAsync"), cancellationToken, new object[2]
        {
      (object) groupId,
      (object) fileId
        });
    }

    Task<bool> IFileGroupService.RemoveFileAsync(
      long groupId,
      long fileId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveFileAsync"), cancellationToken, new object[2]
        {
      (object) groupId,
      (object) fileId
        });
    }

    Task IFileGroupService.AddRoleAsync(
      long groupId,
      long roleId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddRoleAsync"), cancellationToken, new object[2]
        {
      (object) groupId,
      (object) roleId
        });
    }

    Task<bool> IFileGroupService.RemoveRoleAsync(
      long groupId,
      long roleId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, new object[2]
        {
      (object) groupId,
      (object) roleId
        });
    }
}
