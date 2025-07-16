// Decompiled with JetBrains decompiler
// Type: #=zgg5QOmcWitJriAsXqwM_mmKL7LRAQHeU0CkDEWjOUd11EjbkZ3YobQ0=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
public static class \u0023\u003Dzgg5QOmcWitJriAsXqwM_mmKL7LRAQHeU0CkDEWjOUd11EjbkZ3YobQ0\u003D
{
  public static DependencyProperty \u0023\u003Dz7aV0h3kX1zh\u0024 = DependencyProperty.RegisterAttached("SeriesStyle", typeof (Style), typeof (\u0023\u003Dzgg5QOmcWitJriAsXqwM_mmKL7LRAQHeU0CkDEWjOUd11EjbkZ3YobQ0\u003D), new PropertyMetadata((PropertyChangedCallback) null));

  public static Style GetSeriesStyle(DependencyObject _param0)
  {
    return (Style) _param0.GetValue(\u0023\u003Dzgg5QOmcWitJriAsXqwM_mmKL7LRAQHeU0CkDEWjOUd11EjbkZ3YobQ0\u003D.\u0023\u003Dz7aV0h3kX1zh\u0024);
  }

  public static void SetSeriesStyle(DependencyObject _param0, Style _param1)
  {
    _param0.SetValue(\u0023\u003Dzgg5QOmcWitJriAsXqwM_mmKL7LRAQHeU0CkDEWjOUd11EjbkZ3YobQ0\u003D.\u0023\u003Dz7aV0h3kX1zh\u0024, (object) _param1);
  }

  public static void \u0023\u003Dzjqa\u00243wA\u003D(
    this BaseRenderableSeries _param0,
    Style _param1)
  {
    if (_param1 == null)
      return;
    Style style = new Style()
    {
      TargetType = typeof (BaseRenderableSeries)
    };
    foreach (Setter setter in _param1.Setters.OfType<Setter>())
    {
      DependencyProperty property = setter.Property;
      object obj = _param0.GetValue(property);
      style.Setters.Add((SetterBase) new Setter(property, obj));
      _param0.SetCurrentValue(property, setter.Value);
    }
    \u0023\u003Dzgg5QOmcWitJriAsXqwM_mmKL7LRAQHeU0CkDEWjOUd11EjbkZ3YobQ0\u003D.SetSeriesStyle((DependencyObject) _param0, style);
  }

  public static bool \u0023\u003DzLDwF88FLhD9n2pkW3Q\u003D\u003D(
    this IRenderableSeries _param0)
  {
    switch (_param0)
    {
      case FastLineRenderableSeries _ when ((FastLineRenderableSeries) _param0).IsDigitalLine:
      case FastMountainRenderableSeries _ when ((BaseMountainRenderableSeries) _param0).IsDigitalLine:
        return true;
      case dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd _:
        return ((dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd) _param0).IsDigitalLine;
      default:
        return false;
    }
  }

  public static \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003DzZZbJdAS6fDJ\u0024(
    this IRenderableSeries _param0,
    HitTestInfo _param1)
  {
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D vdj8C0KctI6r27Gg;
    switch (_param1.\u0023\u003DzRkghOq8y7ncj())
    {
      case (DataSeriesType) 1:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D(_param0, _param1);
        break;
      case (DataSeriesType) 2:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D(_param0, _param1);
        break;
      case (DataSeriesType) 3:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003DzupHrUO0UFO07vWyNRguf_6KxLa4699odrw\u003D\u003D(_param0, _param1);
        break;
      case (DataSeriesType) 4:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEdnlPeUGAD5h8w\u003D\u003D(_param0, _param1);
        break;
      case (DataSeriesType) 6:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u002407I0wJVDIX8uAYHkX8\u003D(_param0, _param1);
        break;
      case (DataSeriesType) 7:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003DzCp5d2Zte2oCosmmx2S7no7oM806RFMQA4oT0jRI\u003D(_param0, _param1);
        break;
      case (DataSeriesType) 8:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H3DyRXy\u0024crDQ0zcN7c_LKq7HenVQrw\u003D\u003D(_param0, _param1);
        break;
      case (DataSeriesType) 9:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc(_param0, _param1);
        break;
      case (DataSeriesType) 10:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd(_param0, _param1);
        break;
      default:
        vdj8C0KctI6r27Gg = (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D) new \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciTIXSve_YM8UPQ\u003D\u003D(_param0, _param1);
        break;
    }
    return vdj8C0KctI6r27Gg;
  }

  public static Color \u0023\u003Dz3_4\u0024b5dxRhLlXyFK3Q\u003D\u003D(
    this IRenderableSeries _param0,
    HitTestInfo _param1)
  {
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = _param0.get_PaletteProvider();
    Color color1 = _param0.SeriesColor;
    FastCandlestickRenderableSeries renderableSeries = _param0 as FastCandlestickRenderableSeries;
    dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd fyrzxrpaxM3VuD4LqEjd = _param0 as dje_zXLPLNSHM7QU5GA2T5S4C4SHVF6L8PVAPMBCTY5WFUGN8USCF94BEQYVU9VJCT4CGFWU4FYRZXRPAXM3VUD4LQ_ejd;
    if (renderableSeries != null || fyrzxrpaxM3VuD4LqEjd != null)
    {
      Color color2 = renderableSeries != null ? renderableSeries.UpWickColor : fyrzxrpaxM3VuD4LqEjd.UpWickColor;
      Color color3 = renderableSeries != null ? renderableSeries.DownWickColor : fyrzxrpaxM3VuD4LqEjd.DownWickColor;
      color1 = _param1.\u0023\u003DzrRG8qdg_pzoL().CompareTo((object) _param1.\u0023\u003DzlVz0JivzQhAY()) >= 0 ? color2 : color3;
    }
    if (_param0.get_PaletteProvider() != null)
    {
      Color? nullable1 = new Color?();
      Color? nullable2 = _param1.\u0023\u003DzRkghOq8y7ncj() != (DataSeriesType) 1 ? (_param1.\u0023\u003DzRkghOq8y7ncj() != (DataSeriesType) 3 ? paletteProvider.\u0023\u003DzP50Orng\u003D(_param0, _param1.\u0023\u003DztryT5H42SVj8().ToDouble(), _param1.\u0023\u003Dzd9IAScWutAfJ().ToDouble()) : paletteProvider.\u0023\u003DzLCyKrYI\u003D(_param0, _param1.\u0023\u003DztryT5H42SVj8().ToDouble(), _param1.\u0023\u003Dzd9IAScWutAfJ().ToDouble(), _param1.ZValue.ToDouble())) : paletteProvider.\u0023\u003DzLCyKrYI\u003D(_param0, _param1.\u0023\u003DztryT5H42SVj8().ToDouble(), _param1.\u0023\u003DzlVz0JivzQhAY().ToDouble(), _param1.\u0023\u003Dzk8BrWRwbV\u0024Y\u0024().ToDouble(), _param1.\u0023\u003Dz89dSIjCLFKC0().ToDouble(), _param1.\u0023\u003DzrRG8qdg_pzoL().ToDouble());
      color1 = nullable2.HasValue ? nullable2.Value : color1;
    }
    return color1;
  }
}
