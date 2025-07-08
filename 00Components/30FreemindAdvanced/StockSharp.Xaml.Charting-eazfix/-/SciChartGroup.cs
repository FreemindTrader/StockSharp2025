// Decompiled with JetBrains decompiler
// Type: -.SciChartGroup
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Xaml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

#nullable disable
namespace \u002D;

[TemplatePart(Name = "PART_MainGrid")]
[TemplatePart(Name = "PART_TabbedContent")]
[TemplatePart(Name = "PART_StackedContent")]
[TemplatePart(Name = "PART_MainPane")]
[TemplatePart(Name = "PART_UltrachartGroupModifierCanvas")]
internal sealed class SciChartGroup : 
  ItemsControl,
  INotifyPropertyChanged
{
  
  public static readonly DependencyProperty \u0023\u003DzSQ9PALvdLOX4 = DependencyProperty.RegisterAttached("VerticalChartGroup", typeof (string), typeof (SciChartGroup), new PropertyMetadata((object) null, new PropertyChangedCallback(SciChartGroup.\u0023\u003DznWImvCruq8sQ)));
  
  public static readonly DependencyProperty \u0023\u003Dz3Y_OZ80lXJkE = DependencyProperty.Register(nameof (IsTabbed), typeof (bool), typeof (SciChartGroup), new PropertyMetadata((object) false, new PropertyChangedCallback(SciChartGroup.\u0023\u003Dzgxjo2OnorrBU)));
  
  internal static Dictionary<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string> \u0023\u003DzenwCc\u0024sVGsge = new Dictionary<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>();
  
  private ContentPresenter \u0023\u003DzBexXE3VONLO6;
  
  private TabControl \u0023\u003DzsjTC4Q1Ojm2hhyVspw\u003D\u003D;
  
  private StackPanel \u0023\u003DzhUaBLeEcnz4yciN\u0024Mw\u003D\u003D;
  
  private Canvas \u0023\u003DzF8_YcFVDbTFB;
  
  private double \u0023\u003DzrSLETkwHQoH38mnTuw\u003D\u003D;
  
  private double \u0023\u003Dzerqhc5X832JL;
  
  private double \u0023\u003DzVi6cjZRqUP3j;
  
  private double \u0023\u003Dzu9zFAJU\u00246XS3lPkdqi1iVcE\u003D;
  
  private double \u0023\u003Dzi1vdbCxOaDS\u0024;
  
  private \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D \u0023\u003DzzXyefThIl4Ak;
  
  private \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D \u0023\u003DzBq2lcgtVrsr_;
  
  private Action<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D> \u0023\u003DzSp5oAkvK2sET;
  
  private readonly List<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D> \u0023\u003Dzg0gWX4E\u003D = new List<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>();

  public SciChartGroup()
  {
    this.DefaultStyleKey = (object) typeof (SciChartGroup);
    this.\u0023\u003DzSp5oAkvK2sET = new Action<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(this.\u0023\u003Dz\u0024TRcYUKCOmDd);
  }

  public bool IsTabbed
  {
    get
    {
      return (bool) this.GetValue(SciChartGroup.\u0023\u003Dz3Y_OZ80lXJkE);
    }
    set
    {
      this.SetValue(SciChartGroup.\u0023\u003Dz3Y_OZ80lXJkE, (object) value);
    }
  }

  public bool HasTabbedItems
  {
    get
    {
      return this.\u0023\u003DzsjTC4Q1Ojm2hhyVspw\u003D\u003D != null && this.\u0023\u003DzsjTC4Q1Ojm2hhyVspw\u003D\u003D.Items.Count > 0;
    }
  }

  public static void SetVerticalChartGroup(DependencyObject _param0, string _param1)
  {
    _param0.SetValue(SciChartGroup.\u0023\u003DzSQ9PALvdLOX4, (object) _param1);
  }

  public static string GetVerticalChartGroup(DependencyObject _param0)
  {
    return (string) _param0.GetValue(SciChartGroup.\u0023\u003DzSQ9PALvdLOX4);
  }

  protected override bool IsItemItsOwnContainerOverride(object _param1)
  {
    return base.IsItemItsOwnContainerOverride(_param1);
  }

  protected override DependencyObject GetContainerForItemOverride()
  {
    return base.GetContainerForItemOverride();
  }

  protected override void OnItemsSourceChanged(IEnumerable _param1, IEnumerable _param2)
  {
    base.OnItemsSourceChanged(_param1, _param2);
    this.\u0023\u003DzdHZNWU3eA28h(_param1, _param2);
  }

  private void \u0023\u003DzdHZNWU3eA28h(IEnumerable _param1, IEnumerable _param2)
  {
    if (this.\u0023\u003DzBexXE3VONLO6 == null)
      return;
    if (_param1 != null)
      this.\u0023\u003DzPYYDHvoZo80S(_param1.Cast<IScichartSurfaceVM>());
    if (_param2 == null)
      return;
    this.\u0023\u003Dzj8GFjnf5ACg2(_param2.Cast<IScichartSurfaceVM>());
  }

  protected override void OnItemsChanged(NotifyCollectionChangedEventArgs _param1)
  {
    base.OnItemsChanged(_param1);
    if (this.\u0023\u003DzBexXE3VONLO6 == null || _param1.Action == NotifyCollectionChangedAction.Add && _param1.NewItems == null || _param1.Action == NotifyCollectionChangedAction.Remove && _param1.OldItems == null)
      return;
    switch (_param1.Action)
    {
      case NotifyCollectionChangedAction.Add:
        this.\u0023\u003Dzj8GFjnf5ACg2(_param1.NewItems.Cast<IScichartSurfaceVM>());
        break;
      case NotifyCollectionChangedAction.Remove:
        this.\u0023\u003DzPYYDHvoZo80S(_param1.OldItems.Cast<IScichartSurfaceVM>());
        break;
      case NotifyCollectionChangedAction.Reset:
        this.\u0023\u003DzleRWWIS9Sb_X();
        break;
    }
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.\u0023\u003DzleRWWIS9Sb_X();
    this.\u0023\u003DzBexXE3VONLO6 = this.GetTemplateChild("PART_MainPane") as ContentPresenter;
    this.\u0023\u003DzsjTC4Q1Ojm2hhyVspw\u003D\u003D = this.GetTemplateChild("PART_TabbedContent") as TabControl;
    this.\u0023\u003DzhUaBLeEcnz4yciN\u0024Mw\u003D\u003D = this.GetTemplateChild("PART_StackedContent") as StackPanel;
    this.\u0023\u003DzF8_YcFVDbTFB = this.GetTemplateChild("PART_UltrachartGroupModifierCanvas") as Canvas;
    if (this.ItemsSource == null)
      return;
    this.\u0023\u003DzdHZNWU3eA28h((IEnumerable) null, this.ItemsSource);
  }

  private void \u0023\u003DzleRWWIS9Sb_X()
  {
    while (!this.\u0023\u003Dzg0gWX4E\u003D.\u0023\u003DzCCMM80zDpO6N<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>())
      this.\u0023\u003Dz7IDM9DY\u003D(this.\u0023\u003Dzg0gWX4E\u003D[0]);
  }

  private void \u0023\u003DzPYYDHvoZo80S(
    IEnumerable<IScichartSurfaceVM> _param1)
  {
    _param1.\u0023\u003Dz30RSSSygABj_<IScichartSurfaceVM>(new Action<IScichartSurfaceVM>(this.\u0023\u003DzLngL5eSRzHKM7HW4zMEg1WQ\u003D));
  }

  private \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D \u0023\u003Dz7IDM9DY\u003D(
    IScichartSurfaceVM _param1)
  {
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D abYiNis93iXoWaCiA = this.\u0023\u003Dzg0gWX4E\u003D.FirstOrDefault<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(new SciChartGroup.\u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D()
    {
      _someChartElement = _param1
    }.\u0023\u003Dz_0lW5QtpqHwetfa5Xg\u003D\u003D));
    this.\u0023\u003Dz7IDM9DY\u003D(abYiNis93iXoWaCiA);
    return abYiNis93iXoWaCiA;
  }

  private void \u0023\u003Dz7IDM9DY\u003D(
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
  {
    if (_param1 == null)
      return;
    if (!_param1.\u0023\u003DzR8SPjvFW2FAx())
    {
      this.\u0023\u003DzfttffOE\u003D(_param1.PaneElement as dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd);
      this.\u0023\u003Dzg0gWX4E\u003D.Remove(_param1);
      if (_param1.IsTabbed)
        this.\u0023\u003DzyMYzZrp0NM6I(_param1);
      else
        this.\u0023\u003DzDMmjeHINvH28(_param1);
    }
    else
      this.\u0023\u003Dzx3xNOxupdPEo(_param1);
  }

  private void \u0023\u003DzDMmjeHINvH28(
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
  {
    this.\u0023\u003DzhUaBLeEcnz4yciN\u0024Mw\u003D\u003D.Children.Remove((UIElement) _param1.PaneElement);
  }

  private void \u0023\u003DzyMYzZrp0NM6I(
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
  {
    TabItem removeItem = this.\u0023\u003DzsjTC4Q1Ojm2hhyVspw\u003D\u003D.Items.OfType<TabItem>().FirstOrDefault<TabItem>(new Func<TabItem, bool>(new SciChartGroup.\u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D()
    {
      \u0023\u003DzKveAMLo\u003D = _param1
    }.\u0023\u003Dz240YAiB2jNus8hcG9w\u003D\u003D));
    if (removeItem == null)
      return;
    removeItem.Content = (object) null;
    this.\u0023\u003DzsjTC4Q1Ojm2hhyVspw\u003D\u003D.Items.Remove((object) removeItem);
    this.\u0023\u003Dz15moWio\u003D("HasTabbedItems");
  }

  private void \u0023\u003Dzx3xNOxupdPEo(
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
  {
    if (!_param1.\u0023\u003DzR8SPjvFW2FAx())
      throw new ArgumentException("Attempt to remove MainPane was failed. Passed invalid ItemPane argument.");
    this.\u0023\u003DzBexXE3VONLO6.Content = (object) null;
    this.\u0023\u003Dzg0gWX4E\u003D.Remove(_param1);
    _param1.\u0023\u003DzcXGZaBR0mVD3(false);
  }

  private void \u0023\u003Dzj8GFjnf5ACg2(
    IEnumerable<IScichartSurfaceVM> _param1)
  {
    if (this.\u0023\u003DzBexXE3VONLO6.Content == null && _param1.Any<IScichartSurfaceVM>())
    {
      this.\u0023\u003DzllXC_uTwjSwD(_param1.First<IScichartSurfaceVM>());
      _param1.Skip<IScichartSurfaceVM>(1).\u0023\u003Dz30RSSSygABj_<IScichartSurfaceVM>(new Action<IScichartSurfaceVM>(this.\u0023\u003Dzvq\u0024O7vc\u003D));
    }
    else
      _param1.\u0023\u003Dz30RSSSygABj_<IScichartSurfaceVM>(new Action<IScichartSurfaceVM>(this.\u0023\u003Dzvq\u0024O7vc\u003D));
  }

  private void \u0023\u003DzllXC_uTwjSwD(
    IScichartSurfaceVM _param1)
  {
    FrameworkElement frameworkElement = this.\u0023\u003DzAy0v9i5A19lM();
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D abYiNis93iXoWaCiA1 = new \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D();
    abYiNis93iXoWaCiA1.PaneViewModel = _param1;
    abYiNis93iXoWaCiA1.PaneElement = frameworkElement;
    abYiNis93iXoWaCiA1.\u0023\u003DzcXGZaBR0mVD3(true);
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D abYiNis93iXoWaCiA2 = abYiNis93iXoWaCiA1;
    frameworkElement.DataContext = (object) abYiNis93iXoWaCiA2.PaneViewModel;
    this.\u0023\u003DzBexXE3VONLO6.Content = (object) frameworkElement;
    this.\u0023\u003Dzg0gWX4E\u003D.Add(abYiNis93iXoWaCiA2);
  }

  private void \u0023\u003Dzvq\u0024O7vc\u003D(
    IScichartSurfaceVM _param1)
  {
    SciChartGroup.\u0023\u003DzbQEIVhBRscvmjvQLdNZzKp4\u003D brscvmjvQldNzzKp4 = new SciChartGroup.\u0023\u003DzbQEIVhBRscvmjvQLdNZzKp4\u003D();
    brscvmjvQldNzzKp4._variableSome3535 = this;
    FrameworkElement frameworkElement = this.\u0023\u003DzAy0v9i5A19lM();
    brscvmjvQldNzzKp4.\u0023\u003DzKveAMLo\u003D = new \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D()
    {
      PaneViewModel = _param1,
      PaneElement = frameworkElement
    };
    frameworkElement.DataContext = (object) _param1;
    brscvmjvQldNzzKp4.\u0023\u003DzKveAMLo\u003D.ChangeOrientationCommand = (ICommand) new DelegateCommand(new Action(brscvmjvQldNzzKp4.\u0023\u003DzJeMUirilpUA5DzmAPQ\u003D\u003D));
    brscvmjvQldNzzKp4.\u0023\u003DzKveAMLo\u003D.ClosePaneCommand = _param1.ClosePaneCommand;
    dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd bu556QhykV4MlT6VEjd1 = new dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd();
    bu556QhykV4MlT6VEjd1.Content = (object) frameworkElement;
    bu556QhykV4MlT6VEjd1.DataContext = (object) brscvmjvQldNzzKp4.\u0023\u003DzKveAMLo\u003D;
    bu556QhykV4MlT6VEjd1.Style = this.ItemContainerStyle;
    dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd bu556QhykV4MlT6VEjd2 = bu556QhykV4MlT6VEjd1;
    brscvmjvQldNzzKp4.\u0023\u003DzKveAMLo\u003D.PaneElement = (FrameworkElement) bu556QhykV4MlT6VEjd2;
    this.\u0023\u003DzZcbqdpE\u003D(bu556QhykV4MlT6VEjd2);
    this.\u0023\u003DzSp5oAkvK2sET(brscvmjvQldNzzKp4.\u0023\u003DzKveAMLo\u003D);
    this.\u0023\u003Dzg0gWX4E\u003D.Add(brscvmjvQldNzzKp4.\u0023\u003DzKveAMLo\u003D);
  }

  private void \u0023\u003Dz\u0024TRcYUKCOmDd(
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
  {
    _param1.IsTabbed = false;
    this.\u0023\u003DzhUaBLeEcnz4yciN\u0024Mw\u003D\u003D.Children.Add((UIElement) _param1.PaneElement);
  }

  private void \u0023\u003Dz2u\u0024j6ogJlrTF(
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
  {
    this.\u0023\u003DztUBna_qcuT2H(_param1.PaneElement as dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd);
    _param1.IsTabbed = true;
    TabItem tabItem = new TabItem();
    tabItem.Header = (object) _param1.PaneViewModel.Title;
    tabItem.DataContext = (object) _param1;
    tabItem.Content = (object) _param1.PaneElement;
    TabItem newItem = tabItem;
    this.\u0023\u003DzsjTC4Q1Ojm2hhyVspw\u003D\u003D.Items.Add((object) newItem);
    this.\u0023\u003DzsjTC4Q1Ojm2hhyVspw\u003D\u003D.SelectedItem = (object) newItem;
    this.\u0023\u003Dz15moWio\u003D("HasTabbedItems");
  }

  public void \u0023\u003DzjSk6Jvc\u003D(
    IScichartSurfaceVM _param1)
  {
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D abYiNis93iXoWaCiA = this.\u0023\u003Dz7IDM9DY\u003D(_param1);
    if (abYiNis93iXoWaCiA == null)
      return;
    if (abYiNis93iXoWaCiA.IsTabbed)
      this.\u0023\u003Dz\u0024TRcYUKCOmDd(abYiNis93iXoWaCiA);
    else
      this.\u0023\u003Dz2u\u0024j6ogJlrTF(abYiNis93iXoWaCiA);
  }

  private void \u0023\u003DzjSk6Jvc\u003D(
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
  {
    if (_param1.IsTabbed)
    {
      this.\u0023\u003DzyMYzZrp0NM6I(_param1);
      this.\u0023\u003Dz\u0024TRcYUKCOmDd(_param1);
    }
    else
    {
      this.\u0023\u003DzDMmjeHINvH28(_param1);
      this.\u0023\u003Dz2u\u0024j6ogJlrTF(_param1);
    }
  }

  private void \u0023\u003DzZcbqdpE\u003D(
    dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd _param1)
  {
    _param1.\u0023\u003Dz0hN6sXzm\u0024hNI(new EventHandler<DragDeltaEventArgs>(this.\u0023\u003DztNo2m0eYh0OW));
    _param1.\u0023\u003Dzl15u7RiCXQ5Z\u0024LD2pw\u003D\u003D(new EventHandler<DragCompletedEventArgs>(this.\u0023\u003Dzfjq0gu2RlSgRNsm3XA\u003D\u003D));
  }

  private void \u0023\u003DzfttffOE\u003D(
    dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd _param1)
  {
    _param1.\u0023\u003DztDKfd8Um6eoX(new EventHandler<DragDeltaEventArgs>(this.\u0023\u003DztNo2m0eYh0OW));
    _param1.\u0023\u003DzDSAYx73Sokd65zKLxQ\u003D\u003D(new EventHandler<DragCompletedEventArgs>(this.\u0023\u003Dzfjq0gu2RlSgRNsm3XA\u003D\u003D));
  }

  private void \u0023\u003DztNo2m0eYh0OW(object _param1, DragDeltaEventArgs _param2)
  {
    SciChartGroup.\u0023\u003DzgEL8Rs_dn1ebi1vn37xdjks\u003D dn1ebi1vn37xdjks = new SciChartGroup.\u0023\u003DzgEL8Rs_dn1ebi1vn37xdjks\u003D();
    dn1ebi1vn37xdjks._variableSome3535 = this;
    if (this.\u0023\u003DzzXyefThIl4Ak == null)
    {
      this.\u0023\u003DzzXyefThIl4Ak = dn1ebi1vn37xdjks.\u0023\u003Dq1qGQwgs_F6ae19p24dqRj6GgdRzMrVsVc3m6iIKHPR2xR8NNZoxf3Wma3slxNuB2((UIElement) _param1);
      int num = this.\u0023\u003DzhUaBLeEcnz4yciN\u0024Mw\u003D\u003D.Children.IndexOf((UIElement) this.\u0023\u003DzzXyefThIl4Ak.PaneElement);
      if (num < 0)
      {
        \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzUuEJfBE\u003D(this.\u0023\u003DzzXyefThIl4Ak.IsTabbed, "unexpected pane type");
        int count = this.\u0023\u003DzhUaBLeEcnz4yciN\u0024Mw\u003D\u003D.Children.Count;
        this.\u0023\u003DzBq2lcgtVrsr_ = count > 0 ? dn1ebi1vn37xdjks.\u0023\u003Dq1qGQwgs_F6ae19p24dqRj6GgdRzMrVsVc3m6iIKHPR2xR8NNZoxf3Wma3slxNuB2(this.\u0023\u003DzhUaBLeEcnz4yciN\u0024Mw\u003D\u003D.Children[count - 1]) : this.\u0023\u003Dzg0gWX4E\u003D.First<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(SciChartGroup.SomeClass34343383.\u0023\u003Dzje8bZnYPCcLUKo6EWw\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003Dzje8bZnYPCcLUKo6EWw\u003D\u003D = new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzAVi2aBRehqWFfXq9jYMUhjI\u003D)));
      }
      else
        this.\u0023\u003DzBq2lcgtVrsr_ = num != 0 ? dn1ebi1vn37xdjks.\u0023\u003Dq1qGQwgs_F6ae19p24dqRj6GgdRzMrVsVc3m6iIKHPR2xR8NNZoxf3Wma3slxNuB2(this.\u0023\u003DzhUaBLeEcnz4yciN\u0024Mw\u003D\u003D.Children[num - 1]) : this.\u0023\u003Dzg0gWX4E\u003D.First<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(SciChartGroup.SomeClass34343383.\u0023\u003DzH2L3LFSqvgW9n9ynGA\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003DzH2L3LFSqvgW9n9ynGA\u003D\u003D = new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzxWddT5im7KmgxOritgrWWVg\u003D)));
      this.\u0023\u003DzrSLETkwHQoH38mnTuw\u003D\u003D = 0.0;
      this.\u0023\u003Dzerqhc5X832JL = this.\u0023\u003DzzXyefThIl4Ak.PaneElement.ActualHeight;
      this.\u0023\u003DzVi6cjZRqUP3j = this.\u0023\u003Dzerqhc5X832JL + this.\u0023\u003DzBq2lcgtVrsr_.PaneElement.ActualHeight;
      this.\u0023\u003Dzu9zFAJU\u00246XS3lPkdqi1iVcE\u003D = Mouse.GetPosition((IInputElement) this.\u0023\u003DzBq2lcgtVrsr_.PaneElement.GetWindow()).Y;
      this.\u0023\u003Dzi1vdbCxOaDS\u0024 = ((dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd) this.\u0023\u003DzzXyefThIl4Ak.PaneElement).\u0023\u003DzZcUjmkQY5ewz();
    }
    this.\u0023\u003DzrSLETkwHQoH38mnTuw\u003D\u003D = Mouse.GetPosition((IInputElement) this.\u0023\u003DzBq2lcgtVrsr_.PaneElement.GetWindow()).Y - this.\u0023\u003Dzu9zFAJU\u00246XS3lPkdqi1iVcE\u003D;
    dn1ebi1vn37xdjks.\u0023\u003DzniV7Bbk\u003D = Math.Min(this.\u0023\u003DzVi6cjZRqUP3j - this.\u0023\u003Dzi1vdbCxOaDS\u0024, Math.Max(this.\u0023\u003Dzi1vdbCxOaDS\u0024, this.\u0023\u003Dzerqhc5X832JL - this.\u0023\u003DzrSLETkwHQoH38mnTuw\u003D\u003D));
    this.\u0023\u003DzzXyefThIl4Ak.PaneElement.Height = dn1ebi1vn37xdjks.\u0023\u003DzniV7Bbk\u003D;
    if (!this.\u0023\u003DzBq2lcgtVrsr_.\u0023\u003DzR8SPjvFW2FAx())
      this.\u0023\u003DzBq2lcgtVrsr_.PaneElement.Height = this.\u0023\u003DzVi6cjZRqUP3j - dn1ebi1vn37xdjks.\u0023\u003DzniV7Bbk\u003D;
    if (!this.\u0023\u003DzzXyefThIl4Ak.IsTabbed)
      return;
    this.\u0023\u003Dzg0gWX4E\u003D.Where<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(SciChartGroup.SomeClass34343383.\u0023\u003Dz5Jr7r7tFMY9sk95iZA\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003Dz5Jr7r7tFMY9sk95iZA\u003D\u003D = new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzzupMWLepPkF5bY0sYR2kaSM\u003D))).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(new Action<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(dn1ebi1vn37xdjks.\u0023\u003DzhMGwLkEu9NzHL8WMqBTXqNM\u003D));
  }

  private void \u0023\u003Dzfjq0gu2RlSgRNsm3XA\u003D\u003D(
    object _param1,
    DragCompletedEventArgs _param2)
  {
    this.\u0023\u003DzzXyefThIl4Ak = this.\u0023\u003DzBq2lcgtVrsr_ = (\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D) null;
    this.\u0023\u003DzrSLETkwHQoH38mnTuw\u003D\u003D = this.\u0023\u003Dzerqhc5X832JL = 0.0;
  }

  private double \u0023\u003DzGmqug66Rrzv3(
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1,
    double _param2)
  {
    double actualHeight = _param1.PaneElement.ActualHeight;
    double num = Math.Min(_param2 - actualHeight, this.\u0023\u003DzBexXE3VONLO6.ActualHeight);
    return actualHeight + num;
  }

  private void \u0023\u003DztUBna_qcuT2H(
    dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd _param1)
  {
    SciChartGroup.\u0023\u003DzGiH0nC4VY3PyrCtmlxi83_s\u003D c4Vy3PyrCtmlxi83S = new SciChartGroup.\u0023\u003DzGiH0nC4VY3PyrCtmlxi83_s\u003D();
    c4Vy3PyrCtmlxi83S.\u0023\u003Dz7Ewohnc\u003D = _param1;
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D abYiNis93iXoWaCiA = this.\u0023\u003Dzg0gWX4E\u003D.FirstOrDefault<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(c4Vy3PyrCtmlxi83S.\u0023\u003Dzqz62RfBr9ikUsn4dPlrHnzM\u003D));
    if (abYiNis93iXoWaCiA == null)
      return;
    c4Vy3PyrCtmlxi83S.\u0023\u003Dz7Ewohnc\u003D.Height = abYiNis93iXoWaCiA.PaneElement.Height;
  }

  private void \u0023\u003DzHMCEsbHyKuMX(object _param1, SizeChangedEventArgs _param2)
  {
    SciChartGroup.\u0023\u003DzVqJpnDltbvS8bNvezolg25s\u003D dltbvS8bNvezolg25s = new SciChartGroup.\u0023\u003DzVqJpnDltbvS8bNvezolg25s\u003D();
    dltbvS8bNvezolg25s.\u0023\u003Dz1BK01YA\u003D = _param2;
    dltbvS8bNvezolg25s.\u0023\u003Dz7Ewohnc\u003D = _param1 as dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd;
    if (dltbvS8bNvezolg25s.\u0023\u003Dz7Ewohnc\u003D == null)
      return;
    Size size = dltbvS8bNvezolg25s.\u0023\u003Dz1BK01YA\u003D.NewSize;
    double height1 = size.Height;
    ref double local = ref height1;
    size = dltbvS8bNvezolg25s.\u0023\u003Dz1BK01YA\u003D.PreviousSize;
    double height2 = size.Height;
    if (local.CompareTo(height2) == 0 || this.\u0023\u003Dzg0gWX4E\u003D.FirstOrDefault<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(dltbvS8bNvezolg25s.\u0023\u003DzINTieIN7VYUpT8jxmoHH8es\u003D)) == null)
      return;
    this.\u0023\u003Dzg0gWX4E\u003D.Where<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(SciChartGroup.SomeClass34343383.\u0023\u003DzhuuTFZMBN_l8idp5rg\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003DzhuuTFZMBN_l8idp5rg\u003D\u003D = new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003Dzaqpe\u0024pact917Eb9JueaUdBE\u003D))).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(new Action<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(dltbvS8bNvezolg25s.\u0023\u003Dz7gcI5zh4waHqxV9qVl00zsE\u003D));
  }

  private static void \u0023\u003DznWImvCruq8sQ(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is SciChartSurface elwvdvgwnmJ5AjuaEjd))
      throw new InvalidOperationException("VerticalChartGroupProperty can only be applied to types UltrachartSurface derived types");
    string newValue = _param1.NewValue as string;
    string oldValue = _param1.OldValue as string;
    if (string.IsNullOrEmpty(newValue))
    {
      elwvdvgwnmJ5AjuaEjd.Loaded -= SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D ?? (SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D = new RoutedEventHandler(SciChartGroup.\u0023\u003DzvPNhkA9WmuO0));
      elwvdvgwnmJ5AjuaEjd.Unloaded -= SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D ?? (SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D = new RoutedEventHandler(SciChartGroup.\u0023\u003Dz0zuDFtRKqfs9));
      SciChartGroup.\u0023\u003DzC641\u0024PpiHUo22N9TFU19b5A7dw9m(elwvdvgwnmJ5AjuaEjd);
    }
    else
    {
      if (!(newValue != oldValue))
        return;
      if (!string.IsNullOrEmpty(oldValue))
        SciChartGroup.\u0023\u003DzC641\u0024PpiHUo22N9TFU19b5A7dw9m(elwvdvgwnmJ5AjuaEjd);
      elwvdvgwnmJ5AjuaEjd.Loaded -= SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D ?? (SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D = new RoutedEventHandler(SciChartGroup.\u0023\u003DzvPNhkA9WmuO0));
      elwvdvgwnmJ5AjuaEjd.Unloaded -= SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D ?? (SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D = new RoutedEventHandler(SciChartGroup.\u0023\u003Dz0zuDFtRKqfs9));
      elwvdvgwnmJ5AjuaEjd.Loaded += SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D ?? (SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D = new RoutedEventHandler(SciChartGroup.\u0023\u003DzvPNhkA9WmuO0));
      elwvdvgwnmJ5AjuaEjd.Unloaded += SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D ?? (SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D = new RoutedEventHandler(SciChartGroup.\u0023\u003Dz0zuDFtRKqfs9));
      if (!elwvdvgwnmJ5AjuaEjd.IsLoaded)
        return;
      SciChartGroup.\u0023\u003Dz3IT6iI0OoT6Opi1nPzm__kSnx9aj((ISciChartSurface) elwvdvgwnmJ5AjuaEjd, newValue);
    }
  }

  private static void \u0023\u003DzvPNhkA9WmuO0(object _param0, RoutedEventArgs _param1)
  {
    SciChartSurface elwvdvgwnmJ5AjuaEjd = _param0 as SciChartSurface;
    SciChartGroup.\u0023\u003Dz3IT6iI0OoT6Opi1nPzm__kSnx9aj((ISciChartSurface) elwvdvgwnmJ5AjuaEjd, SciChartGroup.GetVerticalChartGroup((DependencyObject) elwvdvgwnmJ5AjuaEjd));
  }

  private static void \u0023\u003Dz0zuDFtRKqfs9(object _param0, RoutedEventArgs _param1)
  {
    SciChartGroup.\u0023\u003DzC641\u0024PpiHUo22N9TFU19b5A7dw9m(_param0 as SciChartSurface);
  }

  private static void \u0023\u003Dz3IT6iI0OoT6Opi1nPzm__kSnx9aj(
    ISciChartSurface _param0,
    string _param1)
  {
    \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D key = new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D(_param0);
    if (SciChartGroup.\u0023\u003DzenwCc\u0024sVGsge.ContainsKey(key))
      return;
    SciChartGroup.\u0023\u003DzenwCc\u0024sVGsge.Add(key, _param1);
    SciChartGroup.\u0023\u003Dz3dE3KTgP\u0024pXw(_param0);
    _param0.\u0023\u003DzKPHSi1vgK\u0024Fx(SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4 ?? (SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4 = new EventHandler<EventArgs>(SciChartGroup.\u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D)));
  }

  private static void \u0023\u003DzC641\u0024PpiHUo22N9TFU19b5A7dw9m(
    SciChartSurface _param0)
  {
    foreach (KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string> keyValuePair in SciChartGroup.\u0023\u003DzenwCc\u0024sVGsge)
    {
      if (keyValuePair.Key.\u0023\u003DzUSgr9afrgN_J81TFBOEZdy4\u003D() == _param0)
        keyValuePair.Key.\u0023\u003Dz_ub4hhw\u003D();
    }
    SciChartGroup.\u0023\u003DzenwCc\u0024sVGsge.Remove(new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D((ISciChartSurface) _param0));
    _param0.\u0023\u003DzrRRdxqQwy\u0024OJ(SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4 ?? (SciChartGroup.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4 = new EventHandler<EventArgs>(SciChartGroup.\u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D)));
  }

  private static void \u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D(object _param0, EventArgs _param1)
  {
    SciChartGroup.\u0023\u003Dz3dE3KTgP\u0024pXw((ISciChartSurface) _param0);
  }

  private static void \u0023\u003Dz3dE3KTgP\u0024pXw(
    ISciChartSurface _param0)
  {
    SciChartGroup.\u0023\u003DzHcysU8drb9mTwzt9H\u0024clWJY\u003D u8drb9mTwzt9HClWjy = new SciChartGroup.\u0023\u003DzHcysU8drb9mTwzt9H\u0024clWJY\u003D();
    if (!SciChartGroup.\u0023\u003DzenwCc\u0024sVGsge.TryGetValue(new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D(_param0), out u8drb9mTwzt9HClWjy.\u0023\u003DzUI\u0024BHyM\u003D))
      return;
    \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D[] array = SciChartGroup.\u0023\u003DzenwCc\u0024sVGsge.Where<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>>(new Func<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>, bool>(u8drb9mTwzt9HClWjy.\u0023\u003DzUB2PB4j3oKnShyLauh0J0fA\u003D)).Select<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>, \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>(SciChartGroup.SomeClass34343383.\u0023\u003DzFJ\u0024eLsYNB1rWEF3RtQ\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003DzFJ\u0024eLsYNB1rWEF3RtQ\u003D\u003D = new Func<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>, \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzlOy_tGKXOJafqoVpt8CsMEw\u003D))).ToArray<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>();
    u8drb9mTwzt9HClWjy.\u0023\u003DzvoLRTxfFcUlmr5yAvA\u003D\u003D = SciChartGroup.\u0023\u003DzngzyCR\u0024m4xucYS6Umw\u003D\u003D((IEnumerable<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>) array, dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left);
    u8drb9mTwzt9HClWjy.\u0023\u003Dz\u0024cOTYk2PrdR148H8pg\u003D\u003D = SciChartGroup.\u0023\u003DzngzyCR\u0024m4xucYS6Umw\u003D\u003D((IEnumerable<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>) array, dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Right);
    ((IEnumerable<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>) array).Select<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, ISciChartSurface>(SciChartGroup.SomeClass34343383.\u0023\u003DzQqTi4Ck_kgzYUKbIuw\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003DzQqTi4Ck_kgzYUKbIuw\u003D\u003D = new Func<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, ISciChartSurface>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzBwZlGEvrpkB3v5dyRDot93k\u003D))).OfType<SciChartSurface>().\u0023\u003Dz30RSSSygABj_<SciChartSurface>(new Action<SciChartSurface>(u8drb9mTwzt9HClWjy.\u0023\u003DzBgT6HSE20ulby6RTtGwqWX8\u003D));
  }

  private static double \u0023\u003DzngzyCR\u0024m4xucYS6Umw\u003D\u003D(
    IEnumerable<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D> _param0,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param1)
  {
    IEnumerable<IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>> source = _param0.Select<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>>(new Func<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>>(new SciChartGroup.\u0023\u003Dzf1B6wwNJbrqhpNKVkrNPKgU\u003D()
    {
      \u0023\u003Dz0V69zGwQUFh\u0024 = _param1
    }.\u0023\u003DzGjtqUzQMvdrNrjgx7OasttW1sq2c));
    double? nullable;
    if (source == null)
    {
      nullable = new double?();
    }
    else
    {
      IEnumerable<double> doubles = source.Select<IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>, double>(SciChartGroup.SomeClass34343383.\u0023\u003Dz5BIbVz_RRJEtvtlEuw\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003Dz5BIbVz_RRJEtvtlEuw\u003D\u003D = new Func<IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>, double>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzdSRIGGNb5qdTUBnzqEYOgPwXxk0F)));
      nullable = doubles != null ? doubles.\u0023\u003DzAAksTMXIKE7d<double>() : new double?();
    }
    return nullable.GetValueOrDefault();
  }

  private FrameworkElement \u0023\u003DzAy0v9i5A19lM()
  {
    return this.ItemTemplate.LoadContent() as FrameworkElement;
  }

  private static void \u0023\u003Dzgxjo2OnorrBU(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    SciChartGroup nynlX7UlamwxZ2Ejd = _param0 as SciChartGroup;
    if ((bool) _param1.NewValue)
    {
      nynlX7UlamwxZ2Ejd.\u0023\u003DzSp5oAkvK2sET = new Action<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(nynlX7UlamwxZ2Ejd.\u0023\u003Dz2u\u0024j6ogJlrTF);
      nynlX7UlamwxZ2Ejd.\u0023\u003Dzg0gWX4E\u003D.Where<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(SciChartGroup.SomeClass34343383.\u0023\u003DzR3CtsoKLWrgHqpJ9ew\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003DzR3CtsoKLWrgHqpJ9ew\u003D\u003D = new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003Dzi1C5NiFzS8NjdGIJdFBfuaE\u003D))).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(new Action<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(nynlX7UlamwxZ2Ejd.\u0023\u003DzjSk6Jvc\u003D));
    }
    else
    {
      nynlX7UlamwxZ2Ejd.\u0023\u003DzSp5oAkvK2sET = new Action<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(nynlX7UlamwxZ2Ejd.\u0023\u003Dz\u0024TRcYUKCOmDd);
      nynlX7UlamwxZ2Ejd.\u0023\u003Dzg0gWX4E\u003D.Where<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(SciChartGroup.SomeClass34343383.\u0023\u003DzF5CGbn39w_V5rzPhsg\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003DzF5CGbn39w_V5rzPhsg\u003D\u003D = new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzP2L\u0024nb5keqxbUbKBSg38m\u0024w\u003D))).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(new Action<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(nynlX7UlamwxZ2Ejd.\u0023\u003DzjSk6Jvc\u003D));
    }
  }

  public event PropertyChangedEventHandler PropertyChanged;

  private void \u0023\u003Dz15moWio\u003D(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog((object) this, new PropertyChangedEventArgs(_param1));
  }

  internal List<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D> Panes
  {
    get => this.\u0023\u003Dzg0gWX4E\u003D;
  }

  private void \u0023\u003DzLngL5eSRzHKM7HW4zMEg1WQ\u003D(
    IScichartSurfaceVM _param1)
  {
    this.\u0023\u003Dz7IDM9DY\u003D(_param1);
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly SciChartGroup.SomeClass34343383 SomeMethond0343 = new SciChartGroup.SomeClass34343383();
    public static Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool> \u0023\u003Dzje8bZnYPCcLUKo6EWw\u003D\u003D;
    public static Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool> \u0023\u003DzH2L3LFSqvgW9n9ynGA\u003D\u003D;
    public static Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool> \u0023\u003Dz5Jr7r7tFMY9sk95iZA\u003D\u003D;
    public static Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool> \u0023\u003DzhuuTFZMBN_l8idp5rg\u003D\u003D;
    public static Func<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>, \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D> \u0023\u003DzFJ\u0024eLsYNB1rWEF3RtQ\u003D\u003D;
    public static Func<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, ISciChartSurface> \u0023\u003DzQqTi4Ck_kgzYUKbIuw\u003D\u003D;
    public static Func<double, dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd, double> \u0023\u003DzQo1YXL32S5xfzg99Jw\u003D\u003D;
    public static Func<IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>, double> \u0023\u003Dz5BIbVz_RRJEtvtlEuw\u003D\u003D;
    public static Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool> \u0023\u003DzR3CtsoKLWrgHqpJ9ew\u003D\u003D;
    public static Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool> \u0023\u003DzF5CGbn39w_V5rzPhsg\u003D\u003D;

    internal bool \u0023\u003DzAVi2aBRehqWFfXq9jYMUhjI\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.\u0023\u003DzR8SPjvFW2FAx();
    }

    internal bool \u0023\u003DzxWddT5im7KmgxOritgrWWVg\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.\u0023\u003DzR8SPjvFW2FAx();
    }

    internal bool \u0023\u003DzzupMWLepPkF5bY0sYR2kaSM\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.IsTabbed;
    }

    internal bool \u0023\u003Dzaqpe\u0024pact917Eb9JueaUdBE\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.IsTabbed;
    }

    internal \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D \u0023\u003DzlOy_tGKXOJafqoVpt8CsMEw\u003D(
      KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string> _param1)
    {
      return _param1.Key;
    }

    internal ISciChartSurface \u0023\u003DzBwZlGEvrpkB3v5dyRDot93k\u003D(
      \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D _param1)
    {
      return _param1.\u0023\u003DzUSgr9afrgN_J81TFBOEZdy4\u003D();
    }

    internal double \u0023\u003DzdSRIGGNb5qdTUBnzqEYOgPwXxk0F(
      IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd> _param1)
    {
      return _param1 == null ? 0.0 : _param1.Aggregate<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd, double>(0.0, SciChartGroup.SomeClass34343383.\u0023\u003DzQo1YXL32S5xfzg99Jw\u003D\u003D ?? (SciChartGroup.SomeClass34343383.\u0023\u003DzQo1YXL32S5xfzg99Jw\u003D\u003D = new Func<double, dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd, double>(SciChartGroup.SomeClass34343383.SomeMethond0343.\u0023\u003DzfNlexRlqK5oJ_TYB1GyrgH8eZql\u0024)));
    }

    internal double \u0023\u003DzfNlexRlqK5oJ_TYB1GyrgH8eZql\u0024(
      double _param1,
      dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd _param2)
    {
      return _param1 + _param2.ActualWidth;
    }

    internal bool \u0023\u003Dzi1C5NiFzS8NjdGIJdFBfuaE\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return !_param1.IsTabbed && !_param1.\u0023\u003DzR8SPjvFW2FAx();
    }

    internal bool \u0023\u003DzP2L\u0024nb5keqxbUbKBSg38m\u0024w\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.IsTabbed && !_param1.\u0023\u003DzR8SPjvFW2FAx();
    }
  }

  private sealed class \u0023\u003DzBsvr5yEJlfn\u0024m5gyTVsnSFo\u003D
  {
    public UIElement \u0023\u003Dzs1FLlYI\u003D;

    internal bool \u0023\u003DzyS1WpzHcC2OUisDR6XFERGQ\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.PaneElement == this.\u0023\u003Dzs1FLlYI\u003D;
    }
  }

  private sealed class \u0023\u003DzGiH0nC4VY3PyrCtmlxi83_s\u003D
  {
    public dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd \u0023\u003Dz7Ewohnc\u003D;

    internal bool \u0023\u003Dzqz62RfBr9ikUsn4dPlrHnzM\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.IsTabbed && _param1.PaneElement != this.\u0023\u003Dz7Ewohnc\u003D;
    }
  }

  private sealed class \u0023\u003DzHcysU8drb9mTwzt9H\u0024clWJY\u003D
  {
    public string \u0023\u003DzUI\u0024BHyM\u003D;
    public double \u0023\u003DzvoLRTxfFcUlmr5yAvA\u003D\u003D;
    public double \u0023\u003Dz\u0024cOTYk2PrdR148H8pg\u003D\u003D;

    internal bool \u0023\u003DzUB2PB4j3oKnShyLauh0J0fA\u003D(
      KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string> _param1)
    {
      return _param1.Value == this.\u0023\u003DzUI\u0024BHyM\u003D;
    }

    internal void \u0023\u003DzBgT6HSE20ulby6RTtGwqWX8\u003D(
      SciChartSurface _param1)
    {
      if (_param1.\u0023\u003Dz7jQDxh7oPk4IxxFHHA\u003D\u003D() != null)
        _param1.\u0023\u003Dz7jQDxh7oPk4IxxFHHA\u003D\u003D().Margin = new Thickness(this.\u0023\u003DzvoLRTxfFcUlmr5yAvA\u003D\u003D - _param1.\u0023\u003Dz7jQDxh7oPk4IxxFHHA\u003D\u003D().ActualWidth, 0.0, 0.0, 0.0);
      if (_param1.\u0023\u003DzxzuKb2Lb2Gtlzy1zQA\u003D\u003D() == null)
        return;
      _param1.\u0023\u003DzxzuKb2Lb2Gtlzy1zQA\u003D\u003D().Margin = new Thickness(0.0, 0.0, this.\u0023\u003Dz\u0024cOTYk2PrdR148H8pg\u003D\u003D - _param1.\u0023\u003DzxzuKb2Lb2Gtlzy1zQA\u003D\u003D().ActualWidth, 0.0);
    }
  }

  private sealed class \u0023\u003DzIkfJv_ww2IEiAf0VERJZev4\u003D
  {
    public \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D \u0023\u003DzKveAMLo\u003D;

    internal bool \u0023\u003Dz240YAiB2jNus8hcG9w\u003D\u003D(TabItem _param1)
    {
      return _param1.Content.Equals((object) this.\u0023\u003DzKveAMLo\u003D.PaneElement);
    }
  }

  private sealed class \u0023\u003DzVqJpnDltbvS8bNvezolg25s\u003D
  {
    public dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd \u0023\u003Dz7Ewohnc\u003D;
    public SizeChangedEventArgs \u0023\u003Dz1BK01YA\u003D;

    internal bool \u0023\u003DzINTieIN7VYUpT8jxmoHH8es\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.PaneElement == this.\u0023\u003Dz7Ewohnc\u003D && _param1.IsTabbed;
    }

    internal void \u0023\u003Dz7gcI5zh4waHqxV9qVl00zsE\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      _param1.PaneElement.Height = this.\u0023\u003Dz1BK01YA\u003D.NewSize.Height;
    }
  }

  private sealed class \u0023\u003DzbQEIVhBRscvmjvQLdNZzKp4\u003D
  {
    public SciChartGroup _variableSome3535;
    public \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D \u0023\u003DzKveAMLo\u003D;

    internal void \u0023\u003DzJeMUirilpUA5DzmAPQ\u003D\u003D()
    {
      this._variableSome3535.\u0023\u003DzjSk6Jvc\u003D(this.\u0023\u003DzKveAMLo\u003D);
    }
  }

  private sealed class \u0023\u003Dzf1B6wwNJbrqhpNKVkrNPKgU\u003D
  {
    public dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd \u0023\u003Dz0V69zGwQUFh\u0024;
    public Func<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd, bool> \u0023\u003DzoD2HtVGZvKav;

    internal IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd> \u0023\u003DzGjtqUzQMvdrNrjgx7OasttW1sq2c(
      \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D _param1)
    {
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D yaxes = _param1.\u0023\u003DzUSgr9afrgN_J81TFBOEZdy4\u003D().get_YAxes();
      return yaxes == null ? (IEnumerable<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>) null : yaxes.OfType<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>().Where<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd>(this.\u0023\u003DzoD2HtVGZvKav ?? (this.\u0023\u003DzoD2HtVGZvKav = new Func<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd, bool>(this.\u0023\u003DzOaW1WOaSuiY3z\u0024u5DGB4v5mpnjoA)));
    }

    internal bool \u0023\u003DzOaW1WOaSuiY3z\u0024u5DGB4v5mpnjoA(
      dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd _param1)
    {
      return _param1.AxisAlignment == this.\u0023\u003Dz0V69zGwQUFh\u0024;
    }
  }

  private sealed class \u0023\u003DzgEL8Rs_dn1ebi1vn37xdjks\u003D
  {
    public SciChartGroup _variableSome3535;
    public double \u0023\u003DzniV7Bbk\u003D;

    internal \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D \u0023\u003Dq1qGQwgs_F6ae19p24dqRj6GgdRzMrVsVc3m6iIKHPR2xR8NNZoxf3Wma3slxNuB2(
      UIElement _param1)
    {
      return this._variableSome3535.\u0023\u003Dzg0gWX4E\u003D.First<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>(new Func<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D, bool>(new SciChartGroup.\u0023\u003DzBsvr5yEJlfn\u0024m5gyTVsnSFo\u003D()
      {
        \u0023\u003Dzs1FLlYI\u003D = _param1
      }.\u0023\u003DzyS1WpzHcC2OUisDR6XFERGQ\u003D));
    }

    internal void \u0023\u003DzhMGwLkEu9NzHL8WMqBTXqNM\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      _param1.PaneElement.Height = this.\u0023\u003DzniV7Bbk\u003D;
    }
  }

  private static class \u0023\u003Dzj2IY6aE\u003D
  {
    public static RoutedEventHandler \u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D;
    public static RoutedEventHandler \u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D;
    public static EventHandler<EventArgs> \u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4;
  }

  private sealed class \u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D
  {
    public IScichartSurfaceVM _someChartElement;

    internal bool \u0023\u003Dz_0lW5QtpqHwetfa5Xg\u003D\u003D(
      \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D _param1)
    {
      return _param1.PaneViewModel.Equals((object) this._someChartElement);
    }
  }
}
