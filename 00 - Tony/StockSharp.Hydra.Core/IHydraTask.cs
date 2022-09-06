// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.IHydraTask
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StockSharp.Hydra.Core
{
  /// <summary>Интерфейс, описывающий задачу.</summary>
  public interface IHydraTask : ILogReceiver, ILogSource, IDisposable, ICloneable<IHydraTask>, ICloneable, IPersistable, INotifyPropertyChanged, IScheduledTask
  {
    /// <summary>Заголовок задачи.</summary>
    string Title { get; }

    /// <summary>Адрес иконки, для визуального обозначения.</summary>
    Uri Icon { get; }

    /// <summary>Включена ли задача.</summary>
    bool IsEnabled { get; set; }

    /// <summary>Настройки содержат значений, заданные по-умолчанию.</summary>
    bool IsDefault { get; set; }

    /// <summary>Формат данных.</summary>
    StorageFormats StorageFormat { get; }

    /// <summary>
    /// Директория с данными, куда будут сохраняться конечные файлы в формате StockSharp.
    /// </summary>
    IMarketDataDrive Drive { get; }

    /// <summary>
    /// Задача, которая должна быть выполнена перед запуском текущей.
    /// </summary>
    IHydraTask DependFrom { get; set; }

    /// <summary>Инструменты, связанные с задачей.</summary>
    IEnumerable<HydraTaskSecurity> Securities { get; set; }

    /// <summary>Инициализировать задачу.</summary>
    /// <param name="id">Идентификатор задачи.</param>
    void Init(Guid id);

    /// <summary>Запустить.</summary>
    void Start();

    /// <summary>Остановить.</summary>
    void Stop();

    /// <summary>
    /// Is for the specified <paramref name="dataType" /> all securities downloading enabled.
    /// </summary>
    /// <param name="dataType">Data type info.</param>
    /// <returns>Check result.</returns>
    bool IsAllDownloadingSupported(DataType dataType);

    /// <summary>Поддерживаемые типы данных.</summary>
    IEnumerable<DataType> SupportedDataTypes { get; }

    /// <summary>Поддерживаемые источники данных построения свечей.</summary>
    IEnumerable<Level1Fields> CandlesBuildFrom { get; }

    /// <summary>Supported depths.</summary>
    IEnumerable<int> SupportedDepths { get; }

    /// <summary>Тип поиска инструмента.</summary>
    SecurityLookupSupportTypes SecurityLookupSupportType { get; }

    /// <summary>Событие о загрузке маркет-данных.</summary>
    event Action<Security, DataType, DateTimeOffset?, int, IEnumerable<Message>> DataLoaded;

    /// <summary>Событие запуска.</summary>
    event Action<IHydraTask> Started;

    /// <summary>Событие остановки.</summary>
    event Action<IHydraTask> Stopped;

    /// <summary>Текущее состояние задачи.</summary>
    TaskStates State { get; }

    /// <summary>
    /// Можно ли вызвать метод <see cref="M:StockSharp.Hydra.Core.IHydraTask.TestConnect(System.Action{System.Exception})" />.
    /// </summary>
    bool CanTestConnect { get; }

    void TestConnect(Action<Exception> connectionChanged);

    void Refresh(
      ISecurityStorage securityStorage,
      SecurityLookupMessage criteria,
      Action<Security> newSecurity,
      Func<bool> isCancelled);
  }
}
