// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.StudioCommandService
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Studio.Core.Commands
{
    public class StudioCommandService : Disposable, IStudioCommandService, IDisposable
    {
        private readonly IStudioCommandScope _globalScope = ( IStudioCommandScope )new StudioCommandService.Scope( "global" );
        private readonly IStudioCommandScope _undefinedScope = ( IStudioCommandScope )new StudioCommandService.Scope( "undefined" );
        private readonly SynchronizedDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>> _handlers = new SynchronizedDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>();
        private readonly SynchronizedDictionary<Tuple<Type, object>, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> _listeners = new SynchronizedDictionary<Tuple<Type, object>, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>();
        private readonly SynchronizedPairSet<object, IStudioCommandScope> _binds = new SynchronizedPairSet<object, IStudioCommandScope>();
        private readonly SynchronizedDictionary<object, IStudioCommandScope> _scopes = new SynchronizedDictionary<object, IStudioCommandScope>();
        private readonly BlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]>> _queue = new BlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]>>();
        private readonly SynchronizedDictionary<object, List<IStudioCommand>> _undefinedCommands = new SynchronizedDictionary<object, List<IStudioCommand>>();

        public StudioCommandService()
        {
            new Action( this.Process ).ThreadInvariant().Background( true ).Name( "Studio command service thread" ).Launch();
            this.Register<StudioCommandService.FlushCommand>( ( object )this, false, ( Action<StudioCommandService.FlushCommand> )( cmd => cmd.Pulse() ), ( Func<StudioCommandService.FlushCommand, bool> )null );
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

        private Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] EnsureGetScopeHandlers(
          IStudioCommandScope scope,
          IStudioCommand command )
        {
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers = this.GetScopeHandlers( scope, command );
            if ( scopeHandlers == null && scope.RouteToGlobalScope )
                scopeHandlers = this.GetScopeHandlers( this._globalScope, command );
            return scopeHandlers;
        }

        private Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] GetScopeHandlers(
          IStudioCommandScope scope,
          IStudioCommand command )
        {
            CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> handlers = this.TryGetHandlers( command );
            if ( handlers == null )
                return ( Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] )null;
            CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> synchronizedDictionary = handlers.TryGetValue<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>( scope );
            if ( synchronizedDictionary != null )
            {
                Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] first = synchronizedDictionary.CachedValues;
                if ( command.PossibleDirection == StudioCommandDirections.Deep )
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] array = ( ( IEnumerable<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> )handlers.CachedValues ).SelectMany<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>( ( Func<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>, IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> )( d => ( IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> )d.CachedValues ) ).ToArray<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>();
                    if ( array.Length != 0 )
                        first = first.Concat<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>( array );
                }
                return first;
            }
            switch ( command.PossibleDirection )
            {
                case StudioCommandDirections.None:
                    return ( Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] )null;
                case StudioCommandDirections.Top:
                    return handlers.TryGetValue<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>( this._globalScope )?.CachedValues;
                case StudioCommandDirections.Deep:
                    return ( ( IEnumerable<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> )handlers.CachedValues ).SelectMany<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>( ( Func<CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>, IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> )( d => ( IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> )d.CachedValues ) ).ToArray<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        bool IStudioCommandService.CanProcess(
          object sender,
          IStudioCommand command )
        {
            IStudioCommandScope scopeBySender = this.GetScopeBySender( sender );
            if ( scopeBySender == this._undefinedScope )
                return false;
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers = this.EnsureGetScopeHandlers( scopeBySender, command );
            if ( scopeHandlers == null )
                return false;
            return ( ( IEnumerable<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> )scopeHandlers ).All<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>( ( Func<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, bool> )( tuple =>
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
          object[ ] targets,
          bool isSyncProcess )
        {
            if ( this.TryGetHandlers( command ) == null )
                return;
            if ( targets != null )
            {
                foreach ( object target in targets )
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple;
                    if ( this._listeners.TryGetValue( Tuple.Create<Type, object>( command.GetType(), target ), out tuple ) )
                        this.TryEnqueue( ( isSyncProcess ? 1 : 0 ) != 0, sender, command, new Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[1]
                        {
              tuple
                        } );
                }
            }
            else
            {
                IStudioCommandScope scopeBySender = this.GetScopeBySender( sender );
                if ( scopeBySender == this._undefinedScope )
                {
                    if ( !command.IsPersistent )
                        return;
                    lock ( this._undefinedCommands.SyncRoot )
                        this._undefinedCommands.SafeAdd<object, List<IStudioCommand>>( sender ).Add( command );
                }
                else
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers = this.EnsureGetScopeHandlers( scopeBySender, command );
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
          Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] handlers )
        {
            if ( isSyncProcess )
            {
                if ( Thread.CurrentThread != GuiDispatcher.GlobalDispatcher.Dispatcher.Thread )
                    throw new ArgumentException( LocalizedStrings.Str3596 );
                this.ProcessCommand( sender, command, handlers );
            }
            else
                this._queue.Enqueue( Tuple.Create<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]>( sender, command, handlers ), false );
        }

        private void Process()
        {
            while ( true )
            {
                try
                {
                    Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]> tuple;
                    if ( !this._queue.TryDequeue( out tuple, true, true ) )
                        break;
                    this.ProcessCommand( tuple.Item1, tuple.Item2, tuple.Item3 );
                }
                catch ( Exception ex )
                {
                    ex.LogError( ( string )null );
                }
            }
        }

        private void ProcessCommand(
          object sender,
          IStudioCommand command,
          Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers )
        {
            List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>> guiAsyncActions = ( List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>> )null;
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
            GuiDispatcher.GlobalDispatcher.AddAction( ( Action )( () => guiAsyncActions.ForEach( ( Action<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>> )( t => StudioCommandService.ProcessCommand( t.Item1, sender, t.Item2 ) ) ) ) );
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
                ex.LogError( ( string )null );
            }
        }

        void IStudioCommandService.Register<TCommand>(
          object listener,
          bool guiAsync,
          Action<object, TCommand> handler,
          Func<TCommand, bool> canExecute )
        {
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple = new Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>( ( Action<object, IStudioCommand> )( ( sender, cmd ) => handler( sender, ( TCommand )cmd ) ), ( Func<IStudioCommand, bool> )( cmd => canExecute( ( TCommand )cmd ) ), guiAsync );
            this._listeners[Tuple.Create<Type, object>( typeof( TCommand ), listener )] = tuple;
            this._handlers.SafeAdd<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>( typeof( TCommand ) ).SafeAdd<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>( this.GetScope( listener ) )[listener] = tuple;
        }

        void IStudioCommandService.UnRegister<TCommand>( object listener )
        {
            this._listeners.Remove( Tuple.Create<Type, object>( typeof( TCommand ), listener ) );
            CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> dict = this._handlers.TryGetValue<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>( typeof( TCommand ) );
            if ( dict == null )
                return;
            dict.TryGetValue<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>( this.GetScope( listener ) )?.Remove( listener );
        }

        void IStudioCommandService.Bind( object sender, IStudioCommandScope scope )
        {
            if ( sender == null )
                throw new ArgumentNullException( nameof( sender ) );
            if ( scope == null )
                throw new ArgumentNullException( nameof( scope ) );
            this._binds.Add( sender, scope );
            IStudioCommandScope oldScope = this._scopes.TryGetValue<object, IStudioCommandScope>( sender );
            if ( oldScope != null )
                this.ReplaceHandlersScope( sender, oldScope, scope );
            this._scopes[sender] = scope;
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
            return this._scopes.SafeAdd<object, IStudioCommandScope>( listener, new Func<object, IStudioCommandScope>( this.InternalGetScope ) );
        }

        private IStudioCommandScope InternalGetScope( object listener )
        {
            if ( listener == null )
                throw new ArgumentNullException( nameof( listener ) );
            IStudioCommandScope studioCommandScope1 = listener as IStudioCommandScope;
            if ( studioCommandScope1 != null && !studioCommandScope1.UseParentScope )
                return studioCommandScope1;
            IStudioCommandScope studioCommandScope2 = this._binds.TryGetValue<object, IStudioCommandScope>( listener );
            if ( studioCommandScope2 != null )
                return studioCommandScope2;
            DependencyObject current = listener as DependencyObject;
            if ( current == null || current is Window )
                return this._globalScope;
            DependencyObject parent = LogicalTreeHelper.GetParent( current );
            if ( parent != null )
                return this.InternalGetScope( ( object )parent );
            if ( current is AutoHideGroup )
                return this._globalScope;
            LayoutPanel layoutPanel = current as LayoutPanel;
            if ( layoutPanel != null )
            {
                IStudioCommandScope dataContext = layoutPanel.DataContext as IStudioCommandScope;
                if ( dataContext != null )
                    return this.InternalGetScope( ( object )dataContext );
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
                throw new InvalidOperationException( LocalizedStrings.Str3597Params.Put( sender ) );
            if ( ( this._scopes.TryGetValue<object, IStudioCommandScope>( ( object )studioControl ) ?? this._undefinedScope ) != this._undefinedScope )
                return;
            IStudioCommandScope scope = this.InternalGetScope( ( object )LogicalTreeHelper.GetParent( ( DependencyObject )studioControl ) );
            this._scopes[sender] = scope;
            this.ReplaceHandlersScope( sender, this._undefinedScope, scope );
            lock ( this._undefinedCommands.SyncRoot )
            {
                List<IStudioCommand> studioCommandList = this._undefinedCommands.TryGetValue<object, List<IStudioCommand>>( sender );
                if ( studioCommandList == null )
                    return;
                this._undefinedCommands.Remove( sender );
                foreach ( IStudioCommand command in studioCommandList )
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers = this.EnsureGetScopeHandlers( scope, command );
                    if ( scopeHandlers != null )
                        this._queue.Enqueue( Tuple.Create<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]>( sender, command, scopeHandlers ), false );
                }
            }
        }

        private void ReplaceHandlersScope(
          object sender,
          IStudioCommandScope oldScope,
          IStudioCommandScope newScope )
        {
            lock ( this._handlers.SyncRoot )
            {
                foreach ( KeyValuePair<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>> handler in this._handlers )
                {
                    CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> dict = handler.Value.TryGetValue<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>( oldScope );
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple = dict != null ? dict.TryGetValue<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>( sender ) : ( Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> )null;
                    if ( tuple != null )
                    {
                        dict.Remove( sender );
                        handler.Value.SafeAdd<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>( newScope )[sender] = tuple;
                    }
                }
            }
        }

        private CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> TryGetHandlers(
          IStudioCommand command )
        {
            if ( command == null )
                throw new ArgumentNullException( nameof( command ) );
            Type type = command.GetType();
            CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> synchronizedDictionary = this._handlers.TryGetValue<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>( type );
            if ( synchronizedDictionary != null )
                return synchronizedDictionary;
            Type key = this._handlers.Keys.FirstOrDefault<Type>( new Func<Type, bool>( type.IsSubclassOf ) );
            if ( !( key != ( Type )null ) )
                return ( CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> )null;
            return this._handlers.TryGetValue<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>( key );
        }

        protected override void DisposeManaged()
        {
            StudioCommandService studioCommandService = this;
            StudioCommandService.FlushCommand flushCommand = new StudioCommandService.FlushCommand();
            ( ( IStudioCommandService )studioCommandService ).Process( ( object )this, ( IStudioCommand )flushCommand, ( object[ ] )null, false );
            flushCommand.Wait();
            ( ( IStudioCommandService )studioCommandService ).UnRegister<StudioCommandService.FlushCommand>( ( object )this );
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
                if ( name.IsEmpty() )
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
