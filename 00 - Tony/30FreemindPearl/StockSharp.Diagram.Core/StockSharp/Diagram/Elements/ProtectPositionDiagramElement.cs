// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.ProtectPositionDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.Algo.Strategies.Protective;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Open position protection element.</summary>
  [DescriptionLoc("Str3146", false)]
  [DisplayNameLoc("Str3145")]
  [CategoryLoc("Common")]
  [Doc("topics/Designer_Protect_positions.html")]
  public class ProtectPositionDiagramElement : DiagramElement
  {
    
    private readonly Dictionary<Security, AutoProtectiveStrategy> \u0023\u003DzZLZIA_uxIlkrW3kSjKgoPdw47nYIibAbig\u003D\u003D = new Dictionary<Security, AutoProtectiveStrategy>();
    
    private readonly Guid _typeId = nameof(-1260194840).To<Guid>();
    
    private readonly string _iconName = nameof(-1260194633);
    
    private readonly DiagramElementParam<Unit> \u0023\u003DzFBqqy61uM3n8;
    
    private readonly DiagramElementParam<Unit> \u0023\u003DzTRbYGORN90IM;
    
    private readonly DiagramElementParam<bool> \u0023\u003Dzu\u0024WrMoW\u0024kBCvMp1big\u003D\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzQmLuikGmvjFfbw73je8S5YA\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003Dzgxsy7hBK1jmb1QEnkpDfGww\u003D;
    
    private readonly DiagramElementParam<TimeSpan> \u0023\u003DztJn9BhQ8WRQV_Svp5FPW4BM\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzkuDzjKhGTzjowoEGMQ\u003D\u003D;
    
    private readonly DiagramElementParam<TimeSpan> \u0023\u003Dzl4mFrDToz2OWEyfnZw\u003D\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.ProtectPositionDiagramElement" />.
    /// </summary>
    public ProtectPositionDiagramElement()
    {
      this.AddInput(StaticSocketIds.Trade, LocalizedStrings.Str985, DiagramSocketType.MyTrade, new Action<DiagramSocketValue>(this.\u0023\u003DzHMHk24A\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str3147, DiagramSocketType.Order, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzFBqqy61uM3n8 = this.AddParam<Unit>(nameof(-1260194849), (Unit) null).SetDisplay<DiagramElementParam<Unit>>(LocalizedStrings.TakeProfit, LocalizedStrings.Str3099, LocalizedStrings.Str3150, 40).SetOnValueChangedHandler<Unit>(new Action<Unit>(this.\u0023\u003Dz624pijlNaFaypHm6w1pnTpE\u003D));
      this.\u0023\u003Dzgxsy7hBK1jmb1QEnkpDfGww\u003D = this.AddParam<bool>(nameof(-1260194897), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.TakeProfit, LocalizedStrings.Trailing, LocalizedStrings.TrailingTakeProfit, 41);
      this.\u0023\u003DztJn9BhQ8WRQV_Svp5FPW4BM\u003D = this.AddParam<TimeSpan>(nameof(-1260194894), new TimeSpan()).SetDisplay<DiagramElementParam<TimeSpan>>(LocalizedStrings.TakeProfit, LocalizedStrings.TimeOut, LocalizedStrings.TimeOut, 42);
      this.\u0023\u003DzTRbYGORN90IM = this.AddParam<Unit>(nameof(-1260194918), (Unit) null).SetDisplay<DiagramElementParam<Unit>>(LocalizedStrings.StopLoss, LocalizedStrings.Str3099, LocalizedStrings.Str3152, 20).SetOnValueChangedHandler<Unit>(new Action<Unit>(this.\u0023\u003DznRGTM1F9hyEuJqTWZj\u0024w_YE\u003D));
      this.\u0023\u003DzkuDzjKhGTzjowoEGMQ\u003D\u003D = this.AddParam<bool>(nameof(-1260195222), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.StopLoss, LocalizedStrings.Trailing, LocalizedStrings.TrailingStopLoss, 21);
      this.\u0023\u003Dzl4mFrDToz2OWEyfnZw\u003D\u003D = this.AddParam<TimeSpan>(nameof(-1260194894), new TimeSpan()).SetDisplay<DiagramElementParam<TimeSpan>>(LocalizedStrings.StopLoss, LocalizedStrings.TimeOut, LocalizedStrings.TimeOut, 22);
      this.\u0023\u003Dzu\u0024WrMoW\u0024kBCvMp1big\u003D\u003D = this.AddParam<bool>(nameof(-1260195213), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Strategy, LocalizedStrings.WaitAllTrades, LocalizedStrings.WaitAllTrades, 100);
      this.\u0023\u003DzQmLuikGmvjFfbw73je8S5YA\u003D = this.AddParam<bool>(nameof(-1260195233), true).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Strategy, LocalizedStrings.MarketOrders, LocalizedStrings.MarketOrders, 101);
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

    /// <summary>
    /// The protective level for the take profit. The default level is 0, which means the disabled.
    /// </summary>
    public Unit TakeValue
    {
      get
      {
        return this.\u0023\u003DzFBqqy61uM3n8.Value;
      }
      set
      {
        this.\u0023\u003DzFBqqy61uM3n8.Value = value;
      }
    }

    /// <summary>
    /// The protective level for the stop loss. The default level is 0, which means the disabled.
    /// </summary>
    public Unit StopValue
    {
      get
      {
        return this.\u0023\u003DzTRbYGORN90IM.Value;
      }
      set
      {
        this.\u0023\u003DzTRbYGORN90IM.Value = value;
      }
    }

    /// <summary>
    /// Stop strategy only after getting all trades by registered orders.
    /// </summary>
    /// <remarks>It is disabled by default.</remarks>
    public bool WaitAllTrades
    {
      get
      {
        return this.\u0023\u003Dzu\u0024WrMoW\u0024kBCvMp1big\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003Dzu\u0024WrMoW\u0024kBCvMp1big\u003D\u003D.Value = value;
      }
    }

    /// <summary>Whether to use market orders.</summary>
    public bool UseMarketOrders
    {
      get
      {
        return this.\u0023\u003DzQmLuikGmvjFfbw73je8S5YA\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzQmLuikGmvjFfbw73je8S5YA\u003D.Value = value;
      }
    }

    /// <summary>
    /// Whether to use a trailing technique for <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" />. The default is off.
    /// </summary>
    public bool IsTrailingTakeProfit
    {
      get
      {
        return this.\u0023\u003Dzgxsy7hBK1jmb1QEnkpDfGww\u003D.Value;
      }
      set
      {
        this.\u0023\u003Dzgxsy7hBK1jmb1QEnkpDfGww\u003D.Value = value;
      }
    }

    /// <summary>
    /// Time limit for <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" />. If protection has not worked by this time, the position will be closed on the market. The default is off.
    /// </summary>
    public TimeSpan TakeProfitTimeOut
    {
      get
      {
        return this.\u0023\u003DztJn9BhQ8WRQV_Svp5FPW4BM\u003D.Value;
      }
      set
      {
        this.\u0023\u003DztJn9BhQ8WRQV_Svp5FPW4BM\u003D.Value = value;
      }
    }

    /// <summary>
    /// Whether to use a trailing technique for <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" />. The default is off.
    /// </summary>
    public bool IsTrailingStopLoss
    {
      get
      {
        return this.\u0023\u003DzkuDzjKhGTzjowoEGMQ\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzkuDzjKhGTzjowoEGMQ\u003D\u003D.Value = value;
      }
    }

    /// <summary>
    /// Time limit for <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" />. If protection has not worked by this time, the position will be closed on the market. The default is off.
    /// </summary>
    public TimeSpan StopLossTimeOut
    {
      get
      {
        return this.\u0023\u003Dzl4mFrDToz2OWEyfnZw\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003Dzl4mFrDToz2OWEyfnZw\u003D\u003D.Value = value;
      }
    }

    private void \u0023\u003DzD3gPmrc\u003D()
    {
      string str = (string) null;
      if (this.TakeValue != (Unit) 0)
        str = string.Concat(str, LocalizedStrings.TakeIs.Put((object[]) new object[1]
        {
          (object) this.TakeValue
        }));
      if (this.StopValue != (Unit) 0)
      {
        if (!str.IsEmpty())
          str = string.Concat(str, nameof(-1260195287));
        str = string.Concat(str, LocalizedStrings.StopIs.Put((object[]) new object[1]
        {
          (object) this.StopValue
        }));
      }
      this.SetElementName(str);
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      this.\u0023\u003DzZLZIA_uxIlkrW3kSjKgoPdw47nYIibAbig\u003D\u003D.Clear();
      base.OnStart();
    }

    private void \u0023\u003DzHMHk24A\u003D(DiagramSocketValue _param1)
    {
      MyTrade trade = _param1.GetValue<MyTrade>();
      this.\u0023\u003DzZLZIA_uxIlkrW3kSjKgoPdw47nYIibAbig\u003D\u003D.SafeAdd<Security, AutoProtectiveStrategy>(trade.Order.Security, new Func<Security, AutoProtectiveStrategy>(this.\u0023\u003Dz9PFpdyOv0c4nsL0m1w\u003D\u003D)).ProcessNewMyTrade(trade);
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      this.Strategy.ChildStrategies.RemoveRange<Strategy>((IEnumerable<Strategy>) this.\u0023\u003DzZLZIA_uxIlkrW3kSjKgoPdw47nYIibAbig\u003D\u003D.Values);
      base.OnStop();
    }

    private void \u0023\u003Dz624pijlNaFaypHm6w1pnTpE\u003D(Unit _param1)
    {
      this.\u0023\u003DzD3gPmrc\u003D();
    }

    private void \u0023\u003DznRGTM1F9hyEuJqTWZj\u0024w_YE\u003D(Unit _param1)
    {
      this.\u0023\u003DzD3gPmrc\u003D();
    }

    private AutoProtectiveStrategy \u0023\u003Dz9PFpdyOv0c4nsL0m1w\u003D\u003D(
      Security _param1)
    {
      AutoProtectiveStrategy protectiveStrategy = new AutoProtectiveStrategy();
      protectiveStrategy.Security = _param1;
      Unit unit1 = this.TakeValue;
      if ((object) unit1 == null)
        unit1 = (Unit) 0;
      protectiveStrategy.TakeProfitLevel = unit1;
      Unit unit2 = this.StopValue;
      if ((object) unit2 == null)
        unit2 = (Unit) 0;
      protectiveStrategy.StopLossLevel = unit2;
      protectiveStrategy.WaitAllTrades = this.\u0023\u003Dzu\u0024WrMoW\u0024kBCvMp1big\u003D\u003D.Value;
      protectiveStrategy.IsTrailingStopLoss = this.IsTrailingStopLoss;
      protectiveStrategy.IsTrailingTakeProfit = this.IsTrailingTakeProfit;
      protectiveStrategy.StopLossTimeOut = this.StopLossTimeOut;
      protectiveStrategy.TakeProfitTimeOut = this.TakeProfitTimeOut;
      protectiveStrategy.UseMarketOrders = this.UseMarketOrders;
      AutoProtectiveStrategy strategy = protectiveStrategy;
      strategy.WhenOrderRegistered().Do(new Action<Order>(this.\u0023\u003DzqF5voOcVkFl\u0024ewGRoQ\u003D\u003D)).Apply<Strategy, Order>((IMarketRuleContainer) this.Strategy);
      this.Strategy.ChildStrategies.Add((Strategy) strategy);
      return strategy;
    }

    private void \u0023\u003DzqF5voOcVkFl\u0024ewGRoQ\u003D\u003D(Order _param1)
    {
      this.RaiseProcessOutput(_param1.LastChangeTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }
  }
}
