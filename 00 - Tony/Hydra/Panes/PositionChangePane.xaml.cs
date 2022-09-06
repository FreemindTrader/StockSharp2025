using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo.Storages;
using StockSharp.Hydra.Controls;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    public partial class PositionChangePane : DataPane, IComponentConnector
    {
        private readonly Dictionary<PositionChangeTypes, GridColumn> _columns = new Dictionary<PositionChangeTypes, GridColumn>();
        
        public PositionChangePane()
        {
            InitializeComponent();
            if ( this.IsDesignMode() )
                return;
            Init( ExportBtn, MainGrid, SelectSecurityBtn, new Func<SecurityId, DateTime?, DateTime?, bool, IEnumerable>( GetMessages ) );
            SetTitlePrefix( LocalizedStrings.Str972 );
            ChangeTypesCtrl.SelectedTypes = Enumerator.GetValues<PositionChangeTypes>();
            foreach ( GridColumn column in ( Collection<GridColumn> )FindedChanges.Columns )
            {
                PositionChangeTypes key;
                if ( column.FieldName.TryParse( out key, true ) )
                    _columns.Add( key, column );
            }
        }

        protected override DataType DataType
        {
            get
            {
                return DataType.PositionChanges;
            }
        }

        private IEnumerable<PositionChangeMessage> GetMessages(
          SecurityId securityId,
          DateTime? from,
          DateTime? to,
          bool exporting )
        {
            IEnumerable<PositionChangeMessage> source = InternalGetMessages( securityId, from, to );
            TimeZoneInfo tz = TimeZone.TimeZone;
            if ( tz != null )
                source = source.Select( m =>
                {
                    if ( !m.ServerTime.IsDefault() )
                        m.ServerTime = m.ServerTime.Convert( tz );
                    if ( !m.LocalTime.IsDefault() )
                        m.LocalTime = m.LocalTime.Convert( tz );
                    return m;
                } );
            return source;
        }

        private IEnumerable<PositionChangeMessage> InternalGetMessages(
          SecurityId securityId,
          DateTime? from,
          DateTime? to )
        {
            PositionChangeTypes[ ] excludedTypes = Enumerator.GetValues<PositionChangeTypes>().Except( ChangeTypesCtrl.SelectedTypes ).ToArray();
            IMarketDataStorage<PositionChangeMessage> positionMessageStorage = StorageRegistry.GetPositionMessageStorage( securityId, Drive, StorageFormat );
            DateTime? nullable1 = from;
            DateTimeOffset? from1 = nullable1.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable1.GetValueOrDefault() ) : new DateTimeOffset?();
            DateTime? nullable2 = to;
            DateTimeOffset? to1 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
            return positionMessageStorage.Load( from1, to1 ).Select( m =>
            {
                excludedTypes.ForEach( t => m.Changes.Remove( t ) );
                return m;
            } ).Where( m => m.Changes.Any() );
        }

        private bool CheckExportTypes()
        {
            if ( ChangeTypesCtrl.SelectedTypes.Any() || CanDirectExport )
                return true;
            int num = ( int )new MessageBoxBuilder().Error().Owner( this ).Text( LocalizedStrings.Str2935 ).Show();
            return false;
        }

        protected override void ExportBtnOnExportStarted()
        {
            if ( !CheckExportTypes() )
                return;
            base.ExportBtnOnExportStarted();
        }

        private void OnDateValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Progress.ClearStatus();
        }

        private void FindClick( object sender, RoutedEventArgs e )
        {
            if ( !ValidateSettings() || !CheckExportTypes() )
                return;
            ObservableCollection<PositionChangeMessage> observableCollection = new ObservableCollection<PositionChangeMessage>();
            FindedChanges.ItemsSource = observableCollection;
            Progress.Load( GetAllValues<PositionChangeMessage>( new DateTime?(), new DateTime?(), false ), new Action<IEnumerable<PositionChangeMessage>>( ( observableCollection ).AddRange ), 10000, null, null );
        }

        private void ChangeTypesCtrl_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            ISet<PositionChangeTypes> set = ChangeTypesCtrl.SelectedTypes.ToSet();
            foreach ( KeyValuePair<PositionChangeTypes, GridColumn> column in _columns )
                column.Value.Visible = set.Contains( column.Key );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if ( storage.ContainsKey( "SelectedChangeTypes" ) )
                ChangeTypesCtrl.SelectedTypes = storage.GetValue<string>( "SelectedChangeTypes", null ).SplitByComma( false ).Select( s => s.To<PositionChangeTypes>() ).ToArray();
            FindedChanges.Load( storage.GetValue<SettingsStorage>( "FindedChanges", null ) );
            PositionChangeTypes[ ] array = ChangeTypesCtrl.SelectedTypes.ToArray();
            foreach ( KeyValuePair<PositionChangeTypes, GridColumn> column in _columns )
                column.Value.Visible = array.Contains( column.Key );
            TimeZone.TimeZone = storage.GetValue( "TimeZone", TimeZone.TimeZone );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "SelectedChangeTypes", ChangeTypesCtrl.SelectedTypes.Select( f => f.ToString() ).JoinComma() );
            storage.SetValue( "FindedChanges", FindedChanges.Save() );
            storage.SetValue( "TimeZone", TimeZone.TimeZone );
        }

        
    }
}
