// Decompiled with JetBrains decompiler
// Type: -.AxisTitleTemplateSelector
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;

#nullable disable
namespace SciChart.Charting;

internal sealed class AxisTitleTemplateSelector : 
  DataTemplateSelector
{
  
  private DataTemplate \u0023\u003DzzOBVI_vgcMJc;

  
  public DataTemplate dje_zNJTCVXYXEFW8ZZQ_ejd
  {
    get => this.\u0023\u003DzzOBVI_vgcMJc;
    set
    {
      this.\u0023\u003DzzOBVI_vgcMJc = value;
      this.\u0023\u003DzCZf_oX5aGk\u0024Y();
    }
  }

  public DataTemplate \u0023\u003DzzZucbbFbZqkQ() => this.\u0023\u003DzzOBVI_vgcMJc;

  public void \u0023\u003DzFWzPcBUA\u00249xA(DataTemplate _param1)
  {
    this.\u0023\u003DzzOBVI_vgcMJc = _param1;
    this.\u0023\u003DzCZf_oX5aGk\u0024Y();
  }

  public override DataTemplate \u0023\u003Dzmy_tWbS7jzNB(object _param1, DependencyObject _param2)
  {
    return !(_param1 is string) ? base.\u0023\u003Dzmy_tWbS7jzNB(_param1, _param2) : this.\u0023\u003DzzZucbbFbZqkQ();
  }
}
