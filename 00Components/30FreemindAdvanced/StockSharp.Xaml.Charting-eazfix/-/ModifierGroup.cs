// Decompiled with JetBrains decompiler
// Type: -.ModifierGroup
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

#nullable disable
namespace StockSharp.Charting;

[System.Windows.Markup.ContentProperty("ChildModifiers")]
public sealed class ModifierGroup : 
  MasterSlaveChartModifier
{
  
  public static readonly DependencyProperty \u0023\u003Dzmf0Yyi0vkYuX = DependencyProperty.Register(nameof (ChildModifiers), typeof (ObservableCollection<IChartModifier>), typeof (ModifierGroup), new PropertyMetadata((object) null, new PropertyChangedCallback(ModifierGroup.\u0023\u003DzMGnM8Q7UmYY2)));
  
  private readonly Grid _grid = new Grid();

  public ModifierGroup()
    : this(Array.Empty<IChartModifier>())
  {
  }

  public ModifierGroup(
    params IChartModifier[] _param1)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, "childModifiers");
    for (int index = 0; index < _param1.Length; ++index)
      \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1[index], $"childModifiers[{index}]");
    this.Content = (object) this._grid;
    this.SetCurrentValue(ModifierGroup.\u0023\u003Dzmf0Yyi0vkYuX, (object) new ObservableCollection<IChartModifier>((IEnumerable<IChartModifier>) _param1));
  }

  public ObservableCollection<IChartModifier> ChildModifiers
  {
    get
    {
      return (ObservableCollection<IChartModifier>) this.GetValue(ModifierGroup.\u0023\u003Dzmf0Yyi0vkYuX);
    }
    set
    {
      this.SetValue(ModifierGroup.\u0023\u003Dzmf0Yyi0vkYuX, (object) value);
    }
  }

  public IChartModifier this[string _param1]
  {
    get => this.\u0023\u003DzxRGD7v40sdKc(_param1);
  }

  public IChartModifier this[int _param1]
  {
    get => this.ChildModifiers[_param1];
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this.\u0023\u003DznQRtYJ3QBq0M((IEnumerable<IChartModifier>) this.ChildModifiers);
  }

  public override void OnDetached()
  {
    base.OnDetached();
    this.\u0023\u003DzHs2cDCMHYNdU((IEnumerable<IChartModifier>) this.ChildModifiers);
  }

  private void \u0023\u003DznQRtYJ3QBq0M(
    IEnumerable<IChartModifier> _param1)
  {
    if (!this.IsAttached)
      return;
    _param1.\u0023\u003Dz30RSSSygABj_<IChartModifier>(new Action<IChartModifier>(this.\u0023\u003DzNnndxL_a\u00246L_));
  }

  private void \u0023\u003DzNnndxL_a\u00246L_(
    IChartModifier _param1)
  {
    this.\u0023\u003DztqcafrnOGvWC(_param1);
    _param1.ParentSurface = this.ParentSurface;
    _param1.Services = this.Services;
    _param1.DataContext = this.DataContext;
    _param1.IsAttached = true;
    _param1.OnAttached();
  }

  private void \u0023\u003DztqcafrnOGvWC(
    IChartModifier _param1)
  {
    this._grid.\u0023\u003DzH0osWQkV_Y8_((object) _param1, -1);
  }

  private void \u0023\u003DzHs2cDCMHYNdU(
    IEnumerable<IChartModifier> _param1)
  {
    _param1.\u0023\u003Dz30RSSSygABj_<IChartModifier>(new Action<IChartModifier>(this.\u0023\u003Dz1zWQSIoTjtxX));
  }

  private void \u0023\u003Dz1zWQSIoTjtxX(
    IChartModifier _param1)
  {
    this.\u0023\u003DzBLgi0dgELqPs(_param1);
    _param1.OnDetached();
    _param1.IsAttached = false;
    _param1.ParentSurface = (ISciChartSurface) null;
    _param1.Services = (IServiceContainer) null;
  }

  private void \u0023\u003DzBLgi0dgELqPs(
    IChartModifier _param1)
  {
    this._grid.\u0023\u003DziYdJ\u00246cCiBha((object) _param1);
  }

  protected override void \u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    ModifierGroup.SomeClass7654 lvtppRwsYcyoelU8 = new ModifierGroup.SomeClass7654();
    lvtppRwsYcyoelU8.\u0023\u003DzwM8aRUE\u003D = _param1;
    lvtppRwsYcyoelU8.\u0023\u003DzTi2kmf4\u003D = _param2;
    if (this.ChildModifiers == null)
      return;
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<IChartModifier>(new Action<IChartModifier>(lvtppRwsYcyoelU8.\u0023\u003DzDlxN3i6L\u0024bBQoRc5kCBdWWQ\u003D));
  }

  protected override void \u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    ModifierGroup.SomeClass5555 kiaDl76b0Nyu42rxJq = new ModifierGroup.SomeClass5555();
    kiaDl76b0Nyu42rxJq.\u0023\u003DzwM8aRUE\u003D = _param1;
    kiaDl76b0Nyu42rxJq.\u0023\u003DzTi2kmf4\u003D = _param2;
    if (this.ChildModifiers == null)
      return;
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<IChartModifier>(new Action<IChartModifier>(kiaDl76b0Nyu42rxJq.\u0023\u003Dz\u0024UXNSD6YJPnU9jHAajxBjjg\u003D));
  }

  protected override void \u0023\u003Dzok6jmLaiH5ai(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    ModifierGroup.SomeClass6409 v4vdZv8GtEzAmB0rzFq = new ModifierGroup.SomeClass6409();
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzwM8aRUE\u003D = _param1;
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzTi2kmf4\u003D = _param2;
    if (this.ChildModifiers == null)
      return;
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<IChartModifier>(new Action<IChartModifier>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzSNWKb\u0024dBOE2a0JlR9g\u003D\u003D));
  }

  protected override void OnIsEnabledChanged()
  {
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<IChartModifier>(new Action<IChartModifier>(this.\u0023\u003DzHKDu2R4\u0024lFIb58vAYGbiULo\u003D));
  }

  public override void \u0023\u003Dz5y8F1YNwkhnW(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003Dzgc4SCis4HjHyynhFkS9uoNrJlYYn)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  private void \u0023\u003DziTCeMLw\u003D(
    Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> _param1,
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
  {
    if (this.ChildModifiers == null)
      return;
    foreach (IChartModifier chhAr3Kksm46Uy2Zy in this.ChildModifiers.Where<IChartModifier>(ModifierGroup.SomeClass34343383.\u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D = new Func<IChartModifier, bool>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzxqzF6jTaY9lVLxHYg5yFLN0\u003D))))
    {
      if (chhAr3Kksm46Uy2Zy.ReceiveHandledEvents || !_param2.\u0023\u003Dz882B0y3Ue8fl())
        _param1(chhAr3Kksm46Uy2Zy, _param2);
    }
  }

  public override void OnModifierMouseDown(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.m_public_static_Func_ChartCompentViewModel_bool_ ?? (ModifierGroup.SomeClass34343383.m_public_static_Func_ChartCompentViewModel_bool_ = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003Dz5krLWsvoXbq\u00248_PqIuQDSoaDq3r_)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003Dz3eP\u0024F06Do2Yqw5911Phrm\u0024c\u003D)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void OnModifierMouseUp(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.\u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzZSMUtK9zaXNAI8WwDGKJ3XDLzVv8)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003DzQTINWhMByBmJ(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzZ\u0024KRLvh2qpiaoFfyVxWBpgmJh\u0024ZU)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void OnMasterMouseLeave(
    ModifierMouseArgs _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzVYORBuxvOyw39HzBaLE20tNlRtHr)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003Dz0yya794Z8OaI(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.\u0023\u003DzZ4U93QQjpDamF3o9CQ\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003DzZ4U93QQjpDamF3o9CQ\u003D\u003D = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzT6V9kc7Cfg601etpFH8cUK\u0024ZsXar)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003DzpmQpuKvOtHIk(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.\u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzbHvqWKMkbu6LA9m3wYK0Das\u003D)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public override void \u0023\u003DzsSwjrBzrsGPJ(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    this.\u0023\u003DziTCeMLw\u003D(ModifierGroup.SomeClass34343383.\u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D ?? (ModifierGroup.SomeClass34343383.\u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D = new Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD>(ModifierGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzdWZmB5uhdY2tAn7ZmG_SSuItHoU_)), (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD) _param1);
  }

  public bool \u0023\u003DzG9Tzlq4\u003D(Type _param1)
  {
    return this.ChildModifiers.Any<IChartModifier>(new Func<IChartModifier, bool>(new ModifierGroup.\u0023\u003DzupKvug2uTOcTyk52k2urQQw\u003D()
    {
      \u0023\u003DzCN8ZMQU\u003D = _param1
    }.\u0023\u003DzFNBE1DOFc1Eu9K4xOQ\u003D\u003D));
  }

  protected override void \u0023\u003DzUq0D2jBe2UY\u0024(
    object _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.ChildModifiers.\u0023\u003Dz30RSSSygABj_<IChartModifier>(new Action<IChartModifier>(new ModifierGroup.\u0023\u003DzOZhF6r12fDpdxPyyP9aETuM\u003D()
    {
      \u0023\u003Dz1BK01YA\u003D = _param2
    }.\u0023\u003Dzq9_03GhVOxezkFFX0w\u003D\u003D));
  }

  private IChartModifier \u0023\u003DzxRGD7v40sdKc(
    string _param1)
  {
    return this.ChildModifiers.FirstOrDefault<IChartModifier>(new Func<IChartModifier, bool>(new ModifierGroup.\u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D()
    {
      \u0023\u003DzM_vMnac\u003D = _param1
    }.\u0023\u003DzDUlFPKVc44hNw\u0024Hpo\u0024G0dFo\u003D));
  }

  public override void ReadXml(XmlReader _param1)
  {
    ObservableCollection<ChartModifierBase> observableCollection = new ObservableCollection<ChartModifierBase>();
    observableCollection.\u0023\u003Dz6_E5\u0024pE\u003D<ChartModifierBase>(\u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DztbbHmR4\u003D(_param1));
    foreach (IChartModifier chhAr3Kksm46Uy2Zy in (Collection<ChartModifierBase>) observableCollection)
      this.ChildModifiers.Add(chhAr3Kksm46Uy2Zy);
  }

  public override void WriteXml(XmlWriter _param1)
  {
    \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DzT642HR8\u003D(this.ChildModifiers.Cast<ChartModifierBase>(), _param1);
  }

  public override void \u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D()
  {
    foreach (IChartModifier childModifier in (Collection<IChartModifier>) this.ChildModifiers)
      childModifier.\u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D();
  }

  private static void \u0023\u003DzMGnM8Q7UmYY2(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is ModifierGroup c7XudP8HjK96ZEjd))
      return;
    ObservableCollection<IChartModifier> oldValue = _param1.OldValue as ObservableCollection<IChartModifier>;
    ObservableCollection<IChartModifier> newValue = _param1.NewValue as ObservableCollection<IChartModifier>;
    if (oldValue != null)
    {
      c7XudP8HjK96ZEjd.\u0023\u003DzHs2cDCMHYNdU((IEnumerable<IChartModifier>) oldValue);
      oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(c7XudP8HjK96ZEjd.\u0023\u003DzJTJiOWG1\u0024tjr);
    }
    if (newValue == null)
      return;
    c7XudP8HjK96ZEjd.\u0023\u003DznQRtYJ3QBq0M((IEnumerable<IChartModifier>) newValue);
    newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(c7XudP8HjK96ZEjd.\u0023\u003DzJTJiOWG1\u0024tjr);
  }

  private void \u0023\u003DzJTJiOWG1\u0024tjr(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    if (_param2.NewItems != null)
      this.\u0023\u003DznQRtYJ3QBq0M(_param2.NewItems.Cast<IChartModifier>());
    if (_param2.OldItems == null)
      return;
    this.\u0023\u003DzHs2cDCMHYNdU(_param2.OldItems.Cast<IChartModifier>());
  }

  private void \u0023\u003DzHKDu2R4\u0024lFIb58vAYGbiULo\u003D(
    IChartModifier _param1)
  {
    _param1.set_IsEnabled(this.IsEnabled);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly ModifierGroup.SomeClass34343383 SomeMethond0343 = new ModifierGroup.SomeClass34343383();
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;
    public static Func<IChartModifier, bool> \u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D;
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> m_public_static_Func_ChartCompentViewModel_bool_;
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D;
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D;
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzwCieN8nlFS3aCeRPgg\u003D\u003D;
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D;
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzZ4U93QQjpDamF3o9CQ\u003D\u003D;
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D;
    public static Action<IChartModifier, \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD> \u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D;

    public void \u0023\u003Dzgc4SCis4HjHyynhFkS9uoNrJlYYn(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003Dz5y8F1YNwkhnW((ModifierMouseArgs) _param2);
    }

    public bool \u0023\u003DzxqzF6jTaY9lVLxHYg5yFLN0\u003D(
      IChartModifier _param1)
    {
      return _param1.get_IsEnabled();
    }

    public void \u0023\u003Dz5krLWsvoXbq\u00248_PqIuQDSoaDq3r_(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.OnModifierMouseDown((ModifierMouseArgs) _param2);
    }

    public void \u0023\u003Dz3eP\u0024F06Do2Yqw5911Phrm\u0024c\u003D(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.OnModifierMouseMove((ModifierMouseArgs) _param2);
    }

    public void \u0023\u003DzZSMUtK9zaXNAI8WwDGKJ3XDLzVv8(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.OnModifierMouseUp((ModifierMouseArgs) _param2);
    }

    public void \u0023\u003DzZ\u0024KRLvh2qpiaoFfyVxWBpgmJh\u0024ZU(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003DzQTINWhMByBmJ((ModifierMouseArgs) _param2);
    }

    public void \u0023\u003DzVYORBuxvOyw39HzBaLE20tNlRtHr(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.OnMasterMouseLeave((ModifierMouseArgs) _param2);
    }

    public void \u0023\u003DzT6V9kc7Cfg601etpFH8cUK\u0024ZsXar(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003Dz0yya794Z8OaI((\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf) _param2);
    }

    public void \u0023\u003DzbHvqWKMkbu6LA9m3wYK0Das\u003D(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003DzpmQpuKvOtHIk((\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf) _param2);
    }

    public void \u0023\u003DzdWZmB5uhdY2tAn7ZmG_SSuItHoU_(
      IChartModifier _param1,
      \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD _param2)
    {
      _param1.\u0023\u003DzsSwjrBzrsGPJ((\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf) _param2);
    }
  }

  private sealed class \u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D
  {
    public string \u0023\u003DzM_vMnac\u003D;

    public bool \u0023\u003DzDUlFPKVc44hNw\u0024Hpo\u0024G0dFo\u003D(
      IChartModifier _param1)
    {
      return this.\u0023\u003DzM_vMnac\u003D == _param1.ModifierName;
    }
  }

  private sealed class \u0023\u003DzOZhF6r12fDpdxPyyP9aETuM\u003D
  {
    public DependencyPropertyChangedEventArgs \u0023\u003Dz1BK01YA\u003D;

    public void \u0023\u003Dzq9_03GhVOxezkFFX0w\u003D\u003D(
      IChartModifier _param1)
    {
      _param1.DataContext = this.\u0023\u003Dz1BK01YA\u003D.NewValue;
    }
  }

  private sealed class SomeClass7654
  {
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    public void \u0023\u003DzDlxN3i6L\u0024bBQoRc5kCBdWWQ\u003D(
      IChartModifier _param1)
    {
      _param1.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }

  private sealed class SomeClass6409
  {
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    public void \u0023\u003DzSNWKb\u0024dBOE2a0JlR9g\u003D\u003D(
      IChartModifier _param1)
    {
      _param1.\u0023\u003Dzok6jmLaiH5ai(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }

  private sealed class \u0023\u003DzupKvug2uTOcTyk52k2urQQw\u003D
  {
    public Type \u0023\u003DzCN8ZMQU\u003D;

    public bool \u0023\u003DzFNBE1DOFc1Eu9K4xOQ\u003D\u003D(
      IChartModifier _param1)
    {
      return _param1.GetType() == this.\u0023\u003DzCN8ZMQU\u003D;
    }
  }

  private sealed class SomeClass5555
  {
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    public void \u0023\u003Dz\u0024UXNSD6YJPnU9jHAajxBjjg\u003D(
      IChartModifier _param1)
    {
      _param1.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }
}
