// Decompiled with JetBrains decompiler
// Type: Ecng.Security.DpapiCryptographer
// Assembly: Ecng.Security, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC731BA6-0108-4E2D-8E5E-F8573AC505F7
// Assembly location: T:\00-FreemindTrader\packages\ecng.security\1.0.118\lib\netstandard2.0\Ecng.Security.dll

using System.Security.Cryptography;

namespace Ecng.Security
{
  public class DpapiCryptographer
  {
    public DpapiCryptographer(DataProtectionScope scope)
    {
      this.Scope = scope;
    }

    public DataProtectionScope Scope { get; }

    public byte[] Encrypt(byte[] plainText)
    {
      return this.Encrypt(plainText, (byte[]) null);
    }

    public byte[] Encrypt(byte[] plainText, byte[] entropy)
    {
      return plainText.Protect(entropy, this.Scope);
    }

    public byte[] Decrypt(byte[] cipherText)
    {
      return this.Decrypt(cipherText, (byte[]) null);
    }

    public byte[] Decrypt(byte[] cipherText, byte[] entropy)
    {
      return cipherText.Unprotect(entropy, this.Scope);
    }
  }
}
