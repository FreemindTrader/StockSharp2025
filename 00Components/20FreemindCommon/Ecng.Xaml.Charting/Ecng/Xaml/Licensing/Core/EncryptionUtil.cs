// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.EncryptionUtil
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System.Security.Cryptography;
using System.Text;

namespace StockSharp.Xaml.Licensing.Core
{
    public class EncryptionUtil
    {
        public static string Encrypt( string plainText, string password )
        {
            return SymmetricEncryption.Encrypt( plainText, password );
        }

        public static string Decrypt( string plainText, string password )
        {
            return SymmetricEncryption.Decrypt( plainText, password );
        }

        public static string Hash( string plainText )
        {
            SHA256Managed shA256Managed = new SHA256Managed();
            string empty = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            int offset = 0;
            int byteCount = Encoding.UTF8.GetByteCount(plainText);
            foreach ( byte num in shA256Managed.ComputeHash( bytes, offset, byteCount ) )
                empty += num.ToString( "x2" );
            return empty;
        }
    }
}
