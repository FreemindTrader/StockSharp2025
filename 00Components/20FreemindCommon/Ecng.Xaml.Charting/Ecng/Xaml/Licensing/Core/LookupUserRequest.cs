// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.LookupUserRequest
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Reflection;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    public class LookupUserRequest
    {
        public LookupUserRequest()
        {
        }

        public LookupUserRequest( string username, string password )
        {
            this.UserName = username;
            this.Password = SymmetricEncryption.Encrypt( password );
        }

        public string UserName
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string GetPassword()
        {
            return SymmetricEncryption.Decrypt( this.Password );
        }
    }
}
