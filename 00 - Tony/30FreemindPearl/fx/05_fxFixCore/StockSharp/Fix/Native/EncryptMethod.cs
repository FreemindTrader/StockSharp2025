namespace StockSharp.Fix.Native
{
    /// <summary>Encryption methods.</summary>
    public enum EncryptMethod
    {
        /// <summary>None.</summary>
        None,
        /// <summary>PKCS method.</summary>
        PKCS,
        /// <summary>DES method.</summary>
        DES,
        /// <summary>PKCS/DES method.</summary>
        PKCS_DES,
        /// <summary>PGP/DES method.</summary>
        PGP_DES,
        /// <summary>PGP/DES MD5 method.</summary>
        PGP_DES_MD5,
        /// <summary>PEM/DES MD5 method.</summary>
        PEM_DES_MD5,
    }
}
