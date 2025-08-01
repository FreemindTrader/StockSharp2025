using System;
using System.Reflection;

namespace StockSharp.Xaml.Charting;
#nullable disable
public static class InterfaceHelpers2025
{
    public static void CopyInterfaceProperties<T>(T _param0, T _param1)
    {
        InterfaceHelpers2025.CheckInterfaceProperties<T>();
        foreach ( PropertyInfo property in typeof(T).GetProperties() )
        {
            if ( property.CanRead && property.CanWrite )
                property.SetValue((object)_param1, property.GetValue((object)_param0, (object[])null), (object[])null);
        }
    }

    private static void CheckInterfaceProperties<T>()
    {
        if ( !typeof(T).IsInterface )
            throw new Exception($"Unable to copy interface properties as typeparam {typeof(T)} is not an interface");
    }
}
