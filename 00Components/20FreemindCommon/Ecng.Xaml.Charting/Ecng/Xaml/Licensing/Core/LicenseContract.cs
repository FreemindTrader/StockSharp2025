// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.LicenseContract
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    public sealed class LicenseContract
    {
        public static LicenseContract InvalidLicence = new LicenseContract() { LicenseType = Decoder.LicenseType.InvalidLicense, ExpiresOn = DateTime.MinValue };

        public LicenseContract()
        {
            this.MachineSpecific = false;
        }

        public Decoder.LicenseType LicenseType
        {
            get; set;
        }

        public string Customer
        {
            get; set;
        }

        public string OrderId
        {
            get; set;
        }

        public string LicensedTo
        {
            get; set;
        }

        public string ProductCode
        {
            get; set;
        }

        public string SerialKey
        {
            get; set;
        }

        public int LicensedDevelopers
        {
            get; set;
        }

        public DateTime ExpiresOn
        {
            get; set;
        }

        public bool IsTrialLicense
        {
            get; set;
        }

        public bool MachineSpecific
        {
            get; set;
        }

        public string KeyCode
        {
            get; set;
        }

        public bool AllowDebugging
        {
            get
            {
                if ( !this.IsTrialLicense )
                    return !string.IsNullOrEmpty( this.SerialKey );
                return true;
            }
        }

        public string Serialize()
        {
            XElement xelement = new XElement((XName) nameof (LicenseContract), new object[5]{ (object) new XElement((XName) "Customer", (object) this.Customer), (object) new XElement((XName) "OrderId", (object) this.OrderId), (object) new XElement((XName) "LicenseCount", (object) this.LicensedDevelopers), (object) new XElement((XName) "IsTrialLicense", (object) this.IsTrialLicense), (object) new XElement((XName) "SupportExpires", (object) this.ExpiresOn.ToString((IFormatProvider) CultureInfo.InvariantCulture)) });
            if ( !string.IsNullOrEmpty( this.ProductCode ) )
                xelement.Add( ( object ) new XElement( ( XName ) "ProductCode", ( object ) this.ProductCode ) );
            if ( this.MachineSpecific )
                xelement.Add( ( object ) new XElement( ( XName ) "MachineSpecific", ( object ) this.MachineSpecific ) );
            if ( !string.IsNullOrEmpty( this.SerialKey ) )
                xelement.Add( ( object ) new XElement( ( XName ) "SerialKey", ( object ) this.SerialKey ) );
            if ( !string.IsNullOrEmpty( this.LicensedTo ) )
                xelement.Add( ( object ) new XElement( ( XName ) "LicensedTo", ( object ) this.LicensedTo ) );
            if ( !string.IsNullOrEmpty( this.KeyCode ) )
                xelement.Add( ( object ) new XElement( ( XName ) "KeyCode", ( object ) this.KeyCode ) );
            return new XDocument( new object[ 1 ] { ( object ) xelement } ).ToString( SaveOptions.None );
        }

        public static LicenseContract Deserialize( string rawXml )
        {
            XDocument xdocument = XDocument.Parse(rawXml);
            LicenseContract licenseContract = new LicenseContract();
            licenseContract.Customer = xdocument.Root.GetRequiredElementValue( "Customer" );
            licenseContract.OrderId = xdocument.Root.GetRequiredElementValue( "OrderId" );
            licenseContract.ExpiresOn = DateTime.Parse( xdocument.Root.GetRequiredElementValue( "SupportExpires" ), ( IFormatProvider ) CultureInfo.InvariantCulture );
            licenseContract.LicensedDevelopers = int.Parse( xdocument.Root.GetRequiredElementValue( "LicenseCount" ) );
            licenseContract.IsTrialLicense = bool.Parse( xdocument.Root.GetRequiredElementValue( "IsTrialLicense" ) );
            string optionalElementValue = xdocument.Root.GetOptionalElementValue("MachineSpecific");
            licenseContract.MachineSpecific = !string.IsNullOrEmpty( optionalElementValue ) && bool.Parse( optionalElementValue );
            licenseContract.SerialKey = xdocument.Root.GetOptionalElementValue( "SerialKey" );
            licenseContract.KeyCode = xdocument.Root.GetOptionalElementValue( "KeyCode" );
            licenseContract.LicensedTo = xdocument.Root.GetOptionalElementValue( "LicensedTo" );
            licenseContract.ProductCode = xdocument.Root.GetOptionalElementValue( "ProductCode" );
            return licenseContract;
        }
    }
}
