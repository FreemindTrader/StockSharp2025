// Decompiled with JetBrains decompiler
// Type: #=ztyAKlj3UbIrpcOb4hAbyLgMrkXYkuX1IGg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading;

#nullable disable
[DataContract]
internal class \u0023\u003DztyAKlj3UbIrpcOb4hAbyLgMrkXYkuX1IGg\u003D\u003D : INotifyPropertyChanged
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private PropertyChangedEventHandler \u0023\u003DziApqnpw\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int \u0023\u003DzBSG7sit66zZH;

  private void \u0023\u003Dz\u0024AL3lYMid4z_(PropertyChangedEventHandler _param1)
  {
    PropertyChangedEventHandler changedEventHandler = this.\u0023\u003DziApqnpw\u003D;
    PropertyChangedEventHandler comparand;
    do
    {
      comparand = changedEventHandler;
      changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.\u0023\u003DziApqnpw\u003D, comparand + _param1, comparand);
    }
    while (changedEventHandler != comparand);
  }

  private void \u0023\u003DzHVseJMBq5be1(PropertyChangedEventHandler _param1)
  {
    PropertyChangedEventHandler changedEventHandler = this.\u0023\u003DziApqnpw\u003D;
    PropertyChangedEventHandler comparand;
    do
    {
      comparand = changedEventHandler;
      changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.\u0023\u003DziApqnpw\u003D, comparand - _param1, comparand);
    }
    while (changedEventHandler != comparand);
  }

  protected void \u0023\u003Dz15moWio\u003D(string _param1)
  {
    this.\u0023\u003Dz0AXa0sp7IkH8(new PropertyChangedEventArgs(_param1));
  }

  protected virtual bool \u0023\u003DzwGPLgl8\u003D<T>(ref T _param1, T _param2, string _param3)
  {
    if (EqualityComparer<T>.Default.Equals(_param1, _param2))
      return false;
    _param1 = _param2;
    this.\u0023\u003Dz15moWio\u003D(_param3);
    return true;
  }

  void INotifyPropertyChanged.\u0023\u003DzEv9LVlkJvUjy\u0024TeZl6vNKqTVYPst(
    PropertyChangedEventHandler _param1)
  {
    this.\u0023\u003Dz\u0024AL3lYMid4z_(_param1);
    if (Interlocked.Increment(ref this.\u0023\u003DzBSG7sit66zZH) <= 100)
      return;
    this.\u0023\u003DzBSG7sit66zZH = 0;
    this.\u0023\u003Dz0AXa0sp7IkH8(new PropertyChangedEventArgs(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430966)));
  }

  void INotifyPropertyChanged.\u0023\u003DzrlR5kckLtgr8uURxQFpsiL65kjNN(
    PropertyChangedEventHandler _param1)
  {
    this.\u0023\u003DzHVseJMBq5be1(_param1);
  }

  protected void \u0023\u003Dz15moWio\u003D(string _param1, object _param2, object _param3)
  {
    this.\u0023\u003Dz0AXa0sp7IkH8((PropertyChangedEventArgs) new \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDCPI15WlokXZO9yDJcU\u003D(_param1, _param2, _param3));
  }

  protected void \u0023\u003Dz0AXa0sp7IkH8<T>(Expression<Func<T>> _param1)
  {
    this.\u0023\u003Dz0AXa0sp7IkH8(\u0023\u003DztyAKlj3UbIrpcOb4hAbyLgMrkXYkuX1IGg\u003D\u003D.\u0023\u003Dzp7QZFP4\u003D<T>(_param1));
  }

  protected void \u0023\u003Dz0AXa0sp7IkH8(string _param1)
  {
    PropertyChangedEventHandler ziApqnpw = this.\u0023\u003DziApqnpw\u003D;
    if (ziApqnpw == null)
      return;
    ziApqnpw((object) this, new PropertyChangedEventArgs(_param1));
  }

  private void \u0023\u003Dz0AXa0sp7IkH8(PropertyChangedEventArgs _param1)
  {
    PropertyChangedEventHandler ziApqnpw = this.\u0023\u003DziApqnpw\u003D;
    if (ziApqnpw == null)
      return;
    ziApqnpw((object) this, _param1);
  }

  public static string \u0023\u003Dzp7QZFP4\u003D<T>(Expression<Func<T>> _param0)
  {
    LambdaExpression lambdaExpression = (LambdaExpression) _param0;
    return (!(lambdaExpression.Body is UnaryExpression) ? (MemberExpression) lambdaExpression.Body : (MemberExpression) ((UnaryExpression) lambdaExpression.Body).Operand).Member.Name;
  }
}
