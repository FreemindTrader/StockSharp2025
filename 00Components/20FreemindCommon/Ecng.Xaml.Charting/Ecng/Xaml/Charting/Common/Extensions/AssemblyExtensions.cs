// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.AssemblyExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Reflection;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    internal static class AssemblyExtensions
    {
        internal static string GetDllName( this Assembly assembly )
        {
            int length = assembly.FullName.IndexOf(",");
            return assembly.FullName.Substring( 0, length );
        }

        internal static bool IsDynamic( this Assembly assembly )
        {
            return false;
        }
    }
}
