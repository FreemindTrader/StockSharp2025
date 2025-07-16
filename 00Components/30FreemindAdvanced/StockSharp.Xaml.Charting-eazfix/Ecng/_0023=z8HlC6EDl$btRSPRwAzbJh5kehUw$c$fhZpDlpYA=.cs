// Decompiled with JetBrains decompiler
// Type: #=z8HlC6EDl$btRSPRwAzbJh5kehUw$c$fhZpDlpYA=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
internal sealed class AnnotationCollection : 
  ObservableCollection<IAnnotation>,
  IXmlSerializable
{
  
  private IServiceContainer \u0023\u003Dzg8Ufa_EMXfJU;
  
  private ISciChartSurface _drawingSurface;
  
  private Delegate \u0023\u003Dzz9oqqeJpzNfoAe16JA\u003D\u003D;
  
  internal Action \u0023\u003Dzpc1\u0024iG76MVacUIlTZA\u003D\u003D = AnnotationCollection.SomeClass34343383.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D ?? (AnnotationCollection.SomeClass34343383.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D = new Action(AnnotationCollection.SomeClass34343383.SomeMethond0343.\u0023\u003DzP7tHDC_yuYcRLkKl1Q\u003D\u003D));

  public AnnotationCollection()
  {
    this.\u0023\u003DzFeNr2Uw\u003D();
  }

  public AnnotationCollection(
    IEnumerable<IAnnotation> _param1)
    : base(_param1)
  {
    this.\u0023\u003DzFeNr2Uw\u003D();
  }

  public ISciChartSurface ParentSurface
  {
    get => this._drawingSurface;
    set
    {
      ISciChartSurface zos6SmwAmxZ33 = this._drawingSurface;
      if (zos6SmwAmxZ33 != null)
      {
        this.\u0023\u003Dzg8Ufa_EMXfJU = (IServiceContainer) null;
        this.\u0023\u003DzIDbfqzYC9gDs(zos6SmwAmxZ33);
        this.\u0023\u003Dz30RSSSygABj_<IAnnotation>(new Action<IAnnotation>(this.\u0023\u003DzGbNKU1bxSZqc));
      }
      this._drawingSurface = value;
      if (this._drawingSurface == null)
        return;
      this.\u0023\u003Dz5ClIah4mXevT(this._drawingSurface);
      this.\u0023\u003Dzg8Ufa_EMXfJU = this._drawingSurface.Services();
      this.\u0023\u003Dz30RSSSygABj_<IAnnotation>(new Action<IAnnotation>(this.\u0023\u003DzigVBoK19B\u0024jW));
    }
  }

  public void \u0023\u003Dz5ClIah4mXevT(
    ISciChartSurface _param1)
  {
    this.\u0023\u003DzIDbfqzYC9gDs(_param1);
    if (!(_param1 is SciChartSurface elwvdvgwnmJ5AjuaEjd) || (object) this.\u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D() != null)
      return;
    this.\u0023\u003DztYN0Jd9msFURcCzgfw\u003D\u003D((Delegate) new MouseButtonEventHandler(this.\u0023\u003DzDdgdA_hwCt\u0024N8skFfw\u003D\u003D));
    elwvdvgwnmJ5AjuaEjd.AddHandler(UIElement.MouseLeftButtonDownEvent, this.\u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D(), true);
  }

  public void \u0023\u003DzIDbfqzYC9gDs(
    ISciChartSurface _param1)
  {
    if (!(_param1 is SciChartSurface elwvdvgwnmJ5AjuaEjd) || (object) this.\u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D() == null)
      return;
    elwvdvgwnmJ5AjuaEjd.RemoveHandler(UIElement.MouseLeftButtonDownEvent, this.\u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D());
    this.\u0023\u003DztYN0Jd9msFURcCzgfw\u003D\u003D((Delegate) null);
  }

  private void \u0023\u003DzDdgdA_hwCt\u0024N8skFfw\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    if (_param2.OriginalSource is Rectangle originalSource && RenderSurfaceBase.\u0023\u003Dz8UAGL9e3cOCn.Equals(originalSource.Tag))
    {
      this.\u0023\u003DzS__l_ifl50tN();
      _param2.Handled = true;
    }
    this.\u0023\u003Dzpc1\u0024iG76MVacUIlTZA\u003D\u003D();
  }

  public void \u0023\u003DzS__l_ifl50tN()
  {
    this.\u0023\u003Dz30RSSSygABj_<IAnnotation>(AnnotationCollection.SomeClass34343383.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D ?? (AnnotationCollection.SomeClass34343383.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D = new Action<IAnnotation>(AnnotationCollection.SomeClass34343383.SomeMethond0343.\u0023\u003Dzi1mryHm_34LaAslQH6vheC4\u003D)));
  }

  protected override void ClearItems()
  {
    foreach (IAnnotation hhh93Q0DqkV5Sv90k in (Collection<IAnnotation>) this)
      this.\u0023\u003DzGbNKU1bxSZqc(hhh93Q0DqkV5Sv90k);
    base.ClearItems();
  }

  internal void \u0023\u003Dzdb4OQr1\u0024A5Qg(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    if (this.ParentSurface == null)
      return;
    if (_param2.OldItems != null)
    {
      foreach (IAnnotation oldItem in (IEnumerable) _param2.OldItems)
        this.\u0023\u003DzGbNKU1bxSZqc(oldItem);
    }
    foreach (IAnnotation hhh93Q0DqkV5Sv90k in (Collection<IAnnotation>) this)
    {
      if (!hhh93Q0DqkV5Sv90k.IsAttached)
        this.\u0023\u003DzigVBoK19B\u0024jW(hhh93Q0DqkV5Sv90k);
      this.ParentSurface.InvalidateElement();
    }
  }

  private void \u0023\u003DzGbNKU1bxSZqc(
    IAnnotation _param1)
  {
    _param1.OnDetached();
    _param1.remove_DragStarted(new EventHandler<EventArgs>(this.\u0023\u003DzoVyKSXQ5yRzBL57Mlw\u003D\u003D));
    _param1.remove_DragEnded(new EventHandler<EventArgs>(this.\u0023\u003Dzb6pL05pO1ZwuBRQlrw\u003D\u003D));
    _param1.remove_DragDelta(new EventHandler<AnnotationDragDeltaEventArgs>(this.\u0023\u003Dz4Q47Joqo_tHsaan4\u0024A\u003D\u003D));
    _param1.Services = (IServiceContainer) null;
    _param1.ParentSurface = (ISciChartSurface) null;
    _param1.IsAttached = false;
  }

  private void \u0023\u003DzigVBoK19B\u0024jW(
    IAnnotation _param1)
  {
    _param1.Services = this.\u0023\u003Dzg8Ufa_EMXfJU;
    _param1.ParentSurface = this._drawingSurface;
    _param1.IsAttached = true;
    _param1.add_DragStarted(new EventHandler<EventArgs>(this.\u0023\u003DzoVyKSXQ5yRzBL57Mlw\u003D\u003D));
    _param1.add_DragEnded(new EventHandler<EventArgs>(this.\u0023\u003Dzb6pL05pO1ZwuBRQlrw\u003D\u003D));
    _param1.add_DragDelta(new EventHandler<AnnotationDragDeltaEventArgs>(this.\u0023\u003Dz4Q47Joqo_tHsaan4\u0024A\u003D\u003D));
    _param1.OnAttached();
  }

  private void \u0023\u003DzoVyKSXQ5yRzBL57Mlw\u003D\u003D(
    object _param1,
    EventArgs _param2)
  {
    AnnotationCollection.\u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D qws5VcJaSLcUcBty = new AnnotationCollection.\u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D();
    if (!_param2.BoolOne || _param2.BoolTwo)
      return;
    qws5VcJaSLcUcBty.\u0023\u003Dz2vouRgM\u003D = _param1 as IAnnotation;
    if (qws5VcJaSLcUcBty.\u0023\u003Dz2vouRgM\u003D == null)
      return;
    CollectionHelper.ForEach<IAnnotation>((IEnumerable<IAnnotation>) this.Where<IAnnotation>(new Func<IAnnotation, bool>(qws5VcJaSLcUcBty.\u0023\u003DzohWX52bl44U09jwJQRRBQCc\u003D)).ToArray<IAnnotation>(), AnnotationCollection.SomeClass34343383.\u0023\u003DzvUis2ceeooAA32k3YQ\u003D\u003D ?? (AnnotationCollection.SomeClass34343383.\u0023\u003DzvUis2ceeooAA32k3YQ\u003D\u003D = new Action<IAnnotation>(AnnotationCollection.SomeClass34343383.SomeMethond0343.\u0023\u003DzWLL21j0cIbtvfyx1n7j_L3p5Tj3r)));
  }

  private void \u0023\u003Dzb6pL05pO1ZwuBRQlrw\u003D\u003D(
    object _param1,
    EventArgs _param2)
  {
    AnnotationCollection.\u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D uuovZyvmyeAqC2gog8 = new AnnotationCollection.\u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D();
    if (!_param2.BoolOne || _param2.BoolTwo)
      return;
    uuovZyvmyeAqC2gog8.\u0023\u003Dz2vouRgM\u003D = _param1 as IAnnotation;
    if (uuovZyvmyeAqC2gog8.\u0023\u003Dz2vouRgM\u003D == null)
      return;
    CollectionHelper.ForEach<IAnnotation>((IEnumerable<IAnnotation>) this.Where<IAnnotation>(new Func<IAnnotation, bool>(uuovZyvmyeAqC2gog8.\u0023\u003Dzxqad0J2WdWQXWHM7O24\u0024gAw\u003D)).ToArray<IAnnotation>(), AnnotationCollection.SomeClass34343383.\u0023\u003DzUsoE\u00244KLHwOEYi4PCA\u003D\u003D ?? (AnnotationCollection.SomeClass34343383.\u0023\u003DzUsoE\u00244KLHwOEYi4PCA\u003D\u003D = new Action<IAnnotation>(AnnotationCollection.SomeClass34343383.SomeMethond0343.\u0023\u003DznHZdoyT\u0024EEaTkfWvQEMXoEih2Miy)));
  }

  private void \u0023\u003Dz4Q47Joqo_tHsaan4\u0024A\u003D\u003D(
    object _param1,
    AnnotationDragDeltaEventArgs _param2)
  {
    AnnotationCollection.\u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D qeYqJf714K2eMyR9I = new AnnotationCollection.\u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D();
    qeYqJf714K2eMyR9I.\u0023\u003Dz1BK01YA\u003D = _param2;
    if (!qeYqJf714K2eMyR9I.\u0023\u003Dz1BK01YA\u003D.BoolOne || qeYqJf714K2eMyR9I.\u0023\u003Dz1BK01YA\u003D.BoolTwo)
      return;
    qeYqJf714K2eMyR9I.\u0023\u003Dz2vouRgM\u003D = _param1 as IAnnotation;
    if (qeYqJf714K2eMyR9I.\u0023\u003Dz2vouRgM\u003D == null)
      return;
    CollectionHelper.ForEach<IAnnotation>((IEnumerable<IAnnotation>) this.Where<IAnnotation>(new Func<IAnnotation, bool>(qeYqJf714K2eMyR9I.\u0023\u003DzauLrVnHgfyr3yQ0vEFCCM5c\u003D)).ToArray<IAnnotation>(), new Action<IAnnotation>(qeYqJf714K2eMyR9I.\u0023\u003Dz_njip1a1ig5klNz6gwSkhfw\u003D));
  }

  public XmlSchema GetSchema() => (XmlSchema) null;

  public void ReadXml(XmlReader _param1)
  {
    IUpdateSuspender fq05jnDg3bOrIrgCjote = (IUpdateSuspender) null;
    if (this.ParentSurface != null)
      fq05jnDg3bOrIrgCjote = this.ParentSurface.SuspendUpdates();
    this.\u0023\u003Dz6_E5\u0024pE\u003D<IAnnotation>(\u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DztbbHmR4\u003D(_param1));
    fq05jnDg3bOrIrgCjote?.Dispose();
  }

  public void WriteXml(XmlWriter _param1)
  {
    \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DzT642HR8\u003D((IEnumerable<IAnnotation>) this, _param1);
  }

  public void \u0023\u003Dzhwms24tGl3w4(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    foreach (IAnnotation hhh93Q0DqkV5Sv90k in (Collection<IAnnotation>) this)
    {
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = this.\u0023\u003DzvhansM3Nsebc9y\u0024i3fZ48OU\u003D(_param1.\u0023\u003DzSBrmxtNmmDcWbby6Gm0UVio\u003D, hhh93Q0DqkV5Sv90k, hhh93Q0DqkV5Sv90k.get_XAxisId(), true);
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = this.\u0023\u003DzvhansM3Nsebc9y\u0024i3fZ48OU\u003D(_param1.\u0023\u003DzPmZCkENGGLQws2poeTWSb6E\u003D, hhh93Q0DqkV5Sv90k, hhh93Q0DqkV5Sv90k.get_YAxisId(), false);
      hhh93Q0DqkV5Sv90k.Update(xkzemsMs5tGkouk5w1, xkzemsMs5tGkouk5w2);
      if (xkzemsMs5tGkouk5w1 == null)
        _param1.\u0023\u003Dz38sEjvRVtcBw().Add($"Could not draw an annotation of type {hhh93Q0DqkV5Sv90k.GetType()}. XAxis with Id == {hhh93Q0DqkV5Sv90k.get_XAxisId() ?? "NULL"} doesn't exist. Please ensure that the XAxisId property is set to a valid value.");
      if (xkzemsMs5tGkouk5w2 == null)
        _param1.\u0023\u003Dz38sEjvRVtcBw().Add($"Could not draw an annotation of type {hhh93Q0DqkV5Sv90k.GetType()}. YAxis with Id == {hhh93Q0DqkV5Sv90k.get_YAxisId() ?? "NULL"} doesn't exist. Please ensure that the YAxisId property is set to a valid value.");
    }
  }

  private \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003DzvhansM3Nsebc9y\u0024i3fZ48OU\u003D(
    IDictionary<string, \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>> _param1,
    IAnnotation _param2,
    string _param3,
    bool _param4)
  {
    if (_param3 == null)
      return (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w;
    return _param1.TryGetValue(_param3, out xkzemsMs5tGkouk5w) ? xkzemsMs5tGkouk5w : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
  }

  private void \u0023\u003DzFeNr2Uw\u003D()
  {
    this.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003Dzdb4OQr1\u0024A5Qg);
  }

  public bool \u0023\u003DzaO_rUKeW5Orq(
    IAnnotation _param1)
  {
    if (!_param1.get_IsEditable() || _param1.get_IsSelected() || !_param1.IsAttached)
      return false;
    if (\u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk() != MouseModifier.Shift || !this.\u0023\u003DzbjRX\u0024PUuuOXT(_param1))
      this.\u0023\u003Dz30RSSSygABj_<IAnnotation>(AnnotationCollection.SomeClass34343383.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D ?? (AnnotationCollection.SomeClass34343383.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D = new Action<IAnnotation>(AnnotationCollection.SomeClass34343383.SomeMethond0343.\u0023\u003DzVRk6KEtjy1UCzmcX5OcvFww\u003D)));
    this.\u0023\u003DzdkkvQAq7ppeh(_param1);
    return true;
  }

  private void \u0023\u003DzdkkvQAq7ppeh(
    IAnnotation _param1)
  {
    _param1.set_IsSelected(true);
  }

  private bool \u0023\u003DzbjRX\u0024PUuuOXT(
    IAnnotation _param1)
  {
    AnnotationCollection.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D lrrNtIjstOuVg4Rro = new AnnotationCollection.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D()
    {
      \u0023\u003Dz2vouRgM\u003D = _param1
    };
    lrrNtIjstOuVg4Rro.\u0023\u003DzAG7Twks\u003D = this.Where<IAnnotation>(new Func<IAnnotation, bool>(lrrNtIjstOuVg4Rro.\u0023\u003Dzv2EVeGQzvOiLkeQ6I1bYc_s\u003D)).Concat<IAnnotation>((IEnumerable<IAnnotation>) new IAnnotation[1]
    {
      lrrNtIjstOuVg4Rro.\u0023\u003Dz2vouRgM\u003D
    }).ToArray<IAnnotation>();
    return ((IEnumerable<IAnnotation>) lrrNtIjstOuVg4Rro.\u0023\u003DzAG7Twks\u003D).All<IAnnotation>(new Func<IAnnotation, bool>(lrrNtIjstOuVg4Rro.\u0023\u003DzfZsZRxWRaYq2NFL\u0024VzVo6bw\u003D));
  }

  internal Delegate \u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D()
  {
    return this.\u0023\u003Dzz9oqqeJpzNfoAe16JA\u003D\u003D;
  }

  internal void \u0023\u003DztYN0Jd9msFURcCzgfw\u003D\u003D(Delegate _param1)
  {
    this.\u0023\u003Dzz9oqqeJpzNfoAe16JA\u003D\u003D = _param1;
  }

  public void \u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    foreach (IAnnotation hhh93Q0DqkV5Sv90k in (IEnumerable<IAnnotation>) this.Items)
      hhh93Q0DqkV5Sv90k.OnXAxesCollectionChanged(_param1, _param2);
  }

  public void \u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    foreach (IAnnotation hhh93Q0DqkV5Sv90k in (IEnumerable<IAnnotation>) this.Items)
      hhh93Q0DqkV5Sv90k.OnYAxesCollectionChanged(_param1, _param2);
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly AnnotationCollection.SomeClass34343383 SomeMethond0343 = new AnnotationCollection.SomeClass34343383();
    public static Action \u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D;
    public static Action \u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D;
    public static Action<IAnnotation> \u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D;
    public static Action<IAnnotation> \u0023\u003DzvUis2ceeooAA32k3YQ\u003D\u003D;
    public static Action<IAnnotation> \u0023\u003DzUsoE\u00244KLHwOEYi4PCA\u003D\u003D;
    public static Action<IAnnotation> \u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D;

    internal void \u0023\u003DzP7tHDC_yuYcRLkKl1Q\u003D\u003D()
    {
    }

    internal void \u0023\u003Dzmu94CYIyRXEFBQQPPw\u003D\u003D()
    {
    }

    internal void \u0023\u003Dzi1mryHm_34LaAslQH6vheC4\u003D(
      IAnnotation _param1)
    {
      _param1.set_IsSelected(false);
    }

    internal void \u0023\u003DzWLL21j0cIbtvfyx1n7j_L3p5Tj3r(
      IAnnotation _param1)
    {
      _param1.StartDrag(false);
    }

    internal void \u0023\u003DznHZdoyT\u0024EEaTkfWvQEMXoEih2Miy(
      IAnnotation _param1)
    {
      _param1.EndDrag();
    }

    internal void \u0023\u003DzVRk6KEtjy1UCzmcX5OcvFww\u003D(
      IAnnotation _param1)
    {
      _param1.set_IsSelected(false);
    }
  }

  private sealed class \u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D
  {
    public IAnnotation \u0023\u003Dz2vouRgM\u003D;

    internal bool \u0023\u003Dzxqad0J2WdWQXWHM7O24\u0024gAw\u003D(
      IAnnotation _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D && _param1.get_IsSelected();
    }
  }

  private sealed class \u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D
  {
    public IAnnotation \u0023\u003Dz2vouRgM\u003D;
    public IAnnotation[] \u0023\u003DzAG7Twks\u003D;

    internal bool \u0023\u003Dzv2EVeGQzvOiLkeQ6I1bYc_s\u003D(
      IAnnotation _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D && _param1.get_IsSelected();
    }

    internal bool \u0023\u003DzfZsZRxWRaYq2NFL\u0024VzVo6bw\u003D(
      IAnnotation _param1)
    {
      return _param1.CanMultiSelect(this.\u0023\u003DzAG7Twks\u003D);
    }
  }

  private sealed class \u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D
  {
    public IAnnotation \u0023\u003Dz2vouRgM\u003D;

    internal bool \u0023\u003DzohWX52bl44U09jwJQRRBQCc\u003D(
      IAnnotation _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D && _param1.get_IsSelected();
    }
  }

  private sealed class \u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D
  {
    public IAnnotation \u0023\u003Dz2vouRgM\u003D;
    public AnnotationDragDeltaEventArgs \u0023\u003Dz1BK01YA\u003D;

    internal bool \u0023\u003DzauLrVnHgfyr3yQ0vEFCCM5c\u003D(
      IAnnotation _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D && _param1.get_IsSelected();
    }

    internal void \u0023\u003Dz_njip1a1ig5klNz6gwSkhfw\u003D(
      IAnnotation _param1)
    {
      _param1.Drag(this.\u0023\u003Dz1BK01YA\u003D.\u0023\u003Dz06oxr0QggddI(), this.\u0023\u003Dz1BK01YA\u003D.\u0023\u003Dz7Pq57plSf4mj());
    }
  }
}
