// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IPollService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface IPollService : IBaseEntityService<Poll>
{
    Task<BaseEntitySet<Poll>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? clientId = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<PollChoice> AddChoiceAsync(PollChoice choice, CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> RemoveChoiceAsync(long choiceId, CancellationToken cancellationToken = default(CancellationToken));

    Task<PollVote> AddVoteAsync(long choiceId, CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> RemoveVoteAsync(long voteId, CancellationToken cancellationToken = default(CancellationToken));
}
