// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.MarketDepthImpliedVolatilityDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Implied volatility market depth element.</summary>
  [DescriptionLoc("ImpliedVolatilityMarketDepth", false)]
  [DisplayNameLoc("ImpliedVolatilityMarketDepth")]
  [Doc("topics/Designer_Depth_implied_volatility.html")]
  [CategoryLoc("Options")]
  public class MarketDepthImpliedVolatilityDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260197251).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196907);
    
    private BlackScholes \u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D;
    
    private Security \u0023\u003DzmirAKT8\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.MarketDepthImpliedVolatilityDiagramElement" />.
    /// </summary>
    public MarketDepthImpliedVolatilityDiagramElement()
    {
      this.AddInput(StaticSocketIds.MarketDepth, LocalizedStrings.MarketDepth, DiagramSocketType.MarketDepth, new Action<DiagramSocketValue>(this.\u0023\u003Dz8DMCidMTrlWRQBiSVQ\u003D\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.MarketDepth, DiagramSocketType.MarketDepth, int.MaxValue, int.MaxValue, new bool?());
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

    private void \u0023\u003Dz8DMCidMTrlWRQBiSVQ\u003D\u003D(DiagramSocketValue _param1)
    {
      MarketDepth depth = _param1.GetValue<MarketDepth>();
      if (this.\u0023\u003DzmirAKT8\u003D != depth.Security)
      {
        this.\u0023\u003DzmirAKT8\u003D = depth.Security;
        this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D = (BlackScholes) null;
      }
      if (this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D == null)
      {
        if (this.\u0023\u003DzmirAKT8\u003D == null || this.Strategy == null)
          return;
        this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D = new BlackScholes(this.\u0023\u003DzmirAKT8\u003D, (ISecurityProvider) this.Strategy, (IMarketDataProvider) this.Strategy, ServicesRegistry.ExchangeInfoProvider);
      }
      MarketDepth marketDepth = depth.ImpliedVolatility(this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D, _param1.Time);
      this.RaiseProcessOutput(_param1.Time, (object) marketDepth, _param1, (Subscription) null);
    }
  }
}
