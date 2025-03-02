// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OptionVolatilitySmilePanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "VolatilitySmile" )]
    [DescriptionLoc( "VolatilitySmileChart", false )]
    [VectorIcon( "Parabola" )]
    [Doc( "topics/Terminal_smile_of_volatility.html" )]
    public partial class OptionVolatilitySmilePanel : BaseStudioControl, IComponentConnector
    {
        private static readonly Guid          _putBidSmileId   = "E969F589-8A8D-4024-BCE2-694E90F5A4EC".To<Guid>();
        private static readonly Guid          _putAskSmileId   = "1FD8934F-E575-4BAE-895F-BF60E8796631".To<Guid>();
        private static readonly Guid          _putLastSmileId  = "25533681-6933-476A-8A52-D962A0A64D65".To<Guid>();
        private static readonly Guid          _callBidSmileId  = "248D8DD8-F261-408E-A02A-BBFB2BEDCDC7".To<Guid>();
        private static readonly Guid          _callAskSmileId  = "547B3BCC-6CF0-406F-A84F-956C46C8B304".To<Guid>();
        private static readonly Guid          _callLastSmileId = "E4541E58-245B-4BDE-9FB9-ADC79B39D74F".To<Guid>();
        private readonly SyncObject           _needRefreshLock = new SyncObject();
        private readonly OptionDeskModel      _model;
        private ICollection<LineData<double>> _putBidSmile;
        private ICollection<LineData<double>> _putAskSmile;
        private ICollection<LineData<double>> _putLastSmile;
        private ICollection<LineData<double>> _callBidSmile;
        private ICollection<LineData<double>> _callAskSmile;
        private ICollection<LineData<double>> _callLastSmile;
        private bool                          _needRefresh;
        private readonly DispatcherTimer      _refreshTimer;
        

        public OptionVolatilitySmilePanel()
        {
            InitializeComponent();
            FilterPanel.SubscriptionManager = new SubscriptionManager( this );
            IStudioCommandService commandService = CommandService;
            commandService.Register<ResetedCommand>( this, true, cmd => ClearSmiles(), null );
            commandService.Register<SecuritiesRemovedCommand>( this, false, cmd =>
                {
                    Security[ ] array = FilterPanel.RemoveOptions( cmd.Securities ).ToArray();
                    if ( array.Length == 0 )
                        return;
                    foreach ( Security security in array )
                        _model.Remove( security );
                    MakeDirty();
                    RaiseChangedCommand();
                }, null );
            _model = new OptionDeskModel()
            {
                MarketDataProvider = MarketDataProvider,
                ExchangeInfoProvider = ExchangeInfoProvider
            };
            FilterPanel.SecurityProvider = SecurityProvider;
            FilterPanel.MarketDataProvider = MarketDataProvider;
            _putBidSmile = SmileChart.CreateSmile( "Put (B)", Colors.DarkRed, ChartIndicatorDrawStyles.Line, _putBidSmileId );
            _putAskSmile = SmileChart.CreateSmile( "Put (A)", Colors.Red, ChartIndicatorDrawStyles.Line, _putAskSmileId );
            _putLastSmile = SmileChart.CreateSmile( "Put (L)", Colors.OrangeRed, ChartIndicatorDrawStyles.Line, _putLastSmileId );
            _callBidSmile = SmileChart.CreateSmile( "Call (B)", Colors.GreenYellow, ChartIndicatorDrawStyles.Line, _callBidSmileId );
            _callAskSmile = SmileChart.CreateSmile( "Call (A)", Colors.DarkGreen, ChartIndicatorDrawStyles.Line, _callAskSmileId );
            _callLastSmile = SmileChart.CreateSmile( "Call (L)", Colors.DarkOliveGreen, ChartIndicatorDrawStyles.Line, _callLastSmileId );

            _refreshTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds( 5.0 ) };
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
                RefreshModel();
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

        private static void TryAddSmileItem(
          ICollection<LineData<double>> smile,
          Decimal strike,
          Decimal? iv )
        {
            if ( !iv.HasValue )
                return;
            smile.Add( new LineData<double>()
            {
                X = ( double )strike,
                Y = iv.Value
            } );
        }

        private void ClearSmiles()
        {
            _putBidSmile.Clear();
            _putAskSmile.Clear();
            _putLastSmile.Clear();
            _callBidSmile.Clear();
            _callAskSmile.Clear();
            _callLastSmile.Clear();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            FilterPanel.Load( storage.GetValue<SettingsStorage>( "FilterPanel", null ) );
            SmileChart.Load( storage.GetValue<SettingsStorage>( "SmileChart", null ) );
            KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement> keyValuePair = SmileChart.Elements.First( e => e.Value.Id == _putBidSmileId );
            _putBidSmile = keyValuePair.Key;
            keyValuePair = SmileChart.Elements.First( e => e.Value.Id == _putAskSmileId );
            _putAskSmile = keyValuePair.Key;
            keyValuePair = SmileChart.Elements.First( e => e.Value.Id == _putLastSmileId );
            _putLastSmile = keyValuePair.Key;
            keyValuePair = SmileChart.Elements.First( e => e.Value.Id == _callBidSmileId );
            _callBidSmile = keyValuePair.Key;
            keyValuePair = SmileChart.Elements.First( e => e.Value.Id == _callAskSmileId );
            _callAskSmile = keyValuePair.Key;
            keyValuePair = SmileChart.Elements.First( e => e.Value.Id == _callLastSmileId );
            _callLastSmile = keyValuePair.Key;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "FilterPanel", FilterPanel.Save() );
            storage.SetValue( "SmileChart", SmileChart.Save() );
        }

        private void RefreshModel()
        {
            OptionDeskModel model = _model;
            DateTime? currentDate = FilterPanel.CurrentDate;
            DateTimeOffset? currentTime = currentDate.HasValue ? new DateTimeOffset?( ( DateTimeOffset )currentDate.GetValueOrDefault() ) : new DateTimeOffset?();
            Decimal? assetPrice = FilterPanel.AssetPrice;
            model.Refresh( currentTime, assetPrice );
            ClearSmiles();
            foreach ( OptionDeskRow row in _model.Rows )
            {
                Decimal? strike = row.Strike;
                if ( strike.HasValue )
                {
                    TryAddSmileItem( _callBidSmile, strike.Value, row.Call?.ImpliedVolatilityBestBid );
                    TryAddSmileItem( _callAskSmile, strike.Value, row.Call?.ImpliedVolatilityBestAsk );
                    TryAddSmileItem( _callLastSmile, strike.Value, row.Call?.ImpliedVolatilityLastTrade );
                    TryAddSmileItem( _putBidSmile, strike.Value, row.Put?.ImpliedVolatilityBestBid );
                    TryAddSmileItem( _putAskSmile, strike.Value, row.Put?.ImpliedVolatilityBestAsk );
                    TryAddSmileItem( _putLastSmile, strike.Value, row.Put?.ImpliedVolatilityLastTrade );
                }
            }
        }

        private void FilterPanel_OnUnderlyingAssetChanged()
        {
            _model.UnderlyingAsset = FilterPanel.UnderlyingAsset;
            if ( _model.UnderlyingAsset != null )
                FilterPanel.Options.ForEach( new Action<Security>( _model.Add ) );
            Title = LocalizedStrings.VolatilitySmile + " " + _model.UnderlyingAsset?.ToString();
            RefreshModel();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnFilterChanged()
        {
            RefreshModel();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnUseBlackModelChanged()
        {
            _model.UseBlackModel = FilterPanel.UseBlackModel;
            RefreshModel();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnOptionsChanged()
        {
            _model.Clear();
            FilterPanel.Options.ForEach( new Action<Security>( _model.Add ) );
            RefreshModel();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnSecurityChanged(
          bool isOption,
          Security security,
          IEnumerable<KeyValuePair<Level1Fields, object>> values )
        {
            MakeDirty();
        }        
    }
}
