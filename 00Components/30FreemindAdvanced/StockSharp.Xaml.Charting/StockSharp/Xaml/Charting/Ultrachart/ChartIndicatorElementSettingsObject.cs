// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Ultrachart.ChartIndicatorElementSettingsObject
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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

/// <summary>
/// Proxy object to edit chart indicator element in property grid.
/// </summary>
public class ChartIndicatorElementSettingsObject : ChartSettingsObjectBase<
#nullable disable
IChartElement>
{
  /// <summary>Create instance.</summary>
  /// <param name="element">Element.</param>
  public ChartIndicatorElementSettingsObject(IChartElement element)
    : base(element)
  {
    this.CategoriesMode = CategoriesShowMode.Hidden;
    this.Orig.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzzgN9T5XSXDaBdSK82A\u003D\u003D);
  }

  /// <inheritdoc />
  protected override PropertyDescriptor[] OnGetProperties(IChartElement element)
  {
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    if (element is IChartIndicatorElement element1)
    {
      IIndicator indicator = element1.TryGetIndicator();
      if (indicator != null)
        propertyDescriptorList.Add(\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003DzANqI1s0\u003D(indicator.Name, (object) this, indicator));
    }
    propertyDescriptorList.Add(\u0023\u003DzzSV9ePAnJ860K\u0024gH7z\u0024ORjrmDYZJziML8SgUAERSAV0\u0024lfQskg\u003D\u003D.\u0023\u003DzANqI1s0\u003D(LocalizedStrings.Style, (object) this, (IfxChartElement) element, new Func<IfxChartElement, PropertyDescriptor, bool>(ChartIndicatorElementSettingsObject.\u0023\u003DqnUZLhteM7X58WNtOFoAUCxXrehbkyL7b_5OgJmSQR7E0sLjriPtaNbpo_\u0024ilyRhu)));
    propertyDescriptorList.Add(\u0023\u003DzzSV9ePAnJ860K\u0024gH7z\u0024ORjrmDYZJziML8SgUAERSAV0\u0024lfQskg\u003D\u003D.\u0023\u003DzANqI1s0\u003D(LocalizedStrings.Common, (object) this, (IfxChartElement) element, new Func<IfxChartElement, PropertyDescriptor, bool>(ChartIndicatorElementSettingsObject.\u0023\u003DqUgnHZrrykv2VtgSnmyAJ\u0024t24diQG2HPY7YrznSErRWXmOLZGnAaI\u0024eZpwOBQLLof)));
    return propertyDescriptorList.ToArray();
  }

  private void \u0023\u003DzzgN9T5XSXDaBdSK82A\u003D\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    this.NotifyChanged(_param2.PropertyName);
  }

  internal static bool \u0023\u003DqnUZLhteM7X58WNtOFoAUCxXrehbkyL7b_5OgJmSQR7E0sLjriPtaNbpo_\u0024ilyRhu(
    #nullable disable
    IfxChartElement _param0,
    PropertyDescriptor _param1)
  {
    BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
    if ((browsableAttribute != null ? (browsableAttribute.Browsable ? 1 : 0) : 1) != 0)
    {
      \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D iybwdMa94Ybky5Phw = _param1.Attributes.OfType<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D>().FirstOrDefault<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D>();
      if ((iybwdMa94Ybky5Phw != null ? (!iybwdMa94Ybky5Phw.\u0023\u003DzOJt8kfGtb6vl() ? 1 : 0) : 1) != 0)
        return _param0.ParentElement != null || !(_param1.Name == "");
    }
    return false;
  }

  internal static bool \u0023\u003DqUgnHZrrykv2VtgSnmyAJ\u0024t24diQG2HPY7YrznSErRWXmOLZGnAaI\u0024eZpwOBQLLof(
    IfxChartElement _param0,
    PropertyDescriptor _param1)
  {
    if (_param0.ParentElement != null)
      return false;
    if (_param1.Name == "")
      return true;
    BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
    if ((browsableAttribute != null ? (browsableAttribute.Browsable ? 1 : 0) : 1) == 0)
      return false;
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D iybwdMa94Ybky5Phw = _param1.Attributes.OfType<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D>().FirstOrDefault<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D>();
    return iybwdMa94Ybky5Phw != null && iybwdMa94Ybky5Phw.\u0023\u003DzOJt8kfGtb6vl();
  }
}
