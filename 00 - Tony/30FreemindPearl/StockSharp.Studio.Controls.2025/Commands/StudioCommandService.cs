// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Commands.StudioCommandService
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Studio.Controls.Commands
{
    public class StudioCommandService : Disposable, IStudioCommandService, IDisposable
    {
        private readonly IStudioCommandScope _globalScope = new Scope( "global" );
        private readonly IStudioCommandScope _undefinedScope = new Scope( "undefined" );
        
        private readonly SynchronizedDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>> _handlers = new SynchronizedDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>();
        private readonly SynchronizedDictionary<(Type cmdType, object listener), Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> _listeners = new SynchronizedDictionary<(Type cmdType, object listener), Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>();
        private readonly SynchronizedPairSet<object, IStudioCommandScope> _binds = new SynchronizedPairSet<object, IStudioCommandScope>();
        private readonly SynchronizedDictionary<object, IStudioCommandScope> _scopes = new SynchronizedDictionary<object, IStudioCommandScope>();
        private readonly BlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]>> _queue = new BlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]>>();
        private readonly SynchronizedDictionary<object, List<IStudioCommand>> _undefinedCommands = new SynchronizedDictionary<object, List<IStudioCommand>>();

        public StudioCommandService()
        {
            
            ThreadingHelper.Launch( ThreadingHelper.Name( ThreadingHelper.Background( ThreadingHelper.ThreadInvariant( new Action( this.Process ) ), true ), "Studio command service thread" ) );
            this.Register<StudioCommandService.FlushCommand>(  this, false, ( Action<StudioCommandService.FlushCommand> ) ( cmd => cmd.Pulse() ), ( Func<StudioCommandService.FlushCommand, bool> ) null );
        }

        private IStudioCommandScope GetScopeBySender( object sender )
        {
            IStudioCommandScope studioCommandScope = sender as IStudioCommandScope;
            if ( studioCommandScope == null )
                return this.GetScope( sender );
            if ( !studioCommandScope.UseParentScope )
                return this._globalScope;
            return this.InternalGetScope( sender );
        }

        private Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ] EnsureGetScopeHandlers(
          IStudioCommandScope scope,
          IStudioCommand command )
        {
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = this.GetScopeHandlers(scope, command);
            if ( scopeHandlers == null && scope.RouteToGlobalScope )
                scopeHandlers = this.GetScopeHandlers( this._globalScope, command );
            return scopeHandlers;
        }

        private Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ] GetScopeHandlers( IStudioCommandScope scope, IStudioCommand command )
        {
            var handlers = this.TryGetHandlers(command);
            if ( handlers == null )
                return null;

            var myscope = handlers.TryGetValue( scope );
            if ( myscope != null )
            {
                var tupleArray = myscope.CachedValues;
                if ( command.PossibleDirection == StudioCommandDirections.Deep )
                {
                    var array = handlers.CachedValues.SelectMany( d => d.CachedValues).ToArray();
                    if ( array.Length != 0 )
                        tupleArray = tupleArray.Concat( array );
                }
                return tupleArray;
            }
            switch ( command.PossibleDirection )
            {
                case StudioCommandDirections.None:
                    return null;

                case StudioCommandDirections.Top:
                    return CollectionHelper.TryGetValue( handlers,  this._globalScope )?.CachedValues;

                case StudioCommandDirections.Deep:
                    return handlers.CachedValues.SelectMany(  ( d => d.CachedValues ) ).ToArray();
                default:
                throw new ArgumentOutOfRangeException( command.PossibleDirection.ToString() );
            }
        }

        bool IStudioCommandService.CanProcess(
          object sender,
          IStudioCommand command )
        {
            IStudioCommandScope scopeBySender = this.GetScopeBySender(sender);
            if ( scopeBySender == this._undefinedScope )
                return false;
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = this.EnsureGetScopeHandlers(scopeBySender, command);
            if ( scopeHandlers == null )
                return false;
            return ( ( IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> ) scopeHandlers ).All<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>( ( Func<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, bool> ) ( tuple =>
            {
                if ( tuple.Item2 != null )
                    return tuple.Item2( command );
                return true;
            } ) );
        }

        IStudioCommandScope IStudioCommandService.GlobalScope
        {
            get
            {
                return this._globalScope;
            }
        }

        void IStudioCommandService.Process(
          object sender,
          IStudioCommand command,
          object [ ] targets,
          bool isSyncProcess )
        {
            if ( this.TryGetHandlers( command ) == null )
                return;
            if ( targets != null )
            {
                foreach ( object target in targets )
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple;
                    if ( this._listeners.TryGetValue( new ValueTuple<Type, object>( command.GetType(), target ), out tuple ) )
                        this.TryEnqueue( ( isSyncProcess ? 1 : 0 ) != 0, sender, command, new Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [1]
                        {
              tuple
                        } );
                }
            }
            else
            {
                IStudioCommandScope scopeBySender = this.GetScopeBySender(sender);
                if ( scopeBySender == this._undefinedScope )
                {
                    if ( !command.IsPersistent )
                        return;
                    lock ( this._undefinedCommands.SyncRoot )
                        ( ( List<IStudioCommand> ) CollectionHelper.SafeAdd<object, List<IStudioCommand>>( this._undefinedCommands,  sender ) ).Add( command );
                }
                else
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = this.EnsureGetScopeHandlers(scopeBySender, command);
                    if ( scopeHandlers == null )
                        return;
                    this.TryEnqueue( isSyncProcess, sender, command, scopeHandlers );
                }
            }
        }

        private void TryEnqueue(
          bool isSyncProcess,
          object sender,
          IStudioCommand command,
          Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ] handlers )
        {
            if ( isSyncProcess )
            {
                XamlHelper.EnsureUIThread();
                StudioCommandService.ProcessCommand( sender, command, handlers );
            }
            else
                ( ( BaseBlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ]>, QueueEx<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ]>>> ) this._queue ).Enqueue( Tuple.Create<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ]>( sender, command, handlers ), false );
        }

        private void Process()
        {
            while ( true )
            {
                try
                {
                    Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[]> tuple;
                    if ( !( ( BaseBlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ]>, QueueEx<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ]>>> ) this._queue ).TryDequeue( out tuple, true, true ) )
                        break;
                    StudioCommandService.ProcessCommand( tuple.Item1, tuple.Item2, tuple.Item3 );
                }
                catch ( Exception ex )
                {
                    LoggingHelper.LogError( ex, ( string ) null );
                }
            }
        }

        private static void ProcessCommand(
          object sender,
          IStudioCommand command,
          Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ] scopeHandlers )
        {
            List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>> guiAsyncActions = (List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>>) null;
            foreach ( Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> scopeHandler in scopeHandlers )
            {
                if ( !scopeHandler.Item3 )
                {
                    StudioCommandService.ProcessCommand( scopeHandler, sender, command );
                }
                else
                {
                    if ( guiAsyncActions == null )
                        guiAsyncActions = new List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>>();
                    guiAsyncActions.Add( Tuple.Create<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>( scopeHandler, command ) );
                }
            }
            if ( guiAsyncActions == null )
                return;
            GuiDispatcher.GlobalDispatcher.AddAction( ( Action ) ( () => guiAsyncActions.ForEach( ( Action<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>> ) ( t => StudioCommandService.ProcessCommand( t.Item1, sender, t.Item2 ) ) ) ) );
        }

        private static void ProcessCommand(
          Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple,
          object sender,
          IStudioCommand command )
        {
            try
            {
                tuple.Item1( sender, command );
            }
            catch ( Exception ex )
            {
                LoggingHelper.LogError( ex, ( string ) null );
            }
        }

        void IStudioCommandService.Register<TCommand>( object listener, bool guiAsync, Action<object, TCommand> handler, Func<TCommand, bool> canExecute )
        {
            var tuple = new Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>((Action<object, IStudioCommand>) ((sender, cmd) => handler(sender, (TCommand) cmd)), (Func<IStudioCommand, bool>) (cmd => canExecute((TCommand) cmd)), guiAsync);
            this._listeners[(typeof( TCommand ), listener)] = tuple;

            _handlers.SafeAdd( typeof( TCommand ) ).SafeAdd( this.GetScope( listener ) )[ listener ] = tuple;
        }

        void IStudioCommandService.UnRegister( Type commandType, object listener )
        {
            this._listeners.Remove( new ValueTuple<Type, object>( commandType, listener ) );
            CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> synchronizedDictionary1;
            CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> synchronizedDictionary2;
            if ( !this._handlers.TryGetValue( commandType, out synchronizedDictionary1 ) || !( ( SynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> ) synchronizedDictionary1 ).TryGetValue( this.GetScope( listener ), out synchronizedDictionary2 ) )
                return;
            ( ( SynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> ) synchronizedDictionary2 ).Remove( listener );
        }

        void IStudioCommandService.Bind( object sender, IStudioCommandScope scope )
        {
            if ( sender == null )
                throw new ArgumentNullException( nameof( sender ) );
            if ( scope == null )
                throw new ArgumentNullException( nameof( scope ) );
            this._binds.Add( sender, scope );
            IStudioCommandScope oldScope;
            if ( this._scopes.TryGetValue( sender, out oldScope ) )
                this.ReplaceHandlersScope( sender, oldScope, scope );
            this._scopes[ sender ] = scope;
        }

        void IStudioCommandService.UnBind( object sender )
        {
            if ( sender == null )
                throw new ArgumentNullException( nameof( sender ) );
            this._binds.Remove( sender );
            this._scopes.Remove( sender );
        }

        private IStudioCommandScope GetScope( object listener )
        {
            if ( listener == null )
                throw new ArgumentNullException( nameof( listener ) );
            return ( IStudioCommandScope ) CollectionHelper.SafeAdd<object, IStudioCommandScope>(  this._scopes,  listener,  new Func<object, IStudioCommandScope>( this.InternalGetScope ) );
        }

        private IStudioCommandScope InternalGetScope( object listener )
        {
            if ( listener == null )
                throw new ArgumentNullException( nameof( listener ) );
            IStudioCommandScope studioCommandScope1 = listener as IStudioCommandScope;
            if ( studioCommandScope1 != null && !studioCommandScope1.UseParentScope )
                return studioCommandScope1;
            IStudioCommandScope studioCommandScope2 = (IStudioCommandScope) CollectionHelper.TryGetValue<object, IStudioCommandScope>( this._binds,  listener);
            if ( studioCommandScope2 != null )
                return studioCommandScope2;
            DependencyObject current = listener as DependencyObject;
            if ( current == null || current is Window )
                return this._globalScope;
            DependencyObject parent = LogicalTreeHelper.GetParent(current);
            if ( parent != null )
                return this.InternalGetScope(  parent );
            if ( current is AutoHideGroup )
                return this._globalScope;
            LayoutPanel layoutPanel = current as LayoutPanel;
            if ( layoutPanel != null )
            {
                IStudioCommandScope dataContext = layoutPanel.DataContext as IStudioCommandScope;
                if ( dataContext != null )
                    return this.InternalGetScope(  dataContext );
            }
            FrameworkElement frameworkElement = current as FrameworkElement;
            if ( frameworkElement == null )
            {
                FrameworkContentElement frameworkContentElement = current as FrameworkContentElement;
                if ( frameworkContentElement != null )
                    frameworkContentElement.Loaded += new RoutedEventHandler( this.OnUserControlLoaded );
            }
            else
                frameworkElement.Loaded += new RoutedEventHandler( this.OnUserControlLoaded );
            return this._undefinedScope;
        }

        private void OnUserControlLoaded( object sender, RoutedEventArgs e )
        {
            IStudioControl studioControl = sender as IStudioControl;
            for ( ContentControl contentControl = sender as ContentControl; contentControl != null && studioControl == null; contentControl = contentControl.Content as ContentControl )
                studioControl = contentControl.Content as IStudioControl;
            if ( studioControl == null )
                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.CannotDetermineScope, new object [1]
                {
          sender
                } ) );
            if ( ( IStudioCommandScope ) ( CollectionHelper.TryGetValue<object, IStudioCommandScope>( this._scopes,  studioControl ) ?? this._undefinedScope ) != this._undefinedScope )
                return;
            IStudioCommandScope scope = this.InternalGetScope((object) LogicalTreeHelper.GetParent((DependencyObject) studioControl));
            this._scopes[ sender] = scope;

            this.ReplaceHandlersScope( sender, this._undefinedScope, scope );

            lock ( this._undefinedCommands.SyncRoot )
            {
                List<IStudioCommand> studioCommandList = (List<IStudioCommand>) CollectionHelper.TryGetValue<object, List<IStudioCommand>>( this._undefinedCommands,  sender);
                if ( studioCommandList == null )
                    return;
                this._undefinedCommands.Remove( sender );
                foreach ( IStudioCommand command in studioCommandList )
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[] scopeHandlers = this.EnsureGetScopeHandlers(scope, command);
                    if ( scopeHandlers != null )
                        ( ( BaseBlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ]>, QueueEx<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ]>>> ) this._queue ).Enqueue( Tuple.Create<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> [ ]>( sender, command, scopeHandlers ), false );
                }
            }
        }

        private void ReplaceHandlersScope( object sender, IStudioCommandScope oldScope, IStudioCommandScope newScope )
        {
            lock ( this._handlers.SyncRoot )
            {
                using ( IEnumerator<KeyValuePair<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>> enumerator = this._handlers.GetEnumerator() )
                {
                    while ( ( ( IEnumerator ) enumerator ).MoveNext() )
                    {
                        var current = enumerator.Current;
                        var synchronizedDictionary =  current.Value.TryGetValue( oldScope );
                        var tuple = synchronizedDictionary != null ? synchronizedDictionary.TryGetValue( sender) : null;
                        if ( tuple != null )
                        {
                            synchronizedDictionary.Remove( sender );
                            current.Value.SafeAdd( newScope ) [ sender] = tuple;
                        }
                    }
                }
            }
        }

        private CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> TryGetHandlers( IStudioCommand command )
        {
            if ( command == null )
                throw new ArgumentNullException( nameof( command ) );
            
            var handler =  this._handlers.TryGetValue( command.GetType() );
            if ( handler != null )
                return handler;

            Type subclassHandler = this._handlers.Keys.FirstOrDefault<Type>(new Func<Type, bool>(command.GetType().IsSubclassOf));
            if ( subclassHandler == null )
                return null;

            return ( this._handlers.TryGetValue(  subclassHandler ) );
        }

        protected override void DisposeManaged()
        {
            StudioCommandService service = this;
            StudioCommandService.FlushCommand flushCommand = new StudioCommandService.FlushCommand();
            ( ( IStudioCommandService ) service ).Process(  this, ( IStudioCommand ) flushCommand, ( object [ ] ) null, false );
            flushCommand.Wait();
            service.UnRegister<StudioCommandService.FlushCommand>(  this );
            base.DisposeManaged();
        }

        private sealed class Scope : IStudioCommandScope
        {
            private readonly string _name;

            bool IStudioCommandScope.UseParentScope
            {
                get
                {
                    return false;
                }
            }

            bool IStudioCommandScope.RouteToGlobalScope
            {
                get
                {
                    return false;
                }
            }

            public Scope( string name )
            {
                if ( StringHelper.IsEmpty( name ) )
                    throw new ArgumentNullException( nameof( name ) );
                this._name = name;
            }

            public override string ToString()
            {
                return this._name;
            }
        }

        private sealed class FlushCommand : BaseStudioCommand
        {
            private readonly SyncObject _syncObject = new SyncObject();
            private bool _isProcessed;

            public void Pulse()
            {
                lock ( this._syncObject )
                {
                    this._isProcessed = true;
                    this._syncObject.Pulse();
                }
            }

            public void Wait()
            {
                lock ( this._syncObject )
                {
                    if ( this._isProcessed )
                        return;
                    this._syncObject.Wait( new TimeSpan?() );
                }
            }
        }
    }
}
