// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey
// Assembly: Ecng.Security, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC731BA6-0108-4E2D-8E5E-F8573AC505F7
// Assembly location: T:\00-FreemindTrader\packages\ecng.security\1.0.118\lib\netstandard2.0\Ecng.Security.dll

using Ecng.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
{
  public class ProtectedKey : Tuple<byte[], DataProtectionScope>
  {
    public static ProtectedKey CreateFromPlaintextKey(
      byte[] plaintextKey,
      DataProtectionScope dataProtectionScope)
    {
      return new ProtectedKey(plaintextKey.Protect((byte[]) null, dataProtectionScope), dataProtectionScope);
    }

    public static ProtectedKey CreateFromEncryptedKey(
      byte[] encryptedKey,
      DataProtectionScope dataProtectionScope)
    {
      return new ProtectedKey(encryptedKey, dataProtectionScope);
    }

    public byte[] EncryptedKey
    {
      get
      {
        return ((IEnumerable<byte>) this.Item1).ToArray<byte>();
      }
    }

    public byte[] DecryptedKey
    {
      get
      {
        return this.Unprotect();
      }
    }

    public virtual byte[] Unprotect()
    {
      return this.Item1.Unprotect((byte[]) null, this.Item2);
    }

    private ProtectedKey(byte[] protectedKey, DataProtectionScope protectionScope)
      : base(protectedKey, protectionScope)
    {
    }
  }
}
