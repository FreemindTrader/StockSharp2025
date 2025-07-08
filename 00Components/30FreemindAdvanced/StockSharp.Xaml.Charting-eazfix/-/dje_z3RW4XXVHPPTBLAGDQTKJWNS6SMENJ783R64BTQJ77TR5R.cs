// Decompiled with JetBrains decompiler
// Type: -.dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

#nullable enable
namespace \u002D;

[TemplatePart(Name = "PART_NonSelectedArea", Type = typeof (Path))]
[TemplatePart(Name = "PART_Border", Type = typeof (Border))]
[TemplatePart(Name = "PART_BottomThumb", Type = typeof (dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd))]
[TemplatePart(Name = "PART_TopThumb", Type = typeof (dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd))]
[TemplatePart(Name = "PART_LeftThumb", Type = typeof (dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd))]
[TemplatePart(Name = "PART_MiddleThumb", Type = typeof (dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd))]
[TemplatePart(Name = "PART_RightThumb", Type = typeof (dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd))]
internal sealed class dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd : Control
{
  
  public static readonly 
  #nullable disable
  DependencyProperty \u0023\u003Dzmtd1hks\u003D = DependencyProperty.Register(nameof (Axis), typeof (IAxis), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzEM7rzxveqzC2)));
  
  public static readonly DependencyProperty \u0023\u003Dz8z6_NTW32S6R = DependencyProperty.Register(nameof (SelectedRange), typeof (IRange), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzGbJ0qpVdAcnUICGBTQ\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzu60E9d4eT7Mh = DependencyProperty.Register("SelectedRangePoint", typeof (Point), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) new Point(), new PropertyChangedCallback(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzKY6FELNu43vWrzYtTQ\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzC\u0024IzyRw5kNYh_iGfHw\u003D\u003D = DependencyProperty.Register(nameof (GripsThickness), typeof (double), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) 10.0));
  
  public static readonly DependencyProperty \u0023\u003Dz34Mz8ydZLjGnc6U2iA\u003D\u003D = DependencyProperty.Register(nameof (GripsLength), typeof (double), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) double.NaN));
  
  public static readonly DependencyProperty \u0023\u003DzFo\u0024BkIuazahQFPQ3fw\u003D\u003D = DependencyProperty.Register(nameof (GripsStyle), typeof (Style), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003DzfVZZZD9CcUaj = DependencyProperty.Register(nameof (ViewportStyle), typeof (Style), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003DzSszmx5ki9KQKy3MkQQ\u003D\u003D = DependencyProperty.Register(nameof (NonSelectedAreaStyle), typeof (Style), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) Orientation.Horizontal));
  
  public static readonly DependencyProperty \u0023\u003DzhdXvBNBQUywn = DependencyProperty.Register(nameof (ZoomLimit), typeof (double), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), new PropertyMetadata((object) 20.0, new PropertyChangedCallback(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzC5gZCRUAbdp9CTKgQg\u003D\u003D)));
  
  private dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd \u0023\u003DzdKNmsVR3XBR\u0024;
  
  private dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd \u0023\u003DzpiJFhBb5bsfa;
  
  private dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd \u0023\u003DzKbgVW2A4k9IH;
  
  private dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd \u0023\u003DzJ8xCDlzSVvPT;
  
  private dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd \u0023\u003Dz6P0zcFDUJreJ;
  
  private Border \u0023\u003DzksK80_U\u003D;
  
  private Path \u0023\u003Dz91ieiKM\u003D;
  
  private RectangleGeometry \u0023\u003DzkjapkNExDEBm;
  
  private \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvXUD\u0024c40UHSqnHjJIKlCXmEZS_a9vc2bs90\u003D \u0023\u003DzuT_nzto\u003D;
  
  private readonly \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAUTgsMETmsdNgd8UYUJWrW04 \u0023\u003Dz_a7lQd926eAW;
  
  private \u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D \u0023\u003Dz7GpEACzVt3nC;
  
  private EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D> \u0023\u003DzMrFyXitt9QAh;

  public dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd);
    this.\u0023\u003Dz_a7lQd926eAW = new \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAUTgsMETmsdNgd8UYUJWrW04(new Action(this.\u0023\u003Dzt1IQ_1E_Kq\u0024J), (\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D) new \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D(((DispatcherObject) this).Dispatcher));
    this.Loaded += new RoutedEventHandler(this.\u0023\u003DzmkudIBk3H5pH9nQj5m6jjHc\u003D);
    this.Unloaded += new RoutedEventHandler(this.\u0023\u003Dzmh\u0024sJQrpCGk02EhCW2\u0024i7D0\u003D);
    this.SizeChanged += new SizeChangedEventHandler(this.\u0023\u003DzRHWl_io\u003D);
    this.AddHandler(UIElement.MouseLeftButtonUpEvent, (Delegate) new MouseButtonEventHandler(this.\u0023\u003DzDz21rynQYfF9MkroPhv\u0024MQ0\u003D), false);
  }

  public void \u0023\u003DzFyo2BBoi_l9k(
    EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D> _param1)
  {
    EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D> eventHandler = this.\u0023\u003DzMrFyXitt9QAh;
    EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D>>(ref this.\u0023\u003DzMrFyXitt9QAh, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public void \u0023\u003DzRqHe87q\u0024f_eZ(
    EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D> _param1)
  {
    EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D> eventHandler = this.\u0023\u003DzMrFyXitt9QAh;
    EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D>>(ref this.\u0023\u003DzMrFyXitt9QAh, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  private void \u0023\u003Dzt1IQ_1E_Kq\u0024J()
  {
    if (this.Axis == null || this.SelectedRange == null)
      return;
    this.\u0023\u003DzrVPkAKI\u003D(this.SelectedRange);
  }

  public IRange SelectedRange
  {
    get
    {
      return (IRange) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dz8z6_NTW32S6R);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dz8z6_NTW32S6R, (object) value);
    }
  }

  public IAxis Axis
  {
    get
    {
      return (IAxis) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dzmtd1hks\u003D);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dzmtd1hks\u003D, (object) value);
    }
  }

  public double GripsThickness
  {
    get
    {
      return (double) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzC\u0024IzyRw5kNYh_iGfHw\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzC\u0024IzyRw5kNYh_iGfHw\u003D\u003D, (object) value);
    }
  }

  public double GripsLength
  {
    get
    {
      return (double) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dz34Mz8ydZLjGnc6U2iA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dz34Mz8ydZLjGnc6U2iA\u003D\u003D, (object) value);
    }
  }

  public Style NonSelectedAreaStyle
  {
    get
    {
      return (Style) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzSszmx5ki9KQKy3MkQQ\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzSszmx5ki9KQKy3MkQQ\u003D\u003D, (object) value);
    }
  }

  public Style ViewportStyle
  {
    get
    {
      return (Style) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzfVZZZD9CcUaj);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzfVZZZD9CcUaj, (object) value);
    }
  }

  public Style GripsStyle
  {
    get
    {
      return (Style) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzFo\u0024BkIuazahQFPQ3fw\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzFo\u0024BkIuazahQFPQ3fw\u003D\u003D, (object) value);
    }
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }

  public double ZoomLimit
  {
    get
    {
      return (double) this.GetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzhdXvBNBQUywn);
    }
    set
    {
      this.SetValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzhdXvBNBQUywn, (object) value);
    }
  }

  private void \u0023\u003DzrVPkAKI\u003D(
    IRange _param1)
  {
    this.\u0023\u003DzuT_nzto\u003D.\u0023\u003Dz7dZR55k\u003D(_param1);
    this.\u0023\u003DzMukWa4UvVPUn();
  }

  private void InvalidateElement()
  {
    this.\u0023\u003Dz_a7lQd926eAW.\u0023\u003DzEy\u0024V_bY\u003D();
  }

  private void \u0023\u003DzRHWl_io\u003D(object _param1, SizeChangedEventArgs _param2)
  {
    if (this.Axis != null)
      this.\u0023\u003DzlKgG5nJS80MM(this.Axis, this.ZoomLimit);
    this.\u0023\u003DzMukWa4UvVPUn();
  }

  private void \u0023\u003DzlKgG5nJS80MM(
    IAxis _param1,
    double _param2)
  {
    double num = this.Orientation == Orientation.Horizontal ? this.ActualWidth : this.ActualHeight;
    this.\u0023\u003DzuT_nzto\u003D = new \u0023\u003DzdJvCkWEpdQt1kv1Y55xjvXUD\u0024c40UHSqnHjJIKlCXmEZS_a9vc2bs90\u003D(_param1, num, _param2);
  }

  private void \u0023\u003DzMukWa4UvVPUn()
  {
    if (this.\u0023\u003DzksK80_U\u003D == null || this.Axis == null)
      return;
    double num1 = this.\u0023\u003DzuT_nzto\u003D.\u0023\u003DzXvzOhjLoNdjd();
    double num2 = this.\u0023\u003DzuT_nzto\u003D.\u0023\u003DzTI3RDYmCozNk();
    double num3 = this.\u0023\u003DzuT_nzto\u003D.\u0023\u003Dzu79DeCo\u003D();
    double num4 = Math.Max(this.\u0023\u003DzuT_nzto\u003D.\u0023\u003Dzs8tfezEAVTzJ() - this.\u0023\u003DzuT_nzto\u003D.\u0023\u003Dzu79DeCo\u003D(), 0.0);
    if (this.Orientation == Orientation.Horizontal)
    {
      this.\u0023\u003DzksK80_U\u003D.Padding = new Thickness(num1, 0.0, num2, 0.0);
      this.\u0023\u003DzkjapkNExDEBm.Rect = new Rect(num3, 0.0, num4, 1.0);
    }
    else
    {
      this.\u0023\u003DzksK80_U\u003D.Padding = new Thickness(0.0, num1, 0.0, num2);
      this.\u0023\u003DzkjapkNExDEBm.Rect = new Rect(0.0, num3, 1.0, num4);
    }
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.\u0023\u003DzdKNmsVR3XBR\u0024 = this.\u0023\u003DzBxuB3iQ\u003D<dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd>("PART_MiddleThumb");
    this.\u0023\u003DzpiJFhBb5bsfa = this.\u0023\u003DzBxuB3iQ\u003D<dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd>("PART_LeftThumb");
    this.\u0023\u003DzKbgVW2A4k9IH = this.\u0023\u003DzBxuB3iQ\u003D<dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd>("PART_RightThumb");
    this.\u0023\u003DzJ8xCDlzSVvPT = this.\u0023\u003DzBxuB3iQ\u003D<dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd>("PART_TopThumb");
    this.\u0023\u003Dz6P0zcFDUJreJ = this.\u0023\u003DzBxuB3iQ\u003D<dje_z8RH94H793M8R5KETVHK5JQ8YP5CJUPZ44TZM36TAZ3EKY2A_ejd>("PART_BottomThumb");
    this.\u0023\u003DzksK80_U\u003D = this.\u0023\u003DzBxuB3iQ\u003D<Border>("PART_Border");
    this.\u0023\u003Dz91ieiKM\u003D = this.\u0023\u003DzBxuB3iQ\u003D<Path>("PART_NonSelectedArea");
    GeometryCollection geometryCollection = new GeometryCollection();
    this.\u0023\u003DzkjapkNExDEBm = new RectangleGeometry();
    RectangleGeometry rectangleGeometry = new RectangleGeometry()
    {
      Rect = new Rect(0.0, 0.0, 1.0, 1.0)
    };
    geometryCollection.Add((Geometry) rectangleGeometry);
    geometryCollection.Add((Geometry) this.\u0023\u003DzkjapkNExDEBm);
    this.\u0023\u003Dz91ieiKM\u003D.Data = (Geometry) new GeometryGroup()
    {
      Children = geometryCollection
    };
    this.\u0023\u003DzA4X8F1w\u003D();
    this.\u0023\u003DzRHWl_io\u003D((object) this, (SizeChangedEventArgs) null);
  }

  private T \u0023\u003DzBxuB3iQ\u003D<T>(string _param1) where T : FrameworkElement, new()
  {
    return this.GetTemplateChild(_param1) is T templateChild ? templateChild : new T();
  }

  private void \u0023\u003DzA4X8F1w\u003D()
  {
    this.\u0023\u003DzpiJFhBb5bsfa.\u0023\u003DzudBnU1DPUfSSp\u00245wCA\u003D\u003D(new DragDeltaEventHandler(this.\u0023\u003DzpwiAOHmGq0jYU2bs8A\u003D\u003D));
    this.\u0023\u003DzKbgVW2A4k9IH.\u0023\u003DzudBnU1DPUfSSp\u00245wCA\u003D\u003D(new DragDeltaEventHandler(this.\u0023\u003DzyX5EHq6fxJQPTgcLDQ\u003D\u003D));
    this.\u0023\u003DzJ8xCDlzSVvPT.\u0023\u003DzudBnU1DPUfSSp\u00245wCA\u003D\u003D(new DragDeltaEventHandler(this.\u0023\u003DzVRWWo9Z0wS3M8r2HyQ\u003D\u003D));
    this.\u0023\u003Dz6P0zcFDUJreJ.\u0023\u003DzudBnU1DPUfSSp\u00245wCA\u003D\u003D(new DragDeltaEventHandler(this.\u0023\u003DzeKJHwTSbRBK6NHfBSw\u003D\u003D));
    this.\u0023\u003DzdKNmsVR3XBR\u0024.\u0023\u003DzudBnU1DPUfSSp\u00245wCA\u003D\u003D(new DragDeltaEventHandler(this.\u0023\u003DziSdrUc6l_gnJZMq_lA\u003D\u003D));
    this.\u0023\u003Dz91ieiKM\u003D.MouseLeftButtonUp += new MouseButtonEventHandler(this.\u0023\u003DzDz21rynQYfF9MkroPhv\u0024MQ0\u003D);
  }

  private void \u0023\u003DzyX5EHq6fxJQPTgcLDQ\u003D\u003D(
    object _param1,
    DragDeltaEventArgs _param2)
  {
    this.\u0023\u003DzF4GL5Zc\u003D((\u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D) 2);
    this.\u0023\u003DzyYTwKRoddC49(0.0, _param2.HorizontalChange);
  }

  private void \u0023\u003DzpwiAOHmGq0jYU2bs8A\u003D\u003D(
    object _param1,
    DragDeltaEventArgs _param2)
  {
    this.\u0023\u003DzF4GL5Zc\u003D((\u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D) 2);
    this.\u0023\u003DzyYTwKRoddC49(_param2.HorizontalChange, 0.0);
  }

  private void \u0023\u003DzVRWWo9Z0wS3M8r2HyQ\u003D\u003D(
    object _param1,
    DragDeltaEventArgs _param2)
  {
    this.\u0023\u003DzF4GL5Zc\u003D((\u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D) 2);
    this.\u0023\u003DzyYTwKRoddC49(_param2.VerticalChange, 0.0);
  }

  private void \u0023\u003DzeKJHwTSbRBK6NHfBSw\u003D\u003D(
    object _param1,
    DragDeltaEventArgs _param2)
  {
    this.\u0023\u003DzF4GL5Zc\u003D((\u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D) 2);
    this.\u0023\u003DzyYTwKRoddC49(0.0, _param2.VerticalChange);
  }

  private void \u0023\u003DziSdrUc6l_gnJZMq_lA\u003D\u003D(
    object _param1,
    DragDeltaEventArgs _param2)
  {
    double num = this.Orientation == Orientation.Horizontal ? _param2.HorizontalChange : _param2.VerticalChange;
    this.\u0023\u003DzF4GL5Zc\u003D((\u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D) 1);
    this.\u0023\u003DzyYTwKRoddC49(num, num);
  }

  private void \u0023\u003DzyYTwKRoddC49(double _param1, double _param2)
  {
    if (this.Axis == null)
      return;
    this.\u0023\u003DznvRv7Suy\u0024ne6(this.\u0023\u003DzuT_nzto\u003D.\u0023\u003Dz7FKHKl8\u003D(_param1, _param2), false);
  }

  private void \u0023\u003DzDz21rynQYfF9MkroPhv\u0024MQ0\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    if (_param1 != this.\u0023\u003Dz91ieiKM\u003D || this.Axis == null)
      return;
    Point position = _param2.GetPosition((IInputElement) this);
    double num = this.Orientation == Orientation.Horizontal ? position.X : position.Y;
    this.\u0023\u003DzF4GL5Zc\u003D((\u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D) 3);
    this.\u0023\u003DznvRv7Suy\u0024ne6(this.\u0023\u003DzuT_nzto\u003D.\u0023\u003DzfRDRUq8\u003D(num), true);
  }

  private void \u0023\u003DzF4GL5Zc\u003D(
    \u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D _param1)
  {
    this.\u0023\u003Dz7GpEACzVt3nC = _param1;
  }

  private void \u0023\u003DznvRv7Suy\u0024ne6(
    IRange _param1,
    bool _param2)
  {
    if (this.Axis == null || this.Axis.VisibleRange == null || _param1 == null)
      return;
    if (_param2)
      this.\u0023\u003DzJialaqnMsbTX0TJQl_fIDy4\u003D(_param1, TimeSpan.FromMilliseconds(500.0));
    else
      this.\u0023\u003Dz2FKbHTx8Inhh(_param1, true);
  }

  private void \u0023\u003Dz2FKbHTx8Inhh(
    IRange _param1,
    bool _param2)
  {
    this.SetCurrentValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dz8z6_NTW32S6R, (object) _param1);
    if (!_param2)
      return;
    this.\u0023\u003DzwkhKTdfYLBG6();
  }

  private void \u0023\u003DzwkhKTdfYLBG6()
  {
    this.\u0023\u003Dz7GpEACzVt3nC = (\u0023\u003DzPMVhs2rsXmQ8bFDDzjWvxwSDkEh3cCAq4Q\u003D\u003D) 0;
  }

  public void \u0023\u003DzJialaqnMsbTX0TJQl_fIDy4\u003D(
    IRange _param1,
    TimeSpan _param2)
  {
    dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzzrbNmh9DEXM3dn6RaCRlFgQ\u003D dexM3dn6RaCrlFgQ1 = new dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzzrbNmh9DEXM3dn6RaCRlFgQ\u003D();
    dexM3dn6RaCrlFgQ1._variableSome3535 = this;
    dexM3dn6RaCrlFgQ1.\u0023\u003DzAHNI_S0\u003D = _param1;
    Point point1;
    Point point2;
    if (this.Axis.get_IsLogarithmicAxis())
    {
      double logarithmicBase = ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this.Axis).get_LogarithmicBase();
      point1 = new Point(Math.Log(this.SelectedRange.Min.ToDouble(), logarithmicBase), Math.Log(this.SelectedRange.Max.ToDouble(), logarithmicBase));
      point2 = new Point(Math.Log(dexM3dn6RaCrlFgQ1.\u0023\u003DzAHNI_S0\u003D.Min.ToDouble(), logarithmicBase), Math.Log(dexM3dn6RaCrlFgQ1.\u0023\u003DzAHNI_S0\u003D.Max.ToDouble(), logarithmicBase));
    }
    else
    {
      point1 = new Point(this.SelectedRange.Min.ToDouble(), this.SelectedRange.Max.ToDouble());
      point2 = new Point(dexM3dn6RaCrlFgQ1.\u0023\u003DzAHNI_S0\u003D.Min.ToDouble(), dexM3dn6RaCrlFgQ1.\u0023\u003DzAHNI_S0\u003D.Max.ToDouble());
    }
    dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzzrbNmh9DEXM3dn6RaCRlFgQ\u003D dexM3dn6RaCrlFgQ2 = dexM3dn6RaCrlFgQ1;
    PointAnimation pointAnimation = new PointAnimation();
    pointAnimation.From = new Point?(point1);
    pointAnimation.To = new Point?(point2);
    pointAnimation.Duration = (Duration) _param2;
    ExponentialEase exponentialEase = new ExponentialEase();
    exponentialEase.EasingMode = EasingMode.EaseOut;
    exponentialEase.Exponent = 7.0;
    pointAnimation.EasingFunction = (IEasingFunction) exponentialEase;
    dexM3dn6RaCrlFgQ2.\u0023\u003DzXB4BRQQhi9cE = pointAnimation;
    Storyboard.SetTarget((DependencyObject) dexM3dn6RaCrlFgQ1.\u0023\u003DzXB4BRQQhi9cE, (DependencyObject) this);
    Storyboard.SetTargetProperty((DependencyObject) dexM3dn6RaCrlFgQ1.\u0023\u003DzXB4BRQQhi9cE, new PropertyPath("SelectedRangePoint", Array.Empty<object>()));
    Storyboard storyboard = new Storyboard();
    dexM3dn6RaCrlFgQ1.\u0023\u003DzXB4BRQQhi9cE.Completed += new EventHandler(dexM3dn6RaCrlFgQ1.\u0023\u003DzUrhjvMdypVP4nr450dOZW2HHTvcf);
    storyboard.Duration = (Duration) _param2;
    storyboard.Children.Add((Timeline) dexM3dn6RaCrlFgQ1.\u0023\u003DzXB4BRQQhi9cE);
    storyboard.Begin();
  }

  private static void \u0023\u003DzKY6FELNu43vWrzYtTQ\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzgoTc9DaC1qIyGf7GoaiJqfY\u003D c1qIyGf7GoaiJqfY = new dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzgoTc9DaC1qIyGf7GoaiJqfY\u003D();
    c1qIyGf7GoaiJqfY.\u0023\u003DzSygYxus\u003D = (Point) _param1.NewValue;
    c1qIyGf7GoaiJqfY.\u0023\u003DzQc0xUQk\u003D = (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd) _param0;
    ((DispatcherObject) c1qIyGf7GoaiJqfY.\u0023\u003DzQc0xUQk\u003D).Dispatcher.\u0023\u003DznvGFbrlLtrNN(new Action(c1qIyGf7GoaiJqfY.\u0023\u003DzMcNxOaHJdA_oEv9HDTrk3bpRatP8));
  }

  private static void \u0023\u003DzEM7rzxveqzC2(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd rzxguxypaD7ZywzaEjd))
      return;
    if (_param1.OldValue is IAxis oldValue)
      oldValue.\u0023\u003DzwG_uRQ_EmTwc(new EventHandler<EventArgs>(rzxguxypaD7ZywzaEjd.\u0023\u003DzaNiYNgKj9pMk));
    if (_param1.NewValue is IAxis newValue)
    {
      newValue.\u0023\u003DzF_\u0024wky5\u0024qiYa(new EventHandler<EventArgs>(rzxguxypaD7ZywzaEjd.\u0023\u003DzaNiYNgKj9pMk));
      rzxguxypaD7ZywzaEjd.\u0023\u003DzfDNfYoziR5Tp(newValue);
    }
    else
      rzxguxypaD7ZywzaEjd.ClearValue(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dz8z6_NTW32S6R);
    rzxguxypaD7ZywzaEjd.InvalidateElement();
  }

  private void \u0023\u003DzaNiYNgKj9pMk(object _param1, EventArgs _param2)
  {
    this.InvalidateElement();
  }

  private void \u0023\u003DzfDNfYoziR5Tp(
    IAxis _param1)
  {
    this.\u0023\u003DzlKgG5nJS80MM(_param1, this.ZoomLimit);
    this.\u0023\u003DzMukWa4UvVPUn();
    Binding binding = new Binding()
    {
      Source = (object) _param1,
      Path = new PropertyPath((object) dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzWl3LbWhL1z0D),
      Mode = BindingMode.TwoWay
    };
    this.SetBinding(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dz8z6_NTW32S6R, (BindingBase) binding);
  }

  private static void \u0023\u003DzC5gZCRUAbdp9CTKgQg\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd rzxguxypaD7ZywzaEjd) || rzxguxypaD7ZywzaEjd.Axis == null)
      return;
    double newValue = (double) _param1.NewValue;
    rzxguxypaD7ZywzaEjd.\u0023\u003DzlKgG5nJS80MM(rzxguxypaD7ZywzaEjd.Axis, newValue);
  }

  private static void \u0023\u003DzGbJ0qpVdAcnUICGBTQ\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd rzxguxypaD7ZywzaEjd = (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd) _param0;
    IRange oldValue = _param1.OldValue as IRange;
    IRange newValue = _param1.NewValue as IRange;
    if (oldValue != null)
      oldValue.PropertyChanged -= new PropertyChangedEventHandler(rzxguxypaD7ZywzaEjd.\u0023\u003Dz2bU_9BbH19ybd6I4TQ\u003D\u003D);
    if (newValue == null)
      return;
    newValue.PropertyChanged += new PropertyChangedEventHandler(rzxguxypaD7ZywzaEjd.\u0023\u003Dz2bU_9BbH19ybd6I4TQ\u003D\u003D);
    dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzrVPkAKI\u003D(rzxguxypaD7ZywzaEjd, newValue);
  }

  private static void \u0023\u003DzrVPkAKI\u003D(
    dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd _param0,
    IRange _param1)
  {
    if (_param0.Axis == null)
      return;
    _param0.\u0023\u003DzrVPkAKI\u003D(_param1);
    _param0.\u0023\u003DzwYsaBr1sAWjVxDLKBSJjX00\u003D();
  }

  private void \u0023\u003Dz2bU_9BbH19ybd6I4TQ\u003D\u003D(
    object _param1,
    PropertyChangedEventArgs _param2)
  {
    IComparable comparable1 = this.SelectedRange.Min;
    IComparable comparable2 = this.SelectedRange.Max;
    switch (_param2.PropertyName)
    {
      case "Min":
        comparable1 = (IComparable) ((\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDCPI15WlokXZO9yDJcU\u003D) _param2).\u0023\u003DzPo6XanrX3cHa();
        break;
      case "Max":
        comparable2 = (IComparable) ((\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDCPI15WlokXZO9yDJcU\u003D) _param2).\u0023\u003DzPo6XanrX3cHa();
        break;
    }
    if (this.SelectedRange.Equals((object) \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(this.SelectedRange, comparable1, comparable2)))
      return;
    dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003DzrVPkAKI\u003D(this, this.SelectedRange);
    BindingExpression bindingExpression = this.GetBindingExpression(dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd.\u0023\u003Dz8z6_NTW32S6R);
    if (bindingExpression == null || bindingExpression.ParentBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
      return;
    bindingExpression.UpdateSource();
    bindingExpression.UpdateTarget();
  }

  private void \u0023\u003DzwYsaBr1sAWjVxDLKBSJjX00\u003D()
  {
    \u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D e = new \u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D(this.SelectedRange, this.\u0023\u003Dz7GpEACzVt3nC);
    EventHandler<\u0023\u003DzPauio66DvxKtWOFEEHOV9RO_klQa1lEvVk\u0024ROYo\u003D> zMrFyXitt9Qah = this.\u0023\u003DzMrFyXitt9QAh;
    if (zMrFyXitt9Qah == null)
      return;
    zMrFyXitt9Qah((object) this, e);
  }

  private void \u0023\u003DzmkudIBk3H5pH9nQj5m6jjHc\u003D(object _param1, RoutedEventArgs _param2)
  {
    this.\u0023\u003Dz_a7lQd926eAW.\u0023\u003DzrwMjAU8\u003D();
  }

  private void \u0023\u003Dzmh\u0024sJQrpCGk02EhCW2\u0024i7D0\u003D(
    object _param1,
    RoutedEventArgs _param2)
  {
    this.\u0023\u003Dz_a7lQd926eAW.\u0023\u003DzQkZEqBc0xlZziN1nrQ\u003D\u003D();
  }

  private sealed class \u0023\u003DzgoTc9DaC1qIyGf7GoaiJqfY\u003D
  {
    public dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd \u0023\u003DzQc0xUQk\u003D;
    public Point \u0023\u003DzSygYxus\u003D;

    internal void \u0023\u003DzMcNxOaHJdA_oEv9HDTrk3bpRatP8()
    {
      IRange abyLt9clZggmJsWhw;
      if (this.\u0023\u003DzQc0xUQk\u003D.Axis.get_IsLogarithmicAxis())
      {
        double logarithmicBase = ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this.\u0023\u003DzQc0xUQk\u003D.Axis).get_LogarithmicBase();
        abyLt9clZggmJsWhw = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D(this.\u0023\u003DzQc0xUQk\u003D.SelectedRange.GetType(), (IComparable) Math.Pow(logarithmicBase, this.\u0023\u003DzSygYxus\u003D.X), (IComparable) Math.Pow(logarithmicBase, this.\u0023\u003DzSygYxus\u003D.Y));
      }
      else
        abyLt9clZggmJsWhw = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D(this.\u0023\u003DzQc0xUQk\u003D.SelectedRange.GetType(), (IComparable) this.\u0023\u003DzSygYxus\u003D.X, (IComparable) this.\u0023\u003DzSygYxus\u003D.Y);
      this.\u0023\u003DzQc0xUQk\u003D.\u0023\u003Dz2FKbHTx8Inhh(abyLt9clZggmJsWhw, false);
    }
  }

  private sealed class \u0023\u003DzzrbNmh9DEXM3dn6RaCRlFgQ\u003D
  {
    public dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd _variableSome3535;
    public IRange \u0023\u003DzAHNI_S0\u003D;
    public PointAnimation \u0023\u003DzXB4BRQQhi9cE;

    internal void \u0023\u003DzUrhjvMdypVP4nr450dOZW2HHTvcf(
    #nullable enable
    object? _param1, EventArgs _param2)
    {
      this._variableSome3535.\u0023\u003Dz2FKbHTx8Inhh(this.\u0023\u003DzAHNI_S0\u003D, true);
      Storyboard.SetTarget((DependencyObject) this.\u0023\u003DzXB4BRQQhi9cE, (DependencyObject) null);
      this.\u0023\u003DzXB4BRQQhi9cE.FillBehavior = FillBehavior.Stop;
    }
  }
}
