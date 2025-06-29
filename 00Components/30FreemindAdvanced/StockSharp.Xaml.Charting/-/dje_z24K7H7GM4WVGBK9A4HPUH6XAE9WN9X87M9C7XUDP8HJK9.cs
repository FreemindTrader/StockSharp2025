// Decompiled with JetBrains decompiler
// Type: -.dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

#nullable disable
namespace StockSharp.Xaml.Charting;

[System.Windows.Markup.ContentProperty("ChildModifiers")]
internal sealed class dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd : 
  \u0023\u003DzJjGO25As8ZdWGUrb5oVqjcjGGOsTcy1hDBZUMEOXielyUpQaOw\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dzmf0Yyi0vkYuX = DependencyProperty.Register(XXX.SSS(-539429521), typeof (ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>), typeof (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003DzMGnM8Q7UmYY2)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Grid \u0023\u003DzS\u0024OTg_s\u003D = new Grid();

  public dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd()
    : this(Array.Empty<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>())
  {
  }

  public dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd(
    params \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D[] _param1)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, XXX.SSS(-539428190));
    for (int index = 0; index < _param1.Length; ++index)
    {
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D chhAr3Kksm46Uy2Zy = _param1[index];
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(16 /*0x10*/, 1);
      interpolatedStringHandler.AppendLiteral(XXX.SSS(-539428215));
      interpolatedStringHandler.AppendFormatted<int>(index);
      interpolatedStringHandler.AppendLiteral(XXX.SSS(-539428201));
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) chhAr3Kksm46Uy2Zy, stringAndClear);
    }
    this.Content = (object) this.\u0023\u003DzS\u0024OTg_s\u003D;
    this.SetCurrentValue(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dzmf0Yyi0vkYuX, (object) new ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>((IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) _param1));
  }

  public ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D> ChildModifiers
  {
    get
    {
      return (ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) this.GetValue(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dzmf0Yyi0vkYuX);
    }
    set
    {
      this.SetValue(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dzmf0Yyi0vkYuX, (object) value);
    }
  }

  public \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D this[string _param1]
  {
    get => this.\u0023\u003DzxRGD7v40sdKc(_param1);
  }

  public \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D this[int _param1]
  {
    get => this.ChildModifiers[_param1];
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this.\u0023\u003DznQRtYJ3QBq0M((IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) this.ChildModifiers);
  }

  public override void OnDetached()
  {
    base.OnDetached();
    this.\u0023\u003DzHs2cDCMHYNdU((IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) this.ChildModifiers);
  }

  private void \u0023\u003DznQRtYJ3QBq0M(
    IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D> _param1)
  {
    if (!this.IsAttached)
      return;
    _param1.\u0023\u003Dz30RSSSygABj_<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(this.\u0023\u003DzNnndxL_a\u00246L_));
  }

  private void \u0023\u003DzNnndxL_a\u00246L_(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    this.\u0023\u003DztqcafrnOGvWC(_param1);
    _param1.ParentSurface = this.ParentSurface;
    _param1.Services = this.Services;
    _param1.DataContext = this.DataContext;
    _param1.IsAttached = true;
    _param1.OnAttached();
  }

  private void \u0023\u003DztqcafrnOGvWC(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    this.\u0023\u003DzS\u0024OTg_s\u003D.\u0023\u003DzH0osWQkV_Y8_((object) _param1, -1);
  }

  private void \u0023\u003DzHs2cDCMHYNdU(
    IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D> _param1)
  {
    _param1.\u0023\u003Dz30RSSSygABj_<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(this.\u0023\u003Dz1zWQSIoTjtxX));
  }

  private void \u0023\u003Dz1zWQSIoTjtxX(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    this.\u0023\u003DzBLgi0dgELqPs(_param1);
    _param1.OnDetached();
    _param1.IsAttached = false;
    _param1.ParentSurface = (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) null;
    _param1.Services = (\u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D) null;
  }

  private void \u0023\u003DzBLgi0dgELqPs(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    this.\u0023\u003DzS\u0024OTg_s\u003D.\u0023\u003DziYdJ\u00246cCiBha((object) _param1);
  }

  protected override void \u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003DzWDeT\u0024CyLvtppRwsYCyoelU8\u003D lvtppRwsYcyoelU8 = new dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003DzWDeT\u0024CyLvtppRwsYCyoelU8\u003D();
    lvtppRwsYcyoelU8.\u0023\u003DzwM8aRUE\u003D = _param1;
    lvtppRwsYcyoelU8.\u0023\u003DzTi2kmf4\u003D = _param2;
    if (this.ChildModifiers == null)
      return;
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(lvtppRwsYcyoelU8.\u0023\u003DzDlxN3i6L\u0024bBQoRc5kCBdWWQ\u003D));
  }

  protected override void \u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D kiaDl76b0Nyu42rxJq = new dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D();
    kiaDl76b0Nyu42rxJq.\u0023\u003DzwM8aRUE\u003D = _param1;
    kiaDl76b0Nyu42rxJq.\u0023\u003DzTi2kmf4\u003D = _param2;
    if (this.ChildModifiers == null)
      return;
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(kiaDl76b0Nyu42rxJq.\u0023\u003Dz\u0024UXNSD6YJPnU9jHAajxBjjg\u003D));
  }

  protected override void \u0023\u003Dzok6jmLaiH5ai(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D v4vdZv8GtEzAmB0rzFq = new dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D();
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzwM8aRUE\u003D = _param1;
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzTi2kmf4\u003D = _param2;
    if (this.ChildModifiers == null)
      return;
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzSNWKb\u0024dBOE2a0JlR9g\u003D\u003D));
  }

  protected override void \u0023\u003DzCM2UQyuakisf()
  {
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(this.\u0023\u003DzHKDu2R4\u0024lFIb58vAYGbiULo\u003D));
  }

  public override void \u0023\u003Dz5y8F1YNwkhnW(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzgc4SCis4HjHyynhFkS9uoNrJlYYn)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  private void \u0023\u003DziTCeMLw\u003D(
    Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> _param1,
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
  {
    if (this.ChildModifiers == null)
      return;
    foreach (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D chhAr3Kksm46Uy2Zy in this.ChildModifiers.Where<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D = new Func<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, bool>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzxqzF6jTaY9lVLxHYg5yFLN0\u003D))))
    {
      if (chhAr3Kksm46Uy2Zy.ReceiveHandledEvents || !_param2.\u0023\u003Dz882B0y3Ue8fl())
        _param1(chhAr3Kksm46Uy2Zy, _param2);
    }
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz5krLWsvoXbq\u00248_PqIuQDSoaDq3r_)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz3eP\u0024F06Do2Yqw5911Phrm\u0024c\u003D)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzZSMUtK9zaXNAI8WwDGKJ3XDLzVv8)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003DzQTINWhMByBmJ(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzZ\u0024KRLvh2qpiaoFfyVxWBpgmJh\u0024ZU)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003Dz3RBcoKAPKSIX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzVYORBuxvOyw39HzBaLE20tNlRtHr)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003Dz0yya794Z8OaI(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzZ4U93QQjpDamF3o9CQ\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzZ4U93QQjpDamF3o9CQ\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzT6V9kc7Cfg601etpFH8cUK\u0024ZsXar)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003DzpmQpuKvOtHIk(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzbHvqWKMkbu6LA9m3wYK0Das\u003D)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003DzsSwjrBzrsGPJ(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D ?? (dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D = new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzdWZmB5uhdY2tAn7ZmG_SSuItHoU_)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public bool \u0023\u003DzG9Tzlq4\u003D(Type _param1)
  {
    return this.ChildModifiers.Any<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Func<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, bool>(new dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003DzupKvug2uTOcTyk52k2urQQw\u003D()
    {
      \u0023\u003DzCN8ZMQU\u003D = _param1
    }.\u0023\u003DzFNBE1DOFc1Eu9K4xOQ\u003D\u003D));
  }

  protected override void \u0023\u003DzUq0D2jBe2UY\u0024(
    object _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003DzOZhF6r12fDpdxPyyP9aETuM\u003D()
    {
      \u0023\u003Dz1BK01YA\u003D = _param2
    }.\u0023\u003Dzq9_03GhVOxezkFFX0w\u003D\u003D));
  }

  private \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D \u0023\u003DzxRGD7v40sdKc(
    string _param1)
  {
    return this.ChildModifiers.FirstOrDefault<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>(new Func<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, bool>(new dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D()
    {
      \u0023\u003DzM_vMnac\u003D = _param1
    }.\u0023\u003DzDUlFPKVc44hNw\u0024Hpo\u0024G0dFo\u003D));
  }

  public override void ReadXml(XmlReader _param1)
  {
    ObservableCollection<dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd> observableCollection = new ObservableCollection<dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd>();
    observableCollection.\u0023\u003Dz6_E5\u0024pE\u003D<dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd>(\u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DztbbHmR4\u003D(_param1));
    foreach (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D chhAr3Kksm46Uy2Zy in (Collection<dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd>) observableCollection)
      this.ChildModifiers.Add(chhAr3Kksm46Uy2Zy);
  }

  public override void WriteXml(XmlWriter _param1)
  {
    \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DzT642HR8\u003D(this.ChildModifiers.Cast<dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd>(), _param1);
  }

  public override void \u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D()
  {
    foreach (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D childModifier in (Collection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) this.ChildModifiers)
      childModifier.\u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D();
  }

  private static void \u0023\u003DzMGnM8Q7UmYY2(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd c7XudP8HjK96ZEjd))
      return;
    ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D> oldValue = _param1.OldValue as ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>;
    ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D> newValue = _param1.NewValue as ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>;
    if (oldValue != null)
    {
      c7XudP8HjK96ZEjd.\u0023\u003DzHs2cDCMHYNdU((IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) oldValue);
      oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(c7XudP8HjK96ZEjd.\u0023\u003DzJTJiOWG1\u0024tjr);
    }
    if (newValue == null)
      return;
    c7XudP8HjK96ZEjd.\u0023\u003DznQRtYJ3QBq0M((IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) newValue);
    newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(c7XudP8HjK96ZEjd.\u0023\u003DzJTJiOWG1\u0024tjr);
  }

  private void \u0023\u003DzJTJiOWG1\u0024tjr(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    if (_param2.NewItems != null)
      this.\u0023\u003DznQRtYJ3QBq0M(_param2.NewItems.Cast<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>());
    if (_param2.OldItems == null)
      return;
    this.\u0023\u003DzHs2cDCMHYNdU(_param2.OldItems.Cast<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>());
  }

  private void \u0023\u003DzHKDu2R4\u0024lFIb58vAYGbiULo\u003D(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    _param1.set_IsEnabled(this.IsEnabled);
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_z24K7H7GM4WVGBK9A4HPUH6XAE9WN9X87M9C7XUDP8HJK96Z_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;
    public static Func<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, bool> \u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D;
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D;
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D;
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D;
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D;
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D;
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzZ4U93QQjpDamF3o9CQ\u003D\u003D;
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D;
    public static Action<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D;

    internal void \u0023\u003Dzgc4SCis4HjHyynhFkS9uoNrJlYYn(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003Dz5y8F1YNwkhnW((\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY) _param2);
    }

    internal bool \u0023\u003DzxqzF6jTaY9lVLxHYg5yFLN0\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
    {
      return _param1.get_IsEnabled();
    }

    internal void \u0023\u003Dz5krLWsvoXbq\u00248_PqIuQDSoaDq3r_(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003DzsXEfcKpqchyX((\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY) _param2);
    }

    internal void \u0023\u003Dz3eP\u0024F06Do2Yqw5911Phrm\u0024c\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003Dz11bcnbUrALaA((\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY) _param2);
    }

    internal void \u0023\u003DzZSMUtK9zaXNAI8WwDGKJ3XDLzVv8(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003DzU3pYs4rYVmOS((\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY) _param2);
    }

    internal void \u0023\u003DzZ\u0024KRLvh2qpiaoFfyVxWBpgmJh\u0024ZU(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003DzQTINWhMByBmJ((\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY) _param2);
    }

    internal void \u0023\u003DzVYORBuxvOyw39HzBaLE20tNlRtHr(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003Dz3RBcoKAPKSIX((\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY) _param2);
    }

    internal void \u0023\u003DzT6V9kc7Cfg601etpFH8cUK\u0024ZsXar(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003Dz0yya794Z8OaI((\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf) _param2);
    }

    internal void \u0023\u003DzbHvqWKMkbu6LA9m3wYK0Das\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003DzpmQpuKvOtHIk((\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf) _param2);
    }

    internal void \u0023\u003DzdWZmB5uhdY2tAn7ZmG_SSuItHoU_(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003DzsSwjrBzrsGPJ((\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf) _param2);
    }
  }

  private sealed class \u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D
  {
    public string \u0023\u003DzM_vMnac\u003D;

    internal bool \u0023\u003DzDUlFPKVc44hNw\u0024Hpo\u0024G0dFo\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
    {
      return this.\u0023\u003DzM_vMnac\u003D == _param1.ModifierName;
    }
  }

  private sealed class \u0023\u003DzOZhF6r12fDpdxPyyP9aETuM\u003D
  {
    public DependencyPropertyChangedEventArgs \u0023\u003Dz1BK01YA\u003D;

    internal void \u0023\u003Dzq9_03GhVOxezkFFX0w\u003D\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
    {
      _param1.DataContext = this.\u0023\u003Dz1BK01YA\u003D.NewValue;
    }
  }

  private sealed class \u0023\u003DzWDeT\u0024CyLvtppRwsYCyoelU8\u003D
  {
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    internal void \u0023\u003DzDlxN3i6L\u0024bBQoRc5kCBdWWQ\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
    {
      _param1.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }

  private sealed class \u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D
  {
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    internal void \u0023\u003DzSNWKb\u0024dBOE2a0JlR9g\u003D\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
    {
      _param1.\u0023\u003Dzok6jmLaiH5ai(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }

  private sealed class \u0023\u003DzupKvug2uTOcTyk52k2urQQw\u003D
  {
    public Type \u0023\u003DzCN8ZMQU\u003D;

    internal bool \u0023\u003DzFNBE1DOFc1Eu9K4xOQ\u003D\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
    {
      return _param1.GetType() == this.\u0023\u003DzCN8ZMQU\u003D;
    }
  }

  private sealed class \u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D
  {
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    internal void \u0023\u003Dz\u0024UXNSD6YJPnU9jHAajxBjjg\u003D(
      \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
    {
      _param1.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }
}
