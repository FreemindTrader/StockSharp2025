using Ecng.Common;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;

namespace Ecng.Security
{
    public static class CryptoHelper
    {
        private const int _keySize = 256;
        private const int _derivationIterations = 1000;

        public static byte[ ] ToBytes( this ProtectedKey key )
        {
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            return key.DecryptedKey;
        }

        public static ProtectedKey FromBytes( this byte[ ] data )
        {
            return ProtectedKey.CreateFromPlaintextKey( data, DataProtectionScope.LocalMachine );
        }

        public static byte[ ] FromRsa( this RSAParameters param )
        {
            MemoryStream memoryStream = new MemoryStream();
            CryptoHelper.WriteByteArray( ( Stream )memoryStream, param.P );
            CryptoHelper.WriteByteArray( ( Stream )memoryStream, param.Q );
            CryptoHelper.WriteByteArray( ( Stream )memoryStream, param.D );
            CryptoHelper.WriteByteArray( ( Stream )memoryStream, param.DP );
            CryptoHelper.WriteByteArray( ( Stream )memoryStream, param.DQ );
            CryptoHelper.WriteByteArray( ( Stream )memoryStream, param.InverseQ );
            CryptoHelper.WriteByteArray( ( Stream )memoryStream, param.Exponent );
            CryptoHelper.WriteByteArray( ( Stream )memoryStream, param.Modulus );
            return memoryStream.To<byte[ ]>();
        }

        public static RSAParameters ToRsa( this byte[ ] key )
        {
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            Stream stream = key.To<Stream>();
            return new RSAParameters() { P = CryptoHelper.ReadByteArray( stream ), Q = CryptoHelper.ReadByteArray( stream ), D = CryptoHelper.ReadByteArray( stream ), DP = CryptoHelper.ReadByteArray( stream ), DQ = CryptoHelper.ReadByteArray( stream ), InverseQ = CryptoHelper.ReadByteArray( stream ), Exponent = CryptoHelper.ReadByteArray( stream ), Modulus = CryptoHelper.ReadByteArray( stream ) };
        }

        public static RSAParameters ToRsa( this ProtectedKey key )
        {
            if ( key == null )
                throw new ArgumentNullException( nameof( key ) );
            return key.DecryptedKey.ToRsa();
        }


        private static void WriteByteArray( Stream stream, byte[ ] array )
        {
            if ( stream == null )
                throw new ArgumentNullException( nameof( stream ) );
            stream.WriteEx( ( object )( array == null ) );
            if ( array == null )
                return;
            stream.WriteEx( ( object )array );
        }

        private static byte[ ] ReadByteArray( Stream stream )
        {
            if ( stream == null )
                throw new ArgumentNullException( nameof( stream ) );
            if ( stream.Read<bool>() )
                return ( byte[ ] )null;
            return stream.Read<byte[ ]>();
        }

        public static RSAParameters GenerateRsa()
        {
            using ( RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider() )
                return cryptoServiceProvider.ExportParameters( true );
        }

        public static RSAParameters PublicPart( this RSAParameters param )
        {
            return new RSAParameters() { Exponent = param.Exponent, Modulus = param.Modulus };
        }

        public static byte[ ] Protect( this byte[ ] plainText, byte[ ] entropy = null, DataProtectionScope scope = DataProtectionScope.LocalMachine )
        {
            return ProtectedData.Protect( plainText, entropy, scope );
        }

        public static byte[ ] Unprotect( this byte[ ] cipherText, byte[ ] entropy = null, DataProtectionScope scope = DataProtectionScope.LocalMachine )
        {
            return ProtectedData.Unprotect( cipherText, entropy, scope );
        }

        private static SymmetricAlgorithm CreateRijndaelManaged()
        {
            Aes aes = Aes.Create();
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            return ( SymmetricAlgorithm )aes;
        }

        public static byte[ ] Encrypt( this byte[ ] plain, string passPhrase, byte[ ] salt, byte[ ] iv )
        {
            if ( plain == null )
                throw new ArgumentNullException( nameof( plain ) );
            if ( passPhrase.IsEmpty() )
                throw new ArgumentNullException( nameof( passPhrase ) );
            if ( iv != null && iv.Length > 16 )
                iv = ( ( IEnumerable<byte> )iv ).Take<byte>( 16 ).ToArray<byte>();
            using ( Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes( passPhrase, salt, 1000 ) )
            {
                byte[ ] bytes = rfc2898DeriveBytes.GetBytes( 32 );
                using ( SymmetricAlgorithm rijndaelManaged = CryptoHelper.CreateRijndaelManaged() )
                {
                    using ( ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor( bytes, iv ) )
                    {
                        using ( MemoryStream memoryStream = new MemoryStream() )
                        {
                            using ( CryptoStream cryptoStream = new CryptoStream( ( Stream )memoryStream, encryptor, CryptoStreamMode.Write ) )
                            {
                                cryptoStream.Write( plain, 0, plain.Length );
                                cryptoStream.FlushFinalBlock();
                                return memoryStream.ToArray();
                            }
                        }
                    }
                }
            }
        }

        private static byte[ ] ReadStream( this CryptoStream stream, int maxLen )
        {
            byte[ ] array = new byte[maxLen];
            int num1;
            int num2;
            for ( num1 = 0; num1 < maxLen; num1 += num2 )
            {
                num2 = stream.Read( array, num1, maxLen - num1 );
                if ( num2 == 0 )
                    break;
            }
            Array.Resize<byte>( ref array, num1 );
            return array;
        }

        public static byte[ ] Decrypt(
          this byte[ ] cipherText,
          string passPhrase,
          byte[ ] salt,
          byte[ ] iv )
        {
            if ( cipherText == null )
                throw new ArgumentNullException( nameof( cipherText ) );
            if ( passPhrase.IsEmpty() )
                throw new ArgumentNullException( nameof( passPhrase ) );
            if ( iv != null && iv.Length > 16 )
                iv = ( ( IEnumerable<byte> )iv ).Take<byte>( 16 ).ToArray<byte>();
            using ( Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes( passPhrase, salt, 1000 ) )
            {
                byte[ ] bytes = rfc2898DeriveBytes.GetBytes( 32 );
                using ( SymmetricAlgorithm rijndaelManaged = CryptoHelper.CreateRijndaelManaged() )
                {
                    using ( ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor( bytes, iv ) )
                    {
                        using ( MemoryStream memoryStream = new MemoryStream( cipherText ) )
                        {
                            using ( CryptoStream stream = new CryptoStream( ( Stream )memoryStream, decryptor, CryptoStreamMode.Read ) )
                                return stream.ReadStream( cipherText.Length );
                        }
                    }
                }
            }
        }

        private static byte[ ] TransformAes(
          bool isEncrypt,
          byte[ ] inputBytes,
          string passPhrase,
          byte[ ] salt,
          byte[ ] iv )
        {
            if ( inputBytes == null )
                throw new ArgumentNullException( nameof( inputBytes ) );
            if ( passPhrase.IsEmpty() )
                throw new ArgumentNullException( nameof( passPhrase ) );
            using ( Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes( passPhrase, salt, 1000 ) )
            {
                byte[ ] bytes = rfc2898DeriveBytes.GetBytes( 32 );
                using ( Aes aes = Aes.Create() )
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    if ( isEncrypt )
                    {
                        using ( ICryptoTransform encryptor = aes.CreateEncryptor( bytes, iv ) )
                        {
                            using ( MemoryStream memoryStream = new MemoryStream() )
                            {
                                using ( CryptoStream cryptoStream = new CryptoStream( ( Stream )memoryStream, encryptor, CryptoStreamMode.Write ) )
                                {
                                    cryptoStream.Write( inputBytes, 0, inputBytes.Length );
                                    cryptoStream.FlushFinalBlock();
                                    return memoryStream.ToArray();
                                }
                            }
                        }
                    }
                    else
                    {
                        using ( ICryptoTransform decryptor = aes.CreateDecryptor( bytes, iv ) )
                        {
                            using ( MemoryStream memoryStream = new MemoryStream( inputBytes ) )
                            {
                                using ( CryptoStream stream = new CryptoStream( ( Stream )memoryStream, decryptor, CryptoStreamMode.Read ) )
                                    return stream.ReadStream( inputBytes.Length );
                            }
                        }
                    }
                }
            }
        }

        public static byte[ ] EncryptAes( this byte[ ] plain, string passPhrase, byte[ ] salt, byte[ ] iv )
        {
            return CryptoHelper.TransformAes( true, plain, passPhrase, salt, iv );
        }

        public static byte[ ] DecryptAes(
          this byte[ ] cipherText,
          string passPhrase,
          byte[ ] salt,
          byte[ ] iv )
        {
            return CryptoHelper.TransformAes( false, cipherText, passPhrase, salt, iv );
        }

        private static string Hash( this byte[ ] value, HashAlgorithm algo )
        {
            if ( value == null )
                throw new ArgumentNullException( nameof( value ) );
            if ( value.Length == 0 )
                throw new ArgumentOutOfRangeException( nameof( value ) );
            if ( algo == null )
                throw new ArgumentNullException( nameof( algo ) );
            using ( algo )
                return algo.ComputeHash( value ).Digest();
        }

        public static string Md5( this byte[ ] value )
        {
            return value.Hash( ( HashAlgorithm )MD5.Create() );
        }

        public static string Sha256( this byte[ ] value )
        {
            return value.Hash( ( HashAlgorithm )SHA256.Create() );
        }

        public static string Sha512( this byte[ ] value )
        {
            return value.Hash( ( HashAlgorithm )SHA512.Create() );
        }

        public static bool IsValid( this Secret secret, SecureString password )
        {
            return secret.IsValid( password.UnSecure() );
        }

        public static bool IsValid( this Secret secret, string password )
        {
            return secret.Equals( password.CreateSecret( secret ) );
        }

        public static Secret CreateSecret( this SecureString plainText )
        {
            return plainText.UnSecure().CreateSecret();
        }

        public static Secret CreateSecret( this string plainText )
        {
            return plainText.CreateSecret( TypeHelper.GenerateSalt( 128 ), ( CryptoAlgorithm )null );
        }

        public static Secret CreateSecret( this string plainText, Secret secret )
        {
            return plainText.CreateSecret( secret.CheckOnNull<Secret>( nameof( secret ) ).Salt, secret.Algo );
        }

        public static Secret CreateSecret(
          this string plainText,
          byte[ ] salt,
          CryptoAlgorithm algo = null )
        {
            if ( plainText.IsEmpty() )
                throw new ArgumentNullException( nameof( plainText ) );
            if ( salt == null )
                throw new ArgumentNullException( nameof( salt ) );
            byte[ ] numArray = plainText.Unicode();
            byte[ ] passwordBytes = new byte[numArray.Length + salt.Length];
            Buffer.BlockCopy( ( Array )numArray, 0, ( Array )passwordBytes, 0, numArray.Length );
            Buffer.BlockCopy( ( Array )salt, 0, ( Array )passwordBytes, numArray.Length - 1, salt.Length );
            return new Secret( passwordBytes, salt, algo );
        }
    }
}
