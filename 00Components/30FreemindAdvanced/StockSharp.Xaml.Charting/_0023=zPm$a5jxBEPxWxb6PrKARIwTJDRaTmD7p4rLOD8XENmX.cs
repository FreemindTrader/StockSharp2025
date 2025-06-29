// Decompiled with JetBrains decompiler
// Type: #=zPm$a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r$gA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
internal sealed class \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D(
  ChartArea _param1) : dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ChartArea \u0023\u003DzeckSod0\u003D = _param1 ?? throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331288));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D \u0023\u003Dzos6SMwAMXZ33;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Line \u0023\u003DzujKvLh3D_ur8;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D.\u0023\u003Dz959Vc91YOIMItx\u00244vg\u003D\u003D \u0023\u003Dz31Z4JIE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzfEYbsFOKVADr;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzISs1rY1PUWf_ = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331267), typeof (bool), typeof (\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D), new PropertyMetadata((object) true));

  public bool ShowHorizontalLine
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D.\u0023\u003DzISs1rY1PUWf_);
    }
    set
    {
      this.SetValue(\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D.\u0023\u003DzISs1rY1PUWf_, (object) value);
    }
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this.\u0023\u003Dzos6SMwAMXZ33 = this.ParentSurface;
    ((UIElement) this.\u0023\u003Dzos6SMwAMXZ33).PreviewMouseDown += new MouseButtonEventHandler(this.\u0023\u003DzBnA\u00249wEdNjIT);
  }

  public override void OnDetached()
  {
    base.OnDetached();
    if (this.\u0023\u003Dzos6SMwAMXZ33 != null)
    {
      ((UIElement) this.\u0023\u003Dzos6SMwAMXZ33).PreviewMouseDown -= new MouseButtonEventHandler(this.\u0023\u003DzBnA\u00249wEdNjIT);
      this.\u0023\u003Dzos6SMwAMXZ33 = (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) null;
    }
    this.\u0023\u003DzUf222sU\u003D();
  }

  protected override void \u0023\u003DzCM2UQyuakisf()
  {
    base.\u0023\u003DzCM2UQyuakisf();
    this.\u0023\u003DzUf222sU\u003D();
  }

  public override void \u0023\u003Dz3RBcoKAPKSIX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003Dz3RBcoKAPKSIX(_param1);
    this.\u0023\u003DzUf222sU\u003D();
    this.\u0023\u003DzfEYbsFOKVADr = false;
  }

  protected override void \u0023\u003DzRhCP\u0024yGKAqZwVyg1vA\u003D\u003D()
  {
    this.\u0023\u003DzfEYbsFOKVADr = true;
    base.\u0023\u003DzRhCP\u0024yGKAqZwVyg1vA\u003D\u003D();
  }

  public override void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003Dz11bcnbUrALaA(_param1);
    if (!_param1.\u0023\u003DzCJb5Ya_8UZCR())
      return;
    this.\u0023\u003DzFkV86a8\u003D(_param1.\u0023\u003DztkyOk5amPcz3());
  }

  private void \u0023\u003DzBnA\u00249wEdNjIT(object _param1, MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzUf222sU\u003D();
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    if (!(this.\u0023\u003DzeckSod0\u003D.Chart is Chart chart) || !_param1.\u0023\u003DzCJb5Ya_8UZCR() || _param1.\u0023\u003DzgMFxvpJd_50n() != \u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D.Ctrl || _param1.\u0023\u003DzwuSh61ofE2mr() != (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 1 && _param1.\u0023\u003DzwuSh61ofE2mr() != (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 4)
      return;
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    Sides sides = _param1.\u0023\u003DzwuSh61ofE2mr() == (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 1 ? (Sides) 0 : (Sides) 1;
    double num = (double) this.YAxis.\u0023\u003DzACwLhyc\u003D(_param1.\u0023\u003DztkyOk5amPcz3().Y);
    Order order = new Order()
    {
      Side = sides,
      Price = Converter.To<Decimal>((object) num),
      Type = new OrderTypes?((OrderTypes) 0)
    };
    chart?.\u0023\u003DzGJwj2DzYuV1h(this.\u0023\u003DzeckSod0\u003D, order);
  }

  private void \u0023\u003DzFkV86a8\u003D(Point _param1)
  {
    if (!this.\u0023\u003DzfEYbsFOKVADr || Mouse.LeftButton != MouseButtonState.Released)
      return;
    if (this.\u0023\u003DzujKvLh3D_ur8 == null)
    {
      \u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D modifierSurface = this.ModifierSurface;
      if (modifierSurface == null)
        return;
      Line line1 = new Line();
      line1.X1 = 0.0;
      line1.X2 = modifierSurface.\u0023\u003Dzu2ObQ3hMALTN();
      line1.Y1 = _param1.Y;
      line1.Y2 = _param1.Y;
      line1.StrokeThickness = 1.0;
      Line line2 = line1;
      DoubleCollection doubleCollection = new DoubleCollection();
      double[] numArray = new double[2]{ 7.0, 4.0 };
      foreach (double num in numArray)
        doubleCollection.Add(num);
      line2.StrokeDashArray = doubleCollection;
      line1.Stroke = (Brush) new SolidColorBrush(Colors.OrangeRed);
      line1.IsHitTestVisible = false;
      this.\u0023\u003DzujKvLh3D_ur8 = line1;
      this.\u0023\u003DzujKvLh3D_ur8.SetBindings(UIElement.VisibilityProperty, (object) this, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331267), BindingMode.OneWay, (IValueConverter) new BoolToVisibilityConverter()
      {
        FalseValue = Visibility.Hidden
      });
      modifierSurface.\u0023\u003DzBDSV99pPo8hY().Add((UIElement) this.\u0023\u003DzujKvLh3D_ur8);
      this.\u0023\u003Dz31Z4JIE\u003D = new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIwTJDRaTmD7p4rLOD8XENmXciu1eU_Ge7r60S1WUU1r\u0024gA\u003D\u003D.\u0023\u003Dz959Vc91YOIMItx\u00244vg\u003D\u003D((UIElement) this.\u0023\u003DzujKvLh3D_ur8);
      AdornerLayer.GetAdornerLayer((Visual) this.\u0023\u003DzujKvLh3D_ur8).Add((Adorner) this.\u0023\u003Dz31Z4JIE\u003D);
    }
    double num1 = (double) this.YAxis.\u0023\u003DzACwLhyc\u003D(_param1.Y);
    this.\u0023\u003DzujKvLh3D_ur8.Y1 = _param1.Y;
    this.\u0023\u003DzujKvLh3D_ur8.Y2 = _param1.Y;
    this.\u0023\u003Dz31Z4JIE\u003D.\u0023\u003Dz0wmAX8SO\u0024zQCN\u0024Ke6Q\u003D\u003D().Text = StringHelper.Put(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331324), new object[2]
    {
      this.\u0023\u003DzieB4BQP3oCyc((Sides) 1, num1) == null ? (object) \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331104) : (object) \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331091),
      (object) num1
    });
    this.\u0023\u003Dz31Z4JIE\u003D.\u0023\u003DzLxDR0lXAxPbrXgmUDQ\u003D\u003D().Text = StringHelper.Put(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331084), new object[2]
    {
      this.\u0023\u003DzieB4BQP3oCyc((Sides) 0, num1) == null ? (object) \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331104) : (object) \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331091),
      (object) num1
    });
    this.\u0023\u003Dz31Z4JIE\u003D.\u0023\u003DzJcHd2F8ayj13(this.\u0023\u003Dz31Z4JIE\u003D.IsMouseOver);
  }

  private OrderTypes \u0023\u003DzieB4BQP3oCyc(Sides _param1, double _param2)
  {
    dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd eemgnbdtcxdA2A3Ejd = this.ParentSurface.get_RenderableSeries().OfType<dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd>().FirstOrDefault<dje_z8KP94MAVAC8XVLJD2A4JR8Q5NFBLA2X8EB75XQ5CG2QAS7W8ZPHQMTXMAE3XE28M8857AH3KGB5ERBMZUQ2EEMGNBDTCXDA2A3_ejd>();
    if (eemgnbdtcxdA2A3Ejd == null)
      return (OrderTypes) 0;
    IList list = ((\u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrFQ3G9W9xt2vxQkAWz\u0024zVnJ) eemgnbdtcxdA2A3Ejd.DataSeries).\u0023\u003Dz5xgdlG8Htgc7();
    if (list.Count == 0)
      return (OrderTypes) 0;
    double num = (double) list[list.Count - 1];
    return _param1 != null ? (_param2 >= num ? (OrderTypes) 0 : (OrderTypes) 1) : (_param2 <= num ? (OrderTypes) 0 : (OrderTypes) 1);
  }

  private void \u0023\u003DzUf222sU\u003D()
  {
    if (this.ModifierSurface == null)
      return;
    if (this.\u0023\u003DzujKvLh3D_ur8 != null)
      this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Remove((UIElement) this.\u0023\u003DzujKvLh3D_ur8);
    this.\u0023\u003DzujKvLh3D_ur8 = (Line) null;
  }

  private sealed class \u0023\u003Dz959Vc91YOIMItx\u00244vg\u003D\u003D : Adorner
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Grid \u0023\u003DzS\u0024OTg_s\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TextBlock \u0023\u003Dz9JyKjT\u0024p5TwcRYYQWvrjKuE\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TextBlock \u0023\u003DzeohlA0_r5\u0024pFHhDGdTiKpR4\u003D;

    public \u0023\u003Dz959Vc91YOIMItx\u00244vg\u003D\u003D(UIElement _param1)
      : base(_param1)
    {
      this.IsHitTestVisible = false;
      TextBlock textBlock1 = new TextBlock();
      textBlock1.Text = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331235);
      textBlock1.Background = (Brush) new SolidColorBrush(Colors.Red);
      textBlock1.Foreground = (Brush) new SolidColorBrush(Colors.White);
      textBlock1.FontSize = 11.0;
      textBlock1.VerticalAlignment = VerticalAlignment.Center;
      this.\u0023\u003Dz9JyKjT\u0024p5TwcRYYQWvrjKuE\u003D = textBlock1;
      TextBlock textBlock2 = new TextBlock();
      textBlock2.Text = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539331242);
      textBlock2.Background = (Brush) new SolidColorBrush(Colors.Green);
      textBlock2.Foreground = (Brush) new SolidColorBrush(Colors.White);
      textBlock2.FontSize = 11.0;
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      this.\u0023\u003DzeohlA0_r5\u0024pFHhDGdTiKpR4\u003D = textBlock2;
      Grid grid = new Grid();
      System.Windows.Controls.ToolTip toolTip = new System.Windows.Controls.ToolTip();
      toolTip.Content = (object) (LocalizedStrings.BuyCtrlLeftMouse + Environment.NewLine + LocalizedStrings.SellCtrlRightMouse);
      grid.ToolTip = (object) toolTip;
      this.\u0023\u003DzS\u0024OTg_s\u003D = grid;
      this.\u0023\u003DzS\u0024OTg_s\u003D.ColumnDefinitions.Add(new ColumnDefinition()
      {
        Width = new GridLength(0.0, GridUnitType.Auto)
      });
      this.\u0023\u003DzS\u0024OTg_s\u003D.RowDefinitions.Add(new RowDefinition()
      {
        Height = new GridLength(0.0, GridUnitType.Auto)
      });
      this.\u0023\u003DzS\u0024OTg_s\u003D.RowDefinitions.Add(new RowDefinition()
      {
        Height = new GridLength(0.0, GridUnitType.Auto)
      });
      Grid.SetRow((UIElement) this.\u0023\u003Dz0wmAX8SO\u0024zQCN\u0024Ke6Q\u003D\u003D(), 0);
      Grid.SetRow((UIElement) this.\u0023\u003DzLxDR0lXAxPbrXgmUDQ\u003D\u003D(), 1);
      this.\u0023\u003DzS\u0024OTg_s\u003D.Children.Add((UIElement) this.\u0023\u003Dz0wmAX8SO\u0024zQCN\u0024Ke6Q\u003D\u003D());
      this.\u0023\u003DzS\u0024OTg_s\u003D.Children.Add((UIElement) this.\u0023\u003DzLxDR0lXAxPbrXgmUDQ\u003D\u003D());
    }

    public TextBlock \u0023\u003Dz0wmAX8SO\u0024zQCN\u0024Ke6Q\u003D\u003D()
    {
      return this.\u0023\u003Dz9JyKjT\u0024p5TwcRYYQWvrjKuE\u003D;
    }

    public TextBlock \u0023\u003DzLxDR0lXAxPbrXgmUDQ\u003D\u003D()
    {
      return this.\u0023\u003DzeohlA0_r5\u0024pFHhDGdTiKpR4\u003D;
    }

    public void \u0023\u003DzJcHd2F8ayj13(bool _param1)
    {
      ((System.Windows.Controls.ToolTip) this.\u0023\u003DzS\u0024OTg_s\u003D.ToolTip).IsOpen = _param1;
    }

    protected override Size MeasureOverride(Size _param1)
    {
      this.\u0023\u003DzS\u0024OTg_s\u003D.Measure(_param1);
      return _param1;
    }

    protected override Size ArrangeOverride(Size _param1)
    {
      Line adornedElement = (Line) this.AdornedElement;
      this.\u0023\u003DzS\u0024OTg_s\u003D.Arrange(new Rect(adornedElement.X1, adornedElement.Y1 - this.\u0023\u003DzS\u0024OTg_s\u003D.ActualHeight / 2.0, this.\u0023\u003DzS\u0024OTg_s\u003D.ActualWidth, this.\u0023\u003DzS\u0024OTg_s\u003D.ActualHeight));
      return _param1;
    }

    protected override Visual GetVisualChild(int _param1)
    {
      return (Visual) this.\u0023\u003DzS\u0024OTg_s\u003D;
    }

    protected override int VisualChildrenCount => 1;
  }
}
