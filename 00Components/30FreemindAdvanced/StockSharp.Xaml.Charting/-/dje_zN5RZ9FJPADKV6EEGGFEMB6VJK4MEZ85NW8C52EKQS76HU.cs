// Decompiled with JetBrains decompiler
// Type: -.dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Charting;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Xaml.Charting;

[TemplatePart(Name = "PART_InputTextArea", Type = typeof (TextBox))]
internal sealed class dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd : Control
{
  
  public static readonly DependencyProperty \u0023\u003DzrXSFm_E\u003D = DependencyProperty.Register("", typeof (string), typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd), new PropertyMetadata((object) string.Empty));
  
  public static readonly DependencyProperty \u0023\u003DzbhlExb5p620n = DependencyProperty.Register("", typeof (LabelPlacement), typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd), new PropertyMetadata((object) LabelPlacement.Auto, new PropertyChangedCallback(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzXXEXIlUNtjM5)));
  
  public static readonly DependencyProperty \u0023\u003DzOjRqbPmhsyO_ = DependencyProperty.Register("", typeof (Style), typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzXXEXIlUNtjM5)));
  
  public static readonly DependencyProperty \u0023\u003DzcXOoBjH4oSyy = DependencyProperty.Register("", typeof (Style), typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzXXEXIlUNtjM5)));
  
  public static readonly DependencyProperty \u0023\u003Dz2k8rcqz_ESAS = DependencyProperty.Register("", typeof (CornerRadius), typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd), new PropertyMetadata((object) new CornerRadius()));
  
  public static readonly DependencyProperty \u0023\u003DzkSbs6pXjW8dc = DependencyProperty.Register("", typeof (double), typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd), new PropertyMetadata((object) 0.0));
  
  public static readonly DependencyProperty \u0023\u003DzGHSiJVJNBzic = DependencyProperty.Register("", typeof (bool), typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzALRRz3KBB3Uz = DependencyProperty.Register("", typeof (string), typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd), new PropertyMetadata((object) string.Empty));
  
  private LineAnnotationWithLabelsBase \u0023\u003Dzoem3lxBSJacx;
  
  private TextBox \u0023\u003DzA5cpUiCtbnuj;

  public dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd);
    this.MouseLeftButtonDown += new MouseButtonEventHandler(this.\u0023\u003DzGDG1SrCuVxZHIZFp\u00248qj2tc\u003D);
  }

  internal bool \u0023\u003DztUuF6EohuIU9()
  {
    if (this.LabelPlacement == LabelPlacement.Axis)
      return true;
    return this.\u0023\u003DzLtJnA4CR3Exc() != null && this.\u0023\u003DzLtJnA4CR3Exc().GetLabelPlacement(this) == LabelPlacement.Axis;
  }

  public bool CanEditText
  {
    get
    {
      return (bool) this.GetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzGHSiJVJNBzic);
    }
    set
    {
      this.SetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzGHSiJVJNBzic, (object) value);
    }
  }

  public double RotationAngle
  {
    get
    {
      return (double) this.GetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzkSbs6pXjW8dc);
    }
    set
    {
      this.SetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzkSbs6pXjW8dc, (object) value);
    }
  }

  public LineAnnotationWithLabelsBase \u0023\u003DzLtJnA4CR3Exc() => this.\u0023\u003Dzoem3lxBSJacx;

  public void \u0023\u003DzBV_vk9PuzvJU(LineAnnotationWithLabelsBase _param1)
  {
    if (this.\u0023\u003Dzoem3lxBSJacx != null)
      this.\u0023\u003Dzoem3lxBSJacx.Unselected -= new EventHandler(this.\u0023\u003Dzdb7_FucBsCsx);
    this.\u0023\u003Dzoem3lxBSJacx = _param1;
    if (this.\u0023\u003Dzoem3lxBSJacx == null)
      return;
    this.\u0023\u003Dzoem3lxBSJacx.Unselected += new EventHandler(this.\u0023\u003Dzdb7_FucBsCsx);
    this.\u0023\u003DzxKKlOa0jEDmy();
  }

  public string Text
  {
    get
    {
      return (string) this.GetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzrXSFm_E\u003D);
    }
    set
    {
      this.SetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzrXSFm_E\u003D, (object) value);
    }
  }

  public LabelPlacement LabelPlacement
  {
    get
    {
      return (LabelPlacement) this.GetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzbhlExb5p620n);
    }
    set
    {
      this.SetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzbhlExb5p620n, (object) value);
    }
  }

  [Obsolete("We're sorry! AnnotationLabel.TextFormatting is obsolete. Please use a value converter or set StringFormat on a binding.", true)]
  public string TextFormatting
  {
    get
    {
      throw new Exception("");
    }
    set
    {
      throw new Exception("");
    }
  }

  public Style LabelStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzOjRqbPmhsyO_);
    }
    set
    {
      this.SetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzOjRqbPmhsyO_, (object) value);
    }
  }

  public Style AxisLabelStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzcXOoBjH4oSyy);
    }
    set
    {
      this.SetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003DzcXOoBjH4oSyy, (object) value);
    }
  }

  public CornerRadius CornerRadius
  {
    get
    {
      return (CornerRadius) this.GetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003Dz2k8rcqz_ESAS);
    }
    set
    {
      this.SetValue(dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd.\u0023\u003Dz2k8rcqz_ESAS, (object) value);
    }
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    if (this.\u0023\u003DzA5cpUiCtbnuj != null)
      this.\u0023\u003DzA5cpUiCtbnuj.ClearValue(TextBox.TextProperty);
    this.\u0023\u003DzA5cpUiCtbnuj = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<TextBox>("");
  }

  protected T \u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<T>(string _param1) where T : class
  {
    return this.GetTemplateChild(_param1) is T templateChild ? templateChild : throw new InvalidOperationException(string.Format("", (object) _param1));
  }

  private void \u0023\u003DzktbQ8VbB8fkZOpg\u0024LQ\u003D\u003D()
  {
    if (!this.CanEditText || !this.\u0023\u003DzLtJnA4CR3Exc().CanEditText || !this.\u0023\u003DzLtJnA4CR3Exc().IsSelected)
      return;
    this.\u0023\u003DzA5cpUiCtbnuj.IsEnabled = true;
    this.\u0023\u003DzA5cpUiCtbnuj.Focus();
  }

  private void \u0023\u003DzBjc4\u0024DEtqblaECuCQg\u003D\u003D()
  {
    if (this.\u0023\u003DzA5cpUiCtbnuj == null)
      return;
    this.\u0023\u003DzA5cpUiCtbnuj.IsEnabled = false;
  }

  private void \u0023\u003DzxKKlOa0jEDmy()
  {
    this.Style = this.\u0023\u003DztUuF6EohuIU9() ? this.AxisLabelStyle : this.LabelStyle;
  }

  private static void \u0023\u003DzXXEXIlUNtjM5(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd annotationLabel) || annotationLabel.\u0023\u003DzLtJnA4CR3Exc() == null)
      return;
    annotationLabel.\u0023\u003DzxKKlOa0jEDmy();
    annotationLabel.\u0023\u003DzLtJnA4CR3Exc().InvalidateLabel(annotationLabel);
  }

  private void \u0023\u003Dzdb7_FucBsCsx(object _param1, EventArgs _param2)
  {
    this.\u0023\u003DzBjc4\u0024DEtqblaECuCQg\u003D\u003D();
  }

  private void \u0023\u003DzGDG1SrCuVxZHIZFp\u00248qj2tc\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzktbQ8VbB8fkZOpg\u0024LQ\u003D\u003D();
    this.\u0023\u003DzLtJnA4CR3Exc().TrySelectAnnotation();
  }
}
