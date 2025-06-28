using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public interface IStudioCommandService : IDisposable
{
    IStudioCommandScope GlobalScope { get; }

    void Process(object sender, IStudioCommand command, object[] targets, bool isSyncProcess);

    bool CanProcess(object sender, IStudioCommand command);

    void Register<TCommand>(
      object listener,
      bool guiAsync,
      Action<object, TCommand> handler,
      Func<TCommand, bool> canExecute = null)
      where TCommand : IStudioCommand;

    void UnRegister(Type commandType, object listener);

    void Bind(object sender, IStudioCommandScope scope);

    void UnBind(object sender);
}
