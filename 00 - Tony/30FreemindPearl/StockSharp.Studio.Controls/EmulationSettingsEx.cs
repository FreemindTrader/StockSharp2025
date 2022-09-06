using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Commissions;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Strategies.Testing;
using StockSharp.Localization;
using StockSharp.Studio.Core.Services;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockSharp.Studio.Controls
{
    [TypeConverter( typeof( ExpandableObjectConverter ) )]
    public class EmulationSettingsEx : EmulationSettings
    {
        private IMarketDataDrive _marketDataDrive;
        private StorageFormats _storageFormat;
        private int _maxVolume;
        private IEnumerable<CommissionRule> _commissionRules;

        [Display( Description = "MarketDataStorage", GroupName = "Str1174", Name = "MarketData", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( DriveComboBoxEditor ), typeof( DriveComboBoxEditor ) )]
        public IMarketDataDrive MarketDataDrive
        {
            get
            {
                return this._marketDataDrive;
            }
            set
            {
                this._marketDataDrive = value;
                this.NotifyChanged( nameof( MarketDataDrive ) );
            }
        }

        [Display( Description = "StorageFormatDot", GroupName = "Str1174", Name = "StorageFormat", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public StorageFormats StorageFormat
        {
            get
            {
                return this._storageFormat;
            }
            set
            {
                this._storageFormat = value;
                this.NotifyChanged( nameof( StorageFormat ) );
            }
        }

        [Display( Description = "XamlStr293", GroupName = "Str1174", Name = "XamlStr293", Order = 80, ResourceType = typeof( LocalizedStrings ) )]
        public int MaxVolume
        {
            get
            {
                return this._maxVolume;
            }
            set
            {
                this._maxVolume = value;
                this.NotifyChanged( nameof( MaxVolume ) );
            }
        }

        [Display( Description = "Str160", GroupName = "Str1174", Name = "Str159", Order = 300, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( CommissionRulesEditor ), typeof( CommissionRulesEditor ) )]
        public IEnumerable<CommissionRule> CommissionRules
        {
            get
            {
                return this._commissionRules;
            }
            set
            {
                this._commissionRules = value;
                this.NotifyChanged( nameof( CommissionRules ) );
            }
        }

        public EmulationSettingsEx()
        {
            this.StartTime = DateTime.Today.AddDays( -60.0 );
            this.StopTime = DateTime.Today;
            this.StorageFormat = StorageFormats.Binary;
            this.MaxDepth = 5;
            this.MaxVolume = 100;
            this.IsSupportAtomicReRegister = true;
            this.MatchOnTouch = false;
            this.Latency = TimeSpan.Zero;
            this.CommissionRules = ( IEnumerable<CommissionRule> )Array.Empty<CommissionRule>();
            this.TradeDataMode = this.DepthDataMode = this.OrderLogDataMode = EmulationMarketDataModes.No;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.MaxVolume = storage.GetValue<int>( "MaxVolume", this.MaxVolume );
            this.MarketDataDrive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "MarketDataDrive", ( string )null ) );
            this.StorageFormat = storage.GetValue<StorageFormats>( "StorageFormat", this.StorageFormat );
            storage.TryLoadSettings<SettingsStorage[ ]>( "CommissionRules", ( Action<SettingsStorage[ ]> )( s => this.CommissionRules = ( IEnumerable<CommissionRule> )( ( IEnumerable<SettingsStorage> )s ).Select<SettingsStorage, CommissionRule>( ( Func<SettingsStorage, CommissionRule> )( i => i.LoadEntire<CommissionRule>() ) ).ToArray<CommissionRule>() ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<int>( "MaxVolume", this.MaxVolume );
            if ( this.MarketDataDrive != null )
                storage.SetValue<string>( "MarketDataDrive", this.MarketDataDrive.Path );
            storage.SetValue<StorageFormats>( "StorageFormat", this.StorageFormat );
            storage.SetValue<SettingsStorage[ ]>( "CommissionRules", this.CommissionRules.Select<CommissionRule, SettingsStorage>( ( Func<CommissionRule, SettingsStorage> )( c => c.SaveEntire( false ) ) ).ToArray<SettingsStorage>() );
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
