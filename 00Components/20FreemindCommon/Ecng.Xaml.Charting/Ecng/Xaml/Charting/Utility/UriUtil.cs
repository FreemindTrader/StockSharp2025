// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Utility.UriUtil
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Reflection;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting.Utility
{
    internal static class UriUtil
    {
        internal static Uri MakePackUri( string resource )
        {
            return new Uri( string.Format( "pack://application:,,,/{0};component/{1}", ( object ) typeof( UriUtil ).Assembly.GetDllName(), ( object ) resource ), UriKind.RelativeOrAbsolute );
        }

        internal static Uri MakePackUri( Assembly assembly, string resource )
        {
            return new Uri( string.Format( "pack://application:,,,/{0};component/{1}", ( object ) assembly.GetDllName(), ( object ) resource ), UriKind.RelativeOrAbsolute );
        }

        internal static Uri PackUri
        {
            get
            {
                return new Uri( string.Format( "pack://application:,,,/{0};component/Resources/SCW.png", ( object ) typeof( UltrachartSurface ).Assembly.GetDllName() ) );
            }
        }

        internal static Uri PackUri2
        {
            get
            {
                return new Uri( string.Format( "pack://application:,,,/{0};component/Resources/sct.png", ( object ) typeof( UltrachartSurface ).Assembly.GetDllName() ) );
            }
        }

        internal static string ExtUri
        {
            get
            {
                return "http://www.ultrachart.com";
            }
        }
    }
}
