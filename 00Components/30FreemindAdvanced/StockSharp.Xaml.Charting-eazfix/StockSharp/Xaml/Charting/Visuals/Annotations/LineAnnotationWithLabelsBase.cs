// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.LineAnnotationWithLabelsBase
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using StockSharp.Charting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

internal abstract class LineAnnotationWithLabelsBase : LineAnnotation
{
  public static readonly DependencyProperty ShowLabelProperty = DependencyProperty.Register(nameof (ShowLabel), typeof (bool), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) false, new PropertyChangedCallback(LineAnnotationWithLabelsBase.OnShowLabelChanged)));
  protected internal static readonly DependencyProperty DefaultLabelValueProperty = DependencyProperty.Register(nameof (DefaultLabelValue), typeof (IComparable), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((PropertyChangedCallback) null));
  protected static readonly DependencyProperty DefaultTextFormattingProperty = DependencyProperty.Register(nameof (DefaultTextFormatting), typeof (string), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((PropertyChangedCallback) null));
  public static readonly DependencyProperty LabelPlacementProperty = DependencyProperty.Register(nameof (LabelPlacement), typeof (LabelPlacement), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) LabelPlacement.Auto));
  public static readonly DependencyProperty LabelValueProperty = DependencyProperty.Register(nameof (LabelValue), typeof (IComparable), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) null));
  public static readonly DependencyProperty LabelTextFormattingProperty = DependencyProperty.Register(nameof (LabelTextFormatting), typeof (string), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(LineAnnotationWithLabelsBase.OnLabelTextFormattingChanged)));
  public static readonly DependencyProperty FormattedLabelProperty = DependencyProperty.Register(nameof (FormattedLabel), typeof (string), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata((object) string.Empty));
  public static readonly DependencyProperty AnnotationLabelsProperty = DependencyProperty.Register(nameof (AnnotationLabels), typeof (ObservableCollection<AnnotationLabel>), typeof (LineAnnotationWithLabelsBase), new PropertyMetadata(new PropertyChangedCallback(LineAnnotationWithLabelsBase.OnAnnotationLabelsChanged)));
  private \u0023\u003DzKasBY8yFp0kHGchcdspopNuEz657XY3Et8L1BAmkUV5h _xyValueConverter;

  protected LineAnnotationWithLabelsBase()
  {
    this.AnnotationLabels = new ObservableCollection<AnnotationLabel>();
    Binding binding = new Binding(nameof (LabelValue))
    {
      Source = (object) this,
      Mode = BindingMode.OneWay,
      Converter = (IValueConverter) new \u0023\u003Dz2Qv8a0HfC9ieiy4m0TM2aAzmsNDy8Fpd\u0024of18IRv__L_(this)
    };
    this.SetBinding(LineAnnotationWithLabelsBase.FormattedLabelProperty, (BindingBase) binding);
  }

  protected IComparable DefaultLabelValue
  {
    get => (IComparable) this.GetValue(LineAnnotationWithLabelsBase.DefaultLabelValueProperty);
  }

  protected string DefaultTextFormatting
  {
    get => (string) this.GetValue(LineAnnotationWithLabelsBase.DefaultTextFormattingProperty);
  }

  protected string FormattedLabel
  {
    get => (string) this.GetValue(LineAnnotationWithLabelsBase.FormattedLabelProperty);
  }

  public ObservableCollection<AnnotationLabel> AnnotationLabels
  {
    get
    {
      return (ObservableCollection<AnnotationLabel>) this.GetValue(LineAnnotationWithLabelsBase.AnnotationLabelsProperty);
    }
    set => this.SetValue(LineAnnotationWithLabelsBase.AnnotationLabelsProperty, (object) value);
  }

  public bool ShowLabel
  {
    get => (bool) this.GetValue(LineAnnotationWithLabelsBase.ShowLabelProperty);
    set => this.SetValue(LineAnnotationWithLabelsBase.ShowLabelProperty, (object) value);
  }

  public LabelPlacement LabelPlacement
  {
    get => (LabelPlacement) this.GetValue(LineAnnotationWithLabelsBase.LabelPlacementProperty);
    set => this.SetValue(LineAnnotationWithLabelsBase.LabelPlacementProperty, (object) value);
  }

  [TypeConverter(typeof (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLmLZc7bTaOG0v8uu5qE\u003D))]
  public IComparable LabelValue
  {
    get => (IComparable) this.GetValue(LineAnnotationWithLabelsBase.LabelValueProperty);
    set => this.SetValue(LineAnnotationWithLabelsBase.LabelValueProperty, (object) value);
  }

  public string LabelTextFormatting
  {
    get => (string) this.GetValue(LineAnnotationWithLabelsBase.LabelTextFormattingProperty);
    set => this.SetValue(LineAnnotationWithLabelsBase.LabelTextFormattingProperty, (object) value);
  }

  protected void AttachLabels(
    IEnumerable<AnnotationLabel> labels)
  {
    bool flag = false;
    foreach (AnnotationLabel label in labels)
    {
      this.Attach(label);
      flag = label.\u0023\u003DztUuF6EohuIU9();
    }
    if (!flag)
      return;
    this.Refresh();
  }

  protected void DetachLabels(
    IEnumerable<AnnotationLabel> labels)
  {
    foreach (AnnotationLabel label in labels)
      this.Detach(label);
  }

  protected virtual void Attach(
    AnnotationLabel label)
  {
    if (this.IsHidden)
      return;
    LabelPlacement labelPlacement = this.GetLabelPlacement(label);
    this.ApplyPlacement(label, labelPlacement);
    label.DataContext = (object) this;
    label.\u0023\u003DzBV_vk9PuzvJU(this);
    IAxis usedAxis = this.GetUsedAxis();
    if (label.\u0023\u003DztUuF6EohuIU9())
    {
      if (usedAxis == null)
        return;
      usedAxis.get_ModifierAxisCanvas().\u0023\u003DzH0osWQkV_Y8_((object) label, -1);
    }
    else
      (this.AnnotationRoot as Grid).\u0023\u003DzH0osWQkV_Y8_((object) label, -1);
  }

  public abstract IAxis GetUsedAxis();

  protected override void OnXAxesCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs args)
  {
    this.InvalidateAnnotation();
  }

  private void InvalidateAnnotation()
  {
    this.InvalidateAxisLabels();
    this.BindDefaultLabelValue();
  }

  private void InvalidateAxisLabels()
  {
    using (this.SuspendUpdates())
      this.AnnotationLabels.Where<AnnotationLabel>(LineAnnotationWithLabelsBase.SomeClass34343383.\u0023\u003Dz_NsrFZCmUa8QKCiCnA\u003D\u003D ?? (LineAnnotationWithLabelsBase.SomeClass34343383.\u0023\u003Dz_NsrFZCmUa8QKCiCnA\u003D\u003D = new Func<AnnotationLabel, bool>(LineAnnotationWithLabelsBase.SomeClass34343383.SomeMethond0343.\u0023\u003DzCbZnNpb6mEZkqjOkAzkwfSE\u003D))).\u0023\u003Dz30RSSSygABj_<AnnotationLabel>((Action<AnnotationLabel>) (label =>
      {
        this.Detach(label);
        this.InvalidateLabel(label);
      }));
  }

  public void InvalidateLabel(
    AnnotationLabel annotationLabel)
  {
    if (annotationLabel != null)
      this.Attach(annotationLabel);
    this.MeasureRefresh();
  }

  private void BindDefaultLabelValue()
  {
    IAxis usedAxis = this.GetUsedAxis();
    this._xyValueConverter = this._xyValueConverter ?? (this._xyValueConverter = new \u0023\u003DzKasBY8yFp0kHGchcdspopNuEz657XY3Et8L1BAmkUV5h(this));
    Binding binding1 = new Binding(usedAxis == null || !usedAxis.\u0023\u003DzFrVmckt\u0024NpG6() ? "Y1" : "X1")
    {
      Source = (object) this,
      Converter = (IValueConverter) this._xyValueConverter
    };
    this.SetBinding(LineAnnotationWithLabelsBase.DefaultLabelValueProperty, (BindingBase) binding1);
    if (usedAxis == null)
      return;
    Binding binding2 = new Binding("CursorTextFormatting")
    {
      Source = (object) usedAxis
    };
    this.SetBinding(LineAnnotationWithLabelsBase.DefaultTextFormattingProperty, (BindingBase) binding2);
  }

  protected override void OnYAxesCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs args)
  {
    this.InvalidateAnnotation();
  }

  protected override void OnYAxisIdChanged() => this.InvalidateAnnotation();

  protected override void OnXAxisIdChanged() => this.InvalidateAnnotation();

  protected override void OnAxisAlignmentChanged(
    IAxis axis,
    AxisAlignment oldAlignment)
  {
    base.OnAxisAlignmentChanged(axis, oldAlignment);
    this.InvalidateAnnotation();
  }

  protected override void MakeInvisible()
  {
    base.MakeInvisible();
    this.DetachLabels(this.AnnotationLabels.Where<AnnotationLabel>(LineAnnotationWithLabelsBase.SomeClass34343383.\u0023\u003DzRvDbWZIRxYgzjR1S\u0024g\u003D\u003D ?? (LineAnnotationWithLabelsBase.SomeClass34343383.\u0023\u003DzRvDbWZIRxYgzjR1S\u0024g\u003D\u003D = new Func<AnnotationLabel, bool>(LineAnnotationWithLabelsBase.SomeClass34343383.SomeMethond0343.\u0023\u003DzjWiJK\u0024N5Qts4a4OqehmURFQ\u003D))));
  }

  protected override void MakeVisible(
    AnnotationCoordinates coordinates)
  {
    base.MakeVisible(coordinates);
    IAxis usedAxis = this.GetUsedAxis();
    if (usedAxis == null || usedAxis.get_ModifierAxisCanvas() == null)
      return;
    this.AttachLabels(this.AnnotationLabels.Where<AnnotationLabel>(LineAnnotationWithLabelsBase.SomeClass34343383.\u0023\u003Dz9HSFgt5cVDGouicyKg\u003D\u003D ?? (LineAnnotationWithLabelsBase.SomeClass34343383.\u0023\u003Dz9HSFgt5cVDGouicyKg\u003D\u003D = new Func<AnnotationLabel, bool>(LineAnnotationWithLabelsBase.SomeClass34343383.SomeMethond0343.\u0023\u003Dzhg7sTccUaiLYo11XTCsKbp4\u003D))));
  }

  public override void OnApplyTemplate()
  {
    this.DetachLabels((IEnumerable<AnnotationLabel>) this.AnnotationLabels);
    this.AnnotationRoot = (FrameworkElement) this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Grid>("PART_LineAnnotationRoot");
    this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Line>("PART_GhostLine");
    this.AttachLabels((IEnumerable<AnnotationLabel>) this.AnnotationLabels);
    this.Refresh();
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this.BindDefaultLabelValue();
  }

  protected override void OnAnnotationLoaded(object sender, RoutedEventArgs e)
  {
    base.OnAnnotationLoaded(sender, e);
    this.GetBindingExpression(LineAnnotationWithLabelsBase.DefaultLabelValueProperty)?.UpdateTarget();
  }

  public AnnotationLabel AddLabel()
  {
    AnnotationLabel c52EkqS76HuN2Ejd = new AnnotationLabel();
    Binding binding1 = new Binding("LabelPlacement")
    {
      Source = (object) this,
      Mode = BindingMode.OneWay
    };
    c52EkqS76HuN2Ejd.SetBinding(AnnotationLabel.\u0023\u003DzbhlExb5p620n, (BindingBase) binding1);
    Binding binding2 = new Binding("ContextMenu")
    {
      Source = (object) this,
      Mode = BindingMode.OneWay
    };
    c52EkqS76HuN2Ejd.SetBinding(FrameworkElement.ContextMenuProperty, (BindingBase) binding2);
    this.AnnotationLabels.Add(c52EkqS76HuN2Ejd);
    return c52EkqS76HuN2Ejd;
  }

  protected void TryPlaceAxisLabels(Point offset)
  {
    LineAnnotationWithLabelsBase.\u0023\u003DzVqJpnDltbvS8bNvezolg25s\u003D dltbvS8bNvezolg25s = new LineAnnotationWithLabelsBase.\u0023\u003DzVqJpnDltbvS8bNvezolg25s\u003D();
    dltbvS8bNvezolg25s._variableSome3535 = this;
    dltbvS8bNvezolg25s.\u0023\u003Dzze_xX5E\u003D = offset;
    dltbvS8bNvezolg25s.\u0023\u003Dzfl\u0024A1s0\u003D = this.GetUsedAxis();
    if (dltbvS8bNvezolg25s.\u0023\u003Dzfl\u0024A1s0\u003D == null || dltbvS8bNvezolg25s.\u0023\u003Dzfl\u0024A1s0\u003D.get_ModifierAxisCanvas() == null)
      return;
    this.AnnotationLabels.Where<AnnotationLabel>(LineAnnotationWithLabelsBase.SomeClass34343383.\u0023\u003DzxqTBwn3Lm9d_a3_OxA\u003D\u003D ?? (LineAnnotationWithLabelsBase.SomeClass34343383.\u0023\u003DzxqTBwn3Lm9d_a3_OxA\u003D\u003D = new Func<AnnotationLabel, bool>(LineAnnotationWithLabelsBase.SomeClass34343383.SomeMethond0343.\u0023\u003DzLjDDzfRiLodJ0Yk0_kZqhnuCB1rY))).\u0023\u003Dz30RSSSygABj_<AnnotationLabel>(new Action<AnnotationLabel>(dltbvS8bNvezolg25s.\u0023\u003DzjBItOqBmy2tJFsdrzSt4jsU\u003D));
  }

  protected virtual void PlaceAxisLabel(
    IAxis axis,
    AnnotationLabel axisLabel,
    Point offset)
  {
    if (axisLabel.Parent == null)
    {
      this.Attach(axisLabel);
      this.Refresh();
    }
    axis.\u0023\u003DzXOlF_vImljp4((FrameworkElement) axisLabel, offset);
    axis.\u0023\u003Dz07\u0024lhqBRmUuR((FrameworkElement) axisLabel, offset);
  }

  protected virtual void Detach(
    AnnotationLabel label)
  {
    label.\u0023\u003DzBV_vk9PuzvJU((LineAnnotationWithLabelsBase) null);
    Grid annotationRoot = this.AnnotationRoot as Grid;
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk uxEichh7AgNdmShk = this.GetUsedAxis()?.get_ModifierAxisCanvas() ?? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) (label.Parent as ModifierAxisCanvas);
    annotationRoot.\u0023\u003DziYdJ\u00246cCiBha((object) label);
    uxEichh7AgNdmShk.\u0023\u003DziYdJ\u00246cCiBha((object) label);
  }

  protected virtual void ApplyPlacement(
    AnnotationLabel label,
    LabelPlacement placement)
  {
    bool flag1 = placement.\u0023\u003DzEiJTKKs\u003D();
    bool flag2 = placement.\u0023\u003DzHnHjVLw\u003D();
    bool flag3 = placement.\u0023\u003DzY_J1ez4\u003D();
    bool flag4 = placement.\u0023\u003DzsJ2ryUw\u003D();
    if (flag1 | flag2)
    {
      label.SetValue(Grid.ColumnProperty, (object) 1);
      label.SetValue(Grid.RowProperty, (object) (flag1 ? 0 : 2));
      label.HorizontalAlignment = HorizontalAlignment.Center;
      label.VerticalAlignment = flag1 ? VerticalAlignment.Bottom : VerticalAlignment.Top;
      if (flag4)
        label.HorizontalAlignment = HorizontalAlignment.Right;
      if (!flag3)
        return;
      label.HorizontalAlignment = HorizontalAlignment.Left;
    }
    else
    {
      label.SetValue(Grid.ColumnProperty, (object) (flag4 ? 2 : (!flag3 ? 1 : 0)));
      label.SetValue(Grid.RowProperty, (object) 1);
      label.HorizontalAlignment = HorizontalAlignment.Center;
      label.VerticalAlignment = VerticalAlignment.Center;
    }
  }

  internal virtual LabelPlacement ResolveAutoPlacement() => LabelPlacement.Top;

  internal LabelPlacement GetLabelPlacement(
    AnnotationLabel label)
  {
    return label.LabelPlacement == LabelPlacement.Auto ? this.ResolveAutoPlacement() : label.LabelPlacement;
  }

  protected override Cursor GetSelectedCursor() => Cursors.SizeNS;

  public override bool IsPointWithinBounds(Point point)
  {
    Grid annotationRoot = this.AnnotationRoot as Grid;
    point = this.ParentSurface.\u0023\u003DzBgWxEdRxHdEh().TranslatePoint(point, (IHitTestable) this);
    Point point1 = point;
    return annotationRoot.\u0023\u003DzbOxVzAyGdX66(point1);
  }

  private static void OnAnnotationLabelsChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    ObservableCollection<AnnotationLabel> newValue = e.NewValue as ObservableCollection<AnnotationLabel>;
    ObservableCollection<AnnotationLabel> oldValue = e.OldValue as ObservableCollection<AnnotationLabel>;
    LineAnnotationWithLabelsBase annotationWithLabelsBase = (LineAnnotationWithLabelsBase) d;
    if (newValue != null)
      newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(annotationWithLabelsBase.OnAnnotationLabelsCollectionChanged);
    if (oldValue != null)
      oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(annotationWithLabelsBase.OnAnnotationLabelsCollectionChanged);
    annotationWithLabelsBase.OnAnnotationLabelsChanged((IList) newValue, (IList) oldValue);
    annotationWithLabelsBase.Refresh();
  }

  private void OnAnnotationLabelsChanged(IList newItems, IList oldItems)
  {
    if (newItems != null)
      this.AttachLabels(newItems.OfType<AnnotationLabel>());
    if (oldItems == null)
      return;
    this.DetachLabels(oldItems.OfType<AnnotationLabel>());
  }

  private void OnAnnotationLabelsCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs e)
  {
    this.OnAnnotationLabelsChanged(e.NewItems, e.OldItems);
  }

  private static void OnLabelTextFormattingChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    if (!(d is LineAnnotationWithLabelsBase annotationWithLabelsBase))
      return;
    annotationWithLabelsBase.GetBindingExpression(LineAnnotationWithLabelsBase.FormattedLabelProperty)?.UpdateTarget();
  }

  private static void OnShowLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is LineAnnotationWithLabelsBase annotationWithLabelsBase))
      return;
    if (annotationWithLabelsBase.ShowLabel && annotationWithLabelsBase.AnnotationLabels.Count == 0)
    {
      AnnotationLabel annotationLabel = annotationWithLabelsBase.AddLabel();
      annotationWithLabelsBase.InvalidateLabel(annotationLabel);
    }
    else
    {
      if (annotationWithLabelsBase.ShowLabel || annotationWithLabelsBase.AnnotationLabels.Count != 1)
        return;
      AnnotationLabel annotationLabel = annotationWithLabelsBase.AnnotationLabels[0];
      annotationWithLabelsBase.AnnotationLabels.Remove(annotationLabel);
      annotationWithLabelsBase.InvalidateLabel((AnnotationLabel) null);
    }
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly LineAnnotationWithLabelsBase.SomeClass34343383 SomeMethond0343 = new LineAnnotationWithLabelsBase.SomeClass34343383();
    public static Func<AnnotationLabel, bool> \u0023\u003Dz_NsrFZCmUa8QKCiCnA\u003D\u003D;
    public static Func<AnnotationLabel, bool> \u0023\u003DzRvDbWZIRxYgzjR1S\u0024g\u003D\u003D;
    public static Func<AnnotationLabel, bool> \u0023\u003Dz9HSFgt5cVDGouicyKg\u003D\u003D;
    public static Func<AnnotationLabel, bool> \u0023\u003DzxqTBwn3Lm9d_a3_OxA\u003D\u003D;

    internal bool \u0023\u003DzCbZnNpb6mEZkqjOkAzkwfSE\u003D(
      AnnotationLabel _param1)
    {
      return _param1.\u0023\u003DztUuF6EohuIU9();
    }

    internal bool \u0023\u003DzjWiJK\u0024N5Qts4a4OqehmURFQ\u003D(
      AnnotationLabel _param1)
    {
      return _param1.\u0023\u003DztUuF6EohuIU9();
    }

    internal bool \u0023\u003Dzhg7sTccUaiLYo11XTCsKbp4\u003D(
      AnnotationLabel _param1)
    {
      return _param1.Parent == null;
    }

    internal bool \u0023\u003DzLjDDzfRiLodJ0Yk0_kZqhnuCB1rY(
      AnnotationLabel _param1)
    {
      return _param1.\u0023\u003DztUuF6EohuIU9() && _param1.\u0023\u003DzLtJnA4CR3Exc() != null;
    }
  }

  private sealed class \u0023\u003DzVqJpnDltbvS8bNvezolg25s\u003D
  {
    public LineAnnotationWithLabelsBase _variableSome3535;
    public IAxis \u0023\u003Dzfl\u0024A1s0\u003D;
    public Point \u0023\u003Dzze_xX5E\u003D;

    internal void \u0023\u003DzjBItOqBmy2tJFsdrzSt4jsU\u003D(
      AnnotationLabel _param1)
    {
      this._variableSome3535.PlaceAxisLabel(this.\u0023\u003Dzfl\u0024A1s0\u003D, _param1, this.\u0023\u003Dzze_xX5E\u003D);
    }
  }
}
