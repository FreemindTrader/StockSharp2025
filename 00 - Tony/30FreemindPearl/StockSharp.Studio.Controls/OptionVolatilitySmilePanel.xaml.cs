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
            this.InitializeComponent();
            this.FilterPanel.SubscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<ResetedCommand>( ( object )this, true, ( Action<ResetedCommand> )( cmd => this.ClearSmiles() ), ( Func<ResetedCommand, bool> )null );
            commandService.Register<SecuritiesRemovedCommand>( ( object )this, false, ( Action<SecuritiesRemovedCommand> )( cmd =>
                {
                    Security[ ] array = this.FilterPanel.RemoveOptions( cmd.Securities ).ToArray<Security>();
                    if ( array.Length == 0 )
                        return;
                    foreach ( Security security in array )
                        this._model.Remove( security );
                    this.MakeDirty();
                    this.RaiseChangedCommand();
                } ), ( Func<SecuritiesRemovedCommand, bool> )null );
            this._model = new OptionDeskModel()
            {
                MarketDataProvider = BaseStudioControl.MarketDataProvider,
                ExchangeInfoProvider = BaseStudioControl.ExchangeInfoProvider
            };
            this.FilterPanel.SecurityProvider = BaseStudioControl.SecurityProvider;
            this.FilterPanel.MarketDataProvider = BaseStudioControl.MarketDataProvider;
            this._putBidSmile = this.SmileChart.CreateSmile( "Put (B)", Colors.DarkRed, ChartIndicatorDrawStyles.Line, OptionVolatilitySmilePanel._putBidSmileId );
            this._putAskSmile = this.SmileChart.CreateSmile( "Put (A)", Colors.Red, ChartIndicatorDrawStyles.Line, OptionVolatilitySmilePanel._putAskSmileId );
            this._putLastSmile = this.SmileChart.CreateSmile( "Put (L)", Colors.OrangeRed, ChartIndicatorDrawStyles.Line, OptionVolatilitySmilePanel._putLastSmileId );
            this._callBidSmile = this.SmileChart.CreateSmile( "Call (B)", Colors.GreenYellow, ChartIndicatorDrawStyles.Line, OptionVolatilitySmilePanel._callBidSmileId );
            this._callAskSmile = this.SmileChart.CreateSmile( "Call (A)", Colors.DarkGreen, ChartIndicatorDrawStyles.Line, OptionVolatilitySmilePanel._callAskSmileId );
            this._callLastSmile = this.SmileChart.CreateSmile( "Call (L)", Colors.DarkOliveGreen, ChartIndicatorDrawStyles.Line, OptionVolatilitySmilePanel._callLastSmileId );

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
            lock ( this._needRefreshLock )
                this._needRefresh = true;
        }

        public override void Dispose()
        {
            this._refreshTimer.Stop();
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<ResetedCommand>( ( object )this );
            commandService.UnRegister<SecuritiesRemovedCommand>( ( object )this );
            this.FilterPanel.Dispose();
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
            this._putBidSmile.Clear();
            this._putAskSmile.Clear();
            this._putLastSmile.Clear();
            this._callBidSmile.Clear();
            this._callAskSmile.Clear();
            this._callLastSmile.Clear();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.FilterPanel.Load( storage.GetValue<SettingsStorage>( "FilterPanel", ( SettingsStorage )null ) );
            this.SmileChart.Load( storage.GetValue<SettingsStorage>( "SmileChart", ( SettingsStorage )null ) );
            KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement> keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>( ( Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool> )( e => e.Value.Id == OptionVolatilitySmilePanel._putBidSmileId ) );
            this._putBidSmile = keyValuePair.Key;
            keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>( ( Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool> )( e => e.Value.Id == OptionVolatilitySmilePanel._putAskSmileId ) );
            this._putAskSmile = keyValuePair.Key;
            keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>( ( Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool> )( e => e.Value.Id == OptionVolatilitySmilePanel._putLastSmileId ) );
            this._putLastSmile = keyValuePair.Key;
            keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>( ( Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool> )( e => e.Value.Id == OptionVolatilitySmilePanel._callBidSmileId ) );
            this._callBidSmile = keyValuePair.Key;
            keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>( ( Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool> )( e => e.Value.Id == OptionVolatilitySmilePanel._callAskSmileId ) );
            this._callAskSmile = keyValuePair.Key;
            keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>( ( Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool> )( e => e.Value.Id == OptionVolatilitySmilePanel._callLastSmileId ) );
            this._callLastSmile = keyValuePair.Key;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "FilterPanel", this.FilterPanel.Save() );
            storage.SetValue<SettingsStorage>( "SmileChart", this.SmileChart.Save() );
        }

        private void RefreshModel()
        {
            OptionDeskModel model = this._model;
            DateTime? currentDate = this.FilterPanel.CurrentDate;
            DateTimeOffset? currentTime = currentDate.HasValue ? new DateTimeOffset?( ( DateTimeOffset )currentDate.GetValueOrDefault() ) : new DateTimeOffset?();
            Decimal? assetPrice = this.FilterPanel.AssetPrice;
            model.Refresh( currentTime, assetPrice );
            this.ClearSmiles();
            foreach ( OptionDeskRow row in this._model.Rows )
            {
                Decimal? strike = row.Strike;
                if ( strike.HasValue )
                {
                    OptionVolatilitySmilePanel.TryAddSmileItem( this._callBidSmile, strike.Value, row.Call?.ImpliedVolatilityBestBid );
                    OptionVolatilitySmilePanel.TryAddSmileItem( this._callAskSmile, strike.Value, row.Call?.ImpliedVolatilityBestAsk );
                    OptionVolatilitySmilePanel.TryAddSmileItem( this._callLastSmile, strike.Value, row.Call?.ImpliedVolatilityLastTrade );
                    OptionVolatilitySmilePanel.TryAddSmileItem( this._putBidSmile, strike.Value, row.Put?.ImpliedVolatilityBestBid );
                    OptionVolatilitySmilePanel.TryAddSmileItem( this._putAskSmile, strike.Value, row.Put?.ImpliedVolatilityBestAsk );
                    OptionVolatilitySmilePanel.TryAddSmileItem( this._putLastSmile, strike.Value, row.Put?.ImpliedVolatilityLastTrade );
                }
            }
        }

        private void FilterPanel_OnUnderlyingAssetChanged()
        {
            this._model.UnderlyingAsset = this.FilterPanel.UnderlyingAsset;
            if ( this._model.UnderlyingAsset != null )
                ( ( IEnumerable<Security> )this.FilterPanel.Options ).ForEach<Security>( new Action<Security>( this._model.Add ) );
            this.Title = LocalizedStrings.VolatilitySmile + " " + this._model.UnderlyingAsset?.ToString();
            this.RefreshModel();
            this.RaiseChangedCommand();
        }

        private void FilterPanel_OnFilterChanged()
        {
            this.RefreshModel();
            this.RaiseChangedCommand();
        }

        private void FilterPanel_OnUseBlackModelChanged()
        {
            this._model.UseBlackModel = this.FilterPanel.UseBlackModel;
            this.RefreshModel();
            this.RaiseChangedCommand();
        }

        private void FilterPanel_OnOptionsChanged()
        {
            this._model.Clear();
            ( ( IEnumerable<Security> )this.FilterPanel.Options ).ForEach<Security>( new Action<Security>( this._model.Add ) );
            this.RefreshModel();
            this.RaiseChangedCommand();
        }

        private void FilterPanel_OnSecurityChanged(
          bool isOption,
          Security security,
          IEnumerable<KeyValuePair<Level1Fields, object>> values )
        {
            this.MakeDirty();
        }        
    }
}
