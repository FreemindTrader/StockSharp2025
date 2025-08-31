using System;
using System.Runtime.InteropServices;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// A helper class for template types.
/// </summary>
[StructLayout( LayoutKind.Auto, CharSet = CharSet.Auto )]
public static class TemplateTypeHelper
{
    public static T GetFirstElement<T>( T[ ] ttype )
    {
        if ( ttype == null )
            throw new ArgumentNullException();

        return ttype.Length != 0 ? ttype[0] : default( T );
    }

    public static bool IsNotEmpty<T>( T[ ] ttype )
    {
        if ( ttype == null )
            throw new ArgumentNullException();

        return ttype.Length != 0;
    }
}
