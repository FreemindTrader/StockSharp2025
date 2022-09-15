// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.MultiBoolToOpacityConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// </summary>
  public class MultiBoolToOpacityConverter : IMultiValueConverter
  {
    
    private double \u0023\u003DzCKfYWyMHwCxNdsYPGxCp2wQ\u003D;
    
    private double \u0023\u003DzIuE6zCjc\u0024YEdBMlmF93CFQM\u003D;

    /// <summary>
    /// </summary>
    public MultiBoolToOpacityConverter()
    {
      this.FalseOpacityValue = 1.0;
      this.TrueOpacityValue = 0.5;
    }

    /// <summary>
    /// </summary>
    public double TrueOpacityValue
    {
      get
      {
        return this.\u0023\u003DzCKfYWyMHwCxNdsYPGxCp2wQ\u003D;
      }
      set
      {
        this.\u0023\u003DzCKfYWyMHwCxNdsYPGxCp2wQ\u003D = value;
      }
    }

    /// <summary>
    /// </summary>
    public double FalseOpacityValue
    {
      get
      {
        return this.\u0023\u003DzIuE6zCjc\u0024YEdBMlmF93CFQM\u003D;
      }
      set
      {
        this.\u0023\u003DzIuE6zCjc\u0024YEdBMlmF93CFQM\u003D = value;
      }
    }

    object IMultiValueConverter.Convert(
      object[] _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      bool flag = _param3 == null || _param3.To<bool>();
      return (object) (_param1.OfType<bool>().All<bool>(MultiBoolToOpacityConverter.SomeShit.\u0023\u003Dz5pHPlllntxm7YXEetA\u003D\u003D ?? (MultiBoolToOpacityConverter.SomeShit.\u0023\u003Dz5pHPlllntxm7YXEetA\u003D\u003D = new Func<bool, bool>(MultiBoolToOpacityConverter.SomeShit.ShitMethod02.\u0023\u003DzE9jNUfueYpgn1BVlE75sgFagxfLP3I7grA0DLnE\u003D))) == flag ? this.TrueOpacityValue : this.FalseOpacityValue);
    }

    object[] IMultiValueConverter.ConvertBack(
      object _param1,
      Type[] _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly MultiBoolToOpacityConverter.SomeShit ShitMethod02 = new MultiBoolToOpacityConverter.SomeShit();
      public static Func<bool, bool> \u0023\u003Dz5pHPlllntxm7YXEetA\u003D\u003D;

      internal bool \u0023\u003DzE9jNUfueYpgn1BVlE75sgFagxfLP3I7grA0DLnE\u003D(bool _param1)
      {
        return _param1;
      }
    }
  }
}
