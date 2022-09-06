// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.CandleSourceDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Candles source element.</summary>
  [DescriptionLoc("Str3071", false)]
  [DisplayNameLoc("Candles")]
  [CategoryLoc("Sources")]
  [Doc("topics/Designer_Candles.html")]
  public sealed class CandleSourceDiagramElement : SubscriptionDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260196028).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196037);
    
    private readonly DiagramElementParam<CandleSeries> \u0023\u003DzoVROS7HDMzoyN3ZPjg\u003D\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzW4cOH6CNK14z;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzbDG_37IRIacQCuPIeQ\u003D\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzAT92AobvlQ_IETDjiyhLtJs\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzbZ3kMW8Q1Yv8bOtlaf1colM\u003D;
    
    private readonly DiagramElementParam<MarketDataBuildModes> \u0023\u003DzHzmDOAIyaLvT8eEOqI_2L8E\u003D;
    
    private readonly DiagramElementParam<DataType> \u0023\u003DzIsXU\u0024tBLBgNwEdRUlw\u003D\u003D;
    
    private readonly DiagramElementParam<Level1Fields?> \u0023\u003DzAY9qjM482b_1dYiuWg\u003D\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.CandleSourceDiagramElement" />.
    /// </summary>
    public CandleSourceDiagramElement()
    {
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Candles, DiagramSocketType.Candle, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzoVROS7HDMzoyN3ZPjg\u003D\u003D = this.AddParam<CandleSeries>(nameof(-1260196087), new CandleSeries()
      {
        CandleType = typeof (TimeFrameCandle),
        Arg = (object) TimeSpan.FromMinutes(5.0)
      }).SetDisplay<DiagramElementParam<CandleSeries>>(LocalizedStrings.Series, LocalizedStrings.Series, string.Concat(LocalizedStrings.CandlesSeries, nameof(-1260196070)), 20).SetOnValueChangingHandler<CandleSeries>(new Action<CandleSeries, CandleSeries>(this.\u0023\u003DzVqZioI\u0024vAjFet85ZOH8FjJw\u003D)).SetOnValueChangedHandler<CandleSeries>(new Action<CandleSeries>(this.\u0023\u003DzM8TyJWia6GEQaCsXG_Xrkck\u003D));
      this.\u0023\u003DzW4cOH6CNK14z = this.AddParam<bool>(nameof(-1260196078), true).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Series, LocalizedStrings.OnlyFormed, LocalizedStrings.ProcessOnlyFormed, 30);
      this.\u0023\u003DzbDG_37IRIacQCuPIeQ\u003D\u003D = this.AddParam<bool>(nameof(-1260195841), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Series, LocalizedStrings.VolumeProfile, LocalizedStrings.Str3077, 40);
      this.\u0023\u003DzbZ3kMW8Q1Yv8bOtlaf1colM\u003D = this.AddParam<bool>(nameof(-1260195899), true).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Series, LocalizedStrings.SmallerTimeFrame, LocalizedStrings.SmallerTimeFrameDesc, 50);
      this.\u0023\u003DzAT92AobvlQ_IETDjiyhLtJs\u003D = this.AddParam<bool>(nameof(-1260195906), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Series, LocalizedStrings.RegularHours, LocalizedStrings.RegularTradingHours, 60);
      this.\u0023\u003DzHzmDOAIyaLvT8eEOqI_2L8E\u003D = this.AddParam<MarketDataBuildModes>(nameof(-1260195966), MarketDataBuildModes.LoadAndBuild).SetDisplay<DiagramElementParam<MarketDataBuildModes>>(LocalizedStrings.Series, LocalizedStrings.Mode, LocalizedStrings.BuildMode, 70);
      this.\u0023\u003DzIsXU\u0024tBLBgNwEdRUlw\u003D\u003D = this.AddParam<DataType>(nameof(-1260196243), (DataType) null).SetDisplay<DiagramElementParam<DataType>>(LocalizedStrings.Series, LocalizedStrings.Str213, LocalizedStrings.CandlesBuildSource, 80);
      this.\u0023\u003DzAY9qjM482b_1dYiuWg\u003D\u003D = this.AddParam<Level1Fields?>(nameof(-1260196236), new Level1Fields?()).SetDisplay<DiagramElementParam<Level1Fields?>>(LocalizedStrings.Series, LocalizedStrings.Str748, LocalizedStrings.Level1Field, 90).SetEditor<DiagramElementParam<Level1Fields?>>((Attribute) new ItemsSourceAttribute(typeof (BuildCandlesFieldSource)));
    }

    /// <summary>Candles series.</summary>
    public CandleSeries Series
    {
      get
      {
        return this.\u0023\u003DzoVROS7HDMzoyN3ZPjg\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzoVROS7HDMzoyN3ZPjg\u003D\u003D.Value = value;
      }
    }

    /// <summary>Send only formed candles.</summary>
    public bool IsFinishedOnly
    {
      get
      {
        return this.\u0023\u003DzW4cOH6CNK14z.Value;
      }
      set
      {
        this.\u0023\u003DzW4cOH6CNK14z.Value = value;
      }
    }

    /// <summary>
    /// To perform the calculation <see cref="P:StockSharp.Algo.Candles.Candle.PriceLevels" />. By default, it is disabled.
    /// </summary>
    public bool IsCalcVolumeProfile
    {
      get
      {
        return this.\u0023\u003DzbDG_37IRIacQCuPIeQ\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzbDG_37IRIacQCuPIeQ\u003D\u003D.Value = value;
      }
    }

    /// <summary>
    /// Use only the regular trading hours for which data will be requested.
    /// </summary>
    public bool IsRegularTradingHours
    {
      get
      {
        return this.\u0023\u003DzAT92AobvlQ_IETDjiyhLtJs\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzAT92AobvlQ_IETDjiyhLtJs\u003D.Value = value;
      }
    }

    /// <summary>Allow build candles from smaller timeframe.</summary>
    public bool AllowBuildFromSmallerTimeFrame
    {
      get
      {
        return this.\u0023\u003DzbZ3kMW8Q1Yv8bOtlaf1colM\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzbZ3kMW8Q1Yv8bOtlaf1colM\u003D.Value = value;
      }
    }

    /// <summary>Build mode.</summary>
    public MarketDataBuildModes BuildCandlesMode
    {
      get
      {
        return this.\u0023\u003DzHzmDOAIyaLvT8eEOqI_2L8E\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzHzmDOAIyaLvT8eEOqI_2L8E\u003D.Value = value;
      }
    }

    /// <summary>Which market-data type is used as a source value.</summary>
    public DataType BuildCandlesFrom
    {
      get
      {
        return this.\u0023\u003DzIsXU\u0024tBLBgNwEdRUlw\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzIsXU\u0024tBLBgNwEdRUlw\u003D\u003D.Value = value;
      }
    }

    /// <summary>
    /// Extra info for the <see cref="P:StockSharp.Diagram.Elements.CandleSourceDiagramElement.BuildCandlesFrom" />.
    /// </summary>
    public Level1Fields? BuildCandlesField
    {
      get
      {
        return this.\u0023\u003DzAY9qjM482b_1dYiuWg\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzAY9qjM482b_1dYiuWg\u003D\u003D.Value = value;
      }
    }

    private void \u0023\u003DzKqP9U2eO8LUYkHSH8BOMQRI\u003D(
      object _param1,
      PropertyChangedEventArgs _param2)
    {
      this.SetElementName(((object) this.\u0023\u003DzoVROS7HDMzoyN3ZPjg\u003D\u003D.Value)?.ToString());
    }

    /// <inheritdoc />
    public override Guid TypeId
    {
      get
      {
        return this._typeId;
      }
    }

    /// <inheritdoc />
    public override string IconName
    {
      get
      {
        return this._iconName;
      }
    }

    /// <inheritdoc />
    protected override Subscription OnCreateSubscription()
    {
      CandleSourceDiagramElement.\u0023\u003Dz5gO5y09QcXKegNbD4LUovYg\u003D qcXkegNbD4LuovYg = new CandleSourceDiagramElement.\u0023\u003Dz5gO5y09QcXKegNbD4LUovYg\u003D();
      qcXkegNbD4LuovYg._diagramElement = this;
      qcXkegNbD4LuovYg.\u0023\u003Dzv10ueLU\u003D = new Subscription(new CandleSeries(this.\u0023\u003DzoVROS7HDMzoyN3ZPjg\u003D\u003D.Value.CandleType, this.Security, this.\u0023\u003DzoVROS7HDMzoyN3ZPjg\u003D\u003D.Value.Arg)
      {
        IsCalcVolumeProfile = this.IsCalcVolumeProfile,
        IsRegularTradingHours = this.IsRegularTradingHours,
        AllowBuildFromSmallerTimeFrame = this.AllowBuildFromSmallerTimeFrame,
        BuildCandlesMode = this.BuildCandlesMode,
        BuildCandlesFrom2 = this.BuildCandlesFrom,
        BuildCandlesField = this.BuildCandlesField,
        IsFinishedOnly = this.IsFinishedOnly
      });
      qcXkegNbD4LuovYg.\u0023\u003Dzv10ueLU\u003D.WhenCandleReceived((ISubscriptionProvider) this.Strategy).Do(new Action<Candle>(qcXkegNbD4LuovYg.\u0023\u003Dzf3cAGPy66XOs\u0024\u00243fUw\u003D\u003D)).Apply<Subscription, Candle>((IMarketRuleContainer) this.Strategy);
      return qcXkegNbD4LuovYg.\u0023\u003Dzv10ueLU\u003D;
    }

    private void \u0023\u003DzVqZioI\u0024vAjFet85ZOH8FjJw\u003D(
      CandleSeries _param1,
      CandleSeries _param2)
    {
      if (_param1 == null)
        return;
      _param1.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003DzKqP9U2eO8LUYkHSH8BOMQRI\u003D);
    }

    private void \u0023\u003DzM8TyJWia6GEQaCsXG_Xrkck\u003D(CandleSeries _param1)
    {
      if (_param1 == null)
        return;
      this.\u0023\u003DzKqP9U2eO8LUYkHSH8BOMQRI\u003D((object) null, (PropertyChangedEventArgs) null);
      _param1.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzKqP9U2eO8LUYkHSH8BOMQRI\u003D);
    }

    private sealed class \u0023\u003Dz5gO5y09QcXKegNbD4LUovYg\u003D
    {
      public CandleSourceDiagramElement _diagramElement;
      public Subscription \u0023\u003Dzv10ueLU\u003D;

      internal void \u0023\u003Dzf3cAGPy66XOs\u0024\u00243fUw\u003D\u003D(Candle _param1)
      {
        if (this._diagramElement.IsFinishedOnly && _param1.State != CandleStates.Finished)
          return;
        this._diagramElement.RaiseProcessOutput(_param1.OpenTime, (object) _param1, (DiagramSocketValue) null, this.\u0023\u003Dzv10ueLU\u003D);
        this._diagramElement.Strategy.Flush();
      }
    }
  }
}
