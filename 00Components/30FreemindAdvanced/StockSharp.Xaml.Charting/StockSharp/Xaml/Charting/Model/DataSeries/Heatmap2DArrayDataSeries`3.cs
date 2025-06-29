// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.Heatmap2DArrayDataSeries`3
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.Model.DataSeries;

internal sealed class Heatmap2DArrayDataSeries<TX, TY, TZ> : 
  \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>,
  \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D,
  \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D,
  \u0023\u003Dzboj3ckhISv7k6koCkTeIfz13hju1HWdmJxf054ica0kRcYMVoV1Kpw0\u003D,
  \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D
  where TX : IComparable
  where TY : IComparable
  where TZ : IComparable
{
  private readonly Func<int, TX> _xMapping;
  private readonly Func<int, TY> _yMapping;
  private readonly TZ[,] _array2D;
  private double[,] _cachedArray2D;
  private int[,] _cachedArgbColorArray2D;
  private \u0023\u003DzPlFvps97y7rWR4vc5KUjR5Ch17PMi3H3ortKPxkjv7KR _cachedMappingSettings;
  private readonly object _syncRoot = new object();
  private \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D _parentSurface;

  public Heatmap2DArrayDataSeries(TZ[,] array2D, Func<int, TX> xMapping, Func<int, TY> yMapping)
  {
    if (array2D == null)
      throw new ArgumentNullException();
    this.AcceptsUnsortedData = false;
    this._array2D = array2D;
    this._xMapping = xMapping;
    this._yMapping = yMapping;
  }

  event EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X> \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EDataSeriesChanged
  {
    add
    {
    }
    remove
    {
    }
  }

  public object SyncRoot => this._syncRoot;

  public bool AcceptsUnsortedData { get; set; }

  public bool IsEvenlySpaced => true;

  \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EParentSurface
  {
    get => this._parentSurface;
    set => this._parentSurface = value;
  }

  public string SeriesName { get; set; }

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EXRange
  {
    get
    {
      return (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(this._xMapping(0).\u0023\u003Dzb9UCYbo\u003D(), this._xMapping(this.ArrayWidth - 1).\u0023\u003Dzb9UCYbo\u003D());
    }
  }

  IComparable \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EYMin
  {
    get => (IComparable) this._yMapping(0);
  }

  IComparable \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EYMax
  {
    get => (IComparable) this._yMapping(this.ArrayHeight - 1);
  }

  IComparable \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EXMin
  {
    get => (IComparable) this._xMapping(0);
  }

  IComparable \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EXMax
  {
    get => (IComparable) this._xMapping(this.ArrayWidth - 1);
  }

  bool \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EIsFifo
  {
    get => false;
  }

  bool \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EHasValues
  {
    get => this.ArrayHeight != 0 && this.ArrayWidth != 0;
  }

  bool \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EIsSecondary
  {
    get => false;
  }

  int \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002ECount
  {
    get => this.ArrayWidth;
  }

  bool \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EIsSorted
  {
    get => true;
  }

  public Type XType => typeof (TX);

  public Type YType => typeof (TY);

  private int ArrayWidth => this._array2D.GetLength(1);

  private int ArrayHeight => this._array2D.GetLength(0);

  int \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIHeatmap2DArrayDataSeries\u002EArrayWidth
  {
    get => this.ArrayWidth;
  }

  int \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIHeatmap2DArrayDataSeries\u002EArrayHeight
  {
    get => this.ArrayHeight;
  }

  IList \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EXValues
  {
    get
    {
      return (IList) ((\u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>) this).get_XValues();
    }
  }

  IList \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EYValues
  {
    get
    {
      return (IList) ((\u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>) this).get_YValues();
    }
  }

  public IComparable LatestYValue => (IComparable) null;

  IList<TX> \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002EXValues
  {
    get
    {
      TX[] xvalues = new TX[this.ArrayWidth];
      for (int index = 0; index < xvalues.Length; ++index)
        xvalues[index] = this._xMapping(index);
      return (IList<TX>) xvalues;
    }
  }

  IList<TY> \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002EYValues
  {
    get
    {
      TY[] yvalues = new TY[this.ArrayHeight];
      for (int index = 0; index < yvalues.Length; ++index)
        yvalues[index] = this._yMapping(index);
      return (IList<TY>) yvalues;
    }
  }

  int[,] \u0023\u003Dzboj3ckhISv7k6koCkTeIfz13hju1HWdmJxf054ica0kRcYMVoV1Kpw0\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIHeatmap2DArrayDataSeriesInternal\u002EGetArgbColorArray2D(
    \u0023\u003DzPlFvps97y7rWR4vc5KUjR5Ch17PMi3H3ortKPxkjv7KR mappingSettings)
  {
    if (this._cachedArgbColorArray2D == null || !mappingSettings.Equals((object) this._cachedMappingSettings))
    {
      int arrayHeight = this.ArrayHeight;
      int arrayWidth = this.ArrayWidth;
      this._cachedArgbColorArray2D = new int[arrayHeight, arrayWidth];
      for (int index1 = 0; index1 < arrayHeight; ++index1)
      {
        for (int index2 = 0; index2 < arrayWidth; ++index2)
          this._cachedArgbColorArray2D[index1, index2] = Heatmap2DArrayDataSeries<TX, TY, TZ>.DoubleToArgbColor(this._array2D[index1, index2].\u0023\u003Dzb9UCYbo\u003D(), mappingSettings);
      }
      this._cachedMappingSettings = mappingSettings;
    }
    return this._cachedArgbColorArray2D;
  }

  private static int DoubleToArgbColor(
    double x,
    \u0023\u003DzPlFvps97y7rWR4vc5KUjR5Ch17PMi3H3ortKPxkjv7KR mappingSettings)
  {
    x -= mappingSettings.\u0023\u003Dz2wWd_ME\u003D;
    x *= mappingSettings.\u0023\u003DzLbUokN5r_WG1;
    if (mappingSettings.\u0023\u003Dz5E7OGR0KE6WL == null)
    {
      mappingSettings.\u0023\u003Dz5E7OGR0KE6WL = new int[1000];
      for (int index1 = 0; index1 < mappingSettings.\u0023\u003Dz5E7OGR0KE6WL.Length; ++index1)
      {
        double num1 = (double) index1 / (double) mappingSettings.\u0023\u003Dz5E7OGR0KE6WL.Length;
        GradientStop[] zHu4tBcuxUmvl = mappingSettings.\u0023\u003DzHu4tBcuxUMVL;
        int num2;
        if (zHu4tBcuxUmvl.Length > 1)
        {
          GradientStop gradientStop1 = zHu4tBcuxUmvl[0];
          for (int index2 = 1; index2 < zHu4tBcuxUmvl.Length; ++index2)
          {
            GradientStop gradientStop2 = zHu4tBcuxUmvl[index2];
            if (num1 < gradientStop2.Offset)
            {
              double offset1 = gradientStop1.Offset;
              Color color1 = gradientStop1.Color;
              double offset2 = gradientStop2.Offset;
              Color color2 = gradientStop2.Color;
              num2 = Heatmap2DArrayDataSeries<TX, TY, TZ>.DoubleToArgbColor((num1 - offset1) / (offset2 - offset1), (int) color1.A, (int) color1.R, (int) color1.G, (int) color1.B, (int) color2.A, (int) color2.R, (int) color2.G, (int) color2.B);
              goto label_9;
            }
            gradientStop1 = gradientStop2;
          }
        }
        Color color = mappingSettings.\u0023\u003DzHu4tBcuxUMVL[mappingSettings.\u0023\u003DzHu4tBcuxUMVL.Length - 1].Color;
        num2 = -16777216 /*0xFF000000*/ | (int) color.R << 16 /*0x10*/ | (int) color.G << 8 | (int) color.B;
label_9:
        mappingSettings.\u0023\u003Dz5E7OGR0KE6WL[index1] = num2;
      }
    }
    int index = (int) (x * (double) mappingSettings.\u0023\u003Dz5E7OGR0KE6WL.Length);
    if (index < 0)
      index = 0;
    else if (index >= mappingSettings.\u0023\u003Dz5E7OGR0KE6WL.Length)
      index = mappingSettings.\u0023\u003Dz5E7OGR0KE6WL.Length - 1;
    return mappingSettings.\u0023\u003Dz5E7OGR0KE6WL[index];
  }

  private static int DoubleToArgbColor(
    double x,
    int a1,
    int r1,
    int g1,
    int b1,
    int a2,
    int r2,
    int g2,
    int b2)
  {
    if (x > 1.0)
      x = 1.0;
    if (x < 0.0)
      x = 0.0;
    return a1 + (int) ((double) (a2 - a1) * x) << 24 | r1 + (int) ((double) (r2 - r1) * x) << 16 /*0x10*/ | g1 + (int) ((double) (g2 - g1) * x) << 8 | b1 + (int) ((double) (b2 - b1) * x);
  }

  double[,] \u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIHeatmap2DArrayDataSeries\u002EGetArray2D()
  {
    if (this._cachedArray2D == null)
    {
      int length1 = this._array2D.GetLength(0);
      int length2 = this._array2D.GetLength(1);
      this._cachedArray2D = new double[length1, length2];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
          this._cachedArray2D[index1, index2] = this._array2D[index1, index2].\u0023\u003Dzb9UCYbo\u003D();
      }
    }
    return this._cachedArray2D;
  }

  [Obsolete("IsAttached is obsolete because there is no DataSeriesSet now")]
  bool \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EIsAttached
  {
    get => throw new NotImplementedException();
  }

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EYRange
  {
    get
    {
      return (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(this._yMapping(0).\u0023\u003Dzb9UCYbo\u003D(), this._yMapping(this.ArrayHeight - 1).\u0023\u003Dzb9UCYbo\u003D());
    }
  }

  \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EDataSeriesType
  {
    get => (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 7;
  }

  int? \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EFifoCapacity
  {
    get => throw new NotImplementedException();
    set => throw new NotImplementedException();
  }

  public int FindClosestLine(
    int start,
    int count,
    IComparable x,
    IComparable y,
    double xyScaleRatio,
    double maxXDistance)
  {
    throw new NotImplementedException();
  }

  public int FindClosestPoint(
    int start,
    int count,
    IComparable x,
    IComparable y,
    double xyScaleRatio,
    double maxXDistance)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002EAppend(
    TX x,
    params TY[] yValues)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002EAppend(
    IEnumerable<TX> x,
    params IEnumerable<TY>[] yValues)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002ERemove(
    TX x)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002ERemoveAt(
    int index)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002ERemoveRange(
    int startIndex,
    int count)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EClear()
  {
    throw new NotImplementedException();
  }

  \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY> \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002EClone()
  {
    throw new NotImplementedException();
  }

  TY \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002EGetYMinAt(
    int index,
    TY existingYMin)
  {
    throw new NotImplementedException();
  }

  TY \u0023\u003DzTbSy5Tg7CNKewHb2FguXq\u00249fYrtRMypdmYI2qF8ZEFkx<TX, TY>.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u003CTX\u002CTY\u003E\u002EGetYMaxAt(
    int index,
    TY existingYMax)
  {
    throw new NotImplementedException();
  }

  \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EToPointSeries(
    IList column,
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D resamplingMode,
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D pointRange,
    int viewportWidth,
    bool isCategoryAxis)
  {
    throw new NotImplementedException();
  }

  int \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EFindIndex(
    IComparable x,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D searchMode)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EInvalidateParentSurface(
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_f7seBjAlONQGY47Zh8\u003D rangeMode)
  {
    throw new NotImplementedException();
  }

  public \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D ToStackedDataSeriesComponent(
    IEnumerable<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D> previousDataSeriesInSameGroup)
  {
    throw new NotImplementedException();
  }

  public int FindClosestPoint(
    IComparable x,
    IComparable y,
    double xyScaleRatio,
    double maxXDistance)
  {
    throw new NotImplementedException();
  }

  public int FindClosestLine(
    IComparable x,
    IComparable y,
    double xyScaleRatio,
    double maxXDistance,
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D drawNanAs)
  {
    throw new NotImplementedException();
  }

  public void OnBeginRenderPass()
  {
  }

  bool \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EISuspendable\u002EIsSuspended
  {
    get => throw new NotImplementedException();
  }

  \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EISuspendable\u002ESuspendUpdates()
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EISuspendable\u002EResumeUpdates(
    \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote suspender)
  {
    throw new NotImplementedException();
  }

  void \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EISuspendable\u002EDecrementSuspend()
  {
    throw new NotImplementedException();
  }

  \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetIndicesRange(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D visibleRange)
  {
    return new \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D(0, this.ArrayWidth - 1);
  }

  \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EToPointSeries(
    \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D resamplingMode,
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D pointRange,
    int viewportWidth,
    bool isCategoryAxis,
    bool? dataIsDisplayedAs2D,
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D visibleXRange,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D factory,
    object pointSeriesArg)
  {
    return (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) new \u0023\u003Dz\u0024ZziqW8v\u0024JjjV5dA4z4_CaT59YnEciXH9I0nFFXWH2DvMJoLMw\u003D\u003D<TX, TY>((\u0023\u003DzKasBY8yFp0kHGchcdspopBzm5WEkx4_svXlI48ABMxC7sN4E32vyGbw\u003D) this, this._xMapping, this._yMapping);
  }

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetWindowedYRange(
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D xIndexRange)
  {
    return this.GetYRange();
  }

  private \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D GetYRange()
  {
    return (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(this._yMapping(0).\u0023\u003Dzb9UCYbo\u003D(), this._yMapping(this.ArrayHeight).\u0023\u003Dzb9UCYbo\u003D());
  }

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetWindowedYRange(
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D xIndexRange,
    bool getPositiveRange)
  {
    return this.GetYRange();
  }

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetWindowedYRange(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D xRange,
    bool getPositiveRange)
  {
    return this.GetYRange();
  }

  \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EGetWindowedYRange(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D xRange)
  {
    return this.GetYRange();
  }

  \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D.StockSharp\u002EXaml\u002ECharting\u002EModel\u002EDataSeries\u002EIDataSeries\u002EToHitTestInfo(
    int index)
  {
    return this.GetHitTestInfo(new int?(index), new int?(index));
  }

  private \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D GetHitTestInfo(
    int? xIndex,
    int? yIndex)
  {
    lock (this.SyncRoot)
    {
      bool flag = xIndex.HasValue && yIndex.HasValue;
      IComparable comparable = (IComparable) null;
      if (flag)
        comparable = (IComparable) this._array2D[yIndex.Value, xIndex.Value];
      \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D hitTestInfo = new \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D();
      hitTestInfo.\u0023\u003DzOCYm7g4gfYSc(this.SeriesName);
      hitTestInfo.\u0023\u003DzQ9xCEGz0Gl\u0024q((\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 7);
      hitTestInfo.ZValue = comparable;
      hitTestInfo.\u0023\u003Dzn3o1RS9wuET8(flag);
      hitTestInfo.\u0023\u003DzZjtwJshPYJrbgaR43Q\u003D\u003D(flag);
      hitTestInfo.\u0023\u003DzkNMVgQ88lfxP(flag);
      hitTestInfo = hitTestInfo;
      return hitTestInfo;
    }
  }

  public \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D ToHitTestInfo(
    double xValue,
    double yValue,
    bool interpolateXy = true)
  {
    int? index1 = this.GetIndex<TX>(this._xMapping, xValue, this.ArrayWidth);
    int? index2 = this.GetIndex<TY>(this._yMapping, yValue, this.ArrayHeight);
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D hitTestInfo = this.GetHitTestInfo(index1, index2);
    if (interpolateXy)
    {
      hitTestInfo.\u0023\u003Dz2Iv\u0024sxQuGDBR((IComparable) xValue);
      hitTestInfo.\u0023\u003DzBswzhzuQHrrX((IComparable) yValue);
    }
    else if (hitTestInfo.\u0023\u003Dzmh1LiTa467ce())
    {
      hitTestInfo.\u0023\u003Dz2Iv\u0024sxQuGDBR((IComparable) this._xMapping(index1 ?? -1));
      hitTestInfo.\u0023\u003DzBswzhzuQHrrX((IComparable) this._yMapping(index2 ?? -1));
    }
    return hitTestInfo;
  }

  private int? GetIndex<T>(Func<int, T> mapping, double value, int dimension) where T : IComparable
  {
    for (int index = 0; index < dimension; ++index)
    {
      if (mapping(index + 1).\u0023\u003Dzb9UCYbo\u003D() >= value && mapping(index).\u0023\u003Dzb9UCYbo\u003D() < value)
        return new int?(index);
    }
    return new int?();
  }
}
