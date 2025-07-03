// Decompiled with JetBrains decompiler
// Type: #=zJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG$7WLh
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

#nullable disable
internal static class \u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh
{
  private static readonly FontFamily \u0023\u003Dz02dEDD\u0024fu7S8 = new TextBlock().FontFamily;
  private static readonly IDictionary<string, FontWeight> \u0023\u003DzwxuTPxReUxmtqJZBWw\u003D\u003D = (IDictionary<string, FontWeight>) new Dictionary<string, FontWeight>()
  {
    {
      FontWeights.Bold.ToString(),
      FontWeights.Bold
    },
    {
      FontWeights.Black.ToString(),
      FontWeights.Black
    },
    {
      FontWeights.ExtraBlack.ToString(),
      FontWeights.ExtraBlack
    },
    {
      FontWeights.ExtraBold.ToString(),
      FontWeights.ExtraBold
    },
    {
      FontWeights.ExtraLight.ToString(),
      FontWeights.ExtraLight
    },
    {
      FontWeights.Light.ToString(),
      FontWeights.Light
    },
    {
      FontWeights.Medium.ToString(),
      FontWeights.Medium
    },
    {
      FontWeights.Normal.ToString(),
      FontWeights.Normal
    },
    {
      FontWeights.SemiBold.ToString(),
      FontWeights.SemiBold
    },
    {
      FontWeights.Thin.ToString(),
      FontWeights.Thin
    }
  };
  private static readonly IDictionary<string, FontStyle> \u0023\u003DzTBZRzOW0NpnF = (IDictionary<string, FontStyle>) new Dictionary<string, FontStyle>()
  {
    {
      FontStyles.Italic.ToString(),
      FontStyles.Italic
    },
    {
      FontStyles.Normal.ToString(),
      FontStyles.Normal
    }
  };

  public static void \u0023\u003DzpJLX1I844aD6zcceiA\u003D\u003D(
    this XmlReader _param0,
    object _param1,
    string _param2)
  {
    PropertyInfo property = _param1.GetType().GetProperty(_param2);
    object obj = _param0.\u0023\u003Dzm2nn9hA\u003D(_param2, property.PropertyType);
    property.SetValue(_param1, obj, (object[]) null);
  }

  public static object \u0023\u003Dzm2nn9hA\u003D(
    this XmlReader _param0,
    string _param1,
    Type _param2)
  {
    string str = _param0[_param1];
    object obj;
    if (_param2.IsEnum)
      obj = str == null ? (object) null : Enum.Parse(_param2, str, false);
    else if (_param2 == typeof (Brush))
    {
      if (str == null)
        return (object) null;
      obj = (object) new SolidColorBrush(\u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003DzJVr0pwOVdK7r(str));
    }
    else if (_param2 == typeof (Color))
    {
      if (str == null)
        return (object) Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      obj = (object) \u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003DzJVr0pwOVdK7r(str);
    }
    else if (_param2 == typeof (Thickness))
    {
      if (str == null)
        return (object) new Thickness(0.0, 0.0, 0.0, 0.0);
      obj = (object) \u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003DzI5BKnKbAL5dB(str);
    }
    else if (_param2 == typeof (FontFamily))
    {
      try
      {
        obj = string.IsNullOrEmpty(str) ? (object) \u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003Dz02dEDD\u0024fu7S8 : (object) new FontFamily(str);
      }
      catch
      {
        obj = (object) \u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003Dz02dEDD\u0024fu7S8;
      }
    }
    else if (_param2 == typeof (FontWeight))
      obj = str == null || !\u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003DzwxuTPxReUxmtqJZBWw\u003D\u003D.ContainsKey(str) ? (object) FontWeights.Normal : (object) \u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003DzwxuTPxReUxmtqJZBWw\u003D\u003D[str];
    else if (_param2 == typeof (FontStyle))
      obj = str == null || !\u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003DzTBZRzOW0NpnF.ContainsKey(str) ? (object) FontStyles.Normal : (object) \u0023\u003DzJ9vSi7sIwIEed80npzusCPygKelehJ5SBE8SjlG\u00247WLh.\u0023\u003DzTBZRzOW0NpnF[str];
    else if (typeof (IRange).IsAssignableFrom(_param2))
    {
      if (str == null)
        return (object) null;
      string[] strArray = str.Split(',');
      obj = (object) \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D(Type.GetType(strArray[0]), (IComparable) Convert.ToDouble(strArray[1]), (IComparable) Convert.ToDouble(strArray[2]));
    }
    else if (_param2 == typeof (TimeSpan))
    {
      if (str == null)
        return (object) TimeSpan.Zero;
      obj = (object) TimeSpan.Parse(str);
    }
    else
    {
      if (str == null)
        return (object) null;
      Type type = Nullable.GetUnderlyingType(_param2);
      if ((object) type == null)
        type = _param2;
      Type conversionType = type;
      obj = Convert.ChangeType((object) str, conversionType, (IFormatProvider) CultureInfo.CurrentCulture);
    }
    return obj;
  }

  private static Color \u0023\u003DzJVr0pwOVdK7r(string _param0)
  {
    string[] strArray = _param0.Split(',');
    return Color.FromArgb(Convert.ToByte(strArray[0], 16 /*0x10*/), Convert.ToByte(strArray[1], 16 /*0x10*/), Convert.ToByte(strArray[2], 16 /*0x10*/), Convert.ToByte(strArray[3], 16 /*0x10*/));
  }

  private static Thickness \u0023\u003DzI5BKnKbAL5dB(string _param0)
  {
    string[] strArray = _param0.Split(',');
    return new Thickness(Convert.ToDouble(strArray[0]), Convert.ToDouble(strArray[1]), Convert.ToDouble(strArray[2]), Convert.ToDouble(strArray[3]));
  }
}
