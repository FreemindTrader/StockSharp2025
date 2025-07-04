// Decompiled with JetBrains decompiler
// Type: -.dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

#nullable enable
namespace \u002D;

internal sealed class dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd
{
  public static readonly 
  #nullable disable
  DependencyProperty \u0023\u003Dz\u0024fIrKTSXQyu0 = DependencyProperty.RegisterAttached("ItemsControlParentSurface", typeof (ItemsControl), typeof (dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.\u0023\u003DzfboVa5iAInKu)));

  public static void SetItemsControlParentSurface(UIElement _param0, ItemsControl _param1)
  {
    _param0.SetValue(dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.\u0023\u003Dz\u0024fIrKTSXQyu0, (object) _param1);
  }

  public static ItemsControl GetItemsControlParentSurface(UIElement _param0)
  {
    return (ItemsControl) _param0.GetValue(dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.\u0023\u003Dz\u0024fIrKTSXQyu0);
  }

  private static void \u0023\u003DzfboVa5iAInKu(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.\u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D u5Svx6MhYdSkOpoa = new dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.\u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D();
    u5Svx6MhYdSkOpoa.\u0023\u003DzbWUE4QnsAHzF0cRUyQ\u003D\u003D = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd) _param0;
    u5Svx6MhYdSkOpoa.\u0023\u003Dzki8twMk\u003D = (ItemsControl) _param1.NewValue;
    u5Svx6MhYdSkOpoa.\u0023\u003Dzki8twMk\u003D.Loaded += new RoutedEventHandler(u5Svx6MhYdSkOpoa.\u0023\u003Dz0xfb5\u0024uSJ4miTqDRzdsXVbY\u003D);
  }

  private static void \u0023\u003DzgCXh\u0024GcoJtLk(
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd _param0,
    ItemsControl _param1)
  {
    PropertyInfo propertyInfo = ((IEnumerable<PropertyInfo>) ((object) _param1).GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)).FirstOrDefault<PropertyInfo>(dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.SomeClass34343383.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D ?? (dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.SomeClass34343383.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D = new Func<PropertyInfo, bool>(dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DznZr9nJa8\u00246cBEfGURUqenkw\u003D)));
    if (propertyInfo == (PropertyInfo) null)
      return;
    List<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D> abYiNis93iXoWaCiAList = (List<\u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D>) propertyInfo.GetValue((object) _param1, (object[]) null);
    if (abYiNis93iXoWaCiAList == null || abYiNis93iXoWaCiAList.Count == 0)
      return;
    SciChartSurface visualChild = abYiNis93iXoWaCiAList[0].PaneElement.FindVisualChild<SciChartSurface>();
    _param0.ParentSurface = (ISciChartSurface) visualChild;
    // ISSUE: explicit non-virtual call
    _param0.DataSeries = visualChild != null ? __nonvirtual (visualChild.RenderableSeries).FirstOrDefault<IRenderableSeries>()?.get_DataSeries() : (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) null;
  }

  private sealed class \u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D
  {
    public ItemsControl \u0023\u003Dzki8twMk\u003D;
    public dje_zT5LWWY2ES5P78EADY3KXQ8WJ3YWFSQBLBPMNGFPE3EC5F696R6DA4LBDRF4Q_ejd \u0023\u003DzbWUE4QnsAHzF0cRUyQ\u003D\u003D;
    public NotifyCollectionChangedEventHandler \u0023\u003DzuAeZVTPDgzYE;

    internal void \u0023\u003Dz0xfb5\u0024uSJ4miTqDRzdsXVbY\u003D(
      object _param1,
      RoutedEventArgs _param2)
    {
      if (this.\u0023\u003Dzki8twMk\u003D.ItemsSource is INotifyCollectionChanged itemsSource)
      {
        NotifyCollectionChangedEventHandler changedEventHandler = this.\u0023\u003DzuAeZVTPDgzYE ?? (this.\u0023\u003DzuAeZVTPDgzYE = new NotifyCollectionChangedEventHandler(this.\u0023\u003DzlOmNQalRcYS04kULyvkLsQQ\u003D));
        itemsSource.CollectionChanged += changedEventHandler;
      }
      dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.\u0023\u003DzgCXh\u0024GcoJtLk(this.\u0023\u003DzbWUE4QnsAHzF0cRUyQ\u003D\u003D, this.\u0023\u003Dzki8twMk\u003D);
    }

    internal void \u0023\u003DzlOmNQalRcYS04kULyvkLsQQ\u003D(
      #nullable enable
      object? _param1,
      NotifyCollectionChangedEventArgs _param2)
    {
      dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.\u0023\u003DzgCXh\u0024GcoJtLk(this.\u0023\u003DzbWUE4QnsAHzF0cRUyQ\u003D\u003D, this.\u0023\u003Dzki8twMk\u003D);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly 
    #nullable disable
    dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.SomeClass34343383 SomeMethond0343 = new dje_zR8DJERVYW3NHVYV462QZ339FHABYL8NEK6A3454HWSHNTFSSZXTJ63928QBSEB5EHR8WMD3J_ejd.SomeClass34343383();
    public static Func<PropertyInfo, bool> \u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D;

    internal bool \u0023\u003DznZr9nJa8\u00246cBEfGURUqenkw\u003D(PropertyInfo _param1)
    {
      return _param1.Name == "Panes";
    }
  }
}
