// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.StrategyTradesDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Strategy trades element.</summary>
  [CategoryLoc("Common")]
  [DisplayNameLoc("Str3155")]
  [Doc("topics/Designer_Trades_strategy.html")]
  [DescriptionLoc("Str3156", false)]
  public class StrategyTradesDiagramElement : StrategyInputDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260192487).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195722);
    
    private Security \u0023\u003DzmirAKT8\u003D;
    
    private IMarketRule \u0023\u003DzV6kLP74\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OrderTradesDiagramElement" />.
    /// </summary>
    public StrategyTradesDiagramElement()
      : base(LocalizedStrings.Common)
    {
      this.AddInput(StaticSocketIds.Security, LocalizedStrings.Security, DiagramSocketType.Security, new Action<DiagramSocketValue>(this.\u0023\u003DzECIEXk\u0024A5Mms), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str985, DiagramSocketType.MyTrade, int.MaxValue, int.MaxValue, new bool?());
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
    protected override void OnStart()
    {
      this.\u0023\u003DzV6kLP74\u003D = (IMarketRule) null;
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnSubscribe(Strategy strategy, DiagramSocketValue source)
    {
      this.\u0023\u003DzV6kLP74\u003D = (IMarketRule) StrategyHelper.WhenNewMyTrade(strategy).Do(new Action<MyTrade>(this.\u0023\u003Dz_su27khXoiuVGDdnYjx1eww\u003D)).Apply<Strategy, MyTrade>((IMarketRuleContainer) strategy);
    }

    /// <inheritdoc />
    protected override void OnUnSubscribe(Strategy strategy, DiagramSocketValue source)
    {
      if (this.\u0023\u003DzV6kLP74\u003D == null)
        return;
      strategy.TryRemoveRule(this.\u0023\u003DzV6kLP74\u003D, true);
    }

    private void \u0023\u003DzECIEXk\u0024A5Mms(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzmirAKT8\u003D = _param1.GetValue<Security>();
    }

    private void \u0023\u003Dz_su27khXoiuVGDdnYjx1eww\u003D(MyTrade _param1)
    {
      if (this.\u0023\u003DzmirAKT8\u003D != null && _param1.Order.Security != this.\u0023\u003DzmirAKT8\u003D)
        return;
      this.RaiseProcessOutput(_param1.Trade.Time, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }
  }
}
