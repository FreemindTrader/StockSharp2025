// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.ChartDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Chart panel element (candles display area, indicators, orders and trades).
  /// </summary>
  [CategoryLoc("Common")]
  [DisplayNameLoc("ChartPanel")]
  [Doc("topics/Designer_Panel_graphics.html")]
  [DescriptionLoc("ChartPanelElement", false)]
  [Browsable(false)]
  public class ChartDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260196260).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196301);
    
    private readonly DiagramElementParam<bool> \u0023\u003Dz1lBbasD5\u0024DDUGtzxkXZ673aIU58A;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.ChartDiagramElement" />.
    /// </summary>
    public ChartDiagramElement()
    {
      this.\u0023\u003Dz1lBbasD5\u0024DDUGtzxkXZ673aIU58A = this.AddParam<bool>(nameof(-1260196348), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Common, LocalizedStrings.NonFormed, LocalizedStrings.ShowNonFormedIndicators, 90);
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

    /// <summary>Show non formed indicators values.</summary>
    public bool ShowNonFormedIndicators
    {
      get
      {
        return this.\u0023\u003Dz1lBbasD5\u0024DDUGtzxkXZ673aIU58A.Value;
      }
      set
      {
        this.\u0023\u003Dz1lBbasD5\u0024DDUGtzxkXZ673aIU58A.Value = value;
      }
    }
  }
}
