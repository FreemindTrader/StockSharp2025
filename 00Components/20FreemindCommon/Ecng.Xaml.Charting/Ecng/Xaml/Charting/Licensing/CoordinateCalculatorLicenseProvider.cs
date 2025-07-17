// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Licensing.CoordinateCalculatorLicenseProvider
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Reflection;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Licensing
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    internal class CoordinateCalculatorLicenseProvider : Credentials, IUltrachartLicenseProvider
    {
        private static readonly Random rand = new Random();
        private static bool? _flag = new bool?();

        public void Validate( object parameter )
        {
            DoubleCoordinateCalculator coordinateCalculator1 = parameter as DoubleCoordinateCalculator;
            if ( coordinateCalculator1 != null && CoordinateCalculatorLicenseProvider.rand.NextDouble() < 0.33 && this.GetTamper() )
            {
                coordinateCalculator1.CoordinateConstant *= CoordinateCalculatorLicenseProvider.rand.NextDouble();
            }

            FlippedDoubleCoordinateCalculator coordinateCalculator2 = parameter as FlippedDoubleCoordinateCalculator;
            if ( coordinateCalculator1 == null || CoordinateCalculatorLicenseProvider.rand.NextDouble() >= 0.33 || !this.GetTamper() )
            {
                return;
            }

            coordinateCalculator1.CoordinateConstant *= CoordinateCalculatorLicenseProvider.rand.NextDouble();
        }

        [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
        private bool GetTamper()
        {
            if ( !CoordinateCalculatorLicenseProvider._flag.HasValue )
            {
                CoordinateCalculatorLicenseProvider._flag = new bool?( false );
            }

            return CoordinateCalculatorLicenseProvider._flag.Value;
        }
    }
}
