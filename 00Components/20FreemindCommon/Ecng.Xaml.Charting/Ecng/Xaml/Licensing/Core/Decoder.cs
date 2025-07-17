// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.Decoder
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Reflection;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    public class Decoder
    {
        private readonly DateTime _buildDate;

        public enum LicenseType
        {
            Trial = 2,
            InvalidDeveloperLicense = 15, // 0x0000000F
            Full = 32, // 0x00000020
            TrialExpired = 64, // 0x00000040
            SubscriptionExpired = 128, // 0x00000080
            InvalidLicense = 255, // 0x000000FF
        }
    }
}
