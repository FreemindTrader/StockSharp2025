// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ICommandServiceExtensions
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public static class ICommandServiceExtensions
    {
        public static Task StartAsync<TStatus>(
          this ICommandService<TStatus> service,
          CommandScopes scope,
          long? id,
          string userId,
          CancellationToken cancellationToken )
        {
            return ( ( ICommandService<TStatus> ) TypeHelper.CheckOnNull<ICommandService<TStatus>>( service, nameof( service ) ) ).SendAsync( id, userId, ICommandServiceExtensions.CreateCommand( scope, CommandTypes.Start ), cancellationToken );
        }

        public static Task StopAsync<TStatus>(
          this ICommandService<TStatus> service,
          CommandScopes scope,
          long? id,
          string userId,
          CancellationToken cancellationToken )
        {
            return ( ( ICommandService<TStatus> ) TypeHelper.CheckOnNull<ICommandService<TStatus>>( service, nameof( service ) ) ).SendAsync( id, userId, ICommandServiceExtensions.CreateCommand( scope, CommandTypes.Stop ), cancellationToken );
        }

        private static CommandInfo CreateCommand( CommandScopes scope, CommandTypes command )
        {
            return new CommandInfo()
            {
                Command = command,
                Scope = scope
            };
        }
    }
}
