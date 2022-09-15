using Ecng.Common;
using Ecng.Security.Properties;
using System;
using System.Security.Cryptography;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
{
    public class HashCryptographer
    {
        private readonly Type algorithmType;
        private readonly ProtectedKey key;

        public HashCryptographer( Type algorithmType )
        {
            if ( ( object )algorithmType == null )
                throw new ArgumentNullException( nameof( algorithmType ) );
            if ( !algorithmType.Is<HashAlgorithm>() )
                throw new ArgumentException( Resources.ExceptionCreatingHashAlgorithmInstance, nameof( algorithmType ) );
            this.algorithmType = algorithmType;
        }

        public HashCryptographer( Type algorithmType, ProtectedKey protectedKey )
          : this( algorithmType )
        {
            this.key = protectedKey;
        }

        public byte[ ] ComputeHash( byte[ ] plaintext )
        {
            using ( HashAlgorithm hashAlgorithm = this.GetHashAlgorithm() )
                return hashAlgorithm.ComputeHash( plaintext );
        }

        private HashAlgorithm GetHashAlgorithm()
        {
            HashAlgorithm instance = Activator.CreateInstance( this.algorithmType, true ) as HashAlgorithm;
            KeyedHashAlgorithm keyedHashAlgorithm = instance as KeyedHashAlgorithm;
            if ( keyedHashAlgorithm == null || this.key == null )
                return instance;
            keyedHashAlgorithm.Key = this.key.DecryptedKey;
            return instance;
        }
    }
}
