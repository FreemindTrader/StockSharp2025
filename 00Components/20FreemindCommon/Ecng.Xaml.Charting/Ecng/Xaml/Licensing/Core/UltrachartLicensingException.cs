// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.UltrachartLicensingException
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Reflection;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    public class UltrachartLicensingException : Exception
    {
        public UltrachartLicensingException( string message )
          : base( message )
        {
        }

        public static UltrachartLicensingException Create( bool isTrialExpired, bool isRuntimeKeyPresent )
        {
            return new UltrachartLicensingException( ( !isRuntimeKeyPresent ? ( !isTrialExpired ? "Your Ultrachart licence is invalid" : "Your trial of Ultrachart has expired." ) : "Ultrachart must be activated on this machine using a purchased serial key to allow development." ) + Environment.NewLine + "Please contact support@ultrachart.com" );
        }
    }
}
