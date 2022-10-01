using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace Ecng.Xaml
{
    /// <summary>
    /// Специальный класс, обеспечивающий исполнение действий в графическом потоке.
    /// </summary>
    public class GuiDispatcher : Disposable, IDispatcher
    {
        private sealed class ActionInfo
        {
            private readonly Action _action;
            private readonly Func<object> _func;
            private object _classlock;
            private object _result;
            private bool _processed;
            private Exception _exception;

            public ActionInfo( Action a )
            {
                Action action = a;
                if ( action == null )
                {
                    throw new ArgumentNullException( "action" );
                }

                this._action = action;
            }

            public ActionInfo( Func<object> f )
            {
                Func<object> func = f;
                if ( func == null )
                    throw new ArgumentNullException( "func" );
                this._func = func;
            }

            public void Process()
            {
                try
                {
                    if ( _action == null )
                    {
                        _result = _func();
                    }
                    else
                    {
                        _action();
                    }
                }
                catch ( Exception ex )
                {
                    if ( this._classlock == null )
                        throw;
                    else
                        this._exception = ex;
                }
                finally
                {
                    if ( this._classlock != null )
                    {
                        lock ( this._classlock )
                        {
                            this._processed = true;
                            Monitor.Pulse( this._classlock );
                        }
                    }
                }
            }

            public void CreateLock()
            {
                this._classlock = new object();
            }

            public T Wait<T>()
            {
                lock ( this._classlock )
                {
                    if ( !this._processed )
                        Monitor.Wait( this._classlock );
                }
                if ( this._exception != null )
                    throw new InvalidOperationException( "_exception is not null", this._exception );
                return this._result.To<T>();
            }
        }

        private readonly object _lock = new object();

        private readonly TimeSpan _timeOut = TimeSpan.FromSeconds( 30.0 );

        private readonly SynchronizedList<GuiDispatcher.ActionInfo> _actions = new SynchronizedList<GuiDispatcher.ActionInfo>();

        private readonly CachedSynchronizedDictionary<object, Action> _periodicalActions = new CachedSynchronizedDictionary<object, Action>();

        private readonly SynchronizedDictionary<Action, int> _periodicalActionsErrors = new SynchronizedDictionary<Action, int>();

        private int _maxPeriodicalActionErrors = 100;

        private TimeSpan _interval = TimeSpan.FromMilliseconds( 1.0 );

        private DispatcherTimer _timer;

        private DateTime _lastTime;

        private long _counter;

        private bool _flushSignal;

        private readonly Dispatcher _dispatcher;

        private static GuiDispatcher _globalDispatcher;

        void IDispatcher.InvokeAsync( Action _param1 )
        {
            this.AddSyncAction( _param1 );
        }

        void IDispatcher.Invoke( Action _param1 )
        {
            this.AddAction( _param1 );
        }
        /// <summary>
        /// </summary>
        public static Dispatcher CurrentThreadDispatcher
        {
            get
            {
                Dispatcher dispatcher = Dispatcher.FromThread( Thread.CurrentThread );
                if ( dispatcher != null )
                    return dispatcher;
                throw new InvalidOperationException( "dispatcher" );
            }
        }

        /// <summary>
        /// Создать <see cref="T:Ecng.Xaml.GuiDispatcher" />.
        /// </summary>
        public GuiDispatcher() : this( CurrentThreadDispatcher )
        {
        }

        /// <summary>
        /// Создать <see cref="T:Ecng.Xaml.GuiDispatcher" />.
        /// </summary>
        /// <param name="d">Объект для доступа к графическому потоку.</param>
        public GuiDispatcher( Dispatcher d )
        {
            Dispatcher dispatch = d;
            if ( dispatch == null )
                throw new ArgumentNullException( "dispatch" );
            this._dispatcher = dispatch;
        }



        /// <summary>Объект для доступа к графическому потоку.</summary>
        public Dispatcher Dispatcher
        {
            get
            {
                return this._dispatcher;
            }
        }

        /// <summary>
        /// </summary>
        public event Action<Exception> Error;

        /// <summary>
        /// </summary>
        public int MaxPeriodicalActionErrors
        {
            get
            {
                return this._maxPeriodicalActionErrors;
            }
            set
            {
                if ( value < -1 )
                    throw new ArgumentOutOfRangeException( "value" );
                this._maxPeriodicalActionErrors = value;
            }
        }

        /// <summary>
        /// Интервал обработки накопленных действий. По-умолчанию равен 1 млс.
        /// </summary>
        public TimeSpan Interval
        {
            get
            {
                return this._interval;
            }
            set
            {
                if ( value <= TimeSpan.Zero )
                    throw new ArgumentOutOfRangeException( "value" );
                this._interval = value;
                this.StopTimer();
                this.StartTimer();
            }
        }

        /// <summary>Количество действий, которое ожидает обработку.</summary>
        public int PendingActionsCount
        {
            get
            {
                return this._actions.Count + this._periodicalActions.Count;
            }
        }

        /// <summary>
        /// </summary>
        public object AddPeriodicalAction( Action action )
        {
            if ( action == null )
                throw new ArgumentNullException( "action" );
            object key = new object();
            this._periodicalActions.Add( key, action );
            this.StartTimer();
            return key;
        }

        /// <summary>Выполнить все действия в очереди.</summary>
        public void FlushPendingActions()
        {
            this._flushSignal = true;
        }

        /// <summary>
        /// </summary>
        public void RemovePeriodicalAction( object token )
        {
            if ( token == null )
                throw new ArgumentNullException( "token" );
            Action andRemove = this._periodicalActions.TryGetAndRemove<object, Action>( token );
            if ( andRemove == null )
                return;
            this._periodicalActionsErrors.Remove( andRemove );
        }

        /// <summary>Добавить действие.</summary>
        /// <param name="action">Действие.</param>
        public void AddAction( Action action )
        {
            if ( this.CheckAccess() )
                action();
            else
                this.AddAction( new GuiDispatcher.ActionInfo( action ) );
        }

        /// <summary>
        /// Добавить действие. Пока оно не будет обработано, метод не отдаст управление программе.
        /// </summary>
        /// <param name="action">Действие.</param>
        public void AddSyncAction( Action action )
        {
            if ( this.CheckAccess() )
            {
                action();
            }
            else
            {
                GuiDispatcher.ActionInfo act = new GuiDispatcher.ActionInfo( action );
                act.CreateLock();
                this.AddAction( act );
                act.Wait<VoidType>();
            }
        }

        /// <summary>
        /// Добавить действие. Пока оно не будет обработано, метод не отдаст управление программе.
        /// </summary>
        /// <param name="action">Действие, возвращающее результат.</param>
        public T AddSyncAction<T>( Func<T> action )
        {
            if ( this.CheckAccess() )
            {
                return action();
            }

            GuiDispatcher.ActionInfo ai = new GuiDispatcher.ActionInfo( () => action );
            ai.CreateLock();
            this.AddAction( ai );
            return ai.Wait<T>();
        }



        private void AddAction( GuiDispatcher.ActionInfo _param1 )
        {
            this._actions.Add( _param1 );
            this.StartTimer();
        }

        private void StartTimer()
        {
            this._lastTime = DateTime.Now;
            lock ( this._lock )
            {
                if ( this._timer != null )
                    return;
                this._timer = new DispatcherTimer( DispatcherPriority.Normal, this.Dispatcher );
                _timer.Tick += new EventHandler( OnTimerTick );
                this._timer.Interval += new TimeSpan( this.Interval.Ticks / 10L );
                this._timer.Start();
            }
        }

        private sealed class SomeShit
        {
            public static readonly GuiDispatcher.SomeShit ShitMethod02 = new GuiDispatcher.SomeShit();
            public static Func<SynchronizedList<GuiDispatcher.ActionInfo>, GuiDispatcher.ActionInfo[ ]> _functions;

            internal GuiDispatcher.ActionInfo[ ] CopyAndClear(
              SynchronizedList<GuiDispatcher.ActionInfo> p )
            {
                return p.CopyAndClear<GuiDispatcher.ActionInfo>();
            }
        }


        private void OnTimerTick( object _param1, EventArgs _param2 )
        {
            if ( ++this._counter % 10L != 0L && !this._flushSignal )
                return;

            this._flushSignal = false;
            bool flag = false;

            foreach ( var ai in this._actions.SyncGet<SynchronizedList<GuiDispatcher.ActionInfo>, GuiDispatcher.ActionInfo[ ]>( ( p ) => p.CopyAndClear<GuiDispatcher.ActionInfo>() ) )
            {
                flag = true;
                try
                {
                    ai.Process();
                }
                catch ( Exception ex1 )
                {
                    try
                    {
                        Action<Exception> myError = this.Error;
                        if ( myError != null )
                            myError( ex1 );
                    }
                    catch ( Exception ex2 )
                    {
                    }
                }
            }

            foreach ( KeyValuePair<object, Action> cachedPair in this._periodicalActions.CachedPairs )
            {
                Action key = cachedPair.Value;
                flag = true;
                try
                {
                    key();
                    this._periodicalActionsErrors.Remove( key );
                }
                catch ( Exception ex1 )
                {
                    try
                    {
                        Action<Exception> myError = this.Error;
                        if ( myError != null )
                            myError( ex1 );
                    }
                    catch ( Exception ex2 )
                    {
                    }
                    if ( this.MaxPeriodicalActionErrors >= 0 )
                    {
                        int num;
                        if ( !this._periodicalActionsErrors.TryGetValue( key, out num ) )
                            num = 0;
                        ++num;
                        if ( num >= this.MaxPeriodicalActionErrors )
                            this._periodicalActions.Remove( cachedPair.Key );
                        this._periodicalActionsErrors[key] = num;
                    }
                }
            }
            if ( flag || !( DateTime.Now - this._lastTime > this._timeOut ) )
                return;
            this.StopTimer();
        }

        private void StopTimer()
        {
            lock ( this._lock )
            {
                if ( this._timer == null )
                    return;
                this._timer.Stop();
                this._timer = ( DispatcherTimer )null;
            }
        }

        /// <summary>
        /// </summary>
        public static GuiDispatcher GlobalDispatcher
        {
            get
            {
                return GuiDispatcher._globalDispatcher ?? ( GuiDispatcher._globalDispatcher = new GuiDispatcher() );
            }
        }

        /// <summary>
        /// </summary>
        public static void InitGlobalDispatcher()
        {
            if ( GuiDispatcher._globalDispatcher != null )
                return;
            GuiDispatcher._globalDispatcher = new GuiDispatcher();
        }

        /// <summary>Освободить занятые ресурсы.</summary>
        protected override void DisposeManaged()
        {
            this.StopTimer();
            base.DisposeManaged();
        }

        /// <inheritdoc />
        public bool CheckAccess()
        {
            return this.Dispatcher.CheckAccess();
        }
    }
}
