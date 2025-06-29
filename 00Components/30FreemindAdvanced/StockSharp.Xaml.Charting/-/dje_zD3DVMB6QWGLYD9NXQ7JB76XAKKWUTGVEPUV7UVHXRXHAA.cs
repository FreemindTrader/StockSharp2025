// Decompiled with JetBrains decompiler
// Type: -.dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
namespace StockSharp.Xaml.Charting;

internal abstract class dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd : 
  \u0023\u003DzHHCBm9UpsKz28K2k\u002432Cv_vXeGtDJsjTomycKYo\u003D,
  \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D,
  \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSrfQ\u0024fvhBTfuaBKLOTsYHeMg,
  \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpOgj\u0024HEwAG4ZlfwSGT7i2APW,
  INotifyPropertyChanged,
  IXmlSerializable
{
  
  public static readonly DependencyProperty \u0023\u003DzkiTDvI_Iu6kL = DependencyProperty.Register(XXX.SSS(-539427989), typeof (bool), typeof (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzSLZmDSF5TsAu = DependencyProperty.RegisterAttached(XXX.SSS(-539428020), typeof (bool), typeof (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzMFI5J30qjapk)));
  
  public static readonly DependencyProperty \u0023\u003DzdfZ5r82v29C_ = DependencyProperty.Register(XXX.SSS(-539428004), typeof (dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd), typeof (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd), new PropertyMetadata((object) dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseLeftButton));
  
  public static readonly DependencyProperty \u0023\u003DzuabWX4LvY\u0024qS = DependencyProperty.Register(XXX.SSS(-539428052), typeof (\u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D), typeof (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd), new PropertyMetadata((object) \u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D.None));
  
  private \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D \u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D;
  
  private static Dictionary<\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D, dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd> \u0023\u003DzKlT17PlFLc73;
  
  private \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003DzBd9ykz0\u003D;
  
  private \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw422C01wi00GXG6siWuShyyh \u0023\u003DzNJAhntg15gAM;
  
  private \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw422C01wi00GXG6siWuShyyh \u0023\u003DzYGSxD7qIfp16puTVlw\u003D\u003D;
  
  private bool \u0023\u003DzgBeNpsZ8d\u0024uI;
  
  private bool \u0023\u003DzIp\u0024m94zfWIDB;
  
  private bool \u0023\u003DzO10ORkEELARh;
  
  private string \u0023\u003Dzp3bx1gz2PPH2VRct1w\u003D\u003D;
  
  private string \u0023\u003DzIOF\u0024pM4fvIzk4nVUyHULhOk\u003D;
  
  private bool \u0023\u003Dz2PHh1f4YKRbtmShpA\u0024Kym4A1qOUa;

  protected dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd()
  {
    this.\u0023\u003DzM\u0024iyCHU\u003D();
    this.DataContextChanged += dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D ?? (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D = new DependencyPropertyChangedEventHandler(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzGHSYN_3KV4orJNfNmStVzRA\u003D));
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
      throw new NotSupportedException(((object) this).GetType().Name + XXX.SSS(-539428040));
  }

  public override void OnDetached()
  {
  }

  public new bool IsEnabled
  {
    get
    {
      return (bool) this.GetValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzSLZmDSF5TsAu);
    }
    set
    {
      this.SetValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzSLZmDSF5TsAu, (object) value);
    }
  }

  public dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd ExecuteOn
  {
    get
    {
      return (dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd) this.GetValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzdfZ5r82v29C_);
    }
    set
    {
      this.SetValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzdfZ5r82v29C_, (object) value);
    }
  }

  public \u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D MouseModifier
  {
    get
    {
      return (\u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D) this.GetValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzuabWX4LvY\u0024qS);
    }
    set
    {
      this.SetValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzuabWX4LvY\u0024qS, (object) value);
    }
  }

  public bool ReceiveHandledEvents
  {
    get
    {
      return (bool) this.GetValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzkiTDvI_Iu6kL);
    }
    set
    {
      this.SetValue(dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzkiTDvI_Iu6kL, (object) value);
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
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
  }

  public virtual void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DzgBeNpsZ8d\u0024uI = _param1.\u0023\u003DzwuSh61ofE2mr() == (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 1;
    this.\u0023\u003DzO10ORkEELARh = _param1.\u0023\u003DzwuSh61ofE2mr() == (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 4;
    this.\u0023\u003DzIp\u0024m94zfWIDB = _param1.\u0023\u003DzwuSh61ofE2mr() == (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 2;
  }

  public virtual void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
  }

  public virtual void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DzgBeNpsZ8d\u0024uI = _param1.\u0023\u003DzwuSh61ofE2mr() != (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 1 && this.\u0023\u003DzgBeNpsZ8d\u0024uI;
    this.\u0023\u003DzO10ORkEELARh = _param1.\u0023\u003DzwuSh61ofE2mr() != (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 4 && this.\u0023\u003DzO10ORkEELARh;
    this.\u0023\u003DzIp\u0024m94zfWIDB = _param1.\u0023\u003DzwuSh61ofE2mr() != (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 2 && this.\u0023\u003DzIp\u0024m94zfWIDB;
  }

  public virtual void \u0023\u003DzQTINWhMByBmJ(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
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

  public override \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D Services
  {
    get => this.\u0023\u003DzBd9ykz0\u003D;
    set
    {
      if (this.\u0023\u003DzBd9ykz0\u003D != null)
      {
        if (this.\u0023\u003DzNJAhntg15gAM != null)
          this.\u0023\u003DzBd9ykz0\u003D.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzfttffOE\u003D<\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt>(this.\u0023\u003DzNJAhntg15gAM);
        if (this.\u0023\u003DzYGSxD7qIfp16puTVlw\u003D\u003D != null)
          this.\u0023\u003DzBd9ykz0\u003D.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzfttffOE\u003D<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D>(this.\u0023\u003DzYGSxD7qIfp16puTVlw\u003D\u003D);
      }
      this.\u0023\u003DzBd9ykz0\u003D = value;
      if (this.\u0023\u003DzBd9ykz0\u003D == null)
        return;
      this.\u0023\u003DzNJAhntg15gAM = this.\u0023\u003DzBd9ykz0\u003D.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzZcbqdpE\u003D<\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt>(new Action<\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt>(this.\u0023\u003DzY1JcdEJm3Ryc), true);
      this.\u0023\u003DzYGSxD7qIfp16puTVlw\u003D\u003D = this.\u0023\u003DzBd9ykz0\u003D.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzZcbqdpE\u003D<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D>(new Action<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D>(this.\u0023\u003DzuzARK4K7AoZvKMXK2g\u003D\u003D), true);
    }
  }

  public override \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D ParentSurface
  {
    get => this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd z3jEwApUub0ZdjNsDq1)
      {
        z3jEwApUub0ZdjNsDq1.MouseLeave -= new MouseEventHandler(this.\u0023\u003DziUPejVp2MwiMm3b8mjptA7lYtdq1);
        z3jEwApUub0ZdjNsDq1.MouseEnter -= new MouseEventHandler(this.\u0023\u003Dz1RWF4qCdwtL7iWLRBKs1AWljs19M);
        z3jEwApUub0ZdjNsDq1.SelectedRenderableSeries.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz0tZIEaEc845fryC8ai9UjY4Y\u00240TT);
      }
      this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D = value;
      if (this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd z3jEwApUub0ZdjNsDq2)
      {
        z3jEwApUub0ZdjNsDq2.MouseLeave += new MouseEventHandler(this.\u0023\u003DziUPejVp2MwiMm3b8mjptA7lYtdq1);
        z3jEwApUub0ZdjNsDq2.MouseEnter += new MouseEventHandler(this.\u0023\u003Dz1RWF4qCdwtL7iWLRBKs1AWljs19M);
        z3jEwApUub0ZdjNsDq2.SelectedRenderableSeries.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz0tZIEaEc845fryC8ai9UjY4Y\u00240TT);
      }
      this.\u0023\u003Dz15moWio\u003D(XXX.SSS(-539428462));
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
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param2)
  {
    return this.\u0023\u003Dzwc4Gzka23TGB().TranslatePoint(_param1, _param2);
  }

  public bool \u0023\u003DzbOxVzAyGdX66(
    Point _param1,
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param2)
  {
    return _param2.IsPointWithinBounds(_param1);
  }

  public Point \u0023\u003DzjR6X_\u0024l_TULw(
    Point _param1,
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param2)
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

  protected virtual void \u0023\u003DzCM2UQyuakisf()
  {
  }

  public virtual void \u0023\u003Dz3RBcoKAPKSIX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DzAX8eceNZBdIhNPOcDA\u003D\u003D();
  }

  protected virtual void \u0023\u003DzAX8eceNZBdIhNPOcDA\u003D\u003D()
  {
  }

  protected virtual void \u0023\u003DzRhCP\u0024yGKAqZwVyg1vA\u003D\u003D()
  {
  }

  protected virtual void \u0023\u003Dz\u0024523lOKnSPCb(
    IEnumerable<IRenderableSeries> _param1,
    IEnumerable<IRenderableSeries> _param2)
  {
  }

  protected bool \u0023\u003DzK46Xo3q3PoYX(
    \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D _param1,
    dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd _param2)
  {
    \u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D nijb7bojGmWzLupPhdCYfw = \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk();
    if (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzKlT17PlFLc73 == null)
      dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzKlT17PlFLc73 = new Dictionary<\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D, dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd>()
      {
        {
          (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 0,
          dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseMove
        },
        {
          (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 1,
          dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseLeftButton
        },
        {
          (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 2,
          dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseMiddleButton
        },
        {
          (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 4,
          dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseRightButton
        }
      };
    return dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzKlT17PlFLc73.ContainsKey(_param1) && dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzKlT17PlFLc73[_param1] == _param2 && (this.MouseModifier & nijb7bojGmWzLupPhdCYfw) == nijb7bojGmWzLupPhdCYfw;
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

  void \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D.\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcaoefme7BYIyXg0AoqnWibP2uTotBtUl7kE\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003DzBialNpcCEeGSuz54SUkHEKI\u003D();
    this.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(_param1, _param2);
  }

  void \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D.\u0023\u003DzUib3SzczDtLU7txM4YiSeONHMnrsSmO444UA3Zf\u0024bQeg2fDdqpMCk_s\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(_param1, _param2);
  }

  void \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D.\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3814whdkylj5s7cHSDnhPRAnbbEqKg\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003Dzok6jmLaiH5ai(_param1, _param2);
  }

  private static void \u0023\u003DzOgeRiHKIiMn2(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd) _param0).\u0023\u003DzUq0D2jBe2UY\u0024((object) _param0, _param1);
  }

  private static void \u0023\u003DzMFI5J30qjapk(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd) _param0).\u0023\u003DzCM2UQyuakisf();
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
    this.\u0023\u003DzRhCP\u0024yGKAqZwVyg1vA\u003D\u003D();
  }

  private void \u0023\u003Dz0tZIEaEc845fryC8ai9UjY4Y\u00240TT(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    this.\u0023\u003Dz\u0024523lOKnSPCb(_param2.OldItems != null ? _param2.OldItems.Cast<IRenderableSeries>() : (IEnumerable<IRenderableSeries>) null, _param2.NewItems != null ? _param2.NewItems.Cast<IRenderableSeries>() : (IEnumerable<IRenderableSeries>) null);
  }

  object \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSrfQ\u0024fvhBTfuaBKLOTsYHeMg.\u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPd7JGkEE5\u0024rDskVZayhiSyyWH46tAA\u003D\u003D()
  {
    return this.DataContext;
  }

  void \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSrfQ\u0024fvhBTfuaBKLOTsYHeMg.\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmgVxjV2V5TzCKPaK0aNTVee8QjFeXw\u003D\u003D(
    object _param1)
  {
    this.DataContext = _param1;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static DependencyPropertyChangedEventHandler \u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D;

    internal void \u0023\u003DzGHSYN_3KV4orJNfNmStVzRA\u003D(
      object _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd.\u0023\u003DzOgeRiHKIiMn2((DependencyObject) _param1, _param2);
    }
  }
}
