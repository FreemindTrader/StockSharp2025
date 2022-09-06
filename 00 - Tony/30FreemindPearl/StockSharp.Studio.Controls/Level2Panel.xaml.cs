// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Level2Panel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Level2" )]
    [DescriptionLoc( "Level2Panel", false )]
    [VectorIcon( "Table" )]
    public partial class Level2Panel : BaseStudioControl, IComponentConnector
    {
        private static readonly DataType _dataType = DataType.Level1;
        private readonly HashSet<string> _askDefaultColumns = new HashSet<string>() { "Board", "BestAsk.Price", "BestAsk.Volume" };
        private readonly HashSet<string> _bidDefaultColumns = new HashSet<string>() { "Board", "BestBid.Price", "BestBid.Volume" };
        private readonly SubscriptionManager _subscriptionManager;
        private Security[ ] _securities;
        
        private BuySellSettings Settings
        {
            get
            {
                return this.BuySellPanel.Settings;
            }
        }

        public Level2Panel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            this.BuySellPanel.Host = ( IStudioControl )this;
            this.SecurityAsksGrid.LayoutChanged += RaiseChangedCommand;
            this.SecurityAsksGrid.SelectionChanged += new GridSelectionChangedEventHandler( this.SecuritiesCtrlOnSelectionChanged );
            this.SecurityBidsGrid.LayoutChanged += RaiseChangedCommand;
            this.SecurityBidsGrid.SelectionChanged += new GridSelectionChangedEventHandler( this.SecuritiesCtrlOnSelectionChanged );
            this.SecurityPicker.EditValueChanged += new EditValueChangedEventHandler( this.SecurityPickerSecuritySelected );
            this.SecurityPicker.SecurityProvider = BaseStudioControl.SecurityProvider;
            this.SecurityAsksGrid.MarketDataProvider = this.SecurityBidsGrid.MarketDataProvider = BaseStudioControl.MarketDataProvider;
            Level2Panel.SetVisibleColumns( ( GridControl )this.SecurityAsksGrid, ( ISet<string> )this._askDefaultColumns );
            Level2Panel.SetVisibleColumns( ( GridControl )this.SecurityBidsGrid, ( ISet<string> )this._bidDefaultColumns );
        }

        private static void SetVisibleColumns( GridControl grid, ISet<string> columns )
        {
            foreach ( GridColumn column in ( Collection<GridColumn> )grid.Columns )
                column.Visible = columns.Contains( column.FieldName );
        }

        private void SecurityPickerSecuritySelected( object sender, EditValueChangedEventArgs e )
        {
            Security[ ] array = BaseStudioControl.SecurityProvider.LookupByCode( this.SecurityPicker.SelectedSecurity.Code, new SecurityTypes?() ).ToArray<Security>();
            Security[ ] securities = this._securities;
            if ( securities != null )
                ( ( IEnumerable<Security> )securities ).ForEach<Security>( ( Action<Security> )( s => this._subscriptionManager.RemoveSubscriptions( s, Level2Panel._dataType ) ) );
            this.SecurityAsksGrid.Securities.Clear();
            this.SecurityBidsGrid.Securities.Clear();
            this._securities = array;
            if ( this._securities == null )
                return;
            ( ( IEnumerable<Security> )this._securities ).ForEach<Security>( ( Action<Security> )( s => this._subscriptionManager.CreateSubscription( s, Level2Panel._dataType, ( Action<Subscription> )null ) ) );
            this.SecurityAsksGrid.Securities.AddRange( ( IEnumerable<Security> )this._securities );
            this.SecurityBidsGrid.Securities.AddRange( ( IEnumerable<Security> )this._securities );
        }

        private void SecuritiesCtrlOnSelectionChanged( object sender, GridSelectionChangedEventArgs e )
        {
            this.Settings.Security = ( ( SecurityGrid )sender ).SelectedSecurity;
        }

        public override void Load( SettingsStorage storage )
        {
            string id = storage.GetValue<string>( "SecurityId", ( string )null );
            if ( id != null )
                this.SecurityPicker.SelectedSecurity = BaseStudioControl.SecurityProvider.LookupById( id );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "SecurityAsksGrid", ( SettingsStorage )null );
            if ( storage1 != null )
                this.SecurityAsksGrid.Load( storage1 );
            SettingsStorage storage2 = storage.GetValue<SettingsStorage>( "SecurityBidsGrid", ( SettingsStorage )null );
            if ( storage2 != null )
                this.SecurityBidsGrid.Load( storage2 );
            SettingsStorage storage3 = storage.GetValue<SettingsStorage>( "BuySellSettings", ( SettingsStorage )null );
            if ( storage3 == null )
                return;
            this.BuySellPanel.Load( storage3 );
        }

        public override void Save( SettingsStorage storage )
        {
            storage.SetValue<string>( "SecurityId", this.SecurityPicker.SelectedSecurity?.Id );
            storage.SetValue<SettingsStorage>( "SecurityAsksGrid", this.SecurityAsksGrid.Save() );
            storage.SetValue<SettingsStorage>( "SecurityBidsGrid", this.SecurityBidsGrid.Save() );
            storage.SetValue<SettingsStorage>( "BuySellSettings", this.BuySellPanel.Save() );
        }

        public override void Dispose( CloseReason reason )
        {
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }        
    }
}
