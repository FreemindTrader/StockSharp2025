// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ICommandService`1
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System.Threading;
using System.Threading.Tasks;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface ICommandService<TStatus>
{
    Task SendAsync(
      long? id,
      string userId,
      CommandInfo command,
      CancellationToken cancellationToken = default(CancellationToken));

    Task UpdateStatusAsync(TStatus status, CancellationToken cancellationToken = default(CancellationToken));
}
