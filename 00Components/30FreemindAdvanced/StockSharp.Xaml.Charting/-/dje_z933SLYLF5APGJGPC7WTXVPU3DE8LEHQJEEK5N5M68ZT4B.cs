// Decompiled with JetBrains decompiler
// Type: -.dje_z933SLYLF5APGJGPC7WTXVPU3DE8LEHQJEEK5N5M68ZT4B7C9WY368B8P8GJQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_z933SLYLF5APGJGPC7WTXVPU3DE8LEHQJEEK5N5M68ZT4B7C9WY368B8P8GJQ_ejd : 
  IMultiValueConverter
{
  public object Convert(object[] _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) new Thickness(_param1.OfType<double>().Sum());
  }

  public object[] ConvertBack(object _param1, Type[] _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
