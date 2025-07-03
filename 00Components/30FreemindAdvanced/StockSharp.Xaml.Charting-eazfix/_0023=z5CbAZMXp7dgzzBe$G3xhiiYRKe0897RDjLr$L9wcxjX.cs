// Decompiled with JetBrains decompiler
// Type: #=z5CbAZMXp7dgzzBe$G3xhiiYRKe0897RDjLr$L9wcxjXImUKaPnpxZj0=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhiiYRKe0897RDjLr\u0024L9wcxjXImUKaPnpxZj0\u003D : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) (Converter.To<double>(_param1) / 10.0);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotSupportedException();
  }
}
