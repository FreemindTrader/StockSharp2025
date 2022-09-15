using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Ecng.Security.Cryptographers
{
    public class X509Cryptographer : AsymmetricCryptographer
    {
        public X509Cryptographer( X509Certificate2 certificate )
          : base( ( AsymmetricAlgorithm )certificate.GetRSAPublicKey(), ( AsymmetricAlgorithm )certificate.GetRSAPrivateKey() )
        {
        }
    }
}
