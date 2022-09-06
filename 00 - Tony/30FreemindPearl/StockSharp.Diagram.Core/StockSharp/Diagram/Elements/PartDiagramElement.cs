// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.PartDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Reflection;
using StockSharp.Algo;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Composite value of a complex object receiving element.
  /// </summary>
  [CategoryLoc("Converter")]
  [DescriptionLoc("Str3132", false)]
  [DisplayNameLoc("Str3131")]
  [Doc("topics/Designer_Converter.html")]
  public class PartDiagramElement : TypedDiagramElement<PartDiagramElement>
  {
    
    private readonly Guid _typeId = nameof(-1260195763).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195808);
    
    private readonly DiagramElementParam<string> \u0023\u003DzgxSMq\u0024A\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.PartDiagramElement" />.
    /// </summary>
    public PartDiagramElement()
      : base(LocalizedStrings.Str3131)
    {
      this.\u0023\u003DzgxSMq\u0024A\u003D = this.AddParam<string>(nameof(-1260195785), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Str3131, LocalizedStrings.Str3133, LocalizedStrings.Str3134, 20).SetEditor<DiagramElementParam<string>>((Attribute) new TemplateEditorAttribute()
      {
        TemplateKey = nameof(-1260195834)
      }).SetOnValueChangedHandler<string>(new Action<string>(this.\u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D));
      this.SetTypes(DiagramSocketType.AllTypes.Where<DiagramSocketType>(PartDiagramElement.LamdaShit.\u0023\u003DzQdkmixKRua\u0024x71IVPQ\u003D\u003D ?? (PartDiagramElement.LamdaShit.\u0023\u003DzQdkmixKRua\u0024x71IVPQ\u003D\u003D = new Func<DiagramSocketType, bool>(PartDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz5EcDcWh2upEudvPnd7QuaPw\u003D))));
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

    /// <summary>Property.</summary>
    public string Property
    {
      get
      {
        return this.\u0023\u003DzgxSMq\u0024A\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzgxSMq\u0024A\u003D.Value = value;
      }
    }

    /// <inheritdoc />
    protected override void TypeChanged()
    {
      this.Property = (string) null;
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if (this.Property == null)
        throw new InvalidOperationException(LocalizedStrings.NotInitializedParams.Put((object[]) new object[1]{ (object) LocalizedStrings.Str3133 }));
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnProcess(DiagramSocketValue value)
    {
      ComplexIndicatorValue complexIndicatorValue = value.Value as ComplexIndicatorValue;
      object obj;
      if (complexIndicatorValue != null)
      {
        obj = PartDiagramElement.\u0023\u003DzHIm18zoSq2O1(complexIndicatorValue, this.Property);
        if (obj == null)
          return;
      }
      else
        obj = value.Value.GetPropValue(this.Property);
      this.RaiseProcessOutput(value.Time, obj, value, (Subscription) null);
    }

    private static object \u0023\u003DzHIm18zoSq2O1(ComplexIndicatorValue _param0, string _param1)
    {
      object obj = (object) _param0;
      foreach (string name in ((IEnumerable<string>) ((string) _param1).Split('.')).Skip<string>(1))
      {
        ComplexIndicatorValue complexIndicatorValue = (ComplexIndicatorValue) obj;
        IIndicator propValue = (IIndicator) complexIndicatorValue.Indicator.GetPropValue(name);
        IIndicatorValue indicatorValue;
        if (propValue == null || !complexIndicatorValue.InnerValues.TryGetValue(propValue, out indicatorValue))
          return (object) null;
        obj = (object) indicatorValue;
      }
      return obj;
    }

    private static string \u0023\u003DzgLLV2CHlpHDW(Type _param0, string _param1)
    {
      string str0 = _param0.GetDisplayName((string) null);
      string str = _param1;
      char[] chArray = new char[1]{ '.' };
      foreach (string name in ((string) str).Split(chArray))
      {
        PropertyInfo property = _param0.GetProperty(name);
        if (!(property == (PropertyInfo) null))
        {
          _param0 = property.PropertyType;
          str0 = string.Concat(str0, nameof(-1260197653), property.GetDisplayName((string) null));
        }
      }
      return str0;
    }

    private void \u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D(string _param1)
    {
      DiagramSocket socket = this.OutputSockets.First<DiagramSocket>();
      if ((Equatable<DiagramSocketType>) this.Type != (DiagramSocketType) null && _param1 != null)
      {
        if ((Equatable<DiagramSocketType>) this.Type == DiagramSocketType.IndicatorValue)
        {
          socket.Type = DiagramSocketType.IndicatorValue;
          this.SetElementName(_param1);
        }
        else
        {
          socket.Type = DiagramSocketType.GetSocketType(this.Type.Type.GetPropType(_param1));
          this.SetElementName(PartDiagramElement.\u0023\u003DzgLLV2CHlpHDW(this.Type.Type, _param1));
        }
      }
      else
      {
        socket.Type = DiagramSocketType.Any;
        this.SetElementName((string) null);
      }
      this.RaiseSocketChanged(socket);
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly PartDiagramElement.LamdaShit _lamdaShit = new PartDiagramElement.LamdaShit();
      public static Func<DiagramSocketType, bool> \u0023\u003DzQdkmixKRua\u0024x71IVPQ\u003D\u003D;

      internal bool \u0023\u003Dz5EcDcWh2upEudvPnd7QuaPw\u003D(DiagramSocketType _param1)
      {
        if ((!_param1.Type.IsPrimitive() || _param1.Type == typeof (DateTimeOffset)) && (!_param1.Type.IsEnum() && _param1.Type != typeof (Unit)) && (_param1.Type != typeof (object) && (_param1.Type == typeof (MarketDepth) || !_param1.Type.IsCollection())))
          return _param1.Type != typeof (IComparable);
        return false;
      }
    }
  }
}
