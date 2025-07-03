// Decompiled with JetBrains decompiler
// Type: -.dje_z3Z8T7U2SE2UJT59CTNY3TET7JJZY8AQFVXBJG85D_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class dje_z3Z8T7U2SE2UJT59CTNY3TET7JJZY8AQFVXBJG85D_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) (Orientation) ((Orientation) _param1 == Orientation.Horizontal ? 1 : 0);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
