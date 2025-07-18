// Decompiled with JetBrains decompiler
// Type: #=zjFV3E4nzZ192p80vvoilf9WS4WoGxY87RV3wblXTQ04iAcjlIQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

#nullable enable
public static class ExtensionHelper
{
  public static 
  #nullable disable
  AxisBase InitAndSetBinding(
    this IChartAxis _param0,
    ICommand _param1,
    ICommand _param2,
    IChart _param3)
  {
    ExtensionHelper.SomeWheireosoe vbxLeArTkallkIdHg1 = new ExtensionHelper.SomeWheireosoe();
    vbxLeArTkallkIdHg1._IChartAxis_098 = _param0;
    vbxLeArTkallkIdHg1.\u0023\u003Dzi8fyBqQ\u003D = _param1;
    ExtensionHelper.PrivateSealedClass0392 d295Ww4skLs1HZBq = new ExtensionHelper.PrivateSealedClass0392();
    d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D = vbxLeArTkallkIdHg1;
    switch (d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098.AxisType)
    {
      case ChartAxisType.DateTime:
        d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D = (AxisBase) new DateTimeAxis();
        break;
      case ChartAxisType.CategoryDateTime:
        ExtensionHelper.SomeWheireosoe ulUkgXkBneEwzP3h0_1 = d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;
        CategoryDateTimeAxis nu9622VfydaypdeqEjd = new CategoryDateTimeAxis();
        nu9622VfydaypdeqEjd.LabelProvider = (ILabelProvider) new ExtensionHelper.\u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D(_param3, d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098);
        nu9622VfydaypdeqEjd.TickProvider = (ITickProvider) new ExtensionHelper.CategoryDateTimeAxisTickProvider();
        ulUkgXkBneEwzP3h0_1.\u0023\u003DzCejzG6Y\u003D = (AxisBase) nu9622VfydaypdeqEjd;
        d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.SetBindings(CategoryDateTimeAxis.SubDayTextFormattingProperty, (object) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098, "SubDayTextFormatting");
        break;
      case ChartAxisType.Numeric:
        bool flag = ((ICollection<IChartAxis>) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098.ChartArea.XAxises).Contains(d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098);
        d295Ww4skLs1HZBq.\u0023\u003DzZBtfoxHC6iyc = new DoubleRange(0.0, flag ? 0.0 : 0.05);
        ExtensionHelper.SomeWheireosoe ulUkgXkBneEwzP3h0_2 = d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;
        NumericAxis mrfV9Sq7FuggeEjd = new NumericAxis();
        mrfV9Sq7FuggeEjd.GrowBy = (IRange<double>) d295Ww4skLs1HZBq.\u0023\u003DzZBtfoxHC6iyc;
        ulUkgXkBneEwzP3h0_2.\u0023\u003DzCejzG6Y\u003D = (AxisBase) mrfV9Sq7FuggeEjd;
        d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.FlipCoordinatesProperty, (object) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098, "FlipCoordinates");
        d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.CursorTextFormattingProperty, (object) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098, "CursorTextFormatting");
        if (!flag)
        {
          d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.MouseDoubleClick += new MouseButtonEventHandler(d295Ww4skLs1HZBq.\u0023\u003Dz4EeJ2Z9T93ppthogLg\u003D\u003D);
          break;
        }
        break;
      default:
        throw new ArgumentOutOfRangeException("axis", (object) d295Ww4skLs1HZBq.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098.AxisType, LocalizedStrings.InvalidValue);
    }
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.Tag = (object) vbxLeArTkallkIdHg1._IChartAxis_098;
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.IdProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "Id");
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.AutoRangeProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "AutoRange", converter: (IValueConverter) new ExtensionHelper.AutoRangeConverter());
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.DrawLabelsProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "DrawLabels");
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.DrawMajorGridLinesProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "DrawMajorGridLines");
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.DrawMajorTicksProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "DrawMajorTicks");
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.DrawMajorBandsProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "DrawMinorGridLines");
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.DrawMinorGridLinesProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "DrawMinorTicks");
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.DrawMinorTicksProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "TextFormatting");
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(AxisBase.TextFormattingProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "SwitchAxisLocation", converter: (IValueConverter) new ExtensionHelper.AxisAlignmentConverter(((ICollection<IChartAxis>) vbxLeArTkallkIdHg1._IChartAxis_098.ChartArea.XAxises).Contains(vbxLeArTkallkIdHg1._IChartAxis_098)));
    vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D.SetBindings(UIElement.VisibilityProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098, "IsVisible", converter: (IValueConverter) new BoolToVisibilityConverter());
    ControlTemplate controlTemplate1 = new ControlTemplate();
    controlTemplate1.VisualTree = new FrameworkElementFactory(typeof (PropertyGridEx));
    ControlTemplate controlTemplate2 = controlTemplate1;
    controlTemplate2.VisualTree.SetValue(FrameworkElement.WidthProperty, (object) 300.0);
    controlTemplate2.VisualTree.SetValue(FrameworkElement.HeightProperty, (object) 400.0);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.ShowCategoriesProperty, (object) CategoriesShowMode.Hidden);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.ShowMenuButtonInRowsProperty, (object) false);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.ShowToolPanelProperty, (object) false);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.ShowSearchBoxProperty, (object) false);
    controlTemplate2.VisualTree.SetValue(PropertyGridControl.SelectedObjectProperty, (object) vbxLeArTkallkIdHg1._IChartAxis_098);
    ExtensionHelper.SomeWheireosoe vbxLeArTkallkIdHg2 = vbxLeArTkallkIdHg1;
    ContentControl contentControl = new ContentControl();
    contentControl.Template = controlTemplate2;
    vbxLeArTkallkIdHg2.\u0023\u003Dzs1tPnmG2z8D2 = contentControl;
    PopupMenu popupMenu1 = new PopupMenu();
    CommonBarItemCollection items1 = popupMenu1.Items;
    BarSplitButtonItem barSplitButtonItem = new BarSplitButtonItem();
    barSplitButtonItem.Glyph = ThemedIconsExtension.GetImage("Settings");
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
      barButtonItem.Glyph = ThemedIconsExtension.GetImage("Remove2");
      barButtonItem.Content = (object) LocalizedStrings.Delete;
      barButtonItem.Command = vbxLeArTkallkIdHg1.\u0023\u003Dzi8fyBqQ\u003D;
      barButtonItem.CommandParameter = (object) vbxLeArTkallkIdHg1._IChartAxis_098;
      barButtonItem.CommandTarget = (IInputElement) vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D;
      items2.Add((IBarItem) barButtonItem);
    }
    if (vbxLeArTkallkIdHg1._IChartAxis_098.AxisType == ChartAxisType.CategoryDateTime && _param2 != null)
    {
      CommonBarItemCollection items3 = popupMenu2.Items;
      BarButtonItem barButtonItem = new BarButtonItem();
      barButtonItem.Glyph = ThemedIconsExtension.GetImage("Refresh");
      barButtonItem.Content = (object) LocalizedStrings.ResetTimeZone;
      barButtonItem.Command = _param2;
      barButtonItem.CommandParameter = (object) vbxLeArTkallkIdHg1._IChartAxis_098;
      barButtonItem.CommandTarget = (IInputElement) vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D;
      items3.Add((IBarItem) barButtonItem);
    }
    vbxLeArTkallkIdHg1.\u0023\u003Dzan5_\u0024vA\u003D = (PropertyGridEx) null;
    popupMenu2.Opening += new CancelEventHandler(vbxLeArTkallkIdHg1.\u0023\u003DzQGBHhFhQ7lQdReeBKA\u003D\u003D);
    BarManager.SetDXContextMenu((UIElement) vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D, (IPopupControl) popupMenu2);
    return vbxLeArTkallkIdHg1.\u0023\u003DzCejzG6Y\u003D;
  }

  private sealed class SomeWheireosoe
  {
    public IChartAxis _IChartAxis_098;
    public AxisBase \u0023\u003DzCejzG6Y\u003D;
    public PropertyGridEx \u0023\u003Dzan5_\u0024vA\u003D;
    public ContentControl \u0023\u003Dzs1tPnmG2z8D2;
    public ICommand \u0023\u003Dzi8fyBqQ\u003D;

    public void \u0023\u003DzQGBHhFhQ7lQdReeBKA\u003D\u003D(
      #nullable enable
      object? _param1,
      CancelEventArgs _param2)
    {
      if (this.\u0023\u003Dzan5_\u0024vA\u003D == null)
        this.\u0023\u003Dzan5_\u0024vA\u003D = this.\u0023\u003Dzs1tPnmG2z8D2.FindVisualChild<PropertyGridEx>();
      if (this.\u0023\u003Dzan5_\u0024vA\u003D != null)
      {
        this.\u0023\u003Dzan5_\u0024vA\u003D.SelectedObject = (object) null;
        this.\u0023\u003Dzan5_\u0024vA\u003D.SelectedObject = (object) this._IChartAxis_098;
      }
      ((DelegateCommand<ChartAxis>) this.\u0023\u003Dzi8fyBqQ\u003D)?.RaiseCanExecuteChanged();
    }
  }

  private sealed class AxisAlignmentConverter(bool _param1) : IValueConverter
  {
    
    private readonly bool \u0023\u003DzlG_k5f4\u003D = _param1;

    public 
    #nullable disable
    object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
      return (object) (ChartAxisAlignment) ((_param1 as bool?).GetValueOrDefault() ? (this.\u0023\u003DzlG_k5f4\u003D ? 2 : 1) : (this.\u0023\u003DzlG_k5f4\u003D ? 3 : 0));
    }

    public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
      ChartAxisAlignment obj = (ChartAxisAlignment) _param1;
      return (object) (bool) (obj == ChartAxisAlignment.Default ? 0 : (this.\u0023\u003DzlG_k5f4\u003D ? (obj != ChartAxisAlignment.Bottom ? 1 : 0) : (obj != 0 ? 1 : 0)));
    }
  }

  private sealed class AutoRangeConverter : IValueConverter
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

  public sealed class CategoryDateTimeAxisTickProvider : 
    NumericTickProvider
  {
    private AxisParams _axisParams;
    private ICategoryCoordinateCalculator \u0023\u003DzBs9H8NO5qLL8;

    public void OnBeginAxisDraw()
    {
      this._axisParams = ((AxisBase) this.\u0023\u003DzHZDgUSdfqmkx()).GetAxisParams();
    }

    public override double[] GetMajorTicks(
      IAxisParams _param1)
    {
      IndexRange  visibleRange = _param1.VisibleRange as IndexRange ;
      double majorDelta = (double) _param1.MajorDelta;
      if ((visibleRange != null ? (!visibleRange.IsDefined ? 1 : 0) : 1) != 0)
        return base.GetMajorTicks(_param1);
      IPointSeries znUzlqIn9ReXl = this._axisParams.\u0023\u003DznUzlqIN9ReXL;
      IndexRange  zToxB29CkMiO6 = this._axisParams.\u0023\u003DzToxB29CkMiO6;
      return (zToxB29CkMiO6 != null ? (!zToxB29CkMiO6.IsDefined ? 1 : 0) : 1) != 0 || znUzlqIn9ReXl == null || znUzlqIn9ReXl.\u0023\u003DzlpVGw6E\u003D() == 0 ? base.GetMajorTicks(_param1) : this.GetTicksWithinRange(visibleRange, majorDelta) ?? base.GetMajorTicks(_param1);
    }

    private int GetDateTimeDifferent(DateTime _param1, DateTime _param2)
    {
      _param1 = this.\u0023\u003DzHZDgUSdfqmkx().get_LabelProvider() is ExtensionHelper.\u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D labelProvider1 ? labelProvider1.GetDateTime((IComparable) _param1) : _param1;
      _param2 = this.\u0023\u003DzHZDgUSdfqmkx().get_LabelProvider() is ExtensionHelper.\u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D labelProvider2 ? labelProvider2.GetDateTime((IComparable) _param2) : _param2;
      if (_param1.Date != _param2.Date)
        return 3;
      return _param1.Hour == _param2.Hour ? (_param1.Second == 0 ? 1 : 0) : 2;
    }

    public bool DiffAtDate(IComparable _param1)
    {
      if (this.\u0023\u003DzBs9H8NO5qLL8 == null)
        return false;
      int num = this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzFk6sufr\u0024co4e(_param1);
      DateTime dateTime = this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num - 1);
      return num == 0 || this.GetDateTimeDifferent((DateTime) _param1, dateTime) == 3;
    }

    private double[] GetTicksWithinRange(
      IndexRange  _param1,
      double _param2)
    {
      this.\u0023\u003DzBs9H8NO5qLL8 = ((AxisBase) this.\u0023\u003DzHZDgUSdfqmkx()).GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
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
        int num6 = num1 == 0 ? 3 : this.GetDateTimeDifferent(this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num5), this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num5 - 1));
        for (int index2 = 1; index2 <= val2 && num6 < 3; ++index2)
        {
          int num7 = num1 - index2;
          if (num7 > num2 && num7 <= max)
          {
            int num8 = num7 == 0 ? 3 : this.GetDateTimeDifferent(this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num7), this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num7 - 1));
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
            int num10 = num9 == 0 ? 3 : this.GetDateTimeDifferent(this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num9), this.\u0023\u003DzBs9H8NO5qLL8.\u0023\u003DzWZQlXHuDrnKc(num9 - 1));
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

  private sealed class PrivateSealedClass0392
  {
    public DoubleRange \u0023\u003DzZBtfoxHC6iyc;
    public ExtensionHelper.SomeWheireosoe \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;

    public void \u0023\u003Dz4EeJ2Z9T93ppthogLg\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      _param2.Handled = true;
      this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D._IChartAxis_098.AutoRange = true;
      this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.SetValue(AxisBase.\u0023\u003Dz3kyPJRWoiKq0, (object) this.\u0023\u003DzZBtfoxHC6iyc);
      this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzCejzG6Y\u003D.ParentSurface?.InvalidateElement();
    }
  }

  public sealed class \u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D : 
    TradeChartAxisLabelProvider
  {
    private readonly IChart _chart;
    private readonly IChartAxis \u0023\u003DzLXQXNXQ\u003D;
    private CategoryDateTimeAxis \u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D;
    private string \u0023\u003Dz0Evdh7mohgMvZAgX2A\u003D\u003D;
    private string \u0023\u003DzT__mYRdWRdMk;

    public \u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D()
    {
    }

    public \u0023\u003DznXVw5HITBHoBrjSOBA\u003D\u003D(IChart _param1, IChartAxis _param2)
    {
      this._chart = _param1 ?? throw new ArgumentNullException("chart");
      this.\u0023\u003DzLXQXNXQ\u003D = _param2 ?? throw new ArgumentNullException("axis");
      ((INotifyPropertyChanged) _param1).PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzay2Z0OS2mqYxHjGOXg\u003D\u003D);
      _param2.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzi\u0024H3kFzx2GWHssWxHw\u003D\u003D);
    }

    private TimeZoneInfo \u0023\u003DzE01O19iZ7vda()
    {
      TimeZoneInfo timeZone = this.\u0023\u003DzLXQXNXQ\u003D?.TimeZone;
      if (timeZone != null)
        return timeZone;
      // ISSUE: explicit non-virtual call
      return !(this._chart is Chart zF58l4g) ? (TimeZoneInfo) null : __nonvirtual (zF58l4g.TimeZone);
    }

    public override void Init(
      IAxis _param1)
    {
      this.\u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D = (CategoryDateTimeAxis) _param1;
      base.Init(_param1);
    }

    public override void OnBeginAxisDraw()
    {
      this.\u0023\u003Dz7E45WaMjKaIP();
      if (((AxisBase) this.\u0023\u003DzHZDgUSdfqmkx()).TickProvider is ExtensionHelper.CategoryDateTimeAxisTickProvider tickProvider)
        tickProvider.OnBeginAxisDraw();
      base.OnBeginAxisDraw();
    }

    private void \u0023\u003Dz7E45WaMjKaIP()
    {
      this.\u0023\u003Dz0Evdh7mohgMvZAgX2A\u003D\u003D = $"{{0:{this.\u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D.SubDayTextFormatting}}}";
      this.\u0023\u003DzT__mYRdWRdMk = $"{{0:{this.\u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D.TextFormatting}}}";
    }

    public DateTime GetDateTime(IComparable _param1)
    {
      if (_param1 == null)
        return DateTime.MinValue;
      DateTime dateTime = (DateTime) _param1;
      return dateTime == DateTime.MinValue || this.\u0023\u003DzLXQXNXQ\u003D == null || this._chart == null ? dateTime : TimeHelper.To(dateTime, TimeZoneInfo.Utc, this.\u0023\u003DzE01O19iZ7vda() ?? TimeZoneInfo.Local);
    }

    public override string FormatCursorLabel(IComparable _param1, bool _param2)
    {
      DateTime dateTime = this.GetDateTime(_param1);
      if (dateTime == DateTime.MinValue)
        return string.Empty;
      TimeSpan? baseUtcOffset = this.\u0023\u003DzLXQXNXQ\u003D?.TimeZone?.BaseUtcOffset;
      if (baseUtcOffset.HasValue)
        return $"{dateTime:G} UTC{(baseUtcOffset.Value < TimeSpan.Zero ? "-" : "+") + baseUtcOffset.Value.ToString("hh\\:mm")}";
      return $"{dateTime:G}";
    }

    public override string FormatLabel(IComparable _param1)
    {
      if (this.\u0023\u003Dzki6FtdthyFT0AuhYN0Coyw0\u003D == null)
        return base.FormatLabel(_param1);
      if (this.\u0023\u003DzT__mYRdWRdMk == null || this.\u0023\u003Dz0Evdh7mohgMvZAgX2A\u003D\u003D == null)
        this.\u0023\u003Dz7E45WaMjKaIP();
      return StringHelper.Put((this.\u0023\u003DzHZDgUSdfqmkx()?.TickProvider is ExtensionHelper.CategoryDateTimeAxisTickProvider tickProvider ? (tickProvider.DiffAtDate(_param1) ? 1 : 0) : 0) != 0 ? this.\u0023\u003DzT__mYRdWRdMk : this.\u0023\u003Dz0Evdh7mohgMvZAgX2A\u003D\u003D, new object[1]
      {
        (object) this.GetDateTime(_param1)
      });
    }

    private void \u0023\u003Dzay2Z0OS2mqYxHjGOXg\u003D\u003D(
      #nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2)
    {
      if (!(_param2.PropertyName == "TimeZone"))
        return;
      this.\u0023\u003DzHZDgUSdfqmkx().InvalidateElement();
    }

    private void \u0023\u003Dzi\u0024H3kFzx2GWHssWxHw\u003D\u003D(
      object? _param1,
      PropertyChangedEventArgs _param2)
    {
      if (!(_param2.PropertyName == "TimeZone"))
        return;
      this.\u0023\u003DzHZDgUSdfqmkx().InvalidateElement();
    }
  }
}
