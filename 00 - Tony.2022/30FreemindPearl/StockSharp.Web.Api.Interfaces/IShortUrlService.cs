﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IShortUrlService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IShortUrlService : IBaseEntityService<ShortUrl>, IBaseService
    {
        Task<BaseEntitySet<ShortUrl>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? clientId = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<ShortUrl> GetByShortAsync(string url, CancellationToken cancellationToken = default(CancellationToken));

        Task<ShortUrl> GetByOriginAsync(string origin, CancellationToken cancellationToken = default(CancellationToken));
    }
}
