// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Commands.StudioCommandService
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Core.Commands;

#nullable enable
namespace StockSharp.Studio.Controls.Commands;

public class StudioCommandService : Disposable, IStudioCommandService, IDisposable
{
    private readonly
#nullable disable
    IStudioCommandScope _globalScope = (IStudioCommandScope)new StudioCommandService.Scope("global");
    private readonly IStudioCommandScope _undefinedScope = (IStudioCommandScope)new StudioCommandService.Scope("undefined");
    private readonly SynchronizedDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>> _handlers = new SynchronizedDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>();
    private readonly SynchronizedDictionary<(Type cmdType, object listener), Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> _listeners = new SynchronizedDictionary<(Type, object), Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>();
    private readonly SynchronizedDictionary<object, IStudioCommandScope> _binds = new SynchronizedDictionary<object, IStudioCommandScope>();
    private readonly SynchronizedDictionary<object, IStudioCommandScope> _scopes = new SynchronizedDictionary<object, IStudioCommandScope>();
    private readonly BlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>> _queue = new BlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>>();
    private readonly SynchronizedDictionary<object, List<IStudioCommand>> _undefinedCommands = new SynchronizedDictionary<object, List<IStudioCommand>>();

    public StudioCommandService()
    {
        ThreadingHelper.Launch(ThreadingHelper.Name(ThreadingHelper.Background(ThreadingHelper.ThreadInvariant(new Action(this.Process)), true), "Studio command service thread"));
        this.Register<StudioCommandService.FlushCommand>((object)this, false, (Action<StudioCommandService.FlushCommand>)(cmd => cmd.Pulse()));
    }

    private IStudioCommandScope GetScopeBySender(object sender)
    {
        if (!(sender is IStudioCommandScope studioCommandScope))
            return this.GetScope(sender);
        return !studioCommandScope.UseParentScope ? this._globalScope : this.InternalGetScope(sender);
    }

    private Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] EnsureGetScopeHandlers(
      IStudioCommandScope scope,
      IStudioCommand command)
    {
        Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = this.GetScopeHandlers(scope, command);
        if (scopeHandlers == null && scope.RouteToGlobalScope)
            scopeHandlers = this.GetScopeHandlers(this._globalScope, command);
        return scopeHandlers;
    }

    private Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] GetScopeHandlers(
      IStudioCommandScope scope,
      IStudioCommand command)
    {
        CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> handlers = this.TryGetHandlers(command);
        if (handlers == null)
            return (Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[])null;
        CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> synchronizedDictionary = CollectionHelper.TryGetValue<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>((IDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)handlers, scope);
        if (synchronizedDictionary != null)
        {
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = synchronizedDictionary.CachedValues;
            if (command.PossibleDirection == StudioCommandDirections.Deep)
            {
                Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] array = ((IEnumerable<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)handlers.CachedValues).SelectMany<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>((Func<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>, IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)(d => (IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>)d.CachedValues)).ToArray<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>();
                if (array.Length != 0)
                    scopeHandlers = ArrayHelper.Concat<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>(scopeHandlers, array);
            }
            return scopeHandlers;
        }
        switch (command.PossibleDirection)
        {
            case StudioCommandDirections.None:
                return (Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[])null;
            case StudioCommandDirections.Top:
                return CollectionHelper.TryGetValue<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>((IDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)handlers, this._globalScope)?.CachedValues;
            case StudioCommandDirections.Deep:
                return ((IEnumerable<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)handlers.CachedValues).SelectMany<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>((Func<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>, IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)(d => (IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>)d.CachedValues)).ToArray<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>();
            default:
                throw new ArgumentOutOfRangeException(command.PossibleDirection.ToString());
        }
    }

    bool IStudioCommandService.CanProcess(object sender, IStudioCommand command)
    {
        IStudioCommandScope scopeBySender = this.GetScopeBySender(sender);
        if (scopeBySender == this._undefinedScope)
            return false;
        Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = this.EnsureGetScopeHandlers(scopeBySender, command);
        return scopeHandlers != null && ((IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>)scopeHandlers).All<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>((Func<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, bool>)(tuple => tuple.Item2 == null || tuple.Item2(command)));
    }

    IStudioCommandScope IStudioCommandService.GlobalScope => this._globalScope;

    void IStudioCommandService.Process(
      object sender,
      IStudioCommand command,
      object[] targets,
      bool isSyncProcess)
    {
        if (this.TryGetHandlers(command) == null)
            return;
        if (targets != null)
        {
            foreach (object target in targets)
            {
                Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple;
                if (this._listeners.TryGetValue((command.GetType(), target), out tuple))
                    this.TryEnqueue((isSyncProcess ? 1 : 0) != 0, sender, command, new Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[1]
                    {
            tuple
                    });
            }
        }
        else
        {
            IStudioCommandScope scopeBySender = this.GetScopeBySender(sender);
            if (scopeBySender == this._undefinedScope)
            {
                if (!command.IsPersistent)
                    return;
                lock (this._undefinedCommands.SyncRoot)
                    CollectionHelper.SafeAdd<object, List<IStudioCommand>>((IDictionary<object, List<IStudioCommand>>)this._undefinedCommands, sender).Add(command);
            }
            else
            {
                Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = this.EnsureGetScopeHandlers(scopeBySender, command);
                if (scopeHandlers == null)
                    return;
                this.TryEnqueue(isSyncProcess, sender, command, scopeHandlers);
            }
        }
    }

    private void TryEnqueue(
      bool isSyncProcess,
      object sender,
      IStudioCommand command,
      Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] handlers)
    {
        if (isSyncProcess)
        {
            XamlHelper.EnsureUIThread();
            StudioCommandService.ProcessCommand(sender, command, handlers);
        }
        else
            ((BaseBlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>, QueueEx<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>>>)this._queue).Enqueue(Tuple.Create<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>(sender, command, handlers), false);
    }

    private void Process()
    {
        while (true)
        {
            try
            {
                Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]> tuple;
                if (!((BaseBlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>, QueueEx<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>>>)this._queue).TryDequeue(out tuple, true, true))
                    break;
                StudioCommandService.ProcessCommand(tuple.Item1, tuple.Item2, tuple.Item3);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(ex, (string)null);
            }
        }
    }

    private static void ProcessCommand(
      object sender,
      IStudioCommand command,
      Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers)
    {
        List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>> guiAsyncActions = (List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>>)null;
        foreach (Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> scopeHandler in scopeHandlers)
        {
            if (!scopeHandler.Item3)
            {
                StudioCommandService.ProcessCommand(scopeHandler, sender, command);
            }
            else
            {
                if (guiAsyncActions == null)
                    guiAsyncActions = new List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>>();
                guiAsyncActions.Add(Tuple.Create<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>(scopeHandler, command));
            }
        }
        if (guiAsyncActions == null)
            return;
        GuiDispatcher.GlobalDispatcher.AddAction((Action)(() => guiAsyncActions.ForEach((Action<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>>)(t => StudioCommandService.ProcessCommand(t.Item1, sender, t.Item2)))));
    }

    private static void ProcessCommand(
      Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple,
      object sender,
      IStudioCommand command)
    {
        try
        {
            tuple.Item1(sender, command);
        }
        catch (Exception ex)
        {
            LoggingHelper.LogError(ex, (string)null);
        }
    }

    void IStudioCommandService.Register<TCommand>(
      object listener,
      bool guiAsync,
      Action<object, TCommand> handler,
      Func<TCommand, bool> canExecute)
    {
        Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple = new Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>((Action<object, IStudioCommand>)((sender, cmd) => handler(sender, (TCommand)cmd)), (Func<IStudioCommand, bool>)(cmd => canExecute((TCommand)cmd)), guiAsync);
        this._listeners[(typeof(TCommand), listener)] = tuple;
        ((SynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>)CollectionHelper.SafeAdd<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>((IDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)CollectionHelper.SafeAdd<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>((IDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>)this._handlers, typeof(TCommand)), this.GetScope(listener)))[listener] = tuple;
    }

    void IStudioCommandService.UnRegister(Type commandType, object listener)
    {
        this._listeners.Remove((commandType, listener));
        CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> synchronizedDictionary1;
        CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> synchronizedDictionary2;
        if (!this._handlers.TryGetValue(commandType, out synchronizedDictionary1) || !((SynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)synchronizedDictionary1).TryGetValue(this.GetScope(listener), out synchronizedDictionary2))
            return;
        ((SynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>)synchronizedDictionary2).Remove(listener);
    }

    void IStudioCommandService.Bind(object sender, IStudioCommandScope scope)
    {
        if (sender == null)
            throw new ArgumentNullException(nameof(sender));
        if (scope == null)
            throw new ArgumentNullException(nameof(scope));
        this._binds.Add(sender, scope);
        IStudioCommandScope oldScope;
        if (this._scopes.TryGetValue(sender, out oldScope))
            this.ReplaceHandlersScope(sender, oldScope, scope);
        this._scopes[sender] = scope;
    }

    void IStudioCommandService.UnBind(object sender)
    {
        if (sender == null)
            throw new ArgumentNullException(nameof(sender));
        this._binds.Remove(sender);
        this._scopes.Remove(sender);
    }

    private IStudioCommandScope GetScope(object listener)
    {
        return listener != null ? CollectionHelper.SafeAdd<object, IStudioCommandScope>((IDictionary<object, IStudioCommandScope>)this._scopes, listener, new Func<object, IStudioCommandScope>(this.InternalGetScope)) : throw new ArgumentNullException(nameof(listener));
    }

    private IStudioCommandScope InternalGetScope(object listener)
    {
        if (listener == null)
            throw new ArgumentNullException(nameof(listener));
        if (listener is IStudioCommandScope scope1 && !scope1.UseParentScope)
            return scope1;
        IStudioCommandScope scope2 = CollectionHelper.TryGetValue<object, IStudioCommandScope>((IDictionary<object, IStudioCommandScope>)this._binds, listener);
        if (scope2 != null)
            return scope2;
        if (!(listener is DependencyObject current) || current is Window)
            return this._globalScope;
        DependencyObject parent = LogicalTreeHelper.GetParent(current);
        if (parent != null)
            return this.InternalGetScope((object)parent);
        switch (current)
        {
            case AutoHideGroup _:
                return this._globalScope;
            case LayoutPanel layoutPanel when layoutPanel.DataContext is IStudioCommandScope dataContext:
                return this.InternalGetScope((object)dataContext);
            case FrameworkElement frameworkElement:
                frameworkElement.Loaded += new RoutedEventHandler(this.OnUserControlLoaded);
                break;
            case FrameworkContentElement frameworkContentElement:
                frameworkContentElement.Loaded += new RoutedEventHandler(this.OnUserControlLoaded);
                break;
        }
        return this._undefinedScope;
    }

    private void OnUserControlLoaded(object sender, RoutedEventArgs e)
    {
        IStudioControl current = sender as IStudioControl;
        for (ContentControl contentControl = sender as ContentControl; contentControl != null && current == null; contentControl = contentControl.Content as ContentControl)
            current = contentControl.Content as IStudioControl;
        if (current == null)
            throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotDetermineScope, new object[1]
            {
        sender
            }));
        if ((CollectionHelper.TryGetValue<object, IStudioCommandScope>((IDictionary<object, IStudioCommandScope>)this._scopes, (object)current) ?? this._undefinedScope) != this._undefinedScope)
            return;
        IStudioCommandScope scope = this.InternalGetScope((object)LogicalTreeHelper.GetParent((DependencyObject)current));
        this._scopes[sender] = scope;
        this.ReplaceHandlersScope(sender, this._undefinedScope, scope);
        lock (this._undefinedCommands.SyncRoot)
        {
            List<IStudioCommand> studioCommandList = CollectionHelper.TryGetValue<object, List<IStudioCommand>>((IDictionary<object, List<IStudioCommand>>)this._undefinedCommands, sender);
            if (studioCommandList == null)
                return;
            this._undefinedCommands.Remove(sender);
            foreach (IStudioCommand command in studioCommandList)
            {
                Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = this.EnsureGetScopeHandlers(scope, command);
                if (scopeHandlers != null)
                    ((BaseBlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>, QueueEx<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>>>)this._queue).Enqueue(Tuple.Create<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>(sender, command, scopeHandlers), false);
            }
        }
    }

    private void ReplaceHandlersScope(
      object sender,
      IStudioCommandScope oldScope,
      IStudioCommandScope newScope)
    {
        lock (this._handlers.SyncRoot)
        {
            foreach (KeyValuePair<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>> handler in this._handlers)
            {
                CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> synchronizedDictionary = CollectionHelper.TryGetValue<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>((IDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)handler.Value, oldScope);
                Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple = synchronizedDictionary != null ? CollectionHelper.TryGetValue<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>((IDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>)synchronizedDictionary, sender) : (Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>)null;
                if (tuple != null)
                {
                    ((SynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>)synchronizedDictionary).Remove(sender);
                    ((SynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>)CollectionHelper.SafeAdd<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>((IDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)handler.Value, newScope))[sender] = tuple;
                }
            }
        }
    }

    private CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> TryGetHandlers(
      IStudioCommand command)
    {
        Type type1 = command != null ? command.GetType() : throw new ArgumentNullException(nameof(command));
        CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> handlers = CollectionHelper.TryGetValue<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>((IDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>)this._handlers, type1);
        if (handlers != null)
            return handlers;
        Type type2 = this._handlers.Keys.FirstOrDefault<Type>(new Func<Type, bool>(type1.IsSubclassOf));
        return !(type2 != (Type)null) ? (CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>)null : CollectionHelper.TryGetValue<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>((IDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>)this._handlers, type2);
    }

    protected override void DisposeManaged()
    {
        StudioCommandService service = this;
        StudioCommandService.FlushCommand command = new StudioCommandService.FlushCommand();
        ((IStudioCommandService)service).Process((object)this, (IStudioCommand)command, (object[])null, false);
        command.Wait();
        service.UnRegister<StudioCommandService.FlushCommand>((object)this);
        base.DisposeManaged();
    }

    private sealed class Scope : IStudioCommandScope
    {
        private readonly string _name;

        bool IStudioCommandScope.UseParentScope => false;

        bool IStudioCommandScope.RouteToGlobalScope => false;

        public Scope(string name)
        {
            this._name = !StringHelper.IsEmpty(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        public override string ToString() => this._name;
    }

    private sealed class FlushCommand : BaseStudioCommand
    {
        private readonly SyncObject _syncObject = new SyncObject();
        private bool _isProcessed;

        public void Pulse()
        {
            lock (this._syncObject)
            {
                this._isProcessed = true;
                this._syncObject.Pulse();
            }
        }

        public void Wait()
        {
            lock (this._syncObject)
            {
                if (this._isProcessed)
                    return;
                this._syncObject.Wait(new TimeSpan?());
            }
        }
    }
}
