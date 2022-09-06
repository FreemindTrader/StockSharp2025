// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.Server.HydraServerSettings
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Storages;
using StockSharp.Localization;
using StockSharp.Server.Core;
using StockSharp.Server.Fix;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Hydra.Core.Server
{
  /// <summary>Hydra server settings.</summary>
  [DisplayNameLoc("Str2211")]
  public class HydraServerSettings : IPersistable
  {
    private int _maxSecurityCount = 1000;
    private int _candleHistoryMaxDays = 180;
    private int _tickHistoryMaxDays = 5;
    private int _transactionsHistoryMaxDays = 1;
    private int _orderBookHistoryMaxDays;
    private int _orderLogHistoryMaxDays;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Hydra.Core.Server.HydraServerSettings" />.
    /// </summary>
    public HydraServerSettings()
    {
      FixSession marketDataSession = this.ServerSettings.MarketDataSession;
      FixSession transactionSession = this.ServerSettings.TransactionSession;
      transactionSession.MaxReadBytes = marketDataSession.MaxReadBytes = 1048576;
      transactionSession.MaxWriteBytes = marketDataSession.MaxWriteBytes = 10485760;
      marketDataSession.Address = RemoteMarketDataDrive.DefaultAddress;
      marketDataSession.TargetCompId = RemoteMarketDataDrive.DefaultTargetCompId;
      marketDataSession.IsEnabled = true;
      transactionSession.IsEnabled = false;
    }

    /// <summary>Is server mode enabled.</summary>
    [Display(Description = "HydraFixServer", GroupName = "DataServer", Name = "FixServer", Order = 1, ResourceType = typeof (LocalizedStrings))]
    public bool IsFixServer { get; set; }

    /// <summary>FIX market data session settings.</summary>
    [Display(Description = "MarketDataSession", GroupName = "DataServer", Name = "FixServer", Order = 2, ResourceType = typeof (LocalizedStrings))]
    public FixServerSettings ServerSettings { get; set; } = new FixServerSettings();

    /// <summary>Authorization type.</summary>
    [Display(Description = "Str2216", GroupName = "DataServer", Name = "Authorization", Order = 3, ResourceType = typeof (LocalizedStrings))]
    public AuthorizationModes Authorization { get; set; }

    /// <summary>Max securities count per request.</summary>
    [Display(Description = "Str2215", GroupName = "DataServer", Name = "Str2214", Order = 4, ResourceType = typeof (LocalizedStrings))]
    public int MaxSecurityCount
    {
      get
      {
        return this._maxSecurityCount;
      }
      set
      {
        if (value < 1)
          throw new ArgumentOutOfRangeException(nameof (value));
        this._maxSecurityCount = value;
      }
    }

    /// <summary>
    /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.IsCandles" /> data history.
    /// </summary>
    [Display(Description = "CandleMaxDaysDescription", GroupName = "DataServer", Name = "CandleMaxDays", Order = 5, ResourceType = typeof (LocalizedStrings))]
    public int CandleHistoryMaxDays
    {
      get
      {
        return this._candleHistoryMaxDays;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value));
        this._candleHistoryMaxDays = value;
      }
    }

    /// <summary>
    /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.Ticks" /> data history.
    /// </summary>
    [Display(Description = "TickMaxDaysDescription", GroupName = "DataServer", Name = "TickMaxDays", Order = 6, ResourceType = typeof (LocalizedStrings))]
    public int TickHistoryMaxDays
    {
      get
      {
        return this._tickHistoryMaxDays;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value));
        this._tickHistoryMaxDays = value;
      }
    }

    /// <summary>
    /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.MarketDepth" /> data history.
    /// </summary>
    [Display(Description = "OrderBookMaxDaysDescription", GroupName = "DataServer", Name = "OrderBookMaxDays", Order = 7, ResourceType = typeof (LocalizedStrings))]
    public int OrderBookHistoryMaxDays
    {
      get
      {
        return this._orderBookHistoryMaxDays;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value));
        this._orderBookHistoryMaxDays = value;
      }
    }

    /// <summary>
    /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.OrderLog" /> data history.
    /// </summary>
    [Display(Description = "OrderLogMaxDaysDescription", GroupName = "DataServer", Name = "OrderLogMaxDays", Order = 8, ResourceType = typeof (LocalizedStrings))]
    public int OrderLogHistoryMaxDays
    {
      get
      {
        return this._orderLogHistoryMaxDays;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value));
        this._orderLogHistoryMaxDays = value;
      }
    }

    /// <summary>
    /// The maximum number of days available to download the <see cref="P:StockSharp.Messages.DataType.Transactions" /> data history.
    /// </summary>
    [Display(Description = "TransactionsMaxDaysDescription", GroupName = "DataServer", Name = "TransactionsMaxDays", Order = 9, ResourceType = typeof (LocalizedStrings))]
    public int TransactionsHistoryMaxDays
    {
      get
      {
        return this._transactionsHistoryMaxDays;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value));
        this._transactionsHistoryMaxDays = value;
      }
    }

    /// <summary>Is trading simulator enabled.</summary>
    [Display(Description = "Str1209", GroupName = "DataServer", Name = "Str1209", Order = 12, ResourceType = typeof (LocalizedStrings))]
    public bool SimulatorEnabled { get; set; }

    /// <summary>Translates on client only mapped securities.</summary>
    [Display(Description = "OnlyMappedSecurities", GroupName = "DataServer", Name = "SecurityMapping", Order = 13, ResourceType = typeof (LocalizedStrings))]
    public bool OnlyMappedSecurities { get; set; }

    /// <summary>Load settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Load(SettingsStorage storage)
    {
      this.IsFixServer = storage.GetValue<bool>("IsFixServer", false);
      this.Authorization = storage.GetValue<AuthorizationModes>("Authorization", AuthorizationModes.Anonymous);
      if (storage.ContainsKey("ServerSettings"))
      {
        this.ServerSettings.ForceLoad<FixServerSettings>(storage.GetValue<SettingsStorage>("ServerSettings", (SettingsStorage) null));
      }
      else
      {
        SettingsStorage storage1 = storage.GetValue<SettingsStorage>("FixMarketDataSession", (SettingsStorage) null);
        if (storage1 != null)
          this.ServerSettings.MarketDataSession.Load(storage1);
      }
      this.MaxSecurityCount = storage.GetValue<int>("MaxSecurityCount", this.MaxSecurityCount);
      this.CandleHistoryMaxDays = storage.GetValue<int>("CandleHistoryMaxDays", this.CandleHistoryMaxDays);
      this.TickHistoryMaxDays = storage.GetValue<int>("TickHistoryMaxDays", this.TickHistoryMaxDays);
      this.OrderBookHistoryMaxDays = storage.GetValue<int>("OrderBookHistoryMaxDays", this.OrderBookHistoryMaxDays);
      this.OrderLogHistoryMaxDays = storage.GetValue<int>("OrderLogHistoryMaxDays", this.OrderLogHistoryMaxDays);
      this.TransactionsHistoryMaxDays = storage.GetValue<int>("TransactionsHistoryMaxDays", this.TransactionsHistoryMaxDays);
      this.SimulatorEnabled = storage.GetValue<bool>("SimulatorEnabled", this.SimulatorEnabled);
      this.OnlyMappedSecurities = storage.GetValue<bool>("OnlyMappedSecurities", this.OnlyMappedSecurities);
    }

    /// <summary>Save settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Save(SettingsStorage storage)
    {
      storage.SetValue<bool>("IsFixServer", this.IsFixServer);
      storage.SetValue<string>("Authorization", ((object) this.Authorization).To<string>());
      storage.SetValue<SettingsStorage>("ServerSettings", this.ServerSettings.Save());
      storage.SetValue<int>("MaxSecurityCount", this.MaxSecurityCount);
      storage.SetValue<int>("CandleHistoryMaxDays", this.CandleHistoryMaxDays);
      storage.SetValue<int>("TickHistoryMaxDays", this.TickHistoryMaxDays);
      storage.SetValue<int>("OrderBookHistoryMaxDays", this.OrderBookHistoryMaxDays);
      storage.SetValue<int>("OrderLogHistoryMaxDays", this.OrderLogHistoryMaxDays);
      storage.SetValue<int>("TransactionsHistoryMaxDays", this.TransactionsHistoryMaxDays);
      storage.SetValue<bool>("SimulatorEnabled", this.SimulatorEnabled);
      storage.SetValue<bool>("OnlyMappedSecurities", this.OnlyMappedSecurities);
    }
  }
}
