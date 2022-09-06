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
    [Display(Description = "Str2230", GroupName = "General", Name = "Str2229", Order = 0, ResourceType = typeof (LocalizedStrings))]
    [Browsable(false)]
    public bool IsEnabled
    {
      get
      {
        return this._isEnabled;
      }
      set
      {
        if (this._isEnabled == value)
          return;
        this._isEnabled = value;
        this.NotifyPropertyChanged(nameof (IsEnabled));
      }
    }

    bool IScheduledTask.CanStart
    {
      get
      {
        if (this.IsEnabled)
          return this.State == TaskStates.Stopped;
        return false;
      }
    }

    bool IScheduledTask.CanStop
    {
      get
      {
        return this.State == TaskStates.Started;
      }
    }

    /// <inheritdoc />
    [Display(Description = "WorkingHours", GroupName = "General", Name = "WorkingTime", Order = 1, ResourceType = typeof (LocalizedStrings))]
    public WorkingTime WorkingTime
    {
      get
      {
        return this._workingTime;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (this._workingTime == value)
          return;
        this._workingTime = value;
        this.NotifyPropertyChanged(nameof (WorkingTime));
      }
    }

    /// <summary>Интервал работы.</summary>
    [Display(Description = "Str2235Dot", GroupName = "General", Name = "Str2235", Order = 3, ResourceType = typeof (LocalizedStrings))]
    [TimeSpanEditor(Mask = TimeSpanEditorMask.Days | TimeSpanEditorMask.Hours | TimeSpanEditorMask.Minutes | TimeSpanEditorMask.Seconds)]
    public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(5.0);

    /// <inheritdoc />
    [Display(Description = "Str2238", GroupName = "General", Name = "Str2237", Order = 4, ResourceType = typeof (LocalizedStrings))]
    public IMarketDataDrive Drive
    {
      get
      {
        return this._drive;
      }
      set
      {
        IMarketDataDrive marketDataDrive = value;
        if (marketDataDrive == null)
          throw new ArgumentNullException(nameof (value));
        this._drive = marketDataDrive;
      }
    }

    /// <inheritdoc />
    [Display(Description = "Str2240", GroupName = "General", Name = "Str2239", Order = 5, ResourceType = typeof (LocalizedStrings))]
    public StorageFormats StorageFormat { get; set; }

    /// <inheritdoc />
    [Display(Description = "Str2242", GroupName = "General", Name = "Str2241", Order = 10, ResourceType = typeof (LocalizedStrings))]
    public IHydraTask DependFrom { get; set; }

    /// <summary>
    /// Максимальное количество ошибок, после которого задача будет остановлена.
    /// По-умолчанию равно 0, что означает игнорирование количества ошибок.
    /// </summary>
    [Display(Description = "Str2245", GroupName = "General", Name = "Str2244", Order = 6, ResourceType = typeof (LocalizedStrings))]
    public int MaxErrorCount { get; set; }

    /// <summary>Поддерживаемые поля маркет-данных первого уровня.</summary>
    [Browsable(false)]
    [Display(Description = "Str2247", GroupName = "General", Name = "Str2246", Order = 7, ResourceType = typeof (LocalizedStrings))]
    [ItemsSource(typeof (Level1Fields))]
    [EditorExtension(ShowSelectedItemsCount = true)]
    public virtual IEnumerable<Level1Fields> SupportedLevel1Fields
    {
      get
      {
        return (IEnumerable<Level1Fields>) this._supportedLevel1Fields.Cache;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        lock (this._supportedLevel1Fields.SyncRoot)
        {
          this._supportedLevel1Fields.Clear();
          this._supportedLevel1Fields.AddRange(value);
        }
      }
    }

    /// <inheritdoc />
    [Browsable(false)]
    public bool IsDefault { get; set; } = true;

    /// <inheritdoc />
    [Display(Description = "Str2248", GroupName = "General", Name = "Str215", Order = 0, ResourceType = typeof (LocalizedStrings))]
    public string Title
    {
      get
      {
        return this._title;
      }
      set
      {
        if (this._title == value)
          return;
        if (value.IsEmpty())
          value = this.GetDisplayName();
        this._title = value;
        this.NotifyPropertyChanged(nameof (Title));
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
    protected void SaveSecurity(Security security)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      if (this.EntityRegistry.Securities.LookupById(security.ToSecurityId((SecurityIdGenerator) null, true, false)) != null)
        return;
      this.EntityRegistry.Securities.Save(security, false);
      this.AddInfoLog(LocalizedStrings.Str2188Params, (object) security);
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override string Name
    {
      get
      {
        if (!this.Title.IsEmpty())
          return this.Title;
        return this.GetDisplayName();
      }
    }

    /// <inheritdoc />
    [Browsable(false)]
    public Uri Icon
    {
      get
      {
        return this.GetType().GetIcon();
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
    public void Init(Guid id)
    {
      this.Id = id;
      this.Title = this.GetDisplayName();
    }

    /// <inheritdoc />
    [Browsable(false)]
    public TaskStates State
    {
      get
      {
        return this._state;
      }
      private set
      {
        if (this._state == value)
          return;
        switch (value)
        {
          case TaskStates.Stopped:
            this._state = value;
            this.AddInfoLog(LocalizedStrings.Str2190Params, (object) value);
            break;
          case TaskStates.Stopping:
            if (this._state == TaskStates.Stopped)
              throw new InvalidOperationException(LocalizedStrings.Str2189Params.Put((object) this._state, (object) value));
            goto case TaskStates.Stopped;
          case TaskStates.Starting:
            if (this._state != TaskStates.Stopped)
              throw new InvalidOperationException(LocalizedStrings.Str2189Params.Put((object) this._state, (object) value));
            goto case TaskStates.Stopped;
          case TaskStates.Started:
            if (this._state != TaskStates.Starting)
              throw new InvalidOperationException(LocalizedStrings.Str2189Params.Put((object) this._state, (object) value));
            goto case TaskStates.Stopped;
          default:
            throw new ArgumentOutOfRangeException(nameof (value), (object) value, LocalizedStrings.Str1219);
        }
      }
    }

    /// <inheritdoc />
    [Browsable(false)]
    public virtual bool CanTestConnect
    {
      get
      {
        return false;
      }
    }

    public virtual void TestConnect(Action<Exception> connectionChanged)
    {
      throw new NotSupportedException();
    }

    /// <inheritdoc />
    [Browsable(false)]
    public IEnumerable<HydraTaskSecurity> Securities
    {
      get
      {
        return this._securities;
      }
      set
      {
        IEnumerable<HydraTaskSecurity> hydraTaskSecurities = value;
        if (hydraTaskSecurities == null)
          throw new ArgumentNullException(nameof (value));
        this._securities = hydraTaskSecurities;
      }
    }

    /// <inheritdoc />
    public void Start()
    {
      if (this.State != TaskStates.Stopped)
        return;
      this._currentErrorCount = 0;
      ((Action) (() =>
      {
        try
        {
          this.WaitWhileActive(TimeSpan.Zero);
          this._workingSecurities.Clear();
          this._isAllSecurity = this.GetAllSecurity() != null;
          if (!this._isAllSecurity)
            this._workingSecurities.AddRange(this.Securities.Select<HydraTaskSecurity, Security>((Func<HydraTaskSecurity, Security>) (s => s.Security)));
          this.State = TaskStates.Starting;
          this.OnStarting();
          int num = 60;
          while (this.State == TaskStates.Starting && num-- > 0)
            this.WaitWhileActive(TimeSpan.FromSeconds(1.0));
          if (this.State == TaskStates.Starting)
            this.AddErrorLog(LocalizedStrings.Str2191);
          while (this.State == TaskStates.Started)
          {
            try
            {
              TimeSpan interval = this.OnProcess();
              if (interval.TotalDays >= 20.0)
              {
                this.Stop();
                break;
              }
              this._currentErrorCount = 0;
              this.WaitWhileActive(interval);
            }
            catch (Exception ex)
            {
              this.HandleError(ex);
              this.WaitWhileActive(TimeSpan.FromSeconds(5.0));
            }
          }
        }
        catch (Exception ex)
        {
          this.AddErrorLog(ex);
          this.AddErrorLog(LocalizedStrings.Str2192);
        }
        finally
        {
          this.FinalizeTask();
        }
      })).Thread().Name(this.Name + " Task thread").Launch();
    }

    /// <summary>Обработать ошибку.</summary>
    /// <param name="error">Ошибка.</param>
    protected void HandleError(Exception error)
    {
      this.AddErrorLog(error);
      if (this.MaxErrorCount == 0 || ++this._currentErrorCount < this.MaxErrorCount)
        return;
      this.AddErrorLog(LocalizedStrings.Str2193);
      this.State = TaskStates.Stopping;
    }

    /// <inheritdoc />
    public void Stop()
    {
      lock (this._syncObject)
      {
        this.State = TaskStates.Stopping;
        this._syncObject.Pulse();
      }
    }

    /// <summary>Действие при запуске загрузки данных.</summary>
    protected virtual void OnStarting()
    {
      this.RaiseStarted();
    }

    /// <summary>
    /// Вызвать событие <see cref="E:StockSharp.Hydra.Core.BaseHydraTask.Started" />.
    /// </summary>
    protected void RaiseStarted()
    {
      this.State = TaskStates.Started;
      Action<IHydraTask> started = this.Started;
      if (started == null)
        return;
      started((IHydraTask) this);
    }

    /// <summary>
    /// Вызвать событие <see cref="E:StockSharp.Hydra.Core.BaseHydraTask.Stopped" />.
    /// </summary>
    protected void RaiseStopped()
    {
      this.State = TaskStates.Stopped;
      Action<IHydraTask> stopped = this.Stopped;
      if (stopped == null)
        return;
      stopped((IHydraTask) this);
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
        this.OnStopped();
      }
      catch (Exception ex)
      {
        this.AddErrorLog(ex);
      }
      this.RaiseStopped();
    }

    /// <summary>
    /// Можно ли продолжить работу задачи в методе <see cref="M:StockSharp.Hydra.Core.BaseHydraTask.OnProcess" />.
    /// </summary>
    /// <returns><see langword="true" />, если работу продолжить возможно, иначе, работу метода необходимо прервать.</returns>
    protected bool CanProcess()
    {
      return this.State == TaskStates.Started;
    }

    /// <summary>Выполнить ожидание на указанный отрезок времени.</summary>
    /// <param name="interval">Интервал.</param>
    private void WaitWhileActive(TimeSpan interval)
    {
      lock (this._syncObject)
      {
        if (this.State != TaskStates.Starting && this.State != TaskStates.Started)
          return;
        this._syncObject.Wait(new TimeSpan?(interval));
      }
    }

    /// <summary>Выполнить задачу.</summary>
    /// <returns>Минимальный интервал, после окончания которого необходимо снова выполнить задачу.</returns>
    protected virtual TimeSpan OnProcess()
    {
      return this.Interval;
    }

    /// <inheritdoc />
    [Browsable(false)]
    public abstract IEnumerable<StockSharp.Messages.DataType> SupportedDataTypes { get; }

    /// <inheritdoc />
    [Browsable(false)]
    public virtual IEnumerable<Level1Fields> CandlesBuildFrom { get; } = Enumerable.Empty<Level1Fields>();

    /// <inheritdoc />
    [Browsable(false)]
    public virtual IEnumerable<int> SupportedDepths { get; } = Enumerable.Empty<int>();

    /// <inheritdoc />
    [Browsable(false)]
    public virtual SecurityLookupSupportTypes SecurityLookupSupportType
    {
      get
      {
        return SecurityLookupSupportTypes.NotSupported;
      }
    }

    /// <inheritdoc />
    public virtual bool IsAllDownloadingSupported(StockSharp.Messages.DataType dataType)
    {
      return false;
    }

    public virtual void Refresh(
      ISecurityStorage securityStorage,
      SecurityLookupMessage criteria,
      Action<Security> newSecurity,
      Func<bool> isCancelled)
    {
      throw new NotSupportedException();
    }

    private void SafeSave<T>(
      Security security,
      StockSharp.Messages.DataType dataType,
      IEnumerable<T> values,
      Func<T, DateTimeOffset> getTime,
      IEnumerable<Func<T, string>> getErrors,
      bool isWarning = true)
      where T : Message
    {
      this.SafeSave<T>(security, dataType, values, getTime, getErrors, isWarning, (Func<Security, IMarketDataDrive, StorageFormats, IMarketDataStorage<T>>) ((s, d, f) => (IMarketDataStorage<T>) this.StorageRegistry.GetStorage(s, dataType, d, f)));
    }

    private void SafeSave<T>(
      Security security,
      StockSharp.Messages.DataType dataType,
      IEnumerable<T> values,
      Func<T, DateTimeOffset> getTime,
      IEnumerable<Func<T, string>> getErrors,
      bool isWarning,
      Func<Security, IMarketDataDrive, StorageFormats, IMarketDataStorage<T>> getStorage)
      where T : Message
    {
      if ((Equatable<StockSharp.Messages.DataType>) dataType == (StockSharp.Messages.DataType) null)
        throw new ArgumentNullException(nameof (dataType));
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      if (getErrors == null)
        throw new ArgumentNullException(nameof (getErrors));
      List<T> list = values.ToList<T>();
      if (list.Count == 0)
        return;
      getErrors = (IEnumerable<Func<T, string>>) getErrors.ToArray<Func<T, string>>();
      for (int index = 0; index < list.Count; ++index)
      {
        T obj = list[index];
        foreach (Func<T, string> getError in getErrors)
        {
          string str = getError(obj);
          if (!str.IsEmpty())
          {
            this.AddWarningLog(str);
            if (!isWarning)
            {
              list.Remove(obj);
              --index;
            }
          }
        }
      }
      if (!this._isAllSecurity && !this._workingSecurities.Contains(security))
        return;
      try
      {
        getStorage(security, this.Drive, this.StorageFormat).Save((IEnumerable<T>) list);
        this.RaiseDataLoaded<T>(security, dataType, list.ToArray(), getTime);
      }
      catch (Exception ex)
      {
        this.AddErrorLog(ex);
        if (this.MaxErrorCount <= 0)
          return;
        throw;
      }
    }

    private static string CheckStep(Decimal? priceStep, Decimal? price, string message)
    {
      int num1;
      if (priceStep.HasValue && price.HasValue)
      {
        Decimal? nullable1 = priceStep;
        Decimal num2 = new Decimal();
        if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
        {
          Decimal? nullable2 = price;
          Decimal? nullable3 = priceStep;
          Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() % nullable3.GetValueOrDefault()) : new Decimal?();
          Decimal num3 = new Decimal();
          num1 = !(nullable4.GetValueOrDefault() == num3 & nullable4.HasValue) ? 1 : 0;
          goto label_4;
        }
      }
      num1 = 0;
label_4:
      if (num1 == 0)
        return string.Empty;
      return message.Put((object) priceStep, (object) price);
    }

    /// <summary>Сохранить тиковые сделки в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="ticks">Тиковые сделки.</param>
    protected void SaveTicks(HydraTaskSecurity security, IEnumerable<ExecutionMessage> ticks)
    {
      this.SaveTicks(security.Security, ticks);
    }

    /// <summary>Сохранить тиковые сделки в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="ticks">Тиковые сделки.</param>
    protected void SaveTicks(Security security, IEnumerable<ExecutionMessage> ticks)
    {
      this.SafeSave<ExecutionMessage>(security, StockSharp.Messages.DataType.Ticks, ticks, (Func<ExecutionMessage, DateTimeOffset>) (t => t.ServerTime), (IEnumerable<Func<ExecutionMessage, string>>) new Func<ExecutionMessage, string>[2]
      {
        (Func<ExecutionMessage, string>) (t => BaseHydraTask.CheckStep(security.PriceStep, t.TradePrice, LocalizedStrings.TradePriceNotMultiple)),
        (Func<ExecutionMessage, string>) (t => BaseHydraTask.CheckStep(security.VolumeStep, t.TradeVolume, LocalizedStrings.TradeVolumeNotMultiple))
      }, true);
    }

    /// <summary>Сохранить стаканы в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="depths">Стаканы.</param>
    protected void SaveDepths(HydraTaskSecurity security, IEnumerable<QuoteChangeMessage> depths)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      this.SaveDepths(security.Security, depths);
    }

    /// <summary>Сохранить стаканы в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="depths">Стаканы.</param>
    protected void SaveDepths(Security security, IEnumerable<QuoteChangeMessage> depths)
    {
      depths = depths.Select<QuoteChangeMessage, QuoteChangeMessage>((Func<QuoteChangeMessage, QuoteChangeMessage>) (d =>
      {
        if (d.ServerTime.IsDefault<DateTimeOffset>())
        {
          this.AddWarningLog("got QuoteChangeMessage with empty time, replacing with prevtime/now");
          if (this._lastServerTime.IsDefault<DateTimeOffset>())
            this._lastServerTime = DateTimeOffset.UtcNow;
          d = d.TypedClone<QuoteChangeMessage>();
          d.ServerTime = this._lastServerTime;
        }
        this._lastServerTime = d.ServerTime;
        return d;
      }));
      this.SafeSave<QuoteChangeMessage>(security, StockSharp.Messages.DataType.MarketDepth, depths, (Func<QuoteChangeMessage, DateTimeOffset>) (d => d.ServerTime), Enumerable.Empty<Func<QuoteChangeMessage, string>>(), true);
    }

    /// <summary>Сохранить лог заявок по инструменту в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="items">Лог заявок.</param>
    protected void SaveOrderLog(HydraTaskSecurity security, IEnumerable<ExecutionMessage> items)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      this.SaveOrderLog(security.Security, items);
    }

    /// <summary>Сохранить лог заявок по инструменту в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="items">Лог заявок.</param>
    protected void SaveOrderLog(Security security, IEnumerable<ExecutionMessage> items)
    {
      this.SafeSave<ExecutionMessage>(security, StockSharp.Messages.DataType.OrderLog, items, (Func<ExecutionMessage, DateTimeOffset>) (i => i.ServerTime), Enumerable.Empty<Func<ExecutionMessage, string>>(), true);
    }

    /// <summary>Сохранить изменения по инструменту в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="messages">Изменения.</param>
    protected void SaveLevel1Changes(
      HydraTaskSecurity security,
      IEnumerable<Level1ChangeMessage> messages)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      this.SaveLevel1Changes(security.Security, messages);
    }

    /// <summary>Сохранить изменения по инструменту в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="messages">Изменения.</param>
    protected void SaveLevel1Changes(Security security, IEnumerable<Level1ChangeMessage> messages)
    {
      this.SafeSave<Level1ChangeMessage>(security, StockSharp.Messages.DataType.Level1, messages, (Func<Level1ChangeMessage, DateTimeOffset>) (c => c.ServerTime), (IEnumerable<Func<Level1ChangeMessage, string>>) new Func<Level1ChangeMessage, string>[1]
      {
        (Func<Level1ChangeMessage, string>) (m =>
        {
          if (!m.Changes.IsEmpty<KeyValuePair<Level1Fields, object>>())
            return string.Empty;
          return LocalizedStrings.Str920;
        })
      }, false);
    }

    /// <summary>Сохранить изменения по позиции в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="messages">Изменения.</param>
    protected void SavePositionChanges(
      Security security,
      IEnumerable<PositionChangeMessage> messages)
    {
      this.SafeSave<PositionChangeMessage>(security, StockSharp.Messages.DataType.PositionChanges, messages, (Func<PositionChangeMessage, DateTimeOffset>) (c => c.ServerTime), (IEnumerable<Func<PositionChangeMessage, string>>) new Func<PositionChangeMessage, string>[1]
      {
        (Func<PositionChangeMessage, string>) (m =>
        {
          if (!m.Changes.IsEmpty<KeyValuePair<PositionChangeTypes, object>>())
            return string.Empty;
          return LocalizedStrings.Str920;
        })
      }, false);
    }

    /// <summary>Сохранить свечи по инструменту в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="candles">Свечи.</param>
    protected void SaveCandles(HydraTaskSecurity security, IEnumerable<CandleMessage> candles)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      this.SaveCandles(security.Security, candles);
    }

    /// <summary>Сохранить свечи по инструменту в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="candles">Свечи.</param>
    protected void SaveCandles(Security security, IEnumerable<CandleMessage> candles)
    {
      candles.GroupBy<CandleMessage, StockSharp.Messages.DataType>((Func<CandleMessage, StockSharp.Messages.DataType>) (c => ((ISubscriptionIdMessage) c).DataType)).ForEach<IGrouping<StockSharp.Messages.DataType, CandleMessage>>((Action<IGrouping<StockSharp.Messages.DataType, CandleMessage>>) (g => this.SafeSave<CandleMessage>(security, g.Key, (IEnumerable<CandleMessage>) g, (Func<CandleMessage, DateTimeOffset>) (c => c.OpenTime), (IEnumerable<Func<CandleMessage, string>>) new Func<CandleMessage, string>[5]
      {
        (Func<CandleMessage, string>) (c => BaseHydraTask.CheckStep(security.PriceStep, new Decimal?(c.OpenPrice), LocalizedStrings.Str2203)),
        (Func<CandleMessage, string>) (c => BaseHydraTask.CheckStep(security.PriceStep, new Decimal?(c.HighPrice), LocalizedStrings.Str2204)),
        (Func<CandleMessage, string>) (c => BaseHydraTask.CheckStep(security.PriceStep, new Decimal?(c.LowPrice), LocalizedStrings.Str2205)),
        (Func<CandleMessage, string>) (c => BaseHydraTask.CheckStep(security.PriceStep, new Decimal?(c.ClosePrice), LocalizedStrings.Str2206)),
        (Func<CandleMessage, string>) (c => BaseHydraTask.CheckStep(security.VolumeStep, new Decimal?(c.TotalVolume), LocalizedStrings.CandleVolumeNotMultiple))
      }, true, (Func<Security, IMarketDataDrive, StorageFormats, IMarketDataStorage<CandleMessage>>) ((s, d, c) => this.StorageRegistry.GetCandleMessageStorage(g.Key.MessageType, security.ToSecurityId((SecurityIdGenerator) null, true, false), g.Key.Arg, d, c)))));
    }

    /// <summary>Сохранить новости в хранилище.</summary>
    /// <param name="news">Новости.</param>
    protected void SaveNews(IEnumerable<NewsMessage> news)
    {
      NewsMessage[] array = news.ToArray<NewsMessage>();
      if (!((IEnumerable<NewsMessage>) array).Any<NewsMessage>())
        return;
      this.StorageRegistry.GetNewsMessageStorage(this.Drive, this.StorageFormat).Save((IEnumerable<NewsMessage>) array);
      this.RaiseDataLoaded<NewsMessage>((Security) null, StockSharp.Messages.DataType.News, array, (Func<NewsMessage, DateTimeOffset>) (n => n.ServerTime));
    }

    /// <summary>Сохранить состояние площадок в хранилище.</summary>
    /// <param name="states">Состояние площадок.</param>
    protected void SaveBoardStates(IEnumerable<BoardStateMessage> states)
    {
      BoardStateMessage[] array = states.ToArray<BoardStateMessage>();
      if (!((IEnumerable<BoardStateMessage>) array).Any<BoardStateMessage>())
        return;
      this.StorageRegistry.GetBoardStateMessageStorage(this.Drive, this.StorageFormat).Save((IEnumerable<BoardStateMessage>) array);
      this.RaiseDataLoaded<BoardStateMessage>((Security) null, StockSharp.Messages.DataType.BoardState, array, (Func<BoardStateMessage, DateTimeOffset>) (n => n.ServerTime));
    }

    /// <summary>Сохранить транзакции в хранилище.</summary>
    /// <param name="security">Инструмент.</param>
    /// <param name="transactions">Транзакции.</param>
    protected void SaveTransactions(Security security, IEnumerable<ExecutionMessage> transactions)
    {
      foreach (IGrouping<ExecutionTypes?, ExecutionMessage> grouping in transactions.GroupBy<ExecutionMessage, ExecutionTypes?>((Func<ExecutionMessage, ExecutionTypes?>) (e => e.ExecutionType)))
        this.SafeSave<ExecutionMessage>(security, StockSharp.Messages.DataType.Transactions, (IEnumerable<ExecutionMessage>) grouping, (Func<ExecutionMessage, DateTimeOffset>) (t => t.ServerTime), Enumerable.Empty<Func<ExecutionMessage, string>>(), true);
    }

    private void RaiseDataLoaded<TMessage>(
      Security security,
      StockSharp.Messages.DataType dataType,
      TMessage[] messages,
      Func<TMessage, DateTimeOffset> getTime)
      where TMessage : Message
    {
      if (messages.Length == 0)
        return;
      this.RaiseDataLoaded(security, dataType, new DateTimeOffset?(getTime(((IEnumerable<TMessage>) messages).Last<TMessage>())), messages.Length, (IEnumerable<Message>) messages);
    }

    private void RaiseDataLoaded(
      Security security,
      StockSharp.Messages.DataType dataType,
      DateTimeOffset? time,
      int count,
      IEnumerable<Message> messages)
    {
      if (security == null)
        this.AddInfoLog(LocalizedStrings.Str2207Params, (object) count, (object) dataType);
      else
        this.AddInfoLog(LocalizedStrings.Str2208Params, (object) security.Id, (object) count, (object) dataType);
      Action<Security, StockSharp.Messages.DataType, DateTimeOffset?, int, IEnumerable<Message>> dataLoaded = this.DataLoaded;
      if (dataLoaded == null)
        return;
      dataLoaded(security, dataType, time, count, messages);
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
      StockSharp.Messages.DataType dataType,
      DateTimeOffset? time,
      int count)
    {
      this.RaiseDataLoaded(security, dataType, time, count, Enumerable.Empty<Message>());
    }

    /// <inheritdoc />
    public event Action<Security, StockSharp.Messages.DataType, DateTimeOffset?, int, IEnumerable<Message>> DataLoaded;

    /// <summary>
    /// Invoke <see cref="E:StockSharp.Hydra.Core.BaseHydraTask.PropertyChanged" /> event.
    /// </summary>
    /// <param name="name">Property name/</param>
    protected void NotifyPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged.Invoke((object) this, name);
    }

    /// <summary>
    /// Получить список инструментов, с которыми будет работать данный источник.
    /// </summary>
    /// <returns>Инструменты.</returns>
    protected IEnumerable<HydraTaskSecurity> GetWorkingSecurities()
    {
      if (this.GetAllSecurity() != null)
        return this.ToHydraSecurities(this.EntityRegistry.Securities.Where<Security>((Func<Security, bool>) (s => !s.IsAllSecurity())));
      return this.Securities;
    }

    /// <summary>Получить инструмент по идентификатору.</summary>
    /// <param name="securityId">Идентификатор инструмента.</param>
    /// <returns>Инструмент.</returns>
    protected Security GetSecurity(SecurityId securityId)
    {
      Security security = this.EntityRegistry.Securities.LookupById(securityId);
      if (security == null)
      {
        security = new Security()
        {
          Id = securityId.ToStringId((SecurityIdGenerator) null, false),
          Code = securityId.SecurityCode,
          Board = this.ExchangeInfoProvider.GetOrCreateBoard(securityId.BoardCode, (Func<string, ExchangeBoard>) null),
          ExternalId = securityId.ToExternalId()
        };
        this.SaveSecurity(security);
      }
      return security;
    }

    /// <inheritdoc />
    public event PropertyChangedEventHandler PropertyChanged;

    /// <inheritdoc />
    public override void Save(SettingsStorage storage)
    {
      base.Save(storage);
      storage.Add("Title", (object) this.Title);
      storage.Add("IsDefault", (object) this.IsDefault);
      storage.Add("IsEnabled", (object) this.IsEnabled);
      storage.Add("Interval", (object) this.Interval);
      storage.Add("WorkingTime", (object) this.WorkingTime.Save());
      storage.Add("SupportedLevel1Fields", (object) this.SupportedLevel1Fields.Select<Level1Fields, string>((Func<Level1Fields, string>) (s => s.To<string>())).ToArray<string>());
      storage.Add("MaxErrorCount", (object) this.MaxErrorCount);
      if (this.DependFrom != null)
        storage.Add("DependFrom", (object) this.DependFrom.Id);
      storage.Add("StorageFormat", (object) this.StorageFormat);
      if (this.Drive == ServicesRegistry.DriveCache.DefaultDrive)
        return;
      storage.Add("Drive", (object) this.Drive.Path);
    }

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
      base.Load(storage);
      this.Title = storage.GetValue<string>("Title", this.Title);
      this.IsDefault = storage.GetValue<bool>("IsDefault", this.IsDefault);
      this.IsEnabled = storage.GetValue<bool>("IsEnabled", this.IsEnabled);
      this.Interval = storage.GetValue<TimeSpan>("Interval", this.Interval);
      if (storage.ContainsKey("WorkingTime"))
        this.WorkingTime.Load(storage.GetValue<SettingsStorage>("WorkingTime", (SettingsStorage) null));
      if (storage.ContainsKey("SupportedLevel1Fields"))
      {
        this._supportedLevel1Fields.Clear();
        this._supportedLevel1Fields.AddRange(storage.GetValue<IEnumerable<string>>("SupportedLevel1Fields", (IEnumerable<string>) null).Select<string, Level1Fields>((Func<string, Level1Fields>) (t => t.To<Level1Fields>())));
      }
      this.MaxErrorCount = storage.GetValue<int>("MaxErrorCount", this.MaxErrorCount);
      if (storage.ContainsKey("DependFrom"))
      {
        Guid dependFromId = storage.GetValue<Guid>("DependFrom", new Guid());
        this.DependFrom = HydraTaskManager.Instance.Tasks.FirstOrDefault<IHydraTask>((Func<IHydraTask, bool>) (t => t.Id == dependFromId));
      }
      this.StorageFormat = storage.GetValue<StorageFormats>("StorageFormat", this.StorageFormat);
      if (!storage.ContainsKey("Drive"))
        return;
      this.Drive = ServicesRegistry.DriveCache.GetDrive((string) storage["Drive"]);
    }

    /// <summary>Создать копию.</summary>
    /// <returns>Копия.</returns>
    public IHydraTask Clone()
    {
      IHydraTask instance = this.GetType().CreateInstance<IHydraTask>();
      instance.Init(this.Id);
      instance.Load(this.Save());
      instance.Parent = (ILogSource) this;
      return instance;
    }

    object ICloneable.Clone()
    {
      return (object) this.Clone();
    }
  }
}
