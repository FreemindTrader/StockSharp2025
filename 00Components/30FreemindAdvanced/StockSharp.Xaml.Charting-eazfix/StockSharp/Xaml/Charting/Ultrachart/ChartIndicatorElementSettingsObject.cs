// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Ultrachart.ChartIndicatorElementSettingsObject
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.PropertyGrid;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace StockSharp.Xaml.Charting.Ultrachart;

public class ChartIndicatorElementSettingsObject : ChartSettingsObjectBase<IChartElement>
{
  public ChartIndicatorElementSettingsObject(IChartElement element)
    : base(element)
  {
    this.CategoriesMode = CategoriesShowMode.Hidden;
    this.Orig.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
  }

  protected override PropertyDescriptor[] OnGetProperties(IChartElement element)
  {
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    if (element is IChartIndicatorElement element1)
    {
      IIndicator indicator = element1.TryGetIndicator();
      if (indicator != null)
        propertyDescriptorList.Add(IndicatorSettingsObject.GetPropertyDescriptor(indicator.Name, (object) this, indicator));
    }
    propertyDescriptorList.Add(ChartComponentElementSettings.GetPropertyDescriptor(LocalizedStrings.Style, (object) this, (IChartComponent) element, new Func<IChartComponent, PropertyDescriptor, bool>(ChartIndicatorElementSettingsObject.StaticMethod0348)));
    propertyDescriptorList.Add(ChartComponentElementSettings.GetPropertyDescriptor(LocalizedStrings.Common, (object) this, (IChartComponent) element, new Func<IChartComponent, PropertyDescriptor, bool>(ChartIndicatorElementSettingsObject.StaticMethod0983)));
    return propertyDescriptorList.ToArray();
  }

  private void OnPropertyChanged(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    this.NotifyChanged(_param2.PropertyName);
  }

  public static bool StaticMethod0348(
    #nullable disable
    IChartComponent _param0,
    PropertyDescriptor _param1)
  {
    BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
    if ((browsableAttribute != null ? (browsableAttribute.Browsable ? 1 : 0) : 1) != 0)
    {
      Attribute0 iybwdMa94Ybky5Phw = _param1.Attributes.OfType<Attribute0>().FirstOrDefault<Attribute0>();
      if ((iybwdMa94Ybky5Phw != null ? (!iybwdMa94Ybky5Phw.\u0023\u003DzOJt8kfGtb6vl() ? 1 : 0) : 1) != 0)
        return _param0.ParentElement != null || !(_param1.Name == "IsVisible");
    }
    return false;
  }

  public static bool StaticMethod0983(
    IChartComponent _param0,
    PropertyDescriptor _param1)
  {
    if (_param0.ParentElement != null)
      return false;
    if (_param1.Name == "IsVisible")
      return true;
    BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
    if ((browsableAttribute != null ? (browsableAttribute.Browsable ? 1 : 0) : 1) == 0)
      return false;
    Attribute0 iybwdMa94Ybky5Phw = _param1.Attributes.OfType<Attribute0>().FirstOrDefault<Attribute0>();
    return iybwdMa94Ybky5Phw != null && iybwdMa94Ybky5Phw.\u0023\u003DzOJt8kfGtb6vl();
  }
}
