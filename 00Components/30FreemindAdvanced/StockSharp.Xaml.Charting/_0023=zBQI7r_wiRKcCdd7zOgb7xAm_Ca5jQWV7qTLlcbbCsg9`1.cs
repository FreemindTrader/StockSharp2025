// Decompiled with JetBrains decompiler
// Type: #=zBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB$
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;

#nullable disable
internal abstract class \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<\u0023\u003Dzav4EkcQ\u003D> : 
  UIBaseVM
  where \u0023\u003Dzav4EkcQ\u003D : ChartPart<\u0023\u003Dzav4EkcQ\u003D>, IDrawableChartElement
{
  
  private readonly HashSet<IfxChartElement> \u0023\u003DzoWo2SyTLHbq4_D3IcQ\u003D\u003D = new HashSet<IfxChartElement>();
  
  private readonly \u0023\u003Dzav4EkcQ\u003D \u0023\u003DzUpC1DAYavIvKAFbBxA\u003D\u003D;

  protected \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024(
    \u0023\u003Dzav4EkcQ\u003D _param1)
  {
    this.\u0023\u003DzUpC1DAYavIvKAFbBxA\u003D\u003D = _param1 ?? throw new ArgumentNullException("");
    this.\u0023\u003DzZcbqdpE\u003D((IfxChartElement) this.\u0023\u003DzeaszzAAoBOY9());
  }

  public override IDrawableChartElement Element
  {
    get
    {
      return (IDrawableChartElement) this.\u0023\u003DzeaszzAAoBOY9();
    }
  }

  protected \u0023\u003Dzav4EkcQ\u003D \u0023\u003DzeaszzAAoBOY9()
  {
    return this.\u0023\u003DzUpC1DAYavIvKAFbBxA\u003D\u003D;
  }

  public sealed override void \u0023\u003Dz\u0024abmkXc\u003D()
  {
    foreach (IfxChartElement ddznyiGmdRlAevOq in this.\u0023\u003DzoWo2SyTLHbq4_D3IcQ\u003D\u003D.ToArray<IfxChartElement>())
      this.\u0023\u003DzfttffOE\u003D(ddznyiGmdRlAevOq);
    base.\u0023\u003Dz\u0024abmkXc\u003D();
  }

  protected void \u0023\u003DzZcbqdpE\u003D(
    IfxChartElement _param1)
  {
    if (!this.\u0023\u003DzoWo2SyTLHbq4_D3IcQ\u003D\u003D.Add(_param1))
      return;
    _param1.PropertyValueChanging += new Action<object, string, object>(this.\u0023\u003DztQIFoT6W122c);
    _param1.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzn1W_trW7DSn3);
  }

  private void \u0023\u003DzfttffOE\u003D(
    IfxChartElement _param1)
  {
    if (!this.\u0023\u003DzoWo2SyTLHbq4_D3IcQ\u003D\u003D.Remove(_param1))
      return;
    _param1.PropertyValueChanging -= new Action<object, string, object>(this.\u0023\u003DztQIFoT6W122c);
    _param1.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003Dzn1W_trW7DSn3);
  }

  private void \u0023\u003DztQIFoT6W122c(object _param1, string _param2, object _param3)
  {
    this.\u0023\u003DzfrhXX9MCW\u0024SC((IfxChartElement) _param1, _param2, _param3);
  }

  private void \u0023\u003Dzn1W_trW7DSn3(object _param1, PropertyChangedEventArgs _param2)
  {
    this.\u0023\u003Dz3u1qwgvgJlZC((IfxChartElement) _param1, _param2.PropertyName);
  }

  protected static void \u0023\u003Dz9tL3mkpMz5PJ<T>(
    IfxChartElement _param0,
    string _param1,
    T[] _param2)
  {
    _param0.PropertyValueChanging += new Action<object, string, object>(new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<\u0023\u003Dzav4EkcQ\u003D>.\u0023\u003DzoCQsx9qZ9BND33HUkljNxJY\u003D<T>()
    {
      \u0023\u003DzlO56\u0024U0\u003D = _param1,
      \u0023\u003Dzl0ap9G9KAFrl = _param2
    }.\u0023\u003Dz4Ri02UyRDgR_MM8068hswR8\u003D);
  }

  protected T \u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<T>(
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[] _param1)
    where T : BaseRenderableSeries, new()
  {
    if (_param1 != null && ((IEnumerable<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>) _param1).Any<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>(\u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<\u0023\u003Dzav4EkcQ\u003D>.\u0023\u003DzCYFmgQJxXjskITDYkg\u003D\u003D<T>.\u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D ?? (\u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<\u0023\u003Dzav4EkcQ\u003D>.\u0023\u003DzCYFmgQJxXjskITDYkg\u003D\u003D<T>.\u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D = new Func<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy, bool>(\u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<\u0023\u003Dzav4EkcQ\u003D>.\u0023\u003DzCYFmgQJxXjskITDYkg\u003D\u003D<T>.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzG0_RV0031CU\u0024Q\u00241ZckMpFgXIR2zjjeMsCA\u003D\u003D))))
      throw new InvalidOperationException("" + typeof (T).Name);
    T obj = new T();
    if (this.\u0023\u003DzeaszzAAoBOY9().RootElement == (object) this.\u0023\u003DzeaszzAAoBOY9())
    {
      obj.SetBindings(BaseRenderableSeries.\u0023\u003Dz8b6MQAIE8UZn, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
    }
    else
    {
      // ISSUE: variable of a boxed type
      __Boxed<T> local = (object) obj;
      DependencyProperty z8b6MqaiE8Uzn = BaseRenderableSeries.\u0023\u003Dz8b6MQAIE8UZn;
      BoolAllConverter conv = new BoolAllConverter();
      conv.Value = true;
      Binding[] bindingArray = new Binding[2]
      {
        new Binding("")
        {
          Source = (object) this.\u0023\u003DzeaszzAAoBOY9()
        },
        new Binding("")
        {
          Source = (object) this.\u0023\u003DzeaszzAAoBOY9().RootElement
        }
      };
      local.SetMultiBinding(z8b6MqaiE8Uzn, (IMultiValueConverter) conv, bindingArray);
    }
    obj.SetBindings(BaseRenderableSeries.\u0023\u003DzSEAakZbtZKgY, (object) this.RootElem, "");
    obj.SetBindings(BaseRenderableSeries.\u0023\u003DziAqnE8_\u0024SBDB, (object) this.RootElem, "");
    \u0023\u003Dzav4EkcQ\u003D zav4EkcQ = this.\u0023\u003DzeaszzAAoBOY9();
    if (!((object) zav4EkcQ is ChartBandElement) && !((object) zav4EkcQ is IChartTransactionElement))
    {
      obj.SetBindings(BaseRenderableSeries.\u0023\u003DzTe_gV3cWjEp7, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
      obj.SetBindings(BaseRenderableSeries.\u0023\u003Dzdr5RTntdbeN7, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
    }
    obj.Tag = _param1 == null || _param1.Length == 0 ? (object) (Tuple<UIBaseVM, \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[]>) null : (object) Tuple.Create<UIBaseVM, \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[]>((UIBaseVM) this, _param1);
    this.GetParentVM().\u0023\u003DzMNK339lzrtSc();
    return obj;
  }

  protected static void \u0023\u003DzpbLgaWJ0hngn(
    IRenderableSeries _param0,
    bool _param1)
  {
    BaseRenderableSeries ls4St64EqzfbaEjd = (BaseRenderableSeries) _param0;
    dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd.SetIncludeSeries((DependencyObject) ls4St64EqzfbaEjd, _param1);
    dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.SetIncludeSeries((DependencyObject) ls4St64EqzfbaEjd, _param1);
    dje_zHR7NJDL95STD4SWVG26WR43JGQLXY8NHRZ7Q26829WGD2MZ_ejd.SetIncludeSeries((DependencyObject) ls4St64EqzfbaEjd, _param1);
    dje_zW53QLLBXPN8WXR6G6DUDNUP6W2EZEB9SXV35FLJUAR85WHV6RM45V_ejd.SetIncludeSeries((DependencyObject) ls4St64EqzfbaEjd, _param1);
  }

  [Serializable]
  private sealed class \u0023\u003DzCYFmgQJxXjskITDYkg\u003D\u003D<T> where T : BaseRenderableSeries, new()
  {
    public static readonly \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<\u0023\u003Dzav4EkcQ\u003D>.\u0023\u003DzCYFmgQJxXjskITDYkg\u003D\u003D<T> \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<\u0023\u003Dzav4EkcQ\u003D>.\u0023\u003DzCYFmgQJxXjskITDYkg\u003D\u003D<T>();
    public static Func<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy, bool> \u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D;

    internal bool \u0023\u003DzG0_RV0031CU\u0024Q\u00241ZckMpFgXIR2zjjeMsCA\u003D\u003D(
      \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy _param1)
    {
      return _param1 == null;
    }
  }

  private sealed class \u0023\u003DzoCQsx9qZ9BND33HUkljNxJY\u003D<T>
  {
    public string \u0023\u003DzlO56\u0024U0\u003D;
    public T[] \u0023\u003Dzl0ap9G9KAFrl;

    internal void \u0023\u003Dz4Ri02UyRDgR_MM8068hswR8\u003D(
      object _param1,
      string _param2,
      object _param3)
    {
      if (_param2 != this.\u0023\u003DzlO56\u0024U0\u003D)
        return;
      T zH9Hnkng = (T) _param3;
      if (!((IEnumerable<T>) this.\u0023\u003Dzl0ap9G9KAFrl).Contains<T>(zH9Hnkng))
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnsupportedType, new object[1]
        {
          (object) zH9Hnkng
        }));
    }
  }
}
