using Ecng.Common;
using System;
using System.Security.Cryptography;

namespace Ecng.Security.Cryptographers
{
    public class HashCryptographer : Disposable
    {
        private readonly HashAlgorithm _algorithm;

        public HashCryptographer( HashAlgorithm algorithm, byte[ ] key = null )
        {
            HashAlgorithm hashAlgorithm = algorithm;
            if ( hashAlgorithm == null )
                throw new ArgumentNullException( nameof( algorithm ) );
            this._algorithm = hashAlgorithm;
            KeyedHashAlgorithm keyedHashAlgorithm = algorithm as KeyedHashAlgorithm;
            if ( keyedHashAlgorithm == null )
                return;
            keyedHashAlgorithm.Key = key;
        }

        protected override void DisposeManaged()
        {
            if ( this.IsDisposed )
                return;
            this._algorithm.Dispose();
        }

        public byte[ ] ComputeHash( byte[ ] plaintext )
        {
            return this._algorithm.ComputeHash( plaintext );
        }
    }
}
