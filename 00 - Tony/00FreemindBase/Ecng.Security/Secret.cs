// Decompiled with JetBrains decompiler
// Type: Ecng.Security.Secret
// Assembly: Ecng.Security, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E35A157-8791-4E76-9265-8307D4E0C0A6
// Assembly location: T:\00-FreemindTrader\packages\ecng.security\1.0.119\lib\net6.0\Ecng.Security.dll

using Ecng.Collections;
using Ecng.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Ecng.Security
{
    public class Secret : Equatable<Secret>
    {
        public const int DefaultSaltSize = 128;
        private int _hashCode;

        public Secret()
        {
            this.Algo = Secret.CreateDefaultAlgo();
        }

        public Secret( byte[ ] passwordBytes, byte[ ] salt, CryptoAlgorithm algo = null )
        {
            byte[ ] numArray1 = salt;
            if ( numArray1 == null )
                throw new ArgumentNullException( nameof( salt ) );
            this.Salt = numArray1;
            this.Algo = algo ?? Secret.CreateDefaultAlgo();
            byte[ ] numArray2 = passwordBytes;
            if ( numArray2 == null )
                throw new ArgumentNullException( nameof( passwordBytes ) );
            this.Hash = numArray2;
            this.Hash = this.Algo.Encrypt( this.Hash );
        }

        private static CryptoAlgorithm CreateDefaultAlgo()
        {
            return CryptoAlgorithm.Create( AlgorithmTypes.Hash );
        }

        public byte[ ] Salt { get; set; }

        public byte[ ] Hash { get; set; }

        public CryptoAlgorithm Algo { get; }

        protected override bool OnEquals( Secret other )
        {
            if ( this.EnsureGetHashCode() != other.EnsureGetHashCode() )
                return false;
            if ( this.Hash == null )
            {
                if ( other.Hash != null )
                    return false;
            }
            else if ( other.Hash == null || !( ( IEnumerable<byte> )this.Hash ).SequenceEqual<byte>( ( IEnumerable<byte> )other.Hash ) )
                return false;
            if ( this.Salt == null )
            {
                if ( other.Salt != null )
                    return false;
            }
            else if ( other.Salt == null || !( ( IEnumerable<byte> )this.Salt ).SequenceEqual<byte>( ( IEnumerable<byte> )other.Salt ) )
                return false;
            return true;
        }

        private int EnsureGetHashCode()
        {
            if ( this._hashCode == 0 )
            {
                byte[ ] hash = this.Hash;
                int num1 = hash != null ? ( ( IEnumerable<byte> )hash ).GetHashCodeEx<byte>() : 0;
                byte[ ] salt = this.Salt;
                int num2 = salt != null ? ( ( IEnumerable<byte> )salt ).GetHashCodeEx<byte>() : 0;
                this._hashCode = num1 ^ num2;
            }
            return this._hashCode;
        }

        public override int GetHashCode()
        {
            return this.EnsureGetHashCode();
        }

        public override Secret Clone()
        {
            Secret secret = new Secret();
            byte[ ] hash = this.Hash;
            secret.Hash = hash != null ? ( ( IEnumerable<byte> )hash ).ToArray<byte>() : ( byte[ ] )null;
            byte[ ] salt = this.Salt;
            secret.Salt = salt != null ? ( ( IEnumerable<byte> )salt ).ToArray<byte>() : ( byte[ ] )null;
            return secret;
        }
    }
}
