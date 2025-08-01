// Decompiled with JetBrains decompiler
// Type: #=ztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a$$M2u5Uiq7Pu7_oc1A1JQ8nQQRm
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Drawing;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
public sealed class \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX> : 
  ChartCompentWpfBaseViewModel<ChartBandElement>
  where TX : struct, IComparable
{
  
  private readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, double> \u0023\u003DzDh1lJfFlHUWk;
  
  private readonly XyDataSeries<TX, double> \u0023\u003Dzva\u002460XCiLL2n;
  
  private readonly XyDataSeries<TX, double> \u0023\u003DzRSDkBpQ1QWG0;
  
  private ChartSeriesViewModel \u0023\u003DzYirGqB2gXz09;
  
  private ChartSeriesViewModel \u0023\u003DzIHjxTqC159pe;
  
  private ChartSeriesViewModel \u0023\u003DzRm0WUjzJSu8n;
  
  private ChartElementViewModel \u0023\u003Dzh0ozsIDILK5b;
  
  private ChartElementViewModel \u0023\u003DzXNWLRaQhQW_0;
  
  private IComparable \u0023\u003DzFEDR40ugZMK3;

  public \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm(
    ChartBandElement _param1)
    : base(_param1)
  {
    Type type = typeof (TX);
    if (type != typeof (DateTime) && type != typeof (double))
      throw new NotSupportedException($"X type {type.Name} is not supported");
    this.\u0023\u003DzDh1lJfFlHUWk = new \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<TX, double>();
    this.\u0023\u003Dzva\u002460XCiLL2n = new XyDataSeries<TX, double>();
    this.\u0023\u003DzRSDkBpQ1QWG0 = new XyDataSeries<TX, double>();
  }

  protected override void Init()
  {
    base.Init();
    DrawStyles[] drawStylesArray = new DrawStyles[4]
    {
      DrawStyles.Line,
      DrawStyles.NoGapLine,
      DrawStyles.StepLine,
      DrawStyles.DashedLine
    };
    ChartCompentWpfBaseViewModel<ChartBandElement>.AddStylePropertyChanging<DrawStyles>((IChartComponent) this.ChartComponentView.Line1, "Style", drawStylesArray);
    ChartCompentWpfBaseViewModel<ChartBandElement>.AddStylePropertyChanging<DrawStyles>((IChartComponent) this.ChartComponentView.Line2, "Style", drawStylesArray);
    string[] strArray = new string[2]
    {
      "Color",
      "AdditionalColor"
    };
    this.ChartViewModel.AddChild(this.\u0023\u003Dzh0ozsIDILK5b = new ChartElementViewModel((INotifyPropertyChanged) this.ChartComponentView.Line1, new Func<SeriesInfo, Color>(this.\u0023\u003Dzgl15kO2PtK5giK8GzTIwj08\u003D), \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX>.SomeClass34343383.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D ?? (\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX>.SomeClass34343383.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D = new Func<SeriesInfo, string>(\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX>.SomeClass34343383.SomeMethond0343.\u0023\u003Dz_kGaLH\u0024IiOuHLLvhkS2WN_s\u003D)), strArray));
    this.ChartViewModel.AddChild(this.\u0023\u003DzXNWLRaQhQW_0 = new ChartElementViewModel((INotifyPropertyChanged) this.ChartComponentView.Line2, new Func<SeriesInfo, Color>(this.\u0023\u003Dz7GRJChTRxjdOfay3RCW2oqs\u003D), \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX>.SomeClass34343383.\u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D ?? (\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX>.SomeClass34343383.\u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D = new Func<SeriesInfo, string>(\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX>.SomeClass34343383.SomeMethond0343.\u0023\u003DzMUMqMtnuX9rATNeQa7kIAUA\u003D)), strArray));
    this.AddPropertyEvents((IChartComponent) this.ChartComponentView.Line1);
    this.AddPropertyEvents((IChartComponent) this.ChartComponentView.Line2);
    this.\u0023\u003DzYirGqB2gXz09 = new ChartSeriesViewModel((IDataSeries) this.\u0023\u003Dzva\u002460XCiLL2n, (IRenderableSeries) null);
    this.\u0023\u003DzIHjxTqC159pe = new ChartSeriesViewModel((IDataSeries) this.\u0023\u003DzRSDkBpQ1QWG0, (IRenderableSeries) null);
    this.\u0023\u003DzRm0WUjzJSu8n = new ChartSeriesViewModel((IDataSeries) this.\u0023\u003DzDh1lJfFlHUWk, (IRenderableSeries) null);
    this.\u0023\u003DzcIqdE4oVd9lsrOCnFSgflME\u003D();
    this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzYirGqB2gXz09, this.ChartComponentView.Line1, this.\u0023\u003Dzh0ozsIDILK5b);
    this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzIHjxTqC159pe, this.ChartComponentView.Line2, this.\u0023\u003DzXNWLRaQhQW_0);
    this.SetupAxisMarkerAndBinding(this.\u0023\u003DzYirGqB2gXz09.RenderSeries, (IChartComponent) this.ChartComponentView.Line1, "ShowAxisMarker", "Color");
    this.SetupAxisMarkerAndBinding(this.\u0023\u003DzIHjxTqC159pe.RenderSeries, (IChartComponent) this.ChartComponentView.Line2, "ShowAxisMarker", "Color");
    this.ScichartSurfaceMVVM.AddSeriesViewModelsToRoot(this.RootElem, (IRenderableSeries) this.\u0023\u003DzYirGqB2gXz09);
    this.ScichartSurfaceMVVM.AddSeriesViewModelsToRoot(this.RootElem, (IRenderableSeries) this.\u0023\u003DzIHjxTqC159pe);
    this.ScichartSurfaceMVVM.AddSeriesViewModelsToRoot(this.RootElem, (IRenderableSeries) this.\u0023\u003DzRm0WUjzJSu8n);
    this.\u0023\u003DzJGn0U4ESy8cx();
  }

  private void \u0023\u003DzJGn0U4ESy8cx()
  {
    ChartCompentWpfBaseViewModel<ChartBandElement>.SetIncludeSeries(this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries, false);
    ChartCompentWpfBaseViewModel<ChartBandElement>.SetIncludeSeries(this.\u0023\u003DzYirGqB2gXz09.RenderSeries, true);
    ChartCompentWpfBaseViewModel<ChartBandElement>.SetIncludeSeries(this.\u0023\u003DzIHjxTqC159pe.RenderSeries, this.ChartComponentView.Style == DrawStyles.Band);
  }

  private void \u0023\u003DzcIqdE4oVd9lsrOCnFSgflME\u003D()
  {
    if (this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries is dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd)
      return;
    dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd xc8MfylkhywkcxaEjd = this.CreateRenderableSeries<dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd>(Array.Empty<ChartElementViewModel>());
    this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries = (IRenderableSeries) xc8MfylkhywkcxaEjd;
    xc8MfylkhywkcxaEjd.SeriesColor = xc8MfylkhywkcxaEjd.Series1Color = Colors.Transparent;
    xc8MfylkhywkcxaEjd.SetBindings(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd.\u0023\u003DzqXdfa4iudfGA, (object) this.ChartComponentView.Line1, "AdditionalColor");
    xc8MfylkhywkcxaEjd.SetBindings(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd.\u0023\u003DzJJDbrFb7cXV\u0024, (object) this.ChartComponentView.Line2, "AdditionalColor");
  }

  private void \u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(
    ChartSeriesViewModel _param1,
    ChartLineElement _param2,
    ChartElementViewModel _param3)
  {
    if (!(_param1.RenderSeries is FastLineRenderableSeries mq47Z7V297V2U22Ejd))
    {
      ChartSeriesViewModel js00Vef8BmoZoBlhDa = _param1;
      ChartElementViewModel[] h0icPdKkp5z7HsjcOyArray = new ChartElementViewModel[1]
      {
        _param3
      };
      FastLineRenderableSeries mq47Z7V297V2U22Ejd1;
      mq47Z7V297V2U22Ejd = mq47Z7V297V2U22Ejd1 = this.CreateRenderableSeries<FastLineRenderableSeries>(h0icPdKkp5z7HsjcOyArray);
      js00Vef8BmoZoBlhDa.RenderSeries = (IRenderableSeries) mq47Z7V297V2U22Ejd1;
      mq47Z7V297V2U22Ejd.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) _param2, "Color");
      mq47Z7V297V2U22Ejd.SetBindings(BaseRenderableSeries.\u0023\u003Dz13qAkT\u0024eFXTPuGCUqjA\u0024Svw\u003D, (object) _param2, "DrawTemplate");
      mq47Z7V297V2U22Ejd.SetBindings(BaseRenderableSeries.StrokeThicknessProperty, (object) _param2, "StrokeThickness");
      mq47Z7V297V2U22Ejd.SetBindings(BaseRenderableSeries.AntiAliasingProperty, (object) _param2, "AntiAliasing");
      FastLineRenderableSeries mq47Z7V297V2U22Ejd2 = mq47Z7V297V2U22Ejd;
      DependencyProperty z8b6MqaiE8Uzn = BaseRenderableSeries.IsVisibleProperty;
      BoolAllConverter conv = new BoolAllConverter();
      conv.Value = true;
      Binding[] bindingArray = new Binding[3]
      {
        new Binding("IsVisible") { Source = (object) _param2 },
        new Binding("IsVisible")
        {
          Source = (object) this.ChartComponentView
        },
        new Binding("IsVisible")
        {
          Source = (object) ((IChartComponent) this.ChartComponentView).RootElement
        }
      };
      mq47Z7V297V2U22Ejd2.SetMultiBinding(z8b6MqaiE8Uzn, (IMultiValueConverter) conv, bindingArray);
    }
    mq47Z7V297V2U22Ejd.StrokeDashArray = (double[]) null;
    mq47Z7V297V2U22Ejd.IsDigitalLine = false;
    if (_param2.Style == DrawStyles.DashedLine)
    {
      mq47Z7V297V2U22Ejd.StrokeDashArray = new double[2]
      {
        5.0,
        5.0
      };
    }
    else
    {
      if (_param2.Style != DrawStyles.StepLine)
        return;
      mq47Z7V297V2U22Ejd.IsDigitalLine = true;
    }
  }

  protected override void Clear()
  {
    this.ScichartSurfaceMVVM.RemoveChartComponent(this.RootElem);
  }

  protected override void UpdateUi()
  {
    this.\u0023\u003Dzva\u002460XCiLL2n.Clear();
    this.\u0023\u003DzRSDkBpQ1QWG0.Clear();
    this.\u0023\u003DzDh1lJfFlHUWk.Clear();
    this.\u0023\u003DzFEDR40ugZMK3 = (IComparable) default (TX);
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.Draw<TX>(CollectionHelper.ToEx<ChartDrawData.sxTuple<TX>>(((IEnumerable) _param1).Cast<ChartDrawData.sxTuple<TX>>(), ((IEnumerableEx) _param1).Count));
  }

  public bool Draw<TX1>(
    IEnumerableEx<ChartDrawData.sxTuple<TX1>> _param1)
    where TX1 : struct, IComparable
  {
    if (_param1 == null)
      return false;
    int count = ((IEnumerableEx) _param1).Count;
    IComparable comparable = this.\u0023\u003DzFEDR40ugZMK3;
    int index = -1;
    TX[] array1 = new TX[count];
    double[] array2 = new double[count];
    double[] array3 = new double[count];
    foreach (ChartDrawData.sxTuple<TX1> z6MdlWkBsH4 in (IEnumerable<ChartDrawData.sxTuple<TX1>>) _param1)
    {
      TX zulcL8Ra = (TX) (ValueType) z6MdlWkBsH4.X;
      switch (zulcL8Ra.CompareTo((object) comparable))
      {
        case -1:
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotChangeCandleValue, new object[2]
          {
            (object) zulcL8Ra,
            (object) comparable
          }));
        case 0:
          this.\u0023\u003Dzva\u002460XCiLL2n.UpdateOrderAdornerLayer(zulcL8Ra, z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv());
          this.\u0023\u003DzRSDkBpQ1QWG0.UpdateOrderAdornerLayer(zulcL8Ra, z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA());
          this.\u0023\u003DzDh1lJfFlHUWk.UpdateOrderAdornerLayer(zulcL8Ra, z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv(), z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA());
          --count;
          break;
        default:
          ++index;
          array1[index] = zulcL8Ra;
          array2[index] = z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv();
          array3[index] = z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA();
          break;
      }
      comparable = (IComparable) zulcL8Ra;
    }
    if (count == 0)
      return false;
    Array.Resize<TX>(ref array1, count);
    Array.Resize<double>(ref array2, count);
    Array.Resize<double>(ref array3, count);
    this.\u0023\u003Dzva\u002460XCiLL2n.\u0023\u003Dznc8esWY\u003D((IEnumerable<TX>) array1, (IEnumerable<double>) array2);
    this.\u0023\u003DzRSDkBpQ1QWG0.\u0023\u003Dznc8esWY\u003D((IEnumerable<TX>) array1, (IEnumerable<double>) array3);
    this.\u0023\u003DzDh1lJfFlHUWk.\u0023\u003Dznc8esWY\u003D((IEnumerable<TX>) array1, (IEnumerable<double>) array2, (IEnumerable<double>) array3);
    this.\u0023\u003DzFEDR40ugZMK3 = comparable;
    return true;
  }

  protected override void RootElementPropertyChanged(
    IChartComponent _param1,
    string _param2)
  {
    base.RootElementPropertyChanged(_param1, _param2);
    if (_param2 == "Style")
    {
      this.\u0023\u003DzcIqdE4oVd9lsrOCnFSgflME\u003D();
      this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzYirGqB2gXz09, this.ChartComponentView.Line1, this.\u0023\u003Dzh0ozsIDILK5b);
      this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzIHjxTqC159pe, this.ChartComponentView.Line2, this.\u0023\u003DzXNWLRaQhQW_0);
    }
    if (!(_param2 == "Style"))
      return;
    this.\u0023\u003DzJGn0U4ESy8cx();
  }

  private Color \u0023\u003Dzgl15kO2PtK5giK8GzTIwj08\u003D(
    SeriesInfo _param1)
  {
    return ChartElementViewModel.GetHigherAlphaColor(this.ChartComponentView.Line1.Color, this.ChartComponentView.Line1.AdditionalColor);
  }

  private Color \u0023\u003Dz7GRJChTRxjdOfay3RCW2oqs\u003D(
    SeriesInfo _param1)
  {
    return ChartElementViewModel.GetHigherAlphaColor(this.ChartComponentView.Line2.Color, this.ChartComponentView.Line2.AdditionalColor);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX>.SomeClass34343383 SomeMethond0343 = new \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<TX>.SomeClass34343383();
    public static Func<SeriesInfo, string> \u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D;

    public string \u0023\u003Dz_kGaLH\u0024IiOuHLLvhkS2WN_s\u003D(
      SeriesInfo _param1)
    {
      return _param1.FormattedYValue;
    }

    public string \u0023\u003DzMUMqMtnuX9rATNeQa7kIAUA\u003D(
      SeriesInfo _param1)
    {
      return _param1.FormattedYValue;
    }
  }
}
