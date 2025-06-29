// Decompiled with JetBrains decompiler
// Type: #=ztyAKlj3UbIrpcOb4hAbyLmLZc7bTaOG0v8uu5qE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
internal sealed class \u0023\u003DztyAKlj3UbIrpcOb4hAbyLmLZc7bTaOG0v8uu5qE\u003D : TypeConverter
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
    if (double.TryParse(s, out result1))
      return (object) result1;
    DateTime result2;
    return DateTime.TryParse(s, out result2) ? (object) result2 : (object) s;
  }
}
