// Decompiled with JetBrains decompiler
// Type: -.dje_zKEMJWHZWSE279KQB6EDEEPQJWHB6YY5ZXWCLNSCZADNXTZVR46SKD_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class dje_zKEMJWHZWSE279KQB6EDEEPQJWHB6YY5ZXWCLNSCZADNXTZVR46SKD_ejd : 
  IMultiValueConverter
{
  public object Convert(object[] _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) _param1.OfType<bool>().Any<bool>(dje_zKEMJWHZWSE279KQB6EDEEPQJWHB6YY5ZXWCLNSCZADNXTZVR46SKD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D ?? (dje_zKEMJWHZWSE279KQB6EDEEPQJWHB6YY5ZXWCLNSCZADNXTZVR46SKD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D = new Func<bool, bool>(dje_zKEMJWHZWSE279KQB6EDEEPQJWHB6YY5ZXWCLNSCZADNXTZVR46SKD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzsYV7b2N1RLymHLqTEQ\u003D\u003D)));
  }

  public object[] ConvertBack(object _param1, Type[] _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zKEMJWHZWSE279KQB6EDEEPQJWHB6YY5ZXWCLNSCZADNXTZVR46SKD_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zKEMJWHZWSE279KQB6EDEEPQJWHB6YY5ZXWCLNSCZADNXTZVR46SKD_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<bool, bool> \u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D;

    internal bool \u0023\u003DzsYV7b2N1RLymHLqTEQ\u003D\u003D(bool _param1) => !_param1;
  }
}
