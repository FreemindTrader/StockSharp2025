// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.IStudioCommandService
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public interface IStudioCommandService : IDisposable
    {
        IStudioCommandScope GlobalScope { get; }

        void Process( object sender, IStudioCommand command, object[ ] targets, bool isSyncProcess );

        bool CanProcess( object sender, IStudioCommand command );

        void Register<TCommand>(
          object listener,
          bool guiAsync,
          Action<object, TCommand> handler,
          Func<TCommand, bool> canExecute = null )
          where TCommand : IStudioCommand;

        void UnRegister<TCommand>( object listener ) where TCommand : IStudioCommand;

        void Bind( object sender, IStudioCommandScope scope );

        void UnBind( object sender );
    }
}
