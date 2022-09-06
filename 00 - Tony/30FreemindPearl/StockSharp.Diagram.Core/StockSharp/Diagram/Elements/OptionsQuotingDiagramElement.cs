// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OptionsQuotingDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Strategies;
using StockSharp.Algo.Strategies.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Options quoting diagram element.</summary>
  [Doc("topics/Designer_Quoting.html")]
  [CategoryLoc("Options")]
  [DescriptionLoc("OptionsQuotingDiagramElement", false)]
  [DisplayNameLoc("Str1264")]
  public class OptionsQuotingDiagramElement : DiagramElement
  {
    
    private readonly Dictionary<Security, Strategy> \u0023\u003DzzZYEjZYHg4TbxVeymZqhPPkEOATr = new Dictionary<Security, Strategy>();
    
    private readonly Guid _typeId = nameof(-1260194664).To<Guid>();
    
    private readonly string _iconName = nameof(-1260194586);
    
    private Decimal? \u0023\u003DzeCmqHLzxP9Mg;
    
    private readonly DiagramElementParam<OptionsQuotingTypes> \u0023\u003DzCjCa6bWAyEC3UjAznA\u003D\u003D;
    
    private readonly DiagramElementParam<Sides> \u0023\u003DzUvBN\u00249E25AsOk\u0024v9pA\u003D\u003D;
    
    private readonly DiagramElementParam<Decimal?> \u0023\u003DzT_Q2A0E\u003D;
    
    private readonly DiagramElementParam<Decimal?> \u0023\u003Dz_nCHtsE\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OptionsQuotingDiagramElement" />.
    /// </summary>
    public OptionsQuotingDiagramElement()
    {
      this.AddInput(StaticSocketIds.Options, LocalizedStrings.Options, DiagramSocketType.Options, new Action<DiagramSocketValue>(this.\u0023\u003Dzd_4pjLI\u003D), 1, int.MaxValue, false, new bool?());
      this.AddInput(StaticSocketIds.Volume, LocalizedStrings.Volume, DiagramSocketType.Unit, new Action<DiagramSocketValue>(this.\u0023\u003Dz8b17eddFxrQT), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str504, DiagramSocketType.Order, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzCjCa6bWAyEC3UjAznA\u003D\u003D = this.AddParam<OptionsQuotingTypes>(nameof(-1260193969), OptionsQuotingTypes.Volalitity).SetDisplay<DiagramElementParam<OptionsQuotingTypes>>(LocalizedStrings.Options, LocalizedStrings.Str1264, string.Concat(LocalizedStrings.Str1264, nameof(-1260196070)), 10);
      this.\u0023\u003DzUvBN\u00249E25AsOk\u0024v9pA\u003D\u003D = this.AddParam<Sides>(nameof(-1260193955), Sides.Buy).SetDisplay<DiagramElementParam<Sides>>(LocalizedStrings.Options, LocalizedStrings.Str128, LocalizedStrings.Str277, 20);
      this.\u0023\u003DzT_Q2A0E\u003D = this.AddParam<Decimal?>(nameof(-1260194005), new Decimal?()).SetDisplay<DiagramElementParam<Decimal?>>(LocalizedStrings.Options, LocalizedStrings.Minimum, string.Concat(LocalizedStrings.Minimum, nameof(-1260196070)), 30);
      this.\u0023\u003Dz_nCHtsE\u003D = this.AddParam<Decimal?>(nameof(-1260194015), new Decimal?()).SetDisplay<DiagramElementParam<Decimal?>>(LocalizedStrings.Options, LocalizedStrings.Str3407, string.Concat(LocalizedStrings.Str3407, nameof(-1260196070)), 40);
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

    /// <summary>Quoting type.</summary>
    public OptionsQuotingTypes QuotingType
    {
      get
      {
        return this.\u0023\u003DzCjCa6bWAyEC3UjAznA\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzCjCa6bWAyEC3UjAznA\u003D\u003D.Value = value;
      }
    }

    /// <summary>Quoting direction.</summary>
    public Sides QuotingSide
    {
      get
      {
        return this.\u0023\u003DzUvBN\u00249E25AsOk\u0024v9pA\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzUvBN\u00249E25AsOk\u0024v9pA\u003D\u003D.Value = value;
      }
    }

    /// <summary>Min.</summary>
    public Decimal? Min
    {
      get
      {
        return this.\u0023\u003DzT_Q2A0E\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzT_Q2A0E\u003D.Value = value;
      }
    }

    /// <summary>Max.</summary>
    public Decimal? Max
    {
      get
      {
        return this.\u0023\u003Dz_nCHtsE\u003D.Value;
      }
      set
      {
        this.\u0023\u003Dz_nCHtsE\u003D.Value = value;
      }
    }

    private void \u0023\u003Dz8b17eddFxrQT(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzeCmqHLzxP9Mg = new Decimal?(_param1.GetValue<Decimal>());
    }

    private void \u0023\u003Dzd_4pjLI\u003D(DiagramSocketValue _param1)
    {
      if (!this.\u0023\u003DzeCmqHLzxP9Mg.HasValue)
        return;
      IEnumerable<Security> values = (IEnumerable<Security>) _param1.Value;
      if (values == null)
      {
        this.Strategy.ChildStrategies.RemoveRange<Strategy>((IEnumerable<Strategy>) this.\u0023\u003DzzZYEjZYHg4TbxVeymZqhPPkEOATr.Values);
        this.\u0023\u003DzzZYEjZYHg4TbxVeymZqhPPkEOATr.Clear();
      }
      else
      {
        ISet<Security> set = values.ToSet<Security>();
        foreach (KeyValuePair<Security, Strategy> keyValuePair in this.\u0023\u003DzzZYEjZYHg4TbxVeymZqhPPkEOATr.ToArray<KeyValuePair<Security, Strategy>>())
        {
          if (!set.Remove(keyValuePair.Key))
          {
            this.Strategy.ChildStrategies.Remove(keyValuePair.Value);
            this.\u0023\u003DzzZYEjZYHg4TbxVeymZqhPPkEOATr.Remove(keyValuePair.Key);
          }
        }
        foreach (Security key in (IEnumerable<Security>) set)
        {
          Decimal? nullable;
          Strategy strategy;
          switch (this.QuotingType)
          {
            case OptionsQuotingTypes.Volalitity:
              int quotingSide1 = (int) this.QuotingSide;
              Decimal quotingVolume1 = this.\u0023\u003DzeCmqHLzxP9Mg.Value;
              nullable = this.Min;
              Decimal valueOrDefault1 = nullable.GetValueOrDefault();
              nullable = this.Max;
              Decimal max1 = nullable ?? new Decimal(100);
              Range<Decimal> ivRange = new Range<Decimal>(valueOrDefault1, max1);
              IExchangeInfoProvider exchangeInfoProvider = ServicesRegistry.ExchangeInfoProvider;
              strategy = (Strategy) new VolatilityQuotingStrategy((Sides) quotingSide1, quotingVolume1, ivRange, exchangeInfoProvider);
              break;
            case OptionsQuotingTypes.TheorPrice:
              int quotingSide2 = (int) this.QuotingSide;
              Decimal quotingVolume2 = this.\u0023\u003DzeCmqHLzxP9Mg.Value;
              nullable = this.Min;
              Unit valueOrDefault2 = (Unit) nullable.GetValueOrDefault();
              nullable = this.Max;
              Unit max2 = (Unit) (nullable ?? new Decimal(-1, -1, -1, false, (byte) 0));
              Range<Unit> theorPriceOffset = new Range<Unit>(valueOrDefault2, max2);
              strategy = (Strategy) new TheorPriceQuotingStrategy((Sides) quotingSide2, quotingVolume2, theorPriceOffset);
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof(-1260193993), (object) this.QuotingType, LocalizedStrings.Str1219);
          }
          strategy.Security = key;
          strategy.WhenOrderRegistered().Do(new Action<Order>(this.\u0023\u003DzTN1u5fIADGDoSMl9wxqZEgw\u003D)).Apply<Strategy, Order>((IMarketRuleContainer) this.Strategy);
          this.\u0023\u003DzzZYEjZYHg4TbxVeymZqhPPkEOATr.Add(key, strategy);
          this.Strategy.ChildStrategies.Add(strategy);
        }
      }
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      this.\u0023\u003DzzZYEjZYHg4TbxVeymZqhPPkEOATr.Clear();
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      this.Strategy.ChildStrategies.RemoveRange<Strategy>((IEnumerable<Strategy>) this.\u0023\u003DzzZYEjZYHg4TbxVeymZqhPPkEOATr.Values);
      base.OnStop();
    }

    private void \u0023\u003DzTN1u5fIADGDoSMl9wxqZEgw\u003D(Order _param1)
    {
      this.RaiseProcessOutput(_param1.LastChangeTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
    }
  }
}
