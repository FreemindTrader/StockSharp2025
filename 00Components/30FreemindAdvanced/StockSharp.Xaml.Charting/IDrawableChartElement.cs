// Decompiled with JetBrains decompiler
// Type: #=zbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System.ComponentModel;
using System.Windows.Media;

#nullable disable
internal interface IDrawableChartElement :
  IfxChartElement,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable
{
    Color Color { get; }

    UIBaseVM CreateViewModel(
      IScichartSurfaceVM _param1);

    bool StartDrawing(IEnumerableEx<ChartDrawData.IDrawValue> _param1);

    void ChildElementsStartDrawing();
}
