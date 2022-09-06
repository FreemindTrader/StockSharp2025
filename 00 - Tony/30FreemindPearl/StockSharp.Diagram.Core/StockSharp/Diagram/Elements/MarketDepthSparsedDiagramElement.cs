// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.MarketDepthSparsedDiagramElement
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
  /// <summary>Sparsed market depth element.</summary>
  [CategoryLoc("MarketDepths")]
  [Doc("topics/Designer_Depth_Spread.html")]
  [DescriptionLoc("SparsedMarketDepth", false)]
  [DisplayNameLoc("SparsedMarketDepth")]
  public class MarketDepthSparsedDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260197296).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197369);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.MarketDepthSparsedDiagramElement" />.
    /// </summary>
    public MarketDepthSparsedDiagramElement()
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
      MarketDepth marketDepth1 = _param1.GetValue<MarketDepth>();
      MarketDepth marketDepth2 = marketDepth1.Sparse().Join(marketDepth1);
      this.RaiseProcessOutput(_param1.Time, (object) marketDepth2, _param1, (Subscription) null);
    }
  }
}
