// Decompiled with JetBrains decompiler
// Type: #=zY$Fbb9SLuCh5D6t_XKcJqKuhsUbi2xjkbRlj6EVaEl1lCbDsuw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using Ecng.Xaml;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqKuhsUbi2xjkbRlj6EVaEl1lCbDsuw\u003D\u003D : 
  IValueConverter
{
  object IValueConverter.Convert(
    object _param1,
    Type _param2,
    object _param3,
    CultureInfo _param4)
  {
    return (object) ((Color) _param1).ToTransparent(_param3 != null ? Converter.To<byte>(_param3) : (byte) 0);
  }

  object IValueConverter.ConvertBack(
    object _param1,
    Type _param2,
    object _param3,
    CultureInfo _param4)
  {
    throw new NotSupportedException();
  }
}
