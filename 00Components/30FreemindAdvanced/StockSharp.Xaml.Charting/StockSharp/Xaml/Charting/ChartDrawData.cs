// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartDrawData
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>Chart drawing data.</summary>
public class ChartDrawData : IChartDrawData
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>> \u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>> \u0023\u003Dz3mmVBunXGsFQZ9EtJw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>> \u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartOrderElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>> \u0023\u003DzFL6oOk7l7AmA;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartTradeElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>> \u0023\u003DzW6Dk_spcRYsL;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>> \u0023\u003Dz4fpZVTfoaMgNvfE8Ng\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>> \u0023\u003Dz6X\u0024wSK2o_hkW;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>> \u0023\u003DzPFyjopyhj6ky;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>> \u0023\u003DzCjHRT3IUXyJaPuaHpg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>> \u0023\u003DzeKPa3YtJH2v6RS3JOQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<IChartAnnotationElement, IAnnotationData> \u0023\u003DzfS3q6Qc\u003D;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartDrawData" />.
  /// </summary>
  public ChartDrawData()
  {
  }

  internal ChartDrawData(
    IEnumerable<RefPair<DateTimeOffset, IDictionary<IChartElement, object>>> _param1)
  {
    if (_param1 == null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427367));
    foreach (RefPair<DateTimeOffset, IDictionary<IChartElement, object>> refPair in _param1)
    {
      DateTimeOffset first = refPair.First;
      foreach (KeyValuePair<IChartElement, object> keyValuePair in (IEnumerable<KeyValuePair<IChartElement, object>>) refPair.Second)
      {
        IChartElement key = keyValuePair.Key;
        object obj = keyValuePair.Value;
        switch (key)
        {
          case IChartCandleElement chartCandleElement:
            if (obj is ICandleMessage icandleMessage)
            {
              CollectionHelper.SafeAdd<IChartCandleElement, List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>>((IDictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>>) this.\u0023\u003DzOrWGMasVFnPJ9EIggA\u003D\u003D(), chartCandleElement).Add(new ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D(first, icandleMessage.DataType, icandleMessage.OpenPrice, icandleMessage.HighPrice, icandleMessage.LowPrice, icandleMessage.ClosePrice, icandleMessage.PriceLevels));
              continue;
            }
            if (obj is System.Windows.Media.Color color)
            {
              CollectionHelper.SafeAdd<IChartCandleElement, List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>>((IDictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>>) this.\u0023\u003Dz\u0024lu1PoF9036WD8AKeQ\u003D\u003D(), chartCandleElement).Add(new ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D(first, new System.Windows.Media.Color?(color)));
              continue;
            }
            continue;
          case IChartIndicatorElement indicatorElement:
            IIndicatorValue val = (IIndicatorValue) obj;
            CollectionHelper.SafeAdd<IChartIndicatorElement, List<ChartDrawData.IndicatorData>>((IDictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>>) this.\u0023\u003DzMvN\u0024j0JXehj_W7wJCsA5vYc\u003D(), indicatorElement).Add(new ChartDrawData.IndicatorData(first, val));
            continue;
          case IChartOrderElement chartOrderElement:
            Order order = (Order) obj;
            CollectionHelper.SafeAdd<IChartOrderElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>((IDictionary<IChartOrderElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>) this.\u0023\u003Dz_sjyNtZSz2JXDm4nPw\u003D\u003D(), chartOrderElement).Add(new ChartDrawData.\u0023\u003DzU3TaXFs\u003D(first, order.TransactionId, (string) null, order.Side, order.Price, order.Volume, order.State != 3 ? (string) null : LocalizedStrings.Failed, true));
            continue;
          case IChartTradeElement chartTradeElement:
            MyTrade myTrade = (MyTrade) obj;
            Trade trade = myTrade.Trade;
            CollectionHelper.SafeAdd<IChartTradeElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>((IDictionary<IChartTradeElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>) this.\u0023\u003Dz\u0024VcyBPrNElR\u0024tFZL2A\u003D\u003D(), chartTradeElement).Add(new ChartDrawData.\u0023\u003DzU3TaXFs\u003D(first, trade.Id.GetValueOrDefault(), trade.StringId, myTrade.Order.Side, trade.Price, trade.Volume, (string) null, false));
            continue;
          case IChartActiveOrdersElement activeOrdersElement:
            CollectionHelper.SafeAdd<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>>((IDictionary<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>>) this.\u0023\u003Dz9iIPMoqLxvOreh0Dog\u003D\u003D(), activeOrdersElement).Add((ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D) obj);
            continue;
          default:
            throw new ArgumentException(StringHelper.Put(LocalizedStrings.UnknownType, new object[1]
            {
              (object) key
            }));
        }
      }
    }
  }

  private Dictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>> \u0023\u003DzOrWGMasVFnPJ9EIggA\u003D\u003D()
  {
    return this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D ?? (this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D = new Dictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>>());
  }

  private Dictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>> \u0023\u003Dz\u0024lu1PoF9036WD8AKeQ\u003D\u003D()
  {
    return this.\u0023\u003Dz3mmVBunXGsFQZ9EtJw\u003D\u003D ?? (this.\u0023\u003Dz3mmVBunXGsFQZ9EtJw\u003D\u003D = new Dictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>>());
  }

  private Dictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>> \u0023\u003DzMvN\u0024j0JXehj_W7wJCsA5vYc\u003D()
  {
    return this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D ?? (this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D = new Dictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>>());
  }

  private Dictionary<IChartOrderElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>> \u0023\u003Dz_sjyNtZSz2JXDm4nPw\u003D\u003D()
  {
    return this.\u0023\u003DzFL6oOk7l7AmA ?? (this.\u0023\u003DzFL6oOk7l7AmA = new Dictionary<IChartOrderElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>());
  }

  private Dictionary<IChartTradeElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>> \u0023\u003Dz\u0024VcyBPrNElR\u0024tFZL2A\u003D\u003D()
  {
    return this.\u0023\u003DzW6Dk_spcRYsL ?? (this.\u0023\u003DzW6Dk_spcRYsL = new Dictionary<IChartTradeElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>());
  }

  private Dictionary<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>> \u0023\u003Dz9iIPMoqLxvOreh0Dog\u003D\u003D()
  {
    return this.\u0023\u003Dz4fpZVTfoaMgNvfE8Ng\u003D\u003D ?? (this.\u0023\u003Dz4fpZVTfoaMgNvfE8Ng\u003D\u003D = new Dictionary<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>>());
  }

  private Dictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>> \u0023\u003DzIfFLwHfr6UGj()
  {
    return this.\u0023\u003Dz6X\u0024wSK2o_hkW ?? (this.\u0023\u003Dz6X\u0024wSK2o_hkW = new Dictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>>());
  }

  private Dictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>> \u0023\u003DzwTJ32ssHOG49()
  {
    return this.\u0023\u003DzPFyjopyhj6ky ?? (this.\u0023\u003DzPFyjopyhj6ky = new Dictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>>());
  }

  private Dictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>> \u0023\u003DzaJmO2TNyAvUf()
  {
    return this.\u0023\u003DzCjHRT3IUXyJaPuaHpg\u003D\u003D ?? (this.\u0023\u003DzCjHRT3IUXyJaPuaHpg\u003D\u003D = new Dictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>>());
  }

  private Dictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>> \u0023\u003DzL5dW_Xfrnb5k()
  {
    return this.\u0023\u003DzeKPa3YtJH2v6RS3JOQ\u003D\u003D ?? (this.\u0023\u003DzeKPa3YtJH2v6RS3JOQ\u003D\u003D = new Dictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>>());
  }

  private Dictionary<IChartAnnotationElement, IAnnotationData> \u0023\u003DzatSa84WWjykt()
  {
    return this.\u0023\u003DzfS3q6Qc\u003D ?? (this.\u0023\u003DzfS3q6Qc\u003D = new Dictionary<IChartAnnotationElement, IAnnotationData>());
  }

  /// <inheritdoc />
  public IChartDrawData.IChartDrawDataItem Group(DateTimeOffset timeStamp)
  {
    return (IChartDrawData.IChartDrawDataItem) new ChartDrawData.ChartDrawDataItem(this, timeStamp);
  }

  /// <inheritdoc />
  public IChartDrawData.IChartDrawDataItem Group(double xValue)
  {
    return (IChartDrawData.IChartDrawDataItem) new ChartDrawData.ChartDrawDataItem(this, xValue);
  }

  /// <inheritdoc />
  public IChartDrawData Add(IChartAnnotationElement element, IAnnotationData data)
  {
    Dictionary<IChartAnnotationElement, IAnnotationData> dictionary = this.\u0023\u003DzatSa84WWjykt();
    IChartAnnotationElement key = element;
    dictionary[key] = data ?? throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427374));
    return (IChartDrawData) this;
  }

  /// <inheritdoc />
  public IChartDrawData Add(
    IChartActiveOrdersElement element,
    Order order,
    bool? isFrozen = null,
    bool autoRemoveFromChart = true,
    bool isHidden = false,
    bool? isError = null,
    Decimal? price = null,
    Decimal? balance = null,
    OrderStates? state = null)
  {
    state.GetValueOrDefault();
    if (!state.HasValue)
      state = new OrderStates?(order != null ? order.State : (OrderStates) (object) 0);
    Decimal valueOrDefault = price.GetValueOrDefault();
    if (!price.HasValue)
    {
      // ISSUE: explicit non-virtual call
      price = order != null ? new Decimal?(__nonvirtual (order.Price)) : throw new ArgumentException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427417));
    }
    valueOrDefault = balance.GetValueOrDefault();
    if (!balance.HasValue)
      balance = new Decimal?(order != null ? order.Balance : 0M);
    isFrozen.GetValueOrDefault();
    if (!isFrozen.HasValue)
    {
      OrderStates? nullable = state;
      OrderStates orderStates = (OrderStates) 0;
      isFrozen = new bool?(nullable.GetValueOrDefault() == orderStates & nullable.HasValue || state.GetValueOrDefault() == 4);
    }
    CollectionHelper.SafeAdd<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>>((IDictionary<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>>) this.\u0023\u003Dz9iIPMoqLxvOreh0Dog\u003D\u003D(), element).Add(new ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D(order, balance.Value, state.Value, (Decimal?) order?.Security?.PriceStep ?? 0.01M, autoRemoveFromChart, isFrozen.Value, isHidden, ((int) isError ?? (state.GetValueOrDefault() == 3 ? 1 : 0)) != 0, price.Value));
    return (IChartDrawData) this;
  }

  internal List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D> \u0023\u003DzaZ5Qc3xeNY95(
    IChartCandleElement _param1)
  {
    Dictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>> a4BPs40sQtxmCo6Ew = this.\u0023\u003DzA4B\u0024PS40sQTxmCO6Ew\u003D\u003D;
    return a4BPs40sQtxmCo6Ew == null ? (List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>) null : CollectionHelper.TryGetValue<IChartCandleElement, List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>>((IDictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>>) a4BPs40sQtxmCo6Ew, _param1);
  }

  internal List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D> \u0023\u003DzCEKAoZ7e0Ko9(
    IChartCandleElement _param1)
  {
    Dictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>> z3mmVbunXgsFqZ9EtJw = this.\u0023\u003Dz3mmVBunXGsFQZ9EtJw\u003D\u003D;
    return z3mmVbunXgsFqZ9EtJw == null ? (List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>) null : CollectionHelper.TryGetValue<IChartCandleElement, List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>>((IDictionary<IChartCandleElement, List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>>) z3mmVbunXgsFqZ9EtJw, _param1);
  }

  internal List<ChartDrawData.IndicatorData> \u0023\u003DzaZ5Qc3xeNY95(
    IChartIndicatorElement _param1)
  {
    Dictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>> ireF1JupHymGa845Q = this.\u0023\u003Dz\u0024IReF1JUpHYmGa845Q\u003D\u003D;
    return ireF1JupHymGa845Q == null ? (List<ChartDrawData.IndicatorData>) null : CollectionHelper.TryGetValue<IChartIndicatorElement, List<ChartDrawData.IndicatorData>>((IDictionary<IChartIndicatorElement, List<ChartDrawData.IndicatorData>>) ireF1JupHymGa845Q, _param1);
  }

  internal List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D> \u0023\u003DzaZ5Qc3xeNY95(
    IChartOrderElement _param1)
  {
    Dictionary<IChartOrderElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>> zFl6oOk7l7AmA = this.\u0023\u003DzFL6oOk7l7AmA;
    return zFl6oOk7l7AmA == null ? (List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>) null : CollectionHelper.TryGetValue<IChartOrderElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>((IDictionary<IChartOrderElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>) zFl6oOk7l7AmA, _param1);
  }

  internal List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D> \u0023\u003DzaZ5Qc3xeNY95(
    IChartTradeElement _param1)
  {
    Dictionary<IChartTradeElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>> zW6DkSpcRysL = this.\u0023\u003DzW6Dk_spcRYsL;
    return zW6DkSpcRysL == null ? (List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>) null : CollectionHelper.TryGetValue<IChartTradeElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>((IDictionary<IChartTradeElement, List<ChartDrawData.\u0023\u003DzU3TaXFs\u003D>>) zW6DkSpcRysL, _param1);
  }

  internal List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D> \u0023\u003DzaZ5Qc3xeNY95(
    IChartActiveOrdersElement _param1)
  {
    Dictionary<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>> z4fpZvTfoaMgNvfE8Ng = this.\u0023\u003Dz4fpZVTfoaMgNvfE8Ng\u003D\u003D;
    return z4fpZvTfoaMgNvfE8Ng == null ? (List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>) null : CollectionHelper.TryGetValue<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>>((IDictionary<IChartActiveOrdersElement, List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>>) z4fpZvTfoaMgNvfE8Ng, _param1);
  }

  internal List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>> \u0023\u003Dz_XHLxEJ0hNof(
    IChartLineElement _param1)
  {
    Dictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>> z6XWSk2oHkW = this.\u0023\u003Dz6X\u0024wSK2o_hkW;
    return z6XWSk2oHkW == null ? (List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>) null : CollectionHelper.TryGetValue<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>>((IDictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>>) z6XWSk2oHkW, _param1);
  }

  internal List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>> \u0023\u003DzAMdsOQvnof32(
    IChartLineElement _param1)
  {
    Dictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>> zPfyjopyhj6ky = this.\u0023\u003DzPFyjopyhj6ky;
    return zPfyjopyhj6ky == null ? (List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>) null : CollectionHelper.TryGetValue<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>>((IDictionary<IChartLineElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>>) zPfyjopyhj6ky, _param1);
  }

  internal List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>> \u0023\u003Dz_XHLxEJ0hNof(
    IChartBandElement _param1)
  {
    Dictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>> hrT3IuXyJaPuaHpg = this.\u0023\u003DzCjHRT3IUXyJaPuaHpg\u003D\u003D;
    return hrT3IuXyJaPuaHpg == null ? (List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>) null : CollectionHelper.TryGetValue<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>>((IDictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>>) hrT3IuXyJaPuaHpg, _param1);
  }

  internal List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>> \u0023\u003DzAMdsOQvnof32(
    IChartBandElement _param1)
  {
    Dictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>> kpa3YtJh2v6Rs3Joq = this.\u0023\u003DzeKPa3YtJH2v6RS3JOQ\u003D\u003D;
    return kpa3YtJh2v6Rs3Joq == null ? (List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>) null : CollectionHelper.TryGetValue<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>>((IDictionary<IChartBandElement, List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>>) kpa3YtJh2v6Rs3Joq, _param1);
  }

  internal IEnumerableEx<ChartDrawData.IDrawValue> \u0023\u003DzaZ5Qc3xeNY95(
    IChartLineElement _param1)
  {
    List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>> source1 = this.\u0023\u003Dz_XHLxEJ0hNof(_param1);
    if (source1 != null && source1.Count > 0)
      return CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source1.Cast<ChartDrawData.IDrawValue>(), source1.Count);
    List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>> source2 = this.\u0023\u003DzAMdsOQvnof32(_param1);
    return source2 == null ? (IEnumerableEx<ChartDrawData.IDrawValue>) null : CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source2.Cast<ChartDrawData.IDrawValue>(), source2.Count);
  }

  internal IEnumerableEx<ChartDrawData.IDrawValue> \u0023\u003DzaZ5Qc3xeNY95(
    IChartBandElement _param1)
  {
    List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>> source1 = this.\u0023\u003Dz_XHLxEJ0hNof(_param1);
    if (source1 != null && source1.Count > 0)
      return CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source1.Cast<ChartDrawData.IDrawValue>(), source1.Count);
    List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>> source2 = this.\u0023\u003DzAMdsOQvnof32(_param1);
    return source2 == null ? (IEnumerableEx<ChartDrawData.IDrawValue>) null : CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source2.Cast<ChartDrawData.IDrawValue>(), source2.Count);
  }

  internal ChartDrawData.AnnotationData \u0023\u003Dzp_r3R3U\u003D(IChartAnnotationElement _param1)
  {
    Dictionary<IChartAnnotationElement, IAnnotationData> zfS3q6Qc = this.\u0023\u003DzfS3q6Qc\u003D;
    return zfS3q6Qc != null ? (ChartDrawData.AnnotationData) CollectionHelper.TryGetValue<IChartAnnotationElement, IAnnotationData>((IDictionary<IChartAnnotationElement, IAnnotationData>) zfS3q6Qc, _param1) : (ChartDrawData.AnnotationData) null;
  }

  internal readonly struct \u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D> : 
    ChartDrawData.IDrawValue
    where \u0023\u003DzulcL8RA\u003D : struct, IComparable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly \u0023\u003DzulcL8RA\u003D \u0023\u003Dzi356d4J_BEO1MM1iHw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double \u0023\u003DzOo4y0SHpsMppCYgBgQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double \u0023\u003DzHsqtIm4nTXsCXtUzPQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int \u0023\u003DzQEv2E_7fpX84HUnmDA\u003D\u003D;

    private \u0023\u003Dz6MdlWkBS_h\u00244(double _param1, double _param2, double _param3)
      : this((\u0023\u003DzulcL8RA\u003D) (ValueType) _param1, _param2, _param3)
    {
    }

    private \u0023\u003Dz6MdlWkBS_h\u00244(DateTime _param1, double _param2, double _param3)
      : this((\u0023\u003DzulcL8RA\u003D) (ValueType) _param1, _param2, _param3)
    {
    }

    private \u0023\u003Dz6MdlWkBS_h\u00244(
      \u0023\u003DzulcL8RA\u003D _param1,
      double _param2,
      double _param3)
    {
      this.\u0023\u003DzQEv2E_7fpX84HUnmDA\u003D\u003D = 0;
      this.\u0023\u003Dzi356d4J_BEO1MM1iHw\u003D\u003D = _param1;
      this.\u0023\u003DzOo4y0SHpsMppCYgBgQ\u003D\u003D = _param2;
      this.\u0023\u003DzHsqtIm4nTXsCXtUzPQ\u003D\u003D = _param3;
    }

    public \u0023\u003DzulcL8RA\u003D \u0023\u003Dz2_4KSTY\u003D()
    {
      return this.\u0023\u003Dzi356d4J_BEO1MM1iHw\u003D\u003D;
    }

    public double \u0023\u003DzZB\u0024O5xT4bzKv()
    {
      return this.\u0023\u003DzOo4y0SHpsMppCYgBgQ\u003D\u003D;
    }

    public double \u0023\u003Dzggdh\u0024\u00245CXRMA()
    {
      return this.\u0023\u003DzHsqtIm4nTXsCXtUzPQ\u003D\u003D;
    }

    public int \u0023\u003Dz6qkMxm4QKemy() => this.\u0023\u003DzQEv2E_7fpX84HUnmDA\u003D\u003D;

    public void \u0023\u003DzGs5EgMMiGKcz(int _param1)
    {
      this.\u0023\u003DzQEv2E_7fpX84HUnmDA\u003D\u003D = _param1;
    }

    public static ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D> \u0023\u003DzpxJeWbQ\u003D<TXOrig>(
      TXOrig _param0,
      double _param1,
      double _param2)
      where TXOrig : struct, IComparable
    {
      if ((ValueType) _param0 is DateTimeOffset)
        return new ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>(Converter.To<DateTimeOffset>((object) _param0).UtcDateTime, _param1, _param2);
      if ((ValueType) _param0 is double)
        return new ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>(Converter.To<double>((object) _param0), _param1, _param2);
      throw new NotSupportedException(StringHelper.Put(LocalizedStrings.UnsupportedType, new object[1]
      {
        (object) typeof (TXOrig).Name
      }));
    }

    public static ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D> \u0023\u003DzbmumV0s\u003D(
      \u0023\u003DzulcL8RA\u003D _param0)
    {
      return new ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>(_param0, double.NaN, double.NaN);
    }

    public static ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D> \u0023\u003DzpxJeWbQ\u003D(
      \u0023\u003DzulcL8RA\u003D _param0,
      double _param1,
      double _param2,
      int _param3)
    {
      ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D> z6MdlWkBsH4 = new ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>(_param0, _param1, _param2);
      z6MdlWkBsH4.\u0023\u003DzGs5EgMMiGKcz(_param3);
      return z6MdlWkBsH4;
    }
  }

  internal sealed class \u0023\u003DzU3TaXFs\u003D : ChartDrawData.IDrawValue
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string \u0023\u003Dz0tbDXgRR3FkF;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly long \u0023\u003DzSjIW5nBSyyCc;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly DateTime \u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Sides \u0023\u003DzZgovNezOGNlHK2a0Ow\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double \u0023\u003Dz3G68FsAyTsTPL5Tu9cevQzk\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly long \u0023\u003Dz3MLQsd3EnU9BIGBvjQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string \u0023\u003Dz08Q5EpJM\u0024ZwaBqbCsg\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool \u0023\u003DzQpEWlZ0T5BXiRgBNyQ\u003D\u003D;

    public \u0023\u003DzU3TaXFs\u003D(
      DateTimeOffset _param1,
      long _param2,
      string _param3,
      Sides _param4,
      Decimal _param5,
      Decimal _param6,
      string _param7,
      bool _param8)
    {
      this.\u0023\u003Dz0tbDXgRR3FkF = _param2 == 0L ? _param3 : (string) null;
      this.\u0023\u003DzSjIW5nBSyyCc = _param2;
      this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D = _param1.UtcDateTime;
      this.\u0023\u003DzZgovNezOGNlHK2a0Ow\u003D\u003D = _param4;
      this.\u0023\u003Dz3G68FsAyTsTPL5Tu9cevQzk\u003D = (double) _param5;
      this.\u0023\u003Dz3MLQsd3EnU9BIGBvjQ\u003D\u003D = (long) _param6;
      this.\u0023\u003Dz08Q5EpJM\u0024ZwaBqbCsg\u003D\u003D = _param7;
      this.\u0023\u003DzQpEWlZ0T5BXiRgBNyQ\u003D\u003D = _param8;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    public DateTime \u0023\u003Dzg86amuQ\u003D()
    {
      return this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;
    }

    public string \u0023\u003DzDmTtC9WghRFa()
    {
      return this.\u0023\u003Dz0tbDXgRR3FkF ?? (this.\u0023\u003Dz0tbDXgRR3FkF = this.\u0023\u003DzSjIW5nBSyyCc.ToString());
    }

    public Sides \u0023\u003DzUYTxG_Bgl8ih() => this.\u0023\u003DzZgovNezOGNlHK2a0Ow\u003D\u003D;

    public double \u0023\u003DzbH5YDNBwpnry() => this.\u0023\u003Dz3G68FsAyTsTPL5Tu9cevQzk\u003D;

    public long Volume => this.\u0023\u003Dz3MLQsd3EnU9BIGBvjQ\u003D\u003D;

    public string \u0023\u003Dzj4eGSep8GqT3()
    {
      return this.\u0023\u003Dz08Q5EpJM\u0024ZwaBqbCsg\u003D\u003D;
    }

    public bool \u0023\u003DzHE7yEzyy7k7_() => this.\u0023\u003DzQpEWlZ0T5BXiRgBNyQ\u003D\u003D;

    public bool IsError => !StringHelper.IsEmptyOrWhiteSpace(this.\u0023\u003Dzj4eGSep8GqT3());

    public override string ToString()
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(16 /*0x10*/, 6);
      interpolatedStringHandler.AppendFormatted(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427554));
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427356));
      interpolatedStringHandler.AppendFormatted(this.\u0023\u003DzDmTtC9WghRFa());
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427336));
      interpolatedStringHandler.AppendFormatted<DateTime>(this.\u0023\u003Dzg86amuQ\u003D());
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427378));
      interpolatedStringHandler.AppendFormatted<Sides>(this.\u0023\u003DzUYTxG_Bgl8ih());
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432316));
      interpolatedStringHandler.AppendFormatted<long>(this.Volume);
      interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427391));
      interpolatedStringHandler.AppendFormatted<double>(this.\u0023\u003DzbH5YDNBwpnry());
      return interpolatedStringHandler.ToStringAndClear();
    }
  }

  internal readonly struct \u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D : ChartDrawData.IDrawValue
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly DateTime \u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly DataType \u0023\u003DzdgcgaziC7FxHmFtv9g\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double \u0023\u003DziHpFjupL57O5oHu1Mw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double \u0023\u003DzD8EFLWLAF8etf1TLXA\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double \u0023\u003DzByFaQYKUGa5UYX0fCQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double \u0023\u003DzIo3eteZ50RUQ\u0024tgctA\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly CandlePriceLevel[] \u0023\u003DzUNPJK31aZC4jEiC7vA\u003D\u003D;

    public \u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D(
      DateTimeOffset _param1,
      DataType _param2,
      Decimal _param3,
      Decimal _param4,
      Decimal _param5,
      Decimal _param6,
      IEnumerable<CandlePriceLevel> _param7)
    {
      this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D = _param1.UtcDateTime;
      this.\u0023\u003DzdgcgaziC7FxHmFtv9g\u003D\u003D = _param2 ?? throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427521));
      this.\u0023\u003DziHpFjupL57O5oHu1Mw\u003D\u003D = (double) _param3;
      this.\u0023\u003DzD8EFLWLAF8etf1TLXA\u003D\u003D = (double) _param4;
      this.\u0023\u003DzByFaQYKUGa5UYX0fCQ\u003D\u003D = (double) _param5;
      this.\u0023\u003DzIo3eteZ50RUQ\u0024tgctA\u003D\u003D = (double) _param6;
      this.\u0023\u003DzUNPJK31aZC4jEiC7vA\u003D\u003D = _param7 != null ? _param7.ToArray<CandlePriceLevel>() : (CandlePriceLevel[]) null;
    }

    public DateTime \u0023\u003Dzg86amuQ\u003D()
    {
      return this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;
    }

    public DataType \u0023\u003DzdR0PhFO4Br84() => this.\u0023\u003DzdgcgaziC7FxHmFtv9g\u003D\u003D;

    public double \u0023\u003DzGze4a8XU7KvB() => this.\u0023\u003DziHpFjupL57O5oHu1Mw\u003D\u003D;

    public double \u0023\u003DzolXXlhDBER_c() => this.\u0023\u003DzD8EFLWLAF8etf1TLXA\u003D\u003D;

    public double \u0023\u003DzchuwVU\u00245sIH8()
    {
      return this.\u0023\u003DzByFaQYKUGa5UYX0fCQ\u003D\u003D;
    }

    public double Close => this.\u0023\u003DzIo3eteZ50RUQ\u0024tgctA\u003D\u003D;

    public CandlePriceLevel[] \u0023\u003Dzeu8tE1P9bfD8()
    {
      return this.\u0023\u003DzUNPJK31aZC4jEiC7vA\u003D\u003D;
    }
  }

  internal readonly struct \u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D : 
    ChartDrawData.IDrawValue
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly DateTime \u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly System.Windows.Media.Color? \u0023\u003DzAxbyTBB0PvNop5R0Pg\u003D\u003D;

    public \u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D(DateTimeOffset _param1, System.Windows.Media.Color? _param2)
    {
      this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D = _param1.UtcDateTime;
      this.\u0023\u003DzAxbyTBB0PvNop5R0Pg\u003D\u003D = _param2;
    }

    public DateTime \u0023\u003Dzg86amuQ\u003D()
    {
      return this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;
    }

    public System.Windows.Media.Color? Color => this.\u0023\u003DzAxbyTBB0PvNop5R0Pg\u003D\u003D;
  }

  internal readonly struct \u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D : ChartDrawData.IDrawValue
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Order \u0023\u003Dz4\u00248iX2\u0024NTce5pVGZ5A\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Decimal \u0023\u003DzXj8Y9RlEtrX1U1I1Eg\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly OrderStates \u0023\u003Dz2NuYvWXuCZCqNlHg9w\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Decimal \u0023\u003Dz97RDdiI3O4NqwPctSvrh_bI\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool \u0023\u003DzsnB_gYU5TL6yIoS2YB\u0024_LhE\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool \u0023\u003Dzv4Ros9fpAMv35I6yEw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool \u0023\u003DzSzVwji8YCShTexyi6g\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool \u0023\u003DzQempeQBdiy_DpilZpg\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Decimal \u0023\u003Dz3G68FsAyTsTPL5Tu9cevQzk\u003D;

    public \u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D(
      Order _param1,
      Decimal _param2,
      OrderStates _param3,
      Decimal _param4,
      bool _param5,
      bool _param6,
      bool _param7,
      bool _param8,
      Decimal _param9)
    {
      Order order = _param1 != null || _param8 ? _param1 : throw new ArgumentException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434416));
      if (order == null)
        order = new Order()
        {
          State = (OrderStates) 3,
          Volume = _param2,
          Balance = _param2
        };
      this.\u0023\u003Dz4\u00248iX2\u0024NTce5pVGZ5A\u003D\u003D = order;
      this.\u0023\u003DzXj8Y9RlEtrX1U1I1Eg\u003D\u003D = _param2;
      this.\u0023\u003Dz2NuYvWXuCZCqNlHg9w\u003D\u003D = _param1 == null ? (OrderStates) (object) 3 : _param3;
      this.\u0023\u003Dz97RDdiI3O4NqwPctSvrh_bI\u003D = _param4;
      this.\u0023\u003DzsnB_gYU5TL6yIoS2YB\u0024_LhE\u003D = _param5 || _param1 == null;
      this.\u0023\u003Dzv4Ros9fpAMv35I6yEw\u003D\u003D = _param6 || _param1 == null;
      this.\u0023\u003DzSzVwji8YCShTexyi6g\u003D\u003D = _param7;
      this.\u0023\u003DzQempeQBdiy_DpilZpg\u003D\u003D = _param8;
      this.\u0023\u003Dz3G68FsAyTsTPL5Tu9cevQzk\u003D = _param9;
    }

    public Order \u0023\u003DzEbEKEpf9EiRR()
    {
      return this.\u0023\u003Dz4\u00248iX2\u0024NTce5pVGZ5A\u003D\u003D;
    }

    public Decimal \u0023\u003DzP9vQqYe3EED\u0024()
    {
      return this.\u0023\u003DzXj8Y9RlEtrX1U1I1Eg\u003D\u003D;
    }

    public OrderStates \u0023\u003Dzj7Cw0iE\u003D()
    {
      return this.\u0023\u003Dz2NuYvWXuCZCqNlHg9w\u003D\u003D;
    }

    public Decimal \u0023\u003DzTmtGqP_rl3YU6gjEDQ\u003D\u003D()
    {
      return this.\u0023\u003Dz97RDdiI3O4NqwPctSvrh_bI\u003D;
    }

    public bool \u0023\u003DzzTd2XsqYavfdlfkXJw\u003D\u003D()
    {
      return this.\u0023\u003DzsnB_gYU5TL6yIoS2YB\u0024_LhE\u003D;
    }

    public bool \u0023\u003DzOWugxxYxGyL4() => this.\u0023\u003Dzv4Ros9fpAMv35I6yEw\u003D\u003D;

    public bool \u0023\u003DzM6hjJk33XfMg() => this.\u0023\u003DzSzVwji8YCShTexyi6g\u003D\u003D;

    public bool IsError => this.\u0023\u003DzQempeQBdiy_DpilZpg\u003D\u003D;

    public Decimal \u0023\u003DzbH5YDNBwpnry() => this.\u0023\u003Dz3G68FsAyTsTPL5Tu9cevQzk\u003D;
  }

  /// <summary>Used to transfer annotation draw data.</summary>
  public class AnnotationData : ChartDrawData.IDrawValue, IPersistable, IAnnotationData
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool? \u0023\u003DzDVmik2Cw62ImfJj4oQ\u003D\u003D = new bool?(true);
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool? \u0023\u003DzYXubScDfQrTXB6UBKw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IComparable \u0023\u003Dzqd3FQYBK_LA3hXY2yQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IComparable \u0023\u003DzOo4y0SHpsMppCYgBgQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IComparable \u0023\u003Dz\u0024ZnIEGwkZ5eX9ae8QA\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IComparable \u0023\u003DzHsqtIm4nTXsCXtUzPQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private System.Windows.Media.Brush \u0023\u003DzyzlJTQKpqY_x2irG8w\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private System.Windows.Media.Brush \u0023\u003DzzC_8Z\u0024y5dAaxpXf06Q\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private System.Windows.Media.Brush \u0023\u003DzxqYIFOtQe2yvE8IR9w\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private System.Windows.Thickness? \u0023\u003DzNuku2Q_MJPTAy5dSZA\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool? \u0023\u003Dz8fLAWoL79IMaST2\u0024wlIkc0I\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private LabelPlacement? \u0023\u003Dz9PsZz0tnRpDYHWiQjNcgqMQ\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private System.Windows.HorizontalAlignment? \u0023\u003DzEVNVu_ZK7\u00248NZaMf6lPJDrQ\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private System.Windows.VerticalAlignment? \u0023\u003DzD\u0024vImVfDGuVPRP1kiGxuWsE\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private AnnotationCoordinateMode? \u0023\u003DzFEFuGp8\u0024pUqAK41W_KORrlU\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string \u0023\u003Dz\u0024O4Ws2iqxrjv3AMtlw\u003D\u003D;

    /// <inheritdoc />
    public bool? IsVisible
    {
      get => this.\u0023\u003DzDVmik2Cw62ImfJj4oQ\u003D\u003D;
      set => this.\u0023\u003DzDVmik2Cw62ImfJj4oQ\u003D\u003D = value;
    }

    /// <inheritdoc />
    public bool? IsEditable
    {
      get => this.\u0023\u003DzYXubScDfQrTXB6UBKw\u003D\u003D;
      set => this.\u0023\u003DzYXubScDfQrTXB6UBKw\u003D\u003D = value;
    }

    /// <inheritdoc />
    public IComparable X1
    {
      get => this.\u0023\u003Dzqd3FQYBK_LA3hXY2yQ\u003D\u003D;
      set => this.\u0023\u003Dzqd3FQYBK_LA3hXY2yQ\u003D\u003D = value;
    }

    /// <inheritdoc />
    public IComparable Y1
    {
      get => this.\u0023\u003DzOo4y0SHpsMppCYgBgQ\u003D\u003D;
      set => this.\u0023\u003DzOo4y0SHpsMppCYgBgQ\u003D\u003D = value;
    }

    /// <inheritdoc />
    public IComparable X2
    {
      get => this.\u0023\u003Dz\u0024ZnIEGwkZ5eX9ae8QA\u003D\u003D;
      set => this.\u0023\u003Dz\u0024ZnIEGwkZ5eX9ae8QA\u003D\u003D = value;
    }

    /// <inheritdoc />
    public IComparable Y2
    {
      get => this.\u0023\u003DzHsqtIm4nTXsCXtUzPQ\u003D\u003D;
      set => this.\u0023\u003DzHsqtIm4nTXsCXtUzPQ\u003D\u003D = value;
    }

    /// <summary>Brush to draw lines and borders.</summary>
    public System.Windows.Media.Brush Stroke
    {
      get => this.\u0023\u003DzyzlJTQKpqY_x2irG8w\u003D\u003D;
      set => this.\u0023\u003DzyzlJTQKpqY_x2irG8w\u003D\u003D = value;
    }

    /// <summary>Brush to fill background.</summary>
    public System.Windows.Media.Brush Fill
    {
      get => this.\u0023\u003DzzC_8Z\u0024y5dAaxpXf06Q\u003D\u003D;
      set => this.\u0023\u003DzzC_8Z\u0024y5dAaxpXf06Q\u003D\u003D = value;
    }

    /// <summary>Brush to fill background.</summary>
    public System.Windows.Media.Brush Foreground
    {
      get => this.\u0023\u003DzxqYIFOtQe2yvE8IR9w\u003D\u003D;
      set => this.\u0023\u003DzxqYIFOtQe2yvE8IR9w\u003D\u003D = value;
    }

    /// <summary>Line thickness.</summary>
    public System.Windows.Thickness? Thickness
    {
      get => this.\u0023\u003DzNuku2Q_MJPTAy5dSZA\u003D\u003D;
      set => this.\u0023\u003DzNuku2Q_MJPTAy5dSZA\u003D\u003D = value;
    }

    /// <inheritdoc />
    public bool? ShowLabel
    {
      get => this.\u0023\u003Dz8fLAWoL79IMaST2\u0024wlIkc0I\u003D;
      set => this.\u0023\u003Dz8fLAWoL79IMaST2\u0024wlIkc0I\u003D = value;
    }

    /// <inheritdoc />
    public LabelPlacement? LabelPlacement
    {
      get => this.\u0023\u003Dz9PsZz0tnRpDYHWiQjNcgqMQ\u003D;
      set => this.\u0023\u003Dz9PsZz0tnRpDYHWiQjNcgqMQ\u003D = value;
    }

    /// <summary>Alignment for horizontal lines.</summary>
    public System.Windows.HorizontalAlignment? HorizontalAlignment
    {
      get => this.\u0023\u003DzEVNVu_ZK7\u00248NZaMf6lPJDrQ\u003D;
      set => this.\u0023\u003DzEVNVu_ZK7\u00248NZaMf6lPJDrQ\u003D = value;
    }

    /// <summary>Alignment for vertical lines.</summary>
    public System.Windows.VerticalAlignment? VerticalAlignment
    {
      get => this.\u0023\u003DzD\u0024vImVfDGuVPRP1kiGxuWsE\u003D;
      set => this.\u0023\u003DzD\u0024vImVfDGuVPRP1kiGxuWsE\u003D = value;
    }

    /// <inheritdoc />
    public AnnotationCoordinateMode? CoordinateMode
    {
      get => this.\u0023\u003DzFEFuGp8\u0024pUqAK41W_KORrlU\u003D;
      set => this.\u0023\u003DzFEFuGp8\u0024pUqAK41W_KORrlU\u003D = value;
    }

    /// <inheritdoc />
    public string Text
    {
      get => this.\u0023\u003Dz\u0024O4Ws2iqxrjv3AMtlw\u003D\u003D;
      set => this.\u0023\u003Dz\u0024O4Ws2iqxrjv3AMtlw\u003D\u003D = value;
    }

    Ecng.Drawing.Brush IAnnotationData.Stroke
    {
      get => this.Stroke.FromWpf();
      set => this.Stroke = value.ToWpf();
    }

    Ecng.Drawing.Brush IAnnotationData.Fill
    {
      get => this.Fill.FromWpf();
      set => this.Fill = value.ToWpf();
    }

    Ecng.Drawing.Brush IAnnotationData.Foreground
    {
      get => this.Foreground.FromWpf();
      set => this.Foreground = value.ToWpf();
    }

    Ecng.Drawing.Thickness? IAnnotationData.Thickness
    {
      get
      {
        System.Windows.Thickness? thickness = this.Thickness;
        ref System.Windows.Thickness? local = ref thickness;
        return !local.HasValue ? new Ecng.Drawing.Thickness?() : new Ecng.Drawing.Thickness?(local.GetValueOrDefault().FromWpf());
      }
      set
      {
        this.Thickness = value.HasValue ? new System.Windows.Thickness?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Thickness?();
      }
    }

    Ecng.Drawing.HorizontalAlignment? IAnnotationData.HorizontalAlignment
    {
      get
      {
        System.Windows.HorizontalAlignment? horizontalAlignment = this.HorizontalAlignment;
        ref System.Windows.HorizontalAlignment? local = ref horizontalAlignment;
        return !local.HasValue ? new Ecng.Drawing.HorizontalAlignment?() : new Ecng.Drawing.HorizontalAlignment?(local.GetValueOrDefault().FromWpf());
      }
      set
      {
        this.HorizontalAlignment = value.HasValue ? new System.Windows.HorizontalAlignment?(value.GetValueOrDefault().ToWpf()) : new System.Windows.HorizontalAlignment?();
      }
    }

    Ecng.Drawing.VerticalAlignment? IAnnotationData.VerticalAlignment
    {
      get
      {
        System.Windows.VerticalAlignment? verticalAlignment = this.VerticalAlignment;
        ref System.Windows.VerticalAlignment? local = ref verticalAlignment;
        return !local.HasValue ? new Ecng.Drawing.VerticalAlignment?() : new Ecng.Drawing.VerticalAlignment?(local.GetValueOrDefault().FromWpf());
      }
      set
      {
        this.VerticalAlignment = value.HasValue ? new System.Windows.VerticalAlignment?(value.GetValueOrDefault().ToWpf()) : new System.Windows.VerticalAlignment?();
      }
    }

    /// <summary>Load settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Load(SettingsStorage storage)
    {
      this.IsVisible = storage.GetValue<bool?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433813), this.IsVisible);
      this.IsEditable = storage.GetValue<bool?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434496), this.IsEditable);
      this.X1 = ChartDrawData.AnnotationData.\u0023\u003DqS8Rz7CLyLtZi1DPj0ZEyaLSgFDmJg4JMOyHZMbcvphk\u003D(storage.GetValue<IComparable>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434477), this.X1));
      this.Y1 = storage.GetValue<IComparable>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434262), this.Y1);
      this.X2 = ChartDrawData.AnnotationData.\u0023\u003DqS8Rz7CLyLtZi1DPj0ZEyaLSgFDmJg4JMOyHZMbcvphk\u003D(storage.GetValue<IComparable>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434243), this.X2));
      this.Y2 = storage.GetValue<IComparable>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434252), this.Y2);
      SettingsStorage settingsStorage1 = storage.GetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434289), (SettingsStorage) null);
      this.Stroke = settingsStorage1 != null ? settingsStorage1.\u0023\u003DzMlbK6H8\u003D() : (System.Windows.Media.Brush) null;
      SettingsStorage settingsStorage2 = storage.GetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434302), (SettingsStorage) null);
      this.Fill = settingsStorage2 != null ? settingsStorage2.\u0023\u003DzMlbK6H8\u003D() : (System.Windows.Media.Brush) null;
      SettingsStorage settingsStorage3 = storage.GetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434281), (SettingsStorage) null);
      this.Foreground = settingsStorage3 != null ? settingsStorage3.\u0023\u003DzMlbK6H8\u003D() : (System.Windows.Media.Brush) null;
      try
      {
        SettingsStorage settingsStorage4 = storage.GetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434330), (SettingsStorage) null);
        this.Thickness = settingsStorage4 != null ? new System.Windows.Thickness?(settingsStorage4.\u0023\u003DzNnTxBiiEvu0_2NsnpA\u003D\u003D()) : new System.Windows.Thickness?();
      }
      catch
      {
      }
      this.ShowLabel = storage.GetValue<bool?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434314), this.ShowLabel);
      this.LabelPlacement = storage.GetValue<LabelPlacement?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434362), this.LabelPlacement);
      this.HorizontalAlignment = storage.GetValue<System.Windows.HorizontalAlignment?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427475), this.HorizontalAlignment);
      this.VerticalAlignment = storage.GetValue<System.Windows.VerticalAlignment?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427465), this.VerticalAlignment);
      this.CoordinateMode = storage.GetValue<AnnotationCoordinateMode?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427489), this.CoordinateMode);
      this.Text = storage.GetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427542), this.Text);
    }

    /// <summary>Save settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Save(SettingsStorage storage)
    {
      storage.SetValue<bool?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433813), this.IsVisible);
      storage.SetValue<bool?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434496), this.IsEditable);
      storage.SetValue<IComparable>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434477), ChartDrawData.AnnotationData.\u0023\u003DqQ1G3fw2s0nP9veUenvj8sEj2aOo8jkJJ5IBJj0VGllQ\u003D(this.X1));
      storage.SetValue<IComparable>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434262), this.Y1);
      storage.SetValue<IComparable>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434243), ChartDrawData.AnnotationData.\u0023\u003DqQ1G3fw2s0nP9veUenvj8sEj2aOo8jkJJ5IBJj0VGllQ\u003D(this.X2));
      storage.SetValue<IComparable>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434252), this.Y2);
      SettingsStorage settingsStorage1 = storage;
      string str1 = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434289);
      System.Windows.Media.Brush stroke = this.Stroke;
      SettingsStorage settingsStorage2 = stroke != null ? stroke.\u0023\u003DzXzUiEzE\u003D() : (SettingsStorage) null;
      settingsStorage1.SetValue<SettingsStorage>(str1, settingsStorage2);
      SettingsStorage settingsStorage3 = storage;
      string str2 = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434302);
      System.Windows.Media.Brush fill = this.Fill;
      SettingsStorage settingsStorage4 = fill != null ? fill.\u0023\u003DzXzUiEzE\u003D() : (SettingsStorage) null;
      settingsStorage3.SetValue<SettingsStorage>(str2, settingsStorage4);
      SettingsStorage settingsStorage5 = storage;
      string str3 = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434281);
      System.Windows.Media.Brush foreground = this.Foreground;
      SettingsStorage settingsStorage6 = foreground != null ? foreground.\u0023\u003DzXzUiEzE\u003D() : (SettingsStorage) null;
      settingsStorage5.SetValue<SettingsStorage>(str3, settingsStorage6);
      SettingsStorage settingsStorage7 = storage;
      string str4 = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434330);
      System.Windows.Thickness? thickness = this.Thickness;
      ref System.Windows.Thickness? local = ref thickness;
      SettingsStorage settingsStorage8 = local.HasValue ? local.GetValueOrDefault().\u0023\u003DzXzUiEzE\u003D() : (SettingsStorage) null;
      settingsStorage7.SetValue<SettingsStorage>(str4, settingsStorage8);
      storage.SetValue<bool?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434314), this.ShowLabel);
      storage.SetValue<LabelPlacement?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434362), this.LabelPlacement);
      storage.SetValue<System.Windows.HorizontalAlignment?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427475), this.HorizontalAlignment);
      storage.SetValue<System.Windows.VerticalAlignment?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427465), this.VerticalAlignment);
      storage.SetValue<AnnotationCoordinateMode?>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427489), this.CoordinateMode);
      storage.SetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427542), this.Text);
    }

    internal static IComparable \u0023\u003DqS8Rz7CLyLtZi1DPj0ZEyaLSgFDmJg4JMOyHZMbcvphk\u003D(
      IComparable _param0)
    {
      return _param0 is DateTime dateTime ? (IComparable) TimeHelper.UtcKind(dateTime) : _param0;
    }

    internal static IComparable \u0023\u003DqQ1G3fw2s0nP9veUenvj8sEj2aOo8jkJJ5IBJj0VGllQ\u003D(
      IComparable _param0)
    {
      switch (_param0)
      {
        case DateTimeOffset dateTimeOffset:
          return (IComparable) dateTimeOffset.UtcDateTime;
        case DateTime dateTime:
          return (IComparable) dateTime.ToUniversalTime();
        default:
          return _param0;
      }
    }
  }

  /// <summary>Chart drawing data item.</summary>
  public sealed class ChartDrawDataItem : IChartDrawData.IChartDrawDataItem
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ChartDrawData \u0023\u003DzU\u0024_meog\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly DateTimeOffset \u0023\u003DzRDsdW3VoE5Y8g7uFWw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly double \u0023\u003Dzxb_4cLfI55054x5Z\u0024g\u003D\u003D;

    internal ChartDrawDataItem(ChartDrawData _param1, DateTimeOffset _param2)
    {
      this.\u0023\u003DzU\u0024_meog\u003D = _param1 ?? throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427536));
      this.\u0023\u003DzRDsdW3VoE5Y8g7uFWw\u003D\u003D = _param2;
      this.\u0023\u003Dzxb_4cLfI55054x5Z\u0024g\u003D\u003D = double.NaN;
    }

    internal ChartDrawDataItem(ChartDrawData _param1, double _param2)
    {
      this.\u0023\u003DzU\u0024_meog\u003D = _param1 ?? throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427579));
      this.\u0023\u003Dzxb_4cLfI55054x5Z\u0024g\u003D\u003D = _param2;
    }

    /// <summary>The time stamp of the new data generation.</summary>
    public DateTimeOffset TimeStamp => this.\u0023\u003DzRDsdW3VoE5Y8g7uFWw\u003D\u003D;

    /// <summary>
    /// Value of X coordinate for <see cref="T:StockSharp.Xaml.Charting.ChartLineElement" />.
    /// </summary>
    public double XValue => this.\u0023\u003Dzxb_4cLfI55054x5Z\u0024g\u003D\u003D;

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(IChartCandleElement element, System.Drawing.Color? color)
    {
      return this.Add(element, color.HasValue ? new System.Windows.Media.Color?(color.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?());
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(IChartCandleElement element, System.Windows.Media.Color? color)
    {
      return (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartCandleElement, ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003Dz\u0024lu1PoF9036WD8AKeQ\u003D\u003D(), element, new ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D(this.TimeStamp, color));
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(
      IChartCandleElement element,
      DataType dataType,
      SecurityId secId,
      Decimal openPrice,
      Decimal highPrice,
      Decimal lowPrice,
      Decimal closePrice,
      CandlePriceLevel[] priceLevels,
      CandleStates _)
    {
      return (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartCandleElement, ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzOrWGMasVFnPJ9EIggA\u003D\u003D(), element, new ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D(this.TimeStamp, dataType, openPrice, highPrice, lowPrice, closePrice, (IEnumerable<CandlePriceLevel>) priceLevels));
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(
      IChartIndicatorElement element,
      IIndicatorValue value)
    {
      return value == null ? (IChartDrawData.IChartDrawDataItem) this : (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartIndicatorElement, ChartDrawData.IndicatorData>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzMvN\u0024j0JXehj_W7wJCsA5vYc\u003D(), element, new ChartDrawData.IndicatorData(this.TimeStamp, value));
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(
      IChartOrderElement element,
      long orderId,
      string orderStringId,
      Sides side,
      Decimal price,
      Decimal volume,
      string errorMessage = null)
    {
      return (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartOrderElement, ChartDrawData.\u0023\u003DzU3TaXFs\u003D>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003Dz_sjyNtZSz2JXDm4nPw\u003D\u003D(), element, new ChartDrawData.\u0023\u003DzU3TaXFs\u003D(this.TimeStamp, orderId, orderStringId, side, price, volume, errorMessage, true));
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(
      IChartTradeElement element,
      long tradeId,
      string tradeStringId,
      Sides side,
      Decimal price,
      Decimal volume)
    {
      return (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartTradeElement, ChartDrawData.\u0023\u003DzU3TaXFs\u003D>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003Dz\u0024VcyBPrNElR\u0024tFZL2A\u003D\u003D(), element, new ChartDrawData.\u0023\u003DzU3TaXFs\u003D(this.TimeStamp, tradeId, tradeStringId, side, price, volume, (string) null, false));
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(
      IChartLineElement element,
      double value1,
      double value2 = double.NaN)
    {
      return !MathHelper.IsNaN(this.XValue) ? (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartLineElement, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzwTJ32ssHOG49(), element, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>.\u0023\u003DzpxJeWbQ\u003D<double>(this.XValue, value1, value2)) : (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartLineElement, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzIfFLwHfr6UGj(), element, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>.\u0023\u003DzpxJeWbQ\u003D<DateTimeOffset>(this.TimeStamp, value1, value2));
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(IChartBandElement element, Decimal value)
    {
      return this.Add(element, (double) value, 0.0);
    }

    /// <inheritdoc />
    public IChartDrawData.IChartDrawDataItem Add(
      IChartBandElement element,
      double value1,
      double value2)
    {
      return !MathHelper.IsNaN(this.XValue) ? (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartBandElement, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzL5dW_Xfrnb5k(), element, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<double>.\u0023\u003DzpxJeWbQ\u003D<double>(this.XValue, value1, value2)) : (IChartDrawData.IChartDrawDataItem) this.\u0023\u003DzxdbSAbQ\u003D<IChartBandElement, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>(this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzaJmO2TNyAvUf(), element, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>.\u0023\u003DzpxJeWbQ\u003D<DateTimeOffset>(this.TimeStamp, value1, value2));
    }

    private ChartDrawData.ChartDrawDataItem \u0023\u003DzxdbSAbQ\u003D<TElement, TValue>(
      Dictionary<TElement, List<TValue>> _param1,
      TElement _param2,
      TValue _param3)
    {
      CollectionHelper.SafeAdd<TElement, List<TValue>>((IDictionary<TElement, List<TValue>>) _param1, _param2).Add(_param3);
      return this;
    }
  }

  /// <summary>Interface which represents all chart draw data types.</summary>
  public interface IDrawValue
  {
  }

  /// <summary>Indicator values to draw on chart.</summary>
  public readonly struct IndicatorData : ChartDrawData.IDrawValue
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly DateTime \u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IIndicatorValue \u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D;

    /// <summary>Create instance.</summary>
    /// <param name="dto">Value timestamp.</param>
    /// <param name="val">Indicator value.</param>
    public IndicatorData(DateTimeOffset dto, IIndicatorValue val)
      : this(dto.UtcDateTime, val)
    {
    }

    internal IndicatorData(DateTime _param1, IIndicatorValue _param2)
    {
      this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D = _param1;
      this.\u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D = _param2;
    }

    /// <summary>Value timestamp.</summary>
    public DateTime Time => this.\u0023\u003DzOAeg4d9aAVB\u0024\u0024e1gKg\u003D\u003D;

    /// <summary>Indicator value.</summary>
    public IIndicatorValue Value => this.\u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D;
  }
}
