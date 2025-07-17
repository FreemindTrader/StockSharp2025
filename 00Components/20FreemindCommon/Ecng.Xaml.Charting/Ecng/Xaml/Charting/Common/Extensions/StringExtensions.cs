// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Extensions.StringExtensions
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    internal static class StringExtensions
    {
        internal static string Substring( this string input, string before, string after )
        {
            int startIndex = string.IsNullOrEmpty(before) ? 0 : input.IndexOf(before) + before.Length;
            int length = string.IsNullOrEmpty(after) ? input.Length : input.IndexOf(after) - startIndex;
            return input.Substring( startIndex, length );
        }

        internal static bool IsNullOrWhiteSpace( this string input )
        {
            if ( !string.IsNullOrEmpty( input ) )
                return string.Equals( input, new string( ' ', input.Length ) );
            return true;
        }
    }
}
