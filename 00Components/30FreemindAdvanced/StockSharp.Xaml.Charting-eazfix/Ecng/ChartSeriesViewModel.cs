// Decompiled with JetBrains decompiler
// Type: #=zdU$qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;
using System.Diagnostics;

#nullable disable
public sealed class ChartSeriesViewModel : 
  BindableObject ,
  INotifyPropertyChanged,
  IRenderableSeries
{
  
  private IDataSeries \u0023\u003DzXfO9DgaVRj7B;
  
  private IRenderableSeries \u0023\u003Dzaoo30gtEU_6B;

  public ChartSeriesViewModel(
    IDataSeries _param1,
    IRenderableSeries _param2)
  {
    this.\u0023\u003DzXfO9DgaVRj7B = _param1;
    this.\u0023\u003Dzaoo30gtEU_6B = _param2;
  }

  public IDataSeries DataSeries
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
