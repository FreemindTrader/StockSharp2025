using Ecng.Common;
using System;
using System.Security.Cryptography;

namespace Ecng.Security.Cryptographers
{
    public class AsymmetricCryptographer : Disposable
    {
        private readonly AsymmetricCryptographer.AsymmetricAlgorithmWrapper _encryptor;
        private readonly AsymmetricCryptographer.AsymmetricAlgorithmWrapper _decryptor;

        public AsymmetricCryptographer(
          AsymmetricAlgorithm algorithm,
          byte[ ] publicKey,
          byte[ ] privateKey )
          : this( publicKey == null ? ( AsymmetricCryptographer.AsymmetricAlgorithmWrapper )null : new AsymmetricCryptographer.AsymmetricAlgorithmWrapper( algorithm, publicKey ), privateKey == null ? ( AsymmetricCryptographer.AsymmetricAlgorithmWrapper )null : new AsymmetricCryptographer.AsymmetricAlgorithmWrapper( algorithm, privateKey ) )
        {
        }

        public AsymmetricCryptographer( AsymmetricAlgorithm algorithm, byte[ ] publicKey )
          : this( publicKey == null ? ( AsymmetricCryptographer.AsymmetricAlgorithmWrapper )null : new AsymmetricCryptographer.AsymmetricAlgorithmWrapper( algorithm, publicKey ), ( AsymmetricCryptographer.AsymmetricAlgorithmWrapper )null )
        {
        }

        protected AsymmetricCryptographer( AsymmetricAlgorithm encryptor, AsymmetricAlgorithm decryptor )
          : this( new AsymmetricCryptographer.AsymmetricAlgorithmWrapper( encryptor ), new AsymmetricCryptographer.AsymmetricAlgorithmWrapper( decryptor ) )
        {
        }

        private AsymmetricCryptographer(
          AsymmetricCryptographer.AsymmetricAlgorithmWrapper encryptor,
          AsymmetricCryptographer.AsymmetricAlgorithmWrapper decryptor )
        {
            if ( encryptor == null && decryptor == null )
                throw new ArgumentException();
            this._encryptor = encryptor;
            this._decryptor = decryptor;
        }

        public static AsymmetricCryptographer CreateFromPublicKey(
          AsymmetricAlgorithm algorithm,
          byte[ ] publicKey )
        {
            return new AsymmetricCryptographer( new AsymmetricCryptographer.AsymmetricAlgorithmWrapper( algorithm, publicKey ), ( AsymmetricCryptographer.AsymmetricAlgorithmWrapper )null );
        }

        public static AsymmetricCryptographer CreateFromPrivateKey(
          AsymmetricAlgorithm algorithm,
          byte[ ] privateKey )
        {
            return new AsymmetricCryptographer( ( AsymmetricCryptographer.AsymmetricAlgorithmWrapper )null, new AsymmetricCryptographer.AsymmetricAlgorithmWrapper( algorithm, privateKey ) );
        }

        public byte[ ] Encrypt( byte[ ] plainText )
        {
            if ( this._encryptor == null )
                throw new InvalidOperationException();
            return this._encryptor.Encrypt( plainText );
        }

        public byte[ ] Decrypt( byte[ ] encryptedText )
        {
            if ( this._decryptor == null )
                throw new InvalidOperationException();
            return this._decryptor.Decrypt( encryptedText );
        }

        protected override void DisposeManaged()
        {
            if ( ( Equatable<Wrapper<AsymmetricAlgorithm>> )this._encryptor != ( Wrapper<AsymmetricAlgorithm> )null )
                this._encryptor.Dispose();
            if ( ( Equatable<Wrapper<AsymmetricAlgorithm>> )this._decryptor != ( Wrapper<AsymmetricAlgorithm> )null )
                this._decryptor.Dispose();
            base.DisposeManaged();
        }

        public byte[ ] CreateSignature( byte[ ] data )
        {
            if ( this._decryptor == null )
                throw new InvalidOperationException();
            return this._decryptor.CreateSignature( data );
        }

        public bool VerifySignature( byte[ ] data, byte[ ] signature )
        {
            if ( this._encryptor == null )
                throw new InvalidOperationException();
            return this._encryptor.VerifySignature( data, signature );
        }

        private sealed class AsymmetricAlgorithmWrapper : Wrapper<AsymmetricAlgorithm>
        {
            public AsymmetricAlgorithmWrapper( AsymmetricAlgorithm algorithm, byte[ ] key )
              : this( AsymmetricCryptographer.AsymmetricAlgorithmWrapper.CreateAlgo( algorithm, key ) )
            {
            }

            public AsymmetricAlgorithmWrapper( AsymmetricAlgorithm value )
              : base( value )
            {
            }

            private static AsymmetricAlgorithm CreateAlgo(
              AsymmetricAlgorithm algorithm,
              byte[ ] key )
            {
                ( algorithm as RSACryptoServiceProvider )?.ImportParameters( key.ToRsa() );
                return algorithm;
            }

            public byte[ ] Encrypt( byte[ ] plainText )
            {
                RSACryptoServiceProvider cryptoServiceProvider = this.Value as RSACryptoServiceProvider;
                if ( cryptoServiceProvider != null )
                    return cryptoServiceProvider.Encrypt( plainText, false );
                throw new NotImplementedException();
            }

            public byte[ ] Decrypt( byte[ ] encryptedText )
            {
                RSACryptoServiceProvider cryptoServiceProvider = this.Value as RSACryptoServiceProvider;
                if ( cryptoServiceProvider != null )
                    return cryptoServiceProvider.Decrypt( encryptedText, false );
                throw new NotImplementedException();
            }

            public byte[ ] CreateSignature( byte[ ] data )
            {
                RSACryptoServiceProvider cryptoServiceProvider1 = this.Value as RSACryptoServiceProvider;
                if ( cryptoServiceProvider1 != null )
                {
                    using ( SHA1 shA1 = SHA1.Create() )
                        return cryptoServiceProvider1.SignData( data, ( object )shA1 );
                }
                else
                {
                    DSACryptoServiceProvider cryptoServiceProvider2 = this.Value as DSACryptoServiceProvider;
                    if ( cryptoServiceProvider2 != null )
                        return cryptoServiceProvider2.SignData( data );
                    throw new NotSupportedException();
                }
            }

            public bool VerifySignature( byte[ ] data, byte[ ] signature )
            {
                RSACryptoServiceProvider cryptoServiceProvider1 = this.Value as RSACryptoServiceProvider;
                if ( cryptoServiceProvider1 != null )
                {
                    using ( SHA1 shA1 = SHA1.Create() )
                        return cryptoServiceProvider1.VerifyData( data, ( object )shA1, signature );
                }
                else
                {
                    DSACryptoServiceProvider cryptoServiceProvider2 = this.Value as DSACryptoServiceProvider;
                    if ( cryptoServiceProvider2 != null )
                        return cryptoServiceProvider2.VerifySignature( data, signature );
                    throw new NotSupportedException();
                }
            }

            public override Wrapper<AsymmetricAlgorithm> Clone()
            {
                throw new NotSupportedException();
            }

            protected override void DisposeManaged()
            {
                this.Value.Clear();
                base.DisposeManaged();
            }
        }
    }
}
