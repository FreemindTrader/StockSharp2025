// Decompiled with JetBrains decompiler
// Type: -.dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAVG8TLRM8ZCRMCKCWQJXLX2_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAVG8TLRM8ZCRMCKCWQJXLX2_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return _param1 == null ? (object) null : (object) new Thickness(((IComparable) _param1).\u0023\u003Dzb9UCYbo\u003D());
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
