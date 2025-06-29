// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.BubbleChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Charts;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The graphical component to display bubble chart.</summary>
/// <summary>BubbleChart</summary>
public class BubbleChart : UserControl, IComponentConnector
{
  
  internal XYDiagram2D \u0023\u003DzC0s2qwQ\u003D;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.BubbleChart" />.
  /// </summary>
  public BubbleChart() => this.InitializeComponent();

  private BubbleSeries2D \u0023\u003Dz_wRpn38\u003D(Color? _param1)
  {
    BubbleSeries2D bubbleSeries2D1 = new BubbleSeries2D();
    bubbleSeries2D1.Transparency = 0.2;
    bubbleSeries2D1.AutoSize = true;
    bubbleSeries2D1.ColorEach = !_param1.HasValue;
    BubbleSeries2D bubbleSeries2D2 = bubbleSeries2D1;
    this.\u0023\u003DzC0s2qwQ\u003D.Series.Add((Series) bubbleSeries2D2);
    return bubbleSeries2D2;
  }

  /// <summary>
  /// </summary>
  public BubbleChart.IBubbleSeries<DateTime> CreateTimeSeries(Color? color)
  {
    return (BubbleChart.IBubbleSeries<DateTime>) new BubbleChart.\u0023\u003Dz3j8PT5y_w1jq<DateTime>(this.\u0023\u003Dz_wRpn38\u003D(color), BubbleChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D ?? (BubbleChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D = new Func<DateTime, SeriesPoint>(BubbleChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzpXJS7zsG3ayStA6Cv3aiwRc\u003D)));
  }

  /// <summary>
  /// </summary>
  public BubbleChart.IBubbleSeries<double> CreateDoubleSeries(Color? color)
  {
    return (BubbleChart.IBubbleSeries<double>) new BubbleChart.\u0023\u003Dz3j8PT5y_w1jq<double>(this.\u0023\u003Dz_wRpn38\u003D(color), BubbleChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D ?? (BubbleChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D = new Func<double, SeriesPoint>(BubbleChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzlSVLoTzOi9mOI2TX7beRTB8\u003D)));
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.\u0023\u003DzC0s2qwQ\u003D = (XYDiagram2D) target;
    else
      this.\u0023\u003DzQGCmQMjHdLKS = true;
  }

  private sealed class \u0023\u003Dz3j8PT5y_w1jq<T>(
    BubbleSeries2D _param1,
    Func<T, SeriesPoint> _param2) : 
    BubbleChart.IBubbleSeries<T>
    where T : IComparable
  {
    
    private readonly BubbleSeries2D \u0023\u003DzlkmfHYgr1H49 = _param1 ?? throw new ArgumentNullException("");
    
    private readonly Func<T, SeriesPoint> \u0023\u003DzEcmsYfw\u003D = _param2 ?? throw new ArgumentNullException("");

    void BubbleChart.IBubbleSeries<T>.\u0023\u003DzGf68ilGq59TJ0aVKr0K_9TbJNTZpqpXQEMLACkc_Y0FCQ79Vng\u003D\u003D(
      IEnumerable<T> _param1,
      IEnumerable<double> _param2,
      IEnumerable<double> _param3)
    {
      BubbleChart.\u0023\u003Dz3j8PT5y_w1jq<T>.\u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D u5Svx6MhYdSkOpoa = new BubbleChart.\u0023\u003Dz3j8PT5y_w1jq<T>.\u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D();
      u5Svx6MhYdSkOpoa.\u0023\u003DzRRvwDu67s9Rm = this;
      T[] array = _param1.ToArray<T>();
      u5Svx6MhYdSkOpoa.\u0023\u003Dzj_CDbmQ\u003D = _param2.ToArray<double>();
      u5Svx6MhYdSkOpoa.\u0023\u003Dz4PSczfw\u003D = _param3.ToArray<double>();
      this.\u0023\u003DzlkmfHYgr1H49.Points.AddRange(((IEnumerable<T>) array).Select<T, SeriesPoint>(new Func<T, int, SeriesPoint>(u5Svx6MhYdSkOpoa.\u0023\u003DzrzDZ5OVIPyBNGNp\u00241sguT43C6ka8toxgnazul1zIRoYJE94gVtBF8ErxvX1e)));
    }

    void BubbleChart.IBubbleSeries<T>.\u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPS150wdkA6rm99k00_RKpVcVh9qnoQ\u003D\u003D()
    {
      this.\u0023\u003DzlkmfHYgr1H49.Points.Clear();
    }

    private sealed class \u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D
    {
      public BubbleChart.\u0023\u003Dz3j8PT5y_w1jq<T> \u0023\u003DzRRvwDu67s9Rm;
      public double[] \u0023\u003Dzj_CDbmQ\u003D;
      public double[] \u0023\u003Dz4PSczfw\u003D;

      internal SeriesPoint \u0023\u003DzrzDZ5OVIPyBNGNp\u00241sguT43C6ka8toxgnazul1zIRoYJE94gVtBF8ErxvX1e(
        T _param1,
        int _param2)
      {
        SeriesPoint point = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzEcmsYfw\u003D(_param1);
        point.Value = this.\u0023\u003Dzj_CDbmQ\u003D[_param2];
        BubbleSeries2D.SetWeight(point, this.\u0023\u003Dz4PSczfw\u003D[_param2]);
        return point;
      }
    }
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly BubbleChart.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new BubbleChart.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<DateTime, SeriesPoint> \u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D;
    public static Func<double, SeriesPoint> \u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D;

    internal SeriesPoint \u0023\u003DzpXJS7zsG3ayStA6Cv3aiwRc\u003D(DateTime _param1)
    {
      return new SeriesPoint(_param1);
    }

    internal SeriesPoint \u0023\u003DzlSVLoTzOi9mOI2TX7beRTB8\u003D(double _param1)
    {
      return new SeriesPoint(_param1);
    }
  }

  /// <summary>
  /// </summary>
  public interface IBubbleSeries<TX> where TX : IComparable
  {
    /// <summary>
    /// Appends a collection of X, Y and Z points to the series, automatically triggering a redraw
    /// </summary>
    /// <param name="x">The X-values</param>
    /// <param name="y">The Y-values</param>
    /// <param name="z">The Z-values</param>
    void Append(IEnumerable<TX> x, IEnumerable<double> y, IEnumerable<double> z);

    /// <summary>
    /// </summary>
    void Clear();
  }
}
