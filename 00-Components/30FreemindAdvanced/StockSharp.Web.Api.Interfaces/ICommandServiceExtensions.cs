// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ICommandServiceExtensions
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public static class ICommandServiceExtensions
{
    public static Task StartAsync<TStatus>(
      this ICommandService<TStatus> service,
      CommandScopes scope,
      long? id,
      string userId,
      CancellationToken cancellationToken)
    {
        return TypeHelper.CheckOnNull<ICommandService<TStatus>>(service, nameof(service)).SendAsync(id, userId, ICommandServiceExtensions.CreateCommand(scope, (CommandTypes)0), cancellationToken);
    }

    public static Task StopAsync<TStatus>(
      this ICommandService<TStatus> service,
      CommandScopes scope,
      long? id,
      string userId,
      CancellationToken cancellationToken)
    {
        return TypeHelper.CheckOnNull<ICommandService<TStatus>>(service, nameof(service)).SendAsync(id, userId, ICommandServiceExtensions.CreateCommand(scope, (CommandTypes)1), cancellationToken);
    }

    private static CommandInfo CreateCommand(CommandScopes scope, CommandTypes command)
    {
        return new CommandInfo()
        {
            Command = command,
            Scope = scope
        };
    }
}
