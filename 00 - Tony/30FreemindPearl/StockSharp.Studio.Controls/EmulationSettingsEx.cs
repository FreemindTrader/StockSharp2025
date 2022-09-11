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
                return _marketDataDrive;
            }
            set
            {
                _marketDataDrive = value;
                NotifyChanged( nameof( MarketDataDrive ) );
            }
        }

        [Display( Description = "StorageFormatDot", GroupName = "Str1174", Name = "StorageFormat", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public StorageFormats StorageFormat
        {
            get
            {
                return _storageFormat;
            }
            set
            {
                _storageFormat = value;
                NotifyChanged( nameof( StorageFormat ) );
            }
        }

        [Display( Description = "XamlStr293", GroupName = "Str1174", Name = "XamlStr293", Order = 80, ResourceType = typeof( LocalizedStrings ) )]
        public int MaxVolume
        {
            get
            {
                return _maxVolume;
            }
            set
            {
                _maxVolume = value;
                NotifyChanged( nameof( MaxVolume ) );
            }
        }

        [Display( Description = "Str160", GroupName = "Str1174", Name = "Str159", Order = 300, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( CommissionRulesEditor ), typeof( CommissionRulesEditor ) )]
        public IEnumerable<CommissionRule> CommissionRules
        {
            get
            {
                return _commissionRules;
            }
            set
            {
                _commissionRules = value;
                NotifyChanged( nameof( CommissionRules ) );
            }
        }

        public EmulationSettingsEx()
        {
            StartTime = DateTime.Today.AddDays( -60.0 );
            StopTime = DateTime.Today;
            StorageFormat = StorageFormats.Binary;
            MaxDepth = 5;
            MaxVolume = 100;
            IsSupportAtomicReRegister = true;
            MatchOnTouch = false;
            Latency = TimeSpan.Zero;
            CommissionRules = Array.Empty<CommissionRule>();
            TradeDataMode = DepthDataMode = OrderLogDataMode = EmulationMarketDataModes.No;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            MaxVolume = storage.GetValue( "MaxVolume", MaxVolume );
            MarketDataDrive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "MarketDataDrive", null ) );
            StorageFormat = storage.GetValue( "StorageFormat", StorageFormat );
            storage.TryLoadSettings<SettingsStorage[ ]>( "CommissionRules", s => CommissionRules = s.Select( i => i.LoadEntire<CommissionRule>() ).ToArray() );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "MaxVolume", MaxVolume );
            if ( MarketDataDrive != null )
                storage.SetValue( "MarketDataDrive", MarketDataDrive.Path );
            storage.SetValue( "StorageFormat", StorageFormat );
            storage.SetValue( "CommissionRules", CommissionRules.Select( c => c.SaveEntire( false ) ).ToArray() );
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
