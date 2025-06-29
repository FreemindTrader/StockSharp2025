// Decompiled with JetBrains decompiler
// Type: #=zYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j$MVq9JT52kmtoFstXIgXETlSaEaF89mw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using DevExpress.Mvvm;
using Ecng.Collections;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D(
  ChartActiveOrdersElement _param1) : 
  \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartActiveOrdersElement>(_param1)
{
  
  private readonly PairSet<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo> \u0023\u003DzS1WBvIHjLsHj = new PairSet<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>();

  protected override void \u0023\u003DzowR7R4A\u003D()
  {
    this.\u0023\u003Dz4EoFHUaZg4JL(new Action(this.\u0023\u003DzotzePNFdGlR6yejSfw\u003D\u003D), true);
  }

  protected override void \u0023\u003DzXfak0jM\u003D()
  {
  }

  public IEnumerable<Order> \u0023\u003DzQ\u0024gUWeEbsN2c(Func<Order, bool> _param1)
  {
    if (!UIBaseVM.\u0023\u003Dz03PnGbpCXkrj())
      throw new InvalidOperationException("");
    if (_param1 == null)
      _param1 = \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D ?? (\u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D = new Func<Order, bool>(\u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz5Z\u0024LBhMmNfY9qrmIEVzcHfw\u003D));
    return ((KeyedCollection<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>) this.\u0023\u003DzS1WBvIHjLsHj).Keys.Where<Order>(_param1);
  }

  public override bool \u0023\u003DzjgUUUJE\u003D(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    if (_param1 == null || ((IEnumerableEx) _param1).Count == 0)
      return false;
    foreach (ChartDrawData.sActiveOrder k4Ek4jWvUoemvcOq in ((IEnumerable) _param1).Cast<ChartDrawData.sActiveOrder>())
      this.\u0023\u003Dz4ka8DEp6gsYz(k4Ek4jWvUoemvcOq);
    return true;
  }

  private void \u0023\u003Dzu72g2gQaaQQ2(Order _param1)
  {
    if (CollectionHelper.TryGetValue<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>((IDictionary<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>) this.\u0023\u003DzS1WBvIHjLsHj, _param1) == null)
      return;
    ((KeyedCollection<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>) this.\u0023\u003DzS1WBvIHjLsHj).Remove(_param1);
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzwxpBjD0\u003D(this.RootElem, (object) _param1);
  }

  private void \u0023\u003Dz4ka8DEp6gsYz(
    ChartDrawData.sActiveOrder _param1)
  {
    \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz9r5QdtX0xdsJ15Nf5Q\u003D\u003D z9r5QdtX0xdsJ15Nf5Q = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz9r5QdtX0xdsJ15Nf5Q\u003D\u003D();
    z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzRRvwDu67s9Rm = this;
    z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzrnGNvGY\u003D = _param1;
    if (!UIBaseVM.\u0023\u003Dz03PnGbpCXkrj())
    {
      this.\u0023\u003DzY_lPK_VP\u0024B7_(new Action(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzQwL2KivU7y6PEO6v7A\u003D\u003D), true);
    }
    else
    {
      z9r5QdtX0xdsJ15Nf5Q.\u0023\u003Dz54\u0024be\u0024c\u003D = z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzrnGNvGY\u003D.Order();
      \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo annotationInfo;
      if (!((KeyedCollection<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>) this.\u0023\u003DzS1WBvIHjLsHj).TryGetValue(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003Dz54\u0024be\u0024c\u003D, ref annotationInfo))
      {
        if (!\u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz6DZCOhRx49tQ_8lPpw\u003D\u003D(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003Dz54\u0024be\u0024c\u003D, z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzrnGNvGY\u003D.AutoRemoveFromChart(), z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzrnGNvGY\u003D.OrderStates(), z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzrnGNvGY\u003D.IsError))
          return;
        annotationInfo = this.\u0023\u003DzWj46Xvc\u003D(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzrnGNvGY\u003D);
        ((KeyedCollection<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>) this.\u0023\u003DzS1WBvIHjLsHj).Add(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003Dz54\u0024be\u0024c\u003D, annotationInfo);
        this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzIeZhoes\u003D(this.RootElem, (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) annotationInfo.Annotation, (object) z9r5QdtX0xdsJ15Nf5Q.\u0023\u003Dz54\u0024be\u0024c\u003D);
        z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DqUoIStFNKe3Db8yit\u0024T30pXMQNkBLGDj5wZnB6AKllphaeL2REmryfSg0ry3XMnZ4RJdd0z20pme_G3GAImmhDQ\u003D\u003D(annotationInfo, true);
      }
      else if (\u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz6DZCOhRx49tQ_8lPpw\u003D\u003D(z9r5QdtX0xdsJ15Nf5Q.\u0023\u003Dz54\u0024be\u0024c\u003D, annotationInfo.AutoRemoveFromChart, z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzrnGNvGY\u003D.OrderStates(), z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzrnGNvGY\u003D.IsError))
        z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DqUoIStFNKe3Db8yit\u0024T30pXMQNkBLGDj5wZnB6AKllphaeL2REmryfSg0ry3XMnZ4RJdd0z20pme_G3GAImmhDQ\u003D\u003D(annotationInfo, true);
      else
        z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DqUoIStFNKe3Db8yit\u0024T30pXMQNkBLGDj5wZnB6AKllphaeL2REmryfSg0ry3XMnZ4RJdd0z20pme_G3GAImmhDQ\u003D\u003D(annotationInfo, false);
    }
  }

  private \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo \u0023\u003DzWj46Xvc\u003D(
    ChartDrawData.sActiveOrder _param1)
  {
    Order order = _param1.Order();
    ActiveOrderAnnotation annotation = new ActiveOrderAnnotation();
    annotation.CoordinateMode = AnnotationCoordinateMode.RelativeX;
    annotation.DragDirections = dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection;
    annotation.ResizeDirections = dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection;
    annotation.OrderErrorText = LocalizedStrings.Error.ToUpperInvariant();
    annotation.OrderText = order.Side == 1 ? "" : "";
    annotation.X1 = (IComparable) 0.8;
    \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo dataObject = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo(annotation);
    annotation.SetBindings(AnnotationBase.XAxisIdProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
    annotation.SetBindings(AnnotationBase.YAxisIdProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
    annotation.SetBindings(AnnotationBase.IsHiddenProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", converter: (IValueConverter) new InverseBooleanConverter());
    annotation.SetBindings(AnnotationBase.IsEditableProperty, (object) dataObject, "", BindingMode.OneWay, (IValueConverter) new InverseBooleanConverter());
    ColorToBrushConverter converter = new ColorToBrushConverter();
    annotation.SetBindings(Control.ForegroundProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay, (IValueConverter) converter);
    annotation.SetBindings(ActiveOrderAnnotation.StrokeProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay, (IValueConverter) converter);
    annotation.SetBindings(ActiveOrderAnnotation.CancelButtonFillProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay, (IValueConverter) converter);
    annotation.SetBindings(ActiveOrderAnnotation.CancelButtonColorProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay, (IValueConverter) converter);
    annotation.SetBindings(ActiveOrderAnnotation.IsAnimationEnabledProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", BindingMode.OneWay);
    annotation.SetBindings(ActiveOrderAnnotation.BlinkColorProperty, (object) this.\u0023\u003DzeaszzAAoBOY9(), order.Side == 1 ? "" : "", BindingMode.OneWay);
    MultiBinding binding = new MultiBinding()
    {
      Converter = (IMultiValueConverter) new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz2m1NF_ZMmb7l(),
      Mode = BindingMode.OneWay
    };
    binding.Bindings.Add((BindingBase) \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd((object) dataObject, ""));
    binding.Bindings.Add((BindingBase) \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd((object) order, ""));
    binding.Bindings.Add((BindingBase) \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd((object) this.\u0023\u003DzeaszzAAoBOY9(), ""));
    binding.Bindings.Add((BindingBase) \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd((object) this.\u0023\u003DzeaszzAAoBOY9(), ""));
    binding.Bindings.Add((BindingBase) \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd((object) this.\u0023\u003DzeaszzAAoBOY9(), ""));
    binding.Bindings.Add((BindingBase) \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd((object) this.\u0023\u003DzeaszzAAoBOY9(), ""));
    annotation.SetBinding(Control.BackgroundProperty, (BindingBase) binding);
    \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003DzP\u0024rwMb8\u003D(dataObject, _param1);
    annotation.CancelClick += new Action<ActiveOrderAnnotation>(this.\u0023\u003Dzr1A_0cGwcH5c);
    annotation.DragEnded += new EventHandler<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8EceYoYimX3HGDNfbrY\u003D>(this.\u0023\u003DzZe__axziAicn);
    annotation.AnimationDone += new Action<ActiveOrderAnnotation>(this.\u0023\u003DzVh15ej24l3Pl);
    return dataObject;
  }

  private static void \u0023\u003DzP\u0024rwMb8\u003D(
    \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo _param0,
    ChartDrawData.sActiveOrder _param1)
  {
    ActiveOrderAnnotation annotation = _param0.Annotation;
    Order order = _param1.Order();
    _param0.Balance = _param1.Balance();
    _param0.State = _param1.OrderStates();
    _param0.PriceStep = _param1.PriceStep();
    _param0.IsFrozen = _param1.IsFrozen();
    _param0.AutoRemoveFromChart = _param1.AutoRemoveFromChart();
    annotation.Y1 = (IComparable) (double) _param1.Price();
    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
    interpolatedStringHandler.AppendFormatted<Decimal>(order.Volume - _param1.Balance());
    interpolatedStringHandler.AppendLiteral("");
    interpolatedStringHandler.AppendFormatted<Decimal>(order.Volume);
    annotation.OrderSizeText = interpolatedStringHandler.ToStringAndClear();
    annotation.YDragStep = (double) _param1.PriceStep();
  }

  private Order \u0023\u003Dz28gLj6T_roD8(
    ActiveOrderAnnotation _param1,
    out \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo _param2)
  {
    KeyValuePair<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo> keyValuePair = ((IEnumerable<KeyValuePair<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>>) this.\u0023\u003DzS1WBvIHjLsHj).FirstOrDefault<KeyValuePair<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>>(new Func<KeyValuePair<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>, bool>(new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D()
    {
      \u0023\u003Dz2vouRgM\u003D = _param1
    }.\u0023\u003Dzus90X13c0dQNK8NugoaejT0\u003D));
    _param2 = keyValuePair.Value;
    return keyValuePair.Key;
  }

  private void \u0023\u003DzZe__axziAicn(object _param1, EventArgs _param2)
  {
    ActiveOrderAnnotation activeOrderAnnotation = (ActiveOrderAnnotation) _param1;
    Order order = this.\u0023\u003Dz28gLj6T_roD8(activeOrderAnnotation, out \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo _);
    if (order == null)
      return;
    Decimal price = order.Price;
    double num1 = activeOrderAnnotation.Y1 as double? ?? (!(activeOrderAnnotation.Y1 is Decimal y1) ? 0.0 : (double) y1);
    double ydragStep = activeOrderAnnotation.YDragStep;
    if (ydragStep > 0.0)
      num1 = num1.\u0023\u003Dzezm_VedE86O1(ydragStep);
    Decimal num2 = num1 != 0.0 ? (Decimal) num1 : throw new InvalidOperationException("");
    Decimal num3 = num2;
    if (price == num3)
      return;
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().GroupChart?.\u0023\u003DzoSyIfjNKL9Ta(order, num2);
  }

  private void \u0023\u003DzVh15ej24l3Pl(ActiveOrderAnnotation _param1)
  {
    \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo annotationInfo;
    Order order = this.\u0023\u003Dz28gLj6T_roD8(_param1, out annotationInfo);
    if (order == null || \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz6DZCOhRx49tQ_8lPpw\u003D\u003D(order, annotationInfo.AutoRemoveFromChart, annotationInfo.State, false))
      return;
    this.\u0023\u003Dz4ka8DEp6gsYz(new ChartDrawData.sActiveOrder(order, annotationInfo.Balance, annotationInfo.State, annotationInfo.PriceStep, annotationInfo.AutoRemoveFromChart, false, false, false, order.Price));
  }

  private void \u0023\u003Dzr1A_0cGwcH5c(ActiveOrderAnnotation _param1)
  {
    Order order = this.\u0023\u003Dz28gLj6T_roD8(_param1, out \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo _);
    if (order == null)
      return;
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().GroupChart?.\u0023\u003DzrMNjBJFuBLP3(order);
  }

  private static bool \u0023\u003Dz6DZCOhRx49tQ_8lPpw\u003D\u003D(
    Order _param0,
    bool _param1,
    OrderStates _param2,
    bool _param3)
  {
    if (_param3)
      return true;
    if (_param0.Price <= 0M)
      return false;
    return !_param1 || !Extensions.IsFinal(_param2);
  }

  private void \u0023\u003DzotzePNFdGlR6yejSfw\u003D\u003D()
  {
    CollectionHelper.ForEach<Order>((IEnumerable<Order>) ((KeyedCollection<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo>) this.\u0023\u003DzS1WBvIHjLsHj).Keys.ToArray<Order>(), new Action<Order>(this.\u0023\u003Dzu72g2gQaaQQ2));
  }

  internal static Binding \u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd(
    object _param0,
    string _param1)
  {
    return new Binding(_param1)
    {
      Source = _param0,
      Mode = BindingMode.OneWay
    };
  }

  private sealed class \u0023\u003Dz2m1NF_ZMmb7l : IMultiValueConverter
  {
    object IMultiValueConverter.\u0023\u003DzsTgIow6roRv0_9pt7wbO_aSDLkih(
      object[] _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      if (_param1.Length != 6)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
        interpolatedStringHandler.AppendLiteral("");
        interpolatedStringHandler.AppendFormatted<int>(_param1.Length);
        throw new InvalidOperationException(interpolatedStringHandler.ToStringAndClear());
      }
      OrderStates? nullable1 = _param1[0] as OrderStates?;
      Sides? nullable2 = _param1[1] as Sides?;
      Color? nullable3 = _param1[2] as Color?;
      Color? nullable4 = _param1[3] as Color?;
      Color? nullable5 = _param1[4] as Color?;
      Color? nullable6 = _param1[5] as Color?;
      if (!nullable1.HasValue || !nullable2.HasValue || !nullable3.HasValue || !nullable4.HasValue || !nullable5.HasValue || !nullable6.HasValue)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 6);
        interpolatedStringHandler.AppendLiteral("");
        interpolatedStringHandler.AppendFormatted<OrderStates?>(nullable1);
        interpolatedStringHandler.AppendLiteral("");
        interpolatedStringHandler.AppendFormatted<Sides?>(nullable2);
        interpolatedStringHandler.AppendLiteral("");
        interpolatedStringHandler.AppendFormatted<Color?>(nullable3);
        interpolatedStringHandler.AppendLiteral("");
        interpolatedStringHandler.AppendFormatted<Color?>(nullable4);
        interpolatedStringHandler.AppendLiteral("");
        interpolatedStringHandler.AppendFormatted<Color?>(nullable5);
        interpolatedStringHandler.AppendLiteral("");
        interpolatedStringHandler.AppendFormatted<Color?>(nullable6);
        interpolatedStringHandler.AppendLiteral("");
        throw new ArgumentNullException(interpolatedStringHandler.ToStringAndClear());
      }
      OrderStates orderStates = nullable1.Value;
      return orderStates == null || orderStates == 4 ? (object) new SolidColorBrush(nullable2.Value == null ? nullable3.Value : nullable5.Value) : (object) new SolidColorBrush(nullable2.Value == null ? nullable4.Value : nullable6.Value);
    }

    object[] IMultiValueConverter.\u0023\u003DzkoCt60WyxQ8Vp2m8J8PjY8sNdWh7(
      object _param1,
      Type[] _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }
  }

  private sealed class \u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D
  {
    public ActiveOrderAnnotation \u0023\u003Dz2vouRgM\u003D;

    internal bool \u0023\u003Dzus90X13c0dQNK8NugoaejT0\u003D(
      KeyValuePair<Order, \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo> _param1)
    {
      return _param1.Value.Annotation == this.\u0023\u003Dz2vouRgM\u003D;
    }
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<Order, bool> \u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D;

    internal bool \u0023\u003Dz5Z\u0024LBhMmNfY9qrmIEVzcHfw\u003D(Order _param1) => true;
  }

  private sealed class \u0023\u003Dz9r5QdtX0xdsJ15Nf5Q\u003D\u003D
  {
    public \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D \u0023\u003DzRRvwDu67s9Rm;
    public ChartDrawData.sActiveOrder \u0023\u003DzrnGNvGY\u003D;
    public Order \u0023\u003Dz54\u0024be\u0024c\u003D;

    internal void \u0023\u003DzQwL2KivU7y6PEO6v7A\u003D\u003D()
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dz4ka8DEp6gsYz(this.\u0023\u003DzrnGNvGY\u003D);
    }

    internal void \u0023\u003DqUoIStFNKe3Db8yit\u0024T30pXMQNkBLGDj5wZnB6AKllphaeL2REmryfSg0ry3XMnZ4RJdd0z20pme_G3GAImmhDQ\u003D\u003D(
      \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.AnnotationInfo _param1,
      bool _param2)
    {
      ActiveOrderAnnotation annotation = _param1.Annotation;
      bool flag1 = true;
      bool flag2 = _param1.Balance != this.\u0023\u003DzrnGNvGY\u003D.Balance();
      \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D.\u0023\u003DzP\u0024rwMb8\u003D(_param1, this.\u0023\u003DzrnGNvGY\u003D);
      if (annotation.IsAnimationEnabled)
      {
        if (this.\u0023\u003DzrnGNvGY\u003D.IsError)
        {
          annotation.AnimateError();
          flag1 = false;
          if (!_param2)
            annotation.IsAnimationEnabled = false;
        }
        else if (((this.\u0023\u003DzrnGNvGY\u003D.OrderStates() != 2 ? 0 : (this.\u0023\u003DzrnGNvGY\u003D.Balance() == 0M ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
        {
          annotation.AnimateOrderFill();
          flag1 = false;
          if (!_param2)
            annotation.IsAnimationEnabled = false;
        }
      }
      if (!(!_param2 & flag1))
        return;
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dzu72g2gQaaQQ2(this.\u0023\u003Dz54\u0024be\u0024c\u003D);
    }
  }

  private class AnnotationInfo : ViewModelBase
  {
    public AnnotationInfo(ActiveOrderAnnotation annotation)
    {
      this.Annotation = annotation ?? throw new ArgumentNullException("");
      this.AutoRemoveFromChart = true;
    }

    public ActiveOrderAnnotation Annotation { get; }

    public Decimal Balance
    {
      get
      {
        return this.GetValue<Decimal>("");
      }
      set
      {
        this.SetValue<Decimal>(value, "");
      }
    }

    public OrderStates State
    {
      get
      {
        return this.GetValue<OrderStates>("");
      }
      set
      {
        this.SetValue<OrderStates>(value, "");
      }
    }

    public Decimal PriceStep
    {
      get
      {
        return this.GetValue<Decimal>("");
      }
      set
      {
        this.SetValue<Decimal>(value, "");
      }
    }

    public Decimal Price
    {
      get
      {
        return this.GetValue<Decimal>("");
      }
      set
      {
        this.SetValue<Decimal>(value, "");
      }
    }

    public bool AutoRemoveFromChart
    {
      get
      {
        return this.GetValue<bool>("");
      }
      set
      {
        this.SetValue<bool>(value, "");
      }
    }

    public bool IsFrozen
    {
      get
      {
        return this.GetValue<bool>("");
      }
      set
      {
        this.SetValue<bool>(value, "");
      }
    }
  }
}
