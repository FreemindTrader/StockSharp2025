// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.BaseApiEntityClient`1
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public abstract class BaseApiEntityClient<TEntity> : BaseApiClient, IBaseEntityService<TEntity>
    where TEntity : BaseEntity
    {
        protected BaseApiEntityClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        protected BaseApiEntityClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        public virtual Task<TEntity> AddAsync( TEntity entity, CancellationToken cancellationToken )
        {
            return ( Task<TEntity> ) this.PostAsync<TEntity>( RestBaseApiClient.GetCurrentMethod( nameof( AddAsync ) ), cancellationToken, new object [1]
            {
         entity
            } );
        }

        public virtual Task<bool> DeleteAsync( long id, CancellationToken cancellationToken )
        {
            return ( Task<bool> ) this.DeleteAsync<bool>( RestBaseApiClient.GetCurrentMethod( nameof( DeleteAsync ) ), cancellationToken, new object [1]
            {
         id
            } );
        }

        public virtual Task<TEntity> GetAsync( long id, CancellationToken cancellationToken )
        {
            return ( Task<TEntity> ) this.GetAsync<TEntity>( RestBaseApiClient.GetCurrentMethod( nameof( GetAsync ) ), cancellationToken, new object [1]
            {
         id
            } );
        }

        public virtual Task<TEntity [ ]> GetMultiAsync(
          long [ ] ids,
          CancellationToken cancellationToken )
        {
            return ( Task<TEntity [ ]> ) this.GetAsync<TEntity [ ]>( RestBaseApiClient.GetCurrentMethod( nameof( GetMultiAsync ) ), cancellationToken, new object [1]
            {
         ids
            } );
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<TEntity>>( RestBaseApiClient.GetCurrentMethod( nameof( GetRangeAsync ) ), cancellationToken, new object [8]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd
            } );
        }

        public virtual Task<TEntity> UpdateAsync(
          TEntity entity,
          CancellationToken cancellationToken )
        {
            return ( Task<TEntity> ) this.PutAsync<TEntity>( RestBaseApiClient.GetCurrentMethod( nameof( UpdateAsync ) ), cancellationToken, new object [1]
            {
         entity
            } );
        }

        public virtual Task RestoreAsync( long id, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            return ( Task ) this.PostAsync<TEntity>( RestBaseApiClient.GetCurrentMethod( nameof( RestoreAsync ) ), cancellationToken, new object [1]
            {
         id
            } );
        }
    }
}
