// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationBase
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using StockSharp.Charting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

internal abstract class AnnotationBase : 
  \u0023\u003DzHHCBm9UpsKz28K2k\u002432Cv_vXeGtDJsjTomycKYo\u003D,
  \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D,
  \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z,
  \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV,
  IXmlSerializable,
  \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D
{
  public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427791), typeof (string), typeof (AnnotationBase), new PropertyMetadata((object) \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539431487), new PropertyChangedCallback(AnnotationBase.OnXAxisIdChanged)));
  public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427833), typeof (string), typeof (AnnotationBase), new PropertyMetadata((object) \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539431487), new PropertyChangedCallback(AnnotationBase.OnYAxisIdChanged)));
  public static readonly DependencyProperty X1Property = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434477), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationBase.OnAnnotationPositionChanged)));
  public static readonly DependencyProperty Y1Property = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434262), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationBase.OnAnnotationPositionChanged)));
  public static readonly DependencyProperty X2Property = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434243), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationBase.OnAnnotationPositionChanged)));
  public static readonly DependencyProperty Y2Property = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434252), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationBase.OnAnnotationPositionChanged)));
  public static readonly DependencyProperty AnnotationCanvasProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539341973), typeof (\u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D), typeof (AnnotationBase), new PropertyMetadata((object) \u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D.AboveChart, new PropertyChangedCallback(AnnotationBase.OnRenderablePropertyChanged)));
  public static readonly DependencyProperty CoordinateModeProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427489), typeof (AnnotationCoordinateMode), typeof (AnnotationBase), new PropertyMetadata((object) AnnotationCoordinateMode.Absolute, new PropertyChangedCallback(AnnotationBase.OnRenderablePropertyChanged)));
  public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539439396), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(AnnotationBase.OnIsSelectedChanged)));
  public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434496), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(AnnotationBase.OnIsEditableChanged)));
  public static readonly DependencyProperty IsHiddenProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539439409), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(AnnotationBase.OnIsHiddenChanged)));
  public static readonly DependencyProperty DragDirectionsProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539341968), typeof (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd), typeof (AnnotationBase), new PropertyMetadata((object) dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XYDirection));
  public static readonly DependencyProperty ResizeDirectionsProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539341985), typeof (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd), typeof (AnnotationBase), new PropertyMetadata((object) dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XYDirection));
  public static readonly DependencyProperty CanEditTextProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539342044), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false));
  public static readonly DependencyProperty ResizingGripsStyleProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539342026), typeof (Style), typeof (AnnotationBase), new PropertyMetadata((PropertyChangedCallback) null));
  private bool _isAttached;
  private bool _templateApplied;
  protected FrameworkElement AnnotationRoot;
  private bool _isDragging;
  private bool _isPrimaryDrag;
  private Point _startPoint;
  private bool _isMouseLeftDown;
  private bool _isResizable;
  private DateTime _mouseLeftDownTimestamp;
  private \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _startDragAnnotationCoordinates;
  private IList<\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x> _myAdorners = (IList<\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x>) new List<\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x>();
  private \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _yAxis;
  private \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _xAxis;
  private bool _isLoaded;

  protected AnnotationBase()
  {
    this.DefaultStyleKey = (object) typeof (AnnotationBase);
    this.IsResizable = true;
  }

  protected Point DragStartPoint => this._startPoint;

  event EventHandler<\u0023\u003DzaDDeYuGlsOp51QXy5MWJZxERLr9hDQLdDJPw_pXdD1WK> \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchDown
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  event EventHandler<\u0023\u003DzaDDeYuGlsOp51QXy5MWJZxERLr9hDQLdDJPw_pXdD1WK> \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchMove
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  event EventHandler<\u0023\u003DzaDDeYuGlsOp51QXy5MWJZxERLr9hDQLdDJPw_pXdD1WK> \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchUp
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  public event EventHandler Selected;

  public event EventHandler Unselected;

  public event EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D> DragStarted;

  public event EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D> DragEnded;

  public event EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6HbbjLeMvfhhb2s2mR8\u003D> DragDelta;

  public event EventHandler IsHiddenChanged;

  public event MouseButtonEventHandler MouseMiddleButtonDown;

  public event MouseButtonEventHandler MouseMiddleButtonUp;

  private void PreviewMouseUpHandler(object sender, MouseButtonEventArgs e)
  {
    if (e.ChangedButton != MouseButton.Middle)
      return;
    MouseButtonEventHandler mouseMiddleButtonUp = this.MouseMiddleButtonUp;
    if (mouseMiddleButtonUp == null)
      return;
    mouseMiddleButtonUp(sender, e);
  }

  private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
  {
    if (e.ChangedButton != MouseButton.Middle)
      return;
    MouseButtonEventHandler middleButtonDown = this.MouseMiddleButtonDown;
    if (middleButtonDown == null)
      return;
    middleButtonDown(sender, e);
  }

  public Style ResizingGripsStyle
  {
    get => (Style) this.GetValue(AnnotationBase.ResizingGripsStyleProperty);
    set => this.SetValue(AnnotationBase.ResizingGripsStyleProperty, (object) value);
  }

  public bool CanEditText
  {
    get => (bool) this.GetValue(AnnotationBase.CanEditTextProperty);
    set => this.SetValue(AnnotationBase.CanEditTextProperty, (object) value);
  }

  public bool IsResizable
  {
    get => this._isResizable;
    protected set
    {
      this._isResizable = value;
      this.\u0023\u003Dz15moWio\u003D(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539342055));
    }
  }

  public string XAxisId
  {
    get => (string) this.GetValue(AnnotationBase.XAxisIdProperty);
    set => this.SetValue(AnnotationBase.XAxisIdProperty, (object) value);
  }

  public string YAxisId
  {
    get => (string) this.GetValue(AnnotationBase.YAxisIdProperty);
    set => this.SetValue(AnnotationBase.YAxisIdProperty, (object) value);
  }

  public dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd DragDirections
  {
    get
    {
      return (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd) this.GetValue(AnnotationBase.DragDirectionsProperty);
    }
    set => this.SetValue(AnnotationBase.DragDirectionsProperty, (object) value);
  }

  public dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd ResizeDirections
  {
    get
    {
      return (dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd) this.GetValue(AnnotationBase.ResizeDirectionsProperty);
    }
    set => this.SetValue(AnnotationBase.ResizeDirectionsProperty, (object) value);
  }

  public AnnotationCoordinateMode CoordinateMode
  {
    get => (AnnotationCoordinateMode) this.GetValue(AnnotationBase.CoordinateModeProperty);
    set => this.SetValue(AnnotationBase.CoordinateModeProperty, (object) value);
  }

  public \u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D AnnotationCanvas
  {
    get
    {
      return (\u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D) this.GetValue(AnnotationBase.AnnotationCanvasProperty);
    }
    set => this.SetValue(AnnotationBase.AnnotationCanvasProperty, (object) value);
  }

  public bool IsSelected
  {
    get => (bool) this.GetValue(AnnotationBase.IsSelectedProperty);
    set => this.SetValue(AnnotationBase.IsSelectedProperty, (object) value);
  }

  public bool IsEditable
  {
    get => (bool) this.GetValue(AnnotationBase.IsEditableProperty);
    set => this.SetValue(AnnotationBase.IsEditableProperty, (object) value);
  }

  public bool IsHidden
  {
    get => (bool) this.GetValue(AnnotationBase.IsHiddenProperty);
    set => this.SetValue(AnnotationBase.IsHiddenProperty, (object) value);
  }

  [TypeConverter(typeof (\u0023\u003DztorG3HTUDpMsfjPqFEEe9Ofk2uEDAGTcKLRwQVg\u003D))]
  public IComparable X1
  {
    get => (IComparable) this.GetValue(AnnotationBase.X1Property);
    set => this.SetValue(AnnotationBase.X1Property, (object) value);
  }

  [TypeConverter(typeof (\u0023\u003DztorG3HTUDpMsfjPqFEEe9Ofk2uEDAGTcKLRwQVg\u003D))]
  public IComparable X2
  {
    get => (IComparable) this.GetValue(AnnotationBase.X2Property);
    set => this.SetValue(AnnotationBase.X2Property, (object) value);
  }

  [TypeConverter(typeof (\u0023\u003DztorG3HTUDpMsfjPqFEEe9Ofk2uEDAGTcKLRwQVg\u003D))]
  public IComparable Y1
  {
    get => (IComparable) this.GetValue(AnnotationBase.Y1Property);
    set => this.SetValue(AnnotationBase.Y1Property, (object) value);
  }

  [TypeConverter(typeof (\u0023\u003DztorG3HTUDpMsfjPqFEEe9Ofk2uEDAGTcKLRwQVg\u003D))]
  public IComparable Y2
  {
    get => (IComparable) this.GetValue(AnnotationBase.Y2Property);
    set => this.SetValue(AnnotationBase.Y2Property, (object) value);
  }

  protected abstract Cursor GetSelectedCursor();

  protected virtual void OnDragStarted()
  {
  }

  protected virtual void OnDragEnded()
  {
  }

  protected virtual void OnDragDelta(double hOffset, double vOffset)
  {
  }

  public void StartDrag(bool isPrimaryDrag)
  {
    this._isDragging = true;
    this._isPrimaryDrag = isPrimaryDrag;
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = this.YAxis?.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = this.XAxis?.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    this._startDragAnnotationCoordinates = this.GetCoordinates(canvas, xCalc, yCalc);
    this.OnDragStarted();
    this.RaiseAnnotationDragStarted(this._isPrimaryDrag, false);
  }

  public void EndDrag()
  {
    this.OnDragEnded();
    bool isPrimaryDrag = this._isPrimaryDrag;
    this._isDragging = this._isPrimaryDrag = false;
    this.RaiseAnnotationDragEnded(isPrimaryDrag, false);
  }

  public void Drag(double hOffset, double vOffset)
  {
    if (!this._isDragging)
      return;
    using (this.SuspendUpdates())
      (hOffset, vOffset) = this.MoveAnnotationTo(this._startDragAnnotationCoordinates, hOffset, vOffset);
    this.OnDragDelta(hOffset, vOffset);
    this.RaiseAnnotationDragging(hOffset, vOffset, this._isPrimaryDrag, false);
  }

  public virtual bool CanMultiSelect(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D[] annotations)
  {
    return annotations.Length == 1;
  }

  public override bool IsAttached
  {
    get => this._isAttached;
    set
    {
      this._isAttached = value;
      if (this._templateApplied)
        return;
      this.ApplyTemplate();
      this._templateApplied = true;
    }
  }

  public override \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB YAxis
  {
    get => this._yAxis ?? (this._yAxis = this.\u0023\u003Dz4uoxB8oLWxeL(this.YAxisId));
  }

  public override \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB XAxis
  {
    get => this._xAxis ?? (this._xAxis = this.\u0023\u003DzI0EiGDjWkH8S(this.XAxisId));
  }

  protected \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk AnnotationOverlaySurface
  {
    get
    {
      return this.ParentSurface == null ? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) null : this.ParentSurface.\u0023\u003DzFPPJbPlQRagwT6aZuQ\u003D\u003D();
    }
  }

  protected \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk AnnotationUnderlaySurface
  {
    get
    {
      return this.ParentSurface == null ? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) null : this.ParentSurface.\u0023\u003Dz7EP15yq7Yz\u0024jLVX6GgE8gjs\u003D();
    }
  }

  void \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002EOnXAxesCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs args)
  {
    this.\u0023\u003DzHGGuFpQ\u003D((DispatcherPriority) 8, new Action(new AnnotationBase.\u0023\u003Dzffg\u0024YXBnGm7H\u0024PCqlQv7PCc\u003D()
    {
      \u0023\u003DzRRvwDu67s9Rm = this,
      \u0023\u003DzwM8aRUE\u003D = sender,
      \u0023\u003DzTi2kmf4\u003D = args
    }.\u0023\u003Dz51OAXpgKHWL1SXLaCA5i5tpZSV6aNEG7pUicQk0yky3I6F_EHJGXbx7TM94H));
  }

  void \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002EOnYAxesCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs args)
  {
    this.\u0023\u003DzHGGuFpQ\u003D((DispatcherPriority) 8, new Action(new AnnotationBase.\u0023\u003DzPDNKpHuRG7yCW5JW8_EOM4E\u003D()
    {
      \u0023\u003DzRRvwDu67s9Rm = this,
      \u0023\u003DzwM8aRUE\u003D = sender,
      \u0023\u003DzTi2kmf4\u003D = args
    }.\u0023\u003DzIOaiByPzcFm0hgjIqRsFUKJzOnbT2YChDvFyp8X_Z2t0giSUkPjoyzwmi0T_));
  }

  private void OnAxisAlignmentChanged(
    object sender,
    \u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D e)
  {
    if (!(e.\u0023\u003DzAz1mGnFFNNic() == this.XAxisId) && !(e.\u0023\u003DzAz1mGnFFNNic() == this.YAxisId))
      return;
    this.OnAxisAlignmentChanged(e.\u0023\u003DzAz1mGnFFNNic() == this.XAxisId ? this.XAxis : this.YAxis, e.\u0023\u003Dz4LPnYX_iGIbT());
  }

  protected virtual void OnAxisAlignmentChanged(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB axis,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd oldAlignment)
  {
  }

  protected virtual void OnXAxesCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs args)
  {
  }

  protected virtual void OnYAxesCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs args)
  {
  }

  protected virtual void OnYAxisIdChanged()
  {
  }

  protected virtual void OnXAxisIdChanged()
  {
  }

  protected virtual void FocusInputTextArea()
  {
  }

  protected virtual void RemoveFocusInputTextArea()
  {
  }

  public override void OnAttached()
  {
    this.AttachInteractionHandlersTo((FrameworkElement) this);
    this.Loaded += new RoutedEventHandler(this.OnAnnotationLoaded);
    this.ParentSurface.\u0023\u003DzexJGsaAi6rVI(new EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D>(this.OnAxisAlignmentChanged));
  }

  protected virtual void OnAnnotationLoaded(object sender, RoutedEventArgs e)
  {
    this.PrepareForRendering();
  }

  private void PrepareForRendering()
  {
    if (!this._isLoaded)
      this._isLoaded = true;
    this.Refresh();
    this.PerformFocusOnInputTextArea();
  }

  protected void PerformFocusOnInputTextArea()
  {
    if (this.CanEditText && this.IsSelected)
      this.FocusInputTextArea();
    else
      this.RemoveFocusInputTextArea();
  }

  protected virtual void AttachInteractionHandlersTo(FrameworkElement source)
  {
    source.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnAnnotationMouseDown);
    source.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnAnnotationMouseUp);
    source.MouseMove += new MouseEventHandler(this.OnAnnotationMouseMove);
    source.PreviewMouseDown += new MouseButtonEventHandler(this.PreviewMouseDownHandler);
    source.PreviewMouseUp += new MouseButtonEventHandler(this.PreviewMouseUpHandler);
  }

  protected virtual void OnAnnotationMouseDown(object sender, MouseButtonEventArgs e)
  {
    if (e.ChangedButton != MouseButton.Left)
      return;
    e.Handled = this.TrySelectAnnotation();
    if (!this.IsSelected || !this.IsEditable)
      return;
    this._startPoint = Mouse.GetPosition((IInputElement) (this.\u0023\u003Dzwc4Gzka23TGB() as UIElement));
    this._isMouseLeftDown = true;
    this._mouseLeftDownTimestamp = DateTime.UtcNow;
    this.CaptureMouse();
    e.Handled = true;
  }

  protected virtual void OnAnnotationMouseUp(object sender, MouseButtonEventArgs e)
  {
    if (e.ChangedButton != MouseButton.Left)
      return;
    this.ReleaseMouseCapture();
    this._isMouseLeftDown = false;
    if (this._isDragging)
      this.EndDrag();
    else
      this.PerformFocusOnInputTextArea();
  }

  protected virtual void OnAnnotationMouseMove(object sender, MouseEventArgs e)
  {
    if (!this._isMouseLeftDown || DateTime.UtcNow - this._mouseLeftDownTimestamp < TimeSpan.FromMilliseconds(2.0) || e.LeftButton != MouseButtonState.Pressed)
      return;
    if (!this._isDragging)
      this.StartDrag(true);
    Point position = Mouse.GetPosition((IInputElement) (this.\u0023\u003Dzwc4Gzka23TGB() as UIElement));
    this.Drag(this.DragDirections == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection ? 0.0 : position.X - this._startPoint.X, this.DragDirections == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection ? 0.0 : position.Y - this._startPoint.Y);
  }

  public override void OnDetached()
  {
    using (\u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote fq05jnDg3bOrIrgCjote = this.SuspendUpdates())
    {
      fq05jnDg3bOrIrgCjote.\u0023\u003DzZreBqCsNdaNN(false);
      this.IsSelected = false;
      this.MakeInvisible();
      (this.Parent as \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk).\u0023\u003DziYdJ\u00246cCiBha((object) this);
      this.DetachInteractionHandlersFrom((FrameworkElement) this);
      if (this.ParentSurface != null)
        this.ParentSurface.\u0023\u003Dz38JNnebwqLph(new EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D>(this.OnAxisAlignmentChanged));
      this.Loaded -= new RoutedEventHandler(this.OnAnnotationLoaded);
    }
  }

  protected virtual void DetachInteractionHandlersFrom(FrameworkElement source)
  {
    source.MouseLeftButtonDown -= new MouseButtonEventHandler(this.OnAnnotationMouseDown);
    source.MouseLeftButtonUp -= new MouseButtonEventHandler(this.OnAnnotationMouseUp);
    source.MouseMove -= new MouseEventHandler(this.OnAnnotationMouseMove);
    source.PreviewMouseDown -= new MouseButtonEventHandler(this.PreviewMouseDownHandler);
    source.PreviewMouseUp -= new MouseButtonEventHandler(this.PreviewMouseUpHandler);
  }

  public bool Refresh()
  {
    if (this.IsSuspended || !this._isLoaded || !this.IsAttached)
      return false;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCoordinateCalculator = this.XAxis != null ? this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCoordinateCalculator = this.YAxis != null ? this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
    if (xCoordinateCalculator != null && yCoordinateCalculator != null)
      this.Update(xCoordinateCalculator, yCoordinateCalculator);
    return true;
  }

  public virtual void Update(
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCoordinateCalculator,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCoordinateCalculator)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    if (canvas == null)
      return;
    canvas.\u0023\u003DzH0osWQkV_Y8_((object) this, -1);
    if (!this._isLoaded)
      return;
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates = this.GetCoordinates(canvas, xCoordinateCalculator, yCoordinateCalculator);
    if (this.IsInBounds(coordinates, canvas))
    {
      if (!this.IsHidden)
      {
        this.MakeVisible(coordinates);
      }
      else
      {
        if (this.Visibility == Visibility.Collapsed)
          return;
        this.MakeInvisible();
      }
    }
    else
      this.MakeInvisible();
  }

  public void Hide() => this.IsHidden = true;

  public void Show() => this.IsHidden = false;

  protected virtual void MakeInvisible()
  {
    this.HideAdornerMarkers();
    this.Visibility = Visibility.Collapsed;
  }

  protected void HideAdornerMarkers()
  {
    foreach (\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x adorner in (IEnumerable<\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x>) this._myAdorners)
      adorner.\u0023\u003DzUf222sU\u003D();
  }

  protected IEnumerable<T> GetUsedAdorners<T>(Canvas adornerLayer) where T : \u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x
  {
    return (IEnumerable<T>) adornerLayer.Children.OfType<T>().Where<T>((Func<T, bool>) (x => x.\u0023\u003Dzy2oKVLXXOFmI() == this)).ToList<T>();
  }

  protected virtual void MakeVisible(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates)
  {
    this.Visibility = Visibility.Visible;
    if (this.AnnotationRoot != null)
    {
      if (!this._isLoaded || Size.op_Equality(this.AnnotationRoot.RenderSize, new Size()))
        this.AnnotationRoot.\u0023\u003DzI0WdlDcUgrX_();
      this.PlaceAnnotation(coordinates);
    }
    this.UpdateAdorners();
  }

  internal void UpdateAdorners()
  {
    Canvas adornerLayer = this.GetAdornerLayer();
    if (adornerLayer == null)
      return;
    this.GetUsedAdorners<\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x>(adornerLayer).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x>(AnnotationBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D ?? (AnnotationBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D = new Action<\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x>(AnnotationBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzjRwQMrvkACc83ywGyp_nJAU\u003D)));
  }

  protected virtual bool IsInBounds(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates,
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas)
  {
    return this.GetCurrentPlacementStrategy().\u0023\u003DzxGhbraO0gg9\u0024(coordinates, canvas);
  }

  protected virtual void PlaceAnnotation(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates)
  {
    this.GetCurrentPlacementStrategy().\u0023\u003DzNUoYFVRHgzxB(coordinates);
  }

  protected \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk GetCanvas(
    \u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D annotationCanvas)
  {
    if (this.ParentSurface == null)
      return (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) null;
    switch (annotationCanvas)
    {
      case \u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D.AboveChart:
        return this.ParentSurface.\u0023\u003DzFPPJbPlQRagwT6aZuQ\u003D\u003D();
      case \u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D.BelowChart:
        return this.ParentSurface.\u0023\u003Dz7EP15yq7Yz\u0024jLVX6GgE8gjs\u003D();
      case \u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D.YAxis:
        return this.YAxis == null ? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) null : this.YAxis.get_ModifierAxisCanvas();
      case \u0023\u003DzeHqydGt1MYwtwPKPfmmWnMQ7cqtdeTQDwXdpP\u0024g\u003D.XAxis:
        return this.XAxis == null ? (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) null : this.XAxis.get_ModifierAxisCanvas();
      default:
        throw new InvalidOperationException(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539341845), (object) annotationCanvas));
    }
  }

  protected virtual \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<AnnotationBase>(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<AnnotationBase>(this);
  }

  protected static void OnRenderablePropertyChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    ((AnnotationBase) d).Refresh();
  }

  protected static void OnAnnotationPositionChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    ((AnnotationBase) d).Refresh();
    ((\u0023\u003DzHHCBm9UpsKz28K2k\u002432Cv_vXeGtDJsjTomycKYo\u003D) d).\u0023\u003Dz15moWio\u003D(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539429164));
  }

  public void UpdatePosition(Point point1, Point point2)
  {
    using (this.SuspendUpdates())
    {
      IComparable[] comparableArray1 = this.FromCoordinates(point1);
      this.SetCurrentValue(AnnotationBase.X1Property, (object) comparableArray1[0]);
      this.SetCurrentValue(AnnotationBase.Y1Property, (object) comparableArray1[1]);
      if (this is IAnchorPointAnnotation)
        return;
      IComparable[] comparableArray2 = this.FromCoordinates(point2);
      this.SetCurrentValue(AnnotationBase.X2Property, (object) comparableArray2[0]);
      this.SetCurrentValue(AnnotationBase.Y2Property, (object) comparableArray2[1]);
    }
  }

  protected virtual IComparable[] FromCoordinates(Point coords)
  {
    return this.FromCoordinates(coords.X, coords.Y);
  }

  protected virtual IComparable[] FromCoordinates(double xCoord, double yCoord)
  {
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB xaxis = this.XAxis;
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB yaxis = this.YAxis;
    bool isHorizontalAxis = xaxis.IsHorizontalAxis;
    return new IComparable[2]
    {
      isHorizontalAxis ? this.FromCoordinate(xCoord, xaxis) : this.FromCoordinate(yCoord, xaxis),
      isHorizontalAxis ? this.FromCoordinate(yCoord, yaxis) : this.FromCoordinate(xCoord, yaxis)
    };
  }

  protected virtual IComparable FromCoordinate(
    double coord,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB axis)
  {
    if (axis == null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539341909));
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd ks34Z259A4NengcEjd = axis.IsHorizontalAxis ? dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection : dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection;
    return this.CoordinateMode == AnnotationCoordinateMode.Relative || this.CoordinateMode == AnnotationCoordinateMode.RelativeX && ks34Z259A4NengcEjd == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection || this.CoordinateMode == AnnotationCoordinateMode.RelativeY && ks34Z259A4NengcEjd == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection ? this.FromRelativeCoordinate(coord, axis) : (!(axis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0) ? axis.\u0023\u003DzACwLhyc\u003D(coord) : (IComparable) (int) q9i0MXI7Qb9c1V6c0.\u0023\u003DzACwLhyc\u003D(coord));
  }

  protected virtual IComparable FromRelativeCoordinate(
    double coord,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB axis)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    double num = axis.IsHorizontalAxis ? canvas.\u0023\u003Dzu2ObQ3hMALTN() : canvas.\u0023\u003Dz2kO1mtG\u0024bEUM();
    return (IComparable) (coord / num);
  }

  protected double ToCoordinate(
    IComparable dataValue,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB axis)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> coordCalc = axis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd direction = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection : dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection;
    double canvasMeasurement = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? canvas.\u0023\u003Dzu2ObQ3hMALTN() : canvas.\u0023\u003Dz2kO1mtG\u0024bEUM();
    return this.ToCoordinate(dataValue, canvasMeasurement, coordCalc, direction);
  }

  protected virtual Point ToCoordinates(
    IComparable xDataValue,
    IComparable yDataValue,
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCoordCalc,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCoordCalc)
  {
    double coordinate1 = this.GetCoordinate(xDataValue, canvas, xCoordCalc);
    double coordinate2 = this.GetCoordinate(yDataValue, canvas, yCoordCalc);
    if (xCoordCalc != null && !xCoordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K())
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref coordinate1, ref coordinate2);
    return new Point(coordinate1, coordinate2);
  }

  private double GetCoordinate(
    IComparable dataValue,
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> coordCalc)
  {
    if (coordCalc == null)
      return 0.0;
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd direction = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection : dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection;
    double canvasMeasurement = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? canvas.\u0023\u003Dzu2ObQ3hMALTN() : canvas.\u0023\u003Dz2kO1mtG\u0024bEUM();
    return this.ToCoordinate(dataValue, canvasMeasurement, coordCalc, direction);
  }

  protected virtual double ToCoordinate(
    IComparable dataValue,
    double canvasMeasurement,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> coordCalc,
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd direction)
  {
    if (dataValue == null)
      return double.NaN;
    if (this.CoordinateMode == AnnotationCoordinateMode.Relative || this.CoordinateMode == AnnotationCoordinateMode.RelativeX && direction == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection || this.CoordinateMode == AnnotationCoordinateMode.RelativeY && direction == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection)
      return dataValue.\u0023\u003Dzb9UCYbo\u003D() * canvasMeasurement;
    return coordCalc.\u0023\u003DzcNWwm_gWa4NJdtQNJ1Cl\u0024zStdK0t() && dataValue is DateTime ? this.GetCategoryCoordinate(dataValue, coordCalc as \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D) : coordCalc.\u0023\u003DzhL6gsJw\u003D(dataValue.\u0023\u003Dzb9UCYbo\u003D());
  }

  private double GetCategoryCoordinate(
    IComparable dataValue,
    \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D categoryCalc)
  {
    int num1 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e((DateTime) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
    if (num1 != -1)
      return categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num1);
    int num2 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e((DateTime) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2);
    int num3 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e((DateTime) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3);
    DateTime dateTime = categoryCalc.\u0023\u003DzWZQlXHuDrnKc(num2);
    double num4 = categoryCalc.\u0023\u003DzWZQlXHuDrnKc(num3).\u0023\u003Dzb9UCYbo\u003D() - dateTime.\u0023\u003Dzb9UCYbo\u003D();
    double num5 = (dataValue.\u0023\u003Dzb9UCYbo\u003D() - dateTime.\u0023\u003Dzb9UCYbo\u003D()) / num4;
    double val1 = categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num2);
    double num6 = categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num3) - Math.Max(val1, 0.0);
    return num6 <= 0.0 ? -1.0 : val1 + num6 * num5;
  }

  protected \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D GetCoordinates(
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc)
  {
    Point coordinates1 = this.ToCoordinates(this.X1, this.Y1, canvas, xCalc, yCalc);
    Point coordinates2 = this.ToCoordinates(this.X2, this.Y2, canvas, xCalc, yCalc);
    double num = 0.0;
    return new \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D()
    {
      \u0023\u003DzS2_K6sVvd5IY = coordinates1.X,
      \u0023\u003Dz2J4l3QUGwZHE = coordinates1.Y,
      \u0023\u003Dz6aJoeqoqAzym = coordinates2.X,
      \u0023\u003DzWp13vlQiZCJc = coordinates2.Y,
      \u0023\u003DzjAUs6E8\u003D = this.YAxis != null ? this.YAxis.\u0023\u003Dz4wEfDhMr\u0024V6c() : num,
      \u0023\u003DzN0WZcqs\u003D = this.XAxis != null ? this.XAxis.\u0023\u003Dz4wEfDhMr\u0024V6c() : num
    };
  }

  public void MoveAnnotation(double horizOffset, double vertOffset)
  {
    if (!this.IsEditable)
      return;
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    if (this.XAxis == null || this.YAxis == null)
      return;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    using (this.SuspendUpdates())
      this.MoveAnnotationTo(this.GetCoordinates(canvas, xCalc, yCalc), horizOffset, vertOffset);
  }

  protected virtual (double fixedHOffset, double fixedVOffset) MoveAnnotationTo(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates,
    double horizOffset,
    double vertOffset)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    this.GetCurrentPlacementStrategy().\u0023\u003DzuPL3ELSPZybJ(coordinates, horizOffset, vertOffset, canvas);
    return (horizOffset, vertOffset);
  }

  protected bool IsCoordinateValid(double coord, double canvasMeasurement)
  {
    return coord >= 0.0 && coord < canvasMeasurement;
  }

  public Point[] GetBasePoints()
  {
    return this.GetBasePoints(this.GetCoordinates(this.GetCanvas(this.AnnotationCanvas), this.XAxis != null ? this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null, this.YAxis != null ? this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null));
  }

  protected virtual Point[] GetBasePoints(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates)
  {
    return this.GetCurrentPlacementStrategy().\u0023\u003DzfJgp916l7LbX(coordinates);
  }

  public void SetBasePoint(Point newPoint, int index)
  {
    if (!this.IsEditable)
      return;
    using (this.SuspendUpdates())
      this.GetCurrentPlacementStrategy().\u0023\u003DzzNonn\u0024lG8ddm(newPoint, index);
  }

  protected virtual void SetBasePoint(
    Point newPoint,
    int index,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB xAxis,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB yAxis)
  {
    IComparable[] comparableArray = this.FromCoordinates(newPoint);
    DependencyProperty x;
    DependencyProperty y;
    this.GetPropertiesFromIndex(index, out x, out y);
    this.SetCurrentValue(x, (object) comparableArray[0]);
    this.SetCurrentValue(y, (object) comparableArray[1]);
  }

  protected virtual void GetPropertiesFromIndex(
    int index,
    out DependencyProperty x,
    out DependencyProperty y)
  {
    x = AnnotationBase.X1Property;
    y = AnnotationBase.Y1Property;
    switch (index)
    {
      case 0:
        x = AnnotationBase.X1Property;
        y = AnnotationBase.Y1Property;
        break;
      case 1:
        x = AnnotationBase.X2Property;
        y = AnnotationBase.Y1Property;
        break;
      case 2:
        x = AnnotationBase.X2Property;
        y = AnnotationBase.Y2Property;
        break;
      case 3:
        x = AnnotationBase.X1Property;
        y = AnnotationBase.Y2Property;
        break;
    }
  }

  protected virtual void HandleIsEditable()
  {
    Cursor cursor = this.IsEditable ? this.GetSelectedCursor() : Cursors.Arrow;
    this.SetCurrentValue(FrameworkElement.CursorProperty, (object) cursor);
    this.PerformFocusOnInputTextArea();
  }

  protected Canvas GetAdornerLayer()
  {
    return this.ParentSurface == null ? (Canvas) null : this.ParentSurface.\u0023\u003DzjEjGZ817bm4EOO82ig\u003D\u003D();
  }

  protected virtual void AddAdorners(Canvas adornerLayer)
  {
    \u0023\u003DzMoarvB9xsq04k1\u0024YZzaCjHRJN5ZYNqo7rKyswR861f1C nqo7rKyswR861f1C = new \u0023\u003DzMoarvB9xsq04k1\u0024YZzaCjHRJN5ZYNqo7rKyswR861f1C((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) this);
    nqo7rKyswR861f1C.\u0023\u003Dzbw4WNWtere7d(adornerLayer);
    this._myAdorners.Add((\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x) nqo7rKyswR861f1C);
  }

  protected virtual void RemoveAdorners(Canvas adornerLayer)
  {
    this.GetUsedAdorners<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(adornerLayer).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(AnnotationBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D ?? (AnnotationBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D = new Action<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(AnnotationBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzmwmE1Rru1D_mRmp3V7zIKTM\u003D)));
  }

  public virtual Point TranslatePoint(
    Point point,
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z relativeTo)
  {
    return this.\u0023\u003DzaPPLsvfM_Sst(point, relativeTo);
  }

  public virtual bool IsPointWithinBounds(Point point) => this.\u0023\u003DzbOxVzAyGdX66(point);

  public virtual Rect GetBoundsRelativeTo(
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z relativeTo)
  {
    return this.\u0023\u003DzdC9whUui_gN\u0024(relativeTo);
  }

  internal bool TrySelectAnnotation()
  {
    return this.ParentSurface != null && this.ParentSurface.get_Annotations().\u0023\u003DzaO_rUKeW5Orq((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) this);
  }

  private void OnSelected()
  {
    EventHandler selected = this.Selected;
    if (selected == null)
      return;
    selected((object) this, EventArgs.Empty);
  }

  private void OnUnselected()
  {
    EventHandler unselected = this.Unselected;
    if (unselected == null)
      return;
    unselected((object) this, EventArgs.Empty);
  }

  private void OnIsHiddenChanged()
  {
    EventHandler isHiddenChanged = this.IsHiddenChanged;
    if (isHiddenChanged == null)
      return;
    isHiddenChanged((object) this, EventArgs.Empty);
  }

  internal void RaiseAnnotationDragStarted(bool isPrimaryDrag, bool isResize)
  {
    EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D> dragStarted = this.DragStarted;
    if (dragStarted == null)
      return;
    dragStarted((object) this, new \u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D(isPrimaryDrag, isResize));
  }

  internal void RaiseAnnotationDragEnded(bool isPrimaryDrag, bool isResize)
  {
    EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D> dragEnded = this.DragEnded;
    if (dragEnded == null)
      return;
    dragEnded((object) this, new \u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D(isPrimaryDrag, isResize));
  }

  internal void RaiseAnnotationDragging(
    double hOffset,
    double vOffset,
    bool isPrimaryDrag,
    bool isResize)
  {
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6HbbjLeMvfhhb2s2mR8\u003D> dragDelta = this.DragDelta;
    if (dragDelta == null)
      return;
    dragDelta((object) this, new \u0023\u003Dzro0Io1hfSw7LlH634iIk6HbbjLeMvfhhb2s2mR8\u003D(isPrimaryDrag, isResize, hOffset, vOffset));
  }

  private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase))
      return;
    Canvas adornerLayer = annotationBase.GetAdornerLayer();
    if ((bool) e.NewValue)
    {
      annotationBase.AddAdorners(adornerLayer);
      annotationBase.OnSelected();
    }
    else
    {
      annotationBase.ReleaseMouseCapture();
      annotationBase.RemoveAdorners(adornerLayer);
      annotationBase.PerformFocusOnInputTextArea();
      annotationBase.OnUnselected();
    }
  }

  private static void OnIsEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase))
      return;
    annotationBase.HandleIsEditable();
  }

  private static void OnIsHiddenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase) || !annotationBase.IsAttached)
      return;
    if ((bool) e.NewValue)
    {
      annotationBase.MakeInvisible();
    }
    else
    {
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = annotationBase.XAxis != null ? annotationBase.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = annotationBase.YAxis != null ? annotationBase.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = annotationBase.GetCanvas(annotationBase.AnnotationCanvas);
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates = new \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D();
      if (xCalc != null && yCalc != null && canvas != null)
        coordinates = annotationBase.GetCoordinates(canvas, xCalc, yCalc);
      annotationBase.MakeVisible(coordinates);
      annotationBase.Refresh();
    }
    annotationBase.OnIsHiddenChanged();
  }

  private static void OnYAxisIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase))
      return;
    annotationBase._yAxis = annotationBase.\u0023\u003Dz4uoxB8oLWxeL(annotationBase.YAxisId);
    annotationBase.OnYAxisIdChanged();
    ((AnnotationBase) d).Refresh();
  }

  private static void OnXAxisIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase))
      return;
    annotationBase._xAxis = annotationBase.\u0023\u003DzI0EiGDjWkH8S(annotationBase.XAxisId);
    annotationBase.OnXAxisIdChanged();
    ((AnnotationBase) d).Refresh();
  }

  public bool IsSuspended
  {
    get
    {
      return \u0023\u003DzuPRmIFUVJkGxyCE55JH19ZE5sEUdF5DXPLZ7U6Rxl0An.\u0023\u003DzY5RcByYV3P6y((\u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D) this);
    }
  }

  public \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote SuspendUpdates()
  {
    return (\u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote) new \u0023\u003DzuPRmIFUVJkGxyCE55JH19ZE5sEUdF5DXPLZ7U6Rxl0An((\u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D) this);
  }

  public void ResumeUpdates(
    \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote updateSuspender)
  {
    if (!updateSuspender.\u0023\u003DzuWdUDFWIQOsx())
      return;
    this.Refresh();
  }

  public void DecrementSuspend()
  {
  }

  public XmlSchema GetSchema() => (XmlSchema) null;

  public virtual void ReadXml(XmlReader reader)
  {
    if (reader.MoveToContent() != XmlNodeType.Element || !(reader.LocalName == ((object) this).GetType().Name))
      return;
    \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz4EJs3pc\u003D((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) this, reader);
  }

  public virtual void WriteXml(XmlWriter writer)
  {
    \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz7SZ\u0024Lrw\u003D((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) this, writer);
  }

  internal FrameworkElement RootElement => this.AnnotationRoot;

  internal bool IsDragging => this._isDragging;

  internal bool IsDraggingByUser => this._isPrimaryDrag;

  object \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eget_DataContext()
  {
    return this.DataContext;
  }

  void \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eset_DataContext(
    object value)
  {
    this.DataContext = value;
  }

  double \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EIHitTestable\u002Eget_ActualWidth()
  {
    return this.ActualWidth;
  }

  double \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EIHitTestable\u002Eget_ActualHeight()
  {
    return this.ActualHeight;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly AnnotationBase.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new AnnotationBase.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<\u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x> \u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D;
    public static Action<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D> \u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D;

    internal void \u0023\u003DzjRwQMrvkACc83ywGyp_nJAU\u003D(
      \u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x _param1)
    {
      _param1.\u0023\u003DzGDdLHa8\u003D();
    }

    internal void \u0023\u003DzmwmE1Rru1D_mRmp3V7zIKTM\u003D(
      \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D _param1)
    {
      _param1.\u0023\u003DzcNW2KR8\u003D();
    }
  }

  private sealed class \u0023\u003DzPDNKpHuRG7yCW5JW8_EOM4E\u003D
  {
    public AnnotationBase \u0023\u003DzRRvwDu67s9Rm;
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    internal void \u0023\u003DzIOaiByPzcFm0hgjIqRsFUKJzOnbT2YChDvFyp8X_Z2t0giSUkPjoyzwmi0T_()
    {
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      this.\u0023\u003DzRRvwDu67s9Rm._yAxis = __nonvirtual (this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dz4uoxB8oLWxeL(__nonvirtual (this.\u0023\u003DzRRvwDu67s9Rm.YAxisId)));
      this.\u0023\u003DzRRvwDu67s9Rm.OnYAxesCollectionChanged(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }

  public class \u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<\u0023\u003DzH9HNkng\u003D>(
    \u0023\u003DzH9HNkng\u003D _param1) : 
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P8\u00249VOyCNzxoI\u0024gA\u002447LO3X8<\u0023\u003DzH9HNkng\u003D>(_param1)
    where \u0023\u003DzH9HNkng\u003D : AnnotationBase
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      return (Point[]) null;
    }

    public override void \u0023\u003DzzNonn\u0024lG8ddm(Point _param1, int _param2)
    {
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(_param1, _param2, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param2)
    {
      return (_param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 && _param1.\u0023\u003Dz6aJoeqoqAzym < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.\u0023\u003Dzu2ObQ3hMALTN() && _param1.\u0023\u003Dz6aJoeqoqAzym > _param2.\u0023\u003Dzu2ObQ3hMALTN() || _param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 && _param1.\u0023\u003DzWp13vlQiZCJc < 0.0 ? 1 : (_param1.\u0023\u003Dz2J4l3QUGwZHE <= _param2.\u0023\u003Dz2kO1mtG\u0024bEUM() ? 0 : (_param1.\u0023\u003DzWp13vlQiZCJc > _param2.\u0023\u003Dz2kO1mtG\u0024bEUM() ? 1 : 0))) == 0;
    }

    public override void \u0023\u003DzuPL3ELSPZybJ(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      double _param2,
      double _param3,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param4)
    {
      this.\u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(_param1, ref _param2, ref _param3, _param4);
    }

    protected virtual void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      ref double _param2,
      ref double _param3,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param4)
    {
      double d1 = _param1.\u0023\u003DzS2_K6sVvd5IY + _param2;
      double d2 = _param1.\u0023\u003Dz6aJoeqoqAzym + _param2;
      double d3 = _param1.\u0023\u003Dz2J4l3QUGwZHE + _param3;
      double d4 = _param1.\u0023\u003DzWp13vlQiZCJc + _param3;
      if (!this.\u0023\u003DzpTsgWlwWfZwP(d1, _param4.\u0023\u003Dzu2ObQ3hMALTN()) || !this.\u0023\u003DzpTsgWlwWfZwP(d3, _param4.\u0023\u003Dz2kO1mtG\u0024bEUM()) || !this.\u0023\u003DzpTsgWlwWfZwP(d2, _param4.\u0023\u003Dzu2ObQ3hMALTN()) || !this.\u0023\u003DzpTsgWlwWfZwP(d4, _param4.\u0023\u003Dz2kO1mtG\u0024bEUM()))
      {
        double val1_1 = double.IsNaN(d1) ? 0.0 : d1;
        double val2_1 = double.IsNaN(d2) ? 0.0 : d2;
        double val1_2 = double.IsNaN(d3) ? 0.0 : d3;
        double val2_2 = double.IsNaN(d4) ? 0.0 : d4;
        if (Math.Max(val1_1, val2_1) < 0.0)
          _param2 -= Math.Max(val1_1, val2_1);
        if (Math.Min(val1_1, val2_1) > _param4.\u0023\u003Dzu2ObQ3hMALTN())
          _param2 -= Math.Min(val1_1, val2_1) - (_param4.\u0023\u003Dzu2ObQ3hMALTN() - 1.0);
        if (Math.Max(val1_2, val2_2) < 0.0)
          _param3 -= Math.Max(val1_2, val2_2);
        if (Math.Min(val1_2, val2_2) > _param4.\u0023\u003Dz2kO1mtG\u0024bEUM())
          _param3 -= Math.Min(val1_2, val2_2) - (_param4.\u0023\u003Dz2kO1mtG\u0024bEUM() - 1.0);
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY += _param2;
      _param1.\u0023\u003Dz6aJoeqoqAzym += _param2;
      _param1.\u0023\u003Dz2J4l3QUGwZHE += _param3;
      _param1.\u0023\u003DzWp13vlQiZCJc += _param3;
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE), 0, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc), 2, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
    }

    protected bool \u0023\u003DzpTsgWlwWfZwP(double _param1, double _param2)
    {
      return this.\u0023\u003Dz_iIh83yfe01U().IsCoordinateValid(_param1, _param2);
    }

    protected IComparable[] \u0023\u003DzvQ1aszE\u003D(double _param1, double _param2)
    {
      return this.\u0023\u003Dz_iIh83yfe01U().FromCoordinates(_param1, _param2);
    }
  }

  private sealed class \u0023\u003Dzffg\u0024YXBnGm7H\u0024PCqlQv7PCc\u003D
  {
    public AnnotationBase \u0023\u003DzRRvwDu67s9Rm;
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    internal void \u0023\u003Dz51OAXpgKHWL1SXLaCA5i5tpZSV6aNEG7pUicQk0yky3I6F_EHJGXbx7TM94H()
    {
      // ISSUE: explicit non-virtual call
      this.\u0023\u003DzRRvwDu67s9Rm._xAxis = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzI0EiGDjWkH8S(__nonvirtual (this.\u0023\u003DzRRvwDu67s9Rm.XAxisId));
      this.\u0023\u003DzRRvwDu67s9Rm.OnXAxesCollectionChanged(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }

  internal class \u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<\u0023\u003DzH9HNkng\u003D> : 
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P8\u00249VOyCNzxoI\u0024gA\u002447LO3X8<\u0023\u003DzH9HNkng\u003D>
    where \u0023\u003DzH9HNkng\u003D : AnnotationBase
  {
    private readonly \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D \u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J;

    public \u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D(\u0023\u003DzH9HNkng\u003D _param1)
      : base(_param1)
    {
      this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J = _param1.Services.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>().\u0023\u003DzhGnS3f5TTzO8();
    }

    protected \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D \u0023\u003Dzy9phceyLTfoo()
    {
      return this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J;
    }

    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      return (Point[]) null;
    }

    public override void \u0023\u003DzzNonn\u0024lG8ddm(Point _param1, int _param2)
    {
      Point newPoint = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(newPoint, _param2, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param2)
    {
      Size size = this.\u0023\u003DzIr6xoc_4P2lw(_param2);
      return this.\u0023\u003DzRe9EEbV7q4ey(_param1, size);
    }

    private Size \u0023\u003DzIr6xoc_4P2lw(
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param1)
    {
      return new Size(360.0, \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(_param1.\u0023\u003Dzu2ObQ3hMALTN(), _param1.\u0023\u003Dz2kO1mtG\u0024bEUM()));
    }

    protected virtual bool \u0023\u003DzRe9EEbV7q4ey(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      Size _param2)
    {
      return (_param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 && _param1.\u0023\u003Dz6aJoeqoqAzym < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.Width && _param1.\u0023\u003Dz6aJoeqoqAzym > _param2.Width || _param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 && _param1.\u0023\u003DzWp13vlQiZCJc < 0.0 ? 1 : (_param1.\u0023\u003Dz2J4l3QUGwZHE <= _param2.Height ? 0 : (_param1.\u0023\u003DzWp13vlQiZCJc > _param2.Height ? 1 : 0))) == 0;
    }

    public override void \u0023\u003DzuPL3ELSPZybJ(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      double _param2,
      double _param3,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param4)
    {
      Tuple<Point, Point> tuple = this.\u0023\u003DzQDA5x2uuH9m3(_param1, _param2, _param3);
      Size size = this.\u0023\u003DzIr6xoc_4P2lw(_param4);
      this.\u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(_param1, tuple.Item1, tuple.Item2, size);
    }

    protected virtual Tuple<Point, Point> \u0023\u003DzQDA5x2uuH9m3(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      double _param2,
      double _param3)
    {
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D nnpojF4sCpkA8pp0g = this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
      return new Tuple<Point, Point>(this.\u0023\u003DztHc_NcH8us53(new Point(nnpojF4sCpkA8pp0g.\u0023\u003DzS2_K6sVvd5IY, nnpojF4sCpkA8pp0g.\u0023\u003Dz2J4l3QUGwZHE), _param2, _param3), this.\u0023\u003DztHc_NcH8us53(new Point(nnpojF4sCpkA8pp0g.\u0023\u003Dz6aJoeqoqAzym, nnpojF4sCpkA8pp0g.\u0023\u003DzWp13vlQiZCJc), _param2, _param3));
    }

    protected virtual \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D \u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      Point point1 = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003DzsTReN_n58EEf(new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE));
      Point point2 = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003DzsTReN_n58EEf(new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc));
      return new \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D()
      {
        \u0023\u003DzS2_K6sVvd5IY = point1.X,
        \u0023\u003Dz2J4l3QUGwZHE = point1.Y,
        \u0023\u003Dz6aJoeqoqAzym = point2.X,
        \u0023\u003DzWp13vlQiZCJc = point2.Y
      };
    }

    private Point \u0023\u003DztHc_NcH8us53(Point _param1, double _param2, double _param3)
    {
      Point point1 = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
      _param1.X += _param2;
      _param1.Y += _param3;
      Point point2 = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
      return new Point(point2.X - point1.X, point2.Y - point1.Y);
    }

    protected virtual void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      Point _param2,
      Point _param3,
      Size _param4)
    {
      double d1 = _param1.\u0023\u003DzS2_K6sVvd5IY + _param2.X;
      double d2 = _param1.\u0023\u003Dz6aJoeqoqAzym + _param3.X;
      double d3 = _param1.\u0023\u003Dz2J4l3QUGwZHE + _param2.Y;
      double d4 = _param1.\u0023\u003DzWp13vlQiZCJc + _param3.Y;
      if (!this.\u0023\u003DzpTsgWlwWfZwP(d1, _param4.Width) || !this.\u0023\u003DzpTsgWlwWfZwP(d3, _param4.Height) || !this.\u0023\u003DzpTsgWlwWfZwP(d2, _param4.Width) || !this.\u0023\u003DzpTsgWlwWfZwP(d4, _param4.Height))
      {
        double val1_1 = double.IsNaN(d1) ? 0.0 : d1;
        double val2_1 = double.IsNaN(d2) ? 0.0 : d2;
        double val1_2 = double.IsNaN(d3) ? 0.0 : d3;
        double val2_2 = double.IsNaN(d4) ? 0.0 : d4;
        if (Math.Max(val1_1, val2_1) < 0.0)
        {
          double num = Math.Max(val1_1, val2_1);
          _param2.X -= num;
          _param3.X -= num;
        }
        if (Math.Min(val1_1, val2_1) > _param4.Width)
        {
          double num = Math.Min(val1_1, val2_1) - (_param4.Width - 1.0);
          _param2.X -= num;
          _param3.X -= num;
        }
        if (Math.Max(val1_2, val2_2) < 0.0)
        {
          double num = Math.Max(val1_2, val2_2);
          _param2.Y -= num;
          _param3.Y -= num;
        }
        if (Math.Min(val1_2, val2_2) > _param4.Height)
        {
          double num = Math.Min(val1_2, val2_2) - (_param4.Height - 1.0);
          _param2.Y -= num;
          _param3.Y -= num;
        }
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY += _param2.X;
      _param1.\u0023\u003Dz6aJoeqoqAzym += _param3.X;
      _param1.\u0023\u003Dz2J4l3QUGwZHE += _param2.Y;
      _param1.\u0023\u003DzWp13vlQiZCJc += _param3.Y;
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE), 0, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc), 2, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
    }

    protected bool \u0023\u003DzpTsgWlwWfZwP(double _param1, double _param2)
    {
      return this.\u0023\u003Dz_iIh83yfe01U().IsCoordinateValid(_param1, _param2);
    }

    protected IComparable[] \u0023\u003DzvQ1aszE\u003D(double _param1, double _param2)
    {
      return this.\u0023\u003Dz_iIh83yfe01U().FromCoordinates(_param1, _param2);
    }
  }
}
