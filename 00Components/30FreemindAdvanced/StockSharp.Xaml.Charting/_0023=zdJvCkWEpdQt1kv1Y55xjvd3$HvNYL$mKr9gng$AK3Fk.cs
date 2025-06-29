// Decompiled with JetBrains decompiler
// Type: #=zdJvCkWEpdQt1kv1Y55xjvd3$HvNYL$mKr9gng$AK3Fkh
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvd3\u0024HvNYL\u0024mKr9gng\u0024AK3Fkh : 
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
    \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvd3\u0024HvNYL\u0024mKr9gng\u0024AK3Fkh.\u0023\u003DzR\u0024h5gkzF2FxdwMnQnA\u003D\u003D h5gkzF2FxdwMnQnA = new \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvd3\u0024HvNYL\u0024mKr9gng\u0024AK3Fkh.\u0023\u003DzR\u0024h5gkzF2FxdwMnQnA\u003D\u003D();
    h5gkzF2FxdwMnQnA.\u0023\u003DzdKlLQNI\u003D = _param2;
    if (_param3 == null)
      return (object) null;
    if (!this.CanConvertFrom(_param1, _param3.GetType()))
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(102, 1);
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430269));
      interpolatedStringHandler.AppendFormatted<Type>(_param3.GetType());
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539439750));
      throw new FormatException(interpolatedStringHandler.ToStringAndClear());
    }
    string str = (string) _param3;
    try
    {
      return (object) ((IEnumerable<string>) str.Split(new char[3]
      {
        ' ',
        ',',
        ';'
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, double>(new Func<string, double>(h5gkzF2FxdwMnQnA.\u0023\u003DzxSqDeJlOpC6fz\u0024xOVA\u003D\u003D)).ToArray<double>();
    }
    catch (Exception ex)
    {
      throw new FormatException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539439857));
    }
  }

  private sealed class \u0023\u003DzR\u0024h5gkzF2FxdwMnQnA\u003D\u003D
  {
    public CultureInfo \u0023\u003DzdKlLQNI\u003D;

    internal double \u0023\u003DzxSqDeJlOpC6fz\u0024xOVA\u003D\u003D(string _param1)
    {
      return double.Parse(_param1, (IFormatProvider) this.\u0023\u003DzdKlLQNI\u003D);
    }
  }
}
