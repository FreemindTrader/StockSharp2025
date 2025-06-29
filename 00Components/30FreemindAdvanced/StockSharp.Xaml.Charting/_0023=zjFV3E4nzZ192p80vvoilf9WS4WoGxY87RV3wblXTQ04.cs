// Decompiled with JetBrains decompiler
// Type: #=zjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

#nullable enable
internal static class \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D
{
  public static 
  #nullable disable
  dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd \u0023\u003Dz68iph80\u003D(
    this IChartAxis _param0,
    ICommand _param1,
    ICommand _param2,
    IChart _param3)
  {
    \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D vbxLeArTkallkIdHg1 = new \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D();
    vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D = _param0;
    vbxLeArTkallkIdHg1.\u0023\u003Dzi8fyBqQ\u003D = _param1;
    \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003DzmD295WW4skLs1H\u0024zBQ\u003D\u003D d295Ww4skLs1HZBq = new \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003DzmD295WW4skLs1H\u0024zBQ\u003D\u003D();
    d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D = vbxLeArTkallkIdHg1;
    switch (d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D.AxisType)
    {
      case ChartAxisType.DateTime:
        d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) new dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd();
        break;
      case ChartAxisType.CategoryDateTime:
        \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D ulUkgXkBneEwzP3h0_1 = d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;
        dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd nu9622VfydaypdeqEjd = new dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd();
        nu9622VfydaypdeqEjd.LabelProvider = (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH) new \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D(_param3, d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D);
        nu9622VfydaypdeqEjd.TickProvider = (\u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3tOndLKZk0aGELbzVxE4VJ9A) new \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dzg7H9cRyacWHB();
        ulUkgXkBneEwzP3h0_1.\u0023\u003DzCejzG6Y\u003D = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) nu9622VfydaypdeqEjd;
        d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd.\u0023\u003Dz5Kre9LKvddWFL51pIQ\u003D\u003D, (object) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433076));
        break;
      case ChartAxisType.Numeric:
        bool flag = ((ICollection<IChartAxis>) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D.ChartArea.XAxises).Contains(d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D);
        d295Ww4skLs1HZBq.\u0023\u003DzZBtfoxHC6iyc = new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(0.0, flag ? 0.0 : 0.05);
        \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D ulUkgXkBneEwzP3h0_2 = d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;
        dje_zX3N6Q9TLRHQ6K6VPWM4NP8NG2M44BK7RCDU397R9MRFV9SQ7FUGGE_ejd mrfV9Sq7FuggeEjd = new dje_zX3N6Q9TLRHQ6K6VPWM4NP8NG2M44BK7RCDU397R9MRFV9SQ7FUGGE_ejd();
        mrfV9Sq7FuggeEjd.GrowBy = (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double>) d295Ww4skLs1HZBq.\u0023\u003DzZBtfoxHC6iyc;
        ulUkgXkBneEwzP3h0_2.\u0023\u003DzCejzG6Y\u003D = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) mrfV9Sq7FuggeEjd;
        d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzhYW6tiLSC0eZ, (object) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433838));
        d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzkAJ7QX7sBa0Q, (object) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433005));
        if (!flag)
        {
          d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.MouseDoubleClick += new MouseButtonEventHandler(d295Ww4skLs1HZBq.\u0023\u003Dz4EeJ2Z9T93ppthogLg\u003D\u003D);
          break;
        }
        break;
      default:
        throw new ArgumentOutOfRangeException(XXX.SSS(-539328516), (object) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D.AxisType, LocalizedStrings.InvalidValue);
    }
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.Tag = (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D;
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzD\u0024wXQ8E\u003D, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539431122));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzKm2_RDWENeyO, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433854), converter: (IValueConverter) new \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003DzHNYnjAF9C6pAx9qdjQ\u003D\u003D());
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz\u0024geG9XF9qNM9, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433188));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzAaZ_8g9ACaldVRhq\u0024w\u003D\u003D, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433145));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz6E17UGyH3Hxe, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433096));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzqdrX3c4RyinxmAyEug\u003D\u003D, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433163));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzHxEy7A8kQeb2, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433170));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzALRRz3KBB3Uz, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539432975));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzfMY988N0StOA, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433797), converter: (IValueConverter) new \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003DzEAflM12ss7VY(((ICollection<IChartAxis>) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D.ChartArea.XAxises).Contains(vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D)));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(UIElement.VisibilityProperty, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D, XXX.SSS(-539433813), converter: (IValueConverter) new BoolToVisibilityConverter());
    ControlTemplate controlTemplate1 = new ControlTemplate();
    controlTemplate1.VisualTree = new FrameworkElementFactory(typeof (PropertyGridEx));
    ControlTemplate controlTemplate2 = controlTemplate1;
    controlTemplate2.VisualTree.SetValue(FrameworkElement.WidthProperty, (object) 300.0);
    controlTemplate2.VisualTree.SetValue(FrameworkElement.HeightProperty, (object) 400.0);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.ShowCategoriesProperty, (object) CategoriesShowMode.Hidden);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.ShowMenuButtonInRowsProperty, (object) false);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.ShowToolPanelProperty, (object) false);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.ShowSearchBoxProperty, (object) false);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.SelectedObjectProperty, (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D);
    \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D vbxLeArTkallkIdHg2 = vbxLeArTkallkIdHg1;
    ContentControl contentControl = new ContentControl();
    contentControl.Template = controlTemplate2;
    vbxLeArTkallkIdHg2.\u0023\u003Dzs1tPnmG2z8D2 = contentControl;
    PopupMenu popupMenu1 = new PopupMenu();
    CommonBarItemCollection items1 = popupMenu1.Items;
    BarSplitButtonItem barSplitButtonItem = new BarSplitButtonItem();
    barSplitButtonItem.Glyph = ThemedIconsExtension.GetImage(XXX.SSS(-539328527));
    barSplitButtonItem.ActAsDropDown = true;
    barSplitButtonItem.Content = (object) LocalizedStrings.Properties;
    barSplitButtonItem.PopupControl = (IPopupControl) new PopupControlContainer()
    {
      Content = (UIElement) vbxLeArTkallkIdHg1.\u0023\u003Dzs1tPnmG2z8D2
    };
    items1.Add((IBarItem) barSplitButtonItem);
    PopupMenu popupMenu2 = popupMenu1;
    if (vbxLeArTkallkIdHg1.\u0023\u003Dzi8fyBqQ\u003D != null)
    {
      CommonBarItemCollection items2 = popupMenu2.Items;
      BarButtonItem barButtonItem = new BarButtonItem();
      barButtonItem.Glyph = ThemedIconsExtension.GetImage(XXX.SSS(-539328570));
      barButtonItem.Content = (object) LocalizedStrings.Delete;
      barButtonItem.Command = vbxLeArTkallkIdHg1.\u0023\u003Dzi8fyBqQ\u003D;
      barButtonItem.CommandParameter = (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D;
      barButtonItem.CommandTarget = (IInputElement) vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D;
      items2.Add((IBarItem) barButtonItem);
    }
    if (vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D.AxisType == ChartAxisType.CategoryDateTime && _param2 != null)
    {
      CommonBarItemCollection items3 = popupMenu2.Items;
      BarButtonItem barButtonItem = new BarButtonItem();
      barButtonItem.Glyph = ThemedIconsExtension.GetImage(XXX.SSS(-539328556));
      barButtonItem.Content = (object) LocalizedStrings.ResetTimeZone;
      barButtonItem.Command = _param2;
      barButtonItem.CommandParameter = (object) vbxLeArTkallkIdHg1.\u0023\u003Dzfl\u0024A1s0\u003D;
      barButtonItem.CommandTarget = (IInputElement) vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D;
      items3.Add((IBarItem) barButtonItem);
    }
    vbxLeArTkallkIdHg1.\u0023\u003Dzan5_\u0024vA\u003D = (PropertyGridEx) null;
    popupMenu2.Opening += new CancelEventHandler(vbxLeArTkallkIdHg1.\u0023\u003DzQGBHhFhQ7lQdReeBKA\u003D\u003D);
    BarManager.SetDXContextMenu((UIElement) vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D, (IPopupControl) popupMenu2);
    return vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D;
  }

  private sealed class \u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D
  {
    public IChartAxis \u0023\u003Dzfl\u0024A1s0\u003D;
    public dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd \u0023\u003DzCejzG6Y\u003D;
    public PropertyGridEx \u0023\u003Dzan5_\u0024vA\u003D;
    public ContentControl \u0023\u003Dzs1tPnmG2z8D2;
    public ICommand \u0023\u003Dzi8fyBqQ\u003D;

    internal void \u0023\u003DzQGBHhFhQ7lQdReeBKA\u003D\u003D(
      #nullable enable
      object? _param1,
      CancelEventArgs _param2)
    {
      if (this.\u0023\u003Dzan5_\u0024vA\u003D == null)
        this.\u0023\u003Dzan5_\u0024vA\u003D = this.\u0023\u003Dzs1tPnmG2z8D2.FindVisualChild<PropertyGridEx>();
      if (this.\u0023\u003Dzan5_\u0024vA\u003D != null)
      {
        this.\u0023\u003Dzan5_\u0024vA\u003D.SelectedObject = (object) null;
        this.\u0023\u003Dzan5_\u0024vA\u003D.SelectedObject = (object) this.\u0023\u003Dzfl\u0024A1s0\u003D;
      }
      ((DelegateCommand<ChartAxis>) this.\u0023\u003Dzi8fyBqQ\u003D)?.RaiseCanExecuteChanged();
    }
  }

  private sealed class \u0023\u003DzEAflM12ss7VY(bool _param1) : IValueConverter
  {
    
    private readonly bool \u0023\u003DzlG_k5f4\u003D = _param1;

    public 
    #nullable disable
    object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
      return (object) (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) ((_param1 as bool?).GetValueOrDefault() ? (this.\u0023\u003DzlG_k5f4\u003D ? 2 : 1) : (this.\u0023\u003DzlG_k5f4\u003D ? 3 : 0));
    }

    public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
      dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd demydmpA2K68QEjd = (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) _param1;
      return (object) (bool) (demydmpA2K68QEjd == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Default ? 0 : (this.\u0023\u003DzlG_k5f4\u003D ? (demydmpA2K68QEjd != dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom ? 1 : 0) : (demydmpA2K68QEjd != 0 ? 1 : 0)));
    }
  }

  private sealed class \u0023\u003DzHNYnjAF9C6pAx9qdjQ\u003D\u003D : IValueConverter
  {
    public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
      return (object) (dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd) ((bool) _param1 ? 1 : 0);
    }

    public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
      return (object) (bool) (!(_param1 is dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd qctsxU83FdjrbtjvEjd) ? 0 : (qctsxU83FdjrbtjvEjd == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always ? 1 : 0));
    }
  }

  public sealed class \u0023\u003Dzg7H9cRyacWHB : 
    \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4tpH35HeyDPseaiYdk7NQiMjk
  {
    private \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY \u0023\u003Dz41CkRXA\u003D;
    private \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D \u0023\u003DzBs9H8NO5qLL8;

    public void \u0023\u003DzI_kEht21kBsX()
    {
      this.\u0023\u003Dz41CkRXA\u003D = ((dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) this.\u0023\u003DzHZDgUSdfqmkx()).\u0023\u003Dz0RktzzbyC\u002468();
    }

    public override double[] \u0023\u003Dzctqa9kMCtfQQ(
      \u0023\u003Dz6SSn5QQkepq6NeBmeacJnAoj7IAxnW4w0PxdsBxUKNwS _param1)
    {
      \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D visibleRange = _param1.VisibleRange as \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D;
      double majorDelta = (double) _param1.get_MajorDelta();
      if ((visibleRange != null ? (!visibleRange.IsDefined ? 1 : 0) : 1) != 0)
        return base.\u0023\u003Dzctqa9kMCtfQQ(_param1);
      \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ znUzlqIn9ReXl = this.\u0023\u003Dz41CkRXA\u003D.\u0023\u003DznUzlqIN9ReXL;
      \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D zToxB29CkMiO6 = this.\u0023\u003Dz41CkRXA\u003D.\u0023\u003DzToxB29CkMiO6;
      return (zToxB29CkMiO6 != null ? (!zToxB29CkMiO6.IsDefined ? 1 : 0) : 1) != 0 || znUzlqIn9ReXl == null || znUzlqIn9ReXl.\u0023\u003DzlpVGw6E\u003D() == 0 ? base.\u0023\u003Dzctqa9kMCtfQQ(_param1) : this.\u0023\u003DzyPl0NtN\u0024cLlA(visibleRange, majorDelta) ?? base.\u0023\u003Dzctqa9kMCtfQQ(_param1);
    }

    private int \u0023\u003DzFdyJa7S1Nub8(DateTime _param1, DateTime _param2)
    {
      _param1 = this.\u0023\u003DzHZDgUSdfqmkx().get_LabelProvider() is \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D labelProvider1 ? labelProvider1.\u0023\u003Dz_pGtZJ\u002426fjw((IComparable) _param1) : _param1;
      _param2 = this.\u0023\u003DzHZDgUSdfqmkx().get_LabelProvider() is \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D labelProvider2 ? labelProvider2.\u0023\u003Dz_pGtZJ\u002426fjw((IComparable) _param2) : _param2;
      if (_param1.Date != _param2.Date)
        return 3;
      return _param1.Hour == _param2.Hour ? (_param1.Second == 0 ? 1 : 0) : 2;
    }

    public bool \u0023\u003DzmCaFJJFAOMCC(IComparable _param1)
    {
      if (this.\u0023\u003DzBs9H8NO5qLL8 == null)
        return false;
      int num = this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzFk6sufr\u0024co4e(_param1);
      DateTime dateTime = this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num - 1);
      return num == 0 || this.\u0023\u003DzFdyJa7S1Nub8((DateTime) _param1, dateTime) == 3;
    }

    private double[] \u0023\u003DzyPl0NtN\u0024cLlA(
      \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D _param1,
      double _param2)
    {
      this.\u0023\u003DzBs9H8NO5qLL8 = ((dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) this.\u0023\u003DzHZDgUSdfqmkx()).\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() as \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D;
      if (this.\u0023\u003DzBs9H8NO5qLL8 == null)
        return (double[]) null;
      List<double> doubleList = new List<double>();
      int min = _param1.Min;
      int max = _param1.Max;
      int num1 = min;
      int num2 = int.MinValue;
      int num3 = (int) Math.Max(1.0, Math.Round(_param2));
      int val2 = (int) Math.Round(Math.Min(_param2 / 2.0, 1000.0));
      int num4 = max - min + 1;
      int num5;
      for (int index1 = 0; num1 <= max && ++index1 <= num4; num1 = Math.Max(num5 + Math.Max(1, val2), Math.Min(num5 + num3, max)))
      {
        num5 = num1;
        int num6 = num1 == 0 ? 3 : this.\u0023\u003DzFdyJa7S1Nub8(this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num5), this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num5 - 1));
        for (int index2 = 1; index2 <= val2 && num6 < 3; ++index2)
        {
          int num7 = num1 - index2;
          if (num7 > num2 && num7 <= max)
          {
            int num8 = num7 == 0 ? 3 : this.\u0023\u003DzFdyJa7S1Nub8(this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num7), this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num7 - 1));
            if (num8 > num6)
            {
              num5 = num7;
              num6 = num8;
              if (num8 == 3)
                break;
            }
          }
          int num9 = num1 + index2;
          if (num9 <= max)
          {
            int num10 = num9 == 0 ? 3 : this.\u0023\u003DzFdyJa7S1Nub8(this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num9), this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num9 - 1));
            if (num10 > num6)
            {
              num5 = num9;
              num6 = num10;
            }
          }
        }
        doubleList.Add((double) num5);
        num2 = num5;
      }
      return doubleList.ToArray();
    }
  }

  private sealed class \u0023\u003DzmD295WW4skLs1H\u0024zBQ\u003D\u003D
  {
    public dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzZBtfoxHC6iyc;
    public \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;

    internal void \u0023\u003Dz4EeJ2Z9T93ppthogLg\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      _param2.Handled = true;
      this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dzfl\u0024A1s0\u003D.AutoRange = true;
      this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz3kyPJRWoiKq0, (object) this.\u0023\u003DzZBtfoxHC6iyc);
      this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.ParentSurface?.\u0023\u003Dz5q8i9C4\u003D();
    }
  }

  public sealed class \u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D : 
    \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u0oVUOjcWZm6tIn4bpm9YMvj_jwo7f3RMYA\u003D
  {
    private readonly IChart \u0023\u003DzF\u002458l4g\u003D;
    private readonly IChartAxis \u0023\u003DzLXQXNXQ\u003D;
    private dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd \u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D;
    private string \u0023\u003Dz0Evdh7mohgMvZAgX2A\u003D\u003D;
    private string \u0023\u003DzT__mYRdWRdMk;

    public \u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D()
    {
    }

    public \u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D(IChart _param1, IChartAxis _param2)
    {
      this.\u0023\u003DzF\u002458l4g\u003D = _param1 ?? throw new ArgumentNullException(XXX.SSS(-539328706));
      this.\u0023\u003DzLXQXNXQ\u003D = _param2 ?? throw new ArgumentNullException(XXX.SSS(-539328717));
      ((INotifyPropertyChanged) _param1).PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzay2Z0OS2mqYxHjGOXg\u003D\u003D);
      _param2.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzi\u0024H3kFzx2GWHssWxHw\u003D\u003D);
    }

    private TimeZoneInfo \u0023\u003DzE01O19iZ7vda()
    {
      TimeZoneInfo timeZone = this.\u0023\u003DzLXQXNXQ\u003D?.TimeZone;
      if (timeZone != null)
        return timeZone;
      // ISSUE: explicit non-virtual call
      return !(this.\u0023\u003DzF\u002458l4g\u003D is Chart zF58l4g) ? (TimeZoneInfo) null : __nonvirtual (zF58l4g.TimeZone);
    }

    public override void \u0023\u003DzWzUaFxw\u003D(
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
    {
      this.\u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D = (dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd) _param1;
      base.\u0023\u003DzWzUaFxw\u003D(_param1);
    }

    public override void \u0023\u003DzI_kEht21kBsX()
    {
      this.\u0023\u003Dz7E45WaMjKaIP();
      if (((dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) this.\u0023\u003DzHZDgUSdfqmkx()).TickProvider is \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dzg7H9cRyacWHB tickProvider)
        tickProvider.\u0023\u003DzI_kEht21kBsX();
      base.\u0023\u003DzI_kEht21kBsX();
    }

    private void \u0023\u003Dz7E45WaMjKaIP()
    {
      this.\u0023\u003Dz0Evdh7mohgMvZAgX2A\u003D\u003D = XXX.SSS(-539430209) + this.\u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D.SubDayTextFormatting + XXX.SSS(-539430223);
      this.\u0023\u003DzT__mYRdWRdMk = XXX.SSS(-539430209) + this.\u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D.TextFormatting + XXX.SSS(-539430223);
    }

    public DateTime \u0023\u003Dz_pGtZJ\u002426fjw(IComparable _param1)
    {
      if (_param1 == null)
        return DateTime.MinValue;
      DateTime dateTime = (DateTime) _param1;
      return dateTime == DateTime.MinValue || this.\u0023\u003DzLXQXNXQ\u003D == null || this.\u0023\u003DzF\u002458l4g\u003D == null ? dateTime : TimeHelper.To(dateTime, TimeZoneInfo.Utc, this.\u0023\u003DzE01O19iZ7vda() ?? TimeZoneInfo.Local);
    }

    public override string \u0023\u003Dz\u0024WinkXTLMGVP(IComparable _param1, bool _param2)
    {
      DateTime dateTime = this.\u0023\u003Dz_pGtZJ\u002426fjw(_param1);
      if (dateTime == DateTime.MinValue)
        return string.Empty;
      TimeSpan? baseUtcOffset = this.\u0023\u003DzLXQXNXQ\u003D?.TimeZone?.BaseUtcOffset;
      if (baseUtcOffset.HasValue)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
        interpolatedStringHandler.AppendFormatted<DateTime>(dateTime, XXX.SSS(-539426902));
        interpolatedStringHandler.AppendLiteral(XXX.SSS(-539328764));
        interpolatedStringHandler.AppendFormatted((baseUtcOffset.Value < TimeSpan.Zero ? XXX.SSS(-539328751) : XXX.SSS(-539328743)) + baseUtcOffset.Value.ToString(XXX.SSS(-539328535)));
        return interpolatedStringHandler.ToStringAndClear();
      }
      return $"{dateTime}";
    }

    public override string \u0023\u003DzkqN2vZ4\u003D(IComparable _param1)
    {
      if (this.\u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D == null)
        return base.\u0023\u003DzkqN2vZ4\u003D(_param1);
      if (this.\u0023\u003DzT__mYRdWRdMk == null || this.\u0023\u003Dz0Evdh7mohgMvZAgX2A\u003D\u003D == null)
        this.\u0023\u003Dz7E45WaMjKaIP();
      return StringHelper.Put((this.\u0023\u003DzHZDgUSdfqmkx()?.get_TickProvider() is \u0023\u003DzjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ\u003D\u003D.\u0023\u003Dzg7H9cRyacWHB tickProvider ? (tickProvider.\u0023\u003DzmCaFJJFAOMCC(_param1) ? 1 : 0) : 0) != 0 ? this.\u0023\u003DzT__mYRdWRdMk : this.\u0023\u003Dz0Evdh7mohgMvZAgX2A\u003D\u003D, new object[1]
      {
        (object) this.\u0023\u003Dz_pGtZJ\u002426fjw(_param1)
      });
    }

    private void \u0023\u003Dzay2Z0OS2mqYxHjGOXg\u003D\u003D(
      #nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2)
    {
      if (!(_param2.PropertyName == XXX.SSS(-539432434)))
        return;
      this.\u0023\u003DzHZDgUSdfqmkx().\u0023\u003Dz5q8i9C4\u003D();
    }

    private void \u0023\u003Dzi\u0024H3kFzx2GWHssWxHw\u003D\u003D(
      object? _param1,
      PropertyChangedEventArgs _param2)
    {
      if (!(_param2.PropertyName == XXX.SSS(-539432434)))
        return;
      this.\u0023\u003DzHZDgUSdfqmkx().\u0023\u003Dz5q8i9C4\u003D();
    }
  }
}
