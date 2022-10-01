// Decompiled with JetBrains decompiler
// Type: StockSharp.Server.Core.PermissionCredentialsStorage
// Assembly: StockSharp.Server.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04FB8C8D-F403-4011-81A5-E3B253C8DBED
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Server.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// Storage for <see cref="T:StockSharp.Server.Core.PermissionCredentials" />.
    /// </summary>
    public class PermissionCredentialsStorage
    {
        
        private readonly CachedSynchronizedDictionary<string, PermissionCredentials> _credentials = new CachedSynchronizedDictionary<string, PermissionCredentials>( StringComparer.InvariantCultureIgnoreCase );
        
        private readonly string _credentialPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Server.Core.PermissionCredentialsStorage" />.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public PermissionCredentialsStorage( string fileName )
        {
            if ( fileName.IsEmpty() )
                throw new ArgumentNullException( nameof( fileName ) );
            if ( !Directory.Exists( Path.GetDirectoryName( fileName ) ) )
                throw new InvalidOperationException( LocalizedStrings.Str2866Params.Put( new object[1] { fileName } ) );
            _credentialPath = fileName;
        }

        /// <summary>Credentials.</summary>
        public PermissionCredentials[ ] Credentials
        {
            get
            {
                return _credentials.CachedValues;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );

                lock ( _credentials.SyncRoot )
                {
                    _credentials.Clear();
                    foreach ( PermissionCredentials permissionCredentials in value )
                        _credentials.Add( permissionCredentials.Email, permissionCredentials );
                }
            }
        }

        /// <summary>Get credentials by login.</summary>
        /// <param name="login">Login.</param>
        /// <returns>Credentials with set of permissions.</returns>
        public virtual PermissionCredentials TryGetByLogin( string login )
        {
            return _credentials.TryGetValue( login );
        }

        /// <summary>Add new credentials.</summary>
        /// <param name="credentials">Credentials with set of permissions.</param>
        public void Add( PermissionCredentials credentials )
        {
            if ( credentials == null )
                throw new ArgumentNullException( nameof( credentials ) );
            _credentials.Add( credentials.Email, credentials );
        }

        /// <summary>Delete credentials by login.</summary>
        /// <param name="login">Login.</param>
        /// <returns>Operation result.</returns>
        public bool DeleteByLogin( string login )
        {
            return _credentials.Remove( login );
        }

        /// <summary>Load credentials from file.</summary>
        public void LoadCredentials()
        {
            try
            {
                if ( !_credentialPath.IsConfigExists() )
                    return;
                Do.Invariant( new Action( LoadCredentialsAction ) );
            }
            catch ( Exception ex )
            {
                string format = "Load credentials error: {0}";
                ex.LogError( format );
            }
        }

        /// <summary>Save credentials to file.</summary>
        public void SaveCredentials()
        {
            try
            {
                Do.Invariant( new Action( SaveCredentialsAction ) );
            }
            catch ( Exception ex )
            {
                string format = "Save credentials error: {0}";
                ex.LogError( format );
            }
        }

        private void LoadCredentialsAction()
        {
            SettingsStorage[ ] settingsStorageArray = _credentialPath.Deserialize<SettingsStorage[ ]>();
            if ( settingsStorageArray == null )
                return;
            ContinueOnExceptionContext exceptionContext = new ContinueOnExceptionContext();
            exceptionContext.Error += new Action<Exception>( x => x.LogError( ) );
            using ( exceptionContext.ToScope( true ) )
                Credentials = settingsStorageArray.Select( x => x.Load<PermissionCredentials>() ).ToArray();
        }

        private void SaveCredentialsAction()
        {
            Credentials.Select( x => x.Save() ).ToArray().Serialize( _credentialPath );
        }        
    }
}
