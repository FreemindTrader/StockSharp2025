// Decompiled with JetBrains decompiler
// Type: #=ztyAKlj3UbIrpcOb4hAbyLgMrkXYkuX1IGg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading;

#nullable disable
[DataContract]
public class BindableObjectEx : INotifyPropertyChanged
{

    private int _count;

    public event PropertyChangedEventHandler PropertyChanged;

    private void AddPropertyChangedEventHandler(PropertyChangedEventHandler _param1)
    {
        PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
        PropertyChangedEventHandler comparand;
        do
        {
            comparand = changedEventHandler;
            changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, comparand + _param1, comparand);
        }
        while ( changedEventHandler != comparand );
    }

    private void RemovePropertyChangedEventHandler(PropertyChangedEventHandler _param1)
    {
        PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
        PropertyChangedEventHandler comparand;
        do
        {
            comparand = changedEventHandler;
            changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, comparand - _param1, comparand);
        }
        while ( changedEventHandler != comparand );
    }

    protected void OnPropertyChanged(string _param1)
    {
        this.RaisePropertyChangedEvent(new PropertyChangedEventArgs(_param1));
    }

    protected void RaisePropertyChangedEvent<T>(Expression<Func<T>> _param1)
    {
        this.RaisePropertyChangedEvent(BindableObject.GetExpressionBody<T>(_param1));
    }

    protected virtual bool OnSetPropertyChanged<T>(ref T _param1, T _param2, string _param3)
    {
        if ( EqualityComparer<T>.Default.Equals(_param1, _param2) )
            return false;
        _param1 = _param2;
        this.OnPropertyChanged(_param3);
        return true;
    }

    void INotifyPropertyChanged.PropertyChanged(PropertyChangedEventHandler _param1)
    {
        this.AddPropertyChangedEventHandler(_param1);
        if ( Interlocked.Increment(ref this._count) <= 100 )
            return;
        this._count = 0;
        this.RaisePropertyChangedEvent(new PropertyChangedEventArgs("Nothing"));
    }

    void INotifyPropertyChanged.PropertyChanged(
      PropertyChangedEventHandler _param1)
      {
        this.RemovePropertyChangedEventHandler(_param1);
    }

    protected void OnPropertyChanged(string _param1, object _param2, object _param3)
    {
        this.RaisePropertyChangedEvent((PropertyChangedEventArgs)new PropertyChangedEventArgsEx(_param1, _param2, _param3));
    }



    protected void RaisePropertyChangedEvent(string _param1)
    {
        PropertyChangedEventHandler ziApqnpw = this.PropertyChanged;
        if ( ziApqnpw == null )
            return;
        ziApqnpw((object)this, new PropertyChangedEventArgs(_param1));
    }

    private void RaisePropertyChangedEvent(PropertyChangedEventArgs _param1)
    {
        PropertyChangedEventHandler ziApqnpw = this.PropertyChanged;
        if ( ziApqnpw == null )
            return;
        ziApqnpw((object)this, _param1);
    }

    public static string GetExpressionBody<T>(Expression<Func<T>> _param0)
    {
        LambdaExpression lambdaExpression = (LambdaExpression)_param0;
        return ( !( lambdaExpression.Body is UnaryExpression ) ? (MemberExpression)lambdaExpression.Body : (MemberExpression)( (UnaryExpression)lambdaExpression.Body ).Operand ).Member.Name;
    }
}
