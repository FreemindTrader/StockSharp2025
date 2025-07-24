// Decompiled with JetBrains decompiler
// Type: #=zYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j$MVq9JT52kmtoFstXIgXETlSaEaF89mw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
public sealed class ChartActiveOrdersElementVM( ChartActiveOrdersElement _param1 ) : ChartCompentWpfBaseViewModel<ChartActiveOrdersElement>( _param1 )
{
    private class AnnotationInfo : ViewModelBase
    {
        public AnnotationInfo( ActiveOrderAnnotation annotation )
        {
            this.Annotation = annotation ?? throw new ArgumentNullException( nameof( annotation ) );
            this.AutoRemoveFromChart = true;
        }

        public ActiveOrderAnnotation Annotation
        {
            get;
        }

        public Decimal Balance
        {
            get => this.GetValue<Decimal>( nameof( Balance ) );
            set => this.SetValue<Decimal>( value, nameof( Balance ) );
        }

        public OrderStates State
        {
            get => this.GetValue<OrderStates>( nameof( State ) );
            set => this.SetValue<OrderStates>( value, nameof( State ) );
        }

        public Decimal PriceStep
        {
            get => this.GetValue<Decimal>( nameof( PriceStep ) );
            set => this.SetValue<Decimal>( value, nameof( PriceStep ) );
        }

        public Decimal Price
        {
            get => this.GetValue<Decimal>( nameof( Price ) );
            set => this.SetValue<Decimal>( value, nameof( Price ) );
        }

        public bool AutoRemoveFromChart
        {
            get => this.GetValue<bool>( nameof( AutoRemoveFromChart ) );
            set => this.SetValue<bool>( value, nameof( AutoRemoveFromChart ) );
        }

        public bool IsFrozen
        {
            get => this.GetValue<bool>( nameof( IsFrozen ) );
            set => this.SetValue<bool>( value, nameof( IsFrozen ) );
        }
    }

    private readonly PairSet<Order, ChartActiveOrdersElementVM.AnnotationInfo> _orderAnnotationInfoPairSet = new PairSet<Order, ChartActiveOrdersElementVM.AnnotationInfo>();

  protected override void UpdateUi()
    {
        this.PerformUiAction( new Action( this.\u0023\u003DzotzePNFdGlR6yejSfw\u003D\u003D), true );
    }

    protected override void Clear()
    {
    }

    public IEnumerable<Order> GetActiveOrders( Func<Order, bool> _param1 )
    {
        if ( !DrawableChartElementBaseViewModel.IsUiThread() )
            throw new InvalidOperationException( "must be called from ui thread" );
        if ( _param1 == null )
            _param1 = ChartActiveOrdersElementVM.SomeClass34343383._public_static_Func_Order_bool_001 ?? ( ChartActiveOrdersElementVM.SomeClass34343383._public_static_Func_Order_bool_001 = new Func<Order, bool>( ChartActiveOrdersElementVM.SomeClass34343383.SomeMethond0343._public_bool_Method09_Order_param1 ) );
        return ( ( KeyedCollection<Order, ChartActiveOrdersElementVM.AnnotationInfo> ) this._orderAnnotationInfoPairSet).Keys.Where<Order>( _param1 );
    }

    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
    {
        if ( _param1 == null || ( ( IEnumerableEx ) _param1 ).Count == 0 )
            return false;
        foreach ( ChartDrawData.sActiveOrder k4Ek4jWvUoemvcOq in ( ( IEnumerable ) _param1 ).Cast<ChartDrawData.sActiveOrder>() )
            this.\u0023\u003Dz4ka8DEp6gsYz( k4Ek4jWvUoemvcOq );
        return true;
    }

    private void \u0023\u003Dzu72g2gQaaQQ2( Order _param1 )
    {
        if ( CollectionHelper.TryGetValue<Order, ChartActiveOrdersElementVM.AnnotationInfo>( ( IDictionary<Order, ChartActiveOrdersElementVM.AnnotationInfo> ) this._orderAnnotationInfoPairSet, _param1 ) == null )
            return;
        ( ( KeyedCollection<Order, ChartActiveOrdersElementVM.AnnotationInfo> ) this._orderAnnotationInfoPairSet).Remove( _param1 );
        this.ScichartSurfaceMVVM.RemoveAnnotation( this.RootElem, ( object ) _param1 );
    }

    private void \u0023\u003Dz4ka8DEp6gsYz(
      ChartDrawData.sActiveOrder _param1 )
    {
        ChartActiveOrdersElementVM.PrivateSealedClass0830 z9r5QdtX0xdsJ15Nf5Q = new ChartActiveOrdersElementVM.PrivateSealedClass0830();
        z9r5QdtX0xdsJ15Nf5Q._variableSome3535 = this;
        z9r5QdtX0xdsJ15Nf5Q._public_ChartDrawData_sActiveOrder = _param1;
        if ( !DrawableChartElementBaseViewModel.IsUiThread() )
        {
            this.\u0023\u003DzY_lPK_VP\u0024B7_( new Action( z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DzQwL2KivU7y6PEO6v7A\u003D\u003D), true );
        }
        else
        {
            z9r5QdtX0xdsJ15Nf5Q._public_Order_instance = z9r5QdtX0xdsJ15Nf5Q._public_ChartDrawData_sActiveOrder.\u0023\u003DzEbEKEpf9EiRR();
            ChartActiveOrdersElementVM.AnnotationInfo annotationInfo;
            if ( !( ( KeyedCollection<Order, ChartActiveOrdersElementVM.AnnotationInfo> ) this._orderAnnotationInfoPairSet).TryGetValue( z9r5QdtX0xdsJ15Nf5Q._public_Order_instance, ref annotationInfo ))
      {
                if ( !ChartActiveOrdersElementVM.\u0023\u003Dz6DZCOhRx49tQ_8lPpw\u003D\u003D(z9r5QdtX0xdsJ15Nf5Q._public_Order_instance, z9r5QdtX0xdsJ15Nf5Q._public_ChartDrawData_sActiveOrder.\u0023\u003DzzTd2XsqYavfdlfkXJw\u003D\u003D(), z9r5QdtX0xdsJ15Nf5Q._public_ChartDrawData_sActiveOrder.\u0023\u003Dzj7Cw0iE\u003D(), z9r5QdtX0xdsJ15Nf5Q._public_ChartDrawData_sActiveOrder.IsError))
          return;
                annotationInfo = this.CreateAnnotation( z9r5QdtX0xdsJ15Nf5Q._public_ChartDrawData_sActiveOrder );
                ( ( KeyedCollection<Order, ChartActiveOrdersElementVM.AnnotationInfo> ) this._orderAnnotationInfoPairSet).Add( z9r5QdtX0xdsJ15Nf5Q._public_Order_instance, annotationInfo );
                this.ScichartSurfaceMVVM.AddAxisMakerAnnotation( this.RootElem, ( IAnnotation ) annotationInfo.Annotation, ( object ) z9r5QdtX0xdsJ15Nf5Q._public_Order_instance );
                z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DqUoIStFNKe3Db8yit\u0024T30pXMQNkBLGDj5wZnB6AKllphaeL2REmryfSg0ry3XMnZ4RJdd0z20pme_G3GAImmhDQ\u003D\u003D(annotationInfo, true);
            }
      else if ( ChartActiveOrdersElementVM.\u0023\u003Dz6DZCOhRx49tQ_8lPpw\u003D\u003D(z9r5QdtX0xdsJ15Nf5Q._public_Order_instance, annotationInfo.AutoRemoveFromChart, z9r5QdtX0xdsJ15Nf5Q._public_ChartDrawData_sActiveOrder.\u0023\u003Dzj7Cw0iE\u003D(), z9r5QdtX0xdsJ15Nf5Q._public_ChartDrawData_sActiveOrder.IsError))
        z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DqUoIStFNKe3Db8yit\u0024T30pXMQNkBLGDj5wZnB6AKllphaeL2REmryfSg0ry3XMnZ4RJdd0z20pme_G3GAImmhDQ\u003D\u003D(annotationInfo, true);
      else
                z9r5QdtX0xdsJ15Nf5Q.\u0023\u003DqUoIStFNKe3Db8yit\u0024T30pXMQNkBLGDj5wZnB6AKllphaeL2REmryfSg0ry3XMnZ4RJdd0z20pme_G3GAImmhDQ\u003D\u003D(annotationInfo, false);
        }
    }

    private ChartActiveOrdersElementVM.AnnotationInfo CreateAnnotation(
      ChartDrawData.sActiveOrder _param1 )
    {
        Order order = _param1.\u0023\u003DzEbEKEpf9EiRR();
        ActiveOrderAnnotation annotation = new ActiveOrderAnnotation();
        annotation.CoordinateMode = AnnotationCoordinateMode.RelativeX;
        annotation.DragDirections = XyDirection.YDirection;
        annotation.ResizeDirections = XyDirection.XDirection;
        annotation.OrderErrorText = LocalizedStrings.Error.ToUpperInvariant();
        annotation.OrderText = order.Side == 1 ? "SELL LMT" : "BUY LMT";
        annotation.X1 = ( IComparable ) 0.8;
        ChartActiveOrdersElementVM.AnnotationInfo dataObject = new ChartActiveOrdersElementVM.AnnotationInfo(annotation);
        annotation.SetBindings( AnnotationBase.XAxisIdProperty, ( object ) this.ChartComponentView, "XAxisId" );
        annotation.SetBindings( AnnotationBase.YAxisIdProperty, ( object ) this.ChartComponentView, "YAxisId" );
        annotation.SetBindings( AnnotationBase.IsHiddenProperty, ( object ) this.ChartComponentView, "IsVisible", converter: ( IValueConverter ) new InverseBooleanConverter() );
        annotation.SetBindings( AnnotationBase.IsEditableProperty, ( object ) dataObject, "IsFrozen", BindingMode.OneWay, ( IValueConverter ) new InverseBooleanConverter() );
        ColorToBrushConverter converter = new ColorToBrushConverter();
        annotation.SetBindings( Control.ForegroundProperty, ( object ) this.ChartComponentView, "ForegroundColor", BindingMode.OneWay, ( IValueConverter ) converter );
        annotation.SetBindings( ActiveOrderAnnotation.StrokeProperty, ( object ) this.ChartComponentView, "ForegroundColor", BindingMode.OneWay, ( IValueConverter ) converter );
        annotation.SetBindings( ActiveOrderAnnotation.CancelButtonFillProperty, ( object ) this.ChartComponentView, "CancelButtonBackground", BindingMode.OneWay, ( IValueConverter ) converter );
        annotation.SetBindings( ActiveOrderAnnotation.CancelButtonColorProperty, ( object ) this.ChartComponentView, "CancelButtonColor", BindingMode.OneWay, ( IValueConverter ) converter );
        annotation.SetBindings( ActiveOrderAnnotation.IsAnimationEnabledProperty, ( object ) this.ChartComponentView, "IsAnimationEnabled", BindingMode.OneWay );
        annotation.SetBindings( ActiveOrderAnnotation.BlinkColorProperty, ( object ) this.ChartComponentView, order.Side == 1 ? "SellBlinkColor" : "BuyBlinkColor", BindingMode.OneWay );
        MultiBinding binding = new MultiBinding()
        {
            Converter = (IMultiValueConverter) new ChartActiveOrdersElementVM.PrivateIMultiValueConverterSealedClass(),
            Mode = BindingMode.OneWay
        };
        binding.Bindings.Add( ( BindingBase ) ChartActiveOrdersElementVM.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd( ( object ) dataObject, "State" ) );
        binding.Bindings.Add( ( BindingBase ) ChartActiveOrdersElementVM.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd( ( object ) order, "Side" ) );
        binding.Bindings.Add( ( BindingBase ) ChartActiveOrdersElementVM.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd( ( object ) this.ChartComponentView, "BuyPendingColor" ) );
        binding.Bindings.Add( ( BindingBase ) ChartActiveOrdersElementVM.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd( ( object ) this.ChartComponentView, "BuyColor" ) );
        binding.Bindings.Add( ( BindingBase ) ChartActiveOrdersElementVM.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd( ( object ) this.ChartComponentView, "SellPendingColor" ) );
        binding.Bindings.Add( ( BindingBase ) ChartActiveOrdersElementVM.\u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd( ( object ) this.ChartComponentView, "SellColor" ) );
        annotation.SetBinding( Control.BackgroundProperty, ( BindingBase ) binding );
        ChartActiveOrdersElementVM.\u0023\u003DzP\u0024rwMb8\u003D(dataObject, _param1);
        annotation.CancelClick += new Action<ActiveOrderAnnotation>( this.\u0023\u003Dzr1A_0cGwcH5c );
        annotation.DragEnded += new EventHandler<EventArgs>( this.\u0023\u003DzZe__axziAicn );
        annotation.AnimationDone += new Action<ActiveOrderAnnotation>( this.\u0023\u003DzVh15ej24l3Pl );
        return dataObject;
    }

    private static void \u0023\u003DzP\u0024rwMb8\u003D(
      ChartActiveOrdersElementVM.AnnotationInfo _param0,
      ChartDrawData.sActiveOrder _param1)
  {
    ActiveOrderAnnotation annotation = _param0.Annotation;
    Order order = _param1.\u0023\u003DzEbEKEpf9EiRR();
    _param0.Balance = _param1.\u0023\u003DzP9vQqYe3EED\u0024();
    _param0.State = _param1.\u0023\u003Dzj7Cw0iE\u003D();
    _param0.PriceStep = _param1.\u0023\u003DzTmtGqP_rl3YU6gjEDQ\u003D\u003D();
    _param0.IsFrozen = _param1.\u0023\u003DzOWugxxYxGyL4();
    _param0.AutoRemoveFromChart = _param1.\u0023\u003DzzTd2XsqYavfdlfkXJw\u003D\u003D();
    annotation.Y1 = (IComparable) (double) _param1.\u0023\u003DzbH5YDNBwpnry();
    annotation.OrderSizeText = $"{order.Volume - _param1.\u0023\u003DzP9vQqYe3EED\u0024()}/{order.Volume}";
    annotation.YDragStep = (double) _param1.\u0023\u003DzTmtGqP_rl3YU6gjEDQ\u003D\u003D();
}

private Order \u0023\u003Dz28gLj6T_roD8(
  ActiveOrderAnnotation _param1,
  out ChartActiveOrdersElementVM.AnnotationInfo _param2 )
  {
    KeyValuePair<Order, ChartActiveOrdersElementVM.AnnotationInfo> keyValuePair = ((IEnumerable<KeyValuePair<Order, ChartActiveOrdersElementVM.AnnotationInfo>>) this._orderAnnotationInfoPairSet).FirstOrDefault<KeyValuePair<Order, ChartActiveOrdersElementVM.AnnotationInfo>>( new Func<KeyValuePair<Order, ChartActiveOrdersElementVM.AnnotationInfo>, bool>( new ChartActiveOrdersElementVM.PrivateSealedClassDz4hWzOvDOp2Sz_a2WchXH2wc()
    {
        _public_ActiveOrderAnnotation_083 = _param1
    }.public_bool_Method_KeyValuePair_Order_ChartActiveOrdersElementVM_AnnotationInfo_ ) );
    _param2 = keyValuePair.Value;
    return keyValuePair.Key;
}

private void \u0023\u003DzZe__axziAicn(object _param1, EventArgs _param2)
  {
    ActiveOrderAnnotation activeOrderAnnotation = (ActiveOrderAnnotation) _param1;
    Order order = this.\u0023\u003Dz28gLj6T_roD8(activeOrderAnnotation, out ChartActiveOrdersElementVM.AnnotationInfo _);
    if (order == null)
      return;
    Decimal price = order.Price;
    double num1 = activeOrderAnnotation.Y1 as double? ?? (!(activeOrderAnnotation.Y1 is Decimal y1) ? 0.0 : (double) y1);
    double ydragStep = activeOrderAnnotation.YDragStep;
    if (ydragStep > 0.0)
      num1 = num1.NormalizePrice(ydragStep);
    Decimal num2 = num1 != 0.0 ? (Decimal) num1 : throw new InvalidOperationException("incorrect new order price");
    Decimal num3 = num2;
    if (price == num3)
      return;
    this.ScichartSurfaceMVVM.GroupChart?.InvokeMoveOrderEvent(order, num2);
  }

  private void \u0023\u003DzVh15ej24l3Pl(ActiveOrderAnnotation _param1)
  {
    ChartActiveOrdersElementVM.AnnotationInfo annotationInfo;
    Order order = this.\u0023\u003Dz28gLj6T_roD8(_param1, out annotationInfo);
    if (order == null || ChartActiveOrdersElementVM.\u0023\u003Dz6DZCOhRx49tQ_8lPpw\u003D\u003D(order, annotationInfo.AutoRemoveFromChart, annotationInfo.State, false))
      return;
    this.\u0023\u003Dz4ka8DEp6gsYz(new ChartDrawData.sActiveOrder(order, annotationInfo.Balance, annotationInfo.State, annotationInfo.PriceStep, annotationInfo.AutoRemoveFromChart, false, false, false, order.Price));
  }

  private void \u0023\u003Dzr1A_0cGwcH5c(ActiveOrderAnnotation _param1)
  {
    Order order = this.\u0023\u003Dz28gLj6T_roD8(_param1, out ChartActiveOrdersElementVM.AnnotationInfo _);
    if (order == null)
      return;
    this.ScichartSurfaceMVVM.GroupChart?.InvokeCancelOrderEvent(order);
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
    CollectionHelper.ForEach<Order>((IEnumerable<Order>) ((KeyedCollection<Order, ChartActiveOrdersElementVM.AnnotationInfo>) this._orderAnnotationInfoPairSet).Keys.ToArray<Order>(), new Action<Order>(this.\u0023\u003Dzu72g2gQaaQQ2));
  }

  public static Binding \u0023\u003DqnQBHgWwq46HtTk3awt3PNwG0MqJsTB80JyTm_gnLH305sglD_qwjbzhv17Y66uZd(
    object _param0,
    string _param1)
  {
    return new Binding(_param1)
    {
      Source = _param0,
      Mode = BindingMode.OneWay
    };
  }

  private sealed class PrivateIMultiValueConverterSealedClass : IMultiValueConverter
  {
    object IMultiValueConverter.\u0023\u003DzsTgIow6roRv0_9pt7wbO_aSDLkih(
      object[] _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      OrderStates? nullable1 = _param1.Length == 6 ? _param1[0] as OrderStates? : throw new InvalidOperationException($"Unexpected argument count: {_param1.Length}");
      Sides? nullable2 = _param1[1] as Sides?;
      Color? nullable3 = _param1[2] as Color?;
      Color? nullable4 = _param1[3] as Color?;
      Color? nullable5 = _param1[4] as Color?;
      Color? nullable6 = _param1[5] as Color?;
      if (!nullable1.HasValue || !nullable2.HasValue || !nullable3.HasValue || !nullable4.HasValue || !nullable5.HasValue || !nullable6.HasValue)
        throw new ArgumentNullException($"Incomplete params: ({nullable1},{nullable2},{nullable3},{nullable4},{nullable5},{nullable6})");
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

  private sealed class PrivateSealedClassDz4hWzOvDOp2Sz_a2WchXH2wc
  {
    public ActiveOrderAnnotation _public_ActiveOrderAnnotation_083;

    public bool public_bool_Method_KeyValuePair_Order_ChartActiveOrdersElementVM_AnnotationInfo_(
      KeyValuePair<Order, ChartActiveOrdersElementVM.AnnotationInfo> _param1)
    {
      return _param1.Value.Annotation == this._public_ActiveOrderAnnotation_083;
    }
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly ChartActiveOrdersElementVM.SomeClass34343383 SomeMethond0343 = new ChartActiveOrdersElementVM.SomeClass34343383();
    public static Func<Order, bool> _public_static_Func_Order_bool_001;

    public bool _public_bool_Method09_Order_param1(Order _param1) => true;
  }

  private sealed class PrivateSealedClass0830
  {
    public ChartActiveOrdersElementVM _variableSome3535;
    public ChartDrawData.sActiveOrder _public_ChartDrawData_sActiveOrder;
    public Order _public_Order_instance;

    public void \u0023\u003DzQwL2KivU7y6PEO6v7A\u003D\u003D()
    {
      this._variableSome3535.\u0023\u003Dz4ka8DEp6gsYz(this._public_ChartDrawData_sActiveOrder);
    }

    public void \u0023\u003DqUoIStFNKe3Db8yit\u0024T30pXMQNkBLGDj5wZnB6AKllphaeL2REmryfSg0ry3XMnZ4RJdd0z20pme_G3GAImmhDQ\u003D\u003D(
      ChartActiveOrdersElementVM.AnnotationInfo _param1,
      bool _param2)
    {
      ActiveOrderAnnotation annotation = _param1.Annotation;
      bool flag1 = true;
      bool flag2 = _param1.Balance != this._public_ChartDrawData_sActiveOrder.\u0023\u003DzP9vQqYe3EED\u0024();
      ChartActiveOrdersElementVM.\u0023\u003DzP\u0024rwMb8\u003D(_param1, this._public_ChartDrawData_sActiveOrder);
      if (annotation.IsAnimationEnabled)
      {
        if (this._public_ChartDrawData_sActiveOrder.IsError)
        {
          annotation.AnimateError();
          flag1 = false;
          if (!_param2)
            annotation.IsAnimationEnabled = false;
        }
        else if (((this._public_ChartDrawData_sActiveOrder.\u0023\u003Dzj7Cw0iE\u003D() != 2 ? 0 : (this._public_ChartDrawData_sActiveOrder.\u0023\u003DzP9vQqYe3EED\u0024() == 0M ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
        {
          annotation.AnimateOrderFill();
          flag1 = false;
          if (!_param2)
            annotation.IsAnimationEnabled = false;
        }
      }
      if (!(!_param2 & flag1))
        return;
      this._variableSome3535.\u0023\u003Dzu72g2gQaaQQ2(this._public_Order_instance);
    }
  }

  
}
