// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.ActivationRequest
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Reflection;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    public class ActivationRequest
    {
        public Guid SerialKey
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string MachineId
        {
            get; set;
        }

        public bool Activate
        {
            get; set;
        }

        public ActivationRequest()
        {
        }

        public ActivationRequest( string serialKey, string username, string password, string machineId )
          : this( username, password, machineId )
        {
            this.NotNullOrEmpty( serialKey, nameof( serialKey ) );
            Guid result;
            if ( !Guid.TryParse( serialKey, out result ) )
                throw new ArgumentException( "SerialKey is invalid" );
            this.SerialKey = result;
        }

        public ActivationRequest( Guid serialKey, string username, string password, string machineId )
          : this( username, password, machineId )
        {
            this.SerialKey = serialKey;
        }

        private ActivationRequest( string username, string password, string machineId )
        {
            this.NotNullOrEmpty( username, nameof( username ) );
            this.UserName = username;
            this.Password = SymmetricEncryption.Encrypt( password );
            this.NotNullOrEmpty( machineId, nameof( machineId ) );
            this.MachineId = machineId;
            this.Activate = true;
        }

        public string GetPassword()
        {
            try
            {
                return SymmetricEncryption.Decrypt( this.Password );
            }
            catch
            {
                return this.Password;
            }
        }

        private void NotNullOrEmpty( string argument, string argName )
        {
            if ( string.IsNullOrEmpty( argument ) )
                throw new ArgumentNullException( argName );
        }
    }
}
