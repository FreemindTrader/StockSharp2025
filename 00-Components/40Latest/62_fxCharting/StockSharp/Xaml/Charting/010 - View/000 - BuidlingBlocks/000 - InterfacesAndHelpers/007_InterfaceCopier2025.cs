using System;
using System.Reflection;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// The following class is mainly used to copy properties from one interface to another.
/// 
/// It is mainly used in <see cref="T:StockSharp.Xaml.Charting.ThemeColorProviderEx.ApplyTheme( IThemeProvider )"/> to copy properties 
/// from a theme interface to another theme provider's interface.
/// 
/// </summary>
public static class InterfaceCopier2025
{
    public static void CopyInterfaceProperties<T>( T typeSrc, T typeDest )
    {
        CheckInterfaceProperties<T>();
        
        foreach ( PropertyInfo property in typeof( T ).GetProperties() )
        {
            if ( property.CanRead && property.CanWrite )
            {
                property.SetValue( typeDest, property.GetValue( typeSrc, null ), null );
            }
                
        }
    }

    private static void CheckInterfaceProperties<T>()
    {
        if ( !typeof( T ).IsInterface )
        {
            throw new Exception( $"Unable to copy interface properties as typeparam {typeof( T )} is not an interface" );
        }            
    }
}
