// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IPollService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
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
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<PollChoice> AddChoiceAsync(
          PollChoice choice,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool> RemoveChoiceAsync( long choiceId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<PollVote> AddVoteAsync( long choiceId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool> RemoveVoteAsync( long voteId, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
