// Decompiled with JetBrains decompiler
// Type: #=ziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;

#nullable disable
public interface IRenderableSeries : 
  INotifyPropertyChanged
{
  IDataSeries DataSeries { get; set; }

  IRenderableSeries RenderSeries { get; set; }
}
