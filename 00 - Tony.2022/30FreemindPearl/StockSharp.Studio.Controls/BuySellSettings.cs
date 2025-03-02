// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.BuySellSettings
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Studio.Controls
{
    public class BuySellSettings : NotifiableObject, IPersistable
    {
        private Decimal _volume = Decimal.One;
        private int _depth = 5;
        private bool _showCancelAll = true;
        private bool _showLimitOrderPanel = true;
        private bool _showMarketOrderPanel = true;
        private string _priceTextFormat = "0.00";
        private string _volumeTextFormat = "0";
        private bool _isLoading;
        private Security _security;
        private Portfolio _portfolio;
        private bool _showBoardColumn;
        private bool _isBidsOnTop;
        private Decimal _limitPrice;

        public event Action<string, object, object> ValueChanged;

        [Display( Description = "Str3192", Name = "Security", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public Security Security
        {
            get
            {
                return _security;
            }
            set
            {
                if ( _security == value )
                    return;
                Security security = _security;
                _security = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( Security ), security, value );
                NotifyChanged( nameof( Security ) );
                if ( _isLoading )
                    return;
                PriceTextFormat = value != null ? value.GetPriceTextFormat() : null;
                VolumeTextFormat = value != null ? value.GetVolumeTextFormat() : null;
            }
        }

        [Display( Description = "Str1997", Name = "Portfolio", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public Portfolio Portfolio
        {
            get
            {
                return _portfolio;
            }
            set
            {
                if ( _portfolio == value )
                    return;
                Portfolio portfolio = _portfolio;
                _portfolio = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( Portfolio ), portfolio, value );
                NotifyChanged( nameof( Portfolio ) );
            }
        }

        [Display( Description = "Str3194", Name = "Str3193", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public Decimal Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if ( _volume == value )
                    return;
                Decimal volume = _volume;
                _volume = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( Volume ), volume, value );
                NotifyChanged( nameof( Volume ) );
            }
        }

        [Display( Description = "Str3195", Name = "Str1660", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        public int Depth
        {
            get
            {
                return _depth;
            }
            set
            {
                if ( value < 1 )
                    throw new ArgumentOutOfRangeException();
                if ( _depth == value )
                    return;
                int depth = _depth;
                _depth = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( Depth ), depth, value );
                NotifyChanged( nameof( Depth ) );
            }
        }

        [Display( Description = "Str3196", Name = "CancelAll", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowCancelAll
        {
            get
            {
                return _showCancelAll;
            }
            set
            {
                if ( _showCancelAll == value )
                    return;
                bool showCancelAll = _showCancelAll;
                _showCancelAll = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( ShowCancelAll ), showCancelAll, value );
                NotifyChanged( nameof( ShowCancelAll ) );
            }
        }

        [Display( Description = "Str3198", Name = "Str3197", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowLimitOrderPanel
        {
            get
            {
                return _showLimitOrderPanel;
            }
            set
            {
                if ( _showLimitOrderPanel == value )
                    return;
                bool showLimitOrderPanel = _showLimitOrderPanel;
                _showLimitOrderPanel = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( ShowLimitOrderPanel ), showLimitOrderPanel, value );
                NotifyChanged( nameof( ShowLimitOrderPanel ) );
            }
        }

        [Display( Description = "Str3199", Name = "MarketOrders", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowMarketOrderPanel
        {
            get
            {
                return _showMarketOrderPanel;
            }
            set
            {
                if ( _showMarketOrderPanel == value )
                    return;
                bool marketOrderPanel = _showMarketOrderPanel;
                _showMarketOrderPanel = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( ShowMarketOrderPanel ), marketOrderPanel, value );
                NotifyChanged( nameof( ShowMarketOrderPanel ) );
            }
        }

        [Display( Description = "ShowBoardColumn", Name = "Board", Order = 7, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowBoardColumn
        {
            get
            {
                return _showBoardColumn;
            }
            set
            {
                if ( _showBoardColumn == value )
                    return;
                bool showBoardColumn = _showBoardColumn;
                _showBoardColumn = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( ShowBoardColumn ), showBoardColumn, value );
                NotifyChanged( nameof( ShowBoardColumn ) );
            }
        }

        [Display( Description = "BidsOnTopDesc", Name = "BidsOnTop", Order = 8, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsBidsOnTop
        {
            get
            {
                return _isBidsOnTop;
            }
            set
            {
                if ( _isBidsOnTop == value )
                    return;
                bool isBidsOnTop = _isBidsOnTop;
                _isBidsOnTop = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( IsBidsOnTop ), isBidsOnTop, value );
                NotifyChanged( nameof( IsBidsOnTop ) );
            }
        }

        [Display( Description = "PriceTextFormat", Name = "PriceStep", Order = 9, ResourceType = typeof( LocalizedStrings ) )]
        public string PriceTextFormat
        {
            get
            {
                return _priceTextFormat;
            }
            set
            {
                if ( _priceTextFormat == value )
                    return;
                string priceTextFormat = _priceTextFormat;
                _priceTextFormat = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( PriceTextFormat ), priceTextFormat, value );
                NotifyChanged( nameof( PriceTextFormat ) );
            }
        }

        [Display( Description = "VolumeTextFormat", Name = "VolumeStep", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
        public string VolumeTextFormat
        {
            get
            {
                return _volumeTextFormat;
            }
            set
            {
                if ( _volumeTextFormat == value )
                    return;
                string volumeTextFormat = _volumeTextFormat;
                _volumeTextFormat = value;
                Action<string, object, object> valueChanged = ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( VolumeTextFormat ), volumeTextFormat, value );
                NotifyChanged( nameof( VolumeTextFormat ) );
            }
        }

        [Browsable( false )]
        public Decimal LimitPrice
        {
            get
            {
                return _limitPrice;
            }
            set
            {
                if ( _limitPrice == value )
                    return;
                _limitPrice = value;
                NotifyChanged( nameof( LimitPrice ) );
            }
        }

        public void Load( SettingsStorage storage )
        {
            Volume = storage.GetValue( "Volume", 1 );
            ShowLimitOrderPanel = storage.GetValue( "ShowLimitOrderPanel", true );
            ShowMarketOrderPanel = storage.GetValue( "ShowMarketOrderPanel", true );
            ShowCancelAll = storage.GetValue( "ShowCancelAll", true );
            Depth = storage.GetValue( "Depth", 20 );
            ShowBoardColumn = storage.GetValue( "ShowBoardColumn", ShowBoardColumn );
            IsBidsOnTop = storage.GetValue( "IsBidsOnTop", IsBidsOnTop );
            PriceTextFormat = storage.GetValue( "PriceTextFormat", PriceTextFormat );
            VolumeTextFormat = storage.GetValue( "VolumeTextFormat", VolumeTextFormat );
            ISecurityProvider securityProvider = ServicesRegistry.SecurityProvider;
            string str1 = storage.GetValue<string>( "Security", null );
            if ( !str1.IsEmpty() )
            {
                _isLoading = true;
                try
                {
                    Security = securityProvider.LookupById( str1 );
                }
                finally
                {
                    _isLoading = false;
                }
            }
            IPortfolioProvider portfolioProvider = ServicesRegistry.PortfolioProvider;
            string str2 = storage.GetValue<string>( "Portfolio", null );
            if ( !str2.IsEmpty() )
                Portfolio = portfolioProvider.LookupByPortfolioName( str2 );
            LimitPrice = storage.GetValue( "LimitPrice", Decimal.Zero );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "Volume", Volume );
            storage.SetValue( "ShowLimitOrderPanel", ShowLimitOrderPanel );
            storage.SetValue( "ShowMarketOrderPanel", ShowMarketOrderPanel );
            storage.SetValue( "ShowCancelAll", ShowCancelAll );
            storage.SetValue( "Depth", Depth );
            storage.SetValue( "ShowBoardColumn", ShowBoardColumn );
            storage.SetValue( "IsBidsOnTop", IsBidsOnTop );
            storage.SetValue( "PriceTextFormat", PriceTextFormat );
            storage.SetValue( "VolumeTextFormat", VolumeTextFormat );
            storage.SetValue( "Security", Security?.Id );
            SettingsStorage settingsStorage = storage;
            Portfolio portfolio = Portfolio;
            string str = portfolio != null ? portfolio.GetUniqueId() : null;
            settingsStorage.SetValue( "Portfolio", str );
            storage.SetValue( "LimitPrice", LimitPrice );
        }
    }
}
