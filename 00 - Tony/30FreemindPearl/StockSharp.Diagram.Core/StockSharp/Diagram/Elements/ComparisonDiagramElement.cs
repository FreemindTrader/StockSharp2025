// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.ComparisonDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Two values comparison element.</summary>
  [Doc("topics/Designer_Comparison.html")]
  [DescriptionLoc("TwoValuesComparisonElement", false)]
  [DisplayNameLoc("Comparison")]
  [CategoryLoc("Common")]
  public sealed class ComparisonDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260196143).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196220);
    
    private readonly DiagramElementParam<DiagramSocket> \u0023\u003Dz5u8yFAoxzQAQ;
    
    private readonly DiagramElementParam<ComparisonOperator?> \u0023\u003DzqebnDAw\u003D;
    
    private readonly DiagramElementParam<DiagramSocket> \u0023\u003DzSVzhlkB\u0024DIK1;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.ComparisonDiagramElement" />.
    /// </summary>
    public ComparisonDiagramElement()
    {
      DiagramSocket diagramSocket1 = this.AddInput(StaticSocketIds.Input, string.Concat(LocalizedStrings.Str3099, nameof(-1260196201)), DiagramSocketType.Comparable, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      DiagramSocket diagramSocket2 = this.AddInput(StaticSocketIds.SecondInput, string.Concat(LocalizedStrings.Str3099, nameof(-1260197524)), DiagramSocketType.Comparable, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Signal, LocalizedStrings.Signal, DiagramSocketType.Bool, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003Dz5u8yFAoxzQAQ = this.AddParam<DiagramSocket>(nameof(-1260197531), diagramSocket1).SetDisplay<DiagramElementParam<DiagramSocket>>(LocalizedStrings.Str154, LocalizedStrings.Left, LocalizedStrings.LeftOperand, 10).SetEditor<DiagramElementParam<DiagramSocket>>((Attribute) new TemplateEditorAttribute()
      {
        TemplateKey = nameof(-1260197515)
      }).SetSaveLoadHandlers<DiagramSocket>(new Func<DiagramSocket, SettingsStorage>(ComparisonDiagramElement.\u0023\u003DzRsMmMuFn62Sl), new Func<SettingsStorage, DiagramSocket>(this.\u0023\u003DzLelECTsrq6DV));
      this.\u0023\u003DzqebnDAw\u003D = this.AddParam<ComparisonOperator?>(nameof(-1260197550), new ComparisonOperator?()).SetDisplay<DiagramElementParam<ComparisonOperator?>>(LocalizedStrings.Str154, LocalizedStrings.Operator, LocalizedStrings.EqualityOperator, 20).SetOnValueChangedHandler<ComparisonOperator?>(new Action<ComparisonOperator?>(this.\u0023\u003Dz1DFoGRVGMJirY7xLCgN5OQM\u003D)).SetIsParam<DiagramElementParam<ComparisonOperator?>>();
      this.\u0023\u003DzSVzhlkB\u0024DIK1 = this.AddParam<DiagramSocket>(nameof(-1260197595), diagramSocket2).SetDisplay<DiagramElementParam<DiagramSocket>>(LocalizedStrings.Str154, LocalizedStrings.Right, LocalizedStrings.RightOperand, 30).SetEditor<DiagramElementParam<DiagramSocket>>((Attribute) new TemplateEditorAttribute()
      {
        TemplateKey = nameof(-1260197515)
      }).SetSaveLoadHandlers<DiagramSocket>(new Func<DiagramSocket, SettingsStorage>(ComparisonDiagramElement.\u0023\u003DzRsMmMuFn62Sl), new Func<SettingsStorage, DiagramSocket>(this.\u0023\u003DzLelECTsrq6DV));
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

    /// <summary>Left operand.</summary>
    public DiagramSocket LeftValue
    {
      get
      {
        return this.\u0023\u003Dz5u8yFAoxzQAQ.Value;
      }
      set
      {
        this.\u0023\u003Dz5u8yFAoxzQAQ.Value = value;
      }
    }

    /// <summary>Operator.</summary>
    public ComparisonOperator? Operator
    {
      get
      {
        return this.\u0023\u003DzqebnDAw\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzqebnDAw\u003D.Value = value;
      }
    }

    /// <summary>Right operand.</summary>
    public DiagramSocket RightValue
    {
      get
      {
        return this.\u0023\u003DzSVzhlkB\u0024DIK1.Value;
      }
      set
      {
        this.\u0023\u003DzSVzhlkB\u0024DIK1.Value = value;
      }
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if (!this.Operator.HasValue)
        throw new InvalidOperationException(LocalizedStrings.NotInitializedParams.Put((object[]) new object[1]{ (object) LocalizedStrings.Comparison }));
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnProcess(
      DateTimeOffset time,
      IDictionary<DiagramSocket, DiagramSocketValue> values,
      DiagramSocketValue source)
    {
      if (!this.Operator.HasValue)
        throw new InvalidOperationException(LocalizedStrings.NotInitializedParams.Put((object[]) new object[1]{ (object) LocalizedStrings.Comparison }));
      DiagramSocketValue diagramSocketValue1 = values[this.\u0023\u003Dz5u8yFAoxzQAQ.Value];
      DiagramSocketValue diagramSocketValue2 = values[this.\u0023\u003DzSVzhlkB\u0024DIK1.Value];
      Unit unit1 = diagramSocketValue1.GetValue<Unit>();
      Unit unit2 = diagramSocketValue2.GetValue<Unit>();
      ComparisonOperator? nullable = this.Operator;
      if (nullable.HasValue)
      {
        bool flag;
        switch (nullable.GetValueOrDefault())
        {
          case ComparisonOperator.Equal:
            flag = unit1.Compare((object) unit2) == 0;
            break;
          case ComparisonOperator.NotEqual:
            flag = (uint) unit1.Compare((object) unit2) > 0U;
            break;
          case ComparisonOperator.Greater:
            flag = unit1.Compare((object) unit2) == 1;
            break;
          case ComparisonOperator.GreaterOrEqual:
            flag = unit1.Compare((object) unit2) >= 0;
            break;
          case ComparisonOperator.Less:
            flag = unit1.Compare((object) unit2) == -1;
            break;
          case ComparisonOperator.LessOrEqual:
            flag = unit1.Compare((object) unit2) <= 0;
            break;
          case ComparisonOperator.Any:
            flag = true;
            break;
          default:
            goto label_11;
        }
        this.RaiseProcessOutput(time, (object) flag, source, (Subscription) null);
        return;
      }
label_11:
      throw new InvalidOperationException(this.Operator.To<string>());
    }

    private static SettingsStorage \u0023\u003DzRsMmMuFn62Sl(DiagramSocket _param0)
    {
      if (_param0 == null)
        return (SettingsStorage) null;
      SettingsStorage settingsStorage = new SettingsStorage();
      settingsStorage.SetValue<string>(nameof(-1260199113), _param0.Id);
      return settingsStorage;
    }

    private DiagramSocket \u0023\u003DzLelECTsrq6DV(SettingsStorage _param1)
    {
      string id = _param1.GetValue<string>(nameof(-1260199113), (string) null);
      if (id == null)
        return (DiagramSocket) null;
      return this.InputSockets.FindById(id);
    }

    private void \u0023\u003Dz1DFoGRVGMJirY7xLCgN5OQM\u003D(ComparisonOperator? _param1)
    {
      this.SetElementName(_param1.HasValue ? _param1.GetValueOrDefault().GetDisplayName() : (string) null);
    }
  }
}
