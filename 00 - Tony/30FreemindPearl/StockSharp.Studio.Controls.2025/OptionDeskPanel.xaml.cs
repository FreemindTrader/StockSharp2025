﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OptionDeskPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "OptionDeskPanel", Name = "OptionDesk", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Table" )]
    [Doc( "topics/terminal/user_interface/components/option_desk.html" )]
    public partial class OptionDeskPanel : BaseStudioControl
    {
        private readonly SyncObject _needRefreshLock = new SyncObject();
        private bool _needRefresh;
        private readonly DispatcherTimer _refreshTimer;
        private readonly OptionDeskModel _model;
        
        public OptionDeskPanel()
        {
            this.InitializeComponent();
            this.FilterPanel.SubscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            this.Register<ResetedCommand>(  this, true, ( Action<ResetedCommand> ) ( cmd =>
            {
                this._model.Clear();
                CollectionHelper.ForEach<Security>(  this.FilterPanel.Options,  new Action<Security>( this._model.Add ) );
                this.MakeDirty();
            } ), ( Func<ResetedCommand, bool> ) null );
            this.Register<EntitiesRemovedCommand<Security>>(  this, false, ( Action<EntitiesRemovedCommand<Security>> ) ( cmd =>
            {
                Security[] array = this.FilterPanel.RemoveOptions(cmd.Entities).ToArray<Security>();
                if ( array.Length == 0 )
                    return;
                foreach ( Security security in array )
                    this._model.Remove( security );
                this.MakeDirty();
                this.RaiseChangedCommand();
            } ), ( Func<EntitiesRemovedCommand<Security>, bool> ) null );
            this._model = new OptionDeskModel()
            {
                MarketDataProvider = BaseStudioControl.MarketDataProvider,
                ExchangeInfoProvider = BaseStudioControl.ExchangeInfoProvider
            };
            this.Desk.Model = this._model;
            this.FilterPanel.SecurityProvider = BaseStudioControl.SecurityProvider;
            this.FilterPanel.MarketDataProvider = BaseStudioControl.MarketDataProvider;
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = ( TimeSpan.FromSeconds( 5.0 ) );
            this._refreshTimer = dispatcherTimer;
            this._refreshTimer.Tick += ( ( ( s, e ) =>
            {
                lock ( this._needRefreshLock )
                {
                    if ( !this._needRefresh )
                        return;
                    this._needRefresh = false;
                }
                this.Desk.BeginEndUpdate( new Action( this.RefreshModel ) );
            } ) );
            this._refreshTimer.Start();
        }

        private void MakeDirty()
        {
            lock ( this._needRefreshLock )
                this._needRefresh = true;
        }

        private void OpenOptionDepth( Security option )
        {
            OpenWindowCommand command = new OpenWindowCommand(typeof (ScalpingMarketDepthControl), true);
            command.SyncProcess(  this );
            ValueTuple<IStudioControl, bool> result = command.Result;
            IStudioControl studioControl = result.Item1;
            bool flag = result.Item2;
            ScalpingMarketDepthControl marketDepthControl = studioControl as ScalpingMarketDepthControl;
            if ( !( marketDepthControl != null & flag ) )
                return;
            marketDepthControl.Settings.Security = option;
        }

        private void Desk_OnItemDoubleClick( object sender, ItemDoubleClickEventArgs e )
        {
            OptionDeskRow row = (OptionDeskRow) e.Row;
            if ( row.Call != null && e.Column.FieldName.Contains( "Call" ) )
            {
                this.OpenOptionDepth( row.Call.Option );
            }
            else
            {
                if ( row.Put == null || !e.Column.FieldName.Contains( "Put" ) )
                    return;
                this.OpenOptionDepth( row.Put.Option );
            }
        }

        private void Desk_OnPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            this.RaiseChangedCommand();
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "FilterPanel",  PersistableHelper.Save( ( IPersistable ) this.FilterPanel ) );
            storage.SetValue<SettingsStorage>( "Desk",  PersistableHelper.Save( ( IPersistable ) this.Desk ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.FilterPanel, storage, "FilterPanel" );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.Desk, storage, "Desk" );
        }

        public override void Dispose( CloseReason reason )
        {
            this._refreshTimer.Stop();
            this.FilterPanel.Dispose();
            base.Dispose( reason );
        }

        private void RefreshModel()
        {
            OptionDeskModel model = this._model;
            DateTime? currentDate = this.FilterPanel.CurrentDate;
            DateTimeOffset? currentTime = currentDate.HasValue ? new DateTimeOffset?((DateTimeOffset) currentDate.GetValueOrDefault()) : new DateTimeOffset?();
            Decimal? assetPrice = this.FilterPanel.AssetPrice;
            model.Refresh( currentTime, assetPrice );
        }

        protected override void OnActiveLanguageChanged()
        {
            this.UpdateTitle();
        }

        private void UpdateTitle()
        {
            this.Title = LocalizedStrings.OptionDesk + " " + (  this._model?.UnderlyingAsset )?.ToString();
        }

        private void FilterPanel_OnUnderlyingAssetChanged()
        {
            this._model.UnderlyingAsset = this.FilterPanel.UnderlyingAsset;
            if ( this._model.UnderlyingAsset != null )
                CollectionHelper.ForEach<Security>(  this.FilterPanel.Options,  new Action<Security>( this._model.Add ) );
            this.UpdateTitle();
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
            CollectionHelper.ForEach<Security>(  this.FilterPanel.Options,  new Action<Security>( this._model.Add ) );
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
