// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.PreviousValueDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Previous value receiving element.</summary>
  [CategoryLoc("Common")]
  [Doc("topics/Designer_Previous_value.html")]
  [DescriptionLoc("Str3141", false)]
  [DisplayNameLoc("Str3140")]
  public class PreviousValueDiagramElement : TypedDiagramElement<PreviousValueDiagramElement>
  {
    
    private readonly Queue<object> \u0023\u003DztMGIL0E\u003D = new Queue<object>();
    
    private readonly Guid _typeId = nameof(-1260195030).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195071);
    
    private readonly DiagramElementParam<int> \u0023\u003Dz25tBf6M\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.PreviousValueDiagramElement" />.
    /// </summary>
    public PreviousValueDiagramElement()
      : base(LocalizedStrings.Common)
    {
      this.\u0023\u003Dz25tBf6M\u003D = this.AddParam<int>(nameof(-1260195052), 1).SetDisplay<DiagramElementParam<int>>(LocalizedStrings.Common, LocalizedStrings.Str841, LocalizedStrings.Str3142, 30).SetIsParam<DiagramElementParam<int>>().SetOnValueChangingHandler<int>(PreviousValueDiagramElement.LamdaShit.Function01 ?? (PreviousValueDiagramElement.LamdaShit.Function01 = new Action<int, int>(PreviousValueDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzAUahV67X0_TXdY4O7RvSOiE\u003D))).SetOnValueChangedHandler<int>(new Action<int>(this.\u0023\u003DzkX6Vrq7235y0ZpLwg1dDzaE\u003D));
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

    /// <summary>Shift.</summary>
    public int Shift
    {
      get
      {
        return this.\u0023\u003Dz25tBf6M\u003D.Value;
      }
      set
      {
        this.\u0023\u003Dz25tBf6M\u003D.Value = value;
      }
    }

    /// <inheritdoc />
    protected override void TypeChanged()
    {
      this.UpdateOutputSocketType();
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if ((Equatable<DiagramSocketType>) this.Type == (DiagramSocketType) null)
        throw new InvalidOperationException(LocalizedStrings.NotInitializedParams.Put((object[]) new object[1]{ (object) LocalizedStrings.Str3143 }));
      this.\u0023\u003DztMGIL0E\u003D.Clear();
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnProcess(DiagramSocketValue value)
    {
      bool flag = false;
      object obj = value.Value;
      Candle candle = obj as Candle;
      if (candle == null)
      {
        IIndicatorValue indicatorValue = obj as IIndicatorValue;
        if (indicatorValue != null)
          flag = !indicatorValue.IsFinal;
      }
      else
        flag = candle.State != CandleStates.Finished;
      if (flag)
        return;
      if (this.\u0023\u003DztMGIL0E\u003D.Count >= this.Shift)
        this.RaiseProcessOutput(value.Time, this.\u0023\u003DztMGIL0E\u003D.Dequeue(), value, (Subscription) null);
      this.\u0023\u003DztMGIL0E\u003D.Enqueue(value.Value);
    }

    private void \u0023\u003DzkX6Vrq7235y0ZpLwg1dDzaE\u003D(int _param1)
    {
      this.SetElementName(LocalizedStrings.ShiftIs.Put((object[]) new object[1]
      {
        (object) _param1
      }));
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly PreviousValueDiagramElement.LamdaShit _lamdaShit = new PreviousValueDiagramElement.LamdaShit();
      public static Action<int, int> Function01;

      internal void \u0023\u003DzAUahV67X0_TXdY4O7RvSOiE\u003D(int _param1, int _param2)
      {
        if (_param2 < 1)
          throw new ArgumentOutOfRangeException(nameof(-1260194985), (object) _param2, LocalizedStrings.Str1219);
      }
    }
  }
}
