using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
internal static class Class2
{
    internal static bool smethod_0< T >( T[] gparam_0 )
    {
        if( gparam_0 == null )
        {
            throw new ArgumentNullException( );
        }

        return gparam_0.Length != 0;
    }

    internal static T smethod_1< T >( T[] gparam_0 )
    {
        if( gparam_0 == null )
        {
            throw new ArgumentNullException( );
        }

        if( gparam_0.Length != 0 )
        {
            return gparam_0[ 0 ];
        }

        return default( T );
    }
}
