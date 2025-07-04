// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.OptimizerChart3D
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Editors;
using StockSharp.Algo.Statistics;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class OptimizerChart3D : UserControl, IComponentConnector
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly DispatcherTimer \u0023\u003DzI189dKeTiS2S;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action \u0023\u003DzTsL\u0024ygfSgp6i;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal ComboBoxEdit \u0023\u003Dz7qmq9F8\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal ComboBoxEdit \u0023\u003DzRBR31Bw\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal ComboBoxEdit \u0023\u003DzlFT\u00249og\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal Chart3D \u0023\u003DzO72kpz0\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public OptimizerChart3D()
  {
    this.InitializeComponent();
    this.\u0023\u003DzI189dKeTiS2S = new DispatcherTimer()
    {
      Interval = TimeSpan.FromMilliseconds(50.0)
    };
    this.\u0023\u003DzI189dKeTiS2S.Tick += new EventHandler(this.\u0023\u003DznBSvX0vDvPRU);
  }

  public IEnumerable<IChart3DParameter> XValues
  {
    get => (IEnumerable<IChart3DParameter>) this.\u0023\u003Dz7qmq9F8\u003D.ItemsSource;
    set => this.\u0023\u003Dz7qmq9F8\u003D.ItemsSource = (object) value;
  }

  public IEnumerable<IChart3DParameter> YValues
  {
    get => (IEnumerable<IChart3DParameter>) this.\u0023\u003DzRBR31Bw\u003D.ItemsSource;
    set => this.\u0023\u003DzRBR31Bw\u003D.ItemsSource = (object) value;
  }

  public IEnumerable<IStatisticParameter> ZValues
  {
    get => (IEnumerable<IStatisticParameter>) this.\u0023\u003DzlFT\u00249og\u003D.ItemsSource;
    set => this.\u0023\u003DzlFT\u00249og\u003D.ItemsSource = (object) value;
  }

  public IChart3DParameter X
  {
    get => (IChart3DParameter) this.\u0023\u003Dz7qmq9F8\u003D.EditValue;
    set => this.\u0023\u003Dz7qmq9F8\u003D.EditValue = (object) value;
  }

  public IChart3DParameter Y
  {
    get => (IChart3DParameter) this.\u0023\u003DzRBR31Bw\u003D.EditValue;
    set => this.\u0023\u003DzRBR31Bw\u003D.EditValue = (object) value;
  }

  public IStatisticParameter Z
  {
    get => (IStatisticParameter) this.\u0023\u003DzlFT\u00249og\u003D.EditValue;
    set => this.\u0023\u003DzlFT\u00249og\u003D.EditValue = (object) value;
  }

  public event Action Changed;

  public event Func<IChart3DParameter, IChart3DParameter, IStatisticParameter, IEnumerable<SeriesPoint3D>> DataRequested;

  private void ResetUI()
  {
    Action zlDokZjs = this.\u0023\u003DzlDOkZJs\u003D;
    if (zlDokZjs != null)
      zlDokZjs();
    this.RequestData();
  }

  public void Clear() => this.\u0023\u003DzO72kpz0\u003D.Clear();

  public void RequestData()
  {
    Func<IChart3DParameter, IChart3DParameter, IStatisticParameter, IEnumerable<SeriesPoint3D>> zMueAjYg = this.\u0023\u003DzMUEAjYg\u003D;
    if (zMueAjYg == null)
      return;
    IChart3DParameter x = this.X;
    IChart3DParameter y = this.Y;
    IStatisticParameter z = this.Z;
    if (x == null || y == null || z == null || x == y)
      return;
    this.\u0023\u003DzO72kpz0\u003D.Draw(zMueAjYg(x, y, z), x.Name, y.Name, z.DisplayName);
  }

  private void \u0023\u003DzACMpseHhV6bH0\u00243Z1w\u003D\u003D(
    object _param1,
    EditValueChangedEventArgs _param2)
  {
    this.ResetUI();
  }

  private void \u0023\u003DzYI42_\u0024dcJNJaMpFzUQ\u003D\u003D(
    object _param1,
    EditValueChangedEventArgs _param2)
  {
    this.ResetUI();
  }

  private void \u0023\u003DznBSvX0vDvPRU(object _param1, EventArgs _param2)
  {
    Action zTsLYgfSgp6i = this.\u0023\u003DzTsL\u0024ygfSgp6i;
    if (zTsLYgfSgp6i == null)
      return;
    zTsLYgfSgp6i();
  }

  private void \u0023\u003DzMPN5PP4\u003D(Action _param1)
  {
    this.\u0023\u003DzTsL\u0024ygfSgp6i = _param1;
    this.\u0023\u003DzI189dKeTiS2S.Start();
  }

  private void \u0023\u003DzLtUbtqbKLMeN()
  {
    this.\u0023\u003DzI189dKeTiS2S.Stop();
    this.\u0023\u003DzTsL\u0024ygfSgp6i = (Action) null;
  }

  private void \u0023\u003DzbfoeIw\u0024ncGcTiS9UVA\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzMPN5PP4\u003D(new Action(this.\u0023\u003Dzvsv0ozPERJCLH7Qlj2XfYJ33Rsql));
  }

  private void \u0023\u003Dz1vwY4y5d82yJmGjWtQ\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzMPN5PP4\u003D(new Action(this.\u0023\u003DzVbbb3fC3vb43rF4KU9TEZE6hLOjw));
  }

  private void \u0023\u003DznFRD22XRRSsfyu8Edw\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzMPN5PP4\u003D(new Action(this.\u0023\u003Dz05DsYeE9TArwoxHfhj9ipSrE0a0z));
  }

  private void \u0023\u003DzMqlOImglo\u00249PuJ5_BA\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzMPN5PP4\u003D(new Action(this.\u0023\u003DzT\u0024EjzudU2cP__EuN4ALEHHqZ7xSI));
  }

  private void \u0023\u003Dzhx1JXoyt1cvoJErWOw\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzMPN5PP4\u003D(new Action(this.\u0023\u003Dz2k7_5qrtOkXy0eZDlvgLHhnNDAga));
  }

  private void \u0023\u003DzalCIbNBwduPki9Pe6g\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzMPN5PP4\u003D(new Action(this.\u0023\u003DzSc1IksKKMcH8jWMVaFYUjseX8E\u0024W));
  }

  private void \u0023\u003Dzabia0hll75uA(object _param1, MouseButtonEventArgs _param2)
  {
    this.\u0023\u003DzLtUbtqbKLMeN();
  }

  private void \u0023\u003DzYaCFkfF9kh0u(Func<Vector3D, bool> _param1, double _param2)
  {
    OptimizerChart3D.\u0023\u003DzO3RMHvHjCCJ5FySBGtQj1mY\u003D hjCcJ5FySbGtQj1mY;
    hjCcJ5FySbGtQj1mY.\u0023\u003Dz_kvDiExkluUo = _param1;
    hjCcJ5FySbGtQj1mY.\u0023\u003DzYRvsjGVg1QYa = _param2;
    hjCcJ5FySbGtQj1mY.\u0023\u003DzRRvwDu67s9Rm = this;
    if (this.\u0023\u003DzO72kpz0\u003D.ActualContentTransform is Transform3DGroup contentTransform1)
    {
      bool flag = false;
      Transform3DGroup transform3Dgroup = new Transform3DGroup();
      foreach (Transform3D child in contentTransform1.Children)
      {
        if (child is RotateTransform3D rotateTransform3D && rotateTransform3D.Rotation is AxisAngleRotation3D rotation1)
        {
          if (hjCcJ5FySbGtQj1mY.\u0023\u003Dz_kvDiExkluUo(rotation1.Axis))
          {
            flag = true;
            AxisAngleRotation3D rotation = new AxisAngleRotation3D(rotation1.Axis, rotation1.Angle + hjCcJ5FySbGtQj1mY.\u0023\u003DzYRvsjGVg1QYa);
            transform3Dgroup.Children.Add((Transform3D) new RotateTransform3D((Rotation3D) rotation));
          }
          else
            transform3Dgroup.Children.Add((Transform3D) new RotateTransform3D((Rotation3D) rotation1.Clone()));
        }
        else
          transform3Dgroup.Children.Add(child.Clone());
      }
      if (flag)
        this.\u0023\u003DzO72kpz0\u003D.ContentTransform = (Transform3D) transform3Dgroup;
      else
        this.\u0023\u003DqAtrU_jTo9D0lec9ogBVYMlm__npgKE9u30Mc4eGhMQjQRc08XuZ5mQooBrD_B3Sy(contentTransform1.Value, ref hjCcJ5FySbGtQj1mY);
    }
    else
    {
      if (!(this.\u0023\u003DzO72kpz0\u003D.ActualContentTransform is MatrixTransform3D contentTransform))
        return;
      this.\u0023\u003DqAtrU_jTo9D0lec9ogBVYMlm__npgKE9u30Mc4eGhMQjQRc08XuZ5mQooBrD_B3Sy(contentTransform.Matrix, ref hjCcJ5FySbGtQj1mY);
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/optimizerchart3d.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  internal Delegate \u0023\u003DzciIj4U627yBM(Type _param1, string _param2)
  {
    return Delegate.CreateDelegate(_param1, (object) this, _param2);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.\u0023\u003Dz7qmq9F8\u003D = (ComboBoxEdit) target;
        this.\u0023\u003Dz7qmq9F8\u003D.EditValueChanged += new EditValueChangedEventHandler(this.\u0023\u003DzACMpseHhV6bH0\u00243Z1w\u003D\u003D);
        break;
      case 2:
        this.\u0023\u003DzRBR31Bw\u003D = (ComboBoxEdit) target;
        this.\u0023\u003DzRBR31Bw\u003D.EditValueChanged += new EditValueChangedEventHandler(this.\u0023\u003DzACMpseHhV6bH0\u00243Z1w\u003D\u003D);
        break;
      case 3:
        this.\u0023\u003DzlFT\u00249og\u003D = (ComboBoxEdit) target;
        this.\u0023\u003DzlFT\u00249og\u003D.EditValueChanged += new EditValueChangedEventHandler(this.\u0023\u003DzYI42_\u0024dcJNJaMpFzUQ\u003D\u003D);
        break;
      case 4:
        ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.\u0023\u003DzbfoeIw\u0024ncGcTiS9UVA\u003D\u003D);
        ((UIElement) target).PreviewMouseUp += new MouseButtonEventHandler(this.\u0023\u003Dzabia0hll75uA);
        break;
      case 5:
        ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.\u0023\u003Dz1vwY4y5d82yJmGjWtQ\u003D\u003D);
        ((UIElement) target).PreviewMouseUp += new MouseButtonEventHandler(this.\u0023\u003Dzabia0hll75uA);
        break;
      case 6:
        ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.\u0023\u003DznFRD22XRRSsfyu8Edw\u003D\u003D);
        ((UIElement) target).PreviewMouseUp += new MouseButtonEventHandler(this.\u0023\u003Dzabia0hll75uA);
        break;
      case 7:
        ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.\u0023\u003DzMqlOImglo\u00249PuJ5_BA\u003D\u003D);
        ((UIElement) target).PreviewMouseUp += new MouseButtonEventHandler(this.\u0023\u003Dzabia0hll75uA);
        break;
      case 8:
        ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.\u0023\u003Dzhx1JXoyt1cvoJErWOw\u003D\u003D);
        ((UIElement) target).PreviewMouseUp += new MouseButtonEventHandler(this.\u0023\u003Dzabia0hll75uA);
        break;
      case 9:
        ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.\u0023\u003DzalCIbNBwduPki9Pe6g\u003D\u003D);
        ((UIElement) target).PreviewMouseUp += new MouseButtonEventHandler(this.\u0023\u003Dzabia0hll75uA);
        break;
      case 10:
        this.\u0023\u003DzO72kpz0\u003D = (Chart3D) target;
        break;
      default:
        this.\u0023\u003DzQGCmQMjHdLKS = true;
        break;
    }
  }

  private void \u0023\u003Dzvsv0ozPERJCLH7Qlj2XfYJ33Rsql()
  {
    this.\u0023\u003DzYaCFkfF9kh0u(OptimizerChart3D.SomeClass34343383.\u0023\u003DzHf64i7sS_m0gAJCSlg\u003D\u003D ?? (OptimizerChart3D.SomeClass34343383.\u0023\u003DzHf64i7sS_m0gAJCSlg\u003D\u003D = new Func<Vector3D, bool>(OptimizerChart3D.SomeClass34343383.SomeMethond0343.\u0023\u003DzKNVw5MUKA0_gPqNER5gf5wMS6aQ9)), -1.0);
  }

  private void \u0023\u003DzVbbb3fC3vb43rF4KU9TEZE6hLOjw()
  {
    this.\u0023\u003DzYaCFkfF9kh0u(OptimizerChart3D.SomeClass34343383.\u0023\u003Dzkt5Z2jROBjPMkf1ugQ\u003D\u003D ?? (OptimizerChart3D.SomeClass34343383.\u0023\u003Dzkt5Z2jROBjPMkf1ugQ\u003D\u003D = new Func<Vector3D, bool>(OptimizerChart3D.SomeClass34343383.SomeMethond0343.\u0023\u003DzokVMWQ5tUWySX0ZAO5zdCjaws8Kc)), 1.0);
  }

  private void \u0023\u003Dz05DsYeE9TArwoxHfhj9ipSrE0a0z()
  {
    this.\u0023\u003DzYaCFkfF9kh0u(OptimizerChart3D.SomeClass34343383.Method05 ?? (OptimizerChart3D.SomeClass34343383.Method05 = new Func<Vector3D, bool>(OptimizerChart3D.SomeClass34343383.SomeMethond0343.\u0023\u003Dz5EW_l2nSTjbBKuuG93AZ7v1j7TCk)), -1.0);
  }

  private void \u0023\u003DzT\u0024EjzudU2cP__EuN4ALEHHqZ7xSI()
  {
    this.\u0023\u003DzYaCFkfF9kh0u(OptimizerChart3D.SomeClass34343383.\u0023\u003Dzgtt8BV5qq\u0024eBi5JtnA\u003D\u003D ?? (OptimizerChart3D.SomeClass34343383.\u0023\u003Dzgtt8BV5qq\u0024eBi5JtnA\u003D\u003D = new Func<Vector3D, bool>(OptimizerChart3D.SomeClass34343383.SomeMethond0343.\u0023\u003DzvMpUCbGMw4\u0024YcvOPecSimkLobYoM)), 1.0);
  }

  private void \u0023\u003Dz2k7_5qrtOkXy0eZDlvgLHhnNDAga()
  {
    this.\u0023\u003DzYaCFkfF9kh0u(OptimizerChart3D.SomeClass34343383.\u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D ?? (OptimizerChart3D.SomeClass34343383.\u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D = new Func<Vector3D, bool>(OptimizerChart3D.SomeClass34343383.SomeMethond0343.\u0023\u003DzRg6HPHFitFsySR6nRP24a_mwKHUh)), -1.0);
  }

  private void \u0023\u003DzSc1IksKKMcH8jWMVaFYUjseX8E\u0024W()
  {
    this.\u0023\u003DzYaCFkfF9kh0u(OptimizerChart3D.SomeClass34343383.\u0023\u003DzWh8zy81NxgujdHZLXg\u003D\u003D ?? (OptimizerChart3D.SomeClass34343383.\u0023\u003DzWh8zy81NxgujdHZLXg\u003D\u003D = new Func<Vector3D, bool>(OptimizerChart3D.SomeClass34343383.SomeMethond0343.\u0023\u003DzVXnwkZoqTYs1ICn7IXdhIyV4W7mI)), 1.0);
  }

  private void \u0023\u003DqAtrU_jTo9D0lec9ogBVYMlm__npgKE9u30Mc4eGhMQjQRc08XuZ5mQooBrD_B3Sy(
    Matrix3D _param1,
    ref OptimizerChart3D.\u0023\u003DzO3RMHvHjCCJ5FySBGtQj1mY\u003D _param2)
  {
    Vector3D axisOfRotation = new Vector3D((double) (_param2.\u0023\u003Dz_kvDiExkluUo(new Vector3D(1.0, 0.0, 0.0)) ? 1 : 0), (double) (_param2.\u0023\u003Dz_kvDiExkluUo(new Vector3D(0.0, 1.0, 0.0)) ? 1 : 0), (double) (_param2.\u0023\u003Dz_kvDiExkluUo(new Vector3D(0.0, 0.0, 1.0)) ? 1 : 0));
    _param1.Rotate(new Quaternion(axisOfRotation, _param2.\u0023\u003DzYRvsjGVg1QYa));
    this.\u0023\u003DzO72kpz0\u003D.ContentTransform = (Transform3D) new MatrixTransform3D(_param1);
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly OptimizerChart3D.SomeClass34343383 SomeMethond0343 = new OptimizerChart3D.SomeClass34343383();
    public static Func<Vector3D, bool> \u0023\u003DzHf64i7sS_m0gAJCSlg\u003D\u003D;
    public static Func<Vector3D, bool> \u0023\u003Dzkt5Z2jROBjPMkf1ugQ\u003D\u003D;
    public static Func<Vector3D, bool> Method05;
    public static Func<Vector3D, bool> \u0023\u003Dzgtt8BV5qq\u0024eBi5JtnA\u003D\u003D;
    public static Func<Vector3D, bool> \u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D;
    public static Func<Vector3D, bool> \u0023\u003DzWh8zy81NxgujdHZLXg\u003D\u003D;

    internal bool \u0023\u003DzKNVw5MUKA0_gPqNER5gf5wMS6aQ9(Vector3D _param1) => _param1.X == 1.0;

    internal bool \u0023\u003DzokVMWQ5tUWySX0ZAO5zdCjaws8Kc(Vector3D _param1) => _param1.X == 1.0;

    internal bool \u0023\u003Dz5EW_l2nSTjbBKuuG93AZ7v1j7TCk(Vector3D _param1) => _param1.Y == 1.0;

    internal bool \u0023\u003DzvMpUCbGMw4\u0024YcvOPecSimkLobYoM(Vector3D _param1)
    {
      return _param1.Y == 1.0;
    }

    internal bool \u0023\u003DzRg6HPHFitFsySR6nRP24a_mwKHUh(Vector3D _param1) => _param1.Z == 1.0;

    internal bool \u0023\u003DzVXnwkZoqTYs1ICn7IXdhIyV4W7mI(Vector3D _param1) => _param1.Z == 1.0;
  }

  [StructLayout(LayoutKind.Auto)]
  private struct \u0023\u003DzO3RMHvHjCCJ5FySBGtQj1mY\u003D
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public Func<Vector3D, bool> \u0023\u003Dz_kvDiExkluUo;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public double \u0023\u003DzYRvsjGVg1QYa;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public OptimizerChart3D \u0023\u003DzRRvwDu67s9Rm;
  }
}
