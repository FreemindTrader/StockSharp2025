// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IEmailBulkService
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

public interface IEmailBulkService : IBaseEntityService<EmailBulk>
{
    Task<BaseEntitySet<EmailBulk>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? domainId = null,
      EmailPreferences? emailPreferences = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<int> StartAsync(long bulkId, CancellationToken cancellationToken = default(CancellationToken));

    Task StopAsync(long bulkId, CancellationToken cancellationToken = default(CancellationToken));

    Task FinishAsync(long bulkId, CancellationToken cancellationToken = default(CancellationToken));

    Task<EmailBulk> ShortUrlsAsync(long bulkId, CancellationToken cancellationToken = default(CancellationToken));
}
