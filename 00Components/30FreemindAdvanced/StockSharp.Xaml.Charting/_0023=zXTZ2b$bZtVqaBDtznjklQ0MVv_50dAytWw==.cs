// Decompiled with JetBrains decompiler
// Type: #=zXTZ2b$bZtVqaBDtznjklQ0MVv_50dAytWw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003DzXTZ2b\u0024bZtVqaBDtznjklQ0MVv_50dAytWw\u003D\u003D : 
  IMultiValueConverter
{
  public object Convert(object[] _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) Tuple.Create<ChartArea, IChartElement>(_param1[0] as ChartArea, _param1[1] as IChartElement);
  }

  public object[] ConvertBack(object _param1, Type[] _param2, object _param3, CultureInfo _param4)
  {
    throw new NotSupportedException();
  }
}
