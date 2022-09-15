using Ecng.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
{
    public class ProtectedKey : Tuple<byte[ ], DataProtectionScope>
    {
        public static ProtectedKey CreateFromPlaintextKey( byte[ ] plaintextKey, DataProtectionScope dataProtectionScope )
        {
            return new ProtectedKey( plaintextKey.Protect( ( byte[ ] )null, dataProtectionScope ), dataProtectionScope );
        }

        public static ProtectedKey CreateFromEncryptedKey(
          byte[ ] encryptedKey,
          DataProtectionScope dataProtectionScope )
        {
            return new ProtectedKey( encryptedKey, dataProtectionScope );
        }

        public byte[ ] EncryptedKey
        {
            get
            {
                return ( ( IEnumerable<byte> )this.Item1 ).ToArray<byte>();
            }
        }

        public byte[ ] DecryptedKey
        {
            get
            {
                return this.Unprotect();
            }
        }

        public virtual byte[ ] Unprotect()
        {
            return this.Item1.Unprotect( ( byte[ ] )null, this.Item2 );
        }

        private ProtectedKey( byte[ ] protectedKey, DataProtectionScope protectionScope )
          : base( protectedKey, protectionScope )
        {
        }
    }
}
