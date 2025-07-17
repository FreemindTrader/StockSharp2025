// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Licensing.UltrachartLicenseProviderFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Reflection;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    internal class UltrachartLicenseProviderFactory : IProviderFactory
    {
        public IUltrachartLicenseProvider CreateInstance( Type providerType )
        {
            return ( IUltrachartLicenseProvider ) Activator.CreateInstance( providerType );
        }
    }
}
