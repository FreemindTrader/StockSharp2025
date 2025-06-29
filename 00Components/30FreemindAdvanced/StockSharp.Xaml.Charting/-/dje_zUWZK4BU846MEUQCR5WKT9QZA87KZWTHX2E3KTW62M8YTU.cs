// Decompiled with JetBrains decompiler
// Type: -.dje_zUWZK4BU846MEUQCR5WKT9QZA87KZWTHX2E3KTW62M8YTU58NG74EVYAUVUQQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zUWZK4BU846MEUQCR5WKT9QZA87KZWTHX2E3KTW62M8YTU58NG74EVYAUVUQQ_ejd : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) (Visibility) ((Orientation) _param1 == Orientation.Horizontal ? 0 : 2);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
