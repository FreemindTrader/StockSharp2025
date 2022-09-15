// Decompiled with JetBrains decompiler
// Type: Ecng.Security.X509Cryptographer
// Assembly: Ecng.Security, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC731BA6-0108-4E2D-8E5E-F8573AC505F7
// Assembly location: T:\00-FreemindTrader\packages\ecng.security\1.0.118\lib\netstandard2.0\Ecng.Security.dll

using System.Security.Cryptography.X509Certificates;

namespace Ecng.Security
{
  public class X509Cryptographer : AsymmetricCryptographer
  {
    public X509Cryptographer(X509Certificate2 certificate)
      : base(certificate.PublicKey.Key, certificate.PrivateKey)
    {
    }
  }
}
