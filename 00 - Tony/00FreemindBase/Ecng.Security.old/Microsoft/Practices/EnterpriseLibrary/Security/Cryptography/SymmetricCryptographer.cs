// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.SymmetricCryptographer
// Assembly: Ecng.Security, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1B8DA2C0-4192-443C-B400-59D7DF43C80E
// Assembly location: T:\00-StockSharp\Data\Ecng.Security.dll

using Ecng.Common;
using Ecng.Security.Properties;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
{
    public class SymmetricCryptographer : IDisposable
    {
        private readonly SymmetricAlgorithm algorithm;
        private readonly ProtectedKey key;

        private byte[ ] Key
        {
            get
            {
                return this.key.DecryptedKey;
            }
        }

        public SymmetricCryptographer( Type algorithmType, ProtectedKey key )
        {
            if ( ( object )algorithmType == null )
                throw new ArgumentNullException( nameof( algorithmType ) );
            if ( !algorithmType.Is<SymmetricAlgorithm>() )
                throw new ArgumentException( Resources.ExceptionCreatingSymmetricAlgorithmInstance, nameof( algorithmType ) );
            ProtectedKey protectedKey = key;
            if ( protectedKey == null )
                throw new ArgumentNullException( nameof( key ) );
            this.key = protectedKey;
            this.algorithm = SymmetricCryptographer.GetSymmetricAlgorithm( algorithmType );
        }

        ~SymmetricCryptographer()
        {
            this.Dispose( false );
        }

        public void Dispose()
        {
            this.Dispose( true );
            GC.SuppressFinalize( ( object )this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( this.algorithm == null )
                return;
            this.algorithm.Clear();
        }

        public byte[ ] Encrypt( byte[ ] plaintext )
        {
            byte[ ] numArray1 = ( byte[ ] )null;
            this.algorithm.Key = this.Key;
            using ( ICryptoTransform encryptor = this.algorithm.CreateEncryptor() )
                numArray1 = SymmetricCryptographer.Transform( encryptor, plaintext );
            byte[ ] numArray2 = new byte[this.IVLength + numArray1.Length];
            Buffer.BlockCopy( ( Array )this.algorithm.IV, 0, ( Array )numArray2, 0, this.IVLength );
            Buffer.BlockCopy( ( Array )numArray1, 0, ( Array )numArray2, this.IVLength, numArray1.Length );
            SymmetricCryptographer.ZeroOutBytes( this.algorithm.Key );
            return numArray2;
        }

        public byte[ ] Decrypt( byte[ ] encryptedText )
        {
            byte[ ] numArray = ( byte[ ] )null;
            byte[ ] iv = this.ExtractIV( encryptedText );
            this.algorithm.Key = this.Key;
            using ( ICryptoTransform decryptor = this.algorithm.CreateDecryptor() )
                numArray = SymmetricCryptographer.Transform( decryptor, iv );
            SymmetricCryptographer.ZeroOutBytes( this.algorithm.Key );
            return numArray;
        }

        private static byte[ ] Transform( ICryptoTransform transform, byte[ ] buffer )
        {
            if ( buffer == null )
                throw new ArgumentNullException( nameof( buffer ) );
            using ( MemoryStream memoryStream = new MemoryStream() )
            {
                CryptoStream cryptoStream = ( CryptoStream )null;
                try
                {
                    cryptoStream = new CryptoStream( ( Stream )memoryStream, transform, CryptoStreamMode.Write );
                    cryptoStream.Write( buffer, 0, buffer.Length );
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
                finally
                {
                    if ( cryptoStream != null )
                    {
                        cryptoStream.Close();
                        cryptoStream.Dispose();
                    }
                }
            }
        }

        public static void ZeroOutBytes( byte[ ] bytes )
        {
            if ( bytes == null )
                return;
            ArrayHelper.Clear( bytes );
        }

        private int IVLength
        {
            get
            {
                if ( this.algorithm.IV == null )
                    this.algorithm.GenerateIV();
                return this.algorithm.IV.Length;
            }
        }

        private byte[ ] ExtractIV( byte[ ] encryptedText )
        {
            byte[ ] numArray1 = new byte[this.IVLength];
            if ( encryptedText.Length < this.IVLength + 1 )
                throw new CryptographicException( Resources.ExceptionDecrypting );
            byte[ ] numArray2 = new byte[encryptedText.Length - this.IVLength];
            Buffer.BlockCopy( ( Array )encryptedText, 0, ( Array )numArray1, 0, this.IVLength );
            Buffer.BlockCopy( ( Array )encryptedText, this.IVLength, ( Array )numArray2, 0, numArray2.Length );
            this.algorithm.IV = numArray1;
            return numArray2;
        }

        private static SymmetricAlgorithm GetSymmetricAlgorithm( Type algorithmType )
        {
            return Activator.CreateInstance( algorithmType ) as SymmetricAlgorithm;
        }
    }
}
