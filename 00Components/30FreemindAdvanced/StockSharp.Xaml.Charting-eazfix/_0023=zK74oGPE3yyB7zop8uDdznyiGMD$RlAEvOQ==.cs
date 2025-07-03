// Decompiled with JetBrains decompiler
// Type: #=zK74oGPE3yyB7zop8uDdznyiGMD$RlAEvOQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

#nullable disable
internal interface IfxChartElement : 
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable
{
  IChartElement ParentElement { get; }

  IEnumerable<IChartElement> ChildElements { get; }

  int Priority { get; }

  event Action<object, string, object> PropertyValueChanging;

  bool DontDraw { get; set; }

  void \u0023\u003Dzy8S_C0E\u003D(IChartElement _param1);

  IfxChartElement RootElement { get; }

  Func<IComparable, Color?> Colorer { get; }

  void AddAxisesAndEventHandler(ChartArea _param1);

  void RemoveAxisesEventHandler();

  bool CheckAxesCompatible(ChartAxisType? _param1, ChartAxisType? _param2);

  void \u0023\u003Dz3MbNd8U\u003D(object _param1);

  bool Draw(ChartDrawData _param1);

  void Reset();

  void ResetUI();

  string GetGeneratedTitle();

  string GetName(IChartElement _param1);

  bool AdditionalName(string _param1);
}
