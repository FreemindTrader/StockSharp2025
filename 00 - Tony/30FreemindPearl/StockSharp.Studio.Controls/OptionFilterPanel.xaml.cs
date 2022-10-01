// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OptionFilterPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class OptionFilterPanel : UserControl, IPersistable, IDisposable, IComponentConnector
    {
        private readonly CachedSynchronizedSet<Security> _options = new CachedSynchronizedSet<Security>();
        private readonly Unit _maxStrikeOffset = 20.Percents();
        private Decimal? _minAssetPrice;
        private Decimal? _maxAssetPrice;
        private DateTimeOffset? _maxDate;
        private DateTimeOffset? _minDate;
        private bool _changeFromCode;
        private bool _fromModifier;
        private ISecurityProvider _securityProvider;
        private IMarketDataProvider _marketDataProvider;
        private Security _underlyingAsset;
        private DateTime? _expiryDate;
        private Decimal? _minStrike;
        private Decimal? _maxStrike;


        public OptionFilterPanel()
        {
            InitializeComponent();
            UpdateSelectOptionsTitle();
        }

        public SubscriptionManager SubscriptionManager { get; set; }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return _securityProvider;
            }
            set
            {
                _securityProvider = value;
                UnderlyingAssetCtrl.SecurityProvider = value;
            }
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return _marketDataProvider;
            }
            set
            {
                if ( value == _marketDataProvider )
                    return;
                if ( _marketDataProvider != null )
                    _marketDataProvider.ValuesChanged -= new Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset>( MarketDataProvider_OnValuesChanged );
                _marketDataProvider = value;
                if ( _marketDataProvider == null )
                    return;
                _marketDataProvider.ValuesChanged += new Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset>( MarketDataProvider_OnValuesChanged );
            }
        }

        public DateTime? CurrentDate
        {
            get
            {
                return ( DateTime? )CurrentDateCtrl.EditValue;
            }
            private set
            {
                CurrentDateCtrl.EditValue = value;
            }
        }

        public Decimal? AssetPrice
        {
            get
            {
                return ( Decimal? )AssetPriceCtrl.EditValue;
            }
            private set
            {
                AssetPriceCtrl.EditValue = value;
            }
        }

        public bool UseBlackModel
        {
            get
            {
                bool? isChecked = UseBlackModelCtrl.IsChecked;
                bool flag = true;
                return isChecked.GetValueOrDefault() == flag & isChecked.HasValue;
            }
            private set
            {
                UseBlackModelCtrl.IsChecked = new bool?( value );
            }
        }

        public Security UnderlyingAsset
        {
            get
            {
                return _underlyingAsset;
            }
            private set
            {
                UnderlyingAssetCtrl.SelectedSecurity = value;
            }
        }

        public DateTime? ExpiryDate
        {
            get
            {
                return _expiryDate;
            }
            private set
            {
                ExpiryDateComboCtrl.SelectedItem = value;
            }
        }

        public Decimal? MinStrike
        {
            get
            {
                return _minStrike;
            }
            private set
            {
                MinStrikeCtrl.EditValue = value;
            }
        }

        public Decimal? MaxStrike
        {
            get
            {
                return _maxStrike;
            }
            private set
            {
                MaxStrikeCtrl.EditValue = value;
            }
        }

        public Security[ ] Options
        {
            get
            {
                return _options.Cache.Where( o =>
                      {
                          if ( ExpiryDate.HasValue )
                          {
                              DateTimeOffset? expiryDate = o.ExpiryDate;
                              ref DateTimeOffset? local = ref expiryDate;
                              DateTime? nullable = local.HasValue ? new DateTime?( local.GetValueOrDefault().Date ) : new DateTime?();
                              DateTime date = ExpiryDate.Value.Date;
                              if ( ( nullable.HasValue ? ( nullable.HasValue ? ( nullable.GetValueOrDefault() != date ? 1 : 0 ) : 0 ) : 1 ) != 0 )
                                  return false;
                          }
                          Decimal? nullable1;
                          if ( MinStrike.HasValue )
                          {
                              Decimal? strike = o.Strike;
                              nullable1 = MinStrike;
                              if ( strike.GetValueOrDefault() < nullable1.GetValueOrDefault() & ( strike.HasValue & nullable1.HasValue ) )
                                  return false;
                          }
                          nullable1 = MaxStrike;
                          if ( nullable1.HasValue )
                          {
                              nullable1 = o.Strike;
                              Decimal? maxStrike = MaxStrike;
                              if ( nullable1.GetValueOrDefault() > maxStrike.GetValueOrDefault() & ( nullable1.HasValue & maxStrike.HasValue ) )
                                  return false;
                          }
                          return true;
                      } ).ToArray();
            }
        }

        public event Action UnderlyingAssetChanged;

        public event Action FilterChanged;

        public event Action OptionsChanged;

        public event Action UseBlackModelChanged;

        public event Action<bool, Security, IEnumerable<KeyValuePair<Level1Fields, object>>> SecurityChanged;

        private void SubscribeMarketData( Security option )
        {
            SubscriptionManager?.CreateSubscription( option, DataType.Level1, null );
        }

        private void UnSubscribeMarketData( Security option )
        {
            SubscriptionManager?.RemoveSubscriptions( option );
        }

        private void MarketDataProvider_OnValuesChanged(
          Security security,
          IEnumerable<KeyValuePair<Level1Fields, object>> values,
          DateTimeOffset serverTime,
          DateTimeOffset localTime )
        {
            if ( _options.Contains( security ) )
            {
                Action<bool, Security, IEnumerable<KeyValuePair<Level1Fields, object>>> securityChanged = SecurityChanged;
                if ( securityChanged == null )
                    return;
                securityChanged( true, security, values );
            }
            else
            {
                if ( security != UnderlyingAsset )
                    return;
                Action<bool, Security, IEnumerable<KeyValuePair<Level1Fields, object>>> securityChanged = SecurityChanged;
                if ( securityChanged == null )
                    return;
                securityChanged( false, security, values );
            }
        }

        public void Save( SettingsStorage storage )
        {
            if ( UnderlyingAsset != null )
                storage.SetValue( "UnderlyingAsset", UnderlyingAsset.Id );
            storage.SetValue( "Options", _options.Cache.Select( o => o.Id ).JoinComma() );
            storage.SetValue( "ExpiryDate", ExpiryDate );
            storage.SetValue( "UseBlackModel", UseBlackModel );
            storage.SetValue( "MinStrike", MinStrike );
            storage.SetValue( "MaxStrike", MaxStrike );
            storage.SetValue( "CurrentDate", CurrentDate );
            storage.SetValue( "AssetPrice", AssetPrice );
        }

        public void Load( SettingsStorage storage )
        {
            ExpiryDate = storage.GetValue( "ExpiryDate", new DateTime?() );
            MinStrike = storage.GetValue( "MinStrike", new Decimal?() );
            MaxStrike = storage.GetValue( "MaxStrike", new Decimal?() );
            UseBlackModel = storage.GetValue( "UseBlackModel", false );
            CurrentDate = storage.GetValue( "CurrentDate", new DateTime?() );
            AssetPrice = storage.GetValue( "AssetPrice", new Decimal?() );
            if ( SecurityProvider == null )
                return;
            if ( storage.ContainsKey( "UnderlyingAsset" ) )
            {
                _changeFromCode = true;
                try
                {
                    UnderlyingAsset = SecurityProvider.LookupById( storage.GetValue<string>( "UnderlyingAsset", null ) );
                }
                finally
                {
                    _changeFromCode = false;
                }
            }
            if ( !storage.ContainsKey( "Options" ) )
                return;
            string[ ] strArray = storage.GetValue<string>( "Options", null ).SplitByComma( false );
            _options.Clear();
            foreach ( string id in strArray )
                _options.Add( SecurityProvider.LookupById( id ) );
            foreach ( Security option in _options.Cache )
                SubscribeMarketData( option );
            UpdateSelectOptionsTitle();
            UpdateDatesComboBox();
            Action optionsChanged = OptionsChanged;
            if ( optionsChanged == null )
                return;
            optionsChanged();
        }

        private void UnderlyingAssetCtrl_OnSecuritySelected( object sender, EditValueChangedEventArgs e )
        {
            Security selectedSecurity = UnderlyingAssetCtrl.SelectedSecurity;
            if ( _underlyingAsset == selectedSecurity )
                return;
            if ( _underlyingAsset != null )
            {
                foreach ( Security option in _options.Cache )
                    UnSubscribeMarketData( option );
                SubscriptionManager?.RemoveSubscriptions( _underlyingAsset, DataType.Level1 );
                _options.Clear();
                _minAssetPrice = _maxAssetPrice = new Decimal?();
                _minDate = _maxDate = new DateTimeOffset?();
                _changeFromCode = true;
                try
                {
                    AssetPrice = new Decimal?();
                    CurrentDate = new DateTime?();
                }
                finally
                {
                    _changeFromCode = false;
                }
                UpdateSelectOptionsTitle();
            }
            _underlyingAsset = selectedSecurity;
            if ( _underlyingAsset != null )
            {
                if ( !_changeFromCode )
                    _options.AddRange( _underlyingAsset.GetDerivatives( SecurityProvider, new DateTimeOffset?() ) );
                UpdateSelectOptionsTitle();
                foreach ( Security option in _options.Cache )
                    SubscribeMarketData( option );
                SubscriptionManager?.CreateSubscription( selectedSecurity, DataType.Level1, null );
                SetPriceLimits( selectedSecurity );
                SetDateLimits( selectedSecurity );
            }
            UpdateDatesComboBox();
            Action underlyingAssetChanged = UnderlyingAssetChanged;
            if ( underlyingAssetChanged == null )
                return;
            underlyingAssetChanged();
        }

        private void UpdateDatesComboBox()
        {
            ExpiryDateComboCtrl.ItemsSource = _options.Cache.Select( o => o.ExpiryDate.Value.Date ).Distinct().OrderBy( d => d ).ToArray();
        }

        private Decimal? LastTradePrice
        {
            get
            {
                return ( Decimal? )MarketDataProvider.GetSecurityValue( UnderlyingAsset, Level1Fields.LastTradePrice );
            }
        }

        private void SetPriceLimits( Security asset )
        {
            Decimal? securityValue1 = ( Decimal? )MarketDataProvider.GetSecurityValue( asset, Level1Fields.MinPrice );
            Decimal? securityValue2 = ( Decimal? )MarketDataProvider.GetSecurityValue( asset, Level1Fields.MaxPrice );
            if ( securityValue1.HasValue && securityValue2.HasValue )
            {
                Decimal? nullable1 = securityValue1;
                Decimal? nullable2 = securityValue2;
                if ( nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & ( nullable1.HasValue & nullable2.HasValue ) )
                    throw new InvalidOperationException( string.Format( "{0} > {1}", securityValue1, securityValue2 ) );
                _minAssetPrice = new Decimal?( securityValue1.Value );
                _maxAssetPrice = new Decimal?( securityValue2.Value );
            }
            else
            {
                Security[ ] array = UnderlyingAsset.GetDerivatives( SecurityProvider, new DateTimeOffset?() ).ToArray();
                if ( array.Length == 0 )
                {
                    Decimal? lastTradePrice = LastTradePrice;
                    if ( !lastTradePrice.HasValue )
                        return;
                    Security security1 = asset;
                    Decimal? nullable = lastTradePrice;
                    Decimal price1 = ( Decimal )( ( nullable.HasValue ? ( Unit )nullable.GetValueOrDefault() : null ) - _maxStrikeOffset );
                    _minAssetPrice = new Decimal?( security1.ShrinkPrice( price1, ShrinkRules.Auto ) );
                    Security security2 = asset;
                    nullable = lastTradePrice;
                    Decimal price2 = ( Decimal )( ( nullable.HasValue ? ( Unit )nullable.GetValueOrDefault() : null ) + _maxStrikeOffset );
                    _maxAssetPrice = new Decimal?( security2.ShrinkPrice( price2, ShrinkRules.Auto ) );
                }
                else
                {
                    _minAssetPrice = array.Min( o => o.Strike );
                    _maxAssetPrice = array.Max( o => o.Strike );
                }
            }
        }

        private void AssetPriceModified_OnValueChanged(
          object sender,
          RoutedPropertyChangedEventArgs<double> e )
        {
            if ( _changeFromCode || !_minAssetPrice.HasValue || ( !_maxAssetPrice.HasValue || UnderlyingAsset == null ) )
                return;
            Decimal newValue = ( Decimal )e.NewValue;
            Decimal? maxAssetPrice = _maxAssetPrice;
            Decimal? minAssetPrice = _minAssetPrice;
            Decimal num = ( maxAssetPrice.HasValue & minAssetPrice.HasValue ? new Decimal?( maxAssetPrice.GetValueOrDefault() - minAssetPrice.GetValueOrDefault() ) : new Decimal?() ).Value;
            _fromModifier = true;
            try
            {
                AssetPrice = new Decimal?( _minAssetPrice.Value + UnderlyingAsset.ShrinkPrice( newValue / new Decimal( 100 ) * num, ShrinkRules.Auto ) );
            }
            finally
            {
                _fromModifier = false;
            }
        }

        private void AssetPriceCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Decimal? newValue = ( Decimal? )e.NewValue;
            _changeFromCode = true;
            try
            {
                if ( !newValue.HasValue )
                {
                    if ( !_fromModifier )
                        AssetPriceModified.Value = AssetPriceModified.Minimum;
                    AssetPriceReset.IsEnabled = false;
                    Action filterChanged = FilterChanged;
                    if ( filterChanged == null )
                        return;
                    filterChanged();
                    return;
                }
                Decimal num1 = newValue.Value;
                if ( !_fromModifier )
                {
                    if ( _minAssetPrice.HasValue )
                    {
                        if ( _maxAssetPrice.HasValue )
                        {
                            Decimal num2 = num1;
                            Decimal? maxAssetPrice1 = _maxAssetPrice;
                            Decimal valueOrDefault1 = maxAssetPrice1.GetValueOrDefault();
                            if ( num2 > valueOrDefault1 & maxAssetPrice1.HasValue )
                            {
                                AssetPriceModified.Value = AssetPriceModified.Maximum;
                            }
                            else
                            {
                                Decimal num3 = num1;
                                Decimal? minAssetPrice1 = _minAssetPrice;
                                Decimal valueOrDefault2 = minAssetPrice1.GetValueOrDefault();
                                if ( num3 < valueOrDefault2 & minAssetPrice1.HasValue )
                                {
                                    AssetPriceModified.Value = AssetPriceModified.Minimum;
                                }
                                else
                                {
                                    Decimal? maxAssetPrice2 = _maxAssetPrice;
                                    Decimal? minAssetPrice2 = _minAssetPrice;
                                    Decimal num4 = ( maxAssetPrice2.HasValue & minAssetPrice2.HasValue ? new Decimal?( maxAssetPrice2.GetValueOrDefault() - minAssetPrice2.GetValueOrDefault() ) : new Decimal?() ).Value;
                                    if ( num4 == Decimal.Zero )
                                        AssetPriceModified.Value = AssetPriceModified.Minimum;
                                    else
                                        AssetPriceModified.Value = ( double )( ( num1 - _minAssetPrice.Value ) / num4 * new Decimal( 100 ) );
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                _changeFromCode = false;
            }
            AssetPriceReset.IsEnabled = true;
            Action filterChanged1 = FilterChanged;
            if ( filterChanged1 == null )
                return;
            filterChanged1();
        }

        private void AssetPriceReOnClick = ( object sender, RoutedEventArgs e )
        {
            AssetPriceReset.IsEnabled = false;
            AssetPriceCtrl.EditValue = null;
        }

        private void CurrentDateModified_OnValueChanged(
          object sender,
          RoutedPropertyChangedEventArgs<double> e )
        {
            if ( _changeFromCode || !_minDate.HasValue || ( !_maxDate.HasValue || UnderlyingAsset == null ) )
                return;
            DateTimeOffset? maxDate = _maxDate;
            DateTimeOffset? minDate = _minDate;
            long ticks = ( maxDate.HasValue & minDate.HasValue ? new TimeSpan?( maxDate.GetValueOrDefault() - minDate.GetValueOrDefault() ) : new TimeSpan?() ).Value.Ticks;
            if ( ticks == 0L )
                return;
            _fromModifier = true;
            try
            {
                CurrentDate = new DateTime?( _minDate.Value.AddTicks( ( long )( e.NewValue / 100.0 * ticks ) ).DateTime );
            }
            finally
            {
                _fromModifier = false;
            }
        }

        private void CurrentDateCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            DateTime? newValue = ( DateTime? )e.NewValue;
            DateTime? minValue = CurrentDateCtrl.MinValue;
            _changeFromCode = true;
            try
            {
                if ( !newValue.HasValue )
                {
                    if ( !_fromModifier )
                        CurrentDateModified.Value = UnderlyingAsset == null || !minValue.HasValue ? 0.0 : ( TimeHelper.Now - minValue.Value ).TotalDays;
                    CurrentDateReset.IsEnabled = false;
                    Action filterChanged = FilterChanged;
                    if ( filterChanged == null )
                        return;
                    filterChanged();
                    return;
                }
                DateTime dateTime = newValue.Value;
                if ( !_fromModifier )
                {
                    if ( _minDate.HasValue )
                    {
                        if ( _maxDate.HasValue )
                        {
                            DateTimeOffset dateTimeOffset1 = ( DateTimeOffset )dateTime;
                            DateTimeOffset? maxDate1 = _maxDate;
                            if ( ( maxDate1.HasValue ? ( dateTimeOffset1 > maxDate1.GetValueOrDefault() ? 1 : 0 ) : 0 ) != 0 )
                            {
                                CurrentDateModified.Value = CurrentDateModified.Maximum;
                            }
                            else
                            {
                                DateTimeOffset dateTimeOffset2 = ( DateTimeOffset )dateTime;
                                DateTimeOffset? minDate1 = _minDate;
                                if ( ( minDate1.HasValue ? ( dateTimeOffset2 < minDate1.GetValueOrDefault() ? 1 : 0 ) : 0 ) != 0 )
                                {
                                    CurrentDateModified.Value = CurrentDateModified.Minimum;
                                }
                                else
                                {
                                    DateTimeOffset? maxDate2 = _maxDate;
                                    DateTimeOffset? minDate2 = _minDate;
                                    long ticks = ( maxDate2.HasValue & minDate2.HasValue ? new TimeSpan?( maxDate2.GetValueOrDefault() - minDate2.GetValueOrDefault() ) : new TimeSpan?() ).Value.Ticks;
                                    if ( ticks == 0L )
                                        CurrentDateModified.Value = CurrentDateModified.Minimum;
                                    else
                                        CurrentDateModified.Value = ( ( DateTimeOffset )dateTime - _minDate.Value ).Ticks / ticks * 100L;
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                _changeFromCode = false;
            }
            CurrentDateReset.IsEnabled = true;
            Action filterChanged1 = FilterChanged;
            if ( filterChanged1 == null )
                return;
            filterChanged1();
        }

        private void CurrentDateReOnClick = ( object sender, RoutedEventArgs e )
        {
            CurrentDateReset.IsEnabled = false;
            CurrentDateCtrl.EditValue = null;
        }

        private void SetDateLimits( Security asset )
        {
            Security[ ] cache = _options.Cache;
            Security security1 = cache.OrderByDescending( s => s.ExpiryDate ).FirstOrDefault() ?? asset;
            Security security2 = cache.OrderBy( s => s.ExpiryDate ).FirstOrDefault() ?? asset;
            DateTimeOffset? expiryDate = security1?.ExpiryDate;
            _maxDate = new DateTimeOffset?( ( expiryDate ?? ( DateTimeOffset )DateTime.Today ) + TimeSpan.FromDays( 5.0 ) );
            expiryDate = security2?.ExpiryDate;
            _minDate = new DateTimeOffset?( ( expiryDate ?? ( DateTimeOffset )DateTime.Today ) - TimeSpan.FromDays( 30.0 ) );
        }

        private void UseBlackModelCtrl_OnClick( object sender, RoutedEventArgs e )
        {
            if ( UnderlyingAsset == null )
                return;
            Action blackModelChanged = UseBlackModelChanged;
            if ( blackModelChanged == null )
                return;
            blackModelChanged();
        }

        private void OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            _expiryDate = ExpiryDateComboCtrl.EditValue as DateTime?;
            _minStrike = ( Decimal? )MinStrikeCtrl.EditValue;
            _maxStrike = ( Decimal? )MaxStrikeCtrl.EditValue;
            if ( UnderlyingAsset == null )
                return;
            UpdateSelectOptionsTitle();
            Action optionsChanged = OptionsChanged;
            if ( optionsChanged == null )
                return;
            optionsChanged();
        }

        public void Dispose()
        {
            if ( _marketDataProvider != null )
                _marketDataProvider.ValuesChanged -= new Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset>( MarketDataProvider_OnValuesChanged );
            SubscriptionManager?.Dispose();
        }

        public IEnumerable<Security> RemoveOptions( IEnumerable<Security> options )
        {
            foreach ( Security option in options )
            {
                if ( _options.Remove( option ) )
                {
                    UnSubscribeMarketData( option );
                    yield return option;
                }
            }
        }

        private void SelectOptions_OnClick( object sender, RoutedEventArgs e )
        {
            Security[ ] cache = _options.Cache;
            SecuritiesWindowEx wnd = new SecuritiesWindowEx() { SecurityProvider = SecurityProvider, SelectedSecurities = cache };
            if ( !wnd.ShowModal( this ) )
                return;
            Security[ ] array1 = cache.Except( wnd.SelectedSecurities ).ToArray();
            Security[ ] array2 = wnd.SelectedSecurities.Except( cache ).ToArray();
            foreach ( Security option in array1 )
                UnSubscribeMarketData( option );
            foreach ( Security option in array2 )
                SubscribeMarketData( option );
            _options.Clear();
            _options.AddRange( wnd.SelectedSecurities );
            UpdateSelectOptionsTitle();
            Action optionsChanged = OptionsChanged;
            if ( optionsChanged == null )
                return;
            optionsChanged();
        }

        private void UpdateSelectOptionsTitle()
        {
            SimpleButton selectOptions = SelectOptions;
            string nnOptions = LocalizedStrings.NNOptions;
            object[ ] objArray = new object[1];
            int num = Options.Length;
            string str1 = num.ToString();
            num = _options.Count;
            string str2 = num.ToString();
            objArray[0] = str1 + "/" + str2;
            string str3 = nnOptions.Put( objArray );
            selectOptions.Content = str3;
        }

        private void ClearDateButton_Click( object sender, RoutedEventArgs e )
        {
            ExpiryDateComboCtrl.EditValue = null;
        }


    }
}
