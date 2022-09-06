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
                return this._security;
            }
            set
            {
                if ( this._security == value )
                    return;
                Security security = this._security;
                this._security = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( Security ), ( object )security, ( object )value );
                this.NotifyChanged( nameof( Security ) );
                if ( this._isLoading )
                    return;
                this.PriceTextFormat = value != null ? value.GetPriceTextFormat() : ( string )null;
                this.VolumeTextFormat = value != null ? value.GetVolumeTextFormat() : ( string )null;
            }
        }

        [Display( Description = "Str1997", Name = "Portfolio", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public Portfolio Portfolio
        {
            get
            {
                return this._portfolio;
            }
            set
            {
                if ( this._portfolio == value )
                    return;
                Portfolio portfolio = this._portfolio;
                this._portfolio = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( Portfolio ), ( object )portfolio, ( object )value );
                this.NotifyChanged( nameof( Portfolio ) );
            }
        }

        [Display( Description = "Str3194", Name = "Str3193", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public Decimal Volume
        {
            get
            {
                return this._volume;
            }
            set
            {
                if ( this._volume == value )
                    return;
                Decimal volume = this._volume;
                this._volume = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( Volume ), ( object )volume, ( object )value );
                this.NotifyChanged( nameof( Volume ) );
            }
        }

        [Display( Description = "Str3195", Name = "Str1660", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        public int Depth
        {
            get
            {
                return this._depth;
            }
            set
            {
                if ( value < 1 )
                    throw new ArgumentOutOfRangeException();
                if ( this._depth == value )
                    return;
                int depth = this._depth;
                this._depth = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( Depth ), ( object )depth, ( object )value );
                this.NotifyChanged( nameof( Depth ) );
            }
        }

        [Display( Description = "Str3196", Name = "CancelAll", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowCancelAll
        {
            get
            {
                return this._showCancelAll;
            }
            set
            {
                if ( this._showCancelAll == value )
                    return;
                bool showCancelAll = this._showCancelAll;
                this._showCancelAll = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( ShowCancelAll ), ( object )showCancelAll, ( object )value );
                this.NotifyChanged( nameof( ShowCancelAll ) );
            }
        }

        [Display( Description = "Str3198", Name = "Str3197", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowLimitOrderPanel
        {
            get
            {
                return this._showLimitOrderPanel;
            }
            set
            {
                if ( this._showLimitOrderPanel == value )
                    return;
                bool showLimitOrderPanel = this._showLimitOrderPanel;
                this._showLimitOrderPanel = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( ShowLimitOrderPanel ), ( object )showLimitOrderPanel, ( object )value );
                this.NotifyChanged( nameof( ShowLimitOrderPanel ) );
            }
        }

        [Display( Description = "Str3199", Name = "MarketOrders", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowMarketOrderPanel
        {
            get
            {
                return this._showMarketOrderPanel;
            }
            set
            {
                if ( this._showMarketOrderPanel == value )
                    return;
                bool marketOrderPanel = this._showMarketOrderPanel;
                this._showMarketOrderPanel = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( ShowMarketOrderPanel ), ( object )marketOrderPanel, ( object )value );
                this.NotifyChanged( nameof( ShowMarketOrderPanel ) );
            }
        }

        [Display( Description = "ShowBoardColumn", Name = "Board", Order = 7, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowBoardColumn
        {
            get
            {
                return this._showBoardColumn;
            }
            set
            {
                if ( this._showBoardColumn == value )
                    return;
                bool showBoardColumn = this._showBoardColumn;
                this._showBoardColumn = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( ShowBoardColumn ), ( object )showBoardColumn, ( object )value );
                this.NotifyChanged( nameof( ShowBoardColumn ) );
            }
        }

        [Display( Description = "BidsOnTopDesc", Name = "BidsOnTop", Order = 8, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsBidsOnTop
        {
            get
            {
                return this._isBidsOnTop;
            }
            set
            {
                if ( this._isBidsOnTop == value )
                    return;
                bool isBidsOnTop = this._isBidsOnTop;
                this._isBidsOnTop = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( IsBidsOnTop ), ( object )isBidsOnTop, ( object )value );
                this.NotifyChanged( nameof( IsBidsOnTop ) );
            }
        }

        [Display( Description = "PriceTextFormat", Name = "PriceStep", Order = 9, ResourceType = typeof( LocalizedStrings ) )]
        public string PriceTextFormat
        {
            get
            {
                return this._priceTextFormat;
            }
            set
            {
                if ( this._priceTextFormat == value )
                    return;
                string priceTextFormat = this._priceTextFormat;
                this._priceTextFormat = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( PriceTextFormat ), ( object )priceTextFormat, ( object )value );
                this.NotifyChanged( nameof( PriceTextFormat ) );
            }
        }

        [Display( Description = "VolumeTextFormat", Name = "VolumeStep", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
        public string VolumeTextFormat
        {
            get
            {
                return this._volumeTextFormat;
            }
            set
            {
                if ( this._volumeTextFormat == value )
                    return;
                string volumeTextFormat = this._volumeTextFormat;
                this._volumeTextFormat = value;
                Action<string, object, object> valueChanged = this.ValueChanged;
                if ( valueChanged != null )
                    valueChanged( nameof( VolumeTextFormat ), ( object )volumeTextFormat, ( object )value );
                this.NotifyChanged( nameof( VolumeTextFormat ) );
            }
        }

        [Browsable( false )]
        public Decimal LimitPrice
        {
            get
            {
                return this._limitPrice;
            }
            set
            {
                if ( this._limitPrice == value )
                    return;
                this._limitPrice = value;
                this.NotifyChanged( nameof( LimitPrice ) );
            }
        }

        public void Load( SettingsStorage storage )
        {
            this.Volume = ( Decimal )storage.GetValue<int>( "Volume", 1 );
            this.ShowLimitOrderPanel = storage.GetValue<bool>( "ShowLimitOrderPanel", true );
            this.ShowMarketOrderPanel = storage.GetValue<bool>( "ShowMarketOrderPanel", true );
            this.ShowCancelAll = storage.GetValue<bool>( "ShowCancelAll", true );
            this.Depth = storage.GetValue<int>( "Depth", 20 );
            this.ShowBoardColumn = storage.GetValue<bool>( "ShowBoardColumn", this.ShowBoardColumn );
            this.IsBidsOnTop = storage.GetValue<bool>( "IsBidsOnTop", this.IsBidsOnTop );
            this.PriceTextFormat = storage.GetValue<string>( "PriceTextFormat", this.PriceTextFormat );
            this.VolumeTextFormat = storage.GetValue<string>( "VolumeTextFormat", this.VolumeTextFormat );
            ISecurityProvider securityProvider = ServicesRegistry.SecurityProvider;
            string str1 = storage.GetValue<string>( "Security", ( string )null );
            if ( !str1.IsEmpty() )
            {
                this._isLoading = true;
                try
                {
                    this.Security = securityProvider.LookupById( str1 );
                }
                finally
                {
                    this._isLoading = false;
                }
            }
            IPortfolioProvider portfolioProvider = ServicesRegistry.PortfolioProvider;
            string str2 = storage.GetValue<string>( "Portfolio", ( string )null );
            if ( !str2.IsEmpty() )
                this.Portfolio = portfolioProvider.LookupByPortfolioName( str2 );
            this.LimitPrice = storage.GetValue<Decimal>( "LimitPrice", Decimal.Zero );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<Decimal>( "Volume", this.Volume );
            storage.SetValue<bool>( "ShowLimitOrderPanel", this.ShowLimitOrderPanel );
            storage.SetValue<bool>( "ShowMarketOrderPanel", this.ShowMarketOrderPanel );
            storage.SetValue<bool>( "ShowCancelAll", this.ShowCancelAll );
            storage.SetValue<int>( "Depth", this.Depth );
            storage.SetValue<bool>( "ShowBoardColumn", this.ShowBoardColumn );
            storage.SetValue<bool>( "IsBidsOnTop", this.IsBidsOnTop );
            storage.SetValue<string>( "PriceTextFormat", this.PriceTextFormat );
            storage.SetValue<string>( "VolumeTextFormat", this.VolumeTextFormat );
            storage.SetValue<string>( "Security", this.Security?.Id );
            SettingsStorage settingsStorage = storage;
            Portfolio portfolio = this.Portfolio;
            string str = portfolio != null ? portfolio.GetUniqueId() : ( string )null;
            settingsStorage.SetValue<string>( "Portfolio", str );
            storage.SetValue<Decimal>( "LimitPrice", this.LimitPrice );
        }
    }
}
