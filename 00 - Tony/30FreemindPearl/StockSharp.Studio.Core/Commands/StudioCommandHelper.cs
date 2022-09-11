// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.StudioCommandHelper
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Configuration;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public static class StudioCommandHelper
    {
        public static IStudioCommandService Service
        {
            get
            {
                return ConfigManager.GetService<IStudioCommandService>();
            }
        }

        public static void Process( this IStudioCommand command, object sender, object[ ] targets )
        {
            Service.Process( sender, command, targets, false );
        }

        public static void Process( this IStudioCommand command, object sender, bool isSyncProcess = false )
        {
            Service.Process( sender, command, null, isSyncProcess );
        }

        public static void SyncProcess( this IStudioCommand command, object sender )
        {
            command.Process( sender, true );
        }

        public static void RouteToGlobal( this IStudioCommand command, bool isSyncProcess = false )
        {
            command.Process( Service.GlobalScope, false );
        }

        public static bool CanProcess( this IStudioCommand command, object sender )
        {
            return Service.CanProcess( sender, command );
        }

        public static TCommand Top<TCommand>( this TCommand command ) where TCommand : BaseStudioCommand
        {
            return command.Direction( StudioCommandDirections.Top );
        }

        public static TCommand Deep<TCommand>( this TCommand command ) where TCommand : BaseStudioCommand
        {
            return command.Direction( StudioCommandDirections.Deep );
        }

        public static TCommand Direction<TCommand>(
          this TCommand command,
          StudioCommandDirections possibleDirection )
          where TCommand : BaseStudioCommand
        {
            if ( command == null )
                throw new ArgumentNullException( nameof( command ) );
            command.PossibleDirection = possibleDirection;
            return command;
        }

        public static void Register<TCommand>(
          this IStudioCommandService service,
          object listener,
          bool guiAsync,
          Action<TCommand> handler,
          Func<TCommand, bool> canExecute = null )
          where TCommand : IStudioCommand
        {
            if ( handler == null )
                throw new ArgumentNullException( nameof( handler ) );
            service.Register( listener, guiAsync, ( sender, cmd ) => handler( cmd ), canExecute );
        }
    }
}
