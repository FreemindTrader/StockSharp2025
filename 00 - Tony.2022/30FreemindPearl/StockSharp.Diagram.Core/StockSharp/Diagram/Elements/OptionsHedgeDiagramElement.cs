// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OptionsHedgeDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.Algo.Strategies.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Options hedging diagram element.</summary>
  [Doc("topics/Designer_Hedging.html")]
  [CategoryLoc("Options")]
  [DescriptionLoc("OptionsHedgeDiagramElement", false)]
  [DisplayNameLoc("Str1244")]
  public class OptionsHedgeDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260194624).To<Guid>();
    
    private readonly string _iconName = nameof(-1260194633);
    
    private Security \u0023\u003DzBP7tu1g\u003D;
    
    private HedgeStrategy \u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D;
    
    private Decimal? \u0023\u003DzZjI18On4726h;
    
    private Decimal? \u0023\u003DzcKfDqmIFrO9a;
    
    private readonly DiagramElementParam<BlackScholesGreeks?> \u0023\u003DzV8c2l8SZ4_gr;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OptionsHedgeDiagramElement" />.
    /// </summary>
    public OptionsHedgeDiagramElement()
    {
      this.AddInput(StaticSocketIds.UnderlyingAsset, LocalizedStrings.UnderlyingAsset, DiagramSocketType.Security, new Action<DiagramSocketValue>(this.\u0023\u003DzJJus3ioOTJH8), 1, int.MaxValue, false, new bool?());
      this.AddInput(StaticSocketIds.Volume, LocalizedStrings.Volume, DiagramSocketType.Unit, new Action<DiagramSocketValue>(this.\u0023\u003DzqSshlkwjECvh), 1, int.MaxValue, false, new bool?());
      this.AddInput(StaticSocketIds.Position, LocalizedStrings.UnderlyingAssetPosition, DiagramSocketType.Unit, new Action<DiagramSocketValue>(this.\u0023\u003DzoagKfQlRUVZA), 1, int.MaxValue, false, new bool?());
      this.AddInput(StaticSocketIds.Signal, LocalizedStrings.Signal, DiagramSocketType.Bool, new Action<DiagramSocketValue>(this.\u0023\u003DzMagJSYg\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str3147, DiagramSocketType.Order, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzV8c2l8SZ4_gr = this.AddParam<BlackScholesGreeks?>(nameof(-1260194680), new BlackScholesGreeks?()).SetDisplay<DiagramElementParam<BlackScholesGreeks?>>(LocalizedStrings.Options, LocalizedStrings.Type, string.Concat(LocalizedStrings.Type, nameof(-1260196070)), 10).SetOnValueChangedHandler<BlackScholesGreeks?>(new Action<BlackScholesGreeks?>(this.\u0023\u003DzjD_2pHqHObHlPnTbpAgNFHU\u003D));
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

    /// <summary>Hedge type.</summary>
    public BlackScholesGreeks? HedgeType
    {
      get
      {
        return this.\u0023\u003DzV8c2l8SZ4_gr.Value;
      }
      set
      {
        this.\u0023\u003DzV8c2l8SZ4_gr.Value = value;
      }
    }

    private void \u0023\u003DzMagJSYg\u003D(DiagramSocketValue _param1)
    {
      if (!this.\u0023\u003DzcKfDqmIFrO9a.HasValue || !this.\u0023\u003DzZjI18On4726h.HasValue || (this.\u0023\u003DzBP7tu1g\u003D == null || this.\u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D != null))
        return;
      this.\u0023\u003DzWlfj8_ZvOriS();
    }

    private void \u0023\u003DzoagKfQlRUVZA(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzcKfDqmIFrO9a = new Decimal?(_param1.Value.To<Decimal>());
    }

    private void \u0023\u003DzqSshlkwjECvh(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzZjI18On4726h = new Decimal?(_param1.Value.To<Decimal>());
    }

    private void \u0023\u003DzJJus3ioOTJH8(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzBP7tu1g\u003D = (Security) _param1.Value;
    }

    private void \u0023\u003DzWlfj8_ZvOriS()
    {
      BlackScholesGreeks? hedgeType = this.HedgeType;
      if (hedgeType.HasValue)
      {
        switch (hedgeType.GetValueOrDefault())
        {
          case BlackScholesGreeks.Delta:
            this.\u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D = (HedgeStrategy) new DeltaHedgeStrategy(ServicesRegistry.ExchangeInfoProvider);
            this.\u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D.Security = this.\u0023\u003DzBP7tu1g\u003D;
            this.\u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D.WhenOrderRegistered().Do(new Action<Order>(this.\u0023\u003DzLxEQpZm\u00247ac9rSZqWCf\u0024IcI\u003D)).Apply<Strategy, Order>((IMarketRuleContainer) this.Strategy);
            this.Strategy.ChildStrategies.Add((Strategy) this.\u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D);
            return;
          case BlackScholesGreeks.Gamma:
          case BlackScholesGreeks.Vega:
          case BlackScholesGreeks.Theta:
          case BlackScholesGreeks.Rho:
            throw new NotSupportedException();
        }
      }
      throw new InvalidOperationException(this.HedgeType.To<string>());
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      this.\u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D = (HedgeStrategy) null;
      this.\u0023\u003DzBP7tu1g\u003D = (Security) null;
      this.\u0023\u003DzcKfDqmIFrO9a = new Decimal?();
      this.\u0023\u003DzZjI18On4726h = new Decimal?();
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      if (this.\u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D != null)
        this.Strategy.ChildStrategies.Remove((Strategy) this.\u0023\u003DzV8QhN1N2WTxCiHDvrQ\u003D\u003D);
      base.OnStop();
    }

    private void \u0023\u003DzjD_2pHqHObHlPnTbpAgNFHU\u003D(BlackScholesGreeks? _param1)
    {
      this.SetElementName(_param1.HasValue ? ((object) _param1.GetValueOrDefault()).GetDisplayName() : (string) null);
    }

    private void \u0023\u003DzLxEQpZm\u00247ac9rSZqWCf\u0024IcI\u003D(Order _param1)
    {
      this.RaiseProcessOutput(_param1.LastChangeTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
    }
  }
}
