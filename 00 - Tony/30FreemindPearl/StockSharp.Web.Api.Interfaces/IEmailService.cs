// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IEmailService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IEmailService : IBaseEntityService<Email>, IBaseService
    {
        Task<BaseEntitySet<Email>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          Guid? transactionId = null,
          int? maxErrorCount = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task StartMassRequestAsync(
          Guid transactionId,
          long domainId,
          long[] included,
          long[] excluded,
          string fromAddress,
          string fromAlias,
          string subject,
          string bodyHtml,
          string bodyPlain,
          CancellationToken cancellationToken = default(CancellationToken));

        Task StopMassRequestAsync(Guid transactionId, CancellationToken cancellationToken = default(CancellationToken));

        Task<BaseEntitySet<(Guid transactionId, bool isCancelled, long domainId, Client[] clients, string fromAddress, string fromAlias, string subject, (string bodyHtml, string bodyPlain))>>
        GetMassRequests(
          CancellationToken cancellationToken = default(CancellationToken));

        //[return: TupleElementNames(new string[] { "transactionId", "isCancelled", "domainId", "clients", "fromAddress", "fromAlias", "subject", "bodyHtml", "bodyPlain", null, null })]
        //Task<BaseEntitySet<ValueTuple<Guid, bool, long, Client[], string, string, string, ValueTuple<string, string>>>> GetMassRequests(
        //  CancellationToken cancellationToken = default(CancellationToken));
    }
}
