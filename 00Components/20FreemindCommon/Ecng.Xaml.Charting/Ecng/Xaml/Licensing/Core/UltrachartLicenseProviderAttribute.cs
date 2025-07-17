// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.UltrachartLicenseProviderAttribute
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Reflection;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = true )]
    public sealed class UltrachartLicenseProviderAttribute : Attribute
    {
        public UltrachartLicenseProviderAttribute( Type providerType )
        {
            this.ProviderType = providerType;
        }

        internal Type ProviderType
        {
            get; private set;
        }
    }
}
