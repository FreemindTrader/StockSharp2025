// Decompiled with JetBrains decompiler
// Type: #=zboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Model.DataSeries.Transactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
public sealed class \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>(
  T _param1) : 
  ChartCompentWpfBaseViewModel<T>(_param1)
  where T : ChartTransactionElement<T>, new()
{
  
  private readonly bool \u0023\u003DzyDntgFbUTuaIjjSSeA\u003D\u003D = (object) _param1 is ChartOrderElement;
  
  private CandlestickUI \u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D;
  
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzKj7nvWQ\u003D;
  
  private ChartElementViewModel \u0023\u003DzZYTLjjg\u003D;

  private TransactionDataSeries \u0023\u003DzcSPUTHNE35Wc()
  {
    return this.\u0023\u003DzKj7nvWQ\u003D?.DataSeries as TransactionDataSeries;
  }

  protected override void Init()
  {
    base.Init();
    this.ChartViewModel.AddChild(this.\u0023\u003DzZYTLjjg\u003D = new ChartElementViewModel(string.Empty, (INotifyPropertyChanged) this.ChartComponentView, new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dq0cVEuBKxrRtQRcGiBDVaAJSkLIlbwCQLRb9RfpzQ\u0024mo\u003D), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DqiBgHXqStJWoTIfDs2MRnGdrXTbDw1HFbm_Laa2\u0024v1lw\u003D), new string[2]
    {
      "BuyColor",
      "SellColor"
    }));
    this.\u0023\u003Dzk_r\u0024wtNtUKwJ(false);
  }

  private void \u0023\u003Dzk_r\u0024wtNtUKwJ(bool _param1)
  {
    \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003Dz9r5QdtX0xdsJ15Nf5Q\u003D\u003D z9r5QdtX0xdsJ15Nf5Q = new \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003Dz9r5QdtX0xdsJ15Nf5Q\u003D\u003D();
    z9r5QdtX0xdsJ15Nf5Q._variableSome3535 = this;
    z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzMpYhyRe\u00247X_0KjiOU\u0024lgSUE\u003D = _param1;
    if (!DrawableChartElementBaseViewModel.IsUiThread())
    {
      this.PerformUiAction(new Action(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzRiz68osX2d1uaMGusg\u003D\u003D), true);
    }
    else
    {
      CandlestickUI candles = this.ScichartSurfaceMVVM.CandlesCompositeElement?.Candles;
      if (this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D != candles)
      {
        this.\u0023\u003DzcSPUTHNE35Wc()?.Clear();
        this.\u0023\u003Dz_JPQrgU\u003D();
        this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D = candles;
      }
      if (this.\u0023\u003DzKj7nvWQ\u003D != null)
        return;
      this.\u0023\u003DzB1NfMuK_wtfq(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzMpYhyRe\u00247X_0KjiOU\u0024lgSUE\u003D);
    }
  }

  private \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D CreateRenderableSeries()
  {
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D vmZznA5Vnnsp4Nd0g = this.CreateRenderableSeries<\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D>(new ChartElementViewModel[1]
    {
      this.\u0023\u003DzZYTLjjg\u003D
    });
    \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DzCwfeuzXRdwFc converter1 = new \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DzCwfeuzXRdwFc((Sides) 0);
    \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DzCwfeuzXRdwFc converter2 = new \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DzCwfeuzXRdwFc((Sides) 1);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003DzQOrK5fl6hpfW, (object) this.ChartComponentView, "UseAltIcon", BindingMode.OneWay, (IValueConverter) converter1);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003DzhKUgypnOH4pU, (object) this.ChartComponentView, "UseAltIcon", BindingMode.OneWay, (IValueConverter) converter2);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dzwfm_Z9y6kr\u0024y, (object) this.ChartComponentView, "DrawSize", BindingMode.OneWay);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dz0EuC16n_30MR, (object) this.ChartComponentView, "BuyColor", BindingMode.OneWay);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003DzRoh3GfgyQiik, (object) this.ChartComponentView, "BuyStrokeColor", BindingMode.OneWay);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dz37lnfREBVw3J, (object) this.ChartComponentView, "SellColor", BindingMode.OneWay);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dz6RapS3\u0024GgFvT, (object) this.ChartComponentView, "SellStrokeColor", BindingMode.OneWay);
    if ((object) this.ChartComponentView is IChartOrderElement)
    {
      vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003DzS_RbfR1zd8IE, (object) this.ChartComponentView, "ErrorColor", BindingMode.OneWay);
      vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dzlf_sEtexhqoz, (object) this.ChartComponentView, "ErrorStrokeColor", BindingMode.OneWay);
    }
    return vmZznA5Vnnsp4Nd0g;
  }

  protected override void Clear() => this.\u0023\u003Dz_JPQrgU\u003D();

  protected override void UpdateUi()
  {
    this.\u0023\u003DzcSPUTHNE35Wc()?.Clear();
    this.PerformUiAction(new Action(this.\u0023\u003DztNCzdO3g0AGeNaIYwDSKLi0\u003D), true);
  }

  private void \u0023\u003Dzip81Rn8\u003D()
  {
    if (!this.\u0023\u003DzyDntgFbUTuaIjjSSeA\u003D\u003D)
      return;
    ChartOrderDisplayFilter filter = ((IChartOrderElement) (object) this.ChartComponentView).Filter;
    if (!(this.\u0023\u003DzKj7nvWQ\u003D?.RenderSeries is \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D renderSeries))
      return;
    renderSeries.ShowError = filter != ChartOrderDisplayFilter.NoErrorsOnly;
    renderSeries.ShowBuy = filter != ChartOrderDisplayFilter.ErrorsOnly;
    renderSeries.ShowSell = filter != ChartOrderDisplayFilter.ErrorsOnly;
  }

  protected override void RootElementPropertyChanged(
    IChartComponent _param1,
    string _param2)
  {
    base.RootElementPropertyChanged(_param1, _param2);
    if (!this.\u0023\u003DzyDntgFbUTuaIjjSSeA\u003D\u003D || _param2 != "Filter")
      return;
    this.\u0023\u003Dzip81Rn8\u003D();
  }

  private void \u0023\u003DzB1NfMuK_wtfq(bool _param1)
  {
    if (this.\u0023\u003DzKj7nvWQ\u003D != null)
      throw new InvalidOperationException("vm is not null");
    if (this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D == null)
    {
      if (_param1)
        throw new InvalidOperationException("unable to draw transactions on a chart without candles");
    }
    else
    {
      this.\u0023\u003DzKj7nvWQ\u003D = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) new TransactionDataSeries(this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D.OhlcSeries), (IRenderableSeries) this.CreateRenderableSeries());
      this.ScichartSurfaceMVVM.\u0023\u003DzBE5I4io\u003D(this.RootElem, (IRenderableSeries) this.\u0023\u003DzKj7nvWQ\u003D);
      this.\u0023\u003Dzip81Rn8\u003D();
    }
  }

  private void \u0023\u003Dz_JPQrgU\u003D()
  {
    if (this.\u0023\u003DzKj7nvWQ\u003D == null)
      return;
    BindingOperations.ClearAllBindings((DependencyObject) this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries);
    this.ScichartSurfaceMVVM.\u0023\u003Dzwh_e_TheVZKh(this.RootElem);
    this.\u0023\u003DzKj7nvWQ\u003D = (\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D) null;
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.Draw(CollectionHelper.ToEx<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>(((IEnumerable) _param1).Cast<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>(), ((IEnumerableEx) _param1).Count));
  }

  private bool Draw(
    IEnumerableEx<ChartDrawData.\u0023\u003DzU3TaXFs\u003D> _param1)
  {
    if (_param1 == null || CollectionHelper.IsEmpty<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>((IEnumerable<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>) _param1))
      return false;
    this.\u0023\u003Dzk_r\u0024wtNtUKwJ(true);
    foreach (ChartDrawData.\u0023\u003DzU3TaXFs\u003D trans in (IEnumerable<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>) _param1)
      this.\u0023\u003DzcSPUTHNE35Wc().AddOrUpdateTransaction(trans);
    return true;
  }

  private Color \u0023\u003Dq0cVEuBKxrRtQRcGiBDVaAJSkLIlbwCQLRb9RfpzQ\u0024mo\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    if (!(_param1 is dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd uwxhcH9YwmL4RsubcEjd))
      return Colors.Transparent;
    return uwxhcH9YwmL4RsubcEjd.Transaction.\u0023\u003DzUYTxG_Bgl8ih() != null ? this.ChartComponentView.SellColor : this.ChartComponentView.BuyColor;
  }

  public static string \u0023\u003DqiBgHXqStJWoTIfDs2MRnGdrXTbDw1HFbm_Laa2\u0024v1lw\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param0)
  {
    return _param0 is dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd uwxhcH9YwmL4RsubcEjd ? uwxhcH9YwmL4RsubcEjd.Action : string.Empty;
  }

  private void \u0023\u003DztNCzdO3g0AGeNaIYwDSKLi0\u003D()
  {
    this.\u0023\u003Dz_JPQrgU\u003D();
    this.\u0023\u003DzB1NfMuK_wtfq(false);
  }

  private sealed class \u0023\u003Dz9r5QdtX0xdsJ15Nf5Q\u003D\u003D
  {
    public \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T> _variableSome3535;
    public bool \u0023\u003DzMpYhyRe\u00247X_0KjiOU\u0024lgSUE\u003D;

    public void \u0023\u003DzRiz68osX2d1uaMGusg\u003D\u003D()
    {
      this._variableSome3535.\u0023\u003Dzk_r\u0024wtNtUKwJ(this.\u0023\u003DzMpYhyRe\u00247X_0KjiOU\u0024lgSUE\u003D);
    }
  }

  private sealed class \u0023\u003DzCwfeuzXRdwFc(Sides _param1) : IValueConverter
  {
    
    private readonly Sides \u0023\u003Dzo1CPutg\u003D = _param1;

    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      if (!(_param1 is bool flag))
        return Binding.DoNothing;
      if (flag)
        return (object) "BuySellCircle";
      return this.\u0023\u003Dzo1CPutg\u003D != null ? (object) "Down2" : (object) "Up2";
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }
  }
}
