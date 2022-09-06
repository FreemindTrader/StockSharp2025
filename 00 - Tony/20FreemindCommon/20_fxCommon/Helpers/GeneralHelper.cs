using fx.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fx.Common
{
    public static class GeneralHelper
    {
        public delegate void DefaultDelegate( );

        public delegate object DefaultReturnDelegate( );

        public delegate void GenericDelegate<TParameterOne>( TParameterOne parameter1 );

        public delegate void GenericDelegate<TParameterOne, TParameterTwo>( TParameterOne parameter1, TParameterTwo parameter2 );

        public delegate void GenericDelegate<TParameterOne, TParameterTwo, TParameterThree>( TParameterOne parameter1, TParameterTwo parameter2, TParameterThree parameter3 );

        public delegate TReturnType GenericReturnDelegate<TReturnType>( );

        public delegate TReturnType GenericReturnDelegate<TReturnType, TParameterOne>( TParameterOne parameter1 );

        public delegate TReturnType GenericReturnDelegate<TReturnType, TParameterOne, TParameterTwo>( TParameterOne parameter1, TParameterTwo parameter2 );

        private static volatile CancellationTokenSource _globalExitSource = new CancellationTokenSource( );

        public static double[ ] IntsToDoubles( int[ ] values )
        {
            double[ ] result = new double[ values.Length ];
            for ( int i = 0; i < result.Length; i++ )
            {
                result[ i ] = values[ i ];
            }
            return result;
        }

        public static string GetExceptionMessage( Exception exception )
        {
            string message = "Exception [" + exception.GetType( ).Name;

            if ( string.IsNullOrEmpty( exception.Message ) == false )
            {
                message += ", " + exception.Message;
            }
            message += "]";

            if ( exception.InnerException != null )
            {
                message += ", Inner [" + exception.InnerException.GetType( ).Name;

                if ( string.IsNullOrEmpty( exception.InnerException.Message ) == false )
                {
                    message += ", " + exception.InnerException.Message;
                }
            }

            message += ", " + exception.StackTrace;

            message += "]";

            return message;
        }

        public static void CancelAllTasks()
        {
            _globalExitSource.Cancel( );
        }

        public static void CreateNewGlobalExitSource()
        {
            _globalExitSource = new CancellationTokenSource();
        }

        public static CancellationToken GlobalExitToken( )
        {
            return _globalExitSource.Token;
        }

        public static TDataType[ ] EnumerableToArray<TDataType>( IEnumerable<TDataType> enumerable )
        {
            PooledList<TDataType> list = new PooledList<TDataType>( );
            foreach ( TDataType value in enumerable )
            {
                list.Add( value );
            }
            return list.ToArray( );
        }

        public static PooledList<TDataType> EnumerableToList<TDataType>( IEnumerable<TDataType> enumerable )
        {
            PooledList<TDataType> list = new PooledList<TDataType>( );
            foreach ( TDataType value in enumerable )
            {
                list.Add( value );
            }
            return list;
        }

        public static string SeparateCapitalLetters( string input )
        {
            StringBuilder result = new StringBuilder( );

            char previousChar = ' ';

            foreach ( char c in input )
            {
                if ( c.ToString( ) == c.ToString( ).ToUpper( ) &&
                     previousChar != ' ' )
                {
                    result.Append( " " );
                }

                result.Append( c );
                previousChar = c;
            }

            return result.ToString( );
        }
    }
}
