// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.TypeConverterHelper
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System.ComponentModel;
using System.Globalization;

namespace Ecng.ComponentModel
{
  public static class TypeConverterHelper
  {
    public static TypeConverter GetConverter<T>()
    {
      return TypeDescriptor.GetConverter(typeof (T));
    }

    public static T FromString<T>(string value)
    {
      return (T) TypeConverterHelper.GetConverter<T>().ConvertFromString(value);
    }

    public static T FromString<T>(
      ITypeDescriptorContext context,
      CultureInfo culture,
      string value)
    {
      return (T) TypeConverterHelper.GetConverter<T>().ConvertFromString(context, culture, value);
    }

    public static string ToString<T>(T value)
    {
      return TypeConverterHelper.GetConverter<T>().ConvertToString((object) value);
    }

    public static string ToString<T>(ITypeDescriptorContext context, CultureInfo culture, T value)
    {
      return TypeConverterHelper.GetConverter<T>().ConvertToString(context, culture, (object) value);
    }
  }
}
