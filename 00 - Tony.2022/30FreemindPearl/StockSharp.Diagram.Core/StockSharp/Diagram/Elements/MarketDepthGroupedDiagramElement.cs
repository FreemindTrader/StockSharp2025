﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.MarketDepthGroupedDiagramElement
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
  /// <summary>Grouped market depth element.</summary>
  [Doc("topics/Designer_Depth_Grouped.html")]
  [DescriptionLoc("GroupedMarketDepth", false)]
  [CategoryLoc("MarketDepths")]
  [DisplayNameLoc("GroupedMarketDepth")]
  public class MarketDepthGroupedDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260196954).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196963);
    
    private readonly DiagramElementParam<int> \u0023\u003DzgaVV8Al5zZHv6Yjbvw\u003D\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.MarketDepthGroupedDiagramElement" />.
    /// </summary>
    public MarketDepthGroupedDiagramElement()
    {
      this.AddInput(StaticSocketIds.MarketDepth, LocalizedStrings.MarketDepth, DiagramSocketType.MarketDepth, new Action<DiagramSocketValue>(this.\u0023\u003Dz8DMCidMTrlWRQBiSVQ\u003D\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.MarketDepth, DiagramSocketType.MarketDepth, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzgaVV8Al5zZHv6Yjbvw\u003D\u003D = this.AddParam<int>(nameof(-1260197268), 5).SetDisplay<DiagramElementParam<int>>(LocalizedStrings.Str3131, LocalizedStrings.PriceRange, LocalizedStrings.PriceRange, 10);
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

    /// <summary>Price range.</summary>
    public int PriceRange
    {
      get
      {
        return this.\u0023\u003DzgaVV8Al5zZHv6Yjbvw\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzgaVV8Al5zZHv6Yjbvw\u003D\u003D.Value = value;
      }
    }

    private void \u0023\u003Dz8DMCidMTrlWRQBiSVQ\u003D\u003D(DiagramSocketValue _param1)
    {
      MarketDepth depth = _param1.GetValue<MarketDepth>();
      MarketDepth marketDepth = depth.Group((Decimal) this.PriceRange.Pips(depth.Security));
      this.RaiseProcessOutput(_param1.Time, (object) marketDepth, _param1, (Subscription) null);
    }
  }
}
