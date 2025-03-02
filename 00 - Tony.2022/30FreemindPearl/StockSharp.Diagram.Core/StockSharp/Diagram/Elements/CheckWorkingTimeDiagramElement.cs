// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.CheckWorkingTimeDiagramElement
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
  /// <summary>
  /// Working time verification element for a specified security.
  /// </summary>
  [DescriptionLoc("Str3085", false)]
  [CategoryLoc("Common")]
  [Doc("topics/Designer_Working_time.html")]
  [DisplayNameLoc("WorkingTime")]
  public class CheckWorkingTimeDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260196122).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196131);
    
    private Security \u0023\u003DzmirAKT8\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.CheckWorkingTimeDiagramElement" />.
    /// </summary>
    public CheckWorkingTimeDiagramElement()
    {
      this.AddInput(StaticSocketIds.Security, LocalizedStrings.Security, DiagramSocketType.Security, new Action<DiagramSocketValue>(this.\u0023\u003DzECIEXk\u0024A5Mms), 1, int.MaxValue, false, new bool?());
      this.AddInput(StaticSocketIds.Time, LocalizedStrings.Time, DiagramSocketType.Date, new Action<DiagramSocketValue>(this.\u0023\u003DzuMGGrgQ\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Output, DiagramSocketType.Bool, int.MaxValue, int.MaxValue, new bool?());
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

    private void \u0023\u003DzuMGGrgQ\u003D(DiagramSocketValue _param1)
    {
      if (this.\u0023\u003DzmirAKT8\u003D == null)
        return;
      DateTimeOffset time = (DateTimeOffset) _param1.Value;
      this.RaiseProcessOutput(_param1.Time, (object) this.\u0023\u003DzmirAKT8\u003D.Board.IsTradeTime(time), _param1, (Subscription) null);
    }

    private void \u0023\u003DzECIEXk\u0024A5Mms(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzmirAKT8\u003D = _param1.GetValue<Security>();
    }
  }
}
