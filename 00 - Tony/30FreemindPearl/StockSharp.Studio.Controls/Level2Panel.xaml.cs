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
                return BuySellPanel.Settings;
            }
        }

        public Level2Panel()
        {
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            BuySellPanel.Host = this;
            SecurityAsksGrid.LayoutChanged += RaiseChangedCommand;
            SecurityAsksGrid.SelectionChanged += new GridSelectionChangedEventHandler( SecuritiesCtrlOnSelectionChanged );
            SecurityBidsGrid.LayoutChanged += RaiseChangedCommand;
            SecurityBidsGrid.SelectionChanged += new GridSelectionChangedEventHandler( SecuritiesCtrlOnSelectionChanged );
            SecurityPicker.EditValueChanged += new EditValueChangedEventHandler( SecurityPickerSecuritySelected );
            SecurityPicker.SecurityProvider = SecurityProvider;
            SecurityAsksGrid.MarketDataProvider = SecurityBidsGrid.MarketDataProvider = MarketDataProvider;
            SetVisibleColumns( SecurityAsksGrid, _askDefaultColumns );
            SetVisibleColumns( SecurityBidsGrid, _bidDefaultColumns );
        }

        private static void SetVisibleColumns( GridControl grid, ISet<string> columns )
        {
            foreach ( GridColumn column in ( Collection<GridColumn> )grid.Columns )
                column.Visible = columns.Contains( column.FieldName );
        }

        private void SecurityPickerSecuritySelected( object sender, EditValueChangedEventArgs e )
        {
            Security[ ] array = SecurityProvider.LookupByCode( SecurityPicker.SelectedSecurity.Code, new SecurityTypes?() ).ToArray();
            Security[ ] securities = _securities;
            if ( securities != null )
                securities.ForEach( s => _subscriptionManager.RemoveSubscriptions( s, _dataType ) );
            SecurityAsksGrid.Securities.Clear();
            SecurityBidsGrid.Securities.Clear();
            _securities = array;
            if ( _securities == null )
                return;
            _securities.ForEach( s => _subscriptionManager.CreateSubscription( s, _dataType, null ) );
            SecurityAsksGrid.Securities.AddRange( _securities );
            SecurityBidsGrid.Securities.AddRange( _securities );
        }

        private void SecuritiesCtrlOnSelectionChanged( object sender, GridSelectionChangedEventArgs e )
        {
            Settings.Security = ( ( SecurityGrid )sender ).SelectedSecurity;
        }

        public override void Load( SettingsStorage storage )
        {
            string id = storage.GetValue<string>( "SecurityId", null );
            if ( id != null )
                SecurityPicker.SelectedSecurity = SecurityProvider.LookupById( id );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "SecurityAsksGrid", null );
            if ( storage1 != null )
                SecurityAsksGrid.Load( storage1 );
            SettingsStorage storage2 = storage.GetValue<SettingsStorage>( "SecurityBidsGrid", null );
            if ( storage2 != null )
                SecurityBidsGrid.Load( storage2 );
            SettingsStorage storage3 = storage.GetValue<SettingsStorage>( "BuySellSettings", null );
            if ( storage3 == null )
                return;
            BuySellPanel.Load( storage3 );
        }

        public override void Save( SettingsStorage storage )
        {
            storage.SetValue( "SecurityId", SecurityPicker.SelectedSecurity?.Id );
            storage.SetValue( "SecurityAsksGrid", SecurityAsksGrid.Save() );
            storage.SetValue( "SecurityBidsGrid", SecurityBidsGrid.Save() );
            storage.SetValue( "BuySellSettings", BuySellPanel.Save() );
        }

        public override void Dispose( CloseReason reason )
        {
            _subscriptionManager.Dispose();
            base.Dispose( reason );
        }        
    }
}
