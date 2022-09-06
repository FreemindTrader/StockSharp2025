// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.PassThroughDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Localization;
using System;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Combined values element.</summary>
  [DescriptionLoc("Str3136", false)]
  [CategoryLoc("Common")]
  [Doc("topics/Designer_Association.html")]
  [DisplayNameLoc("Str3135")]
  public class PassThroughDiagramElement : TypedDiagramElement<PassThroughDiagramElement>
  {
    
    private readonly Guid _typeId = nameof(-1260195613).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195626);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.PassThroughDiagramElement" />.
    /// </summary>
    public PassThroughDiagramElement()
      : base(LocalizedStrings.Common)
    {
      this.InputSockets.First<DiagramSocket>().LinkableMaximum = int.MaxValue;
      this.SetTypes(DiagramSocketType.AllTypes);
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

    /// <inheritdoc />
    protected override void TypeChanged()
    {
      this.UpdateOutputSocketType();
    }

    /// <inheritdoc />
    protected override void OnProcess(DiagramSocketValue value)
    {
      this.RaiseProcessOutput(value.Time, value.Value, value, (Subscription) null);
    }
  }
}
