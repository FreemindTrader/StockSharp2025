// Decompiled with JetBrains decompiler
// Type: #=zdU$qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D : 
  BindableObject ,
  INotifyPropertyChanged,
  IRenderableSeries
{
  
  private \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003DzXfO9DgaVRj7B;
  
  private IRenderableSeries \u0023\u003Dzaoo30gtEU_6B;

  public \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1,
    IRenderableSeries _param2)
  {
    this.\u0023\u003DzXfO9DgaVRj7B = _param1;
    this.\u0023\u003Dzaoo30gtEU_6B = _param2;
  }

  public \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D DataSeries
  {
    get => this.\u0023\u003DzXfO9DgaVRj7B;
    set
    {
      this.\u0023\u003DzXfO9DgaVRj7B = value;
      this.OnPropertyChanged(nameof (DataSeries));
    }
  }

  public IRenderableSeries RenderSeries
  {
    get => this.\u0023\u003Dzaoo30gtEU_6B;
    set
    {
      this.\u0023\u003Dzaoo30gtEU_6B = value;
      this.OnPropertyChanged(nameof (RenderSeries));
    }
  }
}
