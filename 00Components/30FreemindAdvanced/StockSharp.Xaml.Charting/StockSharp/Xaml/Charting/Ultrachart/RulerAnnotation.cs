// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Ultrachart.RulerAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Common;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

#nullable enable
namespace StockSharp.Xaml.Charting.Ultrachart;

internal class RulerAnnotation : AnnotationBase
{
  public static readonly 
  #nullable disable
  DependencyProperty RulerWidthProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328577), typeof (double), typeof (RulerAnnotation), new PropertyMetadata((object) 0.0));
  public static readonly DependencyProperty RulerHeightProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328626), typeof (double), typeof (RulerAnnotation), new PropertyMetadata((object) 0.0));
  public static readonly DependencyProperty Text1Property = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328616), typeof (string), typeof (RulerAnnotation), new PropertyMetadata((PropertyChangedCallback) null));
  public static readonly DependencyProperty Text2Property = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328916), typeof (string), typeof (RulerAnnotation), new PropertyMetadata((PropertyChangedCallback) null));
  private static DispatcherTimer _timer;
  private double _priceStep;
  private bool _subscribed;
  private bool _needToUpdate;
  private bool _removed;
  private Grid _rulerArea;
  private Border _textBorder;

  static RulerAnnotation()
  {
    Control.BackgroundProperty.OverrideMetadata(typeof (RulerAnnotation), (PropertyMetadata) new FrameworkPropertyMetadata((object) new SolidColorBrush((Color) ColorConverter.ConvertFromString(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328928)))));
  }

  public RulerAnnotation()
  {
    this.DefaultStyleKey = (object) typeof (RulerAnnotation);
    if (RulerAnnotation._timer != null)
      return;
    RulerAnnotation._timer = new DispatcherTimer()
    {
      Interval = TimeSpan.FromMilliseconds(200.0)
    };
    RulerAnnotation._timer.Start();
    RulerAnnotation._timer.Tick += RulerAnnotation.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D ?? (RulerAnnotation.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D = new EventHandler(RulerAnnotation.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzgD6Rh__eqnmCn0m__iG5ses\u003D));
  }

  public double RulerWidth
  {
    get => (double) this.GetValue(RulerAnnotation.RulerWidthProperty);
    set => this.SetValue(RulerAnnotation.RulerWidthProperty, (object) value);
  }

  public double RulerHeight
  {
    get => (double) this.GetValue(RulerAnnotation.RulerHeightProperty);
    set => this.SetValue(RulerAnnotation.RulerHeightProperty, (object) value);
  }

  public string Text1
  {
    get => (string) this.GetValue(RulerAnnotation.Text1Property);
    set => this.SetValue(RulerAnnotation.Text1Property, (object) value);
  }

  public string Text2
  {
    get => (string) this.GetValue(RulerAnnotation.Text2Property);
    set => this.SetValue(RulerAnnotation.Text2Property, (object) value);
  }

  private static event Action TimerTick;

  public double PriceStep
  {
    get => this._priceStep;
    set
    {
      this._priceStep = value >= 0.0 ? value : throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328912));
      this._needToUpdate = true;
    }
  }

  public bool RemoveOnClick { get; set; }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.AnnotationRoot = (FrameworkElement) this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Grid>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328955));
    this._rulerArea = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Grid>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328977));
    this._textBorder = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Border>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539328966));
  }

  protected override Cursor GetSelectedCursor() => (Cursor) null;

  protected override void MakeInvisible()
  {
    base.MakeInvisible();
    if (!this._subscribed)
      return;
    this._subscribed = false;
    RulerAnnotation.TimerTick -= new Action(this.TimerOnTick);
  }

  protected override void MakeVisible(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates)
  {
    base.MakeVisible(coordinates);
    if (this._subscribed)
      return;
    this._subscribed = true;
    RulerAnnotation.TimerTick += new Action(this.TimerOnTick);
    this._needToUpdate = true;
  }

  protected override void AttachInteractionHandlersTo(FrameworkElement source)
  {
    this._removed = false;
    ((UIElement) this.ParentSurface).PreviewMouseDown += new MouseButtonEventHandler(this.OnSurfacePreviewMouseDown);
  }

  protected override void DetachInteractionHandlersFrom(FrameworkElement source)
  {
    this.TryRemoveSelf();
  }

  private void OnSurfacePreviewMouseDown(object sender, MouseButtonEventArgs e)
  {
    this.TryRemoveSelf();
  }

  private void TryRemoveSelf()
  {
    if (this._removed || !this.RemoveOnClick)
      return;
    this._removed = true;
    ((UIElement) this.ParentSurface).PreviewMouseDown -= new MouseButtonEventHandler(this.OnSurfacePreviewMouseDown);
    this.ParentSurface.get_Annotations().Remove((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) this);
  }

  private static string FormatDimeDiff(TimeSpan ts)
  {
    List<string> stringList = new List<string>();
    if (ts > TimeSpan.FromDays(1.0))
      stringList.Add(((int) ts.TotalDays).ToString() + LocalizedStrings.DaysLetter);
    if (ts.Hours != 0)
      stringList.Add(ts.Hours.ToString() + LocalizedStrings.HoursLetter);
    if (ts.Minutes != 0)
      stringList.Add(ts.Minutes.ToString() + LocalizedStrings.MinutesLetter);
    if (ts.Seconds != 0)
      stringList.Add(ts.Seconds.ToString() + LocalizedStrings.SecondsLetter);
    return StringHelper.JoinSpace((IEnumerable<string>) stringList);
  }

  private void UpdateData()
  {
    this._needToUpdate = false;
    if (!(this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0))
      return;
    int num1;
    switch (this.X1)
    {
      case DateTime dateTime1:
        num1 = q9i0MXI7Qb9c1V6c0.\u0023\u003DzFk6sufr\u0024co4e(dateTime1);
        break;
      case int num2:
        num1 = num2;
        break;
      default:
        return;
    }
    int num3;
    switch (this.X2)
    {
      case DateTime dateTime2:
        num3 = q9i0MXI7Qb9c1V6c0.\u0023\u003DzFk6sufr\u0024co4e(dateTime2);
        break;
      case int num4:
        num3 = num4;
        break;
      default:
        return;
    }
    double num5 = (double) this.Y1;
    double num6 = (double) this.Y2;
    if (this._priceStep > 0.0)
    {
      num5 = num5.\u0023\u003Dzezm_VedE86O1(this._priceStep);
      num6 = num6.\u0023\u003Dzezm_VedE86O1(this._priceStep);
    }
    if (num1 > num3)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref num1, ref num3);
    if (num5 > num6)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref num5, ref num6);
    double num7 = num6 - num5;
    int num8 = this._priceStep > 0.0 ? (int) Math.Round(num7 / this._priceStep) : 0;
    string str1 = (string) null;
    if (q9i0MXI7Qb9c1V6c0 != null)
    {
      DateTime dateTime3 = q9i0MXI7Qb9c1V6c0.\u0023\u003DzWZQlXHuDrnKc(num1);
      str1 = RulerAnnotation.FormatDimeDiff(q9i0MXI7Qb9c1V6c0.\u0023\u003DzWZQlXHuDrnKc(num3) - dateTime3);
    }
    string str2 = $"{num7}";
    DefaultInterpolatedStringHandler interpolatedStringHandler;
    string str3;
    if (num8 != 0)
    {
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427378));
      interpolatedStringHandler.AppendFormatted<int>(num8);
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432316));
      interpolatedStringHandler.AppendFormatted(LocalizedStrings.Pips);
      str3 = interpolatedStringHandler.ToStringAndClear();
    }
    else
      str3 = string.Empty;
    this.Text1 = str2 + str3;
    interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
    interpolatedStringHandler.AppendFormatted(LocalizedStrings.Bars);
    interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539329097));
    interpolatedStringHandler.AppendFormatted<int>(num3 - num1);
    interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427378));
    interpolatedStringHandler.AppendFormatted(str1);
    this.Text2 = interpolatedStringHandler.ToStringAndClear();
  }

  private void TimerOnTick()
  {
    if (!this._needToUpdate || !this.IsAttached)
      return;
    this.UpdateData();
    this.Refresh();
  }

  public override bool IsPointWithinBounds(Point point)
  {
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates = this.GetCoordinates(this.GetCanvas(this.AnnotationCanvas), this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D(), this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D());
    return new Rect(new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE), new Point(coordinates.\u0023\u003Dz6aJoeqoqAzym, coordinates.\u0023\u003DzWp13vlQiZCJc)).Contains(point);
  }

  protected override IComparable FromCoordinate(
    double coord,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB axis)
  {
    IComparable comparable = base.FromCoordinate(coord, axis);
    if (axis != this.YAxis)
      return comparable;
    double num = (double) comparable;
    return (IComparable) (this._priceStep > 0.0 ? num.\u0023\u003Dzezm_VedE86O1(this._priceStep) : num);
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new RulerAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  private void PlaceAnnotationImpl(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coord)
  {
    this.UpdateData();
    double length = coord.\u0023\u003DzS2_K6sVvd5IY;
    double z6aJoeqoqAzym = coord.\u0023\u003Dz6aJoeqoqAzym;
    double z2J4l3QuGwZhe = coord.\u0023\u003Dz2J4l3QUGwZHE;
    double zWp13vlQiZcJc = coord.\u0023\u003DzWp13vlQiZCJc;
    this._textBorder.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
    if (z6aJoeqoqAzym < length)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref length, ref z6aJoeqoqAzym);
    if (zWp13vlQiZcJc < z2J4l3QuGwZhe)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref z2J4l3QuGwZhe, ref zWp13vlQiZcJc);
    this.RulerWidth = z6aJoeqoqAzym - length;
    this.RulerHeight = zWp13vlQiZcJc - z2J4l3QuGwZhe;
    double rulerWidth = this.RulerWidth;
    Size desiredSize = this._textBorder.DesiredSize;
    double width = desiredSize.Width;
    if (rulerWidth < width)
    {
      double num1 = length;
      desiredSize = this._textBorder.DesiredSize;
      double num2 = (desiredSize.Width - this.RulerWidth) / 2.0;
      length = num1 - num2;
    }
    Canvas.SetLeft((UIElement) this, length);
    Canvas.SetTop((UIElement) this, z2J4l3QuGwZhe);
  }

  private sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(RulerAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<RulerAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      this.\u0023\u003Dz_iIh83yfe01U().PlaceAnnotationImpl(_param1);
    }
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly RulerAnnotation.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new RulerAnnotation.\u0023\u003Dz7qOdpi4\u003D();
    public static EventHandler \u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D;

    internal void \u0023\u003DzgD6Rh__eqnmCn0m__iG5ses\u003D(
    #nullable enable
    object? _param1, EventArgs _param2)
    {
      Action timerTick = RulerAnnotation.TimerTick;
      if (timerTick == null)
        return;
      timerTick();
    }
  }
}
