// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.FileSizeFormatProvider
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;

namespace Ecng.ComponentModel
{
  public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
  {
    private static readonly string[] _letters = new string[6]
    {
      "b",
      "kb",
      "mb",
      "gb",
      "tb",
      "pb"
    };
    private const string _fileSizeFormat = "fs";

    object IFormatProvider.GetFormat(Type formatType)
    {
      if (!formatType.Is<ICustomFormatter>())
        return (object) null;
      return (object) this;
    }

    string ICustomFormatter.Format(
      string format,
      object arg,
      IFormatProvider formatProvider)
    {
      if (format != null)
      {
        if (format.StartsWith("fs"))
        {
          Decimal num1;
          try
          {
            num1 = arg.To<Decimal>();
          }
          catch (InvalidCastException ex)
          {
            return FileSizeFormatProvider.DefaultFormat(format, arg, formatProvider);
          }
          byte num2 = 0;
          while (num1 >= new Decimal(1024) && (int) num2 < FileSizeFormatProvider._letters.Length - 1)
          {
            ++num2;
            num1 /= new Decimal(1024);
          }
          string str = format.Substring(2);
          if (str.IsEmpty())
            str = "2";
          return ("{0:N" + str + "}{1}").Put((object) num1, (object) FileSizeFormatProvider._letters[(int) num2]);
        }
      }
      return FileSizeFormatProvider.DefaultFormat(format, arg, formatProvider);
    }

    private static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
    {
      IFormattable formattable = arg as IFormattable;
      if (formattable == null)
        return arg.ToString();
      return formattable.ToString(format, formatProvider);
    }
  }
}
