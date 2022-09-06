// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OptionsStrikesDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Filtering derivatives by underlying asset diagram element.
  /// </summary>
  [DescriptionLoc("OptionsStrikesDiagramElement", false)]
  [DisplayNameLoc("Str437")]
  [Doc("topics/Designer_Derivatives.html")]
  [CategoryLoc("Options")]
  public class OptionsStrikesDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260194038).To<Guid>();
    
    private readonly string _iconName = nameof(-1260193823);
    
    private Security \u0023\u003DzBP7tu1g\u003D;
    
    private readonly DiagramElementParam<OptionTypes?> \u0023\u003DzkDfGmWTDuIE5;
    
    private readonly DiagramElementParam<DateTimeOffset?> \u0023\u003Dza2VssqSBa20o;
    
    private readonly DiagramElementParam<int?> \u0023\u003DzIxPAsrbI5uGH;
    
    private readonly DiagramElementParam<int?> \u0023\u003Dz\u00245v_X1oV3oxe;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OptionsStrikesDiagramElement" />.
    /// </summary>
    public OptionsStrikesDiagramElement()
    {
      this.AddInput(StaticSocketIds.UnderlyingAsset, LocalizedStrings.UnderlyingAsset, DiagramSocketType.Security, new Action<DiagramSocketValue>(this.\u0023\u003DzJJus3ioOTJH8), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Options, LocalizedStrings.Options, DiagramSocketType.Options, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzkDfGmWTDuIE5 = this.AddParam<OptionTypes?>(nameof(-1260193806), new OptionTypes?()).SetDisplay<DiagramElementParam<OptionTypes?>>(LocalizedStrings.Options, LocalizedStrings.Str551, string.Concat(LocalizedStrings.Str551, nameof(-1260196070)), 10);
      this.\u0023\u003Dza2VssqSBa20o = this.AddParam<DateTimeOffset?>(nameof(-1260193853), new DateTimeOffset?()).SetDisplay<DiagramElementParam<DateTimeOffset?>>(LocalizedStrings.Options, LocalizedStrings.ExpiryDate, string.Concat(LocalizedStrings.ExpiryDate, nameof(-1260196070)), 20);
      this.\u0023\u003DzIxPAsrbI5uGH = this.AddParam<int?>(nameof(-1260193876), new int?()).SetDisplay<DiagramElementParam<int?>>(LocalizedStrings.Options, string.Concat(LocalizedStrings.Strike, nameof(-1260193859)), LocalizedStrings.StrikeLeftOffset, 30);
      this.\u0023\u003Dz\u00245v_X1oV3oxe = this.AddParam<int?>(nameof(-1260193871), new int?()).SetDisplay<DiagramElementParam<int?>>(LocalizedStrings.Options, string.Concat(LocalizedStrings.Strike, nameof(-1260193889)), LocalizedStrings.StrikeRightOffset, 40);
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

    /// <summary>Option type.</summary>
    public OptionTypes? OptionType
    {
      get
      {
        return this.\u0023\u003DzkDfGmWTDuIE5.Value;
      }
      set
      {
        this.\u0023\u003DzkDfGmWTDuIE5.Value = value;
      }
    }

    /// <summary>Expiration date.</summary>
    public DateTimeOffset? ExpirationDate
    {
      get
      {
        return this.\u0023\u003Dza2VssqSBa20o.Value;
      }
      set
      {
        this.\u0023\u003Dza2VssqSBa20o.Value = value;
      }
    }

    /// <summary>Left offset.</summary>
    public int? LeftOffset
    {
      get
      {
        return this.\u0023\u003DzIxPAsrbI5uGH.Value;
      }
      set
      {
        this.\u0023\u003DzIxPAsrbI5uGH.Value = value;
      }
    }

    /// <summary>Right offset.</summary>
    public int? RightOffset
    {
      get
      {
        return this.\u0023\u003Dz\u00245v_X1oV3oxe.Value;
      }
      set
      {
        this.\u0023\u003Dz\u00245v_X1oV3oxe.Value = value;
      }
    }

    private void \u0023\u003DzJJus3ioOTJH8(DiagramSocketValue _param1)
    {
      OptionsStrikesDiagramElement.\u0023\u003DzYJibJAh3gdzvoKgRyp3tj90\u003D jah3gdzvoKgRyp3tj90 = new OptionsStrikesDiagramElement.\u0023\u003DzYJibJAh3gdzvoKgRyp3tj90\u003D();
      this.\u0023\u003DzBP7tu1g\u003D = (Security) _param1.Value;
      if (this.\u0023\u003DzBP7tu1g\u003D == null)
        return;
      IEnumerable<Security> securities1 = this.\u0023\u003DzBP7tu1g\u003D.GetDerivatives(ServicesRegistry.SecurityProvider, this.ExpirationDate);
      OptionTypes? optionType = this.OptionType;
      if (optionType.HasValue)
        securities1 = securities1.Filter(optionType.Value);
      Security[] array = securities1.ToArray<Security>();
      jah3gdzvoKgRyp3tj90.\u0023\u003DzpXfvzGorsiF7dnYQYDf0_Ec\u003D = this.\u0023\u003DzBP7tu1g\u003D.GetCentralStrike(ServicesRegistry.MarketDataProvider, (IEnumerable<Security>) array);
      int? leftOffset = this.LeftOffset;
      if (leftOffset.HasValue)
        array = ((IEnumerable<Security>) array).Where<Security>(new Func<Security, bool>(jah3gdzvoKgRyp3tj90.\u0023\u003DzQLGQX6QOyU9d_XOjyQ\u003D\u003D)).Concat<Security>(((IEnumerable<Security>) array).Where<Security>(new Func<Security, bool>(jah3gdzvoKgRyp3tj90.\u0023\u003Dzv6Y4UhgVQZ4g0G5wZA\u003D\u003D)).OrderByDescending<Security, Decimal?>(OptionsStrikesDiagramElement.LamdaShit.\u0023\u003DzxaRrqO5Xj02re3j7DA\u003D\u003D ?? (OptionsStrikesDiagramElement.LamdaShit.\u0023\u003DzxaRrqO5Xj02re3j7DA\u003D\u003D = new Func<Security, Decimal?>(OptionsStrikesDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzdfSTGUM2M7aFCJxGgo10F\u0024c\u003D))).Take<Security>(leftOffset.Value)).ToArray<Security>();
      int? rightOffset = this.RightOffset;
      if (rightOffset.HasValue)
        array = ((IEnumerable<Security>) array).Where<Security>(new Func<Security, bool>(jah3gdzvoKgRyp3tj90.\u0023\u003DzjizT0XzD5ibC0xTwrg\u003D\u003D)).Concat<Security>(((IEnumerable<Security>) array).Where<Security>(new Func<Security, bool>(jah3gdzvoKgRyp3tj90.\u0023\u003DzbczF\u0024trKJ_9xHMKgHw\u003D\u003D)).OrderBy<Security, Decimal?>(OptionsStrikesDiagramElement.LamdaShit.\u0023\u003DzGAI4DPB5HZxDZi97Aw\u003D\u003D ?? (OptionsStrikesDiagramElement.LamdaShit.\u0023\u003DzGAI4DPB5HZxDZi97Aw\u003D\u003D = new Func<Security, Decimal?>(OptionsStrikesDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz7xOjTVFiuZDN9UZE2r5rY7I\u003D))).Take<Security>(rightOffset.Value)).ToArray<Security>();
      IEnumerable<Security> securities2 = (IEnumerable<Security>) array;
      this.RaiseProcessOutput(_param1.Time, (object) securities2, _param1, (Subscription) null);
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly OptionsStrikesDiagramElement.LamdaShit _lamdaShit = new OptionsStrikesDiagramElement.LamdaShit();
      public static Func<Security, Decimal?> \u0023\u003DzxaRrqO5Xj02re3j7DA\u003D\u003D;
      public static Func<Security, Decimal?> \u0023\u003DzGAI4DPB5HZxDZi97Aw\u003D\u003D;

      internal Decimal? \u0023\u003DzdfSTGUM2M7aFCJxGgo10F\u0024c\u003D(Security _param1)
      {
        return _param1.Strike;
      }

      internal Decimal? \u0023\u003Dz7xOjTVFiuZDN9UZE2r5rY7I\u003D(Security _param1)
      {
        return _param1.Strike;
      }
    }

    private sealed class \u0023\u003DzYJibJAh3gdzvoKgRyp3tj90\u003D
    {
      public Security \u0023\u003DzpXfvzGorsiF7dnYQYDf0_Ec\u003D;

      internal bool \u0023\u003DzQLGQX6QOyU9d_XOjyQ\u003D\u003D(Security _param1)
      {
        Decimal? strike1 = _param1.Strike;
        Decimal? strike2 = this.\u0023\u003DzpXfvzGorsiF7dnYQYDf0_Ec\u003D.Strike;
        return strike1.GetValueOrDefault() >= strike2.GetValueOrDefault() & (strike1.HasValue & strike2.HasValue);
      }

      internal bool \u0023\u003Dzv6Y4UhgVQZ4g0G5wZA\u003D\u003D(Security _param1)
      {
        Decimal? strike1 = _param1.Strike;
        Decimal? strike2 = this.\u0023\u003DzpXfvzGorsiF7dnYQYDf0_Ec\u003D.Strike;
        return strike1.GetValueOrDefault() < strike2.GetValueOrDefault() & (strike1.HasValue & strike2.HasValue);
      }

      internal bool \u0023\u003DzjizT0XzD5ibC0xTwrg\u003D\u003D(Security _param1)
      {
        Decimal? strike1 = _param1.Strike;
        Decimal? strike2 = this.\u0023\u003DzpXfvzGorsiF7dnYQYDf0_Ec\u003D.Strike;
        return strike1.GetValueOrDefault() <= strike2.GetValueOrDefault() & (strike1.HasValue & strike2.HasValue);
      }

      internal bool \u0023\u003DzbczF\u0024trKJ_9xHMKgHw\u003D\u003D(Security _param1)
      {
        Decimal? strike1 = _param1.Strike;
        Decimal? strike2 = this.\u0023\u003DzpXfvzGorsiF7dnYQYDf0_Ec\u003D.Strike;
        return strike1.GetValueOrDefault() > strike2.GetValueOrDefault() & (strike1.HasValue & strike2.HasValue);
      }
    }
  }
}
