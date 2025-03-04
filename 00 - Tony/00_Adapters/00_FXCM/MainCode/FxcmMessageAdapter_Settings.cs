namespace StockSharp.FxConnectFXCM
{
    using System;
    using System.Collections.Generic; 
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security;

    using Ecng.ComponentModel;
    using Ecng.Serialization;

    using StockSharp.Localization;
    using StockSharp.Messages;

    /// <summary>
    /// The message adapter for <see cref="BitStamp"/>.
    /// </summary>
    

    public partial class FxConnectFxcmMsgAdapter
    {
        [Display(
                ResourceType = typeof( LocalizedStrings ),
                Name = LocalizedStrings.ServerAddressKey,
                Description = LocalizedStrings.ServerAddressKey,
                GroupName = LocalizedStrings.ConnectionKey,
                Order = 0 )]
        public Uri Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        [Display(
                ResourceType = typeof( LocalizedStrings ),
                Name = LocalizedStrings.DemoKey,
                Description = LocalizedStrings.DemoKey,
                GroupName = LocalizedStrings.ConnectionKey,
                Order = 1 )]
        public bool IsDemo
        {
            get
            {
                return _isDemo;
            }
            set
            {
                _isDemo = value;
            }
        }

        [Display(
                ResourceType = typeof( LocalizedStrings ),
                Name = LocalizedStrings.LoginKey,
                Description = LocalizedStrings.LoginKey,
                GroupName = LocalizedStrings.ConnectionKey,
                Order = 2 )]
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
            }
        }

        [Display(
                ResourceType = typeof( LocalizedStrings ),
                Name = LocalizedStrings.PasswordKey,
                Description = LocalizedStrings.PasswordKey,
                GroupName = LocalizedStrings.ConnectionKey,
                Order = 3 )]
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        [Display(
                ResourceType = typeof( LocalizedStrings ),
                Name = LocalizedStrings.AccessTokenKey,
                Description = LocalizedStrings.AccessTokenKey,
                GroupName = LocalizedStrings.ConnectionKey,
                Order = 4 )]
        public SecureString Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }
        

        [Display( Description = LocalizedStrings.ProxyAddressKey, GroupName = LocalizedStrings.ConnectionKey, Name = LocalizedStrings.ProxyAddressKey, Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public string Proxy
        {
            get
            {
                return _proxyIP;
            }
            set
            {
                _proxyIP = value;
            }
        }

        [Display( Description = "Proxy Port", GroupName = LocalizedStrings.ConnectionKey, Name = "ProxyPort", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public int ProxyPort
        {
            get
            {
                return _proxyPort;
            }
            set
            {
                _proxyPort = value;
            }
        }

        /// <summary>
        /// Default value for <see cref="MessageAdapter.HeartbeatInterval"/>.
        /// </summary>
        public static readonly TimeSpan DefaultHeartbeatInterval = TimeSpan.FromSeconds(1);

        [Browsable( false )]
        public static IEnumerable<TimeSpan> AllTimeFrames
        {
            get
            {
                return fxcmPeriodString.Keys.ToArray( );
            }
        }

        protected override IEnumerable<TimeSpan> GetTimeFrames( SecurityId securityId, DateTimeOffset? from, DateTimeOffset? to )
        {
            return AllTimeFrames;
        }
        /// <inheritdoc />
        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );

            storage.SetValue( nameof( Address ), Address );
            storage.SetValue( nameof( IsDemo ), IsDemo );
            storage.SetValue( nameof( Login ), Login );
            storage.SetValue( nameof( Password ), Password );            
            storage.SetValue( nameof( Token ), Token );
            storage.SetValue( nameof( Proxy ), Proxy );
            storage.SetValue( nameof( ProxyPort ), ProxyPort );
        }

        /// <inheritdoc />
        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );

            Address   = storage.GetValue<Uri>( nameof( Address ) );
            IsDemo    = storage.GetValue<bool>( nameof( IsDemo ) );
            Login     = storage.GetValue<string>( nameof( Login ) );
            Password  = storage.GetValue<SecureString>( nameof( Password ) );            
            Token     = storage.GetValue<SecureString>( nameof( Token ) );
            Proxy     = storage.GetValue<string>( nameof( Proxy ), null );
            ProxyPort = storage.GetValue( nameof( ProxyPort ), 0 );
        }



        /// <inheritdoc />
        public override string ToString( )
        {
            return base.ToString( ) + ": " + LocalizedStrings.Login + " = " + Login;
        }
    }
}