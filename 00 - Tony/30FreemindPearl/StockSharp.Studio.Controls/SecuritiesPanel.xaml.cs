// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.SecuritiesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Securities" )]
    [DescriptionLoc( "Securities", true )]
    [VectorIcon( "Certificate" )]
    [Doc( "topics/Terminal_Securities.html" )]
    public partial class SecuritiesPanel : BaseStudioControl, IComponentConnector
    {
        private static readonly DataType _dataType = DataType.Level1;
        private static readonly string[ ] _defaultColumns = new string[14] { "LastTrade.Price", "LastTrade.Volume", "LastTrade.Time", "BestBid.Price", "BestBid.Volume", "BestAsk.Price", "BestAsk.Volume", "OpenPrice", "HighPrice", "LowPrice", "ClosePrice", "Volume", "Turnover", "ExpiryDate" };
        private readonly SubscriptionManager _subscriptionManager;

        public SecuritiesPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            ( ( IEnumerable<string> )SecuritiesPanel._defaultColumns ).ForEach<string>( ( Action<string> )( c => this.SecurityPicker.SetColumnVisibility( c, Visibility.Visible ) ) );
            this.SecurityPicker.GridChanged += RaiseChangedCommand;
            this.AlertBtn.SchemaChanged += RaiseChangedCommand;
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<BindCommand>( ( object )this, true, ( Action<BindCommand> )( cmd =>
                {
                    if ( !cmd.CheckControl( ( IStudioControl )this ) )
                        return;
                    this.SecurityPicker.PriceChartDataProvider = StudioServicesRegistry.PriceChartDataProvider;
                } ), ( Func<BindCommand, bool> )null );
            commandService.Register<SecuritiesRemovedCommand>( ( object )this, false, ( Action<SecuritiesRemovedCommand> )( cmd =>
                {
                    bool flag = false;
                    foreach ( Security security in cmd.Securities )
                    {
                        if ( this.SecurityPicker.Securities.Remove( security ) )
                        {
                            this._subscriptionManager.RemoveSubscriptions( security, SecuritiesPanel._dataType );
                            flag = true;
                        }
                    }
                    if ( !flag )
                        return;
                    this.RaiseChangedCommand();
                } ), ( Func<SecuritiesRemovedCommand, bool> )null );
            commandService.Register<FirstInitSecuritiesCommand>( ( object )this, true, ( Action<FirstInitSecuritiesCommand> )( cmd =>
                {
                    this.SecurityPicker.Securities.AddRange( cmd.Securities );
                    cmd.Securities.ForEach<Security>( ( Action<Security> )( s => this._subscriptionManager.CreateSubscription( s, SecuritiesPanel._dataType, ( Action<Subscription> )null ) ) );
                    this.RaiseChangedCommand();
                } ), ( Func<FirstInitSecuritiesCommand, bool> )null );
            commandService.Register<EntityCommand<Level1ChangeMessage>>( ( object )this, false, ( Action<EntityCommand<Level1ChangeMessage>> )( cmd =>
                {
                    Dictionary<Security, Level1ChangeMessage> dictionary = new Dictionary<Security, Level1ChangeMessage>();
                    foreach ( Level1ChangeMessage entity in cmd.Entities )
                    {
                        Security key = BaseStudioControl.SecurityProvider.LookupById( entity.SecurityId );
                        if ( key != null )
                        {
                            Level1ChangeMessage level1ChangeMessage;
                            if ( dictionary.TryGetValue( key, out level1ChangeMessage ) )
                            {
                                foreach ( KeyValuePair<Level1Fields, object> change in ( IEnumerable<KeyValuePair<Level1Fields, object>> )entity.Changes )
                                    level1ChangeMessage.Changes[change.Key] = change.Value;
                            }
                            else
                                dictionary.Add( key, entity );
                        }
                    }
                    this.SecurityPicker.ProcessLevel1( ( IDictionary<Security, Level1ChangeMessage> )dictionary );
                } ), ( Func<EntityCommand<Level1ChangeMessage>, bool> )null );
            this.WhenLoaded( ( Action )( () => new RequestBindSource( ( IStudioControl )this ).SyncProcess( ( object )this ) ) );
        }

        private void RaiseSelectedCommand()
        {
            new SelectCommand<Security>( this.SecurityPicker.SelectedSecurity, false ).Process( ( object )this, false );
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<BindCommand>( ( object )this );
            commandService.UnRegister<SecuritiesRemovedCommand>( ( object )this );
            commandService.UnRegister<EntityCommand<Level1ChangeMessage>>( ( object )this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( ( object )this );
            if ( reason == CloseReason.CloseWindow )
                this.AlertBtn.Dispose();
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "SecurityPicker", this.SecurityPicker.Save() );
            storage.SetValue<string[ ]>( "Securities", this.SecurityPicker.Securities.LookupAll().Select<Security, string>( ( Func<Security, string> )( s => s.Id ) ).ToArray<string>() );
            storage.SetValue<SettingsStorage>( "AlertBtn", this.AlertBtn.Save() );
            storage.SetValue<string>( "BarManager", this.BarManager.SaveDevExpressControl() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "SecurityPicker", ( SettingsStorage )null );
            if ( storage1 != null )
                this.SecurityPicker.Load( storage1 );
            this.SecurityPicker.Securities.Clear();
            foreach ( string id in storage.GetValue<string[ ]>( "Securities", Array.Empty<string>() ) )
            {
                Security security = BaseStudioControl.SecurityProvider.LookupById( id );
                if ( security != null )
                {
                    this.SecurityPicker.Securities.Add( security );
                    this._subscriptionManager.CreateSubscription( security, SecuritiesPanel._dataType, ( Action<Subscription> )null );
                }
            }
            SettingsStorage storage2 = storage.GetValue<SettingsStorage>( "AlertBtn", ( SettingsStorage )null );
            if ( storage2 != null )
                this.AlertBtn.Load( storage2 );
            string settings = storage.GetValue<string>( "BarManager", ( string )null );
            if ( settings == null )
                return;
            this.BarManager.LoadDevExpressControl( settings );
        }

        private void SecurityPicker_SecuritySelected( Security security )
        {
            this.RaiseSelectedCommand();
            this.RemoveSecurities.IsEnabled = this.SecurityPicker.SelectedSecurity != null;
        }

        private void AddSecurities_OnClick( object sender, ItemClickEventArgs e )
        {
            IEnumerable<Security> securities = this.SecurityPicker.Securities.LookupAll();
            SecuritiesWindowEx wnd = new SecuritiesWindowEx() { SecurityProvider = BaseStudioControl.SecurityProvider, SelectedSecurities = securities };
            if ( !wnd.ShowModal( ( DependencyObject )this ) )
                return;
            Security[ ] array1 = securities.Except<Security>( wnd.SelectedSecurities ).ToArray<Security>();
            Security[ ] array2 = wnd.SelectedSecurities.Except<Security>( securities ).ToArray<Security>();
            if ( array1.Length == 0 && array2.Length == 0 )
                return;
            this.SecurityPicker.Securities.RemoveRange( ( IEnumerable<Security> )array1 );
            this.SecurityPicker.Securities.AddRange( ( IEnumerable<Security> )array2 );
            ( ( IEnumerable<Security> )array1 ).ForEach<Security>( ( Action<Security> )( s => this._subscriptionManager.RemoveSubscriptions( s, SecuritiesPanel._dataType ) ) );
            ( ( IEnumerable<Security> )array2 ).ForEach<Security>( ( Action<Security> )( s => this._subscriptionManager.CreateSubscription( s, SecuritiesPanel._dataType, ( Action<Subscription> )null ) ) );
            this.RaiseChangedCommand();
        }

        private void RemoveSecurities_OnClick( object sender, ItemClickEventArgs e )
        {
            Security[ ] array = this.SecurityPicker.SelectedSecurities.ToArray<Security>();
            foreach ( Security security in array )
                this._subscriptionManager.RemoveSubscriptions( security, SecuritiesPanel._dataType );
            this.SecurityPicker.Securities.RemoveRange( ( IEnumerable<Security> )array );
            this.RaiseChangedCommand();
        }


    }
}
