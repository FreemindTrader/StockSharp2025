// Decompiled with JetBrains decompiler
// Type: -.dje_zYEYVHRUCVES8WP44Q8DAVLAAKLT4LSN5PZDM4W4AXGA5UPA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class dje_zYEYVHRUCVES8WP44Q8DAVLAAKLT4LSN5PZDM4W4AXGA5UPA_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    int num1 = (int) _param1;
    int num2 = string.Equals(_param3 as string, "INVERSE", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
    Visibility visibility1 = num2 != 0 ? Visibility.Collapsed : Visibility.Visible;
    Visibility visibility2 = num2 != 0 ? Visibility.Visible : Visibility.Collapsed;
    return (object) (Visibility) (num1 == 0 ? (int) visibility1 : (int) visibility2);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
