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
internal sealed class \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D> : 
  ChartElementUI<ChartBandElement>
  where \u0023\u003DzulcL8RA\u003D : struct, IComparable
{
  
  private readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<\u0023\u003DzulcL8RA\u003D, double> \u0023\u003DzDh1lJfFlHUWk;
  
  private readonly \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<\u0023\u003DzulcL8RA\u003D, double> \u0023\u003Dzva\u002460XCiLL2n;
  
  private readonly \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<\u0023\u003DzulcL8RA\u003D, double> \u0023\u003DzRSDkBpQ1QWG0;
  
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzYirGqB2gXz09;
  
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzIHjxTqC159pe;
  
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzRm0WUjzJSu8n;
  
  private ChildVM \u0023\u003Dzh0ozsIDILK5b;
  
  private ChildVM \u0023\u003DzXNWLRaQhQW_0;
  
  private IComparable \u0023\u003DzFEDR40ugZMK3;

  public \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm(
    ChartBandElement _param1)
    : base(_param1)
  {
    Type type = typeof (\u0023\u003DzulcL8RA\u003D);
    if (type != typeof (DateTime) && type != typeof (double))
      throw new NotSupportedException($"X type {type.Name} is not supported");
    this.\u0023\u003DzDh1lJfFlHUWk = new \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<\u0023\u003DzulcL8RA\u003D, double>();
    this.\u0023\u003Dzva\u002460XCiLL2n = new \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<\u0023\u003DzulcL8RA\u003D, double>();
    this.\u0023\u003DzRSDkBpQ1QWG0 = new \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<\u0023\u003DzulcL8RA\u003D, double>();
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
    ChartElementUI<ChartBandElement>.\u0023\u003Dz9tL3mkpMz5PJ<DrawStyles>((IChartComponent) this.GetDrawableChartElement().Line1, "Style", drawStylesArray);
    ChartElementUI<ChartBandElement>.\u0023\u003Dz9tL3mkpMz5PJ<DrawStyles>((IChartComponent) this.GetDrawableChartElement().Line2, "Style", drawStylesArray);
    string[] strArray = new string[2]
    {
      "Color",
      "AdditionalColor"
    };
    this.ChartViewModel.\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003Dzh0ozsIDILK5b = new ChildVM((INotifyPropertyChanged) this.GetDrawableChartElement().Line1, new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dzgl15kO2PtK5giK8GzTIwj08\u003D), \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D ?? (\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003Dz_kGaLH\u0024IiOuHLLvhkS2WN_s\u003D)), strArray));
    this.ChartViewModel.\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzXNWLRaQhQW_0 = new ChildVM((INotifyPropertyChanged) this.GetDrawableChartElement().Line2, new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dz7GRJChTRxjdOfay3RCW2oqs\u003D), \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.\u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D ?? (\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.\u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003DzMUMqMtnuX9rATNeQa7kIAUA\u003D)), strArray));
    this.\u0023\u003DzZcbqdpE\u003D((IChartComponent) this.GetDrawableChartElement().Line1);
    this.\u0023\u003DzZcbqdpE\u003D((IChartComponent) this.GetDrawableChartElement().Line2);
    this.\u0023\u003DzYirGqB2gXz09 = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003Dzva\u002460XCiLL2n, (IRenderableSeries) null);
    this.\u0023\u003DzIHjxTqC159pe = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003DzRSDkBpQ1QWG0, (IRenderableSeries) null);
    this.\u0023\u003DzRm0WUjzJSu8n = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003DzDh1lJfFlHUWk, (IRenderableSeries) null);
    this.\u0023\u003DzcIqdE4oVd9lsrOCnFSgflME\u003D();
    this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzYirGqB2gXz09, this.GetDrawableChartElement().Line1, this.\u0023\u003Dzh0ozsIDILK5b);
    this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzIHjxTqC159pe, this.GetDrawableChartElement().Line2, this.\u0023\u003DzXNWLRaQhQW_0);
    this.SetupAxisMarkerAndBinding(this.\u0023\u003DzYirGqB2gXz09.RenderSeries, (IChartComponent) this.GetDrawableChartElement().Line1, "ShowAxisMarker", "Color");
    this.SetupAxisMarkerAndBinding(this.\u0023\u003DzIHjxTqC159pe.RenderSeries, (IChartComponent) this.GetDrawableChartElement().Line2, "ShowAxisMarker", "Color");
    this.ScichartSurfaceMVVM.\u0023\u003DzBE5I4io\u003D(this.RootElem, (IRenderableSeries) this.\u0023\u003DzYirGqB2gXz09);
    this.ScichartSurfaceMVVM.\u0023\u003DzBE5I4io\u003D(this.RootElem, (IRenderableSeries) this.\u0023\u003DzIHjxTqC159pe);
    this.ScichartSurfaceMVVM.\u0023\u003DzBE5I4io\u003D(this.RootElem, (IRenderableSeries) this.\u0023\u003DzRm0WUjzJSu8n);
    this.\u0023\u003DzJGn0U4ESy8cx();
  }

  private void \u0023\u003DzJGn0U4ESy8cx()
  {
    ChartElementUI<ChartBandElement>.\u0023\u003DzpbLgaWJ0hngn(this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries, false);
    ChartElementUI<ChartBandElement>.\u0023\u003DzpbLgaWJ0hngn(this.\u0023\u003DzYirGqB2gXz09.RenderSeries, true);
    ChartElementUI<ChartBandElement>.\u0023\u003DzpbLgaWJ0hngn(this.\u0023\u003DzIHjxTqC159pe.RenderSeries, this.GetDrawableChartElement().Style == DrawStyles.Band);
  }

  private void \u0023\u003DzcIqdE4oVd9lsrOCnFSgflME\u003D()
  {
    if (this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries is dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd)
      return;
    dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd xc8MfylkhywkcxaEjd = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd>(Array.Empty<ChildVM>());
    this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries = (IRenderableSeries) xc8MfylkhywkcxaEjd;
    xc8MfylkhywkcxaEjd.SeriesColor = xc8MfylkhywkcxaEjd.Series1Color = Colors.Transparent;
    xc8MfylkhywkcxaEjd.SetBindings(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd.\u0023\u003DzqXdfa4iudfGA, (object) this.GetDrawableChartElement().Line1, "AdditionalColor");
    xc8MfylkhywkcxaEjd.SetBindings(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd.\u0023\u003DzJJDbrFb7cXV\u0024, (object) this.GetDrawableChartElement().Line2, "AdditionalColor");
  }

  private void \u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(
    \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D _param1,
    ChartLineElement _param2,
    ChildVM _param3)
  {
    if (!(_param1.RenderSeries is FastLineRenderableSeries mq47Z7V297V2U22Ejd))
    {
      \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D js00Vef8BmoZoBlhDa = _param1;
      ChildVM[] h0icPdKkp5z7HsjcOyArray = new ChildVM[1]
      {
        _param3
      };
      FastLineRenderableSeries mq47Z7V297V2U22Ejd1;
      mq47Z7V297V2U22Ejd = mq47Z7V297V2U22Ejd1 = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<FastLineRenderableSeries>(h0icPdKkp5z7HsjcOyArray);
      js00Vef8BmoZoBlhDa.RenderSeries = (IRenderableSeries) mq47Z7V297V2U22Ejd1;
      mq47Z7V297V2U22Ejd.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) _param2, "Color");
      mq47Z7V297V2U22Ejd.SetBindings(BaseRenderableSeries.\u0023\u003Dz13qAkT\u0024eFXTPuGCUqjA\u0024Svw\u003D, (object) _param2, "DrawTemplate");
      mq47Z7V297V2U22Ejd.SetBindings(BaseRenderableSeries.\u0023\u003DzTe_gV3cWjEp7, (object) _param2, "StrokeThickness");
      mq47Z7V297V2U22Ejd.SetBindings(BaseRenderableSeries.\u0023\u003Dzdr5RTntdbeN7, (object) _param2, "AntiAliasing");
      FastLineRenderableSeries mq47Z7V297V2U22Ejd2 = mq47Z7V297V2U22Ejd;
      DependencyProperty z8b6MqaiE8Uzn = BaseRenderableSeries.\u0023\u003Dz8b6MQAIE8UZn;
      BoolAllConverter conv = new BoolAllConverter();
      conv.Value = true;
      Binding[] bindingArray = new Binding[3]
      {
        new Binding("IsVisible") { Source = (object) _param2 },
        new Binding("IsVisible")
        {
          Source = (object) this.GetDrawableChartElement()
        },
        new Binding("IsVisible")
        {
          Source = (object) ((IChartComponent) this.GetDrawableChartElement()).RootElement
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
    this.ScichartSurfaceMVVM.\u0023\u003Dzwh_e_TheVZKh(this.RootElem);
  }

  protected override void UpdateUi()
  {
    this.\u0023\u003Dzva\u002460XCiLL2n.Clear();
    this.\u0023\u003DzRSDkBpQ1QWG0.Clear();
    this.\u0023\u003DzDh1lJfFlHUWk.Clear();
    this.\u0023\u003DzFEDR40ugZMK3 = (IComparable) default (\u0023\u003DzulcL8RA\u003D);
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.Draw<\u0023\u003DzulcL8RA\u003D>(CollectionHelper.ToEx<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>>(((IEnumerable) _param1).Cast<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>>(), ((IEnumerableEx) _param1).Count));
  }

  public bool Draw<TX1>(
    IEnumerableEx<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>> _param1)
    where TX1 : struct, IComparable
  {
    if (_param1 == null)
      return false;
    int count = ((IEnumerableEx) _param1).Count;
    IComparable comparable = this.\u0023\u003DzFEDR40ugZMK3;
    int index = -1;
    \u0023\u003DzulcL8RA\u003D[] array1 = new \u0023\u003DzulcL8RA\u003D[count];
    double[] array2 = new double[count];
    double[] array3 = new double[count];
    foreach (ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1> z6MdlWkBsH4 in (IEnumerable<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>>) _param1)
    {
      \u0023\u003DzulcL8RA\u003D zulcL8Ra = (\u0023\u003DzulcL8RA\u003D) (ValueType) z6MdlWkBsH4.\u0023\u003Dz2_4KSTY\u003D();
      switch (zulcL8Ra.CompareTo((object) comparable))
      {
        case -1:
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotChangeCandleValue, new object[2]
          {
            (object) zulcL8Ra,
            (object) comparable
          }));
        case 0:
          this.\u0023\u003Dzva\u002460XCiLL2n.\u0023\u003DzFkV86a8\u003D(zulcL8Ra, z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv());
          this.\u0023\u003DzRSDkBpQ1QWG0.\u0023\u003DzFkV86a8\u003D(zulcL8Ra, z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA());
          this.\u0023\u003DzDh1lJfFlHUWk.\u0023\u003DzFkV86a8\u003D(zulcL8Ra, z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv(), z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA());
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
    Array.Resize<\u0023\u003DzulcL8RA\u003D>(ref array1, count);
    Array.Resize<double>(ref array2, count);
    Array.Resize<double>(ref array3, count);
    this.\u0023\u003Dzva\u002460XCiLL2n.\u0023\u003Dznc8esWY\u003D((IEnumerable<\u0023\u003DzulcL8RA\u003D>) array1, (IEnumerable<double>) array2);
    this.\u0023\u003DzRSDkBpQ1QWG0.\u0023\u003Dznc8esWY\u003D((IEnumerable<\u0023\u003DzulcL8RA\u003D>) array1, (IEnumerable<double>) array3);
    this.\u0023\u003DzDh1lJfFlHUWk.\u0023\u003Dznc8esWY\u003D((IEnumerable<\u0023\u003DzulcL8RA\u003D>) array1, (IEnumerable<double>) array2, (IEnumerable<double>) array3);
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
      this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzYirGqB2gXz09, this.GetDrawableChartElement().Line1, this.\u0023\u003Dzh0ozsIDILK5b);
      this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzIHjxTqC159pe, this.GetDrawableChartElement().Line2, this.\u0023\u003DzXNWLRaQhQW_0);
    }
    if (!(_param2 == "Style"))
      return;
    this.\u0023\u003DzJGn0U4ESy8cx();
  }

  private Color \u0023\u003Dzgl15kO2PtK5giK8GzTIwj08\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    return ChildVM.GetHigherAlphaColor(this.GetDrawableChartElement().Line1.Color, this.GetDrawableChartElement().Line1.AdditionalColor);
  }

  private Color \u0023\u003Dz7GRJChTRxjdOfay3RCW2oqs\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    return ChildVM.GetHigherAlphaColor(this.GetDrawableChartElement().Line2.Color, this.GetDrawableChartElement().Line2.AdditionalColor);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383 SomeMethond0343 = new \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383();
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D;
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D;

    internal string \u0023\u003Dz_kGaLH\u0024IiOuHLLvhkS2WN_s\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return _param1.FormattedYValue;
    }

    internal string \u0023\u003DzMUMqMtnuX9rATNeQa7kIAUA\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return _param1.FormattedYValue;
    }
  }
}
