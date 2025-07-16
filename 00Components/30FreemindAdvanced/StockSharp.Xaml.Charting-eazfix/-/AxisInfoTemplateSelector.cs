// Decompiled with JetBrains decompiler
// Type: -.AxisInfoTemplateSelector
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;

#nullable disable
namespace SciChart.Charting;

internal sealed class AxisInfoTemplateSelector : 
  dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd
{
  
  public static readonly DependencyProperty \u0023\u003Dzp\u0024K\u00240nqsvNAg = DependencyProperty.Register(nameof (YAxisDataTemplate), typeof (DataTemplate), typeof (AxisInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzs6wiQBsd7qIb = DependencyProperty.Register(nameof (XAxisDataTemplate), typeof (DataTemplate), typeof (AxisInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));

  public DataTemplate YAxisDataTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(AxisInfoTemplateSelector.\u0023\u003Dzp\u0024K\u00240nqsvNAg);
    }
    set
    {
      this.SetValue(AxisInfoTemplateSelector.\u0023\u003Dzp\u0024K\u00240nqsvNAg, (object) value);
    }
  }

  public DataTemplate XAxisDataTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(AxisInfoTemplateSelector.\u0023\u003Dzs6wiQBsd7qIb);
    }
    set
    {
      this.SetValue(AxisInfoTemplateSelector.\u0023\u003Dzs6wiQBsd7qIb, (object) value);
    }
  }

  public override DataTemplate \u0023\u003Dzmy_tWbS7jzNB(object _param1, DependencyObject _param2)
  {
    \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D uaaCmXpj7JdmPvp0w = _param1 as \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D;
    DataTemplate dataTemplate = base.\u0023\u003Dzmy_tWbS7jzNB(_param1, _param2);
    if (uaaCmXpj7JdmPvp0w != null)
      dataTemplate = uaaCmXpj7JdmPvp0w.IsXAxis ? this.XAxisDataTemplate : this.YAxisDataTemplate;
    return dataTemplate;
  }
}
