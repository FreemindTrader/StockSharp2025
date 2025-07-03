// Decompiled with JetBrains decompiler
// Type: -.dje_zW53QLLBXPN8WXR6G6DUDNUP6W4XHW79G6NC8CTP7PVJZCMZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class dje_zW53QLLBXPN8WXR6G6DUDNUP6W4XHW79G6NC8CTP7PVJZCMZ_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) (Visibility) ((Orientation) _param1 == Orientation.Vertical ? 0 : 2);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
