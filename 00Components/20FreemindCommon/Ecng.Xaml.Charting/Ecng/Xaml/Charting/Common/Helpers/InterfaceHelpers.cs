// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Helpers.InterfaceHelpers
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Reflection;

namespace StockSharp.Xaml.Charting.Common.Helpers
{
    internal static class InterfaceHelpers
    {
        public static void CopyInterfaceProperties<T>( T from, T to )
        {
            InterfaceHelpers.CheckInterfaceProperties<T>();
            foreach ( PropertyInfo property in typeof( T ).GetProperties() )
            {
                if ( property.CanRead && property.CanWrite )
                    property.SetValue( ( object ) to, property.GetValue( ( object ) from, ( object[ ] ) null ), ( object[ ] ) null );
            }
        }

        private static void CheckInterfaceProperties<T>()
        {
            if ( !typeof( T ).IsInterface )
                throw new Exception( string.Format( "Unable to copy interface properties as typeparam {0} is not an interface", ( object ) typeof( T ) ) );
        }
    }
}
