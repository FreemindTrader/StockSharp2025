using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml.Charting.Definitions
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
            if ( o == null )
            {
                return default( TResult );
            }

            return eval( o );
        }

        public static TResult Return<TInput, TResult>( this TInput o, Func<TInput, TResult> eval, TResult failureValue ) where TInput : class
        {
            if ( o == null )
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
            if ( o == null || !eval( o ) )
            {
                return default( TInput );
            }

            return o;
        }

        public static TInput Unless<TInput>( this TInput o, Func<TInput, bool> eval ) where TInput : class
        {
            if ( o == null || eval( o ) )
            {
                return default( TInput );
            }

            return o;
        }

        public static TInput Do<TInput>( this TInput o, Action<TInput> action ) where TInput : class
        {
            if ( o != null )
            {
                action( o );
            }

            return o;
        }
    }
}
