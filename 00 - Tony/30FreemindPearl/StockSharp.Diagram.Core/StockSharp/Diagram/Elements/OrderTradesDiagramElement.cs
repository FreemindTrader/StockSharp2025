// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OrderTradesDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Trades per order element.</summary>
  [DisplayNameLoc("Str3129")]
  [DescriptionLoc("Str3130", false)]
  [Doc("topics/Designer_Deals_on_request.html")]
  [CategoryLoc("Common")]
  public class OrderTradesDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260195453).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195722);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OrderTradesDiagramElement" />.
    /// </summary>
    public OrderTradesDiagramElement()
    {
      this.AddInput(StaticSocketIds.Order, LocalizedStrings.Str504, DiagramSocketType.Order, new Action<DiagramSocketValue>(this.\u0023\u003DzHMHk24A\u003D), 1, int.MaxValue, false, new bool?());
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

    private void \u0023\u003DzHMHk24A\u003D(DiagramSocketValue _param1)
    {
      _param1.GetValue<Order>().WhenNewTrade((ITransactionProvider) this.Strategy).Do(new Action<MyTrade>(this.\u0023\u003DzbVB2nnE4WNkBBjuIXw\u003D\u003D)).Apply<Order, MyTrade>((IMarketRuleContainer) this.Strategy);
    }

    private void \u0023\u003DzbVB2nnE4WNkBBjuIXw\u003D\u003D(MyTrade _param1)
    {
      this.RaiseProcessOutput(_param1.Trade.Time, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }
  }
}
