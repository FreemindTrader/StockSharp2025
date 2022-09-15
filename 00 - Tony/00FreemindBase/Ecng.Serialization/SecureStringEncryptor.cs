
using Ecng.Common;
using Ecng.Security;
using System;
using System.Security;
using System.Security.Cryptography;

namespace Ecng.Serialization
{
    internal class SecureStringEncryptor
    {
        private static readonly Lazy<SecureStringEncryptor> _instance = new Lazy<SecureStringEncryptor>( ( Func<SecureStringEncryptor> )( () => new SecureStringEncryptor() ) );
        private readonly SecureString _key = "RClVEDn0O3EUsKqym1qd".Secure();
        private readonly byte[ ] _salt = "3hj67-!3".To<byte[ ]>();
        //private readonly DpapiCryptographer _dpapi;

        public static SecureStringEncryptor Instance
        {
            get
            {
                return SecureStringEncryptor._instance.Value;
            }
        }

        private SecureStringEncryptor()
        {
            try
            {
                //this._dpapi = new DpapiCryptographer( DataProtectionScope.CurrentUser );
            }
            catch
            {
            }
        }

        public SecureString Key { get; set; }

        public byte[ ] Entropy { get; set; }

        public SecureString Decrypt( byte[ ] source )
        {
            if ( source == null )
                return ( SecureString )null;
            try
            {
                Scope<ContinueOnExceptionContext> current = Scope<ContinueOnExceptionContext>.Current;
                if ( ( current != null ? ( !current.Value.DoNotEncrypt ? 1 : 0 ) : 1 ) != 0 )
                {
                    try
                    {
                        source = source.DecryptAes( this._key.UnSecure(), this._salt, this._salt );
                    }
                    catch ( CryptographicException ex1 )
                    {
                        if ( ex1 == null )
                        {

                        }
                        //if ( this._dpapi == null )
                        //{
                        //    throw;
                        //}
                        //else
                        //{
                        //    try
                        //    {
                        //        source = this._dpapi.Decrypt( source, this.Entropy );
                        //    }
                        //    catch ( CryptographicException ex2 )
                        //    {
                        //        throw ex1;
                        //    }
                        //    catch ( PlatformNotSupportedException ex2 )
                        //    {
                        //        throw ex1;
                        //    }
                        //}
                    }
                }
                return source.To<string>().Secure();
            }
            catch ( CryptographicException ex )
            {
                if ( ContinueOnExceptionContext.TryProcess( ( Exception )ex ) )
                    return ( SecureString )null;
                throw;
            }
        }

        public byte[ ] Encrypt( SecureString instance )
        {
            if ( instance == null )
                return ( byte[ ] )null;
            byte[ ] plain = instance.UnSecure().To<byte[ ]>();
            Scope<ContinueOnExceptionContext> current = Scope<ContinueOnExceptionContext>.Current;
            if ( ( current != null ? ( current.Value.DoNotEncrypt ? 1 : 0 ) : 0 ) != 0 )
                return plain;
            return plain.EncryptAes( this._key.UnSecure(), this._salt, this._salt );
        }
    }
}
