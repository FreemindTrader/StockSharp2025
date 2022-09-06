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
            this.InitializeComponent();
            this.UpdateSelectOptionsTitle();
        }

        public SubscriptionManager SubscriptionManager { get; set; }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return this._securityProvider;
            }
            set
            {
                this._securityProvider = value;
                this.UnderlyingAssetCtrl.SecurityProvider = value;
            }
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return this._marketDataProvider;
            }
            set
            {
                if ( value == this._marketDataProvider )
                    return;
                if ( this._marketDataProvider != null )
                    this._marketDataProvider.ValuesChanged -= new Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset>( this.MarketDataProvider_OnValuesChanged );
                this._marketDataProvider = value;
                if ( this._marketDataProvider == null )
                    return;
                this._marketDataProvider.ValuesChanged += new Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset>( this.MarketDataProvider_OnValuesChanged );
            }
        }

        public DateTime? CurrentDate
        {
            get
            {
                return ( DateTime? )this.CurrentDateCtrl.EditValue;
            }
            private set
            {
                this.CurrentDateCtrl.EditValue = ( object )value;
            }
        }

        public Decimal? AssetPrice
        {
            get
            {
                return ( Decimal? )this.AssetPriceCtrl.EditValue;
            }
            private set
            {
                this.AssetPriceCtrl.EditValue = ( object )value;
            }
        }

        public bool UseBlackModel
        {
            get
            {
                bool? isChecked = this.UseBlackModelCtrl.IsChecked;
                bool flag = true;
                return isChecked.GetValueOrDefault() == flag & isChecked.HasValue;
            }
            private set
            {
                this.UseBlackModelCtrl.IsChecked = new bool?( value );
            }
        }

        public Security UnderlyingAsset
        {
            get
            {
                return this._underlyingAsset;
            }
            private set
            {
                this.UnderlyingAssetCtrl.SelectedSecurity = value;
            }
        }

        public DateTime? ExpiryDate
        {
            get
            {
                return this._expiryDate;
            }
            private set
            {
                this.ExpiryDateComboCtrl.SelectedItem = ( object )value;
            }
        }

        public Decimal? MinStrike
        {
            get
            {
                return this._minStrike;
            }
            private set
            {
                this.MinStrikeCtrl.EditValue = ( object )value;
            }
        }

        public Decimal? MaxStrike
        {
            get
            {
                return this._maxStrike;
            }
            private set
            {
                this.MaxStrikeCtrl.EditValue = ( object )value;
            }
        }

        public Security[ ] Options
        {
            get
            {
                return ( ( IEnumerable<Security> )this._options.Cache ).Where<Security>( ( Func<Security, bool> )( o =>
                      {
                          if ( this.ExpiryDate.HasValue )
                          {
                              DateTimeOffset? expiryDate = o.ExpiryDate;
                              ref DateTimeOffset? local = ref expiryDate;
                              DateTime? nullable = local.HasValue ? new DateTime?( local.GetValueOrDefault().Date ) : new DateTime?();
                              DateTime date = this.ExpiryDate.Value.Date;
                              if ( ( nullable.HasValue ? ( nullable.HasValue ? ( nullable.GetValueOrDefault() != date ? 1 : 0 ) : 0 ) : 1 ) != 0 )
                                  return false;
                          }
                          Decimal? nullable1;
                          if ( this.MinStrike.HasValue )
                          {
                              Decimal? strike = o.Strike;
                              nullable1 = this.MinStrike;
                              if ( strike.GetValueOrDefault() < nullable1.GetValueOrDefault() & ( strike.HasValue & nullable1.HasValue ) )
                                  return false;
                          }
                          nullable1 = this.MaxStrike;
                          if ( nullable1.HasValue )
                          {
                              nullable1 = o.Strike;
                              Decimal? maxStrike = this.MaxStrike;
                              if ( nullable1.GetValueOrDefault() > maxStrike.GetValueOrDefault() & ( nullable1.HasValue & maxStrike.HasValue ) )
                                  return false;
                          }
                          return true;
                      } ) ).ToArray<Security>();
            }
        }

        public event Action UnderlyingAssetChanged;

        public event Action FilterChanged;

        public event Action OptionsChanged;

        public event Action UseBlackModelChanged;

        public event Action<bool, Security, IEnumerable<KeyValuePair<Level1Fields, object>>> SecurityChanged;

        private void SubscribeMarketData( Security option )
        {
            this.SubscriptionManager?.CreateSubscription( option, DataType.Level1, ( Action<Subscription> )null );
        }

        private void UnSubscribeMarketData( Security option )
        {
            this.SubscriptionManager?.RemoveSubscriptions( option );
        }

        private void MarketDataProvider_OnValuesChanged(
          Security security,
          IEnumerable<KeyValuePair<Level1Fields, object>> values,
          DateTimeOffset serverTime,
          DateTimeOffset localTime )
        {
            if ( this._options.Contains( security ) )
            {
                Action<bool, Security, IEnumerable<KeyValuePair<Level1Fields, object>>> securityChanged = this.SecurityChanged;
                if ( securityChanged == null )
                    return;
                securityChanged( true, security, values );
            }
            else
            {
                if ( security != this.UnderlyingAsset )
                    return;
                Action<bool, Security, IEnumerable<KeyValuePair<Level1Fields, object>>> securityChanged = this.SecurityChanged;
                if ( securityChanged == null )
                    return;
                securityChanged( false, security, values );
            }
        }

        public void Save( SettingsStorage storage )
        {
            if ( this.UnderlyingAsset != null )
                storage.SetValue<string>( "UnderlyingAsset", this.UnderlyingAsset.Id );
            storage.SetValue<string>( "Options", ( ( IEnumerable<Security> )this._options.Cache ).Select<Security, string>( ( Func<Security, string> )( o => o.Id ) ).JoinComma() );
            storage.SetValue<DateTime?>( "ExpiryDate", this.ExpiryDate );
            storage.SetValue<bool>( "UseBlackModel", this.UseBlackModel );
            storage.SetValue<Decimal?>( "MinStrike", this.MinStrike );
            storage.SetValue<Decimal?>( "MaxStrike", this.MaxStrike );
            storage.SetValue<DateTime?>( "CurrentDate", this.CurrentDate );
            storage.SetValue<Decimal?>( "AssetPrice", this.AssetPrice );
        }

        public void Load( SettingsStorage storage )
        {
            this.ExpiryDate = storage.GetValue<DateTime?>( "ExpiryDate", new DateTime?() );
            this.MinStrike = storage.GetValue<Decimal?>( "MinStrike", new Decimal?() );
            this.MaxStrike = storage.GetValue<Decimal?>( "MaxStrike", new Decimal?() );
            this.UseBlackModel = storage.GetValue<bool>( "UseBlackModel", false );
            this.CurrentDate = storage.GetValue<DateTime?>( "CurrentDate", new DateTime?() );
            this.AssetPrice = storage.GetValue<Decimal?>( "AssetPrice", new Decimal?() );
            if ( this.SecurityProvider == null )
                return;
            if ( storage.ContainsKey( "UnderlyingAsset" ) )
            {
                this._changeFromCode = true;
                try
                {
                    this.UnderlyingAsset = this.SecurityProvider.LookupById( storage.GetValue<string>( "UnderlyingAsset", ( string )null ) );
                }
                finally
                {
                    this._changeFromCode = false;
                }
            }
            if ( !storage.ContainsKey( "Options" ) )
                return;
            string[ ] strArray = storage.GetValue<string>( "Options", ( string )null ).SplitByComma( false );
            this._options.Clear();
            foreach ( string id in strArray )
                this._options.Add( this.SecurityProvider.LookupById( id ) );
            foreach ( Security option in this._options.Cache )
                this.SubscribeMarketData( option );
            this.UpdateSelectOptionsTitle();
            this.UpdateDatesComboBox();
            Action optionsChanged = this.OptionsChanged;
            if ( optionsChanged == null )
                return;
            optionsChanged();
        }

        private void UnderlyingAssetCtrl_OnSecuritySelected( object sender, EditValueChangedEventArgs e )
        {
            Security selectedSecurity = this.UnderlyingAssetCtrl.SelectedSecurity;
            if ( this._underlyingAsset == selectedSecurity )
                return;
            if ( this._underlyingAsset != null )
            {
                foreach ( Security option in this._options.Cache )
                    this.UnSubscribeMarketData( option );
                this.SubscriptionManager?.RemoveSubscriptions( this._underlyingAsset, DataType.Level1 );
                this._options.Clear();
                this._minAssetPrice = this._maxAssetPrice = new Decimal?();
                this._minDate = this._maxDate = new DateTimeOffset?();
                this._changeFromCode = true;
                try
                {
                    this.AssetPrice = new Decimal?();
                    this.CurrentDate = new DateTime?();
                }
                finally
                {
                    this._changeFromCode = false;
                }
                this.UpdateSelectOptionsTitle();
            }
            this._underlyingAsset = selectedSecurity;
            if ( this._underlyingAsset != null )
            {
                if ( !this._changeFromCode )
                    this._options.AddRange( this._underlyingAsset.GetDerivatives( this.SecurityProvider, new DateTimeOffset?() ) );
                this.UpdateSelectOptionsTitle();
                foreach ( Security option in this._options.Cache )
                    this.SubscribeMarketData( option );
                this.SubscriptionManager?.CreateSubscription( selectedSecurity, DataType.Level1, ( Action<Subscription> )null );
                this.SetPriceLimits( selectedSecurity );
                this.SetDateLimits( selectedSecurity );
            }
            this.UpdateDatesComboBox();
            Action underlyingAssetChanged = this.UnderlyingAssetChanged;
            if ( underlyingAssetChanged == null )
                return;
            underlyingAssetChanged();
        }

        private void UpdateDatesComboBox()
        {
            this.ExpiryDateComboCtrl.ItemsSource = ( object )( ( IEnumerable<Security> )this._options.Cache ).Select<Security, DateTime>( ( Func<Security, DateTime> )( o => o.ExpiryDate.Value.Date ) ).Distinct<DateTime>().OrderBy<DateTime, DateTime>( ( Func<DateTime, DateTime> )( d => d ) ).ToArray<DateTime>();
        }

        private Decimal? LastTradePrice
        {
            get
            {
                return ( Decimal? )this.MarketDataProvider.GetSecurityValue( this.UnderlyingAsset, Level1Fields.LastTradePrice );
            }
        }

        private void SetPriceLimits( Security asset )
        {
            Decimal? securityValue1 = ( Decimal? )this.MarketDataProvider.GetSecurityValue( asset, Level1Fields.MinPrice );
            Decimal? securityValue2 = ( Decimal? )this.MarketDataProvider.GetSecurityValue( asset, Level1Fields.MaxPrice );
            if ( securityValue1.HasValue && securityValue2.HasValue )
            {
                Decimal? nullable1 = securityValue1;
                Decimal? nullable2 = securityValue2;
                if ( nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & ( nullable1.HasValue & nullable2.HasValue ) )
                    throw new InvalidOperationException( string.Format( "{0} > {1}", ( object )securityValue1, ( object )securityValue2 ) );
                this._minAssetPrice = new Decimal?( securityValue1.Value );
                this._maxAssetPrice = new Decimal?( securityValue2.Value );
            }
            else
            {
                Security[ ] array = this.UnderlyingAsset.GetDerivatives( this.SecurityProvider, new DateTimeOffset?() ).ToArray<Security>();
                if ( array.Length == 0 )
                {
                    Decimal? lastTradePrice = this.LastTradePrice;
                    if ( !lastTradePrice.HasValue )
                        return;
                    Security security1 = asset;
                    Decimal? nullable = lastTradePrice;
                    Decimal price1 = ( Decimal )( ( nullable.HasValue ? ( Unit )nullable.GetValueOrDefault() : ( Unit )null ) - this._maxStrikeOffset );
                    this._minAssetPrice = new Decimal?( security1.ShrinkPrice( price1, ShrinkRules.Auto ) );
                    Security security2 = asset;
                    nullable = lastTradePrice;
                    Decimal price2 = ( Decimal )( ( nullable.HasValue ? ( Unit )nullable.GetValueOrDefault() : ( Unit )null ) + this._maxStrikeOffset );
                    this._maxAssetPrice = new Decimal?( security2.ShrinkPrice( price2, ShrinkRules.Auto ) );
                }
                else
                {
                    this._minAssetPrice = ( ( IEnumerable<Security> )array ).Min<Security>( ( Func<Security, Decimal?> )( o => o.Strike ) );
                    this._maxAssetPrice = ( ( IEnumerable<Security> )array ).Max<Security>( ( Func<Security, Decimal?> )( o => o.Strike ) );
                }
            }
        }

        private void AssetPriceModified_OnValueChanged(
          object sender,
          RoutedPropertyChangedEventArgs<double> e )
        {
            if ( this._changeFromCode || !this._minAssetPrice.HasValue || ( !this._maxAssetPrice.HasValue || this.UnderlyingAsset == null ) )
                return;
            Decimal newValue = ( Decimal )e.NewValue;
            Decimal? maxAssetPrice = this._maxAssetPrice;
            Decimal? minAssetPrice = this._minAssetPrice;
            Decimal num = ( maxAssetPrice.HasValue & minAssetPrice.HasValue ? new Decimal?( maxAssetPrice.GetValueOrDefault() - minAssetPrice.GetValueOrDefault() ) : new Decimal?() ).Value;
            this._fromModifier = true;
            try
            {
                this.AssetPrice = new Decimal?( this._minAssetPrice.Value + this.UnderlyingAsset.ShrinkPrice( newValue / new Decimal( 100 ) * num, ShrinkRules.Auto ) );
            }
            finally
            {
                this._fromModifier = false;
            }
        }

        private void AssetPriceCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Decimal? newValue = ( Decimal? )e.NewValue;
            this._changeFromCode = true;
            try
            {
                if ( !newValue.HasValue )
                {
                    if ( !this._fromModifier )
                        this.AssetPriceModified.Value = this.AssetPriceModified.Minimum;
                    this.AssetPriceReset.IsEnabled = false;
                    Action filterChanged = this.FilterChanged;
                    if ( filterChanged == null )
                        return;
                    filterChanged();
                    return;
                }
                Decimal num1 = newValue.Value;
                if ( !this._fromModifier )
                {
                    if ( this._minAssetPrice.HasValue )
                    {
                        if ( this._maxAssetPrice.HasValue )
                        {
                            Decimal num2 = num1;
                            Decimal? maxAssetPrice1 = this._maxAssetPrice;
                            Decimal valueOrDefault1 = maxAssetPrice1.GetValueOrDefault();
                            if ( num2 > valueOrDefault1 & maxAssetPrice1.HasValue )
                            {
                                this.AssetPriceModified.Value = this.AssetPriceModified.Maximum;
                            }
                            else
                            {
                                Decimal num3 = num1;
                                Decimal? minAssetPrice1 = this._minAssetPrice;
                                Decimal valueOrDefault2 = minAssetPrice1.GetValueOrDefault();
                                if ( num3 < valueOrDefault2 & minAssetPrice1.HasValue )
                                {
                                    this.AssetPriceModified.Value = this.AssetPriceModified.Minimum;
                                }
                                else
                                {
                                    Decimal? maxAssetPrice2 = this._maxAssetPrice;
                                    Decimal? minAssetPrice2 = this._minAssetPrice;
                                    Decimal num4 = ( maxAssetPrice2.HasValue & minAssetPrice2.HasValue ? new Decimal?( maxAssetPrice2.GetValueOrDefault() - minAssetPrice2.GetValueOrDefault() ) : new Decimal?() ).Value;
                                    if ( num4 == Decimal.Zero )
                                        this.AssetPriceModified.Value = this.AssetPriceModified.Minimum;
                                    else
                                        this.AssetPriceModified.Value = ( double )( ( num1 - this._minAssetPrice.Value ) / num4 * new Decimal( 100 ) );
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                this._changeFromCode = false;
            }
            this.AssetPriceReset.IsEnabled = true;
            Action filterChanged1 = this.FilterChanged;
            if ( filterChanged1 == null )
                return;
            filterChanged1();
        }

        private void AssetPriceReset_OnClick( object sender, RoutedEventArgs e )
        {
            this.AssetPriceReset.IsEnabled = false;
            this.AssetPriceCtrl.EditValue = ( object )null;
        }

        private void CurrentDateModified_OnValueChanged(
          object sender,
          RoutedPropertyChangedEventArgs<double> e )
        {
            if ( this._changeFromCode || !this._minDate.HasValue || ( !this._maxDate.HasValue || this.UnderlyingAsset == null ) )
                return;
            DateTimeOffset? maxDate = this._maxDate;
            DateTimeOffset? minDate = this._minDate;
            long ticks = ( maxDate.HasValue & minDate.HasValue ? new TimeSpan?( maxDate.GetValueOrDefault() - minDate.GetValueOrDefault() ) : new TimeSpan?() ).Value.Ticks;
            if ( ticks == 0L )
                return;
            this._fromModifier = true;
            try
            {
                this.CurrentDate = new DateTime?( this._minDate.Value.AddTicks( ( long )( e.NewValue / 100.0 * ( double )ticks ) ).DateTime );
            }
            finally
            {
                this._fromModifier = false;
            }
        }

        private void CurrentDateCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            DateTime? newValue = ( DateTime? )e.NewValue;
            DateTime? minValue = this.CurrentDateCtrl.MinValue;
            this._changeFromCode = true;
            try
            {
                if ( !newValue.HasValue )
                {
                    if ( !this._fromModifier )
                        this.CurrentDateModified.Value = this.UnderlyingAsset == null || !minValue.HasValue ? 0.0 : ( TimeHelper.Now - minValue.Value ).TotalDays;
                    this.CurrentDateReset.IsEnabled = false;
                    Action filterChanged = this.FilterChanged;
                    if ( filterChanged == null )
                        return;
                    filterChanged();
                    return;
                }
                DateTime dateTime = newValue.Value;
                if ( !this._fromModifier )
                {
                    if ( this._minDate.HasValue )
                    {
                        if ( this._maxDate.HasValue )
                        {
                            DateTimeOffset dateTimeOffset1 = ( DateTimeOffset )dateTime;
                            DateTimeOffset? maxDate1 = this._maxDate;
                            if ( ( maxDate1.HasValue ? ( dateTimeOffset1 > maxDate1.GetValueOrDefault() ? 1 : 0 ) : 0 ) != 0 )
                            {
                                this.CurrentDateModified.Value = this.CurrentDateModified.Maximum;
                            }
                            else
                            {
                                DateTimeOffset dateTimeOffset2 = ( DateTimeOffset )dateTime;
                                DateTimeOffset? minDate1 = this._minDate;
                                if ( ( minDate1.HasValue ? ( dateTimeOffset2 < minDate1.GetValueOrDefault() ? 1 : 0 ) : 0 ) != 0 )
                                {
                                    this.CurrentDateModified.Value = this.CurrentDateModified.Minimum;
                                }
                                else
                                {
                                    DateTimeOffset? maxDate2 = this._maxDate;
                                    DateTimeOffset? minDate2 = this._minDate;
                                    long ticks = ( maxDate2.HasValue & minDate2.HasValue ? new TimeSpan?( maxDate2.GetValueOrDefault() - minDate2.GetValueOrDefault() ) : new TimeSpan?() ).Value.Ticks;
                                    if ( ticks == 0L )
                                        this.CurrentDateModified.Value = this.CurrentDateModified.Minimum;
                                    else
                                        this.CurrentDateModified.Value = ( double )( ( ( DateTimeOffset )dateTime - this._minDate.Value ).Ticks / ticks * 100L );
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                this._changeFromCode = false;
            }
            this.CurrentDateReset.IsEnabled = true;
            Action filterChanged1 = this.FilterChanged;
            if ( filterChanged1 == null )
                return;
            filterChanged1();
        }

        private void CurrentDateReset_OnClick( object sender, RoutedEventArgs e )
        {
            this.CurrentDateReset.IsEnabled = false;
            this.CurrentDateCtrl.EditValue = ( object )null;
        }

        private void SetDateLimits( Security asset )
        {
            Security[ ] cache = this._options.Cache;
            Security security1 = ( ( IEnumerable<Security> )cache ).OrderByDescending<Security, DateTimeOffset?>( ( Func<Security, DateTimeOffset?> )( s => s.ExpiryDate ) ).FirstOrDefault<Security>() ?? asset;
            Security security2 = ( ( IEnumerable<Security> )cache ).OrderBy<Security, DateTimeOffset?>( ( Func<Security, DateTimeOffset?> )( s => s.ExpiryDate ) ).FirstOrDefault<Security>() ?? asset;
            DateTimeOffset? expiryDate = security1?.ExpiryDate;
            this._maxDate = new DateTimeOffset?( ( expiryDate ?? ( DateTimeOffset )DateTime.Today ) + TimeSpan.FromDays( 5.0 ) );
            expiryDate = security2?.ExpiryDate;
            this._minDate = new DateTimeOffset?( ( expiryDate ?? ( DateTimeOffset )DateTime.Today ) - TimeSpan.FromDays( 30.0 ) );
        }

        private void UseBlackModelCtrl_OnClick( object sender, RoutedEventArgs e )
        {
            if ( this.UnderlyingAsset == null )
                return;
            Action blackModelChanged = this.UseBlackModelChanged;
            if ( blackModelChanged == null )
                return;
            blackModelChanged();
        }

        private void OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            this._expiryDate = this.ExpiryDateComboCtrl.EditValue as DateTime?;
            this._minStrike = ( Decimal? )this.MinStrikeCtrl.EditValue;
            this._maxStrike = ( Decimal? )this.MaxStrikeCtrl.EditValue;
            if ( this.UnderlyingAsset == null )
                return;
            this.UpdateSelectOptionsTitle();
            Action optionsChanged = this.OptionsChanged;
            if ( optionsChanged == null )
                return;
            optionsChanged();
        }

        public void Dispose()
        {
            if ( this._marketDataProvider != null )
                this._marketDataProvider.ValuesChanged -= new Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset>( this.MarketDataProvider_OnValuesChanged );
            this.SubscriptionManager?.Dispose();
        }

        public IEnumerable<Security> RemoveOptions( IEnumerable<Security> options )
        {
            foreach ( Security option in options )
            {
                if ( this._options.Remove( option ) )
                {
                    this.UnSubscribeMarketData( option );
                    yield return option;
                }
            }
        }

        private void SelectOptions_OnClick( object sender, RoutedEventArgs e )
        {
            Security[ ] cache = this._options.Cache;
            SecuritiesWindowEx wnd = new SecuritiesWindowEx() { SecurityProvider = this.SecurityProvider, SelectedSecurities = ( IEnumerable<Security> )cache };
            if ( !wnd.ShowModal( this ) )
                return;
            Security[ ] array1 = ( ( IEnumerable<Security> )cache ).Except<Security>( wnd.SelectedSecurities ).ToArray<Security>();
            Security[ ] array2 = wnd.SelectedSecurities.Except<Security>( ( IEnumerable<Security> )cache ).ToArray<Security>();
            foreach ( Security option in array1 )
                this.UnSubscribeMarketData( option );
            foreach ( Security option in array2 )
                this.SubscribeMarketData( option );
            this._options.Clear();
            this._options.AddRange( wnd.SelectedSecurities );
            this.UpdateSelectOptionsTitle();
            Action optionsChanged = this.OptionsChanged;
            if ( optionsChanged == null )
                return;
            optionsChanged();
        }

        private void UpdateSelectOptionsTitle()
        {
            SimpleButton selectOptions = this.SelectOptions;
            string nnOptions = LocalizedStrings.NNOptions;
            object[ ] objArray = new object[1];
            int num = this.Options.Length;
            string str1 = num.ToString();
            num = this._options.Count;
            string str2 = num.ToString();
            objArray[0] = ( object )( str1 + "/" + str2 );
            string str3 = nnOptions.Put( objArray );
            selectOptions.Content = ( object )str3;
        }

        private void ClearDateButton_Click( object sender, RoutedEventArgs e )
        {
            this.ExpiryDateComboCtrl.EditValue = ( object )null;
        }


    }
}
