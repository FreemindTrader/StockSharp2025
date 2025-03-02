﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IEmailService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IEmailService : IBaseEntityService<Email>
    {
        Task<BaseEntitySet<Email>> FindAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          int? maxErrorCount = null,
          CancellationToken cancellationToken = default( CancellationToken ) );


        Task<(File mjml, File html)> SaveMjmlAsync( string mjmlName, long? mjmlId, string mjmlText, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<File> TryGetMjmlAsync( long htmlId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<File> GetHtmlAsync( long mjmlId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<string> RenderMjmlAsync( string mjml, CancellationToken cancellationToken = default( CancellationToken ) );

        Task EmailAsSpamAsync( string email, CancellationToken cancellationToken = default( CancellationToken ) );

        Task EmailBouncedAsync( string email, CancellationToken cancellationToken = default( CancellationToken ) );

        Task EmailDeliveredAsync( string email, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
