// Decompiled with JetBrains decompiler
// Type: -.dje_zLNMHB3FMDVJLWN9U6MV4V8GB8HDKH82X4U9VRGDD487Q2ESD3DZPC_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zLNMHB3FMDVJLWN9U6MV4V8GB8HDKH82X4U9VRGDD487Q2ESD3DZPC_ejd : 
  IValueConverter
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly dje_zLNMHB3FMDVJLWN9U6MV4V8GB8HDKH82X4U9VRGDD487Q2ESD3DZPC_ejd \u0023\u003DzUlh\u00243zY\u003D = new dje_zLNMHB3FMDVJLWN9U6MV4V8GB8HDKH82X4U9VRGDD487Q2ESD3DZPC_ejd();

  public object Convert(object _param1, Type _param2 = null, object _param3 = null, CultureInfo _param4 = null)
  {
    if (_param1 != null)
    {
      FieldInfo field = _param1.GetType().GetField(_param1.ToString());
      bool flag = (string) _param3 == \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430936);
      if (field != (FieldInfo) null)
      {
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
        return customAttributes.Length == 0 || !(!string.IsNullOrEmpty(customAttributes[0].Description) | flag) ? (object) _param1.ToString() : (object) customAttributes[0].Description;
      }
    }
    return (object) string.Empty;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new Exception(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430157));
  }
}
