// Decompiled with JetBrains decompiler
// Type: #=zdU$qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.ComponentModel;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D : 
  BindableObject ,
  \u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D,
  INotifyPropertyChanged
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
      this.\u0023\u003Dz15moWio\u003D("");
    }
  }

  public IRenderableSeries RenderSeries
  {
    get => this.\u0023\u003Dzaoo30gtEU_6B;
    set
    {
      this.\u0023\u003Dzaoo30gtEU_6B = value;
      this.\u0023\u003Dz15moWio\u003D("");
    }
  }
}
