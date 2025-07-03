// Decompiled with JetBrains decompiler
// Type: #=zAtYWtSRxk8WC$EcJQ7b1L2lkqxphP$pi$LiKmnKhhiFZMlWg73XsUwM=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L2lkqxphP\u0024pi\u0024LiKmnKhhiFZMlWg73XsUwM\u003D : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return !(_param1 is int num) ? (object) -1 : (object) (num * 60);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
