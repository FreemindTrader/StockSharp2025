// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.BindableObject
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading;

namespace StockSharp.Xaml.Charting
{
    [DataContract]
    public class BindableObject : INotifyPropertyChanged
    {
        private int _refCounter;

        private event PropertyChangedEventHandler _propertyChanged;

        protected void OnPropertyChanged( string propertyName )
        {
            this.RaisePropertyChanged( new PropertyChangedEventArgs( propertyName ) );
        }

        protected virtual bool SetField<T>( ref T field, T value, string name )
        {
            if ( EqualityComparer<T>.Default.Equals( field, value ) )
                return false;
            field = value;
            this.OnPropertyChanged( name );
            return true;
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                this._propertyChanged += value;
                if (Interlocked.Increment(ref this._refCounter) <= 100)
                    return;
                this._refCounter = 0;
                this.RaisePropertyChanged(new PropertyChangedEventArgs("Nothing"));
            }
            remove
            {
                this._propertyChanged -= value;
            }
        }

        protected void OnPropertyChanged( string propertyName, object oldValue, object newValue )
        {
            this.RaisePropertyChanged( ( PropertyChangedEventArgs ) new PropertyChangedEventArgsWithValues( propertyName, oldValue, newValue ) );
        }

        protected void RaisePropertyChanged<T>( Expression<Func<T>> selectorExpression )
        {
            this.RaisePropertyChanged( BindableObject.PropertyName<T>( selectorExpression ) );
        }

        protected void RaisePropertyChanged( string name )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this._propertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged( ( object ) this, new PropertyChangedEventArgs( name ) );
        }

        private void RaisePropertyChanged( PropertyChangedEventArgs args )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this._propertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged( ( object ) this, args );
        }

        public static string PropertyName<T>( Expression<Func<T>> property )
        {
            LambdaExpression lambdaExpression = (LambdaExpression) property;
            return ( !( lambdaExpression.Body is UnaryExpression ) ? ( MemberExpression ) lambdaExpression.Body : ( MemberExpression ) ( ( UnaryExpression ) lambdaExpression.Body ).Operand ).Member.Name;
        }
    }
}
