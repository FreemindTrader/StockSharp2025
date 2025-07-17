//// Decompiled with JetBrains decompiler
//// Type: StockSharp.Xaml.Licensing.Core.Credentials
//// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

//using StockSharp.Xaml.Charting.Visuals;
//using System;
//using System.Globalization;
//using System.Reflection;
//using System.Text;

//namespace StockSharp.Xaml.Licensing.Core
//{
//    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
//    public abstract class Credentials
//    {
//        private bool? _valid;

//        public bool IsFeatureEnabled { get; private set; }

//        public void SetTamper( )
//        {
//        }

//        protected bool IsTrial
//        {
//            get
//            {
//                return false;
//            }
//        }





//        public string ProductCode
//        {
//            get
//            {
//                return "SC-SRC";
//            }
//        }



//        private static ulong CalculateHash( string read )
//        {
//            ulong num = 3074457345618258791;
//            foreach ( char ch in read )
//            {
//                num = ( num + ( ulong ) ch ) * 3074457345618258799UL;
//            }

//            return num;
//        }

//        private void EnsureFlag( )
//        {
//            if ( this._valid.HasValue )
//            {
//                return;
//            }

//            this._valid = new bool?( false );
//            try
//            {
//                string str1 = typeof (UltrachartSurface).GetProperty(Encoding.UTF8.GetString(Convert.FromBase64String("TGljZW5zZUtleQ==")), BindingFlags.Static | BindingFlags.NonPublic).GetValue((object) null, (object[]) null) as string;
//                if ( str1 == null )
//                {
//                    return;
//                }

//                string s1 = str1.Substring(0, 16);
//                string s2 = str1.Substring(16, 2);
//                string str2 = str1.Substring(18);
//                ulong num = ulong.Parse(s1, NumberStyles.HexNumber);
//                this._valid = new bool?( ( long ) Credentials.CalculateHash( s2 + str2 ) == ( long ) num );
//                this.IsFeatureEnabled = ( uint ) ( int.Parse( s2, NumberStyles.HexNumber ) & 1 ) > 0U;
//            }
//            catch
//            {
//            }
//        }
//    }
//}

using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( Feature = "encryptmethod;encryptstrings;encryptconstants", Exclude = false, ApplyToMembers = true, StripAfterObfuscation = true )]
    public abstract class Credentials
    {
        private bool? _valid;

        public bool IsFeatureEnabled
        {
            get;
            private set;
        }


        public bool IsLicenseValid
        {
            get
            {
                return true;
            }
        }

        protected bool IsTrial
        {
            get
            {
                return false;
            }
        }


        protected int LicenseDaysRemaining
        {
            get
            {
                return int.MaxValue;
            }
        }

        protected Decoder.LicenseType LicenseType
        {
            get
            {
                return Decoder.LicenseType.Full;
            }
        }

        public string ProductCode
        {
            get
            {
                return "SC-SRC";
            }
        }

        protected Credentials()
        {
        }

        private static ulong CalculateHash( string read )
        {
            ulong num = 0x2aaaaaaaaaaaab67L;
            string str = read;
            for ( int i = 0 ; i < str.Length ; i++ )
            {
                num += ( ulong ) str[ i ];
                num *= 0x2aaaaaaaaaaaab6fL;
            }
            return num;
        }

        private void EnsureFlag()
        {
            if ( this._valid.HasValue )
            {
                return;
            }
            this._valid = new bool?( false );
            try
            {
                string value = typeof(UltrachartSurface).GetProperty(Encoding.UTF8.GetString(Convert.FromBase64String("TGljZW5zZUtleQ==")), BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null) as string;

                if ( value != null )
                {
                    string str = value.Substring(0, 16);
                    string str1 = value.Substring(16, 2);
                    string str2 = value.Substring(18);
                    ulong num = ulong.Parse(str, NumberStyles.HexNumber);
                    this._valid = new bool?( Credentials.CalculateHash( string.Concat( str1, str2 ) ) == num );
                    int num1 = int.Parse(str1, NumberStyles.HexNumber);
                    this.IsFeatureEnabled = ( num1 & 1 ) != 0;
                }
            }
            catch
            {
            }
        }

        public void SetTamper()
        {
        }
    }
}

