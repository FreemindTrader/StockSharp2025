// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.FileShareApiClient
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

internal class FileShareApiClient :
  BaseApiEntityClient<FileShare>,
  IFileShareService,
  IBaseEntityService<FileShare>
{
    public FileShareApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public FileShareApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<FileShare>> IFileShareService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? clientId,
      long? fileId,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<FileShare>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[12]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) clientId,
      (object) fileId,
      (object) like,
      (object) likeCompare
        });
    }

    Task<FileShare> IFileShareService.GetByTokenAsync(
      string token,
      bool includeBody,
      CancellationToken cancellationToken)
    {
        return this.Get<FileShare>(RestBaseApiClient.GetCurrentMethod("GetByTokenAsync"), cancellationToken, new object[2]
        {
      (object) token,
      (object) includeBody
        });
    }

    Task IFileShareService.RemoveByFileIdAsync(long fileId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("RemoveByFileIdAsync"), cancellationToken, new object[1]
        {
      (object) fileId
        });
    }
}
