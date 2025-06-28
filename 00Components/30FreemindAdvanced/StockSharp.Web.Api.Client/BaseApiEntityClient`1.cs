// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.BaseApiEntityClient`1
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

public abstract class BaseApiEntityClient<TEntity> : BaseApiClient, IBaseEntityService<TEntity> where TEntity : BaseEntity
{
    protected BaseApiEntityClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    protected BaseApiEntityClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return this.PostAsync<TEntity>(RestBaseApiClient.GetCurrentMethod(nameof(AddAsync)), cancellationToken, new object[1]
        {
      (object) entity
        });
    }

    public virtual Task<bool> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        return this.DeleteAsync<bool>(RestBaseApiClient.GetCurrentMethod(nameof(DeleteAsync)), cancellationToken, new object[1]
        {
      (object) id
        });
    }

    public virtual Task<TEntity> GetAsync(long id, CancellationToken cancellationToken)
    {
        return this.GetAsync<TEntity>(RestBaseApiClient.GetCurrentMethod(nameof(GetAsync)), cancellationToken, new object[1]
        {
      (object) id
        });
    }

    public virtual Task<TEntity[]> GetMultiAsync(long[] ids, CancellationToken cancellationToken)
    {
        return this.GetAsync<TEntity[]>(RestBaseApiClient.GetCurrentMethod(nameof(GetMultiAsync)), cancellationToken, new object[1]
        {
      (object) ids
        });
    }

    public virtual Task<BaseEntitySet<TEntity>> GetRangeAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<TEntity>>(RestBaseApiClient.GetCurrentMethod(nameof(GetRangeAsync)), cancellationToken, new object[8]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd
        });
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return this.PutAsync<TEntity>(RestBaseApiClient.GetCurrentMethod(nameof(UpdateAsync)), cancellationToken, new object[1]
        {
      (object) entity
        });
    }

    public virtual Task RestoreAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
    {
        return (Task)this.PostAsync<TEntity>(RestBaseApiClient.GetCurrentMethod(nameof(RestoreAsync)), cancellationToken, new object[1]
        {
      (object) id
        });
    }
}
