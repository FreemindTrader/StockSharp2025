// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OptionsBlackScholesDiagramElement
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>The Black-Scholes "Greeks" evaluation element.</summary>
  [Doc("topics/Designer_Black_Scholes.html")]
  [DescriptionLoc("OptionsBlackScholesDiagramElement", false)]
  [DisplayNameLoc("BlackScholes")]
  [CategoryLoc("Options")]
  public class OptionsBlackScholesDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260194765).To<Guid>();
    
    private readonly string _iconName = nameof(-1260194586);
    
    private BasketBlackScholes \u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D;
    
    private Security[] \u0023\u003DzDC1trCE\u003D;
    
    private Decimal? \u0023\u003DzBtPuUrdq2rZahcsSuw\u003D\u003D;
    
    private Decimal? \u0023\u003DzggKjHMaq6llb7EEoKA\u003D\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzChfyFXgBVGT\u0024;
    
    private readonly DiagramElementParam<BlackScholesGreeks> \u0023\u003DzUSgM8rI\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OptionsBlackScholesDiagramElement" />.
    /// </summary>
    public OptionsBlackScholesDiagramElement()
    {
      this.AddInput(StaticSocketIds.Price, LocalizedStrings.Str2468, DiagramSocketType.Unit, new Action<DiagramSocketValue>(this.\u0023\u003DzzRnUV_mhdWclEEkPKg\u003D\u003D), 1, int.MaxValue, false, new bool?());
      this.AddInput(StaticSocketIds.Options, LocalizedStrings.Options, DiagramSocketType.Options, new Action<DiagramSocketValue>(this.\u0023\u003Dzd_4pjLI\u003D), 1, int.MaxValue, false, new bool?());
      this.AddInput(StaticSocketIds.MaxDeviation, LocalizedStrings.Str2268, DiagramSocketType.Unit, new Action<DiagramSocketValue>(this.\u0023\u003DzAfdPgrQ\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str1738, DiagramSocketType.Unit, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzChfyFXgBVGT\u0024 = this.AddParam<bool>(nameof(-1260194572), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Options, LocalizedStrings.BlackModel, LocalizedStrings.BlackModel, 10).SetOnValueChangedHandler<bool>(new Action<bool>(this.\u0023\u003Dz1DFoGRVGMJirY7xLCgN5OQM\u003D));
      this.\u0023\u003DzUSgM8rI\u003D = this.AddParam<BlackScholesGreeks>(nameof(-1260197747), BlackScholesGreeks.Delta).SetDisplay<DiagramElementParam<BlackScholesGreeks>>(LocalizedStrings.Options, LocalizedStrings.Str3099, LocalizedStrings.Str3099, 20);
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
    /// To use the model <see cref="T:StockSharp.Algo.Derivatives.Black" /> instead of <see cref="T:StockSharp.Algo.Derivatives.IBlackScholes" /> model. The default is off.
    /// </summary>
    public bool UseBlackModel
    {
      get
      {
        return this.\u0023\u003DzChfyFXgBVGT\u0024.Value;
      }
      set
      {
        this.\u0023\u003DzChfyFXgBVGT\u0024.Value = value;
      }
    }

    /// <summary>Value type.</summary>
    public BlackScholesGreeks ValueType
    {
      get
      {
        return this.\u0023\u003DzUSgM8rI\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzUSgM8rI\u003D.Value = value;
      }
    }

    private void CreateNewBehavior()
    {
      this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D = (BasketBlackScholes) null;
      if (this.\u0023\u003DzDC1trCE\u003D == null || this.Strategy == null)
        return;
      this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D = new BasketBlackScholes((ISecurityProvider) this.Strategy, (IMarketDataProvider) this.Strategy, ServicesRegistry.ExchangeInfoProvider, (IPositionProvider) this.Strategy);
      foreach (Security option in this.\u0023\u003DzDC1trCE\u003D)
        this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D.InnerModels.Add(this.UseBlackModel ? (BlackScholes) new Black(option, (ISecurityProvider) this.Strategy, (IMarketDataProvider) this.Strategy, this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D.ExchangeInfoProvider) : new BlackScholes(option, (ISecurityProvider) this.Strategy, (IMarketDataProvider) this.Strategy, this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D.ExchangeInfoProvider));
    }

    private void \u0023\u003DzzRnUV_mhdWclEEkPKg\u003D\u003D(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzBtPuUrdq2rZahcsSuw\u003D\u003D = _param1.GetValue<Decimal?>();
      this.\u0023\u003DzofGJ2\u0024VQa15Y(_param1);
    }

    private void \u0023\u003Dzd_4pjLI\u003D(DiagramSocketValue _param1)
    {
      IEnumerable<Security> source = _param1.GetValue<IEnumerable<Security>>();
      this.\u0023\u003DzDC1trCE\u003D = source != null ? source.ToArray<Security>() : (Security[]) null;
      this.CreateNewBehavior();
      this.\u0023\u003DzofGJ2\u0024VQa15Y(_param1);
    }

    private void \u0023\u003DzAfdPgrQ\u003D(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzggKjHMaq6llb7EEoKA\u003D\u003D = _param1.GetValue<Decimal?>();
      this.\u0023\u003DzofGJ2\u0024VQa15Y(_param1);
    }

    private void \u0023\u003DzofGJ2\u0024VQa15Y(DiagramSocketValue _param1)
    {
      if (this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D == null)
        return;
      DateTimeOffset time = _param1.Time;
      Decimal? nullable;
      switch (this.ValueType)
      {
        case BlackScholesGreeks.Delta:
          nullable = this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D.Delta(time, this.\u0023\u003DzggKjHMaq6llb7EEoKA\u003D\u003D, this.\u0023\u003DzBtPuUrdq2rZahcsSuw\u003D\u003D);
          break;
        case BlackScholesGreeks.Gamma:
          nullable = this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D.Gamma(time, this.\u0023\u003DzggKjHMaq6llb7EEoKA\u003D\u003D, this.\u0023\u003DzBtPuUrdq2rZahcsSuw\u003D\u003D);
          break;
        case BlackScholesGreeks.Vega:
          nullable = this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D.Vega(time, this.\u0023\u003DzggKjHMaq6llb7EEoKA\u003D\u003D, this.\u0023\u003DzBtPuUrdq2rZahcsSuw\u003D\u003D);
          break;
        case BlackScholesGreeks.Theta:
          nullable = this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D.Theta(time, this.\u0023\u003DzggKjHMaq6llb7EEoKA\u003D\u003D, this.\u0023\u003DzBtPuUrdq2rZahcsSuw\u003D\u003D);
          break;
        case BlackScholesGreeks.Rho:
          nullable = this.\u0023\u003Dzf0GcwQPwAgp5OB1AJ\u0024qHfi0\u003D.Rho(time, this.\u0023\u003DzggKjHMaq6llb7EEoKA\u003D\u003D, this.\u0023\u003DzBtPuUrdq2rZahcsSuw\u003D\u003D);
          break;
        default:
          throw new InvalidOperationException(((object) this.ValueType).To<string>());
      }
      if (!nullable.HasValue)
        return;
      this.RaiseProcessOutput(time, (object) nullable.Value, _param1, (Subscription) null);
    }

    private void \u0023\u003Dz1DFoGRVGMJirY7xLCgN5OQM\u003D(bool _param1)
    {
      this.CreateNewBehavior();
    }
  }
}
