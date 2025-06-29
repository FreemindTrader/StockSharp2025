// Decompiled with JetBrains decompiler
// Type: -.dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Xaml;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

#nullable disable
namespace StockSharp.Xaml.Charting;

[TemplatePart(Name = "PART_Container", Type = typeof (Panel))]
[TemplatePart(Name = "PART_BackgroundSurface", Type = typeof (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D))]
[TemplatePart(Name = "PART_Scrollbar", Type = typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd))]
internal sealed class dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd : 
  Control,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe06Do2pQ7ReqT8Ks0apzs3KdsLXgXg\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzIcVMwZBBZ1n3 = DependencyProperty.Register(XXX.SSS(-539440969), typeof (Color), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) new Color()));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzXc9apgJiH9mm = DependencyProperty.Register(XXX.SSS(-539440538), typeof (Brush), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzGTMdFbtr59Hr = DependencyProperty.Register(XXX.SSS(-539430144), typeof (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzBk\u00244thlqlXdL = DependencyProperty.Register(XXX.SSS(-539428462), typeof (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzRx5RhuORvh6CP5D2Lg\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzSEAakZbtZKgY = DependencyProperty.Register(XXX.SSS(-539427791), typeof (string), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) XXX.SSS(-539431487), new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzdXB_wQ4DNB7BU25L6g\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz8z6_NTW32S6R = DependencyProperty.Register(XXX.SSS(-539349935), typeof (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dzmtd1hks\u003D = DependencyProperty.Register(XXX.SSS(-539349955), typeof (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzEM7rzxveqzC2)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz\u0024FkvCXFCwCpX = DependencyProperty.Register(XXX.SSS(-539349962), typeof (Style), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzShv3ynBVbG1B7Q21pZ\u0024hSYI\u003D = DependencyProperty.Register(XXX.SSS(-539349987), typeof (Style), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dzdo5eapdAocIqY_CaIT\u0024QfQgldQWZ)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz7lrsbkJYllce464Io_zCCfI\u003D = DependencyProperty.Register(XXX.SSS(-539349791), typeof (Type), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzQoIGRIjEbowwBxyaQMoyp7vFf5I\u0024)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D \u0023\u003DzVpgkPm_hKDwz;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private PropertyChangeNotifier \u0023\u003DzS8n1gsVhmNXbOgPpfTG1M1Hc_96t;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private PropertyChangeNotifier \u0023\u003DzLFsuvPTJkIKAJEVj\u0024A\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> \u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private PropertyChangeNotifier \u0023\u003DzIByQgd_P4up7CYAE8A\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D \u0023\u003DzWdeNTJKzYxQa;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAUTgsMETmsdNgd8UYUJWrW04 \u0023\u003Dz_a7lQd926eAW;

  public dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd);
    this.\u0023\u003Dz_a7lQd926eAW = new \u0023\u003DzWwDYSe\u0024z_nPO4KEsBbkyAUTgsMETmsdNgd8UYUJWrW04(new Action(this.\u0023\u003Dz5q8i9C4\u003D), (\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D) new \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D(((DispatcherObject) this).Dispatcher));
    this.Loaded += new RoutedEventHandler(this.\u0023\u003DzYIYHxzzNpONOjcWNH_qj88E\u003D);
    this.Unloaded += new RoutedEventHandler(this.\u0023\u003Dz3l29bjkM9LKwIiF9wuOt8h8\u003D);
  }

  public \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D \u0023\u003DzOhSPDVgkFsLoLHEdyw\u003D\u003D()
  {
    return this.\u0023\u003DzVpgkPm_hKDwz;
  }

  public Style RenderableSeriesStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzShv3ynBVbG1B7Q21pZ\u0024hSYI\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzShv3ynBVbG1B7Q21pZ\u0024hSYI\u003D, (object) value);
    }
  }

  public Type RenderableSeriesType
  {
    get
    {
      return (Type) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dz7lrsbkJYllce464Io_zCCfI\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dz7lrsbkJYllce464Io_zCCfI\u003D, (object) value);
    }
  }

  public Color SeriesColor
  {
    get
    {
      return (Color) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzIcVMwZBBZ1n3);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) value);
    }
  }

  public Brush AreaBrush
  {
    get
    {
      return (Brush) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzXc9apgJiH9mm);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzXc9apgJiH9mm, (object) value);
    }
  }

  public \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D DataSeries
  {
    get
    {
      return (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzGTMdFbtr59Hr);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzGTMdFbtr59Hr, (object) value);
    }
  }

  public string XAxisId
  {
    get
    {
      return (string) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzSEAakZbtZKgY);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzSEAakZbtZKgY, (object) value);
    }
  }

  public \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D ParentSurface
  {
    get
    {
      return (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzBk\u00244thlqlXdL);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzBk\u00244thlqlXdL, (object) value);
    }
  }

  public \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D SelectedRange
  {
    get
    {
      return (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dz8z6_NTW32S6R);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dz8z6_NTW32S6R, (object) value);
    }
  }

  public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB Axis
  {
    get
    {
      return (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dzmtd1hks\u003D);
    }
    private set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dzmtd1hks\u003D, (object) value);
    }
  }

  public Style ScrollbarStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dz\u0024FkvCXFCwCpX);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dz\u0024FkvCXFCwCpX, (object) value);
    }
  }

  public void \u0023\u003Dz5q8i9C4\u003D()
  {
    if (this.\u0023\u003DzOhSPDVgkFsLoLHEdyw\u003D\u003D() == null || this.Axis == null)
      return;
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = this.Axis.get_DataRange();
    if (abyLt9clZggmJsWhw == null)
      return;
    if (this.Axis.get_GrowBy() != null)
      abyLt9clZggmJsWhw = abyLt9clZggmJsWhw.\u0023\u003DzzXTqVFg\u003D(this.Axis.get_GrowBy().Min, this.Axis.get_GrowBy().Max);
    this.\u0023\u003DzOhSPDVgkFsLoLHEdyw\u003D\u003D().XAxis.VisibleRange = abyLt9clZggmJsWhw;
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.\u0023\u003DzVpgkPm_hKDwz = this.GetTemplateChild(XXX.SSS(-539349814)) as \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D;
    this.\u0023\u003DzHZ6slBP\u0024iRnw(this.Axis);
    this.\u0023\u003Dzu1rsCACNRRdc0LZjNU6jPzI\u003D(this.RenderableSeriesStyle, this.RenderableSeriesType);
    if (!(this.GetTemplateChild(XXX.SSS(-539349847)) is dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd templateChild))
      return;
    templateChild.MouseWheel += new MouseWheelEventHandler(this.\u0023\u003Dz_VKcn9tY7lpR\u0024XXUytTeyIA\u003D);
  }

  private void \u0023\u003DzHZ6slBP\u0024iRnw(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    if (this.\u0023\u003DzOhSPDVgkFsLoLHEdyw\u003D\u003D() == null || _param1 == null)
      return;
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB = _param1.\u0023\u003DzQ8SgRgQ\u003D();
    dynWmoFzgH4RlWB0lB.set_Visibility(Visibility.Collapsed);
    dynWmoFzgH4RlWB0lB.set_ParentSurface(this.\u0023\u003DzOhSPDVgkFsLoLHEdyw\u003D\u003D());
    dynWmoFzgH4RlWB0lB.set_DrawMinorGridLines(false);
    dynWmoFzgH4RlWB0lB.set_DrawMajorGridLines(false);
    Binding binding = new Binding(XXX.SSS(-539433838))
    {
      Source = (object) _param1,
      Mode = BindingMode.OneWay
    };
    ((FrameworkElement) dynWmoFzgH4RlWB0lB).SetBinding(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzhYW6tiLSC0eZ, (BindingBase) binding);
    this.\u0023\u003DzOhSPDVgkFsLoLHEdyw\u003D\u003D().XAxis = dynWmoFzgH4RlWB0lB;
  }

  private bool \u0023\u003DzZips37gKJQPGLQcrTw\u003D\u003D(
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D _param1,
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param2)
  {
    return _param1 != null && !_param1.get_RenderableSeries().\u0023\u003DzCCMM80zDpO6N<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>() && _param1.get_RenderableSeries().Any<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>(new Func<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D, bool>(new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003Dzzjz1tgS0aO8fKdbpAk8DUVE\u003D()
    {
      \u0023\u003DzV4I\u00248D0bjQIn = _param2
    }.\u0023\u003DzPainzusVDvxJHbnmyWpu6OsDqQPW));
  }

  private static void \u0023\u003DzRx5RhuORvh6CP5D2Lg\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd f696R6Da4LbdrF4QEjd))
      return;
    f696R6Da4LbdrF4QEjd.\u0023\u003DzGRp1Ib8nhGv\u0024(_param1);
  }

  private void \u0023\u003DzGRp1Ib8nhGv\u0024(DependencyPropertyChangedEventArgs _param1)
  {
    this.\u0023\u003DzAnFHa0QS7SrDykk4wg\u003D\u003D();
    if (_param1.NewValue is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd newValue)
    {
      this.\u0023\u003DzS8n1gsVhmNXbOgPpfTG1M1Hc_96t = new PropertyChangeNotifier((DependencyObject) newValue, dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzda5ZTgpF7nPj_QX8WWDrVmQ\u003D);
      this.\u0023\u003DzS8n1gsVhmNXbOgPpfTG1M1Hc_96t.ValueChanged += new Action(this.\u0023\u003Dz_sR2YBsF8Vv_kS6d84fxv3s\u003D);
      this.\u0023\u003DzIByQgd_P4up7CYAE8A\u003D\u003D = new PropertyChangeNotifier((DependencyObject) newValue, dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzDqajrUCXt2L8);
      this.\u0023\u003DzIByQgd_P4up7CYAE8A\u003D\u003D.ValueChanged += new Action(this.\u0023\u003DzgjrLoKvF7hcz9A6IKvJCiAc\u003D);
      if (!this.\u0023\u003DzZips37gKJQPGLQcrTw\u003D\u003D((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) newValue, this.DataSeries))
        this.SetCurrentValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzGTMdFbtr59Hr, (object) null);
      this.\u0023\u003Dz4X8eQxjPK0LOdo2uQqSISqs\u003D(newValue.RenderableSeries);
      this.\u0023\u003DzMEAXmFgTZfXXcCqZiA\u003D\u003D(newValue.XAxes);
    }
    else
    {
      this.\u0023\u003DzAnFHa0QS7SrDykk4wg\u003D\u003D();
      this.SetCurrentValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzGTMdFbtr59Hr, (object) null);
    }
  }

  private void \u0023\u003DzAnFHa0QS7SrDykk4wg\u003D\u003D()
  {
    if (this.\u0023\u003DzIByQgd_P4up7CYAE8A\u003D\u003D != null)
    {
      this.\u0023\u003DzIByQgd_P4up7CYAE8A\u003D\u003D.Dispose();
      this.\u0023\u003DzIByQgd_P4up7CYAE8A\u003D\u003D = (PropertyChangeNotifier) null;
    }
    if (this.\u0023\u003DzS8n1gsVhmNXbOgPpfTG1M1Hc_96t == null)
      return;
    this.\u0023\u003DzS8n1gsVhmNXbOgPpfTG1M1Hc_96t.Dispose();
    this.\u0023\u003DzLFsuvPTJkIKAJEVj\u0024A\u003D\u003D = (PropertyChangeNotifier) null;
  }

  private void \u0023\u003DzMEAXmFgTZfXXcCqZiA\u003D\u003D(
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D _param1)
  {
    if (this.\u0023\u003DzWdeNTJKzYxQa != null)
      this.\u0023\u003DzWdeNTJKzYxQa.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D);
    this.\u0023\u003DzWdeNTJKzYxQa = _param1;
    if (this.\u0023\u003DzWdeNTJKzYxQa != null)
      this.\u0023\u003DzWdeNTJKzYxQa.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D);
    this.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D((object) this, (NotifyCollectionChangedEventArgs) null);
  }

  private void \u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.Axis = !this.\u0023\u003DzWdeNTJKzYxQa.\u0023\u003DzCCMM80zDpO6N<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>() ? this.\u0023\u003DzWdeNTJKzYxQa.\u0023\u003Dz\u0024YoxjvGBoa2C(this.XAxisId, false) : (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB) null;
  }

  private void \u0023\u003Dz4X8eQxjPK0LOdo2uQqSISqs\u003D(
    ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> _param1)
  {
    if (this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D != null)
    {
      this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz7srWHueCApHLwSzFbP3tcMI\u003D);
      this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D = (ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>) null;
    }
    this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D = _param1;
    if (this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D == null)
      return;
    this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz7srWHueCApHLwSzFbP3tcMI\u003D);
    this.\u0023\u003Dz7srWHueCApHLwSzFbP3tcMI\u003D((object) this, (NotifyCollectionChangedEventArgs) null);
  }

  private void \u0023\u003Dz7srWHueCApHLwSzFbP3tcMI\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzHeEbGncgo1okL0HgWiq5JrU\u003D gncgo1okL0HgWiq5JrU = new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzHeEbGncgo1okL0HgWiq5JrU\u003D();
    gncgo1okL0HgWiq5JrU.\u0023\u003DzRRvwDu67s9Rm = this;
    if (this.DataSeries != null || this.ParentSurface == null)
      return;
    gncgo1okL0HgWiq5JrU.\u0023\u003Dz7r_j4hRa0\u0024iRFvyX7P8mUl8\u003D = this.ParentSurface.get_RenderableSeries().FirstOrDefault<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>();
    if (this.\u0023\u003DzLFsuvPTJkIKAJEVj\u0024A\u003D\u003D != null)
    {
      this.\u0023\u003DzLFsuvPTJkIKAJEVj\u0024A\u003D\u003D.Dispose();
      this.\u0023\u003DzLFsuvPTJkIKAJEVj\u0024A\u003D\u003D = (PropertyChangeNotifier) null;
    }
    if (gncgo1okL0HgWiq5JrU.\u0023\u003Dz7r_j4hRa0\u0024iRFvyX7P8mUl8\u003D == null)
      return;
    if (gncgo1okL0HgWiq5JrU.\u0023\u003Dz7r_j4hRa0\u0024iRFvyX7P8mUl8\u003D is dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd)
    {
      this.\u0023\u003DzLFsuvPTJkIKAJEVj\u0024A\u003D\u003D = new PropertyChangeNotifier((DependencyObject) (gncgo1okL0HgWiq5JrU.\u0023\u003Dz7r_j4hRa0\u0024iRFvyX7P8mUl8\u003D as dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzGTMdFbtr59Hr);
      this.\u0023\u003DzLFsuvPTJkIKAJEVj\u0024A\u003D\u003D.ValueChanged += new Action(gncgo1okL0HgWiq5JrU.\u0023\u003Dzq5A_hw8sG0QZl737AckA8VB19QdzQUuQMw\u003D\u003D);
    }
    this.\u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(gncgo1okL0HgWiq5JrU.\u0023\u003Dz7r_j4hRa0\u0024iRFvyX7P8mUl8\u003D.get_DataSeries());
  }

  private void \u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1)
  {
    this.SetCurrentValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd.\u0023\u003DzGTMdFbtr59Hr, (object) _param1);
  }

  private static void \u0023\u003DzdXB_wQ4DNB7BU25L6g\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd f696R6Da4LbdrF4QEjd))
      return;
    f696R6Da4LbdrF4QEjd.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D((object) f696R6Da4LbdrF4QEjd, (NotifyCollectionChangedEventArgs) null);
  }

  private static void \u0023\u003DzEM7rzxveqzC2(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd f696R6Da4LbdrF4QEjd))
      return;
    if (_param1.OldValue is \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB oldValue)
      oldValue.\u0023\u003DzwG_uRQ_EmTwc(new EventHandler<EventArgs>(f696R6Da4LbdrF4QEjd.\u0023\u003DzaNiYNgKj9pMk));
    if (!(_param1.NewValue is \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB newValue))
      return;
    newValue.\u0023\u003DzF_\u0024wky5\u0024qiYa(new EventHandler<EventArgs>(f696R6Da4LbdrF4QEjd.\u0023\u003DzaNiYNgKj9pMk));
    f696R6Da4LbdrF4QEjd.\u0023\u003DzHZ6slBP\u0024iRnw(newValue);
  }

  private void \u0023\u003DzaNiYNgKj9pMk(object _param1, EventArgs _param2)
  {
    this.\u0023\u003Dz_a7lQd926eAW.\u0023\u003DzEy\u0024V_bY\u003D();
  }

  private static void \u0023\u003DzQoIGRIjEbowwBxyaQMoyp7vFf5I\u0024(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    Type newValue = (Type) _param1.NewValue;
    if (!newValue.IsAssignableFrom(typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd)))
      return;
    ((dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd) _param0).\u0023\u003Dzu1rsCACNRRdc0LZjNU6jPzI\u003D(((dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd) _param0).RenderableSeriesStyle, newValue);
  }

  private static void \u0023\u003Dzdo5eapdAocIqY_CaIT\u0024QfQgldQWZ(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd) _param0).\u0023\u003Dzu1rsCACNRRdc0LZjNU6jPzI\u003D(_param1.NewValue as Style, ((dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd) _param0).RenderableSeriesType);
  }

  private void \u0023\u003Dzu1rsCACNRRdc0LZjNU6jPzI\u003D(Style _param1, Type _param2)
  {
    if (this.\u0023\u003DzVpgkPm_hKDwz == null)
      return;
    if (this.\u0023\u003DzVpgkPm_hKDwz.get_RenderableSeries().\u0023\u003DzMeGSfVE\u003D<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>() || this.\u0023\u003DzVpgkPm_hKDwz.get_RenderableSeries().First<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>().GetType() != _param2)
    {
      dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd instance = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) Activator.CreateInstance(_param2);
      instance.SetCurrentValue(FrameworkElement.DataContextProperty, (object) this);
      if (_param1 == null || _param1.TargetType.IsAssignableFrom(_param2))
        instance.Style = _param1;
      using (this.\u0023\u003DzVpgkPm_hKDwz.SuspendUpdates())
      {
        this.\u0023\u003DzVpgkPm_hKDwz.get_RenderableSeries().Clear();
        this.\u0023\u003DzVpgkPm_hKDwz.get_RenderableSeries().Add((\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) instance);
      }
    }
    else
      this.\u0023\u003DzVpgkPm_hKDwz.get_RenderableSeries()[0].\u0023\u003DzlqgXLHiucpqc(_param1);
  }

  private void \u0023\u003DzYIYHxzzNpONOjcWNH_qj88E\u003D(object _param1, RoutedEventArgs _param2)
  {
    this.\u0023\u003Dz_a7lQd926eAW.\u0023\u003DzrwMjAU8\u003D();
  }

  private void \u0023\u003Dz3l29bjkM9LKwIiF9wuOt8h8\u003D(object _param1, RoutedEventArgs _param2)
  {
    this.\u0023\u003Dz_a7lQd926eAW.\u0023\u003DzQkZEqBc0xlZziN1nrQ\u003D\u003D();
  }

  private void \u0023\u003Dz_VKcn9tY7lpR\u0024XXUytTeyIA\u003D(
    object _param1,
    MouseWheelEventArgs _param2)
  {
    Point position = _param2.GetPosition((IInputElement) this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB());
    this.ParentSurface?.get_ChartModifier()?.\u0023\u003DzQTINWhMByBmJ(new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY(position, (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 0, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), _param2.Delta, true));
  }

  private void \u0023\u003Dz_sR2YBsF8Vv_kS6d84fxv3s\u003D()
  {
    this.\u0023\u003Dz4X8eQxjPK0LOdo2uQqSISqs\u003D((ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>) this.\u0023\u003DzS8n1gsVhmNXbOgPpfTG1M1Hc_96t.Value);
  }

  private void \u0023\u003DzgjrLoKvF7hcz9A6IKvJCiAc\u003D()
  {
    this.\u0023\u003DzMEAXmFgTZfXXcCqZiA\u003D\u003D((\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D) this.\u0023\u003DzIByQgd_P4up7CYAE8A\u003D\u003D.Value);
  }

  private sealed class \u0023\u003DzHeEbGncgo1okL0HgWiq5JrU\u003D
  {
    public dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd \u0023\u003DzRRvwDu67s9Rm;
    public \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D \u0023\u003Dz7r_j4hRa0\u0024iRFvyX7P8mUl8\u003D;

    internal void \u0023\u003Dzq5A_hw8sG0QZl737AckA8VB19QdzQUuQMw\u003D\u003D()
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(this.\u0023\u003Dz7r_j4hRa0\u0024iRFvyX7P8mUl8\u003D.get_DataSeries());
    }
  }

  private sealed class \u0023\u003Dzzjz1tgS0aO8fKdbpAk8DUVE\u003D
  {
    public \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003DzV4I\u00248D0bjQIn;

    internal bool \u0023\u003DzPainzusVDvxJHbnmyWpu6OsDqQPW(
      \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1)
    {
      return _param1.get_DataSeries() == this.\u0023\u003DzV4I\u00248D0bjQIn;
    }
  }
}
