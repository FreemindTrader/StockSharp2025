// Decompiled with JetBrains decompiler
// Type: #=zKasBY8yFp0kHGchcdspopNuEz657XY3Et8L1BAmkUV5h
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003DzKasBY8yFp0kHGchcdspopNuEz657XY3Et8L1BAmkUV5h : IValueConverter
{
  
  private readonly LineAnnotationWithLabelsBase \u0023\u003DzIqrOB76fU0aO;

  internal \u0023\u003DzKasBY8yFp0kHGchcdspopNuEz657XY3Et8L1BAmkUV5h(
    LineAnnotationWithLabelsBase _param1)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, "");
    this.\u0023\u003DzIqrOB76fU0aO = _param1;
  }

  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    if (_param1 == null || !this.\u0023\u003DzIqrOB76fU0aO.XAxes.Any<IAxis>())
      return _param1;
    IAxis usedAxis = this.\u0023\u003DzIqrOB76fU0aO.GetUsedAxis();
    if (usedAxis == null)
      return (object) null;
    if (usedAxis is \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe6IW1TrHf1OjeIxI4VnnySGI && usedAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0)
    {
      object obj;
      switch (_param1)
      {
        case DateTime _:
          _param1 = (object) (DateTime) _param1;
          goto label_10;
        case int _:
          obj = _param1;
          break;
        default:
          obj = System.Convert.ChangeType(_param1, typeof (int), (IFormatProvider) CultureInfo.InvariantCulture);
          break;
      }
      int num = (int) obj;
      _param1 = (object) q9i0MXI7Qb9c1V6c0.\u0023\u003DzWZQlXHuDrnKc(num);
    }
label_10:
    double num1 = usedAxis.\u0023\u003DzhL6gsJw\u003D((IComparable) _param1);
    _param1 = (object) usedAxis.\u0023\u003DzACwLhyc\u003D(num1);
    return _param1;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
