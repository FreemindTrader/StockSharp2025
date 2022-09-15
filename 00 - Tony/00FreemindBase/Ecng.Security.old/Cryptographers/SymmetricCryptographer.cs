using Ecng.Common;
using Ecng.Security.Properties;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Ecng.Security.Cryptographers
{
    public class SymmetricCryptographer : Disposable
    {
        private readonly SymmetricAlgorithm _algorithm;

        public SymmetricCryptographer( SymmetricAlgorithm algorithm, byte[ ] key )
        {
            SymmetricAlgorithm symmetricAlgorithm = algorithm;
            if ( symmetricAlgorithm == null )
                throw new ArgumentNullException( nameof( algorithm ) );
            this._algorithm = symmetricAlgorithm;
            this._algorithm.Key = key;
        }

        protected override void DisposeManaged()
        {
            if ( this.IsDisposed )
                return;
            this._algorithm.Dispose();
        }

        public byte[ ] Encrypt( byte[ ] plaintext )
        {
            byte[ ] numArray1 = ( byte[ ] )null;
            using ( ICryptoTransform encryptor = this._algorithm.CreateEncryptor() )
                numArray1 = SymmetricCryptographer.Transform( encryptor, plaintext );
            byte[ ] numArray2 = new byte[this.IVLength + numArray1.Length];
            Buffer.BlockCopy( ( Array )this._algorithm.IV, 0, ( Array )numArray2, 0, this.IVLength );
            Buffer.BlockCopy( ( Array )numArray1, 0, ( Array )numArray2, this.IVLength, numArray1.Length );
            SymmetricCryptographer.ZeroOutBytes( this._algorithm.Key );
            return numArray2;
        }

        public byte[ ] Decrypt( byte[ ] encryptedText )
        {
            byte[ ] numArray = ( byte[ ] )null;
            byte[ ] iv = this.ExtractIV( encryptedText );
            using ( ICryptoTransform decryptor = this._algorithm.CreateDecryptor() )
                numArray = SymmetricCryptographer.Transform( decryptor, iv );
            SymmetricCryptographer.ZeroOutBytes( this._algorithm.Key );
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
                if ( this._algorithm.IV == null )
                    this._algorithm.GenerateIV();
                return this._algorithm.IV.Length;
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
            this._algorithm.IV = numArray1;
            return numArray2;
        }
    }
}
