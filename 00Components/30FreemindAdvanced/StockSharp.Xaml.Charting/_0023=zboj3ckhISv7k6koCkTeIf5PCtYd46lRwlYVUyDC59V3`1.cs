// Decompiled with JetBrains decompiler
// Type: #=zboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
internal sealed class \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>(
  T _param1) : 
  \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<T>(_param1)
  where T : ChartTransactionElement<T>, new()
{
  
  private readonly bool \u0023\u003DzyDntgFbUTuaIjjSSeA\u003D\u003D = (object) _param1 is ChartOrderElement;
  
  private \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D _candleMap;
  
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzKj7nvWQ\u003D;
  
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzZYTLjjg\u003D;

  private TransactionDataSeries \u0023\u003DzcSPUTHNE35Wc()
  {
    return this.\u0023\u003DzKj7nvWQ\u003D?.DataSeries as TransactionDataSeries;
  }

  protected override void \u0023\u003DzY0x9JtY\u003D()
  {
    base.\u0023\u003DzY0x9JtY\u003D();
    this.GetParentVM().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzZYTLjjg\u003D = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(string.Empty, (INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9(), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dq0cVEuBKxrRtQRcGiBDVaAJSkLIlbwCQLRb9RfpzQ\u0024mo\u003D), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DqiBgHXqStJWoTIfDs2MRnGdrXTbDw1HFbm_Laa2\u0024v1lw\u003D), new string[2]
    {
      "",
      ""
    }));
    this.\u0023\u003Dzk_r\u0024wtNtUKwJ(false);
  }

  private void \u0023\u003Dzk_r\u0024wtNtUKwJ(bool _param1)
  {
    \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003Dz9r5QdtX0xdsJ15Nf5Q\u003D\u003D z9r5QdtX0xdsJ15Nf5Q = new \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003Dz9r5QdtX0xdsJ15Nf5Q\u003D\u003D();
    z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzRRvwDu67s9Rm = this;
    z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzMpYhyRe\u00247X_0KjiOU\u0024lgSUE\u003D = _param1;
    if (!UIBaseVM.\u0023\u003Dz03PnGbpCXkrj())
    {
      this.\u0023\u003Dz4EoFHUaZg4JL(new Action(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzRiz68osX2d1uaMGusg\u003D\u003D), true);
    }
    else
    {
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D candles = this.\u0023\u003Dz\u00246aIVrHDxlRJ().CandlesCompositeElement?.Candles;
      if (this._candleMap != candles)
      {
        this.\u0023\u003DzcSPUTHNE35Wc()?.Clear();
        this.\u0023\u003Dz_JPQrgU\u003D();
        this._candleMap = candles;
      }
      if (this.\u0023\u003DzKj7nvWQ\u003D != null)
        return;
      this.\u0023\u003DzB1NfMuK_wtfq(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzMpYhyRe\u00247X_0KjiOU\u0024lgSUE\u003D);
    }
  }

  private \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D \u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D()
  {
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D vmZznA5Vnnsp4Nd0g = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
    {
      this.\u0023\u003DzZYTLjjg\u003D
    });
    \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DzCwfeuzXRdwFc converter1 = new \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DzCwfeuzXRdwFc((Sides) 0);
    \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DzCwfeuzXRdwFc converter2 = new \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>.\u0023\u003DzCwfeuzXRdwFc((Sides) 1);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003DzQOrK5fl6hpfW, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay, (IValueConverter) converter1);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003DzhKUgypnOH4pU, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay, (IValueConverter) converter2);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dzwfm_Z9y6kr\u0024y, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dz0EuC16n_30MR, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003DzRoh3GfgyQiik, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dz37lnfREBVw3J, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay);
    vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dz6RapS3\u0024GgFvT, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay);
    if ((object) this.\u0023\u003DzeaszzAAoBOY9() is IChartOrderElement)
    {
      vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003DzS_RbfR1zd8IE, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay);
      vmZznA5Vnnsp4Nd0g.SetBindings(\u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D.\u0023\u003Dzlf_sEtexhqoz, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay);
    }
    return vmZznA5Vnnsp4Nd0g;
  }

  protected override void \u0023\u003DzXfak0jM\u003D() => this.\u0023\u003Dz_JPQrgU\u003D();

  protected override void \u0023\u003DzowR7R4A\u003D()
  {
    this.\u0023\u003DzcSPUTHNE35Wc()?.Clear();
    this.\u0023\u003Dz4EoFHUaZg4JL(new Action(this.\u0023\u003DztNCzdO3g0AGeNaIYwDSKLi0\u003D), true);
  }

  private void \u0023\u003Dzip81Rn8\u003D()
  {
    if (!this.\u0023\u003DzyDntgFbUTuaIjjSSeA\u003D\u003D)
      return;
    ChartOrderDisplayFilter filter = ((IChartOrderElement) (object) this.\u0023\u003DzeaszzAAoBOY9()).Filter;
    if (!(this.\u0023\u003DzKj7nvWQ\u003D?.RenderSeries is \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A4XoH0BjVmZZNA5VNnsp4ND0g\u003D\u003D renderSeries))
      return;
    renderSeries.ShowError = filter != ChartOrderDisplayFilter.NoErrorsOnly;
    renderSeries.ShowBuy = filter != ChartOrderDisplayFilter.ErrorsOnly;
    renderSeries.ShowSell = filter != ChartOrderDisplayFilter.ErrorsOnly;
  }

  protected override void \u0023\u003Dz3u1qwgvgJlZC(
    IfxChartElement _param1,
    string _param2)
  {
    base.\u0023\u003Dz3u1qwgvgJlZC(_param1, _param2);
    if (!this.\u0023\u003DzyDntgFbUTuaIjjSSeA\u003D\u003D || _param2 != "")
      return;
    this.\u0023\u003Dzip81Rn8\u003D();
  }

  private void \u0023\u003DzB1NfMuK_wtfq(bool _param1)
  {
    if (this.\u0023\u003DzKj7nvWQ\u003D != null)
      throw new InvalidOperationException("");
    if (this._candleMap == null)
    {
      if (_param1)
        throw new InvalidOperationException("");
    }
    else
    {
      this.\u0023\u003DzKj7nvWQ\u003D = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) new TransactionDataSeries(this._candleMap.OhlcSeries), (IRenderableSeries) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D());
      this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBE5I4io\u003D(this.RootElem, (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D) this.\u0023\u003DzKj7nvWQ\u003D);
      this.\u0023\u003Dzip81Rn8\u003D();
    }
  }

  private void \u0023\u003Dz_JPQrgU\u003D()
  {
    if (this.\u0023\u003DzKj7nvWQ\u003D == null)
      return;
    BindingOperations.ClearAllBindings((DependencyObject) this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries);
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003Dzwh_e_TheVZKh(this.RootElem);
    this.\u0023\u003DzKj7nvWQ\u003D = (\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D) null;
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.Draw(CollectionHelper.ToEx<ChartDrawData.sTrade>(((IEnumerable) _param1).Cast<ChartDrawData.sTrade>(), ((IEnumerableEx) _param1).Count));
  }

  private bool Draw(
    IEnumerableEx<ChartDrawData.sTrade> _param1)
  {
    if (_param1 == null || CollectionHelper.IsEmpty<ChartDrawData.sTrade>((IEnumerable<ChartDrawData.sTrade>) _param1))
      return false;
    this.\u0023\u003Dzk_r\u0024wtNtUKwJ(true);
    foreach (ChartDrawData.sTrade trans in (IEnumerable<ChartDrawData.sTrade>) _param1)
      this.\u0023\u003DzcSPUTHNE35Wc().AddOrUpdateTransaction(trans);
    return true;
  }

  private Color \u0023\u003Dq0cVEuBKxrRtQRcGiBDVaAJSkLIlbwCQLRb9RfpzQ\u0024mo\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    if (!(_param1 is dje_zDDJ3D37GQNGTHWK82PDGKZ3UWXHCH9YWML4RSUBC_ejd uwxhcH9YwmL4RsubcEjd))
      return Colors.Transparent;
    return uwxhcH9YwmL4RsubcEjd.Transaction.OrderSides() != null ? this.\u0023\u003DzeaszzAAoBOY9().SellColor : this.\u0023\u003DzeaszzAAoBOY9().BuyColor;
  }

  internal static string \u0023\u003DqiBgHXqStJWoTIfDs2MRnGdrXTbDw1HFbm_Laa2\u0024v1lw\u003D(
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
    public \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T> \u0023\u003DzRRvwDu67s9Rm;
    public bool \u0023\u003DzMpYhyRe\u00247X_0KjiOU\u0024lgSUE\u003D;

    internal void \u0023\u003DzRiz68osX2d1uaMGusg\u003D\u003D()
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dzk_r\u0024wtNtUKwJ(this.\u0023\u003DzMpYhyRe\u00247X_0KjiOU\u0024lgSUE\u003D);
    }
  }

  private sealed class \u0023\u003DzCwfeuzXRdwFc(Sides _param1) : IValueConverter
  {
    
    private readonly Sides \u0023\u003Dzo1CPutg\u003D = _param1;

    object IValueConverter.\u0023\u003DzM9yoqEmGoL\u0024Vcrr_ku1EGJc\u003D(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      if (!(_param1 is bool flag))
        return Binding.DoNothing;
      if (flag)
        return (object) "";
      return this.\u0023\u003Dzo1CPutg\u003D != null ? (object) "" : (object) "";
    }

    object IValueConverter.\u0023\u003Dz7t96kV0doysI1t8U28R3TqlcxXQz(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }
  }
}
