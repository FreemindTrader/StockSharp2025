// Decompiled with JetBrains decompiler
// Type: -.FastHeatMapRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class FastHeatMapRenderableSeries : 
  BaseRenderableSeries,
  INotifyPropertyChanged
{
  
  public static readonly DependencyProperty \u0023\u003Dzx6fa5TzXfc8S = DependencyProperty.Register(nameof (DrawTextInCell), typeof (bool), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzzYBz11wS5ND3 = DependencyProperty.Register(nameof (CellTextForeground), typeof (Color), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) Colors.White));
  
  public static readonly DependencyProperty \u0023\u003DzBbalbHIs26PX = DependencyProperty.Register(nameof (CellFontSize), typeof (float), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) 12f));
  
  public static readonly DependencyProperty \u0023\u003DzdFeuptaClY_J = DependencyProperty.Register(nameof (ColorMap), typeof (LinearGradientBrush), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) FastHeatMapRenderableSeries.\u0023\u003Dz5AeSbygYvqoF()));
  
  public static readonly DependencyProperty \u0023\u003DzyzOpBn8\u003D = DependencyProperty.Register(nameof (Minimum), typeof (double), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(FastHeatMapRenderableSeries.\u0023\u003DzJMP38d8102Gh)));
  
  public static readonly DependencyProperty \u0023\u003Dz2hIt0Yg\u003D = DependencyProperty.Register(nameof (Maximum), typeof (double), typeof (FastHeatMapRenderableSeries), new PropertyMetadata((object) 1.0, new PropertyChangedCallback(FastHeatMapRenderableSeries.\u0023\u003DzJMP38d8102Gh)));

  public FastHeatMapRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastHeatMapRenderableSeries);
  }

  public event PropertyChangedEventHandler PropertyChanged;

  private static LinearGradientBrush \u0023\u003Dz5AeSbygYvqoF()
  {
    return new LinearGradientBrush(new GradientStopCollection()
    {
      new GradientStop() { Color = Colors.Blue, Offset = 0.0 },
      new GradientStop() { Color = Colors.Red, Offset = 1.0 }
    }, 0.0);
  }

  public LinearGradientBrush ColorMap
  {
    get
    {
      return (LinearGradientBrush) this.GetValue(FastHeatMapRenderableSeries.\u0023\u003DzdFeuptaClY_J);
    }
    set
    {
      this.SetValue(FastHeatMapRenderableSeries.\u0023\u003DzdFeuptaClY_J, (object) value);
    }
  }

  public double Minimum
  {
    get
    {
      return (double) this.GetValue(FastHeatMapRenderableSeries.\u0023\u003DzyzOpBn8\u003D);
    }
    set
    {
      this.SetValue(FastHeatMapRenderableSeries.\u0023\u003DzyzOpBn8\u003D, (object) value);
    }
  }

  public double Maximum
  {
    get
    {
      return (double) this.GetValue(FastHeatMapRenderableSeries.\u0023\u003Dz2hIt0Yg\u003D);
    }
    set
    {
      this.SetValue(FastHeatMapRenderableSeries.\u0023\u003Dz2hIt0Yg\u003D, (object) value);
    }
  }

  public bool DrawTextInCell
  {
    get
    {
      return (bool) this.GetValue(FastHeatMapRenderableSeries.\u0023\u003Dzx6fa5TzXfc8S);
    }
    set
    {
      this.SetValue(FastHeatMapRenderableSeries.\u0023\u003Dzx6fa5TzXfc8S, (object) value);
    }
  }

  public Color CellTextForeground
  {
    get
    {
      return (Color) this.GetValue(FastHeatMapRenderableSeries.\u0023\u003DzzYBz11wS5ND3);
    }
    set
    {
      this.SetValue(FastHeatMapRenderableSeries.\u0023\u003DzzYBz11wS5ND3, (object) value);
    }
  }

  public float CellFontSize
  {
    get
    {
      return (float) this.GetValue(FastHeatMapRenderableSeries.\u0023\u003DzBbalbHIs26PX);
    }
    set
    {
      this.SetValue(FastHeatMapRenderableSeries.\u0023\u003DzBbalbHIs26PX, (object) value);
    }
  }

  private \u0023\u003DzPlFvps97y7rWR4vc5KUjR5Ch17PMi3H3ortKPxkjv7KR \u0023\u003Dz3cueoAsBQpe0()
  {
    return new \u0023\u003DzPlFvps97y7rWR4vc5KUjR5Ch17PMi3H3ortKPxkjv7KR()
    {
      \u0023\u003DzHu4tBcuxUMVL = this.ColorMap.GradientStops.ToArray<GradientStop>(),
      \u0023\u003Dz2wWd_ME\u003D = this.Minimum,
      \u0023\u003DzLbUokN5r_WG1 = 1.0 / Math.Abs(this.Maximum - this.Minimum)
    };
  }

  public double MiddleValue => (this.Minimum + this.Maximum) * 0.5;

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    if (this.DataSeries is \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D dataSeries)
    {
      double num1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().GetDataValue(_param1.X);
      double num2 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().GetDataValue(_param1.Y);
      zldchDrVsrVyHh6WyiGy = dataSeries.ToHitTestInfo(num1, num2, _param3);
      zldchDrVsrVyHh6WyiGy.\u0023\u003Dzo2ftAfxjqC04(_param1);
    }
    return zldchDrVsrVyHh6WyiGy;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    if (_param2.\u0023\u003DzDoU1CJhSUWFV())
      throw new NotImplementedException($"We are sorry! The vertical chart feature is not supported by the {((object) this).GetType().Name} currently.");
    \u0023\u003DzPlFvps97y7rWR4vc5KUjR5Ch17PMi3H3ortKPxkjv7KR pmi3H3ortKpxkjv7Kr = this.\u0023\u003Dz3cueoAsBQpe0();
    bool drawTextInCell = this.DrawTextInCell;
    Color cellTextForeground = this.CellTextForeground;
    float cellFontSize = this.CellFontSize;
    double height = _param1.\u0023\u003Dz8DEW4l1E337F().Height;
    int width = (int) _param1.\u0023\u003Dz8DEW4l1E337F().Width;
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    bool flag1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D();
    bool flag2 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D();
    int num1 = ftrixUnpTllY1PkTyq.\u0023\u003DzlpVGw6E\u003D();
    double opacity = this.Opacity;
    for (int index1 = 0; index1 < num1; ++index1)
    {
      \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZnF8py_1UHtuLwP\u0024jqT0El1T obj = (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZnF8py_1UHtuLwP\u0024jqT0El1T) ftrixUnpTllY1PkTyq.\u0023\u003Dz\u0024CeUvME\u003D(index1);
      double num2 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(obj.\u0023\u003Dz4UDE5UByX\u00245LtE1gJA\u003D\u003D());
      double num3 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(obj.\u0023\u003Dzk\u0024jdyMQeK6oiQLL\u00248w\u003D\u003D());
      if ((num2 >= 0.0 || num3 >= 0.0) && (num2 <= (double) width || num3 <= (double) width))
      {
        if (num2 < 0.0)
          num2 = 0.0;
        if (num3 > (double) width)
          num3 = (double) width;
        int num4 = (int) _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(obj.\u0023\u003DzhQUKpHOzWGMGzdOfNg\u003D\u003D());
        int num5 = (int) _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(obj.\u0023\u003DzWoua01IozoKYJ1eZRw\u003D\u003D());
        IList<int> intList = obj.\u0023\u003Dzv5pY0E1wS\u00245oXMKltw\u003D\u003D(pmi3H3ortKpxkjv7Kr);
        if (flag1)
          NumberUtil.Swap(ref num2, ref num3);
        for (int index2 = (int) num2; (double) index2 < num3; ++index2)
        {
          if (index2 >= 0 && index2 < width)
            _param1.\u0023\u003DztJb5\u0024zF1SLeC(index2, num4, num5, intList, opacity, flag2);
        }
        if (drawTextInCell)
        {
          IList<double> doubleList = obj.\u0023\u003Dz9\u0024EVq3uRz9gb();
          int num6 = (num4 - num5) / doubleList.Count;
          if (flag2)
            num6 *= -1;
          for (int index3 = 0; index3 < doubleList.Count; ++index3)
          {
            if (num6 > 0 && num3 > num2)
            {
              int num7 = doubleList.Count - 1 - index3;
              int num8 = num5 + (num4 - num5) * (flag2 ? index3 + 1 : num7) / doubleList.Count;
              if ((double) num8 <= height && num8 + num6 >= 0)
              {
                double num9 = doubleList[flag2 ? num7 : index3];
                _param1.\u0023\u003DzI6mwN\u0024I\u003D(this.\u0023\u003DzXMuBU\u0024yTpkCk(num9), new Rect(num2, (double) num8, num3 - num2, (double) num6), AlignmentX.Center, AlignmentY.Center, cellTextForeground, cellFontSize, (string) null, new FontWeight());
              }
            }
          }
        }
      }
    }
  }

  protected virtual string \u0023\u003DzXMuBU\u0024yTpkCk(double _param1) => _param1.ToString("N2");

  private void OnPropertyChanged(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog((object) this, new PropertyChangedEventArgs(_param1));
  }

  private static void \u0023\u003DzJMP38d8102Gh(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is FastHeatMapRenderableSeries c3TqqyayanmzqEjd))
      return;
    c3TqqyayanmzqEjd.OnPropertyChanged("MiddleValue");
  }
}
