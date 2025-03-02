using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

internal static class Class3
{
    private static Class3.Class4 class4_0 = new Class3.Class4( );

    internal static long smethod_0( )
    {
        if( ( object ) Assembly.GetCallingAssembly( ) != ( object ) typeof( Class3 ).Assembly || !Class3.smethod_1( ) )
        {
            return 0;
        }

        lock( Class3.class4_0 )
        {
            long long_0 = Class3.class4_0.method_0( );
            if( long_0 == 0L )
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly( );
                List< byte > byteList = new List< byte >( );
                AssemblyName assemblyName;
                try
                {
                    assemblyName = executingAssembly.GetName( );
                }
                catch
                {
                    assemblyName = new AssemblyName( executingAssembly.FullName );
                }
                byte[] numArray = assemblyName.GetPublicKeyToken( );
                if( numArray != null && numArray.Length == 0 )
                {
                    numArray = ( byte[ ] )null;
                }

                if( numArray != null )
                {
                    byteList.AddRange( ( IEnumerable< byte > )numArray );
                }

                byteList.AddRange( ( IEnumerable< byte > ) Encoding.Unicode.GetBytes( assemblyName.Name ) );
                int num1 = Class3.smethod_3( typeof( Class3 ) );
                int num2 = Class3.Class10.smethod_0( );
                byteList.Add( ( byte ) ( num1 >> 24 ) );
                byteList.Add( ( byte ) ( num2 >> 8 ) );
                byteList.Add( ( byte ) ( num1 >> 8 ) );
                byteList.Add( ( byte ) ( num2 >> 16 ) );
                byteList.Add( ( byte ) num1 );
                byteList.Add( ( byte ) ( num2 >> 24 ) );
                byteList.Add( ( byte ) ( num1 >> 16 ) );
                byteList.Add( ( byte ) num2 );
                int count = byteList.Count;
                ulong num3 = 0;
                for( int index = 0; index != count; ++index )
                {
                    ulong num4 = num3 + ( ulong ) byteList[ index ];
                    ulong num5 = num4 + ( num4 << 20 );
                    num3 = num5 ^ num5 >> 12;
                    byteList[ index ] = ( byte ) 0;
                }
                ulong num6 = num3 + ( num3 << 6 );
                ulong num7 = num6 ^ num6 >> 22;
                long_0 = ( long ) ( num7 + ( num7 << 30 ) ) ^ -1335956550818274335L;
                Class3.class4_0.method_1( long_0 );
            }
            return long_0;
        }
    }

    private static bool smethod_1( )
    {
        return Class3.smethod_2( );
    }

    private static bool smethod_2( )
    {
        StackFrame frame = new StackTrace( ).GetFrame( 3 );
        MethodBase methodBase = frame == null ? ( MethodBase ) null : frame.GetMethod( );
        Type type = ( object ) methodBase == null ? ( Type ) null : methodBase.DeclaringType;
        return ( object ) type != ( object ) typeof( RuntimeMethodHandle ) && ( object ) type != null && ( object ) type.Assembly == ( object ) typeof( Class3 ).Assembly;
    }

    private static int smethod_3( Type type_0 )
    {
        return type_0.MetadataToken;
    }

    private sealed class Class4
    {
        private int int_0;
        private int int_1;

        internal Class4( )
        {
            this.method_1( 0L );
        }

        internal long method_0( )
        {
            if( ( object ) Assembly.GetCallingAssembly( ) != ( object ) typeof( Class3.Class4 ).Assembly || !Class3.smethod_1( ) )
            {
                return 2918384;
            }

            int[ ] numArray = new int[4] { 0, 0, 0, -1374912990 };
            numArray[ 1 ] = 458493111;
            numArray[ 2 ] = 1130779063;
            numArray[ 0 ] = 1931465575;
            int int0 = this.int_0;
            int int1 = this.int_1;
            int num1 = -1640531527;
            int num2 = -957401312;
            for( int index = 0; index != 32; ++index )
            {
                int1 -= ( int0 << 4 ^ int0 >> 5 ) + int0 ^ num2 + numArray[ num2 >> 11 & 3 ];
                num2 -= num1;
                int0 -= ( int1 << 4 ^ int1 >> 5 ) + int1 ^ num2 + numArray[ num2 & 3 ];
            }
            for( int index = 0; index != 4; ++index )
            {
                numArray[ index ] = 0;
            }

            return ( long ) ( ( ulong ) int1 << 32 | ( ulong ) ( uint ) int0 );
        }

        internal void method_1( long long_0 )
        {
            if( ( object ) Assembly.GetCallingAssembly( ) != ( object ) typeof( Class3.Class4 ).Assembly || !Class3.smethod_1( ) )
            {
                return;
            }

            int[ ] numArray = new int[4] { 0, 458493111, 0, 0 };
            numArray[ 0 ] = 1931465575;
            numArray[ 2 ] = 1130779063;
            numArray[ 3 ] = -1374912990;
            int num1 = -1640531527;
            int num2 = ( int ) long_0;
            int num3 = ( int ) ( long_0 >> 32 );
            int num4 = 0;
            for( int index = 0; index != 32; ++index )
            {
                num2 += ( num3 << 4 ^ num3 >> 5 ) + num3 ^ num4 + numArray[ num4 & 3 ];
                num4 += num1;
                num3 += ( num2 << 4 ^ num2 >> 5 ) + num2 ^ num4 + numArray[ num4 >> 11 & 3 ];
            }
            for( int index = 0; index != 4; ++index )
            {
                numArray[ index ] = 0;
            }

            this.int_0 = num2;
            this.int_1 = num3;
        }
    }

    private sealed class Class5
    {
        internal static int smethod_0( )
        {
            return Class3.Class8.smethod_1( Class3.Class8.smethod_1( Class3.Class11.smethod_0( ), Class3.Class8.smethod_0( Class3.smethod_3( typeof( Class3.Class5 ) ), Class3.Class9.smethod_0( ) ) ), Class3.smethod_3( typeof( Class3.Class7 ) ) );
        }
    }

    private sealed class Class6
    {
        internal static int smethod_0( )
        {
            return Class3.Class8.smethod_2( Class3.smethod_3( typeof( Class3.Class6 ) ), Class3.Class8.smethod_0( Class3.smethod_3( typeof( Class3.Class10 ) ), Class3.Class8.smethod_1( Class3.smethod_3( typeof( Class3.Class9 ) ), Class3.Class8.smethod_2( Class3.smethod_3( typeof( Class3.Class11 ) ), Class3.Class8.smethod_0( Class3.smethod_3( typeof( Class3.Class5 ) ), Class3.smethod_3( typeof( Class3.Class7 ) ) ) ) ) ) );
        }
    }

    private sealed class Class7
    {
        internal static int smethod_0( )
        {
            return Class3.Class8.smethod_0( Class3.smethod_3( typeof( Class3.Class7 ) ), Class3.Class8.smethod_2( Class3.Class8.smethod_1( Class3.smethod_3( typeof( Class3.Class5 ) ), Class3.smethod_3( typeof( Class3.Class10 ) ) ), Class3.Class8.smethod_2( Class3.smethod_3( typeof( Class3.Class11 ) ) ^ 1512870855, Class3.Class5.smethod_0( ) ) ) );
        }
    }

    private static class Class8
    {
        internal static int smethod_0( int int_0, int int_1 )
        {
            return int_0 ^ int_1 - -1549043416;
        }

        internal static int smethod_1( int int_0, int int_1 )
        {
            return int_0 - 1419193580 ^ int_1 - 44586243;
        }

        internal static int smethod_2( int int_0, int int_1 )
        {
            return int_0 ^ ( int_1 - -841262995 ^ int_0 - int_1 );
        }
    }

    private sealed class Class9
    {
        internal static int smethod_0( )
        {
            return Class3.Class8.smethod_0( Class3.smethod_3( typeof( Class3.Class11 ) ), Class3.smethod_3( typeof( Class3.Class6 ) ) ^ Class3.Class8.smethod_1( Class3.smethod_3( typeof( Class3.Class9 ) ), Class3.Class8.smethod_2( Class3.smethod_3( typeof( Class3.Class7 ) ), Class3.Class6.smethod_0( ) ) ) );
        }
    }

    private sealed class Class10
    {
        internal static int smethod_0( )
        {
            return Class3.Class8.smethod_2( Class3.Class8.smethod_1( Class3.smethod_3( typeof( Class3.Class9 ) ), Class3.Class8.smethod_2( Class3.smethod_3( typeof( Class3.Class10 ) ), Class3.smethod_3( typeof( Class3.Class5 ) ) ) ), Class3.Class7.smethod_0( ) );
        }
    }

    private sealed class Class11
    {
        internal static int smethod_0( )
        {
            return Class3.Class8.smethod_2( Class3.Class8.smethod_0( Class3.Class9.smethod_0( ) ^ 527758446, Class3.smethod_3( typeof( Class3.Class6 ) ) ), Class3.Class8.smethod_1( Class3.smethod_3( typeof( Class3.Class10 ) ) ^ Class3.smethod_3( typeof( Class3.Class7 ) ), 1045643646 ) );
        }
    }
}
