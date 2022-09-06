// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.IndicatorDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Reflection;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Indicator element.</summary>
  [Doc("topics/Designer_Indicator.html")]
  [CategoryLoc("Common")]
  [DescriptionLoc("IndicatorElem", false)]
  [DisplayNameLoc("Str1981")]
  public class IndicatorDiagramElement : DiagramElement
  {
    
    private IDiagramElementParam[] \u0023\u003DzcbJ4wLnibf7eTdF2fE9f72Y\u003D = Array.Empty<IDiagramElementParam>();
    
    private readonly Guid _typeId = nameof(-1260197821).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197834);
    
    private DiagramSocket \u0023\u003DzhqFlwtCpv6Mt;
    
    private DiagramSocketValue _value;
    
    private DiagramSocketValue \u0023\u003Dzzi_c5vUBL8kM;
    
    private readonly DiagramElementParam<IndicatorType> _type;
    
    private IIndicator \u0023\u003DzqRsz5SaL3_acatTwlw\u003D\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzeU2L3uu4sNY9;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzinH2zoOykZyn;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.IndicatorDiagramElement" />.
    /// </summary>
    public IndicatorDiagramElement()
    {
      this.AddInput(StaticSocketIds.Input, LocalizedStrings.Input, DiagramSocketType.Any, new Action<DiagramSocketValue>(this.\u0023\u003DzHMHk24A\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Output, DiagramSocketType.IndicatorValue, int.MaxValue, int.MaxValue, new bool?());
      this._type = this.AddParam<IndicatorType>(nameof(-1260198464), (IndicatorType) null).SetDisplay<DiagramElementParam<IndicatorType>>(LocalizedStrings.Str1981, LocalizedStrings.Type, LocalizedStrings.IndicatorType, 10).SetEditor<DiagramElementParam<IndicatorType>>((Attribute) new EditorAttribute(typeof (IIndicatorProvider), typeof (IIndicatorProvider))).SetOnValueChangedHandler<IndicatorType>(new Action<IndicatorType>(this.\u0023\u003Dz7USMd7fQEq_CrHxFKZ\u0024uNyo\u003D)).SetSaveLoadHandlers<IndicatorType>(IndicatorDiagramElement.LamdaShit.\u0023\u003DzFG6KwefeX7LNqVKtIQ\u003D\u003D ?? (IndicatorDiagramElement.LamdaShit.\u0023\u003DzFG6KwefeX7LNqVKtIQ\u003D\u003D = new Func<IndicatorType, SettingsStorage>(IndicatorDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzN46n3Mc1zpz0HSrVCv\u0024heL4\u003D)), IndicatorDiagramElement.LamdaShit.\u0023\u003DzaiHoZMxbIlpZFye10g\u003D\u003D ?? (IndicatorDiagramElement.LamdaShit.\u0023\u003DzaiHoZMxbIlpZFye10g\u003D\u003D = new Func<SettingsStorage, IndicatorType>(IndicatorDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzZAuwJxJoE9mZ4pbsJotsp38\u003D)));
      this.\u0023\u003DzeU2L3uu4sNY9 = this.AddParam<bool>(nameof(-1260197882), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str1981, LocalizedStrings.Final, LocalizedStrings.SendOnlyFinal, 80);
      this.\u0023\u003DzinH2zoOykZyn = this.AddParam<bool>(nameof(-1260197864), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str1981, LocalizedStrings.Formed, LocalizedStrings.SendOnlyFormedIndicators, 90);
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

    /// <summary>Indicator type.</summary>
    public IndicatorType Type
    {
      get
      {
        return this._type.Value;
      }
      set
      {
        this._type.Value = value;
      }
    }

    /// <summary>The indicator parameters.</summary>
    public IIndicator Indicator
    {
      get
      {
        return this.\u0023\u003DzqRsz5SaL3_acatTwlw\u003D\u003D;
      }
      set
      {
        this.\u0023\u003DzqRsz5SaL3_acatTwlw\u003D\u003D = value;
      }
    }

    /// <summary>Send only final values.</summary>
    public bool IsFinal
    {
      get
      {
        return this.\u0023\u003DzeU2L3uu4sNY9.Value;
      }
      set
      {
        this.\u0023\u003DzeU2L3uu4sNY9.Value = value;
      }
    }

    /// <summary>Send values only when the indicator is formed.</summary>
    public bool IsFormed
    {
      get
      {
        return this.\u0023\u003DzinH2zoOykZyn.Value;
      }
      set
      {
        this.\u0023\u003DzinH2zoOykZyn.Value = value;
      }
    }

    private IEnumerable<IDiagramElementParam> \u0023\u003DzuLakOrJ8c0iX(
      IIndicator _param1,
      string _param2,
      string _param3,
      int _param4)
    {
      System.Type type = ((object) _param1).GetType();
      List<IDiagramElementParam> diagramElementParamList = new List<IDiagramElementParam>();
      System.Type[] typeArray = Array.Empty<System.Type>();
      foreach (PropertyInfo member in type.GetMembers<PropertyInfo>(BindingFlags.Instance | BindingFlags.Public, typeArray))
      {
        if (member.PropertyType == typeof (int))
          diagramElementParamList.Add((IDiagramElementParam) this.\u0023\u003Dz4nKEoaU\u003D<int>(_param1, member, _param2, _param3, _param4 + diagramElementParamList.Count));
        else if (member.PropertyType == typeof (Decimal))
          diagramElementParamList.Add((IDiagramElementParam) this.\u0023\u003Dz4nKEoaU\u003D<Decimal>(_param1, member, _param2, _param3, _param4 + diagramElementParamList.Count));
        else if (typeof (IIndicator).IsAssignableFrom(member.PropertyType))
        {
          IIndicator indicator = (IIndicator) member.GetValue((object) _param1, (object[]) null);
          string displayName = member.GetDisplayName((string) null);
          diagramElementParamList.AddRange(this.\u0023\u003DzuLakOrJ8c0iX(indicator, string.Concat(_param2, member.Name, nameof(-1260196070)), string.Concat(_param3, displayName, nameof(-1260197653)), _param4 + diagramElementParamList.Count));
        }
      }
      return (IEnumerable<IDiagramElementParam>) diagramElementParamList;
    }

    private DiagramElementParam<T> \u0023\u003Dz4nKEoaU\u003D<T>(
      IIndicator _param1,
      PropertyInfo _param2,
      string _param3,
      string _param4,
      int _param5)
    {
      IndicatorDiagramElement.\u0023\u003DzQqbuv_O8S\u0024wKvIp0p5SDXT0\u003D<T> o8SWKvIp0p5SdxT0 = new IndicatorDiagramElement.\u0023\u003DzQqbuv_O8S\u0024wKvIp0p5SDXT0\u003D<T>();
      o8SWKvIp0p5SdxT0.\u0023\u003DzQ4Scaxo\u003D = _param2;
      o8SWKvIp0p5SdxT0.\u0023\u003DzU3DMvZ2irHYOdMo\u0024GA\u003D\u003D = _param1;
      o8SWKvIp0p5SdxT0._diagramElement = this;
      T obj = (T) o8SWKvIp0p5SdxT0.\u0023\u003DzQ4Scaxo\u003D.GetValue((object) o8SWKvIp0p5SdxT0.\u0023\u003DzU3DMvZ2irHYOdMo\u0024GA\u003D\u003D, (object[]) null);
      string displayName = o8SWKvIp0p5SdxT0.\u0023\u003DzQ4Scaxo\u003D.GetDisplayName((string) null);
      return new DiagramElementParam<T>() { Name = string.Concat(_param3, o8SWKvIp0p5SdxT0.\u0023\u003DzQ4Scaxo\u003D.Name), Value = obj }.SetDisplay<DiagramElementParam<T>>(LocalizedStrings.Str1981, string.Concat(_param4, displayName), string.Concat(_param4, displayName, nameof(-1260196070)), _param5).SetIsParam<DiagramElementParam<T>>().SetOnValueChangedHandler<T>(new Action<T>(o8SWKvIp0p5SdxT0.\u0023\u003DzrDydH1\u0024XTdY9C7dSoQ\u003D\u003D));
    }

    /// <inheritdoc />
    protected override void OnReseted()
    {
      this.Indicator?.Reset();
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if ((Equatable<IndicatorType>) this.Type == (IndicatorType) null)
        throw new InvalidOperationException(LocalizedStrings.NotInitializedParams.Put((object[]) new object[1]{ (object) LocalizedStrings.Str1981 }));
      base.OnStart();
    }

    private void \u0023\u003DzHMHk24A\u003D(DiagramSocketValue _param1)
    {
      if (this.Indicator == null)
        return;
      if (this.\u0023\u003DzhqFlwtCpv6Mt != null)
      {
        this._value = _param1;
        this.\u0023\u003Dz3cNZpbI\u003D(_param1.Time, _param1);
      }
      else
        this.\u0023\u003Dz3cNZpbI\u003D(_param1.Time, _param1.Value, _param1);
    }

    private void \u0023\u003DzvSl4wl_i8TnJ(DiagramSocketValue _param1)
    {
      if (this.Indicator == null)
        return;
      this.\u0023\u003Dzzi_c5vUBL8kM = _param1;
      this.\u0023\u003Dz3cNZpbI\u003D(_param1.Time, _param1);
    }

    private void \u0023\u003Dz3cNZpbI\u003D(DateTimeOffset _param1, DiagramSocketValue _param2)
    {
      if (this._value == null || this.\u0023\u003Dzzi_c5vUBL8kM == null)
        return;
      Decimal num1 = this.\u0023\u003Dz8GWGifoGTryc(this._value.Value);
      Decimal num2 = this.\u0023\u003Dz8GWGifoGTryc(this.\u0023\u003Dzzi_c5vUBL8kM.Value);
      this._value = (DiagramSocketValue) null;
      this.\u0023\u003Dzzi_c5vUBL8kM = (DiagramSocketValue) null;
      this.\u0023\u003Dz3cNZpbI\u003D(_param1, (object) Tuple.Create<Decimal, Decimal>(num1, num2), _param2);
    }

    private void \u0023\u003Dz3cNZpbI\u003D(
      DateTimeOffset _param1,
      object _param2,
      DiagramSocketValue _param3)
    {
      IIndicatorValue indicatorValue = this.Indicator.Process(this.Indicator.ConvertToIIndicatorValue(_param2));
      if (this.IsFinal && !indicatorValue.IsFinal || this.IsFormed && !indicatorValue.IsFormed)
        return;
      this.RaiseProcessOutput(_param1, (object) indicatorValue, _param3, (Subscription) null);
    }

    private Decimal \u0023\u003Dz8GWGifoGTryc(object _param1)
    {
      Decimal? nullable = new Decimal?();
      object obj = this._value.Value;
      IIndicatorValue indicatorValue = obj as IIndicatorValue;
      if (indicatorValue == null)
      {
        Unit unit = obj as Unit;
        if ((object) unit != null)
          nullable = new Decimal?(unit.Value);
      }
      else
        nullable = new Decimal?(indicatorValue.GetValue<Decimal>());
      if (!nullable.HasValue && ((object) _param1).GetType().IsNumeric())
        nullable = new Decimal?(_param1.To<Decimal>());
      if (!nullable.HasValue)
        throw new ArgumentException(LocalizedStrings.Str3106Params.Put((object[]) new object[1]{ (object) ((object) _param1).GetType().Name }));
      return nullable.Value;
    }

    private void \u0023\u003Dz7USMd7fQEq_CrHxFKZ\u0024uNyo\u003D(IndicatorType _param1)
    {
      ((IEnumerable<IDiagramElementParam>) this.\u0023\u003DzcbJ4wLnibf7eTdF2fE9f72Y\u003D).ForEach<IDiagramElementParam>(new Action<IDiagramElementParam>(((DiagramElement) this).RemoveParam));
      if (this.\u0023\u003DzhqFlwtCpv6Mt != null)
      {
        this.RemoveSocket(this.\u0023\u003DzhqFlwtCpv6Mt);
        this.\u0023\u003DzhqFlwtCpv6Mt = (DiagramSocket) null;
      }
      if ((Equatable<IndicatorType>) _param1 != (IndicatorType) null)
      {
        this.Indicator = Scope<IIndicator>.Current?.Value ?? _param1.Indicator.CreateInstance<IIndicator>();
        if (_param1.Indicator.GetValueType(true) == typeof (PairIndicatorValue<Decimal>))
          this.\u0023\u003DzhqFlwtCpv6Mt = this.AddInput(DiagramElement.GenerateSocketId(nameof(-1260197634)), LocalizedStrings.Input, DiagramSocketType.Any, new Action<DiagramSocketValue>(this.\u0023\u003DzvSl4wl_i8TnJ), 1, int.MaxValue, false, new bool?());
        this.\u0023\u003DzcbJ4wLnibf7eTdF2fE9f72Y\u003D = this.\u0023\u003DzuLakOrJ8c0iX(this.Indicator, string.Empty, string.Empty, 20).ToArray<IDiagramElementParam>();
        ((IEnumerable<IDiagramElementParam>) this.\u0023\u003DzcbJ4wLnibf7eTdF2fE9f72Y\u003D).ForEach<IDiagramElementParam>(new Action<IDiagramElementParam>(((DiagramElement) this).AddParam));
        this.SetElementName(((object) this.Indicator).ToString());
      }
      else
        this.SetElementName((string) null);
      this.RaisePropertiesChanged();
    }

    private sealed class \u0023\u003Dz0Omlz75hk26zqPkrpaKtJ_w\u003D
    {
      public IndicatorType \u0023\u003Dz7cIKYGY\u003D;

      internal bool \u0023\u003DzSkum_pT9U\u0024UTPlBUXA\u003D\u003D(IndicatorType _param1)
      {
        return (Equatable<IndicatorType>) _param1 == this.\u0023\u003Dz7cIKYGY\u003D;
      }
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly IndicatorDiagramElement.LamdaShit _lamdaShit = new IndicatorDiagramElement.LamdaShit();
      public static Func<IndicatorType, SettingsStorage> \u0023\u003DzFG6KwefeX7LNqVKtIQ\u003D\u003D;
      public static Func<SettingsStorage, IndicatorType> \u0023\u003DzaiHoZMxbIlpZFye10g\u003D\u003D;

      internal SettingsStorage \u0023\u003DzN46n3Mc1zpz0HSrVCv\u0024heL4\u003D(
        IndicatorType _param1)
      {
        if (_param1 == null)
          return (SettingsStorage) null;
        return _param1.SaveEntire(false);
      }

      internal IndicatorType \u0023\u003DzZAuwJxJoE9mZ4pbsJotsp38\u003D(
        SettingsStorage _param1)
      {
        IndicatorDiagramElement.\u0023\u003Dz0Omlz75hk26zqPkrpaKtJ_w\u003D omlz75hk26zqPkrpaKtJW = new IndicatorDiagramElement.\u0023\u003Dz0Omlz75hk26zqPkrpaKtJ_w\u003D();
        string str = _param1.GetValue<string>(nameof(-1260198006), (string) null);
        if ((str != null ? (((string) str).StartsWith(nameof(-1260197423)) ? 1 : 0) : 0) != 0)
          _param1[nameof(-1260198006)] = (object) nameof(-1260197501);
        omlz75hk26zqPkrpaKtJW.\u0023\u003Dz7cIKYGY\u003D = _param1.LoadEntire<IndicatorType>();
        return ConfigManager.GetService<IIndicatorProvider>().GetIndicatorTypes().FirstOrDefault<IndicatorType>(new Func<IndicatorType, bool>(omlz75hk26zqPkrpaKtJW.\u0023\u003DzSkum_pT9U\u0024UTPlBUXA\u003D\u003D));
      }
    }

    private sealed class \u0023\u003DzQqbuv_O8S\u0024wKvIp0p5SDXT0\u003D<T>
    {
      public PropertyInfo \u0023\u003DzQ4Scaxo\u003D;
      public IIndicator \u0023\u003DzU3DMvZ2irHYOdMo\u0024GA\u003D\u003D;
      public IndicatorDiagramElement _diagramElement;

      internal void \u0023\u003DzrDydH1\u0024XTdY9C7dSoQ\u003D\u003D(
        T _param1)
      {
        this.\u0023\u003DzQ4Scaxo\u003D.SetValue((object) this.\u0023\u003DzU3DMvZ2irHYOdMo\u0024GA\u003D\u003D, (object) _param1, (object[]) null);
        this._diagramElement.SetElementName(((object) this._diagramElement.Indicator).ToString());
      }
    }
  }
}
