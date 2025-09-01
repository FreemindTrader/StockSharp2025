using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace StockSharp.Xaml.Charting;
#nullable disable
[DataContract]
public class BindableObject100 : INotifyPropertyChanged
{

    private int _count;

    private event PropertyChangedEventHandler NotifyPropertyChanged;

    protected void NotifyChanged( string _param1 )
    {
        RaisePropertyChangedEvent( new PropertyChangedEventArgs( _param1 ) );
    }

    protected void RaisePropertyChangedEvent<T>( Expression<Func<T>> lamdaExp )
    {
        RaisePropertyChangedEvent( GetExpressionBody<T>( lamdaExp ) );
    }

    /// <summary>
    /// Set property value and raise events.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    protected virtual bool SetField<T>( ref T field, T value, string propertyName )
    {
        if ( EqualityComparer<T>.Default.Equals( field, value ) )
            return false;

        field = value;

        NotifyChanged( propertyName );

        return true;
    }

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
        add
        {
            NotifyPropertyChanged += value;

            if ( Interlocked.Increment(ref _count) <= 100 )
                return;
            _count = 0;
            RaisePropertyChangedEvent(new PropertyChangedEventArgs("Nothing"));
        }

        remove
        {
            NotifyPropertyChanged -= value;
        }
    }


    protected void OnPropertyChanged( string propertyName, object objectOne, object objectTwo )
    {
        RaisePropertyChangedEvent( new PropertyChangedEventArgsEx( propertyName, objectOne, objectTwo ) );
    }



    protected void RaisePropertyChangedEvent( string property )
    {
        NotifyPropertyChanged?.Invoke( this, new PropertyChangedEventArgs( property ) );
    }

    private void RaisePropertyChangedEvent( PropertyChangedEventArgs e )
    {
        NotifyPropertyChanged?.Invoke( this, e );
    }

    public static string GetExpressionBody<T>( Expression<Func<T>> lambda )
    {
        var expression = (LambdaExpression)lambda;
        return ( !( expression.Body is UnaryExpression ) ? ( MemberExpression ) expression.Body : ( MemberExpression ) ( ( UnaryExpression ) expression.Body ).Operand ).Member.Name;
    }
}
