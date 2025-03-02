// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.StudioCommandHelper
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

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

        public static void Process( this IStudioCommand command, object sender, object [ ] targets )
        {
            StudioCommandHelper.Service.Process( sender, command, targets, false );
        }

        public static void Process( this IStudioCommand command, object sender, bool isSyncProcess = false )
        {
            StudioCommandHelper.Service.Process( sender, command, ( object [ ] ) null, isSyncProcess );
        }

        public static void SyncProcess( this IStudioCommand command, object sender )
        {
            command.Process( sender, true );
        }

        public static void RouteToGlobal( this IStudioCommand command, bool isSyncProcess = false )
        {
            command.Process( ( object ) StudioCommandHelper.Service.GlobalScope, isSyncProcess );
        }

        public static TCommand Top<TCommand>( this TCommand command ) where TCommand : BaseStudioCommand
        {
            return command.Direction<TCommand>( StudioCommandDirections.Top );
        }

        public static TCommand Deep<TCommand>( this TCommand command ) where TCommand : BaseStudioCommand
        {
            return command.Direction<TCommand>( StudioCommandDirections.Deep );
        }

        public static TCommand Direction<TCommand>(
          this TCommand command,
          StudioCommandDirections possibleDirection )
          where TCommand : BaseStudioCommand
        {
            if ( ( object ) command == null )
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
            service.Register<TCommand>( listener, guiAsync, ( Action<object, TCommand> ) ( ( sender, cmd ) => handler( cmd ) ), canExecute );
        }

        public static void UnRegister<TCommand>( this IStudioCommandService service, object listener ) where TCommand : IStudioCommand
        {
            service.UnRegister( typeof( TCommand ), listener );
        }
    }
}
