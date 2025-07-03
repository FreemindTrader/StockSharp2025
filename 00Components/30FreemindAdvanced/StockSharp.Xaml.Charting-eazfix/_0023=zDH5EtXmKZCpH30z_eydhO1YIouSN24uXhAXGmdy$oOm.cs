// Decompiled with JetBrains decompiler
// Type: #=zDH5EtXmKZCpH30z_eydhO1YIouSN24uXhAXGmdy$oOmKDNxH1w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzDH5EtXmKZCpH30z_eydhO1YIouSN24uXhAXGmdy\u0024oOmKDNxH1w\u003D\u003D : 
  IValueConverter
{
  object IValueConverter.\u0023\u003DzM9yoqEmGoL\u0024Vcrr_ku1EGJc\u003D(
    object _param1,
    Type _param2,
    object _param3,
    CultureInfo _param4)
  {
    Color color = (Color) _param1;
    float num = _param3 != null ? Converter.To<float>(_param3) : 0.8f;
    return (object) new SolidColorBrush(new Color()
    {
      ScA = num,
      R = color.R,
      G = color.G,
      B = color.B
    });
  }

  object IValueConverter.\u0023\u003Dz7t96kV0doysI1t8U28R3TqlcxXQz(
    object _param1,
    Type _param2,
    object _param3,
    CultureInfo _param4)
  {
    throw new NotSupportedException();
  }
}
