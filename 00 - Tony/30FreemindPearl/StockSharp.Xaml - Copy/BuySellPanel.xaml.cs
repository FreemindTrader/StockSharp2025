using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for BuySellPanel.xaml
    /// </summary>
    public partial class BuySellPanel : UserControl, IPersistable
    {
        public event Action<Security, Portfolio, Sides, Decimal, Decimal> OrderRegistering;

        public event Action<BuySellPanel> Closed;

        public event Action<Security, Security> SecurityChanged;

        public event Action<Portfolio, Portfolio> PortfolioChanged;

        private sealed class BuySellSettings : NotifiableObject
        {
            private Decimal? _bestBidPrice;
            private Decimal? _bestAskPrice;
            private Decimal? _spreadSize;
            private Decimal? _lastTradePrice;
            private Decimal? _lowPrice;
            private Decimal? _highPrice;

            public BuySellSettings( )
            {                
            }

            public Decimal? BestBidPrice
            {
                get
                {
                    return _bestBidPrice;
                }
                set
                {
                    _bestBidPrice = value;
                    NotifyChanged( nameof( BestBidPrice ) );
                }
            }

            public Decimal? BestAskPrice
            {
                get
                {
                    return _bestAskPrice;
                }
                set
                {
                    _bestAskPrice = value;
                    NotifyChanged( nameof( BestAskPrice ) );
                }
            }

            public Decimal? SpreadSize
            {
                get
                {
                    return _spreadSize;
                }
                set
                {
                    _spreadSize = value;
                    NotifyChanged( nameof( SpreadSize ) );
                }
            }

            public Decimal? LastTradePrice
            {
                get
                {
                    return _lastTradePrice;
                }
                set
                {
                    _lastTradePrice = value;
                    NotifyChanged( nameof( LastTradePrice ) );
                }
            }

            public Decimal? LowPrice
            {
                get
                {
                    return _lowPrice;
                }
                set
                {
                    _lowPrice = value;
                    NotifyChanged( nameof( LowPrice ) );
                }
            }

            public Decimal? HighPrice
            {
                get
                {
                    return _highPrice;
                }
                set
                {
                    _highPrice = value;
                    NotifyChanged( nameof( HighPrice ) );
                }
            }
        }
        private readonly BuySellSettings _settings;
        private Security _security;
        private Portfolio _portfolio;
        private IMarketDataProvider _imarketDataProvider;
        public BuySellPanel()
        {
            InitializeComponent();
            _settings = new BuySellPanel.BuySellSettings( );
            DataContext = _settings;
            InitializeVolume( );

        }

        private void InitializeVolume( )
        {            
            Decimal volumeStep = Security?.VolumeStep ?? Decimal.One;
            VolumeCtrl.Increment = ( volumeStep );            
            VolumeCtrl.MinValue = ( volumeStep );
            

            if ( ! Volume.HasValue )
            {
                Volume = VolumeCtrl.MinValue;
            }
            else if (  Volume.Value < VolumeCtrl.MinValue.Value )
            {                                                 
                Volume = VolumeCtrl.MinValue;
            }
        }

        public Security Security
        {
            get
            {
                return _security;
            }
            set
            {
                SecurityCtrl.SelectedSecurity = value;
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return SecurityCtrl.SecurityProvider;
            }
            set
            {
                SecurityCtrl.SecurityProvider = value;
            }
        }

        public Portfolio Portfolio
        {
            get
            {
                return _portfolio;
            }
            set
            {
                PortfolioCtrl.SelectedPortfolio = value;
            }
        }

        public Decimal? Volume
        {
            get
            {
                return ( Decimal? ) VolumeCtrl.EditValue;
            }
            set
            {
                VolumeCtrl.EditValue = value;
            }
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return PortfolioCtrl.Portfolios;
            }
            set
            {
                PortfolioCtrl.Portfolios = value;
            }
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return _imarketDataProvider;
            }
            set
            {
                if ( value == _imarketDataProvider )
                    return;

                if ( _imarketDataProvider != null )
                {
                    _imarketDataProvider.ValuesChanged -= _imarketDataProvider_ValuesChanged;
                }
                    
                _imarketDataProvider = value;

                if ( _imarketDataProvider == null )
                {
                    return;
                }
                    
                UpdateSettings( );

                _imarketDataProvider.ValuesChanged += _imarketDataProvider_ValuesChanged;
            }
        }

        private void UpdateSettings( )
        {
            if ( Security == null )
            {
                _settings.BestBidPrice = null;
                _settings.BestAskPrice = null;
                _settings.LastTradePrice = null;
                _settings.LowPrice = null;
                _settings.HighPrice = null;
            }
            else
            {
                _settings.BestBidPrice = ( Decimal? ) MarketDataProvider.GetSecurityValue( Security, Level1Fields.BestBidPrice );
                _settings.BestAskPrice = ( Decimal? ) MarketDataProvider.GetSecurityValue( Security, Level1Fields.BestAskPrice );
                _settings.LastTradePrice = ( Decimal? ) MarketDataProvider.GetSecurityValue( Security, Level1Fields.LastTradePrice );
                _settings.LowPrice = ( Decimal? ) MarketDataProvider.GetSecurityValue( Security, Level1Fields.LowPrice );
                _settings.HighPrice = ( Decimal? ) MarketDataProvider.GetSecurityValue( Security, Level1Fields.HighPrice );
            }

            UpdateSpreadSize( );
        }

        private void _imarketDataProvider_ValuesChanged( Security security, IEnumerable<KeyValuePair<Level1Fields, object>> changedValues, DateTimeOffset dateTimeOffset_0, DateTimeOffset dateTimeOffset_1 )
        {
            if ( Security != security )
                return;
            using ( IEnumerator<KeyValuePair<Level1Fields, object>> enumerator = changedValues.GetEnumerator( ) )
            {
                while ( ( ( IEnumerator ) enumerator ).MoveNext( ) )
                {
                    KeyValuePair<Level1Fields, object> current = enumerator.Current;
                    Level1Fields key = current.Key;
                    if ( key <= Level1Fields.LowPrice )
                    {
                        if ( key != Level1Fields.HighPrice )
                        {
                            if ( key == Level1Fields.LowPrice )
                            {
                                _settings.LowPrice = new Decimal?( ( Decimal ) current.Value );
                            }
                                
                        }
                        else
                            _settings.HighPrice = new Decimal?( ( Decimal ) current.Value );
                    }
                    else if ( key != Level1Fields.LastTradePrice )
                    {
                        if ( key != Level1Fields.BestBidPrice )
                        {
                            if ( key == Level1Fields.BestAskPrice )
                            {
                                _settings.BestAskPrice = new Decimal?( ( Decimal ) current.Value );
                                UpdateSpreadSize( );
                            }
                        }
                        else
                        {
                            _settings.BestBidPrice = new Decimal?( ( Decimal ) current.Value );
                            UpdateSpreadSize( );
                        }
                    }
                    else
                        _settings.LastTradePrice = new Decimal?( ( Decimal ) current.Value );
                }
            }
        }

        private void UpdateSpreadSize( )
        {
            if ( _settings.BestBidPrice.HasValue && _settings.BestAskPrice.HasValue )
            {
                var settings = _settings;
                Decimal? bestAskPrice = _settings.BestAskPrice;
                Decimal? bestBidPrice = _settings.BestBidPrice;
                Decimal? spread = bestAskPrice.HasValue & bestBidPrice.HasValue ? new Decimal?(bestAskPrice.GetValueOrDefault() - bestBidPrice.GetValueOrDefault()) : new Decimal?();
                settings.SpreadSize = spread;
            }
            else
                _settings.SpreadSize = new Decimal?( );
        }

        public void Load( SettingsStorage storage )
        {
            if (  storage.ContainsKey( "Security" ) )
            {
                var securityProvider = SecurityProvider;
                Security = SecurityProvider != null ? SecurityProvider.LookupById( ( string ) storage.GetValue<string>( "Security"  ) ) : ( Security ) null;
            }

            if ( storage.ContainsKey( "Portfolio" ) )
            {                
                var portfolioStr = storage.GetValue<string>( "Portfolio"  );
                PortfolioDataSource portfolios = this.Portfolios;

                Portfolio = ( Portfolio ) ( portfolios != null ? portfolios.Items.FirstOrDefault( x => x.Name.CompareIgnoreCase( portfolioStr ) ) : null );
            }

            Volume = ( Decimal? ) storage.GetValue<Decimal?>( "Volume" );
        }

        public void Save( SettingsStorage storage )
        {
            if ( Security != null )
                storage.SetValue<string>( "Security", Security.Id );

            if ( Portfolio != null )
                storage.SetValue<string>( "Portfolio", Portfolio.Name );

            storage.SetValue<Decimal?>( "Volume", Volume );
        }

        private void VolumeCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {                        
            bool isEnable = Volume.HasValue && ( Volume.GetValueOrDefault() > 0 ) &&  Security != null && Portfolio != null;
            Sell.IsEnabled = isEnable;            
            Buy.IsEnabled = isEnable;
        }

        private void Buy_Click( object sender, RoutedEventArgs e )
        {
            var bestAskPrice = _settings.BestAskPrice;
            if ( !bestAskPrice.HasValue )
                return;

            OrderRegistering?.Invoke( Security, Portfolio, Sides.Buy, bestAskPrice.Value, Volume ?? Decimal.One );
            
        }

        private void Sell_Click( object sender, RoutedEventArgs e )
        {
            var bestBidPrice = _settings.BestBidPrice;
            if ( !bestBidPrice.HasValue )
                return;

            OrderRegistering?.Invoke( Security, Portfolio, Sides.Sell, bestBidPrice.Value, Volume ?? Decimal.One );
        }

        private void CloseBtn_Click( object sender, RoutedEventArgs e )
        {
            Closed?.Invoke( this );
        }

        private void SecurityCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            var oldValue = _security;
            _security = SecurityCtrl.SelectedSecurity;
            VolumeCtrl_EditValueChanged( null, null );
            UpdateSettings( );
            InitializeVolume( );

            SecurityChanged?.Invoke( oldValue, _security );
        }

        private void PortfolioCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Portfolio oldValue = this._portfolio;
            this._portfolio = this.PortfolioCtrl.SelectedPortfolio;
            VolumeCtrl_EditValueChanged( null, null );
            
            PortfolioChanged?.Invoke( oldValue, this._portfolio );
        }
    }
}
