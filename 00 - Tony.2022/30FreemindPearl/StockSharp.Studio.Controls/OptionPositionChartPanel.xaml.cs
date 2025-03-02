using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.Charting;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using Wintellect.PowerCollections;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str3239" )]
    [DescriptionLoc( "Str3240", false )]
    [VectorIcon( "OptionChart" )]
    [Doc( "topics/Terminal_options_positions.html" )]
    public partial class OptionPositionChartPanel : BaseStudioControl, IComponentConnector
    {
        private readonly SyncObject _needRefreshLock = new SyncObject();
        private bool _needRefresh;
        private readonly DispatcherTimer _refreshTimer;
        

        public OptionPositionChartPanel()
        {
            InitializeComponent();
            FilterPanel.SubscriptionManager = new SubscriptionManager( this );
            IStudioCommandService commandService = CommandService;
            commandService.Register<ResetedCommand>(
                                                                    this,
                                                                    true,
                                                                    cmd =>
                                                                    {
                                                                    },
                                                                    null );
            commandService.Register<SecuritiesRemovedCommand>(
                                                                    this,
                                                                    false,
                                                                    cmd =>
                                                                    {
                                                                        if ( FilterPanel.RemoveOptions( cmd.Securities ).ToArray().Length == 0 )
                                                                        {
                                                                            return;
                                                                        }

                                                                        MakeDirty();
                                                                        RaiseChangedCommand();
                                                                    },
                                                                    null );
            FilterPanel.SecurityProvider   = SecurityProvider;
            FilterPanel.MarketDataProvider = MarketDataProvider;
            PosChart.MarketDataProvider    = MarketDataProvider;
            PosChart.ExchangeInfoProvider  = ExchangeInfoProvider;
            PosChart.SecurityProvider      = SecurityProvider;
            PosChart.PositionProvider      = ServicesRegistry.PositionProvider;
            _refreshTimer                  = new DispatcherTimer() { Interval = TimeSpan.FromSeconds( 5.0 ) };
            _refreshTimer.Tick += ( s, e ) =>
                                                                    {
                                                                        lock ( _needRefreshLock )
                                                                        {
                                                                            if ( !_needRefresh )
                                                                            {
                                                                                return;
                                                                            }

                                                                            _needRefresh = false;
                                                                        }
                                                                        RefreshChart();
                                                                    };
            _refreshTimer.Start();
        }

        private void MakeDirty()
        {
            lock ( _needRefreshLock )
                _needRefresh = true;
        }

        public override void Dispose()
        {
            _refreshTimer.Stop();
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<SecuritiesRemovedCommand>( this );
            FilterPanel.Dispose();
            base.Dispose();
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "FilterPanel", FilterPanel.Save() );
            storage.SetValue( "PosChart", PosChart.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            FilterPanel.Load( storage.GetValue<SettingsStorage>( "FilterPanel", null ) );
            PosChart.Load( storage.GetValue<SettingsStorage>( "PosChart", null ) );
        }

        private void RefreshChart()
        {
            if ( FilterPanel.UnderlyingAsset == null )
                return;
            OptionPositionChart posChart = PosChart;
            Decimal? assetPrice = FilterPanel.AssetPrice;
            DateTime? nullable = FilterPanel.CurrentDate;
            DateTimeOffset? currentTime = nullable.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable.GetValueOrDefault() ) : new DateTimeOffset?();
            nullable = FilterPanel.ExpiryDate;
            DateTimeOffset? expiryDate = nullable.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable.GetValueOrDefault() ) : new DateTimeOffset?();
            posChart.Refresh( assetPrice, currentTime, expiryDate );
        }

        private void FilterPanel_OnUnderlyingAssetChanged()
        {
            PosChart.UnderlyingAsset = FilterPanel.UnderlyingAsset;
            if ( PosChart.UnderlyingAsset != null )
                FilterPanel.Options.ForEach( new Action<Security>( PosChart.Options.Add ) );
            Title = LocalizedStrings.Str3239 + " " + FilterPanel.UnderlyingAsset?.ToString();
            RefreshChart();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnFilterChanged()
        {
            RefreshChart();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnUseBlackModelChanged()
        {
            PosChart.UseBlackModel = FilterPanel.UseBlackModel;
            RefreshChart();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnOptionsChanged()
        {
            PosChart.Options.Clear();
            FilterPanel.Options.ForEach( new Action<Security>( PosChart.Options.Add ) );
            RefreshChart();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnSecurityChanged( bool isOption, Security security, IEnumerable<KeyValuePair<Level1Fields, object>> values )
        {
            MakeDirty();
        }        
    }
}
