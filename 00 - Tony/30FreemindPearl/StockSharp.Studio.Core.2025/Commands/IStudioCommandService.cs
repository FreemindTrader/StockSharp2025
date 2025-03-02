// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.IStudioCommandService
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public interface IStudioCommandService : IDisposable
    {
        IStudioCommandScope GlobalScope { get; }

        void Process( object sender, IStudioCommand command, object [ ] targets, bool isSyncProcess );

        bool CanProcess( object sender, IStudioCommand command );

        void Register<TCommand>(
          object listener,
          bool guiAsync,
          Action<object, TCommand> handler,
          Func<TCommand, bool> canExecute = null )
          where TCommand : IStudioCommand;

        void UnRegister( Type commandType, object listener );

        void Bind( object sender, IStudioCommandScope scope );

        void UnBind( object sender );
    }
}
