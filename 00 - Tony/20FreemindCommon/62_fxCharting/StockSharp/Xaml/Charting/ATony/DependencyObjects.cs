using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace fx.Charting.Xaml
{
    public static class DoHelper
    {
        public static readonly DependencyProperty DialogResultProperty;
        private static readonly List<IDisposable> _myList = new List<IDisposable>();

        public static IDisposable AddPropertyListener( this DependencyObject sourceObject, DependencyProperty property, Action<DependencyPropertyChangedEventArgs> onChanged )
        {
            DependencyObjectEx doEx = new DependencyObjectEx( sourceObject, property, onChanged );
            DoHelper._myList.Add( ( IDisposable )doEx );
            return ( IDisposable )doEx;
        }
    }

    public sealed class DependencyObjectEx : DependencyObject, IDisposable
    {
        private static readonly DependencyProperty FreemindProperties = DependencyProperty.Register( "FreemindProperty", typeof( object ), typeof( DependencyObjectEx ), new PropertyMetadata( null, new PropertyChangedCallback( Invoke ) ) );
        private readonly Action<DependencyPropertyChangedEventArgs> myEventArgs;
        private bool _disposing;

        public DependencyObjectEx( DependencyObject d, DependencyProperty p, Action<DependencyPropertyChangedEventArgs> myEvent )
        {
            this.SetBindings( DependencyObjectEx.FreemindProperties, d, new PropertyPath( p ), BindingMode.OneWay, null, null );
            this.myEventArgs = myEvent;
        }


        void IDisposable.Dispose()
        {
            if ( this._disposing )
                return;
            this._disposing = true;
            BindingOperations.ClearBinding( this, DependencyObjectEx.FreemindProperties );
        }

        private static void Invoke( DependencyObject d, DependencyPropertyChangedEventArgs _param1 )
        {
            var me = ( DependencyObjectEx )d;

            if ( me._disposing )
                return;

            Action<DependencyPropertyChangedEventArgs> z8Ybx47Q = me.myEventArgs;
            if ( z8Ybx47Q == null )
                return;
            z8Ybx47Q( _param1 );
        }

    }
}
