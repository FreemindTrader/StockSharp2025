// Decompiled with JetBrains decompiler
// Type: #=z8HlC6EDl$btRSPRwAzbJh5kehUw$c$fhZpDlpYA=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
internal sealed class \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D : 
  ObservableCollection<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>,
  IXmlSerializable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003Dzg8Ufa_EMXfJU;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D \u0023\u003Dzos6SMwAMXZ33;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Delegate \u0023\u003Dzz9oqqeJpzNfoAe16JA\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal Action \u0023\u003Dzpc1\u0024iG76MVacUIlTZA\u003D\u003D = \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D ?? (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D = new Action(\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzP7tHDC_yuYcRLkKl1Q\u003D\u003D));

  public \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D()
  {
    this.\u0023\u003DzFeNr2Uw\u003D();
  }

  public \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D(
    IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D> _param1)
    : base(_param1)
  {
    this.\u0023\u003DzFeNr2Uw\u003D();
  }

  public \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D ParentSurface
  {
    get => this.\u0023\u003Dzos6SMwAMXZ33;
    set
    {
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D zos6SmwAmxZ33 = this.\u0023\u003Dzos6SMwAMXZ33;
      if (zos6SmwAmxZ33 != null)
      {
        this.\u0023\u003Dzg8Ufa_EMXfJU = (\u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D) null;
        this.\u0023\u003DzIDbfqzYC9gDs(zos6SmwAmxZ33);
        this.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(this.\u0023\u003DzGbNKU1bxSZqc));
      }
      this.\u0023\u003Dzos6SMwAMXZ33 = value;
      if (this.\u0023\u003Dzos6SMwAMXZ33 == null)
        return;
      this.\u0023\u003Dz5ClIah4mXevT(this.\u0023\u003Dzos6SMwAMXZ33);
      this.\u0023\u003Dzg8Ufa_EMXfJU = this.\u0023\u003Dzos6SMwAMXZ33.\u0023\u003Dzu\u0024P3XgkcE7BC();
      this.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(this.\u0023\u003DzigVBoK19B\u0024jW));
    }
  }

  public void \u0023\u003Dz5ClIah4mXevT(
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D _param1)
  {
    this.\u0023\u003DzIDbfqzYC9gDs(_param1);
    if (!(_param1 is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd) || (object) this.\u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D() != null)
      return;
    this.\u0023\u003DztYN0Jd9msFURcCzgfw\u003D\u003D((Delegate) new MouseButtonEventHandler(this.\u0023\u003DzDdgdA_hwCt\u0024N8skFfw\u003D\u003D));
    elwvdvgwnmJ5AjuaEjd.AddHandler(UIElement.MouseLeftButtonDownEvent, this.\u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D(), true);
  }

  public void \u0023\u003DzIDbfqzYC9gDs(
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D _param1)
  {
    if (!(_param1 is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd) || (object) this.\u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D() == null)
      return;
    elwvdvgwnmJ5AjuaEjd.RemoveHandler(UIElement.MouseLeftButtonDownEvent, this.\u0023\u003Dz96bk__LBZFAg2I8HPQ\u003D\u003D());
    this.\u0023\u003DztYN0Jd9msFURcCzgfw\u003D\u003D((Delegate) null);
  }

  private void \u0023\u003DzDdgdA_hwCt\u0024N8skFfw\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    if (_param2.OriginalSource is Rectangle originalSource && dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003Dz8UAGL9e3cOCn.Equals(originalSource.Tag))
    {
      this.\u0023\u003DzS__l_ifl50tN();
      _param2.Handled = true;
    }
    this.\u0023\u003Dzpc1\u0024iG76MVacUIlTZA\u003D\u003D();
  }

  public void \u0023\u003DzS__l_ifl50tN()
  {
    this.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D ?? (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D = new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzi1mryHm_34LaAslQH6vheC4\u003D)));
  }

  protected override void ClearItems()
  {
    foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D hhh93Q0DqkV5Sv90k in (Collection<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this)
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
      foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D oldItem in (IEnumerable) _param2.OldItems)
        this.\u0023\u003DzGbNKU1bxSZqc(oldItem);
    }
    foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D hhh93Q0DqkV5Sv90k in (Collection<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this)
    {
      if (!hhh93Q0DqkV5Sv90k.IsAttached)
        this.\u0023\u003DzigVBoK19B\u0024jW(hhh93Q0DqkV5Sv90k);
      this.ParentSurface.\u0023\u003Dz5q8i9C4\u003D();
    }
  }

  private void \u0023\u003DzGbNKU1bxSZqc(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
  {
    _param1.OnDetached();
    _param1.remove_DragStarted(new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(this.\u0023\u003DzoVyKSXQ5yRzBL57Mlw\u003D\u003D));
    _param1.remove_DragEnded(new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(this.\u0023\u003Dzb6pL05pO1ZwuBRQlrw\u003D\u003D));
    _param1.remove_DragDelta(new EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6HbbjLeMvfhhb2s2mR8\u003D>(this.\u0023\u003Dz4Q47Joqo_tHsaan4\u0024A\u003D\u003D));
    _param1.Services = (\u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D) null;
    _param1.ParentSurface = (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) null;
    _param1.IsAttached = false;
  }

  private void \u0023\u003DzigVBoK19B\u0024jW(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
  {
    _param1.Services = this.\u0023\u003Dzg8Ufa_EMXfJU;
    _param1.ParentSurface = this.\u0023\u003Dzos6SMwAMXZ33;
    _param1.IsAttached = true;
    _param1.add_DragStarted(new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(this.\u0023\u003DzoVyKSXQ5yRzBL57Mlw\u003D\u003D));
    _param1.add_DragEnded(new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(this.\u0023\u003Dzb6pL05pO1ZwuBRQlrw\u003D\u003D));
    _param1.add_DragDelta(new EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6HbbjLeMvfhhb2s2mR8\u003D>(this.\u0023\u003Dz4Q47Joqo_tHsaan4\u0024A\u003D\u003D));
    _param1.OnAttached();
  }

  private void \u0023\u003DzoVyKSXQ5yRzBL57Mlw\u003D\u003D(
    object _param1,
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D _param2)
  {
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D qws5VcJaSLcUcBty = new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D();
    if (!_param2.\u0023\u003DzejwSg21X3OO6() || _param2.\u0023\u003DzjoPRpZLXrx8o())
      return;
    qws5VcJaSLcUcBty.\u0023\u003Dz2vouRgM\u003D = _param1 as \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D;
    if (qws5VcJaSLcUcBty.\u0023\u003Dz2vouRgM\u003D == null)
      return;
    CollectionHelper.ForEach<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this.Where<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(new Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D, bool>(qws5VcJaSLcUcBty.\u0023\u003DzohWX52bl44U09jwJQRRBQCc\u003D)).ToArray<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(), \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzvUis2ceeooAA32k3YQ\u003D\u003D ?? (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzvUis2ceeooAA32k3YQ\u003D\u003D = new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzWLL21j0cIbtvfyx1n7j_L3p5Tj3r)));
  }

  private void \u0023\u003Dzb6pL05pO1ZwuBRQlrw\u003D\u003D(
    object _param1,
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D _param2)
  {
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D uuovZyvmyeAqC2gog8 = new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D();
    if (!_param2.\u0023\u003DzejwSg21X3OO6() || _param2.\u0023\u003DzjoPRpZLXrx8o())
      return;
    uuovZyvmyeAqC2gog8.\u0023\u003Dz2vouRgM\u003D = _param1 as \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D;
    if (uuovZyvmyeAqC2gog8.\u0023\u003Dz2vouRgM\u003D == null)
      return;
    CollectionHelper.ForEach<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this.Where<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(new Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D, bool>(uuovZyvmyeAqC2gog8.\u0023\u003Dzxqad0J2WdWQXWHM7O24\u0024gAw\u003D)).ToArray<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(), \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzUsoE\u00244KLHwOEYi4PCA\u003D\u003D ?? (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzUsoE\u00244KLHwOEYi4PCA\u003D\u003D = new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DznHZdoyT\u0024EEaTkfWvQEMXoEih2Miy)));
  }

  private void \u0023\u003Dz4Q47Joqo_tHsaan4\u0024A\u003D\u003D(
    object _param1,
    \u0023\u003Dzro0Io1hfSw7LlH634iIk6HbbjLeMvfhhb2s2mR8\u003D _param2)
  {
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D qeYqJf714K2eMyR9I = new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D();
    qeYqJf714K2eMyR9I.\u0023\u003Dz1BK01YA\u003D = _param2;
    if (!qeYqJf714K2eMyR9I.\u0023\u003Dz1BK01YA\u003D.\u0023\u003DzejwSg21X3OO6() || qeYqJf714K2eMyR9I.\u0023\u003Dz1BK01YA\u003D.\u0023\u003DzjoPRpZLXrx8o())
      return;
    qeYqJf714K2eMyR9I.\u0023\u003Dz2vouRgM\u003D = _param1 as \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D;
    if (qeYqJf714K2eMyR9I.\u0023\u003Dz2vouRgM\u003D == null)
      return;
    CollectionHelper.ForEach<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this.Where<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(new Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D, bool>(qeYqJf714K2eMyR9I.\u0023\u003DzauLrVnHgfyr3yQ0vEFCCM5c\u003D)).ToArray<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(), new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(qeYqJf714K2eMyR9I.\u0023\u003Dz_njip1a1ig5klNz6gwSkhfw\u003D));
  }

  public XmlSchema GetSchema() => (XmlSchema) null;

  public void ReadXml(XmlReader _param1)
  {
    \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote fq05jnDg3bOrIrgCjote = (\u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote) null;
    if (this.ParentSurface != null)
      fq05jnDg3bOrIrgCjote = this.ParentSurface.SuspendUpdates();
    this.\u0023\u003Dz6_E5\u0024pE\u003D<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(\u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DztbbHmR4\u003D(_param1));
    fq05jnDg3bOrIrgCjote?.Dispose();
  }

  public void WriteXml(XmlWriter _param1)
  {
    \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DzT642HR8\u003D((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this, _param1);
  }

  public void \u0023\u003Dzhwms24tGl3w4(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D hhh93Q0DqkV5Sv90k in (Collection<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this)
    {
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = this.\u0023\u003DzvhansM3Nsebc9y\u0024i3fZ48OU\u003D(_param1.\u0023\u003DzSBrmxtNmmDcWbby6Gm0UVio\u003D, hhh93Q0DqkV5Sv90k, hhh93Q0DqkV5Sv90k.get_XAxisId(), true);
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = this.\u0023\u003DzvhansM3Nsebc9y\u0024i3fZ48OU\u003D(_param1.\u0023\u003DzPmZCkENGGLQws2poeTWSb6E\u003D, hhh93Q0DqkV5Sv90k, hhh93Q0DqkV5Sv90k.get_YAxisId(), false);
      hhh93Q0DqkV5Sv90k.Update(xkzemsMs5tGkouk5w1, xkzemsMs5tGkouk5w2);
      if (xkzemsMs5tGkouk5w1 == null)
        _param1.\u0023\u003Dz38sEjvRVtcBw().Add(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539341892), (object) hhh93Q0DqkV5Sv90k.GetType(), (object) (hhh93Q0DqkV5Sv90k.get_XAxisId() ?? \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539431904))));
      if (xkzemsMs5tGkouk5w2 == null)
        _param1.\u0023\u003Dz38sEjvRVtcBw().Add(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539342325), (object) hhh93Q0DqkV5Sv90k.GetType(), (object) (hhh93Q0DqkV5Sv90k.get_YAxisId() ?? \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539431904))));
    }
  }

  private \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003DzvhansM3Nsebc9y\u0024i3fZ48OU\u003D(
    IDictionary<string, \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>> _param1,
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param2,
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
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
  {
    if (!_param1.get_IsEditable() || _param1.get_IsSelected() || !_param1.IsAttached)
      return false;
    if (\u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk() != \u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D.Shift || !this.\u0023\u003DzbjRX\u0024PUuuOXT(_param1))
      this.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D ?? (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D = new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzVRk6KEtjy1UCzmcX5OcvFww\u003D)));
    this.\u0023\u003DzdkkvQAq7ppeh(_param1);
    return true;
  }

  private void \u0023\u003DzdkkvQAq7ppeh(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
  {
    _param1.set_IsSelected(true);
  }

  private bool \u0023\u003DzbjRX\u0024PUuuOXT(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
  {
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D lrrNtIjstOuVg4Rro = new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D()
    {
      \u0023\u003Dz2vouRgM\u003D = _param1
    };
    lrrNtIjstOuVg4Rro.\u0023\u003DzAG7Twks\u003D = this.Where<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(new Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D, bool>(lrrNtIjstOuVg4Rro.\u0023\u003Dzv2EVeGQzvOiLkeQ6I1bYc_s\u003D)).Concat<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) new \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D[1]
    {
      lrrNtIjstOuVg4Rro.\u0023\u003Dz2vouRgM\u003D
    }).ToArray<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>();
    return ((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) lrrNtIjstOuVg4Rro.\u0023\u003DzAG7Twks\u003D).All<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(new Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D, bool>(lrrNtIjstOuVg4Rro.\u0023\u003DzfZsZRxWRaYq2NFL\u0024VzVo6bw\u003D));
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
    foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D hhh93Q0DqkV5Sv90k in (IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this.Items)
      hhh93Q0DqkV5Sv90k.OnXAxesCollectionChanged(_param1, _param2);
  }

  public void \u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D hhh93Q0DqkV5Sv90k in (IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>) this.Items)
      hhh93Q0DqkV5Sv90k.OnYAxesCollectionChanged(_param1, _param2);
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Action \u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D;
    public static Action \u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D;
    public static Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D> \u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D;
    public static Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D> \u0023\u003DzvUis2ceeooAA32k3YQ\u003D\u003D;
    public static Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D> \u0023\u003DzUsoE\u00244KLHwOEYi4PCA\u003D\u003D;
    public static Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D> \u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D;

    internal void \u0023\u003DzP7tHDC_yuYcRLkKl1Q\u003D\u003D()
    {
    }

    internal void \u0023\u003Dzmu94CYIyRXEFBQQPPw\u003D\u003D()
    {
    }

    internal void \u0023\u003Dzi1mryHm_34LaAslQH6vheC4\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      _param1.set_IsSelected(false);
    }

    internal void \u0023\u003DzWLL21j0cIbtvfyx1n7j_L3p5Tj3r(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      _param1.StartDrag(false);
    }

    internal void \u0023\u003DznHZdoyT\u0024EEaTkfWvQEMXoEih2Miy(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      _param1.EndDrag();
    }

    internal void \u0023\u003DzVRk6KEtjy1UCzmcX5OcvFww\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      _param1.set_IsSelected(false);
    }
  }

  private sealed class \u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D
  {
    public \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D \u0023\u003Dz2vouRgM\u003D;

    internal bool \u0023\u003Dzxqad0J2WdWQXWHM7O24\u0024gAw\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D && _param1.get_IsSelected();
    }
  }

  private sealed class \u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D
  {
    public \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D \u0023\u003Dz2vouRgM\u003D;
    public \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D[] \u0023\u003DzAG7Twks\u003D;

    internal bool \u0023\u003Dzv2EVeGQzvOiLkeQ6I1bYc_s\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D && _param1.get_IsSelected();
    }

    internal bool \u0023\u003DzfZsZRxWRaYq2NFL\u0024VzVo6bw\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      return _param1.CanMultiSelect(this.\u0023\u003DzAG7Twks\u003D);
    }
  }

  private sealed class \u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D
  {
    public \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D \u0023\u003Dz2vouRgM\u003D;

    internal bool \u0023\u003DzohWX52bl44U09jwJQRRBQCc\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D && _param1.get_IsSelected();
    }
  }

  private sealed class \u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D
  {
    public \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D \u0023\u003Dz2vouRgM\u003D;
    public \u0023\u003Dzro0Io1hfSw7LlH634iIk6HbbjLeMvfhhb2s2mR8\u003D \u0023\u003Dz1BK01YA\u003D;

    internal bool \u0023\u003DzauLrVnHgfyr3yQ0vEFCCM5c\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      return _param1 != this.\u0023\u003Dz2vouRgM\u003D && _param1.get_IsSelected();
    }

    internal void \u0023\u003Dz_njip1a1ig5klNz6gwSkhfw\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      _param1.Drag(this.\u0023\u003Dz1BK01YA\u003D.\u0023\u003Dz06oxr0QggddI(), this.\u0023\u003Dz1BK01YA\u003D.\u0023\u003Dz7Pq57plSf4mj());
    }
  }
}
