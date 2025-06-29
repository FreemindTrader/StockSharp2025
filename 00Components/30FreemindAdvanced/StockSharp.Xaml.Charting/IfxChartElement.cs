// Decompiled with JetBrains decompiler
// Type: #=zK74oGPE3yyB7zop8uDdznyiGMD$RlAEvOQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable
{
    IChartElement ParentElement { get; }

    IEnumerable<IChartElement> ChildElements { get; }

    int Priority { get; }

    event Action<object, string, object> PropertyValueChanging;

    bool DontDraw { get; set; }

    void SetParent(IChartElement _param1);

    IfxChartElement RootElement { get; }

    Func<IComparable, Color?> Colorer { get; }

    void \u0023\u003DzXB7fUH9eZ9CX(ChartArea _param1);

    void \u0023\u003DzxFfzrajElFbs();

    bool CheckAxesCompatible(ChartAxisType? _param1, ChartAxisType? _param2);

    void Clone(object _param1);

    bool \u0023\u003DzjgUUUJE\u003D(ChartDrawData _param1);

  void \u0023\u003DzYI36Ggg\u003D();

  void \u0023\u003DzMIAnwWQ\u003D();

  string \u0023\u003DzLYR9XSrDCE6W();

    string \u0023\u003Dzbk\u0024mfqA9iwbF(IChartElement _param1);

    bool \u0023\u003DzpU3scm4vRLfa(string _param1);
}
