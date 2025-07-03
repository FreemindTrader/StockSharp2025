// Decompiled with JetBrains decompiler
// Type: #=zdJvCkWEpdQt1kv1Y55xjvd3$HvNYL$mKr9gng$AK3Fkh
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

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
    string str = this.CanConvertFrom(_param1, _param3.GetType()) ? (string) _param3 : throw new FormatException($"Unable to convert the object type {_param3.GetType()} into a double array. Please use a string with format '1.234, 5.678'");
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
      throw new FormatException("Unable to convert the string {0} into a double array. Please use the format '1.234,5.678'");
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
