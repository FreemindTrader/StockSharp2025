// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.HydraTaskSecurity
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Collections;
using Ecng.ComponentModel;
using Newtonsoft.Json;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Hydra.Core
{
  /// <summary>
  /// Инструмент, ассоциированный с <see cref="T:StockSharp.Hydra.Core.IHydraTask" />.
  /// </summary>
  public class HydraTaskSecurity : NotifiableObject
  {
    /// <summary>
    /// Хэш-коллекция для быстрой проверки <see cref="P:StockSharp.Hydra.Core.HydraTaskSecurity.DataTypes" />.
    /// </summary>
    [Obsolete]
    public readonly CachedSynchronizedSet<DataType> DataTypesSet = new CachedSynchronizedSet<DataType>();
    private HydraTaskSecurity.TypeInfo _tradeInfo = new HydraTaskSecurity.TypeInfo();
    private HydraTaskSecurity.TypeInfo _depthInfo = new HydraTaskSecurity.TypeInfo();
    private HydraTaskSecurity.TypeInfo _orderLogInfo = new HydraTaskSecurity.TypeInfo();
    private HydraTaskSecurity.TypeInfo _level1Info = new HydraTaskSecurity.TypeInfo();
    private HydraTaskSecurity.TypeInfo _candleInfo = new HydraTaskSecurity.TypeInfo();
    private HydraTaskSecurity.TypeInfo _transactionInfo = new HydraTaskSecurity.TypeInfo();
    private HydraTaskSecurity.TypeInfo _positionChangeInfo = new HydraTaskSecurity.TypeInfo();
    private HydraTaskSecurity.TypeInfo _newsInfo = new HydraTaskSecurity.TypeInfo();

    /// <summary>Уникальный идентификатор инструмента.</summary>
    [Obsolete]
    public long Id { get; set; }

    /// <summary>Задача.</summary>
    public IHydraTask Task { get; set; }

    /// <summary>Биржевой инструмент.</summary>
    public Security Security { get; set; }

    /// <summary>
    /// Типы данных, которые нужно получать для данного инструмента.
    /// </summary>
    [Obsolete]
    public DataType[] DataTypes
    {
      get
      {
        return this.DataTypesSet.Cache;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (((IEnumerable<DataType>) value).Any<DataType>((Func<DataType, bool>) (t => t.MessageType == (Type) null)))
          throw new ArgumentException(nameof (value));
        this.DataTypesSet.Clear();
        this.DataTypesSet.AddRange((IEnumerable<DataType>) value);
      }
    }

    /// <summary>Источник данных построения свечей.</summary>
    [Obsolete]
    public Level1Fields? CandlesBuildFrom
    {
      get
      {
        return new Level1Fields?();
      }
      set
      {
      }
    }

    /// <summary>Информация о сделках.</summary>
    [Obsolete]
    public HydraTaskSecurity.TypeInfo TradeInfo
    {
      get
      {
        return this._tradeInfo;
      }
      set
      {
        HydraTaskSecurity.TypeInfo typeInfo = value;
        if (typeInfo == null)
          throw new ArgumentNullException(nameof (value));
        this._tradeInfo = typeInfo;
      }
    }

    /// <summary>Информация о стаканах.</summary>
    [Obsolete]
    public HydraTaskSecurity.TypeInfo DepthInfo
    {
      get
      {
        return this._depthInfo;
      }
      set
      {
        HydraTaskSecurity.TypeInfo typeInfo = value;
        if (typeInfo == null)
          throw new ArgumentNullException(nameof (value));
        this._depthInfo = typeInfo;
      }
    }

    /// <summary>Информация о логе заявок.</summary>
    [Obsolete]
    public HydraTaskSecurity.TypeInfo OrderLogInfo
    {
      get
      {
        return this._orderLogInfo;
      }
      set
      {
        HydraTaskSecurity.TypeInfo typeInfo = value;
        if (typeInfo == null)
          throw new ArgumentNullException(nameof (value));
        this._orderLogInfo = typeInfo;
      }
    }

    /// <summary>Информация о Level1.</summary>
    [Obsolete]
    public HydraTaskSecurity.TypeInfo Level1Info
    {
      get
      {
        return this._level1Info;
      }
      set
      {
        HydraTaskSecurity.TypeInfo typeInfo = value;
        if (typeInfo == null)
          throw new ArgumentNullException(nameof (value));
        this._level1Info = typeInfo;
      }
    }

    /// <summary>Информация о свечах.</summary>
    [Obsolete]
    public HydraTaskSecurity.TypeInfo CandleInfo
    {
      get
      {
        return this._candleInfo;
      }
      set
      {
        HydraTaskSecurity.TypeInfo typeInfo = value;
        if (typeInfo == null)
          throw new ArgumentNullException(nameof (value));
        this._candleInfo = typeInfo;
      }
    }

    /// <summary>Информация о логе собственных транзакций.</summary>
    [Obsolete]
    public HydraTaskSecurity.TypeInfo TransactionInfo
    {
      get
      {
        return this._transactionInfo;
      }
      set
      {
        HydraTaskSecurity.TypeInfo typeInfo = value;
        if (typeInfo == null)
          throw new ArgumentNullException(nameof (value));
        this._transactionInfo = typeInfo;
      }
    }

    /// <summary>Информация об изменениях позиций.</summary>
    [Obsolete]
    public HydraTaskSecurity.TypeInfo PositionChangeInfo
    {
      get
      {
        return this._positionChangeInfo;
      }
      set
      {
        HydraTaskSecurity.TypeInfo typeInfo = value;
        if (typeInfo == null)
          throw new ArgumentNullException(nameof (value));
        this._positionChangeInfo = typeInfo;
      }
    }

    /// <summary>Информация о новостях.</summary>
    [Obsolete]
    public HydraTaskSecurity.TypeInfo NewsInfo
    {
      get
      {
        return this._newsInfo;
      }
      set
      {
        HydraTaskSecurity.TypeInfo typeInfo = value;
        if (typeInfo == null)
          throw new ArgumentNullException(nameof (value));
        this._newsInfo = typeInfo;
      }
    }

    /// <summary>Информация по типу данных.</summary>
    public SynchronizedDictionary<DataType, HydraTaskSecurity.DateTypeInfo> InfoDict { get; set; } = new SynchronizedDictionary<DataType, HydraTaskSecurity.DateTypeInfo>();

    /// <summary>
    /// </summary>
    public bool Enabled(DataType dataType)
    {
      HydraTaskSecurity.DateTypeInfo dateTypeInfo;
      if (this.InfoDict.TryGetValue(dataType, out dateTypeInfo))
        return !dateTypeInfo.Disabled;
      return false;
    }

    /// <summary>
    /// </summary>
    public IEnumerable<DataType> GetDataTypes()
    {
      return (IEnumerable<DataType>) this.InfoDict.SyncGet<SynchronizedDictionary<DataType, HydraTaskSecurity.DateTypeInfo>, DataType[]>((Func<SynchronizedDictionary<DataType, HydraTaskSecurity.DateTypeInfo>, DataType[]>) (d => d.Where<KeyValuePair<DataType, HydraTaskSecurity.DateTypeInfo>>((Func<KeyValuePair<DataType, HydraTaskSecurity.DateTypeInfo>, bool>) (p => !p.Value.Disabled)).Select<KeyValuePair<DataType, HydraTaskSecurity.DateTypeInfo>, DataType>((Func<KeyValuePair<DataType, HydraTaskSecurity.DateTypeInfo>, DataType>) (p => p.Key)).ToArray<DataType>()));
    }

    /// <summary>
    /// </summary>
    public HydraTaskSecurity.DateTypeInfo AddDataType(DataType dataType)
    {
      HydraTaskSecurity.DateTypeInfo dateTypeInfo = this.InfoDict.SafeAdd<DataType, HydraTaskSecurity.DateTypeInfo>(dataType);
      dateTypeInfo.Disabled = false;
      return dateTypeInfo;
    }

    /// <summary>
    /// </summary>
    public void RemoveDataType(DataType dataType)
    {
      HydraTaskSecurity.DateTypeInfo dateTypeInfo;
      if (!this.InfoDict.TryGetValue(dataType, out dateTypeInfo))
        return;
      dateTypeInfo.Disabled = true;
    }

    /// <summary>Получить строковое представление.</summary>
    /// <returns>Строковое представление.</returns>
    public override string ToString()
    {
      return this.Security?.Id;
    }

    /// <summary>Информация по типу данных.</summary>
    public class TypeInfo : NotifiableObject
    {
      private long _count;
      private DateTime? _lastTime;

      /// <summary>
      /// </summary>
      [JsonProperty("disabled")]
      public bool Disabled { get; set; }

      /// <summary>Обработанное количество данных.</summary>
      [JsonProperty("count")]
      public long Count
      {
        get
        {
          return this._count;
        }
        set
        {
          this._count = value;
          this.NotifyPropertyChanged(nameof (Count));
        }
      }

      /// <summary>Временная метка последних обработанных данных.</summary>
      [JsonProperty("lastTime")]
      public DateTime? LastTime
      {
        get
        {
          return this._lastTime;
        }
        set
        {
          this._lastTime = value;
          this.NotifyPropertyChanged(nameof (LastTime));
        }
      }
    }

    /// <summary>Информация по типу данных.</summary>
    public class DateTypeInfo : HydraTaskSecurity.TypeInfo
    {
      private DateTime? _beginDate;
      private DateTime? _endDate;
      private Level1Fields? _candlesBuildFrom;
      private int? _maxDepth;
      private bool? _volumeProfile;

      /// <summary>Дата начала загрузки данных.</summary>
      [JsonProperty("beginDate")]
      public DateTime? BeginDate
      {
        get
        {
          return this._beginDate;
        }
        set
        {
          this._beginDate = value;
          this.NotifyChanged(nameof (BeginDate));
        }
      }

      /// <summary>Дата окончания загрузки данных.</summary>
      [JsonProperty("endDate")]
      public DateTime? EndDate
      {
        get
        {
          return this._endDate;
        }
        set
        {
          this._endDate = value;
          this.NotifyChanged(nameof (EndDate));
        }
      }

      /// <summary>Источник данных построения свечей.</summary>
      [JsonProperty("candlesBuildFrom")]
      public Level1Fields? CandlesBuildFrom
      {
        get
        {
          return this._candlesBuildFrom;
        }
        set
        {
          this._candlesBuildFrom = value;
          this.NotifyChanged(nameof (CandlesBuildFrom));
        }
      }

      /// <summary>Max depth.</summary>
      [JsonProperty("maxDepth")]
      public int? MaxDepth
      {
        get
        {
          return this._maxDepth;
        }
        set
        {
          this._maxDepth = value;
          this.NotifyChanged(nameof (MaxDepth));
        }
      }

      /// <summary>Volume profile.</summary>
      [JsonProperty("volProfile")]
      public bool? VolumeProfile
      {
        get
        {
          return this._volumeProfile;
        }
        set
        {
          this._volumeProfile = value;
          this.NotifyChanged(nameof (VolumeProfile));
        }
      }

      /// <summary>
      /// </summary>
      public HydraTaskSecurity.DateTypeInfo Clone()
      {
        HydraTaskSecurity.DateTypeInfo dateTypeInfo = new HydraTaskSecurity.DateTypeInfo();
        dateTypeInfo.BeginDate = this.BeginDate;
        dateTypeInfo.EndDate = this.EndDate;
        dateTypeInfo.CandlesBuildFrom = this.CandlesBuildFrom;
        dateTypeInfo.MaxDepth = this.MaxDepth;
        dateTypeInfo.Count = this.Count;
        dateTypeInfo.LastTime = this.LastTime;
        dateTypeInfo.VolumeProfile = this.VolumeProfile;
        return dateTypeInfo;
      }
    }
  }
}
