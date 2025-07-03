// Decompiled with JetBrains decompiler
// Type: #=z$ZziqW8v$JjjV5dA4z4_CU0vFoN5YdSbsqeBFt15kUDVaNXbig==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003Dz\u0024ZziqW8v\u0024JjjV5dA4z4_CU0vFoN5YdSbsqeBFt15kUDVaNXbig\u003D\u003D : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    Type type = (Type) _param1;
    return (object) type == null ? (object) null : (object) TypeHelper.CreateInstance<FrameworkElement>(type, Array.Empty<object>());
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotSupportedException();
  }
}
