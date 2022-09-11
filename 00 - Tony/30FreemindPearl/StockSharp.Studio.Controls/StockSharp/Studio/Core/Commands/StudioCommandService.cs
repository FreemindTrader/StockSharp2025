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
        private readonly IStudioCommandScope _globalScope = new Scope( "global" );
        private readonly IStudioCommandScope _undefinedScope = new Scope( "undefined" );
        private readonly SynchronizedDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>> _handlers = new SynchronizedDictionary<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>>();
        private readonly SynchronizedDictionary<Tuple<Type, object>, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> _listeners = new SynchronizedDictionary<Tuple<Type, object>, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>();
        private readonly SynchronizedPairSet<object, IStudioCommandScope> _binds = new SynchronizedPairSet<object, IStudioCommandScope>();
        private readonly SynchronizedDictionary<object, IStudioCommandScope> _scopes = new SynchronizedDictionary<object, IStudioCommandScope>();
        private readonly BlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]>> _queue = new BlockingQueue<Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]>>();
        private readonly SynchronizedDictionary<object, List<IStudioCommand>> _undefinedCommands = new SynchronizedDictionary<object, List<IStudioCommand>>();

        public StudioCommandService()
        {
            new Action( Process ).ThreadInvariant().Background( true ).Name( "Studio command service thread" ).Launch();
            this.Register<FlushCommand>( this, false, cmd => cmd.Pulse(), null );
        }

        private IStudioCommandScope GetScopeBySender( object sender )
        {
            IStudioCommandScope studioCommandScope = sender as IStudioCommandScope;
            if ( studioCommandScope == null )
                return GetScope( sender );
            if ( !studioCommandScope.UseParentScope )
                return _globalScope;
            return InternalGetScope( sender );
        }

        private Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] EnsureGetScopeHandlers(
          IStudioCommandScope scope,
          IStudioCommand command )
        {
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers = GetScopeHandlers( scope, command );
            if ( scopeHandlers == null && scope.RouteToGlobalScope )
                scopeHandlers = GetScopeHandlers( _globalScope, command );
            return scopeHandlers;
        }

        private Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] GetScopeHandlers(
          IStudioCommandScope scope,
          IStudioCommand command )
        {
            CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> handlers = TryGetHandlers( command );
            if ( handlers == null )
                return null;
            CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> synchronizedDictionary = handlers.TryGetValue( scope );
            if ( synchronizedDictionary != null )
            {
                Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] first = synchronizedDictionary.CachedValues;
                if ( command.PossibleDirection == StudioCommandDirections.Deep )
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] array = handlers.CachedValues.SelectMany( d => d.CachedValues ).ToArray();
                    if ( array.Length != 0 )
                        first = first.Concat( array );
                }
                return first;
            }
            switch ( command.PossibleDirection )
            {
                case StudioCommandDirections.None:
                    return null;
                case StudioCommandDirections.Top:
                    return handlers.TryGetValue( _globalScope )?.CachedValues;
                case StudioCommandDirections.Deep:
                    return handlers.CachedValues.SelectMany( d => d.CachedValues ).ToArray();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        bool IStudioCommandService.CanProcess(
          object sender,
          IStudioCommand command )
        {
            IStudioCommandScope scopeBySender = GetScopeBySender( sender );
            if ( scopeBySender == _undefinedScope )
                return false;
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers = EnsureGetScopeHandlers( scopeBySender, command );
            if ( scopeHandlers == null )
                return false;
            return scopeHandlers.All( tuple =>
                  {
                      if ( tuple.Item2 != null )
                          return tuple.Item2( command );
                      return true;
                  } );
        }

        IStudioCommandScope IStudioCommandService.GlobalScope
        {
            get
            {
                return _globalScope;
            }
        }

        void IStudioCommandService.Process(
          object sender,
          IStudioCommand command,
          object[ ] targets,
          bool isSyncProcess )
        {
            if ( TryGetHandlers( command ) == null )
                return;
            if ( targets != null )
            {
                foreach ( object target in targets )
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple;
                    if ( _listeners.TryGetValue( Tuple.Create( command.GetType(), target ), out tuple ) )
                        TryEnqueue( ( isSyncProcess ? 1 : 0 ) != 0, sender, command, new Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[1]
                        {
              tuple
                        } );
                }
            }
            else
            {
                IStudioCommandScope scopeBySender = GetScopeBySender( sender );
                if ( scopeBySender == _undefinedScope )
                {
                    if ( !command.IsPersistent )
                        return;
                    lock ( _undefinedCommands.SyncRoot )
                        _undefinedCommands.SafeAdd( sender ).Add( command );
                }
                else
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers = EnsureGetScopeHandlers( scopeBySender, command );
                    if ( scopeHandlers == null )
                        return;
                    TryEnqueue( isSyncProcess, sender, command, scopeHandlers );
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
                ProcessCommand( sender, command, handlers );
            }
            else
                _queue.Enqueue( Tuple.Create( sender, command, handlers ), false );
        }

        private void Process()
        {
            while ( true )
            {
                try
                {
                    Tuple<object, IStudioCommand, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ]> tuple;
                    if ( !_queue.TryDequeue( out tuple, true, true ) )
                        break;
                    ProcessCommand( tuple.Item1, tuple.Item2, tuple.Item3 );
                }
                catch ( Exception ex )
                {
                    ex.LogError( null );
                }
            }
        }

        private void ProcessCommand(
          object sender,
          IStudioCommand command,
          Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers )
        {
            List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>> guiAsyncActions = null;
            foreach ( Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> scopeHandler in scopeHandlers )
            {
                if ( !scopeHandler.Item3 )
                {
                    ProcessCommand( scopeHandler, sender, command );
                }
                else
                {
                    if ( guiAsyncActions == null )
                        guiAsyncActions = new List<Tuple<Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>, IStudioCommand>>();
                    guiAsyncActions.Add( Tuple.Create( scopeHandler, command ) );
                }
            }
            if ( guiAsyncActions == null )
                return;
            GuiDispatcher.GlobalDispatcher.AddAction( () => guiAsyncActions.ForEach( t => ProcessCommand( t.Item1, sender, t.Item2 ) ) );
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
                ex.LogError( null );
            }
        }

        void IStudioCommandService.Register<TCommand>(
          object listener,
          bool guiAsync,
          Action<object, TCommand> handler,
          Func<TCommand, bool> canExecute )
        {
            Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple = new Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>( ( sender, cmd ) => handler( sender, ( TCommand )cmd ), cmd => canExecute( ( TCommand )cmd ), guiAsync );
            _listeners[Tuple.Create( typeof( TCommand ), listener )] = tuple;
            _handlers.SafeAdd( typeof( TCommand ) ).SafeAdd( GetScope( listener ) )[listener] = tuple;
        }

        void IStudioCommandService.UnRegister<TCommand>( object listener )
        {
            _listeners.Remove( Tuple.Create( typeof( TCommand ), listener ) );
            CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> dict = _handlers.TryGetValue( typeof( TCommand ) );
            if ( dict == null )
                return;
            dict.TryGetValue( GetScope( listener ) )?.Remove( listener );
        }

        void IStudioCommandService.Bind( object sender, IStudioCommandScope scope )
        {
            if ( sender == null )
                throw new ArgumentNullException( nameof( sender ) );
            if ( scope == null )
                throw new ArgumentNullException( nameof( scope ) );
            _binds.Add( sender, scope );
            IStudioCommandScope oldScope = _scopes.TryGetValue( sender );
            if ( oldScope != null )
                ReplaceHandlersScope( sender, oldScope, scope );
            _scopes[sender] = scope;
        }

        void IStudioCommandService.UnBind( object sender )
        {
            if ( sender == null )
                throw new ArgumentNullException( nameof( sender ) );
            _binds.Remove( sender );
            _scopes.Remove( sender );
        }

        private IStudioCommandScope GetScope( object listener )
        {
            if ( listener == null )
                throw new ArgumentNullException( nameof( listener ) );
            return _scopes.SafeAdd( listener, new Func<object, IStudioCommandScope>( InternalGetScope ) );
        }

        private IStudioCommandScope InternalGetScope( object listener )
        {
            if ( listener == null )
                throw new ArgumentNullException( nameof( listener ) );
            IStudioCommandScope studioCommandScope1 = listener as IStudioCommandScope;
            if ( studioCommandScope1 != null && !studioCommandScope1.UseParentScope )
                return studioCommandScope1;
            IStudioCommandScope studioCommandScope2 = _binds.TryGetValue( listener );
            if ( studioCommandScope2 != null )
                return studioCommandScope2;
            DependencyObject current = listener as DependencyObject;
            if ( current == null || current is Window )
                return _globalScope;
            DependencyObject parent = LogicalTreeHelper.GetParent( current );
            if ( parent != null )
                return InternalGetScope( parent );
            if ( current is AutoHideGroup )
                return _globalScope;
            LayoutPanel layoutPanel = current as LayoutPanel;
            if ( layoutPanel != null )
            {
                IStudioCommandScope dataContext = layoutPanel.DataContext as IStudioCommandScope;
                if ( dataContext != null )
                    return InternalGetScope( dataContext );
            }
            FrameworkElement frameworkElement = current as FrameworkElement;
            if ( frameworkElement == null )
            {
                FrameworkContentElement frameworkContentElement = current as FrameworkContentElement;
                if ( frameworkContentElement != null )
                    frameworkContentElement.Loaded += new RoutedEventHandler( OnUserControlLoaded );
            }
            else
                frameworkElement.Loaded += new RoutedEventHandler( OnUserControlLoaded );
            return _undefinedScope;
        }

        private void OnUserControlLoaded( object sender, RoutedEventArgs e )
        {
            IStudioControl studioControl = sender as IStudioControl;
            for ( ContentControl contentControl = sender as ContentControl; contentControl != null && studioControl == null; contentControl = contentControl.Content as ContentControl )
                studioControl = contentControl.Content as IStudioControl;
            if ( studioControl == null )
                throw new InvalidOperationException( LocalizedStrings.Str3597Params.Put( sender ) );
            if ( ( _scopes.TryGetValue( studioControl ) ?? _undefinedScope ) != _undefinedScope )
                return;
            IStudioCommandScope scope = InternalGetScope( LogicalTreeHelper.GetParent( ( DependencyObject )studioControl ) );
            _scopes[sender] = scope;
            ReplaceHandlersScope( sender, _undefinedScope, scope );
            lock ( _undefinedCommands.SyncRoot )
            {
                List<IStudioCommand> studioCommandList = _undefinedCommands.TryGetValue( sender );
                if ( studioCommandList == null )
                    return;
                _undefinedCommands.Remove( sender );
                foreach ( IStudioCommand command in studioCommandList )
                {
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>[ ] scopeHandlers = EnsureGetScopeHandlers( scope, command );
                    if ( scopeHandlers != null )
                        _queue.Enqueue( Tuple.Create( sender, command, scopeHandlers ), false );
                }
            }
        }

        private void ReplaceHandlersScope(
          object sender,
          IStudioCommandScope oldScope,
          IStudioCommandScope newScope )
        {
            lock ( _handlers.SyncRoot )
            {
                foreach ( KeyValuePair<Type, CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>>> handler in _handlers )
                {
                    CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>> dict = handler.Value.TryGetValue( oldScope );
                    Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool> tuple = dict != null ? dict.TryGetValue( sender ) : null;
                    if ( tuple != null )
                    {
                        dict.Remove( sender );
                        handler.Value.SafeAdd( newScope )[sender] = tuple;
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
            CachedSynchronizedDictionary<IStudioCommandScope, CachedSynchronizedDictionary<object, Tuple<Action<object, IStudioCommand>, Func<IStudioCommand, bool>, bool>>> synchronizedDictionary = _handlers.TryGetValue( type );
            if ( synchronizedDictionary != null )
                return synchronizedDictionary;
            Type key = _handlers.Keys.FirstOrDefault( new Func<Type, bool>( type.IsSubclassOf ) );
            if ( !( key != null ) )
                return null;
            return _handlers.TryGetValue( key );
        }

        protected override void DisposeManaged()
        {
            StudioCommandService studioCommandService = this;
            FlushCommand flushCommand = new FlushCommand();
            ( ( IStudioCommandService )studioCommandService ).Process( this, flushCommand, null, false );
            flushCommand.Wait();
            ( ( IStudioCommandService )studioCommandService ).UnRegister<FlushCommand>( this );
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
                _name = name;
            }

            public override string ToString()
            {
                return _name;
            }
        }

        private sealed class FlushCommand : BaseStudioCommand
        {
            private readonly SyncObject _syncObject = new SyncObject();
            private bool _isProcessed;

            public void Pulse()
            {
                lock ( _syncObject )
                {
                    _isProcessed = true;
                    _syncObject.Pulse();
                }
            }

            public void Wait()
            {
                lock ( _syncObject )
                {
                    if ( _isProcessed )
                        return;
                    _syncObject.Wait( new TimeSpan?() );
                }
            }
        }
    }
}
