// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.BaseHydraTask
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockSharp.Hydra.Core
{
    /// <summary>Базовая класс для задачи.</summary>
    public abstract class BaseHydraTask : BaseLogReceiver, IHydraTask, ILogReceiver, ILogSource, IDisposable, ICloneable<IHydraTask>, ICloneable, IPersistable, INotifyPropertyChanged, IScheduledTask
    {
        private readonly SyncObject _syncObject = new SyncObject();
        private readonly SynchronizedSet<Security> _workingSecurities = new SynchronizedSet<Security>();
        private WorkingTime _workingTime = new WorkingTime();
        private IMarketDataDrive _drive = ServicesRegistry.DriveCache.DefaultDrive;
        private readonly CachedSynchronizedSet<Level1Fields> _supportedLevel1Fields = new CachedSynchronizedSet<Level1Fields>();
        private IEnumerable<HydraTaskSecurity> _securities = Enumerable.Empty<HydraTaskSecurity>();
        private int _currentErrorCount;
        private bool _isAllSecurity;
        private bool _isEnabled;
        private string _title;
        private TaskStates _state;
        private DateTimeOffset _lastServerTime;

        /// <inheritdoc />
        [Display( Description = "Str2230", GroupName = "General", Name = "Str2229", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        [Browsable( false )]
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if ( _isEnabled == value )
                    return;
                _isEnabled = value;
                NotifyPropertyChanged( nameof( IsEnabled ) );
            }
        }

        bool IScheduledTask.CanStart
        {
            get
            {
                if ( IsEnabled )
                    return State == TaskStates.Stopped;
                return false;
            }
        }

        bool IScheduledTask.CanStop
        {
            get
            {
                return State == TaskStates.Started;
            }
        }

        /// <inheritdoc />
        [Display( Description = "WorkingHours", GroupName = "General", Name = "WorkingTime", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public WorkingTime WorkingTime
        {
            get
            {
                return _workingTime;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                if ( _workingTime == value )
                    return;
                _workingTime = value;
                NotifyPropertyChanged( nameof( WorkingTime ) );
            }
        }

        /// <summary>Интервал работы.</summary>
        [Display( Description = "Str2235Dot", GroupName = "General", Name = "Str2235", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        [TimeSpanEditor( Mask = TimeSpanEditorMask.Days | TimeSpanEditorMask.Hours | TimeSpanEditorMask.Minutes | TimeSpanEditorMask.Seconds )]
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds( 5.0 );

        /// <inheritdoc />
        [Display( Description = "Str2238", GroupName = "General", Name = "Str2237", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        public IMarketDataDrive Drive
        {
            get
            {
                return _drive;
            }
            set
            {
                IMarketDataDrive marketDataDrive = value;
                if ( marketDataDrive == null )
                    throw new ArgumentNullException( nameof( value ) );
                _drive = marketDataDrive;
            }
        }

        /// <inheritdoc />
        [Display( Description = "Str2240", GroupName = "General", Name = "Str2239", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
        public StorageFormats StorageFormat { get; set; }

        /// <inheritdoc />
        [Display( Description = "Str2242", GroupName = "General", Name = "Str2241", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
        public IHydraTask DependFrom { get; set; }

        /// <summary>
        /// Максимальное количество ошибок, после которого задача будет остановлена.
        /// По-умолчанию равно 0, что означает игнорирование количества ошибок.
        /// </summary>
        [Display( Description = "Str2245", GroupName = "General", Name = "Str2244", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
        public int MaxErrorCount { get; set; }

        /// <summary>Поддерживаемые поля маркет-данных первого уровня.</summary>
        [Browsable( false )]
        [Display( Description = "Str2247", GroupName = "General", Name = "Str2246", Order = 7, ResourceType = typeof( LocalizedStrings ) )]
        [ItemsSource( typeof( Level1Fields ) )]
        [EditorExtension( ShowSelectedItemsCount = true )]
        public virtual IEnumerable<Level1Fields> SupportedLevel1Fields
        {
            get
            {
                return _supportedLevel1Fields.Cache;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                lock ( _supportedLevel1Fields.SyncRoot )
                {
                    _supportedLevel1Fields.Clear();
                    _supportedLevel1Fields.AddRange( value );
                }
            }
        }

        /// <inheritdoc />
        [Browsable( false )]
        public bool IsDefault { get; set; } = true;

        /// <inheritdoc />
        [Display( Description = "Str2248", GroupName = "General", Name = "Str215", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if ( _title == value )
                    return;
                if ( value.IsEmpty() )
                    value = this.GetDisplayName();
                _title = value;
                NotifyPropertyChanged( nameof( Title ) );
            }
        }

        /// <inheritdoc />
        public event Action<IHydraTask> Started;

        /// <inheritdoc />
        public event Action<IHydraTask> Stopped;

        /// <summary>
        /// Сохранить инструмент если он отсутствует в <see cref="P:StockSharp.Algo.Storages.IEntityRegistry.Securities" />.
        /// </summary>
        /// <param name="security">Инструмент.</param>
        protected void SaveSecurity( Security security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            if ( EntityRegistry.Securities.LookupById( security.ToSecurityId( null, true, false ) ) != null )
                return;
            EntityRegistry.Securities.Save( security, false );
            this.AddInfoLog( LocalizedStrings.Str2188Params, security );
        }

        /// <inheritdoc />
        [Browsable( false )]
        public override string Name
        {
            get
            {
                if ( !Title.IsEmpty() )
                    return Title;
                return this.GetDisplayName();
            }
        }

        /// <inheritdoc />
        [Browsable( false )]
        public Uri Icon
        {
            get
            {
                return GetType().GetIcon();
            }
        }

        /// <summary>Хранилище торговых объектов.</summary>
        protected IEntityRegistry EntityRegistry
        {
            get
            {
                return ServicesRegistry.EntityRegistry;
            }
        }

        /// <summary>Хранилище маркет-данных.</summary>
        protected IStorageRegistry StorageRegistry
        {
            get
            {
                return ServicesRegistry.StorageRegistry;
            }
        }

        /// <summary>Хранилище системных идентификаторов.</summary>
        protected INativeIdStorage NativeIdStorage
        {
            get
            {
                return ServicesRegistry.NativeIdStorage;
            }
        }

        /// <summary>Провайдер бирж и торговых площадок.</summary>
        protected IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return ServicesRegistry.ExchangeInfoProvider;
            }
        }

        /// <inheritdoc />
        public void Init( Guid id )
        {
            Id = id;
            Title = this.GetDisplayName();
        }

        /// <inheritdoc />
        [Browsable( false )]
        public TaskStates State
        {
            get
            {
                return _state;
            }
            private set
            {
                if ( _state == value )
                    return;
                switch ( value )
                {
                    case TaskStates.Stopped:
                        _state = value;
                        this.AddInfoLog( LocalizedStrings.Str2190Params, value );
                        break;
                    case TaskStates.Stopping:
                        if ( _state == TaskStates.Stopped )
                            throw new InvalidOperationException( LocalizedStrings.Str2189Params.Put( _state, value ) );
                        goto case TaskStates.Stopped;
                    case TaskStates.Starting:
                        if ( _state != TaskStates.Stopped )
                            throw new InvalidOperationException( LocalizedStrings.Str2189Params.Put( _state, value ) );
                        goto case TaskStates.Stopped;
                    case TaskStates.Started:
                        if ( _state != TaskStates.Starting )
                            throw new InvalidOperationException( LocalizedStrings.Str2189Params.Put( _state, value ) );
                        goto case TaskStates.Stopped;
                    default:
                        throw new ArgumentOutOfRangeException( nameof( value ), value, LocalizedStrings.Str1219 );
                }
            }
        }

        /// <inheritdoc />
        [Browsable( false )]
        public virtual bool CanTestConnect
        {
            get
            {
                return false;
            }
        }

        public virtual void TestConnect( Action<Exception> connectionChanged )
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        [Browsable( false )]
        public IEnumerable<HydraTaskSecurity> Securities
        {
            get
            {
                return _securities;
            }
            set
            {
                IEnumerable<HydraTaskSecurity> hydraTaskSecurities = value;
                if ( hydraTaskSecurities == null )
                    throw new ArgumentNullException( nameof( value ) );
                _securities = hydraTaskSecurities;
            }
        }

        /// <inheritdoc />
        public void Start()
        {
            if ( State != TaskStates.Stopped )
                return;
            _currentErrorCount = 0;
            ( ( Action )( () =>
            {
                try
                {
                    WaitWhileActive( TimeSpan.Zero );
                    _workingSecurities.Clear();
                    _isAllSecurity = this.GetAllSecurity() != null;
                    
                    if ( !_isAllSecurity )
                    {
                        _workingSecurities.AddRange( Securities.Select( s => s.Security ) );
                    }
                        
                    
                    State = TaskStates.Starting;
                    OnStarting();
                    int num = 60;
                    
                    while ( State == TaskStates.Starting && num-- > 0 )
                    {
                        WaitWhileActive( TimeSpan.FromSeconds( 1.0 ) );
                    }
                        
                    if ( State == TaskStates.Starting )
                    {
                        this.AddErrorLog( LocalizedStrings.Str2191 );
                    }
                        
                    while ( State == TaskStates.Started )
                    {
                        try
                        {
                            TimeSpan interval = OnProcess();
                            if ( interval.TotalDays >= 20.0 )
                            {
                                Stop();
                                break;
                            }
                            _currentErrorCount = 0;
                            WaitWhileActive( interval );
                        }
                        catch ( Exception ex )
                        {
                            HandleError( ex );
                            WaitWhileActive( TimeSpan.FromSeconds( 5.0 ) );
                        }
                    }
                }
                catch ( Exception ex )
                {
                    this.AddErrorLog( ex );
                    this.AddErrorLog( LocalizedStrings.Str2192 );
                }
                finally
                {
                    FinalizeTask();
                }
            } ) ).Thread().Name( Name + " Task thread" ).Launch();
        }

        /// <summary>Обработать ошибку.</summary>
        /// <param name="error">Ошибка.</param>
        protected void HandleError( Exception error )
        {
            this.AddErrorLog( error );
            if ( MaxErrorCount == 0 || ++_currentErrorCount < MaxErrorCount )
                return;
            this.AddErrorLog( LocalizedStrings.Str2193 );
            State = TaskStates.Stopping;
        }

        /// <inheritdoc />
        public void Stop()
        {
            lock ( _syncObject )
            {
                State = TaskStates.Stopping;
                _syncObject.Pulse();
            }
        }

        /// <summary>Действие при запуске загрузки данных.</summary>
        protected virtual void OnStarting()
        {
            RaiseStarted();
        }

        /// <summary>
        /// Вызвать событие <see cref="E:StockSharp.Hydra.Core.BaseHydraTask.Started" />.
        /// </summary>
        protected void RaiseStarted()
        {
            State = TaskStates.Started;
            Action<IHydraTask> started = Started;
            if ( started == null )
                return;
            started( this );
        }

        /// <summary>
        /// Вызвать событие <see cref="E:StockSharp.Hydra.Core.BaseHydraTask.Stopped" />.
        /// </summary>
        protected void RaiseStopped()
        {
            State = TaskStates.Stopped;
            Action<IHydraTask> stopped = Stopped;
            if ( stopped == null )
                return;
            stopped( this );
        }

        /// <summary>Действие при остановке загрузки данных.</summary>
        protected virtual void OnStopped()
        {
        }

        /// <summary>Обработка окончания работы задачи.</summary>
        protected virtual void FinalizeTask()
        {
            try
            {
                OnStopped();
            }
            catch ( Exception ex )
            {
                this.AddErrorLog( ex );
            }
            RaiseStopped();
        }

        /// <summary>
        /// Можно ли продолжить работу задачи в методе <see cref="M:StockSharp.Hydra.Core.BaseHydraTask.OnProcess" />.
        /// </summary>
        /// <returns><see langword="true" />, если работу продолжить возможно, иначе, работу метода необходимо прервать.</returns>
        protected bool CanProcess()
        {
            return State == TaskStates.Started;
        }

        /// <summary>Выполнить ожидание на указанный отрезок времени.</summary>
        /// <param name="interval">Интервал.</param>
        private void WaitWhileActive( TimeSpan interval )
        {
            lock ( _syncObject )
            {
                if ( State != TaskStates.Starting && State != TaskStates.Started )
                    return;
                _syncObject.Wait( new TimeSpan?( interval ) );
            }
        }

        /// <summary>Выполнить задачу.</summary>
        /// <returns>Минимальный интервал, после окончания которого необходимо снова выполнить задачу.</returns>
        protected virtual TimeSpan OnProcess()
        {
            return Interval;
        }

        /// <inheritdoc />
        [Browsable( false )]
        public abstract IEnumerable<Messages.DataType> SupportedDataTypes { get; }

        /// <inheritdoc />
        [Browsable( false )]
        public virtual IEnumerable<Level1Fields> CandlesBuildFrom { get; } = Enumerable.Empty<Level1Fields>();

        /// <inheritdoc />
        [Browsable( false )]
        public virtual IEnumerable<int> SupportedDepths { get; } = Enumerable.Empty<int>();

        /// <inheritdoc />
        [Browsable( false )]
        public virtual SecurityLookupSupportTypes SecurityLookupSupportType
        {
            get
            {
                return SecurityLookupSupportTypes.NotSupported;
            }
        }

        /// <inheritdoc />
        public virtual bool IsAllDownloadingSupported( Messages.DataType dataType )
        {
            return false;
        }

        public virtual void Refresh(
          ISecurityStorage securityStorage,
          SecurityLookupMessage criteria,
          Action<Security> newSecurity,
          Func<bool> isCancelled )
        {
            throw new NotSupportedException();
        }

        private void SafeSave<T>(
          Security security,
          Messages.DataType dataType,
          IEnumerable<T> values,
          Func<T, DateTimeOffset> getTime,
          IEnumerable<Func<T, string>> getErrors,
          bool isWarning = true )
          where T : Message
        {
            SafeSave( security, dataType, values, getTime, getErrors, isWarning, ( s, d, f ) => ( IMarketDataStorage<T> )StorageRegistry.GetStorage( s, dataType, d, f ) );
        }

        private void SafeSave<T>( Security security, Messages.DataType dataType, IEnumerable<T> values, Func<T, DateTimeOffset> getTime, IEnumerable<Func<T, string>> getErrors, bool isWarning, Func<Security, IMarketDataDrive, StorageFormats, IMarketDataStorage<T>> getStorage )
          where T : Message
        {
            if ( dataType == null )
                throw new ArgumentNullException( nameof( dataType ) );
            if ( values == null )
                throw new ArgumentNullException( nameof( values ) );
            if ( getErrors == null )
                throw new ArgumentNullException( nameof( getErrors ) );
            List<T> list = values.ToList();
            if ( list.Count == 0 )
                return;
            getErrors = getErrors.ToArray();
            
            for ( int index = 0; index < list.Count; ++index )
            {
                T obj = list[index];
                foreach ( Func<T, string> getError in getErrors )
                {
                    string str = getError( obj );
                    if ( !str.IsEmpty() )
                    {
                        this.AddWarningLog( str );
                        if ( !isWarning )
                        {
                            list.Remove( obj );
                            --index;
                        }
                    }
                }
            }
            if ( !_isAllSecurity && !_workingSecurities.Contains( security ) )
                return;

            try
            {
                getStorage( security, Drive, StorageFormat ).Save( list );
                RaiseDataLoaded( security, dataType, list.ToArray(), getTime );
            }
            catch ( Exception ex )
            {
                this.AddErrorLog( ex );
                if ( MaxErrorCount <= 0 )
                    return;
                throw;
            }
        }

        private static string CheckStep( Decimal? priceStep, Decimal? price, string message )
        {
            int num1;
            if ( priceStep.HasValue && price.HasValue )
            {                
                if ( priceStep.HasValue && priceStep.Value > 0  ) 
                {
                    var reminder = price.Value % priceStep.Value;
                    
                    if ( reminder < priceStep )
                        return string.Empty;                    
                }
            }
            
            return message.Put( priceStep, price );
        }

        /// <summary>Сохранить тиковые сделки в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="ticks">Тиковые сделки.</param>
        protected void SaveTicks( HydraTaskSecurity security, IEnumerable<ExecutionMessage> ticks )
        {
            SaveTicks( security.Security, ticks );
        }

        /// <summary>Сохранить тиковые сделки в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="ticks">Тиковые сделки.</param>
        protected void SaveTicks( Security security, IEnumerable<ExecutionMessage> ticks )
        {
            SafeSave( security, Messages.DataType.Ticks, ticks, t => t.ServerTime, new Func<ExecutionMessage, string>[2]
            {
         t => CheckStep(security.PriceStep, t.TradePrice, LocalizedStrings.TradePriceNotMultiple),
         t => CheckStep(security.VolumeStep, t.TradeVolume, LocalizedStrings.TradeVolumeNotMultiple)
            }, true );
        }

        /// <summary>Сохранить стаканы в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="depths">Стаканы.</param>
        protected void SaveDepths( HydraTaskSecurity security, IEnumerable<QuoteChangeMessage> depths )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            SaveDepths( security.Security, depths );
        }

        /// <summary>Сохранить стаканы в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="depths">Стаканы.</param>
        protected void SaveDepths( Security security, IEnumerable<QuoteChangeMessage> depths )
        {
            depths = depths.Select( d =>
            {
                if ( d.ServerTime.IsDefault() )
                {
                    this.AddWarningLog( "got QuoteChangeMessage with empty time, replacing with prevtime/now" );
                    if ( _lastServerTime.IsDefault() )
                        _lastServerTime = DateTimeOffset.UtcNow;
                    d = d.TypedClone();
                    d.ServerTime = _lastServerTime;
                }
                _lastServerTime = d.ServerTime;
                return d;
            } );
            SafeSave( security, Messages.DataType.MarketDepth, depths, d => d.ServerTime, Enumerable.Empty<Func<QuoteChangeMessage, string>>(), true );
        }

        /// <summary>Сохранить лог заявок по инструменту в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="items">Лог заявок.</param>
        protected void SaveOrderLog( HydraTaskSecurity security, IEnumerable<ExecutionMessage> items )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            SaveOrderLog( security.Security, items );
        }

        /// <summary>Сохранить лог заявок по инструменту в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="items">Лог заявок.</param>
        protected void SaveOrderLog( Security security, IEnumerable<ExecutionMessage> items )
        {
            SafeSave( security, Messages.DataType.OrderLog, items, i => i.ServerTime, Enumerable.Empty<Func<ExecutionMessage, string>>(), true );
        }

        /// <summary>Сохранить изменения по инструменту в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="messages">Изменения.</param>
        protected void SaveLevel1Changes(
          HydraTaskSecurity security,
          IEnumerable<Level1ChangeMessage> messages )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            SaveLevel1Changes( security.Security, messages );
        }

        /// <summary>Сохранить изменения по инструменту в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="messages">Изменения.</param>
        protected void SaveLevel1Changes( Security security, IEnumerable<Level1ChangeMessage> messages )
        {
            SafeSave( security, Messages.DataType.Level1, messages, c => c.ServerTime, new Func<Level1ChangeMessage, string>[1]
            {
         m =>
        {
          if (!m.Changes.IsEmpty())
            return string.Empty;
          return LocalizedStrings.Str920;
        }
            }, false );
        }

        /// <summary>Сохранить изменения по позиции в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="messages">Изменения.</param>
        protected void SavePositionChanges(
          Security security,
          IEnumerable<PositionChangeMessage> messages )
        {
            SafeSave( security, Messages.DataType.PositionChanges, messages, c => c.ServerTime, new Func<PositionChangeMessage, string>[1]
            {
         m =>
        {
          if (!m.Changes.IsEmpty())
            return string.Empty;
          return LocalizedStrings.Str920;
        }
            }, false );
        }

        /// <summary>Сохранить свечи по инструменту в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="candles">Свечи.</param>
        protected void SaveCandles( HydraTaskSecurity security, IEnumerable<CandleMessage> candles )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            SaveCandles( security.Security, candles );
        }

        /// <summary>Сохранить свечи по инструменту в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="candles">Свечи.</param>
        protected void SaveCandles( Security security, IEnumerable<CandleMessage> candles )
        {
            candles.GroupBy( c => ( ( ISubscriptionIdMessage )c ).DataType ).ForEach( g => SafeSave( security, 
                g.Key, 
                g, 
                c => c.OpenTime, new Func<CandleMessage, string>[5]
                                            {
                                                c => CheckStep(security.PriceStep, new Decimal?(c.OpenPrice), LocalizedStrings.Str2203),
                                                c => CheckStep(security.PriceStep, new Decimal?(c.HighPrice), LocalizedStrings.Str2204),
                                                c => CheckStep(security.PriceStep, new Decimal?(c.LowPrice), LocalizedStrings.Str2205),
                                                c => CheckStep(security.PriceStep, new Decimal?(c.ClosePrice), LocalizedStrings.Str2206),
                                                c => CheckStep(security.VolumeStep, new Decimal?(c.TotalVolume), LocalizedStrings.CandleVolumeNotMultiple)
                                            }, 
                true, 
                ( s, d, c ) => StorageRegistry.GetCandleMessageStorage( g.Key.MessageType, security.ToSecurityId( null, true, false ), g.Key.Arg, d, c ) ) );
        }

        /// <summary>Сохранить новости в хранилище.</summary>
        /// <param name="news">Новости.</param>
        protected void SaveNews( IEnumerable<NewsMessage> news )
        {
            NewsMessage[ ] array = news.ToArray();
            if ( !array.Any() )
                return;
            StorageRegistry.GetNewsMessageStorage( Drive, StorageFormat ).Save( array );
            RaiseDataLoaded( null, Messages.DataType.News, array, n => n.ServerTime );
        }

        /// <summary>Сохранить состояние площадок в хранилище.</summary>
        /// <param name="states">Состояние площадок.</param>
        protected void SaveBoardStates( IEnumerable<BoardStateMessage> states )
        {
            BoardStateMessage[ ] array = states.ToArray();
            if ( !array.Any() )
                return;
            StorageRegistry.GetBoardStateMessageStorage( Drive, StorageFormat ).Save( array );
            RaiseDataLoaded( null, Messages.DataType.BoardState, array, n => n.ServerTime );
        }

        /// <summary>Сохранить транзакции в хранилище.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="transactions">Транзакции.</param>
        protected void SaveTransactions( Security security, IEnumerable<ExecutionMessage> transactions )
        {
            foreach ( var grouping in transactions.GroupBy( ( Func<ExecutionMessage, ExecutionTypes?> )( e => e.DataTypeEx.ToExecutionType() ) ) )
                SafeSave( security, Messages.DataType.Transactions, grouping, t => t.ServerTime, Enumerable.Empty<Func<ExecutionMessage, string>>(), true );
        }

        private void RaiseDataLoaded<TMessage>(
          Security security,
          Messages.DataType dataType,
          TMessage[ ] messages,
          Func<TMessage, DateTimeOffset> getTime )
          where TMessage : Message
        {
            if ( messages.Length == 0 )
                return;
            RaiseDataLoaded( security, dataType, new DateTimeOffset?( getTime( messages.Last() ) ), messages.Length, messages );
        }

        private void RaiseDataLoaded(
          Security security,
          Messages.DataType dataType,
          DateTimeOffset? time,
          int count,
          IEnumerable<Message> messages )
        {
            if ( security == null )
                this.AddInfoLog( LocalizedStrings.Str2207Params, count, dataType );
            else
                this.AddInfoLog( LocalizedStrings.Str2208Params, security.Id, count, dataType );
            Action<Security, Messages.DataType, DateTimeOffset?, int, IEnumerable<Message>> dataLoaded = DataLoaded;
            if ( dataLoaded == null )
                return;
            dataLoaded( security, dataType, time, count, messages );
        }

        /// <summary>
        /// Вызывать событие <see cref="E:StockSharp.Hydra.Core.BaseHydraTask.DataLoaded" />.
        /// </summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="dataType">Тип данных.</param>
        /// <param name="time">Время последних данных.</param>
        /// <param name="count">Количество последних данных.</param>
        protected void RaiseDataLoaded(
          Security security,
          Messages.DataType dataType,
          DateTimeOffset? time,
          int count )
        {
            RaiseDataLoaded( security, dataType, time, count, Enumerable.Empty<Message>() );
        }

        /// <inheritdoc />
        public event Action<Security, Messages.DataType, DateTimeOffset?, int, IEnumerable<Message>> DataLoaded;

        /// <summary>
        /// Invoke <see cref="E:StockSharp.Hydra.Core.BaseHydraTask.PropertyChanged" /> event.
        /// </summary>
        /// <param name="name">Property name/</param>
        protected void NotifyPropertyChanged( [CallerMemberName] string name = null )
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged.Invoke( this, name );
        }

        /// <summary>
        /// Получить список инструментов, с которыми будет работать данный источник.
        /// </summary>
        /// <returns>Инструменты.</returns>
        protected IEnumerable<HydraTaskSecurity> GetWorkingSecurities()
        {
            if ( this.GetAllSecurity() != null )
                return this.ToHydraSecurities( EntityRegistry.Securities.Where( s => !s.IsAllSecurity() ) );
            return Securities;
        }

        /// <summary>Получить инструмент по идентификатору.</summary>
        /// <param name="securityId">Идентификатор инструмента.</param>
        /// <returns>Инструмент.</returns>
        protected Security GetSecurity( SecurityId securityId )
        {
            Security security = EntityRegistry.Securities.LookupById( securityId );
            if ( security == null )
            {
                security = new Security()
                {
                    Id = securityId.ToStringId( null, false ),
                    Code = securityId.SecurityCode,
                    Board = ExchangeInfoProvider.GetOrCreateBoard( securityId.BoardCode, null ),
                    ExternalId = securityId.ToExternalId()
                };
                SaveSecurity( security );
            }
            return security;
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Add( "Title", Title );
            storage.Add( "IsDefault", IsDefault );
            storage.Add( "IsEnabled", IsEnabled );
            storage.Add( "Interval", Interval );
            storage.Add( "WorkingTime", WorkingTime.Save() );
            storage.Add( "SupportedLevel1Fields", SupportedLevel1Fields.Select( s => s.To<string>() ).ToArray() );
            storage.Add( "MaxErrorCount", MaxErrorCount );
            if ( DependFrom != null )
                storage.Add( "DependFrom", DependFrom.Id );
            storage.Add( "StorageFormat", StorageFormat );
            if ( Drive == ServicesRegistry.DriveCache.DefaultDrive )
                return;
            storage.Add( "Drive", Drive.Path );
        }

        /// <inheritdoc />
        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Title = storage.GetValue( "Title", Title );
            IsDefault = storage.GetValue( "IsDefault", IsDefault );
            IsEnabled = storage.GetValue( "IsEnabled", IsEnabled );
            Interval = storage.GetValue( "Interval", Interval );
            if ( storage.ContainsKey( "WorkingTime" ) )
                WorkingTime.Load( storage.GetValue( "WorkingTime", ( SettingsStorage )null ) );
            if ( storage.ContainsKey( "SupportedLevel1Fields" ) )
            {
                _supportedLevel1Fields.Clear();
                _supportedLevel1Fields.AddRange( storage.GetValue( "SupportedLevel1Fields", ( IEnumerable<string> )null ).Select( t => t.To<Level1Fields>() ) );
            }
            MaxErrorCount = storage.GetValue( "MaxErrorCount", MaxErrorCount );
            if ( storage.ContainsKey( "DependFrom" ) )
            {
                Guid dependFromId = storage.GetValue( "DependFrom", new Guid() );
                DependFrom = HydraTaskManager.Instance.Tasks.FirstOrDefault( t => t.Id == dependFromId );
            }
            StorageFormat = storage.GetValue( "StorageFormat", StorageFormat );
            if ( !storage.ContainsKey( "Drive" ) )
                return;
            Drive = ServicesRegistry.DriveCache.GetDrive( ( string )storage["Drive"] );
        }

        /// <summary>Создать копию.</summary>
        /// <returns>Копия.</returns>
        public IHydraTask Clone()
        {
            IHydraTask instance = GetType().CreateInstance<IHydraTask>();
            instance.Init( Id );
            instance.Load( this.Save() );
            instance.Parent = this;
            return instance;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
