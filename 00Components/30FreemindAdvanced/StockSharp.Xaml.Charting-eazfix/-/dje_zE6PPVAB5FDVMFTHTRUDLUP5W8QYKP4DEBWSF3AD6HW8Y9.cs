// Decompiled with JetBrains decompiler
// Type: -.dje_zE6PPVAB5FDVMFTHTRUDLUP5W8QYKP4DEBWSF3AD6HW8Y9EA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace \u002D;

internal sealed class dje_zE6PPVAB5FDVMFTHTRUDLUP5W8QYKP4DEBWSF3AD6HW8Y9EA_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    IEnumerable<double> doubles = _param1 as IEnumerable<double>;
    DoubleCollection doubleCollection = new DoubleCollection();
    if (doubles != null)
    {
      foreach (double num in doubles)
        doubleCollection.Add(num);
    }
    return (object) doubleCollection;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
