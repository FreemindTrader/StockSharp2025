// Decompiled with JetBrains decompiler
// Type: -.ChartModifierBase
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace \u002D;

internal abstract class ChartModifierBase : 
  ApiElementBase,
  IXmlSerializable,
  INotifyPropertyChanged,
  IChartModifier,
  IChartModifierBase,
  IReceiveMouseEvents 
{
  
  public static readonly DependencyProperty \u0023\u003DzkiTDvI_Iu6kL = DependencyProperty.Register(nameof (ReceiveHandledEvents), typeof (bool), typeof (ChartModifierBase), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzSLZmDSF5TsAu = DependencyProperty.RegisterAttached(nameof (IsEnabled), typeof (bool), typeof (ChartModifierBase), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartModifierBase.\u0023\u003DzMFI5J30qjapk)));
  
  public static readonly DependencyProperty \u0023\u003DzdfZ5r82v29C_ = DependencyProperty.Register(nameof (ExecuteOn), typeof (ExecuteOn), typeof (ChartModifierBase), new PropertyMetadata((object) ExecuteOn.MouseLeftButton));
  
  public static readonly DependencyProperty \u0023\u003DzuabWX4LvY\u0024qS = DependencyProperty.Register(nameof (MouseModifier), typeof (MouseModifier), typeof (ChartModifierBase), new PropertyMetadata((object) MouseModifier.None));
  
  private ISciChartSurface \u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D;
  
  private static Dictionary<MouseButtons, ExecuteOn> \u0023\u003DzKlT17PlFLc73;
  
  private IServiceContainer _serviceContainer;
  
  private \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw422C01wi00GXG6siWuShyyh \u0023\u003DzNJAhntg15gAM;
  
  private \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw422C01wi00GXG6siWuShyyh \u0023\u003DzYGSxD7qIfp16puTVlw\u003D\u003D;
  
  private bool \u0023\u003DzgBeNpsZ8d\u0024uI;
  
  private bool \u0023\u003DzIp\u0024m94zfWIDB;
  
  private bool \u0023\u003DzO10ORkEELARh;
  
  private string \u0023\u003Dzp3bx1gz2PPH2VRct1w\u003D\u003D;
  
  private string \u0023\u003DzIOF\u0024pM4fvIzk4nVUyHULhOk\u003D;
  
  private bool \u0023\u003Dz2PHh1f4YKRbtmShpA\u0024Kym4A1qOUa;

  protected ChartModifierBase()
  {
    this.\u0023\u003DzM\u0024iyCHU\u003D();
    this.DataContextChanged += ChartModifierBase.SomeClass34343383.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D ?? (ChartModifierBase.SomeClass34343383.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D = new DependencyPropertyChangedEventHandler(ChartModifierBase.SomeClass34343383.SomeMethond0343.\u0023\u003DzGHSYN_3KV4orJNfNmStVzRA\u003D));
    this.\u0023\u003Dz3aV1iPcGyuhxDI4kpQEmSBg\u003D(true);
  }

  public virtual bool \u0023\u003Dzo7mdr1Y1DFNe()
  {
    return this.IsEnabled && this.IsAttached && this.ModifierSurface != null && this.ParentSurface != null && this.ParentSurface.IsVisible;
  }

  public override void OnAttached() => this.\u0023\u003DzBialNpcCEeGSuz54SUkHEKI\u003D();

  private void \u0023\u003DzBialNpcCEeGSuz54SUkHEKI\u003D()
  {
    if (!this.\u0023\u003Dz8aGSsI2FehxdngUQaLiJHus\u003D() && this.XAxis != null && this.XAxis.get_IsPolarAxis())
      throw new NotSupportedException(((object) this).GetType().Name + " is not supported by PolarXAxis.");
  }

  public override void OnDetached()
  {
  }

  public new bool IsEnabled
  {
    get
    {
      return (bool) this.GetValue(ChartModifierBase.\u0023\u003DzSLZmDSF5TsAu);
    }
    set
    {
      this.SetValue(ChartModifierBase.\u0023\u003DzSLZmDSF5TsAu, (object) value);
    }
  }

  public ExecuteOn ExecuteOn
  {
    get
    {
      return (ExecuteOn) this.GetValue(ChartModifierBase.\u0023\u003DzdfZ5r82v29C_);
    }
    set
    {
      this.SetValue(ChartModifierBase.\u0023\u003DzdfZ5r82v29C_, (object) value);
    }
  }

  public MouseModifier MouseModifier
  {
    get
    {
      return (MouseModifier) this.GetValue(ChartModifierBase.\u0023\u003DzuabWX4LvY\u0024qS);
    }
    set
    {
      this.SetValue(ChartModifierBase.\u0023\u003DzuabWX4LvY\u0024qS, (object) value);
    }
  }

  public bool ReceiveHandledEvents
  {
    get
    {
      return (bool) this.GetValue(ChartModifierBase.\u0023\u003DzkiTDvI_Iu6kL);
    }
    set
    {
      this.SetValue(ChartModifierBase.\u0023\u003DzkiTDvI_Iu6kL, (object) value);
    }
  }

  public string ModifierName
  {
    get => this.\u0023\u003Dzp3bx1gz2PPH2VRct1w\u003D\u003D;
    protected set => this.\u0023\u003Dzp3bx1gz2PPH2VRct1w\u003D\u003D = value;
  }

  public bool IsMouseLeftButtonDown => this.\u0023\u003DzgBeNpsZ8d\u0024uI;

  public bool IsMouseMiddleButtonDown => this.\u0023\u003DzIp\u0024m94zfWIDB;

  public bool IsMouseRightButtonDown => this.\u0023\u003DzO10ORkEELARh;

  public string MouseEventGroup
  {
    get => this.\u0023\u003DzIOF\u0024pM4fvIzk4nVUyHULhOk\u003D;
    set => this.\u0023\u003DzIOF\u0024pM4fvIzk4nVUyHULhOk\u003D = value;
  }

  public virtual void \u0023\u003Dz5y8F1YNwkhnW(
    ModifierMouseArgs _param1)
  {
  }

  public virtual void \u0023\u003DzsXEfcKpqchyX(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DzgBeNpsZ8d\u0024uI = _param1.MouseButtons() == (MouseButtons) 1;
    this.\u0023\u003DzO10ORkEELARh = _param1.MouseButtons() == (MouseButtons) 4;
    this.\u0023\u003DzIp\u0024m94zfWIDB = _param1.MouseButtons() == (MouseButtons) 2;
  }

  public virtual void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
  }

  public virtual void OnModifierMouseUp(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DzgBeNpsZ8d\u0024uI = e.MouseButtons() != (MouseButtons) 1 && this.\u0023\u003DzgBeNpsZ8d\u0024uI;
    this.\u0023\u003DzO10ORkEELARh = e.MouseButtons() != (MouseButtons) 4 && this.\u0023\u003DzO10ORkEELARh;
    this.\u0023\u003DzIp\u0024m94zfWIDB = _param1.MouseButtons() != (MouseButtons) 2 && this.\u0023\u003DzIp\u0024m94zfWIDB;
  }

  public virtual void \u0023\u003DzQTINWhMByBmJ(
    ModifierMouseArgs _param1)
  {
  }

  public virtual void \u0023\u003Dz0yya794Z8OaI(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
  }

  public virtual void \u0023\u003DzpmQpuKvOtHIk(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
  }

  public virtual void \u0023\u003DzsSwjrBzrsGPJ(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
  }

  public override IServiceContainer Services
  {
    get => this._serviceContainer;
    set
    {
      if (this._serviceContainer != null)
      {
        if (this.\u0023\u003DzNJAhntg15gAM != null)
          this._serviceContainer.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().RemovePropertyEvents<\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt>(this.\u0023\u003DzNJAhntg15gAM);
        if (this.\u0023\u003DzYGSxD7qIfp16puTVlw\u003D\u003D != null)
          this._serviceContainer.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().RemovePropertyEvents<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D>(this.\u0023\u003DzYGSxD7qIfp16puTVlw\u003D\u003D);
      }
      this._serviceContainer = value;
      if (this._serviceContainer == null)
        return;
      this.\u0023\u003DzNJAhntg15gAM = this._serviceContainer.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().AddPropertyEvents<\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt>(new Action<\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt>(this.\u0023\u003DzY1JcdEJm3Ryc), true);
      this.\u0023\u003DzYGSxD7qIfp16puTVlw\u003D\u003D = this._serviceContainer.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().AddPropertyEvents<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D>(new Action<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D>(this.\u0023\u003DzuzARK4K7AoZvKMXK2g\u003D\u003D), true);
    }
  }

  public override ISciChartSurface ParentSurface
  {
    get => this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D is SciChartSurface z3jEwApUub0ZdjNsDq1)
      {
        z3jEwApUub0ZdjNsDq1.MouseLeave -= new MouseEventHandler(this.\u0023\u003DziUPejVp2MwiMm3b8mjptA7lYtdq1);
        z3jEwApUub0ZdjNsDq1.MouseEnter -= new MouseEventHandler(this.\u0023\u003Dz1RWF4qCdwtL7iWLRBKs1AWljs19M);
        z3jEwApUub0ZdjNsDq1.SelectedRenderableSeries.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz0tZIEaEc845fryC8ai9UjY4Y\u00240TT);
      }
      this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D = value;
      if (this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D is SciChartSurface z3jEwApUub0ZdjNsDq2)
      {
        z3jEwApUub0ZdjNsDq2.MouseLeave += new MouseEventHandler(this.\u0023\u003DziUPejVp2MwiMm3b8mjptA7lYtdq1);
        z3jEwApUub0ZdjNsDq2.MouseEnter += new MouseEventHandler(this.\u0023\u003Dz1RWF4qCdwtL7iWLRBKs1AWljs19M);
        z3jEwApUub0ZdjNsDq2.SelectedRenderableSeries.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz0tZIEaEc845fryC8ai9UjY4Y\u00240TT);
      }
      this.\u0023\u003Dz15moWio\u003D(nameof (ParentSurface));
    }
  }

  internal bool \u0023\u003Dz8aGSsI2FehxdngUQaLiJHus\u003D()
  {
    return this.\u0023\u003Dz2PHh1f4YKRbtmShpA\u0024Kym4A1qOUa;
  }

  internal void \u0023\u003Dz3aV1iPcGyuhxDI4kpQEmSBg\u003D(bool _param1)
  {
    this.\u0023\u003Dz2PHh1f4YKRbtmShpA\u0024Kym4A1qOUa = _param1;
  }

  public Point \u0023\u003DzOaYrn8YGTeR7(
    Point _param1,
    IHitTestable _param2)
  {
    return this.\u0023\u003Dzwc4Gzka23TGB().TranslatePoint(_param1, _param2);
  }

  public bool \u0023\u003DzbOxVzAyGdX66(
    Point _param1,
    IHitTestable _param2)
  {
    return _param2.IsPointWithinBounds(_param1);
  }

  public Point \u0023\u003DzjR6X_\u0024l_TULw(
    Point _param1,
    IHitTestable _param2)
  {
    throw new NotImplementedException();
  }

  protected virtual void \u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
  }

  protected virtual void \u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
  }

  protected virtual void \u0023\u003Dzok6jmLaiH5ai(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
  }

  public virtual void \u0023\u003DzuzARK4K7AoZvKMXK2g\u003D\u003D(
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D _param1)
  {
  }

  public virtual void \u0023\u003DzY1JcdEJm3Ryc(
    \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt _param1)
  {
  }

  protected void \u0023\u003DzNqFH9\u00244\u003D(Cursor _param1)
  {
    if (this.ParentSurface == null)
      return;
    this.ParentSurface.\u0023\u003DzqFIyyIbnwGLq(_param1);
  }

  protected virtual void \u0023\u003DzUq0D2jBe2UY\u0024(
    object _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
  }

  protected virtual void OnIsEnabledChanged()
  {
  }

  public virtual void OnMasterMouseLeave(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DzAX8eceNZBdIhNPOcDA\u003D\u003D();
  }

  protected virtual void \u0023\u003DzAX8eceNZBdIhNPOcDA\u003D\u003D()
  {
  }

  protected virtual void OnParentSurfaceMouseEnter()
  {
  }

  protected virtual void \u0023\u003Dz\u0024523lOKnSPCb(
    IEnumerable<IRenderableSeries> _param1,
    IEnumerable<IRenderableSeries> _param2)
  {
  }

  protected bool \u0023\u003DzK46Xo3q3PoYX(
    MouseButtons _param1,
    ExecuteOn _param2)
  {
    MouseModifier nijb7bojGmWzLupPhdCYfw = \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk();
    if (ChartModifierBase.\u0023\u003DzKlT17PlFLc73 == null)
      ChartModifierBase.\u0023\u003DzKlT17PlFLc73 = new Dictionary<MouseButtons, ExecuteOn>()
      {
        {
          (MouseButtons) 0,
          ExecuteOn.MouseMove
        },
        {
          (MouseButtons) 1,
          ExecuteOn.MouseLeftButton
        },
        {
          (MouseButtons) 2,
          ExecuteOn.MouseMiddleButton
        },
        {
          (MouseButtons) 4,
          ExecuteOn.MouseRightButton
        }
      };
    return ChartModifierBase.\u0023\u003DzKlT17PlFLc73.ContainsKey(_param1) && ChartModifierBase.\u0023\u003DzKlT17PlFLc73[_param1] == _param2 && (this.MouseModifier & nijb7bojGmWzLupPhdCYfw) == nijb7bojGmWzLupPhdCYfw;
  }

  public XmlSchema GetSchema() => (XmlSchema) null;

  public virtual void ReadXml(XmlReader _param1)
  {
    if (_param1.MoveToContent() != XmlNodeType.Element)
      return;
    \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz4EJs3pc\u003D(this, _param1);
  }

  public virtual void WriteXml(XmlWriter _param1)
  {
    \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz7SZ\u0024Lrw\u003D(this, _param1);
  }

  public virtual void \u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D()
  {
  }

  void IChartModifier.\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcaoefme7BYIyXg0AoqnWibP2uTotBtUl7kE\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003DzBialNpcCEeGSuz54SUkHEKI\u003D();
    this.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(_param1, _param2);
  }

  void IChartModifier.\u0023\u003DzUib3SzczDtLU7txM4YiSeONHMnrsSmO444UA3Zf\u0024bQeg2fDdqpMCk_s\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(_param1, _param2);
  }

  void IChartModifier.\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3814whdkylj5s7cHSDnhPRAnbbEqKg\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003Dzok6jmLaiH5ai(_param1, _param2);
  }

  private static void \u0023\u003DzOgeRiHKIiMn2(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((ChartModifierBase) _param0).\u0023\u003DzUq0D2jBe2UY\u0024((object) _param0, _param1);
  }

  private static void \u0023\u003DzMFI5J30qjapk(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((ChartModifierBase) _param0).OnIsEnabledChanged();
  }

  private void \u0023\u003DzM\u0024iyCHU\u003D()
  {
    string str = ((object) this).GetType().ToString();
    int num = str.LastIndexOf('.');
    this.ModifierName = str.Substring(num + 1);
  }

  private void \u0023\u003DziUPejVp2MwiMm3b8mjptA7lYtdq1(object _param1, MouseEventArgs _param2)
  {
    this.\u0023\u003DzAX8eceNZBdIhNPOcDA\u003D\u003D();
  }

  private void \u0023\u003Dz1RWF4qCdwtL7iWLRBKs1AWljs19M(object _param1, MouseEventArgs _param2)
  {
    this.OnParentSurfaceMouseEnter();
  }

  private void \u0023\u003Dz0tZIEaEc845fryC8ai9UjY4Y\u00240TT(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003Dz\u0024523lOKnSPCb(_param2.OldItems != null ? _param2.OldItems.Cast<IRenderableSeries>() : (IEnumerable<IRenderableSeries>) null, _param2.NewItems != null ? _param2.NewItems.Cast<IRenderableSeries>() : (IEnumerable<IRenderableSeries>) null);
  }

  object IChartModifierBase.\u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPd7JGkEE5\u0024rDskVZayhiSyyWH46tAA\u003D\u003D()
  {
    return this.DataContext;
  }

  void IChartModifierBase.\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmgVxjV2V5TzCKPaK0aNTVee8QjFeXw\u003D\u003D(
    object _param1)
  {
    this.DataContext = _param1;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly ChartModifierBase.SomeClass34343383 SomeMethond0343 = new ChartModifierBase.SomeClass34343383();
    public static DependencyPropertyChangedEventHandler \u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D;

    internal void \u0023\u003DzGHSYN_3KV4orJNfNmStVzRA\u003D(
      object _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ChartModifierBase.\u0023\u003DzOgeRiHKIiMn2((DependencyObject) _param1, _param2);
    }
  }
}
