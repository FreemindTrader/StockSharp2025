// Decompiled with JetBrains decompiler
// Type: #=ztorG3HTUDpMsfjPqFEEe9Ofk2uEDAGTcKLRwQVg=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
internal sealed class \u0023\u003DztorG3HTUDpMsfjPqFEEe9Ofk2uEDAGTcKLRwQVg\u003D : TypeConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext _param1, Type _param2)
  {
    return _param2 == typeof (string) || base.CanConvertFrom(_param1, _param2);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext _param1,
    CultureInfo _param2,
    object _param3)
  {
    string s = (string) _param3;
    double result1;
    if (double.TryParse(s, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
      return (object) result1;
    DateTime result2;
    return DateTime.TryParse(s, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result2) ? (object) result2 : (object) null;
  }
}
