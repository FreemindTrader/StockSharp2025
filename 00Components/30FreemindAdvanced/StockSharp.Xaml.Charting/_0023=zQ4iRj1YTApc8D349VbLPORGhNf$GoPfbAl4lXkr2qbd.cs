// Decompiled with JetBrains decompiler
// Type: #=zQ4iRj1YTApc8D349VbLPORGhNf$GoPfbAl4lXkr2qbdyCeSLr0MtuSBxZdlBDY$hamCa$IQMK2bl
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
internal sealed class \u0023\u003DzQ4iRj1YTApc8D349VbLPORGhNf\u0024GoPfbAl4lXkr2qbdyCeSLr0MtuSBxZdlBDY\u0024hamCa\u0024IQMK2bl : 
  TypeConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext _param1, Type _param2)
  {
    return _param2 == typeof (string);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext _param1,
    CultureInfo _param2,
    object _param3)
  {
    string s = _param3 != null && this.CanConvertFrom(_param1, _param3.GetType()) ? ((string) _param3).Trim() : throw new FormatException(string.Format(XXX.SSS(-539338258), (object) _param3?.GetType()));
    double num;
    try
    {
      num = s.Length != 1 || !(s.ToUpperInvariant() == XXX.SSS(-539338340)) ? double.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture) : Math.E;
    }
    catch (Exception ex)
    {
      throw new FormatException(XXX.SSS(-539338348));
    }
    return (object) num;
  }
}
