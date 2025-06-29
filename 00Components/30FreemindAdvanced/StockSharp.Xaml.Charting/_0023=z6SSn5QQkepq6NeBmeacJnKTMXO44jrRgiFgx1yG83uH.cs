// Decompiled with JetBrains decompiler
// Type: #=z6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D : 
  \u0023\u003DzAuXtmwo_UFdzWVVSiImlM6CrNZf0Y\u0024AoldqYWswSWHE1DEHxuAfkMq_BMaE9<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>,
  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUtzzzd5rSNXl95sF5MghysRDMZyklVKg61SC2QL8,
  \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmiedXllCPFuEn7L1_DWbHW6rxkxNJjBPTR5rC4Mn<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>
{
  private double \u0023\u003DzsTEPHz4R7oLH\u0024K81qFAv0EE\u003D;

  public double \u0023\u003Dzriwlp37sdGNN() => this.\u0023\u003DzsTEPHz4R7oLH\u0024K81qFAv0EE\u003D;

  public void \u0023\u003DzOaokSf3AwB73(double _param1)
  {
    this.\u0023\u003DzsTEPHz4R7oLH\u0024K81qFAv0EE\u003D = _param1;
  }

  public \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003Dzq3MgExWxza1L(
    bool _param1)
  {
    dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd klqcJ87Zm8UwE3WEjd = this.\u0023\u003DzGPGgohxCYHTq().\u0023\u003DzfODy_Nxn8OGy();
    if (!_param1)
    {
      int count = this.\u0023\u003DzaXL4UJQ\u003D[0].get_DataSeries().get_Count();
      double num = count > 1 ? klqcJ87Zm8UwE3WEjd.Diff / (double) (count - 1) / 2.0 * this.\u0023\u003DzYf8vmkLavYktE24jWA\u003D\u003D() : this.\u0023\u003DzYf8vmkLavYktE24jWA\u003D\u003D() / 2.0;
      klqcJ87Zm8UwE3WEjd.Max += num;
      klqcJ87Zm8UwE3WEjd.Min -= num;
    }
    return (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) klqcJ87Zm8UwE3WEjd;
  }

  private \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzGPGgohxCYHTq()
  {
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = this.\u0023\u003DzaXL4UJQ\u003D[0].get_DataSeries().get_XRange();
    for (int index = 1; index < this.\u0023\u003DzaXL4UJQ\u003D.Count; ++index)
      abyLt9clZggmJsWhw = abyLt9clZggmJsWhw.\u0023\u003DzeiifnZI\u003D(this.\u0023\u003DzaXL4UJQ\u003D[index].get_DataSeries().get_XRange());
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D(abyLt9clZggmJsWhw.Min, abyLt9clZggmJsWhw.Max);
  }

  public double \u0023\u003DzYf8vmkLavYktE24jWA\u003D\u003D()
  {
    return this.\u0023\u003DzaXL4UJQ\u003D[0].DataPointWidth;
  }

  public override void \u0023\u003DzNi0XCnZpx1ge(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1)
  {
    if (++this.\u0023\u003DzIbVn\u0024T8\u003D != this.\u0023\u003DzaXL4UJQ\u003D.Count<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D ?? (\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D = new Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, bool>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz7rvFQvphQXMzAdVd7mS9GvM\u003D))))
      return;
    this.\u0023\u003DzIbVn\u0024T8\u003D = 0;
    List<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D> list = this.\u0023\u003DzaXL4UJQ\u003D.Where<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D ?? (\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D = new Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, bool>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzFJKhK_8K_KsZYjuINPEC_Hg\u003D))).ToList<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>();
    if (!list.Any<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>())
      return;
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D mz4rNexJsSmCjpOm = list[0].\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D();
    double num1 = this.\u0023\u003DzYf8vmkLavYktE24jWA\u003D\u003D();
    double num2 = this.\u0023\u003DzDzE1v0A\u003D(mz4rNexJsSmCjpOm.\u0023\u003DzSKfyjpipx8dI().\u0023\u003DzwQnyySN6xaVC());
    this.\u0023\u003DzOaokSf3AwB73((double) list[0].\u0023\u003Dz6BuO4fnhj6SX(mz4rNexJsSmCjpOm.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), mz4rNexJsSmCjpOm.\u0023\u003DzSKfyjpipx8dI(), num2, num1));
    foreach (\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D vosPhel85wyPwiDyo in list)
      this.\u0023\u003DzCWdJqY_tqFkW(_param1, (dje_zNXC69RR7GVTJT3B9825N7L2M67UQWW3U3T7CLNQCYVESFDW77UGBVTMN2R7TDSCWQ2344S4D5UC36KU2HZS32_ejd) vosPhel85wyPwiDyo);
  }

  private double \u0023\u003DzDzE1v0A\u003D(
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> _param1)
  {
    double num1 = double.MaxValue;
    for (int index = 1; index < _param1.Count; ++index)
    {
      double num2 = _param1[index] - _param1[index - 1];
      if (num2 < num1)
        num1 = num2;
    }
    return (_param1[_param1.Count - 1] - _param1[0] + num1) / num1;
  }

  private void \u0023\u003DzCWdJqY_tqFkW(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    dje_zNXC69RR7GVTJT3B9825N7L2M67UQWW3U3T7CLNQCYVESFDW77UGBVTMN2R7TDSCWQ2344S4D5UC36KU2HZS32_ejd _param2)
  {
    using (\u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D vQiJuKqUi9jtIaha = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, _param2.AntiAliasing, (float) _param2.StrokeThickness, _param2.Opacity))
    {
      \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D mz4rNexJsSmCjpOm = _param2.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D();
      string format = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430209) + _param2.LabelTextFormatting + \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430223);
      bool flag1 = this.\u0023\u003Dz4nhIf8\u0024OuuPux\u0024xOhCjDsBI\u003D(_param2.StackedGroupId);
      if (flag1)
        format += \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539339852);
      bool flag2 = mz4rNexJsSmCjpOm.\u0023\u003DzDoU1CJhSUWFV();
      double num1 = _param2.\u0023\u003DzNfVFwxaLW3jC();
      double num2;
      double num3 = Math.Max(this.\u0023\u003DzDU4_kVHHUbzB(_param2.StackedGroupId, out num2), 0.0);
      \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D xrgcdFbSdWgN9GcT8_1 = _param1.\u0023\u003Dze8WyDhI\u003D(_param2.FillBrush, _param2.Opacity, _param2.FillBrushMappingMode);
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J1 = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(_param2.SeriesColor);
      \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, mz4rNexJsSmCjpOm);
      \u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D zeaY3Uu1m4CyxerxRw = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D();
      for (zeaY3Uu1m4CyxerxRw.\u0023\u003DzoKRocJE\u003D = 0; zeaY3Uu1m4CyxerxRw.\u0023\u003DzoKRocJE\u003D < mz4rNexJsSmCjpOm.\u0023\u003DzSKfyjpipx8dI().\u0023\u003DzlpVGw6E\u003D(); zeaY3Uu1m4CyxerxRw.\u0023\u003DzoKRocJE\u003D++)
      {
        \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D kld48pAvUlrTzJ1tmfY = mz4rNexJsSmCjpOm.\u0023\u003DzSKfyjpipx8dI().\u0023\u003Dz\u0024CeUvME\u003D(zeaY3Uu1m4CyxerxRw.\u0023\u003DzoKRocJE\u003D);
        int num4;
        int num5 = this.\u0023\u003DzhwvgGUGVRU_I((\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) _param2, kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D(), out num4);
        double num6 = this.\u0023\u003Dz8iBvfApYdA9X7WlJolT1JzM\u003D(mz4rNexJsSmCjpOm.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D()), num4, num5, num3, num2);
        Tuple<double, double> tuple = this.\u0023\u003DzeKx7SKdwYOP2((\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) _param2, zeaY3Uu1m4CyxerxRw.\u0023\u003DzoKRocJE\u003D, true);
        double num7 = mz4rNexJsSmCjpOm.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(tuple.Item1);
        double num8 = mz4rNexJsSmCjpOm.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(tuple.Item2);
        Point point1 = \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003Dzop6vn0GowyiR(new Point(num6 - num3 / 2.0, num7), flag2);
        Point point2 = \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003Dzop6vn0GowyiR(new Point(num6 + num3 / 2.0, num8), flag2);
        \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = _param2.PaletteProvider;
        if (paletteProvider != null)
        {
          Color? nullable = paletteProvider.\u0023\u003DzP50Orng\u003D((\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) _param2, kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D(), kld48pAvUlrTzJ1tmfY.\u0023\u003Dzu7q98_E\u003D());
          if (nullable.HasValue)
          {
            using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J2 = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(nullable.Value))
            {
              using (\u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D xrgcdFbSdWgN9GcT8_2 = _param1.\u0023\u003Dze8WyDhI\u003D(nullable.Value, 1.0, new bool?()))
                this.\u0023\u003DzpsfFDig\u003D(iluL6N4L8CsqVgQq, point1, point2, rhwYsZxA33iRu6Id7J2, xrgcdFbSdWgN9GcT8_2, num1);
            }
          }
        }
        else
          this.\u0023\u003DzpsfFDig\u003D(iluL6N4L8CsqVgQq, point1, point2, rhwYsZxA33iRu6Id7J1, xrgcdFbSdWgN9GcT8_1, num1);
        if (num3 > 0.0 && _param2.ShowLabel)
        {
          double num9 = kld48pAvUlrTzJ1tmfY.\u0023\u003Dzu7q98_E\u003D();
          if (flag1)
          {
            \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = this.\u0023\u003Dz7rfbi2bO7HOZ((\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) _param2, new Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, double>(zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1BpDS766bygi71RsZA\u003D\u003D));
            num9 /= (double) abyLt9clZggmJsWhw.Diff * 100.0;
          }
          string str = string.Format(format, (object) num9);
          _param1.\u0023\u003DzI6mwN\u0024I\u003D(str, new Rect(point1, point2), AlignmentX.Center, AlignmentY.Center, _param2.LabelColor, _param2.LabelFontSize, (string) null, new FontWeight());
        }
      }
    }
  }

  private void \u0023\u003DzpsfFDig\u003D(
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ _param1,
    Point _param2,
    Point _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D _param5,
    double _param6)
  {
    if (_param2.X.CompareTo(_param3.X) == 0)
      _param1.\u0023\u003Dzk8_eoWQ\u003D(_param2, _param3, _param4);
    else
      _param1.\u0023\u003DzkpjYNfwbvIK8(_param2, _param3, _param5, _param4, _param6);
  }

  private double \u0023\u003DzDU4_kVHHUbzB(string _param1, out double _param2)
  {
    int num1 = this.\u0023\u003Dztqqq7v_6LW7t();
    double num2 = this.\u0023\u003Dzv_5BRY4\u003D(_param1);
    double num3;
    if (this.\u0023\u003DzPgZJyw__KLH8(_param1) == \u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV066G8ZjcZkoR4\u003D.Absolute)
    {
      _param2 = num2;
      num3 = (this.\u0023\u003Dzriwlp37sdGNN() - num2 * (double) (num1 - 1)) / (double) num1;
    }
    else
    {
      num3 = this.\u0023\u003Dzriwlp37sdGNN() / ((double) num1 + num2 * (double) num1 - num2);
      _param2 = num3 * num2;
    }
    return num3;
  }

  private int \u0023\u003Dztqqq7v_6LW7t()
  {
    return this.\u0023\u003DzSgM2QmPFjPGL.Count<Tuple<string, List<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>>>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D ?? (\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D = new Func<Tuple<string, List<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>>, bool>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzr6rwulxKWCKDx0ka48fqluw\u003D)));
  }

  private double \u0023\u003Dz8iBvfApYdA9X7WlJolT1JzM\u003D(
    double _param1,
    int _param2,
    int _param3,
    double _param4,
    double _param5)
  {
    return _param1 - _param4 * (double) _param3 / 2.0 - _param5 * (double) (_param3 - 1) / 2.0 + (double) _param2 * (_param5 + _param4) + 0.5 * _param4;
  }

  public Tuple<double, double> \u0023\u003DzEAAzcP_HlZ8b(
    \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param1,
    int _param2)
  {
    Tuple<double, double> tuple = this.\u0023\u003DzeKx7SKdwYOP2(_param1, _param2, false);
    return new Tuple<double, double>(tuple.Item2, tuple.Item1);
  }

  public double \u0023\u003DzcaynwI5AMDdY(
    \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param1,
    int _param2)
  {
    return this.\u0023\u003DzDU4_kVHHUbzB(_param1.get_StackedGroupId(), out double _);
  }

  internal int \u0023\u003DzhwvgGUGVRU_I(
    \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param1,
    double _param2,
    out int _param3)
  {
    _param3 = -1;
    int num = 0;
    foreach (Tuple<string, List<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>> tuple in this.\u0023\u003DzSgM2QmPFjPGL)
    {
      List<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D> list = tuple.Item2.Where<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzVCaCeyhgY8BAMJLqvA\u003D\u003D ?? (\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzVCaCeyhgY8BAMJLqvA\u003D\u003D = new Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, bool>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzKvZiVbfxf45ElrF4TNmmFrQ\u003D))).ToList<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>();
      foreach (\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D s1JolYrWoYpqmQ6ug in list)
      {
        \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = s1JolYrWoYpqmQ6ug.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
        if (ftrixUnpTllY1PkTyq != null)
        {
          int index = ftrixUnpTllY1PkTyq.\u0023\u003DzwQnyySN6xaVC().\u0023\u003DzFH1yjjY\u003D<double>(true, (IComparable) _param2, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
          if (index != -1 && !\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzeNpB9guo_tur(ftrixUnpTllY1PkTyq.\u0023\u003DzPqsSI6C5MOOb()[index]))
          {
            ++num;
            break;
          }
        }
      }
      if (list.Contains(_param1))
        _param3 = num - 1;
    }
    return num;
  }

  public \u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV066G8ZjcZkoR4\u003D \u0023\u003DzPgZJyw__KLH8(
    string _param1)
  {
    return this.\u0023\u003DzaqOTdcE2Qrcnms5YPQ\u003D\u003D(_param1)[0].get_SpacingMode();
  }

  public double \u0023\u003Dzv_5BRY4\u003D(string _param1)
  {
    return this.\u0023\u003DzaqOTdcE2Qrcnms5YPQ\u003D\u003D(_param1)[0].get_Spacing();
  }

  public override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dznnv4eJBaYYey(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3,
    \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param4)
  {
    _param2 = base.\u0023\u003Dznnv4eJBaYYey(_param1, _param2, _param3, _param4);
    if (!_param2.\u0023\u003DzMeGSfVE\u003D())
    {
      bool flag1 = _param4.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV();
      double num1;
      double num2 = this.\u0023\u003DzDU4_kVHHUbzB(_param4.get_StackedGroupId(), out num1);
      int num3;
      int num4 = this.\u0023\u003DzhwvgGUGVRU_I(_param4, _param2.\u0023\u003DztryT5H42SVj8().\u0023\u003Dzb9UCYbo\u003D(), out num3);
      Point point1 = \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003Dzop6vn0GowyiR(_param2.\u0023\u003DzxZfJER0dbHuS(), flag1);
      double y = point1.Y;
      double num5 = this.\u0023\u003Dz8iBvfApYdA9X7WlJolT1JzM\u003D(point1.X, num3, num4, num2, num1);
      Point point2 = new Point(num5, y);
      _param2.\u0023\u003Dzo2ftAfxjqC04(\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003Dzop6vn0GowyiR(point2, flag1));
      _param2.\u0023\u003Dzn3o1RS9wuET8(\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(point2, _param1) < _param3);
      double num6 = flag1 ? Math.Abs(num5 - _param1.Y) : Math.Abs(num5 - _param1.X);
      ref \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D local = ref _param2;
      bool flag2;
      _param2.\u0023\u003DzkNMVgQ88lfxP(flag2 = num6 < num2 / 2.0);
      int num7 = flag2 ? 1 : 0;
      local.\u0023\u003DzZjtwJshPYJrbgaR43Q\u003D\u003D(num7 != 0);
    }
    return _param2;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, bool> \u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D;
    public static Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, bool> \u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D;
    public static Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, bool> \u0023\u003DzDG61YjmCpqL3k8mhvg\u003D\u003D;
    public static Func<Tuple<string, List<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>>, bool> \u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D;
    public static Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, bool> \u0023\u003DzVCaCeyhgY8BAMJLqvA\u003D\u003D;

    internal bool \u0023\u003Dz7rvFQvphQXMzAdVd7mS9GvM\u003D(
      \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param1)
    {
      return _param1.IsVisible;
    }

    internal bool \u0023\u003DzFJKhK_8K_KsZYjuINPEC_Hg\u003D(
      \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param1)
    {
      return _param1.IsVisible;
    }

    internal bool \u0023\u003Dzr6rwulxKWCKDx0ka48fqluw\u003D(
      Tuple<string, List<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>> _param1)
    {
      return _param1.Item2.Any<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzDG61YjmCpqL3k8mhvg\u003D\u003D ?? (\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzDG61YjmCpqL3k8mhvg\u003D\u003D = new Func<\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D, bool>(\u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz2DLUwa0gtntxz_LmLoPnqd0\u003D)));
    }

    internal bool \u0023\u003Dz2DLUwa0gtntxz_LmLoPnqd0\u003D(
      \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param1)
    {
      return _param1.IsVisible;
    }

    internal bool \u0023\u003DzKvZiVbfxf45ElrF4TNmmFrQ\u003D(
      \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param1)
    {
      return _param1.IsVisible;
    }
  }

  private sealed class \u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D
  {
    public int \u0023\u003DzoKRocJE\u003D;

    internal double \u0023\u003Dz1BpDS766bygi71RsZA\u003D\u003D(
      \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D _param1)
    {
      return _param1.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI().\u0023\u003DzPqsSI6C5MOOb()[this.\u0023\u003DzoKRocJE\u003D];
    }
  }
}
