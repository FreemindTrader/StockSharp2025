
using System.Reflection;
using StockSharp.Xaml.Licensing.Core;

namespace fx.Xaml.Charting
{
    [Obfuscation( Feature = "encryptmethod;encryptstrings;encryptconstants", Exclude = false, ApplyToMembers = true, StripAfterObfuscation = true )]
    internal class UltraTradeChartLicenseProvider : Credentials, IUltrachartLicenseProvider
    {
        public UltraTradeChartLicenseProvider()
        {
        }

        public void Validate( object parameter )
        {
        }
    }
}
