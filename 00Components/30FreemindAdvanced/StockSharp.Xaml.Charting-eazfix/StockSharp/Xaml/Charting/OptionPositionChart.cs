// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.OptionPositionChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class OptionPositionChart : 
  UserControl,
  IPersistable,
  IComponentConnector,
  IThemeableChart,
  IOptionPositionChart
{
  
  private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> \u0023\u003DzVCvdqIwU8qhbnG0tEA\u003D\u003D = new Func<BlackScholes, Decimal, DateTimeOffset, Decimal?>(OptionPositionChart.SomeClass34343383.SomeMethond0343.\u0023\u003DzZJejL2kCbBNrH7yrDJLH0IA\u003D);
  
  private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> \u0023\u003Dz6natRAM\u003D = new Func<BlackScholes, Decimal, DateTimeOffset, Decimal?>(OptionPositionChart.SomeClass34343383.SomeMethond0343.\u0023\u003DzHFmSM0O81iuL\u00243kqxN3iCIg\u003D);
  
  private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> \u0023\u003Dzc9HoyPSZYlUh = new Func<BlackScholes, Decimal, DateTimeOffset, Decimal?>(OptionPositionChart.SomeClass34343383.SomeMethond0343.\u0023\u003DzDsINFHGl_6n9RUocpTfki6I\u003D);
  
  private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> \u0023\u003DzUULrPCW3R3I5 = new Func<BlackScholes, Decimal, DateTimeOffset, Decimal?>(OptionPositionChart.SomeClass34343383.SomeMethond0343.\u0023\u003Dz1wU8wJ6WYCgtx6fHgEzunFw\u003D);
  
  private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> \u0023\u003DzfQoP7vvuCMpC = new Func<BlackScholes, Decimal, DateTimeOffset, Decimal?>(OptionPositionChart.SomeClass34343383.SomeMethond0343.\u0023\u003DzG9O0TpbzkV\u00248x69T63\u0024Ti64\u003D);
  
  private static readonly Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> \u0023\u003DzjXbVlj0Id0BN = new Func<BlackScholes, Decimal, DateTimeOffset, Decimal?>(OptionPositionChart.SomeClass34343383.SomeMethond0343.\u0023\u003Dzu30ElT9OOiFW9OsYoR6JiRo\u003D);
  
  private readonly ScichartSurfaceMVVM \u0023\u003DzKj7nvWQ\u003D;
  
  private readonly Dictionary<BlackScholes, (IChartLineElement, IChartLineElement)> \u0023\u003DzOK03jF5_2xLYbkS0rg\u003D\u003D;
  
  private (IChartLineElement, IChartLineElement) \u0023\u003DzOIqSskgVDoSOBMP3Kg\u003D\u003D;
  
  private Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> \u0023\u003DzYanY0A0\u003D;
  
  private readonly ChartAnnotation \u0023\u003Dz_h3U\u0024zi3bxOZ5JXm\u0024A\u003D\u003D;
  
  private BasketBlackScholes \u0023\u003DzN\u0024mcfM0\u003D;
  
  private BlackScholesGreeks \u0023\u003DzALCsL4AdEago;
  
  private bool \u0023\u003Dz6qr7HB8qT1c_;
  
  private bool \u0023\u003DzISImIRIGaVrr;
  
  private bool \u0023\u003DzJbHNSjjCv_nt;
  
  public Chart \u0023\u003DzO72kpz0\u003D;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public OptionPositionChart()
  {
    OptionPositionChart.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D dop2SzA2WchXh2wc = new OptionPositionChart.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    dop2SzA2WchXh2wc._variableSome3535 = this;
    this.InitializeComponent();
    this.\u0023\u003DzKj7nvWQ\u003D = (ScichartSurfaceMVVM) this.\u0023\u003DzO72kpz0\u003D.DataContext;
    this.\u0023\u003DzKj7nvWQ\u003D.ShowLegend = true;
    IChartAxis chartAxis1 = ((IEnumerable<IChartAxis>) this.\u0023\u003DzKj7nvWQ\u003D.Area.XAxises).First<IChartAxis>();
    IChartAxis chartAxis2 = ((IEnumerable<IChartAxis>) this.\u0023\u003DzKj7nvWQ\u003D.Area.YAxises).First<IChartAxis>();
    chartAxis1.AutoRange = false;
    chartAxis2.AutoRange = true;
    chartAxis2.TextFormatting = "0.##";
    ObservableCollection<IChartModifier> childModifiers = this.\u0023\u003DzKj7nvWQ\u003D.ChartModifier.ChildModifiers;
    IChartModifier[] chhAr3Kksm46Uy2ZyArray = new IChartModifier[5];
    fxZoomPanModifier ypbydebG6VffgcpzeEjd = new fxZoomPanModifier();
    ypbydebG6VffgcpzeEjd.XyDirection = XyDirection.XDirection;
    ypbydebG6VffgcpzeEjd.ClipModeX = ClipMode.None;
    chhAr3Kksm46Uy2ZyArray[0] = (IChartModifier) ypbydebG6VffgcpzeEjd;
    MouseWheelZoomModifier kufjwuuvR4YbN3Ejd = new MouseWheelZoomModifier();
    kufjwuuvR4YbN3Ejd.XyDirection = XyDirection.XDirection;
    chhAr3Kksm46Uy2ZyArray[1] = (IChartModifier) kufjwuuvR4YbN3Ejd;
    ZoomExtentsModifier fk4QgaphfmmujdEjd = new ZoomExtentsModifier();
    fk4QgaphfmmujdEjd.ExecuteOn = ExecuteOn.MouseDoubleClick;
    chhAr3Kksm46Uy2ZyArray[2] = (IChartModifier) fk4QgaphfmmujdEjd;
    YAxisDragModifier dgE2H48XyyA87SEjd = new YAxisDragModifier();
    dgE2H48XyyA87SEjd.AxisId = "Y";
    chhAr3Kksm46Uy2ZyArray[3] = (IChartModifier) dgE2H48XyyA87SEjd;
    RolloverModifier jhzfqwrsvK3MyA6SqEjd = new RolloverModifier();
    jhzfqwrsvK3MyA6SqEjd.ShowAxisLabels = false;
    jhzfqwrsvK3MyA6SqEjd.UseInterpolation = false;
    chhAr3Kksm46Uy2ZyArray[4] = (IChartModifier) jhzfqwrsvK3MyA6SqEjd;
    \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D<IChartModifier> ynkQ1M2eiqOdEcXa = new \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D<IChartModifier>(chhAr3Kksm46Uy2ZyArray);
    CollectionHelper.AddRange<IChartModifier>((ICollection<IChartModifier>) childModifiers, (IEnumerable<IChartModifier>) ynkQ1M2eiqOdEcXa);
    ((ICollection<IChartElement>) this.\u0023\u003DzM1p2o1yl\u0024dah()).Add((IChartElement) this.\u0023\u003Dz_h3U\u0024zi3bxOZ5JXm\u0024A\u003D\u003D);
    dop2SzA2WchXh2wc.\u0023\u003Dz0gbwL\u00244\u003D = false;
    this.Loaded += new RoutedEventHandler(dop2SzA2WchXh2wc.\u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D);
  }

  private INotifyList<IChartElement> \u0023\u003DzM1p2o1yl\u0024dah()
  {
    return this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().Elements;
  }

  public string ChartTheme
  {
    get => this.\u0023\u003DzKj7nvWQ\u003D.SelectedTheme;
    set => this.\u0023\u003DzKj7nvWQ\u003D.SelectedTheme = value;
  }

  public BasketBlackScholes Model
  {
    get => this.\u0023\u003DzN\u0024mcfM0\u003D;
    set
    {
      this.\u0023\u003DzN\u0024mcfM0\u003D = value;
      this.Clear();
      if (this.\u0023\u003DzN\u0024mcfM0\u003D == null)
        return;
      if (this.ShowSeparated)
      {
        foreach (BlackScholes innerModel in (IEnumerable<BlackScholes>) this.\u0023\u003DzN\u0024mcfM0\u003D.InnerModels)
        {
          string id = innerModel.Option.Id;
          ChartLineElement chartLineElement1 = OptionPositionChart.\u0023\u003Dq0HLdWTESvQ6GGDGtNBBTLK5Qq8Y\u0024H3q\u00248gIXe0eWBbj5IK92tA5V4eLCtjG2o_mj(id + " (NOW)", Colors.Black);
          ChartLineElement chartLineElement2 = this.ShowExpiration ? OptionPositionChart.\u0023\u003Dq0HLdWTESvQ6GGDGtNBBTLK5Qq8Y\u0024H3q\u00248gIXe0eWBbj5IK92tA5V4eLCtjG2o_mj(id + " (EXP)", Colors.SandyBrown) : (ChartLineElement) null;
          this.\u0023\u003DzOK03jF5_2xLYbkS0rg\u003D\u003D.Add(innerModel, ((IChartLineElement) chartLineElement1, (IChartLineElement) chartLineElement2));
          ((ICollection<IChartElement>) this.\u0023\u003DzM1p2o1yl\u0024dah()).Add((IChartElement) chartLineElement1);
          if (chartLineElement2 != null)
            ((ICollection<IChartElement>) this.\u0023\u003DzM1p2o1yl\u0024dah()).Add((IChartElement) chartLineElement2);
        }
      }
      else
      {
        ChartLineElement chartLineElement3 = OptionPositionChart.\u0023\u003Dq0HLdWTESvQ6GGDGtNBBTLK5Qq8Y\u0024H3q\u00248gIXe0eWBbj5IK92tA5V4eLCtjG2o_mj("NOW", Colors.Black);
        ChartLineElement chartLineElement4 = this.ShowExpiration ? OptionPositionChart.\u0023\u003Dq0HLdWTESvQ6GGDGtNBBTLK5Qq8Y\u0024H3q\u00248gIXe0eWBbj5IK92tA5V4eLCtjG2o_mj("EXP", Colors.SandyBrown) : (ChartLineElement) null;
        this.\u0023\u003DzOIqSskgVDoSOBMP3Kg\u003D\u003D = ((IChartLineElement) chartLineElement3, (IChartLineElement) chartLineElement4);
        ((ICollection<IChartElement>) this.\u0023\u003DzM1p2o1yl\u0024dah()).Add((IChartElement) chartLineElement3);
        if (chartLineElement4 == null)
          return;
        ((ICollection<IChartElement>) this.\u0023\u003DzM1p2o1yl\u0024dah()).Add((IChartElement) chartLineElement4);
      }
    }
  }

  public BlackScholesGreeks ChartParam
  {
    get => this.\u0023\u003DzALCsL4AdEago;
    set
    {
      if (this.\u0023\u003DzALCsL4AdEago == value)
        return;
      Func<BlackScholes, Decimal, DateTimeOffset, Decimal?> func;
      switch (value)
      {
        case BlackScholesGreeks.Delta:
          func = OptionPositionChart.\u0023\u003Dz6natRAM\u003D;
          break;
        case BlackScholesGreeks.Gamma:
          func = OptionPositionChart.\u0023\u003Dzc9HoyPSZYlUh;
          break;
        case BlackScholesGreeks.Vega:
          func = OptionPositionChart.\u0023\u003DzfQoP7vvuCMpC;
          break;
        case BlackScholesGreeks.Theta:
          func = OptionPositionChart.\u0023\u003DzUULrPCW3R3I5;
          break;
        case BlackScholesGreeks.Rho:
          func = OptionPositionChart.\u0023\u003DzjXbVlj0Id0BN;
          break;
        case BlackScholesGreeks.Premium:
          func = OptionPositionChart.\u0023\u003DzVCvdqIwU8qhbnG0tEA\u003D\u003D;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (value));
      }
      this.\u0023\u003DzYanY0A0\u003D = func;
      this.\u0023\u003DzALCsL4AdEago = value;
      this.Model = (BasketBlackScholes) null;
    }
  }

  public bool ShowSeparated
  {
    get => this.\u0023\u003Dz6qr7HB8qT1c_;
    set
    {
      if (this.\u0023\u003Dz6qr7HB8qT1c_ == value)
        return;
      this.\u0023\u003Dz6qr7HB8qT1c_ = value;
      this.Model = (BasketBlackScholes) null;
    }
  }

  public bool ShowExpiration
  {
    get => this.\u0023\u003DzISImIRIGaVrr;
    set
    {
      if (this.\u0023\u003DzISImIRIGaVrr == value)
        return;
      this.\u0023\u003DzISImIRIGaVrr = value;
      this.Model = (BasketBlackScholes) null;
    }
  }

  public bool UseBlackModel
  {
    get => this.\u0023\u003DzJbHNSjjCv_nt;
    set
    {
      if (this.\u0023\u003DzJbHNSjjCv_nt == value)
        return;
      this.\u0023\u003DzJbHNSjjCv_nt = value;
      this.Model = (BasketBlackScholes) null;
    }
  }

  public void Refresh(Decimal? assetPrice = null, DateTimeOffset? currentTime = null, DateTimeOffset? expiryDate = null)
  {
    BasketBlackScholes model = this.Model;
    if (model == null)
      return;
    Decimal num1 = !assetPrice.HasValue ? 0M : Unit.op_Explicit(Unit.op_Subtraction(assetPrice.Value, UnitHelper.Percents(20)));
    Decimal num2 = !assetPrice.HasValue ? 0M : Unit.op_Explicit(Unit.op_Addition(assetPrice.Value, UnitHelper.Percents(20)));
    DateTimeOffset utcNow = DateTimeOffset.UtcNow;
    if (!currentTime.HasValue)
      currentTime = new DateTimeOffset?(utcNow);
    Security underlyingAsset = model.UnderlyingAsset;
    if (!expiryDate.HasValue)
      expiryDate = new DateTimeOffset?(underlyingAsset.ExpiryDate ?? utcNow);
    Decimal num3 = underlyingAsset.PriceStep ?? 0.01M;
    if (num3 < 0M)
      throw new InvalidOperationException();
    this.\u0023\u003DzO72kpz0\u003D.Reset((IEnumerable<IChartElement>) this.\u0023\u003DzM1p2o1yl\u0024dah());
    IChartDrawData data1 = this.\u0023\u003DzO72kpz0\u003D.CreateData();
    if (this.ShowSeparated)
    {
      foreach (BlackScholes innerModel in (IEnumerable<BlackScholes>) model.InnerModels)
      {
        (IChartLineElement element1, IChartLineElement chartLineElement) = this.\u0023\u003DzOK03jF5_2xLYbkS0rg\u003D\u003D[innerModel];
        Decimal xValue = num1;
        while (xValue < num2)
        {
          IChartDrawData.IChartDrawDataItem chartDrawDataItem1 = data1.Group((double) xValue);
          Decimal? nullable1 = this.\u0023\u003DzYanY0A0\u003D(innerModel, xValue, currentTime.Value);
          chartDrawDataItem1.Add(element1, nullable1.HasValue ? Converter.To<double>((object) nullable1.GetValueOrDefault()) : double.NaN);
          if (chartLineElement != null)
          {
            IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
            IChartLineElement element2 = chartLineElement;
            Decimal? nullable2 = this.\u0023\u003DzYanY0A0\u003D(innerModel, xValue, expiryDate.Value);
            ref Decimal? local = ref nullable2;
            double num4 = local.HasValue ? Converter.To<double>((object) local.GetValueOrDefault()) : double.NaN;
            chartDrawDataItem2.Add(element2, num4);
          }
          xValue += num3;
        }
      }
    }
    else
    {
      Decimal xValue = num1;
      while (xValue < num2)
      {
        IChartDrawData.IChartDrawDataItem chartDrawDataItem3 = data1.Group((double) xValue);
        Decimal? nullable3 = this.\u0023\u003DzYanY0A0\u003D((BlackScholes) model, xValue, currentTime.Value);
        chartDrawDataItem3.Add(this.\u0023\u003DzOIqSskgVDoSOBMP3Kg\u003D\u003D.Item1, nullable3.HasValue ? Converter.To<double>((object) nullable3.GetValueOrDefault()) : double.NaN);
        if (this.\u0023\u003DzOIqSskgVDoSOBMP3Kg\u003D\u003D.Item2 != null)
        {
          IChartDrawData.IChartDrawDataItem chartDrawDataItem4 = chartDrawDataItem3;
          IChartLineElement element = this.\u0023\u003DzOIqSskgVDoSOBMP3Kg\u003D\u003D.Item2;
          Decimal? nullable4 = this.\u0023\u003DzYanY0A0\u003D((BlackScholes) model, xValue, expiryDate.Value);
          ref Decimal? local = ref nullable4;
          double num5 = local.HasValue ? Converter.To<double>((object) local.GetValueOrDefault()) : double.NaN;
          chartDrawDataItem4.Add(element, num5);
        }
        xValue += num3;
      }
    }
    ChartDrawData.AnnotationData data2 = new ChartDrawData.AnnotationData()
    {
      X1 = (IComparable) assetPrice,
      Y1 = (IComparable) 0.0
    };
    data1.Add((IChartAnnotationElement) this.\u0023\u003Dz_h3U\u0024zi3bxOZ5JXm\u0024A\u003D\u003D, (IAnnotationData) data2);
    this.\u0023\u003DzO72kpz0\u003D.Draw(data1);
  }

  private void Clear()
  {
    IChartElement[] array = ((IEnumerable<IChartElement>) this.\u0023\u003DzM1p2o1yl\u0024dah()).Where<IChartElement>(new Func<IChartElement, bool>(this.\u0023\u003DzbeCle_i43QHS_HZk\u0024A\u003D\u003D)).ToArray<IChartElement>();
    CollectionHelper.RemoveRange<IChartElement>((ICollection<IChartElement>) this.\u0023\u003DzM1p2o1yl\u0024dah(), (IEnumerable<IChartElement>) array);
    this.\u0023\u003DzOK03jF5_2xLYbkS0rg\u003D\u003D.Clear();
    this.\u0023\u003DzOIqSskgVDoSOBMP3Kg\u003D\u003D = ();
  }

  public void Load(SettingsStorage storage)
  {
    this.ChartTheme = storage.GetValue<string>("ChartTheme", this.ChartTheme);
    this.UseBlackModel = storage.GetValue<bool>("UseBlackModel", this.UseBlackModel);
  }

  public void Save(SettingsStorage storage)
  {
    storage.SetValue<string>("ChartTheme", this.ChartTheme);
    storage.SetValue<bool>("UseBlackModel", this.UseBlackModel);
  }

  IChartDrawData IThemeableChart.CreateData() => throw new NotSupportedException();

  void IThemeableChart.Draw(IChartDrawData data) => throw new NotSupportedException();

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/optionpositionchart.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.\u0023\u003DzO72kpz0\u003D = (Chart) target;
    else
      this.\u0023\u003DzQGCmQMjHdLKS = true;
  }

  public static ChartLineElement \u0023\u003Dq0HLdWTESvQ6GGDGtNBBTLK5Qq8Y\u0024H3q\u00248gIXe0eWBbj5IK92tA5V4eLCtjG2o_mj(
    string _param0,
    Color _param1)
  {
    ChartLineElement chartLineElement = new ChartLineElement();
    chartLineElement.FullTitle = _param0;
    chartLineElement.Color = _param1;
    return chartLineElement;
  }

  private bool \u0023\u003DzbeCle_i43QHS_HZk\u0024A\u003D\u003D(IChartElement _param1)
  {
    return _param1 != this.\u0023\u003Dz_h3U\u0024zi3bxOZ5JXm\u0024A\u003D\u003D;
  }

  private sealed class \u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D
  {
    public OptionPositionChart _variableSome3535;
    public bool \u0023\u003Dz0gbwL\u00244\u003D;

    public void \u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D(
      object _param1,
      RoutedEventArgs _param2)
    {
      if (this.\u0023\u003Dz0gbwL\u00244\u003D)
        return;
      this.\u0023\u003Dz0gbwL\u00244\u003D = true;
      ChartDrawData.AnnotationData data1 = new ChartDrawData.AnnotationData()
      {
        IsVisible = new bool?(true),
        IsEditable = new bool?(false),
        ShowLabel = new bool?(true),
        LabelPlacement = new LabelPlacement?(LabelPlacement.Axis),
        Stroke = (Brush) Brushes.Orange,
        Thickness = new Thickness?(new Thickness(2.0)),
        VerticalAlignment = new VerticalAlignment?(VerticalAlignment.Stretch)
      };
      IChartDrawData data2 = this._variableSome3535.\u0023\u003DzO72kpz0\u003D.CreateData();
      data2.Add((IChartAnnotationElement) this._variableSome3535.\u0023\u003Dz_h3U\u0024zi3bxOZ5JXm\u0024A\u003D\u003D, (IAnnotationData) data1);
      this._variableSome3535.\u0023\u003DzO72kpz0\u003D.Draw(data2);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly OptionPositionChart.SomeClass34343383 SomeMethond0343 = new OptionPositionChart.SomeClass34343383();

    public Decimal? \u0023\u003DzZJejL2kCbBNrH7yrDJLH0IA\u003D(
      BlackScholes _param1,
      Decimal _param2,
      DateTimeOffset _param3)
    {
      BlackScholes blackScholes = _param1;
      Decimal? nullable = new Decimal?(_param2);
      DateTimeOffset currentTime = _param3;
      Decimal? deviation = new Decimal?();
      Decimal? assetPrice = nullable;
      return blackScholes.Premium(currentTime, deviation, assetPrice);
    }

    public Decimal? \u0023\u003DzHFmSM0O81iuL\u00243kqxN3iCIg\u003D(
      BlackScholes _param1,
      Decimal _param2,
      DateTimeOffset _param3)
    {
      BlackScholes blackScholes = _param1;
      Decimal? nullable = new Decimal?(_param2);
      DateTimeOffset currentTime = _param3;
      Decimal? deviation = new Decimal?();
      Decimal? assetPrice = nullable;
      return blackScholes.Delta(currentTime, deviation, assetPrice);
    }

    public Decimal? \u0023\u003DzDsINFHGl_6n9RUocpTfki6I\u003D(
      BlackScholes _param1,
      Decimal _param2,
      DateTimeOffset _param3)
    {
      BlackScholes blackScholes = _param1;
      Decimal? nullable = new Decimal?(_param2);
      DateTimeOffset currentTime = _param3;
      Decimal? deviation = new Decimal?();
      Decimal? assetPrice = nullable;
      return blackScholes.Gamma(currentTime, deviation, assetPrice);
    }

    public Decimal? \u0023\u003Dz1wU8wJ6WYCgtx6fHgEzunFw\u003D(
      BlackScholes _param1,
      Decimal _param2,
      DateTimeOffset _param3)
    {
      BlackScholes blackScholes = _param1;
      Decimal? nullable = new Decimal?(_param2);
      DateTimeOffset currentTime = _param3;
      Decimal? deviation = new Decimal?();
      Decimal? assetPrice = nullable;
      return blackScholes.Theta(currentTime, deviation, assetPrice);
    }

    public Decimal? \u0023\u003DzG9O0TpbzkV\u00248x69T63\u0024Ti64\u003D(
      BlackScholes _param1,
      Decimal _param2,
      DateTimeOffset _param3)
    {
      BlackScholes blackScholes = _param1;
      Decimal? nullable = new Decimal?(_param2);
      DateTimeOffset currentTime = _param3;
      Decimal? deviation = new Decimal?();
      Decimal? assetPrice = nullable;
      return blackScholes.Vega(currentTime, deviation, assetPrice);
    }

    public Decimal? \u0023\u003Dzu30ElT9OOiFW9OsYoR6JiRo\u003D(
      BlackScholes _param1,
      Decimal _param2,
      DateTimeOffset _param3)
    {
      BlackScholes blackScholes = _param1;
      Decimal? nullable = new Decimal?(_param2);
      DateTimeOffset currentTime = _param3;
      Decimal? deviation = new Decimal?();
      Decimal? assetPrice = nullable;
      return blackScholes.Rho(currentTime, deviation, assetPrice);
    }
  }
}
