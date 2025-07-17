// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Maybe
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    public static class Maybe
    {
        public static TResult With2<TInput, TResult>( this TInput? o, Func<TInput, TResult> eval ) where TInput : struct where TResult : class
        {
            if ( !o.HasValue )
            {
                return default( TResult );
            }

            return eval( o.Value );
        }

        public static TResult With<TInput, TResult>( this TInput o, Func<TInput, TResult> eval ) where TInput : class where TResult : class
        {
            if ( ( object ) o == null )
            {
                return default( TResult );
            }

            return eval( o );
        }

        public static TResult Return<TInput, TResult>( this TInput o, Func<TInput, TResult> eval, TResult failureValue ) where TInput : class
        {
            if ( ( object ) o == null )
            {
                return failureValue;
            }

            return eval( o );
        }

        public static TResult Return2<TInput, TResult>( this TInput? o, Func<TInput, TResult> eval, TResult failureValue ) where TInput : struct
        {
            if ( !o.HasValue )
            {
                return failureValue;
            }

            return eval( o.Value );
        }

        public static TInput If<TInput>( this TInput o, Func<TInput, bool> eval ) where TInput : class
        {
            if ( ( object ) o == null || !eval( o ) )
            {
                return default( TInput );
            }

            return o;
        }

        public static TInput Unless<TInput>( this TInput o, Func<TInput, bool> eval ) where TInput : class
        {
            if ( ( object ) o == null || eval( o ) )
            {
                return default( TInput );
            }

            return o;
        }

        public static TInput Do<TInput>( this TInput o, Action<TInput> action ) where TInput : class
        {
            if ( ( object ) o != null )
            {
                action( o );
            }

            return o;
        }
    }
}
