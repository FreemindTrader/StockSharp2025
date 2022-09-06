// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.MarketDepthTruncateDiagramElement
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
  /// <summary>Truncate market depth element.</summary>
  [DescriptionLoc("TruncatedBookDesc", false)]
  [DisplayNameLoc("TruncatedBook")]
  [CategoryLoc("MarketDepths")]
  [Doc("topics/Designer_MarketDepthTruncateDiagramElement.html")]
  public class MarketDepthTruncateDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260197352).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197169);
    
    private readonly DiagramElementParam<int> \u0023\u003DzOAh\u00244WO8kuz3;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.MarketDepthTruncateDiagramElement" />.
    /// </summary>
    public MarketDepthTruncateDiagramElement()
    {
      this.AddInput(StaticSocketIds.MarketDepth, LocalizedStrings.MarketDepth, DiagramSocketType.MarketDepth, new Action<DiagramSocketValue>(this.\u0023\u003Dz8DMCidMTrlWRQBiSVQ\u003D\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.MarketDepth, DiagramSocketType.MarketDepth, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzOAh\u00244WO8kuz3 = this.AddParam<int>(nameof(-1260197182), 20).SetDisplay<DiagramElementParam<int>>(LocalizedStrings.Str3131, LocalizedStrings.Str1660, LocalizedStrings.MaxDepthOfBook, 10).SetOnValueChangingHandler<int>(MarketDepthTruncateDiagramElement.LamdaShit.Func00023 ?? (MarketDepthTruncateDiagramElement.LamdaShit.Func00023 = new Action<int, int>(MarketDepthTruncateDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D)));
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

    /// <summary>Max depth.</summary>
    public int MaxDepth
    {
      get
      {
        return this.\u0023\u003DzOAh\u00244WO8kuz3.Value;
      }
      set
      {
        this.\u0023\u003DzOAh\u00244WO8kuz3.Value = value;
      }
    }

    private void \u0023\u003Dz8DMCidMTrlWRQBiSVQ\u003D\u003D(DiagramSocketValue _param1)
    {
      MarketDepth depth = _param1.GetValue<MarketDepth>();
      this.RaiseProcessOutput(_param1.Time, (object) depth.Truncate(this.MaxDepth), _param1, (Subscription) null);
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly MarketDepthTruncateDiagramElement.LamdaShit _lamdaShit = new MarketDepthTruncateDiagramElement.LamdaShit();
      public static Action<int, int> Func00023;

      internal void \u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D(int _param1, int _param2)
      {
        if (_param2 < 0)
          throw new InvalidOperationException(LocalizedStrings.Str941);
      }
    }
  }
}
