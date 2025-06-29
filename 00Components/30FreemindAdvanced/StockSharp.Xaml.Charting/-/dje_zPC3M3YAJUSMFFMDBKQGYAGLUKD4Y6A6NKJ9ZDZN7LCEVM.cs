// Decompiled with JetBrains decompiler
// Type: -.dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD4Y6A6NKJ9ZDZN7LCEVMWD7FVB2NFVJDLFRW58UKZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD4Y6A6NKJ9ZDZN7LCEVMWD7FVB2NFVJDLFRW58UKZ_ejd : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) ((bool) _param1 ? 1.0 : 0.7);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
