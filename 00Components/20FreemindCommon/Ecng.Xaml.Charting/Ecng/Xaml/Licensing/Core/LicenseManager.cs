// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.LicenseManager
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    public sealed class LicenseManager : ILicenseManager
    {
        [Obfuscation( Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
        public void Validate<T>( T instance, IProviderFactory factory )
        {
            if ( ( object ) instance == ( object ) default( T ) )
            {
                return;
            }

            UltrachartLicenseProviderAttribute providerAttribute = ((IEnumerable<object>) typeof (T).GetCustomAttributes(typeof (UltrachartLicenseProviderAttribute), true)).FirstOrDefault<object>() as UltrachartLicenseProviderAttribute ?? ((IEnumerable<object>) instance.GetType().GetCustomAttributes(typeof (UltrachartLicenseProviderAttribute), true)).FirstOrDefault<object>() as UltrachartLicenseProviderAttribute;
            if ( providerAttribute == null )
            {
                return;
            }

            factory.CreateInstance( providerAttribute.ProviderType ).Validate( ( object ) instance );
        }
    }
}
