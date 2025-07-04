using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SciChart.Data.Model;

//
// Summary:
//     Provides a base-type for classes that need to raise System.ComponentModel.INotifyPropertyChanged
//     events
public class BindableObject : INotifyPropertyChanged
{
    

    public event PropertyChangedEventHandler PropertyChanged;

    private int \u0023\u003DzBSG7sit66zZH;

  

  protected void \u0023\u003Dz15moWio\u003D(string _param1)
  {
    this.RaisePropertyChangedEvent(new PropertyChangedEventArgs(_param1));
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
    this.RaisePropertyChangedEvent(new PropertyChangedEventArgs(""));
  }

  void INotifyPropertyChanged.\u0023\u003DzrlR5kckLtgr8uURxQFpsiL65kjNN(
    PropertyChangedEventHandler _param1)
  {
    this.\u0023\u003DzHVseJMBq5be1(_param1);
  }

  protected void \u0023\u003Dz15moWio\u003D(string _param1, object _param2, object _param3)
  {
    this.RaisePropertyChangedEvent((PropertyChangedEventArgs) new \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDCPI15WlokXZO9yDJcU\u003D(_param1, _param2, _param3));
  }

  protected void RaisePropertyChangedEvent<T>(Expression<Func<T>> _param1)
  {
    this.RaisePropertyChangedEvent(BindableObject .\u0023\u003Dzp7QZFP4\u003D<T>(_param1));
  }

  protected void RaisePropertyChangedEvent(string _param1)
  {
    PropertyChangedEventHandler ziApqnpw = this.PropertyChangedEvent;
    if (ziApqnpw == null)
      return;
    ziApqnpw((object) this, new PropertyChangedEventArgs(_param1));
  }

  private void RaisePropertyChangedEvent(PropertyChangedEventArgs _param1)
  {
    PropertyChangedEventHandler ziApqnpw = this.PropertyChangedEvent;
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
