// Decompiled with JetBrains decompiler
// Type: -.TestConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class TestConverter : 
  IMultiValueConverter
{
  public object Convert(object[] _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) _param1.OfType<bool>().Any<bool>(TestConverter.SomeClass34343383.\u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D ?? (TestConverter.SomeClass34343383.\u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D = new Func<bool, bool>(TestConverter.SomeClass34343383.SomeMethond0343.\u0023\u003DzsYV7b2N1RLymHLqTEQ\u003D\u003D)));
  }

  public object[] ConvertBack(object _param1, Type[] _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly TestConverter.SomeClass34343383 SomeMethond0343 = new TestConverter.SomeClass34343383();
    public static Func<bool, bool> \u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D;

    internal bool \u0023\u003DzsYV7b2N1RLymHLqTEQ\u003D\u003D(bool _param1) => !_param1;
  }
}
