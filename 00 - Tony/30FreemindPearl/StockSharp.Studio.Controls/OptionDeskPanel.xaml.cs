// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OptionDeskPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "OptionDesk" )]
    [DescriptionLoc( "OptionDeskPanel", false )]
    [VectorIcon( "Table" )]
    [Doc( "topics/Terminal_option_desk.html" )]
    public partial class OptionDeskPanel : BaseStudioControl, IComponentConnector
    {
        private readonly OptionDeskModel _model;
        private bool _needRefresh;
        private readonly SyncObject _needRefreshLock = new SyncObject();
        private readonly DispatcherTimer _refreshTimer;

        public OptionDeskPanel()
        {
            InitializeComponent();
            FilterPanel.SubscriptionManager = new SubscriptionManager( this );
            IStudioCommandService commandService = CommandService;
            commandService.Register<ResetedCommand>(
                                                                this,
                                                                true,
                                                                cmd =>
                                                                {
                                                                    _model.Clear();
                                                                    ( ( IEnumerable<Security> )FilterPanel.Options ).ForEach( new Action<Security>( _model.Add ) );
                                                                    MakeDirty();
                                                                },
                                                                null );

            commandService.Register<SecuritiesRemovedCommand>(
                                                                this,
                                                                false,
                                                                cmd =>
                                                                {
                                                                    Security[ ] array = FilterPanel.RemoveOptions( cmd.Securities ).ToArray();
                                                                    if ( array.Length == 0 )
                                                                    {
                                                                        return;
                                                                    }

                                                                    foreach ( Security security in array )
                                                                    {
                                                                        _model.Remove( security );
                                                                    }

                                                                    MakeDirty();
                                                                    RaiseChangedCommand();
                                                                },
                                                                null );
            _model = new OptionDeskModel()
            {
                MarketDataProvider = MarketDataProvider,
                ExchangeInfoProvider = ExchangeInfoProvider
            };
            Desk.Model = _model;
            FilterPanel.SecurityProvider = SecurityProvider;
            FilterPanel.MarketDataProvider = MarketDataProvider;

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
                Desk.BeginEndUpdate( new Action( RefreshModel ) );
            };
            _refreshTimer.Start();
        }

        private void Desk_OnItemDoubleClick( object sender, ItemDoubleClickEventArgs e )
        {
            OptionDeskRow row = ( OptionDeskRow )e.Row;
            if ( row.Call != null && e.Column.FieldName.Contains( "Call" ) )
            {
                OpenOptionDepth( row.Call.Option );
            }
            else
            {
                if ( row.Put == null || !e.Column.FieldName.Contains( "Put" ) )
                {
                    return;
                }

                OpenOptionDepth( row.Put.Option );
            }
        }

        private void Desk_OnPropertyChanged( object sender, PropertyChangedEventArgs e ) { RaiseChangedCommand(); }

        private void FilterPanel_OnFilterChanged()
        {
            RefreshModel();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnOptionsChanged()
        {
            _model.Clear();
            ( ( IEnumerable<Security> )FilterPanel.Options ).ForEach( new Action<Security>( _model.Add ) );
            RefreshModel();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnSecurityChanged(
            bool isOption,
            Security security,
            IEnumerable<KeyValuePair<Level1Fields, object>> values )
        { MakeDirty(); }

        private void FilterPanel_OnUnderlyingAssetChanged()
        {
            _model.UnderlyingAsset = FilterPanel.UnderlyingAsset;
            if ( _model.UnderlyingAsset != null )
            {
                ( ( IEnumerable<Security> )FilterPanel.Options ).ForEach( new Action<Security>( _model.Add ) );
            }

            Title = LocalizedStrings.OptionDesk + " " + _model.UnderlyingAsset?.ToString();
            RefreshModel();
            RaiseChangedCommand();
        }

        private void FilterPanel_OnUseBlackModelChanged()
        {
            _model.UseBlackModel = FilterPanel.UseBlackModel;
            RefreshModel();
            RaiseChangedCommand();
        }

        private void MakeDirty()
        {
            lock ( _needRefreshLock )
            {
                _needRefresh = true;
            }
        }

        private void OpenOptionDepth( Security option )
        {
            OpenWindowCommand command = new OpenWindowCommand(
                Guid.NewGuid().To<string>(),
                typeof( ScalpingMarketDepthControl ),
                true );
            command.SyncProcess( this );
            ScalpingMarketDepthControl result = command.Result as ScalpingMarketDepthControl;
            if ( result == null )
            {
                return;
            }

            result.Settings.Security = option;
        }

        private void RefreshModel()
        {
            OptionDeskModel model = _model;
            DateTime? currentDate = FilterPanel.CurrentDate;
            DateTimeOffset? currentTime = currentDate.HasValue
                ? new DateTimeOffset?( currentDate.GetValueOrDefault() )
                : new DateTimeOffset?();
            Decimal? assetPrice = FilterPanel.AssetPrice;
            model.Refresh( currentTime, assetPrice );
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            FilterPanel.Load( storage.GetValue<SettingsStorage>( nameof( FilterPanel ), null ) );
            Desk.Load( storage.GetValue<SettingsStorage>( nameof( Desk ), null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( nameof( FilterPanel ), FilterPanel.Save() );
            storage.SetValue( nameof( Desk ), Desk.Save() );
        }
    }
}
