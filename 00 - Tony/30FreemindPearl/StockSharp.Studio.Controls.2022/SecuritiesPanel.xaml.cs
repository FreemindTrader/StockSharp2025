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
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            _defaultColumns.ForEach( c => SecurityPicker.SetColumnVisibility( c, Visibility.Visible ) );
            SecurityPicker.GridChanged += RaiseChangedCommand;
            AlertBtn.SchemaChanged += RaiseChangedCommand;
            IStudioCommandService commandService = CommandService;
            commandService.Register<BindCommand>( this, true, cmd =>
                {
                    if ( !cmd.CheckControl( this ) )
                        return;
                    SecurityPicker.PriceChartDataProvider = StudioServicesRegistry.PriceChartDataProvider;
                }, null );
            commandService.Register<SecuritiesRemovedCommand>( this, false, cmd =>
                {
                    bool flag = false;
                    foreach ( Security security in cmd.Securities )
                    {
                        if ( SecurityPicker.Securities.Remove( security ) )
                        {
                            _subscriptionManager.RemoveSubscriptions( security, _dataType );
                            flag = true;
                        }
                    }
                    if ( !flag )
                        return;
                    RaiseChangedCommand();
                }, null );
            commandService.Register<FirstInitSecuritiesCommand>( this, true, cmd =>
                {
                    SecurityPicker.Securities.AddRange( cmd.Securities );
                    cmd.Securities.ForEach( s => _subscriptionManager.CreateSubscription( s, _dataType, null ) );
                    RaiseChangedCommand();
                }, null );
            commandService.Register<EntityCommand<Level1ChangeMessage>>( this, false, cmd =>
                {
                    Dictionary<Security, Level1ChangeMessage> dictionary = new Dictionary<Security, Level1ChangeMessage>();
                    foreach ( Level1ChangeMessage entity in cmd.Entities )
                    {
                        Security key = SecurityProvider.LookupById( entity.SecurityId );
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
                    SecurityPicker.ProcessLevel1( dictionary );
                }, null );
            WhenLoaded( () => new RequestBindSource( this ).SyncProcess( this ) );
        }

        private void RaiseSelectedCommand()
        {
            new SelectCommand<Security>( SecurityPicker.SelectedSecurity, false ).Process( this, false );
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<BindCommand>( this );
            commandService.UnRegister<SecuritiesRemovedCommand>( this );
            commandService.UnRegister<EntityCommand<Level1ChangeMessage>>( this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( this );
            if ( reason == CloseReason.CloseWindow )
                AlertBtn.Dispose();
            _subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "SecurityPicker", SecurityPicker.Save() );
            storage.SetValue( "Securities", SecurityPicker.Securities.LookupAll().Select( s => s.Id ).ToArray() );
            storage.SetValue( "AlertBtn", AlertBtn.Save() );
            storage.SetValue( "BarManager", BarManager.SaveDevExpressControl() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "SecurityPicker", null );
            if ( storage1 != null )
                SecurityPicker.Load( storage1 );
            SecurityPicker.Securities.Clear();
            foreach ( string id in storage.GetValue( "Securities", Array.Empty<string>() ) )
            {
                Security security = SecurityProvider.LookupById( id );
                if ( security != null )
                {
                    SecurityPicker.Securities.Add( security );
                    _subscriptionManager.CreateSubscription( security, _dataType, null );
                }
            }
            SettingsStorage storage2 = storage.GetValue<SettingsStorage>( "AlertBtn", null );
            if ( storage2 != null )
                AlertBtn.Load( storage2 );
            string settings = storage.GetValue<string>( "BarManager", null );
            if ( settings == null )
                return;
            BarManager.LoadDevExpressControl( settings );
        }

        private void SecurityPicker_SecuritySelected( Security security )
        {
            RaiseSelectedCommand();
            RemoveSecurities.IsEnabled = SecurityPicker.SelectedSecurity != null;
        }

        private void AddSecurities_OnClick( object sender, ItemClickEventArgs e )
        {
            IEnumerable<Security> securities = SecurityPicker.Securities.LookupAll();
            SecuritiesWindowEx wnd = new SecuritiesWindowEx() { SecurityProvider = SecurityProvider, SelectedSecurities = securities };
            if ( !wnd.ShowModal( this ) )
                return;
            Security[ ] array1 = securities.Except( wnd.SelectedSecurities ).ToArray();
            Security[ ] array2 = wnd.SelectedSecurities.Except( securities ).ToArray();
            if ( array1.Length == 0 && array2.Length == 0 )
                return;
            SecurityPicker.Securities.RemoveRange( array1 );
            SecurityPicker.Securities.AddRange( array2 );
            array1.ForEach( s => _subscriptionManager.RemoveSubscriptions( s, _dataType ) );
            array2.ForEach( s => _subscriptionManager.CreateSubscription( s, _dataType, null ) );
            RaiseChangedCommand();
        }

        private void RemoveSecurities_OnClick( object sender, ItemClickEventArgs e )
        {
            Security[ ] array = SecurityPicker.SelectedSecurities.ToArray();
            foreach ( Security security in array )
                _subscriptionManager.RemoveSubscriptions( security, _dataType );
            SecurityPicker.Securities.RemoveRange( array );
            RaiseChangedCommand();
        }


    }
}
