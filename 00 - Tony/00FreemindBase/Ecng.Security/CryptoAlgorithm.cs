using Ecng.Security.Cryptographers;
using System;
using System.Security.Cryptography;

namespace Ecng.Security
{
    [Serializable]
    public class CryptoAlgorithm : IDisposable
    {
        private readonly SymmetricCryptographer _symmetric;
        private readonly AsymmetricCryptographer _asymmetric;
        private readonly HashCryptographer _hash;
        public const string DefaultSymmetricAlgoName = "AES";
        public const string DefaultAsymmetricAlgoName = "RSA";
        public const string DefaultHashAlgoName = "SHA";

        public CryptoAlgorithm( SymmetricCryptographer symmetric )
        {
            SymmetricCryptographer symmetricCryptographer = symmetric;
            if ( symmetricCryptographer == null )
                throw new ArgumentNullException( nameof( symmetric ) );
            this._symmetric = symmetricCryptographer;
        }

        public CryptoAlgorithm( AsymmetricCryptographer asymmetric )
        {
            AsymmetricCryptographer asymmetricCryptographer = asymmetric;
            if ( asymmetricCryptographer == null )
                throw new ArgumentNullException( nameof( asymmetric ) );
            this._asymmetric = asymmetricCryptographer;
        }

        public CryptoAlgorithm( HashCryptographer hash )
        {
            HashCryptographer hashCryptographer = hash;
            if ( hashCryptographer == null )
                throw new ArgumentNullException( nameof( hash ) );
            this._hash = hashCryptographer;
        }

        public static CryptoAlgorithm CreateAssymetricVerifier( byte[ ] publicKey )
        {
            return new CryptoAlgorithm( new AsymmetricCryptographer( AsymmetricAlgorithm.Create( "RSA" ), publicKey ) );
        }

        public static CryptoAlgorithm Create( AlgorithmTypes type, params byte[ ][ ] keys )
        {
            switch ( type )
            {
                case AlgorithmTypes.Symmetric:
                return new CryptoAlgorithm( new SymmetricCryptographer( SymmetricAlgorithm.Create( "AES" ), keys[0] ) );
                case AlgorithmTypes.Asymmetric:
                return new CryptoAlgorithm( new AsymmetricCryptographer( AsymmetricAlgorithm.Create( "RSA" ), keys[0], keys[1] ) );
                case AlgorithmTypes.Hash:
                return new CryptoAlgorithm( keys.Length == 0 ? new HashCryptographer( HashAlgorithm.Create( "SHA" ), ( byte[ ] )null ) : new HashCryptographer( HashAlgorithm.Create( "SHA" ), keys[0] ) );
                default:
                throw new ArgumentOutOfRangeException( nameof( type ), ( object )type, "Unknown type." );
            }
        }

        public byte[ ] Encrypt( byte[ ] data )
        {
            if ( this._symmetric != null )
                return this._symmetric.Encrypt( data );
            if ( this._asymmetric != null )
                return this._asymmetric.Encrypt( data );
            if ( this._hash != null )
                return this._hash.ComputeHash( data );
            throw new ArgumentOutOfRangeException();
        }

        public byte[ ] Decrypt( byte[ ] data )
        {
            if ( this._symmetric != null )
                return this._symmetric.Decrypt( data );
            if ( this._asymmetric != null )
                return this._asymmetric.Decrypt( data );
            if ( this._hash != null )
                throw new NotSupportedException();
            throw new ArgumentOutOfRangeException();
        }

        public byte[ ] CreateSignature( byte[ ] data )
        {
            if ( this._asymmetric == null )
                throw new NotSupportedException();
            return this._asymmetric.CreateSignature( data );
        }

        public bool VerifySignature( byte[ ] data, byte[ ] signature )
        {
            if ( this._asymmetric == null )
                throw new NotSupportedException();
            return this._asymmetric.VerifySignature( data, signature );
        }

        public void Dispose()
        {
            if ( this._symmetric != null )
                this._symmetric.Dispose();
            if ( this._asymmetric == null )
                return;
            this._asymmetric.Dispose();
        }
    }
}
